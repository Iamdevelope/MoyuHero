using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.UI;
using DreamFaction.Utils;

public class Equipment : BaseUI
{
    protected Image m_Bg;
    protected Image m_SelectBg;
    protected Image m_Icon;
    protected Text m_Name;
    protected Text m_Level;
    protected Button m_SelfBtn;
    protected EquipData m_EquipData;
    public int m_Index = 0;

    public override void InitUIData ()
    {
        base.InitUIData ();
        m_Bg = selfTransform.Find ( "Bg" ).GetComponent<Image> ();
        m_SelectBg = selfTransform.Find ( "SelectBg" ).GetComponent<Image> ();
        m_Icon = selfTransform.Find ( "Icon" ).GetComponent<Image> ();
        m_Name = selfTransform.Find ( "Name" ).GetComponent<Text> ();
        m_Level = selfTransform.Find ( "Level/Level" ).GetComponent<Text> ();
        m_SelfBtn = selfTransform.GetComponent<Button> ();
        m_SelfBtn.onClick.AddListener ( OnClickSelfBtn );
    }

    public override void InitUIView ()
    {
        base.InitUIView ();
    }

    public void OnClickSelfBtn ()
    {
        if ( EquipmentPanel.Inst != null )
        {
            EquipmentPanel.Inst.OnClickEquipIcon ( m_EquipData, m_Index );
        }        
    }

    /// <summary>
    /// 更新装备
    /// </summary>
    /// <param name="equipdata">装备数据</param>
    /// <param name="ret">是否选择状态</param>

    public void UpdateEquipment ( EquipData equipdata, bool ret, int index = 0 )
    {
        m_Index = index;
        m_EquipData = equipdata;
        int tableid = equipdata.TableID;
        EquipmentqualityTemplate temp = ( EquipmentqualityTemplate ) DataTemplate.GetInstance ().m_EquipmentqualityTable.getTableData ( tableid );

        m_Level.text = equipdata.IntensifyLev.ToString();

        // TODO...
        m_Bg.sprite = UIResourceMgr.LoadSprite ( common.defaultPath + GameUtils.GetEquipBgColor ( tableid ) );
        m_Icon.sprite = UIResourceMgr.LoadSprite ( common.defaultPath + temp.getIcon () );
        
        m_Name.text = temp.getName ();
        m_Name.color = GameUtils.GetEquipNameColor ( tableid );
        // 选择状态
        m_SelectBg.gameObject.SetActive ( ret );
    }

}
