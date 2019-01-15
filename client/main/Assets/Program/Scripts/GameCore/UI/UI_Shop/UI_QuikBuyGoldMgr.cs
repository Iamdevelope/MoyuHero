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

class QuikBuyGoldItemUI
{
    private ShopTemplate mShopT;

    Transform trans;

    Text nameTxt;
    Image iconBg;
    Image iconImg;
    Button iconBtn;
    Text detailTxt;
    GameObject mCostOldObj;
    Image mOldImg;
    Text mOldTxt;
    GameObject mCostNewObj;
    Image mNewImg;
    Text mNewTxt;

    Button buyBtn;

    public QuikBuyGoldItemUI(GameObject go, ShopTemplate shopT)
    {
        trans = go.transform;

        mShopT = shopT;

        nameTxt = trans.FindChild("NameImg/Text").GetComponent<Text>();
        iconBg = trans.FindChild("iconBg").GetComponent<Image>();
        iconImg = trans.FindChild("iconImg").GetComponent<Image>();
        //iconBtn = trans.FindChild("iconImg").GetComponent<Button>();
        detailTxt = trans.FindChild("DetailTxt").GetComponent<Text>();

        mCostOldObj = trans.FindChild("CostObj/MoneyCost1").gameObject;
        mOldImg = trans.FindChild("CostObj/MoneyCost1/Text/bgImg").GetComponent<Image>();
        mOldTxt = trans.FindChild("CostObj/MoneyCost1/Text").GetComponent<Text>();
        mCostNewObj = trans.FindChild("CostObj/MoneyCost2").gameObject;
        mNewImg = trans.FindChild("CostObj/MoneyCost2/Text/bgImg").GetComponent<Image>();
        mNewTxt = trans.FindChild("CostObj/MoneyCost2/Text").GetComponent<Text>();

        buyBtn = trans.FindChild("BuyBtn").GetComponent<Button>();

        nameTxt.text = GameUtils.getString( mShopT.getCommodityName());
        iconBg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + mShopT.getBaseicon());
        iconBg.SetNativeSize();
        iconImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + mShopT.getResourceName());
        iconImg.SetNativeSize();
        detailTxt.text = GameUtils.getString(mShopT.getCommodityDes());
        mOldImg.sprite = GameUtils.GetSpriteByResourceType(mShopT.getCostType());
        mNewImg.sprite = GameUtils.GetSpriteByResourceType(mShopT.getCostType());

        GameUtils.SetImageGrayState(mOldImg, true);

        UpdatePerSecond();

        //iconBtn.onClick.AddListener(OnItemClick);
        buyBtn.onClick.AddListener(OnItemClick);
    }

    void OnItemClick()
    {
        //判断VIP等级是否足够;
        if (mShopT.getVipLimit() > ObjectSelf.GetInstance().VipLevel)
        {
            InterfaceControler.GetInst().AddMsgBox(string.Format(GameUtils.getString("shop_bubble3"), mShopT.getVipLimit()), UI_ShopMgr.inst.transform);
            return;
        }

        //当前金钱够不够买一个的;
        bool isDiscount = ShopModule.IsShopItemInDiscount(mShopT);

        int buyTimes = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(mShopT.getId()).todaynum;

        int costNum = DataTemplate.GetInstance().GetShopBuyCost(mShopT, buyTimes, isDiscount);

        long curCount = -1;
        EM_RESOURCE_TYPE resType = (EM_RESOURCE_TYPE)mShopT.getCostType();
        if (ObjectSelf.GetInstance().TryGetResourceCountById(resType, ref curCount))
        {
            if (curCount >= costNum)
            {
                if (mShopT.getType() != 51)
                {
                    LogManager.LogError("不是金币类型51的物品，所属标签填写错误!" + mShopT.getId());
                    return;
                }

                //打开金币购买界面;
                UI_GoldBuyMgr.SetData(mShopT);
                UI_HomeControler.Inst.AddUI(UI_GoldBuyMgr.UI_ResPath);
            }
            else
            {
                switch (resType)
                {
                    case EM_RESOURCE_TYPE.Gold:
                        //打开魔钻不足提示窗;
                        InterfaceControler.GetInst().ShowGoldNotEnougth(UI_QuikBuyGoldMgr.Inst.transform);
                        break;
                    default:
                        InterfaceControler.GetInst().AddMsgBox("除魔钻资源不足时，其他资源不足先不做处理", UI_ShopMgr.inst.transform);
                        break;
                }
            }
        }
    }

    /// <sumary>
    /// 
    /// </sumary>
    public void UpdatePerSecond()
    {
        bool isDiscount = ShopModule.IsShopItemInDiscount(mShopT);

        mCostOldObj.SetActive(isDiscount);
        mCostNewObj.SetActive(isDiscount);

        int buyTimes = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(mShopT.getId()).todaynum;
        if (isDiscount)
        {
            mOldTxt.text = DataTemplate.GetInstance().GetShopBuyCost(mShopT, buyTimes, false).ToString();
            mNewTxt.text = DataTemplate.GetInstance().GetShopBuyCost(mShopT, buyTimes, true).ToString();
        }
        else
        {
            //临时这么写;
            mCostNewObj.SetActive(true);
            mNewTxt.text = DataTemplate.GetInstance().GetShopBuyCost(mShopT, buyTimes, true).ToString();
        }
    }

    public void Destroy()
    {
        //iconBtn.onClick.RemoveListener(OnItemClick);
        buyBtn.onClick.RemoveListener(OnItemClick);
    }
}

public class UI_QuikBuyGoldMgr : BaseUI
{
    public static readonly string UI_ResPath = "UI_Shop/UI_QuikBuyGold_1_5";

    private static UI_QuikBuyGoldMgr mInst = null;

    GameObject itemObj;
    GameObject itemListObj;
    Text titleTxt;
    Button closeBtn;
    Text closeBtnTxt;

    Image costImg;
    Text costTxt;

    private int resourceId = -1;

    private float mTime = 0f;

    private List<ShopTemplate> mShopTemList = new List<ShopTemplate>();
    private List<QuikBuyGoldItemUI> mChargeUIs = new List<QuikBuyGoldItemUI>();

    public static UI_QuikBuyGoldMgr Inst
    {
        get
        {
            return mInst;
        }
    }

    public override void InitUIData()
    {
        base.InitUIData();

        mInst = this;

        itemObj = transform.FindChild("Items/Item").gameObject;
        itemListObj = transform.FindChild("ItemList/ListObj").gameObject;
        titleTxt = transform.FindChild("Image/Text").GetComponent<Text>();
        closeBtn = transform.FindChild("CloseBtn").GetComponent<Button>();
        closeBtnTxt = transform.FindChild("CloseBtn/Text").GetComponent<Text>();

        costImg = transform.FindChild("MoneyBar/Gold/Image").GetComponent<Image>();
        costTxt = transform.FindChild("MoneyBar/Gold/bg/Text").GetComponent<Text>();

        closeBtn.onClick.AddListener(OnCloseBtnClick);

        mShopTemList = DataTemplate.GetInstance().GetShopTemplatesByTabID(SHOP_TAB.GOLD);

        GameEventDispatcher.Inst.addEventListener(GameEventID.G_Gold_Update, UpdateMoney);
    }

    public override void InitUIView()
    {
        base.InitUIView();

        titleTxt.text = GameUtils.getString("shop_content24");
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


    public override void UpdateUIData()
    {
        base.UpdateUIData();

        mTime += Time.deltaTime;

        if(mTime >= 1f)
        {
            mTime = 0f;
            OneSecHandler();
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

    void OneSecHandler()
    {
        //刷新所有的UI;
        if(mChargeUIs.Count > 0)
        {
            for (int i = 0; i < mChargeUIs.Count; i++ )
            {
                mChargeUIs[i].UpdatePerSecond();
            }
        }

    }

    public override void OnReadyForClose()
    {
        base.OnReadyForClose();

        closeBtn.onClick.RemoveListener(OnCloseBtnClick);

        for (int i = 0; i < mChargeUIs.Count; i++ )
        {
            mChargeUIs[i].Destroy();
        }
        mChargeUIs.Clear();
        mChargeUIs = null;

        mShopTemList.Clear();
        mShopTemList = null;

        mInst = null;
    }

    void OnDestroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_Gold_Update, UpdateMoney);

        UIState = BaseUI.UIStateEnum.ReadyForClose;
    }

    void OnCloseBtnClick()
    {
        UI_HomeControler.Inst.ReMoveUI(UI_ResPath);
    }

    void CreateItem(ShopTemplate mShopT)
    {
        GameObject go = GameObject.Instantiate(itemObj) as GameObject;

        go.transform.parent = itemListObj.transform;
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;

        QuikBuyGoldItemUI chargeUI = new QuikBuyGoldItemUI(go, mShopT);
        mChargeUIs.Add(chargeUI);
    }

    void OnItemClick(GameObject go)
    {
        int shopId = System.Convert.ToInt32( EventTriggerListener.Get(go).param);

        ShopModule.BuyItem(shopId, 1, false);
    }
}
