using UnityEngine;
using System.Collections;
namespace DreamFaction.GameSceneEditor
{
    /// <summary>
    /// 场景英雄路径编辑数据
    /// </summary>
    public class HeroPathData : MonoBehaviour
    {
        /// <summary>
        /// 保存场景名称
        /// </summary>
        public string SceneName;
        /// <summary>
        /// 战斗中心点
        /// </summary>
        public GameObject FightCenter;
        /// <summary>
        /// 英雄中心点
        /// </summary>
        public GameObject HerosCenter;
        /// <summary>
        /// 进场前准备时间
        /// </summary>
        public float Waittime;
        /// <summary>
        /// 英雄阵型移动速度
        /// </summary>
        public float MoveSpeed;
        /// <summary>
        /// 前排英雄死后，阵型移动的相对距离;
        /// 一方的前排没了，对方的阵容整体向前移动该段距离;
        /// </summary>
        public float MoveDistance;
    }
}

