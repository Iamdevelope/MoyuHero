using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.Utils;
using DreamFaction.UI;

public class EquipLetGoodPop : EquipLetGoodPopBase
{
    public static EquipLetGoodPop Inst;
    public static string UI_ResPath = "HeroStrengthen/UI_EquipLetGoodPop_1_2";

    protected float m_CurTime = 0.0f;
    protected float m_AllTime = 2.0f;

    Image m_LeftIcon;
    Image m_LeftBg;
    GameObject m_Star;

    Image m_RightBg;
    Image m_RightIcon;
    GameObject m_AttrList;

    public override void InitUIData ()
    {
        Inst = this;
        base.InitUIData ();
        m_LeftBg = selfTransform.Find ( "Panel/EquipmentIcon/LeftBg" ).GetComponent<Image> ();
        m_LeftIcon = selfTransform.Find ( "Panel/EquipmentIcon/Icon" ).GetComponent<Image> ();
        m_Star = selfTransform.Find ( "Panel/EquipmentIcon/Star" ).gameObject;

        m_RightBg = selfTransform.Find ( "Panel/Equipment/Bg" ).GetComponent<Image> ();
        m_RightIcon = selfTransform.Find ( "Panel/Equipment/Icon" ).GetComponent<Image> ();
        m_AttrList = selfTransform.Find ( "Panel/AttrList" ).gameObject;
    }

    public void ShowInfo ( EquipData equipdata )
    {
        int tableid = equipdata.TableID;
        EquipmentqualityTemplate temp = ( EquipmentqualityTemplate ) DataTemplate.GetInstance ().m_EquipmentqualityTable.getTableData ( tableid );
        Debug.Log ( "tableid" + tableid );
        EquipmentqualityTemplate nextTemp = ( EquipmentqualityTemplate ) DataTemplate.GetInstance ().m_EquipmentqualityTable.getTableData ( temp.getNextId () );

        // Left
        m_LeftLevel.text = equipdata.IntensifyLev.ToString ();
        // TODO...
        m_LeftBg.sprite = UIResourceMgr.LoadSprite ( common.defaultPath + GameUtils.GetEquipBgColor ( tableid ) );
        m_LeftIcon.sprite = UIResourceMgr.LoadSprite ( common.defaultPath + temp.getIcon () );

        m_Name.text = temp.getName ();
        m_Name.color = GameUtils.GetEquipNameColor ( tableid );

        // 星级
        for ( int i = 0; i < 5; ++i )
        {
            m_Star.transform.GetChild ( i ).gameObject.SetActive ( i < temp.getQualityLevel () );
        }

        // Right
        m_RightName.text = nextTemp.getName ();
        m_RightName.color = GameUtils.GetEquipNameColor ( nextTemp.GetID () );

        m_RightLevel.text = equipdata.IntensifyLev.ToString ();

        m_RightIcon.sprite = UIResourceMgr.LoadSprite ( common.defaultPath + nextTemp.getIcon () );
        m_RightBg.sprite = UIResourceMgr.LoadSprite ( common.defaultPath + GameUtils.GetEquipBgColor ( nextTemp.GetID () ) );

        // 刷新数据
        for ( int i = 0; i < m_AttrList.transform.childCount; ++i )
        {
            GameObject obj = m_AttrList.transform.GetChild ( i ).gameObject;
            if ( i < temp.getQualityAttribute ().Length )
            {
                obj.SetActive ( true );

                obj.transform.Find ( "Text_Lv" ).GetComponent<Text> ().text = GameUtils.GetAttriName ( temp.getQualityAttribute () [ i ] );
                //EquipmentstrengthTemplate equipTemp = DataTemplate.GetInstance ().GetEquipStrengthTemplate ( HeroStrengthen.Inst.m_CurCard.GetHeroRow().getQosition (), //temp.getParts (), equipdata.IntensifyLev );
                EquipmentstrengthTemplate equipTemp = DataTemplate.GetInstance ().GetEquipStrengthTemplate ( HeroStrengthen.Inst.m_CurCard.GetHeroRow ().getQosition (), temp.getParts (), 1 );

                if ( equipTemp != null )
                {
                    obj.transform.Find ( "Text_Gradelimit01" ).GetComponent<Text> ().text = "+" + temp.getNumerical () [ i ].ToString ();
                    Debug.Log ( "temp.getNumbers () [ i ]" + temp.getNumbers () [ i ].ToString() );

                    if ( temp.getNextId () == -1 )
                    {
                        obj.transform.Find ( "Text_Gradelimit02" ).gameObject.SetActive ( false );
                    }
                    else
                    {
                        obj.transform.Find ( "Text_Gradelimit02" ).GetComponent<Text> ().text = "+" + ( nextTemp.getNumerical () [ i ].ToString () );
                    }
                }
            }
            else
            {
                obj.SetActive ( false );
            }
        }
    }

    void Update ()
    {
        if ( Input.GetMouseButtonUp ( 0 ) )
        {
            UI_HomeControler.Inst.ReMoveUI ( gameObject );
        }


        m_CurTime += Time.deltaTime;

        if ( m_CurTime > m_AllTime )
        {
            UI_HomeControler.Inst.ReMoveUI ( gameObject );
        }
    }
}
