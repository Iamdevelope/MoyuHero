using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameCore;
namespace DreamFaction.SkillCore
{
    /// <summary>
    /// 技能通用AIID逻辑
    /// </summary>
    public class SpellAILogic
    {
        public enum LogicState
        {
            EM_LOGICSTATE_IDLE, //闲置 [11/4/2015 Zmy]
            EM_LOGICSTATE_USE,  //使用中
        }
        private float m_TimeInterval;
        private ObjectCreature m_ReleaseObj;             //技能释放者
        private ObjectCreature m_SkillTag;               //该模板满足的技能目标
        private List<ObjectCreature> m_TempSkillTag = new List<ObjectCreature>();           //用于类似逻辑9,10类型的储存目标
        private List<ObjectCreature> m_Oneself  = new List<ObjectCreature>();          //己方
        private List<ObjectCreature> m_Enemy  = new List<ObjectCreature>();            //敌方
        private int m_TemplateID;                        //模板ID
        private int m_Priority;                          //模板优先级 
        private int m_ID;                                //唯一ID
        private SpellInfo m_Spellinfo;                   //当前释放技能数据
        private SkillaiTemplate m_Tempai;                //AI表数据

        private LogicState curState;
        public SpellAILogic()
        {
            ClearUp();
        }

        public void ClearUp()
        {
            curState = LogicState.EM_LOGICSTATE_IDLE;

            m_TempSkillTag.Clear();
            m_Oneself.Clear();
            m_Enemy.Clear();

            m_TemplateID = -1;
            m_ReleaseObj = null;
            m_Priority = -1;
            m_Spellinfo = null;
            m_ID = -1;
            m_Tempai = null;
            m_TimeInterval = 0f ;
        }
        public bool IsInIdle()
        {
            return curState == LogicState.EM_LOGICSTATE_IDLE ? true : false;
        }
        public void InitLogic(int TemplateID,int Priority,ObjectCreature ReleaseObj,SpellInfo info,float timeInterval)
        {
            m_TempSkillTag.Clear();
            m_Oneself.Clear();
            m_Enemy.Clear();

            m_TemplateID = TemplateID;
            m_ReleaseObj = ReleaseObj;
            m_Priority = Priority;
            m_Spellinfo = info;
            m_ID = ReleaseObj.GetGameObject().GetInstanceID();
            m_Tempai = (SkillaiTemplate)DataTemplate.GetInstance().m_SkillaiTable.getTableData(m_TemplateID);
            m_TimeInterval = timeInterval;

            curState = LogicState.EM_LOGICSTATE_USE;
        }
        public ObjectCreature GetCaster()
        {
            return m_ReleaseObj;
        }
        public int GetPriority()
        {
            return m_Priority;
        }
        public SpellInfo GetSkillInfo()
        {
            return m_Spellinfo;
        }
        public ObjectCreature GetSkillTag()
        {
            if (m_SkillTag!=null && m_SkillTag.IsAlive())
                return m_SkillTag;
            else
                return null;
        }
        //判断AI逻辑是否满足
        public bool IsAILogicReady()
        {
            if (m_ReleaseObj.GetGroupType() == EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO)
            {
                m_Oneself.Clear();
                m_Enemy.Clear();
                int count = SceneObjectManager.GetInstance().GetObjectHeroCount();
                for (int i = 0; i < count; ++i)
                {
                    m_Oneself.Add(SceneObjectManager.GetInstance().GetSceneHeroList()[i]);
                }
                int count2 = SceneObjectManager.GetInstance().GetObjectMonsterCount();
                for (int i = 0; i < count2; ++i)
                {
                    m_Enemy.Add(SceneObjectManager.GetInstance().GetSceneMonsterList()[i]);
                }
            }
            else if (m_ReleaseObj.GetGroupType() == EM_OBJECT_TYPE.EM_OBJECT_TYPE_MONSTER)
            {
                m_Oneself.Clear();
                m_Enemy.Clear();
                int count = SceneObjectManager.GetInstance().GetObjectHeroCount();
                for (int i = 0; i < count; ++i)
                {
                    m_Enemy.Add(SceneObjectManager.GetInstance().GetSceneHeroList()[i]);
                }
                int count2 = SceneObjectManager.GetInstance().GetObjectMonsterCount();
                for (int i = 0; i < count2; ++i)
                {
                    m_Oneself.Add(SceneObjectManager.GetInstance().GetSceneMonsterList()[i]);
                }
            }

            if (IsLogicReady(m_Tempai.getCondition1(), m_Tempai.getParam1()) && IsLogicReady(m_Tempai.getCondition2(), m_Tempai.getParam2()) && IsLogicReady(m_Tempai.getCondition3(), m_Tempai.getParam3())
                && IsLogicReady(m_Tempai.getCondition4(), m_Tempai.getParam4()) && IsLogicReady(m_Tempai.getCondition5(), m_Tempai.getParam5()))
            {
               // Debug.Log(m_ReleaseObj.GetGameObject().name+"条件满足~~~~~~~~~~~~~~~~~~~~~~~~");
                OnChooseSkillTarget();
                return true;
            }
            return false;
        }
        public void OnChooseSkillTarget()
        {
            if (m_Spellinfo.IsNeedTarget())
            {
                switch (m_Tempai.getTarget())
                {
                    case (int)EM_SPELL_AI_TAGGRET.EM_SPELL_AI_INVALID:
                        break;
                    case (int)EM_SPELL_AI_TAGGRET.EM_SPELL_AI_TAGGRET1:
                        OnAiTagLogic1();
                        break;
                    case (int)EM_SPELL_AI_TAGGRET.EM_SPELL_AI_TAGGRET2:
                        OnAiTagLogic2();
                        break;
                    case (int)EM_SPELL_AI_TAGGRET.EM_SPELL_AI_TAGGRET3:
                        OnAiTagLogic3();
                        break;
                    case (int)EM_SPELL_AI_TAGGRET.EM_SPELL_AI_TAGGRET4:
                        OnAiTagLogic4();
                        break;
                    case (int)EM_SPELL_AI_TAGGRET.EM_SPELL_AI_TAGGRET5:
                        OnAiTagLogic5();
                        break;
                    case (int)EM_SPELL_AI_TAGGRET.EM_SPELL_AI_TAGGRET6:
                        OnAiTagLogic6();
                        break;
                    case (int)EM_SPELL_AI_TAGGRET.EM_SPELL_AI_TAGGRET7:
                        OnAiTagLogic7();
                        break;
                    case (int)EM_SPELL_AI_TAGGRET.EM_SPELL_AI_TAGGRET8:
                        OnAiTagLogic8();
                        break;
                    case (int)EM_SPELL_AI_TAGGRET.EM_SPELL_AI_TAGGRET9:
                        OnAiTagLogic9();
                        break;
                    case (int)EM_SPELL_AI_TAGGRET.EM_SPELL_AI_TAGGRET10:
                        OnAiTagLogic10();
                        break;
                    case (int)EM_SPELL_AI_TAGGRET.EM_SPELL_AI_TAGGRET11:
                        OnAiTagLogic11();
                        break;
                    case (int)EM_SPELL_AI_TAGGRET.EM_SPELL_AI_TAGGRET12:
                        OnAiTagLogic12();
                        break;
                    case (int)EM_SPELL_AI_TAGGRET.EM_SPELL_AI_TAGGRET13:
                        OnAiTagLogic13();
                        break;
                    case (int)EM_SPELL_AI_TAGGRET.EM_SPELL_AI_TAGGRET14:
                        OnAiTagLogic14();
                        break;
                    default:
                        Debug.Log("未知的目标选择逻辑：" + m_Tempai.getTarget());
                        break;
                }
            }
            else
                m_SkillTag = null;
        }

        private ObjectCreature RandomSelectTarget(List<ObjectCreature> targetList)
        {
            if (targetList == null || targetList.Count <= 0)
                return null;

            if (targetList.Count == 1)
                return targetList[0];
            else
            {
                return targetList[Random.Range(0, targetList.Count)];
            }
        }
        private void OnAiTagLogic1()
        {
            if (m_ReleaseObj.GetGroupType() == EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO)
            {
                int count = SceneObjectManager.GetInstance().GetObjectMonsterCount();
                ObjectHero hero = (ObjectHero)m_ReleaseObj;
                //选择相克目标
//                 switch(hero.GetHeroRow().getCamp())
//                 {
//                     case (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE1:
//                         for (int i = 0; i < count;++i)
//                         {
//                             if (SceneObjectManager.GetInstance().GetSceneMonsterList()[i].GetMonsterRow().getCamp() == (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE2)
//                             {
//                                 m_SkillTag = SceneObjectManager.GetInstance().GetSceneMonsterList()[i];
//                                 return;
//                             }
//                         }
//                             break;
//                     case (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE2:
//                             for (int i = 0; i < count; ++i)
//                             {
//                                 if (SceneObjectManager.GetInstance().GetSceneMonsterList()[i].GetMonsterRow().getCamp() == (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE3)
//                                 {
//                                     m_SkillTag = SceneObjectManager.GetInstance().GetSceneMonsterList()[i];
//                                     return;
//                                 }
//                             }
//                             break;
//                     case (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE3:
//                             for (int i = 0; i < count; ++i)
//                             {
//                                 if (SceneObjectManager.GetInstance().GetSceneMonsterList()[i].GetMonsterRow().getCamp() == (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE1)
//                                 {
//                                     m_SkillTag = SceneObjectManager.GetInstance().GetSceneMonsterList()[i];
//                                     return;
//                                 }
//                             }
//                             break;
//                 }
                //选择普通攻击目标
                if (m_SkillTag==null || m_SkillTag.IsAlive() == false)
                    m_SkillTag = hero.GetCurLockTarget();
            }
            else if (m_ReleaseObj.GetGroupType() == EM_OBJECT_TYPE.EM_OBJECT_TYPE_MONSTER)
            {
                int count = SceneObjectManager.GetInstance().GetObjectHeroCount();
                ObjectMonster monster = (ObjectMonster)m_ReleaseObj;
                //选择相克目标
//                 switch (monster.GetMonsterRow().getCamp())
//                 {
//                     case (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE1:
//                         for (int i = 0; i < count; ++i)
//                         {
//                             if (SceneObjectManager.GetInstance().GetSceneHeroList()[i].GetHeroRow().getCamp() == (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE2)
//                             {
//                                 m_SkillTag = SceneObjectManager.GetInstance().GetSceneHeroList()[i];
//                                 return;
//                             }
//                         }
//                         break;
//                     case (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE2:
//                         for (int i = 0; i < count; ++i)
//                         {
//                             if (SceneObjectManager.GetInstance().GetSceneHeroList()[i].GetHeroRow().getCamp() == (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE3)
//                             {
//                                 m_SkillTag = SceneObjectManager.GetInstance().GetSceneHeroList()[i];
//                                 return;
//                             }
//                         }
//                         break;
//                     case (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE3:
//                         for (int i = 0; i < count; ++i)
//                         {
//                             if (SceneObjectManager.GetInstance().GetSceneHeroList()[i].GetHeroRow().getCamp() == (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE1)
//                             {
//                                 m_SkillTag = SceneObjectManager.GetInstance().GetSceneHeroList()[i];
//                                 return;
//                             }
//                         }
//                         break;
//                 }
                //选择普通攻击目标
                if (m_SkillTag == null || m_SkillTag.IsAlive() == false)
                    m_SkillTag = monster.GetCurLockTarget();
            }
        }
        private void OnAiTagLogic2()
        {
            if (m_Oneself != null && m_Oneself.Count > 0)
            {
                float Mincount = (float)m_Oneself[0].GetHP() / (float)m_Oneself[0].GetMaxHP();
                int cout = m_Oneself.Count;
                m_SkillTag = m_Oneself[0];
                for (int i = 0; i < cout; ++i)
                {
                    if ((float)m_Oneself[i].GetHP() / (float)m_Oneself[i].GetMaxHP() <= Mincount)
                    {
                        Mincount = (float)m_Oneself[i].GetHP() / (float)m_Oneself[i].GetMaxHP();
                        m_SkillTag = m_Oneself[i];
                    }
                }
            }
        }
        private void OnAiTagLogic3()
        {
            if (m_Oneself != null && m_Oneself.Count > 0)
            {
                float Maxcount = (float)m_Oneself[0].GetHP() / (float)m_Oneself[0].GetMaxHP();
                int cout = m_Oneself.Count;
                m_SkillTag = m_Oneself[0];
                for (int i = 0; i < cout; ++i)
                {
                    if ((float)m_Oneself[i].GetHP() / (float)m_Oneself[i].GetMaxHP() >= Maxcount)
                    {
                        Maxcount = (float)m_Oneself[i].GetHP() / (float)m_Oneself[i].GetMaxHP();
                        m_SkillTag = m_Oneself[i];
                    }
                }
            }
        }
        private void OnAiTagLogic4()
        {
            if (m_ReleaseObj.GetGroupType() == EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO)
            {
                int count = SceneObjectManager.GetInstance().GetObjectHeroCount();
                int[] attack = new int[count];
                ObjectCreature[] tempobj = new ObjectCreature[count];
                for (int i = 0; i < count; ++i)
                {
                  if(SceneObjectManager.GetInstance().GetSceneHeroList()[i].GetHeroRow().getClientSignType()[1]==0)
                  {
                      attack[i] = SceneObjectManager.GetInstance().GetSceneHeroList()[i].GetPhysicalAttack();
                      tempobj[i] = SceneObjectManager.GetInstance().GetSceneHeroList()[i];
                  }
                  else 
                  {
                      attack[i] = SceneObjectManager.GetInstance().GetSceneHeroList()[i].GetMagicAttack();
                      tempobj[i] = SceneObjectManager.GetInstance().GetSceneHeroList()[i];
                  }
                }
                int maxcount = 0;
                if (attack.Length > 0)
                {
                    maxcount = attack[0];
                    m_SkillTag = tempobj[0];
                }
                for(int i = 1;i<attack.Length;++i)
                {
                    if(attack[i]>maxcount)
                    {
                        maxcount = attack[i];
                        m_SkillTag = tempobj[i];
                    }
                }
            }
            else if (m_ReleaseObj.GetGroupType() == EM_OBJECT_TYPE.EM_OBJECT_TYPE_MONSTER)
            {
                int count = SceneObjectManager.GetInstance().GetObjectMonsterCount();
                int[] attack = new int[count];
                ObjectCreature[] tempobj = new ObjectCreature[count];
                for (int i = 0; i < count; ++i)
                {
                    if (SceneObjectManager.GetInstance().GetSceneMonsterList()[i].GetMonsterRow().getClientSignType()[1] == 0)
                    {
                        attack[i] = SceneObjectManager.GetInstance().GetSceneMonsterList()[i].GetPhysicalAttack();
                        tempobj[i] = SceneObjectManager.GetInstance().GetSceneMonsterList()[i];
                    }
                    else
                    {
                        attack[i] = SceneObjectManager.GetInstance().GetSceneMonsterList()[i].GetMagicAttack();
                        tempobj[i] = SceneObjectManager.GetInstance().GetSceneMonsterList()[i];
                    }
                }
                int maxcount = 0;
                if (attack.Length > 0)
                {
                    maxcount = attack[0];
                    m_SkillTag = tempobj[0];
                }
                for (int i = 1; i < attack.Length; ++i)
                {
                    if (attack[i] > maxcount)
                    {
                        maxcount = attack[i];
                        m_SkillTag = tempobj[i];
                    }
                }
            }

            
        }
        private void OnAiTagLogic5()
        {
            if (m_Enemy != null && m_Enemy.Count > 0)
            {
                float Mincount = (float)m_Enemy[0].GetHP() / (float)m_Enemy[0].GetMaxHP();
                int cout = m_Enemy.Count;
                m_SkillTag = m_Enemy[0];
                for (int i = 0; i < cout; ++i)
                {
                    if ((float)m_Enemy[i].GetHP() / (float)m_Enemy[i].GetMaxHP() <= Mincount)
                    {
                        Mincount = (float)m_Enemy[i].GetHP() / (float)m_Enemy[i].GetMaxHP();
                        m_SkillTag = m_Enemy[i];
                    }
                }
            }
        }
        private void OnAiTagLogic6()
        {
            if (m_Enemy != null && m_Enemy.Count > 0)
            {
                float Maxcount = (float)m_Enemy[0].GetHP() / (float)m_Enemy[0].GetMaxHP();
                int cout = m_Enemy.Count;
                m_SkillTag = m_Enemy[0];
                for (int i = 0; i < cout; ++i)
                {
                    if ((float)m_Enemy[i].GetHP() / (float)m_Enemy[i].GetMaxHP() >= Maxcount)
                    {
                        Maxcount = (float)m_Enemy[i].GetHP() / (float)m_Enemy[i].GetMaxHP();
                        m_SkillTag = m_Enemy[i];
                    }
                }
            }
        }
        private void OnAiTagLogic7()
        {
            if (m_ReleaseObj.GetGroupType()==0)
            {
                int count = SceneObjectManager.GetInstance().GetObjectHeroCount();
                int[] attack = new int[count];
                ObjectCreature[] tempobj = new ObjectCreature[count];
                for (int i = 0; i < count; ++i)
                {
                    if (SceneObjectManager.GetInstance().GetSceneHeroList()[i].GetHeroRow().getClientSignType()[1] == 0)
                    {
                        attack[i] = SceneObjectManager.GetInstance().GetSceneHeroList()[i].GetPhysicalAttack();
                        tempobj[i] = SceneObjectManager.GetInstance().GetSceneHeroList()[i];
                    }
                    else
                    {
                        attack[i] = SceneObjectManager.GetInstance().GetSceneHeroList()[i].GetMagicAttack();
                        tempobj[i] = SceneObjectManager.GetInstance().GetSceneHeroList()[i];
                    }
                }
                int maxcount = 0;
                if (attack.Length > 0)
                    maxcount = attack[0];
                for (int i = 0; i < attack.Length; ++i)
                {
                    if (attack[i] > maxcount)
                    {
                        maxcount = attack[i];
                        m_SkillTag = tempobj[i];
                    }
                }
            }
            else
            {
                int count = SceneObjectManager.GetInstance().GetObjectMonsterCount();
                int[] attack = new int[count];
                ObjectCreature[] tempobj = new ObjectCreature[count];
                for (int i = 0; i < count; ++i)
                {
                    if (SceneObjectManager.GetInstance().GetSceneMonsterList()[i].GetMonsterRow().getClientSignType()[1] == 0)
                    {
                        attack[i] = SceneObjectManager.GetInstance().GetSceneMonsterList()[i].GetPhysicalAttack();
                        tempobj[i] = SceneObjectManager.GetInstance().GetSceneMonsterList()[i];
                    }
                    else
                    {
                        attack[i] = SceneObjectManager.GetInstance().GetSceneMonsterList()[i].GetMagicAttack();
                        tempobj[i] = SceneObjectManager.GetInstance().GetSceneMonsterList()[i];
                    }
                }
                int maxcount = 0;
                if (attack.Length > 0)
                    maxcount = attack[0];
                for (int i = 0; i < attack.Length; ++i)
                {
                    if (attack[i] > maxcount)
                    {
                        maxcount = attack[i];
                        m_SkillTag = tempobj[i];
                    }
                }
            }
        }
        private void OnAiTagLogic8()
        {
            m_SkillTag = RandomSelectTarget(m_TempSkillTag);
        }
        private void OnAiTagLogic9()
        {
            m_SkillTag = m_ReleaseObj;
        }

        private void OnAiTagLogic10()
        {
            m_SkillTag = RandomSelectTarget(m_TempSkillTag);
        }
        private void OnAiTagLogic11()
        {
            if (m_Enemy.Count <= 0)
                m_SkillTag = null;
            else if (m_Enemy.Count == 1)
                m_SkillTag = m_Enemy[0];
            else
            {
                m_SkillTag = m_Enemy[0];
                for (int i = 1; i < m_Enemy.Count; i++)
                {
                    if (m_Enemy[i].GetSpeed() > m_SkillTag.GetSpeed())
                        m_SkillTag = m_Enemy[i];
                }
            }
        }
        private void OnAiTagLogic12()
        {
            m_SkillTag = RandomSelectTarget(m_Enemy);
        }
        private void OnAiTagLogic13()
        {
            m_SkillTag = RandomSelectTarget(m_TempSkillTag);
        }
        private void OnAiTagLogic14()
        {
            m_SkillTag = RandomSelectTarget(m_Oneself);
        }

        //技能逻辑满足判断
        private bool IsLogicReady(int ConditionID, int[] Param)
        {
            switch (ConditionID)
            {
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_INVALID:
                    return true;
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC1:
                    return CheckCondition1(Param);
                 case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC2:
                    return CheckCondition2(Param);
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC3:
                    return CheckCondition3(Param);
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC4:
                    return CheckCondition4(Param);
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC5:
                    return CheckCondition5(Param);
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC6:
                    return CheckCondition6(Param);
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC7:
                    return CheckCondition7(Param);
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC8:
                    return CheckCondition8(Param);
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC9:
                    return CheckCondition9(Param);
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC10://缺
                    return CheckCondition10(Param);
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC11:
                    return CheckCondition11(Param);
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC12:
                    return CheckCondition12(Param);
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC13:
                    return CheckCondition13(Param);
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC14:
                    return CheckCondition14(Param);
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC15:
                    return CheckCondition15(Param);
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC16:
                    return CheckCondition16(Param);
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC17:
                    return CheckCondition17(Param);
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC18:
                    return CheckCondition18(Param);
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC19:
                    return CheckCondition19(Param);
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC20://缺
                    return CheckCondition20(Param);
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC21:
                    return CheckCondition21(Param);
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC22:
                    return CheckCondition22(Param);
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC23:
                    return CheckCondition23(Param);
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC24:
                    return CheckCondition24(Param);
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC25:
                    return CheckCondition25(Param);
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC26:
                    return CheckCondition26(Param);
                case (int)EM_SPELL_AI_LOGIC.EM_SPELL_AI_LOGIC27:
                    return CheckCondition27(Param);
            }
            return false;
        }
        private bool CheckCondition1(int[] Param)
        {
            return (FightControler.Inst.GetPowerValue(m_ReleaseObj.GetGroupType()) < Param[0]) ? true : false;
        }
        private bool CheckCondition2(int[] Param)
        {
            return (FightControler.Inst.GetPowerValue(m_ReleaseObj.GetGroupType()) > Param[0]) ? true : false;
        }
        private bool CheckCondition3(int[] Param)
        {
            int count = m_Oneself.Count;
            for (int i = 0; i < count; ++i)
            {
                if ((float)m_Oneself[i].GetHP() / (float)m_Oneself[i].GetMaxHP() < (float)Param[0] / 100)
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckCondition4(int[] Param)
        {
            int count = m_Oneself.Count;
            for (int i = 0; i < count; ++i)
            {
                if ((float)m_Oneself[i].GetHP() / (float)m_Oneself[i].GetMaxHP() > (float)Param[0] / 100)
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckCondition5(int[] Param)
        {
            int count = m_Oneself.Count;
            int Num = 0;
            for (int i = 0; i < count; ++i)
            {
                if ((float)m_Oneself[i].GetHP() / (float)m_Oneself[i].GetMaxHP() > (float)Param[0] / 100)
                {
                    Num++;
                }
            }
            if (Num >= 3)
                return true;
            return false;
        }
        private bool CheckCondition6(int[] Param)
        {
            int count = m_Oneself.Count;
            int Num = 0;
            for (int i = 0; i < count; ++i)
            {
                if ((float)m_Oneself[i].GetHP() / (float)m_Oneself[i].GetMaxHP() < (float)Param[0] / 100)
                {
                    Num++;
                }
            }
            if (Num >= 3)
                return true;
            return false;
        }
        private bool CheckCondition7(int[] Param)
        {
            return (m_Oneself.Count< Param[0]) ? true : false;
        }
        private bool CheckCondition8(int[] Param)
        {
            return (m_Oneself.Count > Param[0]) ? true : false;
        }
        private bool CheckCondition9(int[] Param)
        {
            m_TempSkillTag.Clear();
            if (m_ReleaseObj.GetGroupType() == EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO)
            {
                int count = SceneObjectManager.GetInstance().GetObjectHeroCount();
                for (int i = 0; i < count; ++i)
                {
                    for(int j=0;j<Param.Length;++j)
                    {
                        if (SceneObjectManager.GetInstance().GetSceneHeroList()[i].GetHeroRow().getId() == Param[j])
                        {
                            m_TempSkillTag.Add(SceneObjectManager.GetInstance().GetSceneHeroList()[i]);

                        }
                            
                    }
                }
            }
            else 
            {
                int count = SceneObjectManager.GetInstance().GetObjectMonsterCount();
                for (int i = 0; i < count; ++i)
                {
                    for (int j = 0; j < Param.Length; ++j)
                    {
                        if (SceneObjectManager.GetInstance().GetSceneMonsterList()[i].GetMonsterRow().getId() == Param[j])
                        {
                            m_TempSkillTag.Add(SceneObjectManager.GetInstance().GetSceneMonsterList()[i]);

                        }      
                    }
                }
            }
            return m_TempSkillTag.Count>0;
        }
        private bool CheckCondition10(int[] Param)
        {
            return CheckBuffInList(m_Oneself,Param);
        }
        private bool CheckCondition11(int[] Param)
        {
            if (m_ReleaseObj.GetGroupType() == EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO)
                return (FightControler.Inst.GetPowerValue(EM_OBJECT_TYPE.EM_OBJECT_TYPE_MONSTER) < Param[0]) ? true : false;
            else
                return (FightControler.Inst.GetPowerValue(EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO) < Param[0]) ? true : false;
        }
        private bool CheckCondition12(int[] Param)
        {
            if (m_ReleaseObj.GetGroupType() == EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO)
                return (FightControler.Inst.GetPowerValue(EM_OBJECT_TYPE.EM_OBJECT_TYPE_MONSTER) > Param[0]) ? true : false;
            else
                return (FightControler.Inst.GetPowerValue(EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO) > Param[0]) ? true : false;
        }
        private bool CheckCondition13(int[] Param)
        {
            int count = m_Enemy.Count;
            for (int i = 0; i < count; ++i)
            {
                if ((float)m_Enemy[i].GetHP() / (float)m_Enemy[i].GetMaxHP() < (float)Param[0] / 100)
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckCondition14(int[] Param)
        {
            int count = m_Enemy.Count;
            for (int i = 0; i < count; ++i)
            {
                if ((float)m_Enemy[i].GetHP() / (float)m_Enemy[i].GetMaxHP() > (float)Param[0] / 100)
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckCondition15(int[] Param)
        {
            int count = m_Enemy.Count;
            int num = 0;
            for (int i = 0; i < count; ++i)
            {
                if ((float)m_Enemy[i].GetHP() / (float)m_Enemy[i].GetMaxHP() < (float)Param[0] / 100)
                {
                    num++;
                }
            }
            if (num >= 3)
                return true;
            return false;
        }
        private bool CheckCondition16(int[] Param)
        {
            int count = m_Enemy.Count;
            int num = 0;
            for (int i = 0; i < count; ++i)
            {
                if ((float)m_Enemy[i].GetHP() / (float)m_Enemy[i].GetMaxHP() > (float)Param[0] / 100)
                {
                    num++;
                }
            }
            if (num >= 3)
                return true;
            return false;
        }
        private bool CheckCondition17(int[] Param)
        {
            return (m_Enemy.Count< Param[0]) ? true : false;
        }
        private bool CheckCondition18(int[] Param)
        {
            return (m_Enemy.Count > Param[0]) ? true : false;
        }
        private bool CheckCondition19(int[] Param)
        {
            m_TempSkillTag.Clear();
            if (m_ReleaseObj.GetGroupType() == EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO)
            {
                int count = SceneObjectManager.GetInstance().GetObjectMonsterCount();
                for (int i = 0; i < count; ++i)
                {
                    for (int j = 0; j < Param.Length; ++j)
                    {
                        if (SceneObjectManager.GetInstance().GetSceneMonsterList()[i].GetMonsterRow().getId() == Param[j])
                        {
                            m_TempSkillTag.Add(SceneObjectManager.GetInstance().GetSceneMonsterList()[i]);
                        }   
                    }
                }
            }
            else if (m_ReleaseObj.GetGroupType() == EM_OBJECT_TYPE.EM_OBJECT_TYPE_MONSTER)
            {
                int count = SceneObjectManager.GetInstance().GetObjectHeroCount();
                for (int i = 0; i < count; ++i)
                {
                    for (int j = 0; j < Param.Length; ++j)
                    {
                        if (SceneObjectManager.GetInstance().GetSceneHeroList()[i].GetHeroRow().getId() == Param[j])
                        {
                            m_TempSkillTag.Add(SceneObjectManager.GetInstance().GetSceneHeroList()[i]);
                        }
                    }
                }
            }
            return m_TempSkillTag.Count>0;
        }
        private bool CheckCondition20(int[] Param)
        {
            return CheckBuffInList(m_Enemy, Param);
        }
        private bool CheckCondition21(int[] Param)
        {
            return ((float)m_ReleaseObj.GetHP() / (float)m_ReleaseObj.GetMaxHP() < (float)Param[0] / 100) ? true : false;
        }
        private bool CheckCondition22(int[] Param)
        {
            return ((float)m_ReleaseObj.GetHP() / (float)m_ReleaseObj.GetMaxHP() > (float)Param[0] / 100) ? true : false;
        }
        private bool CheckCondition23(int[] Param)
        {
            if (m_ReleaseObj.GetGroupType() == EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO)
            {
                int count = SceneObjectManager.GetInstance().GetObjectHeroCount();
                for (int i = 0; i < count; ++i)
                {
                    for (int j = 0; j < Param.Length; ++j)
                    {
                        if (SceneObjectManager.GetInstance().GetSceneHeroList()[i].GetHeroRow().getId() == Param[j])
                        {
//                            m_TempSkillTag = SceneObjectManager.GetInstance().GetSceneHeroList()[i];
                            return false;
                        }

                    }
                }
            }
            else
            {
                int count = SceneObjectManager.GetInstance().GetObjectMonsterCount();
                for (int i = 0; i < count; ++i)
                {
                    for (int j = 0; j < Param.Length; ++j)
                    {
                        if (SceneObjectManager.GetInstance().GetSceneMonsterList()[i].GetMonsterRow().getId() == Param[j])
                        {
//                            m_TempSkillTag = SceneObjectManager.GetInstance().GetSceneMonsterList()[i];
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        private bool CheckCondition24(int[] Param)
        {
            m_TempSkillTag.Clear();

            float time = SceneObjectManager.GetInstance().TimeInBattleScene;
            for (int i = 0; i < m_Enemy.Count; i++)
            {
                if (time - m_Enemy[i].LastSkillTimestamp < m_TimeInterval 
                    && (m_Enemy[i].SkillTypeFlag&ENUM_SPELL_TYPE_FLAG.SPELL_MAGIC_HEAL) != ENUM_SPELL_TYPE_FLAG.SPELL_NONE)
                {
                    m_TempSkillTag.Add(m_Enemy[i]);
                }
            }
            return m_TempSkillTag.Count > 0;
        }
        private bool CheckCondition25(int[] Param)
        {
            m_TempSkillTag.Clear();

            float time = SceneObjectManager.GetInstance().TimeInBattleScene;
            for (int i = 0; i < m_Enemy.Count; i++)
            {
                if (time - m_Enemy[i].LastSkillTimestamp < m_TimeInterval)
                {
                    SkillTemplate _dataTemplate = DataTemplate.GetInstance().m_SkillTable.getTableData(m_Enemy[i].LastSpellID) as SkillTemplate;
                    if (_dataTemplate != null)
                    {
                        if (CheckBuffGroup(_dataTemplate, Param))
                        {
                            m_TempSkillTag.Add(m_Enemy[i]);
                        }
                    }
                }
            }
            return m_TempSkillTag.Count > 0;
        }

        private bool CheckBuffGroup(SkillTemplate skillTemp, int[] groupArray)
        {
            bool _result = false;

            for (int i = 0; i < groupArray.Length; i++)
            {
                BuffgroupTemplate _buffGroup = DataTemplate.GetInstance().m_BuffGroupTable.getTableData(groupArray[i]) as BuffgroupTemplate;
                var _buffArray = _buffGroup.getParam();
                for (int j = 0; j < _buffArray.Length; j++)
                {
                    _result = CheckBuff(skillTemp.getTemporarySelfBuff(), _buffArray[j]);
                    if (_result)
                        return true;
                    _result = CheckBuff(skillTemp.getTemporaryTargetBuff(), _buffArray[j]);
                    if (_result)
                        return true;
                    _result = CheckBuff(skillTemp.getBuffList(), _buffArray[j]);
                    if (_result)
                        return true;

                }
            }
            return false;
        }

        private bool CheckBuff(int[] _skillBuffArray,int targetBuff)
        {
            for (int k = 0; k < _skillBuffArray.Length; k++)
            {
                if (_skillBuffArray[k] == targetBuff)
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckBuffInList(List<ObjectCreature> list,int[] buffArray)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int[] _targetBuffArray = list[i].GetBuffID();
                for (int j = 0; j < _targetBuffArray.Length; j++)
                {
                    bool _result = CheckBuff(buffArray, _targetBuffArray[j]);
                    if (_result)
                        return true;
                }
            }
            return false;
        }

        private bool CheckCondition26(int[] Param)
        {
            return Random.Range(0, 10000) < Param[0];
        }

        private bool CheckCondition27(int[] Param)
        {
            return (m_ReleaseObj.FightingTimestamp * 1000) > Param[0];
        }
        
    }
}

