using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
namespace GNET
{
	public partial class CSellItem: Protocol
	{

        public byte bagid;
        public Hashtable items;

        public const int PROTOCOL_TYPE = 787544;

        public CSellItem()
            : base(PROTOCOL_TYPE)
		 {
             items = new Hashtable();
		 } 

		public override object Clone()
		{
            CSellItem obj = new CSellItem();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(bagid);
            _os_.compact_uint32(items.Count);
            foreach (DictionaryEntry de in items)
            {
                _os_.marshal((int)de.Key);
                _os_.marshal((int)de.Value);
            }
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{

            bagid = _os_.unmarshal_byte();
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int key, value;
                key = _os_.unmarshal_int();
                value = _os_.unmarshal_int();
                items.Add(key, value);
            }

            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 65535; }

        public override void Process()
        {
          
        }
	}	
}
