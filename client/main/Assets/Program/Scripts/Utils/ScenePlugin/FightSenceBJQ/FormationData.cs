using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace DreamFaction.GameSceneEditor
{
    /// <summary>
    /// 阵型数据类
    /// </summary>
    public class FormationData:MonoBehaviour
    {
        /// <summary>
        /// 阵型GameObj
        /// </summary>
        public GameObject Formation;
        /// <summary>
        /// 阵型前后排距离
        /// </summary>
        public float FormationSpacing;
        /// <summary>
        /// 
        /// </summary>
        private List<GameObject> FormationList;//阵型子节点数组
        private void Update()
        {
            Formation = this.gameObject;
        }
    }
}

