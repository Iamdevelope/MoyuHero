using UnityEngine;
using System.Collections;
using GNET;
using System;
using System.Collections.Generic;

public class ActivityOverviewData 
{
    public int m_key;//活动id
    public int m_id; // 活动id
    public long m_time; // 最近一次时间
    public int m_todaynum; // 今日次数
    public int m_allnum; // 累计次数
    public int m_cangetnum; // 可以领取次数（）
    public int m_activitynum; // 活动计数
    public int m_allactivitynum; // 累计计数
    public int m_issee;//是否查看过 0没有 1有

    public void Copy(gactivity _gactivity)
    {
        this.m_id = _gactivity.id;
        this.m_time = _gactivity.time;
        this.m_todaynum = _gactivity.todaynum;
        this.m_allnum = _gactivity.allnum;
        this.m_cangetnum = _gactivity.cangetnum;
        this.m_activitynum = _gactivity.activitynum;
        this.m_allactivitynum = _gactivity.allactivitynum;
        this.m_issee = _gactivity.issee;
    }

}

public class ActivityOverviewMar
{

    public Dictionary<int, ActivityOverviewData> m_ActivityOverviewData = new Dictionary<int, ActivityOverviewData>();

    public void CopyInfo(Hashtable gameactivitymap)
    {
        m_ActivityOverviewData.Clear();

        foreach (DictionaryEntry kvp in gameactivitymap)
        {
            ActivityOverviewData _other = new ActivityOverviewData();
            _other.Copy(kvp.Value as gactivity);
            m_ActivityOverviewData.Add((int)kvp.Key, _other);
            //Debug.Log("kvp.Key" + kvp.Key +"----m_ActivityOverviewData.Count : " + m_ActivityOverviewData.Count);
        }
    }

    public void RefreshSingleGameAct(gactivity _gactivity)
    { 
        ActivityOverviewData _TempData = new ActivityOverviewData();
        _TempData.Copy(_gactivity);
        m_ActivityOverviewData[_TempData.m_id] = _TempData;
    }

    public bool RedPointShow()
    {
        int _TotalNum = 0;
        foreach (int k in DataTemplate.GetInstance().m_GameactivityTable.GetDataKeys())
        {
            _TotalNum++;
        }

        if (m_ActivityOverviewData.Values.Count <  _TotalNum)
        {
            return true;
        }
        if (m_ActivityOverviewData.Values.Count >= _TotalNum)
        {
            foreach (KeyValuePair<int, ActivityOverviewData> kvp in m_ActivityOverviewData)
            {
                 if(kvp.Value.m_issee == 0)
                 {
                     return true;
                 }
            }
        }
        return false;
    }
}


