using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
namespace GNET
{
	public partial class SResetStage: Protocol
	{

        public const int END_OK = 1; // ³É¹¦
	    public const int END_ERROR = 2; // Ê§°Ü

	    public int endtype;



        public const int PROTOCOL_TYPE = 787947;

        public SResetStage()
            : base(PROTOCOL_TYPE)
		 {
             endtype = 0;


		 } 

		public override object Clone()
		{
            SResetStage obj = new SResetStage();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(endtype);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            endtype = _os_.unmarshal_int();


            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
        {
            
        }
	}	
}
