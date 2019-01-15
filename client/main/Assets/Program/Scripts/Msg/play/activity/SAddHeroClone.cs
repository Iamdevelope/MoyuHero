using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamFaction.GameEventSystem;

namespace GNET
{
	public partial class SAddHeroClone: Protocol
	{

        public int heroid;

        public const int PROTOCOL_TYPE = 789049;

        public SAddHeroClone()
            : base(PROTOCOL_TYPE)
		 {
		 } 

		public override object Clone()
		{
            SAddHeroClone obj = new SAddHeroClone();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(heroid);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            heroid = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
        {
            ResourceindexTemplate _resT = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(heroid);
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.HE_GetHeroHp, _resT.getName());
        }
	}	
}
