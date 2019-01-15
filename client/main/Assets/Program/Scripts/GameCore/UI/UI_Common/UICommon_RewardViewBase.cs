using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UICommon_RewardViewBase : BaseUI
{
	protected Text m_TitleTxt;
	protected Button m_iconImg;
	protected Text m_count;

	public override void InitUIData()
	{
		base.InitUIData();
		m_TitleTxt = selfTransform.FindChild("Panel/TitleTxt").GetComponent<Text>();
		m_iconImg = selfTransform.FindChild("Panel/ListObj/Items/Item/iconImg").GetComponent<Button>();
		m_iconImg.onClick.AddListener(OnClickiconImg);
		m_count = selfTransform.FindChild("Panel/ListObj/Items/Item/count").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickiconImg()
	{
	}

    public virtual void OnDestroy()
    {
        Destroy(m_TitleTxt);
        Destroy(m_iconImg);
        Destroy(m_count);
    }
}
