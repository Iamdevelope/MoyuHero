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
using DreamFaction.LogSystem;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork.Data;

class QuikChargeItemUI
{
    private ExchangeTemplate mExchangeT;

    Transform trans;

    Text nameTxt;
    Image iconBg;
    Image iconImg;
    Button iconBtn;
    Text detailTxt;
    Text costTypeTxt;
    Text costNumTxt;

    Button buyBtn;

    public QuikChargeItemUI(GameObject go, ExchangeTemplate exchangeT)
    {
        trans = go.transform;

        mExchangeT = exchangeT;

        nameTxt = trans.FindChild("NameImg/Text").GetComponent<Text>();
        iconBg = trans.FindChild("iconBg").GetComponent<Image>();
        iconImg = trans.FindChild("iconImg").GetComponent<Image>();
        iconBtn = trans.FindChild("iconImg").GetComponent<Button>();
        detailTxt = trans.FindChild("DetailTxt").GetComponent<Text>();
        costTypeTxt = trans.FindChild("MoneyCost/TypeTxt").GetComponent<Text>();
        costNumTxt = trans.FindChild("MoneyCost/Text").GetComponent<Text>();

        nameTxt.text = GameUtils.getString(exchangeT.getName());
        iconBg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + exchangeT.getBaseicon());
        iconBg.SetNativeSize();
        iconImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + exchangeT.getIcon());
        iconImg.SetNativeSize();
        detailTxt.text = GameUtils.getString(exchangeT.getDetail());
        costTypeTxt.text = ExchangeModule.Money_Str;
        costNumTxt.text = exchangeT.getPrice().ToString();

        buyBtn = trans.FindChild("BuyBtn").GetComponent<Button>();

        //iconBtn.onClick.AddListener(OnItemClick);
        buyBtn.onClick.AddListener(OnItemClick);
    }

    void OnItemClick()
    {
        //ShopModule.BuyItem(mShopT.getId(), 1, false);
        ExchangeModule.ChargeMoney(mExchangeT.getId());
    }
    

    public void Destroy()
    {
        //iconBtn.onClick.RemoveListener(OnItemClick);
        buyBtn.onClick.AddListener(OnItemClick);
    }
}

public class UI_QuikChargeMgr : BaseUI
{
    public static readonly string UI_ResPath = "UI_Shop/UI_QuikCharge_1_4";

    public static int CurShowShopId = -1;

    GameObject itemObj;
    GameObject itemListObj;
    Text titleTxt;
    Button closeBtn;
    Text closeBtnTxt;

    Image costImg;
    Text costTxt;

    private int resourceId = -1;

    private List<ExchangeTemplate> mShopTemList = new List<ExchangeTemplate>();

    private List<QuikChargeItemUI> mChargeUIs = new List<QuikChargeItemUI>();

    public override void InitUIData()
    {
        base.InitUIData();

        itemObj = transform.FindChild("Items/Item").gameObject;
        itemListObj = transform.FindChild("ItemList/ListObj").gameObject;
        titleTxt = transform.FindChild("Image/Text").GetComponent<Text>();
        closeBtn = transform.FindChild("CloseBtn").GetComponent<Button>();
        closeBtnTxt = transform.FindChild("CloseBtn/Text").GetComponent<Text>();

        costImg = transform.FindChild("MoneyBar/Gold/Image").GetComponent<Image>();
        costTxt = transform.FindChild("MoneyBar/Gold/bg/Text").GetComponent<Text>();

        GameEventDispatcher.Inst.addEventListener(GameEventID.G_Gold_Update, UpdateMoney);

        closeBtn.onClick.AddListener(OnCloseBtnClick);

        mShopTemList = DataTemplate.GetInstance().GetQuikChargeShopID(MainGameControler.Inst.mPlatform);
    }

    public override void InitUIView()
    {
        base.InitUIView();

        titleTxt.text = GameUtils.getString("quickly_pay");
        closeBtnTxt.text = GameUtils.getString("common_button_close");

        resourceId = (int)EM_RESOURCE_TYPE.Gold;
        costImg.sprite = GameUtils.GetSpriteByResourceType(resourceId);
        costImg.SetNativeSize();

        long count = -1;
        if (ObjectSelf.GetInstance().TryGetResourceCountById(resourceId, ref count))
        {
            costTxt.text = count.ToString();
        }

        for (int i = 0; i < mShopTemList.Count; i++ )
        {
            CreateItem(mShopTemList[i]);
        }
    }

    void UpdateMoney()
    {
        if (resourceId < 0)
        {
            costTxt.text = "0";
            return;
        }

        long count = -1;
        if (ObjectSelf.GetInstance().TryGetResourceCountById(resourceId, ref count))
        {
            costTxt.text = count.ToString();
        }
    }

    public override void OnReadyForClose()
    {
        base.OnReadyForClose();

        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_Gold_Update, UpdateMoney);

        closeBtn.onClick.RemoveListener(OnCloseBtnClick);

        for (int i = 0; i < mChargeUIs.Count; i++ )
        {
            mChargeUIs[i].Destroy();
        }
        mChargeUIs.Clear();
        mChargeUIs = null;

        mShopTemList.Clear();
        mShopTemList = null;
    }

    void OnDestroy()
    {
        UIState = BaseUI.UIStateEnum.ReadyForClose;
    }

    void OnCloseBtnClick()
    {
        GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_ActivityMoneyChange);
        UI_HomeControler.Inst.ReMoveUI(UI_ResPath);
    }

    void CreateItem(ExchangeTemplate shopT)
    {
        GameObject go = GameObject.Instantiate(itemObj) as GameObject;

        go.transform.parent = itemListObj.transform;
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;

        QuikChargeItemUI chargeUI = new QuikChargeItemUI(go, shopT);
        mChargeUIs.Add(chargeUI);
    }

}
