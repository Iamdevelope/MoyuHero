using System;
using System.Collections;
using System.Collections.Generic;
namespace GNET
{
    public class Bag : Marshal
	{
  
        public List<Item> items;//存储item对象

        public Bag()
        {
            items = new List<Item>();
        }

        public Bag(List<Item> _items_)
        {
            this.items = _items_;
        }

		public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.compact_uint32(items.Count);
            for(int i = 0;i<items.Count;i++)
            {
                Item item = (Item)items[i];
                _os_.marshal(item);
            }

		    return _os_;

		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                Item item = new Item();
                item.unmarshal(_os_);
                items.Add(item);
            }

            return _os_;
		}

	}
}
