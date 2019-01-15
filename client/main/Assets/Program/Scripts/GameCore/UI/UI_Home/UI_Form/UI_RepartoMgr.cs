using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using UnityEngine.Events;
using DreamFaction.Utils;
using DreamFaction.GameCore;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.UI;

public class UI_RepartoMgr : BaseUI
{
    public static string UI_ResPath = "UI_Form/UI_Reparto_2_2";
    private static UI_RepartoMgr _inst;

    public List<GameObject> m_TeamBtns = new List<GameObject>();
    private ObjectCard m_Card = null;                                             //当前选择的卡牌
    private HeroTemplate m_HeroT = null;
    private int m_SelectPos = -1;                                                 //当前选择的位置0是前排 1是后排
    private int m_SelectNo = -1;                                                  //当前选择在队伍中的编号

    private GameObject m_Card3Dmodel = null;                                      //当前实例化3D模型
    private Transform m_Point = null;                                             //3D模型实例化位置
    private Transform m_BrightStar = null;

    //UI控件
    private Button m_SkinBtn = null;
    private Button m_IntensifyBtn = null;
    private Button m_BackBtn = null;
    private Button m_ChangeBtn = null;
    private Text m_HeroTliteTxt = null;
    private Text m_HeroNameTxt = null;
    private Text m_AptitudeStr = null;
    private Text m_PowerTxt = null;
    private Text m_AttackTxt = null;
    private Text m_DefenceTxt = null;
    private Text m_HpTxt = null;
    private Image m_HeroTypeImg = null;
    private Image m_HeroAptImg = null;
    private Button m_HeroTypeBtn = null;
    private Text m_HeroLevelTxt = null;
    private Text m_HeroTypeTxt = null;
    private Image m_ZhongZu = null;

    private Equipment m_EquipImg1 = null;
    private Equipment m_EquipImg2 = null;
    private Equipment m_EquipImg3 = null;
    private Equipment m_EquipImg4 = null;
    private Equipment m_EquipImg5 = null;
    private Equipment m_EquipImg6 = null;

    public static UI_RepartoMgr Inst 
    {
        get { return _inst; } 
    }

    /// <summary>
    /// 设置当前的英雄数据
    /// </summary>
    /// <param name="card">卡牌数据</param>
    /// <param name="pos">在队伍是前排还是后排</param>
    /// <param name="index">在队伍中的索引位置</param>
    public void SetSelectHeoData(ObjectCard card, int pos, int no)
    {
        this.m_Card = card;
        this.m_SelectPos = pos;
        this.m_SelectNo = no;
        if (card != null)
            m_HeroT = card.GetHeroRow();
    }

    public override void InitUIData()
    {
        base.InitUIData();

        _inst = this;

        m_BrightStar = selfTransform.FindChild("HeroInfoPanel/HeroStar/BrightStar");

        m_HeroTliteTxt = selfTransform.FindChild("HeroInfoPanel/HeroTliteTxt").GetComponent<Text>();
        m_HeroNameTxt = selfTransform.FindChild("HeroInfoPanel/HeroNameTxt").GetComponent<Text>();
        m_HeroLevelTxt = selfTransform.FindChild("HeroInfoPanel/HeroLevelTxt").GetComponent<Text>();
        m_HeroTypeTxt = selfTransform.FindChild("HeroInfoPanel/HeroTypeTxt").GetComponent<Text>();
        m_AptitudeStr = selfTransform.FindChild("HeroInfoPanel/AptitudeStr").GetComponent<Text>();
        m_PowerTxt = selfTransform.FindChild("BottomPanel/PowerTxt").GetComponent<Text>();
        m_AttackTxt = selfTransform.FindChild("BottomPanel/AttackTxt").GetComponent<Text>();
        m_DefenceTxt = selfTransform.FindChild("BottomPanel/DefenceTxt").GetComponent<Text>();
        m_HpTxt = selfTransform.FindChild("BottomPanel/HpTxt").GetComponent<Text>();
        m_HeroTypeImg = selfTransform.FindChild("HeroInfoPanel/HeroTypeIcon").GetComponent<Image>();
        m_HeroAptImg = selfTransform.FindChild("HeroInfoPanel/HeroAptImg").GetComponent<Image>();
        m_ZhongZu = selfTransform.FindChild("HeroInfoPanel/ZhongZu").GetComponent<Image>();

        m_EquipImg1 = selfTransform.FindChild("RightPanel/Equip1/Equipment").GetComponent<Equipment>();
        m_EquipImg2 = selfTransform.FindChild("RightPanel/Equip2/Equipment").GetComponent<Equipment>();
        m_EquipImg3 = selfTransform.FindChild("RightPanel/Equip3/Equipment").GetComponent<Equipment>();
        m_EquipImg4 = selfTransform.FindChild("RightPanel/Equip4/Equipment").GetComponent<Equipment>();
        m_EquipImg5 = selfTransform.FindChild("RightPanel/Equip5/Equipment").GetComponent<Equipment>();
        m_EquipImg6 = selfTransform.FindChild("RightPanel/Equip6/Equipment").GetComponent<Equipment>();

        m_BackBtn = selfTransform.FindChild("TopPanel/TopTittle/BackBtn").GetComponent<Button>();
        m_BackBtn.onClick.AddListener(new UnityAction(onBackCall));
        m_ChangeBtn = selfTransform.FindChild("BottomPanel/ChangeBtn").GetComponent<Button>();
        m_ChangeBtn.onClick.AddListener(new UnityAction(onChangeHeroBtnClick));
        m_IntensifyBtn = selfTransform.FindChild("BottomPanel/IntensifyBtn").GetComponent<Button>();
        m_IntensifyBtn.onClick.AddListener(new UnityAction(onIntensifyBtnClick));
        m_SkinBtn = selfTransform.FindChild("BottomPanel/SkinBtn").GetComponent<Button>();
        //m_SkinBtn.onClick.AddListener(new UnityAction(onSkinBtnClick));
        m_HeroTypeBtn = m_HeroTypeImg.GetComponent<Button>();
        m_HeroTypeBtn.onClick.AddListener(UICommonManager.Inst.ShowHeroLocatUI);
    }

    /// <summary>
    /// 更新显示
    /// </summary>
    public void UpdateUIShow()
    {
        ShowStaticText();
        Show3DModel();
        ShowAttrText();
        ShowTeamBtns();
        //ShowEquipUI();
        ShowStarIcon();
    }

    public override void InitUIView()
    {
        base.InitUIView();

        InitStr();
        ShowStaticText();
        Show3DModel();
        ShowAttrText();
        ShowTeamBtns();
        //ShowEquipUI();
        ShowStarIcon();
    }

    /// <summary>
    /// 显示属性文本
    /// </summary>
    private void ShowAttrText()
    {
        m_PowerTxt.text = m_Card.GetHeroData().FightVigor.ToString();
        m_AttackTxt.text = m_Card.GetPhysicalAttack().ToString();
        m_DefenceTxt.text = m_Card.GetPhysicalDefence().ToString();
        m_HpTxt.text = m_Card.GetMaxHP().ToString();
        m_HeroLevelTxt.text = m_Card.GetHeroData().Level.ToString();
        m_ZhongZu.sprite = InterfaceControler.GetInst().GetHeroRaceTypeIcon(m_HeroT);
    }

    /// <summary>
    /// 初始化05表字段
    /// </summary>
    private void InitStr()
    {
        m_AptitudeStr.text = "资质";
        m_HeroAptImg.sprite = InterfaceControler.GetInst().GetHeroAptImg(m_HeroT);
    }
    /// <summary>
    /// 显示静态属性文本
    /// </summary>
    private void ShowStaticText()
    {
        m_HeroTliteTxt.text = string.Format(GameUtils.GetHeroNameFontColor(m_Card.GetHeroData().QualityLev), GameUtils.getString(m_HeroT.getTitleID())); 
        m_HeroNameTxt.text = /*string.Format(GameUtils.GetHeroNameFontColor(m_Card.GetHeroData().QualityLev),*/ GameUtils.getString(m_HeroT.getNameID())/*)*/;
    }


    /// <summary>
    /// 显示星级Icon
    /// </summary>
    private void ShowStarIcon()
    {
        int star = m_Card.GetHeroData().StarLevel;
        for (int i = 0; i < m_BrightStar.transform.childCount; ++i)
        {
            m_BrightStar.transform.GetChild(i).gameObject.SetActive(i < star);
        }
        InterfaceControler.GetInst().ShowTypeIcon(m_HeroT, m_HeroTypeImg, m_HeroTypeTxt);
    }

    /// <summary>
    /// 显示阵型按钮
    /// </summary>
    private void ShowTeamBtns()
    {
        int GroupCount = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
        int HeroCount = ObjectSelf.GetInstance().Teams.m_Matrix.GetLength(1);
        for (int i = 0; i < HeroCount; ++i)
        {
            ObjectCard temp = ObjectSelf.GetInstance().HeroContainerBag.FindHero(ObjectSelf.GetInstance().Teams.m_Matrix[GroupCount, i]);

            UI_TeamBtnItem uiTeamBtnItem = null;
            if (m_TeamBtns[i].GetComponent<UI_TeamBtnItem>() != null)
                uiTeamBtnItem = m_TeamBtns[i].GetComponent<UI_TeamBtnItem>();
            else
                uiTeamBtnItem = m_TeamBtns[i].gameObject.AddComponent<UI_TeamBtnItem>();

            uiTeamBtnItem.InitData(temp, m_SelectNo, CurUI.Reparto);
        }
    }


    /// <summary>
    /// 显示装备
    /// </summary>
    private void ShowEquipUI()
    {
        List<EquipData> tempEquipList = m_Card.GetHeroData().HeroEqupDB.EquipList;

        m_EquipImg1.gameObject.SetActive(tempEquipList[0] != null);
        m_EquipImg2.gameObject.SetActive(tempEquipList[1] != null);
        m_EquipImg3.gameObject.SetActive(tempEquipList[2] != null);
        m_EquipImg4.gameObject.SetActive(tempEquipList[3] != null);
        m_EquipImg5.gameObject.SetActive(tempEquipList[4] != null);
        m_EquipImg6.gameObject.SetActive(tempEquipList[5] != null);

        if (tempEquipList[0] != null)
            m_EquipImg1.UpdateEquipment(tempEquipList[0], false);
        if (tempEquipList[1] != null)
            m_EquipImg2.UpdateEquipment(tempEquipList[1], false);
        if (tempEquipList[2] != null)
            m_EquipImg3.UpdateEquipment(tempEquipList[2], false);
        if (tempEquipList[3] != null)
            m_EquipImg4.UpdateEquipment(tempEquipList[3], false);
        if (tempEquipList[4] != null)
            m_EquipImg5.UpdateEquipment(tempEquipList[4], false);
        if (tempEquipList[5] != null)
            m_EquipImg6.UpdateEquipment(tempEquipList[5], false);

        m_EquipImg1.GetComponent<Button>().onClick.AddListener(new UnityAction(onSkinBtnClick));
        m_EquipImg2.GetComponent<Button>().onClick.AddListener(new UnityAction(onSkinBtnClick));
        m_EquipImg3.GetComponent<Button>().onClick.AddListener(new UnityAction(onSkinBtnClick));
        m_EquipImg4.GetComponent<Button>().onClick.AddListener(new UnityAction(onSkinBtnClick));
        m_EquipImg5.GetComponent<Button>().onClick.AddListener(new UnityAction(onSkinBtnClick));
        m_EquipImg6.GetComponent<Button>().onClick.AddListener(new UnityAction(onSkinBtnClick));
    }

    /// <summary>
    /// 显示3D模型
    /// </summary>
    /// <param name="card"></param>
    public void Show3DModel()
    {
        ModelCear();

        m_Point = GameObject.Find("pos").transform;

        ArtresourceTemplate m_Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(m_Card.GetHeroData().GetHeroViewID());
        GameObject _AssetRes = AssetLoader.Inst.GetAssetRes(m_Artresourcedata.getArtresources());
        if (_AssetRes != null)
        {
            if (_AssetRes.GetComponent<NavMeshAgent>() != null)
                _AssetRes.GetComponent<NavMeshAgent>().enabled = false;

            m_Card3Dmodel = Instantiate(_AssetRes, m_Point.position, m_Point.rotation) as GameObject;
            float _zoom = m_Artresourcedata.getArtresources_zoom();
            m_Card3Dmodel.transform.localScale = new UnityEngine.Vector3(_zoom, _zoom, _zoom);
            m_Card3Dmodel.transform.parent = m_Point;

            ////设置3D模型摩擦力
            //m_Card3Dmodel.rigidbody.angularDrag = 2.8f;
            //m_Card3Dmodel.rigidbody.mass = 1.5f;

            Animation anim = m_Card3Dmodel.GetComponent<Animation>();
            if (anim == null) 
                return;

            m_Card3Dmodel.GetComponent<Animation>().Play("Nidle1");
            m_Card3Dmodel.GetComponent<Animation>().wrapMode = WrapMode.Loop;
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
    /// 关闭界面
    /// </summary>
    private void onClose()
    {
        ModelCear();

        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }

    //返回按钮回调
    private void onBackCall()
    {
        onClose();

        UI_HomeControler.Inst.AddUI(UI_FormMgr.UI_ResPath);
    }

    //更换英雄按钮回调
    public void onChangeHeroBtnClick()
    {
        onClose();

        GameObject go = UI_HomeControler.Inst.AddUI(UI_SelectHeroMgr.UI_ResPath);
        UI_SelectHeroMgr uiSelectHeroMgr = go.GetComponent<UI_SelectHeroMgr>();
        uiSelectHeroMgr.SetSelectHeoData(m_Card, m_SelectPos, m_SelectNo);
    }
    //强化按钮回调
    private void onIntensifyBtnClick()
    {
        onClose();
        UI_HomeControler.Inst.AddUI(HeroStrengthen.UI_ResPath);
        HeroStrengthen.Inst.OnClickHeroIcon(m_Card);
    }

    //装备按钮回调
    private void onSkinBtnClick()
    {
        onClose();
        UI_HomeControler.Inst.AddUI(HeroStrengthen.UI_ResPath);
        HeroStrengthen.Inst.OnClickHeroIcon(m_Card);
        HeroStrengthen.Inst.OpenEquipStrengthen();
    }
}
