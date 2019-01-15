////////////////////////////////////////////////////////////////////////////////
//  
// @module 事件系统
// @author 金奇
// 
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System;
using System.Collections.Generic;
using DreamFaction.LogSystem;

namespace DreamFaction.GameEventSystem
{
    /// <summary>
    /// 事件分发器，全局静态类！用来添加监听器，并派发事件！
    /// </summary>
    public class GameEventDispatcher : IDispatcher
    {
        /// <summary>
        /// 单例
        /// </summary>
        private static GameEventDispatcher _inst;

        // 事件对象，dispatch的时候创建使用！
        private static GameEvent _cEvent;
        /// <summary>
        /// 不带参数，无返回值的回调函数队列
        /// </summary>
        private Dictionary<int, List<EventHandlerFunction>> listners = new Dictionary<int, List<EventHandlerFunction>>();
        /// <summary>
        /// 带有参数，无返回值的回调函数队列
        /// </summary>
        private Dictionary<int, List<DataEventHandlerFunction>> dataListners = new Dictionary<int, List<DataEventHandlerFunction>>();

        // 临时队列,避免重复new操作
        private static List<EventHandlerFunction> t_EventHandlerList;
        private static List<DataEventHandlerFunction> t_DateEventHandlerList;


        public static GameEventDispatcher Inst
        {
            get
            {
                if(_inst == null)
                {
                    _inst = new GameEventDispatcher();
                     _cEvent = new GameEvent();
                     t_EventHandlerList = new List<EventHandlerFunction>();
                     t_DateEventHandlerList = new List<DataEventHandlerFunction>();
                }
                return _inst;
            }
        }

        //--------------------------------------
        // 添加事件监听器：根据回调函数的类型分为带参数的回调函数（DataEventHandlerFunction）和不带参数的回调函数（EventHandlerFunction）
        //--------------------------------------
        /// <summary>
        /// 添加 事件监听器（回调函数不带参数，无返回值）
        /// </summary>
        /// <param name="eventID">事件ID</param>
        /// <param name="handler">监听器为无参数的函数</param>
        /// <remarks>                  
        ///    事件ID定义参见<see  cref="DreamFaction.EventSystem.GameEventID"/><br/>   
        ///    事件定义参见<see  cref="DreamFaction.EventSystem.GameEvent"/><br/>  
        /// </remarks> 
        /// <example>
        /// 示例代码：
        /// <code>
        ///class EventTestClass
        ///{
        ///    public void Init()
        ///    {
        ///        GameEventDispatcher.Inst.addEventListener(GameEventID.G_Clent_ResOK, OnClienResOK);//无参数
        ///        GameEventDispatcher.Inst.addEventListener(GameEventID.G_Clent_ResOK, OnClienResOK_Data);
        ///        GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_Clent_ResOK);
        ///        object obj = new object();
        ///        obj = 10;
        ///        GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_Clent_ResOK, obj);
        ///        obj = "Test string";
        ///        GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_Clent_ResOK, obj);
        ///    }
        ///
        ///    public void OnClienResOK()
        ///    {
        ///        Debug.Log("OnClienResOK..."); //无参数
        ///    }
        ///
        ///    public void OnClienResOK_Data(GameEvent e)
        ///    {
        ///        Debug.Log("OnClienResOK : " + e.data); // 第一次 10  , 第二次 Test String
        ///    }
        ///}
        ///}
        /// </code>
        /// </example>
        public void addEventListener(GameEventID eventID, EventHandlerFunction handler)
        {
            int e = (int)eventID;
            if (listners.ContainsKey(e))
            {
                listners[e].Add(handler);
            }
            else
            {
                List<EventHandlerFunction> handlers = new List<EventHandlerFunction>();
                handlers.Add(handler);
                listners.Add(e, handlers);
            }
        }

        /// <summary>
        /// 添加 事件监听器（回调函数带有参数，无返回值）
        /// </summary>
        /// <param name="eventID">事件ID</param>
        /// <param name="handler">监听器为有参数（GameEvent）的函数</param>
        /// <remarks>                  
        ///    事件ID定义参见<see  cref="DreamFaction.EventSystem.GameEventID"/><br/>   
        ///    事件定义参见<see  cref="DreamFaction.EventSystem.GameEvent"/><br/>  
        /// </remarks> 
        /// <example>
        /// 示例代码：
        /// <code>
        ///class EventTestClass
        ///{
        ///    public void Init()
        ///    {
        ///        GameEventDispatcher.Inst.addEventListener(GameEventID.G_Clent_ResOK, OnClienResOK);//无参数
        ///        GameEventDispatcher.Inst.addEventListener(GameEventID.G_Clent_ResOK, OnClienResOK_Data);
        ///        GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_Clent_ResOK);
        ///        object obj = new object();
        ///        obj = 10;
        ///        GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_Clent_ResOK, obj);
        ///        obj = "Test string";
        ///        GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_Clent_ResOK, obj);
        ///    }
        ///
        ///    public void OnClienResOK()
        ///    {
        ///        Debug.Log("OnClienResOK..."); //无参数
        ///    }
        ///
        ///    public void OnClienResOK_Data(GameEvent e)
        ///    {
        ///        Debug.Log("OnClienResOK : " + e.data); // 第一次 10  , 第二次 Test String
        ///    }
        ///}
        ///}
        /// </code>
        /// </example>
        public void addEventListener(GameEventID eventID, DataEventHandlerFunction handler)
        {
            int e = (int)eventID;
            if (dataListners.ContainsKey(e))
            {
                dataListners[e].Add(handler);
            }
            else
            {
                List<DataEventHandlerFunction> handlers = new List<DataEventHandlerFunction>();
                handlers.Add(handler);
                dataListners.Add(e, handlers);
            }
        }

        //--------------------------------------
        // 删除事件监听器
        //--------------------------------------
        /// <summary>
        /// 删除无参数，无返回值的事件监听器
        /// </summary>
        /// <param name="eventID">事件ID</param>
        /// <param name="handler">监听器为无参数的函数</param>
        public void removeEventListener(GameEventID eventID, EventHandlerFunction handler)
        {
            int e = (int)eventID;
            if (listners.ContainsKey(e))
            {
                List<EventHandlerFunction> handlers = listners[e];
                handlers.Remove(handler);
                handler = null;

                if (handlers.Count == 0)
                {
                    listners.Remove(e);
                }
            }
        }
        /// <summary>
        /// 删除有参数，无返回值的事件监听器
        /// </summary>
        /// <param name="eventID">事件ID</param>
        /// <param name="handler">监听器为有参数的函数</param>
        public void removeEventListener(GameEventID eventID, DataEventHandlerFunction handler)
        {
            int e = (int)eventID;
            if (dataListners.ContainsKey(e))
            {
                List<DataEventHandlerFunction> handlers = dataListners[e];
                handlers.Remove(handler);

                if (handlers.Count == 0)
                {
                    dataListners.Remove(e);
                }
            }
        }

        /// <summary>
        /// 删除所有添加的事件监听器
        /// </summary>
        public void clearEvents()
        {
            listners.Clear();
            dataListners.Clear();
        }

        /// <summary>
        /// 删除某个事件ID的所有监听器
        /// </summary>
        /// <param name="eventID">需要删除监听器的事件ID</param>
        public void clearEvent(GameEventID eventID)
        {
            int e = (int)eventID;
            if (listners.ContainsKey(e))
            {
                List<EventHandlerFunction> handlers = listners[e];
                handlers.Clear();
                listners.Remove(e);
            }
            if (dataListners.ContainsKey(e))
            {
                List<DataEventHandlerFunction> handlers = dataListners[e];
                handlers.Clear();
                dataListners.Remove(e);
            }
        }

        //--------------------------------------
        // 分发事件
        //--------------------------------------
        /// <summary>
        /// 分发无附加数据参数，无返回值的事件
        /// </summary>
        /// <param name="eventID">事件ID</param>
        public void dispatchEvent(GameEventID eventID)
        {
            dispatch((int)eventID, null);
        }
        /// <summary>
        /// 分发有附加数据参数，无返回值的事件
        /// </summary>
        /// <param name="eventID">事件ID</param>
        /// <param name="data">事件附加的数据</param>
        public void dispatchEvent(GameEventID eventID, object data )
        {
            dispatch((int)eventID, data);
        }

        ///// <summary>
        ///// Destroy时回调，清理监听的事件
        ///// </summary>
        //public virtual void OnDestroy()
        //{
        //    clearEvents();
        //    inst = null;
        //}

        //--------------------------------------
        // 具体执行分发操作
        //--------------------------------------
        private void dispatch(int eventID, object data)
        {
            LogManager.LogToFile("事件系统 dispatch：" + (GameEventID)eventID + ", Data = " + data);
            //Debug.Log("dispatch_____" + (GameEventID)eventID + ", Data = " + data + "___" +Time.timeSinceLevelLoad);
            // 创建/重置 事件内的数据,避免频繁new操作
            GameEvent _cEvent = new GameEvent(eventID, data, this);//待处理~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            //_cEvent.ResetEvent(eventID, data, this);
            lock (dataListners)
            {
                // 回调不带参数的监听器
                if (dataListners.ContainsKey(eventID))
                {
                    // 这里之所以需要cloen操作，是为了避免事件调用时，事件队列长度发生变化！
                    List<DataEventHandlerFunction> handlers = cloenArray(dataListners[eventID]);
                    int len = handlers.Count;
                    for (int i = 0; i < len; i++)
                    {
                        if (_cEvent.canBeDisptached(handlers[i].Target))
                        {
                            handlers[i](_cEvent);
                        }
                    }
                }
            }

            lock (listners)
            {
                // 回调带参数的监听器
                if (listners.ContainsKey(eventID))
                {
                    // 这里之所以需要cloen操作，是为了避免事件调用时，事件队列长度发生变化！
                    List<EventHandlerFunction> handlers = cloenArray(listners[eventID]);
                    int len = handlers.Count;
                    for (int i = 0; i < len; i++)
                    {
                        if (_cEvent.canBeDisptached(handlers[i].Target))
                        {
                            handlers[i]();
                        }
                    }
                }
            }
        }

        //--------------------------------------
        // 这里之所以需要cloen操作，是为了避免事件调用时，事件队列长度发生变化！
        //--------------------------------------
        // colen不带参数的监听器队列
        private List<EventHandlerFunction> cloenArray(List<EventHandlerFunction> list)
        {
            t_EventHandlerList.Clear();
            int len = list.Count;
            for (int i = 0; i < len; i++)
            {
                t_EventHandlerList.Add(list[i]);
            }
            return t_EventHandlerList;
        }
        // colen带有参数的监听器队列
        private List<DataEventHandlerFunction> cloenArray(List<DataEventHandlerFunction> list)
        {
            t_DateEventHandlerList.Clear();
            int len = list.Count;
            for (int i = 0; i < len; i++)
            {
                t_DateEventHandlerList.Add(list[i]);
            }
            return t_DateEventHandlerList;
        }
    }
}