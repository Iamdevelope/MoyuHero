using DreamFaction.GameEventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GNET
{
	public partial class SHeroClone: Protocol
	{
        public const int END_OK = 1; // ³É¹¦
        public const int END_ERROR = 2; // Ê§°Ü

	    public int result;
        public int heroid; // Ó¢ÐÛid

        public const int PROTOCOL_TYPE = 789034;

        public SHeroClone()
            : base(PROTOCOL_TYPE)
		 {
		 } 

		public override object Clone()
		{
            SHeroClone obj = new SHeroClone();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(result);
            _os_.marshal(heroid);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            heroid = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
        {
            if (result == END_OK)
            {
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.HE_HeroCloneInject,heroid);
            }

        }
	}	
}
