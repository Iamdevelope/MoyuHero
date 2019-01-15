using UnityEngine;
using System;
using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork;

namespace DreamFaction.GameCore
{
    /// <summary>
    /// 游戏的主逻辑控制器
    /// </summary>
    public class MainGameControler : BaseControler
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static MainGameControler Inst;
        public string mlient_mac;   // 服务器下发的
        public long   roleid;       // 角色ID
        public string mToken;       // 平台识别码
        public string mPlatform;    // 平台类型
        public string mPlatId;      // 平台ID          

        
        // ==================================================================
        // ============ 初始化，更新，删除
        // ==================================================================
        /// <summary>
        /// 游戏初始化操作 -- 其他系统的初始化参见脚本执行顺序（MonoManager）
        /// </summary>
        protected override void InitData()
        {
            Inst = this;
            // 不销毁
            GameObject.DontDestroyOnLoad(this);
            this.gameObject.AddComponent<ObjectSelf>();
            this.gameObject.AddComponent<InterfaceControler>();
            // 切换场景到Login
            if (!IsInvoking("OnChangeScene"))
                Invoke("OnChangeScene", 2f);

        }

        private void OnChangeScene()
        {
            SceneManager.Inst.StartChangeScene(SceneEntry.Login.ToString());
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        protected override void DestroyData()
        {
            Inst = null;
        }
        /// <summary>
        /// 更新数据操作
        /// </summary>
        protected override void UpdateData()
        {
            base.UpdateData();
        }
        // ==================================================================
        // ============ 事件回调函数 
        // ==================================================================


    }
}
