using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CUseItem: Protocol
	{
        public byte bagid; // ��ʱֻ��bag��soul��collect�����Ʒ����
        public int itemkey;
        public short num;
        public int dstkey; // ���������Ʒ���佫ʹ��ʱ��������ʲô��ʹ�õ���Ʒ�Լ��ж�

        public const int PROTOCOL_TYPE = 787545;

        public CUseItem()
            : base(PROTOCOL_TYPE)
		 {

		 } 

		public override object Clone()
		{
            CUseItem obj = new CUseItem();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(bagid);
            _os_.marshal(itemkey);
            _os_.marshal(num);
            _os_.marshal(dstkey);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            bagid = _os_.unmarshal_byte();
            itemkey = _os_.unmarshal_int();
            num = _os_.unmarshal_short();
            dstkey = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 1024; }

        public override void Process() { }
	}	
}
