using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork.Data;

namespace GNET
{
    public class defenceInfo : Marshal
	{
        public byte m_defencer; // 所选目标
        public byte m_hit; // 命中
        public byte m_nimpactcount; // 最终影响目标的数量

        public LinkedList<int> m_impact; // 受影响数组，上限是16个目标！
        public long m_remainhp; // 剩余血量
        

      

        public defenceInfo()
        {
            m_defencer = 0;
            m_hit = 0;
            m_nimpactcount = 0;
            m_impact = new LinkedList<int>();
            m_remainhp = 0;
        }

        public defenceInfo(byte _m_defencer_, byte _m_hit_, byte _m_nimpactcount_, 
                            LinkedList<int> _m_impact_, int _m_remainhp_
                          )
        {
            this.m_defencer = _m_defencer_;
            this.m_hit = _m_hit_;
            this.m_nimpactcount = _m_nimpactcount_;
            this.m_impact = _m_impact_;
            this.m_remainhp = _m_remainhp_;
       
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(m_defencer);
            _os_.marshal(m_hit);
            _os_.marshal(m_nimpactcount);

            _os_.compact_uint32(m_impact.Count);
            LinkedListNode<int> firstNode2 = m_impact.First;
            while (firstNode2 != null)
            {
                _os_.marshal(m_impact.First.Value);

                m_impact.RemoveFirst();
                firstNode2 = m_impact.First;
            }

            _os_.marshal(m_remainhp);      
        
    		return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            m_defencer = _os_.unmarshal_byte();
            m_hit = _os_.unmarshal_byte();
            m_nimpactcount = _os_.unmarshal_byte();

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_;
                _v_ = _os_.unmarshal_int();
                m_impact.AddLast(_v_);
            }

            m_remainhp = _os_.unmarshal_long();
            

  
            return _os_;
		}
	}
}
