using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_RecruitBase : BaseUI
{
	protected Button m_HeroRecruitBtn;
	protected Text m_Text;
	protected Button m_RelicBtn;
	protected Text m_Text_1;
	protected Button m_BackBtn;
	protected Text m_BackText;
	protected Text m_Text_2;
	protected Button m_Add;
	protected Text m_Text_3;
	protected Button m_Add_1;

	public override void InitUIData()
	{
		base.InitUIData();
		m_HeroRecruitBtn = selfTransform.FindChild("UI_Top/HeroRecruitBtn").GetComponent<Button>();
		m_HeroRecruitBtn.onClick.AddListener(OnClickHeroRecruitBtn);
		m_Text = selfTransform.FindChild("UI_Top/HeroRecruitBtn/Text").GetComponent<Text>();
		m_RelicBtn = selfTransform.FindChild("UI_Top/RelicBtn").GetComponent<Button>();
		m_RelicBtn.onClick.AddListener(OnClickRelicBtn);
		m_Text_1 = selfTransform.FindChild("UI_Top/RelicBtn/Text").GetComponent<Text>();
		m_BackBtn = selfTransform.FindChild("UI_Top/BackBtn").GetComponent<Button>();
		m_BackBtn.onClick.AddListener(OnClickBackBtn);
		m_BackText = selfTransform.FindChild("UI_Top/BackBtn/BackText").GetComponent<Text>();
		m_Text_2 = selfTransform.FindChild("UI_Top/MoneyBarUI/Diamond/Text").GetComponent<Text>();
		m_Add = selfTransform.FindChild("UI_Top/MoneyBarUI/Diamond/Add").GetComponent<Button>();
		m_Add.onClick.AddListener(OnClickAdd);
		m_Text_3 = selfTransform.FindChild("UI_Top/MoneyBarUI/Money/Text").GetComponent<Text>();
		m_Add_1 = selfTransform.FindChild("UI_Top/MoneyBarUI/Money/Add").GetComponent<Button>();
		m_Add_1.onClick.AddListener(OnClickAdd_1);

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickHeroRecruitBtn()
	{
	}

	protected virtual void OnClickRelicBtn()
	{
	}

	protected virtual void OnClickBackBtn()
	{
	}

	protected virtual void OnClickAdd()
	{
	}

	protected virtual void OnClickAdd_1()
	{
	}

    void OnDestroy()
    {
        Destroy(m_HeroRecruitBtn);
        Destroy(m_Text);
        Destroy(m_RelicBtn);
        Destroy(m_Text_1);
        Destroy(m_BackBtn);
        Destroy(m_BackText);
        Destroy(m_Text_2);
        Destroy(m_Add);
        Destroy(m_Text_3);
        Destroy(m_Add_1);
    }
}
