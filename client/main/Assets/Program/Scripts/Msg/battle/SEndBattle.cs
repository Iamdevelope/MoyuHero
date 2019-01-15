using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using DreamFaction.UI.Core;
namespace GNET
{
	public partial class SEndBattle: Protocol
	{

        public const int END_OK = 1; // 成功
	    public const int END_ERROR = 2; // 失败

	    public int endtype;
        public int smid; // 神秘关卡或神秘商店ID
        public int time; // 倒计时时间（秒）
        public int zhangjie; // 所属章节

        public Hashtable moheshop; // 魔盒列表
        public Hashtable smshop; // 神秘商店随机出的物品（key为随机商店物品id，int为是否购买，0为未购买）


        public const int PROTOCOL_TYPE = 787943;

        public SEndBattle()
            : base(PROTOCOL_TYPE)
		 {
             endtype = 0;
             smid = 0;
             time = 0;
             zhangjie = 0;
             moheshop = new Hashtable();
             smshop = new Hashtable();
		 } 

		public override object Clone()
		{
            SEndBattle obj = new SEndBattle();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(endtype);
            _os_.marshal(smid);
            _os_.marshal(time);
            _os_.marshal(zhangjie);

            _os_.compact_uint32(moheshop.Count);  //魔盒
            foreach (DictionaryEntry de in moheshop)
            {
                _os_.marshal((int)de.Key);
                _os_.marshal((Mohe)de.Value);
            }

            _os_.compact_uint32(smshop.Count);  //神秘商店物品
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
            smid = _os_.unmarshal_int();
            time = _os_.unmarshal_int();
            zhangjie = _os_.unmarshal_int();

            //魔盒
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int key;
                key = _os_.unmarshal_int();

                Mohe _v_ = new Mohe();
                _v_.unmarshal(_os_);
                moheshop.Add(key, _v_);
            }

            //神秘商店
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
            ObjectSelf.GetInstance().SetIsOpenSealBox(false);
            if (moheshop.Count > 0)
            {
                ObjectSelf.GetInstance().MoheDataClear();
                ObjectSelf.GetInstance().SetIsOpenSealBox(true);
                int nCount = DataTemplate.GetInstance().m_BossboxTable.getDataCount();
                for (int i = 1; i <= nCount; i++)
                {
                    if (moheshop.ContainsKey(i))
                    {
                        Mohe data = moheshop[i] as Mohe;
                        ObjectSelf.GetInstance().MoheDataAdd(data);
                    }
                }
            }
            BattleStageMgr pData = ObjectSelf.GetInstance().BattleStageData;
            if (smid > 0)
            {

                //UnityEngine.Debug.Log("调试：强制修改smid至1310281000 hashTable:" + smshop.Count);
                //smid = 1310281000;
                //time = 600;
                pData.m_IsOpenSpecialStage = true;
                pData.m_SpecialStage.CopyData(smid, time, zhangjie);



                if (smid > 1299999999 && smid < 1400000000)//特殊关卡
                {
                    //GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_SpecialStageTips, true);
                    UI_FightControler.Inst.SetIsSpecialStage(true);

                }
                else                                        //触发神秘商店
                {
                    if (smshop.Count > 0)
                    {
                        pData.LoadMysteriousShop(smshop);
                        //GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_MysteriousShopSpecialTips,true);
                        UI_FightControler.Inst.SetIsMysteriousShop(true);
                    }
                }
            }

            if (endtype == END_OK)
            {
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_FightWin);
            }
        }
	}	
}
