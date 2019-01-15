using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VIPModule
{
    /// <summary>
    /// 获取可以探险加速的VIP最低等级;
    /// </summary>
    /// <returns></returns>
    public static int GetExploreAccelerateVipLv()
    {
        List<int> keys = DataTemplate.GetInstance().m_VipTable.GetDataKeys();

        for (int i = 0; i < keys.Count; i++ )
        {
            int k = keys[i];

            VipTemplate vt = DataTemplate.GetInstance().GetVipTemplateById(k);

            if (vt.getIfCanAccelerate() != 0)
            {
                return k;
            }
        }

        return -1;
    }

    /// <summary>
    /// 获取购买扫荡次数的最低VIP等级;
    /// </summary>
    /// <returns></returns>
    public static int GetStageMopupVipLv()
    {
        List<int> keys = DataTemplate.GetInstance().m_VipTable.GetDataKeys();

        for (int i = 0; i < keys.Count; i++)
        {
            int k = keys[i];

            VipTemplate vt = DataTemplate.GetInstance().GetVipTemplateById(k);

            if (vt.getIfCanRapidClear() != 0)
            {
                return k;
            }
        }

        return -1;
    }

    /// <summary>
    /// 获得每日可购买关卡的扫荡次数;
    /// </summary>
    /// <param name="vipLv"></param>
    /// <returns></returns>
    public static int GetBuyStageMopupTimes(int vipLv)
    {
        VipTemplate vipT = DataTemplate.GetInstance().GetVipTemplateById(vipLv);
        
        if (vipT == null)
        {
            return -1;
        }

        return vipT.getRapidClearBuyTimes();
    }

    /// <summary>
    /// 获得每天可以扫荡的关卡次数;
    /// </summary>
    /// <param name="vipLv"></param>
    /// <returns></returns>
    public static int GetStageMopupInitTimes(int vipLv)
    {
        VipTemplate vipT = DataTemplate.GetInstance().GetVipTemplateById(vipLv);

        if (vipT == null)
        {
            return -1;
        }

        return vipT.getRapidClearNums();
    }
}
