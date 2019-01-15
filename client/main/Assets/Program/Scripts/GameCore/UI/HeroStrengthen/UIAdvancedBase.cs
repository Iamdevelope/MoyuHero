using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using System.Collections.Generic;

public class UIAdvancedBase : HeroAttrPanel
{
	protected Text m_CurStartHalosPnText;
	protected Text m_NextStartHalosPnText;
	protected Text m_NextStartHalosPnInfoText;
	protected Text m_LevelOpenText;
	protected Button m_AdvancedButton;
	protected Text m_SpendText;
    protected Text m_ButtonJinJieText;
    protected Text m_MaxLevelTipText;
    protected GameObject m_Atrributesitem;
    protected GameObject m_WuPingitem;
    protected Transform m_StartList;
    protected Transform m_StarList;
    protected List<GameObject> m_GrayGO = new List<GameObject>();
    protected List<GameObject> m_LightGO = new List<GameObject>();
    protected List<GameObject> m_StarGO = new List<GameObject>();
    protected GameObject m_MaxLevelWindow;
    protected GameObject m_NoMaxLevelWindow;
    protected Transform NoAdvancedEffect;//没有进阶的特效
    protected Transform YesAdvancedEffect;//进阶的特效


	public override void InitUIData()
	{
		base.InitUIData();
        m_CurStartHalosPnText = selfTransform.FindChild("NoMaxLevelWindow/Text/CurStartHalosPnText").GetComponent<Text>();
        m_NextStartHalosPnText = selfTransform.FindChild("NoMaxLevelWindow/Text/NextStartHalosPnText").GetComponent<Text>();
        m_NextStartHalosPnInfoText = selfTransform.FindChild("NoMaxLevelWindow/Text/NextStartHalosPnInfoText").GetComponent<Text>();
        m_LevelOpenText = selfTransform.FindChild("NoMaxLevelWindow/Button/LevelOpenText").GetComponent<Text>();
        m_AdvancedButton = selfTransform.FindChild("NoMaxLevelWindow/Button/AdvancedButton").GetComponent<Button>();
		m_AdvancedButton.onClick.AddListener(OnClickAdvancedButton);
        m_SpendText = selfTransform.FindChild("NoMaxLevelWindow/Button/AdvancedButton/Text").GetComponent<Text>();
        m_ButtonJinJieText = selfTransform.FindChild("NoMaxLevelWindow/Button/AdvancedButton/Text2").GetComponent<Text>();
        m_MaxLevelTipText = selfTransform.FindChild("MaxLevelWindow/Text").GetComponent<Text>();
        m_Atrributesitem = selfTransform.FindChild("NoMaxLevelWindow/AttributesList/StartLayout/Atrributesitem").gameObject;
        m_WuPingitem = selfTransform.FindChild("NoMaxLevelWindow/wuPingList/wuPingLayout/wuPingitem").gameObject;
        m_StartList = selfTransform.FindChild("NoMaxLevelWindow/StartList/StartLayout").transform;
        m_StarList = selfTransform.FindChild("NoMaxLevelWindow/StarList/StartLayout").transform;
        m_MaxLevelWindow = selfTransform.FindChild("MaxLevelWindow").gameObject;
        m_NoMaxLevelWindow = selfTransform.FindChild("NoMaxLevelWindow").gameObject;
        NoAdvancedEffect = selfTransform.FindChild("NoMaxLevelWindow/StartList/StartLayout/UI_Effect_JinJieGeZi01").GetComponent<Transform>();
        YesAdvancedEffect = selfTransform.FindChild("NoMaxLevelWindow/StartList/StartLayout/UI_Effect_JinJieGeZi02").GetComponent<Transform>();
        for (int i = 0; i < m_StartList.childCount; ++i)
        {
            if (m_StartList.GetChild(i).gameObject.name == "GrayImage" + i)
            {
                 m_GrayGO.Add(m_StartList.GetChild(i).gameObject);
            }
            for (int j = 0; j < m_StartList.GetChild(i).transform.childCount; j++)
            {
                if (m_StartList.GetChild(i).transform.GetChild(j).gameObject.name == "LightImage")
                {
                    m_LightGO.Add(m_StartList.GetChild(i).transform.GetChild(j).gameObject);
                }
            }
        }

        for (int i = 0; i < m_StarList.childCount; ++i)
        {
            if (m_StarList.GetChild(i).gameObject.name == "star" + i)
            {
                m_StarGO.Add(m_StarList.GetChild(i).gameObject);
            }
        } 
	}

    public override void ShowHeroInfo(ObjectCard heroCard)
    {

    }

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickAdvancedButton()
	{
	}

    void OnDestroy()
    {
        
    }
}
