using System;
using System.Collections;
using System.Collections.Generic;
namespace GNET
{
    public class stagetxall : Marshal
	{
        public Hashtable teamallmap;        // 探险任务小队表，key小队id（从1开始），value小队英雄key列表
        public Hashtable stagetxallmap; // 探险任务总表，key是章节ID(从1开始)，value是章节探险列表

        public stagetxall()
        {
            teamallmap = new Hashtable();
            stagetxallmap = new Hashtable();
        }

        public stagetxall(Hashtable _teamallmap_, Hashtable _stagetxallmap_)
        {
            this.teamallmap = _teamallmap_;
            this.stagetxallmap = _stagetxallmap_;
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.compact_uint32(teamallmap.Count);
            foreach (DictionaryEntry de in teamallmap)
            {
                _os_.marshal((int)de.Key);
                _os_.marshal((teamtanxian)de.Value);
            }

            _os_.compact_uint32(stagetxallmap.Count);
            foreach (DictionaryEntry de in stagetxallmap)
            {
                _os_.marshal((int)de.Key);
                _os_.marshal((stagetanxian)de.Value);
            }
        
        
    		return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int key;
                teamtanxian value = new teamtanxian();
                key = _os_.unmarshal_int();
                value.unmarshal(_os_);
                teamallmap.Add(key, value);
            }

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int key;
                stagetanxian value = new stagetanxian();
                key = _os_.unmarshal_int();
                value.unmarshal(_os_);
                stagetxallmap.Add(key, value);
            }

            return _os_;
		}

	}
}
