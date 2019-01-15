using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class EquipLetGoodPopBase : BaseUI
{
    protected Text m_Name;
    protected Text m_LeftLevel;
    protected Text m_RightLevel;
    protected Text m_RightName;
    protected Text m_Text_Gradelimit02;
    protected Text m_Text_Gradelimit01;
    protected Text m_Text_Lv;
    protected Text m_Text_Gradelimit02_1;
    protected Text m_Text_Gradelimit01_1;
    protected Text m_Text_Lv_1;
    protected Text m_Text_Gradelimit02_2;
    protected Text m_Text_Gradelimit01_2;
    protected Text m_Text_Lv_2;
    protected Text m_Text_Gradelimit02_3;
    protected Text m_Text_Gradelimit01_3;
    protected Text m_Text_Lv_3;

    public override void InitUIData ()
    {
        base.InitUIData ();
        m_Name = selfTransform.FindChild ( "Panel/EquipmentIcon/Name" ).GetComponent<Text> ();
        m_LeftLevel = selfTransform.FindChild ( "Panel/EquipmentIcon/LeftLevel" ).GetComponent<Text> ();
        m_RightLevel = selfTransform.FindChild ( "Panel/Equipment/Level/RightLevel" ).GetComponent<Text> ();
        m_RightName = selfTransform.FindChild ( "Panel/Equipment/RightName" ).GetComponent<Text> ();
        m_Text_Gradelimit02 = selfTransform.FindChild ( "Panel/AttrList/AttrItem/Text_Gradelimit02" ).GetComponent<Text> ();
        m_Text_Gradelimit01 = selfTransform.FindChild ( "Panel/AttrList/AttrItem/Text_Gradelimit01" ).GetComponent<Text> ();
        m_Text_Lv = selfTransform.FindChild ( "Panel/AttrList/AttrItem/Text_Lv" ).GetComponent<Text> ();
        m_Text_Gradelimit02_1 = selfTransform.FindChild ( "Panel/AttrList/AttrItem/Text_Gradelimit02" ).GetComponent<Text> ();
        m_Text_Gradelimit01_1 = selfTransform.FindChild ( "Panel/AttrList/AttrItem/Text_Gradelimit01" ).GetComponent<Text> ();
        m_Text_Lv_1 = selfTransform.FindChild ( "Panel/AttrList/AttrItem/Text_Lv" ).GetComponent<Text> ();
        m_Text_Gradelimit02_2 = selfTransform.FindChild ( "Panel/AttrList/AttrItem/Text_Gradelimit02" ).GetComponent<Text> ();
        m_Text_Gradelimit01_2 = selfTransform.FindChild ( "Panel/AttrList/AttrItem/Text_Gradelimit01" ).GetComponent<Text> ();
        m_Text_Lv_2 = selfTransform.FindChild ( "Panel/AttrList/AttrItem/Text_Lv" ).GetComponent<Text> ();
        m_Text_Gradelimit02_3 = selfTransform.FindChild ( "Panel/AttrList/AttrItem/Text_Gradelimit02" ).GetComponent<Text> ();
        m_Text_Gradelimit01_3 = selfTransform.FindChild ( "Panel/AttrList/AttrItem/Text_Gradelimit01" ).GetComponent<Text> ();
        m_Text_Lv_3 = selfTransform.FindChild ( "Panel/AttrList/AttrItem/Text_Lv" ).GetComponent<Text> ();

    }

    public override void InitUIView ()
    {
        base.InitUIView ();
    }

    public virtual void OnDestroy ()
    {
        Destroy ( m_Name );
        Destroy ( m_LeftLevel );
        Destroy ( m_RightLevel );
        Destroy ( m_RightName );
        Destroy ( m_Text_Gradelimit02 );
        Destroy ( m_Text_Gradelimit01 );
        Destroy ( m_Text_Lv );
        Destroy ( m_Text_Gradelimit02_1 );
        Destroy ( m_Text_Gradelimit01_1 );
        Destroy ( m_Text_Lv_1 );
        Destroy ( m_Text_Gradelimit02_2 );
        Destroy ( m_Text_Gradelimit01_2 );
        Destroy ( m_Text_Lv_2 );
        Destroy ( m_Text_Gradelimit02_3 );
        Destroy ( m_Text_Gradelimit01_3 );
        Destroy ( m_Text_Lv_3 );
    }
}
