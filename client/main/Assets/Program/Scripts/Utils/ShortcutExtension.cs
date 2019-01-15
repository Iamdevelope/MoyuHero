using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

using DreamFaction.Utils;
using System.Text;

public static class ShortcutExtension
{
    /// <summary>
    /// 将字符串按照指定长度截取;
    /// </summary>
    /// <param name="str"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string[] SplitByLength(this string str, int length)
    {
        int totalCount = str.Length;
        int tmp = totalCount % GlobalMembers.MAX_RUNE_COUNT_PER_LINE;
        int lineNum = 0;

        if (tmp == 0)
            lineNum = totalCount / GlobalMembers.MAX_RUNE_COUNT_PER_LINE;
        else
            lineNum = totalCount / GlobalMembers.MAX_RUNE_COUNT_PER_LINE + 1;

        string[] result = new string[lineNum];

        int startIdx = -1;
        for (int i = 0; i < lineNum; i++)
        {
            startIdx = GlobalMembers.MAX_RUNE_COUNT_PER_LINE * i;
            if (i >= lineNum - 1)
            {
                result[i] = str.Substring(startIdx);
            }
            else
            {
                result[i] = str.Substring(startIdx, length);
            }
        }

        return result;
    }

    public static string WithColor(this object obj, string color)
    {
        if (obj != null)
        {
            return GameUtils.StringWithColor(obj.ToString(), color);
        }

        return string.Empty;
    }

    public static StringBuilder Clear(this StringBuilder sb)
    {
        return sb.Remove(0, sb.ToString().Length);
    }
}
