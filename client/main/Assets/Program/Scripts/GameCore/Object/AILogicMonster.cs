using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;

namespace DreamFaction.SkillCore
{
    /// <summary>
    /// 怪物AI逻辑类
    /// </summary>
    public class AILogicMonster
    {
        private class MonsterSpellLogicPackage
        {
            public ObjectMonster m_Caster;
            public SpellAILogic m_SpellAILogic;

            public MonsterSpellLogicPackage(ObjectMonster caster,SpellAILogic logic)
            {
                m_Caster = caster;
                m_SpellAILogic = logic;
            }
            public int GetPriority()
            {
                return m_SpellAILogic.GetPriority();
            }
        }

        private static AILogicMonster m_Inst;
        private float m_AITime = 0;                                     //AI检测用时间间隔
        private float m_AITimeChange = 0;                               //AI计时

        private List<MonsterSpellLogicPackage> m_SpellList;
        private int m_MaxPriority;

        private List<SpellAILogic> SpellAILogicPool = new List<SpellAILogic>();//技能ai逻辑池 [11/4/2015 Zmy]
        public static AILogicMonster GetInstance()
        {
            if (m_Inst == null)
                m_Inst = new AILogicMonster();
            return m_Inst;
        }
        private AILogicMonster()
        {
            m_SpellList = new List<MonsterSpellLogicPackage>();
            m_MaxPriority = -1;
        }

        private void ReturnData()
        {
            m_SpellList.Clear();
            m_MaxPriority = -1;

            for (int i = 0; i < SpellAILogicPool.Count; i++)
            {
                SpellAILogicPool[i].ClearUp();
            }
        }

        private void RefreshSpellList(List<ObjectMonster> monsterList)
        {
            ReturnData();

            for (int i = 0; i < monsterList.Count; ++i)
            {
                ObjectMonster _monster = monsterList[i];
                if (!_monster.IsSkillAttack() && _monster.IsAlive() && _monster.CheckSelfCanCastSkill())//判断该技能的硬性释放条件
                {
                    AnalyseObjectMonster(_monster);
                }
            }
        }
        private void AnalyseObjectMonster(ObjectMonster monster)
        {
            var _spellInfoArray = monster.Getm_SpellInfo();

            for (int i = 0; i < _spellInfoArray.Count; i++)
            {
                if (_spellInfoArray[i] != null && _spellInfoArray[i].GetSpellID() != -1)
                {
                    SpellInfo _tempInfo = _spellInfoArray[i];
                    for (int j = 0; j < _tempInfo.GetSpellRow().getNormalTemplate().Length; ++j)
                    {
                        if (monster.CheckSkillCondtion(_tempInfo))
                        {
                            SpellAILogic tempLogic = GenerateSubsetFromPool(_tempInfo.GetSpellRow().getNormalTemplate()[j], 
                                                                        _tempInfo.GetSpellRow().getNormalpriority()[j],
                                                                        monster, _tempInfo, m_AITime);
                            if (tempLogic.IsAILogicReady() && tempLogic.GetPriority() >= m_MaxPriority)
                            {
                                m_MaxPriority = tempLogic.GetPriority();
                                m_SpellList.Add(new MonsterSpellLogicPackage(monster, tempLogic));
                            }
                        }
                    }
                }
            }
        }
        private void ProcessSpellLogic()
        {
            if (m_SpellList.Count <= 0 || m_MaxPriority < 0)
                return;

            m_SpellList.Sort((left, right) => right.GetPriority() - left.GetPriority());
            int _boundary;
            for (_boundary = 0; _boundary < m_SpellList.Count; _boundary++)
            {
                if (m_SpellList[_boundary].GetPriority() < m_MaxPriority)
                    break;
            }
            int _randmun = 0;
            if(_boundary>1)
                _randmun = Random.Range(0, _boundary);
            CastSpell(m_SpellList[_randmun]);
        }
        private void CastSpell(MonsterSpellLogicPackage logicPackage)
        {
            logicPackage.m_Caster.SetSkillLockTarget(logicPackage.m_SpellAILogic.GetSkillTag());
            if (logicPackage.m_SpellAILogic.GetSkillInfo() == null)
            {
                return;
            }

                        //        Debug.Log("Guid:" + logicPackage.m_Caster.GetGuid().GUID_value +
                        //"  skillID:" + logicPackage.m_SpellAILogic.GetSkillInfo().GetSpellID() +
                        //"  Type:" + logicPackage.m_SpellAILogic.GetSkillInfo().GetSpellType());
            logicPackage.m_Caster.SetSpellInfoNow(logicPackage.m_SpellAILogic.GetSkillInfo());
            if (logicPackage.m_Caster.OnPre_CheckUseSkillCondtion())
            {
                logicPackage.m_Caster.SetObjectActionState(ObjectCreature.ObjectActionState.skillAttack);
                string name = logicPackage.m_SpellAILogic.GetSkillInfo().GetSpellRow().getSkillNameRes();
                SkillShowNamePackage package = new SkillShowNamePackage(logicPackage.m_Caster.GetGuid(), name);
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_ShowSkillName, package);

                if (logicPackage.m_SpellAILogic.GetSkillInfo().GetSpellRow().getSkillhittype() == 1)// 治疗技能不加成怒气 [10/17/2015 Zmy]
                    return;
                // 怪物攻击怒气加成 [10/17/2015 Zmy]
                AngertableTemplate _data = (AngertableTemplate)DataTemplate.GetInstance().m_AngerTable.getTableData(logicPackage.m_Caster.GetMonsterRow().getFuryId());

                FightControler.Inst.OnUpdatePowerValue(logicPackage.m_Caster.GetGroupType(), _data.getAttackFury());
            }  
        }
        //#################################共有接口#####################################
        public void AIUpdate(List<ObjectMonster> Monsters)
        {
            if (m_AITimeChange > 0)
            {
                m_AITimeChange -= Time.deltaTime;
            }
            else
            {
                m_AITimeChange = m_AITime;
                RefreshSpellList(Monsters);
                ProcessSpellLogic();
            }
        }
        public void SetAItime(float time)
        {
            m_AITime = time/1000;
            m_AITimeChange = time/1000;
        }

        //避免频繁申请内存。使用池管理数据 [11/4/2015 Zmy]
        private SpellAILogic GenerateSubsetFromPool(int TemplateID, int Priority, ObjectCreature ReleaseObj, SpellInfo info, float timeInterval)
        {
            for (int i = 0; i < SpellAILogicPool.Count; i++)
            {
                if (SpellAILogicPool[i].IsInIdle())
                {
                    SpellAILogicPool[i].InitLogic(TemplateID, Priority, ReleaseObj, info, timeInterval);
                    return SpellAILogicPool[i];
                }
            }

            SpellAILogic temp = new SpellAILogic();
            temp.InitLogic(TemplateID, Priority, ReleaseObj, info, timeInterval);
            SpellAILogicPool.Add(temp);
            return temp;
        }
    }
}

