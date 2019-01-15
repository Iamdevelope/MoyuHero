using UnityEngine;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork;
using DreamFaction.GameNetWork.Data;
using DreamFaction.GameEventSystem;
using System.Collections.Generic;
using System.Collections;
using DreamFaction.LogSystem;
using DreamFaction.Utils;
using DG.Tweening;
using GNET;

namespace DreamFaction.UI
{
    public class UI_BattleendPanel : BaseUI
    {
        public enum PanelType
        {
            Win, //胜利界面
            Fail,//失败界面
            Clear, //扫荡界面
        }

        public static string UI_Res = "UI_Fight/UI_FightendLayer_2_7";
        public static UI_BattleendPanel _inist;

        private GameObject mVictoryBG;
        private GameObject mFaileBG;
        private Button AgainBtn;
        private Button BackBtn;
        private Button SelectBtn;

        //失败界面
        private GameObject HeroJoinBtn;//英雄招募
        private GameObject PropertyStrBtn;//属性强化
        private GameObject ExUpBtn;//经验提升
        private GameObject SkillStrBtn;//技能强化
        //private GameObject ChangeRuneBtn;

        //失败界面按钮
        private Button heroJoinBtn;
        private Button propertyStrBtn;
        private Button exUpBtn;
        private Button skillStrBtn;
        private int physicalText; //失败消耗
        private Button m_Fail_Again;
        private Button m_Fail_UpLevel;
        private Button m_Fail_Ok;

        //胜利界面
        private GameObject PlayerExbar;//玩家经验
        //private GameObject Money;//金币
        //private GameObject ExFru;//经验结晶
        //private GameObject HeroFru;//生命结晶
        //private GameObject DaiDing;//待定
        private GameObject Hero;//英雄列表栏
        private GameObject Goods;//物品栏
        private Transform GoodsLayout;//物品栏父节点
        private GameObject Star;
        private GameObject Star1;
        private GameObject Star2;
        private GameObject Star3;
        private GameObject SRewardsImage;//三星奖励图片
        private GameObject MRewardsImage;//月卡奖励图片
        private GameObject SMRewardsImage;//三星和月卡奖励图片
        private Image Remaincount;
        private Text m_AddExpTxt;
        //private Text AgainDekaronCostTxt;
        //-------------10.19 版本 Wyf
        private Button m_Win_AgainBt; //再次挑战
        private Button m_Win_NextLevel; //下一关卡
        private Button m_Win_Ok;     //确定按钮
        //扫荡界面
        //private GameObject moneyRapidClear;
        //private GameObject heroFruRapidClear;
        //private GameObject backgroundRapidClear;
        //private GameObject diamond;
        int sweepCostTxt; //扫荡消耗
        private Text moneyRapidClearText;
        private Text heroFruClearText;
        //private Button selectClearBtn;
        private Button againClearBtn;
        private Button backClearBtn;
        private UniversalItemCell m_Cell;
        // 10.19日版本 wyf
        private Text m_Clear_BattleCount;
        private GameObject m_Clear_Goods;//物品栏
        private Transform m_Clear_GoodsLayout;//物品栏父节点
        private RectTransform m_Clear_GoodsRectTransform;
        private RectTransform m_Clear_GoodsLayoutRectTransform;
        private LoopLayout m_ClearGrid;  //

       // private Text playerName;
        private Text MoneyCount;//金币值
        private Text ExFruCount;//经验结晶值
        private Text HeroFruCount;//生命结晶值
        private Text DaiDingCount;//待定值
        private RectTransform GoodsRectTransform;
        private RectTransform GoodsLayoutRectTransform;
        private Slider PlayerEx;//玩家当前经验
        private Text m_PlayerExText; //玩家当前经验进度
        private Text PlayerLevel;//玩家当前等级
        public  List<GameObject> HeroItemList;//英雄卡牌OBJ
        private List<GameObject> UI_GoodItemList;
        private int Dividend = 1000000;//ID被除数
        private int HeroCounts; //通关英雄数量
        private bool isWin;
        private bool isRapidClearPanel =  false;
        private ObjectSelf obj;
        private Transform MsgBoxGroup;                                                       //消息父节点  

        private Transform m_Panel_Win;  //胜利界面
        private Transform m_Panel_Fail; //失败界面
        private Transform m_Panel_Clear; //扫荡界面
        private Transform m_Buttons_Win;  //胜利按钮组
        private Transform m_Buttons_Fail; //失败按钮组
        private Transform m_Buttons_Clear; //扫荡按钮组
        private Transform m_Effect_Fail; //战斗失败特效
        private Transform m_Effect_Win; //战斗胜利特效
        private Transform m_Effect_Win_StarsTranform; //胜利星星特效
        private Transform[] m_Effect_Win_Stars=new Transform[3]; //胜利星星特效

        private PanelType m_CurPanlType; //当前界面类型
        private int m_CurStars=-1;//当前星级
        private List<BattelInfo> clearRewards; //扫荡战斗信息
        private Scrollbar m_ClearScrollbar;
        private bool isStartScroll = false; //是否开始滑动

        private int m_NewGuideId = 0;//新手引导Id
        //private Transform m_Tween1;
        //private Transform m_Tween2;
        //public Transform m_Postion_0;
        //public Transform m_Postion_1;
        //public Transform m_Postion_2;
        int count = 1; //当前第n次扫荡次数
        public override void InitUIData()         
        {
            obj = ObjectSelf.GetInstance();
            _inist = this;
            //-------------2015.10.17第五版改动 Wyf-----------
            m_Panel_Win = transform.FindChild("Type(Win)");
            m_Panel_Fail = transform.FindChild("Type(Fail)");
            m_Panel_Clear = transform.FindChild("Type(Clear)");
            m_Buttons_Fail = transform.FindChild("Buttons(Fail)");
            m_Buttons_Win = transform.FindChild("Buttons(Win)");
            m_Buttons_Clear = transform.FindChild("Buttons(Clear)");
            //胜利界面
            m_AddExpTxt = m_Panel_Win.FindChild("PlayerExbar/AddExpTxt").GetComponent<Text>();
            PlayerEx = m_Panel_Win.FindChild("PlayerExbar").GetComponent<Slider>();
            m_PlayerExText = m_Panel_Win.FindChild("PlayerExbar/ExpTxt").GetComponent<Text>();
            PlayerLevel = m_Panel_Win.FindChild("PlayerExbar/Image/Lv").GetComponent<Text>();
            MoneyCount = m_Panel_Win.FindChild("Money/Text").GetComponent<Text>();
            Goods = m_Panel_Win.FindChild("GoodList").gameObject;
            GoodsRectTransform = Goods.GetComponent<RectTransform>();
            GoodsLayout = m_Panel_Win.FindChild("GoodList/GoodsLayout");
            GoodsLayoutRectTransform = GoodsLayout.GetComponent<RectTransform>();
            BackBtn = m_Buttons_Win.FindChild("BackBtn").GetComponent<Button>();
            BackBtn.onClick.AddListener(onGoBack);
            AgainBtn = m_Buttons_Win.FindChild("AgainBtn").GetComponent<Button>();
            AgainBtn.onClick.AddListener(onAgainCall);
            SelectBtn = m_Buttons_Win.FindChild("SelectBtn").GetComponent<Button>();
            SelectBtn.onClick.AddListener(OnSelectBtn);
            m_Effect_Win = transform.FindChild("UI_Effect_Zhandoushengli01");
            m_Buttons_Win.gameObject.SetActive(false);
            m_Effect_Win_StarsTranform = transform.FindChild("Stars");
            m_Effect_Win_StarsTranform.gameObject.SetActive(false);
            m_Effect_Win_Stars[0] = m_Effect_Win_StarsTranform.FindChild("1");
            m_Effect_Win_Stars[1] = m_Effect_Win_StarsTranform.FindChild("2");
            m_Effect_Win_Stars[2] = m_Effect_Win_StarsTranform.FindChild("3");
            for (int i = 0; i < m_Effect_Win_Stars.Length; i++)
            {
                m_Effect_Win_Stars[i].gameObject.SetActive(false);
            }
            //失败界面
            HeroJoinBtn = m_Panel_Fail.FindChild("HeroJoinBtn").gameObject;
            PropertyStrBtn = m_Panel_Fail.FindChild("PropertyStrBtn").gameObject;
            ExUpBtn = m_Panel_Fail.FindChild("ExUpBtn").gameObject;
            SkillStrBtn = m_Panel_Fail.FindChild("SkillStrBtn").gameObject;
            heroJoinBtn = HeroJoinBtn.GetComponent<Button>();
            propertyStrBtn = PropertyStrBtn.GetComponent<Button>();
            exUpBtn = ExUpBtn.GetComponent<Button>();
            skillStrBtn = SkillStrBtn.GetComponent<Button>();
            heroJoinBtn.onClick.AddListener(OnHeroJoinBtn);
            propertyStrBtn.onClick.AddListener(OnPropertyStrBtn);
            exUpBtn.onClick.AddListener(OnExUpBtn);
            skillStrBtn.onClick.AddListener(OnSkillStrBtn);
            m_Fail_Again = m_Buttons_Fail.FindChild("AgainBtn").GetComponent<Button>();
            m_Fail_Again.onClick.AddListener(onAgainCall);
            m_Fail_UpLevel = m_Buttons_Fail.FindChild("SelectBtn").GetComponent<Button>();
            m_Fail_UpLevel.onClick.AddListener(OnSelectBtn);
            m_Fail_Ok = m_Buttons_Fail.FindChild("BackBtn").GetComponent<Button>();
            m_Fail_Ok.onClick.AddListener(onGoBack);
            m_Effect_Fail = transform.FindChild("Ui_Zhandoushibai01");
            m_Effect_Fail.gameObject.SetActive(false);
            //扫荡界面
            againClearBtn = m_Buttons_Clear.FindChild("AgainClearBtn").GetComponent<Button>();
            againClearBtn.onClick.AddListener(OnAgainClearBtn);
            backClearBtn = m_Buttons_Clear.FindChild("BackClearBtn").GetComponent<Button>();
            backClearBtn.onClick.AddListener(onGoBack);
            //moneyRapidClearText = m_Panel_Clear.FindChild("Money/Text").GetComponent<Text>();
            //heroFruClearText = m_Panel_Clear.FindChild("AddExpTxt").GetComponent<Text>();
            //m_Clear_Goods = m_Panel_Clear.FindChild("GoodList").gameObject;
            //m_Clear_GoodsRectTransform = Goods.GetComponent<RectTransform>();
            //m_Clear_GoodsLayout = m_Panel_Clear.FindChild("GoodList/GoodsLayout");
            //m_Clear_GoodsLayoutRectTransform = GoodsLayout.GetComponent<RectTransform>();
            //m_Postion_0 = m_Panel_Clear.FindChild("Mask/postion_0");
            //m_Postion_1 = m_Panel_Clear.FindChild("Mask/postion_1");
            //m_Postion_2 = m_Panel_Clear.FindChild("Mask/postion_2");
            //m_Tween1 = m_Panel_Clear.FindChild("Mask/tween1");
            //m_Tween2 = m_Panel_Clear.FindChild("Mask/tween2");
            m_ClearGrid = m_Panel_Clear.FindChild("Mask/Grid").GetComponent<LoopLayout>();
            m_ClearScrollbar = m_Panel_Clear.FindChild("Scrollbar").GetComponent<Scrollbar>();

            //----------------------------------------

            MsgBoxGroup = selfTransform.FindChild("MsgBoxGroup");

            GameEventDispatcher.Inst.addEventListener(GameEventID.G_Money_Update, UpdateMoney);
            GameEventDispatcher.Inst.addEventListener(GameEventID.SE_FightWin, UpdateShow);
            GameEventDispatcher.Inst.addEventListener(GameEventID.G_Guide_Continue, ShowNewGuide);
        }

        /// <summary>
        /// 触发性新手引导
        /// </summary>
        private void NewGuide()
        {
            m_NewGuideId = GuideManager.GetInstance().CheckWhetherNeedGuide();
            if (m_NewGuideId != -1)
            {
                GuideManager.GetInstance().ShowGuideWithIndex(m_NewGuideId, GotoScene);
            }
        }

        /// <summary>
        /// 立即前往回调 跳转场景  [7/13 Lyq]
        /// </summary>
        private void GotoScene()
        {            
            if (m_NewGuideId == 200101)//符文装配引导
            {
                ObjectSelf.GetInstance().isGotoRuneUI = true;
            }
            else if (m_NewGuideId == 200201)//开启支线关卡
            {
                ObjectSelf.GetInstance().SetChangeLevel(true);
                ObjectSelf.GetInstance().m_isOpenZhiXian = true;
            }
            else if (m_NewGuideId == 200301)//开启遗迹宝藏
            {
                ObjectSelf.GetInstance().isGotoRelicCowry = true;
            }
            else if (m_NewGuideId == 200401)//关卡完美奖励领取 
            {
                ObjectSelf.GetInstance().SetChangeLevel(true);
                ObjectSelf.GetInstance().m_isOpenPerfectReward = true;
            }


            SceneManager.Inst.StartChangeScene(SceneEntry.Home.ToString());
        }

        /// <summary>
        /// 监测新手引导 点击继续
        /// </summary>
        /// <param name="e"></param>
        private void ShowNewGuide(GameEvent e)
        {
            if ((int)e.data == -1)
            {
                GuideManager.GetInstance().StopGuide();
            }
            else
            {
                GuideManager.GetInstance().ShowNextGuide();
            }
        }

        public override void InitUIView()
        {
            OnISShowSpecialStageOrMysteriousShop();
            int CurCampaignID = 0;
            if (isRapidClearPanel)
            {
                CurCampaignID = obj.CurStageID;
            }
            else
            {
                if (obj.GetIsPrompt())
                {
                    CurCampaignID = obj.GetPromptCurCampaignID();
                }
                else
                {
                    CurCampaignID = obj.GetCurCampaignID();
                }
            }


            //StageTemplate st = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(CurCampaignID);
            StageTemplate st = StageModule.GetStageTemplateById(CurCampaignID);
            //playerName.text = obj.Name;
            physicalText = st.m_cost;
            //金钱
            DropBoxData FightData = new DropBoxData();
            //MoneyCount.text = FightData.m_DropGold.ToString();
            //玩家等级，经验
            PlayerLevel.text = "Lv "+obj.GetPlayOldLevel().ToString();
            //PlayerTemplate pRow = (PlayerTemplate)DataTemplate.GetInstance().m_PlayerExpTable.getTableData(obj.Level);
            PlayerTemplate pRow = DataTemplate.GetInstance().GetPlayerTemplateById(obj.Level);
            float maxValue = (float)obj.Exp / (float)pRow.getExp();
            
           // PlayerEx.value = obj.Exp + FightData.m_HumanExp;
            //StartCoroutine(HeroExadd(1.0f));

            //再次挑战消耗值
            StageTemplate stageinfo = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(ObjectSelf.GetInstance().CurStageID);
           // AgainDekaronCostTxt.text = stageinfo.m_cost.ToString();
            //}
            if (!isRapidClearPanel)
            {
                //新手引导 支线关卡战斗胜利后 走200204 -- yao
                if (ObjectSelf.GetInstance().m_isOpenZhiXian)
                {
                    GuideManager.GetInstance().ShowGuideWithIndex(200204);
                    ObjectSelf.GetInstance().m_isOpenZhiXian = false;
                }

            }
        }
        public void UpdateShow()
        {

            //UpdateMoney();
            if (isRapidClearPanel)
            {
                for (int i = 0; i < GoodsLayout.transform.childCount; ++i)
                {
                     Destroy(GoodsLayout.GetChild(i).gameObject);
                }
            }
            AddGoddItem();
            //PlayerTemplate pRow = (PlayerTemplate)DataTemplate.GetInstance().m_PlayerExpTable.getTableData(obj.Level);
            PlayerTemplate pRow = DataTemplate.GetInstance().GetPlayerTemplateById(obj.Level);
            //StageTemplate _stage = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(obj.CurStageID);
            StageTemplate _stage = StageModule.GetStageTemplateById(obj.CurStageID);
            float maxValue = (float)obj.Exp / (float)pRow.getExp();
            if (m_CurPanlType == PanelType.Win)
            {
                m_PlayerExText.text = obj.Exp + "/" + pRow.getExp();
                m_AddExpTxt.text = _stage.m_playerexp.ToString();
            }
            else if (m_CurPanlType == PanelType.Clear)
            {
                m_AddExpTxt.text = _stage.m_playerexp.ToString();
                heroFruClearText.text = _stage.m_playerexp.ToString();
            }

            StartCoroutine(SliderRun(PlayerEx, obj.PlayOldExp(), maxValue, obj.GetPlayOldLevel(), obj.Level, PlayerLevel));
        }
        /// <summary>
        /// 扫荡界面
        /// </summary>
        /// <param name="clearRewards"></param>
        public void ClearUpdate(List<BattelInfo> clearRewards)
        {
            if (clearRewards != null)
            {
               // Debug.Log("打开扫荡界面:" + clearRewards.Count);
                if (this.clearRewards != null) this.clearRewards.Clear();
                this.clearRewards = clearRewards;
                m_ClearGrid.cellCount = clearRewards.Count;
                m_ClearGrid.updateCellEvent = CellUpdate;
                m_ClearGrid.Reload();
                Invoke("PlayTween", 0.5f);
            }
          

        }
        void PlayTween()
        {
            isStartScroll = true;
        }
        public override void UpdateUIView()
        {
            base.UpdateUIView();
            if (m_ClearScrollbar.value == 0)
            {
                isStartScroll = false;
            }
            if (isStartScroll)
            {
               m_ClearScrollbar.value-=Time.deltaTime * 0.2f;
            }
        }
        void CellUpdate(int index, RectTransform cell)
        {
           SweepItem _item=  cell.GetComponent<SweepItem>();
           if (_item == null)
           {
               _item= cell.gameObject.AddComponent<SweepItem>();
           }
           _item.index = index;
           _item.SetData(clearRewards[index],index+1);
        }
        private void OnHeroJoinBtn()
        {
            //无面板
            //TODO
            var objSelf = ObjectSelf.GetInstance();
            if (objSelf.GetIsPrompt())
            {
                if (objSelf.Week == objSelf.GetWeek())
                {
                    objSelf.SetHeroJoin(true);
                }
                else
                {
                    objSelf.SetPromptTime(true);
                    objSelf.SetPromptBttleend(true);
                }
            }
            else
            {
                obj.SetHeroJoin(true);
            }
            FailButtonClick();
        }

        private void OnPropertyStrBtn()
        {
            var objSelf = ObjectSelf.GetInstance();
            if (objSelf.GetIsPrompt())
            {
                if (objSelf.Week == objSelf.GetWeek())
                {
                    objSelf.SetProperty(true);
                }
                else
                {
                    objSelf.SetPromptTime(true);
                    objSelf.SetPromptBttleend(true);
                }
            }
            else
            {
                objSelf.SetProperty(true);
            }
            FailButtonClick();

        }

        private void OnExUpBtn()
        {
            var objSelf = ObjectSelf.GetInstance();
            if (objSelf.GetIsPrompt())
            {
                if (objSelf.Week == objSelf.GetWeek())
                {
                    objSelf.SetExUp(true);
                }
                else
                {
                    objSelf.SetPromptTime(true);
                    objSelf.SetPromptBttleend(true);
                }
            }
            else
            {
                objSelf.SetExUp(true);
            }
            FailButtonClick();
        }

        private void OnSkillStrBtn()
        {
            var objSelf=ObjectSelf.GetInstance();
            if (objSelf.GetIsPrompt())
            {
                if (objSelf.Week == objSelf.GetWeek())
                {
                    obj.SetSkillStr(true);
                }
                else
                {
                    objSelf.SetPromptTime(true);
                    objSelf.SetPromptBttleend(true);
                }
            }
            else
            {
                objSelf.SetSkillStr(true);
            }
            FailButtonClick();
        }

        private void FailButtonClick()
        {
            SceneManager.Inst.StartChangeScene(SceneEntry.Home.ToString());
        }


        //初始化通关星级
        private void InitFightStar()
        { 
            
            int _cout = SceneObjectManager.GetInstance().DieHeroCount;
            switch (_cout)
            {
                case 0:
                    m_CurStars = 3;
                    StartCoroutine(PlayStars(3));
                    break;
                case 1:
                    m_CurStars = 2;
                    StartCoroutine(PlayStars(2));
                    break;
                default:
                    m_CurStars = 1;
                    StartCoroutine(PlayStars(1));
                    break;
            }
        }
        IEnumerator PlayStars(int stars)
        {
            yield return new WaitForSeconds(1.0f);
            if (stars == 1)
            {
                m_Effect_Win_Stars[0].gameObject.SetActive(true);
                if (m_Effect_Win_Stars[0].GetComponentInChildren<ParticleSystem>() != null)
                {
                    m_Effect_Win_Stars[0].GetComponentInChildren<ParticleSystem>().Play();
                }
                m_Effect_Win_Stars[1].gameObject.SetActive(false);
                m_Effect_Win_Stars[2].gameObject.SetActive(false);
            }
            if (stars == 2)
            {
                StartCoroutine(PlayStarsByIndex(0,0));
                StartCoroutine(PlayStarsByIndex(1, 0.2f));
            }
            if (stars == 3)
            {
                StartCoroutine(PlayStarsByIndex(0, 0));
                StartCoroutine(PlayStarsByIndex(1, 0.2f));
                StartCoroutine(PlayStarsByIndex(2, 0.4f));
            }
            
        }
        IEnumerator PlayStarsByIndex(int index,float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            m_Effect_Win_Stars[index].gameObject.SetActive(true);
            if (m_Effect_Win_Stars[index].GetComponentInChildren<ParticleSystem>() != null)
            {
                m_Effect_Win_Stars[index].GetComponentInChildren<ParticleSystem>().Play();
            }
        }
        private void UpdateMoney()
        {
            DropBoxData Moneys = new DropBoxData();
            //MoneyCount.text = Moneys.m_DropGold.ToString();
        }
        private void OnDestory()
        {
            GameEventDispatcher.Inst.removeEventListener(GameEventID.G_Money_Update, UpdateMoney);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.SE_FightWin, UpdateShow);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.G_Guide_Continue, ShowNewGuide);
           // GameEventDispatcher.Inst.removeEventListener(GameEventID.G_HumanLevel_Update, UpdateLevel);
           //; GameEventDispatcher.Inst.removeEventListener(GameEventID.G_HumanExp_Update, UpdateHumanEx);
        }
        /// <summary>
        /// 设置界面类型
        /// </summary>
        public void setType(PanelType type)
        {
            m_CurPanlType = type;
            switch (type)
            {
                case PanelType.Win:
                    m_Panel_Win.gameObject.SetActive(true);
                    m_Panel_Fail.gameObject.SetActive(false);
                    m_Panel_Clear.gameObject.SetActive(false);
                    m_Buttons_Win.gameObject.SetActive(true);
                    m_Buttons_Fail.gameObject.SetActive(false);
                    m_Buttons_Clear.gameObject.SetActive(false);
                    m_Effect_Fail.gameObject.SetActive(false);
                    m_Buttons_Win.gameObject.SetActive(true);
                    m_Effect_Win.gameObject.SetActive(true);
                    m_Effect_Win_StarsTranform.gameObject.SetActive(true);
                    InitFightStar();
                    UpdateShow();
                    break;
                case PanelType.Fail:
                    m_Panel_Win.gameObject.SetActive(false);
                    m_Panel_Fail.gameObject.SetActive(true);
                    m_Panel_Clear.gameObject.SetActive(false);
                    m_Buttons_Win.gameObject.SetActive(false);
                    m_Buttons_Fail.gameObject.SetActive(true);
                    m_Buttons_Clear.gameObject.SetActive(false);
                    m_Effect_Fail.gameObject.SetActive(true);
                    m_Buttons_Win.gameObject.SetActive(false);
                    m_Effect_Win.gameObject.SetActive(false);
                    m_Effect_Win_StarsTranform.gameObject.SetActive(false);
                    break;
                case PanelType.Clear:
                    m_Panel_Win.gameObject.SetActive(false);
                    m_Panel_Fail.gameObject.SetActive(false);
                    m_Panel_Clear.gameObject.SetActive(true);
                    m_Buttons_Win.gameObject.SetActive(false);
                    m_Buttons_Fail.gameObject.SetActive(false);
                    m_Buttons_Clear.gameObject.SetActive(true);
                    m_Effect_Fail.gameObject.SetActive(false);
                    m_Buttons_Win.gameObject.SetActive(false);
                    m_Effect_Win.gameObject.SetActive(false);
                    m_Effect_Win_StarsTranform.gameObject.SetActive(false);
                    Goods = m_Clear_Goods;
                    GoodsLayout = m_Clear_GoodsLayout;
                    GoodsLayoutRectTransform = m_Clear_GoodsLayoutRectTransform;
                    GoodsRectTransform = m_Clear_GoodsRectTransform;
                    isRapidClearPanel = true;
                    break;
                default:
                    break;
            }

        }
        public void SetTypeToRapidClearResult(bool value)
        {
            if (value)
            {
                mFaileBG.SetActive(false);
                mVictoryBG.SetActive(false);
                Star.SetActive(false);
                //Money.SetActive(false);
                SelectBtn.gameObject.SetActive(false);
                AgainBtn.gameObject.SetActive(false);
                BackBtn.gameObject.SetActive(false);

                PlayerExbar.SetActive(true);
                Hero.SetActive(true);
                Goods.SetActive(true);
                //selectClearBtn.transform.FindChild("Text").GetComponent<Text>().text = GameUtils.getString("UI_raids_button2");
                againClearBtn.transform.FindChild("Text").GetComponent<Text>().text = GameUtils.getString("UI_raids_button3");
                SelectBtn.transform.FindChild("Text").GetComponent<Text>().text = GameUtils.getString("UI_raids_button1");
            }
            isRapidClearPanel = value;
            againClearBtn.gameObject.SetActive(value);
            backClearBtn.gameObject.SetActive(value);
            //diamond.gameObject.SetActive(value);
        }

        public void SetSweepCostResourceType(int resourceType, int costCount)
        {
            //diamond.GetComponent<Image>().sprite = GameUtils.GetSpriteByResourceType(resourceType);
            sweepCostTxt = costCount;
        }

        //物品显示最后操作
        public void OnUpdatePos()
        {
            if (UI_GoodItemList.Count <= 8)
                return;
            GoodsLayoutRectTransform.anchoredPosition3D = new Vector3(GoodsRectTransform.rect.width - GoodsLayoutRectTransform.rect.width, GoodsLayoutRectTransform.anchoredPosition3D.y, 0);
        }
        public void onAgainCall()
        {
            int MaxCount = 0;
            int SurplusFightCount = 0;
            int m_CurCampaignID = 0;
            if (ObjectSelf.GetInstance().GetIsPrompt())
            {
                m_CurCampaignID = 1001;
            }
            else
            {
                m_CurCampaignID = ObjectSelf.GetInstance().GetCurChapterID();
            }
            //StageTemplate stageinfo = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(ObjectSelf.GetInstance().CurStageID);
            StageTemplate stageinfo = StageModule.GetStageTemplateById(ObjectSelf.GetInstance().CurStageID);
            MaxCount = stageinfo.m_limittime;
            int m_iCurStageID = ObjectSelf.GetInstance().CurStageID;
            //StageData _StageData = ObjectSelf.GetInstance().BattleStageData.m_BattleStageList[m_CurCampaignID].GetStageData(m_iCurStageID);
            BattleStage bs = ObjectSelf.GetInstance().BattleStageData.GetBattleStageByChapterId(m_CurCampaignID);
            if (bs == null)
            {
                LogManager.LogToFile("BattleStage 数据为NULL ID=" + m_CurCampaignID);
                return;
            }
            StageData _StageData = bs.GetStageData(m_iCurStageID);
            if (MaxCount == -1)
            {
                SurplusFightCount = 99;
            }
            else
            {
                SurplusFightCount = MaxCount - _StageData.m_FightSum;
                if (SurplusFightCount <= 0)
                    SurplusFightCount = 0;
            }
            bool isFightBuyNum = (SurplusFightCount <= 0) ? false : true;
            if (physicalText > ObjectSelf.GetInstance().ActionPoint)
            {
                UI_FightControler.Inst.AddUI(UI_PowersAdd.UI_ResPath);
                
            }
            else if (!isFightBuyNum)
            {
                UI_FightControler.Inst.AddUI(UI_MaxFightManage.UI_ResPath);
            }
            else
            {
                var objSelf = ObjectSelf.GetInstance();
                if (objSelf.GetIsPrompt())
                {
                    if (objSelf.Week == objSelf.GetWeek())
                    {
                        UIState = UIStateEnum.PlayingExitAnimation;
                        /*SceneManager.Inst.StartChangeScene(SceneEntry.Battle01_00);*/
                        int nCurSceneID = obj.CurStageID;
                        //SceneManager.Inst.SwitchChangeScene(nCurSceneID);

                        CBeginBattle battle = new CBeginBattle();
                        battle.battleid = nCurSceneID;
                        battle.troopid = obj.Teams.GetDefaultGroup();

                        IOControler.GetInstance().SendProtocol(battle);
                    }
                    else
                    {
                        objSelf.SetPromptTime(true);
                        objSelf.SetPromptBttleend(true);
                        SceneManager.Inst.StartChangeScene(SceneEntry.Home.ToString());
                    }
                }
                else
                {
                    UIState = UIStateEnum.PlayingExitAnimation;
                    /*SceneManager.Inst.StartChangeScene(SceneEntry.Battle01_00);*/
                    int nCurSceneID = obj.CurStageID;
                    //SceneManager.Inst.SwitchChangeScene(nCurSceneID);

                    CBeginBattle battle = new CBeginBattle();
                    battle.battleid = nCurSceneID;
                    battle.troopid = obj.Teams.GetDefaultGroup();

                    IOControler.GetInstance().SendProtocol(battle);
                }
            }
        }
        public void OnSelectBtn()
        {
            var objSelf = ObjectSelf.GetInstance();
            if (objSelf.GetIsPrompt())
            {
                if (objSelf.Week == objSelf.GetWeek())
                {
                    objSelf.SetPromptFight(true);
                }
                else
                {
                    objSelf.SetPromptTime(true);
                    objSelf.SetPromptBttleend(true);
                }
            }
            else
            {
                objSelf.SetChangeLevel(true);
            }
           
            SceneManager.Inst.StartChangeScene(SceneEntry.Home.ToString());
        }
        public void onGoBack()
        {
            if (m_CurPanlType == PanelType.Clear)
            {
                UI_HomeControler.Inst.ReMoveUI(UI_BattleendPanel.UI_Res);
            }
            else
            {
                ObjectSelf.GetInstance().SetChangeLevel(false);
                ObjectSelf.GetInstance().SetPromptFight(false);
                SceneManager.Inst.StartChangeScene(SceneEntry.Home.ToString());
            }  
        }

        public void OnSelectClearBtn()
        {
            //GameObject.Destroy(gameObject);
            Resources.UnloadUnusedAssets();
            UI_MainHome.m_CamForm.SetActive(false);
            SetTypeToRapidClearResult(false); 
            GameEventDispatcher.Inst.clearEvent(GameEventID.G_Formation_Update);
            var selectVevelMgr = UI_SelectLevelMgrNew.Inst;
            if (selectVevelMgr != null)
                selectVevelMgr.RefreshStageItem();
            UI_HomeControler.Inst.ReMoveUI(UI_Form.UI_ResPath);
            Destroy(this.transform.GetComponent<UI_BattleendPanel>());
        }
        public void OnAgainClearBtn()
        {
            //gameObject.SetActive(false);
            //GameObject.Destroy(gameObject,0.1f);
            Resources.UnloadUnusedAssets();
            //UI_Form.GetInst().OnClickMopUpBtn();
            UI_Stage.Inst.OnClickWipeOutOneBtn();
        }
        public void AddLevelNum(string level, Transform par)
        {
            for (int i = 0; i < par.childCount; ++i)
            {
                Destroy(par.GetChild(i).gameObject);
            }
            for (int i = 0; i < level.Length; ++i)
            {
                if (level.Length == 1)
                {
                    GameObject _obj = new GameObject("Num");
                    _obj.transform.SetParent(par, false);
                    _obj.transform.localScale = Vector3.one;
                    _obj.AddComponent<Image>().enabled = false;
                }
                string temp = level.Substring(i, 1);
                GameObject obj = new GameObject("Num");
                obj.transform.SetParent(par, false);
                obj.transform.localScale = Vector3.one;
                string url = "UI/Number/card_number/";
                obj.AddComponent<Image>().sprite = UIResourceMgr.LoadSprite(url + temp);

            }
        }
        IEnumerator HeroExadd(float value)
        {
            yield return new WaitForSeconds(value);
            DropBoxData Exadd = new DropBoxData();
            int exp = Exadd.m_HumanExp + obj.Exp;
            PlayerEx.value = exp;
        }
        IEnumerator SliderRun(Slider mExpProgress, float min, float max, int oldLevel, int newLevel, Text levelText = null)
        {
            float maxValue = 0;
            if (oldLevel<newLevel)
            {
                maxValue = max;
                max = 1.0f;
            }
            for (; ; min += 0.05f)
            {
                if (min > max)
                {
                    if (oldLevel < newLevel)
                    {
                        //升级弹窗
                        UpWindowShow();  
                        oldLevel=newLevel;
                        if (levelText!=null)
                        {
                            levelText.text = "Lv "+oldLevel.ToString();
                        }  
                        StartCoroutine(SliderRun(mExpProgress, 0, maxValue, oldLevel, newLevel,levelText));
                    }
                    else
                    {
                        mExpProgress.value = max;
                        break;
                    }
                    
                }
                else
                {
                    //min = objc.GetHeroData().GetExpPercentage();
                    mExpProgress.value = min;
                }

                yield return new WaitForSeconds(0.001f);
            }
           // StopCoroutine(SliderRun(mExpProgress, min, max));
        }

        //添加掉落
        private void AddGoddItem()
        {
            StageTemplate stage = null;
            if (isRapidClearPanel)
            {
                //stage = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(obj.CurStageID);
                stage = StageModule.GetStageTemplateById(obj.CurStageID);
            }
            else
            {
                if (obj.GetIsPrompt())
                {
                    //stage = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(obj.GetPromptCurCampaignID());
                    stage = StageModule.GetStageTemplateById(obj.GetPromptCurCampaignID());
                }
                else
                {
                    //stage = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(obj.GetCurCampaignID());
                    stage = StageModule.GetStageTemplateById(obj.GetCurCampaignID());
                }
            }
            int items = 0;
            int heroNum = 0;
            int goods = 0;
            int exp = 0;
            List<int> goodlist = obj.BattleDropBoxData.indroplist;
            int count = goodlist.Count;
            // Debug.Log(count);
            for (int i = 0; i < count; ++i)
            {
                m_Cell = UniversalItemCell.GenerateItem(GoodsLayout.transform);

                InnerdropTemplate value = (InnerdropTemplate)DataTemplate.GetInstance().m_InnerdropTable.getTableData(goodlist[i]);
                if (value == null)
                    return;

                int itemid = value.getObjectid();//掉落物ID
                int type = value.getObjectid() / 1000000;
                m_Cell.AddClickListener(ShowItemPreviewUIHandler);

                switch (type)
                {
                    case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES:
                        ResourceindexTemplate _temp_res = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(itemid);
                        if (_temp_res != null)
                        {
                            m_Cell.InitByID(itemid, value.getDropnum());
                            m_Cell.SetText(GameUtils.getString(_temp_res.getName()), "", "");
                        }
                        break;
                    case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE: //符文
                        {
                            ItemTemplate itemTable = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(itemid);
                            if (itemTable != null)
                            {
                                m_Cell.InitByID(itemid, -1);
                                m_Cell.SetText(GameUtils.getString(itemTable.getName()), "", "");
                            }    
                        }
                        break;
                    case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON:
                        {
                            ItemTemplate itemTable = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(itemid);
                            if (itemTable != null)
                            {
                                m_Cell.InitByID(itemid, value.getDropnum());
                                m_Cell.SetText(GameUtils.getString(itemTable.getName()), "", "");
                            }
                        }
                        break;
                    case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO:
                        {
                            HeroTemplate hero = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(itemid);
                            if (hero != null)
                            {
                                m_Cell.InitByID(itemid, value.getDropnum());
                                m_Cell.SetText(GameUtils.getString(hero.getTitleID()), "", "");
                            }
                        }
                        break;

                    default:
                        break;
                }
            }

            if (isRapidClearPanel)
            {
                //moneyRapidClear.SetActive(true);
                moneyRapidClearText.text = (stage.m_goldreward + goods).ToString();
            }
            else
            {
                //Money.SetActive(true);
                MoneyCount.text = (stage.m_goldreward + goods).ToString();
            }
            if (stage.m_expcrystal != -1)
            {

                if (stage.m_expcrystal + exp == 0)
                {
                   // ExFru.SetActive(false);
                }
                else
                {
                   // ExFru.SetActive(true);
                }
                ExFruCount.text = "+" + (stage.m_expcrystal + exp).ToString();
            }
            else
            {
               // ExFru.SetActive(false);
            }
            //if (items>obj.CommonItemContainer.GetBagItemSizeMax()-obj.CommonItemContainer.GetBagItemSum())
            //{
            //    InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("hero_bag_tips2"), MsgBoxGroup);
            //}
            //if (heroNum>obj.HeroContainerBag.GetHeroBagSizeMax()-obj.HeroContainerBag.GetHeroList().Count)
            //{
            //    InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("hero_bag_tips4"), MsgBoxGroup);
            //}
        }

        private  void ShowItemPreviewUIHandler(int tableID)
        {
            EM_OBJECT_CLASS eoc = GameUtils.GetObjectClassById(tableID);
            switch (eoc)
            {
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE:
                    ItemTemplate runeItemT = DataTemplate.GetInstance().GetItemTemplateById(tableID);
                    if (runeItemT == null)
                    {
                        LogManager.LogError("item表格中缺少物品id=" + tableID);
                    }
                    UI_RuneInfo.SetShowRuneDate(runeItemT);
                    break;
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON:
                    ItemTemplate itemT = DataTemplate.GetInstance().GetItemTemplateById(tableID);
                    if (itemT == null)
                    {
                        LogManager.LogError("item表格中缺少物品id=" + tableID);
                    }
     
                    UICommonManager.Inst.ShowHeroObtain(tableID);
                    break;
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_SKIN:
                    ArtresourceTemplate artT = DataTemplate.GetInstance().GetArtResourceTemplate(tableID);
                    if (artT == null)
                    {
                        LogManager.LogError("ArtResource时装表格中缺少物品id=" + tableID);
                    }
   
                    break;
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO:
                    HeroTemplate heroT = DataTemplate.GetInstance().GetHeroTemplateById(tableID);
                    if (heroT == null)
                    {
                        LogManager.LogError("hero表格中缺少物品id=" + tableID);
                    }
                    List<ObjectCard> list= ObjectSelf.GetInstance().HeroContainerBag.GetHeroList();
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].GetHeroRow().GetID() == tableID)
                        {
                            UICommonManager.Inst.ShowHero(list[i]);
                        }
                    }
                   
                    break;
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES:
                    //资源类型点击无响应;
                    break;
                default:
                    LogManager.LogError("未处理的商城物品预览类型");
                    break;
            }
        }

        private void UpWindowShow()
        {
            int frontLevrel = ObjectSelf.GetInstance().GetPlayOldLevel();
            int curLevel = (int)ObjectSelf.GetInstance().Level;
            if (curLevel > frontLevrel)
            {
                int count = curLevel - frontLevrel;
                for (int i = 0; i < count; i++)
                {
                    if (true)//如果该等级需要弹出新功能提示
                    {
                        
                    }
                    StartCoroutine(DelayShowLevelUpUI());
                }               
            }
        }
        IEnumerator DelayShowLevelUpUI()
        { 
            float delayTime=0;
            if (m_CurStars == -1)
            {
                delayTime = 0;
            }
            else if(m_CurStars==1)
            {
                delayTime = 0.3f;
            }
            else if(m_CurStars==2)
            {
                delayTime = 0.5f;
            }
            else if (m_CurStars == 3)
            {
                delayTime = 0.7f;
            }
            yield return new WaitForSeconds(delayTime);
            if (!isRapidClearPanel)
                UI_FightControler.Inst.AddUI(UI_LevelUpShowUI.UI_ResPath);
            else
                UI_HomeControler.Inst.AddUI(UI_LevelUpShowUI.UI_ResPath);
        }
        private void OnISShowSpecialStageOrMysteriousShop()
        {
            int frontLevrel = ObjectSelf.GetInstance().GetPlayOldLevel();
            int curLevel = (int)ObjectSelf.GetInstance().Level;
            if (curLevel > frontLevrel)
                return;
            if (UI_FightControler.Inst != null)
            {
                if (UI_FightControler.Inst.GetIsSpecialStage())
                {
                    UI_FightControler.Inst.SpecialStageSpecialTips();
                    //新手引导相关 【开启特殊关卡（非强制）
                    //if (GuideManager.GetInstance().isGuideUser)
                    //{
                    //    GuideManager.GetInstance().ShowGuideWithIndex(200601);
                    //}
                }
                if (UI_FightControler.Inst.GetIsMysteriousShop())
                {
                    UI_FightControler.Inst.MysteriousShopSpecialTips();
                }
            }


        }
        public override void InitUIState()
        {
            base.InitUIState();
            MsgBoxGroup.transform.SetAsLastSibling();
        }

    }
    public class SweepItem : CellItem
    {
        private Text m_Money;
        private Text m_AddExp;
        private GameObject m_Clear_Goods;//物品栏
        private Transform m_Clear_GoodsLayout;//物品栏父节点
        private RectTransform m_Clear_GoodsRectTransform;
        private RectTransform m_Clear_GoodsLayoutRectTransform;
        private Text m_CountText;
        public override void InitUIData()
        {
            base.InitUIData();
            m_Money = selfTransform.FindChild("Money/Text").GetComponent<Text>();
            m_AddExp = selfTransform.FindChild("AddExpTxt").GetComponent<Text>();
            m_Clear_Goods = selfTransform.FindChild("GoodList").gameObject;
            m_Clear_GoodsLayout = selfTransform.FindChild("GoodList/GoodsLayout");
            m_Clear_GoodsLayoutRectTransform = m_Clear_GoodsLayout.GetComponent<RectTransform>();
            m_CountText = selfTransform.FindChild("RapidClearBG/curBattle").GetComponent<Text>();
        }
        public void SetData(BattelInfo battleInfo,int sweepCount)
        {
            //Debug.Log("当前扫荡次数:"+sweepCount);
            m_CountText.text = string.Format("第{0}次战斗",sweepCount);
            StageTemplate stage = null;
            stage = StageModule.GetStageTemplateById(battleInfo.battleid);
            for (int i = 0; i < m_Clear_GoodsLayout.childCount; i++)
            {
                Destroy(m_Clear_GoodsLayout.GetChild(i).gameObject);
            }
            List<int> goodlist = new List<int>();
            foreach (int item in battleInfo.indroplist)
            {
                goodlist.Add(item);
            } 
            m_AddExp.text = stage.m_playerexp.ToString();//battleInfo.teamexp.ToString();
            m_Money.text = (stage.m_goldreward).ToString();
            int count = goodlist.Count;
            for (int i = 0; i < count; ++i)
            {
                UniversalItemCell m_Cell = UniversalItemCell.GenerateItem(m_Clear_GoodsLayout.transform);

                InnerdropTemplate value = (InnerdropTemplate)DataTemplate.GetInstance().m_InnerdropTable.getTableData(goodlist[i]);
                if (value == null)
                    return;
                int itemid = value.getObjectid();//掉落物ID
                int type = value.getObjectid() / 1000000;
                m_Cell.AddClickListener(ShowItemPreviewUIHandler);

                switch (type)
                {
                    case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES:
                        ResourceindexTemplate _temp_res = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(itemid);
                        if (_temp_res != null)
                        {
                            m_Cell.InitByID(itemid, value.getDropnum());
                            m_Cell.SetText(GameUtils.getString(_temp_res.getName()), "", "");
                          
                        }
                        break;
                    case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE: //符文
                        {
                            ItemTemplate itemTable = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(itemid);
                            if (itemTable != null)
                            {
                                m_Cell.InitByID(itemid, -1);
                                m_Cell.SetText(GameUtils.getString(itemTable.getName()), "", "");
                            }
                        }
                        break;
                    case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON:
                        {
                            ItemTemplate itemTable = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(itemid);
                            if (itemTable != null)
                            {
                                m_Cell.InitByID(itemid, value.getDropnum());
                                m_Cell.SetText(GameUtils.getString(itemTable.getName()), "", "");
                            }
                        }
                        break;
                    case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO:
                        {
                            HeroTemplate hero = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(itemid);
                            if (hero != null)
                            {
                                m_Cell.InitByID(itemid, value.getDropnum());
                                m_Cell.SetText(GameUtils.getString(hero.getTitleID()), "", "");
                            }
                        }
                        break;

                    default:
                        break;
                }
            }

        }
        private void ShowItemPreviewUIHandler(int tableID)
        {
            EM_OBJECT_CLASS eoc = GameUtils.GetObjectClassById(tableID);
            switch (eoc)
            {
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE:
                    ItemTemplate runeItemT = DataTemplate.GetInstance().GetItemTemplateById(tableID);
                    if (runeItemT == null)
                    {
                        LogManager.LogError("item表格中缺少物品id=" + tableID);
                    }
                    UI_RuneInfo.SetShowRuneDate(runeItemT);
                    break;
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON:
                    ItemTemplate itemT = DataTemplate.GetInstance().GetItemTemplateById(tableID);
                    if (itemT == null)
                    {
                        LogManager.LogError("item表格中缺少物品id=" + tableID);
                    }

                    UICommonManager.Inst.ShowHeroObtain(tableID);
                    break;
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_SKIN:
                    ArtresourceTemplate artT = DataTemplate.GetInstance().GetArtResourceTemplate(tableID);
                    if (artT == null)
                    {
                        LogManager.LogError("ArtResource时装表格中缺少物品id=" + tableID);
                    }

                    break;
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO:
                    HeroTemplate heroT = DataTemplate.GetInstance().GetHeroTemplateById(tableID);
                    if (heroT == null)
                    {
                        LogManager.LogError("hero表格中缺少物品id=" + tableID);
                    }
                    List<ObjectCard> list = ObjectSelf.GetInstance().HeroContainerBag.GetHeroList();
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].GetHeroRow().GetID() == tableID)
                        {
                            UICommonManager.Inst.ShowHero(list[i]);
                        }
                    }

                    break;
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES:
                    //资源类型点击无响应;
                    break;
                default:
                    LogManager.LogError("未处理的商城物品预览类型");
                    break;
            }
        }

    }
}