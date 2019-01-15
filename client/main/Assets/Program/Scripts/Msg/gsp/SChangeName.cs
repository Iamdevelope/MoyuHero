using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameEventSystem;
using UnityEngine;
namespace GNET
{
	public partial class SChangeName: Protocol
	{
        public const int OK = 1; // 成功
        public const int ERROR = 2; // 失败
        public const int INVALID = 3; // 名称不合法
        public const int DUPLICATED = 4; // 重名
        public const int NO_ITEM = 5; // 没有道具
        public const int OVERLEN = 6; // 角色名过长
        public const int SHORTLEN = 7; // 角色名过短
        public const int ERRORCHAR = 8; // 特殊符号
        public const int HAVESPACE = 9; // 有空格

	    public byte error;
        public String newname;

        public const int PROTOCOL_TYPE = 786453;

        public SChangeName()
            : base(PROTOCOL_TYPE)
		 {
             newname = "";
		 } 

		public override object Clone()
		{
            SChangeName obj = new SChangeName();
			return obj; 
		}

		public override OctetsStream marshal(OctetsStream os)
		{
            os.marshal(error);
            os.marshal(newname);
			return os;
		}

		public override OctetsStream unmarshal(OctetsStream os)
		{
            error = os.unmarshal_byte();
            newname = os.unmarshal_String();
			return os;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 10; }

        public override void Process() 
        {
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_ChangeName, error);
        }
	}	
}
