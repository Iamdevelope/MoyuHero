using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class ItemStrengthenBase : BaseUI
{
	protected Text m_Text_Number4;
	protected Text m_Text_Number3;
	protected Text m_Text_Number2;
	protected Text m_Text_Number1;
	protected Button m_Img_Facelift;
	protected Text m_Text_Qualifications;
	protected Text m_Text_Title;
	protected Text m_Text_Name;
	protected Text m_Text_LV100;
	protected Button m_Btn_Advanced;
	protected Button m_Btn_Lgoods;
	protected Button m_Btn_Upgrade;
	protected Button m_Btn_Skill;
	protected Button m_Btn_Arcane;
	protected Button m_Btn_Culture;

	public override void InitUIData()
	{
		base.InitUIData();
		m_Text_Number4 = selfTransform.FindChild("Information/Text_Number4").GetComponent<Text>();
		m_Text_Number3 = selfTransform.FindChild("Information/Text_Number3").GetComponent<Text>();
		m_Text_Number2 = selfTransform.FindChild("Information/Text_Number2").GetComponent<Text>();
		m_Text_Number1 = selfTransform.FindChild("Information/Text_Number1").GetComponent<Text>();
		m_Img_Facelift = selfTransform.FindChild("Quality/Img_Facelift").GetComponent<Button>();
		m_Img_Facelift.onClick.AddListener(OnClickImg_Facelift);
		m_Text_Qualifications = selfTransform.FindChild("Quality/Text_Qualifications").GetComponent<Text>();
		m_Text_Title = selfTransform.FindChild("Left/Text_Title").GetComponent<Text>();
		m_Text_Name = selfTransform.FindChild("Left/Text_Name").GetComponent<Text>();
		m_Text_LV100 = selfTransform.FindChild("Left/Text_LV100").GetComponent<Text>();
		m_Btn_Advanced = selfTransform.FindChild("Mainbutton/Btn_Advanced").GetComponent<Button>();
		m_Btn_Advanced.onClick.AddListener(OnClickBtn_Advanced);
		m_Btn_Lgoods = selfTransform.FindChild("Mainbutton/Btn_Lgoods").GetComponent<Button>();
		m_Btn_Lgoods.onClick.AddListener(OnClickBtn_Lgoods);
		m_Btn_Upgrade = selfTransform.FindChild("Mainbutton/Btn_Upgrade").GetComponent<Button>();
		m_Btn_Upgrade.onClick.AddListener(OnClickBtn_Upgrade);
		m_Btn_Skill = selfTransform.FindChild("Mainbutton/Btn_Skill").GetComponent<Button>();
		m_Btn_Skill.onClick.AddListener(OnClickBtn_Skill);
		m_Btn_Arcane = selfTransform.FindChild("Mainbutton/Btn_Arcane").GetComponent<Button>();
		m_Btn_Arcane.onClick.AddListener(OnClickBtn_Arcane);
		m_Btn_Culture = selfTransform.FindChild("Mainbutton/Btn_Culture").GetComponent<Button>();
		m_Btn_Culture.onClick.AddListener(OnClickBtn_Culture);

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickImg_Facelift()
	{
	}

	protected virtual void OnClickBtn_Advanced()
	{
	}

	protected virtual void OnClickBtn_Lgoods()
	{
	}

	protected virtual void OnClickBtn_Upgrade()
	{
	}

	protected virtual void OnClickBtn_Skill()
	{
	}

	protected virtual void OnClickBtn_Arcane()
	{
	}

	protected virtual void OnClickBtn_Culture()
	{
	}

    public void OnDestroy ()
    {
        Destroy(m_Text_Number4);
        Destroy(m_Text_Number3);
        Destroy(m_Text_Number2);
        Destroy(m_Text_Number1);
        Destroy(m_Img_Facelift);
        Destroy(m_Text_Qualifications);
        Destroy(m_Text_Title);
        Destroy(m_Text_Name);
        Destroy(m_Text_LV100);
        Destroy(m_Btn_Advanced);
        Destroy(m_Btn_Lgoods);
        Destroy(m_Btn_Upgrade);
        Destroy(m_Btn_Skill);
        Destroy(m_Btn_Arcane);
        Destroy(m_Btn_Culture);
    }
}
