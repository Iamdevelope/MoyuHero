using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
namespace GNET
{
	public partial class SRefreshMail: Protocol
	{

        public Mail mail;   // ” º˛œÍœ∏–≈œ¢

        public const int PROTOCOL_TYPE = 786940;

        public SRefreshMail()
            : base(PROTOCOL_TYPE)
		 {
             mail = new Mail();
		 } 

		public override object Clone()
		{
            SRefreshMail obj = new SRefreshMail();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(mail);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            mail.unmarshal(_os_);
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            ObjectSelf.GetInstance().GetManager().CopyOneMailDataInfo(mail);
		}
	}	
}
