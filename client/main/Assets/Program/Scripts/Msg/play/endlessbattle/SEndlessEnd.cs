using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;

namespace GNET
{
	public partial class SEndlessEnd: Protocol
	{
        public int groupnum; // �ڼ���
        public int alldropnum; // ����֤������
        public int pact; // ǿ��֮Լ��û����Ϊ-1��
        public int pactispass; // ǿ��֮Լ�Ƿ��ɣ�0Ϊδ��ɣ�1Ϊ��ɣ�
        public Hashtable dropmap; // �������棨keyΪ��Ʒ����ԴID��valueΪ������

        public const int PROTOCOL_TYPE = 788939;

        public SEndlessEnd()
            : base(PROTOCOL_TYPE)
		 {
             dropmap = new Hashtable();
		 } 

		public override object Clone()
		{
            SEndlessEnd obj = new SEndlessEnd();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(groupnum);
            _os_.marshal(alldropnum);
            _os_.marshal(pact);
            _os_.marshal(pactispass);
            _os_.compact_uint32(dropmap.Count);
            foreach (DictionaryEntry de in dropmap)
            {
                _os_.marshal((int)de.Key);
                _os_.marshal((int)de.Value);
            }

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            groupnum = _os_.unmarshal_int();
            alldropnum = _os_.unmarshal_int();
            pact = _os_.unmarshal_int();
            pactispass = _os_.unmarshal_int();
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int key, value;
                key = _os_.unmarshal_int();
                value = _os_.unmarshal_int();
                dropmap.Add(key, value);
            }
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
        {
            //ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter = false;
            ObjectSelf.GetInstance().LimitFightMgr.m_RoundNum = groupnum;
            ObjectSelf.GetInstance().LimitFightMgr.m_AllDropNum = alldropnum;
            ObjectSelf.GetInstance().LimitFightMgr.m_Pact = pact;
            ObjectSelf.GetInstance().LimitFightMgr.m_PactIspass = pactispass;
            ObjectSelf.GetInstance().LimitFightMgr.m_DropMap = dropmap;

            GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_LimitFightEnd);
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_LimitPactOk);
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_LimitFightEnd);
        }
	}	
}
