using System;
using System.Collections;
using System.Collections.Generic;
namespace GNET
{
    public class EndlessRankInfo : Marshal
	{
        public long roleid; // 玩家guid
        public string rolename; // 玩家名称
        public int level; // 玩家等级
        public int groupnum; // 第几轮
        public int trooptype; // 战队类型
        public int alldropnum; // 勇者证明总数量
        public Hashtable heroattribute; // 使用英雄的位置和属性信息（key为位置，value为OtherHero属性信息）
        public int onranknum; // 连续在榜次数
        

      

        public EndlessRankInfo()
        {
            rolename = "";
            heroattribute = new Hashtable();
        }

        public EndlessRankInfo(long _roleid_,string _rolename_, int _level_, int _groupnum_, int _trooptype_, int _alldropnum_,
            Hashtable _heroattribute_, int _onranknum_)
        {
            this.roleid = _roleid_;
            this.rolename = _rolename_;
		    this.level = _level_;
		    this.groupnum = _groupnum_;
	    	this.trooptype = _trooptype_;
	    	this.alldropnum = _alldropnum_;
		    this.heroattribute = _heroattribute_;
		    this.onranknum = _onranknum_;
       
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(roleid);
            _os_.marshal(rolename);
            _os_.marshal(level);
            _os_.marshal(groupnum);
            _os_.marshal(trooptype);
            _os_.marshal(alldropnum);
            _os_.compact_uint32(heroattribute.Count);
            foreach (DictionaryEntry de in heroattribute)
            {
                _os_.marshal((int)de.Key);
                _os_.marshal((OtherHero)de.Value);
            }

            _os_.marshal(onranknum); ;         
        
    		return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            roleid = _os_.unmarshal_long();
            rolename = _os_.unmarshal_String();
            level = _os_.unmarshal_int();
            groupnum = _os_.unmarshal_int();
            trooptype = _os_.unmarshal_int();
            alldropnum = _os_.unmarshal_int();
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int key;
                key = _os_.unmarshal_int();
                OtherHero _v_ = new OtherHero();
                _v_.unmarshal(_os_);
                heroattribute.Add(key, _v_);
            }


            onranknum = _os_.unmarshal_int();
            

  
            return _os_;
		}

	}
}
