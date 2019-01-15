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
using DreamFaction.LogSystem;

public class UI_SkinBuyMgr : BaseUI
{
    public static readonly string UI_ResPath = "UI_Shop/UI_SkinBuy_1_2";

    Text titleTxt;
    Image moneyIcon;
    Text moneyTxt;
    Image iconImg;
    Text nameTxt;
    Text heroTitleTxt;
    Text heroNameTxt;
    Image costImg;
    Text costTxt;
    Button buyBtn;
    Text buyBtnTxt;
    Button closeBtn;
    Text closeBtnTxt;
    Text hintTxt;
    GameObject listObj;
    GameObject attriItem;

    protected GameObject mCostOldObj;
    protected Image mOldImg;
    protected Text mOldTxt;
    protected GameObject mCostNewObj;
    protected Image mNewImg;
    protected Text mNewTxt;

    private static ShopTemplate mShopTemplate = null;

    public override void InitUIData()
    {
        base.InitUIData();

        titleTxt = selfTransform.FindChild("Image/Text").GetComponent<Text>();
        moneyIcon = selfTransform.FindChild("MoneyBar/Gold/Image").GetComponent<Image>();
        moneyTxt = selfTransform.FindChild("MoneyBar/Gold/bg/Text").GetComponent<Text>();
        iconImg = selfTransform.FindChild("Item/iconImg").GetComponent<Image>();
        nameTxt = selfTransform.FindChild("Item/NameImg/Text").GetComponent<Text>();
        costImg = selfTransform.FindChild("BuyBtn/Gold/Text/Image").GetComponent<Image>();
        costTxt = selfTransform.FindChild("BuyBtn/Gold/Text").GetComponent<Text>();
        buyBtn = selfTransform.FindChild("BuyBtn").GetComponent<Button>();
        buyBtnTxt = selfTransform.FindChild("BuyBtn/Text").GetComponent<Text>();
        closeBtn = selfTransform.FindChild("CloseBtn").GetComponent<Button>();
        closeBtnTxt = selfTransform.FindChild("CloseBtn/Text").GetComponent<Text>();
        hintTxt = selfTransform.FindChild("Bottom/Text").GetComponent<Text>();
        listObj = selfTransform.FindChild("Item/Attris").gameObject;
        attriItem = selfTransform.FindChild("Items/AttriPair").gameObject;

        heroTitleTxt = selfTransform.FindChild("HeroTitle").GetComponent<Text>();
        heroNameTxt = selfTransform.FindChild("HeroName").GetComponent<Text>();

        mCostOldObj = selfTransform.FindChild("Item/CostObj/MoneyCost1").gameObject;
        mOldImg = selfTransform.FindChild("Item/CostObj/MoneyCost1/Text/bgImg").GetComponent<Image>();
        mOldTxt = selfTransform.FindChild("Item/CostObj/MoneyCost1/Text").GetComponent<Text>();
        mCostNewObj = selfTransform.FindChild("Item/CostObj/MoneyCost2").gameObject;
        mNewImg = selfTransform.FindChild("Item/CostObj/MoneyCost2/Text/bgImg").GetComponent<Image>();
        mNewTxt = selfTransform.FindChild("Item/CostObj/MoneyCost2/Text").GetComponent<Text>();

        GameUtils.SetImageGrayState(mOldImg, true);
    }

    public override void InitUIView()
    {
        base.InitUIView();

        titleTxt.text = GameUtils.getString("common_purchaseform_title");
        buyBtnTxt.text = GameUtils.getString("common_button_purchase");
        closeBtnTxt.text = GameUtils.getString("common_button_close");
        hintTxt.text = GameUtils.getString("shop_content15");

        buyBtn.onClick.AddListener(OnBuyBtnClick);
        closeBtn.onClick.AddListener(OnCloseBtnClick);

        if (mShopTemplate != null)
            SetShowData(mShopTemplate);
    }

    public override void OnReadyForClose()
    {
        base.OnReadyForClose();

        buyBtn.onClick.RemoveAllListeners();
        closeBtn.onClick.RemoveAllListeners();

        mShopTemplate = null;
    }

    public static void SetShowShopTemplate(ShopTemplate shopT)
    {
        mShopTemplate = shopT;
    }

    public void SetShowData(ShopTemplate shopT)
    {
        if (shopT == null)
        {
            LogManager.LogError("皮肤预览传入的ShopTemplate is null");
            return;
        }

        mOldImg.sprite = GameUtils.GetSpriteByResourceType(shopT.getCostType());
        mNewImg.sprite = GameUtils.GetSpriteByResourceType(shopT.getCostType());

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

        int artTableId = GameUtils.StringToInt(shopT.getPreviewContent());
        ArtresourceTemplate artT = DataTemplate.GetInstance().GetArtResourceTemplate(artTableId);

        if(artT == null)
        {
            LogManager.LogError("ArtresourceTemplate is null id=" + artTableId);
            return;
        }

        iconImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + shopT.getResourceName());
        iconImg.SetNativeSize();

        HeroTemplate heroT = DataTemplate.GetInstance().GetHeroTemplateByArtresourceId(artTableId);

        if (heroT != null)
        {
            heroTitleTxt.text = GameUtils.getString(heroT.getTitleID());
            heroNameTxt.text = GameUtils.getString(heroT.getNameID());
        }
        else
        {
            LogManager.LogError("英雄表中找不到对应皮肤id=" + artTableId + "的数据");
        }

        int count = DataTemplate.GetInstance().GetArtResourceAtrriCount(artT);

        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(artT.getSymbol()[i]);

                if (artT.getIspercentage()[i] == 1)
                {
                    float val = (float)(artT.getAttriValue()[i]) / 10f;
                    sb.Append(val);
                    sb.Append("%");
                }
                else
                {
                    sb.Append(artT.getAttriValue()[i]);
                }

                CreateAttriItem(GameUtils.getString(artT.getAttriDes()[i]), sb.ToString());
            }
        }

        nameTxt.text = GameUtils.getString(artT.getNameID());

        UpdateMoneyInfo();
    }

    void UpdateMoneyInfo()
    {
        int resourceId = mShopTemplate.getCostType();
        moneyIcon.sprite = GameUtils.GetSpriteByResourceType(resourceId);
        moneyIcon.SetNativeSize();
        long count = -1;
        if (ObjectSelf.GetInstance().TryGetResourceCountById(resourceId, ref count))
        {
            moneyTxt.text = count.ToString();
        }

        costImg.sprite = GameUtils.GetSpriteByResourceType(resourceId);
        int buyTimes = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(mShopTemplate.getId()).todaynum;
        bool isDiscount = ShopModule.IsShopItemInDiscount(mShopTemplate);
        costTxt.text = DataTemplate.GetInstance().GetShopBuyCost(mShopTemplate, buyTimes, isDiscount).ToString();
    }

    void CloseUI()
    {
        OnReadyForClose();
        UI_HomeControler.Inst.ReMoveUI(UI_ResPath);
    }

    void CreateAttriItem(string name, string val)
    {
        GameObject go = GameObject.Instantiate(attriItem) as GameObject;
        if (go == null)
        {
            LogManager.LogError("皮肤预览属性obj创建失败");
            return;
        }

        Transform trans = go.transform;

        Text left = trans.FindChild("Left_txt").GetComponent<Text>();
        left.text = name;
        Text right = trans.FindChild("Right_txt").GetComponent<Text>();
        right.text = val;

        trans.parent = listObj.transform;
        trans.localScale = Vector3.one;
        trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y, 0f);
    }

    void OnDestroy()
    {
        OnReadyForClose();
    }

    void OnBuyBtnClick()
    {
        bool isDiscount = ShopModule.IsShopItemInDiscount(mShopTemplate);
        //string errStr = "";
        //if(!ShopModule.BuyItem(mShopTemplate.getId(), 1, isDiscount, out errStr))
        //{
        //    InterfaceControler.GetInst().AddMsgBox(errStr, transform);
        //}
        ShopModule.BuyItem(mShopTemplate.getId(), 1, isDiscount);

    }

    void OnCloseBtnClick()
    {
        CloseUI();
    }
}
