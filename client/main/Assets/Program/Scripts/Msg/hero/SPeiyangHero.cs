using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameEventSystem;
namespace GNET
{
	public partial class SPeiyangHero: Protocol
	{

        public static int END_OK = 1; // �ɹ�
	    public static int END_NOT_OK = 2; // ʧ��

	    public int result; // ���
        public byte slotnum; // ����λ��
        public byte isreset; // �Ƿ����ã�0Ϊ�����ã�1Ϊ���ã�

        public const int PROTOCOL_TYPE = 787780;

        public SPeiyangHero()
            : base(PROTOCOL_TYPE)
		 {

		 } 

		public override object Clone()
		{
            SPeiyangHero obj = new SPeiyangHero();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(result);
            _os_.marshal(slotnum);
            _os_.marshal(isreset);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            slotnum = _os_.unmarshal_byte();
            isreset = _os_.unmarshal_byte();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            if (result == END_OK)
            {
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.HE_PeiyangUp);

            }
		}
	}	
}
