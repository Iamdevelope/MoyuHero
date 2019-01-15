using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using System.Collections.Generic;

public class UI_MysticBase : HeroAttrPanel
{

    protected Transform m_LvText;
    protected Transform m_SpriteText;
    protected Transform m_InfoText;
    protected Transform m_NameText;
    protected Transform m_Lockgo;

    protected Text m_TipInfoText;
    protected List<Text> m_LevelList = new List<Text>();//µÈ¼¶Text
    protected List<Image> m_SpritelList = new List<Image>();//icon
    protected List<Text> m_InfoList = new List<Text>();
    protected List<Text> m_Info_2_List = new List<Text>();
    protected List<Text> m_NameList = new List<Text>();
    protected List<GameObject> m_LocklList = new List<GameObject>();//ËøÍ¼±ê

    protected Button m_Icon_0_Button;
    protected Button m_Icon_1_Button;
    protected Button m_Icon_2_Button;
    protected Button m_Icon_3_Button;
    protected Button m_Icon_4_Button;
    protected Button m_Icon_5_Button;
    

	public override void InitUIData()
	{
		base.InitUIData();
		
//------------------------------------------------------------------------------------------------------------
        m_TipInfoText = selfTransform.FindChild("TipInfo/image/Text").GetComponent<Text>();

        m_LvText = selfTransform.FindChild("SixLv").GetComponent<Transform>();
        m_SpriteText = selfTransform.FindChild("SixIcon").GetComponent<Transform>();
        m_InfoText = selfTransform.FindChild("SixInfo").GetComponent<Transform>();
        m_NameText = selfTransform.FindChild("name").GetComponent<Transform>();
        m_Lockgo = selfTransform.FindChild("lockIcon").GetComponent<Transform>();

        m_Icon_0_Button = selfTransform.FindChild("SixIcon/Icon_0").GetComponent<Button>();
        m_Icon_1_Button = selfTransform.FindChild("SixIcon/Icon_1").GetComponent<Button>();
        m_Icon_2_Button = selfTransform.FindChild("SixIcon/Icon_2").GetComponent<Button>();
        m_Icon_3_Button = selfTransform.FindChild("SixIcon/Icon_3").GetComponent<Button>();
        m_Icon_4_Button = selfTransform.FindChild("SixIcon/Icon_4").GetComponent<Button>();
        m_Icon_5_Button = selfTransform.FindChild("SixIcon/Icon_5").GetComponent<Button>();

        m_Icon_0_Button.onClick.AddListener(OnClick_0_Button);
        m_Icon_1_Button.onClick.AddListener(OnClick_1_Button);
        m_Icon_2_Button.onClick.AddListener(OnClick_2_Button);
        m_Icon_3_Button.onClick.AddListener(OnClick_3_Button);
        m_Icon_4_Button.onClick.AddListener(OnClick_4_Button);
        m_Icon_5_Button.onClick.AddListener(OnClick_5_Button);


        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < m_LvText.childCount; j++)
            {
                if ("Text_" + i.ToString() == m_LvText.GetChild(j).gameObject.name)
                {
                    m_LevelList.Add(m_LvText.GetChild(j).gameObject.GetComponent<Text>());
                }
            }
        }

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < m_SpriteText.childCount; j++)
            {
                if ("Icon_" + i.ToString() == m_SpriteText.GetChild(j).gameObject.name)
                {
                    m_SpritelList.Add(m_SpriteText.GetChild(j).gameObject.GetComponent<Image>());
                }
            }
        }

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < m_InfoText.childCount; j++)
            {
                if ("Text_info_" + i.ToString() == m_InfoText.GetChild(j).gameObject.name)
                {
                    m_InfoList.Add(m_InfoText.GetChild(j).gameObject.GetComponent<Text>());
                }
                if ("Text_info2_" + i.ToString() == m_InfoText.GetChild(j).gameObject.name)
                {
                    m_Info_2_List.Add(m_InfoText.GetChild(j).gameObject.GetComponent<Text>());
                }
            }
        }

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < m_NameText.childCount; j++)
            {
                if ("name" + i.ToString() == m_NameText.GetChild(j).gameObject.name)
                {
                    m_NameList.Add(m_NameText.GetChild(j).gameObject.GetComponent<Text>());
                }
            }
        }

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < m_Lockgo.childCount; j++)
            {
                if ("Icon_" + i.ToString() == m_Lockgo.GetChild(j).gameObject.name)
                {
                    m_LocklList.Add(m_Lockgo.GetChild(j).gameObject);
                }
            }
        }

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickwuPingitem()
	{
	}

	protected virtual void OnClickPop_icon()
	{
	}

	protected virtual void OnClickUpgradeButton()
	{
	}

    protected virtual void OnClick_0_Button()
    {
    }
    protected virtual void OnClick_1_Button()
    {
    }
    protected virtual void OnClick_2_Button()
    {
    }
    protected virtual void OnClick_3_Button()
    {
    }
    protected virtual void OnClick_4_Button()
    {
    }
    protected virtual void OnClick_5_Button()
    {
    }
    protected virtual void OnClickPopButton()
    {
    }

    void OnDestroy()
    {
        
    }
}
