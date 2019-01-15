using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameEventSystem;
using UnityEngine;

namespace GNET
{
	public partial class STuJianBox: Protocol
	{

        public static int END_OK = 1; // 成功
	    public static int END_NOT_OK = 2; // 失败

	    public int result; // 结果
        public int boxid; // 宝箱ID

        public const int PROTOCOL_TYPE = 787764;

        public STuJianBox()
            : base(PROTOCOL_TYPE)
		 {
             result = 0;
             boxid = 0;
		 } 

		public override object Clone()
		{
            STuJianBox obj = new STuJianBox();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(result);
            _os_.marshal(boxid);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            boxid = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            if (result == END_NOT_OK)
            {
                return;
            }

            GameEventDispatcher.Inst.dispatchEvent(GameEventID.HB_BoxUpdate, boxid);
		}
	}	
}
