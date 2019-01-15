////////////////////////////////////////////////////////////////////////////////
//  
// @module �¼�ϵͳ
// @author ����
//
////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;

namespace DreamFaction.GameEventSystem
{
    /// <summary>
    /// �������ݲ������¼�����������
    /// </summary>
    public delegate void EventHandlerFunction();
    /// <summary>
    /// �����ݲ������¼�����������
    /// </summary>
    public delegate void DataEventHandlerFunction(GameEvent e);

    /// <summary>
    /// �¼��������ṩ�Ľӿ�,��������ӣ�ɾ�����ɷ��ȵȣ�
    /// </summary>
    public interface IDispatcher
    {
        //--------------------------------------
        // Add �¼�������
        //--------------------------------------
        /// <summary>
        /// ��� �¼����������ص����������������޷���ֵ��
        /// </summary>
        /// <param name="eventID">�¼�ID</param>
        /// <param name="handler">������Ϊ�޲����ĺ���</param>
        void addEventListener(GameEventID eventID, EventHandlerFunction handler);
        /// <summary>
        /// ��� �¼����������ص��������в������޷���ֵ��
        /// </summary>
        /// <param name="eventID">�¼�ID</param>
        /// <param name="handler">������Ϊ�в�����GameEvent���ĺ���</param>
        void addEventListener(GameEventID eventID, DataEventHandlerFunction handler);


        ////--------------------------------------
        //// REMOVE �¼�������
        ////--------------------------------------
        /// <summary>
        /// ɾ���޲������޷���ֵ���¼�������
        /// </summary>
        /// <param name="eventID">�¼�ID</param>
        /// <param name="handler">������Ϊ�޲����ĺ���</param>
        void removeEventListener(GameEventID eventID, EventHandlerFunction handler);
        /// <summary>
        /// ɾ���в������޷���ֵ���¼�������
        /// </summary>
        void removeEventListener(GameEventID eventID, DataEventHandlerFunction handler);


        ////--------------------------------------
        //// DISPATCH �¼�������
        ////--------------------------------------
        /// <summary>
        /// �ַ��޸������ݲ������޷���ֵ���¼�
        /// </summary>
        /// <param name="eventID">�¼�ID</param>
        void dispatchEvent(GameEventID eventID);
        /// <summary>
        /// �ַ��и������ݲ������޷���ֵ���¼�
        /// </summary>
        /// <param name="eventID">�¼�ID</param>
        /// <param name="data">�¼����ӵ�����</param>
        void dispatchEvent(GameEventID eventID, object data);


        ////--------------------------------------
        //// ����ӿ�
        ////--------------------------------------
        /// <summary>
        /// ɾ��������ӵ��¼�������
        /// </summary>
        void clearEvents();
        /// <summary>
        /// ɾ��ĳ���¼�ID�����м�����
        /// </summary>
        /// <param name="eventID">��Ҫɾ�����������¼�ID</param>
        void clearEvent(GameEventID eventID);
    }
}