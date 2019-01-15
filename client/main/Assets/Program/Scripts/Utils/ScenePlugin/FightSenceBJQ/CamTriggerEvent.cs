using UnityEngine;
using System.Collections;

namespace DreamFaction.GameSceneEditor
{
    /// <summary>
    /// 摄像机行为编辑器
    /// </summary>
    public class CamTriggerEvent : MonoBehaviour
    {
        public Caminfo info;
    }
    /// <summary>
    /// 摄像机行为信息
    /// </summary>
    [System.Serializable]
    public class Caminfo
    {
        /// <summary>
        /// 摄像机开关
        /// </summary>
        public CamHoldType CamHoldtype;
        /// <summary>
        /// 摄像机场景移动模式
        /// </summary>
        public CamType CamType;
        /// <summary>
        /// 摄像机看向的目标点
        /// </summary>
        public CamTagType CamTagType;
        /// <summary>
        /// 摄像机朝向点移动方式
        /// </summary>
        public CamMoveType CamAnglesMovetype;
        /// <summary>
        /// 摄像机坐标移动方式
        /// </summary>
        public CamMoveType CamPosMovetype;
        /// <summary>
        /// 攝像看向方式
        /// </summary>
        public CamLookType Camlooktype;                                           
        /// <summary>
        /// 摄像机坐标移动速度
        /// </summary>
        public float CamPosMoveSpeed;
        /// <summary>
        /// 摄像机朝向点移动速度
        /// </summary>
        public float CamAnglesMoveSpeed;
        /// <summary>
        /// 摄像机坐标平滑阻尼时间
        /// </summary>
        public float CamPosMoveTime;
        /// <summary>
        /// 摄像机看向速度
        /// </summary>
        public float CamLookSpeed;                                                
        /// <summary>
        /// 摄像角度机平滑阻尼时间
        /// </summary>
        public float CamAnglesMoveTime;
        /// <summary>
        /// 摄像机移动到播放动画位置的时间
        /// </summary>
        public float CamAnimReadyTime;
        /// <summary>
        /// 摄像机动画ID
        /// </summary>
        public int CamAnimationID;
        /// <summary>
        /// 触发事件移动到的静态点坐标
        /// </summary>
        public Vector3 CamStaticPos;
        /// <summary>
        /// 触发事件移动到的静态点角度
        /// </summary>
        public Quaternion CamStaticAngles;
        /// <summary>
        /// 摄像机视角偏移值
        /// </summary>
        public Vector3 CamCenterDeviant = Vector3.zero;
        /// <summary>
        /// 摄像机位置偏移值
        /// </summary>
        public Vector3 CamPosDeviant = Vector3.zero; 
        // public Transform CamMovetoPos;
        /// <summary>
        /// 摄像机事件
        /// </summary>
        public string EventID = "ID";
    }
}

