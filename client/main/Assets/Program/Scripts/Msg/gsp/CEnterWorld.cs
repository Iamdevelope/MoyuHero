namespace GNET
{
    public partial class CEnterWorld: Protocol
	{

        public long roleid; // ID
        public string mac;

        public const int PROTOCOL_TYPE = 786437;

        public CEnterWorld()
            : base(PROTOCOL_TYPE)
		 {
             roleid = 0;
             mac = "";
		 } 

		public override object Clone()
		{
            CEnterWorld obj = new CEnterWorld();
			return obj; 
		}

		public override OctetsStream marshal(OctetsStream os)
		{
            os.marshal(roleid);
            os.marshal(mac);
            
			return os;
		}

		public override OctetsStream unmarshal(OctetsStream os)
		{
            roleid = os.unmarshal_long();
            mac = os.unmarshal_String();
			return os;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size <= 32; }

        public override void Process() { }
	}	
}
