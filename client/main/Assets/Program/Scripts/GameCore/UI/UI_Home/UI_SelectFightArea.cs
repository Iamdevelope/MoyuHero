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
namespace DreamFaction.UI
{
    /// <summary>
    /// 关卡选择界面，继承自BaseUI
    /// </summary>
    public class UI_SelectFightArea : BaseUI
    {
        //public static UI_SelectFightArea Inst = null;
        //public static string UI_ResPath = "UI_Home/UI_SelectFightArea_2_1";

        //public GameObject[] mStages;
        //private Button GoNext_Btn; // 布阵按钮
        //private GameObject GoNextEffect = null;
        ////private GameObject Boss; // boss
        //public int iChapterID;    // 章节id
        //private int iStageID;      // 当前关卡id
        //private UI_StageInfo mStageInfo;    // 关卡信息页
        //private List<int> mStageList = new List<int>();   // 章节关卡id列表
        //private List<UI_StageItem> mStageItemList = new List<UI_StageItem>(); // 关卡UI列表
        //private Button mapBtn;
        //private ObjectSelf obj;
        ////UI
        //private CanvasGroup mMenuUI;
        //private Transform mStageLayout;
        //private Text mStartNumber;  // 星星数量
        //private Button mBox;
        //private Image mBoxIamge;
        //public Object mHadOpenItemSprite;   // 已开启的关卡图标
        //public Object mCurOpenItemSprite;   // 当前开启的关卡图标
        //public Object mHadNotOpenSprite;    // 未开启的关卡图标
        //private Transform imageTrans;   // 大背景
        //private RectTransform nameTextRect; //章节名文本
        //private RectTransform nameRect; // 章节名背景
        //private Text charpterName;      // 章节名
        //private float mNameRectInitWidth;   // 章节背景的初始长度

        //private GameObject mlPoint; // 左箭头
        //private Button mrPoint; // 右箭头

        //protected GameObject mStageItemObj = null;   //每个小关卡节点obj;

        //private int iTotalCharpterNum;  // 开启的章节总数
        ////public Dictionary<int, BattleStage> iTotalCharpter = new Dictionary<int, BattleStage>();
        //public UI_StageMapScroll mScrollMap;
        //public int total;//星星总数
        //public int curstart;//星星数
        //public int mCurlevel = 0;
        //private UI_PlayerInfo playerInfo;
        //private GameObject mRewards;
        //public List<UI_StageLevelButton> mLevelButtons = new List<UI_StageLevelButton>(3);
        //public List<Image> mLevelButtonsImage = new List<Image>();
        //private Transform pos;
        //public GameObject Card3Dmodel;  //3D模型
        //public GameObject mEffects;
        //public GameObject mMenu;
        //Dictionary<int, List<StageTemplate>> mMapStages = new Dictionary<int, List<StageTemplate>>();
        //private Transform MsgBoxGroup;         //消息父节点  
        //BattleStage list = null;


        ////特殊关卡
        //private Text m_SpecialStageText;
        //private GameObject m_CountDownObject;
        //private int m_SpecialStageTime;
        //public static bool NeedSetToSpecialStage = false;

        //public Dictionary<int, BattleStage> TotalCharpter
        //{
        //    get
        //    {
        //        return ObjectSelf.GetInstance().BattleStageData.GetBattleDatas();
        //    }
        //}

        //// ===================== 继承 =======================
        //public override void InitUIData()
        //{
        //    base.InitUIData();
        //    Inst = this;

        //    mStageItemObj = UIResourceMgr.LoadPrefab("UI/Prefabs/UI_Home/UI_StageItem") as GameObject;

        //    m_SpecialStageText = selfTransform.FindChild("CountDownImage/CountDownText").GetComponent<Text>();
        //    m_CountDownObject = selfTransform.FindChild("CountDownImage").gameObject;

        //    playerInfo = selfTransform.FindChild("PlayerInfoItem").GetComponent<UI_PlayerInfo>();
        //    mMenu = selfTransform.FindChild("UI_Menu/Menu").gameObject;
        //    mEffects = selfTransform.FindChild("StartFightParticle01/UIFire").gameObject;
        //    GoNext_Btn = selfTransform.FindChild("UI_Menu/StartFightButton01").GetComponent<Button>();
        //    GoNext_Btn.onClick.AddListener(OnClickGoNextBtn);
        //    GoNextEffect = selfTransform.FindChild("UI_Menu/StartFightButton01/StartFightStar01").gameObject;
        //    //Boss = selfTransform.FindChild("Boss").gameObject;
        //    mMenuUI = selfTransform.FindChild("UI_Menu").GetComponent<CanvasGroup>();
        //    //mMenuUI.alpha = 0;
        //    mStageLayout = mMenuUI.transform.FindChild("stagelist").transform;
        //    selfTransform.FindChild("PlayerInfoItem").GetComponent<UI_PlayerInfo>().mBackEvent = onBackCall;
        //    mStageInfo = selfTransform.FindChild("UI_Menu/ButtomLayer/Panel").GetComponent<UI_StageInfo>();
        //    mStartNumber = selfTransform.FindChild("UI_Menu/Box/value").GetComponent<Text>();
        //    mBox = selfTransform.FindChild("UI_Menu/Box").GetComponent<Button>();
        //    mBox.onClick.AddListener(OnClickBox);
        //    mBoxIamge = selfTransform.FindChild("UI_Menu/Box").GetComponent<Image>();
        //    mapBtn = selfTransform.FindChild("UI_Menu/WorldMap").GetComponent<Button>();
        //    mapBtn.onClick.AddListener(OnclickMapBtn);
        //    imageTrans = transform.FindChild("mapscroll/content");
        //    mScrollMap = transform.FindChild("mapscroll").GetComponent<UI_StageMapScroll>();
        //    mScrollMap.beginDelegate = onBeginMoveCall;
        //    mScrollMap.endDelegate = onEndMoveCall;
        //    // 难度按钮
        //    var normalButton = transform.FindChild("UI_Menu/Menu/normal").GetComponent<UI_StageLevelButton>();
        //    var hardButton = transform.FindChild("UI_Menu/Menu/hard").GetComponent<UI_StageLevelButton>();
        //    var hardestButton = transform.FindChild("UI_Menu/Menu/hardest").GetComponent<UI_StageLevelButton>();
        //    mLevelButtons.Add(normalButton);
        //    mLevelButtons.Add(hardButton);
        //    mLevelButtons.Add(hardestButton);
        //    mLevelButtonsImage.Add(normalButton.transform.GetComponent<Image>());
        //    mLevelButtonsImage.Add(hardButton.transform.GetComponent<Image>());
        //    mLevelButtonsImage.Add(hardestButton.transform.GetComponent<Image>());
        //    // 章节名
        //    nameRect = transform.FindChild("chaptername").GetComponent<RectTransform>();
        //    charpterName = nameRect.FindChild("value").GetComponent<Text>();
        //    nameTextRect = charpterName.GetComponent<RectTransform>();
        //    mNameRectInitWidth = nameRect.sizeDelta.x;
        //    // 箭头
        //    mlPoint = mMenuUI.transform.FindChild("backpoint").gameObject;
        //    mrPoint = mMenuUI.transform.FindChild("frontpoint").GetComponent<Button>();

        //    mHadOpenItemSprite = UIResourceMgr.LoadSprite("UI/Sprites/UI_Guan_09");
        //    mCurOpenItemSprite = UIResourceMgr.LoadSprite("UI/Sprites/UI_Guan_10");
        //    mHadNotOpenSprite = UIResourceMgr.LoadSprite("UI/Sprites/UI_Guan_11");
        //    mRewards = selfTransform.FindChild("Rewards").gameObject;
        //    // 初始化列表
        //    List<StageTemplate> normallevel = new List<StageTemplate>();
        //    List<StageTemplate> hardlevel = new List<StageTemplate>();
        //    List<StageTemplate> hardestlevel = new List<StageTemplate>();
        //    mMapStages.Add(1, normallevel);
        //    mMapStages.Add(2, hardlevel);
        //    mMapStages.Add(3, hardestlevel);
        //    MsgBoxGroup = selfTransform.FindChild("MsgBoxGroup");

        //    pos = GameObject.Find("pos").transform;
        //    //Annie01
        //    GameObject _AssetRes = AssetLoader.Inst.GetAssetRes("Annie01");
        //    if (_AssetRes != null)
        //    {
        //        Card3Dmodel = Instantiate(_AssetRes, pos.position, pos.rotation) as GameObject;
        //        Card3Dmodel.transform.parent = pos;
        //        Card3Dmodel.GetComponent<Animation>().Play("Run1");
        //        Card3Dmodel.GetComponent<Animation>().wrapMode = WrapMode.Loop;
        //    }
        //    else
        //    {
        //        //Debug.Log("Annie01+模型为空");
        //    }
        //    // 根据章节id读取关卡数据100101#100102
        //    //var chapid = ObjectSelf.GetInstance().GetCurChapterID();
        //    //var data = DataTemplate.GetInstance().m_ChapterTable.m_Data;
        //    //if (data.ContainsKey(chapid))
        //    //{
        //    //    var liststage = ((ChapterinfoTemplate)data[chapid]).getStageID();
        //    //    mStageList.Capacity = liststage.Length;
        //    //    foreach (int id in liststage)
        //    //    {
        //    //        mStageList.Add(id);
        //    //    }

        //    //    ObjectSelf.GetInstance().CurStageID = mStageList[0];
        //    //}
        //    //else
        //    //{
        //    //    LogManager.LogError("invaild charpid");
        //    //}
        //    iTotalCharpterNum = ObjectSelf.GetInstance().BattleStageData.GetStageCount();
        //    //iTotalCharpter = ObjectSelf.GetInstance().BattleStageData.m_BattleStageList;
        //    //iTotalCharpterNum = DataTemplate.GetInstance().m_ChapterTable.m_Data.Count;
        //    GameEventDispatcher.Inst.addEventListener(GameEventID.F_ShowBox, ShowBox);
        //    if (iTotalCharpterNum - 1 == ObjectSelf.GetInstance().GetCurChapterID())
        //    {
        //        mrPoint.enabled = false;
        //        GameUtils.SetBtnSpriteGrayState(mrPoint, true);
        //    }
        //    else
        //    {
        //        mrPoint.enabled = true;
        //        GameUtils.SetBtnSpriteGrayState(mrPoint, false);
        //    }
        //    GameEventDispatcher.Inst.addEventListener(GameEventID.G_FightNumSucceed, FightNumShow);


        //}

        //// 指引战斗按钮
        //void InitFightGuide()
        //{
        //    GuideManager.GetInstance().ShowNextGuide();
        //}

        //private void OnDestroy()
        //{
        //    GameEventDispatcher.Inst.removeEventListener(GameEventID.G_FightNumSucceed, FightNumShow);
        //    GameEventDispatcher.Inst.removeEventListener(GameEventID.F_ShowBox, ShowBox);

        //    mScrollMap.beginDelegate = null;
        //    mScrollMap.endDelegate = null;
        //}
        //public void FightNumShow()
        //{
        //    //ChapterinfoTemplate levelInfo = (ChapterinfoTemplate)DataTemplate.GetInstance().m_ChapterTable.getTableData(iChapterID);
        //    ChapterinfoTemplate levelInfo = StageModule.GetChapterinfoTemplateById(iChapterID);
        //    int[] levelID = levelInfo.getStageID();
        //    for (int i = 0; i < levelID.Length; i++)
        //    {
        //        if (levelID[i] == ObjectSelf.GetInstance().CurStageID)
        //        {
        //            //StageTemplate stage = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(levelID[i]);
        //            StageTemplate stage = StageModule.GetStageTemplateById(levelID[i]);
        //            mStageItemList[i].init(stage, i);
        //        }

        //    }
        //}
        //public Transform GetMsgBoxGroup() { return MsgBoxGroup; }
        
        //public override void InitUIView()
        //{
           
        //    OnNewMapOpenShow();
        //    Sprite inst = UIResourceMgr.LoadSprite(common.defaultPath + "UI_SelectFightArea_0");
        //    // 初始化地图
        //    // List<int> key = iTotalCharpter.Keys.ToList();

        //    if (ObjectSelf.GetInstance().BattleStageData.GetStageCount() <= 0)
        //    {
        //        LogManager.LogToFile("关卡数据错误：关卡数据为空");
        //        return;
        //    }

        //    Dictionary<int, BattleStage>.KeyCollection keycoll = TotalCharpter.Keys;
        //    foreach (int chapterid in keycoll)
        //    {
        //        if (chapterid != 1001)
        //        {
        //            ChapterinfoTemplate info = StageModule.GetChapterinfoTemplateById(chapterid);
        //            if (info != null)
        //            {
        //                //var info = (ChapterinfoTemplate)DataTemplate.GetInstance().m_ChapterTable.getTableData(chapterid);
        //                //var info = (ChapterinfoTemplate)table[chapterid];
        //                Sprite pic = UIResourceMgr.LoadSprite(common.defaultPath + info.getBackgroundPicture());
        //                GameObject item = new GameObject("background");
        //                if (pic != null)
        //                {
        //                    item.AddComponent<Image>().overrideSprite = Instantiate(pic, Vector3.zero, Quaternion.identity) as Sprite;
        //                }
        //                else
        //                {
        //                    item.AddComponent<Image>().overrideSprite = Instantiate(inst, Vector3.zero, Quaternion.identity) as Sprite;
        //                }
        //                item.transform.SetParent(imageTrans, false);
        //            }
        //        }

        //    }
        //    if (ObjectSelf.GetInstance().GetCurChapterID() != 1001)
        //    {
        //        //if (isReward(ObjectSelf.GetInstance().CurChapterLevel, iTotalCharpter[ObjectSelf.GetInstance().GetCurChapterID()].m_bRewardGot))
        //        if (StageModule.isReward(ObjectSelf.GetInstance().CurChapterLevel, ObjectSelf.GetInstance().BattleStageData.GetRewardGot(ObjectSelf.GetInstance().GetCurChapterID())))
        //        {
        //            mBoxIamge.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Guan_17");
        //        }
        //        else
        //        {
        //            mBoxIamge.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Guan_16");
        //        }
        //    }

        //    onChapterChangeCall();

        //    mScrollMap.setIdx(iChapterID - 1);

        //    obj = ObjectSelf.GetInstance();

        //    m_CountDownObject.SetActive(obj.BattleStageData.m_IsOpenSpecialStage);
        //    if(obj.BattleStageData.m_IsOpenSpecialStage)
        //    {
        //        CloseAllSpecialStage(obj.BattleStageData.m_SpecialStage.m_StageID);
        //        m_SpecialStageTime = obj.BattleStageData.m_SpecialStage.m_Time;


        //    }
        //    else
        //        CloseAllSpecialStage();


        //    if (ObjectSelf.GetInstance().m_isOpenPerfectReward)
        //    {
        //        onPointCall(false);
        //        ObjectSelf.GetInstance().m_isOpenPerfectReward = false;
        //    }
        //}

        ////参数是不需要关闭的关卡ID
        //private void CloseAllSpecialStage(int exceptStage = -1)
        //{

        //    for (int i = 0; i < mStageItemList.Count; i++) 
        //    {
        //        UI_StageItem stage = mStageItemList[i];
        //        StageTemplate stageInfo = stage.mStageInfo;
        //        if (exceptStage > 0 && stage.iStageID == exceptStage)
        //        {
        //            stage.gameObject.SetActive(true);
        //            if (NeedSetToSpecialStage)
        //            {
        //                NeedSetToSpecialStage = false;
        //                stage.onClick();
        //            }
        //        }
        //        else if (stageInfo.m_stagetype == 5)
        //        {
        //            stage.gameObject.SetActive(false);
        //        }
        //    }
            
        //}
        //private void OpenOneSpecialStage(int stageId) 
        //{
        //    for (int i = 0; i < mStageItemList.Count; i++)
        //    {
        //        UI_StageItem stage = mStageItemList[i];
        //        StageTemplate stageInfo = stage.mStageInfo;
        //        if (stage.iStageID == stageId)
        //            stage.gameObject.SetActive(true);
        //    }
        //}

        //public void ShowBox()
        //{

        //    //if (isReward(ObjectSelf.GetInstance().CurChapterLevel, iTotalCharpter[ObjectSelf.GetInstance().GetCurChapterID()].m_bRewardGot))
        //    if (StageModule.isReward(ObjectSelf.GetInstance().CurChapterLevel, ObjectSelf.GetInstance().BattleStageData.GetRewardGot(ObjectSelf.GetInstance().GetCurChapterID())))
        //    {
        //        mBoxIamge.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Guan_17");
        //    }
        //    else
        //    {
        //        mBoxIamge.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Guan_16");
        //    }


        //}

        ////public bool isReward(int difficulttype, int rewardnum)
        ////{
        ////    int reward = 0;
        ////    if (difficulttype == 1)
        ////    {
        ////        reward = rewardnum % 10;
        ////    }
        ////    else if (difficulttype == 2)
        ////    {
        ////        reward = rewardnum % 100 / 10;
        ////    }
        ////    else if (difficulttype == 3)
        ////    {
        ////        reward = rewardnum / 100;
        ////    }
        ////    return reward != 0;
        ////}
        //public override void OnPlayingExitAnimation()
        //{
        //    GoNext_Btn.gameObject.SetActive(false);

        //}

        //public override void UpdateUIView()
        //{
        //    if (UIState == UIStateEnum.PlayingExitAnimation)
        //    {
        //        //if(Boss.transform.localScale.x < 3)
        //        //{
        //        //    Boss.transform.localScale += new Vector3(0.02f, 0.02f, 0.02f);
        //        //}
        //        //else
        //        //{
        //        //    UIState = UIStateEnum.PlayingExitAnimationOver;
        //        //}
        //    }



        //    if(obj.BattleStageData.m_IsOpenSpecialStage)
        //    {
        //        if (m_SpecialStageTime > obj.BattleStageData.m_SpecialStage.m_Time)
        //        {
        //            m_SpecialStageTime = obj.BattleStageData.m_SpecialStage.m_Time;
        //            m_SpecialStageText.text = string.Format("剩余时间：{0}:{1}", m_SpecialStageTime / 60, m_SpecialStageTime%60);
        //        }
        //        if (obj.BattleStageData.m_SpecialStage.m_Time == 0)
        //        {
        //            obj.BattleStageData.m_IsOpenSpecialStage = false;
        //            m_CountDownObject.SetActive(false);
        //        }

        //    }

        //}

        //public override void OnReadyForClose()
        //{
        //    //StartCoroutine(ReadyForClose());
        //}

        //public void onBackCall()
        //{
        //    Destroy(Card3Dmodel);
        //    Card3Dmodel = null;
        //    UI_HomeControler.Inst.ReMoveUI(UI_SelectFightArea.UI_ResPath);
        //}
        //// ==================== 回调 =====================
        //private void OnClickGoNextBtn()
        //{
        //    GoNextEffect.gameObject.SetActive(true);
        //    StartCoroutine(GoFight());
        //}

        //IEnumerator GoFight()
        //{
        //    yield return new WaitForSeconds(0.2f);
        //    UI_Form.UI_ResPath = "UI_Home/UI_ReadyToFight_2_2";
        //    UI_HomeControler.Inst.AddUI(UI_Form.UI_ResPath);
        //    GoNextEffect.gameObject.SetActive(false);
        //}

        //private void OnClickBox()
        //{
        //    mRewards.SetActive(true);
        //    UI_FightRewards._instance.UpdateShow();
        //    //新手引导 200402
        //    if (GuideManager.GetInstance() != null)
        //    {
        //        ObjectSelf.GetInstance().m_isOpenPerfectReward = false;
        //        GuideManager.GetInstance().StopGuide();
        //    }
        //}

        //private void OnclickMapBtn()
        //{
        //    Destroy(Card3Dmodel);
        //    UI_HomeControler.Inst.AddUI(UI_WordMap.UI_ResPath);
        //    UI_HomeControler.Inst.ReMoveUI(UI_SelectFightArea.UI_ResPath);
        //    UI_WorldMapManage._instance.WorldMapShow();
        //}

        //public void onStageSelect(int id)
        //{
        //    //if (iStageID == id) return;
        //    for (int idx = 0; idx < mStageItemList.Count; idx++)
        //    {
        //        if (mStageItemList[idx].transform.parent)
        //        {
        //            mStageItemList[idx].m3dModel.SetActive(false);
        //        }

        //    }

        //    for (int idx = 0; idx < mStageItemList.Count; idx++)
        //    {
        //        if (mStageItemList[idx].transform.parent && mStageItemList[idx].iStageID == iStageID)
        //        {
        //            mStageItemList[idx].unSelect();
        //            break;
        //        }
        //    }

        //    for (int idx = 0; idx < mStageItemList.Count; idx++)
        //    {
        //        if (mStageItemList[idx].transform.parent && mStageItemList[idx].iStageID == id)
        //        {
        //            mStageItemList[idx].onSelect();
        //            break;
        //        }
        //    }

        //    iStageID = id;
        //    ObjectSelf.GetInstance().CurStageID = iStageID;
        //    mStageInfo.setData(iStageID);
        //    mStageInfo.SetGoodsItem(iStageID);
        //}

        //public override void UpdateUIData()
        //{
        //    base.UpdateUIData();
        //    if (mMenuUI.alpha == 1)
        //    {
        //        mMenuUI.gameObject.SetActive(true);
        //        mMenu.SetActive(true);
        //        UI_SelectFightArea.Inst.mScrollMap.enabled = true;
        //        playerInfo.EnableBtn();
        //    }
        //    else
        //    {
        //        playerInfo.DisenableBtn();
        //        mMenuUI.gameObject.SetActive(false);
        //        mMenu.SetActive(false);
        //        // UI_SelectFightArea.Inst.mScrollMap.enabled = false;
        //    }
        //}
        ///// <summary>
        ///// 章节难度切换
        ///// </summary>
        //public void onLevelChange(int iLevel)
        //{
        //    //if (mCurlevel == iLevel) return;
        //    mCurlevel = iLevel;
        //    ObjectSelf.GetInstance().CurChapterLevel = iLevel;
        //    Sequence mySequence = DOTween.Sequence();
        //    mySequence.Append(mMenuUI.DOFade(0, 0.5f));
        //    mySequence.AppendCallback(onLevelChangeCall);
        //    mySequence.Append(mMenuUI.DOFade(1, 0.5f));
        //    mySequence.SetUpdate(true);
        //}
        //private void onLevelChangeCall()
        //{

        //    //Object itemObj = UIResourceMgr.LoadPrefab("UI/Prefabs/UI_Home/UI_StageItem");
        //    mStageLayout.DetachChildren();
        //    List<StageTemplate> stages = mMapStages[mCurlevel];
        //    for (int idx = 0; idx < stages.Count; idx++)
        //    {
        //        if (mStageItemList.Count <= idx)
        //        {
        //            GameObject item = Instantiate(mStageItemObj, Vector3.zero, Quaternion.identity) as GameObject;
        //            UI_StageItem stage = item.GetComponent<UI_StageItem>();
        //            stage.init(stages[idx], idx);
        //            item.transform.SetParent(mStageLayout, false);
        //            mStageItemList.Add(stage);

        //            //if (mStageItemList[idx].gameObject.activeSelf)
        //            //{
        //            //    mStageItemList[idx].gameObject.SetActive(false);
        //            //}
        //        }
        //        else
        //        {
        //            mStageItemList[idx].transform.SetParent(mStageLayout, true);
        //            mStageItemList[idx].init(stages[idx], idx);

        //            if (!mStageItemList[idx].gameObject.activeSelf)
        //            {
        //                mStageItemList[idx].gameObject.SetActive(true);
        //            }
        //        }
        //    }
        //    // 设置选择的关卡
        //    //List<int> 
        //    ObjectSelf info = ObjectSelf.GetInstance();
        //   // list = info.BattleStageData.m_BattleStageList[info.GetCurChapterID()];
        //    if (info.GetIsFight())
        //    {
        //        info.SetIsFight(false);
        //        if (stages.Count > 0)
        //        {
        //            if (ObjectSelf.GetInstance().GetCurCampaignID() == -1)
        //            {
        //                onStageSelect(stages[0].m_stageid);
        //            }
        //            else
        //            {
        //                onStageSelect(ObjectSelf.GetInstance().GetCurCampaignID());
        //            }
        //        }
        //    }
        //    else
        //    {
        //        //if (info.BattleStageData.m_BattleStageList.ContainsKey(info.GetCurChapterID()))
        //        //{
        //        //    list = info.BattleStageData.m_BattleStageList[info.GetCurChapterID()];
        //        //}
        //        list = info.BattleStageData.GetBattleStageByChapterId(info.GetCurChapterID());

        //        for (int i = 0; i < stages.Count; i++)
        //        {
        //            StageData data = list == null ? null : list.GetStageData(stages[i].m_stageid);
        //            if (data == null && i >= 1)
        //            {
        //                //Debug.LogWarning("0.0.0."+i);
        //                onStageSelect(stages[i-1].m_stageid);
        //                break;
        //            }
        //            else
        //            {
        //                if (i==stages.Count-1)
        //                {
        //                    onStageSelect(stages[i].m_stageid);
        //                }
        //            }
        //        }
                
        //    }
               
        //    if (ObjectSelf.GetInstance().GetCurChapterID() != 1001)
        //    {
        //        if (StageModule.isReward(ObjectSelf.GetInstance().CurChapterLevel, ObjectSelf.GetInstance().BattleStageData.GetRewardGot(ObjectSelf.GetInstance().GetCurChapterID())))
        //        //if (isReward(ObjectSelf.GetInstance().CurChapterLevel, iTotalCharpter[ObjectSelf.GetInstance().GetCurChapterID()].m_bRewardGot))
        //        {
        //            mBoxIamge.overrideSprite = Resources.Load(common.defaultPath + "UI_Guan_17", typeof(Sprite)) as Sprite;
        //        }
        //        else
        //        {
        //            mBoxIamge.overrideSprite = Resources.Load(common.defaultPath + "UI_Guan_16", typeof(Sprite)) as Sprite;
        //        }

        //        //var chaptertable = DataTemplate.GetInstance().m_ChapterTable;
        //        //var chapterinfo = (ChapterinfoTemplate)chaptertable.getTableData(iChapterID);
        //        ChapterinfoTemplate chapterinfo = StageModule.GetChapterinfoTemplateById(iChapterID);
        //        //mMenu.SetActive(true);
        //        ObjectSelf selfinfo = ObjectSelf.GetInstance();
        //        int tatalNum = 0;
        //        int curstartNum = 0;
        //        List<StageData> data = selfinfo.BattleStageData.GetStageDataListByChapterId(selfinfo.GetCurChapterID());
        //        if (data != null && data.Count > 0)
        //        {
        //            for (int i = 0; i < chapterinfo.getStageID().Length; i++)
        //            {
        //                //StageTemplate isStag = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(chapterinfo.getStageID()[i]);
        //                StageTemplate isStag = StageModule.GetStageTemplateById(chapterinfo.getStageID()[i]);
        //                if (mCurlevel == 1)
        //                {
        //                    if (isStag.m_stagetype == 4 || isStag.m_stagetype == 1)
        //                    {
        //                        tatalNum += 3;
        //                        for (int j = 0; j < data.Count; j++)
        //                        {
        //                            if (data[j].m_StageID == isStag.m_stageid)
        //                            {
        //                                curstartNum += data[j].m_StageStar;
        //                            }
        //                        }
        //                    }
        //                }
        //                if (mCurlevel == 2)
        //                {
        //                    if (isStag.m_stagetype == 2)
        //                    {
        //                        tatalNum += 3;
        //                        for (int j = 0; j < data.Count; j++)
        //                        {
        //                            if (data[j].m_StageID == isStag.m_stageid)
        //                            {
        //                                curstartNum += data[j].m_StageStar;
        //                            }
        //                        }
        //                    }
        //                }
        //                if (mCurlevel == 3)
        //                {
        //                    if (isStag.m_stagetype == 3)
        //                    {
        //                        tatalNum += 3;
        //                        for (int j = 0; j < data.Count; j++)
        //                        {
        //                            if (data[j].m_StageID == isStag.m_stageid)
        //                            {
        //                                curstartNum += data[j].m_StageStar;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            curstart = curstartNum;
        //            total = tatalNum;
        //            mStartNumber.text = string.Format("{0}/{1}", curstart, total);
        //        }
        //    }

        //    //// 新手引导相关--- 开启支线关卡
        //    if (ObjectSelf.GetInstance().m_isOpenZhiXian)
        //    {
        //        UI_StageItem stage = mStageItemList[mStageItemList.Count - 1];
        //        stage.onClick();
        //        ObjectSelf.GetInstance().m_isOpenZhiXian = false;
        //    }
        //}

        //private void onChapterChangeCall()
        //{
        //    mCurlevel = 1;

        //    // 更新数据
        //    iChapterID = ObjectSelf.GetInstance().GetCurChapterID();
        //    if (iChapterID != 1001)
        //    {
        //        //var chaptertable = DataTemplate.GetInstance().m_ChapterTable;
        //        //var chapterinfo = (ChapterinfoTemplate)chaptertable.getTableData(iChapterID);
        //        ChapterinfoTemplate chapterinfo = StageModule.GetChapterinfoTemplateById(iChapterID);
        //        mlPoint.SetActive(true);
        //        mrPoint.gameObject.SetActive(true);
        //        GameUtils.SetBtnSpriteGrayState(mrPoint, false);
        //        mrPoint.interactable = true;
        //        if (iChapterID == 1)
        //        {
        //            // 左箭头不可见
        //            mlPoint.SetActive(false);
        //        }
        //        if (iChapterID == iTotalCharpterNum-1)
        //        {
        //            // 右箭头不可见
        //            //if (iChapterID == DataTemplate.GetInstance().m_ChapterTable.getDataCount())
        //            if (iChapterID == StageModule.GetChapterCount())
        //            {
        //                //mrPoint.gameObject.SetActive(false);
                        
        //            }
        //            GameUtils.SetBtnSpriteGrayState(mrPoint, true);
        //            mrPoint.interactable = false;
        //        }



        //        LogManager.LogToFile("current chapterid : " + iChapterID);



        //        // 获取章节名
        //        string nameRes = chapterinfo.getChapterName();

        //        string name = GameUtils.getString(nameRes);


        //        string nameformat = GameUtils.getString(common.CharpterNameFormat);

        //        charpterName.text = string.Format(nameformat, iChapterID, name);
        //        float textwidth = charpterName.preferredWidth;
        //        float distance = textwidth - nameTextRect.sizeDelta.x;
        //        if (distance > 0.0f)
        //        {
        //            // 需要增加长度
        //            nameRect.sizeDelta = new Vector2(mNameRectInitWidth + distance, nameRect.sizeDelta.y);
        //        }
        //        else
        //        {
        //            nameRect.sizeDelta = new Vector2(mNameRectInitWidth, nameRect.sizeDelta.y);
        //        }


        //        foreach (var info in mMapStages)
        //        {
        //            info.Value.Clear();
        //        }
        //        // 关卡列表
        //        for (int idx = 0; idx < chapterinfo.getStageID().Length; idx++)
        //        {
        //            // 关卡表数据
        //            //var table = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(chapterinfo.getStageID()[idx]);
        //            StageTemplate table = StageModule.GetStageTemplateById(chapterinfo.getStageID()[idx]);
        //            switch (table.m_stagetype)
        //            {
        //                case 1:
        //                case 4:
        //                case 5:
        //                case 6:
        //                    {
        //                        mMapStages[1].Add(table);
        //                    }
        //                    break;
        //                case 2:
        //                    {
        //                        mMapStages[2].Add(table);
        //                    }
        //                    break;
        //                case 3:
        //                    {
        //                        mMapStages[3].Add(table);
        //                    }
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }

        //        var selfinfo = ObjectSelf.GetInstance();
        //        int tatalNum = 0;
        //        int curstartNum = 0;
        //        List<StageData> data = selfinfo.BattleStageData.GetStageDataListByChapterId(selfinfo.GetCurChapterID());
        //        if (data != null && data.Count > 0)
        //        {
        //            for (int i = 0; i < chapterinfo.getStageID().Length; i++)
        //            {
        //                StageTemplate isStag = StageModule.GetStageTemplateById(chapterinfo.getStageID()[i]);
        //                if (mCurlevel==1)
        //                {
        //                    if (isStag.m_stagetype == 4 || isStag.m_stagetype == 1)
        //                    {
        //                        tatalNum += 3;
        //                        for (int j = 0; j< data.Count; j++)
        //                        {
        //                            if (data[j].m_StageID == isStag.m_stageid)
        //                            {
        //                                curstartNum += data[j].m_StageStar;
        //                            }
        //                        }
                                
        //                    }
        //                }
        //                if (mCurlevel==2)
        //                {
        //                    if (isStag.m_stagetype == 2)
        //                    {
        //                        tatalNum += 3;
        //                        for (int j = 0; j < data.Count; j++)
        //                        {
        //                            if (data[j].m_StageID == isStag.m_stageid)
        //                            {
        //                                curstartNum += data[j].m_StageStar;
        //                            }
        //                        }
        //                    }
        //                }
        //                if (mCurlevel==3)
        //                {
        //                    if (isStag.m_stagetype == 3)
        //                    {
        //                        tatalNum += 3;
        //                        for (int j = 0; j < data.Count; j++)
        //                        {
        //                            if (data[j].m_StageID == isStag.m_stageid)
        //                            {
        //                                curstartNum += data[j].m_StageStar;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            curstart = curstartNum;
        //            total = tatalNum;
        //            mStartNumber.text = string.Format("{0}/{1}", curstart, total);
        //            //Image mBoxImage = mBox.transform.GetComponent<Image>();

        //        }

        //        // 设置难度
        //        for (int i = 0; i < 3; i++)
        //        {
        //            mLevelButtons[i].gameObject.SetActive(mMapStages[i + 1].Count > 0);
        //            if (mMapStages[i + 1].Count == 0)
        //            {
        //                mLevelButtons[i].isOpen = false;
                        
        //            }
        //            else
        //            {
        //                int istart;
        //                int ifirstStage = mMapStages[i + 1][0].m_stageid;
        //                if (selfinfo.BattleStageData.IsCopyScenePass(iChapterID, ifirstStage, out istart) && istart == -1)
        //                {
        //                    mLevelButtons[i].isOpen= false;
        //                }
        //                else
        //                {
        //                    mLevelButtons[i].isOpen= true;
        //                }
        //            }
        //        }
        //        mLevelButtonsImage[0].overrideSprite = Resources.Load(common.defaultPath + "UI_Guan_22", typeof(Sprite)) as Sprite;
        //       // mLevelButtonsImage[0].transform.GetComponent<Button>().enabled = false;
        //        mLevelButtons[0].isClick = true;
        //        for (int i = 1; i < mLevelButtons.Count; i++)
        //        {
        //            mLevelButtons[i].isClick = false;
        //            mLevelButtonsImage[i].overrideSprite = Resources.Load(common.defaultPath + "UI_Guan_23", typeof(Sprite)) as Sprite;
        //           // mLevelButtonsImage[i].transform.GetComponent<Button>().enabled = true;
        //        }
        //        onLevelChangeCall();
        //        //mLevelButtons[ObjectSelf.GetInstance().CurChapterLevel - 1].isOn = true;
        //    }
        //}

        ///// <summary>
        ///// 箭头按钮
        ///// </summary>
        ///// <param name="isFront">是不是向前的</param>
        //public void onPointCall(bool isFront)
        //{
        //    int id = ObjectSelf.GetInstance().GetCurChapterID() + (isFront ? 1 : -1);
        //    //int isize = DataTemplate.GetInstance().m_ChapterTable.m_Data.Count;
        //    if (id <= 1)
        //    {
        //        id = 1;
        //    }
        //    else if (id >= iTotalCharpterNum)
        //    {
        //        id = iTotalCharpterNum;
        //    }


        //    ObjectSelf.GetInstance().SetCurChapterID(id);
        //    //mCurlevel = 0;
        //    mMenuUI.DOFade(0, 0.2f);
        //    mScrollMap.onMoveTo(id - 1);
        //    // mEffects.SetActive(false);
        //    GoNext_Btn.gameObject.SetActive(false);
        //    //Sequence mySequence = DOTween.Sequence();
        //    //mySequence.Append(mMenuUI.DOFade(0, 0.2f));
        //    //mySequence.AppendCallback(onChapterChangeCall);
        //    //mySequence.Append(mMenuUI.DOFade(1, 0.2f));
        //    //mySequence.SetUpdate(true);
        //    // 测试
        //    //mMenuUI.DOFade(0, 0.5f).SetUpdate(false);
        //}

        //public void onBeginMoveCall()
        //{
        //    mMenuUI.DOFade(0, 0.2f);
        //    GoNext_Btn.gameObject.SetActive(false);
        //    // mMenu.SetActive(false);
        //    //mEffects.SetActive(false);
        //}

        //public void onEndMoveCall(int id)
        //{
        //    ObjectSelf.GetInstance().SetCurChapterID(id);
        //    // mEffects.SetActive(true);
        //    GoNext_Btn.gameObject.SetActive(true);
        //    //mMenu.SetActive(true);
        //    UI_StageLevelButton._instance.ilevel = 0;
        //    if (iTotalCharpterNum - 1 == id)
        //    {
        //        mrPoint.enabled = false;
        //    }
        //    else
        //    {
        //        mrPoint.enabled = true;
        //    }
        //    if (iChapterID != id)
        //    {
        //        Sequence mySequence = DOTween.Sequence();
        //        mySequence.AppendCallback(onChapterChangeCall);
        //        mySequence.Append(mMenuUI.DOFade(1, 0.2f));
        //        mySequence.SetUpdate(true);
        //    }
        //    else
        //    {
        //        Sequence mySequence = DOTween.Sequence();
        //        mySequence.Append(mMenuUI.DOFade(1, 0.2f));
        //        mySequence.SetUpdate(true);
        //    }
        //}
        ///// <summary>
        ///// 新地图开启显示
        ///// </summary>
        //public void OnNewMapOpenShow()
        //{

        //    if (ObjectSelf.GetInstance().GetIsNewMap() && ObjectSelf.GetInstance().GetCurChapterID() != 1)
        //    {
        //        UI_HomeControler.Inst.AddUI(UI_NewMapOpenTxt.UI_ResPath);
        //    }
        //}
    }
}
