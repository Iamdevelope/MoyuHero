using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_MissionItemManage : BaseUI
{
	protected Text m_CompletedText;
	protected Button m_GOBtn;
	protected Text m_Text;
	protected Text m_LivenessDesText;
	protected Text m_GetLivenessNumText;
	protected Text m_MissionDesText;
	protected Text m_Finish;
	protected Text m_LivenessNumMin;
	protected Text m_LivenssNumMax;

	public override void InitUIData()
	{
		base.InitUIData();
		m_CompletedText = selfTransform.FindChild("CompletedBG/CompletedText").GetComponent<Text>();
		m_GOBtn = selfTransform.FindChild("GOBtn").GetComponent<Button>();
		m_GOBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickGOBtn));
		m_Text = selfTransform.FindChild("GOBtn/Text").GetComponent<Text>();
		m_LivenessDesText = selfTransform.FindChild("LivenessDesText").GetComponent<Text>();
		m_GetLivenessNumText = selfTransform.FindChild("LivenessDesText/GetLivenessNumText").GetComponent<Text>();
		m_MissionDesText = selfTransform.FindChild("MissionDesText").GetComponent<Text>();
		m_Finish = selfTransform.FindChild("Finish").GetComponent<Text>();
		m_LivenessNumMin = selfTransform.FindChild("Finish/LivenessNumMin").GetComponent<Text>();
		m_LivenssNumMax = selfTransform.FindChild("Finish/LivenssNumMax").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickGOBtn()
	{
	}

}
