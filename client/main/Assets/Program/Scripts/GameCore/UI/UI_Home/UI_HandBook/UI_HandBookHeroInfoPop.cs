using UnityEngine;
using System.Collections;
using DreamFaction.Utils;
using DreamFaction.UI;
using UnityEngine.UI;
using GNET;
using DreamFaction.GameCore;
using DreamFaction.UI.Core;
using System.Collections.Generic;
using System.Text;
using DreamFaction.GameNetWork.Data;
using DreamFaction.GameNetWork;
using UnityEngine.Events;


public class UI_HandBookHeroInfoPop : UI_HandBookHeroInfoPopBase 
{
    public static string UI_ResPath = "UI_Home/UI_HandBook_HeroInfo_1_2";
    public static UI_HandBookHeroInfoPop Inst;

    private ArtresourceTemplate m_Artresourcedata = null;            //资源数据表
    private HeroTemplate m_HeroData = null;                          //英雄数据表
    private SkillTemplate m_Gskilltemp = null;                       //通用技能表数据
    private SkillTemplate m_Pskilltemp = null;                       //被动技能表数据
    private SkillTemplate Askilltemp = null;                         //PVP技能表数据
    private ObjectCard m_Card = null;                                //当前卡牌
    private int[] m_ArtResArray = null;                              //英雄皮肤组
    private int m_heroShowID = 0;                                    //英雄当前皮肤ID

    private Image m_AttackTypeIcon = null;                           //攻击类型图标（远程近战）
    private Image m_RaceTypeIcon = null;                             //种族图标
    private Image m_JobTypeIcon = null;                              //战斗类型图标
    private Transform m_Shars = null;                                //英雄星级父节点
    private Slider m_ExpBar = null;                                  //经验条
    private Image m_SkillImg1 = null;                                //技能图标1
    private Image m_SkillImg2 = null;                                //技能图标1
    private Image m_SkillImg3 = null;                                //技能图标1
    private Transform m_Point = null;                                //3D模型实例化位置
    private GameObject m_Card3Dmodel = null;                         //当前实例化3D模型
    private GameObject m_RaceTipsobj = null;                         //种族tips对象
    private GameObject m_JobTypeTipsobj = null;                      //远近类型Tips对象
    private GameObject m_AttackTypeTipsobj = null;                   //攻击类型Tips对象
    private GameObject m_Skilltipsobj = null;                        //技能Tips对象
    private RectTransform m_SkilltipsbgRect = null;                  //技能Tips背景
    private Image m_GSkillImage = null;                              //通用技能三角
    private Image m_PSkillImage = null;                              //被动技能三角
    private Image m_ASkillImage = null;                              //PVP技能三角
    private int m_SkilltipsBgSizeAmend = 40;                         //技能提示背景框的修正值
    //英雄搭配界面UI
    private GameObject m_HeroMakeinfo = null;                        //英雄搭配Obj
    private List<GameObject> HeroMatchList = new List<GameObject>(); //英雄搭配显示卡牌
    private Text m_HeroTrait_Text = null;                            //英雄特点描述
    private Transform m_HeroMatchListLayout = null;                  //搭配英雄卡牌父节点
    private Text m_HeroMatchName = null;                             //英雄名称
    private Text m_HeroMatchTitle = null;                            //英雄标题
    private Button m_HeroMakeClose_btn = null;                       //英雄搭配关闭按钮
    //获得途径界面UI 
    private GameObject m_GetApprochObj = null;                         //获得途径界面
    private Text m_DesTxt = null;                                      //描述文本
    private Button m_OkBtn = null;                                     //确定按钮
    private Text m_TilteTxt = null;                                    //标题文本
    //进阶信息界面UI
    private GameObject m_HeroAdvanceObj = null;                        //进阶信息界面
    private List<HeroTemplate> m_HeroAdvances = new List<HeroTemplate>(); //进阶的英雄组
    private Transform m_Group = null;                                  //进阶信息实例化模型父节点
    private Button m_HeroAdvanceCloseBtn = null;                       //进阶信息关闭按钮
    private Text m_ACosNumTxt1 = null;                                 //进阶消耗数量1
    private Text m_ACosNumTxt2 = null;                                 //进阶消耗数量2
    private Image m_ACosIcon1 = null;                                  //消耗图标1
    private Image m_ACosIcon2 = null;                                  //消耗图标2
    private Text m_ANameAndTilteTxt = null;                            //名字加称号
    public List<Transform> m_SharsList;                                //星级父节点集合
    public List<GameObject> m_SharsBg;                                 //星级背景组
    //模型旋转
    public bool iSRotate = false;                                      //3D模型旋转开关
    private float m_Card3DRoteh = 0 ;                                  //3D模型旋转参数
    private float m_Card3DRotev = 0;                                   //3D模型旋转参数
    private Vector3 m_Torque;                                          //旋转力数值
    private GameObject m_ModelRotaeBtn = null;                         //3D模型旋转按钮

    private Text skillName;                                          //技能名称+技能等级
    private Text skillLimitLevel;                                   //技能等级限制
    private Text skillxiaohao;                                       //技能消耗怒气
    private Text skillCD;                                            //技能cd
    private Text skilldec;                                        //技能描述
    private Transform postion_Small_Title;                        //切换到被动技能时 标题的位置
    private Transform postion_Small_Desc;                         //切换到被动技能时 描述的位置
    private Transform postion_Big_Title;                          //切换到通用技能时 标题的位置
    private Transform postion_Big_Desc;                           //切换到通用技能时 描述的位置
    private GameObject Skilltipsobj;                               //技能提示
    private GameObject GSkillImage;                                     //通用技能提示图片
    private GameObject PSkillImage;                                     //被动技能提示图片
    private GameObject ASkillImage;                                     //PVP技能提示图片

    public override void InitUIData()
    {
        base.InitUIData();
        Inst = this;
        m_AttackTypeIcon = m_AttackType_Img.GetComponent<Image>();
        m_RaceTypeIcon = m_RaceTypeImg.GetComponent<Image>();
        m_JobTypeIcon = m_JobType_Img.GetComponent<Image>();
        m_Shars = selfTransform.FindChild("HeroInfo/HeroInof_LeftUP/Stars");
        m_ExpBar = selfTransform.FindChild("HeroInfo/HeroInof_LeftUP/ExpSlider").GetComponent<Slider>();
        m_SkillImg1 = selfTransform.FindChild("HeroInfo/HeroInfo_LeftBottom/SkillItem_0/SkillIcon").GetComponent<Image>();
        m_SkillImg2 = selfTransform.FindChild("HeroInfo/HeroInfo_LeftBottom/SkillItem_1/SkillIcon").GetComponent<Image>();
        m_SkillImg3 = selfTransform.FindChild("HeroInfo/HeroInfo_LeftBottom/SkillItem_2/SkillIcon").GetComponent<Image>();
        m_RaceTipsobj = selfTransform.FindChild("HeroInfo/RaceTips").gameObject;
        m_JobTypeTipsobj = selfTransform.FindChild("HeroInfo/JobTypeTips").gameObject;
        m_AttackTypeTipsobj = selfTransform.FindChild("HeroInfo/AttackTypeTips").gameObject;
        m_HeroMakeinfo = selfTransform.FindChild("HeroInfo/HeroMakeinfo").gameObject;
        m_GetApprochObj = selfTransform.FindChild("HeroInfo/GetApprochObj").gameObject;
        m_HeroAdvanceObj = selfTransform.FindChild("HeroInfo/HeroAdvanceObj").gameObject;
        //英雄搭配界面UI
        m_HeroTrait_Text = m_HeroMakeinfo.transform.FindChild("HeroTrait_Image/HeroTrait_Text").GetComponent<Text>();
        m_HeroMatchListLayout = m_HeroMakeinfo.transform.FindChild("UI_MatchHeroList/ListLayout");
        m_HeroMatchName = m_HeroMakeinfo.transform.FindChild("name/Text").GetComponent<Text>();
        m_HeroMatchTitle = m_HeroMakeinfo.transform.FindChild("name").GetComponent<Text>();
        m_HeroMakeClose_btn = m_HeroMakeinfo.transform.FindChild("HeroMakeClose_btn").GetComponent<Button>();
        m_HeroMakeClose_btn.onClick.AddListener(new UnityAction(OnClickMakeClose_btn));
        //获得途径界面UI
        m_DesTxt = m_GetApprochObj.transform.FindChild("DesTxt").GetComponent<Text>();
        m_TilteTxt = m_GetApprochObj.transform.FindChild("TextPop/TilteImg/Text").GetComponent<Text>();
        m_OkBtn = m_GetApprochObj.transform.FindChild("OkBtn").GetComponent<Button>();
        m_OkBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickOKBtn));
        //进阶信息界面
        m_ANameAndTilteTxt = m_HeroAdvanceObj.transform.FindChild("NameAndTilteTxt").GetComponent<Text>();
        m_ACosNumTxt1 = m_HeroAdvanceObj.transform.FindChild("CosNumTxt1").GetComponent<Text>();
        m_ACosNumTxt2 = m_HeroAdvanceObj.transform.FindChild("CosNumTxt2").GetComponent<Text>();
        m_ACosIcon1 = m_HeroAdvanceObj.transform.FindChild("CosIcon1").GetComponent<Image>();
        m_ACosIcon2 = m_HeroAdvanceObj.transform.FindChild("CosIcon2").GetComponent<Image>();
        m_HeroAdvanceCloseBtn = m_HeroAdvanceObj.transform.FindChild("HeroAdvClose_btn").GetComponent<Button>();
        m_HeroAdvanceCloseBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickHeroAdvanceCloseBtn));

        m_Group = GameObject.Find("AdvanceInfoViewRoom/Group").transform;
        m_Point = GameObject.Find("pos").transform;
        m_ModelRotaeBtn = selfTransform.FindChild("HeroInfo/ModelRotaeBtn").gameObject;
        EventTriggerListener.Get(m_ModelRotaeBtn).onDown = OnRotateDown;
        EventTriggerListener.Get(m_ModelRotaeBtn).onUp = OnRotatUp;

        Skilltipsobj = selfTransform.FindChild("HeroInfo/Skilltips").gameObject;
        Skilltipsobj.SetActive(false);

        skillName = Skilltipsobj.transform.FindChild("skillTitle").GetComponent<Text>();
        skillLimitLevel = Skilltipsobj.transform.FindChild("skillTitle/limit").GetComponent<Text>();
        skillxiaohao = Skilltipsobj.transform.FindChild("grid/cell1/skillxiaohao/Text").GetComponent<Text>();
        skillCD = Skilltipsobj.transform.FindChild("grid/cell1/skillcd/Text").GetComponent<Text>();
        skilldec = Skilltipsobj.transform.FindChild("grid/cell2/dec").GetComponent<Text>();

        postion_Small_Title = Skilltipsobj.transform.FindChild("postion_Small(title)");
        postion_Small_Desc = Skilltipsobj.transform.FindChild("grid/cell2/postion_Small(desc)");
        postion_Big_Title = Skilltipsobj.transform.FindChild("postion_Big(title)");
        postion_Big_Desc = Skilltipsobj.transform.FindChild("grid/cell2/postion_Big(desc)");

        captionPath = "HeroInfo/caption";
        HeroModelBack.Inst.ChangePanel("HeroInfo");
    }

    public override void UpdateUIView()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Skilltipsobj.SetActive(false);
            m_RaceTipsobj.SetActive(false);
            m_JobTypeTipsobj.SetActive(false);
            m_AttackTypeTipsobj.SetActive(false);
        }
        if (iSRotate)
        {
            m_Card3DRoteh = Input.GetAxis("Mouse X");//有正左负
            m_Card3DRotev = Input.GetAxis("Mouse Y");//上正下负
        }
        else
        {
            m_Card3DRoteh = 0;
            m_Card3DRotev = 0;
        }
        m_Torque = new Vector3(m_Card3DRotev, -m_Card3DRoteh, 0);
    }

    private void FixedUpdate()
    {
        if (m_Card3Dmodel != null)
        {
            m_Card3Dmodel.rigidbody.AddTorque(m_Torque * 10);
        }
    }

    private void OnRotateDown(GameObject a)
    {
        iSRotate = true;
        m_Card3Dmodel.rigidbody.isKinematic = false;
    }
    private void OnRotatUp(GameObject a)
    {
        iSRotate = false;
    }

    /// <summary>
    /// 显示数据
    /// </summary>
    /// <param name="heroData"></param>
    /// <param name="level"></param>
    public void ShowInfo(HeroTemplate heroData)
    {
        m_HeroData = heroData;
        ShowData();
        Show3DModel(m_Card);
        InitSkinBtnIsGary();
        ShowHeroSkinName();

        ShowHeroInfoText();
        ShowTypeImgAndTips();
        InitHeroMatthData();
        InitGetAppData();
        InitShowOrderUpInfo();
    }
    /// <summary>
    /// 显示等级 经验 技能刷新
    /// </summary>
    /// <param name="heroData"></param>
    /// <param name="level"></param>
    private void ShowData(int level = 1)
    {        
        ObjectCard _card = new ObjectCard();
        Hero hero = new Hero();
        hero.skill1 = m_HeroData.getSkill1ID();
        hero.skill2 = m_HeroData.getSkill2ID();
        hero.skill3 = m_HeroData.getSkill3ID();
        hero.heroid = m_HeroData.getId();
        hero.herolevel = level;
        hero.heroviewid = m_HeroData.getArtresources();
        _card.GetHeroData().Init(hero);
        m_Card = _card;

        ShowHeroLevelAndExp(_card);
        InitSkillInfo(_card);
        UpBtnGrayShow();
    }


    /// <summary>
    /// 英雄信息基本文本描述/星级显示
    /// </summary>
    private void ShowHeroInfoText()
    {
        //称号显示
        m_HeroName_txt.text = GameUtils.getString(m_HeroData.getTitleID());
        //名称显示
        m_PlayerName_txt.text = GameUtils.getString(m_HeroData.getNameID());
        //描述显示
        m_HeroDes_txt.text = GameUtils.getString(m_HeroData.getDescriptionID());
        //星级品质界面控制
        InterfaceControler.GetInst().AddSharLevel(m_Shars, m_HeroData);
    }
    /// <summary>
    /// 英雄类型图标/Tips数据
    /// </summary>
    /// m_Text_5：攻击类型Tipe文本
    /// m_Text_6：远程or进程Tips文本
    private void ShowTypeImgAndTips()
    {
        if (m_HeroData.getClientSignType()[0] == 0 && m_HeroData.getClientSignType()[1] == 0)//近战物理
        {
            m_Text_5.text = GameUtils.getString("hero_info_tip1");//
            m_AttackTypeIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_06");
        }
        if (m_HeroData.getClientSignType()[0] == 0 && m_HeroData.getClientSignType()[1] == 1)//近战法术
        {
            m_Text_5.text = GameUtils.getString("hero_info_tip3");
            m_AttackTypeIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_05");
        }
        if (m_HeroData.getClientSignType()[0] == 1 && m_HeroData.getClientSignType()[1] == 0)//远程物理
        {
            m_Text_5.text = GameUtils.getString("hero_info_tip2");
            m_AttackTypeIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_04");
        }
        if (m_HeroData.getClientSignType()[0] == 1 && m_HeroData.getClientSignType()[1] == 1)//远程法术
        {
            m_Text_5.text = GameUtils.getString("hero_info_tip4");
            m_AttackTypeIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_07");
        }
        if (m_HeroData.getClientSignType()[2] == 0)//肉盾
        {
            m_Text_6.text = GameUtils.getString("hero_info_tip7");
            m_JobTypeIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_02");
        }
        if (m_HeroData.getClientSignType()[2] == 1)//输出
        {
            m_Text_6.text = GameUtils.getString("hero_info_tip5");
            m_JobTypeIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_03");
        }
        if (m_HeroData.getClientSignType()[2] == 2)//辅助
        {
            m_Text_6.text = GameUtils.getString("hero_info_tip6");
            m_JobTypeIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_01");
        }
        if (m_HeroData.getCamp() == 1)//生灵
        {
            m_RaceTypeIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Zhongzu_01");
        }
        if (m_HeroData.getCamp() == 2)//神抵
        {
            m_RaceTypeIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Zhongzu_03");
        }
        if (m_HeroData.getCamp() == 3)//恶魔
        {
            m_RaceTypeIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Zhongzu_02");
        }
    }

    /// <summary>
    /// 英雄属性显示
    /// </summary>
    /// m_InfoValue_txt 气血的值
    /// m_InfoValue_txt_1 物理攻击的值
    /// m_InfoValue_txt_2 法术攻击的值
    /// m_InfoValue_txt_3 物理防御的值
    /// m_InfoValue_txt_4 法术防御的值
    private void ShowHeroLevelAndExp(ObjectCard card)
    {
        //m_HeroMake_btn.gameObject.SetActive(!(m_HeroData.getHeroDes() == "-1.0"));

        //等级显示
        if (card.GetHeroData().Level > m_HeroData.getMaxLevel())
            m_Level_txt.text = m_HeroData.getMaxLevel().ToString();
        else if (card.GetHeroData().Level <= 1)
            m_Level_txt.text = "1";
        else
            m_Level_txt.text = card.GetHeroData().Level.ToString();
        //经验条显示
        //m_ExpBar.value = CurCard.GetHeroData().GetExpPercentage();
        //基础属性
        m_InfoValue_txt.text = card.GetMaxHP().ToString();
        m_InfoValue_txt_1.text = card.GetPhysicalAttack().ToString();
        m_InfoValue_txt_2.text = card.GetMagicAttack().ToString();
        m_InfoValue_txt_3.text = card.GetPhysicalDefence().ToString();
        m_InfoValue_txt_4.text = card.GetMagicDefence().ToString();
    }

    /// <summary>
    /// 显示技能信息
    /// </summary>
    /// m_SkillLevelNumber_txt   技能1的等级
    /// m_SkillLevelNumber_txt_1 技能2的等级
    private void InitSkillInfo(ObjectCard card)
    {
        int Skill1id = card.GetHeroData().SpellDataList[0].SpellID;
        m_Gskilltemp = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.getTableData(Skill1id);
        m_SkillImg1.sprite = UIResourceMgr.LoadSprite(common.defaultPath + m_Gskilltemp.getSkillIcon()); ;
        m_SkillLevelNumber_txt.text = m_Gskilltemp.getSkillLevel().ToString();

        int Skill2id = card.GetHeroData().SpellDataList[1].SpellID;
        m_Pskilltemp = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.getTableData(Skill2id);
        m_SkillImg2.sprite = UIResourceMgr.LoadSprite(common.defaultPath + m_Pskilltemp.getSkillIcon());
        m_SkillLevelNumber_txt_1.text = m_Pskilltemp.getSkillLevel().ToString();

        //int Skill3id = CurCard.GetHeroData().SpellDataList[2].SpellID;
        //Askilltemp = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.m_Data[Skill3id];
        //Skill3Image.sprite = UIResourceMgr.LoadSprite(common.defaultPath + Askilltemp.getSkillIcon());
        //Skill3Lv.text = Askilltemp.getSkillLevel().ToString();
        //判断技能是否开启
        if (!InterfaceControler.GetInst().IsOpenSkill(m_Card.GetHeroRow(), m_Gskilltemp.getSkillNo()))
        {
            GameUtils.SetImageGrayState(m_SkillImg1, true);
        }
        else
        {
            GameUtils.SetImageGrayState(m_SkillImg1, false);
        }
        if (!InterfaceControler.GetInst().IsOpenSkill(m_Card.GetHeroRow(), m_Pskilltemp.getSkillNo()))
        {
            GameUtils.SetImageGrayState(m_SkillImg2, true);
        }
        else
        {
            GameUtils.SetImageGrayState(m_SkillImg2, false);
        }
    }
    /// <summary>
    /// 清除模型
    /// </summary>
    private void ModelCear()
    {
        if (m_Card3Dmodel != null)
            Destroy(m_Card3Dmodel);
    }

    /// <summary>
    /// 显示3D模型
    /// </summary>
    /// <param name="card"></param>
    public void Show3DModel(ObjectCard card,int resId = 0)
    {
        ModelCear();
        if (m_Point == null)
        {
            //ModelViewRoom = Instantiate(Resources.Load("UI/Prefabs/UI_Home/ModelViewRoom"), new Vector3(-5000, 0, 0), Quaternion.identity) as GameObject;
            m_Point = GameObject.Find("pos").transform;
        }

        if (resId == 0)
            m_heroShowID = card.GetHeroData().GetHeroViewID();
        else
            m_heroShowID = resId;

        m_Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(m_heroShowID);
        GameObject _AssetRes = AssetLoader.Inst.GetAssetRes(m_Artresourcedata.getArtresources());
        if (_AssetRes == null)
            return;
        if (_AssetRes != null && _AssetRes.GetComponent<NavMeshAgent>() != null)
            _AssetRes.GetComponent<NavMeshAgent>().enabled = false;

        m_Card3Dmodel = Instantiate(_AssetRes, m_Point.position, m_Point.rotation) as GameObject;
        float _zoom = m_Artresourcedata.getArtresources_zoom();
        m_Card3Dmodel.transform.localScale = new UnityEngine.Vector3(_zoom, _zoom, _zoom);
        m_Card3Dmodel.transform.parent = m_Point;
        //设置3D模型摩擦力
        m_Card3Dmodel.rigidbody.angularDrag =2.8f;
        m_Card3Dmodel.rigidbody.mass = 1.5f;
        Animation anim = m_Card3Dmodel.GetComponent<Animation>();
        if (anim == null)
            return;
        m_Card3Dmodel.GetComponent<Animation>().Play("Nidle1");
        m_Card3Dmodel.GetComponent<Animation>().wrapMode = WrapMode.Loop;
    }
    /// <summary>
    /// 远近图标按钮
    /// </summary>
    protected override void OnClickJobType_Img()
    {
        m_JobTypeTipsobj.SetActive(true);
    }
    /// <summary>
    /// 攻击类型图标按钮
    /// </summary>
    protected override void OnClickAttackType_Img()
    {
        m_AttackTypeTipsobj.SetActive(true);
    }
    /// <summary>
    /// 种族类型图标按钮
    /// </summary>
    protected override void OnClickRaceTypeImg()
    {
        m_RaceTipsobj.SetActive(true);
    }

    /// <summary>
    /// 通用技能按钮
    /// </summary>
    protected override void OnClickSkillItem_0()
    {
        //m_Skilltipsobj.SetActive(true);
        //m_SkilltipsbgRect.gameObject.SetActive(false);
        //m_GSkillImage.enabled = true;
        //m_PSkillImage.enabled = false;
        //m_ASkillImage.enabled = false;
        //UI_SkillTips _tips = new UI_SkillTips(m_Card, m_Gskilltemp);
        //m_Skilltips_Text.text = _tips.SetShow();
        //Invoke("SetSkillTipsBgSize", 0.02f);
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
        UI_SkillTips _tips = new UI_SkillTips(m_Card, m_Gskilltemp);
        skillxiaohao.transform.parent.parent.gameObject.SetActive(true);
        skillName.text = GameUtils.getString(m_Gskilltemp.getSkillName()) + "Lv" + m_Gskilltemp.getSkillLevel();
        skillxiaohao.text = string.Format("<color=#FF0000>{0}</color>{1}", m_Gskilltemp.getSkillCostNum1(), GetSkillCostString());
        skillCD.text = string.Format("<color=#FF0000>{0}</color>秒", m_Gskilltemp.getCooldown() / 1000);
        skilldec.text = _tips.GetDesc();
        if (string.IsNullOrEmpty(GetLilimt(m_Gskilltemp)))
        {
            skillLimitLevel.gameObject.SetActive(false);
        }
        else
        {
            skillLimitLevel.gameObject.SetActive(true);
            skillLimitLevel.text = GetLilimt(m_Gskilltemp);
        }
        //技能是否开启
        if (!InterfaceControler.GetInst().IsOpenSkill(m_Card.GetHeroRow(), m_Gskilltemp.getSkillNo()))
        {
            skillLimitLevel.gameObject.SetActive(true);
        }
        else
        {
            skillLimitLevel.gameObject.SetActive(false);
        }
    }
    private string GetSkillCostString()
    {
        switch (m_Gskilltemp.getSkillCostType1())
        {
            case (int)EM_EXTRAITEM_TYPE.EM_EXTRAITEM_MP:
                return "怒气";
            default:
                return "";
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
    /// <summary>
    /// 被动技能按钮
    /// </summary>
    protected override void OnClickSkillItem_1()
    {
        //m_Skilltipsobj.SetActive(true);
        //m_SkilltipsbgRect.gameObject.SetActive(false);
        //m_GSkillImage.enabled = false;
        //m_PSkillImage.enabled = true;
        //m_ASkillImage.enabled = false;
        //UI_SkillTips _tips = new UI_SkillTips(m_Card, m_Pskilltemp);
        //m_Skilltips_Text.text = _tips.SetShow();

        //if (m_HeroData.getQuality() < 3)
        //{
        //    string tempText = null;
        //    ChsTextTemplate temp = (ChsTextTemplate)DataTemplate.GetInstance().m_ChsTextTable.getTableData("hero_info_skill_open1");
        //    if (temp.languageMap.TryGetValue(AppManager.Inst.GameLanguage, out tempText))
        //    {
        //        m_Skilltips_Text.text += tempText.Replace("\\n", "\n");
        //    }

        //}
        //Invoke("SetSkillTipsBgSize", 0.02f);
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
        UI_SkillTips _tips = new UI_SkillTips(m_Card, m_Pskilltemp);
        //Skilltipstext.text = _tips.GetDesc();

        skillName.text = GameUtils.getString(m_Pskilltemp.getSkillName()) + "Lv" + m_Pskilltemp.getSkillLevel();
        skillxiaohao.transform.parent.parent.gameObject.SetActive(false);
        skilldec.transform.localPosition.Set(skilldec.transform.localPosition.x, -89.1f, skilldec.transform.localPosition.z);
        skilldec.text = _tips.GetDesc();
        if (string.IsNullOrEmpty(GetLilimt(m_Pskilltemp)))
        {
            skillLimitLevel.gameObject.SetActive(false);
        }
        else
        {
            skillLimitLevel.gameObject.SetActive(true);
            skillLimitLevel.text = GetLilimt(m_Pskilltemp);
        }
        if (!InterfaceControler.GetInst().IsOpenSkill(m_Card.GetHeroRow(), m_Pskilltemp.getSkillNo()))
        {
            skillLimitLevel.gameObject.SetActive(true);
        }
        else
        {
            skillLimitLevel.gameObject.SetActive(false);
        }
          
    }

    /// <summary>
    /// 此处要延迟设置背景框体的大小
    /// </summary>
    private void SetSkillTipsBgSize()
    {
        m_SkilltipsbgRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, m_Skilltips_Text.GetComponent<RectTransform>().rect.height + m_SkilltipsBgSizeAmend);
        m_SkilltipsbgRect.gameObject.SetActive(true);
    }




    /// <summary>
    /// 升级按钮显示
    /// </summary>
    private void UpBtnGrayShow()
    {
        m_LeftBtn.interactable = true;
        m_RightBtn.interactable = true;
        if (m_Card.GetHeroData().Level <= 1)
        {
            m_LeftBtn.interactable = false;
        }
        else if (m_Card.GetHeroData().Level >= m_HeroData.getMaxLevel())
        {
            m_RightBtn.interactable = false;
        }

    }

    /// <summary>
    /// 等级减少左按钮
    /// </summary>
    protected override void OnClickLeftBtn()
    {
        if (m_Card.GetHeroData().Level <= 1)
            return;
        int value = m_Card.GetHeroData().Level - 5;
        if (value < 1)
            value = 1;
        ShowData(value);
    }
    /// <summary>
    /// 等级增加右按钮
    /// </summary>
    protected override void OnClickRightBtn()
    {
        if (m_Card.GetHeroData().Level >= m_HeroData.getMaxLevel())
            return;

        int value = m_Card.GetHeroData().Level + 5;
        if (value > m_HeroData.getMaxLevel())
            value = m_HeroData.getMaxLevel();
        ShowData(value);
    }
    /// <summary>
    /// 技能演示按钮
    /// </summary>
    protected override void OnClickSkillShowBtn()
    {
        SceneManager.Inst.StartChangeScene("SkillShow");
        SkillShowSceneControler.SetHeroData(m_Artresourcedata, m_Card, m_HeroData.getId(),m_HeroData.getClientSignType()[0]);
        UI_SkillShow.SetHeroData(m_Card,m_HeroData.getId());
        InitCampaignMonsterGroupData();
    }
    /// <summary>
    /// 关闭按钮
    /// </summary>
    protected override void OnClickCloseBtn()
    {
        ModelCear();
        HeroModelBack.Inst.ClearBg();
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }

    /// <summary>
    /// 初始化关卡中的怪物信息
    /// </summary>
    private void InitCampaignMonsterGroupData()
    {
        CampaignMonsterGroupData _monsterGroupData = new CampaignMonsterGroupData();
        _monsterGroupData.IDs[0, 0] = GlobalMembers.SPELL_SHOW_MONTER_ID;
        _monsterGroupData.Count = 1;
        ObjectSelf.GetInstance().OnCacheMonsterGroupData(_monsterGroupData, -1);
    }


    #region #皮肤模块
    /// <summary>
    /// 皮肤右按钮
    /// </summary>
    protected override void OnClickRightBtn_1()
    {
        for (int i = 0; i < m_ArtResArray.Length; i++)
        {
            if (m_heroShowID != m_ArtResArray[m_ArtResArray.Length - 1])
            {
                if (m_ArtResArray[i] == m_heroShowID)
                {
                    Show3DModel(m_Card, m_ArtResArray[i + 1]);
                }
            }
        }
        ShowHeroSkinName();
        InitSkinBtnIsGary();
    }
    /// <summary>
    /// 皮肤左按钮
    /// </summary>
    protected override void OnClickLeftBtn_1()
    {
        for (int i = 0; i < m_ArtResArray.Length; i++)
        {
            if (m_heroShowID != m_ArtResArray[0])
            {
                if (m_ArtResArray[i] == m_heroShowID)
                {
                    Show3DModel(m_Card, m_ArtResArray[i - 1]);
                }
            }
        }
        ShowHeroSkinName();
        InitSkinBtnIsGary();
    }
    /// <summary>
    /// 显示皮肤名字
    /// </summary>
    /// m_Text_7 皮肤名字Text
    private void ShowHeroSkinName()
    {
        m_Text_7.text = GameUtils.getString(m_Artresourcedata.getNameID());
    }

    /// <summary>
    /// 显示皮肤按钮是否置灰
    /// </summary>
    private void InitSkinBtnIsGary()
    {
        m_LeftBtn_1.interactable = true;
        m_RightBtn_1.interactable = true;

        m_ArtResArray = m_HeroData.getUseableArtresource();
        if (m_ArtResArray.Length == 1)
        {
            m_LeftBtn_1.interactable = false;
            m_RightBtn_1.interactable = false;
        }
        else if (m_heroShowID == m_ArtResArray[0])
        {
            m_LeftBtn_1.interactable = false;
        }
        else if (m_heroShowID == m_ArtResArray[m_ArtResArray.Length - 1])
        {
            m_RightBtn_1.interactable = false;
        }


    } 
    #endregion

    #region #获得途径模块
    /// <summary>
    /// 初始化获得途径面板信息
    /// </summary>
    private void InitGetAppData()
    {
        StringBuilder _str = new StringBuilder();
        string[] _strArray = m_HeroData.getAccessMethod().Split('#');
        for (int i = 0; i < _strArray.Length; i++)
        {
            _str.Append(GameUtils.getString(_strArray[i]));
            if (i != _strArray.Length - 1)
            {
                _str.Append("\n");
            }
        }
        m_DesTxt.text = _str.ToString();
        m_TilteTxt.text = GameUtils.getString("fashion_content1");

        if (m_HeroData.getHerotype() == 2)
        {
            m_InfoDetail_btn.transform.localPosition = new Vector2(335, m_InfoDetail_btn.transform.localPosition.y);
        }
    }
    /// <summary>
    /// 获得途径按钮
    /// </summary>
    protected override void OnClickInfoDetail_btn()
    {
        m_GetApprochObj.SetActive(true);
    }
    /// <summary>
    /// 获得途径面板Ok按钮
    /// </summary>
    private void OnClickOKBtn()
    {
        m_GetApprochObj.SetActive(false);
    } 
    #endregion

    #region #英雄搭配模块
    /// <summary>
    /// 初始化英雄搭配界面
    /// </summary>
    private void InitHeroMatthData()
    {
        if(m_HeroData.getHerotype() == 2)
        {
            m_HeroMake_btn.gameObject.SetActive(false);
        }

        for (int i = 0; i < HeroMatchList.Count; ++i)
        {
            Destroy(HeroMatchList[i]);
        }
        HeroMatchList.Clear();
        string _text = GameUtils.getString(m_Card.GetHeroRow().getHeroDes());

        m_HeroTrait_Text.text = _text;

        int count = m_Card.GetHeroRow().getSkillPair().Length;
        for (int i = 0; i < count; ++i)
        {
            GameObject temp = Instantiate(Resources.Load(common.prefabPath + "UI_Home/UI_SHeroItem")) as GameObject;
            temp.transform.SetParent(m_HeroMatchListLayout, false);
            HeroMatchList.Add(temp);
            if (!DataTemplate.GetInstance().m_HeroTable.tableContainsKey(m_Card.GetHeroRow().getSkillPair()[i]))
                continue;
            HeroTemplate _data = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(m_Card.GetHeroRow().getSkillPair()[i]);
            UI_HeroItem uiItem = temp.AddComponent<UI_HeroItem>();
            uiItem.InitHeroMatchCardData(_data, m_Card);
        }

        m_HeroMatchName.text = GameUtils.getString(m_HeroData.getNameID());
        m_HeroMatchTitle.text = GameUtils.getString(m_HeroData.getTitleID());
    }

    /// <summary>
    /// 英雄搭配按钮
    /// </summary>
    protected override void OnClickHeroMake_btn()
    {
        m_HeroMakeinfo.SetActive(true);
    }
    /// <summary>
    /// 英雄搭配关闭按钮
    /// </summary>
    private void OnClickMakeClose_btn()
    {
        m_HeroMakeinfo.SetActive(false);
    } 
    #endregion

    #region #进阶信息模块
    /// <summary>
    /// 进阶信息按钮
    /// </summary>
    protected override void OnClickOrderUP_btn()
    {
        HeroModelBack.Inst.ChangePanel("Advanced");
        m_HeroAdvanceObj.SetActive(true);
    }

    /// <summary>
    /// 初始化进阶信息
    /// </summary>
    private void InitShowOrderUpInfo()
    {
        m_HeroAdvances.Clear();
        Dictionary<int, IExcelBean> _heroDatas = DataTemplate.GetInstance().m_HeroTable.getData();
        foreach (var item in _heroDatas)
        {
            HeroTemplate _hero = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(item.Key);
            if (GameUtils.GetHeroIDNum(_hero.getId()) == GameUtils.GetHeroIDNum(m_HeroData.getId()))
            {
                m_HeroAdvances.Add(_hero);
            }
        }

        ListRank();
        ClearModels();
        CreateModel();
        ShowHeroAdvanceShar();
        ShowHeroAdvanceNameAndCos();
    }

    /// <summary>
    /// 用于排序
    /// </summary>
    private void ListRank()
    {
        //排序
        for (int i = 0; i < m_HeroAdvances.Count - 1; i++)
        {
            HeroTemplate _temp = null;
            for (int j = 0; j < m_HeroAdvances.Count - 1 - i; j++)
            {
                if (m_HeroAdvances[j].getId() > m_HeroAdvances[j + 1].getId())
                {
                    _temp = m_HeroAdvances[j + 1];
                    m_HeroAdvances[j + 1] = m_HeroAdvances[j];
                    m_HeroAdvances[j] = _temp;
                }
            }
        }
    }
    /// <summary>
    /// 清除Group中的模型对象
    /// </summary>
    private void ClearModels()
    {
        for (int i = 0; i < m_Group.childCount; i++)
        {
            for (int j = 0; j < m_Group.GetChild(i).childCount; j++)
            {
                Destroy(m_Group.GetChild(i).GetChild(j).gameObject);
            }
        }
    }

    /// <summary>
    /// 创建模型
    /// </summary>
    private void CreateModel()
    {
        for (int i = 0; i < m_HeroAdvances.Count; i++)
        {
            int _heroID = m_HeroAdvances[i].getArtresources();
            ArtresourceTemplate _res = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(_heroID);
            if (_res == null) continue;
            GameObject _resModel = AssetLoader.Inst.GetAssetRes(_res.getArtresources());
            if (_resModel != null)
            {
                if (_resModel.GetComponent<NavMeshAgent>() != null)
                    _resModel.GetComponent<NavMeshAgent>().enabled = false;

                GameObject _go = Instantiate(_resModel, m_Group.GetChild(i).position, m_Group.GetChild(i).rotation) as GameObject;
                float _zoom = _res.getArtresources_zoom();
                _go.transform.localScale = new UnityEngine.Vector3(_zoom, _zoom, _zoom);
                _go.transform.parent = m_Group.GetChild(i);
                Animation _anim = _go.GetComponent<Animation>();
                if (_anim != null)
                {
                    if (_go.GetComponent<Animation>()["Idle1"] != null)
                    {
                        _go.GetComponent<Animation>().CrossFade("Idle1");
                    }
                    else if (_go.GetComponent<Animation>()["Nidle1"] != null)
                    {
                        _go.GetComponent<Animation>().CrossFade("Nidle1");
                    }
                    _go.GetComponent<Animation>().wrapMode = WrapMode.Loop;
                }
            }
        }
    }
    /// <summary>
    /// 进阶信息星级显示
    /// </summary>
    private void ShowHeroAdvanceShar()
    {
        for (int i = 0; i < m_SharsList.Count; i++)
        {
            if (i < m_HeroAdvances.Count)
            {
                InterfaceControler.GetInst().AddSharLevel(m_SharsList[i], m_HeroAdvances[i]);
            }
            else
            {
                m_SharsList[i].gameObject.SetActive(false);
                m_SharsBg[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// 显示英雄名称称号 消耗显示
    /// </summary>
    private void ShowHeroAdvanceNameAndCos()
    {
        StringBuilder _str = new StringBuilder();
        _str.Append(GameUtils.getString(m_HeroData.getTitleID()));
        _str.Append("-");
        _str.Append(GameUtils.getString(m_HeroData.getNameID()));
        m_ANameAndTilteTxt.text = _str.ToString();

        int _cosId1 = m_HeroAdvances[0].getStageUpCostType2();
        int _cosId2 = m_HeroAdvances.Count > 2 ? m_HeroAdvances[1].getStageUpCostType2() : -1;
        if (_cosId1 == -1)
        {
            m_ACosIcon1.enabled = false;
            m_ACosNumTxt1.enabled = false;
        }
        else
        {
            ItemTemplate _item1 = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(_cosId1);
            m_ACosIcon1.sprite = UIResourceMgr.LoadSprite(common.defaultPath + _item1.getIcon());
            m_ACosNumTxt1.text = m_HeroAdvances[0].getStageUpCost2().ToString();
        }

        if (_cosId2 == -1)
        {
            m_ACosIcon2.enabled = false;
            m_ACosNumTxt2.enabled = false;
        }
        else
        {
            ItemTemplate _item2 = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(_cosId2);
            m_ACosIcon2.sprite = UIResourceMgr.LoadSprite(common.defaultPath + _item2.getIcon());
            m_ACosNumTxt2.text = m_HeroAdvances[1].getStageUpCost2().ToString();
        }
    }

    /// <summary>
    /// 进阶信息关闭界面
    /// </summary>
    private void OnClickHeroAdvanceCloseBtn()
    {
        HeroModelBack.Inst.ChangePanel("HeroInfo");
        m_HeroAdvanceObj.SetActive(false);
    } 
    #endregion

    void OnDestroy()
    {
        base.OnDestroy();
        ClearModels();
        HeroModelBack.Inst.ClearBg();
    }

}
