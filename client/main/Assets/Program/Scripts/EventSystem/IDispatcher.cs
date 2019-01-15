////////////////////////////////////////////////////////////////////////////////
//  
// @module 事件系统
// @author 金奇
//
////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;

namespace DreamFaction.GameEventSystem
{
    /// <summary>
    /// 不带数据参数的事件监听器队列
    /// </summary>
    public delegate void EventHandlerFunction();
    /// <summary>
    /// 带数据参数的事件监听器队列
    /// </summary>
    public delegate void DataEventHandlerFunction(GameEvent e);

    /// <summary>
    /// 事件监听器提供的接口,包括，添加，删除，派发等等！
    /// </summary>
    public interface IDispatcher
    {
        //--------------------------------------
        // Add 事件监听器
        //--------------------------------------
        /// <summary>
        /// 添加 事件监听器（回调函数不带参数，无返回值）
        /// </summary>
        /// <param name="eventID">事件ID</param>
        /// <param name="handler">监听器为无参数的函数</param>
        void addEventListener(GameEventID eventID, EventHandlerFunction handler);
        /// <summary>
        /// 添加 事件监听器（回调函数带有参数，无返回值）
        /// </summary>
        /// <param name="eventID">事件ID</param>
        /// <param name="handler">监听器为有参数（GameEvent）的函数</param>
        void addEventListener(GameEventID eventID, DataEventHandlerFunction handler);


        ////--------------------------------------
        //// REMOVE 事件监听器
        ////--------------------------------------
        /// <summary>
        /// 删除无参数，无返回值的事件监听器
        /// </summary>
        /// <param name="eventID">事件ID</param>
        /// <param name="handler">监听器为无参数的函数</param>
        void removeEventListener(GameEventID eventID, EventHandlerFunction handler);
        /// <summary>
        /// 删除有参数，无返回值的事件监听器
        /// </summary>
        void removeEventListener(GameEventID eventID, DataEventHandlerFunction handler);


        ////--------------------------------------
        //// DISPATCH 事件监听器
        ////--------------------------------------
        /// <summary>
        /// 分发无附加数据参数，无返回值的事件
        /// </summary>
        /// <param name="eventID">事件ID</param>
        void dispatchEvent(GameEventID eventID);
        /// <summary>
        /// 分发有附加数据参数，无返回值的事件
        /// </summary>
        /// <param name="eventID">事件ID</param>
        /// <param name="data">事件附加的数据</param>
        void dispatchEvent(GameEventID eventID, object data);


        ////--------------------------------------
        //// 清理接口
        ////--------------------------------------
        /// <summary>
        /// 删除所有添加的事件监听器
        /// </summary>
        void clearEvents();
        /// <summary>
        /// 删除某个事件ID的所有监听器
        /// </summary>
        /// <param name="eventID">需要删除监听器的事件ID</param>
        void clearEvent(GameEventID eventID);
    }
}