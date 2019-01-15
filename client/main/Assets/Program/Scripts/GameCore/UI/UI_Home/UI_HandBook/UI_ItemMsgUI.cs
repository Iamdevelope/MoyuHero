using UnityEngine;
using System.Collections;
using DreamFaction.Utils;
using UnityEngine.UI;
using DreamFaction.UI;

public class UI_ItemMsgUI : UI_ItemMsgUIBase
{
    public static UI_ItemMsgUI Inst;
    private ItemTemplate m_ItemData;                  //道具表数据
    private RuneIconItem m_RuneIconItem;              //符文Item  
    private Transform m_RuneIconTrans;                //符文Transform 

    public override void InitUIData()
    {
        base.InitUIData();
        Inst = this;
        m_RuneIconTrans = selfTransform.FindChild("RuneIconItem");
    }


    /// <summary>
    /// 显示专属符文详细
    /// </summary>
    /// <param name="itemData"></param>
    public void ShowRuneData(ItemTemplate itemData)
    {
        m_ItemData = itemData;
        ShowRuneData();
        ShowRuneShar();
        ShowRuneDes();
    }

    /// <summary>
    /// 显示符文信息
    /// </summary>
    private void ShowRuneData()
    {
        m_RuneName.text = GameUtils.getString(m_ItemData.getName());
        m_PatentHeroTxt.text = GameUtils.getString(m_ItemData.getSpecialHeroDes());
        m_RuneIconItem = new RuneIconItem(m_RuneIconTrans);
        m_RuneIconItem.SetIcon(common.defaultPath + m_ItemData.getIcon());
        m_RuneIconItem.SetRuneType(m_ItemData.getRune_type());
        m_RuneIconItem.SetLevelInfoActive(false);
        m_RuneIconItem.SetIsSpecial(RuneModule.IsSpecialRune(m_ItemData));
    }
    /// <summary>
    /// 显示符文描述
    /// </summary>
    private void ShowRuneDes()
    {
        if (m_ItemData.getRune_baseAttri1() == -1 || m_ItemData.getRune_baseAttri2() == -1)
            return;

        BaseruneattributeTemplate _baseRune1 = (BaseruneattributeTemplate)DataTemplate.GetInstance().m_BaseruneattributeTable.getTableData(m_ItemData.getRune_baseAttri1() * 100);
        BaseruneattributeTemplate _baseRune2 = (BaseruneattributeTemplate)DataTemplate.GetInstance().m_BaseruneattributeTable.getTableData(m_ItemData.getRune_baseAttri2() * 100);
        m_PhyAttackTxt.text = GameUtils.getString(_baseRune1.getAttriDes());
        m_PhyAttackNumTxt.text = _baseRune1.getAttriValue().ToString();
        m_DesTxt.text = GameUtils.getString(_baseRune2.getAttriDes());
    }

    /// <summary>
    /// 显示符文星级
    /// </summary>
    private void ShowRuneShar()
    {
        for (int i = 5; i < 5 + m_ItemData.getRune_quality(); i++)
        {
            Image temp = selfTransform.FindChild("Star_Image").GetChild(i).GetComponent<Image>();
            temp.enabled = true;
        }
    }

    /// <summary>
    /// 关闭按钮
    /// </summary>
    protected override void OnClickCloseBtn()
    {
        gameObject.SetActive(false);
    }
}
