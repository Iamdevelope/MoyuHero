using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UICommon_GoldSoulBase : BaseUI
{
	protected Button m_CloseBtn;
	protected Text m_Name;
	protected Text m_typeTitle;
	protected Text m_typeName;
	protected Text m_TitleTxt;
	protected Text m_DetailTxt;
	protected Text m_TitleTxt_1;
	protected Text m_DetailTxt_1;
	protected Text m_HintTxt;

	public override void InitUIData()
	{
		base.InitUIData();
		m_CloseBtn = selfTransform.FindChild("Panel/CloseBtn").GetComponent<Button>();
		m_CloseBtn.onClick.AddListener(OnClickCloseBtn);
		m_Name = selfTransform.FindChild("Panel/ItemInfo/Name").GetComponent<Text>();
		m_typeTitle = selfTransform.FindChild("Panel/ItemInfo/Type/typeTitle").GetComponent<Text>();
		m_typeName = selfTransform.FindChild("Panel/ItemInfo/Type/typeName").GetComponent<Text>();
		m_TitleTxt = selfTransform.FindChild("Panel/EffObj1/TitleTxt").GetComponent<Text>();
		m_DetailTxt = selfTransform.FindChild("Panel/EffObj1/DetailTxt").GetComponent<Text>();
		m_TitleTxt_1 = selfTransform.FindChild("Panel/EffObj2/TitleTxt").GetComponent<Text>();
		m_DetailTxt_1 = selfTransform.FindChild("Panel/EffObj2/DetailTxt").GetComponent<Text>();
		m_HintTxt = selfTransform.FindChild("Panel/HintTxt").GetComponent<Text>();

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
        Destroy(m_typeTitle);
        Destroy(m_typeName);
        Destroy(m_TitleTxt);
        Destroy(m_DetailTxt);
        Destroy(m_TitleTxt_1);
        Destroy(m_DetailTxt_1);
        Destroy(m_HintTxt);
    }
}
