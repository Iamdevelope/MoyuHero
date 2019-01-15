using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UILoopItem : UIBase{

    public UILoop.ButtonClick onItemClick;

    private int m_Index;
    public int Index { get { return m_Index; } }

    private object m_Data;
    public virtual void Data(object data)
    {
        base.Data(data);
    }

    public virtual void UpdateItem(int index,GameObject item)
    {
        m_Index = index;
    }

    public override void OnButtonClick()
    {
        OnButtonClick(gameObject);
    }

    public override void OnButtonClick(GameObject go)
    {
        if (onItemClick != null)
            onItemClick(go);
    }
}
