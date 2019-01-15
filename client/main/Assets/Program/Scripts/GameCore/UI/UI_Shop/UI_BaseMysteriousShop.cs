using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_BaseMysteriousShop : BaseUI
{
	protected Button m_CloseBtn;
	protected Text m_TimePanelText;
	protected Button m_BuyBtn;
	protected Text m_BuyBtnText;
	protected Text m_TittleText;
	protected Text m_DiscountText;
	protected Text m_ConfirmTittleText;
	protected Button m_ConfirmPayBtn;
	protected Text m_PayBtnText;
	protected Button m_ConfirmCloseBtn;
	protected Text m_CloseBtnText;
	protected Text m_PlayerWalletText;
    protected Text m_TopTittleText;
	public override void InitUIData()
	{
		base.InitUIData();
        m_TopTittleText = selfTransform.FindChild("TopPanel/TittleImage/TopTittleText").GetComponent<Text>();
		m_CloseBtn = selfTransform.FindChild("TopPanel/CloseBtn").GetComponent<Button>();
		m_CloseBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickCloseBtn));
		m_TimePanelText = selfTransform.FindChild("TopPanel/TimePanel/TimePanelText").GetComponent<Text>();
		m_BuyBtn = selfTransform.FindChild("OriginPanel/OriginItem/BuyBtn").GetComponent<Button>();
		m_BuyBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBuyBtn));
		m_BuyBtnText = selfTransform.FindChild("OriginPanel/OriginItem/BuyBtn/BuyBtnText").GetComponent<Text>();
		m_TittleText = selfTransform.FindChild("OriginPanel/OriginItem/TittleImage/TittleText").GetComponent<Text>();
		m_DiscountText = selfTransform.FindChild("OriginPanel/OriginItem/DiscountImage/DiscountText").GetComponent<Text>();
		m_ConfirmTittleText = selfTransform.FindChild("ConfirmPanel/ConfirmTittleImage/ConfirmTittleText").GetComponent<Text>();
		m_ConfirmPayBtn = selfTransform.FindChild("ConfirmPanel/ConfirmPayBtn").GetComponent<Button>();
		m_ConfirmPayBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickConfirmPayBtn));
		m_PayBtnText = selfTransform.FindChild("ConfirmPanel/ConfirmPayBtn/PayBtnText").GetComponent<Text>();
		m_ConfirmCloseBtn = selfTransform.FindChild("ConfirmPanel/ConfirmCloseBtn").GetComponent<Button>();
		m_ConfirmCloseBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickConfirmCloseBtn));
		m_CloseBtnText = selfTransform.FindChild("ConfirmPanel/ConfirmCloseBtn/CloseBtnText").GetComponent<Text>();
		m_PlayerWalletText = selfTransform.FindChild("ConfirmPanel/PlayerWalletText").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickCloseBtn()
	{
	}

	protected virtual void OnClickBuyBtn()
	{
	}

	protected virtual void OnClickConfirmPayBtn()
	{
	}

	protected virtual void OnClickConfirmCloseBtn()
	{
	}

}
