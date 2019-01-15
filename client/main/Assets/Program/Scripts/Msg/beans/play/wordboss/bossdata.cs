using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork.Data;

namespace GNET
{
    public class bossdata : Marshal
	{
        public int bossid1; // bossid(��һ��������)
        public int bossid2; // bossid(��һ��boss)
        public int bossid3; // bossid(�ڶ���������)
        public int bossid4; // bossid(�ڶ���boss)
        public int bossiskill; // ��λΪ��һ��boss��ʮλΪ�ڶ���boss,0Ϊδ��ɱ��1Ϊ��ɱ
        public string boss1killname; // ��ɱ1������
        public string boss2killname; // ��ɱ2������
        public int openboss; // ֵΪ1234������ڼ���boss,û����Ϊ-1
        public int openendtime; // ����ʱ����
        public int shouwangzl; // ����֮��
        public int chuanshuozs; // ��˵֮ʯ
      

        public bossdata()
        {
            boss1killname = "";
            boss2killname = "";
        }

        public bossdata(int _bossid1_, int _bossid2_, int _bossid3_, int _bossid4_, int _bossiskill_, string _boss1killname_,
            string _boss2killname_, int _openboss_, int _openendtime_, int _shouwangzl_, int _chuanshuozs_)
        {
            this.bossid1 = _bossid1_;
            this.bossid2 = _bossid2_;
            this.bossid3 = _bossid3_;
            this.bossid4 = _bossid4_;
            this.bossiskill = _bossiskill_;
            this.boss1killname = _boss1killname_;
            this.boss2killname = _boss2killname_;
            this.openboss = _openboss_;
            this.openendtime = _openendtime_;
            this.shouwangzl = _shouwangzl_;
            this.chuanshuozs = _chuanshuozs_;
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(bossid1);
            _os_.marshal(bossid2);
            _os_.marshal(bossid3);
            _os_.marshal(bossid4);
            _os_.marshal(bossiskill);
            _os_.marshal(boss1killname);
            _os_.marshal(boss2killname);
            _os_.marshal(openboss);
            _os_.marshal(openendtime);
            _os_.marshal(shouwangzl);
            _os_.marshal(chuanshuozs);
    		return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            bossid1 = _os_.unmarshal_int();
            bossid2 = _os_.unmarshal_int();
            bossid3 = _os_.unmarshal_int();
            bossid4 = _os_.unmarshal_int();
            bossiskill = _os_.unmarshal_int();
            boss1killname = _os_.unmarshal_String();
            boss2killname = _os_.unmarshal_String();
            openboss = _os_.unmarshal_int();
            openendtime = _os_.unmarshal_int();
            shouwangzl = _os_.unmarshal_int();
            chuanshuozs = _os_.unmarshal_int();
            return _os_;
		}
	}
}
