using System;
using System.Collections;
using System.Collections.Generic;
namespace GNET
{
    public class bossBattleInfo : Marshal
	{
        public int battleid;                    // �ؿ�id
        public Hashtable useherokeylist;        // ʹ��Ӣ��id��λ�ã�keyΪλ�ã�valueΪherokey��
        public LinkedList<int> monstergroup;    //������
        public int trooptype; // ս������
        public int monstertrooptype; // ����ս������
        public int zhufunum; // ף������
        public long bossnowhp; // ���ι���ǰbossѪ��
        public int bossid; // ֵΪ1234������ڼ���boss,û����Ϊ-1

      

        public bossBattleInfo()
        {
            useherokeylist = new Hashtable();
            monstergroup = new LinkedList<int>();
        }

        public bossBattleInfo(int _battleid_, Hashtable _useherokeylist_, LinkedList<int> _monstergroup_, int _trooptype_,
            int _monstertrooptype_, int _zhufunum_, long _bossnowhp_, int _bossid_)
        {
            this.battleid = _battleid_;
            this.useherokeylist = _useherokeylist_;
            this.monstergroup = _monstergroup_;
            this.trooptype = _trooptype_;
            this.monstertrooptype = _monstertrooptype_;
            this.zhufunum = _zhufunum_;
            this.bossnowhp = _bossnowhp_;
            this.bossid = _bossid_;
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(battleid);
		    _os_.compact_uint32(useherokeylist.Count);
            foreach (DictionaryEntry de in useherokeylist)
            {
                _os_.marshal((int)de.Key);
                _os_.marshal((int)de.Value);
            }

            _os_.compact_uint32(monstergroup.Count);
            LinkedListNode<int> firstNode2 = monstergroup.First;
            while (firstNode2 != null)
            {
                _os_.marshal(monstergroup.First.Value);

                monstergroup.RemoveFirst();
                firstNode2 = monstergroup.First;
            }

            _os_.marshal(trooptype);
            _os_.marshal(monstertrooptype);
            _os_.marshal(zhufunum);
            _os_.marshal(bossnowhp);
            _os_.marshal(bossid);
    		return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            battleid = _os_.unmarshal_int();
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int key,value;
                key = _os_.unmarshal_int();
                value = _os_.unmarshal_int();
                useherokeylist.Add(key,value);
            }


            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_;
                _v_ = _os_.unmarshal_int();
                monstergroup.AddLast(_v_);
            }


            trooptype = _os_.unmarshal_int();
            monstertrooptype = _os_.unmarshal_int();

            zhufunum = _os_.unmarshal_int();
            bossnowhp = _os_.unmarshal_long();
            bossid = _os_.unmarshal_int();
  
            return _os_;
		}

	}
}
