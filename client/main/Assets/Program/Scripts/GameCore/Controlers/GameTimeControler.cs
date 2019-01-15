using UnityEngine;
using System.Collections;
using System;
using DreamFaction.GameEventSystem;
using DreamFaction.GameAudio;

namespace DreamFaction.GameCore
{
    public enum TimeScaleState
    {
        TimeScale_Pause = 0,//暂停状态 timeScale = 0
        TimeScale_Normal = 1,//正常状态 timeScale = 1
        TimeScale_Double = 2,//两倍数状态 timeScale = 1.5
        TimeScale_Triple = 3,//三倍数状态 timeScale = 2
        TimeScale_Custom,//自定义timeScale的值
    }

    /// <summary>
    /// 游戏中的TimeScale，保存有当前状态和TimeScale值、前一次的状态和TimeScale值
    /// 有两个public方法，设置状态和设置TimeScale值
    /// 设置状态时会按规则来设置TimeScale的值
    /// 设置TimeScale时会将状态自动切换到自定义状态
    /// 任何状态之间都可切换
    /// </summary>
    public class GameTimeControler : MonoBehaviour
    {
        public static GameTimeControler Inst = null;

        /************************************/

        //前一次的timeScale
        //[HideInInspector]
        public float preTimeScale = -1f;
        //当前的timeScale
        //[HideInInspector]
        public float curTimeScale = -1f;
        //前一次的timeScale状态
        //[HideInInspector]
        public TimeScaleState preTimeScaleState = TimeScaleState.TimeScale_Normal;
        //当前的timeScale状态
        //[HideInInspector]
        public TimeScaleState curTimeScaleState = TimeScaleState.TimeScale_Normal;

        [Range(0f, 5f)]
        public float tempTimeScale = 1f;

        /************************************/

        void Awake()
        {
            Inst = this;

            //GameEventDispatcher.Inst.addEventListener(GameEventID.UI_RefreshMonthCard, OnMonthCardInfoChange);
        }

        void Start()
        {
            curTimeScale = Time.timeScale;
        }

        /// <summary>
        /// 调用这个方法为timeScale设值时会自动把状态切到自定义状态
        /// </summary>
        /// <param name="value">timeScale值</param>
        public void SetValue(float value)
        {
            if (value == curTimeScale)
                return;

            //先为上一次的timeScale赋值
            preTimeScale = curTimeScale;
            //设置新的timeScale
            Time.timeScale = value;
            //为当前timeScale赋值
            curTimeScale = value;

            tempTimeScale = value;
            AudioControler.Inst.SoundPitch(value);
            SetState(TimeScaleState.TimeScale_Custom);
        }

        /// <summary>
        /// 游戏中的TimeScale，设置状态，可自动设置TimeScale的值
        /// </summary>
        /// <param name="state">状态</param>
        public void SetState(TimeScaleState state)
        {
            if (state == curTimeScaleState)
                return;

            preTimeScaleState = curTimeScaleState;

            switch (state)
            {
                case TimeScaleState.TimeScale_Normal:
                    SetValue(1f);
                    break;
                case TimeScaleState.TimeScale_Double:
                    SetValue(1.5f);
                    break;
                case TimeScaleState.TimeScale_Triple:
                    SetValue(2f);
                    break;
                case TimeScaleState.TimeScale_Pause:
                    SetValue(0f);
                    break;
                case TimeScaleState.TimeScale_Custom:
                    break;
                default:
                    break;
            }
            curTimeScaleState = state;
        }

        public void Update()
        {
            SetValue(tempTimeScale);
        }

        void OnDestroy()
        {
            //GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_RefreshMonthCard, OnMonthCardInfoChange);           
        }

        #region 当期游戏设定每次进入游戏后都是初始TimeScale_Normal，这个暂时不加;
        ///// <summary>
        ///// 月卡信息改变时间监听;
        ///// </summary>
        //void OnMonthCardInfoChange()
        //{
        //    int monthcardId = ExchangeModule.GetMaxMonthCard();
        //    int timeScale = ExchangeModule.GetTimeScaleInMonthCard(monthcardId);
            
        //    //如果超过最大加速;
        //    if ((int)curTimeScaleState > timeScale)
        //    {
        //        SetState((TimeScaleState)timeScale);
        //    }
        //}
        #endregion
    }
}


