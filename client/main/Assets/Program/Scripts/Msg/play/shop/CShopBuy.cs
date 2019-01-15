using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GNET
{
	public partial class CShopBuy: Protocol
	{
        public int shopid; // �̵���Ʒid
        public int num; // ��������
        public byte isdiscount; // �Ƿ�����ڼ䣨1�ǣ�0���ǣ�

        public const int PROTOCOL_TYPE = 788833;

        public CShopBuy()
            : base(PROTOCOL_TYPE)
		 {

		 } 

		public override object Clone()
		{
            CShopBuy obj = new CShopBuy();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(shopid);
            _os_.marshal(num);
            _os_.marshal(isdiscount);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            shopid = _os_.unmarshal_int();
            num = _os_.unmarshal_int();
            isdiscount = _os_.unmarshal_byte();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 1024; }

        public override void Process()
        {
            Debug.Log ( "dskajfdls" );
        }
    }
}
