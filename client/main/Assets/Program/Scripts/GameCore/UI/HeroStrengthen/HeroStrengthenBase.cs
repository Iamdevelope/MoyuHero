using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class HeroStrengthenBase : BaseUI
{
    protected Button m_BackBtn;
    protected Button m_Advanced;
    protected Button m_LetGood;
    protected Button m_Upgrade;
    protected Button m_Skill;
    protected Button m_Arcane;
    protected Button m_Culture;
    protected Button m_EquipStrengthen;
    protected Button m_EquipLetGood;
    protected Text m_Text_6;
    protected Text m_Level;
    protected Text m_HeroName;
    protected Text m_TypeName;
    protected Text m_ActName;
    protected Button m_Facelift;
    protected Image m_ZhongZu;
    public override void InitUIData ()
    {
        base.InitUIData ();
        m_BackBtn = selfTransform.FindChild ( "TopPanel/TopTittle/BackBtn" ).GetComponent<Button> ();
        m_BackBtn.onClick.AddListener ( OnClickBackBtn );
        m_Advanced = selfTransform.FindChild ( "BtnGroups/Advanced" ).GetComponent<Button> ();
        m_Advanced.onClick.AddListener ( OnClickAdvanced );
        m_LetGood = selfTransform.FindChild ( "BtnGroups/LetGood" ).GetComponent<Button> ();
        m_LetGood.onClick.AddListener ( OnClickLetGood );
        m_Upgrade = selfTransform.FindChild ( "BtnGroups/Upgrade" ).GetComponent<Button> ();
        m_Upgrade.onClick.AddListener ( OnClickUpgrade );
        m_Skill = selfTransform.FindChild ( "BtnGroups/Skill" ).GetComponent<Button> ();
        m_Skill.onClick.AddListener ( OnClickSkill );
        m_Arcane = selfTransform.FindChild ( "BtnGroups/Arcane" ).GetComponent<Button> ();
        m_Arcane.onClick.AddListener ( OnClickArcane );
        m_Culture = selfTransform.FindChild ( "BtnGroups/Culture" ).GetComponent<Button> ();
        m_Culture.onClick.AddListener ( OnClickCulture );
        m_EquipStrengthen = selfTransform.FindChild ( "EquipGroups/EquipStrengthen" ).GetComponent<Button> ();
        m_EquipStrengthen.onClick.AddListener ( OnClickEquipStrengthen );
        m_EquipLetGood = selfTransform.FindChild ( "EquipGroups/EquipLetGood" ).GetComponent<Button> ();
        m_EquipLetGood.onClick.AddListener ( OnClickEquipLetGood );
        m_Text_6 = selfTransform.FindChild ( "HeroInfo/Left/Text" ).GetComponent<Text> ();
        m_Level = selfTransform.FindChild ( "HeroInfo/Left/Level" ).GetComponent<Text> ();
        m_ZhongZu = selfTransform.FindChild("HeroInfo/Left/ZhongZu").GetComponent<Image>();
        m_HeroName = selfTransform.FindChild ( "HeroInfo/Left/HeroName" ).GetComponent<Text> ();
        m_ActName = selfTransform.FindChild( "HeroInfo/Left/ActName" ).GetComponent<Text>();
        m_TypeName = selfTransform.FindChild ( "HeroInfo/Left/TypeName" ).GetComponent<Text> ();
        m_Facelift = selfTransform.FindChild ( "HeroInfo/Right/Facelift" ).GetComponent<Button> ();
        m_Facelift.onClick.AddListener ( OnClickFacelift );
    }

    public override void InitUIView ()
    {
        base.InitUIView ();
    }

    protected virtual void OnClickBackBtn ()
    {
    }

    protected virtual void OnClickAdvanced ()
    {
    }

    protected virtual void OnClickLetGood ()
    {
    }

    protected virtual void OnClickUpgrade ()
    {
    }

    protected virtual void OnClickSkill ()
    {
    }

    protected virtual void OnClickArcane ()
    {
    }

    protected virtual void OnClickCulture ()
    {
    }

    protected virtual void OnClickEquipStrengthen ()
    {
    }

    protected virtual void OnClickEquipLetGood ()
    {
    }


    protected virtual void OnClickFacelift ()
    {
    }

    public void OnDestroy ()
    {
        Destroy ( m_BackBtn );
        Destroy ( m_Advanced );
        Destroy ( m_LetGood );
        Destroy ( m_Upgrade );
        Destroy ( m_Skill );
        Destroy ( m_Arcane );
        Destroy ( m_Culture ); 
        Destroy ( m_Text_6 );
        Destroy ( m_Level );
        Destroy ( m_HeroName );
        Destroy(m_TypeName);
        Destroy(m_ActName);
        Destroy ( m_Facelift );
    }
}
