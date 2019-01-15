////////////////////////////////////////////////////////////////////////////////
//  
// @module 事件系统
// @author 金奇
// 
////////////////////////////////////////////////////////////////////////////////

namespace DreamFaction.GameEventSystem
{
    /// <summary>
    /// 事件对象，这个类里包含了事件ID，和事件Data. 这个事件只在Dispatch的时候会被重置一次！new操作也只有一次！
    /// </summary>
    public class GameEvent
    {
        private int _id;
        private object _data;
        private IDispatcher _dispatcher;
        private bool _isStoped = false;
        private bool _isLocked = false;
        private object _currentTarget;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public GameEvent()
        {
            _id = 0;
            _data = 0;
            _dispatcher = null;
        }
        /// <summary>
        /// 带参数构造函数
        /// </summary>
        /// <param name="id">事件ID</param>
        /// <param name="data">事件的附加数据</param>
        /// <param name="dispatcher">事件触发器，既是事件发起者也是事件接受者！（任何实现IDispatcher的类）</param>
        public GameEvent(int id, object data, IDispatcher dispatcher)
        {
            _id = id;
            _data = data;
            _dispatcher = dispatcher;
        }
        /// <summary>
        /// 重置事件的状态，重置事件ID，重置事件数据等等！
        /// /// </summary>
        /// <param name="id">新的事件ID</param>
        /// <param name="data">新的事件附加数据</param>
        /// <param name="dispatcher">事件触发器，既是事件发起者也是事件接受者！（任何实现IDispatcher的类）</param>
        public void ResetEvent(int id, object data, IDispatcher dispatcher)
        {
            _id = id;
            _data = data;
            _dispatcher = dispatcher;
            _isStoped = false;
            _isLocked = false;
            _currentTarget = null;
        }


        //--------------------------------------
        // Public 
        //--------------------------------------
        /// <summary>
        /// 立即停止事件后续的派发工作，不包括同一个GameObject中添加的其他监听事件！
        /// 例如：同一个GameObject中给一个事件添加了3个事件监听器，如果第一个监听器调用了此接口，则后续两个监听器还可以触发！
        /// </summary>
        public void stopPropagation()
        {
            _isStoped = true;
        }

        /// <summary>
        /// 立即停止事件后续的派发工作，包括同一个GameObject中添加的监听事件！
        /// 例如：同一个GameObject中给一个事件添加了3个事件监听器，如果第一个监听器调用了此接口，则后续两个监听器无法触发！
        /// </summary>
        public void stopImmediatePropagation()
        {
            _isStoped = true;
            _isLocked = true;
        }

        /// <summary>
        /// 判断是否可以继续派发事件
        /// </summary>
        /// <param name="val">要接收事件的Object</param>
        /// <returns>是否可以继续派发事件</returns>
        public bool canBeDisptached(object val)
        {
            if (_isLocked)
            {
                return false;
            }

            if (_isStoped)
            {
                if (_currentTarget == val)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                _currentTarget = val;
                return true;
            }
        }




        //--------------------------------------
        // GET / SET
        //--------------------------------------
        /// <summary>
        /// 获取事件ID
        /// </summary>
        public int id
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// 获取事件附带的数据
        /// </summary>
        public object data
        {
            get
            {
                return _data;
            }
        }

        /// <summary>
        /// 获取事件发起者，接收者
        /// </summary>
        public IDispatcher dispatcher
        {
            get
            {
                return _dispatcher;
            }
        }

        ///// <summary>
        ///// 获取事件发起者，接收者
        ///// </summary>
        //public object currentTarget {
        //    get {
        //        return _currentTarget;
        //    }
        //}


        /// <summary>
        /// 事件是否被停止！
        /// </summary>
        public bool isStoped
        {
            get
            {
                return _isStoped;
            }
        }

        /// <summary>
        /// 事件是否被锁定！
        /// </summary>
        public bool isLocked
        {
            get
            {
                return _isLocked;
            }
        }
    }

}