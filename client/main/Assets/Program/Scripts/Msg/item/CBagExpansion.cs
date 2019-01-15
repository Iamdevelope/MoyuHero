using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
namespace GNET
{
	public partial class CBagExpansion: Protocol
	{
        public byte bagtype;

        public const int PROTOCOL_TYPE = 787563;

        public CBagExpansion()
            : base(PROTOCOL_TYPE)
		 {
             bagtype = 1;
		 } 

		public override object Clone()
		{
            CBagExpansion obj = new CBagExpansion();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(bagtype);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            bagtype = _os_.unmarshal_byte();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 32; }

        public override void Process()
        {
          
        }
	}	
}
