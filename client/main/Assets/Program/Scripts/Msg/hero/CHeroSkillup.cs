using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CHeroSkillup: Protocol
	{

        public int herokey; // ”¢–€key
        public byte skillnum; // ≈‡—¯Œª÷√


        public const int PROTOCOL_TYPE = 787781;

        public CHeroSkillup()
            : base(PROTOCOL_TYPE)
		 {
             herokey = 0;
             skillnum = 0;
		 } 

		public override object Clone()
		{
            CHeroSkillup obj = new CHeroSkillup();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(herokey);
            _os_.marshal(skillnum);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            herokey = _os_.unmarshal_int();
            skillnum = _os_.unmarshal_byte();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
