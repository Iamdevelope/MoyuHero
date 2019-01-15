
package chuhan.gsp;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class defenceInfo implements Marshal {
	public byte m_defencer; // 所选目标
	public byte m_hit; // 命中-->
	public byte m_nimpactcount; // 最终影响目标的数量-->
	public java.util.LinkedList<Integer> m_impact; // 受影响数组，上限是16个目标！-->
	public long m_remainhp; // 剩余血量

	public defenceInfo() {
		m_impact = new java.util.LinkedList<Integer>();
	}

	public defenceInfo(byte _m_defencer_, byte _m_hit_, byte _m_nimpactcount_, java.util.LinkedList<Integer> _m_impact_, long _m_remainhp_) {
		this.m_defencer = _m_defencer_;
		this.m_hit = _m_hit_;
		this.m_nimpactcount = _m_nimpactcount_;
		this.m_impact = _m_impact_;
		this.m_remainhp = _m_remainhp_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(m_defencer);
		_os_.marshal(m_hit);
		_os_.marshal(m_nimpactcount);
		_os_.compact_uint32(m_impact.size());
		for (Integer _v_ : m_impact) {
			_os_.marshal(_v_);
		}
		_os_.marshal(m_remainhp);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		m_defencer = _os_.unmarshal_byte();
		m_hit = _os_.unmarshal_byte();
		m_nimpactcount = _os_.unmarshal_byte();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			m_impact.add(_v_);
		}
		m_remainhp = _os_.unmarshal_long();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof defenceInfo) {
			defenceInfo _o_ = (defenceInfo)_o1_;
			if (m_defencer != _o_.m_defencer) return false;
			if (m_hit != _o_.m_hit) return false;
			if (m_nimpactcount != _o_.m_nimpactcount) return false;
			if (!m_impact.equals(_o_.m_impact)) return false;
			if (m_remainhp != _o_.m_remainhp) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)m_defencer;
		_h_ += (int)m_hit;
		_h_ += (int)m_nimpactcount;
		_h_ += m_impact.hashCode();
		_h_ += (int)m_remainhp;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(m_defencer).append(",");
		_sb_.append(m_hit).append(",");
		_sb_.append(m_nimpactcount).append(",");
		_sb_.append(m_impact).append(",");
		_sb_.append(m_remainhp).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

