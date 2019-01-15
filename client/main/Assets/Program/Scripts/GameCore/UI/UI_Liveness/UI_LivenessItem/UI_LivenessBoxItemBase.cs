using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_LivenessBoxItemBase : CellItem 
{
    public Text m_ActiveNumText;
    public Button m_NormalBox;
    public Button m_AdvancedBox;
    public Button m_OpenedBox;
    public Image m_Opened;
    public Image m_NormalLight;
    public Image m_PointImageLight;
    public Image m_PointImageHui;
    public Image m_AdvancedOpen;
     public override void InitUIData()
    {
        base.InitUIData();
        m_ActiveNumText = transform.FindChild("ActivityNumText").GetComponent<Text>();
        m_NormalBox = transform.FindChild("Normal").GetComponent<Button>();
        m_NormalBox.onClick.AddListener(OnButtonClick);
        m_AdvancedBox = transform.FindChild("Advanced").GetComponent<Button>();
        m_AdvancedBox.onClick.AddListener(OnButtonClick);
        m_OpenedBox = transform.FindChild("Opened").GetComponent<Button>();
        m_OpenedBox.onClick.AddListener(OnButtonClick);
        m_Opened = transform.FindChild("Opened").GetComponent<Image>();
        m_NormalLight = transform.FindChild("NormalLight").GetComponent<Image>();
        m_PointImageLight = transform.FindChild("PointImageLight").GetComponent<Image>();
        m_PointImageHui = transform.FindChild("PointImageHui").GetComponent<Image>();
        m_AdvancedOpen = transform.FindChild("AdvancedOpen").GetComponent<Image>();
    }

    public virtual void OnButtonClick()
    {
        OnButtonClick(gameObject);
    }

    public void OnButtonClick(GameObject go)
    {
        if (UI_Liveness._instance.itemClick != null)
            UI_Liveness._instance.itemClick(go);
    }
}
