using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameEventSystem;
using UnityEngine;

namespace GNET
{
    public partial class SHeroEquipUp : Protocol
    {

        public static int END_OK = 1; // �ɹ�
        public static int END_NOT_OK = 2; // ʧ��

        public int result; // ���
        public int islevelup; // �Ƿ���������0Ϊ��ǿ������1Ϊ����
        public int isstrength; // �Ƿ�һ��ǿ����0Ϊ��1Ϊ��

        public const int PROTOCOL_TYPE = 787791;

        public SHeroEquipUp ()
            : base ( PROTOCOL_TYPE )
        {

        }

        public override object Clone ()
        {
            SHeroEquipUp obj = new SHeroEquipUp ();
            return obj;
        }

        public override OctetsStream marshal ( OctetsStream _os_ )
        {
            _os_.marshal ( result );
            _os_.marshal ( islevelup );
            _os_.marshal(isstrength);
            return _os_;
        }

        public override OctetsStream unmarshal ( OctetsStream _os_ )
        {
            result = _os_.unmarshal_int ();
            islevelup = _os_.unmarshal_int ();
            isstrength = _os_.unmarshal_int();
            return _os_;
        }

        public override int PriorPolicy ()
        {
            return 1;
        }

        public override bool SizePolicy ( int size )
        {
            return size <= 1024;
        }

        public override void Process ()
        {
            if ( result == END_OK )
            {
                if ( islevelup == 0 )
                {
                    GameEventDispatcher.Inst.dispatchEvent ( GameEventID.I_EquipStrengthen );
                }
                else
                {
                    GameEventDispatcher.Inst.dispatchEvent ( GameEventID.I_EquipLetGood );
                }
            }
            else
            {
            }

        }
    }
}
