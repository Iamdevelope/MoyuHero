using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork.Data;

namespace GNET
{
    public class gactivity : Marshal
	{
        public int id; // 活动id
        public long time; // 最近一次时间
        public int todaynum; // 今日次数
        public int allnum; // 累计次数
        public int cangetnum; // 可以领取次数（）
        public int activitynum; // 活动计数
        public int allactivitynum; // 累计计数
        public int issee; // 是否看过（提示用，0未看，1已看）

      

        public gactivity()
        {
        }

        public gactivity(int _id_, long _time_, int _todaynum_, int _allnum_, int _cangetnum_, int _activitynum_,
            int _allactivitynum_, int _issee_)
        {
            this.id = _id_;
            this.time = _time_;
            this.todaynum = _todaynum_;
            this.allnum = _allnum_;
            this.cangetnum = _cangetnum_;
            this.activitynum = _activitynum_;
            this.allactivitynum = _allactivitynum_;
            this.issee = _issee_;
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(id);
            _os_.marshal(time);
            _os_.marshal(todaynum);
            _os_.marshal(allnum);
            _os_.marshal(cangetnum);
            _os_.marshal(activitynum);
            _os_.marshal(allactivitynum);
            _os_.marshal(issee);
    		return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            id = _os_.unmarshal_int();
            time = _os_.unmarshal_long();
            todaynum = _os_.unmarshal_int();
            allnum = _os_.unmarshal_int();
            cangetnum = _os_.unmarshal_int();
            activitynum = _os_.unmarshal_int();
            allactivitynum = _os_.unmarshal_int();
            issee = _os_.unmarshal_int();
            return _os_;
		}

	}
}
