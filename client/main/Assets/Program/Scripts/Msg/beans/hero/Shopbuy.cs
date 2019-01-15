using System;
namespace GNET
{
    public class Shopbuy : Marshal
	{
        public int shopid; // �̳�ID��key��
        public int todaynum; // �����ѹ������
        public int buyallnum; // �ܹ��������

        public Shopbuy()
        {
            
        }

        public Shopbuy(int _shopid_, int _todaynum_, int _buyallnum_)
        {
            this.shopid = _shopid_;
            this.todaynum = _todaynum_;
            this.buyallnum = _buyallnum_;
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(shopid);
            _os_.marshal(todaynum);
            _os_.marshal(buyallnum);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            shopid = _os_.unmarshal_int();
            todaynum = _os_.unmarshal_int();
            buyallnum = _os_.unmarshal_int();
            return _os_;
		}

	}
}
