using System;
using System.Collections;
using System.Collections.Generic;
namespace GNET
{
    public class endlessBattleInfo : Marshal
	{
        public int battleid;                    // 关卡id
        public int groupnum;                    // 第几轮
        public Hashtable useherokeylist;        // 使用英雄id和位置（key为位置，value为herokey）
        public LinkedList<int> monstergroup;    //怪物组
        public int trooptype; // 战队类型
        public int monstertrooptype; // 怪物战队类型
        public int pact; // 强者之约（没有则为-1）
        

      

        public endlessBattleInfo()
        {
            useherokeylist = new Hashtable();
            monstergroup = new LinkedList<int>();
        }

        public endlessBattleInfo(int _battleid_, int _groupnum_, Hashtable _useherokeylist_,
                          LinkedList<int> _monstergroup_,
                          int _trooptype_, int _monstertrooptype_, int _pact_)
        {
            this.battleid = _battleid_;
            this.groupnum = _groupnum_;
            this.useherokeylist = _useherokeylist_;
            this.monstergroup = _monstergroup_;
            this.trooptype = _trooptype_;
            this.monstertrooptype = _monstertrooptype_;
            this.pact = _pact_;
       
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(battleid);
            _os_.marshal(groupnum);
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
            _os_.marshal(pact);         
        
    		return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            battleid = _os_.unmarshal_int();
            groupnum = _os_.unmarshal_int();
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

            pact = _os_.unmarshal_int();
            

  
            return _os_;
		}

	}
}
