using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.Utils;
using GNET;

public class ActivityItem : CellItem
{
    public delegate void m_OnClickHandle(int id);
    public event m_OnClickHandle m_OnClick = null;

    private Text m_TitleText;
    private Text m_HaveReceiveText;

    private GameObject m_LightImage;
    private GameObject m_BgImage;
    private GameObject m_RedPointImage;

    private Button m_IconButton;

    private ActivityOverviewData m_ActivityOverviewData;
    public int m_itemID = -10000;

    public override void InitUIData()
    {
        base.InitUIData();

        m_TitleText = selfTransform.FindChild("TitleText").GetComponent<Text>();
        m_HaveReceiveText = selfTransform.FindChild("ReceiveText").GetComponent<Text>();

        m_LightImage = selfTransform.FindChild("LightImage").gameObject;
        m_BgImage = selfTransform.FindChild("BgImage").gameObject;
        m_RedPointImage = selfTransform.FindChild("RedPointImage").gameObject;

        m_IconButton = selfTransform.GetComponent<Button>();
        m_IconButton.onClick.AddListener(OnIconBtnClick);
    }


    // 2：初始化UI显示内容
    public override void InitUIView()
    {
        base.InitUIView();
        m_HaveReceiveText.text = GameUtils.getString("sign_content4");
    }

    public void SetReceiveOverShow(bool isHave)
    {
        m_HaveReceiveText.gameObject.SetActive(isHave);
    }
    
    public void SetActivityDate(ActivityOverviewData _data, int _itemId)
    {
        m_ActivityOverviewData = _data;
        m_itemID = _itemId;
        GameactivityTemplate _itemData = (GameactivityTemplate)DataTemplate.GetInstance().m_GameactivityTable.getTableData(_data.m_id);
        m_TitleText.text = GameUtils.getString(_itemData.getTitledes());
        //Debug.Log("_data.m_id : " + _data.m_id + "----" + m_ActivityOverviewData.m_issee);
        m_RedPointImage.gameObject.SetActive(m_ActivityOverviewData.m_issee == 0 ? true : false);
    }

    public void SetOnClick(m_OnClickHandle OnClickHandle)
    {
        m_OnClick = OnClickHandle;
    }

    private void OnIconBtnClick()
    {
        GameactivityTemplate _Data = (GameactivityTemplate)DataTemplate.GetInstance().m_GameactivityTable.getTableData(m_ActivityOverviewData.m_id);
        int _team = _Data.getTeam();
        m_OnClick(_team); 
        if (m_ActivityOverviewData.m_issee == 0)
        {
            CSeeGameAct _CSeeGameAct = new CSeeGameAct();
            _CSeeGameAct.teamid = _team;
            //Debug.Log("_team : " + _team);
            IOControler.GetInstance().SendProtocol(_CSeeGameAct);
        }        
    }

    public void SetImageLight(bool isLight)
    {
        m_LightImage.SetActive(isLight);
        //GameactivityTemplate _Data = (GameactivityTemplate)DataTemplate.GetInstance().m_GameactivityTable.getTableData(m_ActivityOverviewData.m_id);
        //bool isSelected = teamId == _Data.getTeam();
    }
}
