using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork.Data;

namespace GNET
{
    public class bossrole : Marshal
	{
        public long bosshpall; // boss��Ѫ��
        public long killhpall; // ��ɱ��Ѫ��
        public long bossnowhp; // ���ι���ǰbossѪ��
        public int zhufunum; // ף������
        public int shouwangzl; // ����֮��
        public int chuanshuozs; // ��˵֮ʯ
        public int openboss; // ֵΪ1234������ڼ���boss,û����Ϊ-1
        public int openendtime; // ����ʱ����
        public int nextintime; // �´ν��뵹��ʱ����

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
