using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
namespace GNET
{
	public partial class SRefreshVipLevel: Protocol
	{

        public byte data;
        public int vipexp;

        public const int PROTOCOL_TYPE = 787438;

        public SRefreshVipLevel()
            : base(PROTOCOL_TYPE)
		 {
             data = 0;
             vipexp = 0;
		 } 

		public override object Clone()
		{
            SRefreshVipLevel obj = new SRefreshVipLevel();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(data);
            _os_.marshal(vipexp);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            data = _os_.unmarshal_byte();
            vipexp = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size <= 1024; }

        public override void Process() 
        {
            int lv = ObjectSelf.GetInstance().VipLevel;
            ObjectSelf.GetInstance().VipExp = vipexp;

            if (lv < data)
            {
                do
                {
                    lv++;
                    UI_VipLvUpMgr.AddPopupQueue(lv);
                } while (lv < data);

                ObjectSelf.GetInstance().VipLevel = data;
                DreamFaction.UI.Core.UI_HomeControler.NeedShowVipLvUpTips = true;
            }
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_VipLevel_Update);
        }
	}	
}
