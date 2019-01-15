using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace DreamFaction.GameSceneEditor
{
    /// <summary>
    /// 怪物点编辑器
    /// </summary>
    public class MonsterPoint : MonoBehaviour
    {
        /// <summary>
        /// 怪物数据
        /// </summary>
        public Monsterdata Monstergroupdata;
        /// <summary>
        /// 出场行为个数
        /// </summary>
        public int EntertypeListCount;
        /// <summary>
        /// 出场行为数组
        /// </summary>
        public List<MonsterPointData> MonsterPointdataList;
        /// <summary>
        /// 入场延迟时间
        /// </summary>
        public float EnterWaitTime;
        /// <summary>
        /// 自身坐标
        /// </summary>
        public Vector3 MyPos;
        /// <summary>
        /// 添加怪物数据
        /// </summary>
        /// <returns></returns>
        public Monsterdata Adddata()
        {
            Monstergroupdata = new Monsterdata();
            Monstergroupdata.MonsterPointdataList = new List<MonsterPointData>();
            Monstergroupdata.MonsterPointdataList = MonsterPointdataList;
            Monstergroupdata.EnterWaitTime = EnterWaitTime;
            Monstergroupdata.MyPos = this.transform.position;
            Monstergroupdata.MyAngle = this.transform.rotation;
            return Monstergroupdata;
        }
    }
    /// <summary>
    /// 怪物行为数据
    /// </summary>
    [System.Serializable]
    public class MonsterPointData
    {
        /// <summary>
        /// 出现方式
        /// </summary>
        public MonsterEnterType Entertype;
        /// <summary>
        /// 当前位移行为所需要的事件(编辑的时候参考用)
        /// </summary>
        public float RunAroundTime;
        /// <summary>
        /// 怪物行为循环模式
        /// </summary>
        public MonsterRunType MonsterRuntype;
        /// <summary>
        /// 怪物行为循环模式
        /// </summary>
        public MonsterActionType MonsterActiontype;
        /// <summary>
        /// 怪物目标类型
        /// </summary>
        public MonsterLookType MonsterLooktype;
        /// <summary>
        /// 行为持续时间
        /// </summary>
        public float ActionTime;
        /// <summary>
        /// 动作名称索引
        /// </summary>
        public string ActionName;
        /// <summary>
        /// 行为速度
        /// </summary>
        public float ActionSpeed;
        /// <summary>
        /// 怪物替补出场前怪物死亡数量
        /// </summary>
        public int BenchCount;
        /// <summary>
        /// 巡逻路径点数
        /// </summary>
        public int RunAroundCount;
        /// <summary>
        /// 巡逻路径点坐标组
        /// </summary>
        public List<Vector3> RunAroundPoints;
        /// <summary>
        /// 是否瞬间移动到寻路路径初始位置
        /// </summary>
        public bool IsMovetoFirstPoint;
        /// <summary>
        /// 特效名称
        /// </summary>
        public string Effname;
        /// <summary>
        ///特效位置
        /// </summary>
        public Vector3 Effpos;
        /// <summary>
        /// 特效持续时间
        /// </summary>
        public float EffTime;
        /// <summary>
        /// 音效名称
        /// </summary>
        public string Soundname;
    }
}

