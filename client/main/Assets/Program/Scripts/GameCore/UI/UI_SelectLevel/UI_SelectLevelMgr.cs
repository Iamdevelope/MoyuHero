using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using GNET;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using DG.Tweening;
using DreamFaction.LogSystem;
using DreamFaction.GameEventSystem;
using DreamFaction.UI;

public class UI_SelectLevelMgr : UI_SelectLevelBase
{
    public class SelectLevelBox
    {
        public enum State
        {
            NoOpen,     //不可开启;
            CanOpen,    //可以开启;
            Opened,     //已经开启;
        }

        protected GameObject mGo = null;

        protected Image boxImg = null;
        protected GameObject effObj = null;       //宝箱可以开启特效;
        protected Text starNum = null;

        public SelectLevelBox(Transform trans)
        {
            mGo = trans.gameObject;

            boxImg = trans.FindChild("boxImg").GetComponent<Image>();
            effObj = trans.FindChild("selectEff").gameObject;
            starNum = trans.FindChild("panel/curTxt").GetComponent<Text>();
        }

        public void SetStarNum(int num)
        {

        }

        public void SetBoxImg(Sprite spr)
        {
            boxImg.sprite = spr;
            boxImg.SetNativeSize();
        }

        public void SetEffActive(bool isActive)
        {
            effObj.SetActive(isActive);
        }
    }

    public static readonly string UI_ResPath = "UI_Level/UI_SelectLevel_2_1";
    public static int InitLevelId = -1;          //当前展示的关卡ID;
    public static int InitChapterId = -1;        //当前展示的章节ID;
    public static bool NeedSpecialStage = false; //打开特殊关卡;

    private static UI_SelectLevelMgr mInst = null;

    public Sprite[] BoxSprite = null;

    protected Image[] m_HardImgs = null;
    protected Text[] m_HardTxts1 = null;
    protected Text[] m_HardTxts2 = null;
    protected Image m_BoxImg = null;
    protected GameObject m_StageItemObj = null;
    protected GameObject m_StageListObj = null;
    protected GameObject GoNextEffect = null;
    protected UI_StageInfo m_StageInfo = null;
    protected Transform mBgTrans = null;
    protected UI_StageMapScroll m_MapScroll = null;
    protected CanvasGroup mMenuUI = null;
    protected GameObject m_CountDownObject = null;

    protected List<SelectLevelBox> mBoxs = new List<SelectLevelBox>();
    protected Scrollbar mScrollBar = null;

    private int mCurChapterId = -1;     //当前章节ID;
    private int mCurLevelId = -1;       //当前选择的关卡ID;
    private EM_STAGE_DIFFICULTTYPE mDifficutType = EM_STAGE_DIFFICULTTYPE.NORMAL;      //当前选择的关卡难度ID;

    private int mDesChapterID = -1;     //拖动背景目标章节id;

    private float mTime = 0f;
    private int m_SpecialStageTime;

    private List<UI_LevelItem> mLevelItems = null; //当前展示所有关卡
    private List<StageTemplate> stageDatas;

    private Vector3 mShakeStart = new Vector3(0f, 0f, 0f);
    private Vector3 mShakeEnd = new Vector3(0f, 0f, 30f);
    private Vector3 mShakeStrenth = new Vector3(0f, 0f, 10f);
    private float mShakeDelay = 0f;
    private float mShakeRate = 2f;

    public static UI_SelectLevelMgr Inst
    {
        get
        {
            return mInst;
        }
    }

    int CurChapterID
    {
        get
        {
            return mCurChapterId;
        }
        set
        {
            if (value == mCurChapterId)
            {
            }
            else
            {
                mCurChapterId = value;
                
                OnChapterChanged(value);
            }
        }
    }

    int CurLevelID
    {
        get
        {
            return mCurLevelId;
        }
        set
        {
            if (value == mCurLevelId)
            {
            }
            else
            {
                mCurLevelId = value;

                OnLevelChanged(value);
            }
        }
    }

    EM_STAGE_DIFFICULTTYPE DifficultType
    {
        get
        {
            return mDifficutType;
        }
        set
        {
            mDifficutType = value;

            OnDifficultChanged(value);
        }
    }

    public Dictionary<int, BattleStage> TotalCharpter
    {
        get
        {
            return ObjectSelf.GetInstance().BattleStageData.GetBattleDatas();
        }
    }

    public override void InitUIData()
    {
        base.InitUIData();

        mInst = this;

        m_HardImgs = new Image[3];
        m_HardImgs[0] = selfTransform.FindChild("UI_Menu/Menu/normal/Image").GetComponent<Image>();
        m_HardImgs[1] = selfTransform.FindChild("UI_Menu/Menu/hard/Image").GetComponent<Image>();
        m_HardImgs[2] = selfTransform.FindChild("UI_Menu/Menu/hardest/Image").GetComponent<Image>();

        m_HardTxts1 = new Text[3];
        m_HardTxts1[0] = selfTransform.FindChild("UI_Menu/Menu/normal/no/Label").GetComponent<Text>();
        m_HardTxts1[1] = selfTransform.FindChild("UI_Menu/Menu/hard/no/Label").GetComponent<Text>();
        m_HardTxts1[2] = selfTransform.FindChild("UI_Menu/Menu/hardest/no/Label").GetComponent<Text>();
        m_HardTxts2 = new Text[3];
        m_HardTxts2[0] = selfTransform.FindChild("UI_Menu/Menu/normal/Image/Label").GetComponent<Text>();
        m_HardTxts2[1] = selfTransform.FindChild("UI_Menu/Menu/hard/Image/Label").GetComponent<Text>();
        m_HardTxts2[2] = selfTransform.FindChild("UI_Menu/Menu/hardest/Image/Label").GetComponent<Text>();

        m_BoxImg = m_Box.GetComponent<Image>();
        m_StageInfo = selfTransform.FindChild("UI_Menu/ButtomLayer/Panel").GetComponent<UI_StageInfo>();
        //m_StageItemObj = UIResourceMgr.LoadPrefab("UI/Prefabs/UI_Home/UI_StageItem") as GameObject;
        m_MapScroll = selfTransform.FindChild("mapscroll").GetComponent<UI_StageMapScroll>();
        mBgTrans = transform.FindChild("mapscroll/content");
        m_StageItemObj = selfTransform.FindChild("Items/UI_StageItem").gameObject;
        m_StageListObj = selfTransform.FindChild("UI_Menu/stagelist").gameObject;
        GoNextEffect = selfTransform.FindChild("UI_Menu/StartFightButton01/StartFightStar01").gameObject;
        mMenuUI = selfTransform.FindChild("UI_Menu").GetComponent<CanvasGroup>();
        m_CountDownObject = selfTransform.FindChild("CountDownImage").gameObject;

        mLevelItems = new List<UI_LevelItem>();
        
        for (int i = 0; i < 3; i++ )
        {
            SelectLevelBox box = new SelectLevelBox(selfTransform.FindChild("BottomBar/AllBox/Box" + (i + 1)));
            mBoxs.Add(box);
        }

        mScrollBar = selfTransform.FindChild("BottomBar/Scrollbar").GetComponent<Scrollbar>();

        m_MapScroll.beginDelegate = onBeginMoveCall;
        m_MapScroll.endDelegate = onEndMoveCall;

        GameEventDispatcher.Inst.addEventListener(GameEventID.F_ShowBox, ShowBox);
        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_StageDataRefresh, OnStageDataRefreshed);

        //captionPath = "ZouMaDeng";

        InitStr();
    }

    void InitStr()
    {
        //m_BackText.text = GameUtils.getString("common_button_return");          //返回;
        //m_ConsumText.text = GameUtils.getString("fight_stageselect_content2");  //消耗;
        //m_RewardTxt.text = GameUtils.getString("fight_stageselect_content1");  //奖励;
        //m_Label.text = GameUtils.getString("fight_stageselect_difficulty1");    //普通;
        //m_Label_1.text = GameUtils.getString("fight_stageselect_difficulty2");  //困难;
        //m_Label_2.text = GameUtils.getString("fight_stageselect_difficulty3");  //噩梦;


        //m_HardTxts1[0].text = GameUtils.getString("fight_stageselect_difficulty1");
        //m_HardTxts1[1].text = GameUtils.getString("fight_stageselect_difficulty2");
        //m_HardTxts1[2].text = GameUtils.getString("fight_stageselect_difficulty3");
        
        //m_HardTxts2[0].text = GameUtils.getString("fight_stageselect_difficulty1");
        //m_HardTxts2[1].text = GameUtils.getString("fight_stageselect_difficulty2");
        //m_HardTxts2[2].text = GameUtils.getString("fight_stageselect_difficulty3");
    }

    public override void InitUIView()
    {
        base.InitUIView();

        ObjectSelf.GetInstance().SetIsPrompt(false);

        //ChapterinfoTemplate ct = DataTemplate.GetInstance().GetChapterTemplateByStageID(1310381000);
        /////
        /////当前所打关卡没有解锁新关卡时，从战斗结算界面回到关卡选择界面时，选中的关卡仍为之前所打的那关，
        /////当前关卡打完后有新关卡解锁，返回关卡选择界面就跳到下一关（困难难度一样如此），若同时解锁了两个
        /////关卡（主线和支线），返回关卡选择界面选中主线关
        /////
        if (NeedSpecialStage)
        {
            InitLevelId = ObjectSelf.GetInstance().BattleStageData.GetSpecialStageData().m_StageID;
        }
        else
        {
            List<int> newStages = ObjectSelf.GetInstance().BattleStageData.GetNewStageList();

            if (newStages != null && newStages.Count > 0)
            {
                DisplayNormal();
            }
            else
            {
                if (ObjectSelf.GetInstance().CurStageID > 0 && StageModule.IsStageLevelById(ObjectSelf.GetInstance().CurStageID))
                {
                    InitLevelId = ObjectSelf.GetInstance().CurStageID;
                }
                else
                {
                    DisplayNormal();
                }
            }
        }

        if (InitLevelId <= 0)
        {
            if (InitChapterId <= 0)
            {
                DisplayNormal();
            }
            else
            {
                ChapterinfoTemplate chapterT = StageModule.GetChapterinfoTemplateById(InitChapterId);
                if (chapterT == null)
                {
                    DisplayNormal();
                }
                else
                {
                    mDifficutType = EM_STAGE_DIFFICULTTYPE.NORMAL;
                    mCurChapterId = InitChapterId;
                    mCurLevelId = StageModule.GetLastStageIdInTheChapter(chapterT,   mDifficutType);
                }
            }
        }
        else
        {
            StageTemplate stageT = StageModule.GetStageTemplateById(InitLevelId);
            if (stageT == null)
            {
                DisplayNormal();
            }
            else
            {
                mDifficutType = StageModule.GetStageDifficultType(stageT);
                mCurLevelId = InitLevelId;
                int chapterId = DataTemplate.GetInstance().GetChapterIdByStageT(stageT);
                if (chapterId == -1)
                {
                    Debug.LogError("关卡stageid找不到对应的章节id，stageid=" + InitLevelId);
                }
                else
                {
                    mCurChapterId = chapterId;
                }
            }
        }
        OnNewMapOpenShow();
        Sprite inst = UIResourceMgr.LoadSprite(common.defaultPath + "Ui_guanqiabeijing");
        Dictionary<int, BattleStage>.KeyCollection keycoll = TotalCharpter.Keys;
        foreach (int chapterid in keycoll)
        {
            if (chapterid != 1001)
            {
                ChapterinfoTemplate info = StageModule.GetChapterinfoTemplateById(chapterid);
                if (info != null)
                {
                    //var info = (ChapterinfoTemplate)DataTemplate.GetInstance().m_ChapterTable.getTableData(chapterid);
                    //var info = (ChapterinfoTemplate)table[chapterid];
                    Sprite pic = UIResourceMgr.LoadSprite(common.defaultPath + info.getBackgroundPicture());
                    GameObject item = new GameObject("background");
                    if (pic != null)
                    {
                        item.AddComponent<Image>().sprite = Instantiate(pic, Vector3.zero, Quaternion.identity) as Sprite;
                    }
                    else
                    {
                        item.AddComponent<Image>().sprite = Instantiate(inst, Vector3.zero, Quaternion.identity) as Sprite;
                    }
                    item.transform.SetParent(mBgTrans, false);
                }
            }

        }

        Init();

        // 新手引导 100302
        if(GuideManager.GetInstance().isGuideUser && GuideManager.GetInstance().IsContentGuideID(100302) == false)
        {
            GuideManager.GetInstance().ShowGuideWithIndex(100302);
        }
    }

    void Init()
    {
        ChapterinfoTemplate chapterT = StageModule.GetChapterinfoTemplateById(mCurChapterId);
        if (chapterT == null)
        {
            Debug.LogError("章节数据错误id=" + mCurChapterId);
            return;
        }

        UpdateHardBtnImgs(mDifficutType);

        //---------------------关卡难度-----------------------
        //m_normal.isOn = true;
        UpdateDifficults(chapterT);

        //---------------------章节--------------------------
        UpdateChapter(chapterT);

        //--------------------- 关卡详细描述-------------------
        UpdateLevelInfo();

        //---------------------所有关卡-----------------------
        UpdateLevels(chapterT);

        m_MapScroll.setIdx(mCurChapterId - 1);

        //特殊关卡计时;
        //if (ObjectSelf.GetInstance().BattleStageData.m_IsOpenSpecialStage)
        //{
        //    m_SpecialStageTime = ObjectSelf.GetInstance().BattleStageData.m_SpecialStage.m_Time;
        //    m_CountDownObject.SetActive(true);

        //    //m_CountDownText.text = string.Format("剩余时间：{0}:{1}", m_SpecialStageTime / 60, m_SpecialStageTime % 60);
        //}
        //else
        //{
        //    m_CountDownObject.SetActive(false);
        //}

        //// 新手引导相关--- 开启支线关卡
        if (ObjectSelf.GetInstance().m_isOpenZhiXian)
        {
            for (int i = 0; i < stageDatas.Count; i++)
            {
                if (StageModule.GetStageType( stageDatas[i]) == EM_STAGE_TYPE.SIDE_QUEST)
                {
                    OnLevelItemClick(stageDatas[i].m_stageid);
                }
            }
            
        }

        //新手引导 关卡选择 强制点击返回第一章
        if (ObjectSelf.GetInstance().m_isOpenPerfectReward)
        {
            OnClickbackpoint();
        }


    }

    void DisplayNormal()
    {
        mDifficutType = EM_STAGE_DIFFICULTTYPE.NORMAL;
        mCurLevelId = StageModule.GetPlayerLastLevelID(mDifficutType);
        if (mCurLevelId == -1)
        {
            mCurLevelId = 1310101000;
        }
        //最后一个章节;
        mCurChapterId = StageModule.GetPlayerLastChapterID();
        if (mCurChapterId <= 0)
        {
            mCurChapterId = 1;
        }

        ObjectSelf.GetInstance().CurStageID = mCurLevelId;

        InitLevelId = mCurLevelId;
        InitChapterId = mCurChapterId;
    }

    public override void UpdateUIData()
    {
        base.UpdateUIData();

        mTime += Time.deltaTime;

        if (mTime >= 1f)
        {
            mTime = 0f;
            UpdatePerSec();
        }
    }

    public override void UpdateUIView()
    {
        base.UpdateUIView();

        //if (ObjectSelf.GetInstance().BattleStageData.m_IsOpenSpecialStage)
        //{
        //    if (m_SpecialStageTime > ObjectSelf.GetInstance().BattleStageData.m_SpecialStage.m_Time)
        //    {
        //        m_SpecialStageTime = ObjectSelf.GetInstance().BattleStageData.m_SpecialStage.m_Time;
        //        m_CountDownText.text = string.Format("剩余时间：{0}:{1}", m_SpecialStageTime / 60, m_SpecialStageTime % 60);
        //    }
        //    if (ObjectSelf.GetInstance().BattleStageData.m_SpecialStage.m_Time <= 0)
        //    {
        //        ObjectSelf.GetInstance().BattleStageData.m_IsOpenSpecialStage = false;
        //        m_CountDownObject.SetActive(false);
        //    }

        //}
    }

    public override void OnReadyForClose()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.F_ShowBox, ShowBox);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_StageDataRefresh, OnStageDataRefreshed);

        if (mLevelItems != null)
        {
            for (int i = 0; i < mLevelItems.Count; i++ )
            {
                mLevelItems[i].Destroy();
            }

            mLevelItems.Clear();
            mLevelItems = null;
        }

        InitLevelId = -1;
        InitChapterId = -1;
        NeedSpecialStage = false;

        base.OnReadyForClose();
    }

    public void RefreshStageItem()
    {
        //OnChapterChanged(mCurChapterId);
        ChapterinfoTemplate chapterT = StageModule.GetChapterinfoTemplateById(mCurChapterId);
        if (chapterT == null)
        {
            Debug.LogError("章节数据错误id=" + mCurChapterId);
            return;
        }

        UpdateLevels(chapterT);
    }
    void OnDestroy()
    {
        UIState = UIStateEnum.ReadyForClose;

        base.OnDestroy();
    }

    void UpdatePerSec()
    {
        UpdateLevelItems();
    }

    //更新所有小关卡信息;
    void UpdateLevelItems()
    {
        if (mLevelItems == null || mLevelItems.Count == 0)
        {
            return;
        }

        for (int i = 0; i < mLevelItems.Count; i++ )
        {
            mLevelItems[i].UpdatePerSec();
        }
    }

    void OnChapterChanged(int curChapterId)
    {
        ChapterinfoTemplate chapterT = StageModule.GetChapterinfoTemplateById(curChapterId);
        if (chapterT == null)
        {
            Debug.LogError("章节数据错误id=" + curChapterId);
            return;
        }

        ObjectSelf.GetInstance().SetCurChapterID(mCurChapterId);

        //---------------关卡难度---------
        mDifficutType = EM_STAGE_DIFFICULTTYPE.NORMAL;
        UpdateHardBtnImgs(mDifficutType);
        UpdateDifficults(chapterT);

        //---------------章节------------
        UpdateChapter(chapterT);

        //---------------关卡------------
        //选中当前章节最后一个可以挑战的关卡;
        mCurLevelId = StageModule.GetLastStageIdInTheChapter(chapterT, mDifficutType);

        //----------- 关卡详细描述---------
        UpdateLevelInfo();

        //--------------所有关卡----------
        UpdateLevels(chapterT);
    }

    void OnLevelChanged(int levelId)
    {
        ChapterinfoTemplate chapterT = StageModule.GetChapterinfoTemplateById(mCurChapterId);
        if (chapterT == null)
        {
            Debug.LogError("章节数据错误id=" + mCurChapterId);
            return;
        }

        ObjectSelf.GetInstance().CurStageID = mCurLevelId;
        ObjectSelf.GetInstance().SetCurCampaignID(mCurLevelId);

        //--------------------- 关卡详细描述-------------------
        UpdateLevelInfo();

        ////---------------------所有关卡-----------------------
        //UpdateLevels(chapterT);
        UpdateSelectState();
    }

    void OnDifficultChanged(EM_STAGE_DIFFICULTTYPE difficultType)
    {
        ChapterinfoTemplate chapterT = StageModule.GetChapterinfoTemplateById(mCurChapterId);
        if (chapterT == null)
        {
            Debug.LogError("章节数据错误id=" + mCurChapterId);
            return;
        }

        ObjectSelf.GetInstance().CurChapterLevel = (int)difficultType;

        UpdateHardBtnImgs(difficultType);

        //---------------关卡------------
        //选中当前章节最后一个可以挑战的关卡;
        mCurLevelId = StageModule.GetLastStageIdInTheChapter(chapterT, difficultType);

        //---------------章节------------
        UpdateChapter(chapterT);

        //-------------关卡详细描述-------
        UpdateLevelInfo();

        //--------------所有关卡---------
        UpdateLevels(chapterT);
    }

    //刷新所有的levelitem;
    void UpdateLevels(ChapterinfoTemplate chapterT)
    {
        //获取当前章节所有的关卡列表;
        stageDatas = new List<StageTemplate>();
        //章节的所有普通关卡--chapterInfo表中所有的关卡表;
        //主线;
        switch (DifficultType)
        {
            case EM_STAGE_DIFFICULTTYPE.NONE:
                break;
            case EM_STAGE_DIFFICULTTYPE.NORMAL:
                List<StageTemplate> datas1 = StageModule.GetStageDatas(chapterT, EM_STAGE_TYPE.MAIN_QUEST1);
                stageDatas = StageModule.AddList(stageDatas, datas1);
                //支线;
                List<StageTemplate> datas4 = StageModule.GetStageDatas(chapterT, EM_STAGE_TYPE.SIDE_QUEST);
                stageDatas = StageModule.AddList(stageDatas, datas4);
                break;
            case EM_STAGE_DIFFICULTTYPE.HARD:
                List<StageTemplate> datas2 = StageModule.GetStageDatas(chapterT, EM_STAGE_TYPE.MAIN_QUEST2);
                stageDatas = StageModule.AddList(stageDatas, datas2);
                break;
            case EM_STAGE_DIFFICULTTYPE.HARDEST:
                List<StageTemplate> datas3 = StageModule.GetStageDatas(chapterT, EM_STAGE_TYPE.MAIN_QUEST3);
                stageDatas = StageModule.AddList(stageDatas, datas3);
                break;
            default:
                break;
        }
        //章节的特殊关卡;
        SpecialStage ss = ObjectSelf.GetInstance().BattleStageData.GetSpecialStageData();
        if (ss.m_StageID > 0 && (ss.m_BattlePieceNum == mCurChapterId))
        {
            //特殊关卡;
            if (StageModule.IsStageTemplate(ss.m_StageID))
            {
                StageTemplate st = StageModule.GetStageTemplateById(ss.m_StageID);
                stageDatas.Add(st);
            }
            //神秘商店;
            else
            {
                //构造一个神秘商店;
                StageTemplate st = new StageTemplate();
                st.m_stageid = ss.m_BattlePieceNum;  //章节id;
                st.m_mysteriousShop = ss.m_StageID;  //神秘商店表格id;
                st.m_CustomData = true;
                stageDatas.Add(st);
            }
        }

        int adder = stageDatas.Count - mLevelItems.Count;
        if (adder > 0)
        {
            CreateLevels(adder);
        }

        for (int i = 0; i < mLevelItems.Count; i++)
        {
            if (i >= stageDatas.Count)
            {
                mLevelItems[i].SetTemplateData(null);
            }
            else
            {
                if (stageDatas[i].m_CustomData != null)
                {
                    mLevelItems[i].SetTemplateData(stageDatas[i], EM_STAGE_STAGETYPE.MYSTERIOUS);
                }
                else
                {
                    mLevelItems[i].SetTemplateData(stageDatas[i]);
                }

                mLevelItems[i].SetActive(true);
            }

            //设置选中状态;
            mLevelItems[i].SetSelectState(mCurLevelId);
        }
    }

    //刷新难度标签;
    void UpdateDifficults(ChapterinfoTemplate chapterT)
    {
        int difficultCount = StageModule.GetDifficultCount(chapterT);
        
        m_normal.gameObject.SetActive(difficultCount >= 1);
        m_hard.gameObject.SetActive(difficultCount >= 2);
        m_hardest.gameObject.SetActive(difficultCount >= 3);
    }

    //刷新某个关卡详细信息;
    void UpdateLevelInfo()
    {
        ObjectSelf.GetInstance().CurStageID = mCurLevelId;

        m_StageInfo.setData(mCurLevelId);
        m_StageInfo.SetGoodsItem(mCurLevelId);
    }

    //刷新宝箱显示;
    void UpdateChapter(ChapterinfoTemplate chapterT)
    {
        //--------------翻页按钮-----------
        UpdatePageBtnState();

        //--------------章节名字-----------
        m_title.text = string.Format(GameUtils.getString("chapter_title"), GameUtils.ConverIntToString(chapterT.getId()));
        m_value.text = GameUtils.getString(chapterT.getChapterName());

        //--------------宝箱--------------
        //UpdateBox(chapterT);
    }

    void UpdateSelectState()
    {
        for (int i = 0; i < mLevelItems.Count; i++)
        {
            //设置选中状态;
            mLevelItems[i].SetSelectState(mCurLevelId);
        }
    }

    void ResetBoxToZero()
    {
        CancelInvoke("Shake");
        m_BoxImg.transform.eulerAngles = Vector3.zero;
    }

    void UpdateBox(ChapterinfoTemplate chapterT)
    {
        ResetBoxToZero();

        //--------------宝箱--------------
        int curStars = 0;
        int totalStars = 0;

        if (StageModule.GetCurTotalStarsCount(chapterT, mDifficutType, out curStars, out totalStars))
        {
            m_curTxt.text = curStars.ToString();
            m_totalTxt.text = "/" + totalStars;

            mScrollBar.value = (float)(curStars) / (float)(totalStars);

            int[] perStars = chapterT.getStarnum();
            for (int i = 0, j = mBoxs.Count; i < j;  i++)
            {
                //TODO::是否领取过---章节id,难度,第几个宝箱;
                bool isGot = StageModule.IsRewardGot(mCurChapterId);
                if (curStars >= perStars[i])
                {
                    if (isGot)
                    {
                        mBoxs[i].SetBoxImg(BoxSprite[1]);
                        mBoxs[i].SetEffActive(false);
                    }
                    else
                    {
                        mBoxs[i].SetBoxImg(BoxSprite[0]);
                        mBoxs[i].SetEffActive(true);
                    }
                }
                else
                {
                    //NoOpen;
                    mBoxs[i].SetBoxImg(BoxSprite[0]);
                    mBoxs[i].SetEffActive(false);
                }
                mBoxs[i].SetStarNum(perStars[i]);
            }

            //if (curStars >= totalStars)
            //{
            //    //是否领取过;
            //    bool isGot = StageModule.IsRewardGot(mCurChapterId);
            //    if (isGot)
            //    {
            //        m_BoxImg.sprite = BoxSprite[1];
            //    }
            //    else
            //    {
            //        //宝箱震动;
            //        InvokeRepeating("Shake", mShakeDelay, mShakeRate);

            //        m_BoxImg.sprite = BoxSprite[0];
            //    }
            //}
            //else
            //{
            //    m_BoxImg.sprite = BoxSprite[0];
            //}
        }
        else
        {
            Debug.LogError("数据错误");
        }
    }

    void Shake()
    {
        //DOTween.Shake(mShakeStart, mShakeEnd, 1f, mShakeStrenth);
        //Sequence mySequence = DOTween.Sequence();
        //mySequence.Append(m_BoxImg.DOFade(1, 0.2f));
        //mySequence.SetUpdate(true);
        
        //DOTween.Shake(() => m_BoxImg.transform.eulerAngles, x => m_BoxImg.transform.eulerAngles = mShakeEnd, mShakeRate, mShakeStrenth);
        m_BoxImg.transform.DOShakeRotation(mShakeRate, mShakeStrenth);
    }

    bool IsFirstChapter(int chapterId)
    {
        return chapterId <= 1;
    }

    bool IsLastChapter(int chapterId)
    {
        //int total = StageModule.GetChapterCount();
        int total = StageModule.GetPlayerLastChapterID();

        return chapterId >= total;
    }

    void CreateLevels(int count)
    {
        for (int i = 0; i < count; i++ )
        {
            mLevelItems.Add(CreateLevel());
        }
    }

    UI_LevelItem CreateLevel()
    {
        GameObject go = GameObject.Instantiate(m_StageItemObj) as GameObject;
        GameObject.Destroy(go.GetComponent<UI_StageItem>());
        UI_LevelItem item = new UI_LevelItem(go);
        item.SetOnClick(OnLevelItemClick);
        item.SetParent(m_StageListObj.transform);

        return item;
    }

    /// <summary>
    /// 新地图开启显示;
    /// </summary>
    public void OnNewMapOpenShow()
    {

        if (ObjectSelf.GetInstance().GetIsNewMap() && ObjectSelf.GetInstance().GetCurChapterID() != 1)
        {
            UI_HomeControler.Inst.AddUI(UI_NewMapOpenTxt.UI_ResPath);
        }
    }

    void OnLevelItemClick(int stageId)
    {
        //判断是否是神秘商店（不在关卡表中就是神秘商店），是的话打开神秘商店界面;
        if (StageModule.IsMysteriousShop(stageId))
        {
            UI_HomeControler.Inst.AddUI(UI_MysteriousShop.Path);

            return;
        }

        //判断当前关卡是否开启;
        if (ObjectSelf.GetInstance().BattleStageData.IsStageOpen(stageId))
        {
            CurLevelID = stageId;
        }
        else
        {
            string difStr = "";
            switch (mDifficutType)
            {
                case EM_STAGE_DIFFICULTTYPE.NONE:
                    break;
                case EM_STAGE_DIFFICULTTYPE.NORMAL:
                    difStr = GameUtils.getString("fight_stageselect_difficulty1");
                    break;
                case EM_STAGE_DIFFICULTTYPE.HARD:
                    difStr = GameUtils.getString("fight_stageselect_difficulty2");
                    break;
                case EM_STAGE_DIFFICULTTYPE.HARDEST:
                    difStr = GameUtils.getString("fight_stageselect_difficulty3");
                    break;
                default:
                    break;
            }

            string preChapterName = "";
            StageTemplate stageT = StageModule.GetStageTemplateById(stageId);
            if (stageT.m_premissionid != -1)
            {
                StageTemplate premStageT = StageModule.GetStageTemplateById(stageT.m_premissionid);
                preChapterName = GameUtils.getString(premStageT.m_stagename);
            }

            InterfaceControler.GetInst().AddMsgBox(string.Format(GameUtils.getString("fight_stageselect_tip3"), difStr, preChapterName));
        }
    }

    public void onBeginMoveCall()
    {
        mMenuUI.DOFade(0, 0.2f);
        //m_StartFightButton01.gameObject.SetActive(false);
        // mMenu.SetActive(false);
        //mEffects.SetActive(false);
        
    }

    public void onEndMoveCall(int id)
    {
        // mEffects.SetActive(true);
        //m_StartFightButton01.gameObject.SetActive(true);
        //mMenu.SetActive(true);
        mDesChapterID = id;
        if (CurChapterID != id)
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.AppendCallback(onChapterChangeCall);
            mySequence.Append(mMenuUI.DOFade(1, 0.2f));
            mySequence.SetUpdate(true);
        }
        else
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(mMenuUI.DOFade(1, 0.2f));
            mySequence.SetUpdate(true);
        }

    }

    void onChapterChangeCall()
    {
        CurChapterID = mDesChapterID;

    }

    void OnStageDataRefreshed(GameEvent ge)
    {
        ChapterinfoTemplate chapterT = StageModule.GetChapterinfoTemplateById(mCurChapterId);
        if (chapterT == null)
        {
            Debug.LogError("章节数据错误id=" + mCurChapterId);
            return;
        }

        UpdateLevels(chapterT);
    }

    public void ShowBox()
    {
        ChapterinfoTemplate chapterT = StageModule.GetChapterinfoTemplateById(mCurChapterId);
        //UpdateBox(chapterT);
    }

    protected override void OnClickBox()
    {
        //UI_FightRewards._instance.UpdateShow();
        UI_HomeControler.Inst.AddUI(UI_FightRewards.UI_ResPath);

        //新手引导 200402
        if (GuideManager.GetInstance() != null && ObjectSelf.GetInstance().m_isOpenPerfectReward)
        {
            ObjectSelf.GetInstance().m_isOpenPerfectReward = false;
            GuideManager.GetInstance().StopGuide();
        }
    }

    //protected override void OnClickWorldMap()
    //{
    //    UI_HomeControler.Inst.AddUI(UI_WordMap.UI_ResPath);
    //    UI_HomeControler.Inst.ReMoveUI(UI_SelectLevelMgr.UI_ResPath);
    //    UI_WorldMapManage._instance.WorldMapShow();
    //}

    /// <summary>
    /// 判断当前章节，某个难度的关卡是否通关;
    /// </summary>
    /// <param name="chapterId"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    bool CheckPrevStageIsPass(int chapterId, EM_STAGE_DIFFICULTTYPE type, out int prevStageId)
    {
        ChapterinfoTemplate chapterT = StageModule.GetChapterinfoTemplateById(chapterId);

        prevStageId = -1;

        if (chapterT != null)
        {
            int stageId = StageModule.GetFirstStageIdInTheChapter(chapterT, type);

            StageTemplate st = StageModule.GetStageTemplateById(stageId);

            prevStageId = st.m_premissionid;

            ///前置关卡为-1时，表示不需要前置关卡，直接开启;
            if (prevStageId == -1)
            {
                return true;
            }

            int star = -1;
            if (ObjectSelf.GetInstance().BattleStageData.IsCopyScenePass(prevStageId, out star))
            {
                if (star > 0)
                {
                    return true;
                }
            }
            
        }

        return false;
    }

    void UpdateHardBtnImgs(EM_STAGE_DIFFICULTTYPE selectDifType)
    {
        bool tmp = false;
        for (int i = 1; i <= 3; i++ )
        {
            tmp = i == (int)selectDifType;
            m_HardImgs[i - 1].gameObject.SetActive(tmp);
            m_HardTxts1[i - 1].gameObject.SetActive(!tmp);
        }
    }

    protected override void OnClicknormal()
    {
        base.OnClicknormal();
        
        if (mDifficutType == EM_STAGE_DIFFICULTTYPE.NORMAL) return;
        
        DifficultType = EM_STAGE_DIFFICULTTYPE.NORMAL;

        //int prevStageId = -1;
        //if (CheckPrevStageIsPass(mCurChapterId, EM_STAGE_DIFFICULTTYPE.NORMAL, out prevStageId))
        //{
        //    DifficultType = EM_STAGE_DIFFICULTTYPE.NORMAL;
        //}
        //else
        //{
        //    ChapterinfoTemplate ct = DataTemplate.GetInstance().GetChapterTemplateByStageID(prevStageId);
        //    if (ct == null)
        //    {
        //        return;
        //    }
        //    InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("fight_stageselect_tip1") + GameUtils.getString("fight_stageselect_difficulty3") + string.Format(GameUtils.getString("fight_stageselect_tip2"), GameUtils.getString(ct.getChapterName())));
        //}
    }



    /// <summary>
    /// 100302
    /// </summary>

    protected override void OnClickhard()
    {
        base.OnClickhard();
        if (mDifficutType == EM_STAGE_DIFFICULTTYPE.HARD) return;
        int prevStageId = -1;
        if (CheckPrevStageIsPass(mCurChapterId, EM_STAGE_DIFFICULTTYPE.HARD, out prevStageId))
        {
            DifficultType = EM_STAGE_DIFFICULTTYPE.HARD;
        }
        else
        {
            ChapterinfoTemplate ct = DataTemplate.GetInstance().GetChapterTemplateByStageID(prevStageId);

            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("fight_stageselect_tip1") + GameUtils.getString("fight_stageselect_difficulty1") + string.Format(GameUtils.getString("fight_stageselect_tip2"), GameUtils.getString(ct.getChapterName())));
        }

    }

    protected override void OnClickhardest()
    {
        base.OnClickhardest();
        if (mDifficutType == EM_STAGE_DIFFICULTTYPE.HARDEST) return;
        int prevStageId = -1;
        if (CheckPrevStageIsPass(mCurChapterId, EM_STAGE_DIFFICULTTYPE.HARDEST, out prevStageId))
        {
            DifficultType = EM_STAGE_DIFFICULTTYPE.HARDEST;
        }
        else
        {
            ChapterinfoTemplate ct = DataTemplate.GetInstance().GetChapterTemplateByStageID(prevStageId);

            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("fight_stageselect_tip1") + GameUtils.getString("fight_stageselect_difficulty2") + string.Format(GameUtils.getString("fight_stageselect_tip2"), GameUtils.getString(ct.getChapterName())));
        }
    }

    //protected override void OnTogglehard(bool isSelected)
    //{
    //    base.OnTogglehard(isSelected);
        
    //    if (isSelected)
    //    {
    //        int prevStageId = -1;
    //        if (CheckPrevStageIsPass(mCurChapterId, EM_STAGE_DIFFICULTTYPE.HARD, out prevStageId))
    //        {
    //            DifficultType = EM_STAGE_DIFFICULTTYPE.HARD;
    //        }
    //        else
    //        {
    //            switch (mDifficutType)
    //            {
    //                case EM_STAGE_DIFFICULTTYPE.NONE:
    //                    break;
    //                case EM_STAGE_DIFFICULTTYPE.NORMAL:
    //                    m_normal.isOn = true;
    //                    break;
    //                case EM_STAGE_DIFFICULTTYPE.HARD:
    //                    m_hard.isOn = true;
    //                    break;
    //                case EM_STAGE_DIFFICULTTYPE.HARDEST:
    //                    m_hardest.isOn = true;
    //                    break;
    //                default:
    //                    break;
    //            }
    //            m_hard.isOn = false;
                
    //            ChapterinfoTemplate ct = DataTemplate.GetInstance().GetChapterTemplateByStageID(prevStageId);

    //            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("fight_stageselect_tip1") + GameUtils.getString("fight_stageselect_difficulty1") + string.Format(GameUtils.getString("fight_stageselect_tip2"), GameUtils.getString(ct.getChapterName())));
    //        }

    //    }
    //}

    //protected override void OnTogglehardest(bool isSelected)
    //{
    //    base.OnTogglehardest(isSelected);
        
    //    if (isSelected)
    //    {
    //        int prevStageId = -1;
    //        if (CheckPrevStageIsPass(mCurChapterId, EM_STAGE_DIFFICULTTYPE.HARDEST, out prevStageId))
    //        {
    //            //m_hard.isOn = true;
    //            DifficultType = EM_STAGE_DIFFICULTTYPE.HARDEST;
    //        }
    //        else
    //        {
    //            switch (mDifficutType)
    //            {
    //                case EM_STAGE_DIFFICULTTYPE.NONE:
    //                    break;
    //                case EM_STAGE_DIFFICULTTYPE.NORMAL:
    //                    m_normal.isOn = true;
    //                    break;
    //                case EM_STAGE_DIFFICULTTYPE.HARD:
    //                    m_hard.isOn = true;
    //                    break;
    //                case EM_STAGE_DIFFICULTTYPE.HARDEST:
    //                    m_hardest.isOn = true;
    //                    break;
    //                default:
    //                    break;
    //            }
    //            m_hardest.isOn = false;

    //            ChapterinfoTemplate ct = DataTemplate.GetInstance().GetChapterTemplateByStageID(prevStageId);

    //            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("fight_stageselect_tip1") + GameUtils.getString("fight_stageselect_difficulty2") + string.Format(GameUtils.getString("fight_stageselect_tip2"), GameUtils.getString(ct.getChapterName())));
    //        }
    //    }
    //}

    //protected override void OnTogglenormal(bool isSelected)
    //{
    //    base.OnTogglenormal(isSelected);
        
    //    if (isSelected)
    //    {
    //        int prevStageId = -1;
    //        if (CheckPrevStageIsPass(mCurChapterId, EM_STAGE_DIFFICULTTYPE.NORMAL, out prevStageId))
    //        {
    //            DifficultType = EM_STAGE_DIFFICULTTYPE.NORMAL;
    //        }
    //        else
    //        {
    //            switch (mDifficutType)
    //            {
    //                case EM_STAGE_DIFFICULTTYPE.NONE:
    //                    break;
    //                case EM_STAGE_DIFFICULTTYPE.NORMAL:
    //                    break;
    //                case EM_STAGE_DIFFICULTTYPE.HARD:
    //                    m_hard.isOn = true;
    //                    break;
    //                case EM_STAGE_DIFFICULTTYPE.HARDEST:
    //                    m_hardest.isOn = true;
    //                    break;
    //                default:
    //                    break;
    //            }
    //            m_normal.isOn = false;

    //            ChapterinfoTemplate ct = DataTemplate.GetInstance().GetChapterTemplateByStageID(prevStageId);

    //            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("fight_stageselect_tip1") + GameUtils.getString("fight_stageselect_difficulty3") + string.Format(GameUtils.getString("fight_stageselect_tip2"), GameUtils.getString(ct.getChapterName())));
    //        }
    //    }
    //}

    //protected override void OnClickStartFightButton01()
    //{
    //    UI_Form.UI_ResPath = "UI_Home/UI_ReadyToFight_2_2";
    //    UI_HomeControler.Inst.AddUI(UI_Form.UI_ResPath);
    //    GoNextEffect.gameObject.SetActive(false);
    //    bool isStageScene = !(ObjectSelf.GetInstance().IsLimitFight
    //        || ObjectSelf.GetInstance().IsInWorldBoss);
    //    ObjectSelf.GetInstance().SetChangeLevel(isStageScene);
    //}

    /// <summary>
    /// 箭头按钮
    /// </summary>
    /// <param name="isFront">是不是向前的</param>
    public void onMoveCall(int moveToId)
    {
        mMenuUI.DOFade(0, 0.2f);
        m_MapScroll.onMoveTo(moveToId - 1);
        // mEffects.SetActive(false);
        //m_StartFightButton01.gameObject.SetActive(false);
        //Sequence mySequence = DOTween.Sequence();
        //mySequence.Append(mMenuUI.DOFade(0, 0.2f));
        //mySequence.AppendCallback(onChapterChangeCall);
        //mySequence.Append(mMenuUI.DOFade(1, 0.2f));
        //mySequence.SetUpdate(true);
        // 测试
        //mMenuUI.DOFade(0, 0.5f).SetUpdate(false);
    }

    protected override void OnClickfrontpoint()
    {
        if (IsLastChapter(mCurChapterId))
        {
            return;
        }

        CurChapterID++;

        onMoveCall(mCurChapterId);
    }

    protected override void OnClickbackpoint()
    {
        if (IsFirstChapter(mCurChapterId))
        {
            return;
        }

        CurChapterID--;

        onMoveCall(mCurChapterId);
    }

    /// <summary>
    /// 刷新翻页按钮状态;
    /// </summary>
    void UpdatePageBtnState()
    {
        GameUtils.SetBtnSpriteGrayState(m_frontpoint, IsLastChapter(mCurChapterId));
        GameUtils.SetBtnSpriteGrayState(m_backpoint, IsFirstChapter(mCurChapterId));

        m_backpoint.gameObject.SetActive(!IsFirstChapter(mCurChapterId));
    }


    protected override void OnClickBackBtn()
    {
 	     base.OnClickBackBtn();
         UI_HomeControler.Inst.ReMoveUI(UI_ResPath);
    }
    
}
