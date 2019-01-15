using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameNetWork.Data;
using DreamFaction.Interface;

public class HeroRuneModule
{
    /// <summary>
    /// 用b字典中的值减去a字典中的值;
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns>pair中a为起始值,b为增量值</returns>
    static Dictionary<int, Pair> GetDifference(Dictionary<int, int> a, Dictionary<int, int> b)
    {
        if (a == null || b == null)
            return null;

        if (a.Count != b.Count)
            return null;

        Dictionary<int, Pair> result = new Dictionary<int, Pair>();
        int key = -1;
        int val = -1;

        //这里可以肯定a和b所有的键值都是相同的;不用考虑a和b特有的键值添加到结果字典里;
        IEnumerator ie = a.Keys.GetEnumerator();
        while (ie.MoveNext())
        {
            key = (int)ie.Current;

            if (b.ContainsKey(key))
            {
                val = b[key] - a[key];
                if (val != 0) //不含无变化项;
                {
                    result.Add(key, new Pair(a[key], val));
                }
            }
            else
            {
                val = -a[key];
                if (val != 0) //不含无变化项;
                {
                    result.Add(key, new Pair(a[key], val));
                }
            }
        }

        return result;
    }

    /// <summary>
    /// 属性类型                    是否显示
    ///1 生命值                        1
    ///2 物攻值                        1
    ///3 物防值                        1    
    ///4 法攻值                        1
    ///5 法防值                        1
    ///6 命中值                        1   
    ///7 闪避值
    ///8 暴击值
    ///9 韧性值
    ///10 速度值
    ///11 恢复值
    ///12 命中率*1000
    ///13 闪避率*1000
    ///14 暴击率*1000
    ///15 韧性率*1000
    ///16 暴击伤害率*1000
    ///17 物伤加成率*1000
    ///18 物伤减免率*1000
    ///19 法伤加成率*1000
    ///20 法伤减免率*1000
    ///21 附加伤害值
    ///22 绝对减伤值
    ///23 普攻吸血*1000
    ///24 减少技能CD*1000
    ///25 初始加成怒气*1000
    ///26 攻击额外怒气*1000
    ///27 受击额外怒气*1000
    ///101 生命率*1000
    ///102 物攻率*1000
    ///103 物防率*1000
    ///104 法攻率*1000
    ///105 法防率*1000
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static Dictionary<int, Pair> CompareAttriDic(ObjectCard _ObjectCard, List<X_GUID> items = null)
    {
        Dictionary<int, int> curDic = new Dictionary<int, int>();
        Dictionary<int, int> afterDic = new Dictionary<int, int>();

        HeroData curHd = _ObjectCard.GetHeroData();

        ObjectCard beforC = new ObjectCard();
        HeroData beforCHd = beforC.GetHeroData();
        beforCHd.Copy(curHd);
        beforC.UpdateAttributeValue();

        curDic.Add(1, (int)beforC.GetMaxHP());                  //生命值;
        curDic.Add(2, beforC.GetPhysicalAttack());         //物理攻击力;
        curDic.Add(4, beforC.GetMagicAttack());               //法术攻击力
        curDic.Add(3, beforC.GetPhysicalDefence());            //物理防御力
        curDic.Add(5, beforC.GetMagicDefence());           //法术防御力;
        curDic.Add(6, beforC.GetHit());                    //命中;
        curDic.Add(7, beforC.GetDodge());                  //闪避;
        curDic.Add(8, beforC.GetCritical());               //暴击;
        curDic.Add(16, (int)beforC.GetCriticalRate());      //暴击率;
        curDic.Add(9, beforC.GetTenacity());               //韧性;
        curDic.Add(10, beforC.GetSpeed());                  //速度;
        curDic.Add(11, (int)beforC.GetHpRecover());         //生命恢复力;
        curDic.Add(25, (int)beforC.GetInitPowerAddition()); //初始怒气;
        curDic.Add(17, (int)beforC.GetPhysicalHurtAddPermil());  //物理伤害加成率;
        curDic.Add(19, (int)beforC.GetMagicHurtAddPermil());     //法伤加成率;
        curDic.Add(21, beforC.GetExtraHurt());              //伤害附加值;
        curDic.Add(18, (int)beforC.GetPhysicalHurtReducePermil());//物伤减免率;
        curDic.Add(20, (int)beforC.GetMagicHurtReducePermil());  //法伤减免率;
        curDic.Add(22, (int)beforC.GetReduceHurtPoint());        //伤害减免值;

        ObjectCard tmpCard = new ObjectCard();
        HeroData tmpHD = tmpCard.GetHeroData();
        tmpHD.Copy(curHd);
        // 全部符文（当前属性是装备符文后的-没有符文的）属性加成与符文更换（符文更改后的减去-符文更改前的）;
        bool isInverse = true;
        if (items != null)
        {
            isInverse = false;
            //如果选择的更换符文已经装备在其身上，那么相当于换位置或者卸载--所以需要去除重复值;
            List<X_GUID> tmpItems = new List<X_GUID>();
            foreach(X_GUID x in items)
            {
                X_GUID i = new X_GUID();
                i.Copy(x);
                tmpItems.Add(i);
            }

            int idx = -1;
            if (GetItemsListRepeatFirstIdx(tmpItems, ref idx))
            {
                tmpItems[idx].CleanUp();
            }

            tmpHD.SetEquipItems(tmpItems);
            tmpCard.UpdateItemEffectValue();
        }

        tmpCard.UpdateSpellEffectValue();
        tmpCard.UpdateTeamEffectValue();

        afterDic.Add(1, (int)tmpCard.GetMaxHP());                  //生命值;
        afterDic.Add(2, tmpCard.GetPhysicalAttack());         //物理攻击力;
        afterDic.Add(3, tmpCard.GetPhysicalDefence());            //物理防御力;
        afterDic.Add(4, tmpCard.GetMagicAttack());        //法术攻击力;
        afterDic.Add(5, tmpCard.GetMagicDefence());           //法术防御力;
        afterDic.Add(6, tmpCard.GetHit());                    //命中;
        afterDic.Add(7, tmpCard.GetDodge());                  //闪避;
        afterDic.Add(8, tmpCard.GetCritical());               //暴击;
        afterDic.Add(16, (int)tmpCard.GetCriticalRate());      //暴击率;
        afterDic.Add(9, tmpCard.GetTenacity());               //韧性;
        afterDic.Add(10, tmpCard.GetSpeed());                  //速度;
        afterDic.Add(11, (int)tmpCard.GetHpRecover());         //生命恢复力;
        afterDic.Add(25, (int)tmpCard.GetInitPowerAddition()); //初始怒气;
        afterDic.Add(17, (int)tmpCard.GetPhysicalHurtAddPermil());  //物理伤害加成率;
        afterDic.Add(19, (int)tmpCard.GetMagicHurtAddPermil());     //法伤加成率;
        afterDic.Add(21, tmpCard.GetExtraHurt());              //伤害附加值;
        afterDic.Add(18, (int)tmpCard.GetPhysicalHurtReducePermil());//物伤减免率;
        afterDic.Add(20, (int)tmpCard.GetMagicHurtReducePermil());  //法伤减免率;
        afterDic.Add(22, tmpCard.GetReduceHurtPoint());        //伤害减免值;

        if (isInverse)
            return GetDifference(afterDic, curDic);
        else
            return GetDifference(curDic, afterDic);
    }

    static bool GetItemsListRepeatFirstIdx(List<X_GUID> list, ref int idx)
    {
        List<X_GUID> temp = new List<X_GUID>();
        foreach (X_GUID x in list)
        {
            temp.Add(x);
        }

        for (int i = 0, j = temp.Count; i < j; i++)
        {
            if (!temp[i].IsValid())
                continue;

            int count = 0;

            for (int m = 0; m < j; m++)
            {
                if (temp[i].Equals(list[m]))
                    count++;

                if (count > 1)
                {
                    idx = i;
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// 判断列表中是否有重复的值,如果有返回第一个重复的索引值;
    /// 列表元素必须实现IEquatable接口;
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="idx"></param>
    /// <returns></returns>
    static bool GetListRepeatFirstIdx<T>(List<T> list, ref int idx) where T : IEquatable<T>, IValid
    {
        List<T> temp = new List<T>(list);

        for (int i = 0, j = temp.Count; i < j; i++)
        {
            if (!temp[i].IsValid())
                continue;

            int count = 0;
            
            for (int m = 0; m < j; m++)
            {
                if (temp[i].Equals(list[m]))
                    count++;

                if (count > 1)
                {
                    idx = i;
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// 策划说写死的，每隔3级激活一条附加属性3/6/9/12;
    /// </summary>
    /// <param name="strenghLv"></param>
    /// <returns></returns>
    public static int GetAppendAttriActiveNum(int strenghLv)
    {
        return Mathf.Min(GlobalMembers.MAX_RUNE_APPEND_ATTRIBUTE_COUNT, strenghLv / 3);
    }


}
