using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UICommon_HeroBase : BaseUI
{
	protected Button m_CloseBtn;
	protected Text m_Name;
	protected Text m_Alias;
	protected Text m_Title;
	protected Text m_Value;
	protected Text m_ExpTxt;
	protected Text m_ApitudeName;
	protected Text m_PropTitleTxt;
	protected Text m_AText0;
	protected Text m_AText1;
	protected Text m_AText2;
	protected Text m_AText3;
	protected Text m_AValue0;
	protected Text m_AValue1;
	protected Text m_AValue2;
	protected Text m_AValue3;
	protected Text m_FeatureTitleTxt;
	protected Text m_FeatureTxt;
	protected Text m_SkillTitleTxt;
	protected Text m_typeTxt;
	protected Text m_NameTxt;
	protected Text m_LvTxt;
	protected Text m_Text;
	protected Button m_Button0;
	protected Text m_typeTxt_1;
	protected Text m_NameTxt_1;
	protected Text m_LvTxt_1;
	protected Text m_Text_1;
	protected Button m_Button1;
	protected Text m_typeTxt_2;
	protected Text m_NameTxt_2;
	protected Text m_LvTxt_2;
	protected Text m_Text_2;
	protected Button m_Button2;
	protected Text m_typeTxt_3;
	protected Text m_NameTxt_3;
	protected Text m_LvTxt_3;
	protected Text m_Text_3;
	protected Button m_Button3;
	protected Text m_typeTxt_4;
	protected Text m_NameTxt_4;
	protected Text m_LvTxt_4;
	protected Text m_Text_4;
	protected Button m_Button4;
	protected Text m_typeTxt_5;
	protected Text m_NameTxt_5;
	protected Text m_LvTxt_5;
	protected Text m_Text_5;
	protected Button m_Button5;
	protected Text m_IntroTitleTxt;
	protected Text m_IntroductTxt;
	protected Text m_typeTxt_6;
	protected Text m_NameTxt_6;
	protected Text m_LvTxt_6;
	protected Text m_Text_6;

	public override void InitUIData()
	{
		base.InitUIData();
		m_CloseBtn = selfTransform.FindChild("Panel/CloseBtn").GetComponent<Button>();
		m_CloseBtn.onClick.AddListener(OnClickCloseBtn);
		m_Name = selfTransform.FindChild("Panel/LeftObj/HeroInfo/Name").GetComponent<Text>();
		m_Alias = selfTransform.FindChild("Panel/LeftObj/HeroInfo/Alias").GetComponent<Text>();
		m_Title = selfTransform.FindChild("Panel/LeftObj/HeroInfo/LevelObj/Title").GetComponent<Text>();
		m_Value = selfTransform.FindChild("Panel/LeftObj/HeroInfo/LevelObj/Value").GetComponent<Text>();
		m_ExpTxt = selfTransform.FindChild("Panel/LeftObj/HeroInfo/ExpTxt").GetComponent<Text>();
		m_ApitudeName = selfTransform.FindChild("Panel/LeftObj/HeroInfo/ApitudeObj/ApitudeName").GetComponent<Text>();
		m_PropTitleTxt = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/TitleObj1/PropTitleTxt").GetComponent<Text>();
		m_AText0 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj1/NameGrid1/AText0").GetComponent<Text>();
		m_AText1 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj1/NameGrid1/AText1").GetComponent<Text>();
		m_AText2 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj1/NameGrid1/AText2").GetComponent<Text>();
		m_AText3 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj1/NameGrid1/AText3").GetComponent<Text>();
		m_AValue0 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj1/ValueGrid1/AValue0").GetComponent<Text>();
		m_AValue1 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj1/ValueGrid1/AValue1").GetComponent<Text>();
		m_AValue2 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj1/ValueGrid1/AValue2").GetComponent<Text>();
		m_AValue3 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj1/ValueGrid1/AValue3").GetComponent<Text>();
		m_FeatureTitleTxt = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/TitleObj2/FeatureTitleTxt").GetComponent<Text>();
		m_FeatureTxt = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj2/FeatureTxt").GetComponent<Text>();
		m_SkillTitleTxt = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/TitleObj3/SkillTitleTxt").GetComponent<Text>();
		m_typeTxt = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item0/IconObj/typeTxt").GetComponent<Text>();
		m_NameTxt = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item0/NameTxt").GetComponent<Text>();
		m_LvTxt = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item0/LvTxt").GetComponent<Text>();
		m_Text = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item0/Text").GetComponent<Text>();
		m_Button0 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item0/Button0").GetComponent<Button>();
		m_Button0.onClick.AddListener(OnClickButton0);
		m_typeTxt_1 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item1/IconObj/typeTxt").GetComponent<Text>();
		m_NameTxt_1 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item1/NameTxt").GetComponent<Text>();
		m_LvTxt_1 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item1/LvTxt").GetComponent<Text>();
		m_Text_1 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item1/Text").GetComponent<Text>();
		m_Button1 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item1/Button1").GetComponent<Button>();
		m_Button1.onClick.AddListener(OnClickButton1);
		m_typeTxt_2 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item2/IconObj/typeTxt").GetComponent<Text>();
		m_NameTxt_2 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item2/NameTxt").GetComponent<Text>();
		m_LvTxt_2 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item2/LvTxt").GetComponent<Text>();
		m_Text_2 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item2/Text").GetComponent<Text>();
		m_Button2 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item2/Button2").GetComponent<Button>();
		m_Button2.onClick.AddListener(OnClickButton2);
		m_typeTxt_3 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item3/IconObj/typeTxt").GetComponent<Text>();
		m_NameTxt_3 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item3/NameTxt").GetComponent<Text>();
		m_LvTxt_3 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item3/LvTxt").GetComponent<Text>();
		m_Text_3 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item3/Text").GetComponent<Text>();
		m_Button3 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item3/Button3").GetComponent<Button>();
		m_Button3.onClick.AddListener(OnClickButton3);
		m_typeTxt_4 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item4/IconObj/typeTxt").GetComponent<Text>();
		m_NameTxt_4 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item4/NameTxt").GetComponent<Text>();
		m_LvTxt_4 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item4/LvTxt").GetComponent<Text>();
		m_Text_4 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item4/Text").GetComponent<Text>();
		m_Button4 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item4/Button4").GetComponent<Button>();
		m_Button4.onClick.AddListener(OnClickButton4);
		m_typeTxt_5 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item5/IconObj/typeTxt").GetComponent<Text>();
		m_NameTxt_5 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item5/NameTxt").GetComponent<Text>();
		m_LvTxt_5 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item5/LvTxt").GetComponent<Text>();
		m_Text_5 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item5/Text").GetComponent<Text>();
		m_Button5 = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj3/Grid/Item5/Button5").GetComponent<Button>();
		m_Button5.onClick.AddListener(OnClickButton5);
		m_IntroTitleTxt = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/TitleObj4/IntroTitleTxt").GetComponent<Text>();
		m_IntroductTxt = selfTransform.FindChild("Panel/RightObj/ItemList/Panel/AttriObj4/IntroductTxt").GetComponent<Text>();
		m_typeTxt_6 = selfTransform.FindChild("Items/Item/IconObj/typeTxt").GetComponent<Text>();
		m_NameTxt_6 = selfTransform.FindChild("Items/Item/NameTxt").GetComponent<Text>();
		m_LvTxt_6 = selfTransform.FindChild("Items/Item/LvTxt").GetComponent<Text>();
		m_Text_6 = selfTransform.FindChild("Items/Item/Text").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickCloseBtn()
	{
	}

	protected virtual void OnClickButton0()
	{
	}

	protected virtual void OnClickButton1()
	{
	}

	protected virtual void OnClickButton2()
	{
	}

	protected virtual void OnClickButton3()
	{
	}

	protected virtual void OnClickButton4()
	{
	}

	protected virtual void OnClickButton5()
	{
	}

    public virtual void OnDestroy()
    {
        Destroy(m_CloseBtn);
        Destroy(m_Name);
        Destroy(m_Alias);
        Destroy(m_Title);
        Destroy(m_Value);
        Destroy(m_ExpTxt);
        Destroy(m_ApitudeName);
        Destroy(m_PropTitleTxt);
        Destroy(m_AText0);
        Destroy(m_AText1);
        Destroy(m_AText2);
        Destroy(m_AText3);
        Destroy(m_AValue0);
        Destroy(m_AValue1);
        Destroy(m_AValue2);
        Destroy(m_AValue3);
        Destroy(m_FeatureTitleTxt);
        Destroy(m_FeatureTxt);
        Destroy(m_SkillTitleTxt);
        Destroy(m_typeTxt);
        Destroy(m_NameTxt);
        Destroy(m_LvTxt);
        Destroy(m_Text);
        Destroy(m_Button0);
        Destroy(m_typeTxt_1);
        Destroy(m_NameTxt_1);
        Destroy(m_LvTxt_1);
        Destroy(m_Text_1);
        Destroy(m_Button1);
        Destroy(m_typeTxt_2);
        Destroy(m_NameTxt_2);
        Destroy(m_LvTxt_2);
        Destroy(m_Text_2);
        Destroy(m_Button2);
        Destroy(m_typeTxt_3);
        Destroy(m_NameTxt_3);
        Destroy(m_LvTxt_3);
        Destroy(m_Text_3);
        Destroy(m_Button3);
        Destroy(m_typeTxt_4);
        Destroy(m_NameTxt_4);
        Destroy(m_LvTxt_4);
        Destroy(m_Text_4);
        Destroy(m_Button4);
        Destroy(m_typeTxt_5);
        Destroy(m_NameTxt_5);
        Destroy(m_LvTxt_5);
        Destroy(m_Text_5);
        Destroy(m_Button5);
        Destroy(m_IntroTitleTxt);
        Destroy(m_IntroductTxt);
        Destroy(m_typeTxt_6);
        Destroy(m_NameTxt_6);
        Destroy(m_LvTxt_6);
        Destroy(m_Text_6);
    }
}
