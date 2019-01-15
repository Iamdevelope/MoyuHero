using UnityEngine;
using System.Collections;
namespace DreamFaction.GameSceneEditor
{ 
    public class AnimationEditorContrler : MonoBehaviour
    {
        /// <summary>
        /// 英雄ID
        /// </summary>
        public int HeroID;
        /// <summary>
        /// 读取动作名称
        /// </summary>
        public string LoadAnimName;
        /// <summary>
        /// 该动作函数数据
        /// </summary>
        public AnimationFuntionData[] AnimationFuntiondata;
    }
    [System.Serializable]
    public class AnimationFuntionData
    {
        /// <summary>
        /// 函数时间点
        /// </summary>
        public float FuntionTime;
        /// <summary>
        /// 函数名称
        /// </summary>
        public string FuntionName;
        /// <summary>
        /// 函数参数名称
        /// </summary>
        public string FuntionParameter;
    }
}

