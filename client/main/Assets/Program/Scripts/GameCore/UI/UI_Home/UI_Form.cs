using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using DreamFaction.GameCore;
using UnityEngine.Events;
using DreamFaction.Utils;
using GNET;
using System.Text;
namespace DreamFaction.UI
{
    /// <summary>
    /// 阵型界面，继承自BaseUI
    /// </summary>d
    public class UI_Form : BaseUI
    {
        public static string UI_ResPath = "UI_Home/UI_Form_2_2";
        private static UI_Form _inst;

        private enum MsgBoxType
        {
            BeginBattle,
            RapidClear
        }

        //公共变量
        public List<UI_HeroItem> heroList = new List<UI_HeroItem>();
        private List<ObjectCard> templist = null;
        public GameObject NotHeroObj = null;                                                        //没有可上阵英雄时显示      
        public GameObject HeroObj = null;                                                           //正常有上线英雄
        public Animator HeroBagAnim = null;                                                         //英雄背包Anim
        public Text HeroBagTilteTxt = null;                                                         //显示标题  是上阵英雄还是换英雄
        private Button BackBtn = null;                                                              //返回按钮
        private Button PackBtn = null;                                                              //收起按钮
        private Button m_CloseBtn = null;                                                           //无上阵英雄关闭按钮
        private List<UI_HeroItem> HeroItemList = null;                                              //当前卡牌组
        private List<UI_ClickHero> HeroClickList1;                                                  //阵型1
        private List<UI_ClickHero> HeroClickList2;                                                  //阵型2
        private List<UI_ClickHero> CurrentClickHeroList;                                            //当前用阵型
        private Transform HeroListLayOut = null;                                                    //英雄卡牌父节点   
        private LoopLayout m_LoopLayout = null;
        private GameObject HeroClickTeam1;                                                          //阵型按钮1组
        private GameObject HeroClickTeam2;                                                          //阵型按钮2组
        private GameObject ChangeFormation;                                                         //阵型类型编辑
        private Text TeamSortType = null;                                                           //手动排序类型显示
        private Text curCount = null;                                                               //当前英雄个数
        private Text maxCount = null;                                                               //最大英雄个数
        private Button m_ClearTeamBtn = null;                                                       //清除编队按钮
        private Button m_TeamBtn1 = null;                                                           //编队按钮1
        private Button m_TeamBtn2 = null;                                                           //编队按钮2
        private Button m_TeamBtn3 = null;                                                           //编队按钮3
        private GameObject m_Team1SelectObj = null;
        private GameObject m_Team1NotSelectObj = null;
        private GameObject m_Team2SelectObj = null;
        private GameObject m_Team2NotSelectObj = null;
        private GameObject m_Team3SelectObj = null;
        private GameObject m_Team3NotSelectObj = null;
        private UI_SlideBtn SortSlide = null;                                                       //滑动组件
        private bool isStatr = false;                                                               //英雄列表是否滑出屏幕完成
        private int HandSortType;                                                                   //1为品质排序2为等级排序
        private int m_MaxCount;                                                                     //当前可容纳的英雄个数

        //准备战斗变量
        private Button FightBtn = null;                                                             //战斗开始按钮
        private Button SetTeamBtn = null;                                                           //变换阵型按钮
        //private Button Continue_Btn = null;                                                         //继续战斗按钮
        private Image MopUpBtnImage = null;                                                         //扫荡按钮图标
        private Button MopUpBtn = null;                                                             //扫荡按钮
        private Text MopUpBtnName = null;                                                           //扫荡按钮名称
        private Text ClearCountText = null;                                                         //剩余扫荡次数显示字段
        private Text StrengthValue = null;                                                          //关卡消耗
        private Text SurplusChallengeCount = null;                                                  //关卡剩余挑战次数
        private Transform MonsterListLayOut = null;                                                 //怪物卡牌父节点   
        private Transform MsgBoxGroup = null;                                                       //消息父节点           
        private GameObject MonsterList = null;                                                      //怪物卡牌隐藏父节点
        private GameObject SurplusFightObj = null;                                                  //剩余挑战次数OBJ
        private GameObject m_Dir_up;                                                                //向上image
        private GameObject m_Dir_bottom;                                                            //向下image
        private Button m_QualitysortBtn = null;                                                     //品质按钮
        private Button m_LevelsortBtn = null;                                                       //等级按钮
        
        private MsgBoxType messageSource;
        private int SurplusFightCount;                                                       //剩余挑战次数
        public bool isClick = false;                                                         //是否点击英雄模型
        public UI_HeroItem CurrentHeroItem;                                                  //当前选中英雄卡牌 
        private X_GUID guid;                                                                 //当前选择英雄的GUID
        private ObjectCard selectModel;                                                      //当前选择的英雄模型的卡牌信息
        private int selectModelPos;                                                          //当前选择模型所在队伍的位置编号
        private int AttackPos;                                                               //记录当前点击的英雄是前排还是后排 前排是0后排是2
        private int CurPos;                                                                  //记录当前英雄站在前排还是后排
        private List<long> BackHeroGuids = new List<long>();                                 //记录当前站在后排的英雄GUid

        bool mInitFightLayerDone = false;
        GameObject fightLayerObj = null;

        private Transform m_CaptionLayout;
        public static UI_Form GetInst()
        {
            return _inst;
        }
        //冒泡消息父窗体
        public Transform GetMsgBoxGroup() { return MsgBoxGroup; }
        //设置当前选择英雄的GUID 
        public void SetGuid(X_GUID id) { guid = id; }
        //获取当前选择英雄的GUID 
        public X_GUID GetGuid() { return guid; }
        
        //设置当前选择英雄的卡牌信息
        public void SetSelectModel(ObjectCard card) { selectModel = card; }
        
        //获取当前选择英雄的卡牌信息
        public ObjectCard GetSelectModel() { return selectModel; }
        
        //设置选中英雄在队伍中的编号
        public void SetSelectModelPos(int pos) { selectModelPos = pos; }
        
        //获取选中英雄在队伍中的编号
        public int GetSelectModelPos() { return selectModelPos; }

        // 设置当前选中的卡牌
        public void SetCurrentHeroItem(UI_HeroItem item) { CurrentHeroItem = item; }

        // 返回当前选中的卡牌
        public UI_HeroItem GetCurrentHeroItem() { return CurrentHeroItem; }
        
        //记录当前选中的攻击位置
        public void SetAttackPos(int attackPos) { AttackPos = attackPos; }
        
        //获取当前选择的攻击位置
        public int GetAttackPos() { return AttackPos; }

        //设置当前英雄在队伍中的位置
        public void SetCurPos(int curPos) { CurPos = curPos; }

        //获取当前英雄在队伍中的位置
        public int GetCurPos() { return CurPos; }

        //设置当前站在后排的英雄的GUID 
        public void SetBackHeroGuids(long guid) { BackHeroGuids.Add(guid); }

        //获得当前站在后排的英雄GUID
        public List<long> GetBackHeroGuids() { return BackHeroGuids; }

        public override void InitUIData()
        {
            base.InitUIData();
            _inst = this;
            RenderSettings.fog = false;
            UI_MainHome.m_CamForm.SetActive(true);
            templist = ObjectSelf.GetInstance().HeroContainerBag.GetHeroList();

            HeroItemList = new List<UI_HeroItem>();
            HeroClickList1 = new List<UI_ClickHero>();
            HeroClickList2 = new List<UI_ClickHero>();
            m_CaptionLayout = selfTransform.FindChild("pos");
//             HeroClickTeam1 = selfTransform.FindChild("Team1Buttons").gameObject;
//             HeroClickTeam2 = selfTransform.FindChild("Team2Buttons").gameObject;
            ChangeFormation = selfTransform.FindChild("ChangeFormationBG").gameObject;
            MsgBoxGroup = selfTransform.FindChild("MsgBoxGroup");

            HeroBagAnim = selfTransform.FindChild("HeroBag").GetComponent<Animator>();
            HeroBagTilteTxt = selfTransform.FindChild("HeroBag/HeroObj/Text").GetComponent<Text>();
            NotHeroObj = selfTransform.FindChild("HeroBag/NotHeroObj").gameObject;
            HeroObj = selfTransform.FindChild("HeroBag/HeroObj").gameObject;
            NotHeroObj.SetActive(false);

            BackBtn = selfTransform.FindChild("TopPanel/BackBtn").GetComponent<Button>();
            BackBtn.onClick.AddListener(new UnityAction(onBackCall));
            SetTeamBtn = selfTransform.FindChild("SetTeamBtn").GetComponent<Button>();
            //SetTeamBtn.onClick.AddListener(new UnityAction(OnSetFormation));
            m_ClearTeamBtn = selfTransform.FindChild("ClearTeamBtn").GetComponent<Button>();
//             m_ClearTeamBtn.onClick.AddListener(new UnityAction(OnClearTeamHintInfo));

//             for (int i = 0; i < 5; ++i)
//             {
//                 HeroClickList1.Add(HeroClickTeam1.transform.GetChild(i).GetComponent<UI_ClickHero>());
//                 HeroClickList2.Add(HeroClickTeam2.transform.GetChild(i).GetComponent<UI_ClickHero>());
//             }
            SetCurrentClickHeroList(ObjectSelf.GetInstance().Teams.GetFormationType());
      
            m_TeamBtn1 = selfTransform.FindChild("Team1Btn").GetComponent<Button>();
            m_Team1SelectObj = m_TeamBtn1.transform.FindChild("SelectStateImg").gameObject;
            m_Team1NotSelectObj = m_TeamBtn1.transform.FindChild("NotSelectStateImg").gameObject;
/*            /m_TeamBtn1.onClick.AddListener(new UnityAction(OnTeam1Btn));*/
            m_TeamBtn2 = selfTransform.FindChild("Team2Btn").GetComponent<Button>();
            m_Team2SelectObj = m_TeamBtn2.transform.FindChild("SelectStateImg").gameObject;
            m_Team2NotSelectObj = m_TeamBtn2.transform.FindChild("NotSelectStateImg").gameObject;
/*            m_TeamBtn2.onClick.AddListener(new UnityAction(OnTeam2Btn));*/
            m_TeamBtn3 = selfTransform.FindChild("Team3Btn").GetComponent<Button>();
            m_Team3SelectObj = m_TeamBtn3.transform.FindChild("SelectStateImg").gameObject;
            m_Team3NotSelectObj = m_TeamBtn3.transform.FindChild("NotSelectStateImg").gameObject;
/*            m_TeamBtn3.onClick.AddListener(new UnityAction(OnTeam3Btn));*/

            HeroListLayOut = selfTransform.FindChild("HeroBag/HeroObj/HeroList/ListLayOut");
            m_LoopLayout = HeroListLayOut.GetComponent<LoopLayout>();

            curCount = selfTransform.FindChild("HeroBag/HeroObj/SortObj/NumberNoBg/CurPlayers_txt").GetComponent<Text>();
            maxCount = selfTransform.FindChild("HeroBag/HeroObj/SortObj/NumberNoBg/MaxPlayers_txt").GetComponent<Text>();
            PackBtn = selfTransform.FindChild("HeroBag/HeroObj/PackBtn").GetComponent<Button>();
            PackBtn.onClick.AddListener(new UnityAction(OnClickPackBtn));
            m_CloseBtn = selfTransform.FindChild("HeroBag/NotHeroObj/CloseBtn").GetComponent<Button>();
            m_CloseBtn.onClick.AddListener(new UnityAction(OnClickPackBtn));

            SortSlide = selfTransform.FindChild("HeroBag/HeroObj/SortObj/MainBagBtn").GetComponent<UI_SlideBtn>();
            m_Dir_up = SortSlide.transform.FindChild("Image(up)").gameObject;
            m_Dir_bottom = SortSlide.transform.FindChild("Image(bottom)").gameObject;
            m_QualitysortBtn = selfTransform.FindChild("HeroBag/HeroObj/SortObj/QualitysortBtn").GetComponent<Button>();
            m_QualitysortBtn.onClick.AddListener(new UnityAction(OnQualitysortBtn));//品质排序
            m_QualitysortBtn.transform.FindChild("Text").GetComponent<Text>().text = GameUtils.getString("hero_info_sort_quality");
            m_LevelsortBtn = selfTransform.FindChild("HeroBag/HeroObj/SortObj/LevelsortBtn").GetComponent<Button>();
            m_LevelsortBtn.onClick.AddListener(new UnityAction(OnLevelsortBtn));
            m_LevelsortBtn.transform.FindChild("Text").GetComponent<Text>().text = GameUtils.getString("hero_info_sort_level");
            TeamSortType = SortSlide.transform.FindChild("Text").GetComponent<Text>();//当前英雄背包的排序显示
            HandSortType = 1;//默认排序为  品质排序
            SetHandSortType(HandSortType);
            HomeControler.Inst.PushFunly(2, 101);


            if (UI_ResPath != "UI_Home/UI_Form_2_2") 
            {
                InitSurplusFightCount();
                FightBtn = selfTransform.FindChild("MonsterBag/FightBtn").GetComponent<Button>();
                FightBtn.onClick.AddListener(new UnityAction(OnClickFightBegin));
                MopUpBtnImage = selfTransform.FindChild("MonsterBag/MopUpBtn_Image").GetComponent<Image>();
                MopUpBtn = selfTransform.FindChild("MonsterBag/MopUpBtn").GetComponent<Button>();
                MopUpBtnName = selfTransform.FindChild("MonsterBag/MopUpBtn_Image/ButtonNameText").GetComponent<Text>();
                ClearCountText = selfTransform.FindChild("MonsterBag/MopUpBtn_Image/CountText").GetComponent<Text>();
                MonsterList = selfTransform.FindChild("MonsterBag/MonsterList").gameObject;
                StrengthValue = selfTransform.FindChild("MonsterBag/FightBtn/StrengthValue").GetComponent<Text>();
                //StageTemplate stageinfo = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(ObjectSelf.GetInstance().CurStageID);
                StageTemplate stageinfo = StageModule.GetStageTemplateById(ObjectSelf.GetInstance().CurStageID);
                //if (list.ContainsKey(ObjectSelf.GetInstance().CurStageID))
                {
                    //StageTemplate stageinfo = (StageTemplate)list[ObjectSelf.GetInstance().CurStageID];
                    StrengthValue.text = stageinfo.m_cost.ToString();
                }
                MonsterListLayOut = MonsterList.transform.FindChild("ListLayOut");

            }
            if (MopUpBtnImage != null)
            {
                if (ObjectSelf.GetInstance().GetIsPrompt() || StageModule.GetStageType(ObjectSelf.GetInstance().CurStageID) == EM_STAGE_TYPE.SPEC_QUEST)
                {
                    MopUpBtnImage.gameObject.SetActive(false);
                }
                else
                {
                    //扫荡功能未做
                    MopUpBtnImage.gameObject.SetActive(true);
                }
            }
            if (MopUpBtn != null)
            {
                //MopUpBtn.onClick.AddListener(OnClickMopUpBtn);
                MopUpBtn.enabled = !(ObjectSelf.GetInstance().GetIsPrompt() || StageModule.GetStageType(ObjectSelf.GetInstance().CurStageID) == EM_STAGE_TYPE.SPEC_QUEST);
            }
            ResetMopUpBtnInfo();
            //templist.Sort(SortHeroHandler);

            UI_CaptionManager cap = UI_CaptionManager.GetInstance();
            if (cap != null)
                cap.AwakeUp(m_CaptionLayout);

            GameEventDispatcher.Inst.addEventListener(GameEventID.G_Formation_Update, OnG_Formation_Update);
            GameEventDispatcher.Inst.addEventListener(GameEventID.U_RapidClearRespond, RapidClearRespondHandler);
            GameEventDispatcher.Inst.addEventListener(GameEventID.G_FightNumSucceed, InitSurplusFightCount);
            GameEventDispatcher.Inst.addEventListener(GameEventID.UI_StageSweepDataChange, OnSweepDataChange);

            SortSlide.openFinish += SortSlide_openFinish;
            SortSlide.closeFinish += SortSlide_closeFinish;
           
        }

        void SortSlide_closeFinish()
        {
            m_Dir_bottom.SetActive(true);
            m_Dir_up.SetActive(false);
        }

        void SortSlide_openFinish()
        {
            m_Dir_bottom.SetActive(false);
            m_Dir_up.SetActive(true);
        }

        public override void InitUIView()
        {
            base.InitUIView();
            HomeControler.Inst.UpdateFormationGameObject();
            InitMonsterData();
            InitClickHeroData();
            isFight();
            InitHeroBagCount();
            RefreshStateInfo();

            // 新手引导
            if (GuideManager.GetInstance().isGuideUser 
                && GuideManager.GetInstance().IsContentGuideID(100303) == false 
                && GuideManager.GetInstance().GetBackCount(100302) 
                && GuideManager.GetInstance().interruptID < 100305)
            {
                GuideManager.GetInstance().ShowGuideWithIndex(100303);
                return;
            }

            if (GuideManager.GetInstance().isGuideUser 
                && GuideManager.GetInstance().IsContentGuideID(100402) == false 
                && GuideManager.GetInstance().GetBackCount(100401))
            {
                GuideManager.GetInstance().ShowGuideWithIndex(100402);
                return;
            }

            // 判断中断 ID ，直接引导战斗
            int interruptID = GuideManager.GetInstance().interruptID;
            if (interruptID >= 100305 
                && interruptID <= 100313 
                && GuideManager.GetInstance().GetLastID() <= 100313)
            {
                for (int i = 100304; i < 100313; ++i)
                    GuideManager.GetInstance().RemoveGuideID(i);

                GuideManager.GetInstance().ShowGuideWithIndex(300103);
            }
        }

        //初始化英雄背包上线
        private void InitHeroBagCount()
        {
            int Herocount = ObjectSelf.GetInstance().HeroContainerBag.GetHeroList().Count;
            m_MaxCount = ObjectSelf.GetInstance().HeroContainerBag.GetHeroBagSizeMax();

            curCount.text = Herocount.ToString();
            StringBuilder _str = new StringBuilder();
            _str.Append("/");
            _str.Append(m_MaxCount);
            maxCount.text = _str.ToString();

        }

        //英雄背包更新
        private void OnKE_HeroBagItemSizeShow()
        {
            //OnReturnHerobagButton();
            int Herocount = ObjectSelf.GetInstance().HeroContainerBag.GetHeroList().Count;
            m_MaxCount = ObjectSelf.GetInstance().HeroContainerBag.GetHeroBagSizeMax();

            curCount.text = Herocount.ToString();
            StringBuilder _str = new StringBuilder();
            _str.Append("/");
            _str.Append(m_MaxCount);
            maxCount.text = _str.ToString();

            string msgtext = string.Empty;
            if (ObjectSelf.GetInstance().HeroBuyCount >= DataTemplate.GetInstance().m_GameConfig.getHero_packset_max_purchase())
            {
                msgtext = GameUtils.getString("hero_info_expand_tip2");
                InterfaceControler.GetInst().AddMsgBox(msgtext, transform.parent);
                //AddHeroObj.SetActive(false);
            }
            else
            {
                msgtext = GameUtils.getString("hero_info_expand_tip1") + m_MaxCount.ToString();
                InterfaceControler.GetInst().AddMsgBox(msgtext, transform.parent);
            }

        }

        /// <summary>
        /// 设置当前选择的阵型
        /// </summary>
        /// <param name="type">阵型编号</param>
        public void SetCurrentClickHeroList(int type)
        {
//             if (type == 1)
//             {
//                 HeroClickTeam2.SetActive(false);
//                 HeroClickTeam1.SetActive(true);
//                 CurrentClickHeroList = HeroClickList1;
//             }
//             else
//             {
//                 HeroClickTeam1.SetActive(false);
//                 HeroClickTeam2.SetActive(true);
//                 CurrentClickHeroList = HeroClickList2;
//             }
        }


        // 1：准备播放进场动画
        public override void OnPlayingEnterAnimation()
        {
            //transform.localScale = new Vector3(0, 0, 0);
        }

        // 2: 准备删除UI
        public override void OnReadyForClose()
        {
            UI_HomeControler.Inst.ReMoveUI(gameObject);
        }


        // 3: 更新UI显示
        public override void UpdateUIView()
        {
            if (UIState == UIStateEnum.PlayingEnterAnimation)
            {
            }
            else if (UIState == UIStateEnum.PlayingExitAnimation)
            {
                //transform.position += new Vector3(0.1f, 0.00f, 0.00f);
                //if (transform.position.x > -20)
                //{
                //    UIState = UIStateEnum.PlayingExitAnimationOver;
                //}
            }
            //是否播放完成
            IsStart();

        }

        /// <summary>
        /// 设置英雄背包排序
        /// </summary>
        /// <param name="type">排序方式编号</param>
        private void SetHandSortType(int type)
        {
            HandSortType = type;
            string _text = "";
            if (HandSortType == 1)
            {
                _text = GameUtils.getString("hero_info_sort_quality");
            }
            else
            {
                _text = GameUtils.getString("hero_info_sort_level");
            }
            TeamSortType.text = _text;
        }

        /// <summary>
        /// 动态更新列表显示
        /// </summary>
        /// <param name="index"></param>
        /// <param name="cell"></param>
        private void UpdateLoadHeroItem(int index, RectTransform cell)
        {
            ObjectCard objHero = templist[index];
            UI_HeroItem uiIt = cell.gameObject.GetComponent<UI_HeroItem>();
            if (uiIt == null)
            {
                cell.gameObject.AddComponent<Button>();
                uiIt = cell.gameObject.AddComponent<UI_HeroItem>();
                heroList.Add(uiIt);
            }
            uiIt.index = index;
            uiIt.InitUIFormation(objHero);

        }

        //初始化已经拥有英雄卡片
        public void InitHeroData()
        {
            templist.Sort(SortHeroHandler);
            m_LoopLayout.cellCount = templist.Count;
            if (templist.Count < 9)
            {
                m_LoopLayout.emptyCellCount = 9 - templist.Count;
            }
            else
            {
                if (templist.Count % m_LoopLayout.columns != 0)
                {
                    m_LoopLayout.emptyCellCount = m_LoopLayout.columns - templist.Count % m_LoopLayout.columns;
                }
                else
                {
                    m_LoopLayout.emptyCellCount = 0;
                }
            }
            m_LoopLayout.updateCellEvent = UpdateLoadHeroItem;
            m_LoopLayout.Reload();

            //UpdateHeroItemData();
        }

        private int SortHeroHandler(ObjectCard leftCard, ObjectCard rightCard)
        {
            bool _accordingToLv = HandSortType == 2;
            int key = GetHeroCardSortKey(rightCard, _accordingToLv) - GetHeroCardSortKey(leftCard, _accordingToLv);
            if (key == 0)
            {
                key = leftCard.GetHeroRow().getId() - rightCard.GetHeroRow().getId();
            }
            return key;
        }
        private int GetHeroCardSortKey(ObjectCard hero, bool accordingToLv = false)
        {
            int _weightFactorTeam = 1000000;
            int _weightFactoQuality;
            int _weightFactorLevel;
            if (accordingToLv)
            {
                _weightFactoQuality = 1;
                _weightFactorLevel = 100;
            }
            else
            {
                _weightFactoQuality = 1000;
                _weightFactorLevel = 1;
            }

            var _objSelf = ObjectSelf.GetInstance();

            int _sortKey = 0;

            bool[] _teamArray = _objSelf.Teams.GetHeroInTeamList(hero.GetGuid());

            for (int i = 0; i < _teamArray.Length; i++)
            {
                if (_teamArray[i])
                {
                    _sortKey += (_teamArray.Length - i) * 2 - 1;
                }
            }
            _sortKey *= _weightFactorTeam;

            _sortKey += _weightFactoQuality * (hero.GetHeroRow().getQuality());
            _sortKey += _weightFactorLevel * (hero.GetHeroData().Level);

            return _sortKey;
        }
        //刷新卡牌信息
        private void UpdateHeroItemData()
        {
            List<ObjectCard> templist = ObjectSelf.GetInstance().HeroContainerBag.GetHeroList();
            templist.Sort(SortHeroHandler);
            m_LoopLayout.UpdateCell();
            int count = HeroItemList.Count;
            for (int i = 0; i < count; ++i)
            {
                HeroItemList[i].UpdateHeroCardData(templist[i]);
            }
        }

        public bool isFight()
        {
            int GroupCount = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
            int HeroCount = ObjectSelf.GetInstance().Teams.m_Matrix.GetLength(1);
            for (int i = 0; i < HeroCount; ++i)
            {
                ObjectCard temp = ObjectSelf.GetInstance().HeroContainerBag.FindHero(ObjectSelf.GetInstance().Teams.m_Matrix[GroupCount, i]);
                if (temp == null)
                    continue;
                if (FightBtn != null)
                {
                    GameUtils.SetBtnSpriteGrayState(FightBtn, false);
                    return true;
                }
            }
            if (FightBtn != null)
            {
                GameUtils.SetBtnSpriteGrayState(FightBtn, true);
            }
            return false;
        }
        public void SetHeroListLayOutActive(bool isActive)
        {
            HeroListLayOut.gameObject.SetActive(isActive);
        }
        //初始化按钮信息
        private void InitClickHeroData()
        {
            BackHeroGuids.Clear();

            int GroupCount = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
            int HeroCount = ObjectSelf.GetInstance().Teams.m_Matrix.GetLength(1);
            for (int i = 0; i < HeroCount; ++i)
            {
                ObjectCard temp = ObjectSelf.GetInstance().HeroContainerBag.FindHero(ObjectSelf.GetInstance().Teams.m_Matrix[GroupCount, i]);
                if (temp == null)
                    continue;
//               CurrentClickHeroList[i].InitData(temp);
            }

            //编队按钮显示信息
            switch (ObjectSelf.GetInstance().Teams.GetDefaultGroup())
            {
                case 0:
                    OnTeam1Btn();
                    break;
                case 1:
                    OnTeam2Btn();
                    break;
                case 2:
                    OnTeam3Btn();
                    break;
            }
        }

        //品质排序
        private void OnQualitysortBtn()
        {
            SortSlide.OnClose();
            if (HandSortType == 1)
                return;
            SetHandSortType(1);
            UpdateHeroItemData();
        }
        //等级排序
        private void OnLevelsortBtn()
        {
            SortSlide.OnClose();
            if (HandSortType == 2)
                return;
            SetHandSortType(2);
            UpdateHeroItemData();
        }

        //编队1
        private void OnTeam1Btn()
        {
            OnChangeTeam(0, m_Team1SelectObj, m_Team1NotSelectObj);
        }
        //编队2
        private void OnTeam2Btn()
        {
            OnChangeTeam(1, m_Team2SelectObj, m_Team2NotSelectObj);
        }
        //编队3
        private void OnTeam3Btn()
        {
            OnChangeTeam(2, m_Team3SelectObj, m_Team3NotSelectObj);
        }

        /// <summary>
        /// 切换编队
        /// </summary>
        /// <param name="TeamNum">编队编号</param>
        private void OnChangeTeam(int TeamNum,GameObject selectObj,GameObject selectObjNot)
        {
            m_Team1SelectObj.SetActive(false);
            m_Team2SelectObj.SetActive(false);
            m_Team3SelectObj.SetActive(false);
            m_Team1NotSelectObj.SetActive(true);
            m_Team2NotSelectObj.SetActive(true);
            m_Team3NotSelectObj.SetActive(true);

            selectObj.SetActive(true);
            selectObjNot.SetActive(false);

            if (ObjectSelf.GetInstance().Teams.GetDefaultGroup() == TeamNum)
                return;

            ObjectSelf.GetInstance().Teams.SetDefaultGroup((byte)(TeamNum));
            //OnG_Formation_Update();
            OnClickPackBtn();
            int type = ObjectSelf.GetInstance().Teams.GetFormationType();
            int count = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
            //SendProtocol_ChangeFormation(type, count);
        }

        /// <summary>
        /// 更新阵型
        /// </summary>
        private void OnG_Formation_Update()
        {
            SetCurrentClickHeroList(ObjectSelf.GetInstance().Teams.GetFormationType());
            HomeControler.Inst.UpdateFormationGameObject();
//             for (int i = 0; i < CurrentClickHeroList.Count; ++i)
//             {
//                 CurrentClickHeroList[i].OnClearEff();
//                 CurrentClickHeroList[i].OnClearObj();
//             }
            InitClickHeroData();
            isFight();
            //UpdateHeroItemData();
        }

        /// <summary>
        /// 设置阵型类型
        /// </summary>
        private void OnSetFormation()
        {
            OnClickPackBtn();
            ChangeFormation.SetActive(true);
            SetHeroListLayOutActive(false);
            int type = ObjectSelf.GetInstance().Teams.GetFormationType();
            if (type == 1)
            {
                UI_ChangeFam.GetInst().SetAttackFormation();
            }
            else
            {
                UI_ChangeFam.GetInst().SetCrucialFormation();
            }
        }

        /// <summary>
        /// 提示是否清空队伍
        /// </summary>
        private void OnClearTeamHintInfo()
        {
            string text = GameUtils.getString("embattle_window1");
            UI_RechargeBox box = UI_HomeControler.Inst.AddUI(UI_RechargeBox.UI_ResPath).GetComponent<UI_RechargeBox>();
            box.SetIsNeedDescription(false);
            box.SetDescription_text(text);
            box.SetLeftBtn_text(GameUtils.getString("common_button_ok"));
            box.SetLeftClick(OnClearTeam);

        }

        //清除模型脚下的光圈特效
        public void ClearCircle()
        {
            for (int i = 0; i < GameObject.Find("TeamViewRoom").transform.FindChild("Team1").childCount; i++)
            {
                GameObject eff = GameObject.Find("TeamViewRoom").transform.FindChild("Team1").GetChild(i).gameObject;
                eff.SetActive(false);
            }
            for (int i = 0; i < GameObject.Find("TeamViewRoom").transform.FindChild("Team2").childCount; i++)
            {
                GameObject eff = GameObject.Find("TeamViewRoom").transform.FindChild("Team2").GetChild(i).gameObject;
                eff.SetActive(false);
            }
        }
        //清除标志箭头
        public void ClearPointImg()
        {
//             for (int i = 0; i < HeroClickTeam1.transform.childCount; i++)
//             {
//                 HeroClickTeam1.transform.GetChild(i).FindChild("Point").gameObject.SetActive(false);
//             }
//             for (int i = 0; i < HeroClickTeam2.transform.childCount; i++)
//             {
//                 HeroClickTeam2.transform.GetChild(i).FindChild("Point").gameObject.SetActive(false);
//             }

        }

        //收起英雄列表
        public void OnClickPackBtn()
        {
            HeroBagAnim.SetBool("Enter_HeroBag", false);
            isStatr = true;
            //StartCoroutine(WaitForAnimationPlayOver());

            ClearCircle();
            ClearPointImg();
        }

        /// <summary>
        /// 是否收起列表完成
        /// </summary>
        private void IsStart()
        {
            if (isStatr)
            {
                if (HeroBagAnim.GetCurrentAnimatorStateInfo(0).IsName("Exit_HeroBag"))
                {
                    CloseMonsterOrEdit(true);
                    isStatr = false;
                }
            }
        }

        /// <summary>
        /// 设置提示英雄编辑或者怪物列表隐藏还是显示
        /// </summary>
        /// <param name="isActive"></param>
        public void CloseMonsterOrEdit(bool isActive)
        {
            if (UI_ResPath == "UI_Home/UI_Form_2_2")
            {
                selfTransform.FindChild("EditClickObj").gameObject.SetActive(isActive);
            }
            else
            {
                selfTransform.FindChild("MonsterBag").gameObject.SetActive(isActive);
            }
        }

#region 消息相关函数
        /// <summary>
        /// 清空队形
        /// </summary>
        private void OnClearTeam()
        {
            OnClickPackBtn();
            CCleanTroop battle = new CCleanTroop();
            battle.troopid = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
            IOControler.GetInstance().SendProtocol(battle);
            UI_HomeControler.Inst.ReMoveUI(UI_RechargeBox.UI_ResPath);
        }
        /// <summary>
        /// 上阵或者换人
        /// </summary>
        /// <param name="formtype">阵型类型</param>
        /// <param name="troopid">阵型编号</param>
//         public void SendProtocol(int troopid, int FormationNum)
//         {
//             CAddTroop battle = new CAddTroop();
//             battle.trooptype = ObjectSelf.GetInstance().Teams.GetFormationType();
//             battle.herokey = (int)CurrentHeroItem.GetHeroObject().GetGuid().GUID_value;
//             battle.troopid = troopid;
//             battle.locationid = FormationNum;
//             IOControler.GetInstance().SendProtocol(battle);
//         }
//         //发送下阵消息
//         public void SendDownProtocol(int troopid, int FormationNum)
//         {
//             CAddTroop battle = new CAddTroop();
//             battle.trooptype = ObjectSelf.GetInstance().Teams.GetFormationType();
//             battle.herokey = 0;
//             battle.troopid = troopid;
//             battle.locationid = FormationNum;
//             IOControler.GetInstance().SendProtocol(battle);
//         }
//         //发送切换阵型消息
//         public void SendProtocol_ChangeFormation(int formtype, int troopid)
//         {
//             CAddTroop battle = new CAddTroop();
//             battle.trooptype = formtype;
//             battle.herokey = 0;
//             battle.troopid = troopid;
//             battle.locationid = 0;
//             IOControler.GetInstance().SendProtocol(battle);
//         }
#endregion

        //返回按钮
        private void onBackCall()
        {
            RenderSettings.fog = true;
            UI_MainHome.m_CamForm.SetActive(false);
            GameEventDispatcher.Inst.clearEvent(GameEventID.G_Formation_Update);
            UI_HomeControler.Inst.ReMoveUI(UI_Form.UI_ResPath);

            HomeControler.Inst.DestroyFromModel();

            if (GuideManager.GetInstance().isGuideUser && GuideManager.GetInstance().IsContentGuideID(100501) == false && GuideManager.GetInstance().GetBackCount(100404))
            {
                UI_MainHome.GetInst().InitGuideFightBtn(100501);
            }
        }

        private void OnDestroy()
        {
            UI_CaptionManager _caption = UI_CaptionManager.GetInstance();
            if (_caption != null)
                _caption.Release(m_CaptionLayout);

            GameEventDispatcher.Inst.removeEventListener(GameEventID.KE_HeroBagItemSizeShow, OnKE_HeroBagItemSizeShow);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.G_Formation_Update, OnG_Formation_Update);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.U_RapidClearRespond, RapidClearRespondHandler);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.G_FightNumSucceed, InitSurplusFightCount);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_StageSweepDataChange, OnSweepDataChange);

            SortSlide.openFinish -= SortSlide_openFinish;
            SortSlide.closeFinish -= SortSlide_closeFinish;

            mInitFightLayerDone = false;
            fightLayerObj = null;

            _inst = null;
        }


        //*****************************************************************准备战斗（相关）****************************************************************************//

        void OnSweepDataChange()
        {
            RefreshStateInfo();
            RapidClearRespondHandler();
        }

        /// <summary>
        /// 初始化剩余挑战次数
        /// </summary>
        private void InitSurplusFightCount()
        {
            SurplusChallengeCount = selfTransform.FindChild("MonsterBag/SurplusChallengeBg/SurplusChallengeCount").GetComponent<Text>();
            SurplusFightObj = selfTransform.FindChild("MonsterBag/SurplusChallengeBg").gameObject;
            int MaxCount = 0;

            StageTemplate stageinfo = StageModule.GetStageTemplateById(ObjectSelf.GetInstance().CurStageID);
            //if (list.ContainsKey(ObjectSelf.GetInstance().CurStageID))
            {
                MaxCount = stageinfo.m_limittime;
            }

            int m_CurCampaignID = 0;
            if (ObjectSelf.GetInstance().GetIsPrompt())
            {
                m_CurCampaignID = 1001;
            }
            else
            {
                m_CurCampaignID = ObjectSelf.GetInstance().GetCurChapterID();
            }

            int m_iCurStageID = ObjectSelf.GetInstance().CurStageID;
            //StageData _StageData = ObjectSelf.GetInstance().BattleStageData.m_BattleStageList[m_CurCampaignID].GetStageData(m_iCurStageID);
            StageData _StageData = ObjectSelf.GetInstance().BattleStageData.GetStageDataByStageId(m_iCurStageID);
            if (MaxCount == -1)
            {
                SurplusFightObj.SetActive(false);
                SurplusFightCount = 99;
            }
            else
            {
                SurplusFightCount = MaxCount - _StageData.m_FightSum;
                if (SurplusFightCount <= 0)
                    SurplusFightCount = 0;
                SurplusChallengeCount.text = SurplusFightCount.ToString();
            }
        }

        /// <summary>
        /// 初始化当前关卡怪物卡牌
        /// </summary>
        private void InitMonsterData()
        {
            if (UI_ResPath == "UI_Home/UI_Form_2_2")
                return;

            StageTemplate stageinfo = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(ObjectSelf.GetInstance().CurStageID);
            //if (list.ContainsKey(ObjectSelf.GetInstance().CurStageID))
            {
                //StageTemplate stageinfo = (StageTemplate)list[ObjectSelf.GetInstance().CurStageID];
                List<MonsterTemplate> _BossTemp = new List<MonsterTemplate>();
                List<MonsterTemplate> _MonsterTemp = new List<MonsterTemplate>();
                for (int i = 0; i < stageinfo.m_displayMonster.Length; ++i)
                {
                    MonsterTemplate _monster = (MonsterTemplate)DataTemplate.GetInstance().m_MonsterTable.getTableData(stageinfo.m_displayMonster[i]);
                    if (_monster.getMonstertype() == 2)
                    {
                        _BossTemp.Add(_monster);
                    }
                    else
                    {
                        _MonsterTemp.Add(_monster);
                    }
                }

                for (int i = 0; i < _MonsterTemp.Count; i++)
                {
                    MonsterTemplate _item = _MonsterTemp[i];
                    _BossTemp.Add(_item);
                }

                for (int i = 0; i < _BossTemp.Count; ++i)
                {
                    GameObject item = Instantiate(Resources.Load("UI/Prefabs/UI_Home/UI_Monstertem")) as GameObject;
                    item.transform.SetParent(MonsterListLayOut, false);
                    item.name = "UI/Prefabs/UI_Home/UI_Monstertem";
                    UI_HeroItem uiItem = item.AddComponent<UI_HeroItem>();
                    uiItem.InitMonsterCard(_BossTemp[i]);
                }
            }
        }

        private void ResetMopUpBtnInfo()
        {
            string tempText;
            if (MopUpBtnName != null)
            {
                ChsTextTemplate temp = (ChsTextTemplate)DataTemplate.GetInstance().m_ChsTextTable.getTableData("fight_fightprepare_button3");
                if (temp.languageMap.TryGetValue(AppManager.Inst.GameLanguage, out tempText))
                {
                    MopUpBtnName.text = tempText;
                }
                else
                {
                    MopUpBtnName.text = "文字数据：fight_fightprepare_button3 丢失";
                }

            }
            if (ClearCountText != null)
            {
                ChsTextTemplate temp = (ChsTextTemplate)DataTemplate.GetInstance().m_ChsTextTable.getTableData("fight_fightprepare_content4");
                if (temp.languageMap.TryGetValue(AppManager.Inst.GameLanguage, out tempText))
                {
                    //                   int vipLevel = ObjectSelf.GetInstance().VipLevel;
                    //                   VipTemplate pRow = (VipTemplate)DataTemplate.GetInstance().m_VipTable.getTableData(vipLevel);
                    ClearCountText.text = string.Format(tempText, ObjectSelf.GetInstance().RapidClearNums);
                }
                else
                {
                    ClearCountText.text = "文字数据：fight_fightprepare_content4 丢失";
                }
            }
        }

        //点击扫荡按钮
        public void OnClickMopUpBtn()
        {
            ObjectSelf obj = ObjectSelf.GetInstance();
            if (obj == null)
                return;

            int limitVipLv = VIPModule.GetStageMopupVipLv();
            if (limitVipLv > obj.VipLevel)
            {
                InterfaceControler.GetInst().AddMsgBox("VIP" + limitVipLv + "开启该功能");
                return;
            }

            if (RefreshStateInfo(false))
            {
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("UI_raids_tips1"), GetMsgBoxGroup());
                return;
            }
            int clearTimes = 1;
            messageSource = MsgBoxType.RapidClear;
            //int vipLevel = ObjectSelf.GetInstance().VipLevel;
            //VipTemplate pRow = (VipTemplate)DataTemplate.GetInstance().m_VipTable.getTableData(vipLevel);

            if (!isEnoughRapidClearCounts(clearTimes))
            {
                if (obj.RapidClearBuyTimes > 0)
                {
                    //UI_GameTips ui = UI_HomeControler.Inst.AddUI(UI_GameTips.UI_ResPath).GetComponent<UI_GameTips>();
                    //ui.type = UI_GameTips.TipsType.NotEnoughFightCount;
                    UI_RechargeBox box = UI_HomeControler.Inst.AddUI(UI_RechargeBox.UI_ResPath).GetComponent<UI_RechargeBox>();
                    int vipLevel = ObjectSelf.GetInstance().VipLevel;
                    StringBuilder sb = new StringBuilder();
                    sb.Append(GameUtils.getString("fight_stagepurchase_form_content"));
                    sb.Append(string.Format("<size=40><color=#F7F709> {0}</color></size>", ObjectSelf.GetInstance().RapidClearBuyTimes));
                    box.SetDescription_text(sb.ToString());
                    box.SetLeftClick(OnBuyRapidClick);
                    int curGold = ObjectSelf.GetInstance().Gold;
                    box.SetMoneyInfo((int)EM_RESOURCE_TYPE.Gold, curGold);
                    box.SetMoneyInfoActive(true);
                    int buyTimes = VIPModule.GetBuyStageMopupTimes(vipLevel) - ObjectSelf.GetInstance().RapidClearBuyTimes;
                    int cost = StageModule.GetBuyRapidCost(buyTimes);
                    box.SetConNum(cost + "");
                    UI_RechargeBox.Data = curGold >= cost;
                    box.SetConsume_Image(GameUtils.GetSpriteByResourceType(EM_RESOURCE_TYPE.Gold));
                    box.SetLeftBtn_text(GameUtils.getString("common_button_purchase"));
                    return;
                }
                else
                {
                    InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("UI_raids_tips2"), GetMsgBoxGroup());
                    return;
                }
            }
            if (!isEnoughHero())
            {
                return;
            }  
            else if (!isEnoughPow())
            {
                UI_HomeControler.Inst.AddUI(UI_PowersAdd.UI_ResPath);
            }
            else if (!isEnoughCount())
            {
                UI_HomeControler.Inst.AddUI(UI_MaxFightManage.UI_ResPath);
            }
            else
            {
                ObjectSelf.GetInstance().SetOldHeroPlayer();
                CSweepBattle battle = new CSweepBattle();
                battle.battleid = ObjectSelf.GetInstance().CurStageID;
                battle.troopid = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
                IOControler.GetInstance().SendProtocol(battle);
            }
        }

        void OnBuyRapidClick()
        {
            UI_RechargeBox.Inst.OnCloes();
            //钱够不够;
            bool isEnougth = (bool)UI_RechargeBox.Data;

            if (isEnougth)
            {
                CBuyStateBattleNum csbn = new CBuyStateBattleNum();
                csbn.buytype = 1;
                IOControler.GetInstance().SendProtocol(csbn);
            }
            else
            {
                InterfaceControler.GetInst().ShowGoldNotEnougth();
            }
        }

        //返回当前关卡是否小于3星
        public bool RefreshStateInfo(bool needChangeImageColor = true)
        {
            if (MopUpBtnImage == null)
                return true;
            bool result = true;
            ObjectSelf obj = ObjectSelf.GetInstance();
            BattleStage stage = null;
            if (obj.GetIsPrompt())
            {
                stage = obj.BattleStageData.m_BattleStageList[1001];
            }
            else
            {
                stage = obj.BattleStageData.m_BattleStageList[obj.GetCurChapterID()];
            }
            StageData stageData = stage.GetStageData(obj.CurStageID);
            result = stageData == null || stageData.m_StageStar < 3;
            if (needChangeImageColor)
                GameUtils.SetImageGrayState(MopUpBtnImage, result || obj.RapidClearNums < 1);
            return result;
        }

        void InitFightendLayer()
        {
            Object prefab = UIResourceMgr.LoadPrefab("UI/Prefabs/UI_Fight/UI_FightendLayer");
            fightLayerObj = Instantiate(prefab) as GameObject;
            fightLayerObj.transform.SetParent(transform, false);

            fightLayerObj.AddComponent<UI_BattleendPanel>();
        }

        private void RapidClearRespondHandler()
        {
            ResetMopUpBtnInfo();

            if (!mInitFightLayerDone)
            {
                mInitFightLayerDone = true;
                InitFightendLayer();
            }

            UI_BattleendPanel temp = fightLayerObj.GetComponent<UI_BattleendPanel>();

            temp.enabled = true;
            temp.SetTypeToRapidClearResult(true);
            //关卡表目前是写死的;
            StageTemplate stageinfo = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(ObjectSelf.GetInstance().CurStageID);
            temp.SetSweepCostResourceType(1400010001, stageinfo.m_cost);
            temp.setType(UI_BattleendPanel.PanelType.Clear);
            temp.UpdateShow();

            RefreshStateInfo(true);
            InitSurplusFightCount();
            OnG_Formation_Update();
        }

        //战斗开始
        private void OnClickFightBegin()
        {
            messageSource = MsgBoxType.BeginBattle;
            if (!isEnoughHero())
            {
                return;
            }
            else if (!isEnoughPow())
            {
                UI_HomeControler.Inst.AddUI(UI_PowersAdd.UI_ResPath);
            }
            else if (!isEnoughCount())
            {
                UI_HomeControler.Inst.AddUI(UI_MaxFightManage.UI_ResPath);
            }
            else
            {
                UIState = UIStateEnum.PlayingExitAnimation;
                //if (UI_SelectFightArea.Inst != null)
                //{
                //    Destroy(UI_SelectFightArea.Inst.Card3Dmodel);
                //    UI_SelectFightArea.Inst.Card3Dmodel = null;
                //}
                var objSelf = ObjectSelf.GetInstance();
                if (objSelf.GetIsPrompt())
                {
                    if (objSelf.GetWeek() == objSelf.Week)
                    {
                        CBeginBattle battle = new CBeginBattle();
                        battle.battleid = ObjectSelf.GetInstance().CurStageID;
                        battle.troopid = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
                        IOControler.GetInstance().SendProtocol(battle);
                    }
                    else
                    {
                        objSelf.SetPromptFome(true);
                        objSelf.SetPromptTime(true);
                        SceneManager.Inst.StartChangeScene(SceneEntry.Home.ToString());
                    }
                }
                else
                {
                    CBeginBattle battle = new CBeginBattle();
                    battle.battleid = ObjectSelf.GetInstance().CurStageID;
                    battle.troopid = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
                    IOControler.GetInstance().SendProtocol(battle);
                }
            }
        }

        private bool isEnoughRapidClearCounts(int times = 1)
        {
            ObjectSelf obj = ObjectSelf.GetInstance();
            if (obj != null)
            {
                return obj.RapidClearNums >= times;
            }
            else
                return false;
        }

        //队伍中英雄数量判断
        private bool isEnoughHero()
        {
            int GroupCount = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
            int HeroCount = ObjectSelf.GetInstance().Teams.m_Matrix.GetLength(1);
            for (int i = 0; i < HeroCount; ++i)
            {
                ObjectCard temp = ObjectSelf.GetInstance().HeroContainerBag.FindHero(ObjectSelf.GetInstance().Teams.m_Matrix[GroupCount, i]);
                if (temp == null)
                    continue;
                if (FightBtn != null)
                {
                    GameUtils.SetBtnSpriteGrayState(FightBtn, false);
                    return true;
                }
            }
            if (FightBtn != null)
            {
                string text = GameUtils.getString("fight_fightprepare_tip1");
                InterfaceControler.GetInst().AddMsgBox(text, GetMsgBoxGroup());
                GameUtils.SetBtnSpriteGrayState(FightBtn, true);
            }
            return false;
        }

        //活力判断
        private bool isEnoughPow()
        {
            int iPower = ObjectSelf.GetInstance().ActionPoint;
            StageTemplate stageinfo = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(ObjectSelf.GetInstance().CurStageID);
            if (iPower >= stageinfo.m_cost)
            {
                return true;
            }
            return false;
        }
        //关卡剩余挑战次数判断
        private bool isEnoughCount()
        {
            return (SurplusFightCount <= 0) ? false : true;
        }

        public override void UpdateUIState()
        {   
            //置顶tip提示
            MsgBoxGroup.transform.SetAsLastSibling();
        }

        ////继续战斗
        //private void OnContinue_Btn()
        //{
        //    switch (messageSource)
        //    {
        //        case MsgBoxType.BeginBattle:
        //            UIState = UIStateEnum.PlayingExitAnimation;
        //            CBeginBattle battle = new CBeginBattle();
        //            battle.battleid = ObjectSelf.GetInstance().CurStageID;
        //            battle.troopid = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
        //            IOControler.GetInstance().SendProtocol(battle);
        //            break;
        //        case MsgBoxType.RapidClear:
        //            UIState = UIStateEnum.PlayingExitAnimation;
        //            //OnReturn_Btn();
        //            //OnCloseHeroBag();
        //            CSweepBattle temp = new CSweepBattle();
        //            temp.battleid = ObjectSelf.GetInstance().CurStageID;
        //            temp.troopid = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
        //            IOControler.GetInstance().SendProtocol(temp);
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }
}