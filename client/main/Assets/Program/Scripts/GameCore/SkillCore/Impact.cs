using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using DreamFaction.GameNetWork.Data;
using DreamFaction.LogSystem;
using DreamFaction.GameNetWork;
using DreamFaction.GameCore;
using DreamFaction.GameAudio;
/// <summary>
/// 可挂在对象身上的impact效果,具有持续性
/// </summary>
namespace DreamFaction.SkillCore
{
    public delegate bool ImpctLogicFunction(ObjectCreature pHolder, Impact pImpact, int nFlag);

    public class Impact
    {

        private int m_ObjectID;                     //管理器中id
        ImpactData m_pImpactObject;             //impact存档数据
        private int m_nLastIntervalBeginTime;       // 间隔时间上次记录开始时间
        private int m_uImpactBeginTime;             // impact开始时间
        ObjectCreature m_pHolder;                       //持有人
        private int m_nIndex;                       //在buf列表中的索引
        private int m_ImpactHP;                     //生命之盾
        private int m_ImpactActiveCount;            //激活次数
        private int m_DisappearParam;               //消失参数
        private bool m_bActive;                     //是否有效
        private bool m_bNew;                            //新的
        BuffTemplate m_pImpactRow;                  //技能表格数据
        Impact m_pPartner;						//伙伴impact 逻辑id107专用
        private int m_Param;                        //额外参数
        private int m_Hurt;                         //伤害
        private int m_Heal;                         //治疗
        private int m_SpellID;                      //技能id
        public int m_AttributeEffectRefixCount;
        public int m_HurtCount;                    //记录受到的伤害
        private float m_FimllCount;                   //记录受到伤害的float
        private int m_RefHp;                        //记录回复的生命
        public __ITEM_ATTRIBUTE[] m_AttributeEffectRefix = new __ITEM_ATTRIBUTE[GlobalMembers.MAX_IMPACT_ATTRIBUTE_COUNT];	//buffer产生的effect
        public int[] m_RefixParam = new int[GlobalMembers.MAX_IMPACT_LOGIC_PARAM_COUNT];    //impact 参数修正

        private SpellInfo m_TempSpellInfo = new SpellInfo();     //子技能技能信息
        private Spell     m_TempSpell     = new Spell();         //子技能

        public Impact()
        {
            m_ImpactHP = 0;
            m_ImpactActiveCount = 0;
            m_DisappearParam = 0;
            m_DisappearParam = 0;
            m_bNew = true;
            m_bActive = false;
            m_pPartner = null;
            for (int i = 0; i < GlobalMembers.MAX_IMPACT_LOGIC_PARAM_COUNT; i++)
            {
                m_RefixParam[i] = 0;
            }
            m_pImpactObject = new ImpactData();
            m_pImpactRow = new BuffTemplate();

            CleanUp();
        }
        public bool Init(int nImpactID, ObjectCreature pCaster, ObjectCreature pHolder, int nSpellID)
        {
            m_pImpactObject.m_nImpactID = nImpactID;
            if (pCaster != null)
            {
                m_pHolder = pHolder;
                m_pImpactObject.m_CasterGUID.Copy(pCaster.GetGuid());
            }

            BuffTemplate pImpactRow = (BuffTemplate)DataTemplate.GetInstance().m_BufferTable.getTableData(m_pImpactObject.m_nImpactID);

            LogManager.LogAssert(pImpactRow);
            m_pImpactRow = pImpactRow;

            m_pImpactObject.m_ImpactTime = m_pImpactRow.getDurationTime();
            //m_pImpactObject.m_nCount     = m_pImpactRow.m;
            if (m_pImpactRow.getBuffEffectInterval() > 0)
            {
                m_pImpactObject.m_IntervalTime = m_pImpactRow.getBuffEffectInterval();
            }
            for (int i = 0; i < GlobalMembers.MAX_IMPACT_LOGIC_PARAM_COUNT; i++)
            {
                m_RefixParam[i] = 0;
            }
            m_bNew = true;
            m_bActive = true;
            m_pPartner = null;
            m_SpellID = nSpellID;
            return true;
        }

        public void CleanUp()
        {
            m_pImpactObject.CleanUp();
            m_pImpactRow = null;
            m_ImpactHP = 0;
            m_ImpactActiveCount = 0;
            m_DisappearParam = 0;
            m_bNew = false;
            m_bActive = false;
            m_pPartner = null;
            for (int i = 0; i < GlobalMembers.MAX_IMPACT_LOGIC_PARAM_COUNT; i++)
            {
                m_RefixParam[i] = 0;
            }
        }

        public BuffTemplate GetImpactRow()
        {
            return m_pImpactRow;
        }
        public Impact GetPartner()
        {
            return m_pPartner;
        }
        public void SetPartner(Impact pImpact)
        {
            m_pPartner = pImpact;
        }
        public int GetType_()
        {
            if (IsActive())
            {
                return m_pImpactRow.getConduce();
            }
            return 2;
        }
        public bool IsActive()
        {
            if (m_bActive)
            {
                if (m_pImpactObject.IsOverTime())
                {
                    return false;
                }
            }
            return m_bActive;
        }

        public bool IsDeadInvalid()
        {
            return true;
        }

        public bool IsOffLineInvalid()
        {
            return true;
        }

        public int GetImpactID()
        {
            return m_pImpactObject.m_nImpactID;
        }

        public bool IsCanCancel()
        {
            return true;
        }

        public int GetImpactTime()
        {
            if (IsActive())
            {
                return m_pImpactObject.m_ImpactTime;
            }

            return 0;
        }

        public void RefixImpactTime(int nValue)
        {

            //if (nValue >= 0)
            //{
            m_pImpactObject.m_ImpactTime = (int)(m_pImpactObject.m_ImpactTime *( 1 + nValue / 1000f));
            //}       
        }
        public void SetImpactHP(int nImpactHP)
        {
            m_ImpactHP = nImpactHP;
        }

        public int GetImpactHp()
        {
            return m_ImpactHP;
        }

        public void SetImpactActiveCount(int nCount)
        {
            m_ImpactActiveCount = nCount;
        }
        public int GetImpactActiveCount()
        {
            return m_ImpactActiveCount;
        }

        public int GetMutexID()
        {
            LogManager.LogAssert(IsActive());
            BuffTemplate pImpactRow = GetImpactRow();
            LogManager.LogAssert(pImpactRow);

            return pImpactRow.getExclusionID();
        }

        public int GetMutexLevel()
        {
            LogManager.LogAssert(IsActive());
            BuffTemplate pImpactRow = GetImpactRow();
            LogManager.LogAssert(pImpactRow);

            return pImpactRow.getExclusionPriority();

        }

        public int GetElapsedTime()
        {
            if (IsActive())
            {
                return m_pImpactObject.m_ElapsedTime;
            }

            return 0;
        }

        public void SetElapsedTime(int val)
        {
            if (IsActive())
            {
                m_pImpactObject.m_ElapsedTime = val;
            }
        }
        public int GetSpellID()
        {
            return m_SpellID;
        }//属于某技能
        public void SetSpellID(int nID)
        {
            m_SpellID = nID;
        }
        public int GetIntervalTime()
        {
            if (IsActive())
            {
                return m_pImpactObject.m_IntervalTime;
            }

            return 0;
        }

        public void SetIntervalTime(int val)
        {
            if (IsActive())
            {
                m_pImpactObject.m_IntervalTime = val;
            }
        }

        public int GetIntervalElapsed()
        {
            if (IsActive())
            {
                return m_pImpactObject.m_ElapsedIntervalTime;
            }

            return 0;
        }

        public void SetIntervalElapsed(int val)
        {
            if (IsActive())
            {
                m_pImpactObject.m_ElapsedIntervalTime = val;
            }

        }

        public int GetLayedCount()
        {
            return m_pImpactObject.m_nLayedCount;
        }

        public void IncreaseLayedCount()
        {
            if (IsActive())
            {
                ++m_pImpactObject.m_nLayedCount;
            }
        }

        public EM_IMPACT_LOGIC GetImpactLogicID()
        {
            if (IsActive())
            {
                BuffTemplate pImpactRow = GetImpactRow();
                if (pImpactRow != null)
                {
                    return (EM_IMPACT_LOGIC)pImpactRow.getBuffType();
                }
            }

            return EM_IMPACT_LOGIC.EM_IMPACT_LOGIC_INVALID;
        }

        public int GetGroupID()
        {
            if (IsActive())
            {
                BuffTemplate pImpactRow = GetImpactRow();
                if (pImpactRow != null)
                {
                    //return pImpactRow.m_buffgroup;
                }
            }

            return -1;
        }

        public X_GUID GetCaster()
        {
            if (IsActive())
            {
                return m_pImpactObject.m_CasterGUID;
            }
            return null;
        }

        public void SetIndex(int nIndex)
        {

            if ((nIndex < 0) || (nIndex >= GlobalMembers.MAX_IMPACT_NUMBER))
            {
                LogManager.LogAssert(0);
            }
            m_nIndex = nIndex;

        }

        public void OnOffline()
        {
            if (IsActive())
            {
                if (IsOffLineInvalid())
                {
                    //m_pHolder.RemoveImpact(this);
                }

            }
        }

        /// <summary>
        /// 移除Buff
        /// </summary>
        /// <returns></returns>
        public bool OnDisappear()
        {
            //清除Buff音效
            AudioControler.Inst.PlaySound(m_pImpactRow.getBuffEndSound());

            List<SPELL_EVENT> pList = m_pHolder.GetSpellEventQueue().GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_BEADDIMPACT);
            LogManager.LogAssert(pList);

            foreach (var item in pList)
            {
                Impact pImpact = item.m_pImpact;
                LogManager.LogAssert(pImpact);
                pImpact.OnWhenAddImpact(m_pHolder, pImpact.GetImpactID(),true);
            }

            BuffTemplate pImpactRow = GetImpactRow();

            if (pImpactRow != null && pImpactRow.getBuffType() >= 0)
            {
                //自然消失
                if (pImpactRow.getBuffType() == 2019)
                {
                    long nHp = (m_pHolder.GetMaxHP() * pImpactRow.getParam()[2]) / 1000;
                    if (nHp <= 0)
                    {
                        nHp = 1;
                    }
                    m_pHolder.SetHP(nHp);
                }
                bool bRet = __DoImpactDisappearLogic(pImpactRow.getBuffType());
                if (bRet)
                {
                    //生成结束特效  [3/12/2015 Zmy]
                    EffectManager.GetInstance().DisableStaticEffect(m_pHolder, pImpactRow.getBuffEffect());

                    if (string.IsNullOrEmpty(pImpactRow.getBuffEffectEd()) == false)
                    {
                        Transform paran = m_pHolder.GetGameObject().GetComponent<AnimationEventControler>().GetTransform(m_pHolder.GetGameObject().transform, pImpactRow.getBuffEffectPosition());
                        EffectManager.GetInstance().InstanceEffect_Static(pImpactRow.getBuffEffectEd(), m_pHolder, paran, 0f, true);
                    }
                }

                m_bActive = false;
            }
            return true;
        }
        public bool OnRemove()
        {
            BuffTemplate pImpactRow = GetImpactRow();
            LogManager.LogAssert(pImpactRow);
            if (pImpactRow.getBuffType() >= 0)
            {
                bool bRet = __DoImpactDisappearLogic(pImpactRow.getBuffType());
                LogManager.LogAssert(bRet);

            }
            StopActive();
            return true;
        }
        private void StopActive()
        {
            m_bActive = false;
        }
        //public bool OnBeAttacked(ObjectCreature pSource, ref X_GUID endguid)
        //{

        //    LogManager.LogAssert(IsActive());
        //    BuffTemplate pImpactRow = GetImpactRow();
        //    LogManager.LogAssert(pImpactRow);
        //    switch (pImpactRow.getBuffType())
        //    {

        //    }
        //    return true;

        //}
        public bool OnAddToObjectCard(ObjectCreature pCaster)
        {
            if (pCaster != null)
            {
                m_pImpactObject.m_CasterGUID.Copy(pCaster.GetGuid());
            }
            BuffTemplate pImpactRow = GetImpactRow();
            LogManager.LogAssert(pImpactRow);
            bool bRet = DoPassvityImpactLogic(pImpactRow.getBuffType());
            LogManager.LogAssert(bRet);

            return true;
        }
        public bool OnAddToTarget(ObjectCreature pCaster)
        {
            LogManager.LogAssert(IsActive());
            if (pCaster != null)
            {
                m_pImpactObject.m_CasterGUID.Copy(pCaster.GetGuid());
            }
            BuffTemplate pImpactRow = GetImpactRow();
            LogManager.LogAssert(pImpactRow);
            bool bRet = __DoImpactLogic(pImpactRow.getBuffType());
            LogManager.LogAssert(bRet);

            return true;
        }
        //被动技能只记录以下影响卡牌属性的逻辑 [5/6/2015 Zmy]
        public ImpctLogicFunction GetPassvityImpctLogicFunction(int nImpactLogicID)
        {
            switch ((EM_IMPACT_LOGIC)nImpactLogicID)
            {
                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC101:
                    {
                        return ImpactLogic.DoLogic101;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC104:
                    {
                        return ImpactLogic.DoLogic104;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC105:
                    {
                        return ImpactLogic.DoLogic105;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC108:
                    {
                        return ImpactLogic.DoLogic108;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC119:
                    {
                        return ImpactLogic.DoLogic119;
                    }
                default:
                    break;
            }

            return null;
        }

        public ImpctLogicFunction __GetImpctLogicFunction(int nImpactLogicID)
        {

            LogManager.LogAssert(IsActive());
            switch ((EM_IMPACT_LOGIC)nImpactLogicID)
            {
                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC101:
                    {
                        return ImpactLogic.DoLogic101;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC102:
                    {
                        return ImpactLogic.DoLogic102;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC103:
                    {
                        return ImpactLogic.DoLogic103;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC104:
                    {
                        return ImpactLogic.DoLogic104;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC105:
                    {
                        return ImpactLogic.DoLogic105;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC106:
                    {
                        return ImpactLogic.DoLogic106;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC107:
                    {
                        return ImpactLogic.DoLogic107;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC108:
                    {
                        return ImpactLogic.DoLogic108;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC109:
                    {
                        return ImpactLogic.DoLogic109;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC110:
                    {
                        return ImpactLogic.DoLogic110;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC111:
                    {
                        return ImpactLogic.DoLogic111;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC112:
                    {
                        return ImpactLogic.DoLogic112;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC113:
                    {
                        return ImpactLogic.DoLogic113;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC114:
                    {
                        return ImpactLogic.DoLogic114;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC115:
                    {
                        return ImpactLogic.DoLogic115;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC116:
                    {
                        return ImpactLogic.DoLogic116;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC117:
                    {
                        return ImpactLogic.DoLogic117;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC118:
                    {
                        return ImpactLogic.DoLogic118;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC119:
                    {
                        return ImpactLogic.DoLogic119;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC1001:
                    {
                        return ImpactLogic.DoLogic1001;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC1002:
                    {
                        return ImpactLogic.DoLogic1002;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC1003:
                    {
                        return ImpactLogic.DoLogic1003;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC1004:
                    {
                        return ImpactLogic.DoLogic1004;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC1005:
                    {
                        return ImpactLogic.DoLogic1005;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC1006:
                    {
                        return ImpactLogic.DoLogic1006;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC1101:
                    {
                        return ImpactLogic.DoLogic1101;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC1102:
                    {
                        return ImpactLogic.DoLogic1102;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC1103:
                    {
                        return ImpactLogic.DoLogic1103;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC1201:
                    {
                        return ImpactLogic.DoLogic1201;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC1301:
                    {
                        return ImpactLogic.DoLogic1301;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC1401:
                    {
                        return ImpactLogic.DoLogic1401;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC1402:
                    {
                        return ImpactLogic.DoLogic1402;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2001:
                    {
                        return ImpactLogic.DoLogic2001;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2002:
                    {
                        return ImpactLogic.DoLogic2002;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2003:
                    {
                        return ImpactLogic.DoLogic2003;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2004:
                    {
                        return ImpactLogic.DoLogic2004;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2005:
                    {
                        return ImpactLogic.DoLogic2005;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2007:
                    {
                        return ImpactLogic.DoLogic2007;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2008:
                    {
                        return ImpactLogic.DoLogic2008;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2009:
                    {
                        return ImpactLogic.DoLogic2009;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2010:
                    {
                        return ImpactLogic.DoLogic2010;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2011:
                    {
                        return ImpactLogic.DoLogic2011;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2012:
                    {
                        return ImpactLogic.DoLogic2012;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2013:
                    {
                        return ImpactLogic.DoLogic2013;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2014:
                    {
                        return ImpactLogic.DoLogic2014;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2015:
                    {
                        return ImpactLogic.DoLogic2015;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2016:
                    {
                        return ImpactLogic.DoLogic2016;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2017:
                    {
                        return ImpactLogic.DoLogic2017;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2018:
                    {
                        return ImpactLogic.DoLogic2018;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2019:
                    {
                        return ImpactLogic.DoLogic2019;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2020:
                    {
                        return ImpactLogic.DoLogic2020;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2021:
                    {
                        return ImpactLogic.DoLogic2021;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2022:
                    {
                        return ImpactLogic.DoLogic2022;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2023:
                    {
                        return ImpactLogic.DoLogic2023;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2024:
                    {
                        return ImpactLogic.DoLogic2024;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2025:
                    {
                        return ImpactLogic.DoLogic2025;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2026:
                    {
                        return ImpactLogic.DoLogic2026;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2028:
                    {
                        return ImpactLogic.DoLogic2028;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2029:
                    {
                        return ImpactLogic.DoLogic2029;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2030:
                    {
                        return ImpactLogic.DoLogic2030;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2031:
                    {
                        return ImpactLogic.DoLogic2031;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2032:
                    {
                        return ImpactLogic.DoLogic2032;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2033:
                    {
                        return ImpactLogic.DoLogic2033;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2034:
                    {
                        return ImpactLogic.DoLogic2034;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2035:
                    {
                        return ImpactLogic.DoLogic2035;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2036:
                    {
                        return ImpactLogic.DoLogic2036;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2039:
                    {
                        return ImpactLogic.DoLogic2039;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2040:
                    {
                        return ImpactLogic.DoLogic2040;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2041:
                    {
                        return ImpactLogic.DoLogic2041;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2042:
                    {
                        return ImpactLogic.DoLogic2042;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2043:
                    {
                        return ImpactLogic.DoLogic2043;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2044:
                    {
                        return ImpactLogic.DoLogic2044;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2045:
                    {
                        return ImpactLogic.DoLogic2045;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2046:
                    {
                        return ImpactLogic.DoLogic2046;
                    }

                case EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2047:
                    {
                        return ImpactLogic.DoLogic2047;
                    }
                default:
                    break;
            }
            return null;
        }

        public bool DoPassvityImpactLogic(int nImpactLogicID)
        {
            ImpctLogicFunction myfunction = GetPassvityImpctLogicFunction(nImpactLogicID);
            if (myfunction != null)
            {
                return myfunction(m_pHolder, this, (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD);
            }

            return false;
        }
        public bool __DoImpactLogic(int nImpactLogicID)
        {
            LogManager.LogAssert(IsActive());

            ImpctLogicFunction myfunction = __GetImpctLogicFunction(nImpactLogicID);
            if (myfunction != null)
            {
                return myfunction(m_pHolder, this, (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD);
            }

            return false;
        }

        public bool __DoImpactDisappearLogic(int nImpactLogicID)
        {
            ImpctLogicFunction myfunction = __GetImpctLogicFunction(nImpactLogicID);
            if (myfunction != null)
            {
                return myfunction(m_pHolder, this, (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE);
            }

            return false;

        }

        public bool __DoImpactIntervalLogic(int nImpactLogicID)
        {

            ImpctLogicFunction myfunction = __GetImpctLogicFunction(nImpactLogicID);
            if (myfunction != null)
            {
                return myfunction(m_pHolder, this, (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_DOT);
            }

            return false;
        }

        //逻辑
        public bool HeartBeat(int uTime)
        {
            //bool bRet = false;
            //if ( m_pImpactObject.m_ImpactTime == -1 )
            //{
            //    return true;
            //}
            if (m_pImpactObject.m_IntervalTime > 0)
            {
                m_pImpactObject.m_ElapsedIntervalTime += uTime;
                if (m_pImpactObject.m_ElapsedIntervalTime >= m_pImpactObject.m_IntervalTime)
                {
                    BuffTemplate pImpactRow = GetImpactRow();
                    LogManager.LogAssert(pImpactRow);
                    int nCount = m_pImpactObject.m_ElapsedIntervalTime / m_pImpactObject.m_IntervalTime;

                    for (int i = 0; i < nCount; ++i)
                    {
                        bool bRet = __DoImpactIntervalLogic(pImpactRow.getBuffType());
                        LogManager.LogAssert(bRet);
                    }
                    m_pImpactObject.m_ElapsedIntervalTime = m_pImpactObject.m_ElapsedIntervalTime - nCount * m_pImpactObject.m_IntervalTime;
                }
            }
            m_pImpactObject.m_ElapsedTime += uTime;

            m_bNew = false;

            return true;

        }

        public void AddAttributeEffectRefix(EM_EXTEND_ATTRIBUTE nAttrType, int nValue, bool bCheckAlive = false)
        {
            //if ( bCheckAlive )
            //{
            for (int i = 0; i < m_AttributeEffectRefixCount; i++)
            {
                if ((int)m_AttributeEffectRefix[i].m_AttrType == (int)nAttrType)
                {
                    m_AttributeEffectRefix[i].m_Value += nValue;
                    return;
                }
            }
            //}

            if (m_AttributeEffectRefixCount < GlobalMembers.MAX_IMPACT_ATTRIBUTE_COUNT)
            {
                m_AttributeEffectRefix[m_AttributeEffectRefixCount].m_AttrType = (byte)nAttrType;
                m_AttributeEffectRefix[m_AttributeEffectRefixCount].m_Value = nValue;
                ++m_AttributeEffectRefixCount;
            }
        }

        public int GetParam(int nIndex)
        {

            if ((nIndex < 0) || (nIndex > GlobalMembers.MAX_IMPACT_LOGIC_PARAM_COUNT))
            {
                LogManager.LogAssert(0);
            }
            if (m_pImpactRow == null)
                return 0;
            int nParam = m_pImpactRow.getParam()[nIndex];
            nParam = nParam + (nParam * m_RefixParam[nIndex]) / 1000;
            return nParam;

        }
        public int GetParam()
        {
            return m_Param;
        }//额外参数
        public void SetParam(int nParam)
        {
            m_Param = nParam;
        }
        public int GetHurt()
        {
            return m_Hurt;
        }//伤害
        public void SetHurt(int nParam)
        {
            m_Hurt = nParam;
        }
        public int GetHeal()
        {
            return m_Heal;
        }//治疗
        public void SetHeal(int nParam)
        {
            m_Heal = nParam;
        }
        public void SetParamRefix(int nIndex, int nValue)
        {

            if ((nIndex < 0) || (nIndex > GlobalMembers.MAX_IMPACT_LOGIC_PARAM_COUNT))
            {
                LogManager.LogAssert(0);
            }
            //m_RefixParam[nIndex] = (int)(m_RefixParam[nIndex] + m_RefixParam[nIndex] * nValue / 1000f);
            m_RefixParam[nIndex] += nValue;


        }

        ////////////////////////////////////////////////////////////////////////////////////

        //攻击时生效
        public int GeneratorHurtPermilWhenAttack()
        {

            if (!m_pHolder.IsAlive())
            {
                return 0;
            }
            if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2019)
            {
                //效果存在时，下(param_1)次造成的物理/法术伤害增减(param_2)‰。次数为0时删除该buff
                if (GetParam(1) != 0)
                {
                    return GetParam(1);
                }
                ++m_ImpactActiveCount;
                if (m_ImpactActiveCount >= GetParam(0))
                {
                    m_ImpactActiveCount = 0;
                    OnDisappear();
                }
            }
            return 0;

        }

        //计算伤害时就生效
        public int GeneratorHurtPermilEffectBeAttack(int nDamageType, ref int nHurtPoint)
        {
            LogManager.LogAssert(IsActive());
            if (!m_pHolder.IsAlive())
            {
                return 0;
            }
            if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC109)
            {
                //受到(param_2)类型的伤害时增减(param_1)点伤害，生效概率(param_3)‰,概率为-1时表示不进行概率判断。正数代表增加，负数代表减少
                for (int i = 0; i < 9; i = i + 3)
                {
                    if (((m_pImpactRow.getParam()[i + 1] == (int)ENUM_HURT_TYPE.HURT_TYPE_ALL) || (m_pImpactRow.getParam()[i + 1] == nDamageType)))
                    {
                        int nParam = GetParam(2);
                        if ((nParam < 1000) && (nParam >= 0))
                        {
                            System.Random pRand = new System.Random();
                            int nRand = pRand.Next(1, 1000);

                            if (nRand <= GetParam(2))
                            {
                                nHurtPoint = nHurtPoint + GetParam(i);
                            }
                        }
                        else
                        {
                            nHurtPoint = nHurtPoint + GetParam(i);
                        }
                    }
                }
                return 0;
            }
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC110)
            {
                //增减(param_1)‰点(param_2)--伤害类型，生效概率(param_3)‰,概率为-1时表示不进行概率判断
                for (int i = 0; i < 10; i = i + 3)
                {
                    if (((m_pImpactRow.getParam()[i + 1] == (int)ENUM_HURT_TYPE.HURT_TYPE_ALL) || (m_pImpactRow.getParam()[i + 1] == nDamageType)))
                    {
                        int nParam = GetParam(2);
                        if ((nParam <= 1000) && (nParam >= 0) || nParam == -1)
                        {
                            int nRand =  UnityEngine.Random.Range(1,1000);
                            if (nRand < GetParam(2))
                            {
                                nHurtPoint = nHurtPoint + (int)(nHurtPoint * GetParam(i) * 0.001f);
                            }
                        }
                    }
                }
            }
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC1201)
            {
                nHurtPoint = (int)(nHurtPoint + nHurtPoint * GetParam(1) * 0.001f);
            }
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2041)
            {
                System.Random ran = new System.Random();
                int ranNum = ran.Next(1, 1001);
                if (ranNum <= m_pImpactRow.getParam()[0])
                {
                    if (m_pImpactRow.getParam()[1] == 1)
                    {
                        nHurtPoint = nHurtPoint + m_pImpactRow.getParam()[2];
                    }
                    else if (m_pImpactRow.getParam()[1] == 2)
                    {
                        nHurtPoint = nHurtPoint + nHurtPoint * m_pImpactRow.getParam()[2] / 1000;
                    }
                }
            }
            return 0;

        }

        /// <summary>
        /// 计算伤害时生效
        /// </summary>
        /// <param name="nDamageType">伤害类型</param>
        /// <param name="pTarget">目标OBJ</param>
        /// <param name="nHurtPoint">伤害点</param>
        /// <returns></returns>
        public int ChangeTargetHurtPermilEffect(int nDamageType, ObjectCreature pTarget, ref int nHurtPoint)
        {
            if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2036)
            {
                //效果持续时技能造成的伤害根据目标所拥有两个BUFF组中BUFF数关系改变（不包括技能附带BUFF的伤害）
                //BuffgroupTemplate pRow = (BuffgroupTemplate)DataTemplate.GetInstance().m_BuffGroupTable.getTableData(m_pImpactRow.getParam()[0]);
                if (m_pImpactRow.getParam()[0] == 1)
                {
                    int nCount1 = pTarget.GetImpactCountByGroupID(m_pImpactRow.getParam()[1]);
                    int nCount2 = pTarget.GetImpactCountByGroupID(m_pImpactRow.getParam()[2]);
                    if (nCount1 >= nCount2)
                    {
                        nHurtPoint = (nHurtPoint * GetParam(1)) / 1000;
                    }
                    else
                    {
                        nHurtPoint = (nHurtPoint * GetParam(2)) / 1000;
                    }
                }
            }
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2004)
            {
                //效果持续时，若对拥有(param_1)效果组的目标造成伤害，则伤害扩大(param_2)‰并附加(param_3)的固定伤害
                if (m_pImpactRow.getParam()[0] > 0)
                {
                    BuffgroupTemplate pRow = (BuffgroupTemplate)DataTemplate.GetInstance().m_BuffGroupTable.getTableData(m_pImpactRow.getParam()[0]);

                    if (pRow != null)
                    {
                        //int nCount = 0;
                        for (int i = 0; i < pRow.getParam().Length; i++)
                        {
                            for (int j = 0; j < pTarget.GetImpactList().Count; j++)
                            {
                                if (pRow.getParam()[i] == pTarget.GetImpactList()[j].m_pImpactRow.getId())
                                {
                                    nHurtPoint = nHurtPoint + (nHurtPoint * GetParam(1)) / 1000 + GetParam(2);
                                }
                            }
                            // nCount = nCount + m_pHolder.GetImpactCountByID(pRow.getParam()[i]);
                        }
                    }
                }
            }
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2023)
            {
                //效果持续时使用技能造成伤害根据敌方当前怒气量改变（不包括技能附带BUFF的伤害）
                if (m_pImpactRow.getParam()[0] == 1)
                {
                    if (FightControler.Inst != null)
                    {
                        int nTargetTeamMP = FightControler.Inst.GetPowerValue(EM_OBJECT_TYPE.EM_OBJECT_TYPE_MONSTER);
                        nHurtPoint = (int)(nHurtPoint *
                            ((m_pImpactRow.getParam()[1] / 1000.0f) * Math.Pow(nTargetTeamMP, m_pImpactRow.getParam()[2] / 1000) + (m_pImpactRow.getParam()[3] / 1000.0f)
                            * Math.Pow(nTargetTeamMP, m_pImpactRow.getParam()[4] / 1000) + m_pImpactRow.getParam()[5] / 1000.0f));
                    }
                }
            }
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2024)
            {
                X_GUID guid = pTarget.GetGuid();
                SceneObjectManager pScene = SceneObjectManager.GetInstance();
                LogManager.LogAssert(pScene);
                ObjectCreature pCreature = pScene.GetObjectHeroByGUID(guid);
                if (m_pImpactRow.getParam()[0] == 1)
                {
                    if (m_pHolder.GetHPPercent() >= pCreature.GetHPPercent())
                    {
                        if (m_pImpactRow.getParam()[1] != 0)
                        {
                            nHurtPoint = (int)(m_pImpactRow.getParam()[1] / 1000.0f) * nHurtPoint;
                        }
                    }
                    else
                    {
                        if (m_pImpactRow.getParam()[2] != 0)
                        {
                            nHurtPoint = (int)(m_pImpactRow.getParam()[2] / 1000.0f) * nHurtPoint;
                        }
                    }
                }
                else if (m_pImpactRow.getParam()[0] == 2)
                {
                    int nPercent = (int)pCreature.GetHPPercent();
                    nHurtPoint = (int)(nHurtPoint * ((m_pImpactRow.getParam()[1] / 1000.0f) * Math.Pow(nPercent / 100, m_pImpactRow.getParam()[2] / 1000.0f) +
                        (m_pImpactRow.getParam()[3] / 1000.0f) * Math.Pow(nPercent, m_pImpactRow.getParam()[4] / 1000.0f) + m_pImpactRow.getParam()[5] / 1000.0f));
                }
                else if (m_pImpactRow.getParam()[0] == 3)
                {
                    int nPercent = (int)m_pHolder.GetHPPercent();
                    nHurtPoint = (int)(nHurtPoint * ((m_pImpactRow.getParam()[1] / 1000.0f) * Math.Pow(nPercent / 100, m_pImpactRow.getParam()[2] / 1000.0f) +
                        (m_pImpactRow.getParam()[3] / 1000.0f) * Math.Pow(nPercent, m_pImpactRow.getParam()[4] / 1000.0f) + m_pImpactRow.getParam()[5] / 1000.0f));
                }
            }


            return 0;
        }

        public void HurtDistribute(ref int nHurtPoint)
        {
            LogManager.LogAssert(IsActive());

            if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC1401)
            {
                //效果持续时，将所受伤害减少(param_1)‰，并将减少的伤害分摊到(param_2)中拥有该类效果的相同buffID的角色身上。最终伤害为减完的伤害/数量。受到治疗时，
                //(param_2)中拥有该类效果的相同buffID的角色都会恢复生命。由该效果分摊出去的伤害为直接伤害。
                int nTempValue = (nHurtPoint * GetParam(0)) / 1000;
                nHurtPoint = nHurtPoint - nTempValue;
                SceneObjectManager pScene = SceneObjectManager.GetInstance();
                LogManager.LogAssert(pScene);
                SCANOPERATOR_INIT init = new SCANOPERATOR_INIT();
                init.m_Type = m_pImpactRow.getParam()[1];
                init.m_ChildType = (int)EM_SPELL_TARGET_SENIOR_TYPE.EM_SEPLL_TARGET_REQUIRE_IMPACTID;
                init.m_Param = GetImpactID();
                pScene.ScanByObject(m_pHolder, ref init);
                nTempValue = nTempValue / init.m_ObjectList.Count;
                if (nTempValue <= 0)
                {
                    nTempValue = 0;
                }
                for (int j = 0; j < init.m_ObjectList.Count; j++)
                {
                    init.m_ObjectList[j].OnDamage(nTempValue);
                }
            }
        }
        //伤害或治疗延时
        public void OnDelayHurtorHeal(bool bHurt, ref int nValue, ObjectCreature pSource)
        {
            LogManager.LogAssert(IsActive());
            if (!m_pHolder.IsAlive())
            {
                return;
            }
            if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2039)
            {
                if (bHurt)
                {
                    m_Hurt = m_Hurt + nValue;
                }
                else
                {
                    m_Heal = m_Heal + nValue;
                }
            }
        }

        /// <summary>
        /// 受到伤害
        /// </summary>
        /// <param name="nDamageType">伤害类型</param>
        /// <param name="nInDamage">伤害数值</param>
        /// <param name="pSource">伤害来源</param>
        public void OnAbsorbHurt(int nDamageType, ref int nInDamage, ObjectCreature pSource)
        {
            LogManager.LogAssert(IsActive());
            if (!m_pHolder.IsAlive())
            {
                return;
            }
            if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC111)
            {
                /*效果持续时吸收(param_1)‰点(param_2)--伤害类型，生命盾拥有一定生命，
                生命盾被击破则激活技能(param_3)，生命盾未被击破则将剩余生命盾值*(param_7)/1000增加至当前生命值上。(param_3)为-1时表示不调用技能*/
                if ((m_pImpactRow.getParam()[1] == (int)ENUM_HURT_TYPE.HURT_TYPE_ALL) || (m_pImpactRow.getParam()[1] == nDamageType))
                {
                    int nPermil = m_pImpactRow.getParam()[0];
                    if ((nPermil > 0) && (nPermil <= 1000))
                    {
                        int reduceDamage = (nInDamage * nPermil) / 1000;
                        if (reduceDamage >= m_ImpactHP)
                        {
                            nInDamage = nInDamage - m_ImpactHP;
                            m_ImpactHP = 0;
                            //技能
                            if (m_pImpactRow.getParam()[3] > 0)
                            {
                                if (GetParam(2) > 0)
                                {
                                    OnActiveChildSpell(GetParam(2), pSource);
                                }
                            }

                            OnDisappear();
                        }
                        else
                        {
                            m_ImpactHP = m_ImpactHP - reduceDamage;
                            nInDamage = nInDamage - reduceDamage;
                        }
                    }
                }
            }
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC113)
            {
                //吸收(param_1)‰点(param_2)--伤害类型，生命盾生效(param_3)次，生命盾被击破则激活技能(param_4)。(param_3)和(parm_4)为-1时表示无效
                if ((m_pImpactRow.getParam()[1] == (int)ENUM_HURT_TYPE.HURT_TYPE_ALL) || (m_pImpactRow.getParam()[1] == nDamageType))
                {
                    int nPermil = GetParam(0);
                    if ((nPermil > 0) && (nPermil <= 1000))
                    {
                        int reduceDamage = (nInDamage * nPermil) / 1000;
                        nInDamage = nInDamage - reduceDamage;
                        ++m_ImpactActiveCount;
                        if (m_ImpactActiveCount >= m_pImpactRow.getParam()[2] || m_pImpactRow.getParam()[2] == -1)
                        {
                            m_ImpactActiveCount = 0;
                            //技能
                            if (GetParam(3) > 0)
                            {
                                OnActiveChildSpell(GetParam(3), pSource);
                            }
                            OnDisappear();
                        }
                    }
                }
            }

        }

        public void OnHurtBack(int nDamageType, int nInDamage, ObjectCreature pSource)
        {
            LogManager.LogAssert(IsActive());
            if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC1201)
            {
                //受击时反弹(param_1)‰，并增减(param_2)‰所受伤害，并对反弹目标造成(param_3)点直接伤害，持续(param_4)次。
                //持续时间到或者达到param_4次数后该buff/debuff移除，param_2为-1时不判断次数。正数代表增加，负数代表减少。通过该效果掉血造成的是直接伤害，并且该效果不能反弹直接伤害。
                if (((int)ENUM_HURT_TYPE.HURT_TYPE_ALL == nDamageType)
                    || ((int)ENUM_HURT_TYPE.HURT_TYPE_PHY == nDamageType)
                    || ((int)ENUM_HURT_TYPE.HURT_TYPE_MAGIC == nDamageType))
                {
                    //反弹伤害 = 最终伤害 * (1 + param2‰) * (1 + param1‰)
                    int ranNum = UnityEngine.Random.Range(1, 1001);
                    if (ranNum <= m_pImpactRow.getParam()[4])
                    {
                        nInDamage = (int)(nInDamage * (GetParam(0) * 0.001f));
                    }
                    nInDamage = nInDamage + GetParam(2);
                    pSource.OnDamage(nInDamage);
                    ++m_ImpactActiveCount;
                    if (m_ImpactActiveCount >= GetParam(3) && GetParam(3) != -1)
                    {
                        m_ImpactActiveCount = 0;
                        OnDisappear();
                    }
                }
            }
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2028)
            {
                System.Random ran = new System.Random();
                int ranNum = ran.Next(1, 1000);
                if (ranNum < m_pImpactRow.getParam()[3] || m_pImpactRow.getParam()[3] < 0)
                {
                    //效果持续时受到特定伤害则几率对指定单位反馈BUFF
                    if (((int)ENUM_HURT_TYPE.HURT_TYPE_ALL == nDamageType) || 
                        ((int)ENUM_HURT_TYPE.HURT_TYPE_PHY == nDamageType) || 
                        ((int)ENUM_HURT_TYPE.HURT_TYPE_MAGIC == nDamageType))
                    {
                        SceneObjectManager pScene = SceneObjectManager.GetInstance();
                        LogManager.LogAssert(pScene);
                        SCANOPERATOR_INIT init = new SCANOPERATOR_INIT();
                        if (m_pImpactRow.getParam()[1] == 1)
                        {
                            init.m_Type = (int)EM_TARGET_TYPE.EM_TARGET_ALL_NO_SELF;
                            pScene.ScanByObject(m_pHolder, ref init);
                            if (init.m_ObjectList.Count > 0)
                            {
                                int randomNum = UnityEngine.Random.Range(0, init.m_ObjectList.Count);
                                init.m_ObjectList[randomNum].AddImpact(m_pImpactRow.getParam()[2], m_pHolder, false, m_SpellID);
                            }
                        }
                        else if (m_pImpactRow.getParam()[1] == 2)
                        {
                            init.m_Type = (int)EM_TARGET_TYPE.EM_TARGET_ENEMY_RAND;
                            pScene.ScanByObject(m_pHolder, ref init);
                            if (init.m_ObjectList.Count > 0)
                            {
                                init.m_ObjectList[0].AddImpact(m_pImpactRow.getParam()[2], m_pHolder, false, m_SpellID);
                            }
                        }
                        else if (m_pImpactRow.getParam()[1] == 3)
                        {
                            init.m_Type = (int)EM_TARGET_TYPE.EM_TARGET_FRIEND_RAND;
                            pScene.ScanByObject(m_pHolder, ref init, 1);
                            if (init.m_ObjectList.Count > 0)
                            {
                                init.m_ObjectList[0].AddImpact(m_pImpactRow.getParam()[2], m_pHolder, false, m_SpellID);
                            }
                        }
                        else if (m_pImpactRow.getParam()[1] == 4)
                        {
                            pSource.AddImpact(m_pImpactRow.getParam()[2], m_pHolder, false, m_SpellID);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 命中目标
        /// </summary>
        /// <param name="nSpellID">技能ID</param>
        /// <param name="pTarget">目标</param>
        public void OnHitTarget(SpellInfo nSpellInfo, ObjectCreature pTarget)
        {
            LogManager.LogAssert(IsActive());
            LogManager.LogAssert(nSpellInfo);
            int nSpellID = nSpellInfo.GetSpellID();
            if ((m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2013))
            {
                //效果持续时，若效果拥有者施放编号为(param_2)、(param_3)、(param_4)、(param_5)中的父技能，则有(param_6)‰概率激活子技能(param_1)。
                //(param_2)、(param_3)、(param_4)、(param_5)为(param_1)的父技能且为-1时表示无效
                if ((nSpellInfo.GetSpellNum() == m_pImpactRow.getParam()[1])
                    || (nSpellInfo.GetSpellNum() == m_pImpactRow.getParam()[2])
                    || (nSpellInfo.GetSpellNum() == m_pImpactRow.getParam()[3])
                    || (nSpellInfo.GetSpellNum() == m_pImpactRow.getParam()[4]))
                {
                    int nRand = UnityEngine.Random.Range(1, 300);
                    if (nRand < GetParam(5))
                    {
                        if (GetParam(0) > 0)
                        {
                            OnActiveChildSpell(GetParam(0), null, pTarget);
                        }
                    }
                }
            }
            //效果持续时技能每命中一个目标会使此技能CD改变（最小变至0）
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2021)
            {
                if (nSpellID != -1 && nSpellID == GetSpellID())
                {
                    SpellInfo pSpellInfo = m_pHolder.GetSpellInfo(nSpellID);
                    if (pSpellInfo != null)
                    {
                        CoolDownList pCoolDown = m_pHolder.GetCoolDownList();
                        LogManager.LogAssert(pCoolDown);
                        CoolDownObject pCoolDownObject = pCoolDown.GetCoolDownObject((uint)nSpellID);
                        if (pCoolDownObject != null)
                        {
                            SkillTemplate pSpellRow = pSpellInfo.GetSpellRow();
                            if (m_pImpactRow.getParam()[0] == 1)
                            {
                                if (m_pImpactRow.getParam()[1] < 0)
                                {
                                    if ((-m_pImpactRow.getParam()[1]) > pCoolDownObject.m_nCoolDownTime)
                                    {
                                        pCoolDownObject.m_nCoolDownTime = 0;
                                    }
                                    else
                                    {
                                        pCoolDownObject.m_nCoolDownTime = pCoolDownObject.m_nCoolDownTime + (uint)m_pImpactRow.getParam()[1];
                                    }
                                }
                                else
                                {
                                    pCoolDownObject.m_nCoolDownTime = pCoolDownObject.m_nCoolDownTime + (uint)m_pImpactRow.getParam()[1];
                                }
                            }
                            else if (m_pImpactRow.getParam()[0] == 2)
                            {
                                int nTime = (pSpellRow.getCooldown() * m_pImpactRow.getParam()[1]) / 1000;
                                if (nTime < 0)
                                {
                                    if ((-nTime) > pCoolDownObject.m_nCoolDownTime)
                                    {
                                        pCoolDownObject.m_nCoolDownTime = 0;
                                    }
                                    else
                                    {
                                        pCoolDownObject.m_nCoolDownTime = pCoolDownObject.m_nCoolDownTime + (uint)nTime;
                                    }
                                }
                                else
                                {
                                    pCoolDownObject.m_nCoolDownTime = pCoolDownObject.m_nCoolDownTime + (uint)nTime;
                                }
                            }
                            else if (m_pImpactRow.getParam()[0] == 3)
                            {
                                int nTime = ((int)(pCoolDownObject.m_nCoolDownTime - pCoolDownObject.m_nCoolDownElapsed) * m_pImpactRow.getParam()[1]) / 1000;
                                if (nTime < 0)
                                {
                                    if ((-nTime) > pCoolDownObject.m_nCoolDownTime)
                                    {
                                        pCoolDownObject.m_nCoolDownTime = 0;
                                    }
                                    else
                                    {
                                        pCoolDownObject.m_nCoolDownTime = pCoolDownObject.m_nCoolDownTime + (uint)nTime;
                                    }
                                }
                                else
                                {
                                    pCoolDownObject.m_nCoolDownTime = pCoolDownObject.m_nCoolDownTime + (uint)nTime;
                                }
                            }
                        }
                    }
                }
            }
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2044)
            {
                //效果持续时使用普通攻击命中则几率对指定目标添加指定buff
                //SceneObjectManager pScene = SceneObjectManager.GetInstance();
                //ObjectCreature pTarget = pScene.GetSceneObjectByGUID(m_SpellTarget);
                if (nSpellInfo.GetSpellNum() == 0)
                {
                    int ranNum = UnityEngine.Random.Range(1, 200);
                    if (ranNum <= m_pImpactRow.getParam()[1])
                    {
                        for (int i = 2; i < m_pImpactRow.getParam().Length - 1; i++)
                        {
                            if (m_pImpactRow.getParam()[i] > 0)
                            {
                                if (m_pImpactRow.getParam()[0] == 1)
                                {
                                    m_pHolder.AddImpact(m_pImpactRow.getParam()[i], m_pHolder, false, m_SpellID);
                                }
                                else if (m_pImpactRow.getParam()[0] == 2)
                                {
                                    pTarget.AddImpact(m_pImpactRow.getParam()[i], m_pHolder, false, m_SpellID);
                                }
                            }
                        }
                    }
                }
            }
        }
        public void OnNoHitTarget(SpellInfo nSpellInfo,ObjectCreature pDodge)
        {
            LogManager.LogAssert(IsActive());
            //效果持续时若拥有者攻击未命中（技能或普攻）则改变自己当前生命
            if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2026)
            {
                //1：直接值X
                //2：生命上限*X / 1000
                //3：攻击力*X / 1000"	"改变生命计算因子

                if (m_pImpactRow.getParam()[0] == 1)
                {
                    if (m_pImpactRow.getParam()[1] < 0)
                    {
                        m_pHolder.OnDamage(m_pImpactRow.getParam()[1]);
                    }
                    else if (m_pImpactRow.getParam()[1] > 0)
                    {
                        if (m_pImpactRow.getBuffTriggerType() == 1)
                        {
                            m_pHolder.OnHeal(m_pImpactRow.getParam()[1]);
                        }
                    }
                }
                else if (m_pImpactRow.getParam()[0] == 2)
                {
                    long nValue = (m_pHolder.GetMaxHP() * m_pImpactRow.getParam()[1]) / 1000;
                    if (nValue != 0)
                    {
                        if (m_pImpactRow.getParam()[1] < 0)
                        {
                            m_pHolder.OnDamage(m_pImpactRow.getParam()[1]);
                        }
                        else if (m_pImpactRow.getParam()[1] > 0)
                        {
                            if (m_pImpactRow.getBuffTriggerType() == 1)
                            {
                                m_pHolder.OnHeal(m_pImpactRow.getParam()[1]);
                            }
                        }
                    }
                }
                else if (m_pImpactRow.getParam()[0] == 3)
                {
                    int nValue = (m_pHolder.GetPhysicalAttack() * m_pImpactRow.getParam()[1]) / 1000;
                    if (nValue != 0)
                    {
                        if (m_pImpactRow.getParam()[1] < 0)
                        {
                            m_pHolder.OnDamage(m_pImpactRow.getParam()[1]);
                        }
                        else if (m_pImpactRow.getParam()[1] > 0)
                        {
                            if (m_pImpactRow.getBuffTriggerType() == 1)
                            {
                                m_pHolder.OnHeal(m_pImpactRow.getParam()[1]);
                            }
                        }
                    }
                }
                else if (m_pImpactRow.getParam()[0] == 4)
                {
                    int nValue = (m_pHolder.GetMagicAttack() * m_pImpactRow.getParam()[1]) / 1000;
                    if (nValue != 0)
                    {
                        if (m_pImpactRow.getParam()[1] < 0)
                        {
                            m_pHolder.OnDamage(m_pImpactRow.getParam()[1]);
                        }
                        else if (m_pImpactRow.getParam()[1] > 0)
                        {
                            if (m_pImpactRow.getBuffTriggerType() == 1)
                            {
                                m_pHolder.OnHeal(m_pImpactRow.getParam()[1]);
                            }
                        }
                    }
                }
            }
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2045)
            { 
                //效果持续时，每触发一次被闪避则激活技能(param_1)，最多生效(param_2)次。 持续时间到或者达到param_2次后该buff/debuff移除，param_2为-1时不判断次数
                ++m_ImpactActiveCount;
                if (m_ImpactActiveCount < GetParam(1) || GetParam(1) == -1)
                {
                    if (GetParam(0) > 0)
                    {
                        OnActiveChildSpell(GetParam(0), null, pDodge);
                    }
                }
            }
        }

        /// <summary>
        /// 是否可以添加
        /// </summary>
        /// <param name="nImpactID">BUFF ID</param>
        /// <returns>免疫 or 正常添加</returns>
        public bool OnReadyAddImpact(int nImpactID)
        {
            LogManager.LogAssert(IsActive());
            //效果持续时有(param_2)‰几率免疫(param_1)效果组内的效果。(param_2)为-1时表示无效
            if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC1006)
            {
                BuffgroupTemplate pRow = (BuffgroupTemplate)DataTemplate.GetInstance().m_BuffGroupTable.getTableData(m_pImpactRow.getParam()[0]);
                if (pRow != null)
                {
                    for (int i = 0; i < pRow.getParam().Length; i++)
                    {
                        if (pRow.getParam()[i] == nImpactID)
                        {
                            System.Random pPand = new System.Random();
                            int nRand = pPand.Next(1, 1000);

                            if (nRand < GetParam(1))
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return true;

        }


        /// <summary>
        /// 添加Buff
        /// </summary>
        /// <param name="pSource"></param>
        /// <param name="nImpactID"></param>
        /// <param name="isRemove"></param>
        /// <returns></returns>
        public bool OnWhenAddImpact(ObjectCreature pSource, int nImpactID,bool isRemove = false)
        {
            LogManager.LogAssert(IsActive());
            if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC106)
            {
                BuffTemplate buff = (BuffTemplate)DataTemplate.GetInstance().m_BufferTable.getTableData(nImpactID);
                if (isRemove == false)
                {
                    for (int i = 0; i < 11; i = i + 3)
                    {
                        if (m_pImpactRow.getParam()[i + 3] > 0)
                        {
                            if ((buff.getConduce() == m_pImpactRow.getParam()[i + 3]) 
                                ||(m_pImpactRow.getParam()[i + 3] == 3 && (buff.getConduce() == 1 || buff.getConduce() == 0)) 
                                ||(m_pImpactRow.getParam()[i + 3] ==4))
                            {
                                SceneObjectManager pScene = SceneObjectManager.GetInstance();
                                LogManager.LogAssert(pScene);
                                EM_EXTEND_ATTRIBUTE nExtendAttribute = Impact.GetExtendAttribute(m_pImpactRow.getParam()[i + 1]);

                                //int bufferCount = 0;
                                //SCANOPERATOR_INIT init = new SCANOPERATOR_INIT();
                                //init.m_Type = m_pImpactRow.getParam()[i + 2];
                                //init.m_ChildType = (int)EM_SPELL_TARGET_SENIOR_TYPE.EM_SEPLL_TARGET_REQUIRE_IMPACTEFFECTTYPE;
                                //pScene.ScanByObject(m_pHolder, ref init);
                                //for (int j = 0; j < init.m_ObjectList.Count; j++)
                                //{
                                //    //GetImpactCountByType 根据Buff类型来记数 
                                //    bufferCount = bufferCount + m_pHolder.GetImpactCountByType(m_pImpactRow.getParam()[i + 3]);
                                //}
                                //int nChangeValue = (int)(bufferCount * GetParam(i));
                                int nChangeValue = GetParam(i);

                                m_pHolder.ChangeEffect(nExtendAttribute, nChangeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT, false);
                                AddAttributeEffectRefix(nExtendAttribute, nChangeValue);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < m_AttributeEffectRefixCount; i++)
                    {
                        if (m_AttributeEffectRefix[i].m_Value != 0)
                        {
                            for (int j = 0; j < 11; j = j + 3)
                            {
                                if (m_AttributeEffectRefix[i].m_AttrType == m_pImpactRow.getParam()[j + 1])
                                {
                                    m_pHolder.ChangeEffect((EM_EXTEND_ATTRIBUTE)m_AttributeEffectRefix[i].m_AttrType,
                                        GetParam(j),
                                        EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT,
                                        true);
                                    m_AttributeEffectRefix[i].m_Value -= GetParam(j);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            //每受一个debuff则用自身随机一个buff抵消，自身无buff可用来抵消则激活技能(param_1)
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2017)
            {
                BuffTemplate pImpactRow = (BuffTemplate)DataTemplate.GetInstance().m_BufferTable.getTableData(nImpactID);

                LogManager.LogAssert(pImpactRow);
                //debuff
                if (pImpactRow.getConduce() == 0)
                {
                    List<Impact> pImpactList = m_pHolder.GetImpactList();
                    foreach (var item in pImpactList)
                    {
                        Impact pImpact = item;
                        if (pImpact.GetType_() == 1)
                        {
                            pImpact.OnDisappear();
                            pImpactList.Remove(item);
                            return false;
                        }
                    }
                    if (GetParam(0) > 0)
                    {
                        OnActiveChildSpell(GetParam(0), pSource);
                    }
                }
            }
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2033)
            {
                BuffTemplate pImpactRow = (BuffTemplate)DataTemplate.GetInstance().m_BufferTable.getTableData(nImpactID);
                LogManager.LogAssert(pImpactRow);
                BuffgroupTemplate pRow = (BuffgroupTemplate)DataTemplate.GetInstance().m_BuffGroupTable.getTableData(m_pImpactRow.getParam()[0]);
                if (pRow != null)
                {
                    for (int i = 0; i < pRow.getParam().Length; i++)
                    {
                        if (pRow.getParam()[i] == nImpactID)
                        {
                            int nRand = UnityEngine.Random.Range(1, 10);
                            if (nRand < GetParam(1))
                            {
                                if (GetParam(2) == 1)
                                {
                                    if (pSource != null)
                                    {
                                        if (GetParam(3) > 0)
                                        {
                                            pSource.AddImpact(pImpactRow.getParam()[3], m_pHolder, false, m_SpellID);
                                        }
                                    }
                                }
                                else if (GetParam(2) == 2)
                                {
                                    if (m_pHolder != null)
                                    {
                                        if (GetParam(3) > 0)
                                        {
                                            m_pHolder.AddImpact(pImpactRow.getParam()[3], m_pHolder, false, m_SpellID);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //效果持续时无法获得指定BUFF组内BUFF
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2038)
            {
                BuffTemplate pImpactRow = (BuffTemplate)DataTemplate.GetInstance().m_BufferTable.getTableData(nImpactID);
                LogManager.LogAssert(pImpactRow);
                BuffgroupTemplate pRow = (BuffgroupTemplate)DataTemplate.GetInstance().m_BufferTable.getTableData(m_pImpactRow.getParam()[0]);
                if (pRow != null)
                {
                    for (int i = 0; i < pRow.getParam().Length; i++)
                    {
                        if (pRow.getParam()[i] == nImpactID)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public void OnAddImpact(Impact pImpact)
        {
            LogManager.LogAssert(IsActive());
            //效果存在时，有(param_2)‰几率将新加入的buff/debuff某个参数扩大(param_1)‰倍。(param_3)效果组内的是修改持续时间参数，
            //(param_4)效果组内的是修改对应效果组内的(param_1)参数，(param_5)效果组内的是修改对应效果组内的(param_2)参数，
            //(param_6)效果组内的是修改对应效果组内的(param_3)参数，(param_7)效果组内的是修改对应效果组内的(param_4)参数，
            //(param_8)效果组内的是修改对应效果组内的(param_5)参数，(param_9)效果组内的是修改对应效果组内的(param_6)参数。(param_3)---(param_9)为-1时无效
            if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2016)
            {
                System.Random pPand = new System.Random();
                int nRand = pPand.Next(1, 1000);

                if (nRand < m_pImpactRow.getParam()[1])
                {
                    BuffgroupTemplate pRow = null;
                    if (m_pImpactRow.getParam()[2] > 0)
                    {
                        pRow = (BuffgroupTemplate)DataTemplate.GetInstance().m_BuffGroupTable.getTableData(m_pImpactRow.getParam()[2]);
                        if (pRow != null)
                        {
                            for (int i = 0; i < pRow.getParam().Length; i++)
                            {
                                if (pImpact.GetImpactID() == pRow.getParam()[i])
                                {
                                    pImpact.RefixImpactTime(m_pImpactRow.getParam()[0]);
                                    break;
                                }
                            }
                        }
                    }
                    if (m_pImpactRow.getParam()[3] > 0)
                    {
                        pRow = (BuffgroupTemplate)DataTemplate.GetInstance().m_BuffGroupTable.getTableData(m_pImpactRow.getParam()[3]);
                        if (pRow != null)
                        {
                            for (int i = 0; i < pRow.getParam().Length; i++)
                            {
                                if (pImpact.GetImpactID() == pRow.getParam()[i])
                                {
                                    pImpact.SetParamRefix(0, m_pImpactRow.getParam()[0]);
                                    break;
                                }
                            }
                        }
                    }

                    if (m_pImpactRow.getParam()[4] > 0)
                    {
                        pRow = (BuffgroupTemplate)DataTemplate.GetInstance().m_BuffGroupTable.getTableData(m_pImpactRow.getParam()[4]);
                        if (pRow != null)
                        {
                            for (int i = 0; i < pRow.getParam().Length; i++)
                            {
                                if (pImpact.GetImpactID() == pRow.getParam()[i])
                                {
                                    pImpact.SetParamRefix(1, m_pImpactRow.getParam()[0]);
                                    break;
                                }
                            }
                        }
                    }

                    if (m_pImpactRow.getParam()[5] > 0)
                    {
                        pRow = (BuffgroupTemplate)DataTemplate.GetInstance().m_BuffGroupTable.getTableData(m_pImpactRow.getParam()[5]);
                        if (pRow != null)
                        {
                            for (int i = 0; i < pRow.getParam().Length; i++)
                            {
                                if (pImpact.GetImpactID() == pRow.getParam()[i])
                                {
                                    pImpact.SetParamRefix(2, m_pImpactRow.getParam()[0]);
                                    break;
                                }
                            }
                        }
                    }

                    if (m_pImpactRow.getParam()[6] > 0)
                    {
                        pRow = (BuffgroupTemplate)DataTemplate.GetInstance().m_BuffGroupTable.getTableData(m_pImpactRow.getParam()[6]);
                        if (pRow != null)
                        {
                            for (int i = 0; i < pRow.getParam().Length; i++)
                            {
                                if (pImpact.GetImpactID() == pRow.getParam()[i])
                                {
                                    pImpact.SetParamRefix(3, m_pImpactRow.getParam()[0]);
                                    break;
                                }
                            }
                        }
                    }

                    if (m_pImpactRow.getParam()[7] > 0)
                    {
                        pRow = (BuffgroupTemplate)DataTemplate.GetInstance().m_BuffGroupTable.getTableData(m_pImpactRow.getParam()[7]);
                        if (pRow != null)
                        {
                            for (int i = 0; i < pRow.getParam().Length; i++)
                            {
                                if (pImpact.GetImpactID() == pRow.getParam()[i])
                                {
                                    pImpact.SetParamRefix(4, m_pImpactRow.getParam()[0]);
                                    break;
                                }
                            }
                        }
                    }

                    if (m_pImpactRow.getParam()[8] > 0)
                    {
                        pRow = (BuffgroupTemplate)DataTemplate.GetInstance().m_BuffGroupTable.getTableData(m_pImpactRow.getParam()[8]);
                        if (pRow != null)
                        {
                            for (int i = 0; i < pRow.getParam().Length; i++)
                            {
                                if (pImpact.GetImpactID() == pRow.getParam()[i])
                                {
                                    pImpact.SetParamRefix(5, m_pImpactRow.getParam()[0]);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void OnDamage(int nDamageType, ref int nInDamage, ref X_GUID guid)
        {
            LogManager.LogAssert(IsActive());
            /*if ( m_pImpactRow.getBuffType () == ( int ) EM_IMPACT_LOGIC.EM_IMPACT_LOGIC111 )
            {
                //吸收(param_1)‰点(param_2)--伤害类型，生命盾拥有(param_3)值生命，生命盾被击破则给(param_5)敌人造成(param_4)点法术伤害
                if ( ( m_pImpactRow.getParam () [ 1 ] == ( int ) ENUM_HURT_TYPE.HURT_TYPE_ALL ) || ( m_pImpactRow.getParam () [ 1 ] == nDamageType ) )
                {
                    int nPermil = GetParam ( 0 );
                    if ( ( nPermil > 0 ) && ( nPermil <= 1000 ) )
                    {
                        int reduceDamage = ( nInDamage * nPermil ) / 1000;
                        if ( reduceDamage >= m_ImpactHP )
                        {
                            nInDamage = nInDamage - m_ImpactHP;
                            m_ImpactHP = 0;
                            //子技能
                            if ( m_pImpactRow.getParam () [ 3 ] > 0 )
                            {
                                SpellInfo spellinfo = new SpellInfo ();
                                spellinfo.Init ( m_pImpactRow.getParam () [ 3 ] );
                                Spell subspell = new Spell ();
                                if ( spellinfo.GetTargetType () == ( int ) EM_TARGET_TYPE.EM_TARGET_ATTACK_ME )
                                {
                                    subspell.SetTargetGuid ( guid );
                                }
                                else if ( spellinfo.GetTargetType () == ( int ) EM_TARGET_TYPE.EM_TARGET_IMPACT_CASTER )
                                {
                                    X_GUID tguid = GetCaster ();
                                    subspell.SetTargetGuid ( tguid );
                                }
                                subspell.SetHolder ( m_pHolder );
                                subspell.Init ( spellinfo );
                                subspell.ImmActiveOnce ();
                            }

                            OnDisappear ();
                        }
                        else
                        {
                            m_ImpactHP = m_ImpactHP - reduceDamage;
                            nInDamage = nInDamage - reduceDamage;
                        }
                    }
                }
            }
            else */
            if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC113)
            {
                //吸收(param_1)‰点(param_2)--伤害类型，生命盾生效(param_3)次，生命盾被击破则给(param_5)敌人造成(param_4)点法术伤害
                if ((m_pImpactRow.getParam()[1] == (int)ENUM_HURT_TYPE.HURT_TYPE_ALL) || (m_pImpactRow.getParam()[1] == nDamageType))
                {
                    int nPermil = GetParam(0);
                    if ((nPermil > 0) && (nPermil <= 1000))
                    {
                        int reduceDamage = (nInDamage * nPermil) / 1000;
                        nInDamage = nInDamage - reduceDamage;
                        ++m_ImpactActiveCount;
                        if (m_ImpactActiveCount >= GetParam(2))
                        {
                            m_ImpactActiveCount = 0;
                            //子技能
                            if (GetParam(3) > 0)
                            {
                                OnActiveChildSpell(GetParam(3), null,null, guid);
                                //SpellInfo spellinfo = new SpellInfo();
                                //spellinfo.Init(m_pImpactRow.getParam()[3]);
                                //Spell subspell = new Spell();
                                //if (spellinfo.GetTargetType() == (int)EM_TARGET_TYPE.EM_TARGET_ATTACK_ME)
                                //{
                                //    subspell.SetTargetGuid(guid);
                                //}
                                //else if (spellinfo.GetTargetType() == (int)EM_TARGET_TYPE.EM_TARGET_IMPACT_CASTER)
                                //{
                                //    X_GUID tguid = GetCaster();
                                //    subspell.SetTargetGuid(tguid);
                                //}
                                //subspell.SetHolder(m_pHolder);
                                //subspell.Init(spellinfo);
                                //subspell.ImmActiveOnce();
                            }
                            OnDisappear();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 伤害结算后介入
        /// </summary>
        /// <param name="pSpellInfo"></param>
        /// <param name="nInDamage"></param>
        public void OnDamageAfter(SpellInfo pSpellInfo, ref int nInDamage)
        {
            int nAttackDamageType = pSpellInfo.GetSpellNum();
            if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2022)
            {
                //效果持续时不会受到特定类型伤害，并在效果结束时改变自己当前生命
                if ((m_pImpactRow.getParam()[0] == (int)ENUM_ATTACKHURT_TYPE.HURT_ATTACKTYPE_ALL))
                {
                    SetParam(nInDamage);
                    nInDamage = 0;                   
                }
                else if (m_pImpactRow.getParam()[0] == (int)ENUM_ATTACKHURT_TYPE.HURT_ATTACKTYPE_COMMON && nAttackDamageType == 0)
                {
                    SetParam(nInDamage);
                    nInDamage = 0; 
                }
            }
        }

        /// <summary>
        /// 伤害
        /// </summary>
        /// <param name="nDamageType">伤害类型</param>
        /// <param name="nInDamage">伤害值</param>
        /// <param name="pTarget">目标</param>
        /// <param name="pSpellInfo">技能信息</param>
        public void OnHurt(int nDamageType, ref int nInDamage, ObjectCreature pTarget, SpellInfo pSpellInfo)
        {
            LogManager.LogAssert(IsActive());
            if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC1301)
            {
                //将所造成的伤害(param_1)‰转换成自身生命
                int damage = (int)((nInDamage * GetParam(0)) / 1000.0f);
                if (damage >= 0)
                {
                    m_pHolder.OnHeal(damage);
                }
            }
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2005)
            {
                //效果持续时，若对拥有(param_1)效果组的目标造成伤害，则激活(param_2)子技能
                BuffgroupTemplate pRow = (BuffgroupTemplate)DataTemplate.GetInstance().m_BuffGroupTable.getTableData(m_pImpactRow.getParam()[0]);
                if (pRow != null)
                {
                    int nCount = 0;
                    for (int i = 0; i < pRow.getParam().Length; i++)
                    {
                        nCount = nCount + m_pHolder.GetImpactCountByID(pRow.getParam()[i]);
                    }
                    //如果找到拥有(param_1)效果组的目标 
                    if (nCount > 0)
                    {
                        if (GetParam(1) > 0)
                        {
                            OnActiveChildSpell(GetParam(1), null, pTarget);
                        }
                    }
                }
            }
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2035)
            {
                if (nInDamage <= 0) return;
                //效果持续时若拥有者释放指定技能编号的技能造成物理/法术伤害则会恢复一定生命
                SpellInfo pCurSpell = pSpellInfo;// m_pHolder.GetSpell();
                LogManager.LogAssert(pCurSpell);
                if ((pCurSpell.GetSpellNum() == m_pImpactRow.getParam()[0])
                    || (pCurSpell.GetSpellNum() == m_pImpactRow.getParam()[1])
                    || (pCurSpell.GetSpellNum() == m_pImpactRow.getParam()[2]))
                {
                    int ranNum = UnityEngine.Random.Range(0,1001);
                    if (ranNum <= GetParam(5) || GetParam(5) == -1)
                    {
                        int nValue = 0;
                        if (GetParam(3) == 1)
                        {
                            nValue = (nInDamage * GetParam(4)) / 1000;
                        }
                        else
                        {
                            nValue = GetParam(4);
                        }
                        if (nValue > 0)
                        {
                            m_pHolder.OnHeal(nValue);
                            //long nMaxHp = m_pHolder.GetMaxHP();
                            //long nDestHp = (int)m_pHolder.GetHP() + nValue;
                            //if (nDestHp > nMaxHp)
                            //{
                            //    nDestHp = nMaxHp;
                            //}
                            //m_pHolder.SetHP(nDestHp);
                        }
                    }
                }
            }
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2047)
            {
                // 效果持续时指定ID的技能造成的伤害提升（实际伤害=原伤害*（1+X/1000））
                if (m_pImpactRow.getParam()[0] > 0)
                {
                    nInDamage = nInDamage * (1 + GetParam(1) / 1000);
                }
            }
        }

        /// <summary>
        /// 伤害结算后
        /// </summary>
        /// <param name="nDamageType">伤害类型</param>
        /// <param name="nInDamage">伤害值</param>
        /// <param name="fLostPermil">当前生命千分比</param>
        /// <param name="pSource">伤害来源</param>
        public void OnAfterDamage(int nDamageType, int nInDamage, float fLostPermil, ObjectCreature pSource)
        {
            LogManager.LogAssert(IsActive());
            if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC102)
            {
                //每损失(param_1)点生命值转换(param_2)点(param_3)--属性枚举,(param_4)点(param_5)--属性枚举,(param_6)点(param_7)--属性枚举,
                //(param_8)点(param_9)--属性枚举,(param_10)点(param_11)--属性枚举,(param_12)点(param_13)--属性枚举
                m_HurtCount += nInDamage;
                if ((nInDamage > 0) && (m_pImpactRow.getParam()[0] > 0))
                {
                    if (m_HurtCount < GetParam(0))
                        return;
                    int nChangeValue = m_HurtCount / GetParam(0);
                    m_HurtCount = m_HurtCount % GetParam(0);
                    for (int i = 1; i < 11; i = i + 2)
                    {
                        if (m_pImpactRow.getParam()[i] > 0)
                        {
                            EM_EXTEND_ATTRIBUTE nExtendAttribute = GetExtendAttribute(m_pImpactRow.getParam()[i + 1]);
                            if (nExtendAttribute != EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_INVALID)
                            {
                                int _ChangeValue = nChangeValue * GetParam(i);
                                if (_ChangeValue > 0)
                                {
                                    m_pHolder.ChangeEffect(nExtendAttribute, _ChangeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT, false);
                                    AddAttributeEffectRefix(nExtendAttribute, _ChangeValue);
                                }
                            }
                        }
                    }
                }
            }
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC103)
            {
                m_FimllCount += fLostPermil;
                //每损失(param_1)‰生命值转换(param_2)点(param_3)--属性枚举,(param_4)点(param_5)--属性枚举,(param_6)点(param_7)--属性枚举,
                //(param_8)点(param_9)--属性枚举,(param_10)点(param_11)--属性枚举,(param_12)点(param_13)--属性枚举
                if ((nInDamage > 0) && (fLostPermil > 0) && (m_pImpactRow.getParam()[0] > 0))
                {
                    if (m_FimllCount < GetParam(0))
                        return;
                    float fChangeValue = m_FimllCount / GetParam(0);
                    m_FimllCount = m_FimllCount % GetParam(0);
                    for (int i = 1; i < 11; i = i + 2)
                    {
                        if (m_pImpactRow.getParam()[i] > 0)
                        {
                            EM_EXTEND_ATTRIBUTE nExtendAttribute = GetExtendAttribute(m_pImpactRow.getParam()[i + 1]);
                            if (nExtendAttribute != EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_INVALID)
                            {
                                fChangeValue = fChangeValue * GetParam(i);
                                if (fChangeValue > 0)
                                {
                                    m_pHolder.ChangeEffect(nExtendAttribute, (int)fChangeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT, false);
                                    AddAttributeEffectRefix(nExtendAttribute, (int)fChangeValue);
                                }
                            }
                        }
                    }
                }
            }
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC1402)
            {
                if ((pSource == null) || (nDamageType == (int)ENUM_HURT_TYPE.HURT_TYPE_DIRECT))
                {
                    return;
                }
                //效果持续时，效果拥有者受到物理/法术攻击造成的伤害后，将掉血量的(param_1)‰数值作为直接伤害给其他(param_2)中拥有该buffID的角色身上
                if ((nInDamage > 0) && (m_pImpactRow.getParam()[0] > 0))
                {
                    //Zmy 标记
                    SceneObjectManager pScene = SceneObjectManager.GetInstance();
                    LogManager.LogAssert(pScene);
                    SCANOPERATOR_INIT init = new SCANOPERATOR_INIT();
                    init.m_Type = m_pImpactRow.getParam()[1];
                    init.m_ChildType = (int)EM_SPELL_TARGET_SENIOR_TYPE.EM_SEPLL_TARGET_REQUIRE_IMPACTID;
                    init.m_Param = GetImpactID();
                    pScene.ScanByObject(m_pHolder, ref init);
                    for (int j = 0; j < init.m_ObjectList.Count; j++)
                    {
                        for (int k = 0; k < init.m_ObjectList[j].GetImpactList().Count; k++)
                        {
                            if (init.m_ObjectList[j].GetImpactList()[k].m_pImpactRow.getId() == m_pImpactRow.getId())
                            {
                                //直接伤害
                                init.m_ObjectList[j].OnDamage((nInDamage * GetParam(0)) / 1000);
                            }
                        }

                    }
                }
            }
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2002)
            {
                if ((pSource == null) || (nDamageType == (int)ENUM_HURT_TYPE.HURT_TYPE_DIRECT))
                {
                    return;
                }
                //前(param_2)次受到物理/法术伤害时激活技能(param_1)。(param_2)为-1时不对次数进行判断
                if (GetParam(0) > 0)
                {
                    OnActiveChildSpell(GetParam(0), pSource);

                    if (GetParam(1) > 0)
                    {
                        ++m_ImpactActiveCount;
                        if (m_ImpactActiveCount >= GetParam(1))
                        {
                            m_ImpactActiveCount = 0;

                            OnDisappear();
                        }
                    }
                }
            }
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2018)
            {
                if ((pSource == null) || (nDamageType == (int)ENUM_HURT_TYPE.HURT_TYPE_DIRECT))
                {
                    return;
                }
                //效果存在时，若有单位对该效果拥有者造成物理/法术伤害，则将该次伤害的(param_1)‰数值作为治疗效果给伤害制造者
                if ((nInDamage > 0) && (m_pImpactRow.getParam()[0] > 0))
                {
                    //治疗
                    pSource.OnHeal((nInDamage * GetParam(0)) / 1000);
                }
            }
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2042)
            {
                //效果持续时受到伤害后当前生命低于(param_1)‰时调用(param_2)子技能
                long nMaxHp = m_pHolder.GetMaxHP();
                long nCurHp = m_pHolder.GetHP();                
                int nValue = (int)((float)nCurHp / (float)nMaxHp * 1000);
                if (nValue < GetParam(0))
                {
                    if (GetParam(1) > 0)
                    {
                        OnActiveChildSpell(GetParam(1), pSource);   
                    }
                }
            }
        }
        /// <summary>
        /// 死亡
        /// </summary>
        public void OnOwnerDead()
        {
            LogManager.LogAssert(IsActive());
            if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC107)
            {
                OnDisappear();
            }
            //效果持续时，若效果拥有者死亡，则效果拥有者激活技能(param_1)
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2001)
            {
                if (GetParam(0) > 0)
                {
                    OnActiveChildSpell(GetParam(0), null);

                    //OnDisappear();
                }
            }

        }

        /// <summary>
        /// 被杀死
        /// </summary>
        /// <param name="pObject"></param>
        /// <param name="pSpellInfo">技能信息</param>
        public void OnBeKillTarget(ObjectCreature pSource, SpellInfo pSpellInfo)
        {
            LogManager.LogAssert(IsActive());
            LogManager.LogAssert(pSource);
            LogManager.LogAssert(pSpellInfo);
            if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2018)
            {
                //效果存在时，若有单位对该效果拥有者造成物理/法术伤害，则将该次伤害的(param_1)‰数值作为治疗效果给伤害制造者，击杀该效果拥有者的单位恢复生命
                if (m_pImpactRow.getParam()[1] == 1)
                {
                    if (m_pImpactRow.getParam()[2] > 0)
                    {
                        if (m_pImpactRow.getBuffTriggerType() == 1)
                        {
                            pSource.OnHeal(m_pImpactRow.getParam()[2]);
                        }
                    }
                }
                else if (m_pImpactRow.getParam()[1] == 2)
                {
                    long nValue = (m_pHolder.GetMaxHP() * m_pImpactRow.getParam()[2]) / 1000;
                    if (nValue != 0)
                    {
                        if (m_pImpactRow.getParam()[2] > 0)
                        {
                            if (m_pImpactRow.getBuffTriggerType() == 1)
                            {
                                pSource.OnHeal(m_pImpactRow.getParam()[2]);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 闪避
        /// </summary>
        /// <param name="pCaster">攻击者</param>
        /// <param name="pSpellInfo"></param>
        public void OnDodge(ObjectCreature pCaster, SpellInfo pSpellInfo)
        {
            LogManager.LogAssert(IsActive());
            //效果持续时，每触发一次闪避则激活技能(param_1)，最多生效(param_2)次。 持续时间到或者达到param_2次后该buff/debuff移除，param_2为-1时不判断次数
            if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2003)
            {
                if (GetParam(0) > 0)
                {
                    OnActiveChildSpell(GetParam(0),pCaster);

                    if (GetParam(1) > 0)
                    {
                        ++m_ImpactActiveCount;
                        if (m_ImpactActiveCount >= GetParam(1))
                        {
                            m_ImpactActiveCount = 0;
                            OnDisappear();
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 暴击
        /// </summary>
        /// <param name="pTarget">目标</param>
        /// <param name="pSpellInfo">技能信息</param>
        /// <param name="bCritical">是否暴击</param>
        public void OnSpellAfterCaculateCritical(ObjectCreature pTarget, SpellInfo pSpellInfo, bool bCritical)
        {
            LogManager.LogAssert(IsActive());
            if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC118)
            {
                //增减(param_1)‰暴击率，暴击后删除该buff。正数代表增加，负数代表减少
                if (bCritical)
                {
                    m_ImpactActiveCount = 0;
                    OnDisappear();
                }
            }
            //效果持续时，若效果拥有者(param_3)(param_4)(param_5)(param_6)技能编号里的技能攻击未暴击则调用(param_1)子技能，若效果拥有者攻击暴击则调用(param_2)子技能。
            //其中(param_3)(param_4)(param_5)(param_6)中技能编号里所属的技能为(param_1)和(param_2)的父技能。(param_3)---(param_6)为-1时表示无效
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2007)
            {
                for (int i = 2; i < 6; i++)
                {
                    if (m_pImpactRow.getParam()[i] >= 0)
                    {
                        if (pSpellInfo.GetSpellNum() == m_pImpactRow.getParam()[i])
                        {
                            if (bCritical)
                            {
                                if (GetParam(1) > 0)
                                {
                                    OnActiveChildSpell(GetParam(1), null,pTarget);
                                }
                            }
                            else
                            {
                                if (GetParam(0) > 0)
                                {
                                    OnActiveChildSpell(GetParam(0), null, pTarget);
                                }
                            }
                        }
                    }
                }
            }
        }


        public void OnSpellBeforeUpdateConsume(bool bNormal, ref int nMP)
        {
            LogManager.LogAssert(IsActive());
            LogManager.LogAssert(IsActive());
            //效果存在时，(param_1)次释放技能不消耗怒气。剩余次数为0时删除buff
            if (((m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC117) && (!bNormal)) || (m_pImpactRow.getBuffType() == 7))
            {
                ++m_ImpactActiveCount;

                if (m_ImpactActiveCount >= m_pImpactRow.getParam()[0])
                {
                    if (m_pImpactRow.getParam()[1] == 1)
                    {
                        nMP = nMP + m_pImpactRow.getParam()[2];
                        if (nMP < 0)
                        {
                            nMP = 0;
                        }
                    }
                    else if (m_pImpactRow.getParam()[1] == 2)
                    {
                        nMP = (nMP * m_pImpactRow.getParam()[2]) / 1000;
                        if (nMP < 0)
                        {
                            nMP = 0;
                        }
                    }
                    m_ImpactActiveCount = 0;
                    OnDisappear();
                }
            }
        }

        /// <summary>
        /// 被暴击
        /// </summary>
        /// <param name="pCaster"></param>
        /// <param name="pSpellInfo"></param>
        public void OnBeCritical(ObjectCreature pCaster, SpellInfo pSpellInfo)
        {
            LogManager.LogAssert(IsActive());
            //效果持续时，若效果拥有者被暴击则调用(param_1)技能
            if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2008)
            {

                System.Random ran = new System.Random();
                int nOdds = ran.Next(1, 1000);
                if (nOdds < m_pImpactRow.getParam()[1] || m_pImpactRow.getParam()[1] < 0)
                {
                    //子技能
                    if (GetParam(0) > 0)
                    {
                        OnActiveChildSpell(GetParam(0), pCaster);
                    }
                }
            }
        }

        /// <summary>
        /// 受到治疗
        /// </summary>
        /// <param name="hp">治疗的值</param>
        /// <param name="nHealValue"></param>
        /// <param name="bNormal"></param>
        public void OnBeHeal(int hp = 0, int nHealValue = 0, bool bNormal = true)
        {
            LogManager.LogAssert(IsActive());
            if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC102)
            {
                m_RefHp += hp;
                if (hp > 0)
                {
                    if (m_RefHp < GetParam(0))
                        return;
                    int nChangeValue = m_RefHp / GetParam(0);
                    m_RefHp = m_RefHp % GetParam(0);
                    for (int i = 1; i < 11; i = i + 2)
                    {
                        if (m_pImpactRow.getParam()[i] > 0)
                        {
                            EM_EXTEND_ATTRIBUTE nExtendAttribute = GetExtendAttribute(m_pImpactRow.getParam()[i + 1]);
                            if (nExtendAttribute != EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_INVALID)
                            {
                                int _ChangeValue = nChangeValue * GetParam(i);
                                if (_ChangeValue > 0)
                                {
                                    m_pHolder.ChangeEffect(nExtendAttribute, _ChangeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT, true);
                                }
                            }
                        }
                    }
                }
            }
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC1401)
            {
                //效果持续时，将所受伤害减少(param_1)‰，并将减少的伤害分摊到(param_2)中拥有该类效果的相同buffID的角色身上。最终伤害为减完的伤害/数量。受到治疗时，
                //(param_2)中拥有该类效果的相同buffID的角色都会恢复生命。由该效果分摊出去的伤害为直接伤害。
                SceneObjectManager pScene = SceneObjectManager.GetInstance();
                LogManager.LogAssert(pScene);
                SCANOPERATOR_INIT init = new SCANOPERATOR_INIT();
                init.m_Type = m_pImpactRow.getParam()[1];
                init.m_ChildType = (int)EM_SPELL_TARGET_SENIOR_TYPE.EM_SEPLL_TARGET_REQUIRE_IMPACTID;
                init.m_Param = GetImpactID();
                pScene.ScanByObject(m_pHolder, ref init);
                if (init.m_ObjectList.Count > 0)
                {
                    if (m_pImpactRow.getParam()[2] == 1)
                    {
                        if (m_pImpactRow.getParam()[3] < 0)
                        {
                            for (int j = 0; j < init.m_ObjectList.Count; j++)
                            {
                                init.m_ObjectList[j].OnDamage(m_pImpactRow.getParam()[3]);
                            }
                        }
                        else if (m_pImpactRow.getParam()[3] > 0)
                        {
                            if (m_pImpactRow.getBuffTriggerType() == 1)
                            {
                                for (int j = 0; j < init.m_ObjectList.Count; j++)
                                {
                                    init.m_ObjectList[j].OnHeal(m_pImpactRow.getParam()[3]);
                                }
                            }
                            else
                            {
                                for (int j = 0; j < init.m_ObjectList.Count; j++)
                                {
                                    int nHp = (int)init.m_ObjectList[j].GetHP() + m_pImpactRow.getParam()[3];
                                    init.m_ObjectList[j].SetHP(nHp);
                                }
                            }
                        }
                    }
                    else if (m_pImpactRow.getParam()[2] == 2)
                    {
                        long nValue = (m_pHolder.GetMaxHP() * m_pImpactRow.getParam()[3]) / 1000;
                        if (nValue != 0)
                        {
                            if (nValue < 0)
                            {
                                for (int j = 0; j < init.m_ObjectList.Count; j++)
                                {
                                    init.m_ObjectList[j].OnDamage(m_pImpactRow.getParam()[3]);
                                }
                            }
                            else if (nValue > 0)
                            {
                                if (m_pImpactRow.getBuffTriggerType() == 1)
                                {
                                    for (int j = 0; j < init.m_ObjectList.Count; j++)
                                    {
                                        init.m_ObjectList[j].OnHeal(m_pImpactRow.getParam()[3]);
                                    }
                                }
                                else
                                {
                                    for (int j = 0; j < init.m_ObjectList.Count; j++)
                                    {
                                        int nHp = (int)init.m_ObjectList[j].GetHP() + m_pImpactRow.getParam()[3];
                                        init.m_ObjectList[j].SetHP(nHp);
                                    }
                                }
                            }
                        }
                    }
                }
            }                            
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2009)
            {
                //效果持续时，若效果拥有者受到治疗则调用(param_1)技能
                System.Random ran = new System.Random();
                int nOdds = ran.Next(1, 1000);
                if (nOdds < m_pImpactRow.getParam()[1] || m_pImpactRow.getParam()[1] == -1)
                {
                    //子技能
                    if (GetParam(0) > 0)
                    {
                        OnActiveChildSpell(GetParam(0),null);
                    }
                }
            } 
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2032)
            {
                ++m_ImpactActiveCount;
                m_Param = m_Param + nHealValue;
                if (m_pImpactRow.getParam()[0] == 1)
                {
                    if (m_ImpactActiveCount >= m_pImpactRow.getParam()[1])
                    {
                        m_ImpactActiveCount = 0;
                        if (m_pImpactRow.getParam()[2] == 1)
                        {
                            int nValue = (m_Param * m_pImpactRow.getParam()[3]) / 1000;
                            OnDamage((int)ENUM_HURT_TYPE.HURT_TYPE_DIRECT, ref nValue, ref m_pImpactObject.m_CasterGUID);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 杀死目标
        /// </summary>
        /// <param name="pTarget">目标</param>
        /// <param name="pSpellInfo">技能信息</param>
        public void OnKillTarget(ObjectCreature pTarget, SpellInfo pSpellInfo,ref int nPowerValue)
        {
            LogManager.LogAssert(IsActive());
            LogManager.LogAssert(pTarget);
            LogManager.LogAssert(pSpellInfo);
            if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2014)
            {
                //效果持续时，(param_1)技能编号若击杀目标则技能的冷却时间改变为(param_2) 注：正在冷却中的技能不受影响;
                if (pSpellInfo.GetSpellNum() == m_pImpactRow.getParam()[0])
                {
                    CoolDownList pCoolDown = m_pHolder.GetCoolDownList();
                    LogManager.LogAssert(pCoolDown);
                    bool bRet = pCoolDown.AddElement(pSpellInfo.GetSpellID(), GetParam(1));
                    if (!bRet)
                    {
                        LogManager.LogAssert(0);
                    }
                }
            }
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2043)
            {
                //效果持续时使用指定技能编号技能击杀敌人后获得怒气量改变
                if (pSpellInfo.GetSpellNum() == GetParam(2) 
                    || pSpellInfo.GetSpellNum() == GetParam(3) 
                    || pSpellInfo.GetSpellNum() == GetParam(4))
                {
                    if (GetParam(0) == 1)
                    {
                        //获得怒气=原怒气*（1+X/1000）
                        nPowerValue = (int)(nPowerValue * (1 + GetParam(1) / 1000.0f));
                    }
                    else if (GetParam(0) == 2)
                    {
                        //获得怒气=X    GetParam(1)为X
                        nPowerValue = GetParam(1);
                    }
                }
            }
        }


        /// <summary>
        /// 自身血量变化
        /// </summary>
        public void OnSelfHpChange()
        {
            LogManager.LogAssert(IsActive());
            if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2015)
            {
                //当前生命跨越(param_1)‰时激活子技能，大于等于(param_1)激活技能(param_2)，否则激活(param_3)技能。(param_2）与(param_3）不能共存，一个生效时删除另外一个
                float f = m_pHolder.GetHP() / m_pHolder.GetMaxHP();
                if (f >= GetParam(0) / 1000.0f)
                {
                    if (GetParam(1) > 0)
                    {
                        OnActiveChildSpell(GetParam(1), null);
                    }
                }
                else
                {
                    if (GetParam(2) > 0)
                    {
                        OnActiveChildSpell(GetParam(2), null);
                    }
                }
            }
        }

        /// <summary>
        /// 使用技能
        /// </summary>
        /// <param name="pSpellInfo">技能</param>
        /// <param name="m_SpellTarget">技能目标</param>
        public void OnSpellUse(SpellInfo pSpellInfo, X_GUID m_SpellTarget)
        {
            LogManager.LogAssert(pSpellInfo);
            //             if ( ( m_pImpactRow.getBuffType () == ( int ) EM_IMPACT_LOGIC.EM_IMPACT_LOGIC117 )  )
            //             {
            //                 ++m_ImpactActiveCount;
            //                 if ( m_ImpactActiveCount >= GetParam ( 0 ) )
            //                 {
            //                     m_ImpactActiveCount = 0;
            //                     OnDisappear ();
            //                 }
            //             }
            if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC1102)
            {
                OnDisappear();
            }
            else if ((m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2012))
            {
                //任意目标施放一个技能编号为(param_3)(param_4)(param_5)的技能便获得+1计数，当计数达到(param_1)则激活技能(param_2)。(param_3)(param_4)(param_5)为-1时表示无效
                if ((pSpellInfo.GetSpellID() == m_pImpactRow.getParam()[2])
                    || (pSpellInfo.GetSpellID() == m_pImpactRow.getParam()[3])
                    || (pSpellInfo.GetSpellID() == m_pImpactRow.getParam()[4]))
                {
                    SceneObjectManager pScene = SceneObjectManager.GetInstance();
                    LogManager.LogAssert(pScene);
                    X_GUID guid = GetCaster();
                    if (guid.IsValid())
                    {
                        ObjectCreature pCreature = pScene.GetObjectHeroByGUID(guid);
                        if (pCreature != null && pCreature.IsAlive())
                        {
                            Impact pCaserImpact = pCreature.GetImpactByIDAndCastGuid(GetImpactID(), guid);
                            int nActiveCount = pCaserImpact.GetImpactActiveCount();
                            pCaserImpact.SetImpactActiveCount(nActiveCount + 1);
                            if (nActiveCount >= GetParam(0))
                            {
                                if (GetParam(1) > 0)
                                {
                                    OnActiveChildSpell(GetParam(1), null);
                                }
                            }
                        }
                    }
                }
            }
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2029)
            {
                //效果持续时每一定次数攻击（普攻/技能）触发特定BUFF
                if ((pSpellInfo.GetSpellNum() == m_pImpactRow.getParam()[1])
                    || (pSpellInfo.GetSpellNum() == m_pImpactRow.getParam()[2])
                    || (pSpellInfo.GetSpellNum() == m_pImpactRow.getParam()[3]))
                {
                    int nActiveCount = GetImpactActiveCount();
                    SetImpactActiveCount(nActiveCount + 1);
                    if (nActiveCount >= GetParam(0))
                    {
                        for (int i = 4; i < 11; i++)
                        {
                            if (GetParam(i) > 0)
                            {
                                m_pHolder.AddImpact(GetParam(i), m_pHolder, false, m_SpellID);
                            }
                        }
                    }
                }
            }
            else if (m_pImpactRow.getBuffType() == (int)EM_IMPACT_LOGIC.EM_IMPACT_LOGIC2030)
            {
                //效果持续时每一定次数攻击（普攻/技能）触发特定BUFF
                if ((pSpellInfo.GetSpellNum() == m_pImpactRow.getParam()[0])
                    || (pSpellInfo.GetSpellNum() == m_pImpactRow.getParam()[1])
                    || (pSpellInfo.GetSpellNum() == m_pImpactRow.getParam()[2]))
                {
                    int nActiveCount = GetImpactActiveCount();
                    SetImpactActiveCount(nActiveCount + 1);
                    if (nActiveCount >= GetParam(3))
                    {
                        OnDisappear();
                    }
                }
            }

        }

        public static EM_EXTEND_ATTRIBUTE GetExtendAttribute(int nAttribute)
        {
            if (nAttribute < (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_INVALID || nAttribute >= (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_NUMBER)
            {
                Debug.LogError("!!!Error:GetExtendAttribute() nAttribute RangeOut!:" + nAttribute);
                return EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_INVALID;
            }
            switch (nAttribute)
            {
                // 尚未定义的返回error [7/27/2015 Zmy]
                case 0:
                case 34:
                    {
                        Debug.LogError("!!!Error:GetExtendAttribute() nAttribute Param Error!");
                        return EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_INVALID;
                    }
                default:
                    return (EM_EXTEND_ATTRIBUTE)nAttribute;
            }
        }

        /// <summary>
        /// 激活子技能
        /// </summary>
        /// <param name="pSpellID">子技能ID</param>
        /// <param name="pSource">伤害来源</param>
        /// <param name="nguid">伤害来源Guid</param>
        public void OnActiveChildSpell(int pSpellID,ObjectCreature pSource,ObjectCreature pTarget = null,X_GUID nguid = null)
        {
            if (m_TempSpellInfo.GetSpellID() == -1 || m_TempSpellInfo.GetSpellID() != pSpellID)
                m_TempSpellInfo.Init(pSpellID);

            if (m_TempSpellInfo.GetTargetType() == (int)EM_TARGET_TYPE.EM_TARGET_ATTACK_ME)
            {
                LogManager.LogAssert(pSource);
                if(nguid == null)
                    nguid = pSource.GetGuid();
            }
            else if (m_TempSpellInfo.GetTargetType() == (int)EM_TARGET_TYPE.EM_TARGET_IMPACT_CASTER)
            {
                nguid = GetCaster();
            }
            else
            {
                if (pTarget != null)
                    nguid = pTarget.GetGuid();               
            }


            if (nguid != null) 
                m_TempSpell.SetTargetGuid(nguid);


            m_TempSpell.SetHolder(m_pHolder);
            m_TempSpell.Init(m_TempSpellInfo);
            if (m_TempSpell._CheckSpellCooldown())
            {
                m_TempSpell.ImmActiveOnce();
                m_pHolder.SetActivationSpellCD(m_TempSpellInfo);
            }
        }

    }
}
