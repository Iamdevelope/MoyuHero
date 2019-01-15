using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
namespace GNET
{
	public partial class SBuySmShop: Protocol
	{

        public const int END_OK = 1; // 成功
	    public const int END_ERROR = 2; // 失败

        public int endtype;
        public int smshopid; // 成功购买的神秘商店ID
        public Hashtable smshop; // 魔盒列表



        public const int PROTOCOL_TYPE = 787951;

        public SBuySmShop()
            : base(PROTOCOL_TYPE)
		 {
             endtype = 0;
             smshopid = 0;
             smshop = new Hashtable();

		 } 

		public override object Clone()
		{
            SBuySmShop obj = new SBuySmShop();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(endtype);
            _os_.marshal(smshopid);

            _os_.compact_uint32(smshop.Count);  //魔盒
            foreach (DictionaryEntry de in smshop)
            {
                _os_.marshal((int)de.Key);
                _os_.marshal((Smshopdata)de.Value);
            }
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            endtype = _os_.unmarshal_int();
            smshopid = _os_.unmarshal_int();

            //魔盒
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int key;
                key = _os_.unmarshal_int();

                Smshopdata _v_ = new Smshopdata();
                _v_.unmarshal(_os_);
                smshop.Add(key, _v_);
            }

            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
        {
            BattleStageMgr pData = ObjectSelf.GetInstance().BattleStageData;
            if (smshop.Count > 0)
            {
                pData.LoadMysteriousShop(smshop);

            }
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_MysteriousShopBuyReplay, endtype);
        }
	}	
}
