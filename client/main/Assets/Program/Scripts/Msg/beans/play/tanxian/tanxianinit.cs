using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork.Data;

namespace GNET
{
    public class tanxianinit : Marshal
	{
        public int tanxianid; // 探险id
        public int tanxiantype; // 状态，0未开启，1进行中，2已完成
        public long endtime; // 结束时间
        public int teamnum; // 队伍号

      

        public tanxianinit()
        {
            this.tanxianid = 0;
            this.tanxiantype = 0;
            this.endtime = 0;
            this.teamnum = 0;
        }

        public tanxianinit(int _tanxianid_, int _tanxiantype_, long _endtime_, int _teamnum_)
        {
            this.tanxianid = _tanxianid_;
            this.tanxiantype = _tanxiantype_;
            this.endtime = _endtime_;
            this.teamnum = _teamnum_;
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(tanxianid);
            _os_.marshal(tanxiantype);
            _os_.marshal(endtime);
            _os_.marshal(teamnum);
    		return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            tanxianid = _os_.unmarshal_int();
            tanxiantype = _os_.unmarshal_int();
            endtime = _os_.unmarshal_long();
            teamnum = _os_.unmarshal_int();
            return _os_;
		}
	}
}
