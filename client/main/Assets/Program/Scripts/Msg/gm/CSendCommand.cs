using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CSendCommand: Protocol
	{

        public string cmd;

        public const int PROTOCOL_TYPE = 787431;

        public CSendCommand()
            : base(PROTOCOL_TYPE)
		 {
             cmd = "";
		 } 

		public override object Clone()
		{
            CSendCommand obj = new CSendCommand();
			return obj; 
		}

		public override OctetsStream marshal(OctetsStream os)
		{
            os.marshal(cmd);
			return os;
		}

		public override OctetsStream unmarshal(OctetsStream os)
		{
            cmd = os.unmarshal_String();
			return os;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
        {

        }
	}	
}
