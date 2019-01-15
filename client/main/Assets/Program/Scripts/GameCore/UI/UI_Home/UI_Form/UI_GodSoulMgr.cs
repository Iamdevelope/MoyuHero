using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class UI_GodSoulMgr : BaseUI
{
    public static string UI_ResPath = "UI_Form/UI_GodSoul_2_3";
    private static UI_GodSoulMgr _inst;

    private Button m_CloseBtn = null;
    private LoopLayout m_LoopLayout = null;
    private List<ItemTemplate> m_ItemList = null;
    private ItemTemplate m_SelectItem = null;
    private int m_SelectNo = -1;


    public static UI_GodSoulMgr Inst 
    { 
        get { return _inst; } 
    }

    public void SetSelectItem(ItemTemplate item,int no)
    {
        m_SelectItem = item;
        m_SelectNo = no;
    }

    public int GetSelectNo()
    {
        return m_SelectNo;
    }
    public ItemTemplate GetSelectItem()
    {
        return m_SelectItem;
    }


    public override void InitUIData()
    {
        base.InitUIData();

        _inst = this;

        m_LoopLayout = selfTransform.FindChild("Panel/ScrollRect/ListLayout").GetComponent<LoopLayout>();

        m_CloseBtn = selfTransform.FindChild("Panel/CloseBtn").GetComponent<Button>();
        m_CloseBtn.onClick.AddListener(new UnityAction(onClose));
    }

    public override void InitUIView()
    {
        base.InitUIView();

        InitHeroData();
    }

    /// <summary>
    /// 初始化列表
    /// </summary>
    private void InitHeroData()
    {
        List<ItemTemplate> m_SoulList = new List<ItemTemplate>();
        ItemTemplate m_item1 = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(1402010002);
        ItemTemplate m_item2 = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(1402010007);
        ItemTemplate m_item3 = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(1402010008);

        m_SoulList.Add(m_item1);
        m_SoulList.Add(m_item2);
        m_SoulList.Add(m_item3);

        m_ItemList = m_SoulList;
        m_LoopLayout.cellCount = m_ItemList.Count;
        m_LoopLayout.updateCellEvent = UpdateLoadHeroItem;
        m_LoopLayout.Reload();

    }

    private void UpdateLoadHeroItem(int index, RectTransform cell)
    {
        ItemTemplate obj = m_ItemList[index];
        UI_GodSoulItem uiIt = cell.gameObject.GetComponent<UI_GodSoulItem>();
        if (uiIt == null)
        {
            uiIt = cell.gameObject.AddComponent<UI_GodSoulItem>();
        }
        uiIt.index = index;
        uiIt.InitData(obj);

    }



    public void onClose()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
        //UI_HomeControler.Inst.AddUI(UI_FormMgr.UI_ResPath);
    }

}
