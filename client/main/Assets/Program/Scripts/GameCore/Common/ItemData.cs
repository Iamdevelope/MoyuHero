using DreamFaction.GameNetWork;

namespace DreamFaction.GameNetWork.Data
{
    /// <summary>
    /// 物品属性，包括属性类型，属性值！
    /// </summary>
    public struct __ITEM_ATTRIBUTE
    {
        /// <summary>
        /// 属性类型
        /// </summary>
        public byte m_AttrType;
        /// <summary>
        /// 属性值
        /// </summary>
        public int m_Value;

        /// <summary>
        /// 清理操作
        /// </summary>
        public void CleanUp()
        {
            m_AttrType = byte.MaxValue;
            m_Value = 0;
        }
    }

    /// <summary>
    /// 物品数据是一个聚合体，包括了不同的物品数据,这个枚举是刷新标记，用来控制刷新什么数据！
    /// </summary>
    public enum ITEM_ATTRIBUTE_FLAG : int
    {
        /// <summary>
        /// 绑定SPECIALITEM
        /// </summary>
        ITEM_DATA_BIND = 1,					
        /// <summary>
        /// 物品公共属性数量
        /// </summary>
        ITEM_DATA_COMMON_DATA_COUNT = 2,	
	    /// <summary>
        /// 物品公共属性数量
	    /// </summary>
        COMMONITEM_DATA_COUNT = ITEM_DATA_COMMON_DATA_COUNT,//2
        /// <summary>
        /// 符文
        /// </summary>
        RUNE_DATA_RUNE = ITEM_DATA_COMMON_DATA_COUNT,//2
        /// <summary>
        /// 符文等级
        /// </summary>
        RUNE_DATA_LEVEL = RUNE_DATA_RUNE + 1,//3
        /// <summary>
        /// 符文鉴定属性
        /// </summary>
        RUNE_DATA_ACTIVEATTRIBUTECOUNT = RUNE_DATA_LEVEL + 1,//4
        /// <summary>
        /// 符文属性
        /// </summary>
        RUNE_DATA_ATTRIBUTE = RUNE_DATA_ACTIVEATTRIBUTECOUNT + 1,//5
        /// <summary>
        /// 碎片数量
        /// </summary>
        FRAGMENT_DATA_COUNT = ITEM_DATA_COMMON_DATA_COUNT,
        /// <summary>
        /// 特殊物品数量
        /// </summary>
        SPECIALITEM_DATA_COUNT = ITEM_DATA_COMMON_DATA_COUNT,
    }

    /// <summary>
    /// 物品数据，包括是否绑定，GUID，等级，数量等等！
    /// </summary>
    public class ItemData
    {
        //第一部分---------公共数据-------------------------------------------------------
        /// <summary>
        /// //物品GUID
        /// </summary>
        private X_GUID m_GUID = new X_GUID();	
        /// <summary>
        /// //物品编号
        /// </summary>
        private int m_ItemTableID;				
        /// <summary>
        ///  //是否绑定
        /// </summary>
        public byte bBind;					   

        //普通道具数据-------------------------------------------------------------------------
        /// <summary>
        /// //当前数量
        /// </summary>
        public int nCount;	                    

        //装备符文
        /// <summary>
        ///  //等级
        /// </summary>
        public  byte level;                    
        /// <summary>
        /// 属性数量
        /// </summary>
        public  byte attributeCount;
        /// <summary>
        /// 激活的属性数量
        /// </summary>
        public byte m_ActiveAttributeCount;                 
        /// <summary>
        /// //装备属性数组！
        /// </summary>
        public __ITEM_ATTRIBUTE[] attribute = new __ITEM_ATTRIBUTE[GlobalMembers.MAX_RUNE_APPEND_ATTRIBUTE_COUNT];	

        /// <summary>
        /// 构造函数，执行清理操作
        /// </summary>
        public ItemData()
        {
            CleanUp();
        }
        /// <summary>
        /// 清理操作
        /// </summary>
        public void CleanUp()
        {
            m_ItemTableID = int.MaxValue;
            bBind = 0;
            m_GUID.CleanUp();
            nCount = 0;
            level = 0;
            attributeCount = 0;
            for (int i = 0; i < GlobalMembers.MAX_RUNE_APPEND_ATTRIBUTE_COUNT; ++i)
            {
                attribute[i].CleanUp();
            }
        }

        /// <summary>
        /// 获取物品在数据表中的ID编号
        /// </summary>
        public int ItemTableID { get { return m_ItemTableID; } set { m_ItemTableID = value; } }

        /// <summary>
        /// 获取物品的GUID
        /// </summary>
        public X_GUID GUID { get { return m_GUID; } }

        /// <summary>
        /// 获取物品标记，用来确定如何更新物品数据
        /// </summary>
        /// <returns></returns>
        private int GetItemClass()
        {
            return m_ItemTableID / 1000000;
        }

        /// <summary>
        /// 获取物品标记，用来确定如何更新物品数据
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        public static int GetItemClass(int nItemTableID) 
        {
            return nItemTableID / 1000000;
        }

        /// <summary>
        /// 数据拷贝
        /// </summary>
        /// <param name="itemdata">数据源</param>
        public void Copy(ItemData itemdata)
        {
            this.m_GUID.Copy(itemdata.m_GUID);
            this.bBind = itemdata.bBind;
            this.m_ItemTableID = itemdata.m_ItemTableID;
            switch (GetItemClass())
            {
                case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE:
                    {
                        this.level = itemdata.level;
                        this.attributeCount = itemdata.attributeCount;
                        for (int i = 0; i < attributeCount; ++i)
                        {
                            this.attribute[i].m_AttrType = itemdata.attribute[i].m_AttrType;
                            this.attribute[i].m_Value = itemdata.attribute[i].m_Value;
                        }
                    }
                    break;
                case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON:
                    {
                        this.nCount = itemdata.nCount;
                    }
                    break;
            }
        }
    }
}