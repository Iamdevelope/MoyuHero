
package chuhan.gsp.battle.realtime;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SCreateRoom__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SCreateRoom extends __SCreateRoom__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787885;

	public int getType() {
		return 787885;
	}

	public int roomkey;
	public chuhan.gsp.battle.realtime.Battleroleinfo maininfo;
	public chuhan.gsp.battle.realtime.Battleroleinfo otherinfo;

	public SCreateRoom() {
		maininfo = new chuhan.gsp.battle.realtime.Battleroleinfo();
		otherinfo = new chuhan.gsp.battle.realtime.Battleroleinfo();
	}

	public SCreateRoom(int _roomkey_, chuhan.gsp.battle.realtime.Battleroleinfo _maininfo_, chuhan.gsp.battle.realtime.Battleroleinfo _otherinfo_) {
		this.roomkey = _roomkey_;
		this.maininfo = _maininfo_;
		this.otherinfo = _otherinfo_;
	}

	public final boolean _validator_() {
		if (!maininfo._validator_()) return false;
		if (!otherinfo._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(roomkey);
		_os_.marshal(maininfo);
		_os_.marshal(otherinfo);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		roomkey = _os_.unmarshal_int();
		maininfo.unmarshal(_os_);
		otherinfo.unmarshal(_os_);
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SCreateRoom) {
			SCreateRoom _o_ = (SCreateRoom)_o1_;
			if (roomkey != _o_.roomkey) return false;
			if (!maininfo.equals(_o_.maininfo)) return false;
			if (!otherinfo.equals(_o_.otherinfo)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += roomkey;
		_h_ += maininfo.hashCode();
		_h_ += otherinfo.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(roomkey).append(",");
		_sb_.append(maininfo).append(",");
		_sb_.append(otherinfo).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

