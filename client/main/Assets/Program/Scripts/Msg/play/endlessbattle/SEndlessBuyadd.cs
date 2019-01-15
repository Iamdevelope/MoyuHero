using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;


namespace GNET
{
	public partial class SEndlessBuyadd: Protocol
	{
        public const int END_OK = 1; // 成功
        public const int END_ERROR = 2; // 失败

	    public int result;
        public int add1; // 属性1购买次数
        public int add2; // 属性2购买次数
        public int add3; // 属性3购买次数
        public int add4; // 属性4购买次数（仅计数）


        public const int PROTOCOL_TYPE = 788943;

        public SEndlessBuyadd()
            : base(PROTOCOL_TYPE)
		 {
		 } 

		public override object Clone()
		{
            SEndlessBuyadd obj = new SEndlessBuyadd();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(result);
            _os_.marshal(add1);
            _os_.marshal(add2);
            _os_.marshal(add3);
            _os_.marshal(add4);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            add1 = _os_.unmarshal_int();
            add2 = _os_.unmarshal_int();
            add3 = _os_.unmarshal_int();
            add4 = _os_.unmarshal_int();
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
               ObjectSelf.GetInstance().LimitFightMgr.m_AttrAdd1 = add1;
               ObjectSelf.GetInstance().LimitFightMgr.m_AttrAdd2 = add2;
               ObjectSelf.GetInstance().LimitFightMgr.m_AttrAdd3 = add3;
               if (ObjectSelf.GetInstance().LimitFightMgr.m_AttrAdd4 < add4)
               {
                   ObjectSelf.GetInstance().LimitFightMgr.m_AttrAdd4 = add4;
                   SceneObjectManager.GetInstance().OnHealAllHero();
               }
               

               UI_LimitFight.isBuy = true;
               GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_LimitAddEnd);

           }
        }
	}	
}
