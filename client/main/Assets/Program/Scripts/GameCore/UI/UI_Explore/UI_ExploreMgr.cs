using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using GNET;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using DreamFaction.LogSystem;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork.Data;

public class ExploreBaseUI
{

}

#region 探险小队UI
public class ExploreTeamUI
{
    public delegate void OnClick(int id);
    public event OnClick onClick = null;

    protected Text m_TitleTxt = null;
    protected Text m_DetailTxt = null;
    protected Image m_Bg = null;

    private GameObject mGo = null;
    private int mIdx = -1;

    public ExploreTeamUI(GameObject go)
    {
        mGo = go;
        
        Transform trans = mGo.transform;

        m_Bg = trans.FindChild("bg").GetComponent<Image>();
        m_TitleTxt = trans.FindChild("NameTxt").GetComponent<Text>();
        m_DetailTxt = trans.FindChild("DetailTxt").GetComponent<Text>();

        EventTriggerListener.Get(m_Bg.gameObject).onClick = OnItemClick;
    }

    public void SetID(int id)
    {
        mIdx = id;
    }

    public void SetTitle(string str)
    {
        m_TitleTxt.text = str;
    }

    public void SetDetail(string str)
    {
        m_DetailTxt.text = str;
    }

    public void SetOnClick(OnClick clickHandler)
    {
        onClick = clickHandler;
    }

    public void Destroy()
    {
        
    }

    void OnItemClick(GameObject go)
    {
        if (onClick != null)
        {
            onClick(mIdx);
        }
    }
}
#endregion

#region 任务UI
public class ExploreTaskUI
{
    public delegate void OnClick(int id);
    public event OnClick onClick = null;

    protected Text m_TitleTxt = null;
    //protected Text m_RewardDetailTxt = null;
    protected RichText m_RewardDetailRichTxt1 = null;
    protected RichText m_RewardDetailRichTxt2 = null;
    protected Text m_RewardTitleTxt = null;
    protected Text m_TimeTitleTxt = null;
    protected Text m_TimeTxt = null;
    protected Image m_SelectedImg = null;
    protected Text m_HintTxt = null;
    protected Button m_GetBtn = null;
    protected Text m_GetBtnTxt = null;
    protected Button m_TimeUpBtn = null;
    protected Text m_TimeUpBtnTxt = null;
    protected Text m_TimeUpCostTxt = null;
    protected Button m_CallBackBtn = null;
    protected Text m_CallBackBtnTxt = null;
    protected Image m_DoneImg = null;

    protected Button m_Btn = null;

    private GameObject mGo = null;

    private tanxianinit mData = null;
    private ExplorequestTemplate mTemplate = null;
    private EXPLORE_TASK_STATE mTaskState = EXPLORE_TASK_STATE.None;

    private string mHintNotEnoughStr = null;
    private string mHintCanStartStr = null;

    public tanxianinit TXData
    {
        get
        {
            return mData;
        }
    }

    public GameObject Go
    {
        get
        {
            return mGo;
        }
    }

    public ExploreTaskUI(GameObject go)
    {
        mGo = go;

        Transform trans = mGo.transform;

        m_TitleTxt = trans.FindChild("TitleTxt").GetComponent<Text>();
        m_RewardTitleTxt = trans.FindChild("RewardObj/TitleTxt").GetComponent<Text>();
        //m_RewardDetailTxt = trans.FindChild("RewardObj/RewardTxt").GetComponent<Text>();
        m_RewardDetailRichTxt1 = trans.FindChild("RewardObj/RewardTxt1").GetComponent<RichText>();
        m_RewardDetailRichTxt2 = trans.FindChild("RewardObj/RewardTxt2").GetComponent<RichText>();
        m_TimeTitleTxt = trans.FindChild("TimeObj/TimeTitleTxt").GetComponent<Text>(); 
        m_TimeTxt = trans.FindChild("TimeObj/TimeTxt").GetComponent<Text>(); 
        m_SelectedImg = trans.FindChild("SelectImg").GetComponent<Image>();
        m_HintTxt = trans.FindChild("HintText").GetComponent<Text>();
        m_GetBtn = trans.FindChild("Buttons/GetBtn").GetComponent<Button>();
        m_GetBtnTxt = trans.FindChild("Buttons/GetBtn/Text").GetComponent<Text>();
        m_TimeUpBtn = trans.FindChild("Buttons/TimeUpBtn").GetComponent<Button>();
        m_TimeUpBtnTxt = trans.FindChild("Buttons/TimeUpBtn/Text").GetComponent<Text>();
        m_TimeUpCostTxt = trans.FindChild("Buttons/TimeUpBtn/CostObj/Text").GetComponent<Text>();
        m_CallBackBtn = trans.FindChild("Buttons/CallBackBtn").GetComponent<Button>();
        m_CallBackBtnTxt = trans.FindChild("Buttons/CallBackBtn/Text").GetComponent<Text>();
        m_DoneImg = trans.FindChild("DoneImg").GetComponent<Image>();
        
        m_Btn = trans.FindChild("btnImg").GetComponent<Button>();

        m_Btn.onClick.AddListener(OnItemClick);

        m_GetBtn.onClick.AddListener(OnGetBtnClick);
        m_TimeUpBtn.onClick.AddListener(OnTimeUpBtnClick);
        m_CallBackBtn.onClick.AddListener(OnCallBackBtnClick);

        InitStr();
    }

    void InitStr()
    {
        m_RewardTitleTxt.text = GameUtils.getString("explore_cotnent2");
        m_GetBtnTxt.text = GameUtils.getString("explore_button3");
        m_TimeUpBtnTxt.text = GameUtils.getString("explore_button1");
        m_CallBackBtnTxt.text = GameUtils.getString("explore_button2");
        //m_HintTxt.text = GameUtils.getString("explore_cotnent7");
        mHintNotEnoughStr = GameUtils.getString("explore_cotnent17");
        mHintCanStartStr = GameUtils.getString("explore_cotnent16");
    }

    public void SetData(tanxianinit data)
    {
        mData = data;

        ExplorequestTemplate exploreT = DataTemplate.GetInstance().GetExplorequestTemplateById(data.tanxianid);
        mTemplate = exploreT;

        m_TitleTxt.text = GameUtils.getString(exploreT.getName());
        //m_RewardDetailTxt.text = GameUtils.getString(exploreT.getBonusDes()).Replace("#", "/n");
        string[] rewardContent = GameUtils.getString(exploreT.getBonusDes()).Split(new char[]{'#'});
        if (rewardContent != null && rewardContent.Length > 0)
        {
            m_RewardDetailRichTxt1.ShowRichText(rewardContent[0]);

            m_RewardDetailRichTxt2.ShowRichText(rewardContent.Length > 1 ? rewardContent[1] : "");
        }
        UpdatePerSec();
    }

    public void UpdatePerSec()
    {
        mTaskState = UI_ExploreModule.GetExploreTaskState(mData);

        switch (mTaskState)
        {
            case EXPLORE_TASK_STATE.NotStarted:
                m_TimeTitleTxt.text = GameUtils.getString("explore_cotnent4"); // 耗时{0};
                m_TimeTxt.text = TimeUtils.FormateTimeWithHMS(mTemplate.getTime());

                //英雄条件是否满足;
                int maxCount = DataTemplate.GetInstance().GetExploreNeedHeroCount(mTemplate);
                List<ObjectCard> cards = UI_ExploreModule.GetCardList(mData.tanxianid, EM_SORT_OBJECT_CARD.QUALITY);
                if ((cards != null) && (cards.Count >= maxCount))
                {
                    m_HintTxt.text = mHintCanStartStr;
                }
                else
                {
                    m_HintTxt.text = mHintNotEnoughStr;
                }

                m_HintTxt.gameObject.SetActive(true);
                m_GetBtn.gameObject.SetActive(false);
                m_TimeUpBtn.gameObject.SetActive(false);
                m_CallBackBtn.gameObject.SetActive(false);
                m_DoneImg.gameObject.SetActive(false);
                break;
            case EXPLORE_TASK_STATE.ExploringNotOver:
                m_TimeTitleTxt.text = GameUtils.getString("explore_cotnent5"); // 剩余{0}
                TimeSpan ts = UI_ExploreModule.GetTaskTimeToEnd(mData);
                m_TimeTxt.text = TimeUtils.FormateTimeWithHMS(ts);
                
                //计算加速消耗金钱;
                int minutes = UI_ExploreModule.GetTaskMinuteToEnd(mData);
                int cost = UI_ExploreModule.GetCostByMinutes(minutes);
                m_TimeUpCostTxt.text = cost.ToString();

                m_HintTxt.gameObject.SetActive(false);
                m_GetBtn.gameObject.SetActive(false);
                m_TimeUpBtn.gameObject.SetActive(true);
                GameUtils.SetBtnSpriteGrayState(m_TimeUpBtn, ObjectSelf.GetInstance().VipLevel < VIPModule.GetExploreAccelerateVipLv()); 
                m_CallBackBtn.gameObject.SetActive(false);
                m_DoneImg.gameObject.SetActive(false);
                break;
            case EXPLORE_TASK_STATE.ExploringOver:
                m_TimeTitleTxt.text = GameUtils.getString("explore_cotnent6");
                m_TimeTxt.text = string.Empty;

                m_HintTxt.gameObject.SetActive(false);
                m_GetBtn.gameObject.SetActive(true);
                m_TimeUpBtn.gameObject.SetActive(false);
                m_CallBackBtn.gameObject.SetActive(false);
                m_DoneImg.gameObject.SetActive(false);
                break;
            case EXPLORE_TASK_STATE.Over:
                m_TimeTitleTxt.text = string.Empty;
                m_TimeTxt.text = string.Empty;

                m_HintTxt.gameObject.SetActive(false);
                m_GetBtn.gameObject.SetActive(false);
                m_TimeUpBtn.gameObject.SetActive(false);
                m_CallBackBtn.gameObject.SetActive(false);
                m_DoneImg.gameObject.SetActive(true);
                break;
            default:
                break;
        }

    }

    public void SetParent(Transform parent)
    {
        mGo.transform.SetParent(parent, false);
        //mGo.transform.parent = parent;
        //mGo.transform.localScale = Vector3.one;
        //mGo.transform.localPosition = Vector3.zero;
    }
    
    void OnItemClick()
    {
        if (onClick != null)
        {
            onClick(mData.tanxianid);
        }
    }

    void OnGetBtnClick()
    {
        UI_ExploreModule.SendOtherProtocol(CTanXianOther.END_GET, mData.tanxianid);
    }
    void OnCallBackBtnClick()
    {
        UI_ExploreModule.OnCallBackBtnClick(mData.tanxianid);
    }
    void OnTimeUpBtnClick()
    {
        UI_ExploreModule.OnTimeUpBtnClick(mData.tanxianid);
    }

    public void SetOnClick(OnClick clickHandler)
    {
        onClick = clickHandler;
    }

    public void SetSelect(int id)
    {
        m_SelectedImg.gameObject.SetActive(mTaskState != EXPLORE_TASK_STATE.Over && mData.tanxianid == id);
    }

    public void Destroy()
    {
        onClick = null;
    }
}
#endregion

#region 探险队英雄UI
public class ExploreTeamHeroUI:CellItem
{
    public delegate void OnClick(X_GUID guid);
    public event OnClick onClick = null;

    protected Button m_Btn = null;
    protected Text m_NameTxt = null;
    protected GameObject m_LvObj = null;
    protected Text m_LvTxt = null;
    protected Image m_IconImg = null;
    protected Image m_ExploringImg = null;
    protected GameObject m_StarsBgObj = null;
    protected Image[] m_StarBgImgs = null;
    protected GameObject m_StarsFgObj = null;
    protected Image[] m_StarFgImgs = null;

    protected GameObject mGo = null;

    protected X_GUID mHeroGUID = null;

    private bool mIsAddListenerDone = false;

    public X_GUID HeroGUID
    {
        get
        {
            return mHeroGUID;
        }

        set
        {
            mHeroGUID = value;

            OnHeroGUIDChange(mHeroGUID);
        }
    }
    public override void InitUIData()
    {
        base.InitUIData();
        mGo = gameObject;

        Transform trans = mGo.transform;

        m_Btn = trans.FindChild("bg").GetComponent<Button>();
        m_NameTxt = trans.FindChild("NameTxt").GetComponent<Text>();
        m_LvObj = trans.FindChild("LvObj").gameObject;
        m_LvTxt = trans.FindChild("LvObj/LvTxt").GetComponent<Text>();
        m_IconImg = trans.FindChild("HeroIconImg").GetComponent<Image>();
        m_ExploringImg = trans.FindChild("ExploringImg").GetComponent<Image>();

        m_StarsBgObj = trans.FindChild("StarsBgObj").gameObject;
        m_StarBgImgs = new Image[5];
        for (int i = 0; i < 5; i++)
        {
            m_StarBgImgs[i] = trans.FindChild("StarsBgObj/DarkImg" + i).GetComponent<Image>();
        }

        m_StarsFgObj = trans.FindChild("StarsFgObj").gameObject;
        m_StarFgImgs = new Image[5];
        for (int m = 0; m < 5; m++)
        {
            m_StarFgImgs[m] = trans.FindChild("StarsFgObj/LightImg" + m).GetComponent<Image>();
        }

        if (!mIsAddListenerDone)
        {
            mIsAddListenerDone = true;
            m_Btn.onClick.AddListener(OnItemClick);
        }
    }
    public ExploreTeamHeroUI(GameObject go)
    {
        mGo = go;
        //替换Loop循环列表 需要在awake中进行初始化
        Transform trans = mGo.transform;

        m_Btn = trans.FindChild("bg").GetComponent<Button>();
        m_NameTxt = trans.FindChild("NameTxt").GetComponent<Text>();
        m_LvObj = trans.FindChild("LvObj").gameObject;
        m_LvTxt = trans.FindChild("LvObj/LvTxt").GetComponent<Text>();
        m_IconImg = trans.FindChild("HeroIconImg").GetComponent<Image>();
        m_ExploringImg = trans.FindChild("ExploringImg").GetComponent<Image>();

        m_StarsBgObj = trans.FindChild("StarsBgObj").gameObject;
        m_StarBgImgs = new Image[5];
        for (int i = 0; i < 5; i++)
        {
            m_StarBgImgs[i] = trans.FindChild("StarsBgObj/DarkImg" + i).GetComponent<Image>();
        }

        m_StarsFgObj = trans.FindChild("StarsFgObj").gameObject;
        m_StarFgImgs = new Image[5];
        for (int m = 0; m < 5; m++)
        {
            m_StarFgImgs[m] = trans.FindChild("StarsFgObj/LightImg" + m).GetComponent<Image>();
        }

        //m_Btn.onClick.AddListener(OnItemClick);
        if (!mIsAddListenerDone)
        {
            mIsAddListenerDone = true;
            m_Btn.onClick.AddListener(OnItemClick);
        }

        //替换Loop循环列表 需要在awake中进行初始化
    }

    void OnHeroGUIDChange(X_GUID guid)
    {
        if (guid == null)
        {
            m_NameTxt.text = string.Empty;
            m_LvObj.SetActive(false);
            m_ExploringImg.gameObject.SetActive(false);
            m_StarsBgObj.SetActive(false);
            m_StarsFgObj.SetActive(false);
            m_IconImg.gameObject.SetActive(false);
        }
        else
        {
            ObjectCard card = ObjectSelf.GetInstance().HeroContainerBag.FindHero(guid);

            if (card == null)
            {
                LogManager.LogError("不存在的ObjectCard信息，guid=" + guid);
                return;
            }

            //头像;
            HeroTemplate heroT = card.GetHeroRow();
            ArtresourceTemplate artT = DataTemplate.GetInstance().GetArtResourceTemplate(heroT.getArtresources());
            m_IconImg.preserveAspect = false;
            m_IconImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + artT.getHeadartresource());
            //m_IconImg.SetNativeSize();
            //名称;
            m_NameTxt.text = GameUtils.getString(heroT.getTitleID());

            //等级;
            m_LvTxt.text = card.GetHeroData().Level.ToString();

            //品质;
            int quality = heroT.getQuality();
            int maxQuality = heroT.getMaxQuality();
            for (int i = 0; i < 5; i++ )
            {
                m_StarBgImgs[i].gameObject.SetActive(i < maxQuality);
                m_StarFgImgs[i].gameObject.SetActive(i < quality);
            }

            m_LvObj.SetActive(true);
            //m_ExploringImg.gameObject.SetActive(true);
            m_StarsBgObj.SetActive(true);
            m_StarsFgObj.SetActive(true);
            m_IconImg.gameObject.SetActive(true);
        }
    }

    protected virtual void OnItemClick()
    {
        if (onClick != null)
        {
            onClick(mHeroGUID);
        }
    }

    public void SetOnClick(OnClick clickHandler)
    {
        onClick = clickHandler;
    }

    public void SetIsExploring(bool isExploring)
    {
        m_ExploringImg.gameObject.SetActive(isExploring);
    }

    public void Clear()
    {
        HeroGUID = null;
    }

    public void SetActive(bool active)
    {
        mGo.SetActive(active);
    }

    public virtual void Destroy()
    {
        Clear();

        onClick = null;

        m_StarBgImgs = null;
        m_StarFgImgs = null;
    }

}
#endregion

#region 英雄UI
public class ExploreHeroUI : ExploreTeamHeroUI
{
    protected Button m_SelectBtn = null;
    protected Text m_SelectBtnTxt = null;
    protected Button m_CancelBtn = null;
    protected Text m_CancleBtnTxt = null;

    public OnClick onSelectClick = null;
    public OnClick onCancelClick = null;
    private bool isInitFinish=false;
    private bool isAddListenerDone = false;

    public override void InitUIData()
    {
        base.InitUIData();
        Transform trans =transform;
        m_SelectBtn = trans.FindChild("SelectBtn").GetComponent<Button>();
        m_SelectBtnTxt = trans.FindChild("SelectBtn/Text").GetComponent<Text>();
        m_CancelBtn = trans.FindChild("CancleBtn").GetComponent<Button>();
        m_CancleBtnTxt = trans.FindChild("CancleBtn/Text").GetComponent<Text>();

        m_SelectBtnTxt.text = GameUtils.getString("heromelt_button5");
        m_CancleBtnTxt.text = GameUtils.getString("common_button_cancel");

        if (!isAddListenerDone)
        {
            isAddListenerDone = true;
            m_SelectBtn.onClick.AddListener(OnSelectBtnClick);
            m_CancelBtn.onClick.AddListener(OnCancelBtnClick);
        }
        isInitFinish = true;
    }
    public ExploreHeroUI(GameObject go):base(go)
    {
        Transform trans = mGo.transform;

        m_SelectBtn = trans.FindChild("SelectBtn").GetComponent<Button>();
        m_SelectBtnTxt = trans.FindChild("SelectBtn/Text").GetComponent<Text>();
        m_CancelBtn = trans.FindChild("CancleBtn").GetComponent<Button>();
        m_CancleBtnTxt = trans.FindChild("CancleBtn/Text").GetComponent<Text>();

        m_SelectBtnTxt.text = GameUtils.getString("heromelt_button5");
        m_CancleBtnTxt.text = GameUtils.getString("common_button_cancel");

        if (!isAddListenerDone)
        {
            isAddListenerDone = true;
            m_SelectBtn.onClick.AddListener(OnSelectBtnClick);
            m_CancelBtn.onClick.AddListener(OnCancelBtnClick);
        }
        //m_SelectBtn.onClick.AddListener(OnSelectBtnClick);
        //m_CancelBtn.onClick.AddListener(OnCancelBtnClick);
    }

    protected override void OnItemClick()
    {
        //无操作;
        return;
    }

    public void SetSelectClick(OnClick handler)
    {
        onSelectClick = handler;
    }

    public void SetCancelClick(OnClick handler)
    {
        onCancelClick = handler;
    }

    void OnSelectBtnClick()
    {
        if (onSelectClick != null)
        {
            onSelectClick(mHeroGUID);
        }
    }

    void OnCancelBtnClick()
    {
        if (onCancelClick != null)
        {
            onCancelClick(mHeroGUID);
        }
    }

    public override void Destroy()
    {
        m_StarBgImgs = null;
        m_StarFgImgs = null;

        onSelectClick = null;
        onCancelClick = null;
        
        base.Destroy();
    }

    /// <summary>
    /// 设置选择按钮、取消按钮显示状态;
    /// 0-显示取消，隐藏选择按钮;
    /// 1-显示选择，隐藏取消按钮;
    /// </summary>
    /// <param name="type"></param>
    public void SetShowBtnType(int type)
    {
        switch (type)
        {
            case 0:
                m_CancelBtn.gameObject.SetActive(true);
                m_SelectBtn.gameObject.SetActive(false);
                break;
            case 1:
                m_CancelBtn.gameObject.SetActive(false);
                m_SelectBtn.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void SetSelectBtnGrey(bool isGrey)
    {
        GameUtils.SetBtnSpriteGrayState(m_SelectBtn, isGrey);
    }

    public void SetParent(Transform parent)
    {
        mGo.transform.SetParent(parent, false);
        //mGo.transform.parent = parent;
        //mGo.transform.localScale = Vector3.one;
        //mGo.transform.localPosition = Vector3.zero;
    }
    public void FillData(OnClick select,OnClick cancel)
    {
        if (!isInitFinish)
        {
            InitUIData();
        }
        SetSelectClick(select);
        SetCancelClick(cancel);
    }
}
#endregion

#region 场景任务点UI
public class ExploreTaskPointUI
{
    public delegate void OnClick(int id);
    public event OnClick onClick = null;

    protected Text m_TitleTxt = null;
    protected Button m_IconBtn = null;

    private GameObject mGo = null;

    private ExplorequestTemplate mExploreT = null;

    public ExploreTaskPointUI(GameObject go)
    {
        mGo = go;

        Transform trans = mGo.transform;

        m_TitleTxt = trans.FindChild("NameTxt").GetComponent<Text>();
        m_IconBtn = trans.FindChild("bgImg").GetComponent<Button>();

        m_IconBtn.onClick.AddListener(OnIconBtnClick);
    }

    public void SetParent(Transform parent)
    {
        mGo.transform.SetParent(parent, false);
        //mGo.transform.parent = parent;
        //mGo.transform.localScale = Vector3.one;
        //mGo.transform.localPosition = Vector3.zero;
    }

    void OnIconBtnClick()
    {
        if (onClick != null)
        {
            onClick(mExploreT.getId());
        }
    }

    public void SetOnClick(OnClick clickHandler)
    {
        onClick = clickHandler;
    }

    public void SetTemplateData(ExplorequestTemplate data)
    {
        mExploreT = data;

        m_TitleTxt.text = GameUtils.getString(data.getName());

        //位置;
        Vector2 pos = new Vector2(data.getCoordinate()[0], data.getCoordinate()[1]);
        mGo.transform.localPosition = pos;
    }

    public void Destroy()
    {
        onClick = null;
    }

    //设置选中状态显示;
    public void SetSelectState(int selectedId)
    {
        bool isSelected = selectedId == mExploreT.getId();
    }
}
#endregion
public class UI_ExploreMgr : CustomUI
{
    public static readonly string UI_ResPath = "UI_Explore/UI_Explore_02_07";

    private static UI_ExploreMgr mInst = null;


#region TopPanel
    protected Text m_TitleTxt = null;
    protected Button m_ReturnBtn = null;
#endregion
#region ScenePanel
    protected Text m_ChapterTxt = null;
    protected Text m_ChapterNameTxt = null;
    protected Button m_ChapterLeftBtn = null;
    protected Button m_ChapterRightBtn = null;
    protected Text m_TeamOutCountTxt = null;
    protected GameObject m_ChapterListObj = null;
    protected GameObject m_ChapterItemObj = null;
    protected GameObject[] m_TeamItemObj = null;
#endregion
#region TaskPanel
    protected Button m_RefreshBtn = null;
    protected Text m_RefreshBtnTxt = null;
    protected Text m_RefreshCostTxt = null;
    protected ScrollRect m_TaskScrollRect = null;
    protected Scrollbar m_TaskScrollBar = null;
    protected GameObject m_TaskListObj = null;
    protected GameObject m_TaskItemObj = null;
#endregion
#region DetailPanel
    protected GameObject m_DetailPanelObj = null;
    protected Text m_TaskNameTxt = null;
    protected Button m_CloseBtn = null;
    protected Text m_RewardTitleTxt = null;
    protected Text m_RewardContentTxt = null;
    protected RichText m_RewardContentRichTxt = null;
    protected Text m_RewardDetailTxt = null;
    protected Text m_TaskCoditionTxt = null;
    protected Text m_TaskCoditionDesTxt = null;
    protected GameObject[] m_ExploreTeamObj = null;
    protected Button m_GetBtn = null;
    protected Text m_GetBtnTxt = null;
    protected Button m_AutoMatchBtn = null;
    protected Text m_AutoMatchBtnTxt = null;
    protected Button m_StartExploreBtn = null;
    protected Text m_StartExploreCostTxt = null;
    protected Text m_StartExploreBtnTxt = null;
    protected Button m_CallBackBtn = null;
    protected Text m_CallBackBtnTxt = null;
    protected Button m_TimeUpBtn = null;
    protected Text m_TimeUpBtnTxt = null;
    protected Text m_TimeUpBtnCostTxt = null;
#endregion
#region HeroListPanel
    protected GameObject m_HeroListPanel = null;
    protected GameObject m_HeroItemObj = null;
    protected GameObject m_HeroListObj = null;
    protected GameObject m_NullObj = null;
    protected Text m_NullObjTxt = null;
    protected Button m_GetHeroLeftBtn = null;
    protected Text m_GetHeroLeftBtnTxt = null;
    protected Button m_GetHeroRightBtn = null;
    protected Text m_GetHeroRightBtnTxt = null;
    protected GameObject m_BottomObj = null;
    protected Button m_SortQualityBtn = null;
    protected Text m_SortQualityBtnTxt = null;
    protected Button m_SortLvBtn = null;
    protected Text m_SortLvBtnTxt = null;
    protected Text m_BagCountTxt = null;
    protected UI_SlideBtn m_SlideBtn = null;
    protected Text m_SlideBtnTxt = null;
    protected LoopLayout m_LoopLayout=null;
#endregion

    private bool mStartBtnIsGrey = true;                  //出站按钮是否置灰;

    private ExploreTeamUI[] mTeamUIs = new ExploreTeamUI[4];
    private ExploreTeamHeroUI[] mTeamHeroUIs = new ExploreTeamHeroUI[5];

    private List<ExploreTaskPointUI> mExplorePointUIList = new List<ExploreTaskPointUI>();
    private List<ExploreTaskUI> mExploreTaskUIList = new List<ExploreTaskUI>();
    private List<ExploreHeroUI> mExploreHeroUIList = new List<ExploreHeroUI>();

    private List<X_GUID> mExploreHeroKeys = new List<X_GUID>();   //存储即将出战英雄的key;

    private int mCurChapterId = -1;                 //当前显示的章节id;
    private int mCurExploreId = -1;                 //当前选中的探险任务id;
    private int mOldExploreId = -1;                 //上次选中的探险任务id;

    private float mTimeDelta = 0f;

    private int mRefreshCost = 0;

    private bool mAutoMatchBtnGrey = false;         //自动匹配按钮置灰;

    private IFunctionTipsController m_TipsController;

    public List<ObjectCard> datas;//英雄信息列表

    public UI_ExploreMgr Inst
    {
        get
        {
            return mInst;
        }
    }

    int CurChapterId
    {
        get
        {
            return mCurChapterId;
        }

        set
        {
            if (value == mCurChapterId)
            {
                return;
            }

            OnChapterChanged(mCurChapterId, value);

            mCurChapterId = value;
        }
    }

    int CurExploreId
    {
        get
        {
            return mCurExploreId;
        }
        set
        {
            if (value == mCurExploreId)
            {
                UpdateTaskUI(value);
                UpdateTaskDetailUI(value);

                return;
            }

            mOldExploreId = mCurChapterId;

            mCurExploreId = value;

            OnExploreChanged(mOldExploreId, value);
        }
    }

    /// <summary>
    /// 当前出站英雄队列是否满员;
    /// </summary>
    public bool IsTeamHeroFull
    {
        get
        {
            ExplorequestTemplate exploreT = DataTemplate.GetInstance().GetExplorequestTemplateById(mCurExploreId);

            int maxCount = DataTemplate.GetInstance().GetExploreNeedHeroCount(exploreT);
            return mExploreHeroKeys.Count >= maxCount;
        }
    }

    public override void InitUIData()
    {
        base.InitUIData();

        mInst = this;

        captionPath = "ZouMaDeng";

#region TopPanel
        m_TitleTxt = selfTransform.FindChild("TopPanel/TitleButton_0/Text").GetComponent<Text>();
        m_ReturnBtn = selfTransform.FindChild("TopPanel/Btn_back").GetComponent<Button>();
#endregion
#region ScenePanel
        m_ChapterTxt = selfTransform.FindChild("ScenePanel/TopPanel/TitleTxt").GetComponent<Text>();
        m_ChapterNameTxt = selfTransform.FindChild("ScenePanel/TopPanel/ChapterTxt").GetComponent<Text>();
        m_ChapterLeftBtn = selfTransform.FindChild("ScenePanel/TopPanel/LeftImg").GetComponent<Button>();
        m_ChapterRightBtn = selfTransform.FindChild("ScenePanel/TopPanel/RightImg").GetComponent<Button>();
        m_TeamOutCountTxt = selfTransform.FindChild("ScenePanel/TeamPanel/HintObj/CountTxt").GetComponent<Text>();
        m_ChapterListObj = selfTransform.FindChild("ScenePanel/SceneObj/SceneListObj").gameObject;
        m_ChapterItemObj = selfTransform.FindChild("ScenePanel/SceneObj/Items/Item").gameObject;
        m_TeamItemObj = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            m_TeamItemObj[i] = selfTransform.FindChild("ScenePanel/TeamPanel/TeamList/Team" + i).gameObject;
        }
#endregion
#region TaskPanel
        m_RefreshBtn = selfTransform.FindChild("TaskPanel/BottomObj/RefreshBtn").GetComponent<Button>();
        m_RefreshBtnTxt = selfTransform.FindChild("TaskPanel/BottomObj/RefreshBtn/Text").GetComponent<Text>();
        m_RefreshCostTxt = selfTransform.FindChild("TaskPanel/BottomObj/RefreshBtn/CostObj/Text").GetComponent<Text>();
        m_TaskScrollRect = selfTransform.FindChild("TaskPanel/TaskObj").GetComponent<ScrollRect>();
        m_TaskScrollBar = selfTransform.FindChild("TaskPanel/Scrollbar").GetComponent<Scrollbar>();
        m_TaskListObj = selfTransform.FindChild("TaskPanel/TaskObj/TaskListObj").gameObject;
        m_TaskItemObj = selfTransform.FindChild("TaskPanel/TaskObj/Items/Item").gameObject;
#endregion
#region DetailPanel
        m_DetailPanelObj = selfTransform.FindChild("DetailPanel").gameObject;
        m_TaskNameTxt = selfTransform.FindChild("DetailPanel/TopObj/TitleTxt").GetComponent<Text>();
        m_CloseBtn = selfTransform.FindChild("DetailPanel/TopObj/ExitBtn").GetComponent<Button>();
        m_RewardTitleTxt = selfTransform.FindChild("DetailPanel/RewardObj/TitleTxt").GetComponent<Text>();
        //m_RewardContentTxt = selfTransform.FindChild("DetailPanel/RewardObj/RewardTxt").GetComponent<Text>();
        m_RewardContentRichTxt = selfTransform.FindChild("DetailPanel/RewardObj/RewardTxt").GetComponent<RichText>();
        m_RewardDetailTxt = selfTransform.FindChild("DetailPanel/RewardObj/DetailTxt").GetComponent<Text>();
        m_TaskCoditionTxt = selfTransform.FindChild("DetailPanel/ConditionObj/TitleTxt").GetComponent<Text>();
        m_TaskCoditionDesTxt = selfTransform.FindChild("DetailPanel/ConditionObj/ConditionTxt").GetComponent<Text>();
        m_ExploreTeamObj = new GameObject[5];
        for (int i = 0; i < 5; i++)
        {
            m_ExploreTeamObj[i] = selfTransform.FindChild("DetailPanel/HeroListObj/Item" + (i + 1)).gameObject;
        }
        m_GetBtn = selfTransform.FindChild("DetailPanel/GetBtn").GetComponent<Button>();
        m_GetBtnTxt = selfTransform.FindChild("DetailPanel/GetBtn/Text").GetComponent<Text>();
        m_AutoMatchBtn = selfTransform.FindChild("DetailPanel/AutoMatchBtn").GetComponent<Button>();
        m_AutoMatchBtnTxt = selfTransform.FindChild("DetailPanel/AutoMatchBtn/Text").GetComponent<Text>();
        m_StartExploreBtn = selfTransform.FindChild("DetailPanel/StartBtn").GetComponent<Button>();
        m_StartExploreBtnTxt = selfTransform.FindChild("DetailPanel/StartBtn/Text").GetComponent<Text>();
        m_StartExploreCostTxt = selfTransform.FindChild("DetailPanel/StartBtn/CostObj/Text").GetComponent<Text>();
        m_CallBackBtn = selfTransform.FindChild("DetailPanel/CallBackBtn").GetComponent<Button>();
        m_CallBackBtnTxt = selfTransform.FindChild("DetailPanel/CallBackBtn/Text").GetComponent<Text>();
        m_TimeUpBtn = selfTransform.FindChild("DetailPanel/TimeUpBtn").GetComponent<Button>();
        m_TimeUpBtnTxt = selfTransform.FindChild("DetailPanel/TimeUpBtn/Text").GetComponent<Text>();
        m_TimeUpBtnCostTxt = selfTransform.FindChild("DetailPanel/TimeUpBtn/CostObj/Text").GetComponent<Text>();
#endregion
#region HeroListPanel
        m_HeroListPanel = selfTransform.FindChild("HeroListPanel").gameObject;
        //m_HeroItemObj = selfTransform.FindChild("HeroListPanel/HerosObj/Items/Item").gameObject;
        m_HeroListObj = selfTransform.FindChild("HeroListPanel/HerosObj/HeroListObj").gameObject;
        m_NullObj = selfTransform.FindChild("HeroListPanel/NullObj").gameObject;
        m_NullObjTxt = selfTransform.FindChild("HeroListPanel/NullObj/Hint/Text").GetComponent<Text>();
        m_GetHeroLeftBtn = selfTransform.FindChild("HeroListPanel/NullObj/LeftBtn").GetComponent<Button>();
        m_GetHeroLeftBtnTxt = selfTransform.FindChild("HeroListPanel/NullObj/LeftBtn/Text").GetComponent<Text>();
        m_GetHeroRightBtn = selfTransform.FindChild("HeroListPanel/NullObj/RightBtn").GetComponent<Button>();
        m_GetHeroRightBtnTxt = selfTransform.FindChild("HeroListPanel/NullObj/RightBtn/Text").GetComponent<Text>();
        m_BottomObj = selfTransform.FindChild("HeroListPanel/BottomObj").gameObject;
        m_SortQualityBtn = selfTransform.FindChild("HeroListPanel/BottomObj/Bottom/sort_quality").GetComponent<Button>();
        m_SortQualityBtnTxt = selfTransform.FindChild("HeroListPanel/BottomObj/Bottom/sort_quality/Text").GetComponent<Text>();
        m_SortLvBtn = selfTransform.FindChild("HeroListPanel/BottomObj/Bottom/sort_lv").GetComponent<Button>();
        m_SortLvBtnTxt = selfTransform.FindChild("HeroListPanel/BottomObj/Bottom/sort_lv/Text").GetComponent<Text>();
        m_BagCountTxt = selfTransform.FindChild("HeroListPanel/BottomObj/Bottom/CountObj/BagCount_txt").GetComponent<Text>();
        m_SlideBtn = selfTransform.FindChild("HeroListPanel/BottomObj/Bottom/sort").GetComponent<UI_SlideBtn>();
        m_SlideBtnTxt = selfTransform.FindChild("HeroListPanel/BottomObj/Bottom/sort/Text").GetComponent<Text>();
        m_LoopLayout = selfTransform.FindChild("HeroListPanel/HerosObj/HeroListObj").GetComponent<LoopLayout>();
         
#endregion

        for (int i = 0, j = m_TeamItemObj.Length; i < j;  i++)
        {
            mTeamUIs[i] = new ExploreTeamUI(m_TeamItemObj[i]);
            mTeamUIs[i].SetID(i + 1);
            mTeamUIs[i].SetOnClick(OnTeamUIClick);
        }

        for (int i = 0, j = m_ExploreTeamObj.Length; i < j; i++)
        {
            mTeamHeroUIs[i] = new ExploreTeamHeroUI(m_ExploreTeamObj[i]);
            mTeamHeroUIs[i].SetOnClick(OnTeamHeroAddBtnClick);
        }

        InitStrs();
        InitEventListener();

        mRefreshCost = DataTemplate.GetInstance().GetGameConfig().getExplore_refresh_cost();
        m_RefreshCostTxt.text = mRefreshCost.ToString();

        m_TipsController = CreateFunctionTipsController();
    }

    void InitStrs()
    {
        m_TitleTxt.text = GameUtils.getString("explore_cotnent1");
        m_RefreshBtnTxt.text = GameUtils.getString("explore_button4");
        m_RewardTitleTxt.text = GameUtils.getString("explore_cotnent2");
        m_TaskCoditionTxt.text = GameUtils.getString("explore_cotnent8");
        m_GetBtnTxt.text = GameUtils.getString("explore_button3");
        m_AutoMatchBtnTxt.text = GameUtils.getString("explore_button5");
        m_StartExploreBtnTxt.text = GameUtils.getString("explore_button6");
        m_CallBackBtnTxt.text = GameUtils.getString("explore_button2");
        m_TimeUpBtnTxt.text = GameUtils.getString("explore_button1");
        m_NullObjTxt.text = GameUtils.getString("explore_bubble3");
        m_GetHeroLeftBtnTxt.text = GameUtils.getString("heromelt_button8"); //招募;
        m_GetHeroRightBtnTxt.text = GameUtils.getString("heromelt_button9");     //冒险管卡;
        m_SortQualityBtnTxt.text = GameUtils.getString("hero_info_sort_quality");
        m_SortLvBtnTxt.text = GameUtils.getString("hero_info_sort_level");
        m_SlideBtnTxt.text = GameUtils.getString("hero_info_sort_quality");
    }

    void InitEventListener()
    {
        //Top
        m_ReturnBtn.onClick.AddListener(OnReturnBtnClick);

        //Scene
        m_ChapterLeftBtn.onClick.AddListener(OnPageLeftBtnClick);
        m_ChapterRightBtn.onClick.AddListener(OnPageRightBtnClick);

        //Task
        m_RefreshBtn.onClick.AddListener(OnRefreshBtnClick);

        //Detail
        m_AutoMatchBtn.onClick.AddListener(OnAutoMatchBtnClick);
        m_StartExploreBtn.onClick.AddListener(OnStartExploreBtnClick);
        m_CallBackBtn.onClick.AddListener(OnCallBackBtnClick);
        m_TimeUpBtn.onClick.AddListener(OnTimeUpBtnClick);
        m_GetBtn.onClick.AddListener(OnGetBtnClick);
        m_CloseBtn.onClick.AddListener(OnCloseBtnClick);

        //HeroList
        m_SortLvBtn.onClick.AddListener(OnSortLvBtnClick);
        m_SortQualityBtn.onClick.AddListener(OnSortQualityBtnClick);
        m_GetHeroLeftBtn.onClick.AddListener(OnGetHeroLeftBtnClick);
        m_GetHeroRightBtn.onClick.AddListener(OnGetHeroRightBtnClick);

        GameEventDispatcher.Inst.addEventListener(GameEventID.G_ExploreData_Update, OnExploreDataChange);
        GameEventDispatcher.Inst.addEventListener(GameEventID.G_ExploreTeamCallBack, OnExploreCallBackSucess);
        GameEventDispatcher.Inst.addEventListener(GameEventID.G_ExploreTeamTimeUp, OnExploreTimeUpSucess);
        GameEventDispatcher.Inst.addEventListener(GameEventID.G_ExploreTeamGetReward, OnExploreGetRewardSucess);
        GameEventDispatcher.Inst.addEventListener(GameEventID.G_ExploreTeamRefreshTasks, OnExploreRefreshTasksSucess);
        GameEventDispatcher.Inst.addEventListener(GameEventID.G_ExploreTeamBeginTasks, OnExploreBeginSucess);
        GameEventDispatcher.Inst.addEventListener(GameEventID.Net_RefreshHero, OnHeroListChange);
        GameEventDispatcher.Inst.addEventListener(GameEventID.Net_RemoveHero, OnHeroListChange);
    }

    public override void InitUIView()
    {
        base.InitUIView();

        //设置默认展示章节id为最新章节;
        int maxChapterId = ObjectSelf.GetInstance().GetAllExploreTaskData().Count;
        CurChapterId = maxChapterId;

        //显示小队信息;
        UpdateExploreTeamUIs();
        m_TipsController.Refresh();
    }

    public override void UpdateUIData()
    {
        base.UpdateUIData();

        mTimeDelta += Time.deltaTime;

        if (mTimeDelta >= 1f)
        {
            mTimeDelta = 0f;

            OneSecondHandler();
        }
    }

    void OneSecondHandler()
    {
        //TaskPanel;
        for (int i = 0; i < mExploreTaskUIList.Count; i++ )
        {
            mExploreTaskUIList[i].UpdatePerSec();
        }
        
        //DetailPanel;
        if (m_DetailPanelObj.activeSelf)
        {
            UpdateTaskDetailUI(mCurExploreId);
        }

        //提示框检测;
        CheckAndRefreshHintUI();
    }

    /// <summary>
    /// 检测UI_RechargeBox提示框是否打开，如果打开且显示时间加速信息，则刷新界面;
    /// </summary>
    void CheckAndRefreshHintUI()
    {
        if (UI_RechargeBox.Inst != null && UI_RechargeBox.Inst.gameObject.activeSelf && (UI_RechargeBox.CurOpenType == EM_RECHARGEBOX_OPEN_TYPE.EXPLORE_TIMEUP_HINT))
        {
            tanxianinit mData = ObjectSelf.GetInstance().GetExploreTaskDataById((int)(UI_RechargeBox.Data));
            int minutes = UI_ExploreModule.GetTaskMinuteToEnd(mData);
            int cost = UI_ExploreModule.GetCostByMinutes(minutes);
            
            UI_RechargeBox box = UI_RechargeBox.Inst;
            box.SetConNum(cost.ToString());
            box.SetConsume_Image(GameUtils.GetSpriteByResourceType(EM_RESOURCE_TYPE.Gold));
            box.SetMoneyInfoActive(true);
            box.SetMoneyInfo((int)EM_RESOURCE_TYPE.Gold, ObjectSelf.GetInstance().Gold);
        }
    }

    public override void OnReadyForClose()
    {
        base.OnReadyForClose();

        StopAllCoroutines();

        SetAutoMatchBtnGrey(false);
        
        //执行所有界面的关闭逻辑;
        ClearExploreTeamHeroUIs();  //DetailPanel清空
        mTeamHeroUIs = null;
        CloseExploreDetailUI();     //DetailPanel关闭;
        ClearExploreHeroListUIs();  //heroListPanel清空;
        CloseHeroListUI();          //关闭HeroListPanel界面;
        ClearExplorePointUIs();     //ScenePanel清空
        ClearExploreTeamUIs();
        mTeamUIs = null;
        ClearExploreTaskUIs();      //TaskPanel清空;

        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_ExploreData_Update, OnExploreDataChange);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_ExploreTeamCallBack, OnExploreCallBackSucess);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_ExploreTeamTimeUp, OnExploreTimeUpSucess);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_ExploreTeamGetReward, OnExploreGetRewardSucess);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_ExploreTeamRefreshTasks, OnExploreRefreshTasksSucess);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_ExploreTeamBeginTasks, OnExploreBeginSucess);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.Net_RefreshHero, OnHeroListChange);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.Net_RemoveHero, OnHeroListChange);
    }

    void OnDestroy()
    {
        UIState = BaseUI.UIStateEnum.ReadyForClose;

        base.OnDestroy();
    }

    /// <summary>
    /// 界面右上角，返回按钮点击;
    /// </summary>
    void OnReturnBtnClick()
    {
        m_SlideBtn.OnClose();

        UI_HomeControler.Inst.ReMoveUI(UI_ResPath);
    }

    void OnChapterChanged(int oldid, int newid)
    {
        //更新章节信息;
        UpdateChapterInfo(newid);

        //更新按钮显示状态;
        UpdatePageBtnState(newid);

        //创建任务点;
        CreateExplorePointUIs(newid);

        //创建任务列表;
        CreateExploreTaskUIs(newid);

        SetAutoMatchBtnGrey(false);
    }

    void OnExploreChanged(int oldid, int newid)
    {
        mExploreHeroKeys.Clear();

        if (newid != -1)
        {
            //刷新任务列表界面;
            UpdateTaskUI(newid);

            //刷新任务详情界面;
            UpdateTaskDetailUI(newid);

            SetAutoMatchBtnGrey(false);
        }
        else//清空选中状态;
        {
            //清空任务选中状态;
            ClearTaskUISelect();

            //清空任务点选中状态;
            ClearExplorePointUISelect();
        }
    }

    /// <summary>
    /// 探险成功;
    /// </summary>
    void OnExploreBeginSucess()
    {
        //关闭DetailPanel界面;
        ClearExploreTeamHeroUIs();
        CloseExploreDetailUI();
    }

    /// <summary>
    /// 探险任务数据改变;
    /// </summary>
    void OnExploreDataChange()
    {
        //重新构造所有的taskPointUi;
        CreateExplorePointUIs(mCurChapterId);
        //重新构造所有的taskUi;
        CreateExploreTaskUIs(mCurChapterId);

        //关闭DetailPanel界面;
        //ClearExploreTeamHeroUIs();
        //CloseExploreDetailUI();

        //关闭HeroListPanel界面;
        ClearExploreHeroListUIs();
        CloseHeroListUI();
        mCurSortType = EM_SORT_OBJECT_CARD.NONE;

        //刷新小队信息;
        UpdateExploreTeamUIs();
        m_TipsController.Refresh();
    }

    /// <summary>
    /// 探险任务，队伍成功召回;
    /// </summary>
    /// <param name="ge"></param>
    void OnExploreCallBackSucess(GameEvent ge)
    {
        bool needClose = true;
        if (ge != null && ge.data != null)
        {
            int txid = (int)ge.data;

            if (txid == mCurExploreId)
            {
                needClose = false;

                UpdateTaskDetailUI(mCurExploreId);
                UpdateTaskUI(mCurExploreId);
            }

        }

        if(needClose)
        {
            //关闭DetailPanel界面;
            ClearExploreTeamHeroUIs();
            CloseExploreDetailUI();
        }

    }
    /// <summary>
    /// 探险任务--时间加速;
    /// </summary>
    /// <param name="ge"></param>
    void OnExploreTimeUpSucess(GameEvent ge)
    {
        // 时间加速也可以不关闭DetailPanel界面;
        //if (ge == null || ge.data == null)
        //{
        //    int txid = (int)ge.data;

        //    if (txid == mCurExploreId)
        //    {
        //        UpdateTaskDetailUI(mCurExploreId);
        //    }
        //    else
        //    {
        //        //关闭DetailPanel界面;
        //        ClearExploreTeamHeroUIs();
        //        CloseExploreDetailUI();
        //    }
        //}
        
        //目前策划设定--关闭DetailPanel;
        ClearExploreTeamHeroUIs();
        CloseExploreDetailUI();
    }

    /// <summary>
    /// 探险任务，获取奖励;
    /// </summary>
    /// <param name="ge"></param>
    void OnExploreGetRewardSucess(GameEvent ge)
    {
        //关闭DetailPanel;
        ClearExploreTeamHeroUIs();
        CloseExploreDetailUI();
    }
    /// <summary>
    /// 探险任务，队伍成功召回;
    /// </summary>
    /// <param name="ge"></param>
    void OnExploreRefreshTasksSucess(GameEvent ge)
    {
        //关闭DetailPanel;
        ClearExploreTeamHeroUIs();
        CloseExploreDetailUI();
    }
#region ScenePanel
    void OnPageLeftBtnClick()
    {
        if (IsFirstChapter(mCurChapterId))
        {
            return;
        }

        CurChapterId--;
    }

    void OnPageRightBtnClick()
    {
        if (IsLastChapter(mCurChapterId))
        {
            return;
        }

        CurChapterId++;
    }

    void UpdatePageBtnState(int chapterId)
    {
        GameUtils.SetBtnSpriteGrayState(m_ChapterLeftBtn, IsFirstChapter(chapterId));
        GameUtils.SetBtnSpriteGrayState(m_ChapterRightBtn, IsLastChapter(chapterId));
    }

    bool IsLastChapter(int num)
    {
        int total = ObjectSelf.GetInstance().GetAllExploreTaskData().Count;

        return num >= total;
    }

    bool IsFirstChapter(int num)
    {
        return num <= 1;
    }


    void CreateExplorePointUIs(int chapterId)
    {
        ClearExplorePointUIs();

        List<tanxianinit> taskData = ObjectSelf.GetInstance().GetExploreTaskDataByChapterId(chapterId);
        if (taskData == null)
        {
            LogManager.LogError("不存在的章节任务数据，章节id=" + chapterId);
            return;
        }

        foreach (tanxianinit init in taskData)
        {
            ExploreTaskPointUI pointUI = CreateExplorePointUI();

            if (pointUI != null)
            {
                ExplorequestTemplate exploreT = DataTemplate.GetInstance().GetExplorequestTemplateById(init.tanxianid);

                pointUI.SetTemplateData(exploreT);
                mExplorePointUIList.Add(pointUI);
            }
        }
    }

    ExploreTaskPointUI CreateExplorePointUI()
    {
        GameObject go = GameObject.Instantiate(m_ChapterItemObj) as GameObject;

        if (go == null)
        {
            LogManager.LogError("创建章节任务点错误");
            return null;
        }

        ExploreTaskPointUI ui = new ExploreTaskPointUI(go);
        ui.SetParent(m_ChapterListObj.transform);
        ui.SetOnClick(OnExplorePointUIClick);

        return ui;
    }

    void ClearExplorePointUISelect()
    {
        foreach (ExploreTaskPointUI ui in mExplorePointUIList)
        {
            ui.SetSelectState(-1);
        }
    }

    void ClearExplorePointUIs()
    {
        foreach (ExploreTaskPointUI ui in mExplorePointUIList)
        {
            ui.Destroy();
        }

        mExplorePointUIList.Clear();

        GameUtils.DestroyChildsObj(m_ChapterListObj);
    }

    void ClearExploreTeamUIs()
    {
        for (int i = 0, j = m_TeamItemObj.Length; i < j; i++)
        {
            mTeamUIs[i].Destroy();
        }
    }

    void OnExplorePointUIClick(int id)
    {
        for (int i = 0; i < mExplorePointUIList.Count; i++ )
        {
            mExplorePointUIList[i].SetSelectState(id);
        }

        CurExploreId = id;

        AutoCenter(id);
    }

    /// <summary>
    /// idx范围[1,4];
    /// </summary>
    /// <param name="idx"></param>
    void OnTeamUIClick(int idx)
    {
        Dictionary<int, teamtanxian> teamDatas = ObjectSelf.GetInstance().GetExploreTeamData();

        if (teamDatas.ContainsKey(idx))
        {
            int txid = teamDatas[idx].tanxianid;
            int chapterid = DataTemplate.GetInstance().GetExploreChapterIdByExploreId(txid);

            if (chapterid != -1)
            {
                CurChapterId = DataTemplate.GetInstance().GetExploreChapterIdByExploreId(txid);
                CurExploreId = txid;
            }
        }
    }

    /// <summary>
    /// 刷新章节信息;
    /// </summary>
    void UpdateChapterInfo(int chapterId)
    {
        ChapterinfoTemplate chapterT = DataTemplate.GetInstance().GetChapterTemplateByID(chapterId);

        if (chapterT != null)
        {
            m_ChapterTxt.text = string.Format(GameUtils.getString("explore_cotnent15"), chapterId);
            m_ChapterNameTxt.text = GameUtils.getString(chapterT.getChapterName());
        }
    }

    /// <summary>
    /// 刷新小队信息;
    /// </summary>
    void UpdateExploreTeamUIs()
    {
        ClearExploreTeamUIs();

        Dictionary<int, teamtanxian> teamDatas = ObjectSelf.GetInstance().GetExploreTeamData();

        int exploreTeamCount = 0;

        for (int i = 0, j = mTeamUIs.Length; i < j; i++ )
        {
            int key = i + 1;    //小队编号;
            string title = string.Format(GameUtils.getString("explore_cotnent13"), key);
            if(teamDatas.ContainsKey(key))
            {
                teamtanxian data = teamDatas[key];

                //空闲小队;
                if (data.team == null || data.team.Count <= 0)
                {
                    //mTeamUIs[i].SetTitle(title);
                    mTeamUIs[i].SetTitle("");
                    mTeamUIs[i].SetDetail(GameUtils.getString("explore_cotnent14"));
                }
                else
                {
                    tanxianinit tx = ObjectSelf.GetInstance().GetExploreTaskDataById(data.tanxianid);
                    if (tx == null)
                    {
                        Debug.LogError("探险任务数据为NULL exploreid=" + data.tanxianid);
                        return;
                    }

                    EXPLORE_TASK_STATE state = UI_ExploreModule.GetExploreTaskState(tx);
            
                    ExplorequestTemplate temp = DataTemplate.GetInstance().GetExplorequestTemplateById(data.tanxianid);
                    string content = string.Format(GameUtils.getString("explore_cotnent15"), temp.getChapterID());

                    switch (state)
                    {
                        case EXPLORE_TASK_STATE.NotStarted://不可能;
                        case EXPLORE_TASK_STATE.Over:
                            break;
                        case EXPLORE_TASK_STATE.ExploringNotOver:
                            //mTeamUIs[i].SetTitle(title + "  " + GameUtils.getString("explore_cotnent12"));
                            mTeamUIs[i].SetTitle(GameUtils.getString("explore_cotnent12"));
                            mTeamUIs[i].SetDetail(content + "  " + GameUtils.getString(temp.getName()));

                            exploreTeamCount++;
                            break;
                        case EXPLORE_TASK_STATE.ExploringOver:
                            //mTeamUIs[i].SetTitle(title + "  " + GameUtils.getString("explore_cotnent11"));
                            mTeamUIs[i].SetTitle(GameUtils.getString("explore_cotnent11"));
                            mTeamUIs[i].SetDetail(content + "  " + GameUtils.getString(temp.getName()));
                            
                            exploreTeamCount++;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                //Debug.LogError("不存在的探险任务小队" + key);
                //空闲小队;
                //mTeamUIs[i].SetTitle(title);
                mTeamUIs[i].SetTitle("");
                mTeamUIs[i].SetDetail(GameUtils.getString("explore_cotnent14"));
            }
        }

        m_TeamOutCountTxt.text = exploreTeamCount.ToString();
    }

#endregion

#region DetailPanel

    /// <summary>
    /// 任务详细信息界面，右上角关闭按钮;
    /// </summary>
    void OnCloseBtnClick()
    {
        StopAllCoroutines();

        //关闭DetailPanel界面;
        ClearExploreTeamHeroUIs();
        CloseExploreDetailUI();

        //关闭HeroListPanel界面;
        ClearExploreHeroListUIs();
        CloseHeroListUI();
        mCurSortType = EM_SORT_OBJECT_CARD.NONE;

        CurExploreId = -1;

        SetAutoMatchBtnGrey(false);
    }

    void SetAutoMatchBtnGrey(bool isGrey)
    {
        GameUtils.SetBtnSpriteGrayState(m_AutoMatchBtn, isGrey);
        mAutoMatchBtnGrey = isGrey;
    }


    void ShowTaskDetailUI()
    {
        m_DetailPanelObj.SetActive(true);
    }

    void UpdateTaskDetailUI(int exploreid)
    {
        ExplorequestTemplate exploreT = DataTemplate.GetInstance().GetExplorequestTemplateById(exploreid);

        //提示文字处;
        m_TaskNameTxt.text = GameUtils.getString(exploreT.getName());
        //m_RewardContentTxt.text = GameUtils.StringWithGameColor(GameUtils.getString(exploreT.getBonusDes()), GetTxtQuality(exploreT.getQuality()));
        m_RewardContentRichTxt.ShowRichText(GameUtils.getString(exploreT.getBonusDes()).Replace("#", "    "));
        m_RewardDetailTxt.text = GameUtils.getString(exploreT.getDes());
        m_TaskCoditionDesTxt.text = GameUtils.getString(exploreT.getNeedHeroDes());

        //是否正在探险中;
        tanxianinit data = ObjectSelf.GetInstance().GetExploreTaskData(mCurChapterId, exploreid);
        int minutes = UI_ExploreModule.GetTaskMinuteToEnd(data);

        switch (data.tanxiantype)
        {
            case 0:        //未开始;
                RefreshHeroTeamGroup(exploreid, false);

                TEXT_COLOR tc = ObjectSelf.GetInstance().ExplorePoint >= exploreT.getCost() ? TEXT_COLOR.WHITE : TEXT_COLOR.RED;
                m_StartExploreCostTxt.text = GameUtils.StringWithColor(exploreT.getCost().ToString(), tc);

                m_AutoMatchBtn.gameObject.SetActive(true);
                m_StartExploreBtn.gameObject.SetActive(true);
                m_TimeUpBtn.gameObject.SetActive(false);
                m_CallBackBtn.gameObject.SetActive(false);
                m_GetBtn.gameObject.SetActive(false);

                break;
            case 1:        //进行中;
                RefreshHeroTeamGroup(exploreid, true);

                int cost = UI_ExploreModule.GetCostByMinutes(minutes);
                TEXT_COLOR tc1 = ObjectSelf.GetInstance().Gold >= cost ? TEXT_COLOR.WHITE : TEXT_COLOR.RED;
                m_TimeUpBtnCostTxt.text = cost <= 0 ? GameUtils.getString("common_content_free") : GameUtils.StringWithColor(cost.ToString(), tc1);

                m_AutoMatchBtn.gameObject.SetActive(false);
                m_StartExploreBtn.gameObject.SetActive(false);
                
                //判断是否可以领取奖励;
                if (minutes <= 0)
                {
                    //可以领奖;
                    m_TimeUpBtn.gameObject.SetActive(false);
                    m_CallBackBtn.gameObject.SetActive(false);
                    m_GetBtn.gameObject.SetActive(true);
                }
                else
                {
                    //不可以领奖;
                    m_TimeUpBtn.gameObject.SetActive(true);
                    m_CallBackBtn.gameObject.SetActive(true);
                    GameUtils.SetBtnSpriteGrayState(m_TimeUpBtn, ObjectSelf.GetInstance().VipLevel < VIPModule.GetExploreAccelerateVipLv());
                    m_GetBtn.gameObject.SetActive(false);
                }
                break;
            case 2:        //已完成;
                ClearExploreTeamHeroUIs();
                CloseExploreDetailUI();//关闭详细结束界面，且再次点击任务不会打开改界面弹出泡泡提示;

                //m_AutoMatchBtn.gameObject.SetActive(false);
                //m_StartExploreBtn.gameObject.SetActive(false);
                //m_TimeUpBtn.gameObject.SetActive(false);
                //m_CallBackBtn.gameObject.SetActive(false);
                //m_GetBtn.gameObject.SetActive(true);

                break;
            default:
                break;
        }

    }

    

    /// <summary>
    /// 刷新英雄出站列表;
    /// </summary>
    void RefreshHeroTeamGroup(int exploreid, bool isExploring)
    {
        //没在探险读取当前临时探险数据队列;
        if (!isExploring)
        {
            ExplorequestTemplate exploreT = DataTemplate.GetInstance().GetExplorequestTemplateById(exploreid);
        
            int maxCount = DataTemplate.GetInstance().GetExploreNeedHeroCount(exploreT);

            for (int i = 0, j = mTeamHeroUIs.Length; i < j; i++ )
            {
                mTeamHeroUIs[i].SetActive(i < maxCount);

                mTeamHeroUIs[i].HeroGUID = i < mExploreHeroKeys.Count ? mExploreHeroKeys[i] : null;

                mTeamHeroUIs[i].SetIsExploring(isExploring);
            }

            //上阵个数足够;
            if (mExploreHeroKeys.Count >= maxCount)
            {
                GameUtils.SetBtnSpriteGrayState(m_StartExploreBtn, false);
                mStartBtnIsGrey = false;
            }
            else
            {
                GameUtils.SetBtnSpriteGrayState(m_StartExploreBtn, true);
                mStartBtnIsGrey = true;
            }
        }
        else//探险中，读取探险队列信息;
        {
            List<X_GUID> guids = ObjectSelf.GetInstance().GetHeroListByExploreId(exploreid);
            int maxCount = guids == null ? 0 : guids.Count;

            for (int i = 0, j = mTeamHeroUIs.Length; i < j; i++)
            {
                mTeamHeroUIs[i].SetActive(i < maxCount);

                mTeamHeroUIs[i].HeroGUID = i < maxCount ? guids[i] : null;

                mTeamHeroUIs[i].SetIsExploring(isExploring);
            }
        }
    }

    GAME_TXT_COLOR GetTxtQuality(int q)
    {
        switch (q)
        {
            case 1:
                return GAME_TXT_COLOR.WHITE;
            case 2:
                return GAME_TXT_COLOR.GREEN;

            case 3:
                return GAME_TXT_COLOR.BLUE;

            case 4:
                return GAME_TXT_COLOR.PURPLE;

            case 5:
                return GAME_TXT_COLOR.ORANGE;
            default:
                return GAME_TXT_COLOR.WHITE;

        }
    }

    void CloseExploreDetailUI()
    {
        m_DetailPanelObj.SetActive(false);
    }

    void ClearExploreTeamHeroUIs()
    {
        for (int i = 0, j = mTeamHeroUIs.Length; i < j; i++)
        {
            mTeamHeroUIs[i].Clear();
        }

        mExploreHeroKeys.Clear();
    }

    void OnTeamHeroAddBtnClick(X_GUID guid)
    {
        if (guid == null)
        {
            //打开英雄选择界面;
            if (!m_HeroListPanel.activeSelf)
            {
                //设置默认排序;
                CurSortType = EM_SORT_OBJECT_CARD.QUALITY;
                if (datas == null || datas.Count == 0)
                {
                    m_NullObj.SetActive(true);
                    //m_BottomObj.SetActive(false);

                    SetAutoMatchBtnGrey(true);
                }
                else
                {
                    m_NullObj.SetActive(false);
                }
                ShowHeroListUI();
            }
            else
            {
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("explore_bubble4"), selfTransform);
            }
        }
        else//点击已有英雄;
        {
            //无操作;
        }
    }

    /// <summary>
    /// 自动匹配按钮点击;
    /// </summary>
    void OnAutoMatchBtnClick()
    {
        //缺少符合任务的英雄;
        if (mAutoMatchBtnGrey)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("explore_bubble14"), selfTransform);
            return;
        }

        mExploreHeroKeys.Clear();

        List<ObjectCard> list = UI_ExploreModule.GetCardList(mCurExploreId, EM_SORT_OBJECT_CARD.QUALITY);
        if (list == null || list.Count == 0)
        {
            m_NullObj.SetActive(true);
            ShowHeroListUI();
            return;
        }
        else
        {
            m_NullObj.SetActive(false);
        }

        ExplorequestTemplate exploreT = DataTemplate.GetInstance().GetExplorequestTemplateById(mCurExploreId);

        int maxCount = DataTemplate.GetInstance().GetExploreNeedHeroCount(exploreT);

        int count = Mathf.Min(list.Count, maxCount);

        for (int i = 0; i < count; i++ )
        {
            X_GUID guid = new X_GUID();
            guid.Copy(list[i].GetGuid());
            
            mExploreHeroKeys.Add(guid);
        }

        RefreshHeroTeamGroup(mCurExploreId, false);
        
        //打开herolistUI;
        if (!m_HeroListPanel.activeSelf)
        {
            //设置默认排序;
            CurSortType = EM_SORT_OBJECT_CARD.QUALITY;
            ShowHeroListUI();
        }
        else
        {
            UpdateHeroUIBtnsStates();
        }
        //匹配英雄数量不够后置灰;
        if ((count != maxCount) && (count == list.Count))
        {
            SetAutoMatchBtnGrey(true);
        }
    }

    /// <summary>
    /// 开始探险按钮点击;
    /// </summary>
    void OnStartExploreBtnClick()
    {
        //人数不足;
        if (mStartBtnIsGrey)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("explore_bubble5"), selfTransform);
            return;
        }

        //行动力不足--商店快速购买行动力;
        ExplorequestTemplate tem = DataTemplate.GetInstance().GetExplorequestTemplateById(mCurExploreId);
        if(ObjectSelf.GetInstance().ExplorePoint < tem.getCost())
        {
            UI_RechargeBox box = UI_HomeControler.Inst.AddUI(UI_RechargeBox.UI_ResPath).GetComponent<UI_RechargeBox>();
            box.SetIsNeedDescription(true);
            //获取商城中该物品的价格----读config表的商品id==ep_supplement_goods;
            int shopid = DataTemplate.GetInstance().GetGameConfig().getEp_supplement_goods();
            ShopTemplate shopT = DataTemplate.GetInstance().GetShopTemplateByID(shopid);
            if (!ShopModule.IsShopItemInSaling(shopT))
            {
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("shop_bubble6"), transform);
                return;
            }
            //剩余使用次数
            Shopbuy shop = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(shopid);
            
            int mCost = DataTemplate.GetInstance().GetShopBuyCost(shopT, shop.todaynum);
            
            int buyTimes = shop.todaynum;
            bool isdiscount = ShopModule.IsShopItemInDiscount(shopT);
            GameUtils.StringWithColor(mCost.ToString(), TEXT_COLOR.RED);
            string content = GameUtils.getString("explore_cotnent18");
            box.SetDescription_text(content);
            long resourceCount = 0;
            ObjectSelf.GetInstance().TryGetResourceCountById(shopT.getCostType(), ref resourceCount);
            box.SetConNum(GameUtils.StringWithColor(mCost.ToString(), mCost > resourceCount ? TEXT_COLOR.RED : TEXT_COLOR.WHITE));
            box.SetConsume_Image(GameUtils.GetSpriteByResourceType(EM_RESOURCE_TYPE.Gold));
            box.SetMoneyInfo(shopT.getCostType(), (int)resourceCount);
            box.SetMoneyInfoActive(true);
            box.SetLeftBtn_text(GameUtils.getString("common_button_purchase1"));
            box.SetRightBtn_text(GameUtils.getString("common_button_close"));

            if (mCost > resourceCount)
                box.SetLeftClick(OnMsgBoxYesNoMoneyClick);
            else
            {
                UI_RechargeBox.Data = shopid;
                box.SetLeftClick(OnMsgBoxYesClick);
            }

            return;
            //UI_RechargeBox box1 = UI_HomeControler.Inst.AddUI(UI_RechargeBox.UI_ResPath).GetComponent<UI_RechargeBox>();

            //if (box1 == null)
            //{
            //    LogManager.LogError("提示窗is null");
            //    return;
            //}

            //box1.SetDescription_text(GameUtils.getString("common_diamondenough_content"));
            //box1.SetIsNeedDescription(false);
            //box1.SetLeftBtn_text(GameUtils.getString("shop_content4"));
            //box1.SetLeftClick(() =>
            //{
            //    //InterfaceControler.GetInst().AddMsgBox("打开快速充值界面", parent);
            //    UI_HomeControler.Inst.AddUI(UI_QuikChargeMgr.UI_ResPath);
            //    box1.OnCloes();
            //});

            //box1.SetRightBtn_text(GameUtils.getString("common_button_close"));
        }

        //小队是否满员--最多同时派出4个小队;
        if (UI_ExploreModule.GetExploreTeamsCount() >= UI_ExploreModule.GetMaxTeamCount())
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("explore_bubble6"), selfTransform);
            return;
        }

        CTanxianBegin ctb = new CTanxianBegin();
        ctb.tanxianid = mCurExploreId;

        LinkedList<int> team = new LinkedList<int>();

        for (int i = 0; i < mExploreHeroKeys.Count; i++ )
        {
            team.AddLast((int)mExploreHeroKeys[i].GUID_value);
        }

        ctb.team = team;

        IOControler.GetInstance().SendProtocol(ctb);
    }

    void OnMsgBoxYesNoMoneyClick()
    {
        InterfaceControler.GetInst().ShowGoldNotEnougth(transform);
    }

    /// <summary>
    /// 点击立刻购买;
    /// </summary>
    void OnMsgBoxYesClick()
    {
        int shopId = (int)UI_RechargeBox.Data;

        UI_HomeControler.Inst.ReMoveUI(UI_RechargeBox.UI_ResPath);

        ShopTemplate shopT = DataTemplate.GetInstance().GetShopTemplateByID(shopId);
        if (!ShopModule.IsShopItemInSaling(shopT))
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("shop_bubble6"), transform);
            return;
        }
        int buyTimes = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(shopId).todaynum;
        int max = DataTemplate.GetInstance().GetShopItemDailyBuyTimes(shopT, ObjectSelf.GetInstance().VipLevel);

        int mRemineTimes = max - buyTimes;

        if (mRemineTimes <= 0)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("fight_stagepurchase_tip1"));
            return;
        }

        bool isdiscount = ShopModule.IsShopItemInDiscount(shopT);

        ShopModule.BuyItem(shopId, 1, isdiscount);
    }

    /// <summary>
    /// 召回按钮点击;
    /// </summary>
    void OnCallBackBtnClick()
    {
        UI_ExploreModule.OnCallBackBtnClick(mCurExploreId);
    }

    /// <summary>
    /// 时间加速按钮点击;
    /// </summary>
    void OnTimeUpBtnClick()
    {
        //UI_ExploreModule.SendOtherProtocol(CTanXianOther.END_SPEED, mCurExploreId);
        UI_ExploreModule.OnTimeUpBtnClick(mCurExploreId);
    }

    void OnGetBtnClick()
    {
        UI_ExploreModule.SendOtherProtocol(CTanXianOther.END_GET, mCurExploreId);
    }
#endregion

#region TaskPanel
    /// <summary>
    /// 刷新按钮点击;
    /// </summary>
    void OnRefreshBtnClick()
    {
        if (mRefreshCost > ObjectSelf.GetInstance().Gold)
        {
            InterfaceControler.GetInst().ShowGoldNotEnougth(selfTransform);
            return;
        }

        //发送刷新请求;
        UI_ExploreModule.SendOtherProtocol(CTanXianOther.SREFRESH, mCurChapterId);
    }

    void ClearExploreTaskUIs()
    {
        foreach (ExploreTaskUI ui in mExploreTaskUIList)
        {
            ui.Destroy();
        }
         
        mExploreTaskUIList.Clear();

        GameUtils.DestroyChildsObj(m_TaskListObj);
    }

    /// <summary>
    /// 自动滚动并显示到最上边的东西;
    /// </summary>
    void TaskUIListMoveToTop()
    {
        m_TaskScrollBar.value = 1f;
    }

    void CreateExploreTaskUIs(int chapterId)
    {
        ClearExploreTaskUIs();

        List<tanxianinit> taskData = ObjectSelf.GetInstance().GetExploreTaskDataByChapterId(chapterId);
        if (taskData == null)
        {
            LogManager.LogError("不存在的章节任务数据，章节id=" + chapterId);
            return;
        }

        taskData = UI_ExploreModule.SortTaskList(taskData);

        foreach (tanxianinit init in taskData)
        {
            ExploreTaskUI taskUI = CreateTaskUI();

            if (taskUI != null)
            {

                taskUI.SetData(init);
                mExploreTaskUIList.Add(taskUI);
            }
        }

        TaskUIListMoveToTop();
    }

    ExploreTaskUI CreateTaskUI()
    {
        GameObject go = GameObject.Instantiate(m_TaskItemObj) as GameObject;

        if(go == null)
        {
            LogManager.LogError("创建探险任务UI失败");
            return null;
        }

        ExploreTaskUI ui = new ExploreTaskUI(go);
        ui.SetParent(m_TaskListObj.transform);
        ui.SetOnClick(OnTaskUIClick);

        return ui;
    }

    void ClearTaskUISelect()
    {
        SetTaskUISelect(-1);
    }

    void SetTaskUISelect(int id)
    {
        for (int i = 0; i < mExploreTaskUIList.Count; i++ )
        {
            mExploreTaskUIList[i].SetSelect(id);
        }

    }

    void AutoCenter(int id)
    {
        for (int i = 0; i < mExploreTaskUIList.Count; i++)
        {
            if (mExploreTaskUIList[i].TXData.tanxianid == id)
            {
                UI_CenterScrollRectObj csr = m_TaskScrollRect.GetComponent<UI_CenterScrollRectObj>();

                csr.SetCenterObj(4, mExploreTaskUIList[i].Go.transform);
            }
        }
    }

    void UpdateTaskUI(int id)
    {
        SetTaskUISelect(id);

        //判断任务状态;
        tanxianinit data = ObjectSelf.GetInstance().GetExploreTaskData(mCurChapterId, id);
        EXPLORE_TASK_STATE state = UI_ExploreModule.GetExploreTaskState(data);
        
        switch (state)
        {
            case EXPLORE_TASK_STATE.NotStarted:
            case EXPLORE_TASK_STATE.ExploringNotOver:
            case EXPLORE_TASK_STATE.ExploringOver:
                if (!m_DetailPanelObj.activeSelf)
                {
                    ShowTaskDetailUI();
                }
                break;
            case EXPLORE_TASK_STATE.Over:
                if (m_DetailPanelObj.activeSelf)
                {
                    CloseExploreDetailUI();
                }
                //任务已完成提示;
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("explore_bubble1"), selfTransform);
                break;
            default:
                break;
        }
    }

    void OnTaskUIClick(int id)
    {
        CurExploreId = id;

        //UpdateTaskUI(id, false);
    }
#endregion

#region HeroListPanel

    private EM_SORT_OBJECT_CARD mCurSortType = EM_SORT_OBJECT_CARD.NONE;

    EM_SORT_OBJECT_CARD CurSortType
    {
        get
        {
            return mCurSortType;
        }
        set
        {
            if (mCurSortType == value)
            {
                return;
            }

            OnSortTypeChange(mCurSortType, value);
            mCurSortType = value;
        }
    }


    void OnSortTypeChange(EM_SORT_OBJECT_CARD stOld, EM_SORT_OBJECT_CARD stNew)
    {
        //执行清空操作;
        ClearExploreHeroListUIs();

        //获取英雄ObjectCard列表;
        datas = UI_ExploreModule.GetCardList(mCurExploreId, stNew);

        //switch (stNew)
        //{
        //    case EM_SORT_OBJECT_CARD.QUALITY:
        //        GameUtils.SortHeroWithQuailty(ref datas);
        //        break;
        //    case EM_SORT_OBJECT_CARD.LEVEL:
        //        GameUtils.SortHeroWithLevel(ref datas);
        //        break;
        //}

        if (datas == null || datas.Count == 0)
        {
            m_NullObj.SetActive(true);
            //m_BottomObj.SetActive(false);

            SetAutoMatchBtnGrey(true);
        }
        else
        {
            m_NullObj.SetActive(false);
            m_BottomObj.SetActive(true);
            //创建herolistUIs;
            CreateHeroListUIs(datas);

            //StartCoroutine(CreateHeroList(datas));

            //CreateHeroListUIs(datas);

            ////刷新选择、取消按钮显示状态、置灰状态;
            //UpdateHeroUIBtnsStates();

            //SetAutoMatchBtnGrey(false);
        }

    }

    void OnHeroListChange(GameEvent ge)
    {
        //执行清空操作;
        ClearExploreHeroListUIs();

        //获取英雄ObjectCard列表;
        datas = UI_ExploreModule.GetCardList(mCurExploreId, CurSortType);

        if (datas == null || datas.Count == 0)
        {
            m_NullObj.SetActive(true);
            //m_BottomObj.SetActive(false);

            SetAutoMatchBtnGrey(true);
        }
        else
        {
            m_NullObj.SetActive(false);
            m_BottomObj.SetActive(true);
            //创建herolistUIs;
            CreateHeroListUIs(datas);
        }
    }

    /// <summary>
    /// 刷新所有选择、取消按钮显示状态、置灰状态;
    /// </summary>
    void UpdateHeroUIBtnsStates()
    {
        bool isFull = IsTeamHeroFull;

        for(int i = 0; i < mExploreHeroUIList.Count; i++)
        {
            UpdateHeroUIBtnState(isFull, mExploreHeroUIList[i]);
        }

    }

    void UpdateHeroUIBtnState(bool isFull, ExploreHeroUI heroUI)
    {
        if (mExploreHeroKeys.Contains(heroUI.HeroGUID))
        {
            heroUI.SetShowBtnType(0);
        }
        else
        {
            heroUI.SetShowBtnType(1);
            heroUI.SetSelectBtnGrey(isFull);
        }
    }

    /// <summary>
    /// 按品质排序按钮点击;
    /// </summary>
    void OnSortQualityBtnClick()
    {
        CurSortType = EM_SORT_OBJECT_CARD.QUALITY;

        m_SlideBtnTxt.text = GameUtils.getString("hero_info_sort_quality");
        m_SlideBtn.OnClose();
    }

    /// <summary>
    /// 按等级排序;
    /// </summary>
    void OnSortLvBtnClick()
    {
        CurSortType = EM_SORT_OBJECT_CARD.LEVEL;

        m_SlideBtnTxt.text = GameUtils.getString("hero_info_sort_level");
        m_SlideBtn.OnClose();
    }

    void ShowHeroListUI()
    {
        m_HeroListPanel.SetActive(true);
    }

    void CloseHeroListUI()
    {
        m_HeroListPanel.SetActive(false);

        m_SlideBtn.OnClose();
    }

    void ClearExploreHeroListUIs()
    {
        StopCoroutine("CreateHeroList");

        for (int i = 0; i < mExploreHeroUIList.Count; i++ )
        {
            mExploreHeroUIList[i].Destroy();
        }
        mExploreHeroUIList.Clear();

        GameUtils.DestroyChildsObj(m_HeroListObj, true);
    }

    /// <summary>
    /// 创建所有英雄列表;
    /// </summary>
    void CreateHeroListUIs(List<ObjectCard> datas)
    {
        m_LoopLayout.Clear();
        m_LoopLayout.cellCount = datas.Count;
        m_LoopLayout.updateCellEvent += UpdateItem;
        m_LoopLayout.Reload();

        //bool isFull = IsTeamHeroFull;

        //foreach (ObjectCard item in datas)
        //{
        //    if (item == null)
        //    {
        //        continue;
        //    }

        //    ExploreHeroUI ui = CreateHeroItemUI(item);
        //    if (ui != null)
        //    {
        //        mExploreHeroUIList.Add(ui);
        //        ui.HeroGUID = item.GetGuid();
        //    }
        //}
    }

     
    IEnumerator CreateHeroList(List<ObjectCard> datas)
    {
        //       // TOP =-70 238  550   12  -70
        //bool isFull = IsTeamHeroFull;
        //foreach (ObjectCard item in datas)
        //{
        //    if (item == null)
        //    {
        //        continue;
        //    }

        //    ExploreHeroUI ui = CreateHeroItemUI(item);
        //    if (ui != null)
        //    {
        //        mExploreHeroUIList.Add(ui);
        //        ui.HeroGUID = item.GetGuid();

        //        UpdateHeroUIBtnState(isFull, ui);
        //    }

        //    yield return new WaitForEndOfFrame();
        //}
    
        yield return null;
    }
    void UpdateItem(int index, RectTransform cell)
    {
        ExploreHeroUI uib = cell.transform.GetComponent<ExploreHeroUI>();
        if (uib == null)
        {
            uib = cell.gameObject.AddComponent<ExploreHeroUI>();
        }
         uib.index = index;
         bool isFull = IsTeamHeroFull;
         for (int i = 0; i < datas.Count; i++)
         {
             if (index == i)   
             {
                 uib.FillData(OnSelectBtnClick, OnCancelBtnClick);
                 uib.HeroGUID = datas[i].GetGuid();
                 mExploreHeroUIList.Add(uib);
                 UpdateHeroUIBtnState(isFull, uib);
                 uib.SetIsExploring(false);
               
             }
         }
    }
   
    ExploreHeroUI CreateHeroItemUI(ObjectCard oc)
    {
        if (m_HeroItemObj == null) return null;
        GameObject go = GameObject.Instantiate(m_HeroItemObj) as GameObject;
        if(go == null)
        {
            LogManager.LogError("创建英雄卡牌失败");
            return null;
        }

        ExploreHeroUI ui = new ExploreHeroUI(go);
        ui.SetParent(m_HeroListObj.transform);
        ui.SetSelectClick(OnSelectBtnClick);  
        ui.SetCancelClick(OnCancelBtnClick);
        ui.SetIsExploring(false);

        return ui;
    }

    void OnSelectBtnClick(X_GUID guid)
    {
        //添加选中的英雄到出站队列里;
        if (mExploreHeroKeys.Contains(guid))
        {
            return;
        }

        if (IsTeamHeroFull)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("explore_bubble13"), selfTransform);
            return;
        }

        mExploreHeroKeys.Add(guid);

        //通知DetailPanel更新英雄列表;
        RefreshHeroTeamGroup(mCurExploreId, false);

        UpdateHeroUIBtnsStates();
    }

    void OnCancelBtnClick(X_GUID guid)
    {
        //添加选中的英雄到出站队列里;
        if (!mExploreHeroKeys.Contains(guid))
        {
            return;
        }
        mExploreHeroKeys.Remove(guid);

        //通知DetailPanel更新英雄列表;
        RefreshHeroTeamGroup(mCurExploreId, false);

        UpdateHeroUIBtnsStates();
    }

    /// <summary>
    /// 获得英雄左侧按钮点击;
    /// </summary>
    void OnGetHeroLeftBtnClick()
    {
        UI_HomeControler.Inst.AddUI(UI_Recruit.UI_ResPath);
        //UI_HomeControler.Inst.ReMoveUI(UI_ResPath);
    }

    /// <summary>
    /// 获得英雄左侧按钮点击;
    /// </summary>
    void OnGetHeroRightBtnClick()
    {
        //UI_HomeControler.Inst.AddUI(UI_SelectFightArea.UI_ResPath);
        UI_HomeControler.Inst.AddUI(UI_SelectLevelMgrNew.UI_ResPath);
        UI_HomeControler.Inst.ReMoveUI(UI_ResPath);
    }
#endregion


    //生成功能提示控制器
    IFunctionTipsController CreateFunctionTipsController()
    {
        GameObject[] _tipsArray = new GameObject[4];

        var _manager = FunctionTipsManager.GetInstance();
        _tipsArray[0] = selfTransform.FindChild("ScenePanel/TeamPanel/TeamList/Team0/TipsImage").gameObject;
        _tipsArray[1] = selfTransform.FindChild("ScenePanel/TeamPanel/TeamList/Team1/TipsImage").gameObject;
        _tipsArray[2] = selfTransform.FindChild("ScenePanel/TeamPanel/TeamList/Team2/TipsImage").gameObject;
        _tipsArray[3] = selfTransform.FindChild("ScenePanel/TeamPanel/TeamList/Team3/TipsImage").gameObject;

        FunctionTipsControllerBoolArrayType _controller = new FunctionTipsControllerBoolArrayType(_tipsArray, _manager.CheckEveryExploreTeamAward);

        return _controller;
    }
}
