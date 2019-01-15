using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_SelectLevelBase : BaseUI
{
	protected Button m_normal;
	protected Button m_hard;
	protected Text m_Label;
	protected Button m_hardest;
	protected Button m_frontpoint;
	protected Button m_backpoint;
	protected Text m_title;
	protected Text m_value;
	protected Button m_BackBtn;
	protected Text m_Text;
	protected Button m_Add;
	protected Text m_Text_1;
	protected Button m_Add_1;
	protected Button m_AddBtn;
	protected Text m_CurrentpowTxt;
	protected Text m_MaxpowTxt;
	protected Button m_Box;
	protected Text m_curTxt;
	protected Text m_totalTxt;
	protected Button m_Box1;
	protected Text m_curTxt_1;
	protected Button m_Box2;
	protected Text m_curTxt_2;
	protected Button m_Box3;
	protected Text m_curTxt_3;

	public override void InitUIData()
	{
		base.InitUIData();
		m_normal = selfTransform.FindChild("UI_Menu/Menu/normal").GetComponent<Button>();
		m_normal.onClick.AddListener(OnClicknormal);
		m_hard = selfTransform.FindChild("UI_Menu/Menu/hard").GetComponent<Button>();
		m_hard.onClick.AddListener(OnClickhard);
		m_Label = selfTransform.FindChild("UI_Menu/Menu/hard/no/Label").GetComponent<Text>();
		m_hardest = selfTransform.FindChild("UI_Menu/Menu/hardest").GetComponent<Button>();
		m_hardest.onClick.AddListener(OnClickhardest);
		m_frontpoint = selfTransform.FindChild("UI_Menu/frontpoint").GetComponent<Button>();
		m_frontpoint.onClick.AddListener(OnClickfrontpoint);
		m_backpoint = selfTransform.FindChild("UI_Menu/backpoint").GetComponent<Button>();
		m_backpoint.onClick.AddListener(OnClickbackpoint);
		m_title = selfTransform.FindChild("chaptername/title").GetComponent<Text>();
		m_value = selfTransform.FindChild("chaptername/value").GetComponent<Text>();
		m_BackBtn = selfTransform.FindChild("TopPanel/TopTittle/BackBtn").GetComponent<Button>();
		m_BackBtn.onClick.AddListener(OnClickBackBtn);
		m_Text = selfTransform.FindChild("TopPanel/MoneyBarUI/Diamond/Text").GetComponent<Text>();
		m_Add = selfTransform.FindChild("TopPanel/MoneyBarUI/Diamond/Add").GetComponent<Button>();
		m_Add.onClick.AddListener(OnClickAdd);
		m_Text_1 = selfTransform.FindChild("TopPanel/MoneyBarUI/Money/Text").GetComponent<Text>();
		m_Add_1 = selfTransform.FindChild("TopPanel/MoneyBarUI/Money/Add").GetComponent<Button>();
		m_Add_1.onClick.AddListener(OnClickAdd_1);
		m_AddBtn = selfTransform.FindChild("TopPanel/MoneyBarUI/Powers/AddBtn").GetComponent<Button>();
		m_AddBtn.onClick.AddListener(OnClickAddBtn);
		m_CurrentpowTxt = selfTransform.FindChild("TopPanel/MoneyBarUI/Powers/CurrentpowTxt").GetComponent<Text>();
		m_MaxpowTxt = selfTransform.FindChild("TopPanel/MoneyBarUI/Powers/MaxpowTxt").GetComponent<Text>();
		m_Box = selfTransform.FindChild("BottomBar/Box").GetComponent<Button>();
		m_Box.onClick.AddListener(OnClickBox);
		m_curTxt = selfTransform.FindChild("BottomBar/Box/curTxt").GetComponent<Text>();
		m_totalTxt = selfTransform.FindChild("BottomBar/Box/totalTxt").GetComponent<Text>();
		m_Box1 = selfTransform.FindChild("BottomBar/AllBox/Box1").GetComponent<Button>();
		m_Box1.onClick.AddListener(OnClickBox1);
		m_curTxt_1 = selfTransform.FindChild("BottomBar/AllBox/Box1/panel/curTxt").GetComponent<Text>();
		m_Box2 = selfTransform.FindChild("BottomBar/AllBox/Box2").GetComponent<Button>();
		m_Box2.onClick.AddListener(OnClickBox2);
		m_curTxt_2 = selfTransform.FindChild("BottomBar/AllBox/Box2/panel/curTxt").GetComponent<Text>();
		m_Box3 = selfTransform.FindChild("BottomBar/AllBox/Box3").GetComponent<Button>();
		m_Box3.onClick.AddListener(OnClickBox3);
		m_curTxt_3 = selfTransform.FindChild("BottomBar/AllBox/Box3/panel/curTxt").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClicknormal()
	{
	}

	protected virtual void OnClickhard()
	{
	}

	protected virtual void OnClickhardest()
	{
	}

	protected virtual void OnClickfrontpoint()
	{
	}

	protected virtual void OnClickbackpoint()
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

	protected virtual void OnClickAddBtn()
	{
	}

	protected virtual void OnClickBox()
	{
	}

	protected virtual void OnClickBox1()
	{
	}

	protected virtual void OnClickBox2()
	{
	}

	protected virtual void OnClickBox3()
	{
	}

    public virtual void OnDestroy()
    {
        Destroy(m_normal);
        Destroy(m_hard);
        Destroy(m_Label);
        Destroy(m_hardest);
        Destroy(m_frontpoint);
        Destroy(m_backpoint);
        Destroy(m_title);
        Destroy(m_value);
        Destroy(m_BackBtn);
        Destroy(m_Text);
        Destroy(m_Add);
        Destroy(m_Text_1);
        Destroy(m_Add_1);
        Destroy(m_AddBtn);
        Destroy(m_CurrentpowTxt);
        Destroy(m_MaxpowTxt);
        Destroy(m_Box);
        Destroy(m_curTxt);
        Destroy(m_totalTxt);
        Destroy(m_Box1);
        Destroy(m_curTxt_1);
        Destroy(m_Box2);
        Destroy(m_curTxt_2);
        Destroy(m_Box3);
        Destroy(m_curTxt_3);
    }
}
