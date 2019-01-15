using System;
using System.Collections;
using System.Collections.Generic;
namespace GNET
{
    public class LotteryItemAll : Marshal
	{
        public int mapkey; // �ڼ��� ��1��ʼ
        public int mapvalue; // �ڼ��� ��1��ʼ
        public LinkedList<int> superlist;    //�ż���������list
        public int ismonthfirsthave; // �Ƿ����¿���ˢ��0û�У�1��  ����У���һ����ʾ�¿���ˢ
        public int ishavefree; // �Ƿ�����ѳ齱��0�У���0��Ϊ����ʱ���룩
        public Hashtable lotteryitemmap;        // �ż���������Ϣ��keyΪ������valueΪLotteryItemlayer��

        

      

        public LotteryItemAll()
        {
            lotteryitemmap = new Hashtable();
            superlist = new LinkedList<int>();
        }

        public LotteryItemAll(int _mapkey_, int _mapvalue_, LinkedList<int> _superlist_, int _ismonthfirsthave_,
            int _ishavefree_, Hashtable _lotteryitemmap_)
        {
            this.mapkey = _mapkey_;
            this.mapvalue = _mapvalue_;
            this.superlist = _superlist_;
            this.ismonthfirsthave = _ismonthfirsthave_;
            this.ishavefree = _ishavefree_;
            this.lotteryitemmap = _lotteryitemmap_;
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(mapkey);
            _os_.marshal(mapvalue);

            _os_.compact_uint32(superlist.Count);
            LinkedListNode<int> firstNode2 = superlist.First;
            while (firstNode2 != null)
            {
                _os_.marshal(superlist.First.Value);

                superlist.RemoveFirst();
                firstNode2 = superlist.First;
            }
            _os_.marshal(ismonthfirsthave);
            _os_.marshal(ishavefree);

            _os_.compact_uint32(lotteryitemmap.Count);
            foreach (DictionaryEntry de in lotteryitemmap)
            {
                _os_.marshal((int)de.Key);
                _os_.marshal((LotteryItemlayer)de.Value);
            }
        
        
    		return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            mapkey = _os_.unmarshal_int();
            mapvalue = _os_.unmarshal_int();

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_;
                _v_ = _os_.unmarshal_int();
                superlist.AddLast(_v_);
            }

            ismonthfirsthave = _os_.unmarshal_int();
            ishavefree = _os_.unmarshal_int();

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int key;
                LotteryItemlayer value = new LotteryItemlayer();
                key = _os_.unmarshal_int();
                value.unmarshal(_os_);
                lotteryitemmap.Add(key, value);
            }

            return _os_;
		}

	}
}
