using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UICommon_MsgBoxBase : BaseUI
{
	protected Text m_Text;
	protected Button m_CloseBtn;
	protected RichText m_DetailTxt;
    protected RichText m_HintTxt;
	protected Button m_YesBtn;
	protected Text m_yesTxt;

	public override void InitUIData()
	{
		base.InitUIData();
		m_Text = selfTransform.FindChild("Panel/TitleObj/Text").GetComponent<Text>();
		m_CloseBtn = selfTransform.FindChild("Panel/CloseBtn").GetComponent<Button>();
		m_CloseBtn.onClick.AddListener(OnClickCloseBtn);
        m_DetailTxt = selfTransform.FindChild("Panel/DetailTxt").GetComponent<RichText>();
        m_HintTxt = selfTransform.FindChild("Panel/HintTxt").GetComponent<RichText>();
		m_YesBtn = selfTransform.FindChild("Panel/YesBtn").GetComponent<Button>();
		m_YesBtn.onClick.AddListener(OnClickYesBtn);
		m_yesTxt = selfTransform.FindChild("Panel/YesBtn/yesTxt").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickCloseBtn()
	{
	}

	protected virtual void OnClickYesBtn()
	{
	}

    public void OnDestroy()
    {
        Destroy(m_Text);
        Destroy(m_CloseBtn);
        Destroy(m_DetailTxt);
        Destroy(m_HintTxt);
        Destroy(m_YesBtn);
        Destroy(m_yesTxt);
    }
}
