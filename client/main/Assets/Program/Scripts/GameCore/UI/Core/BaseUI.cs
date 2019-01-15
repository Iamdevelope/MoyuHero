using UnityEngine;
using System.Collections;
namespace DreamFaction.UI.Core
{
    //供跑马灯使用，根据不同的UI界面选择不同的优先级排序策略
    public enum UIMark
    { 
        DefaultMark,    //使用默认排序的界面
        HeroUpgrade,    //英雄进阶界面
        HeroRecruit,    //英雄招募界面
        RelicTreasure,  //遗迹宝藏
        Artifact,       //神器界面
        PlayerBag       //背包界面
    }
    /// <summary>
    /// UI基类，提供了UI的基础状态切换功能（进入动画，退出动画，等等）以及各个状态的切换回调！<br/>
    /// UIPrefab命名格式：UI_HeroInfo_x_y<br/>
    /// UI_HeroInfo:表示UI名称<br/>
    /// 第一个"_x":表示所处canvas图层，<br/>
    /// 注：游戏内一共有四个canvas，<br/>
    /// 底层canvas3，一级菜单层<br/>
    /// 中层cavas2，二级菜单层<br/>
    /// 顶层canvas1,常驻界面层（走马灯，滚动提示信息）<br/>
    /// 系统层canvas0,系统提示层（MessageBox等系统提示）<br/>
    /// 第二个"_y":表示当前canvas层中的显示优先级（渲染顺序）<br/>
    /// </summary>
    public class BaseUI : MonoBehaviour
    {
        /// <summary>
        /// UI状态的枚举
        /// </summary>
        public enum UIStateEnum
        {
            /// <summary>
            /// 起始状态
            /// </summary>
            Start,
            /// <summary>
            /// 播放进入动画状态
            /// </summary>
            PlayingEnterAnimation,
            /// <summary>
            /// 进入动画播放完毕状态
            /// </summary>
            PlayingEnterAnimationOver,
            /// <summary>
            /// 可用状态，进入动画播放完毕自动跳转至此
            /// </summary>
            StandBy,
            /// <summary>
            /// 播放退出动画状态
            /// </summary>
            PlayingExitAnimation,
            /// <summary>
            /// 退出动画播放完毕状态
            /// </summary>
            PlayingExitAnimationOver,
            /// <summary>
            /// 准备退出状态，退出动画播放完毕，可以退出！
            /// </summary>
            ReadyForClose, 
        }

        /// <summary>
        /// UI状态是否需要更新标记
        /// </summary>
        private bool m_curStateUpdateFlag;
        /// <summary>
        /// transform的引用，避免频繁调用transform
        /// </summary>
        protected Transform selfTransform;

        /// <summary>
        ///  UI当前状态
        /// </summary>
        private UIStateEnum m_curState = UIStateEnum.Start;

        private bool m_InitDone = false;

        /// <summary>
        ///  获取/设置 当前UI状态
        /// </summary>
        public UIStateEnum UIState
        {
            set
            {
                if (m_curState != value)
                {
                    m_curState = value;
                    m_curStateUpdateFlag = true;
                    UpdateUIState();
                }
            }
            get
            {
                return m_curState;
            }
        }

        public UIMark uiMark = UIMark.DefaultMark;

        public UIMark UIMark 
        {
            get { return uiMark; }
        }

        /// <summary>
        ///  初始化数据/状态
        /// </summary>
        void Awake()
        {
            if (!m_InitDone)
            {
                m_InitDone = true;

                selfTransform = transform;
                InitUIData();
                InitUIState();
            }
        }

        /// <summary>
        ///  初始化显示
        /// </summary>
        void Start()
        {
            if (!m_InitDone)
            {
                m_InitDone = true;

                selfTransform = transform;
                InitUIData();
                InitUIState();
            }

            InitUIView();
        }


        /// <summary>
        ///  更新操作
        /// </summary>
        void LateUpdate()
        {
            UpdateUIData();
            UpdateUIState(); 
            UpdateUIView();
        }


        /// <summary>
        /// 初始化UI数据,和脚本绑定关系，由子类实现 （Awake中调用，先 InitUIData() ; 之后 InitUIState(); ）
        /// </summary>
        public virtual void InitUIData()
        { 
            // TODO...
        }  

        /// <summary>
        ///  初始化UI状态 ，默认值是 UIStateEnum.PlayingEnterAnimation（播放动画状态）可以由子类重载后设置为UIStateEnum.Start状态进行必要的初始化操作
        /// </summary>
        public virtual void InitUIState()
        {
            // 默认初始状态为 PlayingEnterAnimation。
            UIState = UIStateEnum.PlayingEnterAnimation;
        }

        /// <summary>
        ///  初始化UI显示内容, 在Start(); 中调用
        /// </summary>
        public virtual void InitUIView()
        {
            // TODO...
        }

        /// <summary>
        /// 更新UI数据,在Update() 中调用，更新顺序为： 1:UpdateUIData();  2:UpdateUIState();  3:UpdateUIView();
        /// </summary>
        public virtual void UpdateUIData()
        {
            // TODO...
        }

        /// <summary>
        /// 更新UI状态 UI状态切换后，调用对应的状态回调接口。例如：OnPlayingEnterAnimation();
        /// </summary>
        public virtual void UpdateUIState()
        {
            if (!m_curStateUpdateFlag)
                return;
            m_curStateUpdateFlag = false;

            switch (m_curState)
            {
                case UIStateEnum.Start:
                    OnUIState_Start();
                    break;
                case UIStateEnum.PlayingEnterAnimation:
                    OnPlayingEnterAnimation();
                    break;
                case UIStateEnum.PlayingEnterAnimationOver:
                    OnPlayingEnterAnimationOver();
                    break;
                case UIStateEnum.StandBy:
                    OnStandBy();
                    break;
                case UIStateEnum.PlayingExitAnimation:
                    OnPlayingExitAnimation();
                    break;
                case UIStateEnum.PlayingExitAnimationOver:
                    OnPlayingExitAnimationOver();
                    break;
                case UIStateEnum.ReadyForClose:
                    OnReadyForClose();
                    break;
            }
        }

        /// <summary>
        /// 更新UI显示, 可以通过判断UI当前的状态，在这里进行对应状态的逐帧更新操作！
        /// </summary>
        public virtual void UpdateUIView()
        {
            // TODO....
        }


        /// <summary>
        /// UIStateEnum.Start 状态下的回调事件 ，由需要的UI子类实现 
        /// </summary>
        public virtual void OnUIState_Start()
        {
            // TODO...
        }

        /// <summary>
        /// UIStateEnum.PlayingEnterAnimation 状态下的回调事件 ，由需要的UI子类实现 
        /// </summary>
        public virtual void OnPlayingEnterAnimation() 
        {
            // TODO...
        }

        /// <summary>
        /// UIStateEnum.PlayingEnterAnimationOver 状态下的回调事件 ，由需要的UI子类实现 , 默认情况下，直接跳转到UIStateEnum.StandBy状态
        /// </summary>
        public virtual void OnPlayingEnterAnimationOver() 
        {
            UIState = UIStateEnum.StandBy;
        }

        /// <summary>
        /// UIStateEnum.StandBy( 状态下的回调事件 ，由需要的UI子类实现 
        /// </summary>
        public virtual void OnStandBy()
        {
            // TODO...
        }

        /// <summary>
        /// UIStateEnum.PlayExitAnimation 状态下的回调事件 ，由需要的UI子类实现 
        /// </summary>
        public virtual void OnPlayingExitAnimation() 
        {
            // TODO...
        }

        /// <summary>
        /// UIStateEnum.PlayingExitAnimationOver 状态下的回调事件 ，由需要的UI子类实现 , 默认情况下，直接跳转到UIStateEnum.ReadyForClose;状态
        /// </summary>
        public virtual void OnPlayingExitAnimationOver()
        {
            UIState = UIStateEnum.ReadyForClose;
        }

         /// <summary>
         /// UIStateEnum.PlayingExitAnimationOver 状态下的回调事件 ，由需要的UI子类实现 
         /// </summary>
        public virtual void OnReadyForClose() 
        {
            // 通知UIControler删除此UI
        }
    }
}