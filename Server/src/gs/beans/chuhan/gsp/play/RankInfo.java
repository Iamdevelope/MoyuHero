
package chuhan.gsp.play;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class RankInfo implements Marshal {
	public long roleid;
	public short rolelv;
	public int value;
	public java.lang.String rolename;
	public short campid; // 阵营，阵营战用
	public java.util.LinkedList<Integer> heroids;

	public RankInfo() {
		rolename = "";
		heroids = new java.util.LinkedList<Integer>();
	}

	public RankInfo(long _roleid_, short _rolelv_, int _value_, java.lang.String _rolename_, short _campid_, java.util.LinkedList<Integer> _heroids_) {
		this.roleid = _roleid_;
		this.rolelv = _rolelv_;
		this.value = _value_;
		this.rolename = _rolename_;
		this.campid = _campid_;
		this.heroids = _heroids_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(roleid);
		_os_.marshal(rolelv);
		_os_.marshal(value);
		_os_.marshal(rolename, "UTF-16LE");
		_os_.marshal(campid);
		_os_.compact_uint32(heroids.size());
		for (Integer _v_ : heroids) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		roleid = _os_.unmarshal_long();
		rolelv = _os_.unmarshal_short();
		value = _os_.unmarshal_int();
		rolename = _os_.unmarshal_String("UTF-16LE");
		campid = _os_.unmarshal_short();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			heroids.add(_v_);
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof RankInfo) {
			RankInfo _o_ = (RankInfo)_o1_;
			if (roleid != _o_.roleid) return false;
			if (rolelv != _o_.rolelv) return false;
			if (value != _o_.value) return false;
			if (!rolename.equals(_o_.rolename)) return false;
			if (campid != _o_.campid) return false;
			if (!heroids.equals(_o_.heroids)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)roleid;
		_h_ += rolelv;
		_h_ += value;
		_h_ += rolename.hashCode();
		_h_ += campid;
		_h_ += heroids.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(roleid).append(",");
		_sb_.append(rolelv).append(",");
		_sb_.append(value).append(",");
		_sb_.append("T").append(rolename.length()).append(",");
		_sb_.append(campid).append(",");
		_sb_.append(heroids).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

