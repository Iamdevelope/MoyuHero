////////////////////////////////////////////////////////////////////////////////
//  
// @module �¼�ϵͳ
// @author ����
// 
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System;
using System.Collections.Generic;
using DreamFaction.LogSystem;

namespace DreamFaction.GameEventSystem
{
    /// <summary>
    /// �¼��ַ�����ȫ�־�̬�࣡������Ӽ����������ɷ��¼���
    /// </summary>
    public class GameEventDispatcher : IDispatcher
    {
        /// <summary>
        /// ����
        /// </summary>
        private static GameEventDispatcher _inst;

        // �¼�����dispatch��ʱ�򴴽�ʹ�ã�
        private static GameEvent _cEvent;
        /// <summary>
        /// �����������޷���ֵ�Ļص���������
        /// </summary>
        private Dictionary<int, List<EventHandlerFunction>> listners = new Dictionary<int, List<EventHandlerFunction>>();
        /// <summary>
        /// ���в������޷���ֵ�Ļص���������
        /// </summary>
        private Dictionary<int, List<DataEventHandlerFunction>> dataListners = new Dictionary<int, List<DataEventHandlerFunction>>();

        // ��ʱ����,�����ظ�new����
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
        // ����¼������������ݻص����������ͷ�Ϊ�������Ļص�������DataEventHandlerFunction���Ͳ��������Ļص�������EventHandlerFunction��
        //--------------------------------------
        /// <summary>
        /// ��� �¼����������ص����������������޷���ֵ��
        /// </summary>
        /// <param name="eventID">�¼�ID</param>
        /// <param name="handler">������Ϊ�޲����ĺ���</param>
        /// <remarks>                  
        ///    �¼�ID����μ�<see  cref="DreamFaction.EventSystem.GameEventID"/><br/>   
        ///    �¼�����μ�<see  cref="DreamFaction.EventSystem.GameEvent"/><br/>  
        /// </remarks> 
        /// <example>
        /// ʾ�����룺
        /// <code>
        ///class EventTestClass
        ///{
        ///    public void Init()
        ///    {
        ///        GameEventDispatcher.Inst.addEventListener(GameEventID.G_Clent_ResOK, OnClienResOK);//�޲���
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
        ///        Debug.Log("OnClienResOK..."); //�޲���
        ///    }
        ///
        ///    public void OnClienResOK_Data(GameEvent e)
        ///    {
        ///        Debug.Log("OnClienResOK : " + e.data); // ��һ�� 10  , �ڶ��� Test String
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
        /// ��� �¼����������ص��������в������޷���ֵ��
        /// </summary>
        /// <param name="eventID">�¼�ID</param>
        /// <param name="handler">������Ϊ�в�����GameEvent���ĺ���</param>
        /// <remarks>                  
        ///    �¼�ID����μ�<see  cref="DreamFaction.EventSystem.GameEventID"/><br/>   
        ///    �¼�����μ�<see  cref="DreamFaction.EventSystem.GameEvent"/><br/>  
        /// </remarks> 
        /// <example>
        /// ʾ�����룺
        /// <code>
        ///class EventTestClass
        ///{
        ///    public void Init()
        ///    {
        ///        GameEventDispatcher.Inst.addEventListener(GameEventID.G_Clent_ResOK, OnClienResOK);//�޲���
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
        ///        Debug.Log("OnClienResOK..."); //�޲���
        ///    }
        ///
        ///    public void OnClienResOK_Data(GameEvent e)
        ///    {
        ///        Debug.Log("OnClienResOK : " + e.data); // ��һ�� 10  , �ڶ��� Test String
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
        // ɾ���¼�������
        //--------------------------------------
        /// <summary>
        /// ɾ���޲������޷���ֵ���¼�������
        /// </summary>
        /// <param name="eventID">�¼�ID</param>
        /// <param name="handler">������Ϊ�޲����ĺ���</param>
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
        /// ɾ���в������޷���ֵ���¼�������
        /// </summary>
        /// <param name="eventID">�¼�ID</param>
        /// <param name="handler">������Ϊ�в����ĺ���</param>
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
        /// ɾ��������ӵ��¼�������
        /// </summary>
        public void clearEvents()
        {
            listners.Clear();
            dataListners.Clear();
        }

        /// <summary>
        /// ɾ��ĳ���¼�ID�����м�����
        /// </summary>
        /// <param name="eventID">��Ҫɾ�����������¼�ID</param>
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
        // �ַ��¼�
        //--------------------------------------
        /// <summary>
        /// �ַ��޸������ݲ������޷���ֵ���¼�
        /// </summary>
        /// <param name="eventID">�¼�ID</param>
        public void dispatchEvent(GameEventID eventID)
        {
            dispatch((int)eventID, null);
        }
        /// <summary>
        /// �ַ��и������ݲ������޷���ֵ���¼�
        /// </summary>
        /// <param name="eventID">�¼�ID</param>
        /// <param name="data">�¼����ӵ�����</param>
        public void dispatchEvent(GameEventID eventID, object data )
        {
            dispatch((int)eventID, data);
        }

        ///// <summary>
        ///// Destroyʱ�ص�������������¼�
        ///// </summary>
        //public virtual void OnDestroy()
        //{
        //    clearEvents();
        //    inst = null;
        //}

        //--------------------------------------
        // ����ִ�зַ�����
        //--------------------------------------
        private void dispatch(int eventID, object data)
        {
            LogManager.LogToFile("�¼�ϵͳ dispatch��" + (GameEventID)eventID + ", Data = " + data);
            //Debug.Log("dispatch_____" + (GameEventID)eventID + ", Data = " + data + "___" +Time.timeSinceLevelLoad);
            // ����/���� �¼��ڵ�����,����Ƶ��new����
            GameEvent _cEvent = new GameEvent(eventID, data, this);//������~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            //_cEvent.ResetEvent(eventID, data, this);
            lock (dataListners)
            {
                // �ص����������ļ�����
                if (dataListners.ContainsKey(eventID))
                {
                    // ����֮������Ҫcloen��������Ϊ�˱����¼�����ʱ���¼����г��ȷ����仯��
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
                // �ص��������ļ�����
                if (listners.ContainsKey(eventID))
                {
                    // ����֮������Ҫcloen��������Ϊ�˱����¼�����ʱ���¼����г��ȷ����仯��
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
        // ����֮������Ҫcloen��������Ϊ�˱����¼�����ʱ���¼����г��ȷ����仯��
        //--------------------------------------
        // colen���������ļ���������
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
        // colen���в����ļ���������
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