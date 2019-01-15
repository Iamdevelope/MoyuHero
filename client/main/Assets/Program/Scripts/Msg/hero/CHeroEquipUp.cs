using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CHeroEquipUp: Protocol
	{

        public int herokey; // Ӣ��key
        public int equiplocation; // װ��λ�� ��1��ʼ
        public int islevelup; // �Ƿ���������0Ϊ��ǿ������1Ϊ��Ʒ
        public int isstrength; // �Ƿ�һ��ǿ����0Ϊ��1Ϊ��


        public const int PROTOCOL_TYPE = 787790;

        public CHeroEquipUp()
            : base(PROTOCOL_TYPE)
		 {
             herokey = 0;
		 } 

		public override object Clone()
		{
            CHeroEquipUp obj = new CHeroEquipUp();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(herokey);
            _os_.marshal(equiplocation);
            _os_.marshal(islevelup);
            _os_.marshal(isstrength);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            herokey = _os_.unmarshal_int();
            equiplocation = _os_.unmarshal_int();
            islevelup = _os_.unmarshal_int();
            isstrength = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
