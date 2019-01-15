using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using System;
using System.Text;
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

public class UI_GoldBuyMgr : BaseUI
{
    public static readonly string UI_ResPath = "UI_Shop/UI_GoldBuy_1_2";

    Text titleTxt;
    Image moneyImg;
    Text moneyTxt;
    Text nameTxt;
    Image iconImg;
    Image costImg;
    Text costTxt;
    Button buyBtn;
    Text buyBtnTxt;
    Button closeBtn;
    Text closeBtnTxt;

    protected GameObject mCostOldObj;
    protected Image mOldImg;
    protected Text mOldTxt;
    protected GameObject mCostNewObj;
    protected Image mNewImg;
    protected Text mNewTxt;

    private static ShopTemplate shopT = null;

    private bool isDiscount = false;

    public override void InitUIData()
    {
        base.InitUIData();

        titleTxt = transform.FindChild("Image/Text").GetComponent<Text>();
        moneyImg = transform.FindChild("MoneyBar/Gold/Image").GetComponent<Image>();
        moneyTxt = transform.FindChild("MoneyBar/Gold/bg/Text").GetComponent<Text>();
        nameTxt = transform.FindChild("Item/NameImg/Text").GetComponent<Text>();
        iconImg = transform.FindChild("Item/iconImg").GetComponent<Image>();
        costImg = transform.FindChild("BuyBtn/Gold/Text/Image").GetComponent<Image>();
        costTxt = transform.FindChild("BuyBtn/Gold/Text").GetComponent<Text>();
        buyBtn = transform.FindChild("BuyBtn").GetComponent<Button>();
        buyBtnTxt = transform.FindChild("BuyBtn/Text").GetComponent<Text>();
        closeBtn = transform.FindChild("CloseBtn").GetComponent<Button>();
        closeBtnTxt = transform.FindChild("CloseBtn/Text").GetComponent<Text>();

        mCostOldObj = transform.FindChild("Item/CostObj/MoneyCost1").gameObject;
        mOldImg = transform.FindChild("Item/CostObj/MoneyCost1/Text/bgImg").GetComponent<Image>();
        mOldTxt = transform.FindChild("Item/CostObj/MoneyCost1/Text").GetComponent<Text>();
        mCostNewObj = transform.FindChild("Item/CostObj/MoneyCost2").gameObject;
        mNewImg = transform.FindChild("Item/CostObj/MoneyCost2/Text/bgImg").GetComponent<Image>();
        mNewTxt = transform.FindChild("Item/CostObj/MoneyCost2/Text").GetComponent<Text>();


        GameEventDispatcher.Inst.addEventListener(GameEventID.G_MoneyResource_Update, UpdateMoney);
    }

    public override void InitUIView()
    {
        base.InitUIView();

        titleTxt.text = GameUtils.getString("shop_content24");
        buyBtnTxt.text = GameUtils.getString("common_button_purchase");
        closeBtnTxt.text = GameUtils.getString("common_button_close");

        buyBtn.onClick.AddListener(OnBuyBtnClick);
        closeBtn.onClick.AddListener(OnCloseBtnClick);

        if(shopT != null)
        {
            SetShowData(shopT);
        }
    }

    public static void SetData(ShopTemplate _shopT)
    {
        shopT = _shopT;
    }

    public void SetShowData(ShopTemplate shopT)
    {
        nameTxt.text = GameUtils.getString(shopT.getCommodityName());
        iconImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + shopT.getResourceName());

        //判断当前时间是否打折期间---与购买商品时候是否打折进行比对，如果不同，购买失败，让玩家从新购买;
        isDiscount = ShopModule.IsShopItemInDiscount(shopT);

        UpdateMoneyInfo();
    }

    void UpdateMoneyInfo()
    {

        int resourceId = shopT.getCostType();
        moneyImg.sprite = GameUtils.GetSpriteByResourceType(resourceId);
        moneyImg.SetNativeSize();
        long count = -1;
        if (ObjectSelf.GetInstance().TryGetResourceCountById(resourceId, ref count))
        {
            moneyTxt.text = count.ToString();
        }

        mOldImg.sprite = GameUtils.GetSpriteByResourceType(resourceId);
        mNewImg.sprite = GameUtils.GetSpriteByResourceType(resourceId);

        bool isDiscount = ShopModule.IsShopItemInDiscount(shopT);

        mCostOldObj.SetActive(isDiscount);
        mCostNewObj.SetActive(isDiscount);

        int buyTimes = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(shopT.getId()).todaynum;
        if (isDiscount)
        {
            mOldTxt.text = DataTemplate.GetInstance().GetShopBuyCost(shopT, buyTimes, false).ToString();
            mNewTxt.text = DataTemplate.GetInstance().GetShopBuyCost(shopT, buyTimes, true).ToString();
        }
        else
        {
            //临时这么写;
            mCostNewObj.SetActive(true);
            mNewTxt.text = DataTemplate.GetInstance().GetShopBuyCost(shopT, buyTimes, false).ToString();
        }
        //costImg.sprite = GameUtils.GetSpriteByResourceType(resourceId);
        //int buyTimes = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(shopT.getId()).todaynum;
        //bool isDiscount = ShopModule.IsShopItemInDiscount(shopT);
        //costTxt.text = DataTemplate.GetInstance().GetShopBuyCost(shopT, buyTimes, isDiscount).ToString();
    }

    void UpdateMoney()
    {
        long count = -1;
        if (ObjectSelf.GetInstance().TryGetResourceCountById(shopT.getCostType(), ref count))
        {
            moneyTxt.text = count.ToString();
        }
    }

    public override void OnReadyForClose()
    {
        base.OnReadyForClose();

        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_MoneyResource_Update, UpdateMoney);

        shopT = null;
        buyBtn.onClick.RemoveAllListeners();
        closeBtn.onClick.RemoveAllListeners();
    }

    void CloseUI()
    {
        OnReadyForClose();

        UI_HomeControler.Inst.ReMoveUI(UI_ResPath);
    }
	
    void OnCloseBtnClick()
    {
        CloseUI();
    }

    void OnBuyBtnClick()
    {
        //判断是否还在打折;
        //TODO::
        bool discount = ShopModule.IsShopItemInDiscount(shopT);
        if (discount != isDiscount)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("shop_bubble7"), transform);
            return;
        }

        //判断是否已经下架;
        //TODO::
        bool isSale = ShopModule.IsShopItemInSaling(shopT);
        if (!isSale)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("shop_bubble6"), transform);
            return;
        }
        //购买商品接口;
        ShopModule.BuyItem(shopT.getId(), 1, isDiscount);

        CloseUI();
    }
}
