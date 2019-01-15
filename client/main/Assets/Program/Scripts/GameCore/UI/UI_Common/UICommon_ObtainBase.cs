using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UICommon_ObtainBase : BaseUI
{
	protected Button m_CloseBtn;
	protected Text m_Name;
	protected Text m_Title;
	protected Text m_Value;
	protected Text m_Text;
	protected Text m_Text_1;
	protected Text m_detail;
	protected Button m_Button;
	protected Text m_Text_2;
	protected Text m_HintTxt;

	public override void InitUIData()
	{
		base.InitUIData();
		m_CloseBtn = selfTransform.FindChild("Panel/CloseBtn").GetComponent<Button>();
		m_CloseBtn.onClick.AddListener(OnClickCloseBtn);
		m_Name = selfTransform.FindChild("Panel/ItemInfo/Name").GetComponent<Text>();
		m_Title = selfTransform.FindChild("Panel/ItemInfo/Count/Title").GetComponent<Text>();
		m_Value = selfTransform.FindChild("Panel/ItemInfo/Count/Value").GetComponent<Text>();
		m_Text = selfTransform.FindChild("Panel/Detail/Text").GetComponent<Text>();
		m_Text_1 = selfTransform.FindChild("Panel/GetObj/TitleObj/Text").GetComponent<Text>();
		m_detail = selfTransform.FindChild("Items/Item/DropObj/detail").GetComponent<Text>();
		m_Button = selfTransform.FindChild("Items/Item/Button").GetComponent<Button>();
		m_Button.onClick.AddListener(OnClickButton);
		m_Text_2 = selfTransform.FindChild("Items/Item/Button/Text").GetComponent<Text>();
		m_HintTxt = selfTransform.FindChild("Items/Item/HintTxt").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickCloseBtn()
	{
	}

	protected virtual void OnClickButton()
	{
	}

    public void OnDestroy()
    {
        Destroy(m_CloseBtn);
        Destroy(m_Name);
        Destroy(m_Title);
        Destroy(m_Value);
        Destroy(m_Text);
        Destroy(m_Text_1);
        Destroy(m_detail);
        Destroy(m_Button);
        Destroy(m_Text_2);
        Destroy(m_HintTxt);
    }
}
