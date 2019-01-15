
package chuhan.gsp.play.camp;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class Enemy implements Marshal {
	public long roleid;
	public java.lang.String rolename;
	public short rolelv;
	public int ladderank;
	public short campid;
	public java.util.LinkedList<Integer> heroids;

	public Enemy() {
		rolename = "";
		heroids = new java.util.LinkedList<Integer>();
	}

	public Enemy(long _roleid_, java.lang.String _rolename_, short _rolelv_, int _ladderank_, short _campid_, java.util.LinkedList<Integer> _heroids_) {
		this.roleid = _roleid_;
		this.rolename = _rolename_;
		this.rolelv = _rolelv_;
		this.ladderank = _ladderank_;
		this.campid = _campid_;
		this.heroids = _heroids_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(roleid);
		_os_.marshal(rolename, "UTF-16LE");
		_os_.marshal(rolelv);
		_os_.marshal(ladderank);
		_os_.marshal(campid);
		_os_.compact_uint32(heroids.size());
		for (Integer _v_ : heroids) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		roleid = _os_.unmarshal_long();
		rolename = _os_.unmarshal_String("UTF-16LE");
		rolelv = _os_.unmarshal_short();
		ladderank = _os_.unmarshal_int();
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
		if (_o1_ instanceof Enemy) {
			Enemy _o_ = (Enemy)_o1_;
			if (roleid != _o_.roleid) return false;
			if (!rolename.equals(_o_.rolename)) return false;
			if (rolelv != _o_.rolelv) return false;
			if (ladderank != _o_.ladderank) return false;
			if (campid != _o_.campid) return false;
			if (!heroids.equals(_o_.heroids)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)roleid;
		_h_ += rolename.hashCode();
		_h_ += rolelv;
		_h_ += ladderank;
		_h_ += campid;
		_h_ += heroids.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(roleid).append(",");
		_sb_.append("T").append(rolename.length()).append(",");
		_sb_.append(rolelv).append(",");
		_sb_.append(ladderank).append(",");
		_sb_.append(campid).append(",");
		_sb_.append(heroids).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

