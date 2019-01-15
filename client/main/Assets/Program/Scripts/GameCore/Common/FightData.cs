using DreamFaction.GameNetWork;

namespace DreamFaction.GameNetWork.Data
{
    /// <summary>
    /// 防御方属性信息，包括，所选目标，命中，受影响目标数量，剩余血量等！
    /// </summary>
    public class DefenceInfo
    {
        public byte m_Defencer;		//所选目标
        public byte m_Hit;				//命中
        public byte m_nImpactCount;	//最终影响目标的数量
        public int[] m_Impact = new int[GlobalMembers.MAX_IMPACT_NUMBER];// 受影响数组，上限是16个目标！
        public long m_RemainHP;		    //剩余血量

        /// <summary>
        /// 构造函数，执行清理操作
        /// </summary>
        public DefenceInfo()
        {
            CleanUp();
        }
        /// <summary>
        /// 清理操作
        /// </summary>
        public void CleanUp()
        {
            m_Defencer = 0;
            m_Hit = 0;
            m_nImpactCount = 0;
            for (int i = 0; i < GlobalMembers.MAX_IMPACT_NUMBER; ++i)
            {
                m_Impact[i] = int.MaxValue;
            }
            m_RemainHP = 0;
        }

        /// <summary>
        /// 设置防御方，通过位置和是否是攻击标记进行区分，
        /// </summary>
        /// <param name="pos">玩家位置</param>
        /// <param name="bAttack">是否是攻击动作</param>
        public void SetDefencer(byte pos, bool bAttack)
        {
            m_Defencer |= (byte)(1 << pos);
            if (bAttack)
            {
                m_Defencer |= 0x80;
            }
        }


        /// <summary>
        /// 数据拷贝操作
        /// </summary>
        /// <param name="defenceinfo"></param>
        public void Copy(DefenceInfo defenceinfo)
        {
            this.m_Defencer = defenceinfo.m_Defencer;
            this.m_Hit = defenceinfo.m_Hit;
            this.m_nImpactCount = defenceinfo.m_nImpactCount;
            for (int i = 0; i < this.m_nImpactCount; ++i)
            {
                this.m_Impact[i] = defenceinfo.m_Impact[i];
            }
            this.m_RemainHP = defenceinfo.m_RemainHP;
        }

        public void CopyData(GNET.defenceInfo defenceinfo)
        {
            this.m_Defencer = defenceinfo.m_defencer;
            this.m_Hit = defenceinfo.m_hit;
            this.m_nImpactCount = defenceinfo.m_nimpactcount;
            int[] nImpactindex = new int[m_nImpactCount];
            defenceinfo.m_impact.CopyTo(nImpactindex, 0);
            for (int i = 0; i < this.m_nImpactCount; ++i)
            {
                this.m_Impact[i] = nImpactindex[i];
            }
            this.m_RemainHP = defenceinfo.m_remainhp;
        }
    }

    /// <summary>
    /// 战斗记录数据信息，包括，所选目标，技能ID，命中，目标数量，最终受影响目标数量，防御方信息等！<br/>
    /// 战斗中保存此数据，战斗结束后将此数据发送给服务器进行验证！
    /// </summary>
    public class FightInfo
    {
        public byte m_Attacker;		//发动攻击的GUID
        public int m_SpellID;			//当前使用的技能id(有可能无效,如果无效需要继续判断子技能id)
        public byte m_nCount;			//目标数量
        public byte m_nImpactCount;	//最终impact数量
        public int[] m_Impact = new int[GlobalMembers.MAX_IMPACT_NUMBER];
        public DefenceInfo[] m_DefenceInfo = new DefenceInfo[GlobalMembers.MAX_TEAM_CELL_COUNT];//防御方信息
        /// <summary>
        /// 构造函数，执行清理操作
        /// </summary>
        public FightInfo()
        {
            CleanUp();
        }
        /// <summary>
        ///  清理操作
        /// </summary>
        public void CleanUp()
        {
            m_Attacker = 0;
            m_nImpactCount = 0;
            m_SpellID = int.MaxValue;
            m_nCount = 0;
            for (int i = 0; i < GlobalMembers.MAX_IMPACT_NUMBER; ++i)
            {
                m_Impact[i] = int.MaxValue;
            }
            for (int i = 0; i < GlobalMembers.MAX_TEAM_CELL_COUNT; ++i)
            {
                if (m_DefenceInfo[i] == null)
                {
                    m_DefenceInfo[i] = new DefenceInfo();
                }
                m_DefenceInfo[i].CleanUp();
            }
        }

        /// <summary>
        /// 设置攻击方，。。。。。
        /// </summary>
        /// <param name="pos">玩家位置</param>
        /// <param name="bAttacker">是否攻击</param>
        public void SetAttack(byte pos, bool bAttacker)
        {
            m_Attacker |= (byte)(1 << pos);
            if (bAttacker)
            {
                m_Attacker |= 0x80;
            }
        }


        /// <summary>
        /// 数据拷贝操作
        /// </summary>
        /// <param name="fightinfo"></param>
        public void Copy(FightInfo fightinfo)
        {
            this.m_Attacker = fightinfo.m_Attacker;
            this.m_nImpactCount = fightinfo.m_nImpactCount;
            for (int i = 0; i < this.m_nImpactCount; ++i)
            {
                this.m_Impact[i] = fightinfo.m_Impact[i];
            }
            this.m_SpellID = fightinfo.m_SpellID;
            this.m_nCount = fightinfo.m_nCount;
            for (int i = 0; i < this.m_nCount; ++i)
            {
                this.m_DefenceInfo[i].Copy(fightinfo.m_DefenceInfo[i]);
            }
        }

        public void CopyData(GNET.fightInfo fightinfo)
        {
            this.m_Attacker = fightinfo.m_attacker;
            this.m_nImpactCount = fightinfo.m_nimpactcount;

            int[] nImpactIndex = new int[m_nImpactCount];
            fightinfo.m_impact.CopyTo(nImpactIndex, 0);
            for (int i = 0; i < this.m_nImpactCount; ++i)
            {
                this.m_Impact[i] = nImpactIndex[i];
            }
            this.m_SpellID = fightinfo.m_spellid;
            this.m_nCount = fightinfo.m_ncount;
//             for (int i = 0; i < this.m_nCount; ++i)
//             {
//                 this.m_DefenceInfo[i].Copy(fightinfo.m_DefenceInfo[i]);
//             }
            int nindex = 0;
            foreach (var item in fightinfo.m_defenceinfo)
            {
                this.m_DefenceInfo[nindex].CopyData(item);
                nindex++;
            }
        }
    }
}