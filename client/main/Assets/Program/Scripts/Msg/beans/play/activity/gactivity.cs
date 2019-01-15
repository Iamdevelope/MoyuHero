using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork.Data;

namespace GNET
{
    public class gactivity : Marshal
	{
        public int id; // �id
        public long time; // ���һ��ʱ��
        public int todaynum; // ���մ���
        public int allnum; // �ۼƴ���
        public int cangetnum; // ������ȡ��������
        public int activitynum; // �����
        public int allactivitynum; // �ۼƼ���
        public int issee; // �Ƿ񿴹�����ʾ�ã�0δ����1�ѿ���

      

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
