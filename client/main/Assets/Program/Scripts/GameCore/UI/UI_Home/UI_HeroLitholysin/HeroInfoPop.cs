using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using DreamFaction.GameCore;
using DreamFaction.Utils;
using DreamFaction.GameEventSystem;
using DreamFaction.UI;
using GNET;

public class HeroInfoPop : BaseUI
{
	public static HeroInfoPop inst;
	public static string UI_ResPath = "UI_Home/UI_HeroInfoPop_1_3";

	private Text m_TitleTxt;
	private const string m_titleStr = "hero_info_title";
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
	private RectTransform HeroMatchDesRect;                        //英雄搭配背景框
	private RectTransform HeroinfoLPos;                            //英雄信息按钮左侧坐标
	private RectTransform HeroinfoCPos;                            //英雄信息按钮中心坐标
	private RectTransform HeroinfoRPos;                            //英雄信息按钮右侧坐标
	private Button LevelUp_btn;                                    //升级按钮

	private SkillTemplate Gskilltemp;                              //通用技能表数据
	private SkillTemplate Pskilltemp;                              //被动技能表数据
	private SkillTemplate Askilltemp;                              //PVP技能表数据

	private GameObject Skilltipsobj;                               //技能提示
	private RectTransform SkilltipsbgRect;                         //技能提示背景框
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

	private Button _confirm;
	private GameObject Card3Dmodel;                                                      //当前实例化3D模型
	private Transform _Point;                                                            //3D模型实例化位置
    private Transform Card3DmodelRawImage;  

	// 英雄兑换的信息
	protected Text m_FreeTipsText;   // 免费提示
	protected Button m_ExchangeBtn;  // 兑换按钮
	protected Text m_ExchangeText;  // 
	protected Button m_RefreshBtn;  // 刷新按钮
	protected Text m_RefreshText;
	protected Text m_BottomTipsText;
	private GameObject m_UIBottom;  // 英雄兑换的信息
	private Text m_NumTipsText;  // 卷轴数量提示
	private Image m_GoldImage;
    private GameObject m_LeftBottom;

    private GameObject m_bgExchangePanel;//梦想兑换面板
    private Button m_ExchangeBackBtn;
    private Transform m_cappostion; //公告

	// 单次抽奖英雄
	private GameObject m_SingHero;
	private GameObject ModelViewRoom;
	private bool isClone;

	public override void InitUIData()
	{
		//_instance = this;
		base.InitUIData();
		inst = this;
		m_TitleTxt = selfTransform.FindChild("Title/Text").GetComponent<Text>();
		HeroNameTxt = selfTransform.FindChild("HeroInof_LeftUP/HeroName_txt").GetComponent<Text>();
		HeroLevelTxt = selfTransform.FindChild("HeroInof_LeftUP/Level_txt").GetComponent<Text>();
		PlayerNameTxt = selfTransform.FindChild("HeroInof_LeftUP/PlayerName_Img/PlayerName_txt").GetComponent<Text>();
		AttackTypeImg = selfTransform.FindChild("HeroInof_LeftUP/AttackType_Img").GetComponent<Image>();
		RaceTypeImg = selfTransform.FindChild("HeroInof_LeftUP/RaceTypeImg").GetComponent<Image>();
		JobTypeImg = selfTransform.FindChild("HeroInof_LeftUP/JobType_Img").GetComponent<Image>();
		ExpSlider = selfTransform.FindChild("HeroInof_LeftUP/ExpSlider").GetComponent<Slider>();

		HeroDesTxt = selfTransform.FindChild("HeroInfo_Mid/HeroDes_txt").GetComponent<Text>();
		HpTxt = selfTransform.FindChild("HeroInfo_Mid/Info_Hp/InfoValue_txt").GetComponent<Text>();
		PhyAtkTxt = selfTransform.FindChild("HeroInfo_Mid/Info_PhyAttack/InfoValue_txt").GetComponent<Text>();
		ApAtkTxt = selfTransform.FindChild("HeroInfo_Mid/Info_ApAttack/InfoValue_txt").GetComponent<Text>();
		PhyDefenseTxt = selfTransform.FindChild("HeroInfo_Mid/Info_PhyDefense/InfoValue_txt").GetComponent<Text>();
		ApDefenseTxt = selfTransform.FindChild("HeroInfo_Mid/Info_ApDefense/InfoValue_txt").GetComponent<Text>();
		SkillItem_publicBtn = selfTransform.FindChild("HeroInfo_LeftBottom/SkillItem_0").GetComponent<Button>();
		SkillItem_passivityBtn = selfTransform.FindChild("HeroInfo_LeftBottom/SkillItem_1").GetComponent<Button>();
		SkillItem_PVPBtn = selfTransform.FindChild("HeroInfo_LeftBottom/SkillItem_2").GetComponent<Button>();
        m_LeftBottom = selfTransform.FindChild("HeroInfo_LeftBottom").gameObject;
        m_bgExchangePanel = selfTransform.FindChild("mengxiangduihuan").gameObject;
        m_bgExchangePanel.SetActive(false);
        m_ExchangeBackBtn = selfTransform.FindChild("mengxiangduihuan/UI_BG_Top/BackBtn").GetComponent<Button>();
        m_ExchangeBackBtn.onClick.AddListener(OnConfirmClick);
        m_cappostion = selfTransform.FindChild("mengxiangduihuan/cappostion");

		//ReloadingBtn = selfTransform.FindChild("ReloadingBtn").GetComponent<Button>();
		//LevelUp = selfTransform.FindChild("LevelUP_btn").GetComponent<Button>();
		//OrderUp = selfTransform.FindChild("OrderUP_btn").GetComponent<Button>();
		//OrderUpImage = selfTransform.FindChild("OrderUP_btn").GetComponent<Image>();

		Skill1Image = selfTransform.FindChild("HeroInfo_LeftBottom/SkillItem_0/SkillIcon").GetComponent<Image>();
		Skill2Image = selfTransform.FindChild("HeroInfo_LeftBottom/SkillItem_1/SkillIcon").GetComponent<Image>();
		Skill3Image = selfTransform.FindChild("HeroInfo_LeftBottom/SkillItem_2/SkillIcon").GetComponent<Image>();
		Skill1Lv = selfTransform.FindChild("HeroInfo_LeftBottom/SkillItem_0/SkillLevelNumber_txt").GetComponent<Text>();
		Skill2Lv = selfTransform.FindChild("HeroInfo_LeftBottom/SkillItem_1/SkillLevelNumber_txt").GetComponent<Text>();
		Skill3Lv = selfTransform.FindChild("HeroInfo_LeftBottom/SkillItem_2/SkillLevelNumber_txt").GetComponent<Text>();

		//Herodetailinfo = selfTransform.FindChild("Herodetailinfo").gameObject;
		//HeroinfoListLayout = Herodetailinfo.transform.FindChild("HeroList/ListLayOut");
		//HeroInfoClose_btn = Herodetailinfo.transform.FindChild("HeroInfoClose_btn").GetComponent<Button>();
		//Herodetailinfo.SetActive(false);

		//HeroMakeinfo = selfTransform.FindChild("HeroMakeinfo").gameObject;
		//HeroTrait_Text = HeroMakeinfo.transform.FindChild("HeroTrait_Image/HeroTrait_Text").GetComponent<Text>();
		//HeroMatchListLayout = HeroMakeinfo.transform.FindChild("UI_MatchHeroList/ListLayout");
		//HeroMatchDesRect = HeroMakeinfo.transform.FindChild("HeroTrait_Image/Bg_Image").GetComponent<RectTransform>();
		//HeroMakeClose_btn = HeroMakeinfo.transform.FindChild("HeroMakeClose_btn").GetComponent<Button>();
		//HeroMatchList = new List<GameObject>();
		//HeroMakeinfo.transform.SetParent(null, false);//此处没有设置隐藏是因为Text自适应大小有延迟

		HeroinfoLPos = selfTransform.FindChild("HeroInfo_LeftBottom/HeroinfoLPos").GetComponent<RectTransform>();
		HeroinfoCPos = selfTransform.FindChild("HeroInfo_LeftBottom/HeroinfoCPos").GetComponent<RectTransform>();
		HeroinfoRPos = selfTransform.FindChild("HeroInfo_LeftBottom/HeroinfoRPos").GetComponent<RectTransform>();

		//HeroInfoClose_btn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnHeroInfoClose_btn));
		//DetailInfoBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnDetailInfoBtn));
		//HeroMatchBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnHeroMatchBtn));
		//HeroMakeClose_btn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnHeroMakeClose_btn));

		Skilltipsobj = selfTransform.FindChild("Skilltips").gameObject;
		Skilltipsobj.SetActive(false);
		GSkillImage = Skilltipsobj.transform.FindChild("GSkillImage").GetComponent<Image>().gameObject;
		PSkillImage = Skilltipsobj.transform.FindChild("PSkillImage").GetComponent<Image>().gameObject;
		ASkillImage = Skilltipsobj.transform.FindChild("ASkillImage").GetComponent<Image>().gameObject;

		selfTransform.FindChild("HeroInfo_LeftBottom/SkillItem_0").GetComponent<Button>().onClick.AddListener(OnGskillTips);
		selfTransform.FindChild("HeroInfo_LeftBottom/SkillItem_1").GetComponent<Button>().onClick.AddListener(OnPskillTips);
		selfTransform.FindChild("HeroInfo_LeftBottom/SkillItem_2").GetComponent<Button>().onClick.AddListener(OnAskillTips);


        postion_Small_Title = Skilltipsobj.transform.FindChild("postion_Small(title)");
        postion_Small_Desc = Skilltipsobj.transform.FindChild("grid/cell2/postion_Small(desc)");
        postion_Big_Title = Skilltipsobj.transform.FindChild("postion_Big(title)");
        postion_Big_Desc = Skilltipsobj.transform.FindChild("grid/cell2/postion_Big(desc)");

        skillName = Skilltipsobj.transform.FindChild("skillTitle").GetComponent<Text>();
        skillLimitLevel = Skilltipsobj.transform.FindChild("skillTitle/limit").GetComponent<Text>();
        skillxiaohao = Skilltipsobj.transform.FindChild("grid/cell1/skillxiaohao/Text").GetComponent<Text>();
        skillCD = Skilltipsobj.transform.FindChild("grid/cell1/skillcd/Text").GetComponent<Text>();
        skilldec = Skilltipsobj.transform.FindChild("grid/cell2/dec").GetComponent<Text>();

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

		//GameEventDispatcher.Inst.addEventListener(GameEventID.HE_HeroLevelUp, InitCardData);
		//InstantiateCardDetailedData();
		//ShowInfo(ObjectSelf.GetInstance().HeroContainerBag.GetHeroList()[0]);

		_confirm = selfTransform.FindChild("Confirm").GetComponent<Button>();
		_confirm.onClick.AddListener(new UnityEngine.Events.UnityAction(OnConfirmClick));

		m_FreeTipsText = selfTransform.FindChild("UI_Bottom/FreeTipsText").GetComponent<Text>();
		m_ExchangeBtn = selfTransform.FindChild("UI_Bottom/ExchangeBtn").GetComponent<Button>();
		m_ExchangeBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickExchangeBtn));
		m_ExchangeText = selfTransform.FindChild("UI_Bottom/ExchangeBtn/ExchangeText").GetComponent<Text>();
		m_RefreshBtn = selfTransform.FindChild("UI_Bottom/RefreshBtn").GetComponent<Button>();
		m_RefreshBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickRefreshBtn));
		m_RefreshText = selfTransform.FindChild("UI_Bottom/RefreshBtn/RefreshText").GetComponent<Text>();
		m_BottomTipsText = selfTransform.FindChild("UI_Bottom/BottonTips/BottomTipsText").GetComponent<Text>();
		m_UIBottom = selfTransform.FindChild("UI_Bottom").gameObject;
		m_GoldImage = selfTransform.FindChild("UI_Bottom/GoldImage").GetComponent<Image>();
		m_UIBottom.SetActive(false);
		m_GoldImage.gameObject.SetActive(false);

		m_SingHero = selfTransform.FindChild("UI_SingHero").gameObject;
		m_SingHero.SetActive(false);
		m_NumTipsText = selfTransform.FindChild("UI_Bottom/NumTipsText").GetComponent<Text>();
        Card3DmodelRawImage = selfTransform.FindChild("RawImage").GetComponent<RectTransform>();
        if (GameObject.Find("ModelViewRoom/posPopUp") != null)
        {
            //3D旋转 ModelViewRoom(Clone)
            _Point = GameObject.Find("ModelViewRoom/posPopUp").transform;

        }

         // 新手引导相关
        if (GuideManager.GetInstance().isGuideUser && GuideManager.GetInstance().IsContentGuideID(100203) == false)
        {
            InitGuideGetHero();
        }
	}

    // 引导得到英雄
    void InitGuideGetHero()
    {
        GuideManager.GetInstance().ShowGuideWithIndex(100203);
    }

	public void Show3DModel(ObjectCard _card)
	{
		ModelCear();

		if (_Point == null)
		{
            //Vector3 _pos = gameObject.transform.parent.parent.Find("UI_Camera1").GetComponent<Camera>().ScreenToWorldPoint(_confirm.transform.localPosition);
            Vector3 _pos = new Vector3(5000,0,0);
            ModelViewRoom = Instantiate(Resources.Load("UI/Prefabs/UI_Home/MainHome/ModelViewRoom"), _pos, Quaternion.identity) as GameObject;
            ModelViewRoom.name = "ModelViewRoom";
            _Point = ModelViewRoom.transform.FindChild("posPopUp");
            _Point.localPosition = new Vector3 (_Point.localPosition.x + 0.2f,_Point.localPosition.y,_Point.localPosition.z);
		}
        SetCameraActive(true);
		//通过英雄数据表中的资源数据表ID得到资源表数据
		ArtresourceTemplate _Artresourcedata = new ArtresourceTemplate();
		_Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(_card.GetHeroData().GetHeroViewID());
		//通过资源表获取到角色默认美术资源（名称）     通过该名称获取到动态加载数据返回一个对象
		GameObject _AssetRes = AssetLoader.Inst.GetAssetRes(_Artresourcedata.getArtresources());
		//Debug.Log(_Artresourcedata.getArtresources());
		//实例化该对象
        if (_AssetRes != null)
        {
            Card3Dmodel = Instantiate(_AssetRes, _Point.position, _Point.rotation) as GameObject;
            float _zoom = _Artresourcedata.getArtresources_zoom();
            Card3Dmodel.transform.localScale = new UnityEngine.Vector3(_zoom, _zoom, _zoom);
            Card3Dmodel.transform.parent = _Point;
            //设置3D模型摩擦力
            Card3Dmodel.rigidbody.angularDrag = 1;
            Card3Dmodel.rigidbody.mass = 1;
            //_obj.transform.localScale = new Vector3(1.3f,1.3f,1.3f);
            Animation anim = Card3Dmodel.GetComponent<Animation>();
            if (anim == null)
                return;
            Card3Dmodel.GetComponent<Animation>().Play("Nidle1");
            Card3Dmodel.GetComponent<Animation>().wrapMode = WrapMode.Loop;
        }
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

	public void SetShowData(HeroTemplate heroT, int level = 1)
	{
		ObjectCard obj = new ObjectCard();
		Hero hero = new Hero();
		hero.skill1 = heroT.getSkill1ID();
		hero.skill2 = heroT.getSkill2ID();
		hero.skill3 = heroT.getSkill3ID();
		hero.heroid = heroT.getId();
		hero.herolevel = level;
		hero.heroviewid = heroT.getArtresources();
		obj.GetHeroData().Init(hero);
		//HeroInfoPop.inst.Show3DModel(obj);

		ShowInfo(obj);

		Show3DModel(obj);
	}

	public void ShowInfo(ObjectCard objHero)
	{
		CurCard = objHero;
		_HeroItem = CurCard.GetHeroRow();

		InitCardData();
		InitSkillInfo();
		InitHeroDes();
		InitHeroTypes();
		//InitCardDetailedData();
		//InitMatchHeroBtn();
		//InitHeroMatthData();
		//UI_HeroListManager._instance.Show3DModel(CurCard);
		//GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_HeroChangeTarget, CurCard);
		//InitHeroSkinBtn();
	}

	void InitHeroSkinBtn()
	{
		if (CurCard.GetHeroRow().getUseableArtresource().Length == 1)
		{
			ReloadingBtn.GetComponent<Image>().color = Color.gray;
		}
		else
		{
			ReloadingBtn.GetComponent<Image>().color = Color.white;
		}
	}

	//英雄属性
	private void InitCardData()
	{
		//星级
		m_HeroStar = _HeroItem.getQuality();
		int maxStar = _HeroItem.getMaxQuality();
		for (int i = 5; i < 10; ++i)
		{

			selfTransform.FindChild("HeroInof_LeftUP/Stars").GetChild(i).GetComponent<Image>().enabled = i < 5 + m_HeroStar;
			selfTransform.FindChild("HeroInof_LeftUP/Stars").GetChild(i - 5).GetComponent<Image>().enabled = i < 5 + maxStar;
			//if (i < 5 + m_HeroStar)
			//{
			//    Image temp = selfTransform.FindChild("HeroInof_LeftUP/Stars").GetChild(i).GetComponent<Image>();
			//    temp.enabled = true;
			//}
			//else
			//{

			//    Image temp = selfTransform.FindChild("HeroInof_LeftUP/Stars").GetChild(i).GetComponent<Image>();
			//    temp.enabled = false;
			//}
		}
		//等级显示
		m_HeroLevel = CurCard.GetHeroData().Level;
		HeroLevelTxt.text = m_HeroLevel.ToString();
		//经验条显示
		ExpSlider.value = CurCard.GetHeroData().GetExpPercentage();
		//基础属性
		HpTxt.text = CurCard.GetMaxHP().ToString();
		PhyAtkTxt.text = CurCard.GetPhysicalAttack().ToString();
		ApAtkTxt.text = CurCard.GetMagicAttack().ToString();
		PhyDefenseTxt.text = CurCard.GetPhysicalDefence().ToString();
		ApDefenseTxt.text = CurCard.GetMagicDefence().ToString();
		//if (CurCard.GetHeroRow().getQuality() == CurCard.GetHeroRow().getMaxQuality())
		//{
		//	OrderUpImage.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_xinxi_31", typeof(Sprite)) as Sprite;
		//}
		//else
		//{
		//	OrderUpImage.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_xinxi_22", typeof(Sprite)) as Sprite;
		//}
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
		}
	}
	//初始化英雄详细信息面板信息
	private void InitCardDetailedData()
	{
		for (int i = 0; i < CardDetailedDataCount; ++i)
		{
			CardDetailedDataList[i].InitCardDetailedData(i + 1, CurCard);
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

	/// <summary>
	/// 修改标题
	/// </summary>
	/// <param name="titleText"></param>
	public void SetTitleText(string titleText)
	{
		m_TitleTxt.text = titleText;
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
		string _text = CurCard.GetHeroRow().getHeroDes();
		HeroTrait_Text.text = GameUtils.getString(_text);
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

		//Invoke("SetSkillTipsBgSize", 0.02f);
        skillName.transform.localPosition = postion_Big_Title.localPosition;
        skilldec.transform.localPosition = postion_Big_Desc.localPosition;
        UI_SkillTips _tips = new UI_SkillTips(CurCard, Gskilltemp);
        skillxiaohao.transform.parent.parent.gameObject.SetActive(true);
        skillName.text = GameUtils.getString(Gskilltemp.getSkillName()) + "Lv" + Gskilltemp.getSkillLevel();
        skillxiaohao.text = string.Format("<color=#FF0000>{0}</color>{1}", Gskilltemp.getSkillCostNum1(), GetSkillCostString());
        skillCD.text = string.Format("<color=#FF0000>{0}</color>秒", Gskilltemp.getCooldown() / 1000);
        skilldec.text = _tips.GetDesc();
        if (string.IsNullOrEmpty(GetLilimt(Gskilltemp)))
        {
            skillLimitLevel.gameObject.SetActive(false);
        }
        else
        {
            skillLimitLevel.gameObject.SetActive(true);
            skillLimitLevel.text = GetLilimt(Gskilltemp);
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
               // Skilltipstext.text += tempText.Replace("\\n", "\n");
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
            return string.Format("{0}星英雄开启", str);
    }
    private string GetSkillCostString()
    {
        switch (Gskilltemp.getSkillCostType1())
        {
            case (int)EM_EXTRAITEM_TYPE.EM_EXTRAITEM_MP:
                return "怒气";
            default:
                return "";
        }
    }
	//显示PVP技能提示
	private void OnAskillTips()
	{
		//Skilltipsobj.SetActive(true);
	}
	//此处要延迟设置背景框体的大小
	private void SetSkillTipsBgSize()
	{
		SkilltipsbgRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Skilltipstext.GetComponent<RectTransform>().rect.height + SkilltipsBgSizeAmend);
		SkilltipsbgRect.gameObject.SetActive(true);
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
		HeroMatchDesRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
		//HeroMakeinfo.transform.SetParent(this.transform, false);
		//HeroMakeinfo.transform.localScale = Vector3.one;
		//HeroMakeinfo.SetActive(true);
		UI_HeroListManager._instance.SetGridActive(false);
		//Herodetailinfo.SetActive(false);
		UI_HeroListManager._instance.GetCard3Dmodel().rigidbody.isKinematic = true;
		CurAngle = UI_HeroListManager._instance.GetCard3Dmodel().transform.rotation;
		UI_HeroListManager._instance.GetCard3Dmodel().transform.rotation = new Quaternion(0, 0, 0, 0);
	}
	//英雄搭配关闭
	private void OnHeroMakeClose_btn()
	{
		//HeroMakeinfo.transform.SetParent(null, false);
		UI_HeroListManager._instance.SetGridActive(true);
		UI_HeroListManager._instance.GetCard3Dmodel().rigidbody.isKinematic = false;
		UI_HeroListManager._instance.GetCard3Dmodel().transform.rotation = CurAngle;
	}
	//详细信息开启按钮
	private void OnDetailInfoBtn()
	{
		//if (Herodetailinfo.activeSelf)
		//	Herodetailinfo.SetActive(false);
		//else
		//	Herodetailinfo.SetActive(true);
	}
	//详细信息关闭
	private void OnHeroInfoClose_btn()
	{
		//Herodetailinfo.SetActive(false);
	}

	public void ShowUI(bool isFirst)
	{
		this.gameObject.SetActive(true);
		if (isFirst)
			return;
		InitCardData();
		InitSkillInfo();
		InitCardDetailedData();
		//Herodetailinfo.SetActive(false);
	}

	public void HideUI()
	{
		this.gameObject.SetActive(false);

	}

	public void SetIsClone(bool isClone)
	{
		this.isClone = isClone;
	}

	void OnConfirmClick()
	{
        if (UI_CaptionManager.GetInstance() != null)
        {
            UI_CaptionManager.GetInstance().Release(m_cappostion);
        }
		ModelCear();
		if (UI_HomeControler.Inst != null)
		{
			UI_HomeControler.Inst.ReMoveUI(gameObject);
		}
		else
		{
			UI_FightControler.Inst.ReMoveUI(gameObject);
		}
		if (isClone)
		{
			UI_HomeControler.Inst.AddUI(UI_HeroCloneManager.UI_ResPath);
			isClone = false;
		}

        // 关于新手引导
        // TODO...
        // DEBUG...
        if (GuideManager.GetInstance().isGuideUser && GuideManager.GetInstance().IsContentGuideID(100204) == false  && GuideManager.GetInstance().GetBackCount(100203) == true && GuideManager.GetInstance().GetLastID() < 100204)
        {
            int interruptId = GuideManager.GetInstance().interruptID;
            if(interruptId != 100203)
                UI_Recruit.inst.InitGuideBack();
        }
	}

	private void ModelCear()
	{
		if (Card3Dmodel != null)
			Destroy(Card3Dmodel);
        SetCameraActive(false);
	}

	// 单次抽奖
	public void SetSingGainHero()
	{
		SkillItem_publicBtn.gameObject.SetActive(false);
		SkillItem_passivityBtn.gameObject.SetActive(false);
		SkillItem_PVPBtn.gameObject.SetActive(false);
        m_LeftBottom.SetActive(false);
		m_SingHero.SetActive(true);

		string number = string.Format(GameUtils.getString("recruit_bubble1"), 1);
        RichText richText = RichText.GetRichText(number);
        InterfaceControler.GetInst().AddMsgBox(richText.transform, selfTransform.transform.parent);
	}
	
	//////////////////////////  英雄兑换
	public void SetExchangeHero()
	{
		SkillItem_publicBtn.gameObject.SetActive(false);
		SkillItem_passivityBtn.gameObject.SetActive(false);
		SkillItem_PVPBtn.gameObject.SetActive(false);
		_confirm.gameObject.SetActive(false);
        m_LeftBottom.SetActive(false);
		m_UIBottom.SetActive(true);
        m_bgExchangePanel.SetActive(true);

        if (UI_CaptionManager.GetInstance() != null)
        {
            UI_CaptionManager.GetInstance().AwakeUp(m_cappostion);
        }

		if (ObjectSelf.GetInstance().dreamfree == 0)
		{
			m_FreeTipsText.text = GameUtils.getString("hero_recruit_content11");
			m_GoldImage.gameObject.SetActive(false);
		}
		else
		{
			m_FreeTipsText.text = "X" + DataTemplate.GetInstance().m_GameConfig.getDream_refresh_cost();
			m_GoldImage.gameObject.SetActive(true);
		}
	}

	protected void OnClickExchangeBtn()
	{
		CGetDream proto = new CGetDream();
		IOControler.GetInstance().SendProtocol(proto);
	}

	protected void OnClickRefreshBtn()
	{
		// 金币不足
		int cost = DataTemplate.GetInstance().m_GameConfig.getDream_refresh_cost();
		if (UI_HeroRecruit.inst.TipsGoldValue(cost))
		{
			return;
		}

		CChangeDream proto = new CChangeDream();
		IOControler.GetInstance().SendProtocol(proto);
	}

	// 成功刷新英雄
	public void SuccessRefresh(int dream)
	{
        HeroTemplate temp = ( HeroTemplate ) DataTemplate.GetInstance ().m_HeroTable.getTableData ( dream );
        HerorecruitTemplate retemp = ( HerorecruitTemplate ) DataTemplate.GetInstance ().m_HeroRecruitTable.getTableData ( dream );
        HeroInfoPop.inst.SetShowData ( temp, retemp.getHerolevel () );
        HeroInfoPop.inst.SetExchangeHero ();

        string number = string.Format ( GameUtils.getString ( "recruit_bubble2" ), 1 );
        InterfaceControler.GetInst ().AddMsgBox ( number, HeroInfoPop.inst.selfTransform.transform.parent );
    }
    
	// 成功兑换英雄
	public void SuccessExchangeHero()
	{
		ObjectSelf.GetInstance().isTipsDream = true;
		// 删除掉本界面
		ModelCear();
        if (UI_CaptionManager.GetInstance() != null)
        {
            UI_CaptionManager.GetInstance().Release(m_cappostion);
        }
		UI_HeroRecruit.inst.RefreshDreamValue();
		UI_HomeControler.Inst.ReMoveUI(gameObject);
	}

    private void SetCameraActive(bool value)
    {
        if (_Point != null)
        {
            _Point.FindChild("CameraPopUp").gameObject.SetActive(value);
            _Point.FindChild("EffectsCameraPopUp").gameObject.SetActive(value);
        }
    }
    public void OnDestroy()
    {
        SetCameraActive(false);
    }
}
