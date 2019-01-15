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
public class UI_RuneMange : BaseUI
{
    public static UI_RuneMange _instance;
    private LoopLayout m_LoopLayout;
    public List<BaseItem> rune = new List<BaseItem>();
    private GameObject runeMassage;
    public GameObject scrollbar;
    public Transform addItem;
    public X_GUID mGuid = null;
    public int m_SelectIndex = 0;
    public EM_SORT_RUNE_ITEM m_CurType = EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_ALL;

    public override void InitUIData()
    {
        base.InitUIData();
        _instance = this;
        m_LoopLayout = selfTransform.FindChild("UI_BG_Right_Rune/Grid").GetComponent<LoopLayout>();
        runeMassage = selfTransform.FindChild("UI_BG_Left_Rune").gameObject;
        GameEventDispatcher.Inst.addEventListener(GameEventID.Net_RefreshItem, UpdateRuneShow);
    }

    protected void OnDestroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.Net_RefreshItem, UpdateRuneShow);
    }

    void UpdateItem(int index, RectTransform cell)
    {
        UI_RuneButtonMassage uib = cell.transform.GetComponent<UI_RuneButtonMassage>();
        if(uib == null)
        {
            uib = cell.gameObject.AddComponent<UI_RuneButtonMassage>();
        }

        int bagMax = ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSizeMax();
        int emptyCount;
        int lockCellCount = 5;
        if (bagMax - ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSum() >= 5)
        {
            if (rune.Count < 15)
            {
                emptyCount = bagMax - ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSum();
                lockCellCount = 15 - rune.Count  - emptyCount;
            }
            else
            {
                lockCellCount = 0;
                if (rune.Count % m_LoopLayout.columns != 0)
                {
                    emptyCount = m_LoopLayout.columns - rune.Count % m_LoopLayout.columns;
                }
                else
                {
                    emptyCount = 0;
                }
            }
        }
        else  
        {
            if (rune.Count < 15)
            {
                emptyCount = bagMax - ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSum();
                lockCellCount = 15 - rune.Count - emptyCount;
            }
            else
            {
                emptyCount = bagMax - ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSum();
                if (rune.Count % m_LoopLayout.columns == 0)
                {
                    emptyCount = 0;
                    lockCellCount = 0;
                }
                else
                {
                    if (rune.Count % m_LoopLayout.columns + emptyCount > 5)
                    {
                        emptyCount = 5 - rune.Count % m_LoopLayout.columns;
                        lockCellCount = 0;
                    }
                    else
                    {
                        lockCellCount = 5 - rune.Count % m_LoopLayout.columns - emptyCount;
                    }
                }
            }
        }
        int totalCellCount = rune.Count + emptyCount + lockCellCount;
        if (index < rune.Count)
        {
            uib.index = index;
            uib.id = index;
            uib.tableID = rune[index].GetItemTableID();
            uib.itemState = UI_RuneButtonMassage.ItemSate.FillDate;
            uib.RuneShow();

        }
        else if (index >= rune.Count && index < rune.Count + emptyCount)
        {
            uib.index = index;
            uib.id = index;
            uib.itemState = UI_RuneButtonMassage.ItemSate.Empty;
            uib.RuneShow();

        }
        else if (index >= rune.Count + emptyCount && index < totalCellCount)
        {
            uib.index = index;
            uib.id = index;
            uib.itemState = UI_RuneButtonMassage.ItemSate.Lock;
            uib.RuneShow();
        }

        //uib.index = index;
        //uib.id = index;
        //uib.tableID = rune[index].GetItemTableID();
        //uib.itemState = UI_RuneButtonMassage.ItemSate.FillDate;
        //uib.RuneShow();
        if (rune.Count == 0) return;
        if(index == m_SelectIndex)
        {
            UI_RuneMassage._instance.Show();
            uib.transform.FindChild("Parent/Border").gameObject.SetActive(true);
        }
        else
        {
            uib.transform.FindChild("Parent/Border").gameObject.SetActive(false);
        }
    }

    public void SelectRune(EM_SORT_RUNE_ITEM nType)
    {
        rune.Clear();
        m_CurType = nType;
        switch (nType)
        {
            case EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_ALL:
                rune = ObjectSelf.GetInstance().CommonItemContainer.SortRuneItemByType(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_ALL);
                RelodRune();
                break;
            case EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_SPECIAL:
                rune = ObjectSelf.GetInstance().CommonItemContainer.SortRuneItemByType(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_SPECIAL);
                RelodRune();
                break;
            case EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_RED:
                rune = ObjectSelf.GetInstance().CommonItemContainer.SortRuneItemByType(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_RED);
                RelodRune();
                break;
            case EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_GREEN:
                rune = ObjectSelf.GetInstance().CommonItemContainer.SortRuneItemByType(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_GREEN);
                RelodRune();
                break;
            case EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_PURPLE:
                rune = ObjectSelf.GetInstance().CommonItemContainer.SortRuneItemByType(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_PURPLE);
                RelodRune();
                break;
            case EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_BLUE:
                rune = ObjectSelf.GetInstance().CommonItemContainer.SortRuneItemByType(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_BLUE);
                RelodRune();
                break;
            default:
                break;
        }
    }

    public void RelodRune()
    {
        //UI_Bag._instance.GoldShow();

       
        //if (rune.Count < 15)
        //{
        //    m_LoopLayout.emptyCellCount = 15 - rune.Count;
        //}
        //else
        //{
        //    if (rune.Count % m_LoopLayout.columns != 0)
        //    {
        //        m_LoopLayout.emptyCellCount = m_LoopLayout.columns - rune.Count % m_LoopLayout.columns;
        //    }
        //    else
        //    {
        //        m_LoopLayout.emptyCellCount = 0;
        //    }
        //}
        int bagMax = ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSizeMax();
        int emptyCount;
        int lockCellCount = 5;
        if (bagMax - ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSum() >= 5)
        {
            if (rune.Count < 15)
            {
                emptyCount = bagMax - ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSum();
                lockCellCount = 15 - rune.Count - emptyCount;
            }
            else
            {
                lockCellCount = 0;
                if (rune.Count % m_LoopLayout.columns != 0)
                {
                    emptyCount = m_LoopLayout.columns - rune.Count % m_LoopLayout.columns;
                }
                else
                {
                    emptyCount = 0;
                }
            }
        }
        else  
        {
            if (rune.Count < 15)
            {
                emptyCount = bagMax - ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSum();
                lockCellCount = 15 - rune.Count - emptyCount;
            }
            else
            {
                //9.15日改动 当空格不足五个时 才在同一行显示锁链格子
                emptyCount = bagMax - ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSum();
                if (rune.Count % m_LoopLayout.columns == 0)
                {
                    emptyCount = 0;
                    lockCellCount = 0;
                }
                else
                {
                    if (rune.Count % m_LoopLayout.columns + emptyCount > 5)
                    {
                        emptyCount = 5 - rune.Count % m_LoopLayout.columns;
                        lockCellCount = 0;
                    }
                    else
                    {
                        lockCellCount = 5 - rune.Count % m_LoopLayout.columns - emptyCount;
                    }
                }
                //9.15日改动 当空格不足五个时 才在同一行显示锁链格子
                //if (emptyCount  < 5) //当空格数量少于5个时  不需要再开辟一行锁链状态格子
                //{
                //    if (rune.Count % m_LoopLayout.columns != 0)
                //    {
                //        lockCellCount = 5 - rune.Count % m_LoopLayout.columns - emptyCount;
                //    }
                //    else
                //    {
 
                //    }
                   
                //}
                //else
                //{
                //    lockCellCount = 0;
                //}

                //if (emptyCount + rune.Count % m_LoopLayout.columns < 5) //当空格数量少于5个时  不需要再开辟一行锁链状态格子
                //{
                //    lockCellCount = 5 - rune.Count % m_LoopLayout.columns - emptyCount;
                //}
                //else//当空格数量大于5个时  需要再开辟一行锁链状态格子
                //{
                //    lockCellCount = 10 - rune.Count % m_LoopLayout.columns - emptyCount;
                //}
            }
        }
        int totalCellCount = rune.Count + emptyCount + lockCellCount;
        m_LoopLayout.cellCount = totalCellCount;
        m_LoopLayout.updateCellEvent = UpdateItem;
        m_LoopLayout.Reload();
        if(m_LoopLayout.cellList.Count > 0)
            UI_RuneMassage._instance.UpdateShow(0, m_LoopLayout.cellList[0].transform.GetComponent<UI_RuneButtonMassage>().rune);
        if (rune.Count == 0)
        {
            runeMassage.SetActive(false);
        }
    }

    public void UpdateRuneShow()
    {

        m_LoopLayout.UpdateCell();
        //SelectRune(m_CurType);
        List<RectTransform> cellList = m_LoopLayout.cellList;
        for (int i = 0; i < cellList.Count; ++i )
        {
            UI_RuneButtonMassage item = cellList[i].GetComponent<UI_RuneButtonMassage>();
            if (item.index == m_SelectIndex)
            {
                UI_RuneMassage._instance.UpdateShow(m_SelectIndex, item.rune);
                break;
            }
        }
    
    }

    public void RuneList(int index)
    {
        List<RectTransform> cellList = m_LoopLayout.cellList;
        for(int i = 0; i < cellList.Count; ++i)
        {
            UI_RuneButtonMassage rune = cellList[i].GetComponent<UI_RuneButtonMassage>();
            if(rune.index == index)
            {
                rune.transform.FindChild("Parent/Border").gameObject.SetActive(true);
                
            }
            else
            {
                rune.transform.FindChild("Parent/Border").gameObject.SetActive(false);
            }
        }

        m_SelectIndex = index;
    }
}
