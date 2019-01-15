using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using DreamFaction.GameNetWork.Data;
using DreamFaction.LogSystem;
using DreamFaction.GameNetWork;
using DreamFaction.GameCore;
using DreamFaction.GameSceneEditor;
using DreamFaction.GameAudio;
/*
**FileName:
**Description:包含技能共同逻辑,并不具备具体技能发作逻辑,发作逻辑将推倒spelllogic执行,但可接受在发动或执行过程中事件响应,对于远程目标,需要客户端执行移动到目标附近,再释放技能
**Autor: liam
**Create Time:
*/

namespace DreamFaction.SkillCore
{
    public class SpellTargetInfo
    {
        public ObjectCreature       m_pTargetObject;                                                //目标
        public byte                 m_bHit;                                                         //命中否
        public int[]                m_ValidImpact = new int[GlobalMembers.MAX_IMPACT_NUMBER];       //受影响的impact
        private int                 m_ImpactCount;                                                  //自增imact数组索引标记
        public SpellTargetInfo()
        {
            m_pTargetObject = null;
            m_bHit = 1;
            for (int i = 0; i < GlobalMembers.MAX_IMPACT_NUMBER; ++i)
            {
                m_ValidImpact[i] = -1;
            }
            m_ImpactCount = 0;
        }
        public void AddValidImpact(int nImpactID)
        {
            if (nImpactID < 0)
                return;
            if (m_ImpactCount >= GlobalMembers.MAX_IMPACT_NUMBER)
                return;

            m_ValidImpact[m_ImpactCount] = nImpactID;
            m_ImpactCount++;
            
        }
    }
    public class FIGHTOBJECT_LIST
    {

        public List<SpellTargetInfo> m_pObjectList = new List<SpellTargetInfo>();

        public int m_nCount
        {
            get
            {
                return m_pObjectList.Count; 
            }
        }
        public FIGHTOBJECT_LIST()
        {
            m_pObjectList.Clear();
        }
        public bool Add(ObjectCreature pObject)
        {
            if (pObject == null)
	        {
		        return false;
	        }
            SpellTargetInfo listInfo = new SpellTargetInfo();
            listInfo.m_pTargetObject = pObject;
            m_pObjectList.Add(listInfo);
            return true;
        }
        public bool Remove(ObjectCreature pObject)
        {
            if (pObject == null)
	        {
		        return false;
	        }
            int nCount = m_pObjectList.Count;
            for (int i = 0; i < nCount; i++)
			{
			    if (pObject == m_pObjectList[i].m_pTargetObject)
	            {
		            m_pObjectList.RemoveAt(i);
                    return true;
	            }
			}
            return false;
        }

        //刷新改变目标命中状态，默认全命中，更新未命中的目标 [3/23/2015 Zmy]
        public void OnUpdateTargetHit(ObjectCreature pObject)
        {
            if (pObject == null)
                return;

            int nCount = m_pObjectList.Count;
            for (int i = 0; i < nCount; i++)
            {
                if (pObject == m_pObjectList[i].m_pTargetObject)
                {
                    m_pObjectList[i].m_bHit = 0;
                    break;
                }
            }
        }
        //刷新附加到目标身上的有效Impact [3/23/2015 Zmy]
        public void OnUpdateTargetImpact(ObjectCreature pTarget,int nImpactID)
        {
            if (pTarget == null)
                return;

            int nCount = m_pObjectList.Count;
            for (int i = 0; i < nCount; i++)
            {
                if (pTarget == m_pObjectList[i].m_pTargetObject)
                {
                    m_pObjectList[i].AddValidImpact(nImpactID);
                    break;
                }
            }
        }
    }
    public class Spell
    {
        private ObjectCreature              m_pHolder;										//持有者
        private SpellInfo                   m_pSpellInfo;									//释放的技能信息
        private X_GUID                      m_SpellTarget = new X_GUID();				    //技能目标
        private float                       m_SpellTargetDir;								//方向
        private Vector3                     m_SpellTargetPosition;					        //技能目标位置(无目标时候可能在地上)
        private FIGHTOBJECT_LIST            m_TargetList = new FIGHTOBJECT_LIST();          //目标列表
        private int[]                       m_ValidImpact= new int[GlobalMembers.MAX_IMPACT_NUMBER]; //技能对自身造成的有效buff
        public Spell(ObjectCreature pHolder)
        {
            this.m_pHolder = pHolder;
        }
        public Spell()
        {
            this.m_pHolder = null;
            for (int i = 0; i < GlobalMembers.MAX_IMPACT_NUMBER; ++i)
            {
                m_ValidImpact[i] = -1;
            }
        }

        public bool Init(int nSpellID, ref Vector3 Targetpos, float fDir, ref X_GUID guidTarget)
        {
            LogManager.LogAssert(m_pHolder);
            if (nSpellID == -1)
            {
                LogManager.LogAssert(0);
            }

            SpellInfo pSpellInfo = m_pHolder.GetSpellInfo(nSpellID);
            if (pSpellInfo != null)
            {
                m_pSpellInfo = pSpellInfo;
                m_SpellTargetDir = fDir;
                m_SpellTargetPosition = Targetpos;
                m_SpellTarget.Copy(guidTarget);
            }

            return true;
        }
        public bool Init(SpellInfo pSpellInfo/*, WORLD_POS& Targetpos*/)
        {

            LogManager.LogAssert(m_pHolder);
            LogManager.LogAssert(pSpellInfo);

            m_pSpellInfo = pSpellInfo;
            //	m_SpellTargetPosition = Targetpos;
            return true;
        }

        public bool Init(SpellInfo pSpellInfo, ref X_GUID guidTarget)
        {

            LogManager.LogAssert(m_pHolder);
            LogManager.LogAssert(pSpellInfo);

            m_pSpellInfo = pSpellInfo;
            m_SpellTarget.Copy(guidTarget);
            return true;
        }

        public void CleanUp()
        {

            LogManager.LogAssert(m_pHolder);
            m_pSpellInfo = null;
            m_SpellTargetPosition = new Vector3(0, 0, 0);
        }

        public bool IsValid()
        {

            LogManager.LogAssert(m_pHolder);
            LogManager.LogAssert(m_pSpellInfo);

            return m_pSpellInfo.IsValid();

        }
        public	X_GUID		GetTargetGuid() 
        {
            return m_SpellTarget; 
        }
	    public void		    SetTargetGuid(X_GUID nGuid)
        {
            m_SpellTarget.Copy(nGuid); 
        }

        public void SetHolder(ObjectCreature pHolder)
        {

            LogManager.LogAssert(pHolder);

            m_pHolder = pHolder;
        }

        public int GetSpellID()
        {
            LogManager.LogAssert(m_pHolder);
            LogManager.LogAssert(m_pSpellInfo);

            return m_pSpellInfo.GetSpellID();
        }
        public int GetSpellNum()
        {
            LogManager.LogAssert(m_pHolder);
            LogManager.LogAssert(m_pSpellInfo); 

            return m_pSpellInfo.GetSpellNum();
        }

        public bool ImmActiveOnce()
        {

            LogManager.LogAssert(m_pHolder);
            LogManager.LogAssert(m_pSpellInfo);
            if (m_pSpellInfo.IsValid() == false)
                return false;
            
            __ImpactEffectOnSpellStart();

            ActiveOnce();

            return true;

        }
        public bool ActivePassivityOnce()
        {
            CalcPassivitySpellTarget(ref m_TargetList);
            if (m_TargetList.m_nCount > 0)
            {
                SkillTemplate pSpellRow = m_pSpellInfo.GetSpellRow();
                ObjectCreature pScanObject = null;
                for (int TargetIndex = 0; TargetIndex < m_TargetList.m_nCount; TargetIndex++)
                {
                    pScanObject = m_TargetList.m_pObjectList[TargetIndex].m_pTargetObject;

                    for (int m = 0; m < pSpellRow.getBuffList().Length; ++m)
                    {
                        if (pSpellRow.getBuffList()[m] != -1)
                        {
                            pScanObject.AddPassvityImpact(pSpellRow.getBuffList()[m], m_pHolder, false, GetSpellID());
                        }
                    }
                }

            }
            return true;
        }
        public bool ActiveOnce()
        {
            //释放技能音效
            //AudioControler.Inst.PlaySound(m_pSpellInfo.GetSpellRow().getAttackSound());

            List<SPELL_EVENT> pList = m_pHolder.GetSpellEventQueue().GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_USESPELL);
            LogManager.LogAssert(pList);
            foreach (var item in pList)
            {
                Impact pImpact = item.m_pImpact;
                if (pImpact != null)
                {
                    pImpact.OnSpellUse(m_pSpellInfo, m_SpellTarget);
                }
            }

            LogManager.LogAssert(m_pHolder);
            LogManager.LogAssert(m_pSpellInfo);
            int m_Hitcount=0;//命中个数
            bool bRet = false;
            //目标逻辑
            //FIGHTOBJECT_LIST targetlist = new FIGHTOBJECT_LIST();
            __CalculateSpellTarget(ref m_TargetList);
            AddLinkEff();
            if (m_TargetList.m_nCount > 0)
            {
                SkillTemplate pSpellRow = m_pSpellInfo.GetSpellRow();
                LogManager.LogAssert(pSpellRow);
                // 将该技能的效果作用于目标身上
                ObjectCreature pScanObject = null;
                //产生impact
                for (int TargetIndex = 0; TargetIndex < m_TargetList.m_nCount; TargetIndex++)
                {
                    pScanObject = m_TargetList.m_pObjectList[TargetIndex].m_pTargetObject;
                    //目标临时buff
                    __ImpactEffectOnSpellStartToTarget(pScanObject);

                    //根据圆桌算法 返回行为结果：暴击，闪避，普通攻击 [10/16/2015 Zmy]
                    EM_SPELL_RESULT nResult = EM_SPELL_RESULT.EM_SPELL_RESULT_FAIL;
                    OnRoundTableOperation(pScanObject, ref nResult);
                    //计算命中并且未命中
                    if (/*__HitLogic(pScanObject)*/ nResult == EM_SPELL_RESULT.EM_SPELL_RESULT_MISS)
                    {
                        pScanObject.OnDodge(m_pHolder, m_pSpellInfo);
                        m_TargetList.OnUpdateTargetHit(pScanObject);
                        //发送消息
                        continue;
                    }

                    //命中可以震动
                    m_Hitcount++;

                    //每命中一个单位 奖励怒气
                    //if (FightControler.Inst != null)
                    //    FightControler.Inst.OnUpdatePowerValue(m_pHolder.GetGroupType(), m_pSpellInfo.GetSpellRow().getInitHitFury());
                    

                    //bool bCritical = __CriticalAttack(pScanObject);
                    bool bCritical = nResult == EM_SPELL_RESULT.EM_SPELL_RESULT_CRITICAL ? true : false;
                    //爆击逻辑
                    m_pHolder.OnCritical(pScanObject, m_pSpellInfo, bCritical);
                    //被爆击逻辑
                    pScanObject.OnBeCritical(m_pHolder, m_pSpellInfo, bCritical);
                    //逻辑
					bRet = __DoSpellLogic(pSpellRow.getSkillLogicID(), pScanObject, bCritical);
                    LogManager.LogAssert(bRet);
                    
                    for (int m = 0; m < pSpellRow.getBuffList().Length; ++m)
                    {
                        if (pSpellRow.getBuffList()[m] != -1)
                        {
                            EM_IMPACT_RESULT bImpactRet = pScanObject.AddImpact(pSpellRow.getBuffList()[m], m_pHolder, bCritical,GetSpellID());
                            if (bImpactRet == EM_IMPACT_RESULT.EM_IMPACT_RESULT_NORMAL)
                            {
                                m_TargetList.OnUpdateTargetImpact(pScanObject, pSpellRow.getBuffList()[m]);
                            }
                        }
                    }
                }
                //命中结算
                __SpellOverLogic(m_Hitcount);
            }
            //技能结尾部分，缓存所产生的战斗信息 [3/23/2015 Zmy]
            OnCacheSepllInfo();
            return true;
        }

        public bool ActivateEachTick()
        {

            LogManager.LogAssert(m_pHolder);
            LogManager.LogAssert(m_pSpellInfo);
            bool bRet = false;
            //FIGHTOBJECT_LIST targetlist = new FIGHTOBJECT_LIST();
            __CalculateSpellTarget(ref m_TargetList);

            if (m_TargetList.m_nCount > 0)
            {
                SkillTemplate pSpellRow = m_pSpellInfo.GetSpellRow();
                LogManager.LogAssert(pSpellRow);
                // 将该技能的效果作用于目标身上
                ObjectCreature pScanObject = null;
                //产生impact
                for (int TargetIndex = 0; TargetIndex < m_TargetList.m_nCount; TargetIndex++)
                {
                    pScanObject = m_TargetList.m_pObjectList[TargetIndex].m_pTargetObject;
                    //目标临时buff
                    __ImpactEffectOnSpellStartToTarget(pScanObject);
                    //计算命中并且未命中
                    if (__HitLogic(pScanObject) != EM_SPELL_RESULT.EM_SPELL_RESULT_NORMAL)
                    {
                        pScanObject.OnDodge(m_pHolder, m_pSpellInfo);
                        //发送消息
                        continue;
                    }
                    bool bCritical = __CriticalAttack(pScanObject);
                    //爆击逻辑
                    m_pHolder.OnCritical(pScanObject, m_pSpellInfo, bCritical);
                    //被爆击逻辑
                    pScanObject.OnBeCritical(m_pHolder, m_pSpellInfo);
                    //逻辑
					bRet = __DoSpellLogic(pSpellRow.getSkillLogicID(), pScanObject, bCritical);
                    LogManager.LogAssert(bRet);
                    for (int m = 0; m < pSpellRow.getBuffList().Length; ++m)
                    {
                        if (pSpellRow.getBuffList()[m] != -1)
                        {
                            pScanObject.AddImpact(pSpellRow.getBuffList()[m], m_pHolder, bCritical,GetSpellID());
                        }
                    }
                }
            }

            return true;
        }

        public bool DoSubSpell()
        {
            LogManager.LogAssert(m_pHolder);
            LogManager.LogAssert(m_pSpellInfo);
            return true;
        }

        public bool OnFinish()
        {
            LogManager.LogAssert(m_pHolder);
            LogManager.LogAssert(m_pSpellInfo);

            m_pSpellInfo = null;
            m_SpellTarget.CleanUp();
            m_SpellTargetDir = 0.0f;
            m_SpellTargetPosition = new Vector3(0, 0, 0);

            return true;

        }

        public bool UpdateCoolDown(bool bForce = false)
        {
	        
	        LogManager.LogAssert(m_pHolder);
	        if (bForce)
	        {
		        CoolDownList pCoolDown = m_pHolder.GetCoolDownList();
		        LogManager.LogAssert(pCoolDown);
		        pCoolDown.ResetCommonCD((uint)DataTemplate.GetInstance().m_GameConfig.getPersonPublicCD());
		        if (m_pSpellInfo.GetInterruptCooldownTime()>0)
		        {
			        return true;
		        }
		
		        bool bRet = pCoolDown.AddElement(m_pSpellInfo.GetSpellID(), m_pSpellInfo.GetInterruptCooldownTime());
		        if (!bRet)
		        {
			        LogManager.LogAssert(0);
		        }
	        }
	        CoolDownList _pCoolDown = m_pHolder.GetCoolDownList();
	        LogManager.LogAssert(_pCoolDown);
	        _pCoolDown.ResetCommonCD((uint)DataTemplate.GetInstance().m_GameConfig.getPersonPublicCD());
	        if (m_pSpellInfo.GetCoolDownTime()>0)
	        {
		        return true;
	        }
	
	        bool _bRet = _pCoolDown.AddElement(m_pSpellInfo.GetSpellID(), m_pSpellInfo.GetCoolDownTime());
            if (!_bRet)
	        {
		        LogManager.LogAssert(0);
	        }

	        return true;
	        
        }


        public void __ImpactEffectOnSpellStart()
        {
            LogManager.LogAssert(m_pHolder);
            LogManager.LogAssert(m_pSpellInfo);
            SkillTemplate pSpellRow = m_pSpellInfo.GetSpellRow();
            LogManager.LogAssert(pSpellRow);
            for (int i = 0,index = 0; i < pSpellRow.getTemporarySelfBuff().Length; ++i)
            {
                EM_IMPACT_RESULT bRet = m_pHolder.AddImpact(pSpellRow.getTemporarySelfBuff()[i], m_pHolder, false, GetSpellID());
                if (bRet == EM_IMPACT_RESULT.EM_IMPACT_RESULT_NORMAL)
                {
                    m_ValidImpact[index] = pSpellRow.getTemporarySelfBuff()[i];
                    index++;
                }
            }
        }

        public void __ImpactEffectOnSpellStartToTarget(ObjectCreature pTarget)
        {
            LogManager.LogAssert(m_pHolder);
            LogManager.LogAssert(m_pSpellInfo);
            LogManager.LogAssert(pTarget);
            SkillTemplate pSpellRow = m_pSpellInfo.GetSpellRow();
            LogManager.LogAssert(pSpellRow);
            for (int i = 0; i < pSpellRow.getTemporaryTargetBuff().Length; ++i)
            {
                EM_IMPACT_RESULT bRet = pTarget.AddImpact(pSpellRow.getTemporaryTargetBuff()[i], m_pHolder, false, GetSpellID());
                if (bRet == EM_IMPACT_RESULT.EM_IMPACT_RESULT_NORMAL)
                {
                    m_TargetList.OnUpdateTargetImpact(pTarget, pSpellRow.getTemporaryTargetBuff()[i]);
                }
            }
        }

        public bool __ImmuneLogic(ObjectCreature pTarget)
        {
            LogManager.LogAssert(m_pHolder);
            LogManager.LogAssert(m_pSpellInfo);

            return false;		//没有免疫
        }

        public EM_SPELL_RESULT __HitLogic(ObjectCreature pTarget)
        {
	        
		    LogManager.LogAssert(m_pHolder);
	        LogManager.LogAssert(m_pSpellInfo);
            // 目标是友方 直接命中 [3/9/2015 Zmy]
            if (pTarget.GetGroupType() == m_pHolder.GetGroupType())
            {
                return EM_SPELL_RESULT.EM_SPELL_RESULT_NORMAL;
            }
	        /*if ((pTarget.IsInFightState(EM_CARD_FIGHT_STATE_IDLE))||(pTarget.IsInFightState(EM_CARD_FIGHT_STATE_IMM)))
	        {
		        return EM_SPELL_RESULT_MISS;
	        }*/
            /*一次攻击行为的命中判定率P的计算公式为：
                   P = p - b / (〖a((A + X) / (B + X))〗^k + c) + A^'-B'
                   其中A为攻击方命中最终值，B为防御方最终闪避值，A’为攻击方命中最终率，B’为防御方闪避最终率。
                   X、a、b、c、k、p 分别为命中率计算系数，配置在10_config表中。*/
            float nParamX = DataTemplate.GetInstance().m_GameConfig.getDodge_X();
            float nParama = DataTemplate.GetInstance().m_GameConfig.getDodge_a();
            float nParamb = DataTemplate.GetInstance().m_GameConfig.getDodge_b();
            float nParamc = DataTemplate.GetInstance().m_GameConfig.getDodge_c();
            float nParamk = DataTemplate.GetInstance().m_GameConfig.getDodge_k();
            float nParamp = DataTemplate.GetInstance().m_GameConfig.getDodge_p();

            double fParam = (m_pHolder.GetHit() + nParamX) / (pTarget.GetDodge() + nParamX);
            fParam = Math.Pow(fParam, nParamk);
            int fHit = (int)((nParamp - nParamb / (nParama * fParam + nParamc) + m_pHolder.GetHitRate() - pTarget.GetDodgeRate()) * 10000L);
            fHit = Math.Max((int)(DataTemplate.GetInstance().m_GameConfig.getDodge_min() * 10000f), fHit);

//          System.Random ran = new System.Random();
//          int nRand = ran.Next(1, 10000);
            //取系统时间的毫秒数为随机种子!规避伪随机导致的结果相同[9/22/2015 Zmy]
            int iRnd = System.DateTime.Now.Millisecond;
            System.Random randomCoor = new System.Random(iRnd);
            int nRand = randomCoor.Next(1, 10000);
            if (nRand <= fHit)
	        {
                return EM_SPELL_RESULT.EM_SPELL_RESULT_NORMAL;
	        }

            return EM_SPELL_RESULT.EM_SPELL_RESULT_MISS;	       
        }

        public bool __CriticalAttack(ObjectCreature pTarget)
        {	        
		    LogManager.LogAssert(m_pHolder);
	        LogManager.LogAssert(m_pSpellInfo);
            /*一次攻击行为的暴击判定率P的计算公式为：
                P = p - b / (〖a((A + X) / (B + X))〗^k + c) + A^'-B'
                其中A为攻击方暴击最终值，B为防御方最终韧性值，A’为攻击方暴击最终率，B’为防御方韧性最终率。
                X、a、b、c、k、p 分别为暴击率计算系数，配置在10_config表中。*/
            float nParamX = DataTemplate.GetInstance().m_GameConfig.getCritical_X();
            float nParama = DataTemplate.GetInstance().m_GameConfig.getCritical_a();
            float nParamb = DataTemplate.GetInstance().m_GameConfig.getCritical_b();
            float nParamc = DataTemplate.GetInstance().m_GameConfig.getCritical_c();
            float nParamk = DataTemplate.GetInstance().m_GameConfig.getCritical_k();
            float nParamp = DataTemplate.GetInstance().m_GameConfig.getCritical_p();

            double fParam = (m_pHolder.GetCritical() + nParamX) / (pTarget.GetTenacity() + nParamX);
            fParam = Math.Pow(fParam, nParamk);
            int fHit = (int)((nParamp - nParamb / (nParama * fParam + nParamc) + m_pHolder.GetCriticalRate() - pTarget.GetTenacityRate()) * 10000L);
            fHit = Math.Min((int)(DataTemplate.GetInstance().m_GameConfig.getCritical_max() * 10000f), fHit);
            //System.Random ran = new System.Random();
            //int nRand = ran.Next(1, 10000);

            int iRnd = System.DateTime.Now.Millisecond;
            System.Random randomCoor = new System.Random(iRnd);
            int nRand = randomCoor.Next(1, 10000);

            if (nRand <= fHit)
	        {
		        return true;
	        }
	        return false;

        }

        public bool OnInterrupt()
        {

            LogManager.LogAssert(m_pHolder);
            LogManager.LogAssert(m_pSpellInfo);

            return false;
        }
        private void AddLinkEff()
        {
            if (m_pSpellInfo.GetSpellRow().getUnderAttackEffLink() == -1)
                return;
            List<ObjectCreature> tempobjlist = new List<ObjectCreature>();
            for (int i = 0; i < m_TargetList.m_nCount; ++i)
            {
                tempobjlist.Add(m_TargetList.m_pObjectList[i].m_pTargetObject);
            }
            EffectManager.GetInstance().InstanceEffect_Link(m_pSpellInfo.GetSpellRow().getBallIsticEffID()[0], m_pHolder, m_pHolder.GetGameObject().transform.position, tempobjlist);
        }
        public bool CalcPassivitySpellTarget(ref FIGHTOBJECT_LIST targetlist)
        {
            SkillTemplate pRow = m_pSpellInfo.GetSpellRow();
            switch (m_pSpellInfo.GetTargetType())
            {
                case (int)EM_TARGET_TYPE.EM_TARGET_FRIEND:
                    {
                        int nID = -1;
                        if (ObjectSelf.GetInstance().Teams.IsHeroInTeam(m_pHolder.GetGuid(),ref nID))
                        {
                            for (int i = 0; i < GlobalMembers.MAX_TEAM_CELL_COUNT; ++i)
                            {
                                X_GUID _guid = ObjectSelf.GetInstance().Teams.m_Matrix[nID, i];
                                ObjectCard obj = ObjectSelf.GetInstance().HeroContainerBag.FindHero(_guid);
                                if (obj != null)
                                {
                                    targetlist.Add((ObjectCreature)obj);
                                }
                            }
                        }
                    }
                    break;
                case (int)EM_TARGET_TYPE.EM_TARGET_SELF:
                    {
                        targetlist.Add((ObjectCreature)m_pHolder);
                    }
                    break; 
                default:
                    break;
            }
            return true;
        }
        public bool __CalculateSpellTarget(ref FIGHTOBJECT_LIST targetlist)
        {
            targetlist.m_pObjectList.Clear();
            LogManager.LogAssert(m_pHolder);
            LogManager.LogAssert(m_pSpellInfo);
            SkillTemplate pRow = m_pSpellInfo.GetSpellRow();
            LogManager.LogAssert(pRow);
            switch (m_pSpellInfo.GetTargetType())
            {
                case (int)EM_TARGET_TYPE.EM_TARGET_FRIEND:
                    {
                        if (m_pSpellInfo.IsNeedTarget())
                        {
                            if (m_SpellTarget.IsValid())
                            {
                                SceneObjectManager pScene = SceneObjectManager.GetInstance();
                                LogManager.LogAssert(pScene);
                                ObjectCreature pTarget = pScene.GetObjectHeroByGUID(m_SpellTarget);
                                if (pTarget != null)
                                {
                                    targetlist.Add((ObjectCreature)pTarget);
                                }

                            }
                        }
                        else if (pRow.getCoverage() < 0.0f)
                        {
                            SceneObjectManager pScene = SceneObjectManager.GetInstance();
                            LogManager.LogAssert(pScene);
                            SCANOPERATOR_INIT init = new SCANOPERATOR_INIT();
                            init.m_Type = m_pSpellInfo.GetTargetType();
                            pScene.ScanByObject(m_pHolder, ref init);
                            for (int j = 0; j < init.m_ObjectList.Count; j++)
                            {
                                targetlist.Add((ObjectCreature)init.m_ObjectList[j]);
                            }
                        }
                    }
                    break;
                case (int)EM_TARGET_TYPE.EM_TARGET_ENEMY:
                    {
                        if (m_pSpellInfo.IsNeedTarget())
                        {
                            if (m_SpellTarget.IsValid())
                            {
                                SceneObjectManager pScene = SceneObjectManager.GetInstance();
                                LogManager.LogAssert(pScene);
                                ObjectCreature pTarget = pScene.GetSceneObjectByGUID(m_SpellTarget);
                                if (pTarget != null)
                                {
                                    targetlist.Add((ObjectCreature)pTarget);
                                }
                            }
                        }
                        else if (pRow.getCoverage() < 0.0f)
                        {
                            SceneObjectManager pScene = SceneObjectManager.GetInstance();
                            LogManager.LogAssert(pScene);
							SCANOPERATOR_INIT init = new SCANOPERATOR_INIT();
                            init.m_Type = m_pSpellInfo.GetTargetType();
                            pScene.ScanByObject(m_pHolder, ref init);
                            for (int j = 0; j < init.m_ObjectList.Count; j++)
                            {
                                targetlist.Add((ObjectCreature)init.m_ObjectList[j]);
                            }
                        }
                    }
                    break;
                case (int)EM_TARGET_TYPE.EM_TARGET_SELF:
                    {
                        targetlist.Add((ObjectCreature)m_pHolder);
                    }
                    break;
                case (int)EM_TARGET_TYPE.EM_TARGET_ALL:
                    {
                        SceneObjectManager pScene = SceneObjectManager.GetInstance();
                        LogManager.LogAssert(pScene);
						SCANOPERATOR_INIT init = new SCANOPERATOR_INIT();
                        init.m_Type = m_pSpellInfo.GetTargetType();
                        pScene.ScanByObject(m_pHolder, ref init);
                        for (int j = 0; j < init.m_ObjectList.Count; j++)
                        {
                            targetlist.Add((ObjectCreature)init.m_ObjectList[j]);
                        }
                    }
                    break;
                case (int)EM_TARGET_TYPE.EM_TARGET_FRIEND_NO_SELF:
                    {
                        SceneObjectManager pScene = SceneObjectManager.GetInstance();
                        LogManager.LogAssert(pScene);
                        SCANOPERATOR_INIT init = new SCANOPERATOR_INIT();
                        init.m_Type = m_pSpellInfo.GetTargetType();
                        pScene.ScanByObject(m_pHolder, ref init);
                        for (int j = 0; j < init.m_ObjectList.Count; j++)
                        {
                            targetlist.Add((ObjectCreature)init.m_ObjectList[j]);
                        }
                    }
                    break;
                case (int)EM_TARGET_TYPE.EM_TARGET_FRIEND_MIN_HPPERCENT:
                    {
                        SceneObjectManager pScene = SceneObjectManager.GetInstance();
                        LogManager.LogAssert(pScene);
						SCANOPERATOR_INIT init = new SCANOPERATOR_INIT();
                        init.m_Type = m_pSpellInfo.GetTargetType();
                        pScene.ScanByObject(m_pHolder, ref init);
                        for (int j = 0; j < init.m_ObjectList.Count; j++)
                        {
                            targetlist.Add((ObjectCreature)init.m_ObjectList[j]);
                        }
                    }
                    break;
                case (int)EM_TARGET_TYPE.EM_TARGET_ALL_NO_SELF:
                    {
                        SceneObjectManager pScene = SceneObjectManager.GetInstance();
                        LogManager.LogAssert(pScene);
						SCANOPERATOR_INIT init = new SCANOPERATOR_INIT();
                        init.m_Type = m_pSpellInfo.GetTargetType();
                        pScene.ScanByObject(m_pHolder, ref init);
                        for (int j = 0; j < init.m_ObjectList.Count; j++)
                        {
                            targetlist.Add((ObjectCreature)init.m_ObjectList[j]);
                        }
                    }
                    break;
                case (int)EM_TARGET_TYPE.EM_TARGET_ENEMY_MIN_HPPERCENT:
                    {
                        SceneObjectManager pScene = SceneObjectManager.GetInstance();
                        LogManager.LogAssert(pScene);
						SCANOPERATOR_INIT init = new SCANOPERATOR_INIT();
                        init.m_Type = m_pSpellInfo.GetTargetType();
                        pScene.ScanByObject(m_pHolder, ref init);
                        for (int j = 0; j < init.m_ObjectList.Count; j++)
                        {
                            targetlist.Add((ObjectCreature)init.m_ObjectList[j]);
                        }
                    }
                    break;
                case (int)EM_TARGET_TYPE.EM_TARGET_ATTACK_ME:
                    {
                        SceneObjectManager pScene = SceneObjectManager.GetInstance();
                        LogManager.LogAssert(pScene);
                        ObjectCreature pTarget = pScene.GetObjectHeroByGUID(m_SpellTarget);
                        if (pTarget != null)
                        {
                            targetlist.Add((ObjectCreature)pTarget);
                        }
                    }
                    break;
                case (int)EM_TARGET_TYPE.EM_TARGET_ENEMY_RAND:
                    {
                        SceneObjectManager pScene = SceneObjectManager.GetInstance();
                        LogManager.LogAssert(pScene);
						SCANOPERATOR_INIT init = new SCANOPERATOR_INIT();
                        init.m_Type = m_pSpellInfo.GetTargetType();
                        pScene.ScanByObject(m_pHolder, ref init);
                        for (int j = 0; j < init.m_ObjectList.Count; j++)
                        {
                            targetlist.Add((ObjectCreature)init.m_ObjectList[j]);
                        }
                    }
                    break;
                case (int)EM_TARGET_TYPE.EM_TARGET_IMPACT_CASTER:
                    {
                        SceneObjectManager pScene = SceneObjectManager.GetInstance();
                        LogManager.LogAssert(pScene);
                        ObjectCreature pTarget = pScene.GetObjectHeroByGUID(m_SpellTarget);
                        if (pTarget != null)
                        {
                            targetlist.Add((ObjectCreature)pTarget);
                        }
                    }
                    break;
                case (int)EM_TARGET_TYPE.EM_TARGET_FRIEND_RAND:
                    {
                        SceneObjectManager pScene = SceneObjectManager.GetInstance();
                        LogManager.LogAssert(pScene);
                        SCANOPERATOR_INIT init = new SCANOPERATOR_INIT();
                        init.m_Type = m_pSpellInfo.GetTargetType();
                        int m_Tagnum = m_pSpellInfo.GetBaseSpellTargetNumber();
                        pScene.ScanByObject(m_pHolder, ref init, m_Tagnum);
                        for (int j = 0; j < init.m_ObjectList.Count; j++)
                        {
                            targetlist.Add((ObjectCreature)init.m_ObjectList[j]);
                        }
                    }
                    break;
                case (int)EM_TARGET_TYPE.EM_TARGET_ENEMY_RANDOM:
                    {
                        if (m_pSpellInfo.IsNeedTarget())
                        {
                            if (m_SpellTarget.IsValid())
                            {
                                SceneObjectManager pScene = SceneObjectManager.GetInstance();
                                LogManager.LogAssert(pScene);
                                ObjectCreature pTarget = pScene.GetObjectHeroByGUID(m_SpellTarget);
                                if (pTarget != null)
                                {
                                    targetlist.Add((ObjectCreature)pTarget);
                                }
                                SCANOPERATOR_INIT init = new SCANOPERATOR_INIT();
                                init.m_Type = m_pSpellInfo.GetTargetType();
                                pScene.ScanByObject(m_pHolder, ref init, m_pSpellInfo.GetBaseSpellTargetNumber(), pTarget);
/*                                targetlist.m_pObjectList.Clear();*/

                                for (int j = 0; j < init.m_ObjectList.Count; j++)
                                {
                                    targetlist.Add((ObjectCreature)init.m_ObjectList[j]);
                                }                              
                            }
                        }
                        else
                        {
                            LogManager.Log("FUCK");
                        }
                    }
                    break;
                case (int)EM_TARGET_TYPE.EM_TARGET_FRIEND_RANDOM:
                    {
                        if (m_pSpellInfo.IsNeedTarget())
                        {
                            if (m_SpellTarget.IsValid())
                            {
                                SceneObjectManager pScene = SceneObjectManager.GetInstance();
                                LogManager.LogAssert(pScene);
                                ObjectCreature pTarget = pScene.GetObjectHeroByGUID(m_SpellTarget);
                                if (pTarget != null)
                                {
                                    targetlist.Add((ObjectCreature)pTarget);
                                }
                                SCANOPERATOR_INIT init = new SCANOPERATOR_INIT();
                                init.m_Type = m_pSpellInfo.GetTargetType();
                                pScene.ScanByObject(m_pHolder, ref init, m_pSpellInfo.GetBaseSpellTargetNumber(), pTarget);
                                for (int j = 0; j < init.m_ObjectList.Count; j++)
                                {
                                    targetlist.Add((ObjectCreature)init.m_ObjectList[j]);
                                }
                            }
                        }
                        else
                        {
                            LogManager.Log("FUCK");
                        }
                    }
                    break;
                case (int)EM_TARGET_TYPE.EM_TARGET_SELF_RANDOM:
                    {
                        if (m_pSpellInfo.IsNeedTarget())
                        {
                            if (m_SpellTarget.IsValid())
                            {
                                SceneObjectManager pScene = SceneObjectManager.GetInstance();
                                LogManager.LogAssert(pScene);
                                ObjectCreature pTarget = pScene.GetSceneObjectByGUID(m_SpellTarget);
                                if (pTarget != null)
                                {
                                    targetlist.Add((ObjectCreature)pTarget);
                                    int m_Tagnum = m_pSpellInfo.GetBaseSpellTargetNumber();
                                    if (pTarget.GetGameObject().GetHashCode() != m_pHolder.GetGameObject().GetHashCode())
                                    {
                                        targetlist.Add((ObjectCreature)m_pHolder);                                       
                                    }
                                    else
                                    {
                                        m_Tagnum = m_Tagnum + 1;
                                    }

                                    SCANOPERATOR_INIT init = new SCANOPERATOR_INIT();
                                    init.m_Type = m_pSpellInfo.GetTargetType();
                                    pScene.ScanByObject(m_pHolder, ref init, m_Tagnum, pTarget);
                                    for (int j = 0; j < init.m_ObjectList.Count; j++)
                                    {
                                        targetlist.Add((ObjectCreature)init.m_ObjectList[j]);
                                    }
                                }
                            }
                        }
                        else
                        {
                            LogManager.Log("FUCK");
                        }
                    }
                    break;
                default:
                    break;
            }
            return true;

        }

        public EM_IMPACT_RESULT AddImpactToTarget(ObjectCreature pCreature, int impactID, bool bCritical)
        {

            LogManager.LogAssert(pCreature);
            LogManager.LogAssert(m_pHolder);
            LogManager.LogAssert(m_pSpellInfo);

            EM_IMPACT_RESULT nResult = pCreature.AddImpact(impactID, m_pHolder, bCritical,GetSpellID());

            return nResult;

        }

        //技能逻辑,目标不一定是有效目标
        public bool __DoSpellLogic(int nSpellLogicID, ObjectCreature pTarget, bool bCritical)
        {
	        
	        LogManager.LogAssert(m_pHolder);
	        LogManager.LogAssert(m_pSpellInfo);

	        switch(nSpellLogicID)
	        {
	        case (int)EM_SPELL_LOGIC.EM_SPELL_LOGIC1:
		        {
			        return SpellLogic.DoLogic1(m_pHolder, pTarget, m_pSpellInfo, bCritical);
		        }
	        case (int)EM_SPELL_LOGIC.EM_SPELL_LOGIC2:
		        {
			        return SpellLogic.DoLogic2(m_pHolder, pTarget, m_pSpellInfo, bCritical);
		        }
            case (int)EM_SPELL_LOGIC.EM_SPELL_LOGIC3:
		        {
			        return SpellLogic.DoLogic3(m_pHolder, pTarget, m_pSpellInfo, bCritical);
		        }
            case (int)EM_SPELL_LOGIC.EM_SPELL_LOGIC4:
		        {
			        return SpellLogic.DoLogic4(m_pHolder, pTarget, m_pSpellInfo, bCritical);
		        }
            case (int)EM_SPELL_LOGIC.EM_SPELL_LOGIC5:
		        {
			        return SpellLogic.DoLogic5(m_pHolder, pTarget, m_pSpellInfo, bCritical);
		        }
            case (int)EM_SPELL_LOGIC.EM_SPELL_LOGIC6:
		        {
			        return SpellLogic.DoLogic6(m_pHolder, pTarget, m_pSpellInfo, bCritical);
		        }
            case (int)EM_SPELL_LOGIC.EM_SPELL_LOGIC7:
		        {
			        return SpellLogic.DoLogic7(m_pHolder, pTarget, m_pSpellInfo, bCritical);
		        }
            case (int)EM_SPELL_LOGIC.EM_SPELL_LOGIC11:
		        {
			        return SpellLogic.DoLogic11(m_pHolder, pTarget, m_pSpellInfo, bCritical);
		        }
            case (int)EM_SPELL_LOGIC.EM_SPELL_LOGIC12:
		        {
			        return SpellLogic.DoLogic12(m_pHolder, pTarget, m_pSpellInfo, bCritical);
		        }
            case (int)EM_SPELL_LOGIC.EM_SPELL_LOGIC14:
                {
                    return SpellLogic.DoLogic14(m_pHolder, pTarget, m_pSpellInfo, bCritical);
                }
            case (int)EM_SPELL_LOGIC.EM_SPELL_LOGIC15:
                {
                    return SpellLogic.DoLogic15(m_pHolder, pTarget, m_pSpellInfo, bCritical);
                }
            case (int)EM_SPELL_LOGIC.EM_SPELL_LOGIC16:
                {
                    return SpellLogic.DoLogic16(m_pHolder, pTarget, m_pSpellInfo, bCritical);
                }
            case (int)EM_SPELL_LOGIC.EM_SPELL_LOGIC17:
                {
                    return SpellLogic.DoLogic17(m_pHolder, pTarget, m_pSpellInfo, bCritical);
                }
            case (int)EM_SPELL_LOGIC.EM_SPELL_LOGIC18:
                {
                    return SpellLogic.DoLogic18(m_pHolder, pTarget, m_pSpellInfo, bCritical);
                }
	        default:
		        {
                        //其他id不处理即可。不用error [8/4/2015 Zmy]
//                     LogManager.LogAssert(nSpellLogicID);
//                     LogManager.LogToFile("!!!!Error:__DoSpellLogic()'s nSpellLogicID param is null :" + nSpellLogicID);
		        }
                break;
	        }
	        return true;
        }

        // 技能条件验证 [3/2/2015 Zmy]
        public bool _CheckSpellConditions()
        {
            //CD 检查：
            //if (!_CheckSpellCooldown())
            //   return false;

            //消耗检查：
            if (!m_pSpellInfo.IsSkillRelease(m_pHolder))
                return false;

            return true;
        }

        public bool _CheckSpellCooldown()
        {
            //CD 检查：
            CoolDownList pCoolDownList = m_pHolder.GetCoolDownList();
            if (pCoolDownList == null)
                return false;

            if ((m_pSpellInfo.GetCoolDownTime() > 0) && pCoolDownList.IsSpellCoolDown(m_pSpellInfo.GetSpellID()))
            {
                return false;
            }
            return true;
        }

        //释放奖励 无视命中关系 [3/2/2015 Zmy]
        public void _OnFreeAward()
        {
            //怒气奖励 [3/2/2015 Zmy]
            //if (FightControler.Inst != null)
            //    FightControler.Inst.OnUpdatePowerValue(m_pHolder.GetGroupType(), m_pSpellInfo.GetSpellRow().getSkillAttackFury());

        }
        //释放消耗 无视命中关系
        public void _OnFreeConsume()
        {
            if (m_pHolder == null)
                return;
            //生命消耗.怒气消耗
            m_pSpellInfo.SkillResourceUpdate(m_pHolder);
        }
        //技能每命中一次目标产生结算后的处理 [3/2/2015 Zmy]
        public void __SpellOverLogic(int Hitcount)
        {
            if (Hitcount >= 1)
            {
                if (FightEditorContrler.GetInstantiate() != null)
                    FightEditorContrler.GetInstantiate().SkillShake(m_pSpellInfo.GetSpellRow().getVibrationScreen(), EM_SPELL_SHAKE_TYPE.EM_SPELL_SHAKE_TYPE_HIT);

                AudioControler.Inst.PlaySound(m_pSpellInfo.GetSpellRow().getUnderAttackSound());
            }
        }

        // 技能结束时，缓存此次技能生成的战斗信息 [3/23/2015 Zmy]
        public void OnCacheSepllInfo()
        {
            FightInfo _info = new FightInfo();
            bool bAttacker = m_pHolder.GetGroupType() == EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO ? true : false;
            _info.SetAttack(m_pHolder.GetTeamPos(), bAttacker);
            _info.m_SpellID                 = m_pSpellInfo.GetSpellID();
            _info.m_nCount                  = (byte)m_TargetList.m_nCount;
            for (int i = 0; i < m_ValidImpact.Length; ++i)
            {
                if (m_ValidImpact[i] < 0)
                    continue;
                //只记录有效的Impact [3/23/2015 Zmy]
                _info.m_Impact[i] = m_ValidImpact[i];
                _info.m_nImpactCount++;
            }

            for (int n = 0; n < m_TargetList.m_nCount; ++n)
            {
                DefenceInfo _deInfo = new DefenceInfo();
                bool _bAttacker = m_TargetList.m_pObjectList[n].m_pTargetObject.GetGroupType() == EM_OBJECT_TYPE.EM_OBJECT_TYPE_MONSTER ? true : false;
                _deInfo.SetDefencer(m_TargetList.m_pObjectList[n].m_pTargetObject.GetTeamPos(), _bAttacker);
                _deInfo.m_Hit               = m_TargetList.m_pObjectList[n].m_bHit;
                for (int nIndex = 0; nIndex < m_TargetList.m_pObjectList[n].m_ValidImpact.Length;++nIndex )
                {
                    if (m_TargetList.m_pObjectList[n].m_ValidImpact[nIndex] < 0)
                        continue;                    
                    
                    _deInfo.m_Impact[nIndex] = m_TargetList.m_pObjectList[n].m_ValidImpact[nIndex];
                    _deInfo.m_nImpactCount++;
                }
                _deInfo.m_RemainHP = m_TargetList.m_pObjectList[n].m_pTargetObject.GetHP();

                _info.m_DefenceInfo[n].Copy(_deInfo);
            }

            SceneObjectManager.GetInstance()._CacheFightInfo(_info);
        }

        /// <summary>
        /// 技能行为圆桌算法
        /// </summary>
        /// <param name="pTarget">技能目标</param>
        /// <param name="nResult">返回结果</param>
        private void OnRoundTableOperation(ObjectCreature pTarget, ref EM_SPELL_RESULT nResult)
        {
            int nAttackAttributeCritical = m_pHolder.GetBaseCritical() + m_pHolder.GetDevelopAttributeSub((int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE); //攻击者暴击率 [10/15/2015 Zmy]
            int nTargetAttributeTenacity = pTarget.GetBaseTenacity() + pTarget.GetDevelopAttributeSub((int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITYRATE);     //目标韧性率 [10/15/2015 Zmy]

            int nAttributeCritical = Math.Min((int)DataTemplate.GetInstance().m_GameConfig.getMax_crit_limit(), nAttackAttributeCritical - nTargetAttributeTenacity); //属性暴击率

            // 最终暴击率 = 攻击者属性暴击率+攻击者（技能附带）暴击率+攻击者暴击率buff-目标韧性率buff [10/15/2015 Zmy]
            int nFinalCritical = nAttributeCritical +
                                 m_pSpellInfo.GetSpellRow().getSkillCrit() + 
                                 m_pHolder.GetSpellAttibute((int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE) - pTarget.GetSpellAttibute((int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITYRATE);
            //修正最终暴击率
            if (nFinalCritical <= 0)
                nFinalCritical = 0;
            else
                nFinalCritical *= 10;

            if (nFinalCritical >= 10000) //暴击达到上限，直接得出结果 [10/16/2015 Zmy]
            {
                nResult = EM_SPELL_RESULT.EM_SPELL_RESULT_CRITICAL;
                return;
            }

            int nAttackAttriHit = m_pHolder.GetBaseHit() + m_pHolder.GetDevelopAttributeSub((int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HITRATE);       //攻击者命中率 [10/15/2015 Zmy]
            int nTargetAttriDodge = pTarget.GetBaseDodge() + pTarget.GetDevelopAttributeSub((int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGERATE);     //目标闪避率 [10/15/2015 Zmy]

            int nAttributeDodge = Math.Min((int)DataTemplate.GetInstance().m_GameConfig.getMax_hiding_limit(), nTargetAttriDodge - nAttackAttriHit); //目标属性闪避率

            // 目标最终闪避率=目标属性闪避率-攻击方（技能附带)命中率+目标闪避率buff-攻击方命中率buff+固定未命中几率 [10/15/2015 Zmy]
            int nFinalDodge = nAttributeDodge -
                              m_pSpellInfo.GetSpellRow().getSkillHit() + 
                              pTarget.GetSpellAttibute((int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGERATE) - m_pHolder.GetSpellAttibute((int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HITRATE) +
                              (int)DataTemplate.GetInstance().m_GameConfig.getFix_nothit_value();

            if (nFinalDodge <= 0)
                nFinalDodge = 0;
            else
            {
                nFinalDodge = (nFinalDodge * 10) > (10000 - nFinalCritical) ? (10000 - nFinalCritical) : (nFinalDodge * 10); //修正剩余闪避概率区间 [10/16/2015 Zmy]
            }


            //生成随机种子 [10/15/2015 Zmy]
            int iRnd = System.DateTime.Now.Millisecond;
            System.Random randomCoor = new System.Random(iRnd);
            int nRand = randomCoor.Next(1, 10000);

            if (nRand > 0 && nRand <= nFinalCritical)//暴击区间
            {
                nResult = EM_SPELL_RESULT.EM_SPELL_RESULT_CRITICAL;
            }
            else if (nRand <= nFinalDodge + nFinalCritical)//闪避区间
            {
                nResult = EM_SPELL_RESULT.EM_SPELL_RESULT_MISS;
            }
            else//普通命中
            {
                nResult = EM_SPELL_RESULT.EM_SPELL_RESULT_NORMAL;
            }
        }
    }
}
