using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class EquipLetGoodBase : HeroAttrPanel
{
    protected Text m_LeftName;
    protected Text m_LeftLevel;
    protected Text m_RightLevel;
    protected Text m_Name;
    protected Button m_BtnStrengthen;
    protected Text m_UpOneText;
    protected Text m_Number;
    protected Text m_Condition;

    public override void InitUIData ()
    {
        base.InitUIData ();
        m_LeftName = selfTransform.FindChild ( "Right/LeftName" ).GetComponent<Text> ();
        m_LeftLevel = selfTransform.FindChild ( "Right/Level/LeftLevel" ).GetComponent<Text> ();
        m_RightLevel = selfTransform.FindChild ( "Equipment/Level/RightLevel" ).GetComponent<Text> ();
        m_Name = selfTransform.FindChild ( "Equipment/Name" ).GetComponent<Text> ();
        m_BtnStrengthen = selfTransform.FindChild ( "BtnStrengthen" ).GetComponent<Button> ();
        m_BtnStrengthen.onClick.AddListener ( OnClickBtnStrengthen );
        m_UpOneText = selfTransform.FindChild ( "BtnStrengthen/UpOneText" ).GetComponent<Text> ();
        m_Number = selfTransform.FindChild ( "BtnStrengthen/Number" ).GetComponent<Text> ();
        m_Condition = selfTransform.FindChild ( "Condition" ).GetComponent<Text> ();
    }

    public override void InitUIView ()
    {
        base.InitUIView ();
    }

    protected virtual void OnClickBtnStrengthen ()
    {
    }

    public virtual void OnDestroy ()
    {
        Destroy ( m_LeftName );
        Destroy ( m_LeftLevel );
        Destroy ( m_RightLevel );
        Destroy ( m_Name );
        Destroy ( m_BtnStrengthen );
        Destroy ( m_UpOneText );
        Destroy ( m_Number );
        Destroy ( m_Condition );
    }
}
