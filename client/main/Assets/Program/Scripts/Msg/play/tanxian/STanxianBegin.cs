using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using UnityEngine;
using DreamFaction;
namespace GNET
{
	public partial class STanxianBegin: Protocol
	{
        public const int END_OK = 1; // 成功
	    public const int END_ERROR = 2; // 失败

        public int result;
        public LinkedList<int> team; // 小队英雄key列表
        public int tanxianid; // 探险id
        public long endtime; // 结束时间

        public const int PROTOCOL_TYPE = 788985;

        public STanxianBegin()
            : base(PROTOCOL_TYPE)
		 {
             team = new LinkedList<int>();
             result = 0;
             tanxianid = 0;
             endtime = 0;
		 } 

		public override object Clone()
		{
            STanxianBegin obj = new STanxianBegin();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(result);

            _os_.compact_uint32(team.Count);
            LinkedListNode<int> firstNode = team.First;
            while (firstNode != null)
            {
                _os_.marshal(team.First.Value);

                team.RemoveFirst();
                firstNode = team.First;
            }
            _os_.marshal(tanxianid);
            _os_.marshal(endtime);
            
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_ ;
                _v_ = _os_.unmarshal_int();
                team.AddFirst(_v_);
            }
            tanxianid = _os_.unmarshal_int();
            endtime = _os_.unmarshal_long();
            
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            switch (result)
            {
                case END_OK:
                    //Debug.LogError("探险成功");
                    GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_ExploreTeamBeginTasks);
                    break;
                case END_ERROR:
                    //Debug.LogError("探险失败");
                    break;
                default:
                    break;
            }
			
		}
			
	}	
}
