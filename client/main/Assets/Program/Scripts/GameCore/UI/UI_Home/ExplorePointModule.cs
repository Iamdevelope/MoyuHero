using UnityEngine;
using System.Collections;

public class ExplorePointModule
{

    /// <summary>
    /// 获取行动力药水每日使用最大次数;
    /// </summary>
    /// <param name="itemT"></param>
    /// <param name="vipLv"></param>
    /// <returns></returns>
    public static int GetEPItemUseTimes(ItemTemplate itemT, int vipLv)
    {
        VipTemplate vipT = DataTemplate.GetInstance().GetVipTemplateById(vipLv);

        return itemT.getIfUse() + vipT.getMaxUseEpPotion();
    }

    /// <summary>
    /// 获得最大行动力上限;
    /// </summary>
    /// <param name="vipLv"></param>
    /// <returns></returns>
    public static int GetMaxExplorePoint(int vipLv)
    {
        int initCount = DataTemplate.GetInstance().GetGameConfig().getInitial_ep_upper_limit();
        VipTemplate vipT = DataTemplate.GetInstance().GetVipTemplateById(vipLv);

        return initCount + vipT.getExtraEp();
    }
}
