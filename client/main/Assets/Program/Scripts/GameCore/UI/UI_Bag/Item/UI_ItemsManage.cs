using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.LogSystem;
using GNET;
using DreamFaction.UI;
using DreamFaction.GameEventSystem;
//*******提示 
//当刷新物品数量时 返回的消息是  SRefreshItem和 SModItemNum
//先返回SRefreshItem 再刷新SModItemNum
//刷新物品其他信息 返回的是SRefreshItem
//开启礼包 返回的是SAddItem
public class UI_ItemsManage : BaseUI
{

    public static UI_ItemsManage _instance;

    public LoopLayout m_LoopLayout;
    public List<GameObject> itemList = new List<GameObject>();
    public List<BaseItem> item = new List<BaseItem>();
    public GameObject scrollbar;
    public X_GUID recordX_GUID;
    public List<int> giftIDList;
    public int m_SelectIndex = 0;
    public EM_SORT_COMMON_ITEM curType;
    public BaseItem itemID;

    public bool isShowItemGetWindow;//是否显示道具获得窗口 主要用来屏蔽useTip不为-1时的显示

    public override void InitUIData()
    {
        _instance = this;
    }

    public override void InitUIView()
    {
        base.InitUIView();
        m_LoopLayout = selfTransform.FindChild("UI_BG_Right_Goods/new/Right/ListItems/loopLayout").GetComponent<LoopLayout>();
        GameEventDispatcher.Inst.addEventListener(GameEventID.KE_ShowGift, GiftShow);
        GameEventDispatcher.Inst.addEventListener(GameEventID.KE_ModItemNum, OnUpdateShow);
        SelectItem(EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_ALL);
    }

    protected void OnDestroy()
    {
        GameEventDispatcher.Inst.clearEvent(GameEventID.KE_ShowGift);
        GameEventDispatcher.Inst.clearEvent(GameEventID.KE_ModItemNum);
    }
  
    void UpdateItem(int index, RectTransform cell)
    {
        UI_ItemsButtonMassage uib = cell.transform.GetComponent<UI_ItemsButtonMassage>(); 
        //int bagMax= ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSizeMax();
        //int emptyCount;
        //int lockCellCount = 5;
        //if (bagMax - ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSum() >= 5)
        //{
        //    if (item.Count < 15)
        //    {
        //        emptyCount = bagMax - ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSum();
        //        lockCellCount = 15 - item.Count - emptyCount;
        //    }
        //    else
        //    {
        //        lockCellCount = 0;
        //        if (item.Count % m_LoopLayout.columns != 0)
        //        {
        //            emptyCount = m_LoopLayout.columns - item.Count % m_LoopLayout.columns;
        //        }
        //        else
        //        {
        //            emptyCount = 0;
        //        }
        //    }
        //}
        //else    //改动参考符文
        //{
        //    if (item.Count < 15)
        //    {
        //        emptyCount = bagMax - ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSum();
        //        lockCellCount = 15 - item.Count - emptyCount;
        //    }
        //    else
        //    {
        //        emptyCount = bagMax - ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSum();
        //        if (item.Count % m_LoopLayout.columns == 0)
        //        {
        //            emptyCount = 0;
        //            lockCellCount = 0;
        //        }
        //        else
        //        {
        //            if (item.Count % m_LoopLayout.columns + emptyCount > 5)
        //            {
        //                emptyCount = 5 - item.Count % m_LoopLayout.columns;
        //                lockCellCount = 0;
        //            }
        //            else
        //            {
        //                lockCellCount = 5 - item.Count % m_LoopLayout.columns - emptyCount;
        //            }
        //        }
        //    }
        //}
        //int totalCellCount = item.Count + emptyCount + lockCellCount;
        if (index < item.Count)
        {
            uib.baseItem = item[index];
            uib.itemState = UI_ItemsButtonMassage.ItemSate.FillDate;
            uib.ItemShow();
            uib.index = index;

        }
        //else if(index>=item.Count&&index<item.Count+emptyCount)
        //{
        //    //uib.baseItem = null;
        //    //uib.itemState = UI_ItemsButtonMassage.ItemSate.Empty;
        //    //uib.ItemShow();
        //    //uib.index = index;
            
        //}
        //else if (index >= item.Count + emptyCount && index < totalCellCount)
        //{
        //    //uib.baseItem = null;
        //    //uib.itemState = UI_ItemsButtonMassage.ItemSate.Lock;
        //    //uib.ItemShow();
        //    //uib.index = index;
        //}
        itemList.Add(cell.gameObject);

        List<RectTransform> cellList = m_LoopLayout.cellList;
        for (int i = 0; i < cellList.Count; ++i)
        {
            UI_ItemsButtonMassage items = cellList[i].GetComponent<UI_ItemsButtonMassage>();
            if (items.index == m_SelectIndex)
            {
                ItemList(items.baseItem);
                UI_ItemsMassage._instance.UpdateShow(items.baseItem);
                break;
            }
            else
            {
                //uib.transform.FindChild("Parent/Border").gameObject.SetActive(false);
                items.SetSelectState(false);
            }
        }
       
    }
    public void SelectItem(EM_SORT_COMMON_ITEM type)
    {
        item.Clear();
        curType = type;
        switch (type)
        {
            case EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_ALL:
                item = ObjectSelf.GetInstance().CommonItemContainer.SortCommonItemByType(EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_ALL);
                ReloadItem();
                break;
            case EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_CONSUME:   //也不知道策划发什么神经 非要消耗品和礼包都显示在消耗品呢 傻缺呢？
                List<BaseItem>  itemTemp = ObjectSelf.GetInstance().CommonItemContainer.SortCommonItemByType(EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_CONSUME);
                item = ObjectSelf.GetInstance().CommonItemContainer.SortCommonItemByType(EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_GIFT);
                item.AddRange(itemTemp);
                ReloadItem();
                break;
            case EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_MATERIAL:
                item = ObjectSelf.GetInstance().CommonItemContainer.SortCommonItemByType(EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_MATERIAL);
                ReloadItem();
                break;
            case EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_FRAGMENT:
                item = ObjectSelf.GetInstance().CommonItemContainer.SortCommonItemByType(EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_FRAGMENT);
                ReloadItem();
                break;
            default:
                break;
        }
        if (item.Count <= 0)
        {
            UI_Bag._instance.SetShowHideNullItemTip(true);
        }
        else
        {
            UI_Bag._instance.SetShowHideNullItemTip(false);
        }

    }
    private void OnUpdateShow()
    {
        m_LoopLayout.UpdateCell();
        //更新列表
       // SelectItem(curType);
        List<RectTransform> cellList = m_LoopLayout.cellList;
        for (int i = 0; i < cellList.Count; ++i)
        {
            UI_ItemsButtonMassage items = cellList[i].GetComponent<UI_ItemsButtonMassage>();
            if (items.index == m_SelectIndex)
            {
                ItemList(items.baseItem);
                //更新左边详细信息
                UI_ItemsMassage._instance.UpdateShow(items.baseItem);
                //更新item信息
                items.UpdateShow(items.baseItem);
                break;
            }
            else
            {
                //uib.transform.FindChild("Parent/Border").gameObject.SetActive(false);
                items.SetSelectState(false);
            }
        }
    }
    void OnEnable()
    {
    }
    public void ReloadItem()
    {
        //UI_Bag._instance.GoldShow();

        if (item.Count <= 0)
        {
            UI_ItemsMassage._instance.transform.gameObject.SetActive(false);
        }
        else
        {
            UI_ItemsMassage._instance.transform.gameObject.SetActive(true);
        }        

        ////m_LoopLayout.cellCount = item.Count;
        //int bagMax= ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSizeMax();
        //int emptyCount;
        //int lockCellCount = 5;
        //if (bagMax - ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSum() >= 5)
        //{
        //    if (item.Count < 15)
        //    {
        //        emptyCount = bagMax - ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSum();
        //        lockCellCount = 15 - item.Count - emptyCount;
        //    }
        //    else
        //    {
        //        lockCellCount = 0;
        //        if (item.Count % m_LoopLayout.columns != 0)
        //        {
        //            emptyCount = m_LoopLayout.columns - item.Count % m_LoopLayout.columns;
        //        }
        //        else
        //        {
        //            emptyCount = 0;
        //        }
        //    }
        //}
        //else   
        //{
        //    if (item.Count < 15)
        //    {
        //        emptyCount = bagMax - ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSum();
        //        lockCellCount = 15 - item.Count - emptyCount;
        //    }
        //    else
        //    {
        //        emptyCount = bagMax - ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSum();
        //        if (item.Count % m_LoopLayout.columns == 0)
        //        {
        //            emptyCount = 0;
        //            lockCellCount = 0;
        //        }
        //        else
        //        {
        //            if (item.Count % m_LoopLayout.columns + emptyCount > 5)
        //            {
        //                emptyCount = 5 - item.Count % m_LoopLayout.columns;
        //                lockCellCount = 0;
        //            }
        //            else
        //            {
        //                lockCellCount = 5 - item.Count % m_LoopLayout.columns - emptyCount;
        //            }
        //        }
        //    }
        //}
        //int totalCellCount = item.Count + emptyCount + lockCellCount;
        m_LoopLayout.cellCount = item.Count;
        //if (item.Count < 15)
        //{
        //    m_LoopLayout.emptyCellCount = 15 - item.Count;
        //}
        //else
        //{
        //    if (item.Count % m_LoopLayout.columns != 0)
        //    {
        //        m_LoopLayout.emptyCellCount = m_LoopLayout.columns - item.Count % m_LoopLayout.columns;
        //    }
        //    else
        //    {
        //        m_LoopLayout.emptyCellCount = 0;
        //    }
        //}

        m_LoopLayout.updateCellEvent = UpdateItem;
        m_LoopLayout.Reload();

    }
    public void GiftShow(GameEvent e )
    {
        if (!isShowItemGetWindow) return;
  
 
            UI_Bag._instance.OnOpenItemGet();
            UI_ItemGet.inist.mGiftList = giftIDList;
            UI_ItemGet.inist.FillData();
        

    }

    public void RemoveItem()
    {
        // 
        switch (curType)
        {
            case EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_ALL:
                item = ObjectSelf.GetInstance().CommonItemContainer.SortCommonItemByType(EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_ALL);
                ReloadItem();
                break;
            case EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_CONSUME:   //也不知道策划发什么神经 非要消耗品和礼包都显示在消耗品呢 傻缺呢？
                List<BaseItem> itemTemp = ObjectSelf.GetInstance().CommonItemContainer.SortCommonItemByType(EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_CONSUME);
                item = ObjectSelf.GetInstance().CommonItemContainer.SortCommonItemByType(EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_GIFT);
                item.AddRange(itemTemp);
                ReloadItem();
                break;
            case EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_MATERIAL:
                item = ObjectSelf.GetInstance().CommonItemContainer.SortCommonItemByType(EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_MATERIAL);
                ReloadItem();
                break;
            case EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_FRAGMENT:
                item = ObjectSelf.GetInstance().CommonItemContainer.SortCommonItemByType(EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_FRAGMENT);
                ReloadItem();
                break;
            default:
                break;
        }
        if (item.Count <= 0)
        {
            UI_Bag._instance.SetShowHideNullItemTip(true);
        }
        else
        {
            UI_Bag._instance.SetShowHideNullItemTip(false);
        }

        //if (item.Count <= 0)
        //{
        //    UI_ItemsMassage._instance.transform.gameObject.SetActive(false);
        //}
        //else
        //{
        //    UI_ItemsMassage._instance.transform.gameObject.SetActive(true);
        //}

        //int bagMax = ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSizeMax();
        //int emptyCount;
        //int lockCellCount = 5;
        //if (bagMax - ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSum() >= 5)
        //{
        //    if (item.Count < 15)
        //    {
        //        emptyCount = bagMax - ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSum();
        //        lockCellCount = 15 - item.Count - emptyCount;
        //    }
        //    else
        //    {
        //        lockCellCount = 0;
        //        if (item.Count % m_LoopLayout.columns != 0)
        //        {
        //            emptyCount = m_LoopLayout.columns - item.Count % m_LoopLayout.columns;
        //        }
        //        else
        //        {
        //            emptyCount = 0;
        //        }
        //    }
        //}
        //else  
        //{
        //    if (item.Count < 15)
        //    {
        //        emptyCount = bagMax - ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSum();
        //        lockCellCount = 15 - item.Count - emptyCount;
        //    }
        //    else
        //    {
        //        emptyCount = bagMax - ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSum();
        //        if (item.Count % m_LoopLayout.columns == 0)
        //        {
        //            emptyCount = 0;
        //            lockCellCount = 0;
        //        }
        //        else
        //        {
        //            if (item.Count % m_LoopLayout.columns + emptyCount > 5)
        //            {
        //                emptyCount = 5 - item.Count % m_LoopLayout.columns;
        //                lockCellCount = 0;
        //            }
        //            else
        //            {
        //                lockCellCount = 5 - item.Count % m_LoopLayout.columns - emptyCount;
        //            }
        //        }
        //    }
        //}
        //int totalCellCount = item.Count + emptyCount + lockCellCount;

        m_LoopLayout.cellCount = item.Count;

        //int bagSize = ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSizeMax();

        //    if (item.Count < 15)
        //    {
        //        m_LoopLayout.emptyCellCount = 15 - item.Count;
        //    }
        //    else
        //    {
        //        if (item.Count % m_LoopLayout.columns != 0)
        //        {
        //            m_LoopLayout.emptyCellCount = m_LoopLayout.columns - item.Count % m_LoopLayout.columns;
        //        }
        //        else
        //        {
        //            m_LoopLayout.emptyCellCount = 0;
        //        }
        //    }

        m_LoopLayout.updateCellEvent = UpdateItem;
        m_LoopLayout.Reload();
    }

    public void ItemList(BaseItem id)
    {
        itemID = id;
        List<RectTransform> cellList = m_LoopLayout.cellList;
        for (int i = 0; i < cellList.Count; ++i)
        {
            UI_ItemsButtonMassage item = cellList[i].GetComponent<UI_ItemsButtonMassage>();
            if (item.baseItem == id)
            {
                //item.transform.FindChild("Parent/Border").gameObject.SetActive(true);
                item.SetSelectState(true);
                m_SelectIndex = item.index;
            }
            else
            {
                //item.transform.FindChild("Parent/Border").gameObject.SetActive(false);
                item.SetSelectState(false);
            }
        }
    }
}
