
package chuhan.gsp.play.lottery;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRefreshLotty__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRefreshLotty extends __SRefreshLotty__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788735;

	public int getType() {
		return 788735;
	}

	public chuhan.gsp.Lotty lotty;

	public SRefreshLotty() {
		lotty = new chuhan.gsp.Lotty();
	}

	public SRefreshLotty(chuhan.gsp.Lotty _lotty_) {
		this.lotty = _lotty_;
	}

	public final boolean _validator_() {
		if (!lotty._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(lotty);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		lotty.unmarshal(_os_);
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRefreshLotty) {
			SRefreshLotty _o_ = (SRefreshLotty)_o1_;
			if (!lotty.equals(_o_.lotty)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += lotty.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(lotty).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SRefreshLotty _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = lotty.compareTo(_o_.lotty);
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

