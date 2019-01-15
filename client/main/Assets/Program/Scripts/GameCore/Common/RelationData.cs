using DreamFaction.GameNetWork;

namespace DreamFaction.GameNetWork.Data
{
    /// <summary>
    /// 好友信息，包括，头像ID，排名，等级，登录时间，战斗力，队伍战斗力，公会GUID等等！
    /// </summary>
    public class RelationMemberData
    {
        private X_GUID m_MemberGUID = new X_GUID();	//guid
        private int m_IconID;		//头像ID
        private byte m_nVIPLevel;	//VIP等级
        private byte m_nLevel;		//神庙等级
        private int m_nRank;		//排名
        private byte m_NameLength;  //名字长度
        private string m_MemberName;//名字
        private long m_LoginTime;	//登录时间
        private long m_QuitTime;	//退出时间
        private int m_Comat;		//战斗力
        private int m_TeamCombat;   //队伍战斗力
        private int m_Camp;			//阵营
        private int m_CycleCharge;	//周期充值(月/周)
        private X_GUID m_GuildGuid = new X_GUID(); //公会GUID
        public RelationMemberData()
        {
            CleanUp();
        }

        /// <summary>
        /// 清理操作
        /// </summary>
        public void CleanUp()
        {
            m_MemberGUID.CleanUp();
            m_IconID = -1;
            m_nVIPLevel = 0;
            m_nLevel = 0;
            m_nRank = 0;
            m_LoginTime = 0;
            m_QuitTime = 0;
            m_Comat = 0;
            m_TeamCombat = 0;
            m_Camp = 0;
            m_CycleCharge = 0;
            m_GuildGuid.CleanUp();
        }

        /// <summary>
        /// 获取好友成员的GUID
        /// </summary>
        public X_GUID MemberGUID { get { return m_MemberGUID; } }

        /// <summary>
        /// 获取好友成员的名字
        /// </summary>
        public string MemberName { get { return m_MemberName; } }

        /// <summary>
        /// 数据拷贝操作
        /// </summary>
        /// <param name="memberdata">数据源</param>
        public void Copy(RelationMemberData memberdata)
        {
            this.m_MemberGUID.Copy(memberdata.m_MemberGUID);
            this.m_IconID = memberdata.m_IconID;
            this.m_nVIPLevel = memberdata.m_nVIPLevel;
            this.m_nLevel = memberdata.m_nLevel;
            this.m_nRank = memberdata.m_nRank;
            this.m_NameLength = memberdata.m_NameLength;
            this.m_MemberName = memberdata.m_MemberName;
            this.m_LoginTime = memberdata.m_LoginTime;
            this.m_QuitTime = memberdata.m_QuitTime;
            this.m_Comat = memberdata.m_Comat;
            this.m_TeamCombat = memberdata.m_TeamCombat;
            this.m_Camp = memberdata.m_Camp;
            this.m_CycleCharge = memberdata.m_CycleCharge;
            this.m_GuildGuid.Copy(memberdata.m_GuildGuid);
        }
    }

    /// <summary>
    /// 好友申请列表数据信息！
    /// </summary>
    public class ApplyMemberData
    {
        private long m_ApplyTime;		//申请时间
        private RelationMemberData m_MemberData = new RelationMemberData(); //申请列表，申请的玩家都在这里
        /// <summary>
        /// 构造函数，清理操作
        /// </summary>
        public ApplyMemberData()
        {
            CleanUp();
        }
        /// <summary>
        /// 清理操作
        /// </summary>
        public void CleanUp()
        {
            m_ApplyTime = 0;
            m_MemberData.CleanUp();
        }
        /// <summary>
        /// 获取申请人 申请的时间
        /// </summary>
        public long ApplyTime { get { return m_ApplyTime; } set { m_ApplyTime = value; } }

        /// <summary>
        /// 获取申请列表成员数据
        /// </summary>
        public RelationMemberData MemberData { get { return m_MemberData; } }



        /// <summary>
        /// 数据拷贝操作
        /// </summary>
        /// <param name="memberdata">数据源</param>
        public void Copy(ApplyMemberData memberdata)
        {
            this.m_ApplyTime = memberdata.m_ApplyTime;
            this.m_MemberData.Copy(memberdata.m_MemberData);
        }
    }
}