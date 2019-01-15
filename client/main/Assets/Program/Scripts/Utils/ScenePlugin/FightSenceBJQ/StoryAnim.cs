using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace DreamFaction.GameSceneEditor
{
    public class StoryAnim : MonoBehaviour
    {
        public StoryAnimGroup StoryAnimgroup;
        public StoryAnimGroup Adddata()
        {
            this.StoryAnimgroup.StoryAnimDateList = new List<StoryAnimdate>();
            for(int i=0;i<this.transform.childCount;i++)
            {
                this.StoryAnimgroup.StoryAnimDateList.Add(this.transform.GetChild(i).GetComponent<StoryAnimDate>().StoryAnimdata);
            }
            return this.StoryAnimgroup;
        }
    }
    [System.Serializable]
    public class StoryAnimGroup
    {
        /// <summary>
        /// 触发剧情ID索引
        /// </summary>
        public int ID;
        /// <summary>
        /// 触发剧情时候当前摄像机移动到剧情动画摄像机速度值
        /// </summary>
        public float CamToTagTime;
        /// <summary>
        /// 触发剧情时候摄像机移动到的目标坐标
        /// </summary>
        public Vector3 CamTagPos;
        /// <summary>
        /// 触发剧情时候摄像机移动到的目标角度
        /// </summary>
        public Quaternion CamTagAngle;
        public List<StoryAnimdate> StoryAnimDateList;
    }
}

