using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
	public partial class CArtifactAddHero: Protocol
	{
        public int artifacttype; // …Ò∆˜¿‡–Õ£®key£©
        public LinkedList<int> herokeylist;


        public const int PROTOCOL_TYPE = 787772;

        public CArtifactAddHero()
            : base(PROTOCOL_TYPE)
		 {
             herokeylist = new LinkedList<int>();
		 } 

		public override object Clone()
		{
            CArtifactAddHero obj = new CArtifactAddHero();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(artifacttype);
            _os_.compact_uint32(herokeylist.Count);
            LinkedListNode<int> firstNode = herokeylist.First;
            while (firstNode != null)
            {
                _os_.marshal(herokeylist.First.Value);

                herokeylist.RemoveFirst();
                firstNode = herokeylist.First;
            }
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            artifacttype = _os_.unmarshal_int();
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int herokey = _os_.unmarshal_int();
                herokeylist.AddFirst(herokey);
            }
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() { }
	}	
}
