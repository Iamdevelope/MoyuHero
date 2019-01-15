using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public interface IData
{
    void Data(object data);

    object GetData();
}

/// <summary>
/// 当前组件中的按钮点击触发的事件
/// </summary>
public interface IButtonClickHnadler
{
    void OnButtonClick(GameObject go);
    void OnButtonClick();
}

#region hide
/*
public interface IEventDispatcher
{
    void AddEventListener(string type, Action<BaseEvent> listener, int priority, bool useWeakReference);

    void RemoveEventListener(string type, Action<BaseEvent> listener);

    void RemoveEventListeners(string type);

    bool HasEventListener(string type);

    void DispatchEvent(BaseEvent evt);
}
*/
#endregion

/// <summary>
/// 当前组件中的UILoop中的Item被点击触发的事件
/// </summary>
public interface IItemClickHandler
{
    void OnItemClick(GameObject go);
}
