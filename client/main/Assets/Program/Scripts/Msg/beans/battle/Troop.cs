using System;
using System.Collections.Generic;
namespace GNET
{
    public class Troop : Marshal
	{
        public int troopnum; // 战队编号
        public int trooptype; // 战队类型，1为前2后3，2为前3后2
        public int location1; // 0没装
        public int location2; // 0没装
        public int location3; // 0没装
        public int location4; // 0没装
        public int location5; // 0没装
        public int sh1; // 神魂1号，0没装
        public int sh2; // 神魂2号，0没装
        public int sh3; // 神魂3号，0没装
        public int sh4; // 神魂4号，0没装

        public Troop()
        {
            troopnum = 0;
            trooptype = 0;
            location1 = 0;
            location2 = 0;
            location3 = 0;
            location4 = 0;
            location5 = 0;
        }

        public Troop(int _troopnum_, int _trooptype_, int _location1_, int _location2_, int _location3_, int _location4_, int _location5_,
            int _sh1_, int _sh2_, int _sh3_, int _sh4_)
        {
            this.troopnum = _troopnum_;
            this.trooptype = _trooptype_;
            this.location1 = _location1_;
            this.location2 = _location2_;
            this.location3 = _location3_;
            this.location4 = _location4_;
            this.location5 = _location5_;
            this.sh1 = _sh1_;
		    this.sh2 = _sh2_;
		    this.sh3 = _sh3_;
	    	this.sh4 = _sh4_;
        }

		public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(troopnum);
            _os_.marshal(trooptype);
            _os_.marshal(location1);
            _os_.marshal(location2);
            _os_.marshal(location3);
            _os_.marshal(location4);
            _os_.marshal(location5);
            _os_.marshal(sh1);
		    _os_.marshal(sh2);
		    _os_.marshal(sh3);
		    _os_.marshal(sh4);

            /*
            _os_.compact_uint32(skills.Count);
            LinkedListNode<int> firstNode = skills.First;
            while (firstNode != null)
            {
                _os_.marshal(skills.First.Value);

                skills.RemoveFirst();
                firstNode = skills.First;
            }

            _os_.compact_uint32(viceheros.Count);
            LinkedListNode<int> firstNode2 = viceheros.First;
            while (firstNode2 != null)
            {
                _os_.marshal(viceheros.First.Value);

                viceheros.RemoveFirst();
                firstNode2 = viceheros.First;
            }
             * */

		    return _os_;

		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            troopnum = _os_.unmarshal_int();
            trooptype = _os_.unmarshal_int();
            location1 = _os_.unmarshal_int();
            location2 = _os_.unmarshal_int();
            location3 = _os_.unmarshal_int();
            location4 = _os_.unmarshal_int();
            location5 = _os_.unmarshal_int();

            sh1 = _os_.unmarshal_int();
		    sh2 = _os_.unmarshal_int();
		    sh3 = _os_.unmarshal_int();
	    	sh4 = _os_.unmarshal_int();
            /*
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_;
                _v_ = _os_.unmarshal_int();
                skills.AddFirst(_v_);
            }
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_;
                _v_ = _os_.unmarshal_int();
                viceheros.AddFirst(_v_);
            }
             * 
             * */
            return _os_;
		}

	}
}
