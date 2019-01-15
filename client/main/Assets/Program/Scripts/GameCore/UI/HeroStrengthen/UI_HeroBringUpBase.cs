using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using System.Collections.Generic;

public class UI_HeroBringUpBase : HeroAttrPanel
{
	protected Text m_addPowText;
	protected Text m_NextLevelText;
	protected Button m_BringButton;
    protected Text m_BringText;
	protected Button m_ResetButton;
    protected Text m_ResetText;
    protected Text m_ElementMaxLevel;
    protected Text m_ResetFont;
    protected Text m_BringUpFont;
    //protected GameObject m_CurAtrributesitem;
    protected GameObject m_UpEffect;
    protected Text m_CurAtrributesitemText_0;
    protected Text m_CurAtrributesitemText_1;
    protected Text m_NextAtrributesitemText_0;
    protected Text m_NextAtrributesitemText_1;
    protected GameObject m_LightPoint;
    protected Transform m_WuPingitem;
    protected Transform m_FourLv;
    protected List<Text> m_FourLvList = new List<Text>();

    protected Button m_HuoButton;
    protected Button m_WaterButton;
    protected Button m_EarthButton;
    protected Button m_WindButton;

	public override void InitUIData()
	{
		base.InitUIData();
		m_addPowText = selfTransform.FindChild("Text/addPowText").GetComponent<Text>();
        m_NextLevelText = selfTransform.FindChild("NextAttributesList/NextLevelText").GetComponent<Text>();
		m_BringButton = selfTransform.FindChild("Button/BringButton").GetComponent<Button>();
		m_BringButton.onClick.AddListener(OnClickBringButton);
        m_BringText = selfTransform.FindChild("Button/BringButton/Text").GetComponent<Text>();
		m_ResetButton = selfTransform.FindChild("Button/ResetButton").GetComponent<Button>();
		m_ResetButton.onClick.AddListener(OnClickResetButton);
        m_ResetText = selfTransform.FindChild("Button/ResetButton/Text").GetComponent<Text>();
        m_UpEffect = selfTransform.FindChild("FourIcon/Ui_Effect_Peiyangtisheng01").gameObject;
        m_ElementMaxLevel = selfTransform.FindChild("maxLevelTip").GetComponent<Text>();
        m_ResetFont = selfTransform.FindChild("Button/ResetButton/Text").GetComponent<Text>();
        m_BringUpFont = selfTransform.FindChild("Button/BringButton/Text").GetComponent<Text>();
        //m_NextAtrributesitem = selfTransform.FindChild("NextAttributesList/StartLayout/Atrributesitem").gameObject;

        m_CurAtrributesitemText_0 = selfTransform.FindChild("CurAttributesList/StartLayout/Atrributesitem_0/Right_text").GetComponent<Text>();
        m_CurAtrributesitemText_1 = selfTransform.FindChild("CurAttributesList/StartLayout/Atrributesitem_1/Right_text").GetComponent<Text>();
        m_NextAtrributesitemText_0 = selfTransform.FindChild("NextAttributesList/StartLayout/Atrributesitem_0/Right_text").GetComponent<Text>();
        m_NextAtrributesitemText_1 = selfTransform.FindChild("NextAttributesList/StartLayout/Atrributesitem_1/Right_text").GetComponent<Text>();
        m_WuPingitem = selfTransform.FindChild("wuPingList/wuPingLayout/wuPingitem").transform;
        m_FourLv = selfTransform.FindChild("FourLv").transform;
        m_LightPoint = selfTransform.FindChild("FourIcon/Light").gameObject;

        m_HuoButton = selfTransform.FindChild("FourIcon/huo").GetComponent<Button>();
        m_HuoButton.onClick.AddListener(OnClickHuoButton);
        m_WaterButton = selfTransform.FindChild("FourIcon/water").GetComponent<Button>();
        m_WaterButton.onClick.AddListener(OnClickWaterButton);
        m_EarthButton = selfTransform.FindChild("FourIcon/earth").GetComponent<Button>();
        m_EarthButton.onClick.AddListener(OnClickEarthButton);
        m_WindButton = selfTransform.FindChild("FourIcon/wind").GetComponent<Button>();
        m_WindButton.onClick.AddListener(OnClickWindButton);

        for (int i = 0; i < 4;i++ )
        {
            for (int j = 0; j < m_FourLv.childCount; j++)
            {
                if ("Lv_" + i.ToString() == m_FourLv.GetChild(j).gameObject.name)
                {
                    m_FourLvList.Add(m_FourLv.GetChild(j).gameObject.GetComponent<Text>());
                }
            }
        }
	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

    protected virtual void OnClickResetButton()
    {
    }

	protected virtual void OnClickBringButton()
	{
	}

    protected virtual void OnClickHuoButton()
    {
    }

    protected virtual void OnClickWaterButton()
    {
    }

    protected virtual void OnClickEarthButton()
    {
    }

    protected virtual void OnClickWindButton()
    {
    }

    public override void ShowHeroInfo(ObjectCard heroCard)
    {

    }


    void OnDestroy()
    {
        
    }
}
