
package chuhan.gsp.battle.realtime;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CMove__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CMove extends __CMove__ {
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
				xdb.Procedure.psendWhileCommit(room.getOtherRoleId(roleId), new SMove(roomkey,movelist));
				return true;
			};
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787891;

	public int getType() {
		return 787891;
	}

	public int roomkey;
	public java.util.LinkedList<chuhan.gsp.battle.realtime.BHeroMove> movelist;

	public CMove() {
		movelist = new java.util.LinkedList<chuhan.gsp.battle.realtime.BHeroMove>();
	}

	public CMove(int _roomkey_, java.util.LinkedList<chuhan.gsp.battle.realtime.BHeroMove> _movelist_) {
		this.roomkey = _roomkey_;
		this.movelist = _movelist_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.battle.realtime.BHeroMove _v_ : movelist)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(roomkey);
		_os_.compact_uint32(movelist.size());
		for (chuhan.gsp.battle.realtime.BHeroMove _v_ : movelist) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		roomkey = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.battle.realtime.BHeroMove _v_ = new chuhan.gsp.battle.realtime.BHeroMove();
			_v_.unmarshal(_os_);
			movelist.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CMove) {
			CMove _o_ = (CMove)_o1_;
			if (roomkey != _o_.roomkey) return false;
			if (!movelist.equals(_o_.movelist)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += roomkey;
		_h_ += movelist.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(roomkey).append(",");
		_sb_.append(movelist).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

