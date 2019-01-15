using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_RelicTreasureBase : BaseUI
{
	protected Text m_TopTipsText;
	protected Text m_ButTenGiftText;
	protected Text m_ReGoldNum;
	protected Button m_RefreshBtn;
	protected Text m_RefreshText;
	protected Text m_RefreshTipsText;
	protected Text m_ReDiamondNum;
	protected Text m_ButTenGiftText_1;
	protected Text m_BuyOneGoldNum;
	protected Button m_BuyOneBtn;
	protected Text m_BuyOneText;
	protected Text m_BuyOneTipsText;
	protected Text m_BuyOneDiamondNum;
	protected Text m_ButTenGiftText_2;
	protected Text m_BuyTenGoldNum;
	protected Button m_BuyTenBtn;
	protected Text m_BuyTenText;
	protected Text m_BuyTenDiamondNum;

	public override void InitUIData()
	{
		base.InitUIData();
		m_TopTipsText = selfTransform.FindChild("UI_Top/TopTipsText").GetComponent<Text>();
		m_ButTenGiftText = selfTransform.FindChild("UI_Bottom/ReGift/ButTenGiftText").GetComponent<Text>();
		m_ReGoldNum = selfTransform.FindChild("UI_Bottom/ReGift/ReGoldNum").GetComponent<Text>();
		m_RefreshBtn = selfTransform.FindChild("UI_Bottom/RefreshBtn").GetComponent<Button>();
		m_RefreshBtn.onClick.AddListener(OnClickRefreshBtn);
		m_RefreshText = selfTransform.FindChild("UI_Bottom/RefreshBtn/RefreshText").GetComponent<Text>();
		m_RefreshTipsText = selfTransform.FindChild("UI_Bottom/RefreshBtn/RefreshTipsText").GetComponent<Text>();
		m_ReDiamondNum = selfTransform.FindChild("UI_Bottom/RefreshBtn/ReDiamondNum").GetComponent<Text>();
		m_ButTenGiftText_1 = selfTransform.FindChild("UI_Bottom/OneGift/ButTenGiftText").GetComponent<Text>();
		m_BuyOneGoldNum = selfTransform.FindChild("UI_Bottom/OneGift/BuyOneGoldNum").GetComponent<Text>();
		m_BuyOneBtn = selfTransform.FindChild("UI_Bottom/BuyOneBtn").GetComponent<Button>();
		m_BuyOneBtn.onClick.AddListener(OnClickBuyOneBtn);
		m_BuyOneText = selfTransform.FindChild("UI_Bottom/BuyOneBtn/BuyOneText").GetComponent<Text>();
		m_BuyOneTipsText = selfTransform.FindChild("UI_Bottom/BuyOneBtn/BuyOneTipsText").GetComponent<Text>();
		m_BuyOneDiamondNum = selfTransform.FindChild("UI_Bottom/BuyOneBtn/BuyOneDiamondNum").GetComponent<Text>();
		m_ButTenGiftText_2 = selfTransform.FindChild("UI_Bottom/TenGift/ButTenGiftText").GetComponent<Text>();
		m_BuyTenGoldNum = selfTransform.FindChild("UI_Bottom/TenGift/BuyTenGoldNum").GetComponent<Text>();
		m_BuyTenBtn = selfTransform.FindChild("UI_Bottom/BuyTenBtn").GetComponent<Button>();
		m_BuyTenBtn.onClick.AddListener(OnClickBuyTenBtn);
		m_BuyTenText = selfTransform.FindChild("UI_Bottom/BuyTenBtn/BuyTenText").GetComponent<Text>();
		m_BuyTenDiamondNum = selfTransform.FindChild("UI_Bottom/BuyTenBtn/BuyTenDiamondNum").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickRefreshBtn()
	{
	}

	protected virtual void OnClickBuyOneBtn()
	{
	}

	protected virtual void OnClickBuyTenBtn()
	{
	}

    void OnDestroy()
    {
        Destroy(m_TopTipsText);
        Destroy(m_ButTenGiftText);
        Destroy(m_ReGoldNum);
        Destroy(m_RefreshBtn);
        Destroy(m_RefreshText);
        Destroy(m_RefreshTipsText);
        Destroy(m_ReDiamondNum);
        Destroy(m_ButTenGiftText_1);
        Destroy(m_BuyOneGoldNum);
        Destroy(m_BuyOneBtn);
        Destroy(m_BuyOneText);
        Destroy(m_BuyOneTipsText);
        Destroy(m_BuyOneDiamondNum);
        Destroy(m_ButTenGiftText_2);
        Destroy(m_BuyTenGoldNum);
        Destroy(m_BuyTenBtn);
        Destroy(m_BuyTenText);
        Destroy(m_BuyTenDiamondNum);
    }
}
