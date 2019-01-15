using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.LogSystem;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork.Data;
using System.Text;
using DreamFaction.GameNetWork;
using UnityEngine.UI;
using System;
using System.Net;
using System.Net.NetworkInformation;
using DreamFaction.UI;
using DreamFaction.UI.Core;
using GNET;
using DreamFaction.Utils;

public class TimeUtils
{
    /// <summary>
    /// 以秒为单位比较current是否在start到end的时间段内，返回负数表示尚未到达(离start还有多少秒)，
    /// 0表示在其中，
    /// 正数表示已经过去（超过end多少秒）
    /// 精确到秒。
    /// </summary>
    /// <param name="current"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static int TimeDurationCompare(int current, int start, int end)
    {
        int _result1 = current - start;

        if (_result1 < 0)
            return _result1;
        else
        {
            int _result2 = current - end;
            return _result2 > 0 ? _result2 : 0;
        }
    }
    public static long TimeDurationCompareL(long current, long start, long end)
    {
        long _result1 = current - start;

        if (_result1 < 0)
            return _result1;
        else
        {
            long _result2 = current - end;
            return _result2 > 0 ? _result2 : 0;
        }
    }
    /// <summary>
    /// 判断time是否在startTime和endTime间隔内;
    /// 精确到小时和分钟;
    /// </summary>
    /// <param name="time"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <returns></returns>
    public static bool IsInHourTimeDuration(TimeInfoHM time, TimeInfoHM startTime, TimeInfoHM endTime)
    {
        return (time.CompareTo(startTime) >= 0) && (time.CompareTo(endTime) < 0);
    }


    public static TimeSpan GetTimeSpan(DateTime dt1, DateTime dt2)
    {
        return dt1 - dt2;
    }

    public static DateTime ConverMillionSecToDateTime(long millionSec, int timeZone)
    {
        DateTime dt = new DateTime(1970,1,1);
        dt = dt.AddMilliseconds(millionSec);
        dt = dt.AddHours(timeZone);

        return dt;
    }

    /// <summary>
    /// 向服务器同步客户端记录的时间;
    /// </summary>
    public static void SyncServerTime()
    {
        CGameTime cgt = new CGameTime();
        IOControler.GetInstance().SendProtocol(cgt);
    }

    /// <summary>
    /// 返回格式HH:MM:SS;
    /// </summary>
    /// <param name="minutes"></param>
    /// <returns></returns>
    public static string FormateTimeWithHMS(int minutes, string separator = ":")
    {
        int h = minutes / 60;
        int m = minutes - h * 60;
        int s = 0;

        string hs = GameUtils.FillWithChar(h, 2, '0');
        string ms = GameUtils.FillWithChar(m, 2, '0');
        string ss = GameUtils.FillWithChar(s, 2, '0');

        StringBuilder sb = new StringBuilder();
        sb.Append(hs);
        sb.Append(separator);
        sb.Append(ms);
        sb.Append(separator);
        sb.Append(ss);

        return sb.ToString();
    }

    public static string FormateTimeWithHMS(TimeSpan ts, string separator = ":")
    {

        string hs = GameUtils.FillWithChar(ts.Hours, 2, '0');
        string ms = GameUtils.FillWithChar(ts.Minutes, 2, '0');
        string ss = GameUtils.FillWithChar(ts.Seconds, 2, '0');

        StringBuilder sb = new StringBuilder();
        sb.Append(hs);
        sb.Append(separator);
        sb.Append(ms);
        sb.Append(separator);
        sb.Append(ss);

        return sb.ToString();
    }
}

public class TimeInfoHM : IComparable
{
    public int hour;
    public int minute;

    public int CompareTo(object obj)
    {
        if(obj is TimeInfoHM)
        {
            TimeInfoHM info = obj as TimeInfoHM;

            if (info == null)
            {
                return -1;
            }

            return CompareTo(info);
        }
        else
        {
            throw new Exception("必须是TimeInfoHMmm的对象类型");
        }
    }

    public int CompareTo(TimeInfoHM other)
    {
        return (hour * 60 + minute) - (other.hour * 60 + minute);
    }
}