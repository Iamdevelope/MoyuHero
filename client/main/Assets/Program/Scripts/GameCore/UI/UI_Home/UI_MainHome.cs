using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;
using DreamFaction.UI.Core;
using GNET;
using DG.Tweening;
using DreamFaction.GameNetWork;
using System.Text;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DreamFaction.LogSystem;

namespace DreamFaction.UI
{
    /// <summary>
    /// 主场景的主界面，继承自BaseUI
    /// </summary>
    public class UI_MainHome : BaseUI
    {
        public static string UI_ResPath = "UI_Home/UI_MainHome_3_1";
        private static UI_MainHome _Inst;
        public static GameObject m_CamForm;   //阵型摄像机
        public static GameObject m_CamClone;  //克隆摄像机

        public static bool NeedShowBattlePanel = false;

        public Camera m_CamUI;                 //UI相机

        private Button _ShowToolBtn;          //显示左边工具栏按钮
        private Button _BackToolBtn;          //返回左边工具栏按钮
        private GameObject _Tream1;           //阵型1英雄信息
        private GameObject _Tream2;           //阵型2英雄信息

        private Button m_AtlasBtn;             //图鉴按钮
        private Button m_CloneBtn;             //英雄克隆按钮
        private Button _FightBtn;              //战斗按钮
        private Button _HeroInfoBtn;           // 英雄信息按钮
        private Button _BagBtn;                //背包按钮
        private Button _FormBtn;               //阵型按钮
        private Button _UniBtn;                //公会按钮
        private Button _ShopBtn;               //商店按钮
        private Button _ExPBuffBtn;
        private Button _SetBtn;                //设置按钮
        private Button _ChatBtn;               //聊天按钮
        private Button _EmailBtn;              //信件按钮
        private Image _NewEmail;                //新邮件提醒
        private Button _LitholysinBtn;         // 熔灵按钮
        private Button _ExpItemBtn;            //历练按钮
        private Button _artifact;             // 神器按钮
        private Button _runeExpBtn;           // 符文熔炼按钮
        private Button _FriendBtn;             //朋友按钮
        private Button _PlayingItem;            //玩法按钮
        private Button _PromptBtn;             //限时关卡按钮
        private Button _LivenessBtn;           //活跃度任务按钮
        private Button _RecruitBtn;            // 招募按钮
        private Button _ActivityBtn;            //活动总览按钮

        private Transform MsgBoxGroup;         //消息父节点  
        private Animation FightBtn_Anim;      // 战斗按钮动画组
        private Animator anim_TopPanel;       //顶部组件动画机
        private Animator anim_RightPanel;     //右部组件状态机
        private Animator anim_LeftToolPanel;  //左侧工具栏组件状态机
        private Animator anim_LeftPanel;      //左侧组件状态机

        private FunctionTipsManager m_FunctionTipsManager;//功能提示管理器
        private IFunctionTipsController m_TipsController;
        private int m_TestIndex = -1;                            //历练按钮功能提示在m_TipsController中的索引
        public bool isUpdateHeroInfo = false;
        private List<int> tipsImdex = new List<int>();
        //private List<GameObject> MsgBoxList;  //冒泡盒子组

        // ========================= 继承 =========================
        // 1：初始化数据
        public override void InitUIData()
        {
            _Inst = this;
            m_CamForm = GameObject.Find("TeamViewRoom/Camera");
            GameObject _modelViewRoom  = GameObject.Find("ModelViewRoom");
            m_CamClone = _modelViewRoom.transform.FindChild("Camera2").gameObject;
            m_CamForm.SetActive(false);
            m_CamClone.SetActive(false);
            m_CamUI = GameObject.Find("UI_HomeControler/UI_Camera3").GetComponent<Camera>();
            _Tream1 = selfTransform.FindChild("Trem1").gameObject;
            _Tream2 = selfTransform.FindChild("Trem2").gameObject;
            MsgBoxGroup = selfTransform.FindChild("MsgBoxGroup");
            _FightBtn = selfTransform.FindChild("Fight_Btn").GetComponent<Button>();
            _HeroInfoBtn = selfTransform.FindChild("LeftPanel/LeftDynamicPanel/HeroItem/Button").GetComponent<Button>();
            _LitholysinBtn = selfTransform.FindChild("LeftPanel/LeftDynamicPanel/LitholysinItem/Button").GetComponent<Button>();
            _artifact = selfTransform.FindChild("LeftPanel/LeftDynamicPanel/GodweaponItem/Button").GetComponent<Button>();
            _runeExpBtn = selfTransform.FindChild("LeftPanel/LeftDynamicPanel/CsmeltItem/Button").GetComponent<Button>();
            _BagBtn = selfTransform.FindChild("LeftPanel/LeftDynamicPanel/knpItem/Button").GetComponent<Button>();
            _FormBtn = selfTransform.FindChild("LeftPanel/LeftDynamicPanel/FormItem/Button").GetComponent<Button>();
            _UniBtn = selfTransform.FindChild("LeftPanel/LeftDynamicPanel/UniItem/Button").GetComponent<Button>();
            _ShopBtn = selfTransform.FindChild("LeftPanel/LeftDynamicPanel/ShopItem/Button").GetComponent<Button>();
            _ExpItemBtn = selfTransform.FindChild("LeftPanel/LeftDynamicPanel/ExpItem/Button").GetComponent<Button>();
            _ExPBuffBtn = selfTransform.FindChild ( "ExPBuffBtn" ).GetComponent<Button> ();
            
            _SetBtn = selfTransform.FindChild("RightPanel/Set_Btn").GetComponent<Button>();
            _ChatBtn = selfTransform.FindChild("RightPanel/Chat_Btn").GetComponent<Button>();
            _EmailBtn = selfTransform.FindChild("RightPanel/Email_Btn").GetComponent<Button>();
            _NewEmail = selfTransform.FindChild("RightPanel/Email_Btn/Image").GetComponent<Image>();
            _FriendBtn = selfTransform.FindChild("RightPanel/Friend_Btn").GetComponent<Button>();
            _PromptBtn = selfTransform.FindChild("Prompt").GetComponent<Button>();
            _RecruitBtn = selfTransform.FindChild("RightPanel/Recruit_Btn").GetComponent<Button>();
            _RecruitBtn.onClick.AddListener(new UnityAction(OnClickRecruitBtn));
            _ActivityBtn = selfTransform.FindChild("ActivityOverview").GetComponent<Button>();
            _ActivityBtn.onClick.AddListener(new UnityAction(OnClickActivityBtn));
            m_CloneBtn = selfTransform.FindChild("LeftPanel/LeftDynamicPanel/CloneItem/Button").GetComponent<Button>();
            m_AtlasBtn = selfTransform.FindChild("LeftPanel/LeftDynamicPanel/AtlasItem/Button").GetComponent<Button>();


            _PlayingItem = selfTransform.FindChild("LeftPanel/LeftDynamicPanel/PlayingItem/Button").GetComponent<Button>();

            //vipLevelPos = RoleName.transform.FindChild("VIPIcon/VIPLevel");
            _ShowToolBtn = selfTransform.FindChild("LeftPanel/LeftDynamicPanel/ShowBtn").GetComponent<Button>();
            _BackToolBtn = selfTransform.FindChild("LeftPanel/LeftDynamicPanel/BackBtn").GetComponent<Button>();
            _LivenessBtn = selfTransform.FindChild("RightPanel/ActiveValue_Btn").GetComponent<Button>();
            //获取动画控制
            anim_TopPanel = selfTransform.Find("TopPanel").GetComponent<Animator>();
            FightBtn_Anim = _FightBtn.GetComponent<Animation>();
            anim_RightPanel = selfTransform.Find("RightPanel").GetComponent<Animator>();
            anim_LeftPanel = selfTransform.FindChild("LeftPanel").GetComponent<Animator>();
            anim_LeftToolPanel = anim_LeftPanel.transform.FindChild("LeftDynamicPanel").GetComponent<Animator>();
            InitFunly();

            m_FunctionTipsManager = new FunctionTipsManager();
            m_FunctionTipsManager.Init();
            //添加监听事件

            GameEventDispatcher.Inst.addEventListener(GameEventID.G_VipLevelUp, VipLevelUpHandler);
            GameEventDispatcher.Inst.addEventListener(GameEventID.F_LimitFightEnd, LimitFightEndClearing);
            GameEventDispatcher.Inst.addEventListener(GameEventID.UI_InterfaceChange, OnInterfaceChange);
            GameEventDispatcher.Inst.addEventListener(GameEventID.G_GetWorldBoss, OnWorldBossMessage);
            GameEventDispatcher.Inst.addEventListener(GameEventID.G_SGetBossRank, OnWorldBossMessage);

            _ShowToolBtn.onClick.AddListener(new UnityAction(OnShowTool));
            _BackToolBtn.onClick.AddListener(new UnityAction(OnBackTool));
            _FightBtn.onClick.AddListener(new UnityAction(OnclickFightBtn));
            _HeroInfoBtn.onClick.AddListener(new UnityAction(OnclickHeroInfoBtn));
            _LitholysinBtn.onClick.AddListener(new UnityAction(OnclickLitholysinBtn));
            _artifact.onClick.AddListener(new UnityAction(OnclickArtifact));
            _runeExpBtn.onClick.AddListener(new UnityAction(OnclickRuneExp));
            _ExpItemBtn.onClick.AddListener(new UnityAction(OnclickExpItemBtn));
            _BagBtn.onClick.AddListener(new UnityAction(OnclickBagBtn));
            _FormBtn.onClick.AddListener(new UnityAction(OnclickFormBtn));
            _UniBtn.onClick.AddListener(new UnityAction(OnclickUniBtn));
            _ShopBtn.onClick.AddListener(new UnityAction(OnclickShopBtn));
            _ExPBuffBtn.onClick.AddListener ( new UnityAction ( OnclickExPBuffBtn ) );
            
            _SetBtn.onClick.AddListener(new UnityAction(OnclickSetBtn));
            _ChatBtn.onClick.AddListener(new UnityAction(OnclickChatBtn));
            _EmailBtn.onClick.AddListener(new UnityAction(OnclickEmailBtn));
            _FriendBtn.onClick.AddListener(new UnityAction(OnclickFriendBtn));
            m_CloneBtn.onClick.AddListener(new UnityAction(OnclickCloneBtn));
            m_AtlasBtn.onClick.AddListener(new UnityAction(OnclickAtlasBtn));
            _LivenessBtn.onClick.AddListener(new UnityAction(OnLivenssBtn));
            _PromptBtn.onClick.AddListener(new UnityAction(OnClickPromptBtn));
            _PlayingItem.onClick.AddListener(new UnityAction(OnClickPlayingItemBtn));


        }

        public Image GetNewEmail()
        {
            return _NewEmail;
        }


        //初始化UI数据
        public override void InitUIView()
        {
            if (ObjectSelf.GetInstance().isLimitWindow)
            {
                ObjectSelf.GetInstance().isLimitWindow = false;
                var _ui = UI_HomeControler.Inst.AddUI(UI_TestPanel.GetPath());
                _ui.GetComponent<UI_TestPanel>().GotoLimitWindow();

            }
            if (ObjectSelf.GetInstance().GetChangeLevel())
            {
                ObjectSelf.GetInstance().SetChangeLevel(false);
                OnclickFightBtn();

            }
            if (ObjectSelf.GetInstance().GetPromptTime())
            {
                if (ObjectSelf.GetInstance().GetPromptFome())
                {
                    ObjectSelf.GetInstance().SetPromptFome(false);
                    isPromptFome();
                }
                if (ObjectSelf.GetInstance().GetPromptBttleend())
                {
                    ObjectSelf.GetInstance().SetPromptBttleend(false);
                    isPromptBttleend();
                }
                ObjectSelf.GetInstance().SetPromptTime(false);
            }

            if (ObjectSelf.GetInstance().GetPromoptFight())
            {
                ObjectSelf.GetInstance().SetPromptFight(false);
                OnClickPromptBtn();
            }

            if (ObjectSelf.GetInstance().GetHeroJoin())
            {
                ObjectSelf.GetInstance().SetHeroJoin(false);
                UI_HomeControler.Inst.AddUI(UI_Recruit.UI_ResPath);
            }
            if (ObjectSelf.GetInstance().GetProperty())
            {
                ObjectSelf.GetInstance().SetProperty(false);
                FailButtonClick(2);
            }
            if (ObjectSelf.GetInstance().GetExUp())
            {
                ObjectSelf.GetInstance().SetExUp(false);
                FailButtonClick(0);
            }
            if (ObjectSelf.GetInstance().GetSkillStr())
            {
                ObjectSelf.GetInstance().SetSkillStr(false);
                FailButtonClick(3);
            }


            if (ObjectSelf.GetInstance().GetHandBook())
            {
                ObjectSelf.GetInstance().SetHandBook(false);
                OnclickAtlasBtn();
            }

            UI_HomeControler.NeedShowMedardGotoObj = ShowMedalReardGotoObj();
            //经验
            //uint Ex = ObjectSelf.GetInstance().ExpFruit;
            //Exbar.value = Ex;
            //英雄信息
            ShowMianHeroData();

            if (NeedShowBattlePanel)
            {
                NeedShowBattlePanel = false;
                OnclickFightBtn();
            }


            m_TipsController = CreateFunctionTipsController();
            m_TipsController.Refresh();
            //InvokeRepeating("UpdateMainHeroInfo", 0, 1);

            JudegIsShowNewGuide();
            InitGuide();

            UI_HomeControler.Inst.AddUI(UI_CaptionManager.GetPath());
            UpdateMainHeroInfo ();
        }

        // 初始化取名界面
        void InitUserName()
        {
            UI_HomeControler.Inst.AddUI(UI_Intitle.UI_ResPath);
        }

        void InitGuide()
        {
            // 重新加载场景,开始新手引导,引导布阵
            if (GuideManager.GetInstance().isGuideUser)
            {
                if (GuideManager.GetInstance().GetBackCount(100313) && GuideManager.GetInstance().IsContentGuideID(100401) == false)
                {
                    GuideManager.GetInstance().ShowGuideWithIndex(100401);
                    return;
                }
            }

            int interruptID = GuideManager.GetInstance().interruptID;
            // 新手引导
            if (GuideManager.GetInstance().isIntitle)
            {
                GuideManager.GetInstance().ShieldGuide();//关闭新手引导
                //InitUserName();
            }
            else if (GuideManager.GetInstance().GetLastID() == 100201 || GuideManager.GetInstance().GetLastID() == 100202)
            {
                GuideManager.GetInstance().RemoveGuideID(100202);
                GuideManager.GetInstance().ShowGuideWithIndex(100201);
            }
            else if (interruptID >= 100203 && interruptID <= 100313 && GuideManager.GetInstance().GetLastID() <= 100313)
            {
                for (int i = 100203; i < 100313; ++i)
                    GuideManager.GetInstance().RemoveGuideID(i);

                GuideManager.GetInstance().ShowGuideWithIndex(100301);
            }
            else if (interruptID >= 100401 && interruptID <= 100403 && GuideManager.GetInstance().GetLastID() <= 100403)
            {
                GuideManager.GetInstance().RemoveGuideID(100401);
                GuideManager.GetInstance().RemoveGuideID(100402);
                GuideManager.GetInstance().RemoveGuideID(100403);

                GuideManager.GetInstance().ShowGuideWithIndex(100401);
            }
            else
            {

            }
        }

        public Transform GetMsgBoxGroup() { return MsgBoxGroup; }

        public override void UpdateUIView()
        {
            base.UpdateUIView();
            //for (int i = 0; i < _Tream1.transform.childCount; i++)
            //{
            //    Vector3 pos1 = Camera.main.WorldToScreenPoint(HomeControler.Inst.GetHoergroup1().GetChild(i).position);//目标的世界坐标
            //    pos1 = _camUI.ScreenToWorldPoint(pos1);
            //    _Tream1.transform.GetChild(i).position = pos1;
            //}

        }

        public void UpdateMainHeroInfo()
        {
            //优化：每帧产生5.7kB的 GC Alloc 标记一下这个地方的代码要换一个地方处理 [7/29/2015 Zmy]
            Vector3 pos1 = Camera.main.WorldToScreenPoint(HomeControler.Inst.GetHoergroup1().position);//目标的世界坐标
            pos1 = m_CamUI.ScreenToWorldPoint(pos1);
            pos1.y += 0.3f;
            _Tream1.transform.position = pos1;

//             Vector3 pos2 = Camera.main.WorldToScreenPoint(HomeControler.Inst.GetHoergroup2().position);//目标的世界坐标
//             pos2 = m_CamUI.ScreenToWorldPoint(pos2);
//             pos2.y += 0.3f;
//             _Tream2.transform.position = pos2;
        }


        // 2：播放进场动画 默认UI状态是 PlayingEnterAnimation
        public override void OnPlayingEnterAnimation()
        {
            anim_TopPanel.SetBool("Enter_TopPanel", true);
            anim_RightPanel.SetBool("Enter_RightRanel", true);
            anim_LeftPanel.SetBool("Enter_LeftPanel", true);
            FightBtn_Anim.Play("Enter_Fight_Btn");

        }

        // 3：播放退出动画 默认UI状态是 PlayingEnterAnimation 
        public override void OnPlayingExitAnimation()
        {
            anim_TopPanel.SetBool("Enter_TopPanel", false);
            anim_RightPanel.SetBool("Enter_RightRanel", false);
            anim_LeftPanel.SetBool("Enter_LeftPanel", false);
            FightBtn_Anim.Play("Exit_Fight_Btn");

        }

        public static UI_MainHome GetInst()
        {
            return _Inst;
        }


        private void VipLevelUpHandler()
        {
            UI_HomeControler.Inst.AddUI(UI_VipLvUpMgr.Path);
        }

        // ========================== 回调事件 =======================
        // 开始战斗回调事件
        private void OnclickFightBtn()
        {
            //UIState = UIStateEnum.PlayingExitAnimation;
            //UI_HomeControler.Inst.AddUI(UI_SelectFightArea.UI_ResPath);
            //ObjectSelf.GetInstance().Week = ObjectSelf.GetInstance().GetWeek();
            //ObjectSelf.GetInstance().SetIsPrompt(false);

            // 100301
            UI_HomeControler.Inst.AddUI(UI_SelectLevelMgrNew.UI_ResPath);

            if (GuideManager.GetInstance().IsContentGuideID(100501))
            {
                GuideManager.GetInstance().StopGuide();
            }
        }

        // 点击英雄头像按钮回调事件
        private void OnclickHeroInfoBtn()
        {
            int count = ObjectSelf.GetInstance().HeroContainerBag.GetHeroList().Count;
            if (count > 0)
            {
                UI_HomeControler.Inst.AddUI(UI_HeroInfo.UI_ResPath);
                UI_HeroInfo._instance.SelectedShow(0, true);
            }
            else
            {
                GameObject go = UI_HomeControler.Inst.AddUI(UI_GameTips.UI_ResPath);
                go.GetComponent<UI_GameTips>().type = UI_GameTips.TipsType.Recruit;
            }
        }

        //背包回调
        private void OnclickBagBtn()
        {
            UI_HomeControler.Inst.AddUI(UI_Bag.UI_ResPath);
            
        }
        //阵型回调
        private void OnclickFormBtn()
        {
            //UI_Form.UI_ResPath = "UI_Home/UI_Form_2_2";
            UI_HomeControler.Inst.AddUI(UI_FormMgr.UI_ResPath);
        }
        //公会回调
        private void OnclickUniBtn()
        {
            //UI_HomeControler.Inst.AddUI ( HeroStrengthen.UI_ResPath );

            //HomeControler.Inst.PushFunly(3, 97);
            ////UI_HomeControler.Inst.AddUI(UI_Uni.UI_ResPath);
            //SSendMsgNotify _smn = new SSendMsgNotify();
            //_smn.msgid = 0;
            //GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_MsgNotify,_smn);

            UI_HomeControler.Inst.AddUI ( UI_StoreMgr.UI_ResPath );

            CGetNewShop cgns = new CGetNewShop();
            IOControler.GetInstance().SendProtocol(cgns);
        }
        //商店回调
        private void OnclickShopBtn()
        {
            //UI_HomeControler.Inst.AddUI(UI_Shop.UI_ResPath);
            UI_HomeControler.Inst.AddUI(UI_ShopMgr.UI_ResPath);

        }

        //装备强化
        private void OnclickExPBuffBtn ()
        {
            //UI_HomeControler.Inst.AddUI ( HeroStrengthen.UI_ResPath );
            //HeroStrengthen.Inst.OpenEquipStrengthen ();
        }

        // 熔灵
        private void OnclickLitholysinBtn()
        {
            UI_HomeControler.Inst.AddUI(UI_HeroLitholysin.UI_ResPath);
        }

        // 神器
        private void OnclickArtifact()
        {
            UI_HomeControler.Inst.AddUI(UI_Artifact.UI_ResPath);
        }

        // 符文熔炼
        private void OnclickRuneExp()
        {
            UI_HomeControler.Inst.AddUI(UI_RuneExp.UI_ResPath);
        }

        //英雄克隆按钮
        private void OnclickCloneBtn()
        {
            UI_HomeControler.Inst.AddUI(UI_HeroCloneManager.UI_ResPath);
        }

        //图鉴按钮
        private void OnclickAtlasBtn()
        {
            CTuJianHeros _ctj = new CTuJianHeros();
            IOControler.GetInstance().SendProtocol(_ctj);

            UI_HomeControler.Inst.AddUI(UI_HandBookManager.UI_ResPath);
        }

        //设置
        private void OnclickSetBtn()
        {
            //UI_HomeControler.Inst.AddUI(UI_Set.UI_ResPath);
            UI_HomeControler.Inst.AddUI(SystemSetting.UI_ResPath);
        }
        //聊天
        private void OnclickChatBtn()
        {
            // UI_HomeControler.Inst.AddUI(UI_Chat.UI_ResPath);
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_OpenOrCloseUI, UI_GM_InputTxt.UI_ResPath);
        }
        //信件
        private void OnclickEmailBtn()
        {
            UI_HomeControler.Inst.AddUI(UI_EmailManager.UI_ResPath);          
        }

        //好友
        private void OnclickFriendBtn()
        {
            ////UI_HomeControler.Inst.AddUI(UI_Friend.UI_ResPath);
            //SSendMsgNotify _smn = new SSendMsgNotify();
            //_smn.msgid = 0;
            //GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_MsgNotify, _smn);

            //UI_HomeControler.Inst.AddUI(HeroStrengthen.UI_ResPath);
        }

        private void OnMonthCardBtnClick()
        {
            UI_HomeControler.Inst.AddUI(UI_YueKaMgr.UI_ResPath);
        }

        //历练按钮
        private void OnclickExpItemBtn()
        {
            UI_HomeControler.Inst.AddUI(UI_TestPanel.GetPath());
        }

        // 招募
        private void OnClickRecruitBtn()
        {
            UI_HomeControler.Inst.AddUI(UI_Lottery.UI_ResPath);
        }

        // 活动总览
        private void OnClickActivityBtn()
        {
            UI_HomeControler.Inst.AddUI(ActivityOverviewManager.UI_ResPath);
        }

        //限时关卡
        private void OnClickPromptBtn()
        {
            ObjectSelf.GetInstance().SetIsPrompt(true);
            ObjectSelf.GetInstance().Week = ObjectSelf.GetInstance().GetWeek();
            UI_HomeControler.Inst.AddUI(UI_PromptFightArea.UI_ResPath);
        }
        //玩法按钮
        private void OnClickPlayingItemBtn()
        {
            UI_HomeControler.Inst.AddUI(UI_PlayingMethod.UI_ResPath);
        }

        private void OnLivenssBtn()
        {
            CGetHuoYue chy = new CGetHuoYue();
            IOControler.GetInstance().SendProtocol(chy);
            UI_HomeControler.Inst.AddUI(UI_Liveness.UI_ResPath);

        }
        //点击显示按钮
        public void OnShowTool()
        {
            anim_LeftToolPanel.SetBool("LeftDynamicPanel", true);
            _ShowToolBtn.gameObject.SetActive(false);
            _BackToolBtn.gameObject.SetActive(true);
        }
        //返回按钮
        public void OnBackTool()
        {
            anim_LeftToolPanel.SetBool("LeftDynamicPanel", false);
            _ShowToolBtn.gameObject.SetActive(true);
            _BackToolBtn.gameObject.SetActive(false);
        }
        private void FailButtonClick(int type)
        {

            UI_HomeControler.Inst.AddUI(UI_HeroInfo.UI_ResPath);
            UI_HeroInfo._instance.DefeatShow(type);
        }


        private void isPromptFome()
        {
            InterfaceControler.GetInst().AddMsgBox("限时关卡已结束无法战斗，请等待下次开启", GetMsgBoxGroup());
        }
        private void isPromptBttleend()
        {
            InterfaceControler.GetInst().AddMsgBox("限时关卡已结束，请等待下次开启", GetMsgBoxGroup());
        }

        /// <summary>
        /// 显示主界面英雄的信息
        /// </summary>
        public void ShowMianHeroData()
        {
            _Tream1.SetActive(false);
            _Tream2.SetActive(false);
            for (int i = 0; i < _Tream1.transform.childCount; ++i)
            {
                UI_MainHeroInfo uiInfo = _Tream1.transform.GetChild(i).GetComponent<UI_MainHeroInfo>();
                uiInfo.OnClearObj();
            }
            for (int i = 0; i < _Tream2.transform.childCount; ++i)
            {
                UI_MainHeroInfo uiInfo = _Tream2.transform.GetChild(i).GetComponent<UI_MainHeroInfo>();
                uiInfo.OnClearObj();
            }
            int type = ObjectSelf.GetInstance().Teams.GetFormationType();
            int GroupCount = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
            int HeroCount = ObjectSelf.GetInstance().Teams.m_Matrix.GetLength(1);
            if (type == 1)
            {
                _Tream1.SetActive(true);
                for (int i = 0; i < HeroCount; ++i)
                {
                    ObjectCard temp = ObjectSelf.GetInstance().HeroContainerBag.FindHero(ObjectSelf.GetInstance().Teams.m_Matrix[GroupCount, i]);
                    if (temp == null)
                        continue;
                    UI_MainHeroInfo uiHeroInfo = _Tream1.transform.GetChild(i).GetComponent<UI_MainHeroInfo>();
                    uiHeroInfo.InitHeroData(temp);
                }
            }
            else
            {
                _Tream2.SetActive(true);
                for (int i = 0; i < HeroCount; ++i)
                {
                    ObjectCard temp = ObjectSelf.GetInstance().HeroContainerBag.FindHero(ObjectSelf.GetInstance().Teams.m_Matrix[GroupCount, i]);
                    if (temp == null)
                        continue;
                    UI_MainHeroInfo uiHeroInfo = _Tream2.transform.GetChild(i).GetComponent<UI_MainHeroInfo>();
                    uiHeroInfo.InitHeroData(temp);
                }
            }
        }

        /// <summary>
        /// 加载试炼结算框
        /// </summary>
        private void LimitFightEndClearing()
        {
            if (UI_HomeControler.Inst != null)
            {
                UI_HomeControler.Inst.AddUI(UI_TestClearingObj.UI_ResPath);
            }
        }

        //删除监听事件
        void OnDestroy()
        {
            m_FunctionTipsManager.Release();
            GameEventDispatcher.Inst.removeEventListener(GameEventID.G_VipLevelUp, VipLevelUpHandler);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.F_LimitFightEnd, LimitFightEndClearing);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_InterfaceChange, OnInterfaceChange);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.G_GetWorldBoss, OnWorldBossMessage);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.G_SGetBossRank, OnWorldBossMessage);
            _Inst = null;
        }

        void OnInterfaceChange()
        {
            if (UI_HomeControler.Inst.GetCanvas2ChildCount() <= 0)
            {
                m_TipsController.Refresh();
            }
        }
        void OnWorldBossMessage()
        {
            m_TipsController.RefreshByIndex(m_TestIndex);
        }
        //生成功能提示控制器
        IFunctionTipsController CreateFunctionTipsController()
        {
            GameObject _go;
            FunctionTipsController _controller = new FunctionTipsController();

            var _manager = FunctionTipsManager.GetInstance();
            _go = selfTransform.FindChild("RightPanel/Recruit_Btn/Image").gameObject;                     //招募按钮
            _controller.AddControlledObject(_go, _manager.CheckRelicFreeCount,_manager.CheckHeroRecruitFree);

            _go = selfTransform.FindChild("RightPanel/Email_Btn/Image").gameObject;                       //邮箱按钮
            _controller.AddControlledObject(_go, _manager.CheckUnreadMail);

            _go = selfTransform.FindChild("RightPanel/ActiveValue_Btn/Image").gameObject;                 //活跃度按钮
            _controller.AddControlledObject(_go, _manager.CheckLivenessAward);

            _go = selfTransform.FindChild("LeftPanel/LeftDynamicPanel/HeroItem/Image").gameObject;        //英雄按钮
            _controller.AddControlledObject(_go, _manager.CheckUpgradableHeroInTeam,
                                                _manager.CheckAdvancedHeroInTeam,
                                                _manager.CheckHeroRuneInTeam,
                                                _manager.CheckAttributeTrainInTeam,
                                                _manager.CheckSkillUpgradeInTeam
                                                );

            _go = selfTransform.FindChild("LeftPanel/LeftDynamicPanel/knpItem/Image").gameObject;         //背包按钮
            _controller.AddControlledObject(_go, _manager.CheckIsNewCommon,_manager.CheckIsNewRune);

            _go = selfTransform.FindChild("LeftPanel/LeftDynamicPanel/FormItem/Image").gameObject;        //阵型按钮
            _controller.AddControlledObject(_go, _manager.CheckTeamNoMember);

            _go = selfTransform.FindChild("LeftPanel/LeftDynamicPanel/ShopItem/Image").gameObject;        //商店按钮
            _controller.AddControlledObject(_go, _manager.CheckNonPurchasedGiftSet);

            _go = selfTransform.FindChild("LeftPanel/LeftDynamicPanel/AtlasItem/Image").gameObject;       //图鉴按钮
            int _atlasItem = _controller.AddControlledObject(_go, _manager.CheckHandbookAward);

            _go = selfTransform.FindChild("LeftPanel/LeftDynamicPanel/ExpItem/Image").gameObject;         //历练按钮
            m_TestIndex = _controller.AddControlledObject(_go, _manager.CheckLimitTest, _manager.CheckWorldBoss);

            _go = selfTransform.FindChild("LeftPanel/LeftDynamicPanel/PlayingItem/Image").gameObject;     //玩法按钮
            int _playingItem = _controller.AddControlledObject(_go, _manager.CheckExploreAward, _manager.CheckInGetPowerTime, _manager.CheckSacredAltar);

            _go = selfTransform.FindChild("LeftPanel/LeftDynamicPanel/FlashTips").gameObject;
            _controller.AddParentCtrlObjUnit(_go, _atlasItem, m_TestIndex, _playingItem);

            _go = selfTransform.FindChild("ActivityOverview/TipsImage").gameObject;
            _controller.AddControlledObject(_go, ()=>false);

            //_go = selfTransform.FindChild("TopPanel/PlayerInfo/MonthCardBtn/TipsImage").gameObject;
            //_controller.AddControlledObject(_go, _manager.CheckUnclaimedMonthCard);

            return _controller;
        }

        /// <summary>
        /// 是否显示勋章达成奖励跳转提示
        /// </summary>
        /// <returns>是 or 否</returns>
        private bool ShowMedalReardGotoObj()
        {
            Dictionary<int, IExcelBean> _medalXmlData = DataTemplate.GetInstance().m_MedalexchangeTable.getData();
            foreach (var item in _medalXmlData)
            {
                MedalexchangeTemplate _medalData = item.Value as MedalexchangeTemplate;
                if (ObjectSelf.GetInstance().GetHandBookBoxList().Contains(_medalData.getId()) == false)
                {
                    if (_medalData.getExchangeType() == 1 && ObjectSelf.GetInstance().HuangjinXZ >= _medalData.getNeedNum())
                    {
                        //UI_HomeControler.Inst.AddUI(UI_MedardGotoObj.UI_ResPath);
                        return true;
                    }
                    else if (_medalData.getExchangeType() == 2 && ObjectSelf.GetInstance().BaiJinXZ >= _medalData.getNeedNum())
                    {
                        return true;
                    }
                    else if (_medalData.getExchangeType() == 3 && ObjectSelf.GetInstance().QingTongXZ >= _medalData.getNeedNum())
                    {
                        return true;
                    }
                    else if (_medalData.getExchangeType() == 4 && ObjectSelf.GetInstance().ChiTieXZ >= _medalData.getNeedNum())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // 引导战斗按钮
        public void InitGuideFightBtn(int id)
        {
            GuideManager.GetInstance().ShowGuideWithIndex(id);
        }

        // 引导战斗按钮
        public void InitGuideFight()
        {
            if (GuideManager.GetInstance().isGuideUser && GuideManager.GetInstance().IsContentGuideID(100301) == false)
            {
                GuideManager.GetInstance().ShowGuideWithIndex(100301);
            }
        }


        /// <summary>
        /// 新手引导招募相关
        /// </summary>
        /// <param name="type"></param>
        public void ShowGuideRecruit()
        {
            if (GuideManager.GetInstance().IsContentGuideID(100201) == false)
                GuideManager.GetInstance().ShowGuideWithIndex(100201);
        }

        /// <summary>
        /// 新手引导跳转
        /// </summary>
        private void JudegIsShowNewGuide()
        {
            if (ObjectSelf.GetInstance().m_NewGuidePath != string.Empty)
            {
                UI_HomeControler.Inst.AddUI(ObjectSelf.GetInstance().m_NewGuidePath);
                ObjectSelf.GetInstance().m_NewGuidePath = string.Empty;
            }

            if (ObjectSelf.GetInstance().isGotoRuneUI)//通关1-2  跳转到符文装配界面
            {
                FailButtonClick(1);
                ObjectSelf.GetInstance().isGotoRuneUI = false;
            }
            if (ObjectSelf.GetInstance().m_isOpenZhiXian)//通关1-4  跳转到支线界面
            {
                GuideManager.GetInstance().ShowGuideWithIndex(200202);

            }
            if (ObjectSelf.GetInstance().m_isOpenPerfectReward)//通关1-6  跳转到选择关卡界面 小手指向礼品箱
            {
                GuideManager.GetInstance().ShowGuideWithIndex(200402);
            }

            if (ObjectSelf.GetInstance().isGotoRelicCowry)//通关1-5 跳转到遗迹宝藏
            {
                UI_HomeControler.Inst.AddUI(UI_Recruit.UI_ResPath);
                UI_Recruit.inst.OpenRelicBtn();
                ObjectSelf.GetInstance().isGotoRelicCowry = false;
            }
        }
        void InitFunly()
        {
            tipsImdex.Add(68);
            tipsImdex.Add(114);
            tipsImdex.Add(101);
            tipsImdex.Add(97);
            tipsImdex.Add(109);
            tipsImdex.Add(101);
            tipsImdex.Add(97);
            tipsImdex.Add(114);
            tipsImdex.Add(32);
            tipsImdex.Add(71);
            tipsImdex.Add(97);
            tipsImdex.Add(109);
            tipsImdex.Add(101);
        }

    }

    
}
