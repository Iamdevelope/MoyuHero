namespace GNET
{
    public partial class CCreateRole: Protocol
	{

        public string name;
        public int firsthero;

        public const int PROTOCOL_TYPE = 786435;

        public CCreateRole()
            : base(PROTOCOL_TYPE)
		 {
             name = "";
             firsthero = 0;
		 } 

		public override object Clone()
		{
            CCreateRole obj = new CCreateRole();
			return obj; 
		}

		public override OctetsStream marshal(OctetsStream os)
		{
            os.marshal(name);
            os.marshal(firsthero);
			return os;
		}

		public override OctetsStream unmarshal(OctetsStream os)
		{
            name = os.unmarshal_String();
            firsthero = os.unmarshal_int();
			return os;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
