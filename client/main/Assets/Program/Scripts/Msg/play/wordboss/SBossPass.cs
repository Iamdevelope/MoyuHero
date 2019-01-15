using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamFaction.GameNetWork;
using DreamFaction.GameNetWork.Data;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;

namespace GNET
{
	public partial class SBossPass: Protocol
	{
        public const int END_OK = 1; // �ɹ�
        public const int END_ERROR = 2; // ʧ��
        public const int END_ISKILL = 3; // BOSS�����˻�ɱ
        public const int END_KILLBOSS = 4; // ��ɱBOSS

	    public int result;
        public int bossid; // ֵΪ1234������ڼ���boss
        public bossrole mywordboss;
        public Hashtable dropmap; // ���棬keyΪ��Ʒ����ԴID��valueΪ����
        public string bosskillname; // ��ɱ������

        public const int PROTOCOL_TYPE = 788890;

        public SBossPass()
            : base(PROTOCOL_TYPE)
		 {
             mywordboss = new bossrole();
             dropmap = new Hashtable();
             bosskillname = "";
 
		 } 

		public override object Clone()
		{
            SBossPass obj = new SBossPass();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(result);
            _os_.marshal(bossid);
            _os_.marshal(mywordboss);

            _os_.compact_uint32(dropmap.Count);
            foreach (DictionaryEntry de in dropmap)
            {
                _os_.marshal((int)de.Key);
                _os_.marshal((int)de.Value);
            }
            _os_.marshal(bosskillname);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            bossid = _os_.unmarshal_int();
            mywordboss.unmarshal(_os_);

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int key,value;
                key = _os_.unmarshal_int();
                value = _os_.unmarshal_int();
                dropmap.Add(key, value);
            }
            bosskillname = _os_.unmarshal_String();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
        {
            if (result == END_ERROR)
            {
            } 
            else
            {
                ObjectSelf.GetInstance().WorldBossMgr.RefeashBossRole(bossid, mywordboss);
                ObjectSelf.GetInstance().WorldBossMgr.m_bStartEnter = false;

                ObjectSelf.GetInstance().WorldBossMgr.m_DropItemMap.Clear();
                foreach (DictionaryEntry item in dropmap)
                {
                    ObjectSelf.GetInstance().WorldBossMgr.m_DropItemMap.Add((int)item.Key, (int)item.Value);
                }
                BossPassDataPack pack = new BossPassDataPack();
                pack.m_BossID = bossid;
                pack.m_Result = result;
                pack.m_BossKillName = bosskillname;
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_BossPass, pack);

            }

        }
	}	
}
