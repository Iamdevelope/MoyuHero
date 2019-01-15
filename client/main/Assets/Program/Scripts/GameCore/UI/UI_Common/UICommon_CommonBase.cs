using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UICommon_CommonBase : BaseUI
{
	protected Button m_CloseBtn;
	protected Text m_Name;
	protected Text m_HintTxt;
	protected Text m_Text;

	public override void InitUIData()
	{
		base.InitUIData();
		m_CloseBtn = selfTransform.FindChild("Panel/CloseBtn").GetComponent<Button>();
		m_CloseBtn.onClick.AddListener(OnClickCloseBtn);
		m_Name = selfTransform.FindChild("Panel/ItemInfo/Name").GetComponent<Text>();
		m_HintTxt = selfTransform.FindChild("Panel/ItemInfo/HintTxt").GetComponent<Text>();
		m_Text = selfTransform.FindChild("Panel/Detail/Text").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickCloseBtn()
	{
	}

    public void OnDestroy()
    {
        Destroy(m_CloseBtn);
        Destroy(m_Name);
        Destroy(m_HintTxt);
        Destroy(m_Text);
    }
}
