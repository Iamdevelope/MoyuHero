using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CBeginBoss: Protocol
	{

        public short troopid; // ս��ID
        public int bossid; // ֵΪ1234������ڼ���boss
        public int iscost; // �Ƿ�ˢ�½��룬0��1��

        public const int PROTOCOL_TYPE = 788887;

        public CBeginBoss()
            : base(PROTOCOL_TYPE)
		 {
            troopid = 0;
		 } 

		public override object Clone()
		{
            CBeginBoss obj = new CBeginBoss();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(troopid);
            _os_.marshal(bossid);
            _os_.marshal(iscost);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            troopid = _os_.unmarshal_short();
            bossid = _os_.unmarshal_int();
            iscost = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
