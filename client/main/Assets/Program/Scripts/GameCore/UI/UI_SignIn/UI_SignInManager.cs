using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DreamFaction.Utils;
using DreamFaction.UI;
using DreamFaction.GameNetWork;


public class UI_SignInManager : UI_BaseSignInManager 
{
    /// <summary>
    /// 管理UI_SignIn_2_10预制件中Award的控件,用于领奖确认和查看连续签到面板
    /// </summary>
    private class AwardData
    {
        public GameObject m_SelfGameObject;

        public Text m_AwardTittleText;  //标题字样：累计登陆/连续登陆
        public Text m_DayText;          //与标题位置相同，粗体
        public Transform m_AwardItemLayout;  //奖励物品的图片
        public Text m_AwardNameText;    //奖励物品名称


        public UniversalItemCell m_Cell;
        public AwardData(GameObject go)
        {
            m_SelfGameObject = go;

            Transform trans = go.transform;

            m_AwardTittleText = trans.FindChild("AwardTittleText").GetComponent<Text>();
            m_DayText = trans.FindChild("DayText").GetComponent<Text>();
            m_AwardItemLayout = trans.FindChild("AwardItemPanel/AwardItemLayout");
            m_AwardNameText = trans.FindChild("AwardItemPanel/AwardNameText").GetComponent<Text>();
            m_Cell = UniversalItemCell.GenerateItem(m_AwardItemLayout);
                
        }
        public void Init(string tittleText,int awardId,int count,bool isContinuous,bool isClaimed = false)
        {
            m_Cell.InitByID(awardId, count);
            m_Cell.SetCheckClaim(isClaimed);
            if (isContinuous)
            {
                m_AwardTittleText.gameObject.SetActive(false);
                m_DayText.gameObject.SetActive(true);
                m_DayText.text = tittleText;
            }
            else
            {
                m_AwardTittleText.gameObject.SetActive(true);
                m_DayText.gameObject.SetActive(false);
                m_AwardTittleText.text = tittleText;
            }

            m_AwardNameText.text = UI_SignInManager.GetAwardName(awardId);
        }
    }
    /// <summary>
    /// 管理UI_SignIn_2_10预制件中SignInCell的控件,用于累计签到
    /// </summary>
    private class SignInCellData
    {
        public UniversalItemCell m_Cell;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="go">预制件</param>
        /// <param name="tittleImage">需要切换的标题精灵图片（图片顺序：已经领取\今天\未领取）</param>
        public SignInCellData(Transform layout)
        {
            m_Cell = UniversalItemCell.GenerateItem(layout);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dayX">初始化的累计奖励提示是第几天的（1-28）</param>
        /// <param name="today">今天是第几天</param>
        public void Init(int dayX,int today,int awardId,int count)
        {
            int _dayOffset = dayX - today;
            m_Cell.InitByID(awardId, count);
            m_Cell.SetHightLight(dayX == today);

            m_Cell.SetCheckClaim(_dayOffset < 0);
            m_Cell.SetItemGray(_dayOffset < 0);

            m_Cell.SetText(string.Format(GameUtils.getString("sign_content3"), dayX),null,null);
        }

        public void ScaleTween(TweenCallback callback, float scaleFactor = 1.2f, float duration = 1.0f)
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(m_Cell.gameObject.transform.DOScale(Vector3.one * scaleFactor, duration));
            //mySequence.Insert(0, m_DataArray[m_CurDataArrayIdx].m_LayoutObject.DOMoveX(m_PrevPoint.position.x, 0.5f));
            if (callback != null)
                mySequence.AppendCallback(callback);
            mySequence.SetUpdate(true);
        }

    }

    private const int c_MaxTotalDay = 28;
    private const int c_MaxContinuousDay = 7;
    private const int c_MaxClaimCheckCount = 2;

    public static string Path = "UI_SignIn/UI_SignIn_2_10";

    /// <summary>
    /// 累计签到奖励ID
    /// </summary>
    private int m_TotalDayId;
    /// <summary>
    /// 连续签到奖励ID
    /// </summary>
    private int m_ContinuousDayId;
    private bool isFirstLogin;

    private int m_DayX;             //今天是累计签到的第几天
    private int m_ContinuousDayX;   //今天是连续签到的第几天

    private GameObject m_OriginAwardObject;
    private GameObject m_OriginSignInCellObject;
    private GameObject m_OriginStarObject;

    private Transform m_SignInLayout;   //累计签到奖励28个格子的挂载点
    private Transform m_ClaimCheckLayout;
    private Transform m_AwardCheckLayout;

    private GameObject m_ClaimCheckPanel;
    private GameObject m_AwardCheckPanel;

    private SortedList<int, LoginbonusTemplate> m_TotalBonusList;
    private SortedList<int, LoginbonusTemplate> m_ContinuousBonusList;

    private List<SignInCellData> m_SignInDataList;
    private List<AwardData> m_ClaimCheckDataList;
    private List<AwardData> m_AwardCheckList;
    //累计签到标题背景图片切换，图片顺序：已经领取\今天\未领取(后续算法依赖此顺序)
//    public Sprite[] m_TittleSpriteList = new Sprite[3];
    public Sprite m_BlackStar;
    public Sprite m_Star;
    public override void InitUIData()
    {
        base.InitUIData();
        m_OriginAwardObject = selfTransform.FindChild("OriginalObjectPanel/Award").gameObject;
        m_OriginSignInCellObject = selfTransform.FindChild("OriginalObjectPanel/SignInCell").gameObject;
        m_OriginStarObject = selfTransform.FindChild("OriginalObjectPanel/StarPanel").gameObject;

        m_SignInLayout = selfTransform.FindChild("SignInPanel/SignInLeftPanel/SignInLayout");
        m_ClaimCheckLayout = selfTransform.FindChild("ClaimCheckPanel/ClaimCheckLayout");
        m_AwardCheckLayout = selfTransform.FindChild("AwardCheckPanel/AwardCheckLayout");

        m_ClaimCheckPanel = selfTransform.FindChild("ClaimCheckPanel").gameObject;
        m_AwardCheckPanel = selfTransform.FindChild("AwardCheckPanel").gameObject;

        ObjectSelf objSelf = ObjectSelf.GetInstance();
        m_TotalDayId = objSelf.SignIn28;
        m_ContinuousDayId = objSelf.SignIn7;

        LoginbonusTemplate _totalBonusTable = (LoginbonusTemplate)DataTemplate.GetInstance().m_LoginbonusTable.getTableData(m_TotalDayId);
        LoginbonusTemplate _ContinuousBonusTable = (LoginbonusTemplate)DataTemplate.GetInstance().m_LoginbonusTable.getTableData(m_ContinuousDayId);

        m_DayX = _totalBonusTable.getDay();
        m_ContinuousDayX = _ContinuousBonusTable.getDay();

        m_TotalBonusList = GetBonusGroup(_totalBonusTable.getRoom(), DataTemplate.GetInstance().m_LoginbonusTable);
        m_ContinuousBonusList = GetBonusGroup(_ContinuousBonusTable.getRoom(), DataTemplate.GetInstance().m_LoginbonusTable);


    }

    /// <summary>
    /// 初始化签到面板所需数据
    /// </summary>
    /// <param name="signIn7">连续签到奖励ID</param>
    /// <param name="signIn28">累计签到奖励ID</param>
    /// <param name="isFirstLogin">是否是第一次登陆（是否会出现领取奖励确认弹出）</param>
    public void InitSignInManager(bool isFirstLogin)
    {
        this.isFirstLogin = isFirstLogin;
    }

    public override void InitUIView()
    {
        base.InitUIView();
        m_ClaimCheckPanel.SetActive(false);
        m_AwardCheckPanel.SetActive(false);

        InitAllText();
        InitSignInPanel(m_DayX);
        InitClaimCheckPanel(true);
        InitAwardCheckPanel(m_ContinuousDayX);
    }

    private void InitAllText()
    {
        m_SignInTopTittleText.text = GameUtils.getString("sign_title");
        m_SignInBackText.text = GameUtils.getString("common_button_return");
        m_RightPanelTipsText.text = GameUtils.getString("sign_content2");
        m_CheckAwardBtnText.text = GameUtils.getString("sign_button1");
        m_LeftPanelTittleText.text = string.Format(GameUtils.getString("sign_content1"), m_DayX);
        m_ClaimCheckTittleText.text = GameUtils.getString("sign_content5");
        m_ClaimBtnText.text = GameUtils.getString("sign_button2");
        m_AwardCheckTopTittleText.text = GameUtils.getString("sign_content8");
        m_AwardCheckOkBtnText.text = GameUtils.getString("common_button_ok");
    }

    /// <summary>
    /// 初始化累计奖励面板
    /// </summary>
    /// <param name="today">今天是累计奖励的第几天</param>
    private void InitSignInPanel(int today)
    {
        if (m_SignInDataList == null)
        {
            m_SignInDataList = new List<SignInCellData>(c_MaxTotalDay);
        }
        else
        { 
            if(m_SignInDataList.Count>0)
            {
                for (int i = 0; i < m_SignInDataList.Count; i++)
                {
                    Destroy(m_SignInDataList[i].m_Cell.gameObject);
                }
                m_SignInDataList.Clear();
            }

        }

        int _dayX = Mathf.Clamp(today, 0, c_MaxTotalDay-1);

        for (int i = 0; i < c_MaxTotalDay; i++)
        {
            var data = CreatSignInCellData(i + 1, today,m_SignInLayout);


            if (i + 1 == today)
            {
                if (isFirstLogin)
                    data.ScaleTween(TweenCallback, 1.25f, 1.6f);
                else
                    data.m_Cell.transform.localScale = Vector3.one * 1.25f;
            }

            m_SignInDataList.Add(data);
        }

    }

    /// <summary>
    /// 初始化领取确认面板
    /// </summary>
    /// <param name="isContinuous">是否存在连续奖励</param>
    private void InitClaimCheckPanel(bool isContinuous)
    {
        if (m_ClaimCheckDataList == null)
        {
            m_ClaimCheckDataList = new List<AwardData>(c_MaxClaimCheckCount);
        }
        else
        {
            if (m_ClaimCheckDataList.Count > 0)
            {
                for (int i = 0; i < m_ClaimCheckDataList.Count; i++)
                {
                    Destroy(m_ClaimCheckDataList[i].m_SelfGameObject);
                }
                m_ClaimCheckDataList.Clear();
            }

        }

        LoginbonusTemplate bonusData;
        bonusData = m_TotalBonusList[m_DayX];
        AwardData data;
        data = CreatAwardData(bonusData, GameUtils.getString("sign_content6"), m_ClaimCheckLayout,false);
        if (data != null)
            m_ClaimCheckDataList.Add(data);
        if(isContinuous)
        {
            bonusData = m_ContinuousBonusList[m_ContinuousDayX];
            data = CreatAwardData(bonusData, GameUtils.getString("sign_content7"), m_ClaimCheckLayout,false);
            if (data != null)
                m_ClaimCheckDataList.Add(data);
        }

    }

    /// <summary>
    /// 初始化查看连续签到奖励面板
    /// </summary>
    /// <param name="dayCount">已经连续领取几天</param>
    private void InitAwardCheckPanel(int dayCount)
    {
        if (m_AwardCheckList == null)
        {
            m_AwardCheckList = new List<AwardData>(c_MaxContinuousDay);
        }
        else
        {
            if (m_AwardCheckList.Count > 0)
            {
                for (int i = 0; i < m_AwardCheckList.Count; i++)
                {
                    Destroy(m_AwardCheckList[i].m_SelfGameObject);
                }
                m_AwardCheckList.Clear();
            }

        }

        string _tittleTemp = GameUtils.getString("sign_content3");
        for (int i = 0; i < c_MaxContinuousDay; i++)
        {
            LoginbonusTemplate bonusData = m_ContinuousBonusList[i+1];
            AwardData data;
            data = CreatAwardData(bonusData, string.Format(_tittleTemp, i + 1), m_AwardCheckLayout,true,i<dayCount);
            if(data != null)
                m_AwardCheckList.Add(data);
        }
    }

    /// <summary>
    /// 获取42表中所有room为指定值的条目
    /// </summary>
    /// <param name="room"></param>
    /// <returns></returns>
    private SortedList<int, LoginbonusTemplate> GetBonusGroup(int room, TableReader tableReader)
    {
        SortedList<int, LoginbonusTemplate> list = new SortedList<int, LoginbonusTemplate>();
        var dataTable = tableReader.getData();
        foreach (LoginbonusTemplate data in dataTable.Values)
        {
            if (data.getRoom() == room)
                list.Add(data.getDay(), data);
        }
        return list;
    }


    private GameObject InstantiateObject(GameObject originObj, Transform parent)
    {
        GameObject go = Instantiate(originObj, parent.position, parent.rotation) as GameObject;
        go.transform.SetParent(parent);
        go.transform.localScale = Vector3.one;
        return go;
    }

    /// <summary>
    /// 生成SignInCellData数据，在面板上挂载累计奖励的预制件
    /// </summary>
    /// <param name="dayX">生成的是第几天的累计奖励</param>
    /// <param name="today">今天是累计奖励的第几天</param>
    /// <returns></returns>
    private SignInCellData CreatSignInCellData(int dayX, int today, Transform layout)
    {
        int _awardId;
        SignInCellData data = new SignInCellData(layout);
        var bonusData = m_TotalBonusList[dayX];
        _awardId = bonusData.getRewardAndNum()[0];

        data.Init(dayX, today, _awardId, bonusData.getShowNum());
        return data;
    }

    /// <summary>
    /// 生成Award预制件
    /// </summary>
    /// <param name="bonusData">签到奖励表一条数据</param>
    /// <param name="tittle">预制件标题Text</param>
    /// <param name="layout">挂载点</param>
    /// <returns></returns>
    private AwardData CreatAwardData(LoginbonusTemplate bonusData, string tittle, Transform layout, bool isContinuous, bool isClaimed = false)
    {
        AwardData data;
        int _awardId = bonusData.getRewardAndNum()[0];
        data = new AwardData(InstantiateObject(m_OriginAwardObject, layout));
        data.Init(tittle, _awardId, bonusData.getShowNum(), isContinuous,isClaimed);
        return data;
    }
    /// <summary>
    /// StarPanel控制方法,maxStar必须大于star
    /// </summary>
    /// <param name="targetPoint">星级面板挂载位置</param>
    /// <param name="star">星级</param>
    /// <param name="maxStar">最大星级，未达到的部分由黑星背景填充</param>
    /// <param name="needBlackBG">是否需要黑星背景，如果不需要，参数maxStar无效</param>
    /// <returns></returns>
    private GameObject LoadStar(Transform targetPoint, int star, int maxStar, bool needBlackBG = true)
    {
        if (maxStar < star)
            maxStar = star;
        GameObject go = InstantiateObject(m_OriginStarObject,targetPoint);

        for (int i = 0; i < 5;i++ )
        {
            Image image = go.transform.GetChild(i).GetComponent<Image>();

            if (i < star)
            {
                image.sprite = m_Star;
            }
            else
            {
                image.sprite = m_BlackStar;
            }

            if (needBlackBG)
                image.gameObject.SetActive(i < maxStar);
            else
                image.gameObject.SetActive(i < star);

        }

        return go;
    }
    
    //返回null表示该道具不是英雄或者符文，不需要加星
    private GameObject AddStarPanel(Transform targetPoint, int awardId)
    {
        GameObject go = null;
        var bonusType = GameUtils.GetObjectClassById(awardId);
        if (bonusType == EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO)
        {
            HeroTemplate _heroTable = DataTemplate.GetInstance().m_HeroTable.getTableData(awardId) as HeroTemplate;
            if (_heroTable != null)
            {
                go = LoadStar(targetPoint, _heroTable.getQuality(), _heroTable.getMaxQuality());
            }
        }
        else if (bonusType == EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE)
        {
            ItemTemplate _runeTable = DataTemplate.GetInstance().m_ItemTable.getTableData(awardId) as ItemTemplate;
            if (_runeTable != null)
            {
                go = LoadStar(targetPoint, _runeTable.getQuality(), _runeTable.getQuality(), false);
            }
        }
        return go;
    }



    /***********callback*************/
    private void TweenCallback()
    {
        m_ClaimCheckPanel.SetActive(true);
    }

    protected override void OnClickSignInBackBtn()
    {
        DreamFaction.UI.Core.UI_HomeControler.Inst.ReMoveUI(Path);
    }

    protected override void OnClickClaimBtn()
    {
        m_ClaimCheckPanel.SetActive(false);
    }

    protected override void OnClickCheckAwardBtn()
    {
        m_AwardCheckPanel.SetActive(true);
    }
    protected override void OnClickAwardCheckOkBtn()
    {
        m_AwardCheckPanel.SetActive(false);
    }
    protected override void OnClickAwardCheckCloseBtn()
    {
        m_AwardCheckPanel.SetActive(false);
    }



    /******静态方法******/

    public static string GetAwardName(int AwardId)
    {
        string _result = null;

        EM_OBJECT_CLASS _awardClass =  GameUtils.GetObjectClassById(AwardId);
        switch (_awardClass)
        {
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES:
                ResourceindexTemplate _resTable = DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(AwardId) as ResourceindexTemplate;
                if (_resTable != null)
                {
                    _result = GameUtils.getString(_resTable.getName());
                }
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE:
                ItemTemplate _runeTable = DataTemplate.GetInstance().m_ItemTable.getTableData(AwardId) as ItemTemplate;
                if (_runeTable != null)
                {
                    _result = GameUtils.getString(_runeTable.getName());
                }
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON:
                ItemTemplate _itemTable = DataTemplate.GetInstance().m_ItemTable.getTableData(AwardId) as ItemTemplate;
                if (_itemTable != null)
                {
                    _result = GameUtils.getString(_itemTable.getName());
                }
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO:
                HeroTemplate _heroTable = DataTemplate.GetInstance().m_HeroTable.getTableData(AwardId) as HeroTemplate;
                if (_heroTable != null)
                {
                    _result = GameUtils.getString(_heroTable.getNameID());
                }
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_SKIN:
                ArtresourceTemplate _atrResTable = DataTemplate.GetInstance().m_ArtresourceTable.getTableData(AwardId) as ArtresourceTemplate;
                if (_atrResTable != null)
                {
                    _result = GameUtils.getString(_atrResTable.getNameID());
                }
                break;
            default:
                break;
        }

        return _result;
    }
    public static string GetAwardSpriteName(int AwardId)
    {
        string _result = null;

        EM_OBJECT_CLASS _awardClass = GameUtils.GetObjectClassById(AwardId);
        switch (_awardClass)
        {
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES:
                ResourceindexTemplate _resTable = DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(AwardId) as ResourceindexTemplate;
                if (_resTable != null)
                {
                    _result = _resTable.getIcon3();
                }
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE:
                ItemTemplate _itemTable = DataTemplate.GetInstance().m_ItemTable.getTableData(AwardId) as ItemTemplate;
                if (_itemTable != null)
                {
                    _result = _itemTable.getIcon_s();
                }
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON:
                ItemTemplate _runeTable = DataTemplate.GetInstance().m_ItemTable.getTableData(AwardId) as ItemTemplate;
                if (_runeTable != null)
                {
                    _result = _runeTable.getIcon_s();
                }
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO:
                HeroTemplate _heroTable = DataTemplate.GetInstance().m_HeroTable.getTableData(AwardId) as HeroTemplate;

                if (_heroTable != null)
                {
                    ArtresourceTemplate _atrResTable = DataTemplate.GetInstance().m_ArtresourceTable.getTableData(_heroTable.getArtresources()) as ArtresourceTemplate;
                    _result = _atrResTable.getHeadiconresource();
                }
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_SKIN:
                ArtresourceTemplate _atrResTable1 = DataTemplate.GetInstance().m_ArtresourceTable.getTableData(AwardId) as ArtresourceTemplate;
                if (_atrResTable1 != null)
                {
                    _result = _atrResTable1.getHeadiconresource();
                }
                break;
            default:
                break;
        }

        return _result;
    }
}
