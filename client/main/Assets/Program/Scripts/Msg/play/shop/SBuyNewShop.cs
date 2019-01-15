using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameEventSystem;
using UnityEngine;

namespace GNET
{
	public partial class SBuyNewShop: Protocol
	{

        public static int END_OK = 1; // �ɹ�
	    public static int END_NOT_OK = 2; // ʧ��

	    public int result; // ���
        public int shopid; // 76���ID
        public int itemid; // 77��ĵ���ID
        public int costtype; // ������Դ
        public int price; // �۸�
        public int num; // ����

        public const int PROTOCOL_TYPE = 788840;

        public SBuyNewShop()
            : base(PROTOCOL_TYPE)
		 {

		 } 

		public override object Clone()
		{
            SBuyNewShop obj = new SBuyNewShop();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(result);
            _os_.marshal(shopid);
            _os_.marshal(itemid);
            _os_.marshal(costtype);
            _os_.marshal(price);
            _os_.marshal(num);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            shopid = _os_.unmarshal_int();
            itemid = _os_.unmarshal_int();
            costtype = _os_.unmarshal_int();
            price = _os_.unmarshal_int();
            num = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{

		}
	}	
}
