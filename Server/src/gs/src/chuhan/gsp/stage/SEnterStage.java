
package chuhan.gsp.stage;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SEnterStage__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SEnterStage extends __SEnterStage__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787937;

	public int getType() {
		return 787937;
	}

	public byte stageid;
	public java.util.LinkedList<chuhan.gsp.stage.StageBattle> stagebattles; // 关卡

	public SEnterStage() {
		stagebattles = new java.util.LinkedList<chuhan.gsp.stage.StageBattle>();
	}

	public SEnterStage(byte _stageid_, java.util.LinkedList<chuhan.gsp.stage.StageBattle> _stagebattles_) {
		this.stageid = _stageid_;
		this.stagebattles = _stagebattles_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.stage.StageBattle _v_ : stagebattles)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(stageid);
		_os_.compact_uint32(stagebattles.size());
		for (chuhan.gsp.stage.StageBattle _v_ : stagebattles) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		stageid = _os_.unmarshal_byte();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.stage.StageBattle _v_ = new chuhan.gsp.stage.StageBattle();
			_v_.unmarshal(_os_);
			stagebattles.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SEnterStage) {
			SEnterStage _o_ = (SEnterStage)_o1_;
			if (stageid != _o_.stageid) return false;
			if (!stagebattles.equals(_o_.stagebattles)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)stageid;
		_h_ += stagebattles.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(stageid).append(",");
		_sb_.append(stagebattles).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

