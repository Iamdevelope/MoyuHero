using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using UnityEngine.UI;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;
using System.Collections.Generic;

public class HeroStrengthen : HeroStrengthenBase
{
    public static HeroStrengthen Inst;
    public static string UI_ResPath = "UI_Home/HeroStrengthen_1_4";

    private List<ObjectCard> m_HeroList = new List<ObjectCard> ();

    protected LoopLayout m_HeroLayout;   // 英雄列表
    public ObjectCard m_CurCard = null;  // 英雄卡牌

    protected GameObject m_BtnGroups;   // 按钮组
    protected GameObject m_EquipGroups;   // 按钮组
    protected Button m_HeroType;         // 英雄类型
    protected GameObject m_BrightStar;  // 星级
    protected GameObject m_HeroAttr;    // 属性面板
    protected Image m_Qualifications;    // 资质

    protected HeroAttrPanel m_CurAttrPanel = null;  // 现在打开的属性面板   

    protected int m_CurBtnIndex = -1;
    protected GameObject m_AttrPanel;   // 右侧功能面板父节点
    protected string m_AdvancedUI = "UI_Advanced";//升阶
    protected string m_HeroBringUI = "UI_HeroBringUp";//培养
    protected string m_HeroMysticUI = "UI_Mystic";//秘术
    protected string m_QualityProUI = "UI_QualityProUI";//升品
    protected string m_UpgradePanelUI = "UpgradePanel";//升级
    protected string m_SkillUI = "UI_HeroSkill";//技能

    protected string m_EquipStrengthenUI = "StrengthenPanel";//装备强化
    protected string m_EquipLetGoodUI = "EquipLetGood";//装备升品

    protected EquipmentPanel m_EquipmentPanel;

    private GameObject Card3Dmodel;                                                      //当前实例化3D模型
    private Transform m_Point;                                                            //3D模型实例化位置
    private GameObject m_ModelRotaeBtn;                                                    //3D模型旋转按钮
    private bool iSRotate;                                                                //3D模型旋转开关
    private float Card3DRoteh;                                                           //3D模型旋转参数
    private float Card3DRotev;                                                           //3D模型旋转参数
    private Vector3 Torque;                                                              //旋转力数值


    protected bool isEquipType = false;                                                   // 是否是装备模式

    public override void InitUIData ()
    {
        Inst = this;
        base.InitUIData ();

        SortHero ();


        //if ( m_HeroList.Count > 0 )
        //{
        //    m_CurCard = m_HeroList [ 0 ];
        //    m_HeroLayout = selfTransform.Find ( "HeroList/HeroLayout" ).GetComponent<LoopLayout> ();

        //    m_HeroLayout.cellCount = m_HeroList.Count;
        //    m_HeroLayout.updateCellEvent = UpdateHeroCellItem;
        //    m_HeroLayout.Reload ();
        //}

        m_BtnGroups = selfTransform.Find ( "BtnGroups" ).gameObject;
        m_EquipGroups = selfTransform.Find ( "EquipGroups" ).gameObject;
        m_BtnGroups.SetActive ( true );
        m_EquipGroups.SetActive ( false );

        //
        m_AttrPanel = selfTransform.Find ( "AttrPanel" ).gameObject;
        m_Qualifications = selfTransform.Find ( "HeroInfo/Right/Qualifications" ).GetComponent<Image> ();
        //
        m_HeroType = selfTransform.Find ( "HeroInfo/Left/HeroType" ).GetComponent<Button> ();
        m_HeroType.onClick.AddListener ( UICommonManager.Inst.ShowHeroLocatUI );
        m_BrightStar = selfTransform.Find ( "HeroInfo/Left/HeroStar/BrightStar" ).gameObject;

        m_HeroAttr = selfTransform.Find ( "HeroAttr" ).gameObject;

        // 模型
        m_Point = GameObject.Find ( "pos" ).transform;
        m_ModelRotaeBtn = selfTransform.FindChild ( "ModelRotaeBtn" ).gameObject;
        EventTriggerListener.Get ( m_ModelRotaeBtn ).onDown = OnRotateDown;
        EventTriggerListener.Get ( m_ModelRotaeBtn ).onUp = OnRotatUp;

        // 显示 3D 模型
        //Show3DModel ( m_HeroList [ 0 ] );

        //
        InitTextString ();

        GameEventDispatcher.Inst.addEventListener ( GameEventID.Net_RefreshHero, OnRefreshHero );
    }

    public override void InitUIView ()
    {
        base.InitUIView ();

        if ( !isEquipType )
        {
            ClickSwitchBtn(m_UpgradePanelUI, 2);
        }
        else
        {
            ClickSwitchBtn ( m_EquipStrengthenUI, 6 );
        }

        //OnRefreshHero ();
    }

    void SortHero ()
    {
        m_HeroList.Clear ();
        List<ObjectCard> temp = new List<ObjectCard> ();
        List<ObjectCard> objCardList = ObjectSelf.GetInstance ().HeroContainerBag.GetYetFormList ( ref temp );

        objCardList.Sort ( new HeroComparer () );
        for ( int i = 0; i < objCardList.Count; i++ )
        {
            m_HeroList.Add ( objCardList [ i ] );
        }

        temp.Sort ( new HeroComparer () );
        for ( int i = 0; i < temp.Count; i++ )
        {
            m_HeroList.Add ( temp [ i ] );
        }
    }

    public void OpenEquipStrengthen ()
    {
        isEquipType = true;

        GameObject obj = Instantiate ( Resources.Load ( "UI/Prefabs/HeroStrengthen/EquipmentPanel" ) ) as GameObject;
        obj.transform.SetParent ( transform, false );
        m_EquipmentPanel = obj.transform.GetComponent<EquipmentPanel> ();

        // 切换标题
        selfTransform.Find ( "TopPanel/TopTittle/Title" ).gameObject.SetActive ( false );
        selfTransform.Find ( "TopPanel/TopTittle/Title_2" ).gameObject.SetActive ( true );

        ClickSwitchBtn ( m_EquipStrengthenUI, 6 );

        if ( m_EquipmentPanel != null )
        {
            m_EquipmentPanel.UpdateInfo ( m_CurCard );
        }

        // 
        m_BtnGroups.SetActive ( false );
        m_EquipGroups.SetActive ( true );

        m_Facelift.gameObject.SetActive ( false );
        //


        //

        //
    }

    // 读取 05 表
    void InitTextString ()
    {

    }

    #region 英雄模型旋转
    // 英雄模型旋转
    private void OnRotateDown ( GameObject a )
    {
        iSRotate = true;
    }
    private void OnRotatUp ( GameObject a )
    {
        iSRotate = false;
    }

    public override void UpdateUIView ()
    {
        base.UpdateUIView ();
        if ( iSRotate )
        {
            Card3DRoteh = Input.GetAxis ( "Mouse X" );//有正左负
            Card3DRotev = Input.GetAxis ( "Mouse Y" );//上正下负
        }
        else
        {
            Card3DRoteh = 0;
            Card3DRotev = 0;
        }
        Torque = new Vector3 ( Card3DRotev, -Card3DRoteh, 0 );
    }
    //刷新3D模型旋转
    private void FixedUpdate ()
    {
        if ( Card3Dmodel != null )
        {
            Card3Dmodel.rigidbody.AddTorque ( Torque * 10 );

        }
    }

    public void Show3DModel ( ObjectCard _card )
    {
        ModelCear ();
        //通过英雄数据表中的资源数据表ID得到资源表数据
        ArtresourceTemplate _Artresourcedata = new ArtresourceTemplate ();
        _Artresourcedata = ( ArtresourceTemplate ) DataTemplate.GetInstance ().m_ArtresourceTable.getTableData ( _card.GetHeroData ().GetHeroViewID () );
        //通过资源表获取到角色默认美术资源（名称）     通过该名称获取到动态加载数据返回一个对象
        AssetLoader.Inst.DynamicLoadHeroCardRes(_card.GetHeroRow().getId());
        StartCoroutine( InitShow3DModel ( _Artresourcedata ));
    }

    public IEnumerator InitShow3DModel ( ArtresourceTemplate artT )
    {
        while (AssetLoader.Inst.IsReadyOver == false)
        {
            yield return 1;
        }

        GameObject _AssetRes = AssetLoader.Inst.GetAssetRes ( artT.getArtresources () );
        if ( _AssetRes != null )
        {
            if ( _AssetRes.GetComponent<NavMeshAgent> () != null )
                _AssetRes.GetComponent<NavMeshAgent> ().enabled = false;
            //实例化该对象
            Card3Dmodel = Instantiate ( _AssetRes, m_Point.position, m_Point.rotation ) as GameObject;
            float _zoom = artT.getArtresources_zoom ();
            Card3Dmodel.transform.localScale = new UnityEngine.Vector3 ( _zoom, _zoom, _zoom );
            Card3Dmodel.transform.parent = m_Point;
            //设置3D模型摩擦力
            Card3Dmodel.rigidbody.angularDrag = 2.8f;
            Card3Dmodel.rigidbody.mass = 1.5f;
            //_obj.transform.localScale = new Vector3(1.3f,1.3f,1.3f);
            Animation anim = Card3Dmodel.GetComponent<Animation> ();
            if (anim != null)
            {
                Card3Dmodel.GetComponent<Animation>().Play("Nidle1");
                Card3Dmodel.GetComponent<Animation>().wrapMode = WrapMode.Loop;
            }
                
        }
    }

    public GameObject GetCard3Dmodel ()
    {
        return Card3Dmodel;
    }

    private void ModelCear ()
    {
        if ( Card3Dmodel != null )
            Destroy ( Card3Dmodel );
    }
    #endregion

    // 加载英雄列表
    void UpdateHeroCellItem ( int index, RectTransform cell )
    {
        HeroCellItem item = cell.GetComponent<HeroCellItem> ();
        if ( item == null )
        {
            item = cell.gameObject.AddComponent<HeroCellItem> ();
        }

        item.index = index;

        ObjectCard card = m_HeroList [ index ];
        item.UpdateHeroShow ( card );
        item.SetClickHeroIcon ();

        if ( m_CurCard.GetHeroData ().GUID == card.GetHeroData ().GUID )
        {
            // 显示选中状态
            item.SetSelectState ( true );
        }
        else
        {
            item.SetSelectState ( false );
        }
    }

    public void OnRefreshHero ()
    {
        //SortHero ();

        if ( m_CurCard == null )
        {
            m_CurCard = m_HeroList [ 0 ];
        }

        int count = ObjectSelf.GetInstance ().HeroContainerBag.GetHeroList ().Count;
        for ( int i = 0; i < count; ++i )
        {
            ObjectCard card = m_HeroList [ i ];
            if ( card.GetGuid () == m_CurCard.GetGuid () )
            {
                m_CurCard = card;
                break;
            }
        }

        // 刷新英雄列表
        //m_HeroLayout.UpdateCell ();

        // 更新英雄信息
        UpdateHeroInfo ();

        // 更新资质信息
        UpdateQualification ();

        // 更新下面属性面板
        UpdateHeroAttr ();

        // 更新右边属性面板
        if ( m_CurAttrPanel != null )
        {
            m_CurAttrPanel.ShowHeroInfo ( m_CurCard );
        }

        if ( isEquipType )
        {
            m_EquipmentPanel.UpdateInfo ( m_CurCard );
        }

        Show3DModel(m_CurCard);
    }

    // 点击英雄图标 Icon
    public void OnClickHeroIcon ( ObjectCard card )
    {
        if ( m_CurCard == card )
            return;
        m_CurCard = card;

        // 更新英雄列表
        //m_HeroLayout.UpdateCell ();

        Show3DModel ( m_CurCard );

        // 更新英雄信息
        UpdateHeroInfo ();

        // 更新资质信息
        UpdateQualification ();

        // 更新下面属性面板
        UpdateHeroAttr ();

        // 更新右边属性面板
        if ( m_CurAttrPanel != null )
        {
            m_CurAttrPanel.ShowHeroInfo ( m_CurCard );
        }

        if ( isEquipType )
        {
            m_EquipmentPanel.UpdateInfo ( m_CurCard );
        }
    }

    // 返回回调
    protected override void OnClickBackBtn ()
    {
        ModelCear ();
        UI_HomeControler.Inst.ReMoveUI ( gameObject );
    }

    // 进阶回调
    protected override void OnClickAdvanced ()
    {
        ClickSwitchBtn ( m_AdvancedUI, 0 );
    }

    // 升品回调
    protected override void OnClickLetGood ()
    {
        ClickSwitchBtn ( m_QualityProUI, 1 );
    }

    // 升级回调
    protected override void OnClickUpgrade ()
    {
        ClickSwitchBtn ( m_UpgradePanelUI, 2 );
    }

    // 技能回调
    protected override void OnClickSkill ()
    {
        ClickSwitchBtn ( m_SkillUI, 3 );
    }

    // 秘法回调
    protected override void OnClickArcane ()
    {
        ClickSwitchBtn ( m_HeroMysticUI, 4 );
    }

    // 培养
    protected override void OnClickCulture ()
    {
        ClickSwitchBtn ( m_HeroBringUI, 5 );
    }

    protected override void OnClickEquipStrengthen ()
    {
        ClickSwitchBtn ( m_EquipStrengthenUI, 6 );
    }

    protected override void OnClickEquipLetGood ()
    {
        ClickSwitchBtn ( m_EquipLetGoodUI, 7 );
    }

    // 点击切换按钮
    public void ClickSwitchBtn ( string name, int index )
    {
        if ( index == m_CurBtnIndex )
            return;

        m_CurBtnIndex = index;

        // 改变按钮状态   TODO...
        if ( isEquipType == false )
        {
            for ( int i = 0; i < m_BtnGroups.transform.childCount; ++i )
            {
                Button btn = m_BtnGroups.transform.GetChild ( i ).GetComponent<Button> ();
                if ( i == index )
                {
                    btn.GetComponent<Image> ().sprite = UIResourceMgr.LoadSprite ( common.defaultPath + "img_TY_0013" );

                    //btn.transform.Find ( "Image" ).GetComponent<Image> ().sprite = UIResourceMgr.LoadSprite ( common.defaultPath +
                    //btn.transform.Find ( "Image" ).GetComponent<Image> ().sprite.name );
                }
                else
                {
                    btn.GetComponent<Image> ().sprite = UIResourceMgr.LoadSprite ( common.defaultPath + "img_TY_0012" );
                }
                btn.image.SetNativeSize();
            }
        }
        else
        {
            for ( int i = 0; i < m_EquipGroups.transform.childCount; ++i )
            {
                Button btn = m_EquipGroups.transform.GetChild ( i ).GetComponent<Button> ();
                if ( i + 6 == index )
                {
                    btn.GetComponent<Image> ().sprite = UIResourceMgr.LoadSprite ( common.defaultPath + "img_TY_0013" );
                }
                else
                {
                    btn.GetComponent<Image> ().sprite = UIResourceMgr.LoadSprite ( common.defaultPath + "img_TY_0012" );
                }
                btn.image.SetNativeSize();
            }
        }

        // 将其他所有 gameobject 设置为 enable
        for ( int i = 0; i < m_AttrPanel.transform.childCount; ++i )
        {
            m_AttrPanel.transform.GetChild ( i ).gameObject.SetActive ( false );
        }

        GameObject obj = null;
        try
        {
            // 取得当前技能面板
            obj = m_AttrPanel.transform.Find ( name ).gameObject;
        }
        catch
        {

        }

        if ( obj == null )
        {
            // 如果当前技能面板为空，则实例化
            obj = Instantiate ( Resources.Load ( "UI/Prefabs/HeroStrengthen/" + name ) ) as GameObject;
            obj.name = name;
            obj.transform.SetParent ( m_AttrPanel.transform, false );
            obj.transform.SetSiblingIndex ( m_AttrPanel.transform.childCount );
        }

        obj.SetActive ( true );
        m_CurAttrPanel = obj.GetComponent<HeroAttrPanel> ();
        m_CurAttrPanel.ShowHeroInfo ( m_CurCard );

        if ( isEquipType )
        {
            m_EquipmentPanel.UpdateInfo ( m_CurCard );
        }
    }

    // 更新英雄属性
    public void UpdateHeroAttr ()
    {
        m_HeroAttr.transform.Find ( "PowerTxt" ).GetComponent<Text> ().text = m_CurCard.GetHeroData ().FightVigor.ToString ();
        m_HeroAttr.transform.Find ( "AttackTxt" ).GetComponent<Text> ().text = m_CurCard.GetPhysicalAttack ().ToString ();
        m_HeroAttr.transform.Find ( "DefenceTxt" ).GetComponent<Text> ().text = m_CurCard.GetPhysicalDefence ().ToString ();
        m_HeroAttr.transform.Find ( "HpTxt" ).GetComponent<Text> ().text = m_CurCard.GetMaxHP ().ToString ();
    }

    // 更新英雄信息
    public void UpdateHeroInfo ()
    {
        HeroTemplate heroItem = m_CurCard.GetHeroRow ();

        // 英雄类型
        InterfaceControler.GetInst ().ShowTypeIcon ( m_CurCard.GetHeroRow (), m_HeroType.GetComponent<Image> (), m_ActName );

        m_ZhongZu.sprite = InterfaceControler.GetInst().GetHeroRaceTypeIcon(heroItem);
        m_Level.text = m_CurCard.GetHeroData ().Level.ToString ();
        m_HeroName.text = string.Format ( GameUtils.GetHeroNameFontColor ( m_CurCard.GetHeroData ().QualityLev ), GameUtils.getString ( heroItem.getTitleID () ) );
        //名称显示
        //m_TypeName.text = string.Format(GameUtils.GetHeroNameFontColor(m_CurCard.GetHeroData().QualityLev), GameUtils.getString(heroItem.getNameID()));
        m_TypeName.text = GameUtils.getString(heroItem.getNameID());
        
        // 星级
        int star = m_CurCard.GetHeroData ().StarLevel;
        for ( int i = 0; i < m_BrightStar.transform.childCount; ++i )
        {
            m_BrightStar.transform.GetChild ( i ).gameObject.SetActive ( i < star );
        }
    }

    // 更新英雄资质
    public void UpdateQualification ()
    {
        m_Qualifications.sprite = InterfaceControler.GetInst ().GetHeroAptImg ( m_CurCard.GetHeroRow () );
    }

    void OnClickHeroType ()
    {

    }

    void OnDestroy ()
    {
        base.OnDestroy ();
        ModelCear ();

        GameEventDispatcher.Inst.removeEventListener ( GameEventID.Net_RefreshHero, OnRefreshHero );
    }
}
