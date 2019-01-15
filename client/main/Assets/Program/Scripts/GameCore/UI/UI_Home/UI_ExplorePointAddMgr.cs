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

public class ExploreAddItemUI
{
    protected Text m_TitleTxt = null;
    protected Text m_DetailTxt = null;
    protected Image m_IconImg = null;
    protected Text m_ReminTitleTxt = null;
    protected Text m_ReminTxt = null;
    protected GameObject m_HaveObj = null;
    protected Text m_HaveTitleTxt = null;
    protected Text m_HaveTxt = null;
    protected GameObject m_CostObj = null;
    protected Text m_CostTxt = null;
    protected Button m_UseBtn = null;
    protected Text m_UseBtnTxt = null;

    protected GameObject mGo = null;
    protected int mId = -1;
    protected int mType = -1;
    protected int mHaveCount = -1;
    protected int mRemineTimes = -1;
    protected int mCost = -1;

    protected ShopTemplate shopT = null;
    protected ItemTemplate itemT = null;

    public ExploreAddItemUI(GameObject go)
    {
        mGo = go;

        Transform trans = mGo.transform;

        m_TitleTxt = trans.FindChild("NameTxt").GetComponent<Text>();
        m_DetailTxt = trans.FindChild("DetailTxt").GetComponent<Text>();
        m_IconImg = trans.FindChild("itemicon").GetComponent<Image>();
        m_ReminTitleTxt = trans.FindChild("RemineObj/RemineTitleTxt").GetComponent<Text>();
        m_ReminTxt = trans.FindChild("RemineObj/RemineTxt").GetComponent<Text>();
        m_HaveObj = trans.FindChild("HaveObj").gameObject;
        m_HaveTitleTxt = trans.FindChild("HaveObj/HaveTitleTxt").GetComponent<Text>();
        m_HaveTxt = trans.FindChild("HaveObj/HaveTxt").GetComponent<Text>();
        m_CostObj = trans.FindChild("UseBtn/CostObj").gameObject;
        m_CostTxt = trans.FindChild("UseBtn/CostObj/Text").GetComponent<Text>();
        m_UseBtn = trans.FindChild("UseBtn").GetComponent<Button>();
        m_UseBtnTxt = trans.FindChild("UseBtn/Text").GetComponent<Text>();

        m_HaveTitleTxt.text = GameUtils.getString("relics_content2");
        m_ReminTitleTxt.text = GameUtils.getString("vigour_supplement_content2");

        m_UseBtn.onClick.AddListener(OnUseBtnClick);
    }

    /// <summary>
    /// 设置信息，id表示表格id，type = 0表示对应shop表，1表示物品表;
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    public void SetData(int id, int type)
    {
        mId = id;
        mType = type;

        switch (type)
        {
            case 0:
                m_UseBtnTxt.text = GameUtils.getString("common_button_purchase");
                shopT = DataTemplate.GetInstance().GetShopTemplateByID(id);
                m_IconImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + shopT.getResourceName());
                m_IconImg.SetNativeSize();
                m_TitleTxt.text = GameUtils.getString(shopT.getCommodityName());
                mHaveCount = int.MaxValue;

                int max = DataTemplate.GetInstance().GetShopItemDailyBuyTimes(shopT, ObjectSelf.GetInstance().VipLevel);

                //剩余使用次数
                Shopbuy shop = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(id);
                
                mRemineTimes = max - shop.todaynum;
                mCost = DataTemplate.GetInstance().GetShopBuyCost(shopT, shop.todaynum);

                m_ReminTxt.text = mRemineTimes.ToString();
                m_CostTxt.text = mCost.ToString();
                m_CostObj.SetActive(true);
                m_HaveObj.SetActive(false);

                long moneyCount = -1;
                if(ObjectSelf.GetInstance().TryGetResourceCountById(EM_RESOURCE_TYPE.Gold, ref moneyCount))
                {
                    GameUtils.SetBtnSpriteGrayState(m_UseBtn, mRemineTimes <= 0 || mCost > moneyCount);
                }
                m_DetailTxt.text = GameUtils.getString(shopT.getCommodityDes());
                break;
            case 1:
                m_UseBtnTxt.text = GameUtils.getString("common_button_use");
                itemT = DataTemplate.GetInstance().GetItemTemplateById(id);
                m_IconImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + itemT.getIcon());
                m_IconImg.SetNativeSize();
                m_TitleTxt.text = GameUtils.getString(itemT.getName());
                
                if (!ObjectSelf.GetInstance().TryGetItemCountById(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, id, ref mHaveCount))
                {
                    mHaveCount = 0;
                }

                int max1 = ExplorePointModule.GetEPItemUseTimes(itemT, ObjectSelf.GetInstance().VipLevel);

                //剩余使用次数
                int useTimes = ObjectSelf.GetInstance().CommonItemContainer.GetItemUseTimes(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, id);
               
                mRemineTimes = max1 - Mathf.Max(0, useTimes);
                mCost = 0;

                m_ReminTxt.text = mRemineTimes.ToString();
                m_CostTxt.text = mCost.ToString();
                m_CostObj.SetActive(false);

                m_HaveTxt.text = (mHaveCount > 999 ? 999 : mHaveCount).ToString();
                m_HaveObj.SetActive(true);
                GameUtils.SetBtnSpriteGrayState(m_UseBtn, mRemineTimes <= 0 || mHaveCount <= 0);
                m_DetailTxt.text = GameUtils.getString(itemT.getDes());
                break;
            default:
                break;
        }
    }

    public int GetDataId()
    {
        return mId;
    }

    public void UpdateData()
    {
        SetData(mId, mType);
    }

    void OnUseBtnClick()
    {
        switch (mType)
        {
            case 0:
                bool isDiscount = ShopModule.IsShopItemInDiscount(shopT);
                int buyTimes = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(shopT.getId()).todaynum;
                int costNum = DataTemplate.GetInstance().GetShopBuyCost(shopT, buyTimes, isDiscount);

                long curCount = -1;
                EM_RESOURCE_TYPE resType = (EM_RESOURCE_TYPE)shopT.getCostType();
                if (ObjectSelf.GetInstance().TryGetResourceCountById(resType, ref curCount))
                {
                    if (curCount >= costNum)
                    {
                        //1.是否满活力;
                        if (ObjectSelf.GetInstance().ExplorePoint >= ObjectSelf.GetInstance().ExplorePointMax)
                        {
                            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("explore_bubble2"));
                            return;
                        }
                        //2.是否有购买次数;
                        int maxDayliTimes = DataTemplate.GetInstance().GetShopItemDailyBuyTimes(shopT, ObjectSelf.GetInstance().VipLevel);
                        if (buyTimes >= maxDayliTimes)
                        {
                            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("UI_raids_tips2"));
                            return;
                        }

                        //成功后弹窗;
                        ShowBuyWater(isDiscount, costNum);
                    }
                    else
                    {
                        switch (resType)
                        {
                            case EM_RESOURCE_TYPE.Gold:
                                //打开魔钻不足提示窗;
                                InterfaceControler.GetInst().ShowGoldNotEnougth();
                                break;
                            default:
                                //InterfaceControler.GetInst().AddMsgBox("除魔钻资源不足时，其他资源不足先不做处理");
                                break;
                        }
                    }
                }
                break;
            case 1:
                //是否有使用次数;
                if (mRemineTimes <= 0)
                {
                    InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("vigour_supplement_tip1"));
                    return;
                }

                //是否拥有该道具;
                if (mHaveCount <= 0)
                {
                    InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("vigour_supplement_tip3"));
                    return;
                }

                //成功;
                BaseItem item = ObjectSelf.GetInstance().CommonItemContainer.FindItem(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, mId);
                if (item == null)
                {
                    Debug.LogError("人物身上没有该物品id" + mId);
                    return;
                }

                SendMsg(item.GetItemGuid(), 1);
                break;
            default:
                break;
        }
    }


    private void SendMsg(X_GUID guid, int count)
    {
        CUseItem msg = new CUseItem();
        msg.bagid = (int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON;
        msg.itemkey = (int)guid.GUID_value;
        msg.num = (short)count;
        IOControler.GetInstance().SendProtocol(msg);
    }

    void ShowBuyWater(bool isDiscount, int costNum)
    {
        UI_RechargeBox box = UI_HomeControler.Inst.AddUI(UI_RechargeBox.UI_ResPath).GetComponent<UI_RechargeBox>();

        if (box == null)
        {
            LogManager.LogError("提示窗is null");
            return;
        }

        box.SetDescription_text(GameUtils.getString("vigour_buy_title"));
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

    public void Destroy()
    {
        shopT = null;
        itemT = null;
    }
}

public class UI_ExplorePointAddMgr : BaseUI
{
    public static readonly string UI_ResPath = "UI_Home/UI_ExplorePointAdd_2_2";

    protected Text m_TitleTxt = null;
    protected Button m_ReturnBtn = null;
    protected GameObject[] m_ItemsObj = null;

    private List<ExploreAddItemUI> mItemUIs = new List<ExploreAddItemUI>();

    public override void InitUIData()
    {
        base.InitUIData();

        m_TitleTxt = selfTransform.FindChild("TopPanel/TitleButton_0/Text").GetComponent<Text>();
        m_ReturnBtn = selfTransform.FindChild("TopPanel/CloseBtn").GetComponent<Button>();

        m_ItemsObj = new GameObject[4];

        int id = -1;
        int type = -1;
        int[] itemIds = DataTemplate.GetInstance().GetGameConfig().getEp_supplement_item();
        for (int i = 0; i < 4; i++ )
        {
            m_ItemsObj[i] = selfTransform.FindChild("PowersAddBox/PowersItem_" + (i + 1)).gameObject;

            ExploreAddItemUI ui = new ExploreAddItemUI(m_ItemsObj[i]);
            mItemUIs.Add(ui);

            if (i == 0)
            {
                id = DataTemplate.GetInstance().GetGameConfig().getEp_supplement_goods();
                type = 0;
            }
            else
            {
                id = itemIds[i - 1];
                type = 1;
            }

            ui.SetData(id, type);
        }

        m_ReturnBtn.onClick.AddListener(OnReturnBtnClick);
        GameEventDispatcher.Inst.addEventListener(GameEventID.G_ActionPoint_Update, OnActionPointUpdate);
        GameEventDispatcher.Inst.addEventListener(GameEventID.KE_KnapsackUpdateShow, OnItemChange);
        GameEventDispatcher.Inst.addEventListener(GameEventID.Net_RefreshItem, OnItemChange);
        GameEventDispatcher.Inst.addEventListener(GameEventID.U_RefreshShopInfo, OnShopInfoChange);
    }

    void OnActionPointUpdate()
    {
        RefreshUI();
    }

    void OnItemChange()
    {
        RefreshUI();
    }

    void OnShopInfoChange(GameEvent ge)
    {
        if (ge != null)
        {
            Shopbuy sb = ge.data as Shopbuy;
            if (sb != null && sb.shopid == mItemUIs[0].GetDataId())
            {

                RefreshShopItemUI();
            }
        }
    }

    void RefreshShopItemUI()
    {
        mItemUIs[0].UpdateData();
    }

    void RefreshUI()
    {
        for (int i = 0; i < mItemUIs.Count; i++ )
        {
            mItemUIs[i].UpdateData();
        }
    }

    public override void InitUIView()
    {
        base.InitUIView();

        m_TitleTxt.text = GameUtils.getString("ep_supplement_title");
    }

    void OnReturnBtnClick()
    {
        UI_HomeControler.Inst.ReMoveUI(UI_ResPath);
    }

    public override void OnReadyForClose()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_ActionPoint_Update, OnActionPointUpdate);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.Net_RefreshItem, OnItemChange);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.KE_KnapsackUpdateShow, OnItemChange);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.U_RefreshShopInfo, OnShopInfoChange);

        m_ItemsObj = null;

        for (int i = 0; i < mItemUIs.Count; i++ )
        {
            mItemUIs[i].Destroy();
        }
        mItemUIs.Clear();

        base.OnReadyForClose();
    }

    void OnDestroy()
    {
        UIState = UIStateEnum.ReadyForClose;
    }
}
