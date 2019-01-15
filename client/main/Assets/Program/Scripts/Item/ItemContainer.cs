/********************************************************************
	created:	2015/04/16
	created:	16:4:2015   13:51
	filename: 	d:\dream heros\trunk\assets\program\scripts\item\itemcontainer.cs
	file path:	d:\dream heros\trunk\assets\program\scripts\item
	file base:	itemcontainer
	file ext:	cs
	author:		Zhao Mingyang
	
	purpose:	存储背包物品数据唯一list 内包括符文，消耗品，礼包，材料
 *              
 *              PS：注意！提供按类型排序规则的算法，直接返回的是新的list，不改变原有数据顺序。按需调用
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using DreamFaction.GameNetWork;
using DreamFaction.LogSystem;
using GNET;

/// <summary>
/// 道具类物品排序规则
/// </summary>
public class ItemComparer : IComparer<BaseItem>
{
    public int Compare(BaseItem left, BaseItem right)
    {
        //优先品质排序从高到低 [4/16/2015 Zmy]
        if (left.GetItemRowData().getQuality() < right.GetItemRowData().getQuality())
            return 1;
        else if (left.GetItemRowData().getQuality() == right.GetItemRowData().getQuality())
        {
            //同品质按ID 从低到高
            if (left.GetItemTableID() > right.GetItemTableID())
            {
                return 1;
            }
            else if (left.GetItemTableID() == right.GetItemTableID())
            {
                if (left.GetItemCount() < right.GetItemCount())
                    return 1;
                else if (left.GetItemCount() == right.GetItemCount())
                    return 0;
                else
                    return -1;
            }
            else
            {
                return -1;
            }
        }
        else
            return -1;
    }
}

public class RuneComparer : IComparer<BaseItem>
{
    public int Compare(BaseItem left, BaseItem right)
    {
        //符文排序 优先品质（由高到低）->符文等级(暂无)->符文类型->符文ID(由低到高) [4/17/2015 Zmy]
        if (left.GetItemRowData().getRune_quality() < right.GetItemRowData().getRune_quality())
            return 1;
        else if (left.GetItemRowData().getRune_quality() == right.GetItemRowData().getRune_quality())
        {
            if (left.GetStrenghLevel() < right.GetStrenghLevel())
                return 1;
            else if (left.GetStrenghLevel() == right.GetStrenghLevel())
            {
                if (left.GetItemRowData().getRune_type() < right.GetItemRowData().getRune_type())
                    return 1;
                else if (left.GetItemRowData().getRune_type() == right.GetItemRowData().getRune_type())
                {
                    if (left.GetItemTableID() > right.GetItemTableID())
                        return 1;
                    else if (left.GetItemTableID() == right.GetItemTableID())
                        return 0;
                    else
                        return -1;
                }
                else
                    return -1;
            }
            else
                return -1;
        }
        else
            return -1;
    }
}
public class ItemContainer
{
    private List<int> m_BagTypeList = new List<int>();//背包类型 
    private Dictionary<int, List<BaseItem>> m_ItemMapList = new Dictionary<int, List<BaseItem>>();

    public List<X_GUID> m_NewGetItems = new List <X_GUID>();//新获得的物品  key 为物品X_GUID
    public ItemContainer()
    {
        ClearUp();
    }
    public void ClearUp()
    {
        m_BagTypeList.Clear();
        m_NewGetItems.Clear();
        m_BagTypeList.Add((int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON);
        m_BagTypeList.Add((int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP);

        m_ItemMapList.Clear();
        List<BaseItem> _Commonlist = new List<BaseItem>();
        _Commonlist.Clear();
        m_ItemMapList.Add((int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, _Commonlist);
        List<BaseItem> _Equiplist = new List<BaseItem>();
        _Equiplist.Clear();
        m_ItemMapList.Add((int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP, _Equiplist);

    }
    //返回背包容器的最大上限
    public int GetBagItemSizeMax()
    {
        int nInitialSize = DataTemplate.GetInstance().m_GameConfig.getInitial_common_item_packset();//初始上限
        int nCurSizeSum = DataTemplate.GetInstance().m_GameConfig.getCommon_item_packset_per_expand() * ObjectSelf.GetInstance().BagBuyCount;//已购买增加的上限

        int nCurLevel = ObjectSelf.GetInstance().Level;
        PlayerTemplate _row = (PlayerTemplate)DataTemplate.GetInstance().m_PlayerExpTable.getTableData(nCurLevel);
        int nLeveladdition = _row.getExtraCommonItemPackset();//等级加成

        return nInitialSize + nCurSizeSum + nLeveladdition; 
    }

    public List<BaseItem> GetItemList(EM_BAG_HASHTABLE_TYPE  bagType)
    {
        if (m_ItemMapList.ContainsKey((int)bagType) == false)
        {
            LogManager.LogError(false);
            LogManager.LogToFile("not has bagType Key of GetItemList()");
            return null;
        }

        return m_ItemMapList[(int)bagType];
    }

    //返回当前背包内所有类型的物品总数 [4/28/2015 Zmy]
    public int GetBagItemSum()
    {
        int nSUM = 0;
        for (int i = 0; i < m_BagTypeList.Count; ++i )
        {
            nSUM += m_ItemMapList[m_BagTypeList[i]].Count;
        }

        return nSUM;
    }
    public void InitItemList(int bagType,List<Item> _list)
    {
        for (int i = 0; i < _list.Count; ++i )
        {
            AddItem(bagType,_list[i]);
        }
    }

    public void AddItem(int bagType, Item _item)
    {
        BaseItem item = CreateItem(_item.id);
        if (item != null)
        {
            item.SetItemTableID(_item.id);
            item.SetItemGuid(_item.key);
            item.SetItemCount(_item.number);
            item.SetItemTimesCount(_item.timer);
            if (m_ItemMapList.ContainsKey(bagType))
            {
                m_ItemMapList[bagType].Add(item);
                //LogManager.Log("New Item Add:" + _item.id);
                if (item.GetItemType() == (int)EM_ITEM_TYPE.EM_ITEM_TYPE_RUNE)
                {
                    ItemEquip equip = item as ItemEquip;
                    equip.ResetEquipState();
                    EquipItemData Itemdata = new EquipItemData();
                    Itemdata.unmarshal(OctetsStream.wrap(_item.extdata));
                    equip.InitRuneData(Itemdata);
                }
            }
           
            else
            {
                LogManager.LogError(false);
                LogManager.LogToFile("Error:Try AddItem,But Nonexistent BagType Key" + bagType);
            }
          
        }
    }

    public void RefreshItem(int bagType, Item _item)
    {
        X_GUID _tempData = new X_GUID();
        _tempData.GUID_value = _item.key;
        BaseItem pData = FindItem(bagType,_tempData);
        if (pData != null)
        {
            pData.SetItemTableID(_item.id);
            pData.SetItemCount(_item.number);
            /*pData.SetItemTimesCount(_item.timer);*/
            RefreshItemTimesCount(bagType,_item.id, _item.timer);
            if (pData.GetItemType() == (int)EM_ITEM_TYPE.EM_ITEM_TYPE_RUNE)
            {
                ItemEquip equip = pData as ItemEquip;
                EquipItemData Itemdata = new EquipItemData();
                Itemdata.unmarshal(OctetsStream.wrap(_item.extdata));
                equip.InitRuneData(Itemdata);
            }
        }
        else
        {
            AddItem(bagType,_item);
            m_NewGetItems.Add(_tempData);
        }
        _tempData = null;
    }

    public void RefreshItemTimesCount(int bagType,int nTableID,short nCount)
    {
        if (m_ItemMapList.ContainsKey(bagType) == false)
        {
            LogManager.LogError(false);
            LogManager.LogToFile("not has bagType Key of RefreshItemTimesCount()");
            return;
        }

        for (int i = 0; i < m_ItemMapList[bagType].Count; ++i)
        {
            if (m_ItemMapList[bagType][i].GetItemTableID() == nTableID)
            {
                m_ItemMapList[bagType][i].SetItemTimesCount(nCount);
            }
        }
    }

    /// <summary>
    /// 获得物品的使用次数，如果找不到此物品，返回-1;
    /// 
    /// 现在客户端写的有问题吧;
    /// </summary>
    /// <param name="type"></param>
    /// <param name="nTableId"></param>
    /// <returns></returns>
    public int GetItemUseTimes(EM_BAG_HASHTABLE_TYPE type, int nTableId)
    {
        BaseItem bi = FindItem(type, nTableId);

        if (bi == null)
        {
            return -1;
        }

        return bi.GetItemTimesCount();
    }


    /// <summary>
    /// 增加返回值，获得物品数量前后的差值;
    /// </summary>
    /// <param name="bagType"></param>
    /// <param name="_key"></param>
    /// <param name="_count"></param>
    /// <returns></returns>
    public int RepairItemCount(int bagType,int _key,int _count)
    {
        int delta = 0;
        X_GUID _tempData = new X_GUID();
        _tempData.GUID_value = _key;
        BaseItem pData = FindItem(bagType,_tempData);
        if ( pData != null)
        {
            delta = _count - pData.GetItemCount();
            pData.SetItemCount(_count);
        }
        _tempData = null;

        return delta;
    }

    public BaseItem FindItem(int bagType, X_GUID guid)
    {
        if (m_ItemMapList.ContainsKey(bagType) == false)
        {
            LogManager.LogToFile("not has bagType Key of FindItem()");
            return null;
        }
        for (int i = 0; i < m_ItemMapList[bagType].Count; ++i)
        {
            if (m_ItemMapList[bagType][i].GetItemGuid().Equals(guid))
            {
                return m_ItemMapList[bagType][i];
            }
        }
        return null;
    }

    public BaseItem FindItem(EM_BAG_HASHTABLE_TYPE type, X_GUID guid)
    {
        return FindItem((int)type, guid);
    }

    public BaseItem FindItem(EM_BAG_HASHTABLE_TYPE type, int nTableId)
    {
        int bagType = (int)type;
        if (m_ItemMapList.ContainsKey(bagType) == false)
        {
            LogManager.LogToFile("not has bagType Key of FindItem()");
            return null;
        }

        for (int i = 0; i < m_ItemMapList[bagType].Count; ++i)
        {
            if (m_ItemMapList[bagType][i].GetItemTableID().Equals(nTableId))
            {
                return m_ItemMapList[bagType][i];
            }
        }
        return null;
    }

    /// <summary>
    /// 删除物品，通过GUID
    /// </summary>
    /// <param name="guid">物品GUID</param>
    public void EreaseItem(int bagType,X_GUID guid)
    {
        if (m_ItemMapList.ContainsKey(bagType) == false)
        {
            LogManager.LogToFile("not has bagType Key of EreaseItem()");
            return;
        }
        for (int i = 0; i < m_ItemMapList[bagType].Count; ++i)
        {
            if (m_ItemMapList[bagType][i].GetItemGuid().Equals(guid))
            {
                m_ItemMapList[bagType].RemoveAt(i);
                break;
            }
        }
    }
    
    public BaseItem CreateItem(int nTableID)
    {
        // 道具的ID区间 [4/10/2015 Zmy]
        if (nTableID < ((int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE * 1000000) || nTableID >= ((int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO * 1000000))
        {
            LogManager.LogAssert("!!!!Error CreateItem TableID is item out range");
            return null;
        }
        int nClass = nTableID / 1000000;
        if (nClass == (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON)//道具区间
        {
            ItemTemplate pData = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(nTableID);
            switch (pData.getType())
            {
            case (int)EM_ITEM_TYPE.EM_ITEM_TYPE_MATERIAL:
            	return new ItemMaterial();
            case (int)EM_ITEM_TYPE.EM_ITEM_TYPE_GIFT:
                return new ItemGift();
            case (int)EM_ITEM_TYPE.EM_ITEM_TYPE_CONSUME:
                return new ItemConsume();
            case (int)EM_ITEM_TYPE.EM_ITEM_TYPE_RUNE:
                return new ItemEquip();
                case (int)EM_ITEM_TYPE.EM_ITEM_TYPE_FRAGMENT:
                return new ItemFragment();
            default:
                return null;
            }
        }
        else
        {
            //符文区间
            return new ItemEquip();
        }
    }

    // 返回背包内指定类型的道具 [4/17/2015 Zmy]
    public List<BaseItem> ReturnItemType(int bagType,EM_ITEM_TYPE nType)
    {
        List<BaseItem> _list = new List<BaseItem>();
        _list.Clear();

        if (m_ItemMapList.ContainsKey(bagType) == false)
        {
            LogManager.LogError("not has bagType Key of ReturnItemType()");
            return _list;
        }

        for (int i = 0; i < m_ItemMapList[bagType].Count; ++i )
        {
            switch (nType)
            {
                case EM_ITEM_TYPE.EM_ITEM_TYPE_MATERIAL:
                    if (m_ItemMapList[bagType][i].GetItemType() == (int)EM_ITEM_TYPE.EM_ITEM_TYPE_MATERIAL)
                    {
                        _list.Add(m_ItemMapList[bagType][i]);
                    }
                    break;
                case EM_ITEM_TYPE.EM_ITEM_TYPE_GIFT:
                    if (m_ItemMapList[bagType][i].GetItemType() == (int)EM_ITEM_TYPE.EM_ITEM_TYPE_GIFT)
                    {
                        _list.Add(m_ItemMapList[bagType][i]);
                    }
                    break;
                case EM_ITEM_TYPE.EM_ITEM_TYPE_CONSUME:
                    if (m_ItemMapList[bagType][i].GetItemType() == (int)EM_ITEM_TYPE.EM_ITEM_TYPE_CONSUME)
                    {
                        _list.Add(m_ItemMapList[bagType][i]);
                    }
                    break;
                case EM_ITEM_TYPE.EM_ITEM_TYPE_RUNE:
                    if (m_ItemMapList[bagType][i].GetItemType() == (int)EM_ITEM_TYPE.EM_ITEM_TYPE_RUNE)
                    {
                        _list.Add(m_ItemMapList[bagType][i]);
                    }
                    break;
                case EM_ITEM_TYPE.EM_ITEM_TYPE_FRAGMENT:
                    if (m_ItemMapList[bagType][i].GetItemType() == (int)EM_ITEM_TYPE.EM_ITEM_TYPE_FRAGMENT)
                    {
                        _list.Add(m_ItemMapList[bagType][i]);
                    }
                    break;
            }
        }

        return _list;
    }

    // 返回道具背包里指定类型的符文 [4/17/2015 Zmy]
    public List<BaseItem> ReturnRuneType(EM_SORT_RUNE_ITEM nType)
    {
        List<BaseItem> _list = ReturnItemType((int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP, EM_ITEM_TYPE.EM_ITEM_TYPE_RUNE);

        for (int i = _list.Count - 1; i >= 0; i--)
        {
            switch (nType)
            {
                case EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_SPECIAL:
                    if (_list[i].GetItemRowData().getRune_type() < (int)EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_SPECIAL)
                    {
                        _list.RemoveAt(i);
                    }
                    break;
                case EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_RED:
                    if (_list[i].GetItemRowData().getRune_type() != (int)EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_RED)
                    {
                        _list.RemoveAt(i);
                    }
                    break;
                case EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_GREEN:
                    if (_list[i].GetItemRowData().getRune_type() != (int)EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_GREEN)
                    {
                        _list.RemoveAt(i);
                    }
                    break;
                case EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_PURPLE:
                    if (_list[i].GetItemRowData().getRune_type() != (int)EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_PURPLE)
                    {
                        _list.RemoveAt(i);
                    }
                    break;
                case EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_BLUE:
                    if (_list[i].GetItemRowData().getRune_type() != (int)EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_BLUE)
                    {
                        _list.RemoveAt(i);
                    }
                    break;
                default:
                    break;
            }
        }

        return _list;
    }

    /// <summary>
    /// 对道具类物品进行筛选排序。
    /// </summary>
    /// <param name="nType">排序类型</param>
    /// <returns>返回排序后的列表</returns>
    public List<BaseItem> SortCommonItemByType(EM_SORT_COMMON_ITEM nType)
    {
        List<BaseItem> _list = new List<BaseItem>();
        _list.Clear();

        switch (nType)
        {
            case EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_ALL:
                {
                    List<BaseItem> _consumeList = ReturnItemType((int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, EM_ITEM_TYPE.EM_ITEM_TYPE_CONSUME);
                    _consumeList.Sort(new ItemComparer());

                    List<BaseItem> _giftList = ReturnItemType((int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON,EM_ITEM_TYPE.EM_ITEM_TYPE_GIFT);
                    _giftList.Sort(new ItemComparer());

                    List<BaseItem> _materialList = ReturnItemType((int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON,EM_ITEM_TYPE.EM_ITEM_TYPE_MATERIAL);
                    _materialList.Sort(new ItemComparer());

                    List<BaseItem> _fragmentlList = ReturnItemType((int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, EM_ITEM_TYPE.EM_ITEM_TYPE_FRAGMENT);
                    _fragmentlList.Sort(new ItemComparer());

                    for (int i = 0; i < _consumeList.Count; ++i)
                    {
                        _list.Add(_consumeList[i]);
                    }

                    for (int i = 0; i < _giftList.Count; ++i)
                    {
                        _list.Add(_giftList[i]);
                    }

                    for (int i = 0; i < _materialList.Count; ++i)
                    {
                        _list.Add(_materialList[i]);
                    }
                    for (int i = 0; i < _fragmentlList.Count; ++i)
                    {
                        _list.Add(_fragmentlList[i]);
                    }
                }
                break;
            case EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_CONSUME:
                {
                    List<BaseItem> _consumeList = ReturnItemType((int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON,EM_ITEM_TYPE.EM_ITEM_TYPE_CONSUME);
                    _consumeList.Sort(new ItemComparer());

                    for (int i = 0; i < _consumeList.Count; ++i)
                    {
                        _list.Add(_consumeList[i]);
                    }
                }
                break;
            case EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_GIFT:
                {
                    List<BaseItem> _giftList = ReturnItemType((int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON,EM_ITEM_TYPE.EM_ITEM_TYPE_GIFT);
                    _giftList.Sort(new ItemComparer());

                    for (int i = 0; i < _giftList.Count; ++i)
                    {
                        _list.Add(_giftList[i]);
                    }
                }
                break;
            case EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_MATERIAL:
                {
                    List<BaseItem> _materialList = ReturnItemType((int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON,EM_ITEM_TYPE.EM_ITEM_TYPE_MATERIAL);
                    _materialList.Sort(new ItemComparer());

                    for (int i = 0; i < _materialList.Count; ++i)
                    {
                        _list.Add(_materialList[i]);
                    }
                }
                break;
            case EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_FRAGMENT:
                    List<BaseItem> _fragmentList = ReturnItemType((int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON,EM_ITEM_TYPE.EM_ITEM_TYPE_FRAGMENT);
                    _fragmentList.Sort(new ItemComparer());

                    for (int i = 0; i < _fragmentList.Count; ++i)
                    {
                        _list.Add(_fragmentList[i]);
                    }
                break;
        }
        return _list;
    }

    // 筛选对应符文是否装备的条件进行排序并输出。 [4/17/2015 Zmy]
    private void SortEquipRune(EM_SORT_RUNE_ITEM ntype,ref List<BaseItem> _Returnlist)
    {
        if (ntype == (int)EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_ALL)
        {
            return;
        }
        // 先筛选出已装备的符文进行排序。并剔除掉 [4/17/2015 Zmy]
        List<BaseItem> _Rune = ReturnRuneType(ntype);
        for (int i = _Rune.Count - 1; i >= 0; i--)
        {
            if (_Rune[i].IsEquip())
            {
                _Returnlist.Add(_Rune[i]);
                _Rune.RemoveAt(i);
            }
        }
        _Returnlist.Sort(new RuneComparer());
        // 在对剩余未装备的符文进行排序并添加输出list [4/17/2015 Zmy]
        _Rune.Sort(new RuneComparer());
        for (int i = 0; i < _Rune.Count; ++i)
        {
            _Returnlist.Add(_Rune[i]);
        }

    }

    //对符文进行排序 [4/17/2015 Zmy]
    public List<BaseItem> SortRuneItemByType(EM_SORT_RUNE_ITEM nType)
    {
        List<BaseItem> _list = new List<BaseItem>();
        _list.Clear();

        switch (nType)
        {
            case EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_ALL:
                List<BaseItem> _allRune = ReturnItemType((int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP, EM_ITEM_TYPE.EM_ITEM_TYPE_RUNE);
                //先筛选出已装备的,并从列表中剔除出去（注意要不改变原有的数据内存） [4/17/2015 Zmy]
                for (int i = _allRune.Count - 1; i >= 0; i--)
                {
                    if (_allRune[i].IsEquip())
                    {
                        _list.Add(_allRune[i]);
                        _allRune.RemoveAt(i);
                    }
                }
                _list.Sort(new RuneComparer());

                //未装备的按品质 等级 类型排序一次 [4/17/2015 Zmy]
                _allRune.Sort(new RuneComparer());
                for (int i = 0; i < _allRune.Count; ++i)
                {
                    _list.Add(_allRune[i]);
                }
                break;
            case EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_SPECIAL:
                SortEquipRune(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_SPECIAL, ref _list);
                break;
            case EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_RED:
                SortEquipRune(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_RED, ref _list);
                break;
            case EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_GREEN:
                SortEquipRune(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_GREEN, ref _list);
                break;
            case EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_PURPLE:
                SortEquipRune(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_PURPLE, ref _list);
                break;
            case EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_BLUE:
                SortEquipRune(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_BLUE, ref _list);
                break;
            case EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_NEWGUIDE:
                SortEquipRune(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_NEWGUIDE, ref _list);
                break;
            default:
                break;
        }

        return _list;
    }

    //更新符文装备状态 [5/21/2015 Zmy]
    public void OnUpdateRuneEquipState(X_GUID _equipped, bool _state)
    {
        if (_equipped.IsValid())
        {
            ItemEquip _item = (ItemEquip)FindItem((int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP, _equipped);
            if (_item != null)
            {
                _item.SetEquipState(_state);
            }
        }
    }

    /// <summary>
    /// 根据资源id获取人物身上物品数量,
    /// 材料
    /// 消耗品;
    /// </summary>
    /// <returns></returns>
    public int GetItemCountById(EM_BAG_HASHTABLE_TYPE bagType, int id)
    {
        List<BaseItem> items = new List<BaseItem>();
        int result = 0;
        switch(bagType)
        {
            case EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON:
                items = m_ItemMapList[(int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON];
                if (items == null || items.Count == 0)
                    return 0;
                foreach(BaseItem bi in items)
                {
                    if (bi == null) continue;

                    if (bi.GetItemTableID() == id)
                        result++;
                }
                return result;
            case EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP:
                items = m_ItemMapList[(int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP];
                if (items == null || items.Count == 0)
                    return 0;
                foreach(BaseItem bi in items)
                {
                    if (bi == null) continue;

                    if (bi.GetItemTableID() == id)
                        result++;
                }
                return result;
        }

        return -1;
    }
    #region 物品获得时tip提示接口
    /// <summary>
    /// 是否是新获得物品
    /// </summary>
    /// <param name="guid">物品guid</param>
    public bool IsNewGetItem(X_GUID guid)
    {
        bool result = false;
        foreach (X_GUID item in m_NewGetItems)
        {
            if (guid.Equals(item))
            {
                result = true;
                break;
            }
        }
        return result;
    }
    /// <summary>
    /// 设置某种类型物品为已读状态
    /// </summary>
    /// <param name="type">物品类型</param>
    public void SetNewItemPreviw(EM_BAG_HASHTABLE_TYPE type)
    {
        List<X_GUID> delGUIDs = new List<X_GUID>();
        foreach (X_GUID item in m_NewGetItems)
        {
            if (FindItem(type, item) != null)
            {
                delGUIDs.Add(item);
            }
        }
        foreach (X_GUID item in delGUIDs)
        {
            m_NewGetItems.Remove(item);
        }
    }
    /// <summary>
    /// 设置某个物品为已读状态
    /// </summary>
    /// <param name="guid"></param>
    public void SetItemPreview(X_GUID guid)
    {
        for (int i = 0; i < m_NewGetItems.Count; i++)
        {
            if (m_NewGetItems[i].Equals(guid))
                m_NewGetItems.RemoveAt(i);
        }

    }
    #endregion
    /// <summary>
    /// 获取某个英雄的碎片拥有的碎片数量
    /// </summary>
    /// <param name="heroTableId">英雄表id</param>
    /// <returns></returns>
    public int GetFragmentCount(int heroTableId)
    {
        int count = 0;
         List<BaseItem>  items = ReturnItemType((int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, EM_ITEM_TYPE.EM_ITEM_TYPE_FRAGMENT);
         foreach (BaseItem item in items)
         {
             if (((ItemFragment)item).GetComposeHeoid() == heroTableId)
             {
                 count+=item.GetItemCount();
             }
         }
         return count;
    }
    /// <summary>
    /// 获取某个英雄的碎片BaseItem 此接口 是适用于碎片合成英雄
    /// </summary>
    /// <param name="heroTableId">英雄表id</param>
    /// <returns></returns>
    public BaseItem GetFragmentBaseItem(int heroTableId)
    {
        List<BaseItem> items = ReturnItemType((int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, EM_ITEM_TYPE.EM_ITEM_TYPE_FRAGMENT);
        foreach (BaseItem item in items)
        {
            if (((ItemFragment)item).GetComposeHeoid() == heroTableId)
            {
                return item;
            }
        }
        return null;
    }
 
}