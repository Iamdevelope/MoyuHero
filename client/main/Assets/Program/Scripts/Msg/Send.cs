namespace GNET
{
    public partial class Send: Protocol
	{
        public int ptype;
        public Octets pdata;

        public const int PROTOCOL_TYPE = 65541;

        public Send()
            : base(PROTOCOL_TYPE)
		 {
             ptype = 0;
		 } 

		public override object Clone()
		{
            Send obj = new Send();
			return obj; 
		}

		public override OctetsStream marshal(OctetsStream os)
		{
            os.marshal(ptype);
            os.marshal(pdata);
			return os;
		}

		public override OctetsStream unmarshal(OctetsStream os)
		{
            ptype = os.unmarshal_int();
            pdata = os.unmarshal_Octets();
			return os;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 65535; }

        public override void Process(){}
	}	
}
