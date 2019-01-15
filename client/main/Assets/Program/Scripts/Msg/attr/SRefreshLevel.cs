using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
namespace GNET
{
	public partial class SRefreshLevel: Protocol
	{

        public short data;

        public const int PROTOCOL_TYPE = 787437;

        public SRefreshLevel()
            : base(PROTOCOL_TYPE)
		 {
             data = 0;
		 } 

		public override object Clone()
		{
            SRefreshLevel obj = new SRefreshLevel();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(data);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            data = _os_.unmarshal_short();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
        {
            ObjectSelf.GetInstance().Level = data;
        }
	}	
}
