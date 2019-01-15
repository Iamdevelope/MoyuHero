using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CIdentifyEquip: Protocol
	{

        public int equipkey;

        public const int PROTOCOL_TYPE = 787565;

        public CIdentifyEquip()
            : base(PROTOCOL_TYPE)
		 {
             equipkey = 0;
		 } 

		public override object Clone()
		{
            CIdentifyEquip obj = new CIdentifyEquip();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(equipkey);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{

            equipkey = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 65535; }

        public override void Process(){ }
	}	
}
