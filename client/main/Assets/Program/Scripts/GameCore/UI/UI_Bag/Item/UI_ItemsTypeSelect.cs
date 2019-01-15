using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.LogSystem;
using GNET;
using DreamFaction.Utils;
using DreamFaction.UI;
public class UI_ItemsTypeSelect : BaseUI
{
    public static UI_ItemsTypeSelect _instance;
    private Button allButton;
    private Text allText;
    private Button GiftButton;
    private Button MaterialBtn;
    private Button ConsumablesBtn;
    public UI_SlideBtn allbtn;

    public int itemTypeNum;

    private bool upOrbottom = false;//标识主按钮当前是什么状态

    public override void InitUIData()
    {
        _instance = this;
    }

    public override void InitUIView()
    {
        base.InitUIView();        
        itemTypeNum = 1;
       // itemType = selfTransform.FindChild("Select_Show").GetComponent<Text>();
        MaterialBtn = selfTransform.FindChild("SortObj/MaterialBtn").GetComponent<Button>();
        ConsumablesBtn = selfTransform.FindChild("SortObj/ConsumablesBtn").GetComponent<Button>();
        GiftButton = selfTransform.FindChild("SortObj/GiftlBtn").GetComponent<Button>();
        allButton = selfTransform.FindChild("SortObj/AllBtn").GetComponent<Button>();
        allText = selfTransform.FindChild("SortObj/MainBagBtn/Text").GetComponent<Text>();
        allbtn = selfTransform.FindChild("SortObj/MainBagBtn").GetComponent<UI_SlideBtn>();
        
        MaterialBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnMaterial));
        ConsumablesBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnConsumables));
        GiftButton.onClick.AddListener(new UnityEngine.Events.UnityAction(OnGift));
        allButton.onClick.AddListener(new UnityEngine.Events.UnityAction(OnAll));

        UpdateItemType(1);
    }


    public void  UpdateItemType(int itemTypeID)
    {
        switch (itemTypeID)
        {
            case 1:
                allText.text = GameUtils.getString("bag_item_select1");
                UI_ItemsManage._instance.SelectItem(EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_ALL);
                break;
            case 2:
                 allText.text = GameUtils.getString("bag_item_select2");
                UI_ItemsManage._instance.SelectItem(EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_MATERIAL);
                break;
            case 3:
                allText.text = GameUtils.getString("bag_item_select3");
                UI_ItemsManage._instance.SelectItem(EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_GIFT);
                break;
            case 4:
                allText.text = GameUtils.getString("bag_item_select4");
                UI_ItemsManage._instance.SelectItem(EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_CONSUME);
                break;
            default:
                break;
        }

    }

    public void OnMaterial()
    {
        itemTypeNum = 2;
        UpdateItemType(itemTypeNum);
        allbtn.OnClose();
    }
    public void OnConsumables()
    {
        itemTypeNum = 4;
        UpdateItemType(itemTypeNum);
        allbtn.OnClose();
    }

    public void OnGift()
    {
        itemTypeNum = 3;
        UpdateItemType(itemTypeNum);
        allbtn.OnClose();
    }

    public void OnAll()
    {
        itemTypeNum = 1;
        UpdateItemType(itemTypeNum);
        allbtn.OnClose();
        
    }
}
