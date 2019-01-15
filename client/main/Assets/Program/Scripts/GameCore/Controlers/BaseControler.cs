using UnityEngine;
using System.Collections;
using DreamFaction.LogSystem;

namespace DreamFaction.GameCore
{
    /// <summary>
    /// 此类是所有Controler的基类,提供初始化，更新，清理等基础操作！
    /// </summary>
    public class BaseControler : MonoBehaviour
    {
        void Awake()
        {
            LogManager.StartTimeLog(LogManager.LogStep.Init,this.GetType().Name);
            InitData();
            InitState();
            LogManager.EndTimeLog(LogManager.LogStep.Init,this.GetType().Name);
        }

        void Start()
        {
            InitView();
        }

        void LateUpdate()
        {
            UpdateData();
            UpdateState();
            UpdateView();
        }

        void OnDestroy()
        {
            LogManager.StartTimeLog(LogManager.LogStep.Destroy,this.GetType().Name);
            DestroyView();
            DestroyState();
            DestroyData();
            LogManager.EndTimeLog(LogManager.LogStep.Destroy,this.GetType().Name);
        }

        // ======================= 初始化操作 =========================
        // 1: 初始化数据
        protected virtual void InitData()
        {
            //LogManager.Log("BaseControler.InitData()");
        }
        // 2: 初始化状态机
        protected virtual void InitState()
        {
            //LogManager.Log("BaseControler.InitState()");
        }
        // 3: 初始化显示对象
        protected virtual void InitView()
        {
            //LogManager.Log("BaseControler.InitView()");
        }

        // ======================= 更新操作 ==========================
        // 1: 更新数据
        protected virtual void UpdateData()
        {
            //LogManager.Log("BaseControler.UpdateData()"); 
        }
        // 2: 更新状态机
        protected virtual void UpdateState()
        {
            //LogManager.Log("BaseControler.UpdateState()"); 
        }
        // 3: 更新显示对象
        protected virtual void UpdateView()
        {
            //LogManager.Log("BaseControler.UpdateView()");
        }

        // ======================= 删除操作 ==========================
        // 1: 删除显示对象
        protected virtual void DestroyView()
        {
            //LogManager.Log("BaseControler.DestroyView()");
        }
        // 2: 删除状态机
        protected virtual void DestroyState()
        {
            //LogManager.Log("BaseControler.DestroyState()"); 
        }
        // 3: 删除数据
        protected virtual void DestroyData()
        {
            //LogManager.Log("BaseControler.DestroyData()"); 
        }
    }

}
