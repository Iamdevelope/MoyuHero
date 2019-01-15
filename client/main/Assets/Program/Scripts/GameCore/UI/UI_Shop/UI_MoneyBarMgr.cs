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

public class UI_MoneyBarMgr : BaseUI
{
    protected GameObject PowerObj;

    protected Text MoneyCount;              //金币值
    protected Text CurrentPowerTxt;         //当前体力值
    protected Text MaxPowerTxt;             //最大体力值
    protected Text PowerTime;               //体力更新时间剩余
    protected Text DiamondCount;            //充值币值

    protected Button _PowersAddBtn;          //体力添加按钮
    protected Button _MoneyAddBtn;           //金币添加按钮
    protected Button _DiamondAddBtn;         //钻石添加按钮

    protected static UI_MoneyBarMgr mIns = null;

    public static UI_MoneyBarMgr Ins
    {
        get
        {
            return mIns;
        }
    }

    public bool isShowPowerTime = true;

    public override void InitUIData()
    {
        base.InitUIData();

        mIns = this;

        PowerObj = selfTransform.FindChild("Powers").gameObject;

        CurrentPowerTxt = selfTransform.FindChild("Powers/CurrentpowTxt").GetComponent<Text>();
        MaxPowerTxt = selfTransform.FindChild("Powers/MaxpowTxt").GetComponent<Text>();
        PowerTime = selfTransform.FindChild("Powers/Time").GetComponent<Text>();
        PowerTime.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 40f);
        MoneyCount = selfTransform.FindChild("Money/Text").GetComponent<Text>();
        DiamondCount = selfTransform.FindChild("Diamond/Text").GetComponent<Text>();

        Transform addTrans1 = selfTransform.FindChild("Diamond/Add");
        if (addTrans1 != null)
        {
            throwCanvas(addTrans1);
        }
        Transform addTrans2 = selfTransform.FindChild("Money/Add");
        if (addTrans2 != null)
        {
            throwCanvas(addTrans2);
        }
        Transform addTrans3 = selfTransform.FindChild("Powers/AddBtn");
        if (addTrans3 != null)
        {
            throwCanvas(addTrans3);
        }

        Transform diaTrans = selfTransform.FindChild("Diamond");
        _DiamondAddBtn = diaTrans.GetComponent<Button>();
        if (_DiamondAddBtn == null)
            _DiamondAddBtn = diaTrans.gameObject.AddComponent<Button>();

        Transform monTrans = selfTransform.FindChild("Money");
        _MoneyAddBtn = selfTransform.FindChild("Money").GetComponent<Button>();
        if (_MoneyAddBtn == null)
            _MoneyAddBtn = monTrans.gameObject.AddComponent<Button>();

        Transform powTrans = selfTransform.FindChild("Powers");
        _PowersAddBtn = selfTransform.FindChild("Powers").GetComponent<Button>();
        if (_PowersAddBtn == null)
            _PowersAddBtn = powTrans.gameObject.AddComponent<Button>();
        GameEventDispatcher.Inst.addEventListener(GameEventID.G_ActionPoint_Update, UpdatePower);
        GameEventDispatcher.Inst.addEventListener(GameEventID.G_Money_Update, UpdateMoney);
        GameEventDispatcher.Inst.addEventListener(GameEventID.G_Gold_Update, UpdateDiamond);

        _DiamondAddBtn.onClick.AddListener(OnClickDiamondAddBtn);
        _MoneyAddBtn.onClick.AddListener(OnClickMoneyAddBtn);
        _PowersAddBtn.onClick.AddListener(OnClickPowersAddBtn);
    }

    void throwCanvas(Transform trans)
    {
        CanvasGroup cg = trans.GetComponent<CanvasGroup>();
        if (cg == null)
        {
            cg = trans.gameObject.AddComponent<CanvasGroup>();
        }

        //cg.interactable = false;
        cg.blocksRaycasts = false;
    }


    public override void InitUIView()
    {
        base.InitUIView();

        UpdateMoney();
        UpdateDiamond();
        UpdatePower();
    }

    public override void UpdateUIData()
    {
        base.UpdateUIData();

        if (!isShowPowerTime)
        {
            PowerTime.enabled = false;
            return;
        }

        ////更新体力剩余时间
        int Timer = ObjectSelf.GetInstance().TiTime;
        int CurrentPower = ObjectSelf.GetInstance().ActionPoint;
        int MaxPower = ObjectSelf.GetInstance().ActionPointMax;
        if (CurrentPower >= MaxPower)
        {
            PowerTime.enabled = false;
        }
        else
        {
            int minute = Timer / 60;
            int second = Timer % 60;
            string minuteStr = minute <= 9 ? "0" + minute : minute.ToString();
            string secondStr = second <= 9 ? "0" + second : second.ToString();
            StringBuilder timeStr = new StringBuilder();
            timeStr.Append(minuteStr);
            timeStr.Append(":");
            timeStr.Append(secondStr);
            PowerTime.text = timeStr.ToString();
        }
    }

    void OnDestroy()
    {
        DestroyUIData();
    }

    protected virtual void DestroyUIData()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_ActionPoint_Update, UpdatePower);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_Money_Update, UpdateMoney);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_Gold_Update, UpdateDiamond);
    }

    //添加活力
    private void OnClickPowersAddBtn()
    {
        //HomeControler.Inst.SetMainHomeWindow(false);    
        UI_HomeControler.Inst.AddUI(UI_PowersAdd.UI_ResPath);
    }

    //添加金币
    private void OnClickMoneyAddBtn()
    {
        UI_HomeControler.Inst.AddUI(UI_QuikBuyGoldMgr.UI_ResPath);
    }

    //添加钻石
    private void OnClickDiamondAddBtn()
    {
        UI_HomeControler.Inst.AddUI(UI_QuikChargeMgr.UI_ResPath);
    }

    /// <summary>
    /// 金币更新
    /// </summary>
    private void UpdateMoney()
    {
        long Moneys = ObjectSelf.GetInstance().Money;
        MoneyCount.text = Moneys.ToString();
    }

    /// <summary>
    /// 钻石更新
    /// </summary>
    private void UpdateDiamond()
    {
        int Diam = ObjectSelf.GetInstance().Gold;
        DiamondCount.text = Diam.ToString();
    }

    /// <summary>
    /// 体力更新
    /// </summary>
    private void UpdatePower()
    {
        int CurrentPower = ObjectSelf.GetInstance().ActionPoint;
        int MaxPower = ObjectSelf.GetInstance().ActionPointMax;
        StringBuilder StrTmp = new StringBuilder();
        StrTmp.Append("/");
        StrTmp.Append(MaxPower);
        CurrentPowerTxt.text = CurrentPower.ToString();
        MaxPowerTxt.text = StrTmp.ToString();
        if (CurrentPower >= 200)
        {
            CurrentPowerTxt.color = Color.red;
        }
        else if (CurrentPower >= MaxPower)
        {
            CurrentPowerTxt.color = Color.yellow;
        }
        else
        {
            CurrentPowerTxt.color = Color.white;
        }
    }
}
