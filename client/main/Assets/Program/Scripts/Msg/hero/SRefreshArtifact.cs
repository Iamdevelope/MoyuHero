using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using UnityEngine;

namespace GNET
{
	public partial class SRefreshArtifact: Protocol
	{

        public Artifact artifactinfo;

        public const int PROTOCOL_TYPE = 787774;

        public SRefreshArtifact()
            : base(PROTOCOL_TYPE)
		 {
             artifactinfo = new Artifact();
		 } 

		public override object Clone()
		{
            SRefreshArtifact obj = new SRefreshArtifact();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(artifactinfo);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            artifactinfo.unmarshal(_os_);
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
			ObjectSelf.GetInstance().ArtifactContainerBag.RefeashArtifact(artifactinfo);
		}
	}	
}
