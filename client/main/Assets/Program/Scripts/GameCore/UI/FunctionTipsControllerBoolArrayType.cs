using System;
using UnityEngine;

public interface IFunctionTipsController
{
    /// <summary>
    /// 刷新控制器控制的所有游戏物体
    /// </summary>
    void Refresh();
    /// <summary>
    /// 按索引刷新某个游戏物体
    /// </summary>
    /// <param name="index"></param>
    void RefreshByIndex(int index);

    void CloseAllTips();
}

/// <summary>
/// 多对一控制器，多个游戏物体收到一个控制逻辑返回的布尔数组分别控制
/// </summary>
public class FunctionTipsControllerBoolArrayType : IFunctionTipsController
{
    private GameObject[] m_ControlledObjectArray;        
    private Func<bool[]> m_LogicDelegateListType;
    private bool[] m_LastResult;

    /// <summary>
    /// 生成控制多个GameObject的控制器，参数1数组长度务必和参数2返回的数组长度相等
    /// 否则Refresh函数有可能越界
    /// </summary>
    /// <param name="controlledObjectArr">多个被控制物体的数组</param>
    /// <param name="logicDelegateListType">控制逻辑，返回bool数组长度需与参数1匹配</param>
    public FunctionTipsControllerBoolArrayType(GameObject[] controlledObjectArr, Func<bool[]> logicDelegateListType)
    {
        m_ControlledObjectArray = controlledObjectArr;
        m_LogicDelegateListType = logicDelegateListType;
    }

    public void Refresh()
    {
        m_LastResult = m_LogicDelegateListType();
        for (int i = 0; i < m_ControlledObjectArray.Length; i++)
        {
            if (m_ControlledObjectArray[i] != null)
            m_ControlledObjectArray[i].SetActive(m_LastResult[i]);
        }
    }
    public void RefreshByIndex(int index)
    {
        if (m_LastResult == null || index < 0 || index >= m_LastResult.Length || index >= m_ControlledObjectArray.Length)
            return;

        m_ControlledObjectArray[index].SetActive(m_LastResult[index]);
    }

    public void CloseAllTips()
    {
        for (int i = 0; i < m_ControlledObjectArray.Length; i++)
        {
            if (m_ControlledObjectArray[i] != null)
                m_ControlledObjectArray[i].SetActive(false);
        }
    }


}


