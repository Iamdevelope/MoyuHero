using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_LotteryBase : BaseUI
{
	protected Button m_BackBtn;
	protected Text m_Title;
	protected Text m_Text;
	protected Button m_Add;
	protected Text m_Text_1;
	protected Button m_Add_1;
	protected Text m_Time;
	protected Button m_AddBtn;
	protected Text m_CurrentpowTxt;
	protected Text m_MaxpowTxt;
	protected Button m_ShopBtn;
	protected Text m_ShopBtnTxt;
	protected Text m_TopTxt;
	protected Text m_HintTxt1;
	protected Text m_TimesTxt1;
	protected Text m_SecondsTxt1;
	protected Text m_FreeTxt1;
	protected Text m_CostOneTxt1;
	protected Button m_DrawOneBtn1;
	protected Text m_DrawOneTxt1;
	protected Text m_CostTenTxt1;
	protected Button m_DrawTenBtn1;
	protected Text m_DrawTenTxt1;
	protected Text m_TitleTxt1;
	protected Text m_HintTitleTxt;
	protected Text m_HintTxt2;
	protected Text m_TimesTxt2;
	protected Text m_SecondsTxt2;
	protected Text m_FreeTxt2;
	protected Text m_CostOneTxt2;
	protected Button m_DrawOneBtn2;
	protected Text m_DrawOneTxt2;
	protected Text m_CostTenTxt2;
	protected Button m_DrawTenBtn2;
	protected Text m_DrawTenTxt2;
	protected Text m_TitleTxt2;

	public override void InitUIData()
	{
		base.InitUIData();
		m_BackBtn = selfTransform.FindChild("TopPanel/TopTittle/BackBtn").GetComponent<Button>();
		m_BackBtn.onClick.AddListener(OnClickBackBtn);
		m_Title = selfTransform.FindChild("TopPanel/TopTittle/Title").GetComponent<Text>();
		m_Text = selfTransform.FindChild("TopPanel/MoneyBarUI/Diamond/Text").GetComponent<Text>();
		m_Add = selfTransform.FindChild("TopPanel/MoneyBarUI/Diamond/Add").GetComponent<Button>();
		m_Add.onClick.AddListener(OnClickAdd);
		m_Text_1 = selfTransform.FindChild("TopPanel/MoneyBarUI/Money/Text").GetComponent<Text>();
		m_Add_1 = selfTransform.FindChild("TopPanel/MoneyBarUI/Money/Add").GetComponent<Button>();
		m_Add_1.onClick.AddListener(OnClickAdd_1);
		m_Time = selfTransform.FindChild("TopPanel/MoneyBarUI/Powers/Time").GetComponent<Text>();
		m_AddBtn = selfTransform.FindChild("TopPanel/MoneyBarUI/Powers/AddBtn").GetComponent<Button>();
		m_AddBtn.onClick.AddListener(OnClickAddBtn);
		m_CurrentpowTxt = selfTransform.FindChild("TopPanel/MoneyBarUI/Powers/CurrentpowTxt").GetComponent<Text>();
		m_MaxpowTxt = selfTransform.FindChild("TopPanel/MoneyBarUI/Powers/MaxpowTxt").GetComponent<Text>();
		m_ShopBtn = selfTransform.FindChild("ShopBtn").GetComponent<Button>();
		m_ShopBtn.onClick.AddListener(OnClickShopBtn);
		m_ShopBtnTxt = selfTransform.FindChild("ShopBtn/ShopBtnTxt").GetComponent<Text>();
		m_TopTxt = selfTransform.FindChild("TopTxt").GetComponent<Text>();
		m_HintTxt1 = selfTransform.FindChild("GoldLotteryObj/HintImg/HintTxt1").GetComponent<Text>();
		m_TimesTxt1 = selfTransform.FindChild("GoldLotteryObj/TimesTxt1").GetComponent<Text>();
		m_SecondsTxt1 = selfTransform.FindChild("GoldLotteryObj/SecondsTxt1").GetComponent<Text>();
		m_FreeTxt1 = selfTransform.FindChild("GoldLotteryObj/FreeTxt1").GetComponent<Text>();
		m_CostOneTxt1 = selfTransform.FindChild("GoldLotteryObj/ResourceOneImg1/CostOneTxt1").GetComponent<Text>();
		m_DrawOneBtn1 = selfTransform.FindChild("GoldLotteryObj/DrawOneBtn1").GetComponent<Button>();
		m_DrawOneBtn1.onClick.AddListener(OnClickDrawOneBtn1);
		m_DrawOneTxt1 = selfTransform.FindChild("GoldLotteryObj/DrawOneBtn1/DrawOneTxt1").GetComponent<Text>();
		m_CostTenTxt1 = selfTransform.FindChild("GoldLotteryObj/ResourceTenImg1/CostTenTxt1").GetComponent<Text>();
		m_DrawTenBtn1 = selfTransform.FindChild("GoldLotteryObj/DrawTenBtn1").GetComponent<Button>();
		m_DrawTenBtn1.onClick.AddListener(OnClickDrawTenBtn1);
		m_DrawTenTxt1 = selfTransform.FindChild("GoldLotteryObj/DrawTenBtn1/DrawTenTxt1").GetComponent<Text>();
		m_TitleTxt1 = selfTransform.FindChild("GoldLotteryObj/TitleImg/TitleTxt1").GetComponent<Text>();
		m_HintTitleTxt = selfTransform.FindChild("DiamondLotteryObj/HintTitleTxt").GetComponent<Text>();
		m_HintTxt2 = selfTransform.FindChild("DiamondLotteryObj/HintImg/HintTxt2").GetComponent<Text>();
		m_TimesTxt2 = selfTransform.FindChild("DiamondLotteryObj/TimesTxt2").GetComponent<Text>();
		m_SecondsTxt2 = selfTransform.FindChild("DiamondLotteryObj/SecondsTxt2").GetComponent<Text>();
		m_FreeTxt2 = selfTransform.FindChild("DiamondLotteryObj/FreeTxt2").GetComponent<Text>();
		m_CostOneTxt2 = selfTransform.FindChild("DiamondLotteryObj/ResourceOneImg2/CostOneTxt2").GetComponent<Text>();
		m_DrawOneBtn2 = selfTransform.FindChild("DiamondLotteryObj/DrawOneBtn2").GetComponent<Button>();
		m_DrawOneBtn2.onClick.AddListener(OnClickDrawOneBtn2);
		m_DrawOneTxt2 = selfTransform.FindChild("DiamondLotteryObj/DrawOneBtn2/DrawOneTxt2").GetComponent<Text>();
		m_CostTenTxt2 = selfTransform.FindChild("DiamondLotteryObj/ResourceTenImg2/CostTenTxt2").GetComponent<Text>();
		m_DrawTenBtn2 = selfTransform.FindChild("DiamondLotteryObj/DrawTenBtn2").GetComponent<Button>();
		m_DrawTenBtn2.onClick.AddListener(OnClickDrawTenBtn2);
		m_DrawTenTxt2 = selfTransform.FindChild("DiamondLotteryObj/DrawTenBtn2/DrawTenTxt2").GetComponent<Text>();
		m_TitleTxt2 = selfTransform.FindChild("DiamondLotteryObj/TitleImg/TitleTxt2").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
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

	protected virtual void OnClickShopBtn()
	{
	}

	protected virtual void OnClickDrawOneBtn1()
	{
	}

	protected virtual void OnClickDrawTenBtn1()
	{
	}

	protected virtual void OnClickDrawOneBtn2()
	{
	}

	protected virtual void OnClickDrawTenBtn2()
	{
	}

    public virtual void OnDestroy()
    {
        Destroy(m_BackBtn);
        Destroy(m_Title);
        Destroy(m_Text);
        Destroy(m_Add);
        Destroy(m_Text_1);
        Destroy(m_Add_1);
        Destroy(m_Time);
        Destroy(m_AddBtn);
        Destroy(m_CurrentpowTxt);
        Destroy(m_MaxpowTxt);
        Destroy(m_ShopBtn);
        Destroy(m_ShopBtnTxt);
        Destroy(m_TopTxt);
        Destroy(m_HintTxt1);
        Destroy(m_TimesTxt1);
        Destroy(m_SecondsTxt1);
        Destroy(m_FreeTxt1);
        Destroy(m_CostOneTxt1);
        Destroy(m_DrawOneBtn1);
        Destroy(m_DrawOneTxt1);
        Destroy(m_CostTenTxt1);
        Destroy(m_DrawTenBtn1);
        Destroy(m_DrawTenTxt1);
        Destroy(m_TitleTxt1);
        Destroy(m_HintTitleTxt);
        Destroy(m_HintTxt2);
        Destroy(m_TimesTxt2);
        Destroy(m_SecondsTxt2);
        Destroy(m_FreeTxt2);
        Destroy(m_CostOneTxt2);
        Destroy(m_DrawOneBtn2);
        Destroy(m_DrawOneTxt2);
        Destroy(m_CostTenTxt2);
        Destroy(m_DrawTenBtn2);
        Destroy(m_DrawTenTxt2);
        Destroy(m_TitleTxt2);
    }
}
