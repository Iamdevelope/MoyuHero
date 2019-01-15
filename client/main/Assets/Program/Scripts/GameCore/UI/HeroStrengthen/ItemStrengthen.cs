using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using UnityEngine.UI;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;

public class ItemStrengthen : ItemStrengthenBase
{
    public static ItemStrengthen Inst;
    public static string UI_ResPath = "UI_Home/ItemStrengthen_2_0";

    protected LoopLayout m_HeroLayout;   // 英雄列表
    protected ObjectCard m_CurCard = null;  // 英雄卡牌

    protected GameObject m_EquipmentLayout;    // 左侧装备面板

    protected GameObject m_BtnGroups;   // 按钮组
    protected Image m_HeroType;         // 英雄类型
    protected GameObject m_BrightStar;

    protected HeroAttrPanel m_CurAttrPanel = null;  // 现在打开的属性面板

    protected int m_CurBtnIndex = -1;
    protected GameObject m_AttrPanel;   // 右侧功能面板父节点
    protected string m_AdvancedUI = "HeroStrengthen/UI_Advanced";
    protected string m_HeroBringUI = "HeroStrengthen/UI_HeroBringUp";

    private GameObject Card3Dmodel;                                                      //当前实例化3D模型
    private Transform m_Point;                                                            //3D模型实例化位置
    private GameObject m_ModelRotaeBtn;                                                    //3D模型旋转按钮
    private bool iSRotate;                                                                //3D模型旋转开关
    private float Card3DRoteh;                                                           //3D模型旋转参数
    private float Card3DRotev;                                                           //3D模型旋转参数
    private Vector3 Torque;                                                              //旋转力数值
    protected Button m_BackBtn;

    public override void InitUIData ()
    {
        Inst = this;
        base.InitUIData ();

        if ( ObjectSelf.GetInstance ().HeroContainerBag.GetHeroList ().Count > 0 )
        {
            m_CurCard = ObjectSelf.GetInstance ().HeroContainerBag.GetHeroList () [ 0 ];
            m_HeroLayout = selfTransform.Find ( "HeroList/HeroLayout" ).GetComponent<LoopLayout> ();

            m_HeroLayout.cellCount = ObjectSelf.GetInstance ().HeroContainerBag.GetHeroList ().Count;
            m_HeroLayout.updateCellEvent = UpdateHeroCellItem;
            m_HeroLayout.Reload ();
        }

        m_BtnGroups = selfTransform.Find ( "Mainbutton" ).gameObject;

        //
        m_AttrPanel = selfTransform.Find ( "AttrPanel" ).gameObject;

        //
        m_HeroType = selfTransform.Find ( "Left/Img_Race" ).GetComponent<Image> ();
        m_BrightStar = selfTransform.Find ( "Left/HeroStar/BrightStar" ).gameObject;

        // 模型
        m_Point = GameObject.Find ( "pos" ).transform;
        m_ModelRotaeBtn = selfTransform.FindChild ( "ModelRotaeBtn" ).gameObject;
        EventTriggerListener.Get ( m_ModelRotaeBtn ).onDown = OnRotateDown;
        EventTriggerListener.Get ( m_ModelRotaeBtn ).onUp = OnRotatUp;


        m_BackBtn = selfTransform.FindChild ( "TopPanel/TopTittle/BackBtn" ).GetComponent<Button> ();
        m_BackBtn.onClick.AddListener ( OnClickBackBtn );

        // 显示 3D 模型
        Show3DModel ( ObjectSelf.GetInstance ().HeroContainerBag.GetHeroList () [ 0 ] );

        //
        InitTextString ();

        m_EquipmentLayout = selfTransform.Find ( "EquipmentPanel/EquipmentLayout" ).gameObject;

        GameEventDispatcher.Inst.addEventListener ( GameEventID.Net_RefreshHero, OnRefreshHero );
    }


    public override void InitUIState ()
    {
        base.InitUIState ();
    }

    // 读取 05 表
    void InitTextString ()
    {

    }

    // 加载英雄列表
    void UpdateHeroCellItem ( int index, RectTransform cell )
    {
        HeroCellItem item = cell.GetComponent<HeroCellItem> ();
        if ( item == null )
        {
            item = cell.gameObject.AddComponent<HeroCellItem> ();
        }

        item.index = index;

        ObjectCard card = ObjectSelf.GetInstance ().HeroContainerBag.GetHeroList () [ index ];
        item.UpdateHeroShow ( card );
        item.SetClickItemIcon ();

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
        Show3DModel ( _Artresourcedata );
    }

    public void Show3DModel ( ArtresourceTemplate artT )
    {
        GameObject _AssetRes = AssetLoader.Inst.GetAssetRes ( artT.getArtresources () );
        if ( _AssetRes != null && _AssetRes.GetComponent<NavMeshAgent> () != null )
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
        if ( anim == null )
            return;
        Card3Dmodel.GetComponent<Animation> ().Play ( "Nidle1" );
        Card3Dmodel.GetComponent<Animation> ().wrapMode = WrapMode.Loop;
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


    public void OnRefreshHero ()
    {
        if ( m_CurCard == null )
            return;

        int count = ObjectSelf.GetInstance ().HeroContainerBag.GetHeroList ().Count;
        for ( int i = 0; i < count; ++i )
        {
            ObjectCard card = ObjectSelf.GetInstance ().HeroContainerBag.GetHeroList () [ i ];
            if ( card.GetGuid () == m_CurCard.GetGuid () )
            {
                m_CurCard = card;
                break;
            }
        }

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
    }

    // 返回回调
    protected void OnClickBackBtn ()
    {
        ModelCear ();
        UI_HomeControler.Inst.ReMoveUI ( gameObject );
    }

    protected virtual void OnClickImg_Facelift ()
    {
    }

    protected virtual void OnClickBtn_Advanced ()
    {
    }

    protected virtual void OnClickBtn_Lgoods ()
    {
    }

    protected virtual void OnClickBtn_Upgrade ()
    {
    }

    protected virtual void OnClickBtn_Skill ()
    {
    }

    protected virtual void OnClickBtn_Arcane ()
    {
    }

    protected virtual void OnClickBtn_Culture ()
    {
    }

    // 点击英雄图标 Icon
    public void OnClickItemIcon ( ObjectCard card )
    {
        if ( m_CurCard == card )
            return;
        m_CurCard = card;

        // 更新英雄列表
        m_HeroLayout.UpdateCell ();

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
    }

    // 点击切换按钮
    protected void ClickSwitchBtn ( string name, int index )
    {
        if ( index == m_CurBtnIndex )
            return;

        m_CurBtnIndex = index;

        // 改变按钮状态
        for ( int i = 0; i < m_BtnGroups.transform.childCount; ++i )
        {
            Button btn = m_BtnGroups.transform.GetChild ( i ).GetComponent<Button> ();
            if ( i == index )
            {
                btn.GetComponent<Image> ().sprite = UIResourceMgr.LoadSprite ( common.defaultPath + "img_0103" );
            }
            else
            {
                btn.GetComponent<Image> ().sprite = UIResourceMgr.LoadSprite ( common.defaultPath + "img_0102" );
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
            obj = Instantiate ( Resources.Load ( "UI/Prefabs/" + name ) ) as GameObject;
            obj.transform.SetParent ( m_AttrPanel.transform, false );
            obj.transform.SetSiblingIndex ( m_AttrPanel.transform.childCount );
        }

        obj.SetActive ( true );
        m_CurAttrPanel = obj.GetComponent<HeroAttrPanel> ();
        m_CurAttrPanel.ShowHeroInfo ( m_CurCard );
    }

    // 更新英雄属性
    public void UpdateHeroAttr ()
    {

    }

    // 更新英雄信息
    public void UpdateHeroInfo ()
    {
        HeroTemplate heroItem = m_CurCard.GetHeroRow ();

        // 英雄类型
        if ( heroItem.getCamp () == 1 )//生灵
        {
            m_HeroType.sprite = UIResourceMgr.LoadSprite ( common.defaultPath + "UI_Zhongzu_01" );
        }
        else if ( heroItem.getCamp () == 2 )//神抵
        {
            m_HeroType.sprite = UIResourceMgr.LoadSprite ( common.defaultPath + "UI_Zhongzu_03" );
        }
        else if ( heroItem.getCamp () == 3 )//恶魔
        {
            m_HeroType.sprite = UIResourceMgr.LoadSprite ( common.defaultPath + "UI_Zhongzu_02" );
        }

        m_Text_LV100.text = m_CurCard.GetHeroData ().Level.ToString ();
        m_Text_Title.text = GameUtils.getString ( heroItem.getTitleID () );
        //名称显示
        m_Text_Name.text = GameUtils.getString ( heroItem.getNameID () );

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

    }

    // 更新左侧装备列表
    public void UpdateEquipment ()
    {
        // 重新获取数据，刷新 UI
        for ( int i = 0; i < m_EquipmentLayout.transform.childCount; ++i )
        {
            Equipment equip = m_EquipmentLayout.transform.GetChild ( i ).GetComponent< Equipment>();

            // 获取数据

            //equip.UpdateEquipment ( i );
        }
    }

    void OnDestroy ()
    {
        base.OnDestroy ();
        GameEventDispatcher.Inst.removeEventListener ( GameEventID.Net_RefreshHero, OnRefreshHero );
    }
}
