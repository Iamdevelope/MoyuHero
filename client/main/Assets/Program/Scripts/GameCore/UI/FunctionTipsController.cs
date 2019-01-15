using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 与FunctionTipsController功能类似，用于多对多的场合
/// </summary>
public class FunctionTipsController : IFunctionTipsController
{
    private class ControlUnit
    {
        public GameObject m_ControlledObject;
        public List<Func<bool>> m_LogicDelegateList;

        public ControlUnit(GameObject controlledObject)
        {
            m_ControlledObject = controlledObject;
            m_LogicDelegateList = new List<Func<bool>>();
        }
        public void AddLogicDelegate(Func<bool> logicDelegate)
        {
            m_LogicDelegateList.Add(logicDelegate);
        }

        /// <summary>
        /// 一个游戏物体对应多个控制逻辑时，各逻辑直接是或关系
        /// </summary>
        public void Refresh()
        {
            if (m_ControlledObject == null)
                return;

            bool _result = false;
            for (int i = 0; i < m_LogicDelegateList.Count; i++)
            {
                if (m_LogicDelegateList[i] == null)
                    continue;

                _result |= m_LogicDelegateList[i]();
                if (_result)
                    break;
            }
            m_ControlledObject.SetActive(_result);
        }
    }

    private List<ControlUnit> m_UnitList;
    //每个控制器支持添加一个父单元，逻辑依赖其子单元结果
    private GameObject m_ParentUnit;
    private int[] m_ChildIndex;
    public FunctionTipsController()
    {
        m_UnitList = new List<ControlUnit>();
    }

    public int AddControlledObject(GameObject controlledObject, Func<bool> logicDelegate)
    {
        var temp = new ControlUnit(controlledObject);
        temp.AddLogicDelegate(logicDelegate);
        m_UnitList.Add(temp);
        return m_UnitList.Count - 1;
    }
    public int AddControlledObject(GameObject controlledObject, params Func<bool>[] logicDelegates)
    {
        var temp = new ControlUnit(controlledObject);
        for (int i = 0; i < logicDelegates.Length; i++)
        {
            temp.AddLogicDelegate(logicDelegates[i]);
        }
        m_UnitList.Add(temp);
        return m_UnitList.Count - 1;
    }
    public void AddLogicDelegate(int index, Func<bool> logicDelegate)
    {
        m_UnitList[index].AddLogicDelegate(logicDelegate);
    }
    public void AddParentCtrlObjUnit(GameObject controlledObject, params int[] childIndex)
    {
        m_ParentUnit = controlledObject;
        m_ChildIndex = childIndex;
    }

    public void Refresh()
    {
        for (int i = 0; i < m_UnitList.Count; i++)
        {
            m_UnitList[i].Refresh();
        }
        RefreshParent();
    }

    public void RefreshByIndex(int index)
    {
        if (index < 0 || index >= m_UnitList.Count)
            return;

        m_UnitList[index].Refresh();
        if (Array.Exists(m_ChildIndex, x => x == index))
        {
            RefreshParent();
        }
    }

    public void CloseAllTips()
    {
        m_ParentUnit.SetActive(false);
        m_UnitList.ForEach(controlUnit => controlUnit.m_ControlledObject.SetActive(false));
    }

    public int Count
    {
        get { return m_UnitList.Count; }
    }

    private void RefreshParent()
    {
        if (m_ParentUnit == null || m_ChildIndex == null)
        {
            return;
        }
        bool _value = false;
        for (int i = 0; i < m_ChildIndex.Length; i++)
        {
            int _index = m_ChildIndex[i];
            if (_index < 0 || _index >= m_UnitList.Count)
                continue;
            _value |= m_UnitList[_index].m_ControlledObject.active;
            if (_value)
                break;
        }

        m_ParentUnit.SetActive(_value);
    }

}
