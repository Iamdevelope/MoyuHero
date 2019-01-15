
package chuhan.gsp;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

/** 战斗信息 by yanglk
*/
public class fightInfo implements Marshal {
	public byte m_attacker; // 发动攻击的GUID
	public int m_spellid; // 当前使用的技能id(有可能无效,如果无效需要继续判断子技能id)-->
	public byte m_ncount; // 目标数量-->
	public byte m_nimpactcount; // 最终impact数量-->
	public java.util.LinkedList<Integer> m_impact; // -->
	public java.util.LinkedList<chuhan.gsp.defenceInfo> m_defenceinfo; // 防御方信息

	public fightInfo() {
		m_impact = new java.util.LinkedList<Integer>();
		m_defenceinfo = new java.util.LinkedList<chuhan.gsp.defenceInfo>();
	}

	public fightInfo(byte _m_attacker_, int _m_spellid_, byte _m_ncount_, byte _m_nimpactcount_, java.util.LinkedList<Integer> _m_impact_, java.util.LinkedList<chuhan.gsp.defenceInfo> _m_defenceinfo_) {
		this.m_attacker = _m_attacker_;
		this.m_spellid = _m_spellid_;
		this.m_ncount = _m_ncount_;
		this.m_nimpactcount = _m_nimpactcount_;
		this.m_impact = _m_impact_;
		this.m_defenceinfo = _m_defenceinfo_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.defenceInfo _v_ : m_defenceinfo)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(m_attacker);
		_os_.marshal(m_spellid);
		_os_.marshal(m_ncount);
		_os_.marshal(m_nimpactcount);
		_os_.compact_uint32(m_impact.size());
		for (Integer _v_ : m_impact) {
			_os_.marshal(_v_);
		}
		_os_.compact_uint32(m_defenceinfo.size());
		for (chuhan.gsp.defenceInfo _v_ : m_defenceinfo) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		m_attacker = _os_.unmarshal_byte();
		m_spellid = _os_.unmarshal_int();
		m_ncount = _os_.unmarshal_byte();
		m_nimpactcount = _os_.unmarshal_byte();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			m_impact.add(_v_);
		}
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.defenceInfo _v_ = new chuhan.gsp.defenceInfo();
			_v_.unmarshal(_os_);
			m_defenceinfo.add(_v_);
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof fightInfo) {
			fightInfo _o_ = (fightInfo)_o1_;
			if (m_attacker != _o_.m_attacker) return false;
			if (m_spellid != _o_.m_spellid) return false;
			if (m_ncount != _o_.m_ncount) return false;
			if (m_nimpactcount != _o_.m_nimpactcount) return false;
			if (!m_impact.equals(_o_.m_impact)) return false;
			if (!m_defenceinfo.equals(_o_.m_defenceinfo)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)m_attacker;
		_h_ += m_spellid;
		_h_ += (int)m_ncount;
		_h_ += (int)m_nimpactcount;
		_h_ += m_impact.hashCode();
		_h_ += m_defenceinfo.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(m_attacker).append(",");
		_sb_.append(m_spellid).append(",");
		_sb_.append(m_ncount).append(",");
		_sb_.append(m_nimpactcount).append(",");
		_sb_.append(m_impact).append(",");
		_sb_.append(m_defenceinfo).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

