
package chuhan.gsp.play.activity;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRefreshSingleGameAct__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRefreshSingleGameAct extends __SRefreshSingleGameAct__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 789052;

	public int getType() {
		return 789052;
	}

	public chuhan.gsp.play.activity.gactivity gameactivity;

	public SRefreshSingleGameAct() {
		gameactivity = new chuhan.gsp.play.activity.gactivity();
	}

	public SRefreshSingleGameAct(chuhan.gsp.play.activity.gactivity _gameactivity_) {
		this.gameactivity = _gameactivity_;
	}

	public final boolean _validator_() {
		if (!gameactivity._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(gameactivity);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		gameactivity.unmarshal(_os_);
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRefreshSingleGameAct) {
			SRefreshSingleGameAct _o_ = (SRefreshSingleGameAct)_o1_;
			if (!gameactivity.equals(_o_.gameactivity)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += gameactivity.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(gameactivity).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SRefreshSingleGameAct _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = gameactivity.compareTo(_o_.gameactivity);
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

