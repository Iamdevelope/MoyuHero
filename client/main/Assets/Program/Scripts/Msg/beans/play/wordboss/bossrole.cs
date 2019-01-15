using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork.Data;

namespace GNET
{
    public class bossrole : Marshal
	{
        public long bosshpall; // boss总血量
        public long killhpall; // 击杀总血量
        public long bossnowhp; // 本次攻击前boss血量
        public int zhufunum; // 祝福次数
        public int shouwangzl; // 守望之灵
        public int chuanshuozs; // 传说之石
        public int openboss; // 值为1234，代表第几个boss,没有则为-1
        public int openendtime; // 倒计时，秒
        public int nextintime; // 下次进入倒计时，秒

        public bossrole()
        {

        }

        public bossrole(long _bosshpall_, long _killhpall_, long _bossnowhp_, int _zhufunum_, int _shouwangzl_, int _chuanshuozs_
            , int _openboss_, int _openendtime_, int _nextintime_)
        {
            this.bosshpall = _bosshpall_;
            this.killhpall = _killhpall_;
            this.bossnowhp = _bossnowhp_;
            this.zhufunum = _zhufunum_;
            this.shouwangzl = _shouwangzl_;
            this.chuanshuozs = _chuanshuozs_;
            this.openboss = _openboss_;
            this.openendtime = _openendtime_;
            this.nextintime = _nextintime_;
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(bosshpall);
            _os_.marshal(killhpall);
            _os_.marshal(bossnowhp);
            _os_.marshal(zhufunum);
            _os_.marshal(shouwangzl);
            _os_.marshal(chuanshuozs);
            _os_.marshal(openboss);
            _os_.marshal(openendtime);
            _os_.marshal(nextintime);
    		return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            bosshpall = _os_.unmarshal_long();
            killhpall = _os_.unmarshal_long();
            bossnowhp = _os_.unmarshal_long();
            zhufunum = _os_.unmarshal_int();
            shouwangzl = _os_.unmarshal_int();
            chuanshuozs = _os_.unmarshal_int();
            openboss = _os_.unmarshal_int();
            openendtime = _os_.unmarshal_int();
            nextintime = _os_.unmarshal_int();
            return _os_;
		}
	}
}
