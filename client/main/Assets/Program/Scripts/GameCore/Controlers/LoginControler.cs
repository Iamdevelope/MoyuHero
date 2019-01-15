using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork;
using DreamFaction.UI;
using DreamFaction.GameAudio;

namespace DreamFaction.GameCore
{
    /// <summary>
    /// Login场景控制器，用来控制Login场景下的各个系统之间的协调与数据通信和处理！
    /// </summary>
    public class LoginControler : BaseControler
    {
        // 单例
        private static LoginControler _inst;

        public static bool StartShow = false;

        public AudioClip m_TitleClip = null;                    //登录和登录后的LOADING界面背景音乐

        // ============================= 公共属性(限制外部修改) ===================
        /// <summary>
        /// 单例
        /// </summary>
        public static LoginControler Inst
        {
            get
            {
                return _inst;
            }
        }

        // ============================= 继承接口 =================================
        /// <summary>
        /// 继承自基类BaseControler的初始化方法！
        /// </summary>
        protected override void InitData()
        {
            base.InitData();

            if (_inst == null)
            {
                _inst = this;
            }
            else
            {
                GameObject.Destroy(this);
            }
            if (MainGameControler.Inst.gameObject.GetComponent<ObjectSelf>() == null)
            {
                MainGameControler.Inst.gameObject.AddComponent<ObjectSelf>();
            }
            SceneManager.Inst.EndChangeScene(SceneEntry.Login.ToString());
            //// 注册事件监听器
            GameEventDispatcher.Inst.addEventListener(GameEventID.G_Clent_ResOK, OnClienResOK);
            // 开始检查客户端资源
            AssetManager.Inst.CheckAssetVer();
            //AssetLoader.Inst.ReadyloadRes(SceneEntry.Login.ToString(), true);

            AudioControler.Inst.PlayMusic(m_TitleClip);
        }
        /// <summary>
        /// 继承自基类BaseControler的销毁方法！
        /// </summary>
        protected override void DestroyData()
        {
            GameEventDispatcher.Inst.removeEventListener(GameEventID.G_Clent_ResOK, OnClienResOK);
            _inst = null;
        }
        // ============================= 公共接口 =================================


        // ============================= 私有函数 =================================


        // ============================== 事件回调函数 ============================
        // 客户端资源校验完毕
        private void OnClienResOK()
        {
            // LogManager.Log("LoginControler.OnClienResOK....");
            // SceneManager.Inst.StartChangeScene(SceneEntry.Home);
        }


        private void UI_StartShow()
        {
            //UI_LoginWin.m_StarShow = true;
            //UI_SelectLoginServer.m_StarShow = true;
            StartShow = true;
            if (AssetManager.ClientAllAseetsOK)
            {
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_OpenUI, UI_AnnounceMgr.UI_ResPath);
            }
        }
    }

}
