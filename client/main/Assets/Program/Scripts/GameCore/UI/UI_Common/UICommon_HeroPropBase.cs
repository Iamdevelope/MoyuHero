using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UICommon_HeroPropBase : BaseUI
{
	protected Button m_CloseBtn;
	protected Text m_Text;
	protected Text m_AText1;
	protected Text m_AText2;
	protected Text m_AText3;
	protected Text m_AText4;
	protected Text m_AText5;
	protected Text m_AText6;
	protected Text m_AText7;
	protected Text m_AText8;
	protected Text m_AText9;
	protected Text m_AText10;
	protected Text m_AText11;
	protected Text m_AText12;
	protected Text m_AValue1;
	protected Text m_AValue2;
	protected Text m_AValue3;
	protected Text m_AValue4;
	protected Text m_AValue5;
	protected Text m_AValue6;
	protected Text m_AValue7;
	protected Text m_AValue8;
	protected Text m_AValue9;
	protected Text m_AValue10;
	protected Text m_AValue11;
	protected Text m_AValue12;
	protected Text m_Text_1;
	protected Text m_BText1;
	protected Text m_BText2;
	protected Text m_BText3;
	protected Text m_BText4;
	protected Text m_BText5;
	protected Text m_BText6;
	protected Text m_BText7;
	protected Text m_BText8;
	protected Text m_BText9;
	protected Text m_BText10;
	protected Text m_BText11;
	protected Text m_BText12;
	protected Text m_BValue1;
	protected Text m_BValue2;
	protected Text m_BValue3;
	protected Text m_BValue4;
	protected Text m_BValue5;
	protected Text m_BValue6;
	protected Text m_BValue7;
	protected Text m_BValue8;
	protected Text m_BValue9;
	protected Text m_BValue10;
	protected Text m_BValue11;
	protected Text m_BValue12;

	public override void InitUIData()
	{
		base.InitUIData();
		m_CloseBtn = selfTransform.FindChild("Panel/CloseBtn").GetComponent<Button>();
		m_CloseBtn.onClick.AddListener(OnClickCloseBtn);
		m_Text = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/TitleObj1/Text").GetComponent<Text>();
		m_AText1 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj1/NameGrid1/AText1").GetComponent<Text>();
		m_AText2 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj1/NameGrid1/AText2").GetComponent<Text>();
		m_AText3 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj1/NameGrid1/AText3").GetComponent<Text>();
		m_AText4 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj1/NameGrid2/AText4").GetComponent<Text>();
		m_AText5 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj1/NameGrid2/AText5").GetComponent<Text>();
		m_AText6 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj1/NameGrid2/AText6").GetComponent<Text>();
		m_AText7 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj1/NameGrid2/AText7").GetComponent<Text>();
		m_AText8 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj1/NameGrid2/AText8").GetComponent<Text>();
		m_AText9 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj1/NameGrid2/AText9").GetComponent<Text>();
		m_AText10 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj1/NameGrid3/AText10").GetComponent<Text>();
		m_AText11 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj1/NameGrid3/AText11").GetComponent<Text>();
		m_AText12 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj1/NameGrid3/AText12").GetComponent<Text>();
		m_AValue1 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj1/ValueGrid1/AValue1").GetComponent<Text>();
		m_AValue2 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj1/ValueGrid1/AValue2").GetComponent<Text>();
		m_AValue3 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj1/ValueGrid1/AValue3").GetComponent<Text>();
		m_AValue4 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj1/ValueGrid2/AValue4").GetComponent<Text>();
		m_AValue5 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj1/ValueGrid2/AValue5").GetComponent<Text>();
		m_AValue6 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj1/ValueGrid2/AValue6").GetComponent<Text>();
		m_AValue7 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj1/ValueGrid2/AValue7").GetComponent<Text>();
		m_AValue8 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj1/ValueGrid2/AValue8").GetComponent<Text>();
		m_AValue9 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj1/ValueGrid2/AValue9").GetComponent<Text>();
		m_AValue10 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj1/ValueGrid3/AValue10").GetComponent<Text>();
		m_AValue11 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj1/ValueGrid3/AValue11").GetComponent<Text>();
		m_AValue12 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj1/ValueGrid3/AValue12").GetComponent<Text>();
		m_Text_1 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/TitleObj2/Text").GetComponent<Text>();
		m_BText1 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj2/NameGrid1/BText1").GetComponent<Text>();
		m_BText2 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj2/NameGrid1/BText2").GetComponent<Text>();
		m_BText3 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj2/NameGrid1/BText3").GetComponent<Text>();
		m_BText4 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj2/NameGrid2/BText4").GetComponent<Text>();
		m_BText5 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj2/NameGrid2/BText5").GetComponent<Text>();
		m_BText6 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj2/NameGrid2/BText6").GetComponent<Text>();
		m_BText7 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj2/NameGrid2/BText7").GetComponent<Text>();
		m_BText8 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj2/NameGrid2/BText8").GetComponent<Text>();
		m_BText9 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj2/NameGrid2/BText9").GetComponent<Text>();
		m_BText10 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj2/NameGrid3/BText10").GetComponent<Text>();
		m_BText11 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj2/NameGrid3/BText11").GetComponent<Text>();
		m_BText12 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj2/NameGrid3/BText12").GetComponent<Text>();
		m_BValue1 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj2/ValueGrid1/BValue1").GetComponent<Text>();
		m_BValue2 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj2/ValueGrid1/BValue2").GetComponent<Text>();
		m_BValue3 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj2/ValueGrid1/BValue3").GetComponent<Text>();
		m_BValue4 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj2/ValueGrid2/BValue4").GetComponent<Text>();
		m_BValue5 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj2/ValueGrid2/BValue5").GetComponent<Text>();
		m_BValue6 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj2/ValueGrid2/BValue6").GetComponent<Text>();
		m_BValue7 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj2/ValueGrid2/BValue7").GetComponent<Text>();
		m_BValue8 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj2/ValueGrid2/BValue8").GetComponent<Text>();
		m_BValue9 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj2/ValueGrid2/BValue9").GetComponent<Text>();
		m_BValue10 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj2/ValueGrid3/BValue10").GetComponent<Text>();
		m_BValue11 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj2/ValueGrid3/BValue11").GetComponent<Text>();
		m_BValue12 = selfTransform.FindChild("Panel/UIGold/ItemList/Panel/AttriObj2/ValueGrid3/BValue12").GetComponent<Text>();

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
        Destroy(m_Text);
        Destroy(m_AText1);
        Destroy(m_AText2);
        Destroy(m_AText3);
        Destroy(m_AText4);
        Destroy(m_AText5);
        Destroy(m_AText6);
        Destroy(m_AText7);
        Destroy(m_AText8);
        Destroy(m_AText9);
        Destroy(m_AText10);
        Destroy(m_AText11);
        Destroy(m_AText12);
        Destroy(m_AValue1);
        Destroy(m_AValue2);
        Destroy(m_AValue3);
        Destroy(m_AValue4);
        Destroy(m_AValue5);
        Destroy(m_AValue6);
        Destroy(m_AValue7);
        Destroy(m_AValue8);
        Destroy(m_AValue9);
        Destroy(m_AValue10);
        Destroy(m_AValue11);
        Destroy(m_AValue12);
        Destroy(m_Text_1);
        Destroy(m_BText1);
        Destroy(m_BText2);
        Destroy(m_BText3);
        Destroy(m_BText4);
        Destroy(m_BText5);
        Destroy(m_BText6);
        Destroy(m_BText7);
        Destroy(m_BText8);
        Destroy(m_BText9);
        Destroy(m_BText10);
        Destroy(m_BText11);
        Destroy(m_BText12);
        Destroy(m_BValue1);
        Destroy(m_BValue2);
        Destroy(m_BValue3);
        Destroy(m_BValue4);
        Destroy(m_BValue5);
        Destroy(m_BValue6);
        Destroy(m_BValue7);
        Destroy(m_BValue8);
        Destroy(m_BValue9);
        Destroy(m_BValue10);
        Destroy(m_BValue11);
        Destroy(m_BValue12);
    }
}
