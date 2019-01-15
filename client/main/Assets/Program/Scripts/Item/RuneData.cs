using UnityEngine;
using System.Collections;
using GNET;
public class RuneData 
{
    private int                     m_TableID;
    //private int                     m_StrenghLevel;                 //强化等级
    private int[]                   m_BaseAttributeID= new int[GlobalMembers.MAX_RUNE_BASE_ATTRIBUTE_COUNT];   //基础属性ID
    private int[]                   m_AppendAttribute = new int[GlobalMembers.MAX_RUNE_APPEND_ATTRIBUTE_COUNT];//附加属性ID 默认-1

    public int[] BaseAttributeID
    {
        get { return m_BaseAttributeID; }
    }
    public int[] AppendAttribute
    {
        get { return m_AppendAttribute; }
    }
    public void SetBaseAttributeID(int[] id)
    {
        m_BaseAttributeID = id;
    }
    public void SetAppendAtttibute(int[] id)
    {
        m_AppendAttribute = id;
    }
    public void Init(int _tableID)
    {
        m_TableID = _tableID;
        //m_StrenghLevel = 0;
        for (int i = 0; i < GlobalMembers.MAX_RUNE_BASE_ATTRIBUTE_COUNT;++i )
        {
            m_BaseAttributeID[i] = -1;
        }
        for (int i = 0; i < GlobalMembers.MAX_RUNE_APPEND_ATTRIBUTE_COUNT; ++i)
        {
            m_AppendAttribute[i] = -1;
        }
    }

    public void RefreshData(EquipItemData _data)
    {
        m_BaseAttributeID[0] = _data.init1;
        m_BaseAttributeID[1] = _data.init2;
        m_BaseAttributeID[2] = _data.init3;

        m_AppendAttribute[0] = _data.attr1;
        m_AppendAttribute[1] = _data.attr2;
        m_AppendAttribute[2] = _data.attr3;
        m_AppendAttribute[3] = _data.attr4;
    }
}
