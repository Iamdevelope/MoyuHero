using UnityEngine;
using System.Collections;
using DreamFaction.GameEventSystem;
using DreamFaction.LogSystem;

namespace DreamFaction.GameCore
{
    public class SceneManager : BaseControler
    {
        // 单例
        private static SceneManager _inst;
        // 当前游戏场景
        private string m_CurScene;
        // 下一个要进入的场景
        private string m_NextScene;
        //上一个场景，即A进入下一个场景C，需要经过loading场景B。此对象只标示A场景而非B场景！
        private string m_LastScene;
        // 下一个游戏场景，只有需要loading界面的场景才会使用这个变量。这个变量只在loading界面使用,Ctrl + Shif + F 全局搜索公共属性 查看使用的地方！
        private string m_NextloadScene;
        // 场景跳转需要经过Loading界面的管理列表
        private Hashtable m_needLoadingTable;
        // ============================= 公共属性(限制外部修改) ===================
        public static SceneManager Inst
        {
            get
            {
                return _inst;
            }
        }

        public string CurScene
        {
            get
            {
                return m_CurScene;
            }
        }

        public string NextScene
        {
            get
            {
                return m_NextScene;
            }
        }

        public string NextloadScene
        {
            get
            {
                return m_NextloadScene;
            }
        }

        // ============================= 继承接口 =================================
        protected override void InitData ()
        {
            if ( _inst == null )
            {
                _inst = this;
            }
            else
            {
                GameObject.Destroy ( this );
            }
            m_LastScene = SceneEntry.Init.ToString ();
            m_CurScene = m_NextScene = m_NextloadScene = string.Empty;

            m_needLoadingTable = new Hashtable ();

            m_needLoadingTable.Add ( SceneEntry.Home.ToString (), true );
            m_needLoadingTable.Add ( SceneEntry.Login.ToString (), true );
            m_needLoadingTable.Add ( SceneEntry.SkillShow.ToString (), true );

        }

        protected override void DestroyData ()
        {
            GameEventDispatcher.Inst.removeEventListener ( GameEventID.G_Scene_ResOK, OnSceneResOK );

            _inst = null;
        }
        // ============================= 公共接口 =================================
        public void StartChangeScene ( string next )
        {
            if ( string.IsNullOrEmpty ( next ) )
            {
                LogManager.LogWarning ( "ChangeGameState -> GameState.Null;" );
                return;
            }

            if ( m_NextScene.Equals ( next ) == false )
            {
                m_NextScene = next;

                // 判断是否需要loading界面
                if ( m_needLoadingTable.ContainsKey ( m_NextScene ) && m_LastScene.Equals ( SceneEntry.Init.ToString () ) == false )
                {
                    m_NextScene = SceneEntry.Loading.ToString ();
                    m_NextloadScene = next;
                    Application.LoadLevel ( m_NextScene.ToString () );
                }
                else
                {
                    //在不需要loading界面的场景，先监听一个事件 然后准备目标场景的资源，最后就绪后再跳转场景
                    //GameEventDispatcher.Inst.addEventListener ( GameEventID.G_Scene_ResOK, OnSceneResOK );
                    //AssetLoader.Inst.ReadyloadRes ( m_NextScene.ToString (), true );
                    Application.LoadLevel(m_NextScene.ToString());
                }
            }
            else
            {
                LogManager.LogWarning ( "游戏场景重复跳转到同一个场景: " + m_NextScene );
            }
        }
        protected override void UpdateData ()
        {
            //优化标记：每帧产生28B的GC Alloc [7/29/2015 Zmy]
            if (Application.loadedLevelName.Contains("Battle") && m_CurScene.Equals(SceneEntry.Fight.ToString()) == false)
            {
                GameObject temp = new GameObject ( "FightContrler" );
                temp.AddComponent<FightControler> ();
            }
        }
        public void EndChangeScene ( string overScene )
        {
            //记录除loading场景的上一个场景
            if ( m_CurScene.Equals ( SceneEntry.Loading.ToString () ) == false )
            {
                m_LastScene = m_CurScene;
            }

            m_CurScene = overScene;
            m_NextScene = string.Empty;

            GameEventDispatcher.Inst.dispatchEvent ( GameEventID.G_ChangeScene_Over );
            if ( SocketManager.GetState () != SocketState.state_connectok )
            {
                // TODO...
            }

        }
        // ============================= 私有函数 =================================

        // ============================== 事件回调函数 ============================
        private void OnSceneResOK ()
        {
            //LogManager.Log("OnSceneResOK....");
            if ( string.IsNullOrEmpty ( m_NextScene ) == false && m_CurScene.Equals ( SceneEntry.Loading.ToString () ) == false )
            {
                Application.LoadLevel ( m_NextScene.ToString () );
            }
        }

        public void EnterBattleScene ( int nSceneID )
        {
            StageTemplate pRow = ( StageTemplate ) DataTemplate.GetInstance ().m_StageTable.getTableData ( nSceneID );
            if ( m_needLoadingTable.ContainsKey ( pRow.m_stagemap ) == false )
            {
                m_needLoadingTable.Add ( pRow.m_stagemap, true );
            }

            StartChangeScene ( pRow.m_stagemap );
        }
    }
}
