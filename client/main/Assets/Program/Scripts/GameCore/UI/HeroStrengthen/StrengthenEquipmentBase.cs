using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class StrengthenEquipmentBase : HeroAttrPanel
{
	protected Button m_Equipment;
	protected Text m_Level;
	protected Text m_Name;
	protected Button m_BtnOne;
	protected Text m_TextOne;
	protected Button m_BtnStrengthen;
	protected Text m_Text_strengthen;
	protected Text m_Number;
	protected Text m_Condition;
	protected Text m_AttrName;
	protected Text m_AttrNumber;
	protected Text m_AttrAdd;
	protected Text m_AttrName_1;
	protected Text m_AttrNumber_1;
	protected Text m_AttrAdd_1;
	protected Text m_AttrName_2;
	protected Text m_AttrNumber_2;
	protected Text m_AttrAdd_2;
	protected Text m_AttrName_3;
	protected Text m_AttrNumber_3;
	protected Text m_AttrAdd_3;

	public override void InitUIData()
	{
		base.InitUIData();
		m_Equipment = selfTransform.FindChild("Equipment").GetComponent<Button>();
		m_Equipment.onClick.AddListener(OnClickEquipment);
		m_Level = selfTransform.FindChild("Equipment/Level/Level").GetComponent<Text>();
		m_Name = selfTransform.FindChild("Equipment/Name").GetComponent<Text>();
		m_BtnOne = selfTransform.FindChild("BtnOne").GetComponent<Button>();
		m_BtnOne.onClick.AddListener(OnClickBtnOne);
		m_TextOne = selfTransform.FindChild("BtnOne/TextOne").GetComponent<Text>();
		m_BtnStrengthen = selfTransform.FindChild("BtnStrengthen").GetComponent<Button>();
		m_BtnStrengthen.onClick.AddListener(OnClickBtnStrengthen);
		m_Text_strengthen = selfTransform.FindChild("BtnStrengthen/Text_strengthen").GetComponent<Text>();
		m_Number = selfTransform.FindChild("BtnStrengthen/Number").GetComponent<Text>();
		m_Condition = selfTransform.FindChild("Condition").GetComponent<Text>();
		m_AttrName = selfTransform.FindChild("Information/AttrPanel/AttrName").GetComponent<Text>();
		m_AttrNumber = selfTransform.FindChild("Information/AttrPanel/AttrNumber").GetComponent<Text>();
		m_AttrAdd = selfTransform.FindChild("Information/AttrPanel/AttrAdd").GetComponent<Text>();
		m_AttrName_1 = selfTransform.FindChild("Information/AttrPanel/AttrName").GetComponent<Text>();
		m_AttrNumber_1 = selfTransform.FindChild("Information/AttrPanel/AttrNumber").GetComponent<Text>();
		m_AttrAdd_1 = selfTransform.FindChild("Information/AttrPanel/AttrAdd").GetComponent<Text>();
		m_AttrName_2 = selfTransform.FindChild("Information/AttrPanel/AttrName").GetComponent<Text>();
		m_AttrNumber_2 = selfTransform.FindChild("Information/AttrPanel/AttrNumber").GetComponent<Text>();
		m_AttrAdd_2 = selfTransform.FindChild("Information/AttrPanel/AttrAdd").GetComponent<Text>();
		m_AttrName_3 = selfTransform.FindChild("Information/AttrPanel/AttrName").GetComponent<Text>();
		m_AttrNumber_3 = selfTransform.FindChild("Information/AttrPanel/AttrNumber").GetComponent<Text>();
		m_AttrAdd_3 = selfTransform.FindChild("Information/AttrPanel/AttrAdd").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickEquipment()
	{
	}

	protected virtual void OnClickBtnOne()
	{
	}

	protected virtual void OnClickBtnStrengthen()
	{
	}

    public void OnDestroy()
    {
        Destroy(m_Equipment);
        Destroy(m_Level);
        Destroy(m_Name);
        Destroy(m_BtnOne);
        Destroy(m_TextOne);
        Destroy(m_BtnStrengthen);
        Destroy(m_Text_strengthen);
        Destroy(m_Number);
        Destroy(m_Condition);
        Destroy(m_AttrName);
        Destroy(m_AttrNumber);
        Destroy(m_AttrAdd);
        Destroy(m_AttrName_1);
        Destroy(m_AttrNumber_1);
        Destroy(m_AttrAdd_1);
        Destroy(m_AttrName_2);
        Destroy(m_AttrNumber_2);
        Destroy(m_AttrAdd_2);
        Destroy(m_AttrName_3);
        Destroy(m_AttrNumber_3);
        Destroy(m_AttrAdd_3);
    }
}
