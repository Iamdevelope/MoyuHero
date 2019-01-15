using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameEventSystem;
namespace GNET
{
	public partial class SChangeSkin: Protocol
	{

        public static int END_OK = 1; // 成功
	    public static int END_NOT_OK = 2; // 失败

	    public int result; // 结果

        public const int PROTOCOL_TYPE = 787770;

        public SChangeSkin()
            : base(PROTOCOL_TYPE)
		 {

		 } 

		public override object Clone()
		{
            SChangeSkin obj = new SChangeSkin();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(result);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            //if (result == 1)
            //{
                
            //}

            UI_HeroSkin._inst.GetResult(result);
		}
	}	
}
