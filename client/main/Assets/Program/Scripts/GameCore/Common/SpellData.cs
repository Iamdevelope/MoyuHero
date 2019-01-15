
namespace DreamFaction.GameNetWork.Data
{
    /// <summary>
    /// 技能数据信息，包括技能ID，是否解锁等！
    /// </summary>
    public class SpellData
    {
        private int m_SpellID;		//技能id
        private byte m_Unlock;		//是否解锁
        /// <summary>
        /// 构造函数，清理操作
        /// </summary>
        public SpellData()
        {
            CleanUp();
        }
        /// <summary>
        /// 清理操作
        /// </summary>
        public void CleanUp()
        {
            m_SpellID = int.MaxValue;
            m_Unlock = 0;
        }

        /// <summary>
        /// 获取技能ID, set操作测试用，正式以后删掉set操作！
        /// </summary>
        public int SpellID { get { return m_SpellID; } set { m_SpellID = value; } }

        /// <summary>
        /// 拷贝操作
        /// </summary>
        /// <param name="spelldata"></param>
        public void Copy(SpellData spelldata)
        {
            this.m_SpellID = spelldata.m_SpellID;
            this.m_Unlock = spelldata.m_Unlock;
        }
    }

    /// <summary>
    /// 技能冷却 cooldown ，包括冷却ID，冷却时间，已经过去的时间！<br/>
    /// 注意：只在初始化的时候服务器同步一次给客户端，之后客户端自己进行计时！计时到了以后等待服务器通知删除，客户端不要自行删除！<br/>
    /// </summary>
    public class CoolDownObject
    {
	    public int	 m_nID;					//冷却id
        public uint m_nCoolDownTime;		//冷却时间
        public uint m_nCoolDownElapsed;	//已经经过的时间
        /// <summary>
        /// 构造函数,执行清理操作
        /// </summary>
        public CoolDownObject()
        {
            CleanUp();
        }

        /// <summary>
        /// 清理
        /// </summary>
	    public void CleanUp()
	    {
		    m_nID = -1;
		    m_nCoolDownTime = 0;
		    m_nCoolDownElapsed = 0;
	    }

        /// <summary>
        /// 获取/设置 冷却ID
        /// </summary>
        public int CollDownID { get { return m_nID; } set { m_nID = value; } }

        /// <summary>
        /// 获取/设置 冷却时间
        /// </summary>
        public uint CoolDownTime { get { return m_nCoolDownTime; } set { m_nCoolDownTime = value; } }

        /// <summary>
        /// CoolDownObject 心跳  ，每帧调用一次! 具体调用地方由战斗逻辑控制！
        /// </summary>
        /// <param name="nDeltaTime">已消耗的时间</param>
	    public void HeartBeat(uint nDeltaTime)
	    {
		    if(0>m_nID)
		    {
			    return;
		    }
		    if(m_nCoolDownTime<=m_nCoolDownElapsed)
		    {
			    return;
		    }
		    m_nCoolDownElapsed +=nDeltaTime;
		    if(m_nCoolDownTime<m_nCoolDownElapsed)
		    {
			    m_nCoolDownTime=m_nCoolDownElapsed;
		    }

	    }

        /// <summary>
        /// 判断是否在CoolDown中
        /// </summary>
        /// <returns>是否在冷却</returns>
	    public bool IsCoolDown()
	    {
		    return m_nCoolDownTime>m_nCoolDownElapsed;
	    }

        /// <summary>
        /// 获取剩余冷却时间
        /// </summary>
        /// <returns>剩余时间</returns>
	    public uint GetRemainTime()
	    {
		    return m_nCoolDownTime - m_nCoolDownElapsed;
	    }
    }

    /// <summary>
    /// 冷却列表，包括所有处于冷却中的CoolDownObject！
    /// </summary>
    public class CoolDownList
    {
        /// <summary>
        /// 处于冷却中的对象列表！
        /// </summary>
        public CoolDownObject[] m_sCoolDownObject;
        /// <summary>
        /// 公共CD
        /// </summary>
	    public CoolDownObject commonCD;	
        /// <summary>
        /// 构造函数
        /// </summary>
	    public CoolDownList()
	    {
		    //CleanUp();
            m_sCoolDownObject = new CoolDownObject[GlobalMembers.MAX_SPELL_COOLDOWN_NUMBER];
            for (int i = 0; i < GlobalMembers.MAX_SPELL_COOLDOWN_NUMBER;i++ )
            {
                if (m_sCoolDownObject[i] == null)
                {
                    m_sCoolDownObject[i] = new CoolDownObject();
                }
            }
            commonCD = new CoolDownObject();
		    commonCD.CollDownID = 0;
            commonCD.CoolDownTime = 0;
	    }

        /// <summary>
        /// 清理操作
        /// </summary>
	    public void CleanUp()
	    {
            for (int index = 0; index < GlobalMembers.MAX_SPELL_COOLDOWN_NUMBER; index++)
		    {
                if (m_sCoolDownObject[index] != null)
                {
                    m_sCoolDownObject[index].CleanUp();
                }
		    }
            if (commonCD != null)
            {
                commonCD.CleanUp();
            }
	    }

        /// <summary>
        /// 冷却心跳，每帧更新，具体看逻辑模块！
        /// </summary>
        /// <param name="nDeltaTime">上一帧消耗的时间</param>
	    public void HeartBeat(uint nDeltaTime)
	    {
            for (int index = 0; index < GlobalMembers.MAX_SPELL_COOLDOWN_NUMBER; index++)
		    {
			    m_sCoolDownObject[index].HeartBeat(nDeltaTime);
		    }
		    commonCD.CleanUp();
	    }

        /// <summary>
        /// 判断某个冷却ID，是否处于冷却中，要判断公共CD！
        /// </summary>
        /// <param name="spellCooldownID"></param>
        /// <returns></returns>
	    public bool IsCoolDown(uint spellCooldownID)
	    {
		    if (commonCD.IsCoolDown())
		    {
			    return true;
		    }
		    int index = 0;
            for (index = 0; index < GlobalMembers.MAX_SPELL_COOLDOWN_NUMBER; index++)
		    {
			    if (m_sCoolDownObject[index].CollDownID == spellCooldownID)
			    {
				    return m_sCoolDownObject[index].IsCoolDown();
			    }
		    }
		    return false;
	    }

        /// <summary>
        /// 判断某个技能是否在冷却中！忽略公共CD！
        /// </summary>
        /// <param name="spellCooldownID"></param>
        /// <returns></returns>
        public bool IsSpellCoolDown(int spellCooldownID)
	    {
		    int index = 0;
            for (index = 0; index < GlobalMembers.MAX_SPELL_COOLDOWN_NUMBER; index++)
		    {
			    if (m_sCoolDownObject[index].CollDownID == spellCooldownID)
			    {
				    return m_sCoolDownObject[index].IsCoolDown();
			    }
		    }
		    return false;
	    }

        /// <summary>
        /// 重置公共CD的时间
        /// </summary>
        /// <param name="coolDownTime">要重置的时间</param>
        public void ResetCommonCD(uint coolDownTime)
	    {
            commonCD.CoolDownTime = coolDownTime;
	    }

        /// <summary>
        /// 添加一个技能到冷却队列里
        /// </summary>
        /// <param name="spellCoolID">技能ID</param>
        /// <param name="coolDownTime">技能冷却需要消耗的时间</param>
        /// <returns></returns>
        public bool AddElement(int spellCoolID, int coolDownTime)
	    {
		    int nEmptySlot = -1;
		    if (0 > coolDownTime)
		    {
			    return false;
		    }
		    if (spellCoolID == -1)
		    {
			    return false;
		    }
            for (int index = 0; index < GlobalMembers.MAX_SPELL_COOLDOWN_NUMBER; ++index)
		    {
			    if (-1 == nEmptySlot)
			    {
				    if (-1 == m_sCoolDownObject[index].CollDownID)
				    {
					    nEmptySlot = index;
				    }
				    else if (false == m_sCoolDownObject[index].IsCoolDown())
				    {
					    nEmptySlot = index;
				    }
			    }
			    if (m_sCoolDownObject[index].CollDownID == spellCoolID)
			    {
				    m_sCoolDownObject[index].CleanUp();
				    m_sCoolDownObject[index].CollDownID = spellCoolID;
                    m_sCoolDownObject[index].CoolDownTime = (uint)coolDownTime;
				    return true;
			    }
		    }
		    if (-1 != nEmptySlot)
		    {
			    m_sCoolDownObject[nEmptySlot].CleanUp();
			    m_sCoolDownObject[nEmptySlot].CollDownID = spellCoolID;
                m_sCoolDownObject[nEmptySlot].CoolDownTime = (uint)coolDownTime;

			    return true;
		    }

		    return false;
	    }
        public CoolDownObject GetCoolDownObject(uint spellCoolID)
        {
            int index = 0;
            for (index = 0; index < GlobalMembers.MAX_SPELL_COOLDOWN_NUMBER; index++)
            {
                if (m_sCoolDownObject[index].m_nID == spellCoolID)
                {
                    return m_sCoolDownObject[index];
                }
            }
            return null;
        }
    }

    /// <summary>
    /// BUFF的数据信息
    /// </summary>
    public class ImpactData
    {
        //产生此IMPACT的技能ID
        /// <summary>
        /// 创建者类型
        /// </summary>
        public int m_nCreaterType;		   
        /// <summary>
        /// 释放者GUID	
        /// </summary>
        public X_GUID m_CasterGUID;		   
        /// <summary>
        /// impactID,也就是BuffID
        /// </summary>
        public int m_nImpactID;		       
        /// <summary>
        ///  impact持续时间,也就是Buff持续时间
        /// </summary>
        public int m_ImpactTime;
		/// <summary>
        /// 已经过去的时间
		/// </summary>
        public int m_ElapsedTime;
		/// <summary>
        /// 间隔时间,
		/// </summary>
        public int m_IntervalTime;		    
        /// <summary>
        /// Buff间隔触发时间，已经过去的时间
        /// </summary>
        public int m_ElapsedIntervalTime;   
        /// <summary>
        /// Buff作用次数
        /// </summary>
        public short m_nCount;
	    /// <summary>
        /// 叠加层数 (互斥次数)
	    /// </summary>
        public short m_nLayedCount;         

        public ImpactData()
        {
            m_CasterGUID = new X_GUID();
            CleanUp();
        }
        /// <summary>
        /// 清理操作
        /// </summary>
        public void CleanUp()
        {
            m_nCreaterType = int.MaxValue;
            m_CasterGUID.CleanUp();
            m_nImpactID = -1;
            m_ImpactTime = 0;
            m_ElapsedTime = 0;
            m_IntervalTime = 0;
            m_ElapsedIntervalTime = 0;
            m_nCount = 0;
            m_nLayedCount = 1;
        }
        /// <summary>
        /// 判断这个Buff是否有效
        /// </summary>
        /// <returns></returns>
        public bool IsInValid()
        {
            if (m_ImpactTime == 0)
            {
                return false;
            }
            if (m_nImpactID == -1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断BUFF时间是否结束
        /// </summary>
        /// <returns></returns>
        public bool IsOverTime()
        {
            if (m_ImpactTime == -1)
                return false;
            if (m_ImpactTime <= m_ElapsedTime)
            {
                return true;
            }
            return false;
        }
    }
}