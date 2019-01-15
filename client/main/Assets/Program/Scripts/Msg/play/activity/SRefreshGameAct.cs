using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using DreamFaction;
namespace GNET
{
	public partial class SRefreshGameAct: Protocol
	{

        public Hashtable gameactivitymap;        // 活动（key为活动ID，value为gactivity）

        public const int PROTOCOL_TYPE = 789048;

        public SRefreshGameAct()
            : base(PROTOCOL_TYPE)
		 {
             gameactivitymap = new Hashtable();
		 } 

		public override object Clone()
		{
            SRefreshGameAct obj = new SRefreshGameAct();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.compact_uint32(gameactivitymap.Count);
            foreach (DictionaryEntry de in gameactivitymap)
            {
                _os_.marshal((int)de.Key);
                _os_.marshal((gactivity)de.Value);
            }
            
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int key;
                gactivity value = new gactivity();
                key = _os_.unmarshal_int();
                value.unmarshal(_os_);
                gameactivitymap.Add(key, value);
            }
            
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 655350; }

        public override void Process() 
		{
            ObjectSelf.GetInstance().GetActivityOverviewMar().CopyInfo(gameactivitymap);
		}
	}	
}
