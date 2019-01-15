using UnityEngine;

using System.Collections;
using UnityEngine.UI;

public class UI_LivenessItemBase : CellItem
{

    public Image m_LivenessIcon;
    public Text m_LivenessDesText;
    public Text m_GetLivenessNumText;
    public Text m_MissionDesText;
    public Text m_Finish;
    public Text m_LivenessNumMin;
    public Text m_LivenessNumMax;
    public Button m_GOBtn;
    public GameObject m_CompletedBG;


    public override void InitUIData()
    {
        base.InitUIData();

        m_LivenessIcon = transform.FindChild("LivenessIcon").GetComponent<Image>();
        m_LivenessDesText = transform.FindChild("LivenessDesText").GetComponent<Text>();
        m_GetLivenessNumText = transform.FindChild("GetLivenessNumText").GetComponent<Text>();
        m_MissionDesText = transform.FindChild("MissionDesText").GetComponent<Text>();
        m_Finish = transform.FindChild("Finish").GetComponent<Text>();
        m_LivenessNumMin = transform.FindChild("Finish/LivenessNumMin").GetComponent<Text>();
        m_LivenessNumMax = transform.FindChild("Finish/LivenssNumMax").GetComponent<Text>();
        m_GOBtn = transform.FindChild("GOBtn").GetComponent<Button>();
        m_GOBtn.onClick.AddListener(OnButtonClick);
        m_CompletedBG = transform.FindChild("CompletedBG").gameObject;
    }
    public virtual void OnButtonClick()
    {

    }
}
