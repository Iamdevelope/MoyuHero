using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork.Data;

namespace GNET
{
    public class LotteryItem : Marshal
	{
        public int id; // 遗迹宝藏ID
        public int isget; // 是否领取
        public int viewnum; // 显示位置
        public int superid; // 激活的特殊事件
      

        public LotteryItem()
        {
            id = 0;
            isget = 0;
            viewnum = 0;
            superid = 0;
        }

        public LotteryItem(int _id_, int _isget_, int _viewnum_, int _superid_)
        {
            this.id = _id_;
            this.isget = _isget_;
            this.viewnum = _viewnum_;
            this.superid = _superid_;
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(id);
            _os_.marshal(isget);
            _os_.marshal(viewnum);
            _os_.marshal(superid);
    		return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            id = _os_.unmarshal_int();
            isget = _os_.unmarshal_int();
            viewnum = _os_.unmarshal_int();
            superid = _os_.unmarshal_int();
            return _os_;
		}
	}
}
