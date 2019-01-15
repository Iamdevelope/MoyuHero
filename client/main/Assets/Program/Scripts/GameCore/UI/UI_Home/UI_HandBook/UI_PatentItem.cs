using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.LogSystem;

public class UI_PatentItem : CellItem 
{
    //private Button m_RuneBtn;                                        //专属符文按钮
    private Text m_RuneNameTxt;                                      //专属符文名字
    private RuneIconItem m_RuneIcon;
    private ItemTemplate m_RuneData;
    
    public override void InitUIData()
    {
        base.InitUIData();        
        m_RuneNameTxt = selfTransform.FindChild("RuneNameTxt").GetComponent<Text>();
        //m_RuneBtn = selfTransform.GetComponent<Button>();
        //m_RuneBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickRuneBtn));
    }

    public override void InitUIView()
    {
        base.InitUIView();
    }

    /// <summary>
    ///初始化显示Item的数据
    /// </summary>
    /// <param name="runeHandBookData"></param>
    public void InitItemData(IllustratehandbookTemplate runeHandBookData)
    {
        Transform runeTrans = selfTransform.FindChild("RuneIconItem");
        m_RuneData = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(runeHandBookData.getContentId());
        m_RuneIcon = new RuneIconItem(runeTrans);
        InitSharShow();
        InitViewShow();
    }

    /// <summary>
    /// 初始化星级显示
    /// </summary>
    private void InitSharShow()
    {
        for (int i = 5; i < 5 + m_RuneData.getRune_quality(); i++)
        {
            Image temp = selfTransform.FindChild("Star_Image").GetChild(i).GetComponent<Image>();
            temp.enabled = true;
        }
    }

    /// <summary>
    /// 初始化界面能显示
    /// </summary>
    private void InitViewShow()
    {
        m_RuneNameTxt.text =GameUtils.getString(m_RuneData.getName());
        Sprite sprite = UIResourceMgr.LoadSprite(common.defaultPath + m_RuneData.getIcon_s());
        m_RuneIcon.SetIcon(sprite);
        m_RuneIcon.SetRuneType(m_RuneData.getRune_type());
        m_RuneIcon.SetIsSpecial(true);
        m_RuneIcon.AddIconClickListener(OnClickRuneBtn);
    }

    /// <summary>
    /// 专属符文Item 按钮
    /// </summary>
    private void OnClickRuneBtn()
    {
        //UI_PatentRuneManager.Inst.ItemMsgUI.SetActive(true);
        //UI_ItemMsgUI.Inst.ShowRuneData(m_RuneData);
        UI_RuneInfo.SetShowRuneDate(m_RuneData);
        UI_HomeControler.Inst.AddUI(UI_RuneInfo.UI_ResPath);
    }

}
