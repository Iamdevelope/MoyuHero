using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CDuihuanlb: Protocol
	{

        public string str;

        public const int PROTOCOL_TYPE = 789046;

        public CDuihuanlb()
            : base(PROTOCOL_TYPE)
		 {
             str = "";
		 } 

		public override object Clone()
		{
            CDuihuanlb obj = new CDuihuanlb();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(str);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            str = _os_.unmarshal_String();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 2048; }

        public override void Process() { }
	}	
}
