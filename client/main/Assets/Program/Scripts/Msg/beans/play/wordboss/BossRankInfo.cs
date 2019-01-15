using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork.Data;

namespace GNET
{
    public class BossRankInfo : Marshal
	{
        public long roleid; // Íæ¼Òguid
        public string rolename; // Íæ¼ÒÃû³Æ
        public long num; // ÉËº¦

        public BossRankInfo()
        {
            roleid = 0;
            rolename = "";
            num = 0;
        }

        public BossRankInfo(long _roleid_, string _rolename_, long _num_)
        {
            this.roleid = _roleid_;
            this.rolename = _rolename_;
            this.num = _num_;
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(roleid);
            _os_.marshal(rolename);
            _os_.marshal(num);
    		return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            roleid = _os_.unmarshal_long();
            rolename = _os_.unmarshal_String();
            num = _os_.unmarshal_long();
            return _os_;
		}
	}
}
