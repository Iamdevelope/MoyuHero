namespace GNET
{
    public partial class CRoleList: Protocol
	{
 
        public string mac;

        public const int PROTOCOL_TYPE = 786433;

        public CRoleList()
            : base(PROTOCOL_TYPE)
		 {
             mac = "";
		 } 

		public override object Clone()
		{
            CRoleList obj = new CRoleList();
			return obj; 
		}

		public override OctetsStream marshal(OctetsStream os)
		{
            os.marshal(mac);
			return os;
		}

		public override OctetsStream unmarshal(OctetsStream os)
		{
            mac = os.unmarshal_String();
			return os;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 32; }

        public override void Process() { }
	}	
}
