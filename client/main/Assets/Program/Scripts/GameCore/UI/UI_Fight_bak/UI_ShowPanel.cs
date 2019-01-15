using UnityEngine;
using System.Collections.Generic;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork;
using System.Collections;
using DreamFaction.UI.Core;

namespace DreamFaction.UI
{
    public enum HPNumberType
    {
        HP_NONE,
        HP_ENEMY_HURT,   // 敌人受伤
        HP_SELF_HURT,    // 己方受伤
        HP_HEAL,         // 加血
        HP_HEAVY,        // 暴击
        HP_MISS,         // 未命中
    }

    class UI_ShowPanel : BaseUI
    {
        /// <summary>
        /// HPNumberType - 数字类型
        /// FightNumberDict - 各种位数的数字
        /// </summary>
        public static UI_ShowPanel inst = null;
        private Object numberObj = null;
        private Dictionary<HPNumberType, FightNumberDict> mNumberDict = new Dictionary<HPNumberType, FightNumberDict>();
        public override void InitUIData()
        {
            inst = this;
            numberObj = UIResourceMgr.LoadPrefab("UI/Prefabs/UI_Fight/UI_FightNumber");

            //StartCoroutine(delay());
        }

        IEnumerator delay()
        {
            yield return new WaitForSeconds(0.3f);
            showNumber(Random.Range(10, 100000), (HPNumberType)Random.Range(1, 4), GameObject.Find("Cube").transform.position);
            StartCoroutine(delay());
        }

        public FightNumber CreateFightNumber(HPNumberType type)
        {
            GameObject obj = Instantiate(UI_ShowPanel.inst.numberObj) as GameObject;
            obj.transform.SetParent(transform, false);
            switch (type)
            {
                case HPNumberType.HP_ENEMY_HURT:
                    return obj.AddComponent<MonsterNumber>();
                case HPNumberType.HP_SELF_HURT:
                    return obj.AddComponent<HeroNumber>();
                case HPNumberType.HP_HEAL:
                    return obj.AddComponent<HealNumber>();
                case HPNumberType.HP_HEAVY:
                    return obj.AddComponent<HeavyNumber>();
                case HPNumberType.HP_MISS:
                    return obj.AddComponent<MissNumber>();
            }
            return null;
        }
        public void showNumber(int inum, HPNumberType type, Vector3 position)
        {
            string num = inum.ToString();
            int ibit = num.Length;

            FightNumberDict dict = null;
            if (!mNumberDict.ContainsKey(type))
            {
                dict = new FightNumberDict();
                mNumberDict.Add(type, dict);
            }
            dict = mNumberDict[type];

            FightNumber number = dict.GetNumber(ibit, type);
            if (number && !number.isShowing)
            {
                number.onStart(num, position);
            }
        }

    }

    class FightNumberDict
    {
        // 字数为-2是特殊字体
        private Dictionary<int, List<FightNumber>> mFightDict = new Dictionary<int, List<FightNumber>>();

        public FightNumberDict()
        {

        }
        public FightNumber GetNumber(int ibit, HPNumberType type)
        {
            FightNumber ret = null;

            List<FightNumber> list = null;
            if (!mFightDict.ContainsKey(ibit))
            {
                list = new List<FightNumber>();
                mFightDict.Add(ibit, list);
            }
            list = mFightDict[ibit];

            for (int index = 0; index < list.Count; index++)
            {
                if (!list[index].isShowing)
                {
                    ret = list[index];
                }
            }

            if (ret == null)
            {
                ret = UI_ShowPanel.inst.CreateFightNumber(type);
                list.Add(ret);
            }

            return ret;
        }
    }
}
