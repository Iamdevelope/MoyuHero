
package chuhan.gsp.battle.realtime;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CGetNextKey__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CGetNextKey extends __CGetNextKey__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
				
				chuhan.gsp.attr.PropRole prole = chuhan.gsp.attr.PropRole.getPropRole(roleId, false);
				if(prole == null)
					return false;
				RoomManager.Room room = RoomManager.getInstance().getRoom(roomkey);
				if(room == null || !room.isRoleInRoom(roleId))
					return false;
				xdb.Procedure.psendWhileCommit(roleId, new SGetNextKey(room.getinsideid()));
				return true;
			};
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787893;

	public int getType() {
		return 787893;
	}

	public int roomkey;

	public CGetNextKey() {
	}

	public CGetNextKey(int _roomkey_) {
		this.roomkey = _roomkey_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(roomkey);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		roomkey = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CGetNextKey) {
			CGetNextKey _o_ = (CGetNextKey)_o1_;
			if (roomkey != _o_.roomkey) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += roomkey;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(roomkey).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CGetNextKey _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = roomkey - _o_.roomkey;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

