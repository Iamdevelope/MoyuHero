
package chuhan.gsp.friends;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SFriendList__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SFriendList extends __SFriendList__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788242;

	public int getType() {
		return 788242;
	}

	public java.util.LinkedList<chuhan.gsp.friends.FriendInfo> friends;

	public SFriendList() {
		friends = new java.util.LinkedList<chuhan.gsp.friends.FriendInfo>();
	}

	public SFriendList(java.util.LinkedList<chuhan.gsp.friends.FriendInfo> _friends_) {
		this.friends = _friends_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.friends.FriendInfo _v_ : friends)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.compact_uint32(friends.size());
		for (chuhan.gsp.friends.FriendInfo _v_ : friends) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.friends.FriendInfo _v_ = new chuhan.gsp.friends.FriendInfo();
			_v_.unmarshal(_os_);
			friends.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SFriendList) {
			SFriendList _o_ = (SFriendList)_o1_;
			if (!friends.equals(_o_.friends)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += friends.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(friends).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

