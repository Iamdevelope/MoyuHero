using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
public class UIBase : UIBehaviour, IData, IButtonClickHnadler,IItemClickHandler
{
    private object m_Data;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    public virtual void Data(object data)
    {
        m_Data = data;
    }
 
    public virtual object GetData()
    {
        return m_Data;
    }
    public virtual void OnButtonClick(GameObject go)
    {

    }
    public virtual void OnButtonClick()
    {

    }
    public virtual void OnItemClick(GameObject go)
    {

    }
}

