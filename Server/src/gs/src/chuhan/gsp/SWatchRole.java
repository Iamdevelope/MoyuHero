
package chuhan.gsp;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SWatchRole__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SWatchRole extends __SWatchRole__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 786443;

	public int getType() {
		return 786443;
	}

	public long roleid; // ID
	public java.lang.String name; // 名称
	public short level; // 等级
	public byte viplv; // vip等级
	public java.util.LinkedList<chuhan.gsp.WatchTroopInfo> troops;
	public byte ismyfriend; // 是否我的好友

	public SWatchRole() {
		name = "";
		troops = new java.util.LinkedList<chuhan.gsp.WatchTroopInfo>();
	}

	public SWatchRole(long _roleid_, java.lang.String _name_, short _level_, byte _viplv_, java.util.LinkedList<chuhan.gsp.WatchTroopInfo> _troops_, byte _ismyfriend_) {
		this.roleid = _roleid_;
		this.name = _name_;
		this.level = _level_;
		this.viplv = _viplv_;
		this.troops = _troops_;
		this.ismyfriend = _ismyfriend_;
	}

	public final boolean _validator_() {
		if (roleid < 1) return false;
		if (level < 1) return false;
		if (viplv < 1) return false;
		for (chuhan.gsp.WatchTroopInfo _v_ : troops)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(roleid);
		_os_.marshal(name, "UTF-16LE");
		_os_.marshal(level);
		_os_.marshal(viplv);
		_os_.compact_uint32(troops.size());
		for (chuhan.gsp.WatchTroopInfo _v_ : troops) {
			_os_.marshal(_v_);
		}
		_os_.marshal(ismyfriend);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		roleid = _os_.unmarshal_long();
		name = _os_.unmarshal_String("UTF-16LE");
		level = _os_.unmarshal_short();
		viplv = _os_.unmarshal_byte();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.WatchTroopInfo _v_ = new chuhan.gsp.WatchTroopInfo();
			_v_.unmarshal(_os_);
			troops.add(_v_);
		}
		ismyfriend = _os_.unmarshal_byte();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SWatchRole) {
			SWatchRole _o_ = (SWatchRole)_o1_;
			if (roleid != _o_.roleid) return false;
			if (!name.equals(_o_.name)) return false;
			if (level != _o_.level) return false;
			if (viplv != _o_.viplv) return false;
			if (!troops.equals(_o_.troops)) return false;
			if (ismyfriend != _o_.ismyfriend) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)roleid;
		_h_ += name.hashCode();
		_h_ += level;
		_h_ += (int)viplv;
		_h_ += troops.hashCode();
		_h_ += (int)ismyfriend;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(roleid).append(",");
		_sb_.append("T").append(name.length()).append(",");
		_sb_.append(level).append(",");
		_sb_.append(viplv).append(",");
		_sb_.append(troops).append(",");
		_sb_.append(ismyfriend).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

