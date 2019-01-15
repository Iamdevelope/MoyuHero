using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamFaction.GameNetWork;

namespace GNET
{
	public partial class STodayEndless: Protocol
	{
        public int isend; // 0未开始，1进行中，2结束
        public int groupnum; // 第几轮
        public int alldropnum; // 勇者证明总数
        public int pact; // 强者之约（没有则为-1）
        public int ishalfcostpact; // 本次购买是否半价（0是本次全价，1是半价）
        public int paiming; // 预测今日排名（-1未排名，1~20为具体排名，20以上为20名之外）
        public int isnotfirst; // 不是第一次战斗（0是第一次战斗，1不是第一次战斗）

        public const int PROTOCOL_TYPE = 788941;

        public STodayEndless()
            : base(PROTOCOL_TYPE)
		 {

		 } 

		public override object Clone()
		{
            STodayEndless obj = new STodayEndless();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(isend);
            _os_.marshal(groupnum);
            _os_.marshal(alldropnum);
            _os_.marshal(pact);
            _os_.marshal(ishalfcostpact);
            _os_.marshal(paiming);
            _os_.marshal(isnotfirst);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            isend = _os_.unmarshal_int();
            groupnum = _os_.unmarshal_int();
            alldropnum = _os_.unmarshal_int();
            pact = _os_.unmarshal_int();
            ishalfcostpact = _os_.unmarshal_int();
            paiming = _os_.unmarshal_int();
            isnotfirst = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
        {
           if (isend == 1)
           {
               ObjectSelf.GetInstance().LimitFightMgr.m_bRuntime = true;
           }
           else if(isend == 2)
           {
               ObjectSelf.GetInstance().LimitFightMgr.m_bRuntime = false;
               ObjectSelf.GetInstance().LimitFightMgr.Activate = false;
           }

           ObjectSelf.GetInstance().LimitFightMgr.m_RoundNum = groupnum;
           ObjectSelf.GetInstance().LimitFightMgr.m_AllDropNum = alldropnum;
           ObjectSelf.GetInstance().LimitFightMgr.m_Pact = pact;
           ObjectSelf.GetInstance().LimitFightMgr.m_IsHalfCostPact = ishalfcostpact;
           ObjectSelf.GetInstance().LimitFightMgr.m_TodayRanking = paiming;

        }
	}	
}
