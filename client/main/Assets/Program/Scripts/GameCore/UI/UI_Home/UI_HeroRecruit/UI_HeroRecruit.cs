using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using System.Text;
using GNET;
using System.Collections.Generic;
using DreamFaction.GameCore;
using DreamFaction.UI;

public class UI_HeroRecruit : UI_HeroRecruitBase
{
    public static UI_HeroRecruit inst;

    protected RichText m_LeftTipsText;
    protected RichText m_CenterTipsText;
    //protected RichText m_BottomTipsText;
    protected RichText m_RightTipsText;
    protected Slider m_DreamValueBar;
    protected GameObject m_LeftPriceObj;
    int m_CurFlag = -1;   // 现在要发的消息的标志
    bool m_IsBuyOneFree = false;
    public GameObject m_RecruitScene;
    public GameObject m_CimeliaScene;

    public override void InitUIData()
    {
        base.InitUIData();
        m_RightTipsText = selfTransform.FindChild("Right/RightTipsText").GetComponent<RichText>();
        //m_BottomTipsText = selfTransform.FindChild("Center/BottomTipsText").GetComponent<RichText>();
        m_CenterTipsText = selfTransform.FindChild("Center/CenterTipsText").GetComponent<RichText>();
        m_LeftTipsText = selfTransform.FindChild("Left/LeftTipsText").GetComponent<RichText>();
        m_DreamValueBar = selfTransform.FindChild("Center/DreamValueBar").GetComponent<Slider>();
        m_LeftPriceObj = selfTransform.FindChild("Left/LeftPrice").gameObject;
        inst = this;
    }

    public override void InitUIView()
    {
        base.InitUIView();

        m_BuyOneText.text = GameUtils.getString("hero_recruit_content5");
        m_BuyTenText.text = GameUtils.getString("hero_recruit_content6");
        m_LeftPreText.text = GameUtils.getString("hero_recruit_content16");
        m_CenterPreText.text = GameUtils.getString("hero_recruit_content16");
        m_RightPreText.text = GameUtils.getString("hero_recruit_content16");

        //m_BottomTipsText.ShowRichText(GameUtils.getString("hero_recruit_content9"));
        m_CenterTipsText.ShowRichText((GameUtils.getString("hero_recruit_content17")));

        InitTipsText();

        try
        {
            m_RecruitScene = GameObject.Find("Recruit_Scene");
            m_CimeliaScene = GameObject.Find("Cimelia_Scene");

            m_RecruitScene.SetActive(true);
            m_CimeliaScene.SetActive(false);
        }
        catch (System.Exception ex)
        {

        }

        // 是否进入兑换英雄界面
        if (ObjectSelf.GetInstance().dream != 0)
        {
            UI_HomeControler.Inst.AddUI(HeroInfoPop.UI_ResPath);
            HeroTemplate temp = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(ObjectSelf.GetInstance().dream);
            HerorecruitTemplate retemp = (HerorecruitTemplate)DataTemplate.GetInstance().m_HeroRecruitTable.getTableData(ObjectSelf.GetInstance().dream);
            HeroInfoPop.inst.SetShowData(temp, retemp.getHerolevel());
            HeroInfoPop.inst.SetExchangeHero();
            return;
        }

        // 引导购买一次
        if (GuideManager.GetInstance().isGuideUser && GuideManager.GetInstance().IsContentGuideID(100202) == false)
        {
            GuideManager.GetInstance().ShowGuideWithIndex(100202);
        }


    }

    // 显示提示文字
    public void InitTipsText()
    {
        m_IsBuyOneFree = false;

        // 首抽没有完成
        if (ObjectSelf.GetInstance().firstget == 0)
        {
            m_LeftTipsText.ShowRichText((GameUtils.getString("hero_recruit_content10")));
        }
        else
        {
            m_LeftTipsText.ShowRichText((GameUtils.getString("hero_recruit_content7")));
        }

        m_RightTipsText.ShowRichText((GameUtils.getString("hero_recruit_content8")));

        // 左边价格提示
        m_LeftPrice.text = DataTemplate.GetInstance().m_GameConfig.getSingle_herorecruit_cost().ToString();
        m_RightPrice.text = DataTemplate.GetInstance().m_GameConfig.getTen_herorecruit_cost().ToString();

        m_LeftCountDown.gameObject.SetActive(true);
        m_LeftPriceObj.gameObject.SetActive(true);
        m_LeftFreeTipsText.gameObject.SetActive(false);

        RefreshDreamValue();
    }

    // 显示倒计时
    void InitCountDown()
    {
        // 现在免费
        if (ObjectSelf.GetInstance().freetime <= 0)
        {
            m_LeftFreeTipsText.text = GameUtils.getString("hero_recruit_content11");
            m_LeftFreeTipsText.gameObject.SetActive(true);
            m_LeftPriceObj.gameObject.SetActive(false);
            m_LeftCountDown.gameObject.SetActive(false);
        }
    }

    public override void UpdateUIData()
    {
        base.UpdateUIData();

        if (m_IsBuyOneFree)
            return;

        if (ObjectSelf.GetInstance().freetime <= 0)
        {
            InitCountDown();
            m_IsBuyOneFree = true;
        }
        else
        {
            float Timer = ObjectSelf.GetInstance().freetime;
            int hour = (int)Timer / 3600;
            int minute = (int)Timer / 60 - hour * 60;
            int second = (int)Timer % 60;
            string hourStr = hour <= 9 ? "0" + hour : hour.ToString();
            string minuteStr = minute <= 9 ? "0" + minute : minute.ToString();
            string secondStr = second <= 9 ? "0" + second : second.ToString();
            StringBuilder timeStr = new StringBuilder();
            timeStr.Append(hour);
            timeStr.Append(":");
            timeStr.Append(minuteStr);
            timeStr.Append(":");
            timeStr.Append(secondStr);
            m_LeftCountDown.text = timeStr.ToString();
        }
    }

    protected override void OnClickBuyOneBtn()
    {
       

        // 是否免费
        if (ObjectSelf.GetInstance().freetime <= 0)
        {
            m_RecruitScene.GetComponent<RecruitScene>().PlayEffect();

            m_CurFlag = CLottery.FREE;
            Invoke("SendMessage", 1.5f);
            return;
        }

        // 金币不足
        if (TipsGoldValue(DataTemplate.GetInstance().m_GameConfig.getSingle_herorecruit_cost()))
        {
            return;
        }

        m_CurFlag = CLottery.ONE;
        // 提示梦想值
        if (TipsDreamValue())
        {
            return;
        }

        m_RecruitScene.GetComponent<RecruitScene>().PlayEffect();
        //SendMessage();

        Invoke("SendMessage", 1.5f);
    }

    protected override void OnClickLeftPreBtn()
    {
        UI_HomeControler.Inst.AddUI(UI_HeroPre.UI_ResPath);
        UI_HeroPre.inst.ShowHeroData(1);
    }

    protected override void OnClickCenterPreBtn()
    {
        UI_HomeControler.Inst.AddUI(UI_HeroPre.UI_ResPath);
        UI_HeroPre.inst.ShowHeroData(3);
    }

    protected override void OnClickExchangeBtn()
    {
        // 梦想值不足
        int dreamValue = DataTemplate.GetInstance().m_GameConfig.getDream_need_value();
        if (ObjectSelf.GetInstance().dreamexp < dreamValue)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("hero_recruit_content14"), selfTransform.transform.parent);
            return;
        }

        m_CurFlag = CLottery.DREAM;
        SendMessage();
    }

    protected override void OnClickBuyTenBtn()
    {
        // 金币不足
        if (TipsGoldValue(DataTemplate.GetInstance().m_GameConfig.getTen_herorecruit_cost()))
        {
            return;
        }

        m_CurFlag = CLottery.TEN;
        if (TipsDreamValue())
        {
            return;
        }


        SendMessage();
    }

    protected override void OnClickRightPreBtn()
    {
        UI_HomeControler.Inst.AddUI(UI_HeroPre.UI_ResPath);
        UI_HeroPre.inst.ShowHeroData(2);
    }

    // 成功买到一个
    public void SuccessBuyOne(LinkedList<int> herolist)
    {
        foreach (var item in herolist)
        {
            UI_HomeControler.Inst.AddUI(HeroInfoPop.UI_ResPath);
            HeroTemplate temp = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(item);
            HerorecruitTemplate retemp = (HerorecruitTemplate)DataTemplate.GetInstance().m_HeroRecruitTable.getTableData(item);
            HeroInfoPop.inst.SetShowData(temp, retemp.getHerolevel());
            HeroInfoPop.inst.SetSingGainHero();

            RefreshDreamValue();
            break;
        }
    }

    // 成功买到十个
    public void SuccessBuyTen(LinkedList<int> herolist)
    {
        UI_HomeControler.Inst.AddUI(UI_GainHero.UI_ResPath);
        UI_GainHero.inst.ShowHeroData(herolist);

        RefreshDreamValue();
    }

    // 成功兑换英雄
    public void SuccessExchangeHero(LinkedList<int> herolist)
    {
        List<int> heroId = new List<int>();
        foreach (var item in herolist)
        {
            UI_HomeControler.Inst.AddUI(HeroInfoPop.UI_ResPath);
            HeroTemplate temp = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(item);
            HerorecruitTemplate retemp = (HerorecruitTemplate)DataTemplate.GetInstance().m_HeroRecruitTable.getTableData(item);
            HeroInfoPop.inst.SetShowData(temp, retemp.getHerolevel());
            HeroInfoPop.inst.SetExchangeHero();
            RefreshDreamValue();
            break;
        }
    }

    bool TipsDreamValue()
    {
        // 提示梦想值已满
        int dreamValue = DataTemplate.GetInstance().m_GameConfig.getDream_need_value();
        if (ObjectSelf.GetInstance().dreamexp >= dreamValue && ObjectSelf.GetInstance().isTipsDream)
        {
            ObjectSelf.GetInstance().isTipsDream = false;

            UI_RechargeBox box = UI_HomeControler.Inst.AddUI(UI_RechargeBox.UI_ResPath).GetComponent<UI_RechargeBox>();
            box.SetIsNeedDescription(false);
            box.SetDescription_text(GameUtils.getString("hero_recruit_bubble1"));
            box.SetLeftBtn_text(GameUtils.getString("common_button_ok"));
            box.SetLeftClick(OnClickConfirmBtn);
            box.SetRightBtn_text(GameUtils.getString("common_button_cancel"));
            box.SetRightClick(OnClickCancelBtn);

            return true;
        }
        return false;
    }

    public bool TipsGoldValue(int goldValue)
    {
        // 元宝是否足够
        if (ObjectSelf.GetInstance().Gold < goldValue)
        {
            UI_RechargeBox box = UI_HomeControler.Inst.AddUI(UI_RechargeBox.UI_ResPath).GetComponent<UI_RechargeBox>();
            box.SetIsNeedDescription(false);
            box.SetDescription_text(GameUtils.getString("quickly_pay"));
            box.SetLeftBtn_text(GameUtils.getString("common_button_ok"));
            box.SetLeftClick(OnClickRechargeBtn);
            box.SetRightBtn_text(GameUtils.getString("common_button_cancel"));
            box.SetRightClick(OnClickCancelBtn);

            return true;
        }

        return false;
    }

    // 去充值界面
    void OnClickRechargeBtn()
    {
        UI_HomeControler.Inst.ReMoveUI(UI_RechargeBox.UI_ResPath);
        UI_HomeControler.Inst.AddUI(UI_QuikChargeMgr.UI_ResPath);
    }

    void OnClickConfirmBtn()
    {
        if(m_CurFlag == CLottery.ONE)
        {
            m_RecruitScene.GetComponent<RecruitScene>().PlayEffect();

            Invoke("SendMessage", 1.5f);
        }
        
        UI_HomeControler.Inst.ReMoveUI(UI_RechargeBox.UI_ResPath);
    }

    void OnClickCancelBtn()
    {
        // TODO...
        UI_HomeControler.Inst.ReMoveUI(UI_RechargeBox.UI_ResPath);
    }

    void SendMessage()
    {
        CLottery proto = new CLottery();
        proto.lotterytype = m_CurFlag;
        IOControler.GetInstance().SendProtocol(proto);

        if(m_CurFlag == CLottery.FREE || m_CurFlag == CLottery.ONE )
        {
            m_RecruitScene.GetComponent<RecruitScene>().StopEffect();
        }
    }

    public void RefreshDreamValue()
    {
        int dreamValue = DataTemplate.GetInstance().m_GameConfig.getDream_need_value();
        m_NumTipsText.text = ObjectSelf.GetInstance().dreamexp.ToString();
        m_DreamValueBar.value = (float)(ObjectSelf.GetInstance().dreamexp) / (float)dreamValue;
    }
}
