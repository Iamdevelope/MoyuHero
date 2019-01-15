using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork;
using DreamFaction.LogSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GNET
{
	public partial class STuJianHeros: Protocol
	{

        public static int IS_NEW = 1; // 新增
	    public static int NOT_NEW = 0; // 主动获取

        public LinkedList<HeroTuJian> herotujian;
        public int isnew; // 是否新增，0为否（上线、主动获取），1为有新增
        public LinkedList<int> tujianbox;
        public LinkedList<int> tjheromaxlevel;// 满级图鉴新增列表

        public const int PROTOCOL_TYPE = 787762;

        public STuJianHeros()
            : base(PROTOCOL_TYPE)
		 {
             herotujian = new LinkedList<HeroTuJian>();
             isnew = 0;
             tujianbox = new LinkedList<int>();
             tjheromaxlevel = new LinkedList<int>();
		 } 

		public override object Clone()
		{
            STuJianHeros obj = new STuJianHeros();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.compact_uint32(herotujian.Count);
            LinkedListNode<HeroTuJian> firstNode2 = herotujian.First;
            while (firstNode2 != null)
            {
                _os_.marshal(herotujian.First.Value);

                herotujian.RemoveFirst();
                firstNode2 = herotujian.First;
            }
            _os_.marshal(isnew);

            _os_.compact_uint32(tujianbox.Count);
            LinkedListNode<int> firstNode = tujianbox.First;
            while (firstNode != null)
            {
                _os_.marshal(tujianbox.First.Value);

                tujianbox.RemoveFirst();
                firstNode = tujianbox.First;
            }

            _os_.compact_uint32(tjheromaxlevel.Count);
            LinkedListNode<int> firstNode3 = tjheromaxlevel.First;
            while (firstNode3 != null)
            {
                _os_.marshal(tjheromaxlevel.First.Value);

                tjheromaxlevel.RemoveFirst();
                firstNode3 = tjheromaxlevel.First;
            }
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                HeroTuJian _v_ = new HeroTuJian();
                _v_.unmarshal(_os_);
                herotujian.AddFirst(_v_);
            }
            isnew = _os_.unmarshal_int();

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_ ;
                _v_ = _os_.unmarshal_int();
                tujianbox.AddFirst(_v_);
            }

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_;
                _v_ = _os_.unmarshal_int();
                tjheromaxlevel.AddFirst(_v_);
            }

            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            ObjectSelf.GetInstance().SetHeroHandBookList(herotujian.ToList<HeroTuJian>());
            ObjectSelf.GetInstance().SetHandBookBoxList(tujianbox.ToList<int>());

            GameEventDispatcher.Inst.dispatchEvent(GameEventID.HB_GetSTuJianHeros);
            if (tjheromaxlevel.Count > 0)
            {
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.HB_GetMedalPop, tjheromaxlevel.ToList<int>());
            }
		}
	}	
}
