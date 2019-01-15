////////////////////////////////////////////////////////////////////////////////
//  
// @module �¼�ϵͳ
// @author ����
// 
////////////////////////////////////////////////////////////////////////////////

namespace DreamFaction.GameEventSystem
{
    /// <summary>
    /// �¼������������������¼�ID�����¼�Data. ����¼�ֻ��Dispatch��ʱ��ᱻ����һ�Σ�new����Ҳֻ��һ�Σ�
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
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public GameEvent()
        {
            _id = 0;
            _data = 0;
            _dispatcher = null;
        }
        /// <summary>
        /// ���������캯��
        /// </summary>
        /// <param name="id">�¼�ID</param>
        /// <param name="data">�¼��ĸ�������</param>
        /// <param name="dispatcher">�¼��������������¼�������Ҳ���¼������ߣ����κ�ʵ��IDispatcher���ࣩ</param>
        public GameEvent(int id, object data, IDispatcher dispatcher)
        {
            _id = id;
            _data = data;
            _dispatcher = dispatcher;
        }
        /// <summary>
        /// �����¼���״̬�������¼�ID�������¼����ݵȵȣ�
        /// /// </summary>
        /// <param name="id">�µ��¼�ID</param>
        /// <param name="data">�µ��¼���������</param>
        /// <param name="dispatcher">�¼��������������¼�������Ҳ���¼������ߣ����κ�ʵ��IDispatcher���ࣩ</param>
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
        /// ����ֹͣ�¼��������ɷ�������������ͬһ��GameObject����ӵ����������¼���
        /// ���磺ͬһ��GameObject�и�һ���¼������3���¼��������������һ�������������˴˽ӿڣ���������������������Դ�����
        /// </summary>
        public void stopPropagation()
        {
            _isStoped = true;
        }

        /// <summary>
        /// ����ֹͣ�¼��������ɷ�����������ͬһ��GameObject����ӵļ����¼���
        /// ���磺ͬһ��GameObject�и�һ���¼������3���¼��������������һ�������������˴˽ӿڣ�����������������޷�������
        /// </summary>
        public void stopImmediatePropagation()
        {
            _isStoped = true;
            _isLocked = true;
        }

        /// <summary>
        /// �ж��Ƿ���Լ����ɷ��¼�
        /// </summary>
        /// <param name="val">Ҫ�����¼���Object</param>
        /// <returns>�Ƿ���Լ����ɷ��¼�</returns>
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
        /// ��ȡ�¼�ID
        /// </summary>
        public int id
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// ��ȡ�¼�����������
        /// </summary>
        public object data
        {
            get
            {
                return _data;
            }
        }

        /// <summary>
        /// ��ȡ�¼������ߣ�������
        /// </summary>
        public IDispatcher dispatcher
        {
            get
            {
                return _dispatcher;
            }
        }

        ///// <summary>
        ///// ��ȡ�¼������ߣ�������
        ///// </summary>
        //public object currentTarget {
        //    get {
        //        return _currentTarget;
        //    }
        //}


        /// <summary>
        /// �¼��Ƿ�ֹͣ��
        /// </summary>
        public bool isStoped
        {
            get
            {
                return _isStoped;
            }
        }

        /// <summary>
        /// �¼��Ƿ�������
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