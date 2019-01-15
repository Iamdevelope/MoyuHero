using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
namespace GNET
{
	public partial class SRefreshTili: Protocol
	{

        public int tili; // 体力值
        public int titime; // 体力更新剩余时间
        public int xingdongti; // 行动力值
        public int xingdongtitime; // 行动力更新剩余时间
        public int jineng; // 技能点值
        public int jinengtime; // 技能点更新剩余时间

        public const int PROTOCOL_TYPE = 787441;

        public SRefreshTili()
            : base(PROTOCOL_TYPE)
		 {
             tili = 0;
             titime = 0;
		 } 

		public override object Clone()
		{
            SRefreshTili obj = new SRefreshTili();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(tili);
            _os_.marshal(titime);
            _os_.marshal(xingdongti);
            _os_.marshal(xingdongtitime);
            _os_.marshal(jineng);
            _os_.marshal(jinengtime);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            tili = _os_.unmarshal_int();
            titime = _os_.unmarshal_int();
            xingdongti = _os_.unmarshal_int();
            xingdongtitime = _os_.unmarshal_int();
            jineng = _os_.unmarshal_int();
            jinengtime = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
        {
            ObjectSelf.GetInstance().ActionPoint = tili;
            ObjectSelf.GetInstance().TiTime = titime;

            ObjectSelf.GetInstance().ExplorePoint = xingdongti;
            ObjectSelf.GetInstance().ExplorePointRefreshTimes = xingdongtitime;

            ObjectSelf.GetInstance().SkillPoint = jineng;
            ObjectSelf.GetInstance().SkillPointRefreshTime = jinengtime;

            GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_ActionPoint_Update);
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_ExplorePoint_Update);
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_SkillPoint_Update);
        }
	}	
}
