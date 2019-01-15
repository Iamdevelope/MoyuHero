using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork.Data;

namespace GNET
{
    public class fightInfo : Marshal
	{
        public byte m_attacker;     // ����������GUID
        public int  m_spellid;      // ��ǰʹ�õļ���id(�п�����Ч,�����Ч��Ҫ�����ж��Ӽ���id)
        public byte m_ncount;       // Ŀ������
        public byte m_nimpactcount; // ����impact����

        public LinkedList<int> m_impact; // ��Ӱ�����飬������16��Ŀ�꣡
        public LinkedList<defenceInfo> m_defenceinfo; // ��������Ϣ
        

      

        public fightInfo()
        {
            m_attacker = 0;
            m_spellid = 0;
            m_ncount = 0;
            m_nimpactcount = 0;
            m_impact = new LinkedList<int>();
            m_defenceinfo = new LinkedList<defenceInfo>();
        }

        public fightInfo(byte _m_attacker_, int _m_spellid_, byte _m_ncount_, byte _m_nimpactcount_, 
                           LinkedList<int> _m_impact_, 
                           LinkedList<defenceInfo> _m_defenceinfo_
                          )
        {
            this.m_attacker = _m_attacker_;
            this.m_spellid = _m_spellid_;
            this.m_ncount = _m_ncount_;
            this.m_nimpactcount = _m_nimpactcount_;
            this.m_impact = _m_impact_;
            this.m_defenceinfo = _m_defenceinfo_;
       
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(m_attacker);
            _os_.marshal(m_spellid);
            _os_.marshal(m_ncount);
            _os_.marshal(m_nimpactcount);

            _os_.compact_uint32(m_impact.Count);
            LinkedListNode<int> firstNode2 = m_impact.First;
            while (firstNode2 != null)
            {
                _os_.marshal(m_impact.First.Value);

                m_impact.RemoveFirst();
                firstNode2 = m_impact.First;
            }

            _os_.compact_uint32(m_defenceinfo.Count);
            LinkedListNode<defenceInfo> firstNode = m_defenceinfo.First;
            while (firstNode != null)
            {
                _os_.marshal(m_defenceinfo.First.Value);

                m_defenceinfo.RemoveFirst();
                firstNode = m_defenceinfo.First;
            }     
        
    		return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            m_attacker = _os_.unmarshal_byte();
            m_spellid = _os_.unmarshal_int();
            m_ncount = _os_.unmarshal_byte();
            m_nimpactcount = _os_.unmarshal_byte();

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_;
                _v_ = _os_.unmarshal_int();
                m_impact.AddLast(_v_);
            }
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                defenceInfo _v_ = new defenceInfo();
                _v_.unmarshal(_os_);
                m_defenceinfo.AddLast(_v_);
            } 
            return _os_;
		}
	}
}
