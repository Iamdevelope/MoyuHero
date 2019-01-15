using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using DreamFaction.UI;
namespace GNET
{
	public partial class SIsHaveNotOpen: Protocol
	{

        public int ishave; //  «∑Ò”–Œ¥∂¡” º˛

        public const int PROTOCOL_TYPE = 786935;

        public SIsHaveNotOpen()
            : base(PROTOCOL_TYPE)
		 {
             ishave = 0;
		 } 

		public override object Clone()
		{
            SIsHaveNotOpen obj = new SIsHaveNotOpen();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{

            _os_.marshal(ishave);
            
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{

            ishave = _os_.unmarshal_int();
            
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            ObjectSelf.GetInstance().GetManager().m_HaveNewMail = ishave > 0;
		}
			
	}	
}
