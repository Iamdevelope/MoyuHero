using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork.Data;

namespace GNET
{
    public class NewShop : Marshal
	{
        public int itemid; // 77��ĵ���ID
        public int costtype; // ������Դ
        public int price; // �۸�
        public int num; // ����
        public int isbuy; // 0δ����1Ϊ�ѹ���
      

        public NewShop()
        {

        }

        public NewShop(int _itemid_, int _costtype_, int _price_, int _num_, int _isbuy_)
        {
            this.itemid = _itemid_;
            this.costtype = _costtype_;
            this.price = _price_;
            this.num = _num_;
            this.isbuy = _isbuy_;
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(itemid);
            _os_.marshal(costtype);
            _os_.marshal(price);
            _os_.marshal(num);
            _os_.marshal(isbuy);
    		return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            itemid = _os_.unmarshal_int();
            costtype = _os_.unmarshal_int();
            price = _os_.unmarshal_int();
            num = _os_.unmarshal_int();
            isbuy = _os_.unmarshal_int();
            return _os_;
		}
	}
}
