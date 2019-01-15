/********************************************************************
	created:	2015/03/02
	created:	2:3:2015   11:18
	filename: 	D:\Dream Heros\Assets\Program\Scripts\GameCore\Common\BattleAnger.cs
	file path:	D:\Dream Heros\Assets\Program\Scripts\GameCore\Common
	file base:	BattleAnger
	file ext:	cs
	author:		Zmy
	
	purpose:	战斗怒气结构
*********************************************************************/
using UnityEngine;
using System.Collections;
using DreamFaction.GameNetWork;
using DreamFaction.LogSystem;
using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork;

public class BattleAnger
{
    /// <summary>
    /// 当前怒气单位
    /// </summary>
    private int m_CurPowerPoint;
    /// <summary>
    /// 当前单位怒气值
    /// </summary>
    private int m_CurPointValue;
    /// <summary>
    /// 基础怒气值
    /// </summary>
    private int     m_BasePower;

    /// <summary>
    /// 所属阵营类型 ：EM_OBJECT_TYPE
    /// </summary>
    private EM_OBJECT_TYPE     m_GroupType;

    /// <summary>
    /// 构造对象必须指定所属阵营
    /// </summary>
    /// <param name="nType"></param>
    public BattleAnger(EM_OBJECT_TYPE nType)
    {
        m_BasePower = 0;
        m_GroupType = nType;
        m_CurPointValue = 0;
        m_CurPowerPoint = 0;
    }

    public void OnUpdatePowerValue(int nValue)
    {
        m_BasePower += nValue;

        m_BasePower = m_BasePower < 0 ? 0 : m_BasePower;

        if (m_GroupType == EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO)
        {
            int nMaxNum = ObjectSelf.GetInstance().GetMaxPowerValue();

            m_BasePower = m_BasePower > nMaxNum ? nMaxNum : m_BasePower;

           // LogManager.Log("*******我方增加怒气：【" + nValue + "】****** &&当前总怒气为:" + m_BasePower);
            //*************怒气显示方式调整Wyf*************************
            //获取最大怒气总单位数
            int maxPoint = DataTemplate.GetInstance().m_GameConfig.getMax_rage_point() / DataTemplate.GetInstance().m_GameConfig.getEach_rage_point();
            int curPoint = m_BasePower / DataTemplate.GetInstance().m_GameConfig.getEach_rage_point();
            //if (nValue < 0) //减少的单位数量
            //{
            //    int reducePoint = Mathf.Abs(nValue) / DataTemplate.GetInstance().m_GameConfig.getEach_rage_point();
            //    m_CurPowerPoint -= reducePoint;
            //}
            //else    //增加单位的数量
            //{
            //    int addPoint = nValue / DataTemplate.GetInstance().m_GameConfig.getEach_rage_point();
            //    m_CurPowerPoint += addPoint;
            //}
            if (curPoint > 0)
            {
                m_CurPowerPoint = curPoint;
            }
            else
            {
                m_CurPowerPoint = 0;
            }
            m_CurPowerPoint = m_CurPowerPoint > maxPoint - 1 ? maxPoint - 1 : m_CurPowerPoint;
            m_CurPowerPoint = m_CurPowerPoint < 0 ? 0 : m_CurPowerPoint;
            if (m_CurPowerPoint > 0)
            {
                m_CurPointValue = m_BasePower - ((m_CurPowerPoint) * DataTemplate.GetInstance().m_GameConfig.getEach_rage_point());
            }
            else
            {
                m_CurPointValue = m_BasePower;
            }
            //当达到最大怒气时 不再需要置空怒气
            if (m_CurPowerPoint == maxPoint - 1 && m_BasePower == ObjectSelf.GetInstance().GetMaxPowerValue())
            {
                m_CurPointValue = DataTemplate.GetInstance().m_GameConfig.getEach_rage_point();
            }
           // Debug.Log("当前怒气管数:" + m_CurPowerPoint + "进度:" + m_CurPointValue);
        }
        else
        {
            //LogManager.Log("@@@@@@@敌方增加怒气：【" + nValue + "】@@@@@@ &&当前总怒气为:" + m_BasePower);
        }
        // 更新怒气值ui显示
        GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_Anger_Update, m_GroupType);
    }

    public void OnUpdatePowerPercent(float nPercent)
    {
        float nValue = nPercent * m_BasePower;

        OnUpdatePowerValue((int)nValue);
    }

    public int GetPowerValue()
    {
        if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter && m_GroupType == EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO)
        {
            return m_BasePower + ObjectSelf.GetInstance().LimitFightMgr.OnHeroAngerUp(m_BasePower);
        }
        return m_BasePower;
    }
    /// <summary>
    /// 获取当前怒气 是第几单位的怒气 
    /// 不包括极限试炼百分比怒气提升
    /// </summary>
    /// <returns></returns>
    public int GetCurPowerPoint()
    {
        return m_CurPowerPoint;
    }
    /// <summary>
    /// 获取当前单位怒气的进度值
    /// 不包括极限试炼百分比怒气提升
    /// </summary>
    /// <returns></returns>
    public int GetCurPowerPointValue()
    {
        return m_CurPointValue;
    }
}
