using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using UnityEngine.Events;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using GNET;
using DreamFaction.GameCore;
using DreamFaction.UI;
public class UI_LimitTest : BaseUI 
{
    public static UI_LimitTest Inst;
    public static string UI_ResPath = "UI_Home/UI_LimitTest_2_3";
    private ObjectSelf m_Self;
    private GameConfig m_Config;
    private int[] m_ReardCounts;          //奖励物品数量
    private string[] m_ReardImgs;         //奖励物品图标

    private Button _BackBtn;              //返回按钮
    private Button _RuleBtn;              //试炼规则按钮
    private Button _PowerfulBtn;          //强者之约按钮
    private Button _RankingBtn;           //排行榜按钮
    private Button _StartTestBtn;         //开始试炼按钮
    private Text _LevelNumTxt;            //挑战关数
    private Text _BraveTxt;               //勇者证明
    private Text _PowerfulTxt;            //强者之约是否达成
    private Text _RankingTxt;             //今日排名
    private Text _SurplusTxt;             //剩余极限试炼次数
    private GameObject _TestRuleObj;      //试炼规则面板
    private GameObject _TestRankingObj;   //排行榜面板
    private GameObject _PowerFuObj;       //强者预约面板
    public Transform m_cappostion;    //公告栏位置


    //====================================================（极限试炼UI）======================================================//
         
    public override void InitUIData()
    {
        base.InitUIData();
        Inst = this;
        InitTestRuleObjData();
        _PowerFuObj = selfTransform.FindChild("PowerfuObj").gameObject;
        _TestRankingObj = selfTransform.FindChild("TestRankingObj").gameObject;

        _LevelNumTxt = selfTransform.FindChild("NoticePanel/LevelNumTxt").GetComponent<Text>();
        _BraveTxt = selfTransform.FindChild("NoticePanel/BraveTxt").GetComponent<Text>();
        _PowerfulTxt = selfTransform.FindChild("NoticePanel/PowerfulTxt").GetComponent<Text>();
        _RankingTxt = selfTransform.FindChild("NoticePanel/RankingTxt").GetComponent<Text>();
        _SurplusTxt = selfTransform.FindChild("BottomPanel/SurplusTxt").GetComponent<Text>();
        _BackBtn = selfTransform.FindChild("TopPanel/BackBtn").GetComponent<Button>();
        _RuleBtn = selfTransform.FindChild("RuleBtn").GetComponent<Button>();
        _PowerfulBtn = selfTransform.FindChild("BottomPanel/PowerfulBtn").GetComponent<Button>();
        _RankingBtn = selfTransform.FindChild("BottomPanel/RankingBtn").GetComponent<Button>();
        _StartTestBtn = selfTransform.FindChild("BottomPanel/StartTestBtn").GetComponent<Button>();
        m_cappostion = selfTransform.FindChild("cappostion");

        _BackBtn.onClick.AddListener(new UnityAction(OnBackBtn));
        _RuleBtn.onClick.AddListener(new UnityAction(OnRuleBtn));
        _PowerfulBtn.onClick.AddListener(new UnityAction(OnPowerfuBtn));
        _RankingBtn.onClick.AddListener(new UnityAction(OnRankingBtn));
        _StartTestBtn.onClick.AddListener(new UnityAction(OnStarEnterBtn));
    }
    

    public override void InitUIView()
    {
        base.InitUIView();
        m_Self = ObjectSelf.GetInstance();
        m_Config = (GameConfig)DataTemplate.GetInstance().m_GameConfig;

        InitTestCount();
        InitUIShow();
        //初始化试炼规则界面
        InitTestRuleObjUI();
    }

    /// <summary>
    /// 初始化UI显示
    /// </summary>
    private void InitUIShow()
    {
        _LevelNumTxt.text = m_Self.LimitFightMgr.m_RoundNum.ToString();
        _BraveTxt.text = m_Self.LimitFightMgr.m_AllDropNum.ToString();
        _PowerfulTxt.text = GameUtils.getString(InitPowerfulState());
        _RankingTxt.text = InitRinKingShow();
       UI_CaptionManager cap=  UI_CaptionManager.GetInstance();
       if (cap != null)
           cap.AwakeUp(m_cappostion);

    }
    /// <summary>
    /// 初始化强者之约是否达成
    /// </summary>
    /// <returns>返回： 已达成 Or 未达成</returns>
    private string InitPowerfulState()
    {
        if (m_Self.LimitFightMgr.m_PactIspass == 1)
        {
            return "ultimatetrial_content9";
        }

        return "ultimatetrial_content8";
    }
    /// <summary>
    /// 初始化预测今日排名
    /// </summary>
    /// <returns>返回： 暂未排名 or 1-20 or 20名以外</returns>
    private string InitRinKingShow()
    {
        if (m_Self.LimitFightMgr.m_TodayRanking == -1)
            return GameUtils.getString("ultimatetrial_content10");
        else if (m_Self.LimitFightMgr.m_TodayRanking > 20)
            return GameUtils.getString("ultimatetrial_content11");
        else if (m_Self.LimitFightMgr.m_TodayRanking > 0 && m_Self.LimitFightMgr.m_TodayRanking < 20)
            return m_Self.LimitFightMgr.m_TodayRanking.ToString();

        return GameUtils.getString("ultimatetrial_content10");
    }

    /// <summary>
    /// 显示试炼次数
    /// </summary>
    private void InitTestCount()
    {
        int _count = 0;
        if (ObjectSelf.GetInstance().LimitFightMgr.Activate)
            _count = 1;

        _SurplusTxt.text = _count.ToString();
    }

    /// <summary>
    /// 返回当前队伍人数是否大于0
    /// </summary>
    /// <returns></returns>
    private bool  GetCurGroupCount()
    {
        int _group = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
        int _heroCount = ObjectSelf.GetInstance().Teams.m_Matrix.GetLength(1);
        for (int i = 0; i < _heroCount; i++)
        {
            if (ObjectSelf.GetInstance().Teams.m_Matrix[_group, i].IsValid())
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 开始试炼按钮
    /// </summary>
    private void OnStarEnterBtn()
    {
        if (!ObjectSelf.GetInstance().LimitFightMgr.Activate)
        {
            string _text = GameUtils.getString("ultimatetrial_content21");
            InterfaceControler.GetInst().AddMsgBox(_text, transform);
        }
        else
        {
            //当前队伍没有英雄
            if (!GetCurGroupCount())
            {
                string _text = GameUtils.getString("fight_fightprepare_tip1");
                InterfaceControler.GetInst().AddMsgBox(_text, transform);
            }
            else
            {
                //UI_HomeControler.Inst.ReMoveUI(gameObject);

                CBeginEndless msg = new CBeginEndless();
                msg.troopid = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
                IOControler.GetInstance().SendProtocol(msg);
            }
        }


    }

    /// <summary>
    /// 返回按钮
    /// </summary>
    private void OnBackBtn()
    {
        UI_CaptionManager cap = UI_CaptionManager.GetInstance();
        if (cap != null)
            cap.Release(m_cappostion);
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }


    /// <summary>
    /// 试炼规则按钮
    /// </summary>
    private void OnRuleBtn()
    {
        _TestRuleObj.SetActive(true);
        UI_CaptionManager cap = UI_CaptionManager.GetInstance();
        if (cap != null)
            cap.Release(m_cappostion);
    }


    /// <summary>
    /// 排行榜按钮
    /// </summary>
    private void OnRankingBtn()
    {
        _TestRankingObj.SetActive(true);
        CGetEndlessRank _cer = new CGetEndlessRank();
        IOControler.GetInstance().SendProtocol(_cer);
    }

    /// <summary>
    /// 强者预约按钮
    /// </summary>
    private void OnPowerfuBtn()
    {
        _PowerFuObj.SetActive(true);
    }



    //====================================================（试炼规则界面）======================================================//


    //试炼规则面板控件
    private Button m_TestRuleCloseBtn;             //关闭按钮
    private Text m_TiliteTxt;                     //试炼规则标题
    private Text m_TestCountTxt;                  //试炼次数标题
    private Text m_TestCountDesTxt;               //试炼次数描述
    private Text m_FightModeTxt;                  //战斗方式标题
    private Text m_FightModeDesTxt;               //战斗方式描述
    private Text m_RewardTxt;                     //奖励标题
    private Image m_RewardImg1;                   //奖励1图标
    private Image m_RewardImg2;                   //奖励2图标
    private Text m_RewardCountTxt1;               //奖励1个数
    private Text m_RewardCountTxt2;               //奖励2个数


    /// <summary>
    /// 初始化试炼规则界面
    /// </summary>
    private void InitTestRuleObjData()
    {
        _TestRuleObj = selfTransform.FindChild("TestRuleObj").gameObject;
        m_TestRuleCloseBtn = _TestRuleObj.transform.FindChild("RuleWindow/CloseBtn").GetComponent<Button>();
        m_TiliteTxt = _TestRuleObj.transform.FindChild("RuleWindow/TitleObj/Text").GetComponent<Text>();
        m_TestCountTxt = _TestRuleObj.transform.FindChild("RuleWindow/TestCountTxt").GetComponent<Text>();
        m_TestCountDesTxt = _TestRuleObj.transform.FindChild("RuleWindow/TestCountDesTxt").GetComponent<Text>();
        m_FightModeTxt = _TestRuleObj.transform.FindChild("RuleWindow/FightModeTxt").GetComponent<Text>();
        m_FightModeDesTxt = _TestRuleObj.transform.FindChild("RuleWindow/FightModeDesTxt").GetComponent<Text>();
        m_RewardTxt = _TestRuleObj.transform.FindChild("RuleWindow/RewardTxt").GetComponent<Text>();
        m_RewardCountTxt1 = _TestRuleObj.transform.FindChild("RuleWindow/RewardItem1/Image/Text").GetComponent<Text>();
        m_RewardCountTxt2 = _TestRuleObj.transform.FindChild("RuleWindow/RewardItem2/Image/Text").GetComponent<Text>();
        m_RewardImg1 = _TestRuleObj.transform.FindChild("RuleWindow/RewardItem1/Icon").GetComponent<Image>();
        m_RewardImg2 = _TestRuleObj.transform.FindChild("RuleWindow/RewardItem2/Icon").GetComponent<Image>();

        m_TestRuleCloseBtn.onClick.AddListener(new UnityAction(OnTestRuleObjClose));
    }

    /// <summary>
    /// 初始化界面
    /// </summary>
    private void InitTestRuleObjUI()
    {
        m_ReardCounts = m_Config.getUltimatetrial_5wave_reward_num();
        m_ReardImgs = m_Config.getUltimatetrial_5wave_reward_icon();

        ShowTilteText();
        ShowDesReardUI();
    }

    /// <summary>
    /// 初始化标题文本
    /// </summary>
    private void ShowTilteText()
    {
        m_TiliteTxt.text = GameUtils.getString("ultimatetrial_content23");
        m_TestCountTxt.text = GameUtils.getString("ultimatetrial_content24");
        m_FightModeTxt.text = GameUtils.getString("ultimatetrial_content26");
        m_RewardTxt.text = GameUtils.getString("ultimatetrial_content28");
    }

    /// <summary>
    /// 初始化奖励显示
    /// </summary>
    private void ShowDesReardUI()
    {
        m_TestCountDesTxt.text = GameUtils.getString("ultimatetrial_content25");
        m_FightModeDesTxt.text = GameUtils.getString("ultimatetrial_content27");
        m_RewardCountTxt1.text = m_ReardCounts[0].ToString();
        m_RewardCountTxt2.text = m_ReardCounts[1].ToString();
        m_RewardImg1.sprite = UIResourceMgr.LoadSprite(common.defaultPath + m_ReardImgs[0]);
        m_RewardImg2.sprite = UIResourceMgr.LoadSprite(common.defaultPath + m_ReardImgs[1]);
    }




    /// <summary>
    /// 试炼规则关闭按钮
    /// </summary>
    private void OnTestRuleObjClose()
    {
        _TestRuleObj.SetActive(false);
        UI_CaptionManager cap = UI_CaptionManager.GetInstance();
        if (cap != null)
            cap.AwakeUp(m_cappostion);

       
    }
    









}


