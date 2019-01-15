using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using DreamFaction.GameCore;
using DreamFaction.Utils;
using DreamFaction.GameEventSystem;
namespace DreamFaction.UI
{
    public class UI_HeroInfoManager : BaseUI
    {
        public static UI_HeroInfoManager _instance;

        private string m_HeroName;
        private int m_HeroStar;
        private int m_HeroLevel;
        private ObjectCard CurCard;                                    //当前显示卡牌

        private Text HeroNameTxt;                                      //英雄称号
        private Text HeroLevelTxt;
        private Text PlayerNameTxt;                                    //英雄名称
        private Image AttackTypeImg;                                   //攻击类型
        private Image RaceTypeImg;                                     //种族类型
        private Image JobTypeImg;                                      //英雄战斗担任的位置
        private Slider ExpSlider;                                      //经验条
        private Image m_ExpSliderImg = null;                           //经验条前置图片

        private Text HeroDesTxt;                                       //英雄描述
        private Text HpTxt;                                            //生命值
        private Text PhyAtkTxt;                                        //物理攻击力
        private Text ApAtkTxt;                                         //法术攻击
        private Text PhyDefenseTxt;                                    //物理防御
        private Text ApDefenseTxt;                                     //法术防御


        private Button DetailInfoBtn;                                  //详细信息按钮
        private Button HeroMatchBtn;                                   //英雄搭配按钮
        private Button SkillItem_publicBtn;
        private Button SkillItem_passivityBtn;                         //被动技能按钮
        private Button SkillItem_PVPBtn;

        private Button ReloadingBtn;                                   //换装按钮
        private Button LevelUp;                                        //升级按钮
        private Button OrderUp;                                        //进阶按钮
        private Image OrderUpImage;                                    //进阶背景

        private Button HeroInfoClose_btn;                              //英雄详细信息关闭按钮
        private Button HeroMakeClose_btn;                              //英雄搭配信息关闭按钮

        private Image[] star;                                           //英雄品质星级图片

        private Image[] starBG;                                         //英雄品质星级图片背景

        private Image Skill1Image;                                     //通用技能图片
        private Image Skill2Image;                                     //被动技能图片
        private Image Skill3Image;                                     //PVP技能图片

        private Text Skill1Lv;                                         //通用技能等级
        private Text Skill2Lv;                                         //被动技能等级
        private Text Skill3Lv;                                         //PVP技能等级
        private HeroTemplate _HeroItem;                                //英雄表数据 
        private GameObject Herodetailinfo;                             //英雄详细信息
        private GameObject HeroMakeinfo;                               //英雄搭配信息
        private Transform HeroinfoListLayout;                          //英雄详细数据添加父节点
        private int CardDetailedDataCount = 19;                          //英雄详细信息个数
        private List<UI_GoodItem> CardDetailedDataList;                //英雄详细信息个数数组
        private List<GameObject> HeroMatchList;                        //英雄搭配显示卡牌
        private Text HeroTrait_Text;                                   //英雄特点描述
        private Transform HeroMatchListLayout;                         //英雄搭配卡牌显示父节点
        //private RectTransform HeroMatchDesRect;                        //英雄搭配背景框
        private RectTransform HeroinfoLPos;                            //英雄信息按钮左侧坐标
        private RectTransform HeroinfoCPos;                            //英雄信息按钮中心坐标
        private RectTransform HeroinfoRPos;                            //英雄信息按钮右侧坐标
        private Button LevelUp_btn;                                    //升级按钮
        private Text m_Make_HeroName;                                   //英雄搭配界面中的英雄名字
        private Text m_Make_HeroTitle;                                  //英雄搭配界面中的英雄名称
           
        private SkillTemplate Gskilltemp;                              //通用技能表数据
        private SkillTemplate Pskilltemp;                              //被动技能表数据
        private SkillTemplate Askilltemp;                              //PVP技能表数据

        private GameObject Skilltipsobj;                               //技能提示
       // private RectTransform SkilltipsbgRect;                         //技能提示背景框
        private Text Skilltipstext;                                    //技能信息提示
        private GameObject GSkillImage;                                     //通用技能提示图片
        private GameObject PSkillImage;                                     //被动技能提示图片
        private GameObject ASkillImage;                                     //PVP技能提示图片
        private int SkilltipsBgSizeAmend = 40;                           //技能提示背景框的修正值

        private Text skillName;                                          //技能名称+技能等级
        private Text skillLimitLevel;                                   //技能等级限制
        private Text skillxiaohao;                                       //技能消耗怒气
        private Text skillCD;                                            //技能cd
        private Text skilldec;                                        //技能描述
        private Transform postion_Small_Title;                        //切换到被动技能时 标题的位置
        private Transform postion_Small_Desc;                         //切换到被动技能时 描述的位置
        private Transform postion_Big_Title;                          //切换到通用技能时 标题的位置
        private Transform postion_Big_Desc;                           //切换到通用技能时 描述的位置

        private GameObject RaceTipsobj;                                //种族提示obj
        private GameObject JobTypeTipsobj;                             //位置提示obj
        private GameObject AttackTypeTipsobj;                          //攻击方式提示obj

        private Text JobTypeTipsText;                                  //位置提示文本
        private Text AttackTypeTipsText;                               //攻击方式提示文本
        private Quaternion CurAngle;                                   //记录3D模型当前角度
        public int GUI_ID;
        UI_HeroLevelupmanager mUIHeroLevelMgr = null;

        UI_HeroLevelupmanager HLMgr
        {
            get
            {
                if (mUIHeroLevelMgr == null)
                {
                    GameObject go = (GameObject)Instantiate(UIResourceMgr.LoadPrefab("UI/Prefabs/UI_Home/UI_HeroInfo/LevelUpPage"));
                    if (go != null)
                    {
                        go.transform.parent = this.transform.parent;
                        go.transform.localPosition = Vector3.zero;
                        go.transform.localScale = Vector3.one;
                        mUIHeroLevelMgr = go.GetComponent<UI_HeroLevelupmanager>();
                    }

                }
                return mUIHeroLevelMgr;
            }
        }

        UI_HeroBeginnerManager mUIHeroBeMgr = null;
        UI_HeroBeginnerManager BeMgr
        {
            get
            {
                if (mUIHeroBeMgr == null)
                {
                    GameObject go = (GameObject)Instantiate(UIResourceMgr.LoadPrefab("UI/Prefabs/UI_Home/UI_HeroInfo/HeroBeginner"));
                    if (go != null)
                    {
                        go.transform.parent = this.transform.parent;
                        go.transform.localPosition = Vector3.zero;
                        go.transform.localScale = Vector3.one;
                        mUIHeroBeMgr = go.GetComponent<UI_HeroBeginnerManager>();
                    }

                }
                return mUIHeroBeMgr;
            }
        }

        UI_HeroSkin mUIHeroSkin = null;

        UI_HeroSkin HsMgr
        {
            get
            {
                if (mUIHeroSkin == null)
                {
                    GameObject go = (GameObject)Instantiate(UIResourceMgr.LoadPrefab("UI/Prefabs/UI_Home/UI_HeroInfo/UI_HeroSkin_2_3"));
                    if (go != null)
                    {
                        go.transform.parent = this.transform.parent;
                        go.transform.localPosition = Vector3.zero;
                        go.transform.localScale = Vector3.one;
                        mUIHeroSkin = go.GetComponent<UI_HeroSkin>();
                    }

                }
                return mUIHeroSkin;
            }
        }

        public override void InitUIData()
        {
            _instance = this;
            base.InitUIData();
            star = new Image[5];
            starBG = new Image[5];
            star[0] = selfTransform.FindChild("HeroInof_LeftUP/Stars/Star_0").GetComponent<Image>();
            star[1] = selfTransform.FindChild("HeroInof_LeftUP/Stars/Star_1").GetComponent<Image>();
            star[2] = selfTransform.FindChild("HeroInof_LeftUP/Stars/Star_2").GetComponent<Image>();
            star[3] = selfTransform.FindChild("HeroInof_LeftUP/Stars/Star_3").GetComponent<Image>();
            star[4] = selfTransform.FindChild("HeroInof_LeftUP/Stars/Star_4").GetComponent<Image>();
            starBG[0] = selfTransform.FindChild("HeroInof_LeftUP/Stars/Star_0_BG").GetComponent<Image>();
            starBG[1] = selfTransform.FindChild("HeroInof_LeftUP/Stars/Star_1_BG").GetComponent<Image>();
            starBG[2] = selfTransform.FindChild("HeroInof_LeftUP/Stars/Star_2_BG").GetComponent<Image>();
            starBG[3] = selfTransform.FindChild("HeroInof_LeftUP/Stars/Star_3_BG").GetComponent<Image>();
            starBG[4] = selfTransform.FindChild("HeroInof_LeftUP/Stars/Star_4_BG").GetComponent<Image>();

            HeroNameTxt = selfTransform.FindChild("HeroInof_LeftUP/HeroName_txt").GetComponent<Text>();
            HeroLevelTxt = selfTransform.FindChild("HeroInof_LeftUP/Level_txt").GetComponent<Text>();
            PlayerNameTxt = selfTransform.FindChild("HeroInof_LeftUP/PlayerName_Img/PlayerName_txt").GetComponent<Text>();
            AttackTypeImg = selfTransform.FindChild("HeroInof_LeftUP/AttackType_Img").GetComponent<Image>();
            RaceTypeImg = selfTransform.FindChild("HeroInof_LeftUP/RaceTypeImg").GetComponent<Image>();
            JobTypeImg = selfTransform.FindChild("HeroInof_LeftUP/JobType_Img").GetComponent<Image>();
            ExpSlider = selfTransform.FindChild("HeroInof_LeftUP/ExpSlider").GetComponent<Slider>();
            m_ExpSliderImg = selfTransform.FindChild("HeroInof_LeftUP/ExpSlider/FoeImg").GetComponent<Image>();

            HeroDesTxt = selfTransform.FindChild("HeroInfo_Mid/HeroDes_txt").GetComponent<Text>();
            HpTxt = selfTransform.FindChild("HeroInfo_Mid/Info_Hp/InfoValue_txt").GetComponent<Text>();
            PhyAtkTxt = selfTransform.FindChild("HeroInfo_Mid/Info_PhyAttack/InfoValue_txt").GetComponent<Text>();
            ApAtkTxt = selfTransform.FindChild("HeroInfo_Mid/Info_ApAttack/InfoValue_txt").GetComponent<Text>();
            PhyDefenseTxt = selfTransform.FindChild("HeroInfo_Mid/Info_PhyDefense/InfoValue_txt").GetComponent<Text>();
            ApDefenseTxt = selfTransform.FindChild("HeroInfo_Mid/Info_ApDefense/InfoValue_txt").GetComponent<Text>();

            DetailInfoBtn = selfTransform.FindChild("HeroInfo_LeftBottom/InfoDetail_btn").GetComponent<Button>();
            HeroMatchBtn = selfTransform.FindChild("HeroInfo_LeftBottom/HeroMake_btn").GetComponent<Button>();
            SkillItem_publicBtn = selfTransform.FindChild("HeroInfo_LeftBottom/SkillItem_0").GetComponent<Button>();
            SkillItem_passivityBtn = selfTransform.FindChild("HeroInfo_LeftBottom/SkillItem_1").GetComponent<Button>();
            SkillItem_PVPBtn = selfTransform.FindChild("HeroInfo_LeftBottom/SkillItem_2").GetComponent<Button>();

            ReloadingBtn = selfTransform.FindChild("ReloadingBtn").GetComponent<Button>();
            LevelUp = selfTransform.FindChild("LevelUP_btn").GetComponent<Button>();
            OrderUp = selfTransform.FindChild("OrderUP_btn").GetComponent<Button>();
            OrderUpImage = selfTransform.FindChild("OrderUP_btn").GetComponent<Image>();

            Skill1Image = selfTransform.FindChild("HeroInfo_LeftBottom/SkillItem_0/SkillIcon").GetComponent<Image>();
            Skill2Image = selfTransform.FindChild("HeroInfo_LeftBottom/SkillItem_1/SkillIcon").GetComponent<Image>();
            Skill3Image = selfTransform.FindChild("HeroInfo_LeftBottom/SkillItem_2/SkillIcon").GetComponent<Image>();
            Skill1Lv = selfTransform.FindChild("HeroInfo_LeftBottom/SkillItem_0/SkillLevelNumber_txt").GetComponent<Text>();
            Skill2Lv = selfTransform.FindChild("HeroInfo_LeftBottom/SkillItem_1/SkillLevelNumber_txt").GetComponent<Text>();
            Skill3Lv = selfTransform.FindChild("HeroInfo_LeftBottom/SkillItem_2/SkillLevelNumber_txt").GetComponent<Text>();

            Herodetailinfo = selfTransform.FindChild("Herodetailinfo").gameObject;
            HeroinfoListLayout = Herodetailinfo.transform.FindChild("HeroList/ListLayOut");
            HeroInfoClose_btn = Herodetailinfo.transform.FindChild("HeroInfoClose_btn").GetComponent<Button>();
            Herodetailinfo.SetActive(false);

            HeroMakeinfo = selfTransform.FindChild("HeroMakeinfo").gameObject;
            HeroTrait_Text = HeroMakeinfo.transform.FindChild("HeroTrait_Image/HeroTrait_Text").GetComponent<Text>();
            HeroMatchListLayout = HeroMakeinfo.transform.FindChild("UI_MatchHeroList/ListLayout");
            //HeroMatchDesRect = HeroMakeinfo.transform.FindChild("HeroTrait_Image/Bg_Image").GetComponent<RectTransform>();
            HeroMakeClose_btn = HeroMakeinfo.transform.FindChild("HeroMakeClose_btn").GetComponent<Button>();
            HeroMatchList = new List<GameObject>();
           // HeroMakeinfo.transform.SetParent(null, false);//此处没有设置隐藏是因为Text自适应大小有延迟

            m_Make_HeroTitle= HeroMakeinfo.transform.FindChild("name").GetComponent<Text>();
            m_Make_HeroName= HeroMakeinfo.transform.FindChild("name/Text").GetComponent<Text>();
            HeroinfoLPos = selfTransform.FindChild("HeroInfo_LeftBottom/HeroinfoLPos").GetComponent<RectTransform>();
            HeroinfoCPos = selfTransform.FindChild("HeroInfo_LeftBottom/HeroinfoCPos").GetComponent<RectTransform>();
            HeroinfoRPos = selfTransform.FindChild("HeroInfo_LeftBottom/HeroinfoRPos").GetComponent<RectTransform>();


            LevelUp_btn = selfTransform.FindChild("LevelUP_btn").GetComponent<Button>();
            LevelUp_btn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickShowLevelUp));
            OrderUp.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickShowBeginner));
            HeroInfoClose_btn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnHeroInfoClose_btn));
            DetailInfoBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnDetailInfoBtn));
            HeroMatchBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnHeroMatchBtn));
            HeroMakeClose_btn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnHeroMakeClose_btn));
            ReloadingBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnReloadingBtn));


            Skilltipsobj = selfTransform.FindChild("Skilltips").gameObject;
            Skilltipsobj.SetActive(false);
            Skilltipstext = Skilltipsobj.transform.FindChild("Skilltips_Text").GetComponent<Text>();

            skillName = Skilltipsobj.transform.FindChild("skillTitle").GetComponent<Text>();
            skillLimitLevel = Skilltipsobj.transform.FindChild("skillTitle/limit").GetComponent<Text>();
            skillxiaohao = Skilltipsobj.transform.FindChild("grid/cell1/skillxiaohao/Text").GetComponent<Text>();
            skillCD = Skilltipsobj.transform.FindChild("grid/cell1/skillcd/Text").GetComponent<Text>();
            skilldec = Skilltipsobj.transform.FindChild("grid/cell2/dec").GetComponent<Text>();

            postion_Small_Title = Skilltipsobj.transform.FindChild("postion_Small(title)");
            postion_Small_Desc = Skilltipsobj.transform.FindChild("grid/cell2/postion_Small(desc)");
            postion_Big_Title = Skilltipsobj.transform.FindChild("postion_Big(title)");
            postion_Big_Desc = Skilltipsobj.transform.FindChild("grid/cell2/postion_Big(desc)");

            selfTransform.FindChild("HeroInfo_LeftBottom/SkillItem_0").GetComponent<Button>().onClick.AddListener(OnGskillTips);
            selfTransform.FindChild("HeroInfo_LeftBottom/SkillItem_1").GetComponent<Button>().onClick.AddListener(OnPskillTips);
            selfTransform.FindChild("HeroInfo_LeftBottom/SkillItem_2").GetComponent<Button>().onClick.AddListener(OnAskillTips);

            RaceTipsobj = selfTransform.FindChild("RaceTips").gameObject;
            JobTypeTipsobj = selfTransform.FindChild("JobTypeTips").gameObject;
            JobTypeTipsText = JobTypeTipsobj.transform.FindChild("Text").GetComponent<Text>();
            AttackTypeTipsobj = selfTransform.FindChild("AttackTypeTips").gameObject;
            AttackTypeTipsText = AttackTypeTipsobj.transform.FindChild("Text").GetComponent<Text>();
            RaceTipsobj.SetActive(false);
            JobTypeTipsobj.SetActive(false);
            AttackTypeTipsobj.SetActive(false);
            selfTransform.FindChild("HeroInof_LeftUP/AttackType_Img").GetComponent<Button>().onClick.AddListener(OnAttackTypeTips);
            selfTransform.FindChild("HeroInof_LeftUP/RaceTypeImg").GetComponent<Button>().onClick.AddListener(OnRaceTips);
            selfTransform.FindChild("HeroInof_LeftUP/JobType_Img").GetComponent<Button>().onClick.AddListener(OnJobTypeTips);


            GameEventDispatcher.Inst.addEventListener(GameEventID.HE_HeroLevelUpSucceed, InitCardData);
            GameEventDispatcher.Inst.addEventListener(GameEventID.HE_HeroBeginnerUpdateShow, InitCardData);
            GameEventDispatcher.Inst.addEventListener(GameEventID.U_HeroChangeTarget, InitMatchHeroBtn);
            //InstantiateCardDetailedData();
            //ShowInfo(ObjectSelf.GetInstance().HeroContainerBag.GetHeroList()[0]);
        }

        protected void OnDestroy()
        {
            GameEventDispatcher.Inst.removeEventListener(GameEventID.HE_HeroLevelUpSucceed, InitCardData);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.HE_HeroBeginnerUpdateShow, InitCardData);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.U_HeroChangeTarget, InitMatchHeroBtn);
            Destroy(HeroMakeinfo);
        }

        public override void InitUIView()
        {
            base.InitUIView();
            InstantiateCardDetailedData();
            if(CurCard==null)
            ShowInfo(ObjectSelf.GetInstance().HeroContainerBag.GetHeroList()[0]);
            
            //是否是新手引导 
            if (UI_HeroListManager._instance.isNewGuide)
            {
                for (int i = 0; i < ObjectSelf.GetInstance().HeroContainerBag.GetHeroList().Count; i++)
                {
                    if (ObjectSelf.GetInstance().HeroContainerBag.GetHeroList()[i].GetHeroRow().getId() == 1403210052)
                    {
                        //默认选择小恶魔
                        ShowInfo(ObjectSelf.GetInstance().HeroContainerBag.GetHeroList()[i]);
                        UI_HeroListManager._instance.isNewGuide = false;
                        break;
                    }
                }
            }
            
            
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_HeroChangeTarget, CurCard);

        }
        public override void UpdateUIView()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Skilltipsobj.SetActive(false);
                RaceTipsobj.SetActive(false);
                JobTypeTipsobj.SetActive(false);
                AttackTypeTipsobj.SetActive(false);
            }      
        }
        //当前卡牌
        public ObjectCard GetCurCard() { return CurCard; }
        public void SetCurCard(ObjectCard card) { CurCard = card; }
        public void ShowInfo(ObjectCard objHero)
        {
            CurCard = objHero;
            _HeroItem = CurCard.GetHeroRow();

            InitCardData();
            InitHeroTypes();
            InitSkillInfo();
            InitHeroDes();
            InitCardDetailedData();
            InitMatchHeroBtn();
            InitHeroMatthData();
            //GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_HeroChangeTarget, CurCard);
            InitHeroSkinBtn();
        }
        /// <summary>
        /// 换装按钮显示
        /// </summary>
        void InitHeroSkinBtn()
        {
            bool isGray = CurCard.GetHeroRow().getUseableArtresource().Length == 1;
            GameUtils.SetBtnSpriteGrayState(ReloadingBtn, isGray);
            ReloadingBtn.gameObject.SetActive(true);
            if (CurCard.GetHeroRow().getHerotype() == 2)//如果当前英雄是小怪
            {
                ReloadingBtn.gameObject.SetActive(false);
            }
        }

        //英雄属性
        private void InitCardData()
        {
            HeroTemplate heroTemp = CurCard.GetHeroRow();
            //星级
            m_HeroStar = heroTemp.getQuality();
            int maxStar = heroTemp.getMaxQuality();

            //星级品质界面控制
            for (int i = 0; i < 5; i++)
            {
                star[i].enabled = i + 1 <= m_HeroStar;
                starBG[i].enabled = i + 1 <= maxStar;
            }


            //等级显示
            m_HeroLevel = CurCard.GetHeroData().Level;
            HeroLevelTxt.text = m_HeroLevel.ToString();
            //经验条显示
            ExpSlider.value = CurCard.GetHeroData().GetExpPercentage();
            m_ExpSliderImg.enabled = false;
            if (m_HeroLevel >= heroTemp.getMaxLevel())
            {
                m_ExpSliderImg.enabled = true;
            }
            CurCard.UpdateAttributeValue();
            //基础属性
            HpTxt.text = CurCard.GetMaxHP().ToString();
            PhyAtkTxt.text = CurCard.GetPhysicalAttack().ToString();
            ApAtkTxt.text = CurCard.GetMagicAttack().ToString();
            PhyDefenseTxt.text = CurCard.GetPhysicalDefence().ToString();
            ApDefenseTxt.text = CurCard.GetMagicDefence().ToString();
            if ( CurCard.GetHeroRow().getStageUpTargetID()==-1)
            {
                //OrderUpImage.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_xinxi_31");
                GameUtils.SetBtnSpriteGrayState(OrderUp, true);
            }
            else
            {
                //OrderUpImage.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_xinxi_22");
                GameUtils.SetBtnSpriteGrayState(OrderUp, false);
            }

			InitCardDetailedData();
        }
        //初始化英雄详细信息面板Obj
        public void InstantiateCardDetailedData()
        {
            CardDetailedDataList = new List<UI_GoodItem>();
            for (int i = 0; i < CardDetailedDataCount; ++i)
            {
                GameObject temp = Instantiate(Resources.Load(common.prefabPath + "UI_Home/UI_HeroAttributes")) as GameObject;
                temp.transform.SetParent(HeroinfoListLayout, false);
                // temp.AddComponent<UI_GoodItem>();
                CardDetailedDataList.Add(temp.GetComponent<UI_GoodItem>());
            }
        }
        //如果当期选中卡牌是怪物则不显示英雄搭配按钮
        private void InitMatchHeroBtn()
        {
            if (CurCard == null)
                return;

            HeroTemplate _temp = CurCard.GetHeroRow();
            if (_temp == null || _temp.getSkillPair().Length == 0)
                return;
            if (_temp.getSkillPair()[0] == -1)
            {

                DetailInfoBtn.GetComponent<RectTransform>().anchoredPosition = HeroinfoCPos.anchoredPosition;
                HeroMatchBtn.gameObject.SetActive(false);
            }
            else
            {
                DetailInfoBtn.GetComponent<RectTransform>().anchoredPosition = HeroinfoLPos.anchoredPosition;
                HeroMatchBtn.GetComponent<RectTransform>().anchoredPosition = HeroinfoRPos.anchoredPosition;
                HeroMatchBtn.gameObject.SetActive(true);
            }
        }
        //初始化英雄详细信息面板信息
        private void InitCardDetailedData()
        {
            for (int i = 0; i < CardDetailedDataCount; ++i)
            {
                if (CardDetailedDataList != null)
                {
                    CardDetailedDataList[i].InitCardDetailedData(i + 1, CurCard);
                }
            }
        }
        //英雄文字信息
        private void InitHeroDes()
        {
            //称号显示
            HeroNameTxt.text = GameUtils.getString(_HeroItem.getTitleID());
            //名称显示
            PlayerNameTxt.text = GameUtils.getString(_HeroItem.getNameID());
            //描述显示
            HeroDesTxt.text = GameUtils.getString(_HeroItem.getDescriptionID());
        }
        //初始化技能信息
        private void InitSkillInfo()
        {
            int Skill1id = CurCard.GetHeroData().SpellDataList[0].SpellID;
            Gskilltemp = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.getTableData(Skill1id);
            Sprite _img1 = UIResourceMgr.LoadSprite(common.defaultPath + Gskilltemp.getSkillIcon());
            Skill1Image.sprite = _img1;
            Skill1Lv.text = Gskilltemp.getSkillLevel().ToString();

            int Skill2id = CurCard.GetHeroData().SpellDataList[1].SpellID;
            Pskilltemp = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.getTableData(Skill2id);
            Sprite _img2 = UIResourceMgr.LoadSprite(common.defaultPath + Pskilltemp.getSkillIcon());
            Skill2Image.sprite = _img2;
            Skill2Lv.text = Pskilltemp.getSkillLevel().ToString();

            //int Skill3id = CurCard.GetHeroData().SpellDataList[2].SpellID;
            //Askilltemp = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.m_Data[Skill3id];
            //Sprite _img3 = UIResourceMgr.LoadSprite(common.defaultPath + Askilltemp.getSkillIcon());
            //Skill3Image.sprite = _img3;
            //Skill3Lv.text = Askilltemp.getSkillLevel().ToString();

            //判断技能是否开启
            if (!InterfaceControler.GetInst().IsOpenSkill(CurCard.GetHeroRow(), Gskilltemp.getSkillNo()))
            {
                GameUtils.SetImageGrayState(Skill1Image, true);
            }
            else
            {
                GameUtils.SetImageGrayState(Skill1Image, false);
            }
            if (!InterfaceControler.GetInst().IsOpenSkill(CurCard.GetHeroRow(), Pskilltemp.getSkillNo()))
            {
                GameUtils.SetImageGrayState(Skill2Image, true);
            }
            else
            { 
             GameUtils.SetImageGrayState(Skill2Image, false);
            }
           
        }
        //英雄种族等信息
        private void InitHeroTypes()
        {
            if (_HeroItem.getClientSignType()[0] == 0 && _HeroItem.getClientSignType()[1] == 0)//近战物理
            {
                AttackTypeTipsText.text = GameUtils.getString("hero_info_tip1");
                AttackTypeImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_06");
            }
            if (_HeroItem.getClientSignType()[0] == 0 && _HeroItem.getClientSignType()[1] == 1)//近战法术
            {
                AttackTypeTipsText.text = GameUtils.getString("hero_info_tip3");
                AttackTypeImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_05");
            }
            if (_HeroItem.getClientSignType()[0] == 1 && _HeroItem.getClientSignType()[1] == 0)//远程物理
            {
                AttackTypeTipsText.text = GameUtils.getString("hero_info_tip2");
                AttackTypeImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_04");
            }
            if (_HeroItem.getClientSignType()[0] == 1 && _HeroItem.getClientSignType()[1] == 1)//远程法术
            {
                AttackTypeTipsText.text = GameUtils.getString("hero_info_tip4");
                AttackTypeImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_07");
            }
            if (_HeroItem.getClientSignType()[2] == 0)//肉盾
            {
                JobTypeTipsText.text = GameUtils.getString("hero_info_tip7");
                JobTypeImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_02");
            }
            if (_HeroItem.getClientSignType()[2] == 1)//输出
            {
                JobTypeTipsText.text = GameUtils.getString("hero_info_tip5");
                JobTypeImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_03");
            }
            if (_HeroItem.getClientSignType()[2] == 2)//辅助
            {
                JobTypeTipsText.text = GameUtils.getString("hero_info_tip6");
                JobTypeImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_01");
            }
            if (_HeroItem.getCamp() == 1)//生灵
            {
                RaceTypeImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Zhongzu_01");
            }
            if (_HeroItem.getCamp() == 2)//神抵
            {
                RaceTypeImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Zhongzu_03");
            }
            if (_HeroItem.getCamp() == 3)//恶魔
            {
                RaceTypeImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Zhongzu_02");
            }
        }
        //初始化英雄搭配界面信息
        private void InitHeroMatthData()
        {
            for (int i = 0; i < HeroMatchList.Count; ++i)
            {
                Destroy(HeroMatchList[i]);
            }
            HeroMatchList.Clear();
            string _text = GameUtils.getString(CurCard.GetHeroRow().getHeroDes());

            HeroTrait_Text.text = _text;
            int count = CurCard.GetHeroRow().getSkillPair().Length;
            for (int i = 0; i < count; ++i)
            {
                GameObject temp = Instantiate(Resources.Load(common.prefabPath + "UI_Home/UI_SHeroItem")) as GameObject;
                temp.transform.SetParent(HeroMatchListLayout, false);
                HeroMatchList.Add(temp);
                if (!DataTemplate.GetInstance().m_HeroTable.tableContainsKey(CurCard.GetHeroRow().getSkillPair()[i]))
                    continue;
                HeroTemplate _data = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(CurCard.GetHeroRow().getSkillPair()[i]);
                UI_HeroItem uiItem = temp.AddComponent<UI_HeroItem>();
                uiItem.InitHeroMatchCardData(_data, CurCard);
            }
        
           
        }
        //显示通用技能提示
        private void OnGskillTips()
        {
            Skilltipsobj.SetActive(true);
            Skilltipsobj.transform.FindChild("beidong").gameObject.SetActive(false);
            Skilltipsobj.transform.FindChild("Image").gameObject.SetActive(true);
            GSkillImage = Skilltipsobj.transform.FindChild("Image/Image_1").gameObject;
            PSkillImage = Skilltipsobj.transform.FindChild("Image/Image_2").gameObject;
            ASkillImage = Skilltipsobj.transform.FindChild("Image/Image_3").gameObject;
            GSkillImage.SetActive(true);
            PSkillImage.SetActive(false);
            ASkillImage.SetActive(false);

            skillName.transform.localPosition = postion_Big_Title.localPosition;
            skilldec.transform.localPosition = postion_Big_Desc.localPosition;
            UI_SkillTips _tips = new UI_SkillTips(CurCard, Gskilltemp);
            skillxiaohao.transform.parent.parent.gameObject.SetActive(true);
            skillName.text = GameUtils.getString(Gskilltemp.getSkillName()) + "Lv" + Gskilltemp.getSkillLevel();
            skillxiaohao.text = string.Format("<color=#FF0000>{0}</color>{1}", Gskilltemp.getSkillCostNum1(), GetSkillCostString());
            skillCD.text = string.Format("<color=#FF0000>{0}</color>秒", Gskilltemp.getCooldown() / 1000);
            skilldec.text = _tips.GetDesc();
            if( string.IsNullOrEmpty( GetLilimt(Gskilltemp)))
            {
               skillLimitLevel.gameObject.SetActive(false);
            }
            else
            {
                skillLimitLevel.gameObject.SetActive(true);
                skillLimitLevel.text = GetLilimt(Gskilltemp);
            }
            //技能是否开启
            if (!InterfaceControler.GetInst().IsOpenSkill(CurCard.GetHeroRow(), Gskilltemp.getSkillNo()))
            {
                skillLimitLevel.gameObject.SetActive(true);
            }
            else
            {
                skillLimitLevel.gameObject.SetActive(false);
            }
           
            
        }
        private string  GetSkillCostString()
        {
            switch (Gskilltemp.getSkillCostType1())
            {
                case (int)EM_EXTRAITEM_TYPE.EM_EXTRAITEM_MP:
                    return "怒气";
                default:
                    return "";
            }
        }
        //显示被动技能提示
        private void OnPskillTips()
        {
            Skilltipsobj.SetActive(true);
            Skilltipsobj.transform.FindChild("beidong").gameObject.SetActive(true);
            Skilltipsobj.transform.FindChild("Image").gameObject.SetActive(false);
            GSkillImage = Skilltipsobj.transform.FindChild("beidong/Image_1").gameObject;
            PSkillImage = Skilltipsobj.transform.FindChild("beidong/Image_2").gameObject;
            ASkillImage = Skilltipsobj.transform.FindChild("beidong/Image_3").gameObject;
            GSkillImage.SetActive(false);
            PSkillImage.SetActive(true);
            ASkillImage.SetActive(false);
            skillName.transform.localPosition = postion_Small_Title.transform.localPosition;
            skilldec.transform.localPosition = postion_Small_Desc.transform.localPosition;
            UI_SkillTips _tips = new UI_SkillTips(CurCard, Pskilltemp);
            //Skilltipstext.text = _tips.GetDesc();

            skillName.text = GameUtils.getString(Pskilltemp.getSkillName()) + "Lv" + Pskilltemp.getSkillLevel();
            skillxiaohao.transform.parent.parent.gameObject.SetActive(false);
            skilldec.transform.localPosition.Set(skilldec.transform.localPosition.x, -89.1f, skilldec.transform.localPosition.z);
            skilldec.text = _tips.GetDesc();

            if (m_HeroStar < 3)
            {
                string tempText = null;
                ChsTextTemplate temp = (ChsTextTemplate)DataTemplate.GetInstance().m_ChsTextTable.getTableData("hero_info_skill_open1");
                if (temp.languageMap.TryGetValue(AppManager.Inst.GameLanguage, out tempText))
                {
                    Skilltipstext.text += tempText.Replace("\\n", "\n");
                }

            }
            if (string.IsNullOrEmpty(GetLilimt(Pskilltemp)))
            {
                skillLimitLevel.gameObject.SetActive(false);
            }
            else
            {
                skillLimitLevel.gameObject.SetActive(true);
                skillLimitLevel.text = GetLilimt(Pskilltemp);
            }
            if (!InterfaceControler.GetInst().IsOpenSkill(CurCard.GetHeroRow(), Pskilltemp.getSkillNo()))
            {
                skillLimitLevel.gameObject.SetActive(true);
            }
            else
            {
                skillLimitLevel.gameObject.SetActive(false);
            }
          
        }
        string GetLilimt(SkillTemplate skillTemp)
        {
            string str = string.Empty;
            switch (skillTemp.getSkillNo())
            {
                case 0:
                    str = "一";
                    break;
                case 1:
                    str = "二";
                    break;
                case 2:
                    str = "三";
                    break;
                case 3:
                    str = "四";
                    break;
                default:
                    break;
            }
            if (string.IsNullOrEmpty(str))
                return str;
            else
            return string.Format("{0}星英雄开启",str);
        }
        //显示PVP技能提示
        private void OnAskillTips()
        {
            //Skilltipsobj.SetActive(true);
            //Skilltipsobj.SetActive(true);
        }
        //此处要延迟设置背景框体的大小
        private void SetSkillTipsBgSize()
        {
            //SkilltipsbgRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Skilltipstext.GetComponent<RectTransform>().rect.height + SkilltipsBgSizeAmend);
            //SkilltipsbgRect.gameObject.SetActive(true);
        }
        //种族提示
        private void OnRaceTips()
        {
            RaceTipsobj.SetActive(true);
        }
        //职位提示
        private void OnJobTypeTips()
        {
            JobTypeTipsobj.SetActive(true);
        }
        //攻击模式提示
        private void OnAttackTypeTips()
        {
            AttackTypeTipsobj.SetActive(true);
        }
        //英雄搭配按钮
        private void OnHeroMatchBtn()
        {
            float size = HeroTrait_Text.rectTransform.rect.height + 80;
            //HeroMatchDesRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
            //HeroMakeinfo.transform.SetParent(this.transform, false);
            //HeroMakeinfo.transform.localScale = Vector3.one;
            HeroMakeinfo.SetActive(true);
            UI_HeroListManager._instance.SetGridActive(false);
            Herodetailinfo.SetActive(false);
            UI_HeroListManager._instance.GetCard3Dmodel().rigidbody.isKinematic = true;
            CurAngle = UI_HeroListManager._instance.GetCard3Dmodel().transform.rotation;
            UI_HeroListManager._instance.GetCard3Dmodel().transform.rotation = new Quaternion(0, 0, 0, 0);
             m_Make_HeroTitle.text = GameUtils.getString(_HeroItem.getTitleID());
             m_Make_HeroName.text = GameUtils.getString(_HeroItem.getNameID());

            //需要关闭公告条
             UI_CaptionManager cap = UI_CaptionManager.GetInstance();
             if (cap != null)
                 cap.Release(UI_HeroInfo._instance.m_captionPoston);
        }
        //英雄搭配关闭
        private void OnHeroMakeClose_btn()
        {
            //HeroMakeinfo.transform.SetParent(null, false);
            HeroMakeinfo.SetActive(false);
            UI_HeroListManager._instance.SetGridActive(true);
            UI_HeroListManager._instance.GetCard3Dmodel().rigidbody.isKinematic = false;
            UI_HeroListManager._instance.GetCard3Dmodel().transform.rotation = CurAngle;
            //需要打开公告条
            UI_CaptionManager cap = UI_CaptionManager.GetInstance();
            if (cap != null)
                cap.AwakeUp(UI_HeroInfo._instance.m_captionPoston);
        }
        //详细信息开启按钮
        private void OnDetailInfoBtn()
        {
            if (Herodetailinfo.activeSelf)
                Herodetailinfo.SetActive(false);
            else
                Herodetailinfo.SetActive(true);
        }
        //详细信息关闭
        private void OnHeroInfoClose_btn()
        {
            Herodetailinfo.SetActive(false);
        }
        //显示升级界面
        private void OnClickShowLevelUp()
        {
            HLMgr.ShowUI();
            HLMgr.UpdateShow(CurCard);
            UI_HeroListManager._instance.SetGridActive(false);
            UI_HeroListManager._instance.GetCard3Dmodel().rigidbody.isKinematic = true;
            UI_HeroListManager._instance.GetCard3Dmodel().transform.rotation = new Quaternion(0, 0, 0, 0);
            //新手引导相关 点击【升级】
            if (GuideManager.GetInstance().GetBackCount(200502))
            {
                GuideManager.GetInstance().ShowGuideWithIndex(200503);
            }
        }
        private void OnClickShowBeginner()
        {
            if (CurCard.GetHeroRow().getStageUpTargetID() == -1)
            {
                UI_HeroInfo._instance.AddMsgBox("英雄已达到最高品质，不可进阶");
            }
            else
            {
                BeMgr.ShowUI();
                BeMgr.UpdateShow(CurCard);
                UI_HeroListManager._instance.SetGridActive(false);
                UI_HeroListManager._instance.GetCard3Dmodel().rigidbody.isKinematic = true;
                UI_HeroListManager._instance.GetCard3Dmodel().transform.rotation = new Quaternion(0, 0, 0, 0);
                //新手引导相关 点击【进阶】
                if (GuideManager.GetInstance().GetBackCount(200601))
                {
                    GuideManager.GetInstance().ShowGuideWithIndex(200602);
                }
            }
            
        }

        public void ShowUI(bool isFirst)
        {
            this.gameObject.SetActive(true);
            if (isFirst)
                return;
            InitCardData();
            InitSkillInfo();
            InitCardDetailedData();
            Herodetailinfo.SetActive(false);
        }

        public void HideUI()
        {
            this.gameObject.SetActive(false);
            
        }

        //换装按钮
        public void OnReloadingBtn()
        {
            if (CurCard.GetHeroRow().getUseableArtresource().Length == 1)
            {
                string text = GameUtils.getString("fashion_bubble3");
                InterfaceControler.GetInst().AddMsgBox(text, this.transform);
            }
            else
            {
                HsMgr.ShowUI();
                UI_HeroListManager._instance.GetCard3Dmodel().transform.rotation = new Quaternion(0, 0, 0, 0);
                UI_HeroListManager._instance.GetCard3Dmodel().rigidbody.isKinematic = true;
            }

        }
    }


}

