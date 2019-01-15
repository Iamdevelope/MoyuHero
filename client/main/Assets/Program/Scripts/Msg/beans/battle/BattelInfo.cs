using System;
using System.Collections;
using System.Collections.Generic;
namespace GNET
{
    public class BattelInfo : Marshal
	{
        public int battleid;                    // 关卡id
        public Hashtable useherokeylist;        // 使用英雄id和位置（key为位置，value为herokey）
        public LinkedList<int> monstergroup;    //怪物组
        public int heroexp; // 英雄经验
        public int teamexp; // 队伍经验
        public int tili; // 消耗体力
        public int trooptype; // 战队类型
        public LinkedList<int> indroplist; // 掉落小包组
        

      

        public BattelInfo()
        {
            useherokeylist = new Hashtable();
            monstergroup = new LinkedList<int>();
            heroexp = 0;
            teamexp = 0;
            tili = 0;
            trooptype = 0;
            indroplist = new LinkedList<int>();
        }

        public BattelInfo(int _battleid_, Hashtable _useherokeylist_,
                          LinkedList<int> _monstergroup_,
                          int _heroexp_, int _teamexp_, int _trooptype_, int _tili_, LinkedList<int> _indroplist_)
        {
            this.battleid = _battleid_;
            this.useherokeylist = _useherokeylist_;
            this.heroexp = _heroexp_;
            this.teamexp = _teamexp_;
            this.trooptype = _trooptype_;
            this.tili = _tili_;
            this.monstergroup = _monstergroup_;
            this.indroplist = _indroplist_;
       
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

            _os_.marshal(heroexp);
            _os_.marshal(teamexp);
            _os_.marshal(tili);
            _os_.marshal(trooptype);

            _os_.compact_uint32(indroplist.Count);
            LinkedListNode<int> firstNode3 = indroplist.First;
            while (firstNode3 != null)
            {
                _os_.marshal(indroplist.First.Value);

                indroplist.RemoveFirst();
                firstNode3 = indroplist.First;
            }
            
        
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
      
    
            heroexp = _os_.unmarshal_int();
            teamexp = _os_.unmarshal_int();
            
            tili = _os_.unmarshal_int();
            trooptype = _os_.unmarshal_int();

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_;
                _v_ = _os_.unmarshal_int();
                indroplist.AddLast(_v_);
            }

  
            return _os_;
		}

	}
}
