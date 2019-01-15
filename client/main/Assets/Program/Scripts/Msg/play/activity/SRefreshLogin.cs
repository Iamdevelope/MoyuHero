using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;

namespace GNET
{
	public partial class SRefreshLogin: Protocol
	{
        public int signnum7;
        public int signnum28;

        public const int PROTOCOL_TYPE = 789043;

        public SRefreshLogin()
            : base(PROTOCOL_TYPE)
		 {
		 } 

		public override object Clone()
		{
            SRefreshLogin obj = new SRefreshLogin();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(signnum7);
            _os_.marshal(signnum28);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            signnum7 = _os_.unmarshal_int();
            signnum28 = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
        {
            ObjectSelf objSelf = ObjectSelf.GetInstance();
            if (objSelf != null)
            {
                objSelf.SignIn7 = signnum7;
                objSelf.SignIn28 = signnum28;
                DreamFaction.UI.Core.UI_HomeControler.NeedShowSignInPanel = true;
            }
        }
	}	
}
