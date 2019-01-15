using UnityEngine;
using System.Collections;

using DreamFaction.Utils;
using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork;
using GNET;
using DreamFaction.UI.Core;
using System;

public class UI_Lottery : UI_LotteryBase
{
    public static string UI_ResPath = "UI_Recruit/UI_Lottery_3_2";

    protected GameObject mResourceCostObj1;         //金币一次抽奖消耗显示;
    protected GameObject mResourceCostObj2;         //钻石一次抽奖消耗显示;

    private int mGoldCostOne = -1;
    private int mGoldCostTen = -1;
    private int mGoldFreeTimes = -1;
    private int mGoldExtractionCD = -1;
    private int mDiamondCostOne = -1;
    private int mDiamondCostTen = -1;
    private int mDiamondFreeTimes = -1;

    private string mFreeTimesTitleStr = ""; // 免费次数{0};
    private string mFreeAfterTimeStr = "";  //{0}后免费;


    public override void InitUIData()
    {
        base.InitUIData();

        mResourceCostObj1 = selfTransform.FindChild("GoldLotteryObj/ResourceOneImg1").gameObject;
        mResourceCostObj2 = selfTransform.FindChild("DiamondLotteryObj/ResourceOneImg2").gameObject;

        InitConfigData();

        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_JiuGuanDataUpdate, OnDataChange);
    }

    public override void InitUIView()
    {
        base.InitUIView();

        InitStrData();

        m_CostOneTxt1.text = mGoldCostOne.ToString();
        m_CostTenTxt1.text = mGoldCostTen.ToString();
        m_CostOneTxt2.text = mDiamondCostOne.ToString();
        m_CostTenTxt2.text = mDiamondCostTen.ToString();

        UpdateUI();
    }

    void InitConfigData()
    {
        mGoldCostOne = DataTemplate.GetInstance().GetGameConfig().getGold_consumer_1();
        mGoldCostTen = DataTemplate.GetInstance().GetGameConfig().getGold_consumer_10();
        mGoldFreeTimes = DataTemplate.GetInstance().GetGameConfig().getLotteryGoldFreeTimes();
        mGoldExtractionCD = DataTemplate.GetInstance().GetGameConfig().getLotteryGoldExtractionCD();
        mDiamondCostOne = DataTemplate.GetInstance().GetGameConfig().getDiamond_consumer_1();
        mDiamondCostTen = DataTemplate.GetInstance().GetGameConfig().getDiamond_consumer_10();
        mDiamondFreeTimes = DataTemplate.GetInstance().GetGameConfig().getLotteryDiamondFreeTimes();


    }

    void InitStrData()
    {
        m_Title.text = GameUtils.getString("jiuguan2");

        m_ShopBtnTxt.text = GameUtils.getString("jiuguan1");

        //---------金币抽奖处---------
        m_TitleTxt1.text = GameUtils.getString("jiuguan3");
        m_HintTxt1.text = GameUtils.getString("jiuguan8");
        m_DrawOneTxt1.text = GameUtils.getString("jiuguan6");
        m_DrawTenTxt1.text = GameUtils.getString("jiuguan7");

        //免费次数:
        mFreeTimesTitleStr = GameUtils.getString("jiuguan5");
        mFreeAfterTimeStr = GameUtils.getString("jiuguan10");

        //---------钻石抽奖处----------
        m_TitleTxt2.text = GameUtils.getString("jiuguan4");
        m_HintTitleTxt.text = GameUtils.getString("jiuguan9");
        m_HintTxt2.text = GameUtils.getString("jiuguan8");
        m_DrawOneTxt2.text = GameUtils.getString("jiuguan6");
        m_DrawTenTxt2.text = GameUtils.getString("jiuguan7");
    }

    public override void UpdateUIData()
    {
        base.UpdateUIData();
    }

    public override void UpdateUIView()
    {
        base.UpdateUIView();

        UpdateDraw();
    }

    public override void OnDestroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_JiuGuanDataUpdate, OnDataChange);

        base.OnDestroy();
    }


    protected override void OnClickBackBtn()
    {
        base.OnClickBackBtn();

        UI_HomeControler.Inst.ReMoveUI(UI_Lottery.UI_ResPath);
    }

    protected override void OnClickShopBtn()
    {
        base.OnClickShopBtn();

    }

    protected override void OnClickDrawOneBtn1()
    {
        base.OnClickDrawOneBtn1();
        ProtocolHandler(LOTTERY_TYPE.Nor_One);
    }

    protected override void OnClickDrawTenBtn1()
    {
        base.OnClickDrawTenBtn1();
        ProtocolHandler(LOTTERY_TYPE.Nor_Ten);
    }

    protected override void OnClickDrawOneBtn2()
    {
        base.OnClickDrawOneBtn2();
        ProtocolHandler(LOTTERY_TYPE.Top_One);
    }

    protected override void OnClickDrawTenBtn2()
    {
        base.OnClickDrawTenBtn2();

        ProtocolHandler(LOTTERY_TYPE.Top_Ten);
    }

    void OnDataChange(GameEvent ge)
    {
        UpdateUI();
    }

    /// <summary>
    /// 刷新整个UI显示;
    /// </summary>
    void UpdateUI()
    {
        //是否第一次钻石十连抽;
        if (ObjectSelf.GetInstance().IsFirstTopTenDrawDone())
        {
            m_HintTitleTxt.gameObject.SetActive(false);
        }
        else
        {
            m_HintTitleTxt.gameObject.SetActive(true);
        }


        UpdateDraw();
    }

    /// <summary>
    /// 刷新抽奖显示;
    /// </summary>
    void UpdateDraw()
    {
        //----------------金币抽奖----------------
        int lastTims1 = mGoldFreeTimes - ObjectSelf.GetInstance().GetDrawTimes(LOTTERY_TYPE.Nor_One);

        //是否在计时中
        int sec1 = ObjectSelf.GetInstance().GetDrawTimeSecToFree(LOTTERY_TYPE.Nor_One);
        if (sec1 <= 0) //免费;
        {
            m_TimesTxt1.text = string.Format(mFreeTimesTitleStr, Mathf.Max(0, lastTims1) + "/" + mGoldFreeTimes);
            m_TimesTxt1.gameObject.SetActive(true);
            m_SecondsTxt1.gameObject.SetActive(false);

            if (lastTims1 > 0)
            {
                mResourceCostObj1.SetActive(false);
                m_FreeTxt1.gameObject.SetActive(true);

            }
            else
            {
                mResourceCostObj1.SetActive(true);
                m_FreeTxt1.gameObject.SetActive(false);
            }
        }
        else
        {
            TimeSpan ts1 = GameUtils.ConverToTimeSpan(sec1);
            m_SecondsTxt1.text = string.Format(mFreeAfterTimeStr, ts1);

            m_TimesTxt1.gameObject.SetActive(false);
            m_SecondsTxt1.gameObject.SetActive(true);

            m_FreeTxt1.gameObject.SetActive(false);
            mResourceCostObj1.SetActive(true);
        }

        //----------------钻石抽奖----------------
        int lastTims2 = mDiamondFreeTimes - ObjectSelf.GetInstance().GetDrawTimes(LOTTERY_TYPE.Top_One);

        //是否在计时中
        int sec2 = ObjectSelf.GetInstance().GetDrawTimeSecToFree(LOTTERY_TYPE.Top_One);
        if (sec2 <= 0) //免费;
        {
            m_TimesTxt2.text = string.Format(mFreeTimesTitleStr, Mathf.Max(0, lastTims2) + "/" + mDiamondFreeTimes);
            m_TimesTxt2.gameObject.SetActive(true);
            m_SecondsTxt2.gameObject.SetActive(false);

            if (lastTims2 > 0)
            {
                mResourceCostObj2.SetActive(false);
                m_FreeTxt2.gameObject.SetActive(true);

            }
            else
            {
                mResourceCostObj2.SetActive(true);
                m_FreeTxt2.gameObject.SetActive(false);
            }
        }
        else
        {
            TimeSpan ts2 = GameUtils.ConverToTimeSpan(sec1);
            m_SecondsTxt1.text = string.Format(mFreeAfterTimeStr, ts2);

            m_TimesTxt2.gameObject.SetActive(false);
            m_SecondsTxt2.gameObject.SetActive(true);

            m_FreeTxt2.gameObject.SetActive(false);
            mResourceCostObj2.SetActive(true);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    void ProtocolHandler(LOTTERY_TYPE type)
    {
        CLottery data = new CLottery();

        data.lotterytype = (int)type;

        IOControler.GetInstance().SendProtocol(data);
    }
}
