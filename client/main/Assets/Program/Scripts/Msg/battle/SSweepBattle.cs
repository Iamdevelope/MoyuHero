using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork.Data;
namespace GNET
{
	public partial class SSweepBattle: Protocol
	{

        public const int END_OK = 1; // 成功
	    public const int END_ERROR = 2; // 失败

	    public int endtype;
        public int smid; // 神秘关卡或神秘商店ID
        public int time; // 倒计时时间（秒）
        public int zhangjie; // 所属章节

        public Hashtable moheshop; // 魔盒列表
        public Hashtable smshop; // 神秘商店随机出的物品（key为随机商店物品id，int为是否购买，0为未购买）

        public LinkedList<BattelInfo> battleinfolist; // 扫荡结果列表


        public const int PROTOCOL_TYPE = 787945;

        public SSweepBattle()
            : base(PROTOCOL_TYPE)
		 {
             endtype = 0;
             smid = 0;
             time = 0;
             zhangjie = 0;
             moheshop = new Hashtable();
             smshop = new Hashtable();
             battleinfolist = new LinkedList<BattelInfo>();
		 } 

		public override object Clone()
		{
            SSweepBattle obj = new SSweepBattle();
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
            _os_.compact_uint32(battleinfolist.Count);
            LinkedListNode<BattelInfo> firstNode = battleinfolist.First;
            while (firstNode != null)
            {
                _os_.marshal(battleinfolist.First.Value);

                battleinfolist.RemoveFirst();
                firstNode = battleinfolist.First;
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
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                BattelInfo _v_ = new BattelInfo();
                _v_.unmarshal(_os_);
                battleinfolist.AddFirst(_v_);
            }

            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
        {

            //CampaignMonsterGroupData pMonsterGroupData = new CampaignMonsterGroupData();
            //byte nGroup = 0;
            //int nIndex = 0;
            //foreach (int value in battleinfo.monstergroup)
            //{
            //    if (nIndex >= GlobalMembers.MAX_MONSTER_GROUP_COUNT || nGroup >= GlobalMembers.MAX_CAMPAIGN_MONSTER_GROUP)
            //        continue;
            //    if (value != 0)
            //    {
            //        pMonsterGroupData.IDs[nGroup, nIndex] = value;
            //        nIndex++;
            //    }
            //    else
            //    {
            //        //用0表示每波怪物的分界标示 [4/1/2015 Zmy]
            //        nIndex = 0;//重设索引
            //        nGroup++;
            //    }
            //}
            //List<int> _indroplist = new List<int>();
            //foreach (int value in battleinfolist)
            //{
            //    _indroplist.Add(value);
            //}
            //ObjectSelf.GetInstance().OnCacheCurrentBattleReward(battleinfo.heroexp, battleinfo.teamexp, battleinfo.tili, 0, _indroplist);
            //ObjectSelf.GetInstance().BattleStageData.CheckSpecialStageData(battleinfo.battleid);

            BattleStageMgr pData = ObjectSelf.GetInstance().BattleStageData;

            //if (smid > 0)
            //{
            //    pData.m_IsOpenSpecialStage = true;
            //    pData.m_SpecialStage.CopyData(smid, time, zhangjie);
            //}

            //if (moheshop.Count > 0)
            //{
            //    ObjectSelf.GetInstance().SetIsOpenSealBox(true);
            //}

            List<BattelInfo> _dropList = new List<BattelInfo>();
            foreach (BattelInfo item in battleinfolist)
            {
                _dropList.Add(item);
            }
//            UnityEngine.Debug.Log("扫荡返回信息");
            DreamFaction.GameEventSystem.GameEventDispatcher.Inst.
                dispatchEvent(DreamFaction.GameEventSystem.GameEventID.U_RapidClearRespond,_dropList);
        }
	}	
}
