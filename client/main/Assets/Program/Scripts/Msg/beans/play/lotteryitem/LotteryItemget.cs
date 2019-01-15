using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork.Data;

namespace GNET
{
    public class LotteryItemget : Marshal
	{
        public int id; // 遗迹宝藏ID
        public int superid; // 特殊事件ID
        public int num; // 数量

      

        public LotteryItemget()
        {
            id = 0;
            superid = 0;
            num = 0;
        }

        public LotteryItemget(int _id_, int _superid_, int _num_)
        {
            this.id = _id_;
            this.superid = _superid_;
            this.num = _num_;
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(id);
            _os_.marshal(superid);
            _os_.marshal(num);
    		return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            id = _os_.unmarshal_int();
            superid = _os_.unmarshal_int();
            num = _os_.unmarshal_int();
            return _os_;
		}
	}
}
