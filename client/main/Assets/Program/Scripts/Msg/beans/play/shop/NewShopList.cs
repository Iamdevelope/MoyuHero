using System;
using System.Collections;
using System.Collections.Generic;
namespace GNET
{
    public class NewShopList : Marshal
	{
        public LinkedList<NewShop> shoplist;    // �����̳��б�
        public long lasttime; // ˢ�µ���ʱʱ��
        public int refreshnum; // ˢ�´���

        public NewShopList()
        {
            shoplist = new LinkedList<NewShop>();
        }

        public NewShopList(LinkedList<NewShop> _shoplist_, long _lasttime_)
        {
            this.shoplist = _shoplist_;
            this.lasttime = _lasttime_;
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.compact_uint32(shoplist.Count);
            LinkedListNode<NewShop> firstNode2 = shoplist.First;
            while (firstNode2 != null)
            {
                _os_.marshal(shoplist.First.Value);

                shoplist.RemoveFirst();
                firstNode2 = shoplist.First;
            }
            _os_.marshal(lasttime);
            _os_.marshal(refreshnum);
        
    		return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                 NewShop _v_ = new NewShop();
                 _v_.unmarshal(_os_);
                 shoplist.AddLast(_v_);
            }

            lasttime = _os_.unmarshal_long();
            refreshnum = _os_.unmarshal_int();

            return _os_;
		}

	}
}
