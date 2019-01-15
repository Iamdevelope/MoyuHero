using DreamFaction.GameEventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GNET
{
	public partial class SGetGameAct: Protocol
	{
        public const int END_OK = 1; // �ɹ�
        public const int END_ERROR = 2; // ʧ��

	    public int result;
        public int actid;

        public const int PROTOCOL_TYPE = 789051;

        public SGetGameAct()
            : base(PROTOCOL_TYPE)
		 {
		 } 

		public override object Clone()
		{
            SGetGameAct obj = new SGetGameAct();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(result);
            _os_.marshal(actid);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            actid = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
        {
            if (result == 1)
            {
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_ActivityRefreshSingle);
            }
            if (result == 2)
            {
                Debug.Log("Error");
            }

        }
	}	
}
