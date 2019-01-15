using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameEventSystem;
using UnityEngine;
namespace GNET
{
	public partial class SHeroMSExp: Protocol
	{

        public static int END_OK = 1; // �ɹ�
	    public static int END_NOT_OK = 2; // ʧ��

	    public int result; // ���

        public const int PROTOCOL_TYPE = 787789;

        public SHeroMSExp()
            : base(PROTOCOL_TYPE)
		 {

		 } 

		public override object Clone()
		{
            SHeroMSExp obj = new SHeroMSExp();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(result);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            if (result == 1)
            {
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_MysticSuccess);
            }
		}
	}	
}
