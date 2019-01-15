using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;

namespace GNET
{
	public partial class SRefreshZiYuan: Protocol
	{

        public int shenglingzq; // Ê¥ÁéÖ®Èª
        public int ronglian; // ÈÛÁ¶µã
        public int huangjinxz; // »Æ½ðÑ«ÕÂ
        public int baijinxz; // °×½ðÑ«ÕÂ
        public int qingtongxz; // ÇàÍ­Ñ«ÕÂ
        public int chitiexz; // ³àÌúÑ«ÕÂ
        public int jyjiejing; // ¾­Ñé½á¾§

        public const int PROTOCOL_TYPE = 796449;

        public SRefreshZiYuan()
            : base(PROTOCOL_TYPE)
		 {
             shenglingzq = 0;
             ronglian = 0;
             huangjinxz = 0;
             baijinxz = 0;
             qingtongxz = 0;
             chitiexz = 0;
             jyjiejing = 0;
		 } 

		public override object Clone()
		{
            SRefreshZiYuan obj = new SRefreshZiYuan();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(shenglingzq);
            _os_.marshal(ronglian);
            _os_.marshal(huangjinxz);
            _os_.marshal(baijinxz);
            _os_.marshal(qingtongxz);
            _os_.marshal(chitiexz);
            _os_.marshal(jyjiejing);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            shenglingzq = _os_.unmarshal_int();
            ronglian = _os_.unmarshal_int();
            huangjinxz = _os_.unmarshal_int();
            baijinxz = _os_.unmarshal_int();
            qingtongxz = _os_.unmarshal_int();
            chitiexz = _os_.unmarshal_int();
            jyjiejing = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            ObjectSelf.GetInstance().HeroMoney = shenglingzq;
            ObjectSelf.GetInstance().RuneMoney = ronglian;
            ObjectSelf.GetInstance().ExpFruit  = jyjiejing;
            ObjectSelf.GetInstance().HuangjinXZ= huangjinxz;
            ObjectSelf.GetInstance().BaiJinXZ = baijinxz;
            ObjectSelf.GetInstance().QingTongXZ = qingtongxz;
            ObjectSelf.GetInstance().ChiTieXZ = chitiexz;

            GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_MoneyResource_Update);
		}
	}	
}
