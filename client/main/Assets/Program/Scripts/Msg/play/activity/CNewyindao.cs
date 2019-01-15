using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CNewyindao: Protocol
	{

        public int num;

        public const int PROTOCOL_TYPE = 789044;

        public CNewyindao()
            : base(PROTOCOL_TYPE)
		 {
             num = 0;
		 } 

		public override object Clone()
		{
            CNewyindao obj = new CNewyindao();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(num);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            num = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
