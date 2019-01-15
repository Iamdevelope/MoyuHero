using System;
using System.Collections;
using System.Collections.Generic;
namespace GNET
{
    public class endlessAttr : Marshal
	{
        public int dropnum; // ʣ������֤������
        public int alldropnum; // ����֤������
        public int add1; // ����1�������
        public int add2; // ����2�������
        public int add3; // ����3�������
        public int add4; // ����4�����������������
        public Hashtable herobloodlist; // ʹ��Ӣ�۵�Ѫ����keyΪλ�ã�valueΪѪ����
        

      

        public endlessAttr()
        {
            herobloodlist = new Hashtable();
        }

        public endlessAttr(int _dropnum_, int _alldropnum_, int _add1_, int _add2_, int _add3_, int _add4_
                        , Hashtable _herobloodlist_)
        {
            this.dropnum = _dropnum_;
            this.alldropnum = _alldropnum_;
            this.add1 = _add1_;
            this.add2 = _add2_;
            this.add3 = _add3_;
            this.add4 = _add4_;
            this.herobloodlist = _herobloodlist_;
       
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(dropnum);
            _os_.marshal(alldropnum);
            _os_.marshal(add1);
            _os_.marshal(add2);
            _os_.marshal(add3);
            _os_.marshal(add4);

            _os_.compact_uint32(herobloodlist.Count);
            foreach (DictionaryEntry de in herobloodlist)
            {
                _os_.marshal((int)de.Key);
                _os_.marshal((int)de.Value);
            }      
        
    		return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            dropnum = _os_.unmarshal_int();
            alldropnum = _os_.unmarshal_int();
            add1 = _os_.unmarshal_int();
            add2 = _os_.unmarshal_int();
            add3 = _os_.unmarshal_int();
            add4 = _os_.unmarshal_int();

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int key,value;
                key = _os_.unmarshal_int();
                value = _os_.unmarshal_int();
                herobloodlist.Add(key, value);
            }
            return _os_;
		}

	}
}
