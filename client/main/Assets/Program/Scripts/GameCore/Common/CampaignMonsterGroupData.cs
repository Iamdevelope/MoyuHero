using DreamFaction.GameNetWork;
using DreamFaction.LogSystem;

namespace DreamFaction.GameNetWork.Data
{
    /// <summary>
    /// 战斗场景怪物组数据，包括了波数，每波具体的怪物数量以及怪物ID！<br/>
    /// </summary>
    public class CampaignMonsterGroupData
    {
        /// <summary>
        /// 怪物波数
        /// </summary>
        private byte m_Count;
        /// <summary>
        /// 怪物波数数组 ，上限 GlobalMembers.MAX_CAMPAIGN_MONSTER_GROUP = 10 组怪物
        /// </summary>
        private byte[] m_MonsterCount = new byte[GlobalMembers.MAX_CAMPAIGN_MONSTER_GROUP];
        /// <summary>
        /// 每波具体的怪物ID数组 上限 GlobalMembers.MAX_MATRIX_CELL_COUNT = 6 个怪物
        /// </summary>
        private int[,] m_ID = new int[GlobalMembers.MAX_CAMPAIGN_MONSTER_GROUP, GlobalMembers.MAX_MONSTER_GROUP_COUNT];

        private X_GUID[,] m_GUID = new X_GUID[GlobalMembers.MAX_CAMPAIGN_MONSTER_GROUP, GlobalMembers.MAX_MONSTER_GROUP_COUNT];
        /// <summary>
        /// 构造函数，执行清理操作 CleanUp操作
        /// </summary>
        public CampaignMonsterGroupData()
        {
            CleanUp();
        }
        /// <summary>
        /// 怪物组数量（波数）
        /// </summary>
        public byte Count { get { return m_Count; } set { m_Count = value; } }

        /// <summary>
        /// 每波所有怪物的ID数组，包含了每波每个怪物的ID！int[A,B]
        /// </summary>
        public int[,] IDs { get { return m_ID; } set { m_ID = value; } }

        public X_GUID[,] GUIDs { get { return m_GUID; } }
        /// <summary>
        /// 清理操作，重置怪物组数据！
        /// </summary>
        public void CleanUp()
        {
            m_Count = 0;
            for (int i = 0; i < GlobalMembers.MAX_CAMPAIGN_MONSTER_GROUP; i++)
            {
                m_MonsterCount[i] = 0;
                for (int j = 0; j < GlobalMembers.MAX_MONSTER_GROUP_COUNT; ++j)
                {
                    m_ID[i, j] = 0;
                    if (m_GUID[i,j] == null)
                    {
                        m_GUID[i, j] = new X_GUID();
                    }
                }
            }
        }


        /// <summary>
        /// 数据拷贝操作
        /// </summary>
        /// <param name="data">拷贝的数据源</param>
        public void Copy(CampaignMonsterGroupData data)
        {
            this.m_Count = data.m_Count;
            for (int i = 0; i < this.m_Count; ++i)
            {
                this.m_MonsterCount[i] = data.m_MonsterCount[i];
                for (int j = 0; j < GlobalMembers.MAX_MONSTER_GROUP_COUNT; ++j)
                {
                    this.m_ID[i, j] = data.m_ID[i, j];
                }
            }
        }

        public void SetGUID(X_GUID guid,int nGroup,int nIndex)
        {
            if (nGroup >= GlobalMembers.MAX_CAMPAIGN_MONSTER_GROUP || nGroup < 0)
                return;

            if (nIndex >= GlobalMembers.MAX_MONSTER_GROUP_COUNT || nIndex < 0)
                return;

            m_GUID[nGroup, nIndex].Copy(guid);
        }
        public byte FindMonsterTeamPosByGUID(X_GUID guid)
        {
            for (int i = 0; i < GlobalMembers.MAX_CAMPAIGN_MONSTER_GROUP; i++)
            {
                for (int j = 0; j < GlobalMembers.MAX_MONSTER_GROUP_COUNT; ++j)
                {
                    if (m_GUID[i,j].Equals(guid))
                    {
                        return (byte)j;
                    }
                }
            }
            return byte.MaxValue;
        }
    }
}