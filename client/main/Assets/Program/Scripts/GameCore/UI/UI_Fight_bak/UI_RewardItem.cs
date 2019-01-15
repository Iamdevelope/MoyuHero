using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.Utils;
using DreamFaction.UI;
using System.Text;

public class UI_RewardItem : BaseUI 
{
    public int m_ItemID = 0;
    //奖励图片
    //private Image m_RewardImg = null;
    //是否获得奖励
    private GameObject m_GetObj = null;
    private Text m_GetText;
    private Transform m_Layout;
    //奖励数量
    //private Text m_RewardTxt = null;

    //private GameObject m_RewardNum = null;

    private UniversalItemCell m_Cell;
    
    public override void InitUIData()
    {
        base.InitUIData();
        //m_RewardImg = selfTransform.FindChild("Icon").GetComponent<Image>();
        m_GetObj = selfTransform.FindChild("Image").gameObject;
        m_GetText = selfTransform.FindChild("Image/Text").GetComponent<Text>();
        m_Layout = selfTransform.FindChild("Layout");
        //m_RewardNum = selfTransform.FindChild("Num").gameObject;
        //m_RewardTxt = selfTransform.FindChild("Num/Text").GetComponent<Text>();
        m_Cell = UniversalItemCell.GenerateItem(m_Layout);
    }

    public override void InitUIView()
    {
        base.InitUIView();
        m_GetObj.SetActive(false);
        m_GetText.text = GameUtils.getString("shop_content29");
    }

    //显示资源奖励
    public void ShowBoxResReward(int itemId, int conCount)
    {
        m_Cell.InitByID(itemId, conCount);
        //m_RewardImg.sprite = GameUtils.GetSpriteByResourceType(itemId);
        //m_RewardTxt.text = ReturnConCount(conCount);
    }

    public void ShowBoxItemReward(int itemId, int conCount)
    {
        m_Cell.InitByID(itemId, conCount);
        //ItemTemplate item = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(itemId);
        //m_RewardImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + item.getIcon());
        //m_RewardTxt.text = ReturnConCount(conCount);
    }

    public void SetYetGetImgActive(bool active)
    {
        m_GetObj.SetActive(active);
    }


    private string ReturnConCount(int count)
    {
        StringBuilder str = new StringBuilder();
        str.Append("×");
        str.Append(count);
        return str.ToString();
    }

}
