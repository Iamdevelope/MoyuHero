using DreamFaction.GameNetWork;
using System.Collections.Generic;
namespace DreamFaction.GameNetWork.Data
{
    /// <summary>
    /// 战斗场景的掉落数据，包括 个数,道具类型，道具ID，数量，等级等！
    /// </summary>
    public class DropBoxData
    {
        private byte m_Count;                                                   //掉落物品的数量
        private byte[] m_Type = new byte[GlobalMembers.MAX_ITEM_LIST_COUNT];	//类型
        private int[] m_ID = new int[GlobalMembers.MAX_ITEM_LIST_COUNT];		//道具id
        private int[] m_DropNum = new int[GlobalMembers.MAX_ITEM_LIST_COUNT];	//数量
        private byte[] m_Level = new byte[GlobalMembers.MAX_ITEM_LIST_COUNT];	//等级

        public int m_HumanExp; //主角
        public int m_TeamExp;
        public int m_DropGold;
        private List<int> m_indroplist;
        public List<int> indroplist { 
            get {
                if (m_indroplist == null)
                    return new List<int>();
                else
                    return m_indroplist;
            }
            set
            {
                m_indroplist = value;
            }
        
        }// 掉落小包组
        /// <summary>
        /// 构造函数，执行清理操作
        /// </summary>
        public DropBoxData()
        {
            CleanUp();
        }
        /// <summary>
        /// 清理操作
        /// </summary>
        public void CleanUp()
        {
            m_Count = 0;
            for (int i = 0; i < GlobalMembers.MAX_ITEM_LIST_COUNT; i++)
            {
                m_Type[i]       = 0;
                m_ID[i]         = 0;
                m_DropNum[i]    = 0;
                m_Level[i]      = 0;
            }
            m_HumanExp = 0;
            m_TeamExp = 0;
            m_DropGold = 0;
        }
      
        /// <summary>
        /// 拷贝操作
        /// </summary>
        /// <param name="dropbox">数据源</param>
        public void Copy(DropBoxData dropbox)
        {
            this.m_Count = dropbox.m_Count;
            for (int i = 0; i < this.m_Count; ++i)
            {
                this.m_Type[i] = dropbox.m_Type[i];
                this.m_ID[i] = dropbox.m_ID[i];
                this.m_DropNum[i] = dropbox.m_DropNum[i];
                this.m_Level[i] = dropbox.m_Level[i];
            }
        }
    }
}