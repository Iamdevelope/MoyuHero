using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_IntitleBase : BaseUI
{
	protected Text m_TipsText;
	protected Text m_Placeholder;
	protected Text m_Text;
	protected Button m_RandBtn;
	protected Button m_CloseBtn;
	protected Text m_CloseText;

	public override void InitUIData()
	{
		base.InitUIData();
		m_TipsText = selfTransform.FindChild("TipsText").GetComponent<Text>();
		m_Placeholder = selfTransform.FindChild("Center/InputField/Placeholder").GetComponent<Text>();
		m_Text = selfTransform.FindChild("Center/InputField/Text").GetComponent<Text>();
		m_RandBtn = selfTransform.FindChild("Center/RandBtn").GetComponent<Button>();
		m_RandBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickRandBtn));
		m_CloseBtn = selfTransform.FindChild("CloseBtn").GetComponent<Button>();
		m_CloseBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickCloseBtn));
		m_CloseText = selfTransform.FindChild("CloseBtn/CloseText").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickRandBtn()
	{
	}

	protected virtual void OnClickCloseBtn()
	{
	}

}
