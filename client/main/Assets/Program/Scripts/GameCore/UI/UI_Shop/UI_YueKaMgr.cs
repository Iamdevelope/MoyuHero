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

/// <summary>
/// 月卡获取状态;
/// </summary>
 public enum Monthcard_GotType
{
    HaveNotGot,  //拥有没领取;
    HaveAndGot,  //拥有已领取;
    NotHave,     //尚未拥有;
}

/// <summary>
/// 月卡类型;
/// </summary>
public enum Monthcard_Type
{
    Forever,     //永久月卡;
    Limited,     //限时月卡;
}

public class YueKaItemUI
{
    private GameObject mGo;
    private MonthcardTemplate mt;

    Text titleTxt;
    Text welfareTxt;
    GameObject itemListObj;
    Text getBtnTxt;
    Text hintTxt;
    Button getBtn;
    Image iconBg;
    Image iconImg;
    Button buyBtn;
    Text costTxt;
    Text buyBtnTxt;
    Image itemIconImg1;
    Text itemCountTxt1;
    Image itemIconImg2;
    Text itemCountTxt2;

    GameObject getDoneObj;

    private Monthcard_GotType m_GotType = Monthcard_GotType.NotHave;
    private Monthcard_Type m_Type = Monthcard_Type.Limited;

    public YueKaItemUI(Transform trans)
    {
        mGo = trans.gameObject;

        Init();
    }

    public YueKaItemUI(GameObject go)
    {
        mGo = go;
        Init();
    }

    public int GetTableId()
    {
        if (mt == null)
        {
            return -1;
        }

        return mt.getId();
    }

    void Init()
    {
        Transform trans = mGo.transform;

        titleTxt = trans.FindChild("NameTxt").GetComponent<Text>();
        welfareTxt = trans.FindChild("WelfareTxt").GetComponent<Text>();
        itemListObj = trans.FindChild("WelfareList").gameObject;
        getBtnTxt = trans.FindChild("GetBtn/Text").GetComponent<Text>();
        hintTxt = trans.FindChild("HintTxt").GetComponent<Text>();
        getBtn = trans.FindChild("GetBtn").GetComponent<Button>();
        iconBg = trans.FindChild("IconBg").GetComponent<Image>();
        iconImg = trans.FindChild("IconImg").GetComponent<Image>();
        buyBtn = trans.FindChild("BuyBtn").GetComponent<Button>();
        costTxt = trans.FindChild("BuyBtn/MoneyCost/Text").GetComponent<Text>();
        buyBtnTxt = trans.FindChild("BuyBtn/Text").GetComponent<Text>();
        itemIconImg1 = trans.FindChild("WelfareList/ItemImg1").GetComponent<Image>();
        itemCountTxt1 = trans.FindChild("WelfareList/Text1").GetComponent<Text>();
        itemIconImg2 = trans.FindChild("WelfareList/ItemImg2").GetComponent<Image>();
        itemCountTxt2 = trans.FindChild("WelfareList/Text2").GetComponent<Text>();

        getDoneObj = trans.FindChild("GetDoneObj").gameObject;

        getBtn.onClick.AddListener(OnGetBtnClick);
        EventTriggerListener.Get(iconImg.gameObject).onClick = OnIconClick;
        buyBtn.onClick.AddListener(OnBuyBtnClick);
        buyBtnTxt.text = GameUtils.getString("common_button_purchase");
    }

    public void InitInfo(MonthcardTemplate monthcardT)
    {
        mt = monthcardT;

        titleTxt.text = GameUtils.getString(mt.getName());
        welfareTxt.text = GameUtils.getString("monthcard_content1");
        iconBg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + mt.getBaseicon());
        iconBg.SetNativeSize();
        iconImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + mt.getIcon());
        ExchangeTemplate ex = DataTemplate.GetInstance().GetExchangeTemplateByMonthCardId(monthcardT.getId());
        if (ex != null)
        {
            costTxt.text = ex.getPrice().ToString();
        }
        itemIconImg1.sprite = GameUtils.GetSpriteByResourceType(EM_RESOURCE_TYPE.Gold);
        itemIconImg1.gameObject.SetActive(mt.getDailydiamond() > 0);
        itemCountTxt1.text = mt.getDailydiamond() <= 0 ? "" : mt.getDailydiamond().ToString();
        itemIconImg2.sprite = GameUtils.GetSpriteByResourceType(EM_RESOURCE_TYPE.Money);
        itemIconImg2.gameObject.SetActive(mt.getDailygold() > 0);
        itemCountTxt2.text = mt.getDailygold() <= 0 ? "" : mt.getDailygold().ToString();

        SetMonthcardType(mt.getDuration() > 0 ? Monthcard_Type.Limited : Monthcard_Type.Forever);
    }

    void OnGetBtnClick()
    {
        switch (m_GotType)
        {
            case Monthcard_GotType.HaveNotGot:
                //领取操作;
                CMonthCard data = new CMonthCard();
                data.cardid = mt.getId();
                IOControler.GetInstance().SendProtocol(data);
                break;
            case Monthcard_GotType.HaveAndGot:
                //已经领取过了;
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("monthcard_content6"), UI_YueKaMgr.Inst.transform);
                break;
            case Monthcard_GotType.NotHave:
                //尚未购买跳转到充值界面;
                //UI_ShopMgr.SetCurShowTab(SHOP_TAB.CHARGE);
                //UI_HomeControler.Inst.AddUI(UI_ShopMgr.UI_ResPath);
                //UI_YueKaPreviewMgr.SetMonthTemplate(mt);
                //UI_HomeControler.Inst.AddUI(UI_YueKaPreviewMgr.UI_ResPath);
                UI_YueKaBuyMgr.SetMonthTemplate(mt);
                UI_HomeControler.Inst.AddUI(UI_YueKaBuyMgr.UI_ResPath);
                break;
            default:
                break;
        }
    }

    void OnBuyBtnClick()
    {
        UI_YueKaBuyMgr.SetMonthTemplate(mt);
        UI_HomeControler.Inst.AddUI(UI_YueKaBuyMgr.UI_ResPath);
    }

    void OnIconClick(GameObject go)
    {
        ShowYUEKAPreviewUI(mt);
    }

    bool ShowYUEKAPreviewUI(MonthcardTemplate exchangeT)
    {
        UI_YueKaPreviewMgr.SetMonthTemplate(exchangeT);
        UI_HomeControler.Inst.AddUI(UI_YueKaPreviewMgr.UI_ResPath);

        return true;
    }

    public void Destroy()
    {
        getBtn.onClick.RemoveListener(OnGetBtnClick);
        EventTriggerListener.Get(iconImg.gameObject).onClick = null;

        mt = null;
    }

    public void SetMonthcardGotType(Monthcard_GotType type)
    {
        m_GotType = type;
        
        switch (m_GotType)
        {
            case Monthcard_GotType.HaveNotGot:
                getDoneObj.SetActive(false);
                getBtnTxt.text = GameUtils.getString("common_button_receive");
                GameUtils.SetBtnSpriteGrayState(getBtn, false);
                buyBtn.gameObject.SetActive(false);
                getBtn.gameObject.SetActive(true);
                break;
            case Monthcard_GotType.HaveAndGot:
                getDoneObj.SetActive(true);
                getBtnTxt.text = GameUtils.getString("sign_content4");
                GameUtils.SetBtnSpriteGrayState(getBtn, true);
                buyBtn.gameObject.SetActive(false);
                getBtn.gameObject.SetActive(true);
                break;
            case Monthcard_GotType.NotHave:
                getDoneObj.SetActive(false);
                hintTxt.text = GameUtils.getString("monthcard_content4");
                getBtnTxt.text = GameUtils.getString("common_button_purchase");
                GameUtils.SetBtnSpriteGrayState(getBtn, false);
                buyBtn.gameObject.SetActive(true);
                getBtn.gameObject.SetActive(false);
                break;
            default:
                break;
        }
        
        UpdatePerSec();
    }

    void SetMonthcardType(Monthcard_Type type)
    {
        m_Type = type;

        switch (m_Type)
        {
            case Monthcard_Type.Forever:
                hintTxt.text = GameUtils.getString("monthcard_content2");
                break;
            case Monthcard_Type.Limited:
                break;
            default:
                break;
        }

        UpdatePerSec();
    }

    public void UpdatePerSec()
    {
        if (m_Type == Monthcard_Type.Forever)
        {
            return;
        }

        if (m_GotType == Monthcard_GotType.NotHave)
        {
            return;
        }

        //判断提示信息;
        StringBuilder sb = new StringBuilder();
        sb.Append(GameUtils.getString("monthcard_content3"));
        sb.Append(ExchangeModule.GetMonthCardToEnd(mt.getId()).Days.WithColor("#98DBFF"));

        hintTxt.text =  sb.ToString();
    }
}

public class UI_YueKaMgr : BaseUI
{
    public static readonly string UI_ResPath = "UI_Shop/UI_YueKa_02_03";

    private static UI_YueKaMgr m_Inst = null;

    Text titleTxt;
    Button closeBtn;
    GameObject objList;

    List<YueKaItemUI> items = new List<YueKaItemUI>();

    private int mItemCount = -1;
    private float mCurTime = 0f;

    public static UI_YueKaMgr Inst
    {
        get
        {
            return m_Inst;
        }
    }

    public override void InitUIData()
    {
        base.InitUIData();

        m_Inst = this;

        //刷新服务器时间;
        TimeUtils.SyncServerTime();

        titleTxt = transform.FindChild("TitleTxt").GetComponent<Text>();
        closeBtn = transform.FindChild("ReturnBtn/Image").GetComponent<Button>();
        objList = transform.FindChild("GridObj").gameObject;

        mItemCount = objList.transform.childCount;

        for (int i = 0; i < mItemCount; i++ )
        {
            items.Add(new YueKaItemUI(objList.transform.GetChild(i)));
        }

        closeBtn.onClick.AddListener(OnCloseBtnClick);

        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_RefreshMonthCard, OnMonthCardDataChange);
    }

    public override void InitUIView()
    {
        base.InitUIView();

        titleTxt.text = GameUtils.getString("monthcard_content5");

        List<MonthcardTemplate> datas = DataTemplate.GetInstance().GetAllMonthCardTemplates();

        if(datas.Count != items.Count)
        {
            LogManager.LogError("月卡MonthcardTemplate表格数据数据不为3组");
            return;
        }

        for (int i = 0; i < mItemCount; i++ )
        {
            items[i].InitInfo(datas[i]);
        }

        RefreshUI();
    }

    void RefreshUI()
    {
        OnMonthCardDataChange(null);
    }

    public override void UpdateUIView()
    {
        base.UpdateUIView();

        mCurTime += Time.deltaTime;

        if (mCurTime >= 1f)
        {
            mCurTime = 0f;

            for (int i = 0; i < items.Count; i++)
            {
                items[i].UpdatePerSec();
            }
        }
    }

    public override void OnReadyForClose()
    {
        base.OnReadyForClose();

        closeBtn.onClick.RemoveListener(OnCloseBtnClick);

        for (int i = 0; i < mItemCount; i++ )
        {
            items[i].Destroy();
        }

        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_RefreshMonthCard, OnMonthCardDataChange);

        m_Inst = null;
    }

    void OnDestroy()
    {
        UIState = BaseUI.UIStateEnum.ReadyForClose;
    }

    void OnCloseBtnClick()
    {
        UI_HomeControler.Inst.ReMoveUI(UI_ResPath);
    }


    void OnMonthCardDataChange(GameEvent ge)
    {
        for (int i = 0; i < mItemCount; i++ )
        {
            RefreshUIById(items[i].GetTableId());
        }
    }

    void RefreshUIById(int id)
    {
        for (int i = 0; i < mItemCount; i++ )
        {
            if(items[i].GetTableId() == id)
            {
                Monthcard mc = ObjectSelf.GetInstance().GetMontCardInfoById(id);
                //第一个特殊处理;
                if (id == 1)
                {
                    if (mc == null)
                    {
                        items[i].SetMonthcardGotType(Monthcard_GotType.HaveNotGot);
                    }
                    else
                    {
                        if (mc.istodayget == 1)
                        {
                            items[i].SetMonthcardGotType(Monthcard_GotType.HaveAndGot);
                        }
                        else
                        {
                            items[i].SetMonthcardGotType(Monthcard_GotType.HaveNotGot);
                        }
                    }
                }
                else
                {
                    if (mc == null)
                    {
                        items[i].SetMonthcardGotType(Monthcard_GotType.NotHave);
                    }
                    else
                    {
                        //根据当前时间判断是否过期;
                        DateTime dt = TimeUtils.ConverMillionSecToDateTime(mc.overtime, ObjectSelf.GetInstance().ServerTimeZone);

                        if (ObjectSelf.GetInstance().ServerDateTime >= dt)//过期;
                        {
                            items[i].SetMonthcardGotType(Monthcard_GotType.NotHave);
                        }
                        else//没过期;
                        {
                            if (mc.istodayget == 1)//已经领取;
                            {
                                items[i].SetMonthcardGotType(Monthcard_GotType.HaveAndGot);
                            }
                            else
                            {
                                items[i].SetMonthcardGotType(Monthcard_GotType.HaveNotGot);
                            }
                        }
                    }
                }
            }
        }
    }
}
