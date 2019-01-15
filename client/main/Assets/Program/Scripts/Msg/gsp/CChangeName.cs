using System;

namespace GNET
{
    public partial class CChangeName: Protocol
	{

        public String newname;
        public int itemkey;

        public const int PROTOCOL_TYPE = 786452;

        public CChangeName()
            : base(PROTOCOL_TYPE)
		 {
             
		 } 

		public override object Clone()
		{
            CChangeName obj = new CChangeName();
			return obj; 
		}

		public override OctetsStream marshal(OctetsStream os)
		{
            os.marshal(newname);
            os.marshal(itemkey);
			return os;
		}

		public override OctetsStream unmarshal(OctetsStream os)
		{
            newname = os.unmarshal_String();
            itemkey = os.unmarshal_int();
			return os;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 10; }

        public override void Process() 
        {
            
        }
	}	
}
