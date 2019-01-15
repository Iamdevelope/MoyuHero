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

public class CustomShopTemplate : IComparable
{
    public ShopTemplate ShopT;

    public int CompareTo(object obj)
    {
        CustomShopTemplate st = obj as CustomShopTemplate;
        if (st == null)
            return -1;
        return ShopT.getSorting().CompareTo(st.ShopT.getSorting());
    }
}

public class CustomExchangeTemplate : IComparable
{
    public ExchangeTemplate exchangeT;

    public int CompareTo(object obj)
    {
        CustomExchangeTemplate st = obj as CustomExchangeTemplate;
        if (st == null)
            return -1;
        return exchangeT.getSorting().CompareTo(st.exchangeT.getSorting());
    }
}

#region 商城ItemUI基类;
public class UIBaseShopItem
{
    protected Image iconBg;
    protected Image icon;
    protected Button iconBtn;
    protected Button buyBtn;
    protected Text buyBtnTxt;
    protected Image titleBg;
    protected Text title;
    protected Text name;
    protected Text detail;

    protected GameObject mGo;

    protected ShopTemplate shopT;
    protected bool mIsOnSale = true;

    public bool IsOnSale
    {
        get
        {
            return mIsOnSale;
        }
        set
        {
            if(mIsOnSale != value)
            {
                mIsOnSale = value;
                SetActive(value);
            }
        }
    }

    public UIBaseShopItem(GameObject go)
    {
        if(go  == null)
            return;

        mGo = go;

        Transform trans = go.transform;

        iconBg = trans.FindChild("iconBg").GetComponent<Image>();
        icon = trans.FindChild("iconImg").GetComponent<Image>();
        iconBtn = trans.FindChild("iconImg").GetComponent<Button>();
        buyBtn = trans.FindChild("BuyBtn").GetComponent<Button>();
        buyBtnTxt = trans.FindChild("BuyBtn/Text").GetComponent<Text>();
        titleBg = trans.FindChild("TitleBg").GetComponent<Image>();
        title = trans.FindChild("TitleTxt").GetComponent<Text>();
        name = trans.FindChild("NameImg/Text").GetComponent<Text>();
        detail = trans.FindChild("DetailTxt").GetComponent<Text>();

        iconBtn.onClick.AddListener(OnIconClick);
        buyBtn.onClick.AddListener(OnBuyBtnClick);
    }

    public virtual void InitItemInfo(ShopTemplate shopTemplate)
    {
        if (shopTemplate == null)
        {
            LogManager.LogError("ShopTemplate is NULL");
            return;
        }

        shopT = shopTemplate;

        name.text = GameUtils.getString(shopT.getCommodityName());
        detail.text = GameUtils.getString(shopT.getCommodityDes());
        if (!string.IsNullOrEmpty(shopT.getBaseicon()))
        {
            iconBg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + shopT.getBaseicon());
            iconBg.SetNativeSize();
            iconBg.gameObject.SetActive(true);
        }
        else
        {
            iconBg.gameObject.SetActive(false);
        }
        icon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + shopT.getResourceName());
        buyBtnTxt.text = GameUtils.getString("common_button_purchase");
        icon.SetNativeSize();

        //限购商品增加限购次数监听;
        if (ShopModule.IsShopBuyLimited(shopTemplate))
            GameEventDispatcher.Inst.addEventListener(GameEventID.U_RefreshShopInfo, OnShopBuyInfoChange);

        CallPerSecond();
    }

    protected virtual void OnShopBuyInfoChange(object data)
    {

    }

    public void SetParent(Transform parent)
    {
        mGo.transform.SetParent(parent, false);
        //mGo.transform.parent = parent;
        //mGo.transform.localScale = Vector3.one;
        //mGo.transform.localPosition = new Vector3(mGo.transform.localPosition.x, mGo.transform.localPosition.y, 0f);
    }

    protected virtual void OnBuyBtnClick()
    {

    }

    protected virtual void OnIconClick()
    {
        ShopModule.OnShopItemIconClickHandler(shopT);
    }


    public virtual int GetID()
    {
        return shopT.getId();
    }

    public virtual void Destroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.U_RefreshShopInfo, OnShopBuyInfoChange);
        iconBtn.onClick.RemoveAllListeners();
        buyBtn.onClick.RemoveAllListeners();
    }

    //执行刷新逻辑,每秒执行一次;
    public virtual void CallPerSecond()
    {
        //上架下架处理;
        bool onSale = ShopModule.IsShopItemInSaling(shopT);
        //永久限购购买次数是否达到最大;
        bool totalBuyMax = false;
        
        SHOP_LIMIT_TYPE slt = ShopModule.GetShopLimitType(shopT);
        int cur = -1, max = -1, vipAdder = 0;
        switch (slt)
        {
            case SHOP_LIMIT_TYPE.NONE:
                break;
            case SHOP_LIMIT_TYPE.DAILY:
                break;
            case SHOP_LIMIT_TYPE.TOTAL:
                cur = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(shopT.getId()).buyallnum;
                max = shopT.getShelveMaxBuy();
                totalBuyMax = cur >= max;
                break;
        }

        IsOnSale = onSale && !totalBuyMax;

        if (!IsOnSale)
            return;

        //标题处理;
        title.text = ShopModule.GetShopItemTitle(shopT);

        titleBg.gameObject.SetActive(!string.IsNullOrEmpty(title.text));
    }

    public int CompareTo(UIBaseShopItem obj)
    {
        return shopT.getSorting().CompareTo(obj.shopT.getSorting());
    }

    public virtual void SetActive(bool active)
    {
        if (mGo != null && mGo.activeSelf != active) mGo.SetActive(active);
    }
}
#endregion

#region 道具ItemUI类;
public class UIPropShopItem : UIBaseShopItem
{
    protected Text mReminTimesTxt;
    protected GameObject mCostOldObj;
    protected Image mOldImg;
    protected Text mOldTxt;
    protected GameObject mCostNewObj;
    protected Image mNewImg;
    protected Text mNewTxt;

    private bool isBuyBtnGray = false;

    private string limitStr = GameUtils.getString("shop_content9").WithColor("#BBBBBB");
    private StringBuilder msb = new StringBuilder();

    public UIPropShopItem(GameObject go)
        : base(go)
    {
        Transform trans = mGo.transform;

        mReminTimesTxt = trans.FindChild("Bottom/RemineTxt").GetComponent<Text>();
        mCostOldObj = trans.FindChild("Bottom/CostObj/MoneyCost1").gameObject;
        mOldImg = trans.FindChild("Bottom/CostObj/MoneyCost1/Text/bgImg").GetComponent<Image>();
        mOldTxt = trans.FindChild("Bottom/CostObj/MoneyCost1/Text").GetComponent<Text>();
        mCostNewObj = trans.FindChild("Bottom/CostObj/MoneyCost2").gameObject;
        mNewImg = trans.FindChild("Bottom/CostObj/MoneyCost2/Text/bgImg").GetComponent<Image>();
        mNewTxt = trans.FindChild("Bottom/CostObj/MoneyCost2/Text").GetComponent<Text>();

        GameUtils.SetImageGrayState(mOldImg, true);
    }

    public override void InitItemInfo(ShopTemplate item)
    {
        base.InitItemInfo(item);

        mOldImg.sprite = GameUtils.GetSpriteByResourceType(item.getCostType());
        mNewImg.sprite = GameUtils.GetSpriteByResourceType(item.getCostType());

        SHOP_LIMIT_TYPE slt = ShopModule.GetShopLimitType(shopT);
        int cur = -1, max = -1, vipAdder = 0;
        switch (slt)
        {
            case SHOP_LIMIT_TYPE.NONE:
                mReminTimesTxt.gameObject.SetActive(false);
                break;
            case SHOP_LIMIT_TYPE.DAILY:
                cur = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(shopT.getId()).todaynum;
                max = shopT.getDailyMaxBuy();
                switch (shopT.getType())
                {
                    case 11://普通道具;
                        break;
                    case 12://活力补满;
                        VipTemplate vipT = DataTemplate.GetInstance().GetVipTemplateById(ObjectSelf.GetInstance().VipLevel);
                        vipAdder = vipT.getMaxBuyAp();
                        break;
                    case 13://行动力补满;
                        VipTemplate vipT1 = DataTemplate.GetInstance().GetVipTemplateById(ObjectSelf.GetInstance().VipLevel);
                        vipAdder = vipT1.getMaxBuyAp();
                        break;
                }

                max += vipAdder;

                mReminTimesTxt.gameObject.SetActive(true);

                mReminTimesTxt.text = ShopModule.GetRemindTxtStr(msb, limitStr, max-cur).ToString();
                break;
            case SHOP_LIMIT_TYPE.TOTAL:
                cur = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(shopT.getId()).buyallnum;
                max = shopT.getShelveMaxBuy();
                mReminTimesTxt.gameObject.SetActive(true);
                //mReminTimesTxt.text = GameUtils.getString("shop_content9") + (max - cur);
                mReminTimesTxt.text = ShopModule.GetRemindTxtStr(msb, limitStr, max-cur).ToString();;
                break;
        }

    }

    protected override void OnShopBuyInfoChange(object data)
    {
        base.OnShopBuyInfoChange(data);

        Shopbuy sb = data as Shopbuy;

        if(sb == null)
            return;

        if(sb.shopid != shopT.getId())
            return;
        
        int times = shopT.getDailyMaxBuy();

        if(times > 0)
        {
            int vipAdder = 0;
            switch (shopT.getType())
            {
                case 11://普通道具;
                    break;
                case 12://活力补满;
                    VipTemplate vipT = DataTemplate.GetInstance().GetVipTemplateById(ObjectSelf.GetInstance().VipLevel);
                    vipAdder = vipT.getMaxBuyAp();
                    break;
                case 13://行动力补满;
                    VipTemplate vipT1 = DataTemplate.GetInstance().GetVipTemplateById(ObjectSelf.GetInstance().VipLevel);
                    vipAdder = vipT1.getMaxBuyAp();
                    break;
            }
            times += vipAdder;
            
            //mReminTimesTxt.text = GameUtils.getString("shop_content9") + (times - sb.todaynum);
            mReminTimesTxt.text = ShopModule.GetRemindTxtStr(msb, limitStr, times - sb.todaynum).ToString();
        }
        else
        {
            times = shopT.getShelveMaxBuy();
            if(times > 0)
            {
                //mReminTimesTxt.text = GameUtils.getString("shop_content9") + (times - sb.buyallnum);
                mReminTimesTxt.text = ShopModule.GetRemindTxtStr(msb, limitStr, times - sb.buyallnum).ToString(); 
            }
        }
    }

    public override void CallPerSecond()
    {
        base.CallPerSecond();

        //是否打折;----------------------------可以抽出到父类--商品打折状态改变的一个事件;
        //TODO::
        bool isDiscount = ShopModule.IsShopItemInDiscount(shopT);

        mCostOldObj.SetActive(isDiscount);
        mCostNewObj.SetActive(isDiscount);

        int buyTimes = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(shopT.getId()).todaynum;
        if(isDiscount)
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

        switch (shopT.getType())
        {
            case 11://普通道具;
                break;
            case 12://活力补满;
                //1.是否满活力;
                bool isGray = false;
                //2.是否有购买次数;
                int maxDayliTimes = DataTemplate.GetInstance().GetShopItemDailyBuyTimes(shopT, ObjectSelf.GetInstance().VipLevel);
                isGray = buyTimes >= maxDayliTimes || ObjectSelf.GetInstance().IsFullActionPoint;
                
                if (isGray != isBuyBtnGray)
                {
                    isBuyBtnGray = isGray;
                    GameUtils.SetBtnSpriteGrayState(buyBtn, isGray);
                }
                break;
            case 13://行动力补满;
                bool isGray1 = false;
                //2.是否有购买次数;
                int maxDayliTimes1 = DataTemplate.GetInstance().GetShopItemDailyBuyTimes(shopT, ObjectSelf.GetInstance().VipLevel);
                isGray1 = buyTimes >= maxDayliTimes1 || ObjectSelf.GetInstance().IsFullExplorePoint;
                
                if (isGray1 != isBuyBtnGray)
                {
                    isBuyBtnGray = isGray1;
                    GameUtils.SetBtnSpriteGrayState(buyBtn, isGray1);
                }
                break;
        }
    }

    public void SetReminTimes(int times)
    {
        //算出剩余购买次数;
        mReminTimesTxt.text = "";

    }

    protected override void OnBuyBtnClick()
    {
        if (!UI_ShopMgr.CanBuyItem)
            return;

        //判断VIP等级是否足够;
        if (shopT.getVipLimit() > ObjectSelf.GetInstance().VipLevel)
        {
            InterfaceControler.GetInst().AddMsgBox(string.Format(GameUtils.getString("shop_bubble3"), shopT.getVipLimit()), UI_ShopMgr.inst.transform);
            return;
        }

        //当前金钱够不够买一个的;
        bool isDiscount = ShopModule.IsShopItemInDiscount(shopT);
        int buyTimes = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(shopT.getId()).todaynum;
        int costNum = DataTemplate.GetInstance().GetShopBuyCost(shopT, buyTimes, isDiscount);

        long curCount = -1;
        EM_RESOURCE_TYPE resType = (EM_RESOURCE_TYPE)shopT.getCostType();
        if (ObjectSelf.GetInstance().TryGetResourceCountById(resType, ref curCount))
        {
            if (curCount >= costNum)
            {
                switch (shopT.getType())
                {
                    case 11://普通道具;
                        //道具购买界面;
                        UI_ShopMgr.inst.OpenItemBuyUI(shopT);
                        break;
                    case 12://活力补满;
                        ////1.是否满活力;
                        if (ObjectSelf.GetInstance().IsFullActionPoint)
                        {
                            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("shop_bubble5"), UI_ShopMgr.inst.transform);
                            return;
                        }
                        //2.是否有购买次数;
                        int maxDayliTimes = DataTemplate.GetInstance().GetShopItemDailyBuyTimes(shopT, ObjectSelf.GetInstance().VipLevel);
                        if (buyTimes >= maxDayliTimes)
                        {
                            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("UI_raids_tips2"), UI_ShopMgr.inst.transform);
                            return;
                        }

                        //成功后弹窗;
                        ShowBuyWater(isDiscount, costNum, GameUtils.getString("vigour_buy_title"));
                        ////3.发送购买消息;
                        //ShopModule.BuyItem(shopT.getId(), buyTimes, false);
                        break;
                    case 13://行动力补满;
                        //TODO:
                        //行动力已满
                        if (ObjectSelf.GetInstance().IsFullExplorePoint)
                        {
                            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("explore_bubble2"), UI_ShopMgr.inst.transform);
                            return;
                        }
                        //2.是否有购买次数;
                        int maxDayliTimes_ExplorePoin = DataTemplate.GetInstance().GetShopItemDailyBuyTimes(shopT, ObjectSelf.GetInstance().VipLevel);
                        if (buyTimes >= maxDayliTimes_ExplorePoin)
                        {
                            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("UI_raids_tips2"), UI_ShopMgr.inst.transform);
                            return;
                        }
                        ShowBuyWater(isDiscount, costNum, GameUtils.getString("explore_cotnent19"));
                        //3.发送购买消息;
                        //ShopModule.BuyItem(shopT.getId(), buyTimes, false);
                        break;
                    default:
                        LogManager.LogError("道具标签非法的处理商品类型shopid=" + shopT.getId());
                        break;
                }

            }
            else
            {
                switch (resType)
                {
                    case EM_RESOURCE_TYPE.Gold:
                        //打开魔钻不足提示窗;
                        InterfaceControler.GetInst().ShowGoldNotEnougth(UI_ShopMgr.inst.transform);
                        break;
                    default:
                        InterfaceControler.GetInst().AddMsgBox("除魔钻资源不足时，其他资源不足先不做处理", UI_ShopMgr.inst.transform);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// 购买体力或者活力药剂提示;
    /// </summary>
    /// <param name="itemName"></param>
    /// <param name="buyTimes"></param>
    /// <param name="isDiscount"></param>
    void ShowBuyWater(bool isDiscount, int costNum,string descText)
    {
        UI_RechargeBox box = UI_HomeControler.Inst.AddUI(UI_RechargeBox.UI_ResPath).GetComponent<UI_RechargeBox>();

        if (box == null)
        {
            LogManager.LogError("提示窗is null");
            return;
        }

        box.SetDescription_text(descText);
        box.SetIsNeedDescription(true);
        box.SetConsume_Image(GameUtils.GetSpriteByResourceType(shopT.getCostType()));
        box.SetConNum(costNum.ToString());
        box.SetLeftBtn_text(GameUtils.getString("common_button_purchase"));
        box.SetLeftClick(() =>
        {
            ShopModule.BuyItem(shopT.getId(), 1, isDiscount);
            box.OnCloes();
        });

        box.SetRightBtn_text(GameUtils.getString("common_button_close"));
    }

    void OpenItemUI(ItemTemplate itemT)
    {
        //GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_OpenUI, "");
        UI_Item.SetItemTemplate(itemT);
        UI_HomeControler.Inst.AddUI(UI_Item.UI_ResPath);
        //if(UI_Item._instance != null)
        //{

        //}
        //else
        //{
        //    LogManager.LogError("通用道具展示界面打开失败！");
        //    return;
        //}
    }

    public override void Destroy()
    {
        base.Destroy();

    }
}
#endregion

#region 礼包ItemUI类;
public class UIGiftShopItem : UIBaseShopItem
{
    protected Text mReminTimesTxt;
    protected GameObject mCostOldObj;
    protected Image mOldImg;
    protected Text mOldTxt;
    protected GameObject mCostNewObj;
    protected Image mNewImg;
    protected Text mNewTxt;

    private string limitStr = GameUtils.getString("shop_content9").WithColor("#BBBBBB");
    private StringBuilder msb = new StringBuilder();

    public UIGiftShopItem(GameObject go)
        : base(go)
    {
        Transform trans = mGo.transform;

        mReminTimesTxt = trans.FindChild("Bottom/RemineTxt").GetComponent<Text>();
        mCostOldObj = trans.FindChild("Bottom/CostObj/MoneyCost1").gameObject;
        mOldImg = trans.FindChild("Bottom/CostObj/MoneyCost1/Text/bgImg").GetComponent<Image>();
        mOldTxt = trans.FindChild("Bottom/CostObj/MoneyCost1/Text").GetComponent<Text>();
        mCostNewObj = trans.FindChild("Bottom/CostObj/MoneyCost2").gameObject;
        mNewImg = trans.FindChild("Bottom/CostObj/MoneyCost2/Text/bgImg").GetComponent<Image>();
        mNewTxt = trans.FindChild("Bottom/CostObj/MoneyCost2/Text").GetComponent<Text>();

        GameUtils.SetImageGrayState(mOldImg, true);
    }

    public override void InitItemInfo(ShopTemplate item)
    {
        base.InitItemInfo(item);

        mOldImg.sprite = GameUtils.GetSpriteByResourceType(item.getCostType());
        mNewImg.sprite = GameUtils.GetSpriteByResourceType(item.getCostType());

        SHOP_LIMIT_TYPE slt = ShopModule.GetShopLimitType(shopT);
        int cur = -1, max = -1;
        switch (slt)
        {
            case SHOP_LIMIT_TYPE.NONE:
                mReminTimesTxt.gameObject.SetActive(false);
                break;
            case SHOP_LIMIT_TYPE.DAILY:
                cur = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(shopT.getId()).todaynum;
                max = shopT.getDailyMaxBuy();
                mReminTimesTxt.gameObject.SetActive(true);
                //mReminTimesTxt.text = GameUtils.getString("shop_content9") + (max - cur);
                mReminTimesTxt.text = ShopModule.GetRemindTxtStr(msb, limitStr, max - cur).ToString();
                break;
            case SHOP_LIMIT_TYPE.TOTAL:
                cur = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(shopT.getId()).buyallnum;
                max = shopT.getShelveMaxBuy();
                mReminTimesTxt.gameObject.SetActive(true);
                //mReminTimesTxt.text = GameUtils.getString("shop_content9") + (max - cur);
                mReminTimesTxt.text = ShopModule.GetRemindTxtStr(msb, limitStr, max - cur).ToString();
                
                IsOnSale = cur < max;
                break;
        }
    }

    protected override void OnShopBuyInfoChange(object data)
    {
        base.OnShopBuyInfoChange(data);

        Shopbuy sb = data as Shopbuy;

        if (sb == null)
            return;

        if (sb.shopid != shopT.getId())
            return;

        int times = shopT.getDailyMaxBuy();
        if (times > 0)
        {
            //mReminTimesTxt.text = GameUtils.getString("shop_content9") +(times - sb.todaynum);
            mReminTimesTxt.text = ShopModule.GetRemindTxtStr(msb, limitStr, times - sb.todaynum).ToString();
        }
        else
        {
            times = shopT.getShelveMaxBuy();
            if (times > 0)
            {
                //mReminTimesTxt.text = "总" + (times - sb.buyallnum);
                mReminTimesTxt.text = ShopModule.GetRemindTxtStr(msb, limitStr, times - sb.buyallnum).ToString();
            }
        }
    }

    public override void CallPerSecond()
    {
        base.CallPerSecond();
        
        if (!IsOnSale)
            return;


        //是否打折;----------------------------可以抽出到父类--商品打折状态改变的一个事件;
        //TODO::
        bool isDiscount = ShopModule.IsShopItemInDiscount(shopT);

        mCostOldObj.SetActive(isDiscount);
        mCostNewObj.SetActive(isDiscount);

        int buyTimes = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(shopT.getId()).todaynum;
        
        if(buyTimes > 0)
        {
            if(IsOnSale == true)
            {
                IsOnSale = false;
            }
        }
        
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
    }

    public void SetReminTimes(int times)
    {
        //算出剩余购买次数;
        mReminTimesTxt.text = "";

    }

    protected override void OnBuyBtnClick()
    {
        if (!UI_ShopMgr.CanBuyItem)
            return;

        //判断VIP等级是否足够;
        if (shopT.getVipLimit() > ObjectSelf.GetInstance().VipLevel)
        {
            InterfaceControler.GetInst().AddMsgBox(string.Format(GameUtils.getString("shop_bubble3"), shopT.getVipLimit()), UI_ShopMgr.inst.transform);
            return;
        }

        //当前金钱够不够买一个的;
        bool isDiscount = ShopModule.IsShopItemInDiscount(shopT);
        int buyTimes = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(shopT.getId()).todaynum;
        int costNum = DataTemplate.GetInstance().GetShopBuyCost(shopT, buyTimes, isDiscount);

        long curCount = -1;
        EM_RESOURCE_TYPE resType = (EM_RESOURCE_TYPE)shopT.getCostType();
        if (ObjectSelf.GetInstance().TryGetResourceCountById(resType, ref curCount))
        {
            if (curCount >= costNum)
            {
                switch (shopT.getType())
                {
                    case 21://普通礼包;
                        //1.判断是否有购买次数;
                        bool isLimit = ShopModule.IsShopBuyLimited(shopT);
                        if (isLimit)
                        {
                            SHOP_LIMIT_TYPE slt = ShopModule.GetShopLimitType(shopT);
                            int cur = -1, max = -1;
                            switch (slt)
                            {
                                case SHOP_LIMIT_TYPE.NONE:
                                    break;
                                case SHOP_LIMIT_TYPE.DAILY:
                                    cur = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(shopT.getId()).todaynum;
                                    max = shopT.getDailyMaxBuy();
                                    break;
                                case SHOP_LIMIT_TYPE.TOTAL:
                                    cur = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(shopT.getId()).buyallnum;
                                    max = shopT.getShelveMaxBuy();
                                    break;
                            }

                            if (cur >= max)
                            {
                                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("UI_raids_tips2"), UI_ShopMgr.inst.transform);
                                return;
                            }
                        }

                        UI_GoldBuyMgr.SetData(shopT);
                        UI_HomeControler.Inst.AddUI(UI_GoldBuyMgr.UI_ResPath);
                        break;
                    case 22://VIP礼包;
                        //1.判断是否有购买次数;
                        bool isLimit1 = ShopModule.IsShopBuyLimited(shopT);
                        if (isLimit1)
                        {
                            SHOP_LIMIT_TYPE slt1 = ShopModule.GetShopLimitType(shopT);
                            int cur = -1, max = -1;
                            switch (slt1)
                            {
                                case SHOP_LIMIT_TYPE.NONE:
                                    break;
                                case SHOP_LIMIT_TYPE.DAILY:
                                    cur = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(shopT.getId()).todaynum;
                                    max = shopT.getDailyMaxBuy();
                                    break;
                                case SHOP_LIMIT_TYPE.TOTAL:
                                    cur = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(shopT.getId()).buyallnum;
                                    max = shopT.getShelveMaxBuy();
                                    break;
                            }

                            if (cur >= max)
                            {
                                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("UI_raids_tips2"), UI_ShopMgr.inst.transform);
                                return;
                            }
                        }

                        UI_GoldBuyMgr.SetData(shopT);
                        UI_HomeControler.Inst.AddUI(UI_GoldBuyMgr.UI_ResPath);
                        break;
                    default:
                        LogManager.LogError("礼包标签非法的处理商品类型shopid=" + shopT.getId());
                        break;
                }

            }
            else
            {
                switch (resType)
                {
                    case EM_RESOURCE_TYPE.Gold:
                        //打开魔钻不足提示窗;
                        InterfaceControler.GetInst().ShowGoldNotEnougth(UI_ShopMgr.inst.transform);
                        break;
                    default:
                        InterfaceControler.GetInst().AddMsgBox("除魔钻资源不足时，其他资源不足先不做处理", UI_ShopMgr.inst.transform);
                        break;
                }
            }
        }
    }

    void OpenItemUI(ItemTemplate itemT)
    {
        //GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_OpenUI, "");
        UI_Item.SetItemTemplate(itemT);
        UI_HomeControler.Inst.AddUI(UI_Item.UI_ResPath);
        //if(UI_Item._instance != null)
        //{

        //}
        //else
        //{
        //    LogManager.LogError("通用道具展示界面打开失败！");
        //    return;
        //}
    }

    public override void Destroy()
    {
        base.Destroy();

    }
}
#endregion

#region 皮肤ItemUI类;
public class UISkinShopItem : UIBaseShopItem
{
    protected GameObject mHaveObj;
    protected Text mHaveTxt;
    protected GameObject mCostOldObj;
    protected Image mOldImg;
    protected Text mOldTxt;
    protected GameObject mCostNewObj;
    protected Image mNewImg;
    protected Text mNewTxt;
    protected Text mNoEffTxt;
    protected GameObject mListObj;
    protected GameObject mAttriObj;
    
    public UISkinShopItem(GameObject go)
        : base(go)
    {
        Transform trans = mGo.transform;

        mHaveObj = trans.FindChild("HaveDone").gameObject;
        mHaveTxt = trans.FindChild("HaveDone/Text").GetComponent<Text>();
        mCostOldObj = trans.FindChild("Bottom/CostObj/MoneyCost1").gameObject;
        mOldImg = trans.FindChild("Bottom/CostObj/MoneyCost1/Text/bgImg").GetComponent<Image>();
        mOldTxt = trans.FindChild("Bottom/CostObj/MoneyCost1/Text").GetComponent<Text>();
        mCostNewObj = trans.FindChild("Bottom/CostObj/MoneyCost2").gameObject;
        mNewImg = trans.FindChild("Bottom/CostObj/MoneyCost2/Text/bgImg").GetComponent<Image>();
        mNewTxt = trans.FindChild("Bottom/CostObj/MoneyCost2/Text").GetComponent<Text>();
        mNoEffTxt = trans.FindChild("NoEffTxt").GetComponent<Text>();
        mListObj = trans.FindChild("Attris").gameObject;
        mAttriObj = trans.FindChild("Items/AttriPair").gameObject;


        mHaveTxt.text = GameUtils.getString("shop_content29");
        mNoEffTxt.text = GameUtils.getString("no_effect_text");

        GameUtils.SetImageGrayState(mOldImg, true);
    }

    public override void InitItemInfo(ShopTemplate item)
    {
        base.InitItemInfo(item);

        mOldImg.sprite = GameUtils.GetSpriteByResourceType(item.getCostType());
        mNewImg.sprite = GameUtils.GetSpriteByResourceType(item.getCostType());

        //皮肤属性显示;
        ArtresourceTemplate artT = DataTemplate.GetInstance().GetArtResourceTemplate(GameUtils.StringToInt(item.getPara()));
        if(artT == null)
        {
            LogManager.LogError("时装表数据is null id=" + item.getPara());
            return;
        }

        GameUtils.DestroyChildsObj(mListObj);

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

        mNoEffTxt.gameObject.SetActive(count <= 0);

        mHaveObj.SetActive(ObjectSelf.GetInstance().IsHaveSkin(GameUtils.StringToInt(shopT.getPara())));
    }

    void CreateAttriItem(string name, string val)
    {
        GameObject go = GameObject.Instantiate(mAttriObj) as GameObject;
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

        trans.parent = mListObj.transform;
        trans.localScale = Vector3.one;
        trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y, 0f);
    }

    public override void CallPerSecond()
    {
        base.CallPerSecond();

        //根据商品打折期间，和购买次数判断显示;
        //TODO::
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
    }

    protected override void OnShopBuyInfoChange(object data)
    {
        if (data == null)
            return;

        Shopbuy sb = data as Shopbuy;

        if(sb != null && sb.shopid == shopT.getId())
        {
            mHaveObj.SetActive(sb.buyallnum > 0);
        }
    }

    public override void Destroy()
    {
        base.Destroy();

    }

    protected override void OnBuyBtnClick()
    {
        if (!UI_ShopMgr.CanBuyItem)
            return;
        
        //是否拥有该时装;
        if (ObjectSelf.GetInstance().IsHaveSkin(GameUtils.StringToInt(shopT.getPara())))
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("shop_bubble4"), UI_ShopMgr.inst.transform);
            return;
        }

        //判断VIP等级是否足够;
        if (shopT.getVipLimit() > ObjectSelf.GetInstance().VipLevel)
        {
            InterfaceControler.GetInst().AddMsgBox(string.Format(GameUtils.getString("shop_bubble3"), shopT.getVipLimit()), UI_ShopMgr.inst.transform);
            return;
        }

        //当前金钱够不够买一个的;
        bool isDiscount = ShopModule.IsShopItemInDiscount(shopT);

        int buyTimes = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(shopT.getId()).todaynum;

        int costNum = DataTemplate.GetInstance().GetShopBuyCost(shopT, buyTimes, isDiscount);

        long curCount = -1;
        EM_RESOURCE_TYPE resType = (EM_RESOURCE_TYPE)shopT.getCostType();
        if (ObjectSelf.GetInstance().TryGetResourceCountById(resType, ref curCount))
        {
            if (curCount >= costNum)
            {
                if(shopT.getType() != 31)
                {
                    LogManager.LogError("不是普通时装类型31的物品，所属标签填写错误!" + shopT.getId());
                    return;
                }
                //打开时装购买界面;
                UI_SkinBuyMgr.SetShowShopTemplate(shopT);
                UI_HomeControler.Inst.AddUI(UI_SkinBuyMgr.UI_ResPath);
            }
            else
            {
                switch (resType)
                {
                    case EM_RESOURCE_TYPE.Gold:
                        //打开魔钻不足提示窗;
                        InterfaceControler.GetInst().ShowGoldNotEnougth(UI_ShopMgr.inst.transform);
                        break;
                    default:
                        InterfaceControler.GetInst().AddMsgBox("除魔钻资源不足时，其他资源不足先不做处理", UI_ShopMgr.inst.transform);
                        break;
                }
            }
        }
    }
}
#endregion

#region 充值ItemUI类;
public class UIChargeShopItem
{
    protected Text mDetailTxt; //加送25% 提示文字;
    protected Text mCostTypeTxt;
    protected Text mCostNumTxt;
    protected Image iconBg;
    protected Image icon;
    protected Button iconBtn;
    protected Button buyBtn;
    protected Text buyBtnTxt;
    protected Image titleBg;
    protected Text title;
    protected Text name;
    protected Text detail;
    protected GameObject m_MonthCardGo;  //月卡gameobject  月卡的显示和普通item不一样 需要特殊处理
    protected Text m_MonthCardName;     //月卡名字
    protected Image m_MonthCardIcon;     //月卡图标
    protected Button m_MonthCardIconBt;

    protected GameObject mGo;

    private ExchangeTemplate exchangeT;

    public UIChargeShopItem(GameObject go)
    {
        if (go == null)
            return;

        mGo = go;

        Transform trans = go.transform;

        mDetailTxt = trans.FindChild("DetailObj/Text").GetComponent<Text>();
        mCostTypeTxt = trans.FindChild("MoneyCost/TypeTxt").GetComponent<Text>();
        mCostNumTxt = trans.FindChild("MoneyCost/Text").GetComponent<Text>();

        iconBg = trans.FindChild("iconBg").GetComponent<Image>();
        icon = trans.FindChild("iconImg").GetComponent<Image>();
        iconBtn = trans.FindChild("iconImg").GetComponent<Button>();
        buyBtn = trans.FindChild("BuyBtn").GetComponent<Button>();
        buyBtnTxt = trans.FindChild("BuyBtn/Text").GetComponent<Text>();
        titleBg = trans.FindChild("TitleBg").GetComponent<Image>();
        title = trans.FindChild("TitleTxt").GetComponent<Text>();
        name = trans.FindChild("NameImg/Text").GetComponent<Text>();
        detail = trans.FindChild("DetailTxt").GetComponent<Text>();
        m_MonthCardGo = trans.FindChild("yueka").gameObject;
        m_MonthCardIcon = trans.FindChild("yueka/iconImg").GetComponent<Image>();
        m_MonthCardName = trans.FindChild("yueka/Name").GetComponent<Text>();
        m_MonthCardIconBt = trans.FindChild("yueka/iconImg").GetComponent<Button>();
        m_MonthCardGo.SetActive(false);
        iconBtn.onClick.AddListener(OnIconClick);
        m_MonthCardIconBt.onClick.AddListener(OnIconClick);
        buyBtn.onClick.AddListener(OnBuyBtnClick);
    }

    public void InitItemInfo(ExchangeTemplate data)
    {
        if (data == null)
        {
            LogManager.LogError("ExchangeTemplate is NULL");
            return;
        }

        exchangeT = data;
        title.text = GameUtils.getString(exchangeT.getTitle());
        titleBg.gameObject.SetActive(!string.IsNullOrEmpty(title.text));
        detail.text = GameUtils.getString(exchangeT.getDetail());
        if (!string.IsNullOrEmpty(exchangeT.getBaseicon()))
        {
            iconBg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + exchangeT.getBaseicon());
            iconBg.SetNativeSize();
            iconBg.gameObject.SetActive(true);
        }
        else
        {
            iconBg.gameObject.SetActive(false);
        }
        if (data.getPreviewType() == 1)//月卡
        {
            m_MonthCardGo.SetActive(true);
            m_MonthCardIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + exchangeT.getIcon());
            m_MonthCardName.text = GameUtils.getString(exchangeT.getName());
            icon.gameObject.SetActive(false);
            name.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            icon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + exchangeT.getIcon());
            name.text = GameUtils.getString(exchangeT.getName());
            icon.gameObject.SetActive(true);
            name.transform.parent.gameObject.SetActive(true);
            m_MonthCardGo.SetActive(false);
        }
       
        buyBtnTxt.text = GameUtils.getString("common_button_purchase");
        icon.SetNativeSize();
        if (data.getPreviewType() == 1)//月卡
        {
            icon.transform.localScale = Vector2.one * 0.8f;
        }
        else
        {
            icon.transform.localScale = Vector2.one;
        }

        //mDetailTxt.text = GameUtils.getString(exchangeT.getDetail());
        mDetailTxt.text = "";
        mCostTypeTxt.text = ExchangeModule.Money_Str;
        mCostNumTxt.text = exchangeT.getPrice().ToString();
    }

    public void SetParent(Transform parent)
    {
        mGo.transform.parent = parent;
        mGo.transform.localScale = Vector3.one;
        mGo.transform.localPosition = new Vector3(mGo.transform.localPosition.x, mGo.transform.localPosition.y, 0f);
    }

    public void Destroy()
    {

    }

    protected void OnBuyBtnClick()
    {
        ExchangeModule.ChargeMoney(exchangeT.getId());
    }

    protected void OnIconClick()
    {
        switch(exchangeT.getPreviewType())
        {
            case 1:
                ShowYUEKAPreviewUI(exchangeT);
                break;
            default:
                break;
        }
    }

    bool ShowYUEKAPreviewUI(ExchangeTemplate exchangeT)
    {
        UI_YueKaPreviewMgr.SetExchangeTemplate(exchangeT);
        UI_HomeControler.Inst.AddUI(UI_YueKaPreviewMgr.UI_ResPath);

        return true;
    }

}
#endregion

#region 金币ItemUI类;
public class UIGoldShopItem : UIBaseShopItem
{
    protected Text mDetailTxt;  //加送 文字提示，可能会用图文混排;
    protected Text mReminTimesTxt;
    protected GameObject mCostOldObj;
    protected Image mOldImg;
    protected Text mOldTxt;
    protected GameObject mCostNewObj;
    protected Image mNewImg;
    protected Text mNewTxt;

    public UIGoldShopItem(GameObject go)
        : base(go)
    {
        Transform trans = mGo.transform;

        mDetailTxt = trans.FindChild("DetailObj/Text").GetComponent<Text>();
        mReminTimesTxt = trans.FindChild("RemineTxt").GetComponent<Text>();
        mCostOldObj = trans.FindChild("Bottom/CostObj/MoneyCost1").gameObject;
        mOldImg = trans.FindChild("Bottom/CostObj/MoneyCost1/Text/bgImg").GetComponent<Image>();
        mOldTxt = trans.FindChild("Bottom/CostObj/MoneyCost1/Text").GetComponent<Text>();
        mCostNewObj = trans.FindChild("Bottom/CostObj/MoneyCost2").gameObject;
        mNewImg = trans.FindChild("Bottom/CostObj/MoneyCost2/Text/bgImg").GetComponent<Image>();
        mNewTxt = trans.FindChild("Bottom/CostObj/MoneyCost2/Text").GetComponent<Text>();

        GameUtils.SetImageGrayState(mOldImg, true);
    }

    public override void InitItemInfo(ShopTemplate item)
    {
        base.InitItemInfo(item);

        mReminTimesTxt.gameObject.SetActive(false);

        mDetailTxt.text = GameUtils.getString(item.getContenttag());
        mOldImg.sprite = GameUtils.GetSpriteByResourceType(item.getCostType());
        mNewImg.sprite = GameUtils.GetSpriteByResourceType(item.getCostType());
    }

    public override void CallPerSecond()
    {
        base.CallPerSecond();

        //是否打折;----------------------------可以抽出到父类--商品打折状态改变的一个事件;
        //TODO::
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
            mNewTxt.text = DataTemplate.GetInstance().GetShopBuyCost(shopT, buyTimes, true).ToString();
        }
    }

    protected override void OnBuyBtnClick()
    {
        if (!UI_ShopMgr.CanBuyItem)
            return;

        //判断VIP等级是否足够;
        if (shopT.getVipLimit() > ObjectSelf.GetInstance().VipLevel)
        {
            InterfaceControler.GetInst().AddMsgBox(string.Format(GameUtils.getString("shop_bubble3"), shopT.getVipLimit()), UI_ShopMgr.inst.transform);
            return;
        }

        //当前金钱够不够买一个的;
        bool isDiscount = ShopModule.IsShopItemInDiscount(shopT);

        int buyTimes = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(shopT.getId()).todaynum;

        int costNum = DataTemplate.GetInstance().GetShopBuyCost(shopT, buyTimes, isDiscount);

        long curCount = -1;
        EM_RESOURCE_TYPE resType = (EM_RESOURCE_TYPE)shopT.getCostType();
        if (ObjectSelf.GetInstance().TryGetResourceCountById(resType, ref curCount))
        {
            if (curCount >= costNum)
            {
                if (shopT.getType() != 51)
                {
                    LogManager.LogError("不是金币类型51的物品，所属标签填写错误!" + shopT.getId());
                    return;
                }

                //打开金币购买界面;
                UI_GoldBuyMgr.SetData(shopT);
                UI_HomeControler.Inst.AddUI(UI_GoldBuyMgr.UI_ResPath);
            }
            else
            {
                switch (resType)
                {
                    case EM_RESOURCE_TYPE.Gold:
                        //打开魔钻不足提示窗;
                        InterfaceControler.GetInst().ShowGoldNotEnougth(UI_ShopMgr.inst.transform);
                        break;
                    default:
                        InterfaceControler.GetInst().AddMsgBox("除魔钻资源不足时，其他资源不足先不做处理", UI_ShopMgr.inst.transform);
                        break;
                }
            }
        }

    }
}
#endregion

public class UI_ShopMgr : CustomUI
{
    public const string UI_ResPath = "UI_Shop/UI_Shop_02_05";
    public const float REFRESH_INTERVAL = 1.0f;

    public static UI_ShopMgr inst = null;

    public static bool CanBuyItem = true;

    public GameObject[] NorTabBtnObjs;
    public GameObject[] SelectTabBtnObjs;
    public Sprite[] VipNumImgs;
    //left Panel;
    protected ToggleGroup mToggleGroup;
    protected Toggle[] mToggles;
    //protected Image[] mToggleImgs;
    protected Text[] mToggleTxts1;
    protected Text[] mToggleTxts2;

    //title panel;
    protected Text mTitleTxt1;
    protected Text mTitleTxt2;
    
    protected Button mReturnBtn;

    #region 广告部分;
    protected GameObject mAdvertisementObj;
    protected GameObject mAdImgList;
    protected GameObject mAdImgItem;
    #endregion

    #region 道具;
    protected GameObject mUIProp;
    protected GameObject mPropListObj;
    protected GameObject mPropItem;
    #endregion

    #region 礼包;
    protected GameObject mUIGift;
    protected GameObject mGiftListObj;
    protected GameObject mGiftItem;
    #endregion

    #region 皮肤;
    protected GameObject mUISkin;
    protected GameObject mSkinListObj;
    protected GameObject mSkinItem;
    #endregion

    #region 充值界面;
    //Charge panel;
    protected GameObject mUICharge;
    protected GameObject mChargeObj;
    protected Image mVipNumImg1;
    protected Image mVipNumImg2;
    protected Text mVipLvTxt;
    protected Slider mVipSlider;
    protected GameObject mFillAreaObj;
    protected Text mVipExpTxt;
    protected Text mVipExpTotalTxt;
    protected Button mVipPrivBtn;
    protected Text mVipPrivBtnTxt;
    protected Text mVipNextHintTxt;   //升到下一级...
    protected Image mNextVipNumImg1;  //升到下一级...
    protected Image mNextVipNumImg2;  //升到下一级...
    protected GameObject mNextVipObj;
    protected GameObject mChargeListObj;
    protected GameObject mChargeItem;
    #endregion

    #region 金币;
    protected GameObject mUIGold;
    protected GameObject mGoldListObj;
    protected GameObject mGoldItem;
    #endregion


    private float mCurTime = 0;

    bool[] mInitDone;

    List<CustomShopTemplate> mPropList = new List<CustomShopTemplate>();
    List<CustomShopTemplate> mGiftList = new List<CustomShopTemplate>();
    List<CustomShopTemplate> mSkinList = new List<CustomShopTemplate>();
    List<CustomExchangeTemplate> mChargeList = new List<CustomExchangeTemplate>();
    List<CustomShopTemplate> mGoldList = new List<CustomShopTemplate>();

    private Dictionary<int, UIPropShopItem> mPropItemsDic = new Dictionary<int, UIPropShopItem>();
    private Dictionary<int, UIGiftShopItem> mGiftItemsDic = new Dictionary<int, UIGiftShopItem>();
    private Dictionary<int, UISkinShopItem> mSkinItemsDic = new Dictionary<int, UISkinShopItem>();
    private Dictionary<int, UIChargeShopItem> mChargeItemsDic = new Dictionary<int, UIChargeShopItem>();
    private Dictionary<int, UIGoldShopItem> mGoldItemsDic = new Dictionary<int, UIGoldShopItem>();

    private static SHOP_TAB m_ShopTab = SHOP_TAB.PROP;

    private GameObject m_TipsImage;
    private IFunctionTipsController m_TipsController;
    public override void InitUIData()
    {
        CanBuyItem = true;

        if(NorTabBtnObjs == null || NorTabBtnObjs.Length != 5 || SelectTabBtnObjs == null || SelectTabBtnObjs.Length != 5)
        {
            LogManager.LogError("UI_Shop 数据丢失");
            return;
        }

        inst = this;

        captionPath = "TopPanel/GongGaoPos";

        //同步服务器时间;
        TimeUtils.SyncServerTime();

        mToggles = new Toggle[5];

        mToggles[0] = transform.FindChild("LeftPanel/PropBtn/Button").GetComponent<Toggle>();
        mToggles[1] = transform.FindChild("LeftPanel/GiftBtn/Button").GetComponent<Toggle>();
        mToggles[2] = transform.FindChild("LeftPanel/SkinBtn/Button").GetComponent<Toggle>();
        mToggles[3] = transform.FindChild("LeftPanel/ChargeBtn/Button").GetComponent<Toggle>();
        mToggles[4] = transform.FindChild("LeftPanel/GoldBtn/Button").GetComponent<Toggle>();

        //mToggleImgs = new Image[5];
        //mToggleImgs[0] = transform.FindChild("LeftPanel/PropBtn/Button").GetComponent<Image>();
        //mToggleImgs[1] = transform.FindChild("LeftPanel/GiftBtn/Button").GetComponent<Image>();
        //mToggleImgs[2] = transform.FindChild("LeftPanel/SkinBtn/Button").GetComponent<Image>();
        //mToggleImgs[3] = transform.FindChild("LeftPanel/ChargeBtn/Button").GetComponent<Image>();
        //mToggleImgs[4] = transform.FindChild("LeftPanel/GoldBtn/Button").GetComponent<Image>();

        mToggleTxts1 = new Text[5];
        mToggleTxts1[0] = transform.FindChild("LeftPanel/PropBtn/Obj1/Text").GetComponent<Text>();
        mToggleTxts1[1] = transform.FindChild("LeftPanel/GiftBtn/Obj1/Text").GetComponent<Text>();
        mToggleTxts1[2] = transform.FindChild("LeftPanel/SkinBtn/Obj1/Text").GetComponent<Text>();
        mToggleTxts1[3] = transform.FindChild("LeftPanel/ChargeBtn/Obj1/Text").GetComponent<Text>();
        mToggleTxts1[4] = transform.FindChild("LeftPanel/GoldBtn/Obj1/Text").GetComponent<Text>();

        mToggleTxts2 = new Text[5];
        mToggleTxts2[0] = transform.FindChild("LeftPanel/PropBtn/Obj1/Text").GetComponent<Text>();
        mToggleTxts2[1] = transform.FindChild("LeftPanel/GiftBtn/Obj1/Text").GetComponent<Text>();
        mToggleTxts2[2] = transform.FindChild("LeftPanel/SkinBtn/Obj1/Text").GetComponent<Text>();
        mToggleTxts2[3] = transform.FindChild("LeftPanel/ChargeBtn/Obj1/Text").GetComponent<Text>();
        mToggleTxts2[4] = transform.FindChild("LeftPanel/GoldBtn/Obj1/Text").GetComponent<Text>();

        mTitleTxt1 = transform.FindChild("TopPanel/BtnGroup/Btn_Name1/NoSelect/Text").GetComponent<Text>();
        mTitleTxt2 = transform.FindChild("TopPanel/BtnGroup/Btn_Name1/Selected/Text").GetComponent<Text>();
        
        mReturnBtn = transform.FindChild("TopPanel/BackBtn").GetComponent<Button>(); ;

        mAdvertisementObj = transform.FindChild("ImageView").gameObject;
        mAdImgList = transform.FindChild("ImageView/ImageList").gameObject;
        mAdImgItem = transform.FindChild("ImageView/Items/Image").gameObject;

        mUIProp = transform.FindChild("UIProps").gameObject;
        mPropItem = transform.FindChild("UIProps/Items/Item").gameObject;
        mPropListObj = transform.FindChild("UIProps/ItemList/ListLayOut").gameObject;

        mUIGift = transform.FindChild("UIGifts").gameObject;
        mGiftListObj = transform.FindChild("UIGifts/ItemList/ListLayOut").gameObject;
        mGiftItem = transform.FindChild("UIGifts/Items/Item").gameObject;

        mUISkin = transform.FindChild("UISkin").gameObject;
        mSkinListObj = transform.FindChild("UISkin/ItemList/ListLayOut").gameObject;
        mSkinItem = transform.FindChild("UISkin/Items/Item").gameObject;

        mUICharge = transform.FindChild("UICharge").gameObject;
        mChargeObj = transform.FindChild("UICharge").gameObject;
        mVipNumImg1 = transform.FindChild("UICharge/VIP/VipLv/num1").GetComponent<Image>();
        mVipNumImg2 = transform.FindChild("UICharge/VIP/VipLv/num2").GetComponent<Image>();
        mVipLvTxt = transform.FindChild("UICharge/VIP/VipLv").GetComponent<Text>();
        mVipSlider = transform.FindChild("UICharge/VIP/Slider").GetComponent<Slider>();
        mFillAreaObj = transform.FindChild("UICharge/VIP/Slider/Fill Area").gameObject;
        mVipExpTxt = transform.FindChild("UICharge/VIP/Slider/ExpVal/Text").GetComponent<Text>();
        mVipExpTotalTxt = transform.FindChild("UICharge/VIP/Slider/ExpVal/TotalTxt").GetComponent<Text>();
        mVipPrivBtn = transform.FindChild("UICharge/VIP/Button").GetComponent<Button>();
        mVipPrivBtnTxt = transform.FindChild("UICharge/VIP/Button/Text").GetComponent<Text>();
        mVipNextHintTxt = transform.FindChild("UICharge/VIP/Girl/Image/Text").GetComponent<Text>();   //升到下一级...
        mNextVipNumImg1 = transform.FindChild("UICharge/VIP/Girl/vipImg/VipLv/num1").GetComponent<Image>();  //升到下一级...
        mNextVipNumImg2 = transform.FindChild("UICharge/VIP/Girl/vipImg/VipLv/num2").GetComponent<Image>();  //升到下一级...
        mNextVipObj = transform.FindChild("UICharge/VIP/Girl/vipImg").gameObject;
        mChargeListObj = transform.FindChild("UICharge/ItemList/ListLayOut").gameObject;
        mChargeItem = transform.FindChild("UICharge/Items/Item").gameObject;

        mUIGold = transform.FindChild("UIGold").gameObject;
        mGoldListObj = transform.FindChild("UIGold/ItemList/ListLayOut").gameObject;
        mGoldItem = transform.FindChild("UIGold/Items/Item").gameObject;

        mInitDone = new bool[5];

        for (int i = 0; i < mInitDone.Length; i++ )
        {
            mInitDone[i] = false;
        }

        mToggles[0].onValueChanged.AddListener(OnPropToggleValueChanged);
        mToggles[1].onValueChanged.AddListener(OnGiftToggleValueChanged);
        mToggles[2].onValueChanged.AddListener(OnSkinToggleValueChanged);
        mToggles[3].onValueChanged.AddListener(OnChargeToggleValueChanged);
        mToggles[4].onValueChanged.AddListener(OnGoldToggleValueChanged);

        mReturnBtn.onClick.AddListener(OnCloseBtnClick);

        mVipPrivBtn.onClick.AddListener(OnVipPreviewBtnClick);
        HomeControler.Inst.PushFunly(4, 109);
        m_TipsImage = selfTransform.FindChild("LeftPanel/GiftBtn/Button/TipsImage").gameObject;
        m_TipsController = CreateFunctionTipsController();
        GameEventDispatcher.Inst.addEventListener(GameEventID.G_VipLevel_Update, OnVipDataChange);
        GameEventDispatcher.Inst.addEventListener(GameEventID.U_RefreshShopInfo, RefreshTipsController);
        InitShopAdvertisement();
        AddShopDataItems();
        AddAllExchangeItems();
        InitStringData();
    }

    public override void InitUIView()
    {
        base.InitUIView();

        mVipPrivBtnTxt.text = GameUtils.getString("shop_content17");

        //默认选中第一个;
        //mToggles[0].isOn = true;
        SetOnSelectedState((int)m_ShopTab - 1);
        ShowByTab(m_ShopTab);
        RefreshTipsController();
    }
    protected void OnDestroy()
    {
        base.OnDestroy();
        
        GameEventDispatcher.Inst.removeEventListener(GameEventID.U_RefreshShopInfo, RefreshTipsController);

    }

    public static void SetCurShowTab(SHOP_TAB tabId)
    {
        m_ShopTab = tabId;
    }

    public override void UpdateUIData()
    {
        base.UpdateUIData();

        if(mCurTime.CompareTo(REFRESH_INTERVAL) > 0)
        {
            mCurTime = 0f;

            foreach (UIPropShopItem st in mPropItemsDic.Values)
            {
                st.CallPerSecond();
            }

            foreach (UIGiftShopItem st in mGiftItemsDic.Values)
            {
                st.CallPerSecond();
            }
            foreach (UISkinShopItem st in mSkinItemsDic.Values)
            {
                st.CallPerSecond();
            }
            //foreach (UIChargeShopItem st in mChargeItemsDic.Values)
            //{
            //    st.CallPerSecond();
            //}
            foreach (UIGoldShopItem st in mGoldItemsDic.Values)
            {
                st.CallPerSecond();
            }
        }
        else
        {
            mCurTime += Time.deltaTime;
        }
    }
    
    void OnVipDataChange()
    {
        UpdateVipInfo();
    }

    /// <summary>
    /// TODO::监听人物VIP信息;
    /// </summary>
    void UpdateVipInfo()
    {
        int vipLv = ObjectSelf.GetInstance().VipLevel;
        char[] vipStr = vipLv.ToString().ToCharArray();

        if (vipStr.Length == 1)
        {
            int id = Convert.ToInt32(vipStr[0].ToString());
            mVipNumImg1.sprite = VipNumImgs[id];
            mVipNumImg2.gameObject.SetActive(false);
        }
        
        else if (vipStr.Length == 2)
        {
            mVipNumImg1.sprite = VipNumImgs[Convert.ToInt32(vipStr[0].ToString())];
            mVipNumImg2.sprite = VipNumImgs[Convert.ToInt32(vipStr[1].ToString())];
            mVipNumImg2.gameObject.SetActive(true);
        }

        //根据当前VIP等级获取总经验;
        VipTemplate vipT = DataTemplate.GetInstance().GetVipTemplateById(vipLv);
        int total = vipT.getVipExp();
        int cur = ObjectSelf.GetInstance().VipExp;
        mVipSlider.value = (float)cur / (float)total;
        mFillAreaObj.SetActive(cur != 0);

        mVipExpTxt.text = "<color=#ffae42>" + cur.ToString() + "</color><color=#8877b6>/" + total.ToString() + "</color>";
        if (ShopModule.IsVipFullLv(vipLv))
        {
            mVipSlider.gameObject.SetActive(false);

            mVipNextHintTxt.text = GameUtils.getString("VIP_tips1");
            mNextVipObj.SetActive(false);
        }
        //升到下一级...shop_content16再充值{0}元可升至;
        else
        {
            mVipSlider.gameObject.SetActive(true);

            mVipNextHintTxt.text = string.Format(GameUtils.getString("shop_content16"), ShopModule.GetMoneyCountToNextVipLv(vipLv, cur));
            mNextVipObj.SetActive(true);
        
            int nextVipLv = ObjectSelf.GetInstance().VipLevel + 1;
            char[] nextVipStr = nextVipLv.ToString().ToCharArray();

            if (nextVipStr.Length == 1)
            {
                mNextVipNumImg1.sprite = VipNumImgs[Convert.ToInt32(nextVipStr[0].ToString())];
                mNextVipNumImg2.gameObject.SetActive(false);
            }

            else if (nextVipStr.Length == 2)
            {
                mNextVipNumImg1.sprite = VipNumImgs[Convert.ToInt32(nextVipStr[0].ToString())];
                mNextVipNumImg2.sprite = VipNumImgs[Convert.ToInt32(nextVipStr[1].ToString())];
                mNextVipNumImg2.gameObject.SetActive(true);
            }
        }

    }

    /// <summary>
    /// 初始化商城广告;
    /// </summary>
    void InitShopAdvertisement()
    {
        GameUtils.DestroyChildsObj(mAdImgList, true);

        ///加载商城广告图片资源;
        ///这里提前加入监听事件，防止文件快速加载完成，因为挂载事件监听时候，事件为了数据安全会lock，可能导致监听挂载会在dispa该消息以后完成;
        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_ShopAdAssetDownload, onShopAdDownloadFinish);
        List<Sprite> sprits = AssetLoader.Inst.GetShopSpriteList();

        if (sprits == null)
        {
        }
        else
        {
            GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_ShopAdAssetDownload, onShopAdDownloadFinish);
            onShopAdDownloadFinish();
        }
    }

    void onShopAdDownloadFinish()
    {
        List<Sprite> sprits = AssetLoader.Inst.GetShopSpriteList();

        if(sprits == null)
        {
            LogManager.LogToFile("商城资源下载失败");
            return;
        }

        for (int i = 0; i < sprits.Count; i++)
        {
            GameObject go = GameObject.Instantiate(mAdImgItem) as GameObject;

            go.transform.parent = mAdImgList.transform;
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = mAdImgItem.transform.localPosition;

            Image img = go.GetComponent<Image>();
            img.sprite = sprits[i];
            img.SetNativeSize();
        }

        UI_ImageView imgView = mAdvertisementObj.GetComponent<UI_ImageView>();
        if (imgView == null)
        {
            imgView = mAdvertisementObj.AddComponent<UI_ImageView>();
        }
        imgView.enabled = true;
        imgView.Reset();
        imgView.ChangeTime = 5f;
        imgView.Start();
        imgView.SetTogImgs();

        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_ShopAdAssetDownload, onShopAdDownloadFinish);
    }

    void AddShopDataItems()
    {
        List<ShopTemplate> allItems = DataTemplate.GetInstance().GetAllShopTemplates();

        for(int i = 0; i < allItems.Count; i++)
        {
            ShopTemplate st = allItems[i];
            if (st == null) continue;

            CustomShopTemplate cst = new CustomShopTemplate();
            cst.ShopT = st;
            switch(st.getTabID())
            {
                case 1:
                    mPropList.Add(cst);
                    break;
                case 2:
                    mGiftList.Add(cst);
                    break;
                case 3:
                    mSkinList.Add(cst);
                    break;
                case 4:
                    LogManager.LogError("商城中已经没有该类型了，类型id=4");
                    break;
                case 5:
                    mGoldList.Add(cst);
                    break;
            }
        }
    }

    void AddAllExchangeItems()
    {
        List<ExchangeTemplate> allItems = DataTemplate.GetInstance().GetAllExchangeTemplates(MainGameControler.Inst.mPlatform);

        for (int i = 0; i < allItems.Count; i++)
        {
            ExchangeTemplate st = allItems[i];
            if (st == null) continue;

            CustomExchangeTemplate cst = new CustomExchangeTemplate();
            cst.exchangeT = st;

            mChargeList.Add(cst);
        }
    }

    UIPropShopItem CreatePropItem(bool isOnSale)
    {
        GameObject go = Instantiate(mPropItem) as GameObject;

        UIPropShopItem item = new UIPropShopItem(go);
        //item.InitItemInfo(st);
        item.SetParent(mPropListObj.transform);
        item.IsOnSale = isOnSale;

        return item;
    }
    UIGiftShopItem CreateGiftItem()
    {
        GameObject go = Instantiate(mGiftItem) as GameObject;

        UIGiftShopItem item = new UIGiftShopItem(go);
        //item.InitItemInfo(st);
        item.SetParent(mGiftListObj.transform);

        return item;
    }
    UISkinShopItem CreateSkinItem()
    {
        GameObject go = Instantiate(mSkinItem) as GameObject;

        UISkinShopItem item = new UISkinShopItem(go);
        //item.InitItemInfo(st);
        item.SetParent(mSkinListObj.transform);

        return item;
    }
    UIChargeShopItem CreateChargeItem()
    {
        GameObject go = Instantiate(mChargeItem) as GameObject;

        UIChargeShopItem item = new UIChargeShopItem(go);
        ////item.InitItemInfo(st);
        item.SetParent(mChargeListObj.transform);

        return item;
    }
    UIGoldShopItem CreateGoldItem()
    {
        GameObject go = Instantiate(mGoldItem) as GameObject;

        UIGoldShopItem item = new UIGoldShopItem(go);
        //item.InitItemInfo(st);
        item.SetParent(mGoldListObj.transform);

        return item;
    }

    public void OpenItemBuyUI(ShopTemplate shopT)
    {
        ShopBuyItemData sbid = new ShopBuyItemData();
        sbid.shopId = shopT.getId();
        UI_ShopBuyItemMgr.SetShopBuyItemData(sbid);

        UI_HomeControler.Inst.AddUI(UI_ShopBuyItemMgr.UI_ResPath);
    }

    void InitStringData()
    {
        mTitleTxt1.text = GameUtils.getString("shop_title1");
        mTitleTxt2.text = mTitleTxt1.text;

        mToggleTxts1[0].text = GameUtils.getString("shop_content1");
        mToggleTxts2[0].text = GameUtils.getString("shop_content1");
        mToggleTxts1[1].text = GameUtils.getString("shop_content2");
        mToggleTxts2[1].text = GameUtils.getString("shop_content2");
        mToggleTxts1[2].text = GameUtils.getString("shop_content3");
        mToggleTxts2[2].text = GameUtils.getString("shop_content3");
        mToggleTxts1[3].text = GameUtils.getString("shop_content4");
        mToggleTxts2[3].text = GameUtils.getString("shop_content4");
        mToggleTxts1[4].text = GameUtils.getString("shop_content5");
        mToggleTxts2[4].text = GameUtils.getString("shop_content5");
    }

    public override void OnReadyForClose()
    {
        base.OnReadyForClose();

        mToggles[0].onValueChanged.RemoveListener(OnPropToggleValueChanged);
        mToggles[1].onValueChanged.RemoveListener(OnGiftToggleValueChanged);
        mToggles[2].onValueChanged.RemoveListener(OnSkinToggleValueChanged);
        mToggles[3].onValueChanged.RemoveListener(OnChargeToggleValueChanged);
        mToggles[4].onValueChanged.RemoveListener(OnGoldToggleValueChanged);

        mReturnBtn.onClick.RemoveListener(OnCloseBtnClick);
        mVipPrivBtn.onClick.RemoveListener(OnVipPreviewBtnClick);

        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_VipLevel_Update, OnVipDataChange);

        mToggles = null;
        inst = null;

        m_ShopTab = SHOP_TAB.PROP;

        mPropList.Clear();
        mGiftList.Clear();
        mSkinList.Clear();
        mChargeList.Clear();
        mGoldList.Clear();

        foreach(UIPropShopItem item in mPropItemsDic.Values)
        {
            item.Destroy();
        }
        mPropItemsDic.Clear();
        foreach (UIGiftShopItem item in mGiftItemsDic.Values)
        {
            item.Destroy();
        }
        mGiftItemsDic.Clear();
        foreach(UISkinShopItem item in mSkinItemsDic.Values)
        {
            item.Destroy();
        }
        mSkinItemsDic.Clear();
        foreach(UIChargeShopItem item in mChargeItemsDic.Values)
        {
            item.Destroy();
        }
        mChargeItemsDic.Clear();
        foreach(UIGoldShopItem item in mGoldItemsDic.Values)
        {
            item.Destroy();
        }
        mGoldItemsDic.Clear();
    }

    void ShowByTab(SHOP_TAB st)
    {
        mUIProp.SetActive(false);
        mUIGift.SetActive(false);
        mUISkin.SetActive(false);
        mUICharge.SetActive(false);
        mUIGold.SetActive(false);

        switch(st)
        {
            case SHOP_TAB.PROP:
                if (!mInitDone[0])
                {
                    mInitDone[0] = true;
                    mPropList.Sort();

                    bool isSaling = false;
                    for (int i = 0; i < mPropList.Count; i++)
                    {
                        isSaling = ShopModule.IsShopItemInSaling(mPropList[i].ShopT);
                        mPropItemsDic.Add(mPropList[i].ShopT.getId(), CreatePropItem(isSaling));
                    }
                }
                int key1 = -1;
                for (int i = 0; i < mPropList.Count; i++ )
                {
                    key1 = mPropList[i].ShopT.getId();

                    mPropItemsDic[key1].InitItemInfo(mPropList[i].ShopT);
                }
                mUIProp.SetActive(true);

                mAdvertisementObj.SetActive(true);
                break;
            case SHOP_TAB.GIFT:
                if (!mInitDone[1])
                {
                    mInitDone[1] = true;
                    mGiftList.Sort();

                    for (int i = 0; i < mGiftList.Count; i++)
                    {
                        mGiftItemsDic.Add(mGiftList[i].ShopT.getId(), CreateGiftItem());
                    }
                }
                int key2 = -1;
                for (int i = 0; i < mGiftList.Count; i++)
                {
                    key2 = mGiftList[i].ShopT.getId();

                    mGiftItemsDic[key2].InitItemInfo(mGiftList[i].ShopT);
                }
                mUIGift.SetActive(true);
                
                mAdvertisementObj.SetActive(true);
                break;
            case SHOP_TAB.SKIN:
                if (!mInitDone[2])
                {
                    mInitDone[2] = true;
                    mSkinList.Sort();

                    for (int i = 0; i < mSkinList.Count; i++)
                    {
                        mSkinItemsDic.Add(mSkinList[i].ShopT.getId(), CreateSkinItem());
                    }
                }
                int key3 = -1;
                for (int i = 0; i < mSkinList.Count; i++)
                {
                    key3 = mSkinList[i].ShopT.getId();

                    mSkinItemsDic[key3].InitItemInfo(mSkinList[i].ShopT);
                }
                mUISkin.SetActive(true);

                mAdvertisementObj.SetActive(true);
                break;
            case SHOP_TAB.CHARGE:
                if (!mInitDone[3])
                {
                    mInitDone[3] = true;
                    mChargeList.Sort();

                    for (int i = 0; i < mChargeList.Count; i++)
                    {
                        mChargeItemsDic.Add(mChargeList[i].exchangeT.getId(), CreateChargeItem());
                    }
                }
                int key4 = -1;
                for (int i = 0; i < mChargeList.Count; i++)
                {
                    key4 = mChargeList[i].exchangeT.getId();

                    mChargeItemsDic[key4].InitItemInfo(mChargeList[i].exchangeT);
                }
                mAdvertisementObj.SetActive(false);

                UpdateVipInfo();

                mUICharge.SetActive(true);
                break;
            case SHOP_TAB.GOLD:
                if (!mInitDone[4])
                {
                    mInitDone[4] = true;
                    mGoldList.Sort();

                    for (int i = 0; i < mGoldList.Count; i++)
                    {
                        mGoldItemsDic.Add(mGoldList[i].ShopT.getId(), CreateGoldItem());
                    }
                }
                int key5 = -1;
                for (int i = 0; i < mGoldList.Count; i++)
                {
                    key5 = mGoldList[i].ShopT.getId();

                    mGoldItemsDic[key5].InitItemInfo(mGoldList[i].ShopT);
                }
                mUIGold.SetActive(true);

                mAdvertisementObj.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// 左侧按钮列表显示;
    /// </summary>
    /// <param name="idx"></param>
    void SetOnSelectedState(int idx)
    {
        if(idx < 0 || idx >= 5)
        {
            LogManager.LogError("idx 错误");
            return;
        }

        for (int i = 0; i < 5; i++)
        {
            if (i == idx) continue;

            NorTabBtnObjs[i].SetActive(true);
            SelectTabBtnObjs[i].SetActive(false);
        }

        SelectTabBtnObjs[idx].SetActive(true);
    }

    public void OnPropToggleValueChanged(bool value)
    {
        if(value)
        {
            SetOnSelectedState(0);

            ShowByTab(SHOP_TAB.PROP);
        }
    }

    public void OnGiftToggleValueChanged(bool value)
    {
        if(value)
        {

            SetOnSelectedState(1);

            ShowByTab(SHOP_TAB.GIFT);
        }
    }
    /// <summary>
    /// yao 活动整理需跳转 道具，礼包，时装改为了公共的方法
    /// </summary>
    /// <param name="value"></param>
    public void OnSkinToggleValueChanged(bool value)
    {
        if(value)
        {
            SetOnSelectedState(2);

            ShowByTab(SHOP_TAB.SKIN);

        }
    }

    void OnChargeToggleValueChanged(bool value)
    {
        if(value)
        {

            SetOnSelectedState(3);

            ShowByTab(SHOP_TAB.CHARGE);
        }
    }

    void OnGoldToggleValueChanged(bool value)
    {
        if(value)
        {
            SetOnSelectedState(4);

            ShowByTab(SHOP_TAB.GOLD);

        }
    }

    void OnCloseBtnClick()
    {
        OnReadyForClose();

        UI_HomeControler.Inst.ReMoveUI(UI_ShopMgr.UI_ResPath);
    }

    void OnVipPreviewBtnClick()
    {
        UI_HomeControler.Inst.AddUI(UI_VipPrivilege.GetPath(false));
    }


    //生成功能提示控制器
    IFunctionTipsController CreateFunctionTipsController()
    {
        var _manager = FunctionTipsManager.GetInstance();
        if (_manager == null)
            return null;

        FunctionTipsController _controller = new FunctionTipsController();

        _controller.AddControlledObject(m_TipsImage, _manager.CheckNonPurchasedGiftSet);

        return _controller;
    }
    public void RefreshTipsController()
    {
        if (m_TipsController == null)
            return;

        m_TipsController.Refresh();
    }
}
