using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameEventSystem;
namespace GNET
{
	public partial class SAddSkin: Protocol
	{

        public int skinid;

        public const int PROTOCOL_TYPE = 787771;

        public SAddSkin()
            : base(PROTOCOL_TYPE)
		 {

		 } 

		public override object Clone()
		{
            SAddSkin obj = new SAddSkin();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(skinid);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            skinid = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{

		}
	}	
}
