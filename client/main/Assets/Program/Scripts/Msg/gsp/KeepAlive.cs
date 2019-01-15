namespace GNET
{
    public partial class KeepAlive: Protocol
	{
 
        public int code;

        public const int PROTOCOL_TYPE = 100;

        public KeepAlive()
            : base(PROTOCOL_TYPE)
		 {
             
		 } 

		public override object Clone()
		{
            KeepAlive obj = new KeepAlive();
			return obj; 
		}

		public override OctetsStream marshal(OctetsStream os)
		{
            os.marshal(code);
			return os;
		}

		public override OctetsStream unmarshal(OctetsStream os)
		{
            code = os.unmarshal_int();
			return os;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 10; }

        public override void Process() 
        {

        }
	}
}
