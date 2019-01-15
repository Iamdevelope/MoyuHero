using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork.Data;
using System.Collections.Generic;

public class EquipmentPanel : BaseUI
{
    public static EquipmentPanel Inst;
    protected GameObject m_Layout;
    public ObjectCard Card;

    EquipData m_EquipData;
    public int m_Index = 0;
    public override void InitUIData ()
    {
        Inst = this;
        base.InitUIData ();

        m_Layout = selfTransform.Find ( "EquipmentLayout" ).gameObject;
    }

    public override void InitUIView ()
    {
        base.InitUIView ();
    }

    // 点击 装备 icon
    public void OnClickEquipIcon ( EquipData equipdata, int index = 0 )
    {
        m_Index = index;
        //
        UpdateInfo ( Card, equipdata );

        //
        m_EquipData = equipdata;
    }

    // 更新装备信息
    public void UpdateInfo ( ObjectCard card, EquipData equipdata = null )
    {
        Card = card;

        //if ( equipdata == null && m_EquipData != null )
        //{
        //equipdata = m_EquipData;
        //}

        HeroData data = card.GetHeroData ();
        HeroEquipDB equip = data.HeroEqupDB;
        List<EquipData> equiplist = equip.EquipList;

        // 刷新每一个子节点
        for ( int i = 0; i < equiplist.Count; ++i )
        {
            bool ret = false;
            EquipmentqualityTemplate temp = ( EquipmentqualityTemplate ) DataTemplate.GetInstance ().m_EquipmentqualityTable.getTableData ( equiplist [ i ].TableID );
            if ( temp.getParts () - 1 == m_Index )
            {
                ret = true;
            }

            m_Layout.transform.GetChild ( temp.getParts () - 1 ).GetComponent<Equipment> ().UpdateEquipment ( equiplist [ i ], ret, temp.getParts () - 1 );
        }

        if ( equipdata == null )
        {
            equipdata = equiplist [ m_Index ];
        }

        // 刷新右侧功能面板
        if ( StrengthenEquipment.Inst != null )
        {
            StrengthenEquipment.Inst.UpdateInfo ( equipdata );
        }

        if ( EquipLetGood.Inst != null )
        {
            EquipLetGood.Inst.UpdateInfo ( equipdata );
        }
    }

    bool IsInEquipList ( EquipData equip, List<EquipData> equiplist )
    {
        for ( int i = 0; i < equiplist.Count; ++i )
        {
            if ( equip == equiplist [ i ] )
            {
                return true;
            }
        }
        return false;
    }

}
