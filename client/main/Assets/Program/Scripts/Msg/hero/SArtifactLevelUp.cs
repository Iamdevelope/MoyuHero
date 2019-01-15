using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
namespace GNET
{
    public partial class SArtifactLevelUp : Protocol
    {

        public int artifacttype; // 神器类型（key）
        public int artifactid; // 神器ID

        public const int PROTOCOL_TYPE = 787785;

        public SArtifactLevelUp()
            : base(PROTOCOL_TYPE)
        {

        }

        public override object Clone()
        {
            SArtifactLevelUp obj = new SArtifactLevelUp();
            return obj;
        }

        public override OctetsStream marshal(OctetsStream _os_)
        {
            _os_.marshal(artifacttype);
            _os_.marshal(artifactid);
            return _os_;
        }

        public override OctetsStream unmarshal(OctetsStream _os_)
        {
            artifacttype = _os_.unmarshal_int();
            artifactid = _os_.unmarshal_int();
            return _os_;
        }

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size <= 1024; }

        public override void Process()
        {
			//UI_ArtifactSoul.inst.SoulSuccess();
        }
    }
}
