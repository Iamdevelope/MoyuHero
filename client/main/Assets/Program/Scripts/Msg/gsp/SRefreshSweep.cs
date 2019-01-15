using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
namespace GNET
{
	public partial class SRefreshSweep: Protocol
	{

        public int sweephavenum; // ����ɨ��ʣ�����
        public int sweepbuyhavenum; // ����ɨ��ʣ�๺�����
        public int mszqgetnum; // ��˹����
        public int qiyuannum; // �ۼ���Ը̨����
        public int qiyuanallnum; // ��Ը�غϴ�������һ��Ϊ3����ɺ��Ϊ5
        public int isqiyuantoday; // ��λ�ǽ����Ƿ���Ը��ʮλΪ�Ƿ��ǩ��0�Ƿ�1Ϊ��

        public const int PROTOCOL_TYPE = 786456;

        public SRefreshSweep()
            : base(PROTOCOL_TYPE)
        {
            
        }

		public override object Clone()
		{
            SRefreshSweep obj = new SRefreshSweep();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(sweephavenum);
            _os_.marshal(sweepbuyhavenum);
            _os_.marshal(mszqgetnum);
            _os_.marshal(qiyuannum);
            _os_.marshal(qiyuanallnum);
            _os_.marshal(isqiyuantoday);
            return _os_;
		}

		public override OctetsStream unmarshal(OctetsStream _os_)
		{
            sweephavenum = _os_.unmarshal_int();
            sweepbuyhavenum = _os_.unmarshal_int();
            mszqgetnum = _os_.unmarshal_int();
            qiyuannum = _os_.unmarshal_int();
            qiyuanallnum = _os_.unmarshal_int();
            isqiyuantoday = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 4096; }

        public override void Process() 
        {
            ObjectSelf obj = ObjectSelf.GetInstance();
            if (obj != null)
            {
                obj.RapidClearNums = sweephavenum;
                obj.RapidClearBuyTimes = sweepbuyhavenum;
                obj.IsGetPower = mszqgetnum;
                obj.SacredAltarNum = qiyuannum;
                obj.ScaredAltarNumMax = qiyuanallnum;
                obj.ScaredAltarTypeNum = isqiyuantoday;
            }

            GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_StageSweepDataChange);
        }
	}	
}
