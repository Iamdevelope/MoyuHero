using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace DreamFaction.GameSceneEditor
{
    /// <summary>
    /// 怪物组编辑器
    /// </summary>
    public class MonsterGroup : MonoBehaviour
    {
        /// <summary>
        /// 怪物组数据
        /// </summary>
        public MonstersGroupData MonsterGroupdata;
    }
    /// <summary>
    /// 怪物组数据组
    /// </summary>
    [System.Serializable]
    public class MonstersGroupData
    {
        /// <summary>
        /// 第几波怪物
        /// </summary>
        public int MonstersGroupID;
        /// <summary>
        /// 当前怪物组是否有支援怪物
        /// </summary>
        public bool isSupport;
        /// <summary>
        /// 怪物组角度
        /// </summary>
        public Vector3 MonsterGroupAngle;
        /// <summary>
        /// 单一怪物组数据
        /// </summary>
        public List<Monsterdata> MonsterGroupdata;
    }
    /// <summary>
    /// 怪物数据
    /// </summary>
    [System.Serializable]
    public class Monsterdata
    {
        /// <summary>
        /// 出场行为个数
        /// </summary>
        public int EntertypeListCount;
        /// <summary>
        /// 自身坐标
        /// </summary>
        public Vector3 MyPos;
        /// <summary>
        /// 自身角度
        /// </summary>
        public Quaternion MyAngle;
        /// <summary>
        /// 入场延迟时间
        /// </summary>
        public float EnterWaitTime;
        /// <summary>
        /// 出场行为数组
        /// </summary>
        public List<MonsterPointData> MonsterPointdataList;
    }
}

