using UnityEngine;
using System.Collections;
using GNET;
using DreamFaction.GameNetWork;
/// <summary>
/// 符文类道具
/// </summary>
public class ItemEquip : BaseItem 
{
    private RuneData        m_RuneData = new RuneData();

    private bool m_IsEquip;         //是否已装配
    private int  m_StrenghLevel;    //强化等级
    private bool m_IsAllCount;
    public ItemEquip()
    {
        m_IsEquip = false;
        m_StrenghLevel = 0;
    }

    public RuneData GetRuneData() { return m_RuneData; }

    public override int GetItemType()
    {
        return (int)EM_ITEM_TYPE.EM_ITEM_TYPE_RUNE;
    }

    public void InitRuneData(EquipItemData _data)
    {
        m_RuneData.RefreshData(_data);
        SetStrenghLevel();
    }
    public override void SetItemTableID(int nID)
    {
        base.SetItemTableID(nID);
        m_RuneData.Init(nID);
    }

    public void ResetEquipState()
    {
        //需要英雄背包初始化好后再调用 [5/21/2015 Zmy]
        bool _isEquip = ObjectSelf.GetInstance().HeroContainerBag.IsItemEquiped(this);
        SetEquipState(_isEquip);
    }
    public override bool IsEquip()
    {
        return m_IsEquip;
    }
    public void SetEquipState(bool state)
    {
        m_IsEquip = state;
    }
    public void RefreshItemEquip()
    {

    }
    public void SetStrenghLevel()
    {
        for (int i = 0; i < GlobalMembers.MAX_RUNE_BASE_ATTRIBUTE_COUNT; ++i )
        {
            if (DataTemplate.GetInstance().m_BaseruneattributeTable.tableContainsKey(m_RuneData.BaseAttributeID[i]))
            {
                BaseruneattributeTemplate _row = (BaseruneattributeTemplate)DataTemplate.GetInstance().m_BaseruneattributeTable.getTableData(m_RuneData.BaseAttributeID[i]);
                m_StrenghLevel = _row.getLevel();
                break;
            }
        }
    }
    public override int GetStrenghLevel()
    {
        return m_StrenghLevel;
    }

    /// <summary>
    /// 已经鉴定的次数;
    /// </summary>
    /// <returns></returns>
    public int GetDefineTimes()
    {
        int count = 0;
        
        foreach(int i in m_RuneData.AppendAttribute)
        {
            if (i == -1)
                continue;

            count++;
        }

        return count;
    }

    public override void OnUseItem(int _count)
    {

    }

    public override bool IsAllCount()
    {
        return m_IsAllCount;
    }

    public override void OnSellItem(int _count)
    {
        if (_count > m_Count)
        {
            return;
        }
        if (_count==m_Count)
        {
            m_IsAllCount = true;
        }
        else
        {
            m_IsAllCount = false;
        }
        CSellItem msg = new CSellItem();
        msg.bagid = (int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP;
        msg.items.Add((int)m_BaseGuid.GUID_value, _count);
        IOControler.GetInstance().SendProtocol(msg);
    }
}
