using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DreamFaction.Utils;
using DreamFaction.UI;
using DreamFaction.GameNetWork.Data;
using DreamFaction.GameNetWork;
using DreamFaction.GameCore;
using GNET;
using DreamFaction.GameEventSystem;
using DreamFaction.UI.Core;

public class EquipLetGood : EquipLetGoodBase
{
    public static EquipLetGood Inst;
    EquipData m_EquipData;
    EquipData m_TempData = new EquipData ();
    GameObject m_Information;
    GameObject m_ItemLayout;

    protected Image m_LeftIcon;
    protected Image m_LeftBg;
    protected GameObject Star;
    protected Image m_RightBg;
    protected Image m_RightIcon;

    protected EquipmentqualityTemplate m_Temp;
    protected EquipmentqualityTemplate m_NextTemp;

    public override void InitUIData ()
    {
        Inst = this;
        base.InitUIData ();

        m_Information = selfTransform.Find ( "Information" ).gameObject;
        m_LeftIcon = selfTransform.Find ( "Right/LeftIcon" ).GetComponent<Image> ();
        m_LeftBg = selfTransform.Find ( "Right/LeftBg" ).GetComponent<Image> ();
        Star = selfTransform.Find ( "Right/Star" ).gameObject;
        m_RightBg = selfTransform.Find ( "Equipment/Bg" ).GetComponent<Image> ();
        m_RightIcon = selfTransform.Find ( "Equipment/Icon" ).GetComponent<Image> ();

        m_ItemLayout = selfTransform.Find ( "ItemList/ItemLayout" ).gameObject;

        GameEventDispatcher.Inst.addEventListener ( GameEventID.I_EquipLetGood, LetGoodSeccess );
    }

    public override void InitUIView ()
    {
        base.InitUIView ();
    }

    protected override void OnClickBtnStrengthen ()
    {
        if ( m_Temp.getNextId () == -1 )
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("ui_zhuangbeiqianghua11"));
            return;
        }

        if ( ObjectSelf.GetInstance ().Money < m_Temp.getDemandmoney () )
        {
            //InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("jinbi_tips1"));
            UICommonManager.Inst.ShowMsgBox("",GameUtils.getString("jinbi_tips1"));
            return;
        }

        if ( HeroStrengthen.Inst.m_CurCard.GetHeroData ().Level < m_Temp.getReqlevel () )
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("ui_yingxiongqianghua_jinjie3").Replace("{0}", m_Temp.getReqlevel().ToString()));
            return;
        }

        // 需求道具数量对比
        for ( int i = 0; i < m_Temp.getPropId ().Length; ++i )
        {
            int num = GetIdInBagNum ( m_Temp.getPropId () [ i ] );
            if ( num < m_Temp.getNumbers () [ i ] )
            {
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("ui_zhuangbeiqianghua9"));
                return;
            }
        }

        SendMessage ();
    }

    void SendMessage ()
    {
        CHeroEquipUp proto = new CHeroEquipUp ();
        proto.herokey = ( int ) HeroStrengthen.Inst.m_CurCard.GetHeroData ().GUID.GUID_value;
        proto.islevelup = 1;
        proto.equiplocation = m_Temp.getParts ();
        IOControler.GetInstance ().SendProtocol ( proto );

        m_TempData = m_EquipData;
    }

    void LetGoodSeccess ()
    {
        UI_HomeControler.Inst.AddUI ( EquipLetGoodPop.UI_ResPath );
        EquipLetGoodPop.Inst.ShowInfo ( m_TempData );
    }

    // 刷新数据
    public void UpdateInfo ( EquipData equipdata )
    {

        m_EquipData = equipdata;
        int tableid = equipdata.TableID;
        m_Temp = ( EquipmentqualityTemplate ) DataTemplate.GetInstance ().m_EquipmentqualityTable.getTableData ( tableid );
        m_NextTemp = ( EquipmentqualityTemplate ) DataTemplate.GetInstance ().m_EquipmentqualityTable.getTableData ( m_Temp.getNextId () );
        // 已经到达最高级
        //if ( m_Temp.getNextId () == -1 )
        //{

        //    return;
        //}



        m_LeftLevel.text = equipdata.IntensifyLev.ToString ();
        // TODO...
        m_LeftBg.sprite = UIResourceMgr.LoadSprite ( common.defaultPath + GameUtils.GetEquipBgColor ( tableid ) );
        m_LeftIcon.sprite = UIResourceMgr.LoadSprite ( common.defaultPath + m_Temp.getIcon () );

        m_LeftName.text = m_Temp.getName ();
        m_LeftName.color = GameUtils.GetEquipNameColor ( tableid );

        // 星级
        for ( int k = 0; k < 5; ++k )
        {
            Star.transform.GetChild ( k ).gameObject.SetActive ( k < m_Temp.getQualityLevel () );
        }

        // 中间属性
        int [] attrs = m_Temp.getQualityAttribute ();
        int [] attrsvalue = m_Temp.getNumerical ();
        int i = 0;
        for ( i = 0; i < attrs.Length; ++i )
        {
            m_Information.transform.GetChild ( i ).gameObject.SetActive ( true );
            m_Information.transform.GetChild ( i ).Find ( "AttrName" ).GetComponent<Text> ().text = GameUtils.GetAttriName ( attrs [ i ] );
            m_Information.transform.GetChild ( i ).Find ( "AttrNumber" ).GetComponent<Text> ().text = "+" + attrsvalue [ i ].ToString ();
            m_Information.transform.GetChild ( i ).Find ( "AttrAdd" ).GetComponent<Text> ().text = "+" + ( m_NextTemp.getNumbers () [ i ] - m_Temp.getNumbers () [ i ] ).ToString ();

            Debug.Log ( "Debug.Log ( attrsvalue [ i ].ToString () ); " + attrsvalue [ i ].ToString () );

        }

        // 达到最高级
        if ( m_Temp.getNextId () == -1 )
        {
            return;
        }

        // 右边 对应的显示
        m_RightLevel.text = m_EquipData.IntensifyLev.ToString ();
        // TODO...
        m_RightBg.sprite = UIResourceMgr.LoadSprite ( common.defaultPath + GameUtils.GetEquipBgColor ( m_NextTemp.getId () ) );
        m_RightIcon.sprite = UIResourceMgr.LoadSprite ( common.defaultPath + m_NextTemp.getIcon () );

        m_Name.text = m_NextTemp.getName ();
        m_Name.color = GameUtils.GetEquipNameColor ( m_NextTemp.getId () );

        for ( int j = i; j < m_Information.transform.childCount; ++j )
        {
            m_Information.transform.GetChild ( j ).gameObject.SetActive ( false );
        }

        // 刷新列表
        for ( int k = 0; k < 6; ++k )
        {
            m_ItemLayout.transform.GetChild ( k ).gameObject.SetActive ( k < m_Temp.getPropId ().Length );
            if ( k >= m_Temp.getPropId ().Length )
                continue;

            m_ItemLayout.transform.GetChild ( k ).GetComponent<EquipmentItem> ().ShowInfo ( m_Temp.getPropId () [ k ], m_Temp.getNumbers () [ k ] );
        }

        // 条件
        int level = m_Temp.getReqlevel ();
        if ( HeroStrengthen.Inst.m_CurCard.GetHeroData ().Level < level )
        {
            m_Condition.gameObject.SetActive ( true );
            string str = string.Format ( GameUtils.getString ( "ui_yingxiongqianghua_jinjie3" ), level.ToString () );
            m_Condition.text = str;
        }
        else
        {
            m_Condition.gameObject.SetActive ( false );
        }

        m_Number.text = m_Temp.getDemandmoney ().ToString ();
    }

    private int GetIdInBagNum ( int id )
    {
        int haveNum = -1;
        ObjectSelf.GetInstance ().TryGetItemCountById ( EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, id, ref haveNum );
        return haveNum;
    }

    public override void OnDestroy ()
    {
        base.OnDestroy ();

        GameEventDispatcher.Inst.removeEventListener ( GameEventID.I_EquipLetGood, LetGoodSeccess );
    }

}
