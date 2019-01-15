using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameEventSystem;
using UnityEngine;

namespace GNET
{
	public partial class SSplitHero: Protocol
	{

        public static int END_OK = 1; // 成功
	    public static int END_NOT_OK = 2; // 失败

	    public int result; // 结果

        public const int PROTOCOL_TYPE = 787776;

        public SSplitHero()
            : base(PROTOCOL_TYPE)
		 {

		 } 

		public override object Clone()
		{
            SSplitHero obj = new SSplitHero();
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
			if(result == END_OK)
			{
				// 显示 UI
				GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_Lith_Hero_Update);
				UI_HeroLitholysin.inst.SmeltSuccess();
			}
			else
			{
				//Debug.Log("SSplitHero failure");
			}

			
		}
	}	
}
