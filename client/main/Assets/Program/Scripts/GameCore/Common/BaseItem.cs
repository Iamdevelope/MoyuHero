/********************************************************************
	created:	2015/04/09
	created:	9:4:2015   20:37
	filename: 	D:\Dream Heros\trunk\Assets\Program\Scripts\GameCore\Common\BaseItem.cs
	file path:	D:\Dream Heros\trunk\Assets\Program\Scripts\GameCore\Common
	file base:	BaseItem
	file ext:	cs
	author:		Zhao Mingyang
	
	purpose:	道具抽象类，定义道具基础的属性处理
*********************************************************************/

using UnityEngine;
using System;
using System.Collections;
using DreamFaction.GameNetWork;
using DreamFaction.GameNetWork.Data;
public abstract class BaseItem : IComparable<BaseItem>
{
    protected X_GUID            m_BaseGuid = new X_GUID();
    protected int               m_BaseTableID;

    protected int               m_Count;//当前数量
    protected short             m_TimesCount;//今日已使用次数

    public int CompareTo(BaseItem other)
    {
        return 0;
    }
    public BaseItem()
    {
        m_BaseGuid.CleanUp();
        m_BaseTableID = -1;
        m_Count = 0;
    }

    public abstract int         GetItemType();
    public abstract void        OnUseItem(int _count);
    public abstract void        OnSellItem(int _count);
    public virtual X_GUID       GetItemGuid()
    {
        return m_BaseGuid;
    }
    public virtual void         SetItemGuid(long guid)
    {
        m_BaseGuid.GUID_value = guid;
    }
    public virtual int         GetItemTableID()
    {
        return m_BaseTableID;
    }
    public virtual void        SetItemTableID(int nID)
    {
        m_BaseTableID = nID;
    }
    public virtual ItemTemplate GetItemRowData()
    {
        if (m_BaseTableID < ((int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE * 1000000) || m_BaseTableID >= ((int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO * 1000000))
        {
            return null;
        }
        ItemTemplate m_RowData = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(m_BaseTableID);
        return m_RowData;
    }

    public virtual int      GetItemCount()
    {
        return m_Count;
    }
    public virtual void     SetItemCount(int nValue)
    {
        m_Count = nValue;
    }

    public virtual short    GetItemTimesCount()
    {
        return m_TimesCount;
    }

    public virtual void     SetItemTimesCount(short nValue)
    {
        m_TimesCount = nValue;
    }

    public virtual bool     IsEquip()
    {
        return false;
    }
    
    public virtual bool IsAllCount()
    {
        return false;
    }

    public virtual int      GetStrenghLevel()
    {
        return -1;
    }
    public int GetItemClass()
    {
        return m_BaseTableID / 1000000;
    }
}
