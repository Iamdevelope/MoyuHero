using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UICommon_RewardBoxBase : BaseUI
{
	protected Button m_CloseBtn;
	protected Text m_Text;
	protected Text m_DetailTxt;
	protected Button m_iconImg;

	public override void InitUIData()
	{
		base.InitUIData();
		m_CloseBtn = selfTransform.FindChild("Panel/CloseBtn").GetComponent<Button>();
		m_CloseBtn.onClick.AddListener(OnClickCloseBtn);
		m_Text = selfTransform.FindChild("Panel/TitleObj/Text").GetComponent<Text>();
		m_DetailTxt = selfTransform.FindChild("Panel/DetailTxt").GetComponent<Text>();
		m_iconImg = selfTransform.FindChild("Items/Item/iconImg").GetComponent<Button>();
		m_iconImg.onClick.AddListener(OnClickiconImg);

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickCloseBtn()
	{
	}

	protected virtual void OnClickiconImg()
	{
	}

    public void OnDestroy()
    {
        Destroy(m_CloseBtn);
        Destroy(m_Text);
        Destroy(m_DetailTxt);
        Destroy(m_iconImg);
    }
}
