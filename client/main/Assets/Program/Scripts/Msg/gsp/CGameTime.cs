namespace GNET
{
    public partial class CGameTime: Protocol
	{

        public const int PROTOCOL_TYPE = 786444;

        public CGameTime()
            : base(PROTOCOL_TYPE)
		 {
		 } 

		public override object Clone()
		{
            CGameTime obj = new CGameTime();
			return obj; 
		}

		public override OctetsStream marshal(OctetsStream os)
		{

			return os;
		}

		public override OctetsStream unmarshal(OctetsStream os)
		{
			return os;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
        {
            
        }
	}	
}
