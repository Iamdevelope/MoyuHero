using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using System;
using System.Collections;
using System.Collections.Generic;

using GNET;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork.Data;
using DreamFaction.LogSystem;

public class ShopBuyItemData
{
    public int shopId;
}

public class UI_ShopBuyItemMgr : BaseUI
{
    public static readonly string UI_ResPath = "UI_Shop/UI_ItemBuy_01_03";

    protected Image iconImg;
    protected Text titleTxt;
    protected Text nameTxt;
    protected Text countTxt;
    protected Text curCountTxt;
    protected Image reduceImg;
    protected Image addImg;
    protected Image moneyIcon;
    protected Text moneyTotal;

    //左侧;
    protected Text oneTitleTxt;
    protected Image oneCostImg;
    protected Text oneCostTxt;
    //右侧;
    protected Text totalTitleTxt;
    protected Image totalCostImg;
    protected Text totalCostTxt;

    protected Button buyBtn;
    protected Text buyBtnTxt;
    protected Button closeBtn;
    protected Text closeBtnTxt;

    private bool isDiscount;        //记录界面展示时候是否打折期间;
    private static ShopBuyItemData data = null;
    private int perPrice;           //单价;
    private int count = -1;
    private ShopTemplate _shopT;
    private int maxCount = -1;      //缓存最大购买次数;

    private EventTriggerListener mEventTriggerReduce = null;
    private EventTriggerListener mEventTriggerAdd = null;

    bool canAdd = false;            //加按钮响应开关;
    bool canReduce = false;         //减按钮响应开关;

    public static void SetShopBuyItemData(ShopBuyItemData shopData)
    {
        data = shopData;
    }

    int ItemCount
    {
        get
        {
            return count;
        }
        set
        {
            if (count == value)
                return;

            OnItemCountChange(count, value);

            count = value;
        }
    }

    ShopTemplate ShopT
    {
        get
        {
            if(_shopT == null)
            {
                _shopT = DataTemplate.GetInstance().GetShopTemplateByID(data.shopId);

                if(_shopT == null)
                {
                    //InterfaceControler.GetInst().AddMsgBox("商城中不存在该物品id=" + sbid.shopId);
                    LogManager.LogError("商城中不存在指定的商店类型shopId = " + data.shopId);
                }
            }

            return _shopT;
        }
    }

    //public UI_ShopBuyItemMgr(GameObject go)
    //{
    //    if(go == null)
    //        return;

    //    mGo = go;
    //    //go.AddComponent<UI_ShopBuyItemMgr>();
    //    InitUIData();
    //}

    void UpdateTotalGoldInfo()
    {
        moneyTotal.text = ObjectSelf.GetInstance().Gold.ToString();
    }

    void UpdateTotalMoneyInfo()
    {
        moneyTotal.text = ObjectSelf.GetInstance().Money.ToString();
    }


    void OnBuyBtnClick()
    {
        //判断是否还在打折;
        //TODO::
        bool discount = ShopModule.IsShopItemInDiscount(ShopT);
        if(discount != isDiscount)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("shop_bubble7"), selfTransform);
            return;
        }

        //判断是否已经下架;
        //TODO::
        bool isSale = ShopModule.IsShopItemInSaling(ShopT);
        if(!isSale)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("shop_bubble6"), selfTransform);
            return;
        }

        //购买商品接口;
        //string err = "";
        //if(!ShopModule.BuyItem(ShopT.getId(),ItemCount, isDiscount, out err))
        //{
        //    InterfaceControler.GetInst().AddMsgBox(err, selfTransform);
        //}
        //else
        //{
        //    CloseUI();
        //}

        ShopModule.BuyItem(ShopT.getId(), ItemCount, isDiscount);
        CloseUI();
    }

    void OnCloseBtnClick()
    {
        CloseUI();
    }

    void CloseUI()
    {
        switch (ShopT.getCostType())
        {
            case (int)EM_RESOURCE_TYPE.Gold:
                GameEventDispatcher.Inst.removeEventListener(GameEventID.G_Gold_Update, UpdateTotalGoldInfo);
                break;
            case (int)EM_RESOURCE_TYPE.Money:
                GameEventDispatcher.Inst.removeEventListener(GameEventID.G_Money_Update, UpdateTotalMoneyInfo);
                break;
        }

        UI_HomeControler.Inst.ReMoveUI(UI_ResPath);
    }

    void OnDestroy()
    {
        OnReadyForClose();
    }

    public override void InitUIData()
    {
        base.InitUIData();

        iconImg = selfTransform.FindChild("UI_BG_Main/UI_Image_Icon").GetComponent<Image>();
        titleTxt = selfTransform.FindChild("UI_BG_Main/UI_BG_Title/UI_Text_Title").GetComponent<Text>();
        nameTxt = selfTransform.FindChild("UI_BG_Main/UI_Text_Name").GetComponent<Text>();
        countTxt = selfTransform.FindChild("UI_BG_Main/UI_Bg_Num/UI_Text_Num").GetComponent<Text>();
        curCountTxt = selfTransform.FindChild("UI_BG_Main/HaveObj/CountTxt").GetComponent<Text>();
        reduceImg = selfTransform.FindChild("UI_BG_Main/UI_Btn_Minus").GetComponent<Image>();
        addImg = selfTransform.FindChild("UI_BG_Main/UI_Btn_Add").GetComponent<Image>();
        moneyIcon = selfTransform.FindChild("MoneyBar/Gold/Image").GetComponent<Image>();
        moneyTotal = selfTransform.FindChild("MoneyBar/Gold/bg/Text").GetComponent<Text>();

        //左侧;
        oneTitleTxt = selfTransform.FindChild("UI_BG_Main/UI_BG_Cost/UI_BG_Golds/Text").GetComponent<Text>();
        oneCostImg = selfTransform.FindChild("UI_BG_Main/UI_BG_Cost/UI_BG_Golds").GetComponent<Image>();
        oneCostTxt = selfTransform.FindChild("UI_BG_Main/UI_BG_Cost/UI_BG_Golds/UI_Text_Price").GetComponent<Text>();
        //右侧;
        totalTitleTxt = selfTransform.FindChild("UI_BG_Main/UI_BG_Cost/UI_BG_Total/Text").GetComponent<Text>();
        totalCostImg = selfTransform.FindChild("UI_BG_Main/UI_BG_Cost/UI_BG_Total").GetComponent<Image>();
        totalCostTxt = selfTransform.FindChild("UI_BG_Main/UI_BG_Cost/UI_BG_Total/UI_Text_Total").GetComponent<Text>();

        buyBtn = selfTransform.FindChild("UI_BG_Main/UI_Btn_Buy").GetComponent<Button>();
        buyBtnTxt = selfTransform.FindChild("UI_BG_Main/UI_Btn_Buy/Text").GetComponent<Text>();
        closeBtn = selfTransform.FindChild("UI_BG_Main/UI_Btn_Close").GetComponent<Button>();
        closeBtnTxt = selfTransform.FindChild("UI_BG_Main/UI_Btn_Close/Text").GetComponent<Text>();

        buyBtn.onClick.AddListener(OnBuyBtnClick);
        closeBtn.onClick.AddListener(OnCloseBtnClick);

        mEventTriggerReduce = EventTriggerListener.Get(reduceImg.gameObject);
        mEventTriggerReduce.onPress = OnReduceBtnPress;
        mEventTriggerReduce.InitPressInterval = 0.5f;
        mEventTriggerReduce.needResetInterval = true;
        mEventTriggerAdd = EventTriggerListener.Get(addImg.gameObject);
        mEventTriggerAdd.onPress = OnAddBtnPress;
        mEventTriggerAdd.InitPressInterval = 0.5f;
        mEventTriggerAdd.needResetInterval = true;
        EventTriggerListener.Get(reduceImg.gameObject).onClick = OnReduceBtnPress;
        EventTriggerListener.Get(addImg.gameObject).onClick = OnAddBtnPress;

        buyBtnTxt.text = GameUtils.getString("common_button_purchase");
        closeBtnTxt.text = GameUtils.getString("common_button_close");
        titleTxt.text = GameUtils.getString("common_purchaseform_title");
        oneTitleTxt.text = GameUtils.getString("shop_content10");
        totalTitleTxt.text = GameUtils.getString("shop_content11");
    }

    public override void InitUIView()
    {
        base.InitUIView();

        if (data != null)
        {
            _shopT = DataTemplate.GetInstance().GetShopTemplateByID(data.shopId);

            iconImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + ShopT.getResourceName());
            //iconImg.SetNativeSize();
            nameTxt.text = GameUtils.getString(ShopT.getCommodityName());
            oneCostImg.sprite = GameUtils.GetSpriteByResourceType(ShopT.getCostType());
            totalCostImg.sprite = GameUtils.GetSpriteByResourceType(ShopT.getCostType());

            //判断当前时间是否打折期间---与购买商品时候是否打折进行比对，如果不同，购买失败，让玩家从新购买;
            isDiscount = ShopModule.IsShopItemInDiscount(ShopT);

            if (isDiscount)
            {
                perPrice = ShopT.getDiscountCost()[0];
            }
            else
            {
                perPrice = ShopT.getCost()[0];
            }
            oneCostTxt.text = perPrice.ToString();
            totalCostTxt.text = (ItemCount * perPrice).ToString();

            maxCount = GetMaxCount();

            ItemCount = Mathf.Min(1, maxCount);

            moneyIcon.sprite = GameUtils.GetSpriteByResourceType(ShopT.getCostType());
            switch (ShopT.getCostType())
            {
                case (int)EM_RESOURCE_TYPE.Gold:
                    GameEventDispatcher.Inst.addEventListener(GameEventID.G_Gold_Update, UpdateTotalGoldInfo);
                    UpdateTotalGoldInfo();
                    break;
                case (int)EM_RESOURCE_TYPE.Money:
                    GameEventDispatcher.Inst.addEventListener(GameEventID.G_Money_Update, UpdateTotalMoneyInfo);
                    UpdateTotalMoneyInfo();
                    break;
            }

            int curCount = -1;
            if (ObjectSelf.GetInstance().TryGetItemCountById(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, Convert.ToInt32(_shopT.getPara()), ref curCount))
            {
                curCountTxt.text = curCount + "";
            }
        }
    }

    void OnReduceBtnPress(GameObject go)
    {
        if(canReduce)
        {
            ItemCount--;
            float interval = mEventTriggerReduce.pressInterval;
            mEventTriggerReduce.pressInterval = Mathf.Max(0.1f, interval - 0.1f);
        }
    }

    void OnAddBtnPress(GameObject go)
    {
        if(canAdd)
        {
            ItemCount++;
            float interval = mEventTriggerAdd.pressInterval;
            mEventTriggerAdd.pressInterval = Mathf.Max(0.1f, interval - 0.1f);
        }
    }

    public void OnItemCountChange(int oldCount, int newCount)
    {
        totalCostTxt.text = (newCount * perPrice).ToString();

        bool isMax = newCount >= maxCount;
        canAdd = !isMax;
        GameUtils.SetImageGrayState(addImg, isMax);
        if(isMax)
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("shop_bubble1"), selfTransform);

        bool isMin = newCount <= 1;
        canReduce = !isMin;
        GameUtils.SetImageGrayState(reduceImg, isMin);

        countTxt.text = newCount.ToString();
    }

    /// <summary>
    /// 最大购买次数;
    /// </summary>
    /// <returns></returns>
    public int GetMaxCount()
    {
        ////-----根据人物当前资源最大购买次数--------
        //int countByMoney = -1;
        //long count = -1;
        //if(ObjectSelf.GetInstance().TryGetResourceCountById(ShopT.getCostType(), ref count))
        //{
        //    countByMoney = (int)(count / perPrice);
        //}

        //int vipLv = ObjectSelf.GetInstance().VipLevel;
        //Shopbuy sb = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(ShopT.getId());
        ////-----根据人物当前vip每日限购最大购买次数--
        //int countByDaily = -1;
        ////-----根据人物当前vip总限购最大购买次数----
        //int countByTotal = -1;
        //if(DataTemplate.GetInstance().IsShopBuyLimited(ShopT))
        //{
        //    countByDaily = DataTemplate.GetInstance().GetShopItemDailyBuyTimes(ShopT, vipLv) - sb.todaynum;
        //    countByTotal = DataTemplate.GetInstance().GetShopItemDailyBuyTimes(ShopT, vipLv) - sb.buyallnum;
        //}
        
        //int[] times = new int[3];
        //times[0] = countByMoney;
        //times[1] = countByDaily;
        //times[0] = countByTotal;

        //return Mathf.Max(0, Mathf.Min(times));

        return ShopModule.GetMaxBuyCount(ShopT.getId(), perPrice);
    }

    public override void OnReadyForClose()
    {
        base.OnReadyForClose();

        buyBtn.onClick.RemoveListener(OnBuyBtnClick);
        closeBtn.onClick.RemoveListener(OnCloseBtnClick);

        EventTriggerListener.Get(reduceImg.gameObject).onPress = null;
        EventTriggerListener.Get(addImg.gameObject).onPress = null;
        EventTriggerListener.Get(reduceImg.gameObject).onClick = null;
        EventTriggerListener.Get(addImg.gameObject).onClick = null;
    }

    public override void UpdateUIData()
    {
        ;
    }
}
