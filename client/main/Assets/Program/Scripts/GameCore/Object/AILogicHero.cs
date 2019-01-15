using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;

namespace DreamFaction.SkillCore
{
    /// <summary>
    /// 英雄AI逻辑类
    /// </summary>
    public class AILogicHero
    {
        private static AILogicHero m_Inst;
        private float m_AITime=0;                                       //AI检测用时间间隔
        private float m_AITimeChange=0;                                 //AI计时
        private List<ObjectHero> Rand_heros;                            //随机用英雄信息
        private List<ObjectHero> Ok_heros;                              //满足怒气等各种条件能给释放技能的英雄信息
        private List<ObjectHero> AIOk_heros;                            //满足AI条件的英雄信息
        private List<SpellAILogic> OnlyHeroSpellAILogicList;            //单个英雄技能ID模板数组  
        private List<SpellAILogic> HerosSpellAILogicList;               //所有英雄技能ID模板数组

        private List<SpellAILogic> SpellAILogicPool = new List<SpellAILogic>();//技能ai逻辑池 [11/4/2015 Zmy]
        public static AILogicHero GetInstance()
        {
            if (m_Inst == null)
                m_Inst = new AILogicHero();
            return m_Inst;
        }
        private AILogicHero()
        {
            if (Rand_heros == null)
                Rand_heros = new List<ObjectHero>();
            if (Ok_heros == null)
                Ok_heros = new List<ObjectHero>();
            if (HerosSpellAILogicList == null)
                HerosSpellAILogicList = new List<SpellAILogic>();
            if (AIOk_heros == null)
                AIOk_heros = new List<ObjectHero>();
            if (OnlyHeroSpellAILogicList == null)
                OnlyHeroSpellAILogicList = new List<SpellAILogic>();
        }
        //重置数据
        private void ReturnData()
        {
            m_AITimeChange = m_AITime;
            Ok_heros.Clear();
            HerosSpellAILogicList.Clear();
            AIOk_heros.Clear();
            OnlyHeroSpellAILogicList.Clear();

            for (int i = 0; i < SpellAILogicPool.Count; i++)
            {
                SpellAILogicPool[i].ClearUp();
            }
        }
        private void OnAItypeUpdate(int type)
        {
            int cout = Ok_heros.Count;
            int Maxtemp = -1;
            int FinishHerovalue = -1;//最终释放技能英雄的索引
            SpellAILogic FinishAi = null;//最终释放技能模板
            for (int i = 0; i < cout; ++i)
            {
                SpellAILogic temp = ChooseAILogicPriority(Ok_heros[i], type);//获取能释放技能的有效优先级
                if (temp != null)
                {
                    HerosSpellAILogicList.Add(temp);
                    AIOk_heros.Add(Ok_heros[i]);
                }
            }
            int cout2 = HerosSpellAILogicList.Count;
            for (int i = 0; i < cout2; ++i)
            {
                if (HerosSpellAILogicList[i].GetPriority()> Maxtemp)
                {
                    Maxtemp = HerosSpellAILogicList[i].GetPriority();
                    FinishAi = HerosSpellAILogicList[i];
                    FinishHerovalue = i;
                }
            }
            if (FinishHerovalue >=0)
            {
                if (SceneObjectManager.GetInstance().GetIsFireSignState() && SceneObjectManager.GetInstance().GetFireSighCreatrue() != null)
                {
                    AIOk_heros[FinishHerovalue].SetSkillLockTarget(SceneObjectManager.GetInstance().GetFireSighCreatrue());
                }
                else
                {
                    AIOk_heros[FinishHerovalue].SetSkillLockTarget(FinishAi.GetSkillTag());
                }
                
                if (AIOk_heros[FinishHerovalue].OnPre_CheckUseSkillCondtion())
                {
                    //AIOk_heros[FinishHerovalue].OnSkillConsume();
                    //AIOk_heros[FinishHerovalue].SetObjectActionState(ObjectCreature.ObjectActionState.skillAttack);
                    //string name = FinishAi.GetSkillInfo().GetSpellRow().getSkillNameRes();
                    //SkillShowNamePackage package = new SkillShowNamePackage(AIOk_heros[FinishHerovalue].GetGuid(), name);
                    //GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_ShowSkillName, package);

                    AIOk_heros[FinishHerovalue].LaunchFreeSpellLogic(EM_SPELL_PASSIVE_INDEX.EM_SPELL_PASSIVE_INITIATIVE);
                }
                else
                {
                    AIOk_heros[FinishHerovalue].SetSkillLockTarget(null);
                }
                //Debug.Log(AIOk_heros[FinishHerovalue].GetGameObject().name);
            }
        }
        //通过技能逻辑返回英雄能释放的最大优先级的AI模板
        private SpellAILogic ChooseAILogicPriority(ObjectHero hero, int type ,int _spellIndex = 0)
        {
            if (hero.Getm_SpellInfo()[_spellIndex].GetSpellRow().GetID() == -1)
                return null;
            int[] m_Template=null; //临时自动模式模板
            int[] m_Priority=null; //临时自动模式模板优先级
            switch (type)//选择当前自动战斗模式
            {
                case (int)EM_SPELL_AI_TYPE.EM_SPELL_AI_TYPE_NORMAL:
                    m_Template = hero.Getm_SpellInfo()[_spellIndex].GetSpellRow().getNormalTemplate();
                    m_Priority = hero.Getm_SpellInfo()[_spellIndex].GetSpellRow().getNormalpriority();
                    break;
                case (int)EM_SPELL_AI_TYPE.EM_SPELL_AI_TYPE_ATTACK:
                    m_Template = hero.Getm_SpellInfo()[_spellIndex].GetSpellRow().getAttFirstTemplate();
                    m_Priority = hero.Getm_SpellInfo()[_spellIndex].GetSpellRow().getAttFirstpriority();
                    break;
                case (int)EM_SPELL_AI_TYPE.EM_SPELL_AI_TYPE_CURE:
                    m_Template = hero.Getm_SpellInfo()[_spellIndex].GetSpellRow().getDefFirstTemplate();
                    m_Priority = hero.Getm_SpellInfo()[_spellIndex].GetSpellRow().getDefFirstpriority();
                    break;
                default:
                    break;
            }
            if (m_Template==null||m_Template.Length <= 0)
                return null;
            int Templatecout = m_Template.Length;
            int Maxtemp = -1;//临时优先级
            SpellAILogic tempSpellai=null;//临时AI模板
            for (int i = 0; i < Templatecout;++i)
            {
                //判断是否满足AI逻辑ID的条件
                SpellAILogic temp = GenerateSubsetFromPool(m_Template[i], m_Priority[i], hero, hero.Getm_SpellInfo()[_spellIndex], m_AITime);
                if(temp.IsAILogicReady())
                {
                    OnlyHeroSpellAILogicList.Add(temp);
                }
            }
            for (int i = 0; i < OnlyHeroSpellAILogicList.Count; ++i)
            {
                if (OnlyHeroSpellAILogicList[i].GetPriority()> Maxtemp)
                {
                    Maxtemp = OnlyHeroSpellAILogicList[i].GetPriority();
                    tempSpellai = OnlyHeroSpellAILogicList[i];
                }
            }
            return tempSpellai;
        }
        
        //#################################共有接口#####################################
        public void AIUpdate(List<ObjectHero> Heros)
        {
            if ((int)FightControler.Inst.GetFightAIState() == 0)
            {
                return;
            }
            if (m_AITimeChange > 0)
            {
                m_AITimeChange -= Time.deltaTime;
            }
            else
            {
                ReturnData();
                for (int i = 0; i < Heros.Count; ++i)
                {
                    if (!Heros[i].IsSkillAttack()&&Heros[i].IsAlive())//判断该技能的硬性释放条件
                    {
                        Rand_heros.Add(Heros[i]);
                    }
                }
                if (Rand_heros == null)
                    return;
                int count=Rand_heros.Count;
                for(int i=0;i<count;++i)
                {
                    int _randmun = Random.Range(0, Rand_heros.Count);
                    Ok_heros.Add(Rand_heros[_randmun]);
                    Rand_heros.Remove(Rand_heros[_randmun]);
                }
                OnAItypeUpdate((int)FightControler.Inst.GetFightAIState());
            }
        }
        public void SetAItime(float time)
        {
            m_AITime = time/1000;
            m_AITimeChange = time/1000;
        }

        public void SelectSpellTarget(ObjectHero objHero)
        {
            ReturnData();

            SpellAILogic FinishAi = ChooseAILogicPriority(objHero, (int)EM_SPELL_AI_TYPE.EM_SPELL_AI_TYPE_NORMAL, (int)objHero.GetLaunchFreeSpellIndex());//获取能释放技能的有效优先级

            if (SceneObjectManager.GetInstance().GetIsFireSignState() && SceneObjectManager.GetInstance().GetFireSighCreatrue() != null)
            {
                objHero.SetSkillLockTarget(SceneObjectManager.GetInstance().GetFireSighCreatrue());
            }
            else
            {
                objHero.SetSkillLockTarget(FinishAi.GetSkillTag());
            }
        }

        //避免频繁申请内存。使用池管理数据 [11/4/2015 Zmy]
        private SpellAILogic GenerateSubsetFromPool(int TemplateID, int Priority, ObjectCreature ReleaseObj, SpellInfo info, float timeInterval)
        {
            for (int i = 0; i < SpellAILogicPool.Count; i++ )
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

