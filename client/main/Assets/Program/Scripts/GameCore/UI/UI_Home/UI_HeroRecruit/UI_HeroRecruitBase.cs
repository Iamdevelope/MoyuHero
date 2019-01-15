using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_HeroRecruitBase : BaseUI
{
	protected Button m_BuyOneBtn;
	protected Text m_BuyOneText;
	protected Text m_Text;
	protected Button m_LeftPreBtn;
	protected Text m_LeftPreText;
	protected Text m_LeftCountDown;
	protected Text m_LeftPrice;
	protected Text m_LeftFreeTipsText;
	protected Button m_CenterPreBtn;
	protected Text m_CenterPreText;
	protected Text m_NumTipsText;
	protected Button m_ExchangeBtn; 
	protected Text m_ExchangeText;
	protected Text m_BottomTipsText;
	protected Button m_BuyTenBtn;
	protected Text m_BuyTenText;
	protected Button m_RightPreBtn;
	protected Text m_RightPreText;
	protected Text m_RightPrice;

	public override void InitUIData()
	{
		base.InitUIData();
		m_BuyOneBtn = selfTransform.FindChild("Left/BuyOneBtn").GetComponent<Button>();
		m_BuyOneBtn.onClick.AddListener(OnClickBuyOneBtn);
		m_BuyOneText = selfTransform.FindChild("Left/BuyOneBtn/BuyOneText").GetComponent<Text>();
		m_LeftPreBtn = selfTransform.FindChild("Left/LeftPreBtn").GetComponent<Button>();
		m_LeftPreBtn.onClick.AddListener(OnClickLeftPreBtn);
		m_LeftPreText = selfTransform.FindChild("Left/LeftPreBtn/LeftPreText").GetComponent<Text>();
		m_LeftCountDown = selfTransform.FindChild("Left/LeftCountDown").GetComponent<Text>();
		m_LeftPrice = selfTransform.FindChild("Left/LeftPrice/LeftPrice").GetComponent<Text>();
		m_LeftFreeTipsText = selfTransform.FindChild("Left/LeftFreeTipsText").GetComponent<Text>();
		m_CenterPreBtn = selfTransform.FindChild("Center/CenterPreBtn").GetComponent<Button>();
		m_CenterPreBtn.onClick.AddListener(OnClickCenterPreBtn);
		m_CenterPreText = selfTransform.FindChild("Center/CenterPreBtn/CenterPreText").GetComponent<Text>();
		m_NumTipsText = selfTransform.FindChild("Center/NumTipsText").GetComponent<Text>();
		m_ExchangeBtn = selfTransform.FindChild("Center/ExchangeBtn").GetComponent<Button>();
		m_ExchangeBtn.onClick.AddListener(OnClickExchangeBtn);
		m_ExchangeText = selfTransform.FindChild("Center/ExchangeBtn/ExchangeText").GetComponent<Text>();
		m_BottomTipsText = selfTransform.FindChild("Center/BottonTips/BottomTipsText").GetComponent<Text>();
		m_BuyTenBtn = selfTransform.FindChild("Right/BuyTenBtn").GetComponent<Button>();
		m_BuyTenBtn.onClick.AddListener(OnClickBuyTenBtn);
		m_BuyTenText = selfTransform.FindChild("Right/BuyTenBtn/BuyTenText").GetComponent<Text>();
		m_RightPreBtn = selfTransform.FindChild("Right/RightPreBtn").GetComponent<Button>();
		m_RightPreBtn.onClick.AddListener(OnClickRightPreBtn);
		m_RightPreText = selfTransform.FindChild("Right/RightPreBtn/RightPreText").GetComponent<Text>();
		m_RightPrice = selfTransform.FindChild("Right/RightPrice/RightPrice").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickBuyOneBtn()
	{
	}

	protected virtual void OnClickLeftPreBtn()
	{
	}

	protected virtual void OnClickCenterPreBtn()
	{
	}

	protected virtual void OnClickExchangeBtn()
	{
	}

	protected virtual void OnClickBuyTenBtn()
	{
	}

	protected virtual void OnClickRightPreBtn()
	{
	}

    void OnDestroy()
    {
        Destroy(m_BuyOneBtn);
        Destroy(m_BuyOneText);
        Destroy(m_Text);
        Destroy(m_LeftPreBtn);
        Destroy(m_LeftPreText);
        Destroy(m_LeftCountDown);
        Destroy(m_LeftPrice);
        Destroy(m_LeftFreeTipsText);
        Destroy(m_CenterPreBtn);
        Destroy(m_CenterPreText);
        Destroy(m_NumTipsText);
        Destroy(m_ExchangeBtn);
        Destroy(m_ExchangeText);
        Destroy(m_BottomTipsText);
        Destroy(m_BuyTenBtn);
        Destroy(m_BuyTenText);
        Destroy(m_RightPreBtn);
        Destroy(m_RightPreText);
        Destroy(m_RightPrice);
    }
}
