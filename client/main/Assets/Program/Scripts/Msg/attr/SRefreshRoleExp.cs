using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
namespace GNET
{
	public partial class SRefreshRoleExp: Protocol
	{

        public int data;

        public const int PROTOCOL_TYPE = 787439;

        public SRefreshRoleExp()
            : base(PROTOCOL_TYPE)
		 {
             data = 0;
		 } 

		public override object Clone()
		{
            SRefreshRoleExp obj = new SRefreshRoleExp();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(data);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            data = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process()
        {
           ObjectSelf.GetInstance().UpdateHumanExp(data);
        }
	}	
}
