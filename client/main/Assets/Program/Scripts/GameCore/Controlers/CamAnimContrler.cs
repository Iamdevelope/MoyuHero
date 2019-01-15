using UnityEngine;
using System.Collections;
namespace DreamFaction.GameSceneEditor
{
    /// <summary>
    /// 摄像机动画控制器
    /// </summary>
    public class CamAnimContrler : MonoBehaviour
    {
        private CameraPath CamPath;//摄像机数据组件
        private CameraPathAnimator CamPathAnim;//摄像机动画组件
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            CamPath = this.GetComponent<CameraPath>();
            CamPathAnim = this.GetComponent<CameraPathAnimator>();
        }
        //动画开始
        private void AnimPlay()
        {
            CamPathAnim.Play();
        }
        //动画暂停
        private void AnimPause()
        {
            CamPathAnim.Pause();
        }
        //改变速度值
        private void ChangeSpeed(float Speed)
        {
            CamPathAnim.pathSpeed = Speed;
        }
        //添加模型
        private void AddModel()
        {
            //....
        }
        //添加特效
        private void AddEff()
        {
            //...
        }
    }
}

