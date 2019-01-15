using UnityEngine;
using System;
using System.IO;
using DreamFaction.LogSystem;

namespace DreamFaction.GameCore
{
    /// <summary>
    /// 这个类用来控制App相关的所有对外属性，比如版本号，是否是测试版本，App终止时间，App资源加载路径等等。<br/>
    /// Unity的Application类所有操作，全部在这里进行封装！
    /// </summary>
    public class AppManager : BaseControler
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static AppManager Inst; 
        /// <summary>
        /// 当前系统可读写路径,根据当前平台以及运行环境不同而不同！
        /// </summary>
        [HideInInspector]
        public string readAndWritePath; 
        /// <summary>
        /// 当前系统的只读路径,根据当前平台以及运行环境不同而不同！
        /// </summary>
        [HideInInspector]
        public string readOnlyPath; 
        /// <summary>
        /// 标示是否是本地开发版本，本地开发资源全部从本地Streaming目录下获取，否则全部从服务器下载！
        /// </summary>
        public bool IsLocalVersion;

        private string m_GameLanguage;// 当前使用的游戏语言，不同于系统语言！
        private string m_deviceUniqueIdentifier; // 当前设备唯一标示。
        // ================================================================== 
        // 初始化，更新，删除
        // ==================================================================
        /// <summary>
        /// 游戏初始化操作 -- 其他系统的初始化参见脚本执行顺序（MonoManager）
        /// </summary>
        protected override void InitData()
        {
            base.InitData();
            Inst = this;// 初始化单例
            // 初始化日志系统！0.5秒刷新一次磁盘日志！LogManager的初始化工作放到AppManager中做，AppManager是第一个执行的脚本，参见游戏脚本执行顺序（Mono）设置
            LogManager.Init(true, true, -1.0f);
            // 初始化可读写路径，经过测试，ios，android，win，mac 均可用！
            readAndWritePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar;
            readOnlyPath = Application.streamingAssetsPath + Path.AltDirectorySeparatorChar;
            // 获取设备唯一标示
            m_deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
        }
        /// <summary>
        /// 更新数据操作
        /// </summary>
        protected override void UpdateData()
        {
            base.UpdateData();
            // 刷新磁盘日志缓冲队列,LogManager的初始化工作放到AppManager中做，AppManager是第一个执行的脚本，参见游戏脚本执行顺序（Mono）设置
            LogManager.Update();
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        protected override void DestroyData()
        {
            //退出游戏后，清空新关卡;
            ConfigsManager.Inst.updateNewStages(null);

            Inst = null;
            LogManager.SetPushTime(-1);//退出游戏中，log日志改为实时刷新！避免log日志打印不全！
        }

        // =================================================================
        // ============ Public 对外公共接口
        // =================================================================
        /// <summary>
        /// 游戏当前使用的语言，不同于系统语言，只是游戏语言！
        /// </summary>
        public string GameLanguage
        {
            get
            {
                return m_GameLanguage;
            }
            set
            {
                ConfigsManager.Inst.SetClientConfig(ClientConfigs.GameLanguage,value);
                m_GameLanguage = value;
            }
        }
        /// <summary>
        /// 获取当前设备的唯一标示符
        /// </summary>
        public string DeviceUniqueIdentifier
        {
            get
            {
                return m_deviceUniqueIdentifier;
            }
            set
            {
                m_deviceUniqueIdentifier = value;
            }
        }
    }

}
