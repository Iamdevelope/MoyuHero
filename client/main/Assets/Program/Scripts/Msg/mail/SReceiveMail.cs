using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamFaction.GameEventSystem;


namespace GNET
{
	public partial class SReceiveMail: Protocol
	{
        public const int END_OK = 1; // 成功
        public const int END_ERROR = 2; // 失败

	    public int result;
        public int isget; // 是否领取附件

        public const int PROTOCOL_TYPE = 786939;

        public SReceiveMail()
            : base(PROTOCOL_TYPE)
		 {
		 } 

		public override object Clone()
		{
            SReceiveMail obj = new SReceiveMail();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(result);
            _os_.marshal(isget);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            isget = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
        {
           if (result == END_OK)
           {
               GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_MailRefresh);

           }

        }
	}	
}
