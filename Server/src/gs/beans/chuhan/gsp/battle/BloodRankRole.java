
package chuhan.gsp.battle;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class BloodRankRole implements Marshal {
	public long roleid;
	public java.lang.String rolename;
	public short rolelevel;
	public short maxlevel;
	public java.util.LinkedList<Integer> troopheros;

	public BloodRankRole() {
		rolename = "";
		troopheros = new java.util.LinkedList<Integer>();
	}

	public BloodRankRole(long _roleid_, java.lang.String _rolename_, short _rolelevel_, short _maxlevel_, java.util.LinkedList<Integer> _troopheros_) {
		this.roleid = _roleid_;
		this.rolename = _rolename_;
		this.rolelevel = _rolelevel_;
		this.maxlevel = _maxlevel_;
		this.troopheros = _troopheros_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(roleid);
		_os_.marshal(rolename, "UTF-16LE");
		_os_.marshal(rolelevel);
		_os_.marshal(maxlevel);
		_os_.compact_uint32(troopheros.size());
		for (Integer _v_ : troopheros) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		roleid = _os_.unmarshal_long();
		rolename = _os_.unmarshal_String("UTF-16LE");
		rolelevel = _os_.unmarshal_short();
		maxlevel = _os_.unmarshal_short();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			troopheros.add(_v_);
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof BloodRankRole) {
			BloodRankRole _o_ = (BloodRankRole)_o1_;
			if (roleid != _o_.roleid) return false;
			if (!rolename.equals(_o_.rolename)) return false;
			if (rolelevel != _o_.rolelevel) return false;
			if (maxlevel != _o_.maxlevel) return false;
			if (!troopheros.equals(_o_.troopheros)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)roleid;
		_h_ += rolename.hashCode();
		_h_ += rolelevel;
		_h_ += maxlevel;
		_h_ += troopheros.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(roleid).append(",");
		_sb_.append("T").append(rolename.length()).append(",");
		_sb_.append(rolelevel).append(",");
		_sb_.append(maxlevel).append(",");
		_sb_.append(troopheros).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

