using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.UI;
using DreamFaction.Utils;
using UnityEngine.Events;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using GNET;
using DreamFaction.GameNetWork.Data;
using DreamFaction.GameCore;
using System.Text;
using DreamFaction.LogSystem;

public enum SkillState
{
    Active,   //未开启
    CanUp,    //升级
    ClassMax, //当阶最大
    Finish    //满级
}
public class UI_SkillItem : BaseUI
{
    //private SpellData _SkillData;                            //技能数据信息
    //private HeroTemplate _HeroData;                          //英雄表数据
    //private ObjectCard _objCard;                             //英雄卡牌
    //private SkillupcostTemplate _SkillUpData;                //技能升级表数据                      

    //private int myConAssetCount = 0;                         //当前所拥有的消耗资源个数    
    //private int skillNo;                                     //技能编号
    //private string m_SkillName;                              //技能名称
    //private int m_ConCount;                                  //消耗品个数
    //private int m_CurrentLevel;                              //当前技能等级
    //private int m_ClassMaxLevel;                             //当前上限最大技能等级
    //private int m_MaxLevel = 10;                             //最大等级
    //private SkillState skillState = SkillState.CanUp;        //技能升级状态
    ////private Sprite m_SkillUPBtnIcon;                         //技能升级按钮图标
    //private Image SkillIconImg;                              //技能图标
    //private Text SkillNameTxt;                               //技能名称
    //// private Text SkillTypeTxt;                            //技能类型
    //private Text SkillDesTxt;                                //技能描述
    //private Text ConCountTxt;                                //消耗品的个数
    //private Text CurrentLevelTxt;                            //技能等级
    //private Text ClassMaxLevelTxt;                                                  
    //private Text LevelTxtActive;                             //等级是否隐藏
    //private Image ConGoodsIconImg;                           //技能升级消耗品图标
    //private Image SkillUpBtnIconImg;                         //技能升级按钮图标
    //private Text SkillUPBtnIconTxt;                          //技能升级按钮文字
    //private Button SkillUpBtn;                               //升级按钮
    //private Transform parent;                                //弹窗父节点
    //public override void InitUIData()
    //{
    //    base.InitUIData();
    //    parent = selfTransform.parent.parent;
    //    SkillNameTxt = selfTransform.FindChild("SkillName").GetComponent<Text>();
    //    SkillIconImg = selfTransform.FindChild("SkillIcon").GetComponent<Image>();
    //    SkillDesTxt = selfTransform.FindChild("SkillDes").GetComponent<Text>();
    //    CurrentLevelTxt = selfTransform.FindChild("CurrLevelTxt").GetComponent<Text>();
    //    ClassMaxLevelTxt = selfTransform.FindChild("MaxLevelTxt").GetComponent<Text>();
    //    LevelTxtActive = selfTransform.FindChild("active").GetComponent<Text>();
    //    //SkillTypeTxt = selfTransform.FindChild("SkillType").GetComponent<Text>();
    //    ConGoodsIconImg = selfTransform.FindChild("ConIcon").GetComponent<Image>();
    //    ConCountTxt = selfTransform.FindChild("ConCount").GetComponent<Text>();
    //    SkillUpBtnIconImg = selfTransform.FindChild("Button").GetComponent<Image>();
    //    SkillUPBtnIconTxt = SkillUpBtnIconImg.transform.FindChild("Text").GetComponent<Text>();        
    //    SkillUpBtn = SkillUpBtnIconImg.GetComponent<Button>();
    //    SkillUpBtn.onClick.AddListener(new UnityAction(SkillUpClick));
    //}



    ////显示技能信息
    //public void ShowSkill(SpellData item, ObjectCard objCard)
    //{
    //    _SkillData = item;
    //    _objCard = objCard;
    //    _HeroData = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(objCard.GetHeroData().TableID);
    //    SkillTemplate skill = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.getTableData(_SkillData.SpellID); //_SkillData.SpellID为技能ID
    //    InitData(skill);
    //}



    //void InitData(SkillTemplate skill)
    //{
    //    if (skill.getId() == -1)
    //        return;
    //    _SkillUpData = (SkillupcostTemplate)DataTemplate.GetInstance().m_SkillupcostTable.getTableData(skill.getId());
    //    //_SkillData.Copy(_SkillData);//是否解锁
    //    //技能类型
    //    skillNo = skill.getSkillNo();
    //    //SkillTypeTxt.text = GetSkillType(skillNo);
    //    //获取到技能名称
    //    m_SkillName = GameUtils.getString(skill.getSkillName());
    //    SkillNameTxt.text = m_SkillName;
    //    //技能图标
    //    SkillIconImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + skill.getSkillIcon());
    //    //技能等级
    //    m_CurrentLevel = _SkillUpData.getSkillLevel();
    //    //技能当阶最大等级
    //    m_ClassMaxLevel = HeroCurrentSkillMaxLevel(_HeroData);//_SkillUpData.getUpgradeStarCondition();
    //    //技能升级按钮图标
    //    //m_SkillUPBtnIcon = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Jisheng_02");
    //    //技能描述
    //    SkillDesTxt.text = GameUtils.SetUpShow(_objCard, _SkillUpData);

    //    //技能消耗  
    //    int[] ConAssets = _SkillUpData.getUpgradeCostId();//获取消耗物品ID组
    //    ItemTemplate itemData = null;
    //    if (ConAssets[0] != -1)
    //    {
    //        itemData = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(ConAssets[0]);
    //    }
    //    //消耗品图标
    //    Sprite conIcon = UIResourceMgr.LoadSprite(common.defaultPath + itemData.getIcon());
    //    ConGoodsIconImg.sprite = conIcon;

    //    int[] ConCounts = _SkillUpData.getUpgradeCostNum();//消耗品数组
    //    if (ConCounts.Length != 0 && ConCounts[0] != -1)
    //    {
    //        m_ConCount = ConCounts[0];
    //    }

    //    //当前拥有的资源个数
    //    myConAssetCount = InterfaceControler.GetInst().ReturnItemCount(ConAssets[0]);

    //    switch (skillNo)
    //    {
    //        case 1:
    //            UI_SkillUpManager._instance.ShowCon1Info(myConAssetCount, itemData.getIcon());
    //            break;
    //        case 2:
    //            UI_SkillUpManager._instance.ShowCon2Info(myConAssetCount, itemData.getIcon());
    //            break;
    //        default:
    //            break;
    //    }


    //    //判断状态显示
    //    JudgeState();
    //    JudgeStateShow();
    //}

    ///// <summary>
    ///// 判断技能升级状态
    ///// </summary>
    //void JudgeState()
    //{
    //    //未启用状态
    //    if (InterfaceControler.GetInst().IsOpenSkill(_HeroData, skillNo))
    //    {
    //        skillState = SkillState.CanUp;
    //    }
    //    else
    //    {
    //        skillState = SkillState.Active;
    //    }

    //    //当阶上限状态
    //    if (m_CurrentLevel == m_ClassMaxLevel)
    //    {
    //        skillState = SkillState.ClassMax;
    //    }
    //    //满级状态
    //    if (m_CurrentLevel == m_MaxLevel)
    //    {
    //        skillState = SkillState.Finish;
    //    }

    //}

    ///// <summary>
    ///// 判断当前状态显示   等级、升级消耗
    ///// </summary>
    //private void JudgeStateShow()
    //{
    //    StringBuilder str = new StringBuilder();
    //    str.Append("/");
    //    str.Append(m_ClassMaxLevel);
    //    ClassMaxLevelTxt.text = str.ToString();
    //    CurrentLevelTxt.text = m_CurrentLevel.ToString();

    //    LevelTxtActive.enabled = false;
    //    CurrentLevelTxt.enabled = true;
    //    ClassMaxLevelTxt.enabled = true;
    //    //默认字体为白色
    //    ConCountTxt.color = Color.white;
    //    //判断当前状态
    //    switch (skillState)
    //    {
    //        case SkillState.Active:
    //            ConGoodsIconImg.enabled = false;//不显示消耗物品
    //            ConCountTxt.text = "—  —";//消耗数量显示- -
    //            LevelTxtActive.enabled = true;//等级显示- -
    //            CurrentLevelTxt.enabled = false;
    //            ClassMaxLevelTxt.enabled = false;
    //            GameUtils.SetBtnSpriteGrayState(SkillUpBtn,true);
    //            //SkillUpBtnIconImg.sprite = m_SkillUPBtnIcon;
    //            SkillUPBtnIconTxt.text = GameUtils.getString("hero_skill_button3");
    //            break;
    //        case SkillState.CanUp:
    //            UpLogic();

    //            break;
    //        case SkillState.ClassMax:
    //            UpLogic();

    //            break;
    //        case SkillState.Finish:
    //            ConGoodsIconImg.enabled = false;//不显示消耗物品
    //            ConCountTxt.text = "—  —";//消耗数量显示- -

    //            GameUtils.SetBtnSpriteGrayState(SkillUpBtn, true);
    //            //SkillUpBtnIconImg.sprite = m_SkillUPBtnIcon;
    //            SkillUPBtnIconTxt.text = GameUtils.getString("hero_skill_button2");
    //            break;
    //    }
    //}

    ///// <summary>
    ///// 显示消耗品个数 图标
    ///// </summary>
    //void UpLogic()
    //{
    //    //Sprite _img = UIResourceMgr.LoadSprite(common.defaultPath + "UI_jisheng_01");
    //    //判断当前拥有的消耗品
    //    if (myConAssetCount < m_ConCount)
    //    {
    //        ConCountTxt.color = Color.red;
    //    }
    //    ConGoodsIconImg.enabled = true;//显示图标
    //    ConCountTxt.text = m_ConCount.ToString();//消耗个数

    //    GameUtils.SetBtnSpriteGrayState(SkillUpBtn, false);
    //    //SkillUpBtnIconImg.sprite = _img;
    //    SkillUPBtnIconTxt.text = GameUtils.getString("hero_skill_button1");
    //}


    ///// <summary>
    ///// 播放特效
    ///// </summary>
    //public void ShowEffect()
    //{
    //    GameObject effect = Instantiate(UIResourceMgr.LoadPrefab(common.EffectPath + "SkillLevelUp01")) as GameObject;
    //    effect.transform.parent = SkillIconImg.transform;
    //    effect.transform.localPosition = Vector3.zero;
    //    effect.transform.localScale = Vector3.one;
    //    Destroy(effect, 0.8f);
    //}

    ///// <summary>
    ///// 按钮点击
    ///// </summary>
    //void SkillUpClick()
    //{
    //    string _text = "";
    //    //判断状态
    //    switch (skillState)
    //    {
    //        case SkillState.Active://未开启 需进阶，无法升级 
    //            _text = string.Format(GameUtils.getString("hero_skill_tip4"), (skillNo + 1));
    //            InterfaceControler.GetInst().AddMsgBox(_text, parent);
    //            break;
    //        case SkillState.ClassMax://当阶最大
    //            _text = GameUtils.getString("hero_skill_tip1");
    //            InterfaceControler.GetInst().AddMsgBox(_text, parent);
    //            break;
    //        case SkillState.Finish://已满级
    //            _text = GameUtils.getString("hero_skill_tip3");
    //            InterfaceControler.GetInst().AddMsgBox(_text, parent);
    //            break;
    //        case SkillState.CanUp:
    //            //判断道具是否够升级
    //            if (m_ConCount > myConAssetCount)
    //            {
    //                ConCountTxt.color = Color.red;
    //                _text = GameUtils.getString("hero_skill_tip2");
    //                InterfaceControler.GetInst().AddMsgBox(_text, parent);
    //            }
    //            else
    //            {
    //                //向服务器发送消息  升级
    //                CHeroSkillup chsu = new CHeroSkillup();
    //                byte num = (byte)skillNo;
    //                chsu.skillnum = num;
    //                chsu.herokey = (int)_objCard.GetGuid().GUID_value;
    //                IOControler.GetInstance().SendProtocol(chsu);
    //            }
    //            break;
    //    }
    //}

    ///// <summary>
    ///// 获取技能类型
    ///// </summary>
    ///// <param name="key">技能编号（位置）</param>
    ///// <returns></returns>
    ////private string GetSkillType(int key)
    ////{
    ////    switch (key)
    ////    {
    ////        case 1: return "通用";
    ////        case 2: return "被动";
    ////        case 3: return "PVP";
    ////        default: return "";
    ////    }
    ////}

    ///// <summary>
    ///// 返回英雄最大等级上限
    ///// </summary>
    ///// <param name="heroData">英雄表数据</param>
    ///// <returns>返回英雄最大等级上限</returns>
    //private int HeroCurrentSkillMaxLevel(HeroTemplate heroData)
    //{
    //    int _sharLevel = heroData.getQuality();
    //    return _sharLevel * 2;
    //}
    private int m_Index = -1;                                      //技能排列 （1-6）
    private ObjectCard m_Card = null;                              //当前的英雄卡牌
    private SkillTemplate m_SkillT = null;                         //技能表数据
    private SkillupcostTemplate m_skillUpT = null;                 //技能升级表数据   

    private Image m_SkillIconImg = null;                           //技能图标Icon
    private Image m_SkillTypeImg = null;                           //技能类型Icon    
    private Text m_SkillNameTxt = null;                            //
    private Text m_SkillLevelTxt = null;
    private Text m_SkillConsGold = null;
    private Button m_UpLevelBtn = null;
    private Button m_SkillIconBtn = null;
    private Text m_SkillUnlockTxt = null;

    private GameObject m_OpenStateOBJ = null;
    private GameObject m_CloseStateOBJ = null;
    private GameObject m_Effect = null;

    private bool isSkillOpen = false;
    public override void InitUIData()
    {
        base.InitUIData();

        m_Effect = selfTransform.FindChild("UI_Effect_Yingxiongjinengqianghua01").gameObject;
        m_OpenStateOBJ = selfTransform.FindChild("SkillIcon/Upgrade").gameObject;
        m_CloseStateOBJ = selfTransform.FindChild("SkillIcon/Unlock").gameObject;
        m_SkillIconImg = selfTransform.FindChild("SkillIcon/image_StealthIcon").GetComponent<Image>();
        m_SkillTypeImg = selfTransform.FindChild("SkillIcon/Image_Passive").GetComponent<Image>();
        m_SkillNameTxt = selfTransform.FindChild("SkillIcon/Skillsupgrading/Text_Stealthskills").GetComponent<Text>();
        m_SkillLevelTxt = selfTransform.FindChild("SkillIcon/Upgrade/Text_Numerical").GetComponent<Text>();
        m_SkillConsGold = selfTransform.FindChild("SkillIcon/Upgrade/Text_Gold").GetComponent<Text>();
        m_SkillUnlockTxt = selfTransform.FindChild("SkillIcon/Unlock/Text_Herounlock").GetComponent<Text>();

        m_SkillIconBtn = m_SkillIconImg.GetComponent<Button>();
        m_SkillIconBtn.onClick.AddListener(new UnityAction(onSkillInfoClick));
        m_UpLevelBtn = selfTransform.FindChild("SkillIcon/Upgrade/Btn_Upgradebutton").GetComponent<Button>();
        m_UpLevelBtn.onClick.AddListener(new UnityAction(onUpLevelBtnClick));

    }

    public void ShowSkillData(SkillTemplate skillT, ObjectCard card, int index)
    {
        m_Index = index;
        m_Card = card;
        m_SkillT = skillT;
        m_skillUpT = (SkillupcostTemplate)DataTemplate.GetInstance().m_SkillupcostTable.getTableData(skillT.getId());

        ShowSkillText();
        ShowSkillLevel();
        ShowSkillConsUI();
        m_SkillUnlockTxt.text = GameUtils.GetSkillColorClear(index);
    }

    /// <summary>
    /// 设置开启状态
    /// </summary>
    /// <param name="active"></param>
    public void SetOpenState(bool active)
    {
        isSkillOpen = active;
        m_OpenStateOBJ.SetActive(active);
        m_CloseStateOBJ.SetActive(!active);
    }

    private void ShowSkillText()
    {
        m_SkillNameTxt.text = GameUtils.getString(m_SkillT.getSkillName());
        m_SkillIconImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + m_SkillT.getSkillIcon());
        InterfaceControler.GetInst().ShowSkillTypeIcon(m_SkillT, m_SkillTypeImg);
    }

    private void ShowSkillLevel()
    {
        string str = string.Format("Lv.{0}/{1}", "<color=yellow>" + m_skillUpT.getSkillLevel(), m_Card.GetHeroData().Level + "</color>");
        m_SkillLevelTxt.text = str;
    }

    private void ShowSkillConsUI()
    {
        m_SkillConsGold.text = m_skillUpT.getUpgradeCostNum2()[0].ToString();
    }



    private void onUpLevelBtnClick()
    {
        long count = 0;
        if (m_skillUpT.getSkillLevel() < m_Card.GetHeroData().Level &&
            ObjectSelf.GetInstance().Money >= m_skillUpT.getUpgradeCostNum2()[0])
        {
            if (ObjectSelf.GetInstance().TryGetResourceCountById(m_skillUpT.getUpgradeCostId3()[0], ref count) &&
                count > m_skillUpT.getUpgradeCostNum3()[0])
            {
                m_Effect.SetActive(false);
                m_Effect.SetActive(true);
                onSendMessge();
            }
            else
            {
                HintUI();
            }
        }
        else
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("ui_yingxiongqianghua_jineng18"),transform.parent.parent.parent);
            //HintUI();
        }
    }

    private void HintUI()
    {
        VipTemplate vipT = (VipTemplate)DataTemplate.GetInstance().m_VipTable.getTableData(ObjectSelf.GetInstance().VipLevel);
        UICommonManager.Inst.ShowMsgBox("购买技能点", string.Format("你是VIP1，今天还可以购买<color=#ff0000>{0}</color>次", vipT.getSkillconlimit()),
                                        string.Format("购买<color=#ff0000>{0}</color>技能点，需花费;<image res ='Sprites/zuanshi' height='40' width='40'/>;{1}", 20, 50),
                                        "购买", ResetOKBtn, null, null);
    }


    private void ResetOKBtn(object data)
    {

    }

    //技能按钮图标回调
    private void onSkillInfoClick()
    {
        UICommonManager.Inst.ShowSkill(m_SkillT, m_Card, m_Index);
    }

    private void onSendMessge()
    {
        //向服务器发送消息  升级
        CHeroSkillup chsu = new CHeroSkillup();
        byte num = (byte)m_Index;
        chsu.skillnum = num;
        chsu.herokey = (int)m_Card.GetGuid().GUID_value;
        IOControler.GetInstance().SendProtocol(chsu);
    }


}
