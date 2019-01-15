using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.UI;
using DreamFaction.Utils;
using GNET;
using DreamFaction.GameNetWork;
using DreamFaction.GameCore;

public class StrengthenEquipment : StrengthenEquipmentBase
{
    public static StrengthenEquipment Inst;
    protected EquipData m_EquipData;
    protected Image m_Bg;
    protected Image m_Icon;
    protected GameObject m_ItemLayout;

    protected GameObject m_Information;
    EquipmentqualityTemplate m_Temp;


    public override void InitUIData ()
    {
        Inst = this;
        base.InitUIData ();
        m_Bg = selfTransform.Find ( "Equipment/Bg" ).GetComponent<Image> ();
        m_Icon = selfTransform.Find ( "Equipment/Icon" ).GetComponent<Image> ();
        m_Information = selfTransform.Find ( "Information" ).gameObject;
        m_ItemLayout = selfTransform.Find ( "ItemList/ItemLayout" ).gameObject;
    }

    public override void InitUIView ()
    {
        base.InitUIView ();
    }

    // һ��ǿ��
    protected override void OnClickBtnOne ()
    {
        // ����
        EquipmentstrengthTemplate eqtemp = DataTemplate.GetInstance ().GetEquipStrengthTemplate ( HeroStrengthen.Inst.m_CurCard.GetHeroRow ().getQosition (), m_Temp.getParts (), m_EquipData.IntensifyLev + 1 );
        int level = eqtemp.getLevel ();

        // �ȽϽ��
        if (ObjectSelf.GetInstance().Money < eqtemp.getNumbers()[0])
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("jinbi_tips1"));
            return;
        }

        if ( HeroStrengthen.Inst.m_CurCard.GetHeroData ().Level < level )
        {
            if ( ObjectSelf.GetInstance ().Level < m_Temp.getReqlevel () )
            {
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("ui_zhuangbeiqianghua8"));
                return;
            }
        }

        // �ж��Ƿ�Ϊ����װ��
        if ( m_Temp.getParts () >= 5 )
        {
            if ( !GetResEnough () )
            {
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("ui_zhuangbeiqianghua6"));
                return;
            }

        }

        // ������������Ϣ
        SendMessage ( 1 );
    }


    // ǿ��һ��
    protected override void OnClickBtnStrengthen ()
    {
        // ����
        EquipmentstrengthTemplate eqtemp = DataTemplate.GetInstance ().GetEquipStrengthTemplate ( HeroStrengthen.Inst.m_CurCard.GetHeroRow ().getQosition (), m_Temp.getParts (), m_EquipData.IntensifyLev + 1 );
        int level = eqtemp.getLevel ();
        if ( HeroStrengthen.Inst.m_CurCard.GetHeroData ().Level < level )
        {
            if ( ObjectSelf.GetInstance ().Level < m_Temp.getReqlevel () )
            {
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("ui_zhuangbeiqianghua8"));
                return;
            }
        }

        // �ȽϽ��
        if ( ObjectSelf.GetInstance ().Money < eqtemp.getNumbers () [ 0 ] )
        {
            //InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("jinbi_tips1"));
            UICommonManager.Inst.ShowMsgBox("", GameUtils.getString("jinbi_tips1"));
            return;
        }

        // �ж��Ƿ�Ϊ����װ��
        if ( m_Temp.getParts () >= 5 )
        {
            if ( !GetResEnough () )
            {
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("ui_zhuangbeiqianghua6"));
                return;
            }

        }

        // ������������Ϣ
        SendMessage ( 0 );
    }

    void SendMessage ( int isstrength )
    {
        CHeroEquipUp proto = new CHeroEquipUp ();
        proto.herokey = ( int ) HeroStrengthen.Inst.m_CurCard.GetHeroData ().GUID.GUID_value;
        proto.islevelup = 0;
        EquipmentqualityTemplate temp = ( EquipmentqualityTemplate ) DataTemplate.GetInstance ().m_EquipmentqualityTable.getTableData ( m_EquipData.TableID );
        proto.equiplocation = temp.getParts ();
        proto.isstrength = isstrength;
        IOControler.GetInstance ().SendProtocol ( proto );
    }

    bool GetResEnough ()
    {
        EquipmentstrengthTemplate equipTemp = DataTemplate.GetInstance().GetEquipStrengthTemplate(HeroStrengthen.Inst.m_CurCard.GetHeroRow().getQosition(), m_Temp.getParts(), m_EquipData.IntensifyLev + 1);

        for ( int i = 0; i < equipTemp.getPropid2 ().Length; ++i )
        {
            int num = GetIdInBagNum ( equipTemp.getPropid2 () [ i ] );
            if ( num < equipTemp.getNumbers2 () [ i ] )
                return false;
        }

        return true;
    }

    private int GetIdInBagNum ( int id )
    {
        int haveNum = -1;
        ObjectSelf.GetInstance ().TryGetItemCountById ( EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, id, ref haveNum );
        return haveNum;
    }

    // ������ʾ��Ϣ
    public void UpdateInfo ( EquipData equipdata )
    {
        // �Ϸ�icon
        m_EquipData = equipdata;
        int tableid = equipdata.TableID;
        m_Temp = ( EquipmentqualityTemplate ) DataTemplate.GetInstance ().m_EquipmentqualityTable.getTableData ( tableid );

        m_Level.text = equipdata.IntensifyLev.ToString ();
        m_Bg.sprite = UIResourceMgr.LoadSprite ( common.defaultPath + GameUtils.GetEquipBgColor ( tableid ) );
        m_Icon.sprite = UIResourceMgr.LoadSprite ( common.defaultPath + m_Temp.getIcon () );

        m_Name.text = m_Temp.getName ();
        m_Name.color = GameUtils.GetEquipNameColor ( tableid );

        // �ȼ�
        m_Information.transform.GetChild ( 0 ).Find ( "AttrName" ).GetComponent<Text> ().text = GameUtils.getString ( "hero_info_sort_level" );
        m_Information.transform.GetChild ( 0 ).Find ( "AttrNumber" ).GetComponent<Text> ().text = equipdata.IntensifyLev + "/" + "10";  // TODO...
        m_Information.transform.GetChild ( 0 ).Find ( "AttrAdd" ).gameObject.SetActive ( false );
        // �м�����
        int [] attrs = m_Temp.getQualityAttribute ();
        int [] attrsvalue = m_Temp.getNumerical ();
        int i = 0;
        for ( ; i < attrs.Length; ++i )
        {
            m_Information.transform.GetChild ( i + 1 ).gameObject.SetActive ( true );
            m_Information.transform.GetChild ( i + 1 ).Find ( "AttrName" ).GetComponent<Text> ().text = GameUtils.GetAttriName ( attrs [ i ] );
            m_Information.transform.GetChild ( i + 1 ).Find ( "AttrNumber" ).GetComponent<Text> ().text = attrsvalue [ i ].ToString ();
            m_Information.transform.GetChild ( i + 1 ).Find ( "AttrAdd" ).gameObject.SetActive ( false );
        }

        for ( int j = i + 1; j < m_Information.transform.childCount; ++j )
        {
            m_Information.transform.GetChild ( j ).gameObject.SetActive ( false );
        }

        // ����
        EquipmentstrengthTemplate eqtemp = DataTemplate.GetInstance ().GetEquipStrengthTemplate ( HeroStrengthen.Inst.m_CurCard.GetHeroRow ().getQosition (), m_Temp.getParts (), equipdata.IntensifyLev + 1 );
        int level = eqtemp.getLevel ();
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

        // �·���ť
        if ( m_Temp.getNextId () == -1 )
        {
            m_Number.gameObject.SetActive ( false );
        }
        else
        {
            m_Number.gameObject.SetActive ( true );
            m_Number.text = eqtemp.getNumbers () [ 0 ].ToString ();
        }

        // ����װ��
        EquipmentstrengthTemplate equipTemp = DataTemplate.GetInstance ().GetEquipStrengthTemplate ( HeroStrengthen.Inst.m_CurCard.GetHeroRow ().getQosition (), m_Temp.getParts (), 1 );
        if ( m_Temp.getParts () >= 5 )
        {
            m_ItemLayout.transform.parent.gameObject.SetActive ( true );
            for ( int k = 0; k < 3; ++k )
            {
                m_ItemLayout.transform.GetChild ( k ).gameObject.SetActive ( k < equipTemp.getPropid2 ().Length );
                if ( k >= equipTemp.getPropid2 ().Length )
                    continue;

                m_ItemLayout.transform.GetChild ( k ).GetComponent<EquipmentItem> ().ShowInfo ( equipTemp.getPropid2 () [ k ], equipTemp.getNumbers2 () [ k ] );
            }
        }
        else
        {
            m_ItemLayout.transform.parent.gameObject.SetActive ( false );
        }
    }






}
