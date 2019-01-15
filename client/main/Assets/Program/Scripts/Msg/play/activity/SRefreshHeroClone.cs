using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using System.Linq;
namespace GNET
{
	public partial class SRefreshHeroClone: Protocol
	{

        public LinkedList<int> heroclonelist;

        public const int PROTOCOL_TYPE = 789035;

        public SRefreshHeroClone()
            : base(PROTOCOL_TYPE)
		 {
             heroclonelist = new LinkedList<int>();

		 } 

		public override object Clone()
		{
            SRefreshHeroClone obj = new SRefreshHeroClone();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.compact_uint32(heroclonelist.Count);
            LinkedListNode<int> firstNode = heroclonelist.First;
            while (firstNode != null)
            {
                _os_.marshal(heroclonelist.First.Value);

                heroclonelist.RemoveFirst();
                firstNode = heroclonelist.First;
            }

           
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_ = _os_.unmarshal_int();
                heroclonelist.AddFirst(_v_);
            }

            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            List<int> nHeroCloneList = heroclonelist.ToList<int>();
            ObjectSelf.GetInstance().SetHeroCloneList(nHeroCloneList);
		}
			
	}	
}
