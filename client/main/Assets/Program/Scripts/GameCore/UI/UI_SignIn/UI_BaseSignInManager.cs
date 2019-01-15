using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_BaseSignInManager : BaseUI
{
	protected Text m_SignInTopTittleText;
	protected Button m_SignInBackBtn;
	protected Text m_SignInBackText;
	protected Text m_RightPanelTipsText;
	protected Button m_CheckAwardBtn;
	protected Text m_CheckAwardBtnText;
	protected Text m_LeftPanelTittleText;
	protected Text m_ClaimCheckTittleText;
	protected Button m_ClaimBtn;
	protected Text m_ClaimBtnText;
	protected Text m_AwardCheckTopTittleText;
	protected Button m_AwardCheckCloseBtn;
	protected Button m_AwardCheckOkBtn;
	protected Text m_AwardCheckOkBtnText;

	public override void InitUIData()
	{
		base.InitUIData();
		m_SignInTopTittleText = selfTransform.FindChild("SignInPanel/SignInTopPanel/SignInTittleImage/SignInTopTittleText").GetComponent<Text>();
		m_SignInBackBtn = selfTransform.FindChild("SignInPanel/SignInTopPanel/SignInBackBtn").GetComponent<Button>();
		m_SignInBackBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickSignInBackBtn));
		m_SignInBackText = selfTransform.FindChild("SignInPanel/SignInTopPanel/SignInBackBtn/SignInBackText").GetComponent<Text>();
		m_RightPanelTipsText = selfTransform.FindChild("SignInPanel/SignInRightPanel/RightPanelTipsText").GetComponent<Text>();
		m_CheckAwardBtn = selfTransform.FindChild("SignInPanel/SignInRightPanel/CheckAwardBtn").GetComponent<Button>();
		m_CheckAwardBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickCheckAwardBtn));
		m_CheckAwardBtnText = selfTransform.FindChild("SignInPanel/SignInRightPanel/CheckAwardBtn/CheckAwardBtnText").GetComponent<Text>();
		m_LeftPanelTittleText = selfTransform.FindChild("SignInPanel/SignInLeftPanel/LeftPanelTittleText").GetComponent<Text>();
		m_ClaimCheckTittleText = selfTransform.FindChild("ClaimCheckPanel/Background/SubBackgroundImage/TittleImage/ClaimCheckTittleText").GetComponent<Text>();
		m_ClaimBtn = selfTransform.FindChild("ClaimCheckPanel/ClaimBtn").GetComponent<Button>();
		m_ClaimBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickClaimBtn));
		m_ClaimBtnText = selfTransform.FindChild("ClaimCheckPanel/ClaimBtn/ClaimBtnText").GetComponent<Text>();
		m_AwardCheckTopTittleText = selfTransform.FindChild("AwardCheckPanel/AwardCheckTopPanel/AwardCheckTittleImage/AwardCheckTopTittleText").GetComponent<Text>();
		m_AwardCheckCloseBtn = selfTransform.FindChild("AwardCheckPanel/AwardCheckTopPanel/AwardCheckCloseBtn").GetComponent<Button>();
		m_AwardCheckCloseBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickAwardCheckCloseBtn));
		m_AwardCheckOkBtn = selfTransform.FindChild("AwardCheckPanel/AwardCheckOkBtn").GetComponent<Button>();
		m_AwardCheckOkBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickAwardCheckOkBtn));
		m_AwardCheckOkBtnText = selfTransform.FindChild("AwardCheckPanel/AwardCheckOkBtn/AwardCheckOkBtnText").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickSignInBackBtn()
	{
	}

	protected virtual void OnClickCheckAwardBtn()
	{
	}

	protected virtual void OnClickClaimBtn()
	{
	}

	protected virtual void OnClickAwardCheckCloseBtn()
	{
	}

	protected virtual void OnClickAwardCheckOkBtn()
	{
	}

}
