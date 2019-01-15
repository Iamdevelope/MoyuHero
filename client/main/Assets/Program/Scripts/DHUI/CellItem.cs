using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;

public class CellItem : BaseUI
{
    [SerializeField]
    [ReadOnly]
    protected int m_Index;

    public int index
    {
        get
        {
            return m_Index;
        }
        set
        {
            m_Index = value;
        }
    }

    public virtual void UpdateItem(int index, RectTransform cell)
    {
        m_Index = index;
    }
}
