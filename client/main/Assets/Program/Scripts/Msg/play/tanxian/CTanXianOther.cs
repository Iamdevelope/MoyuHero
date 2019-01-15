using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CTanXianOther: Protocol
	{
        public const int END_GET = 1; // ��ȡ����
        public const int END_SPEED = 2; // �������
        public const int END_NULL = 3; // �ٻ�
        public const int SREFRESH = 4; // ˢ��

        public int endtype;
        public int tanxianid; // ̽��id


        public const int PROTOCOL_TYPE = 788986;

        public CTanXianOther()
            : base(PROTOCOL_TYPE)
		 {
             endtype = 0;
             tanxianid = 0;
		 } 

		public override object Clone()
		{
            CTanXianOther obj = new CTanXianOther();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(endtype);
            _os_.marshal(tanxianid);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            endtype = _os_.unmarshal_int();
            tanxianid = _os_.unmarshal_int();

            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
