using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace DreamFaction.GameSceneEditor
{
    public class StoryAnimDate : MonoBehaviour
    {
        public StoryAnimdate StoryAnimdata;
    }
    [System.Serializable]
    public class StoryAnimdate
    {
        /// <summary>
        /// 事件模式类型
        /// </summary>
        public StoryAnimaHoldType StoryAnimaHoldtype;
        /// <summary>
        /// 当前事件串联事件ID
        /// </summary>
        public string CascadeEventID;
        /// <summary>
        /// 当前事件ID
        /// </summary>
        public int EventID;
        /// <summary>
        /// 事件时间点
        /// </summary>
        public float EventTime;
        /// <summary>
        ///事件功能类型
        /// </summary>
        public StoryAnimEventType StoryAnimEventtype;
        //##########################Obj数据所用参数#######################
        /// <summary>
        /// 创建Obj名字
        /// </summary>
        public string ObjName;
        /// <summary>
        /// 实例化OBJ坐标
        /// </summary>
        public Vector3 ObjPos;
        /// <summary>
        /// 实例化OBJ角度
        /// </summary>
        public Vector3 ObjAngle;
        /// <summary>
        /// 获取动态物体索引ID
        /// </summary>
        public int ObjDynamicID;
        /// <summary>
        /// 动态获取Obj名称(仅限测试用)
        /// </summary>
        public string ObjDynamicName;
        /// <summary>
        /// 实例化OBJ坐标(仅限测试用)
        /// </summary>
        public Vector3 ObjDynamicPos;
        /// <summary>
        /// 实例化OBJ角度(仅限测试用)
        /// </summary>
        public Vector3 ObjDynamicAngle;
        //##########################摄像机事件所用参数##########################
        /// <summary>
        /// 播放摄像机动画的名字
        /// </summary>
        public string CamAnimName;
        //#########################Obj行为参数#########################
        /// <summary>
        /// 怪物行为数据
        /// </summary>
        public Monsterdata MonsterData;
    }
}

