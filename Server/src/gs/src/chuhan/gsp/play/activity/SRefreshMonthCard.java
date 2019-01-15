
package chuhan.gsp.play.activity;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRefreshMonthCard__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRefreshMonthCard extends __SRefreshMonthCard__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 789042;

	public int getType() {
		return 789042;
	}

	public java.util.LinkedList<chuhan.gsp.monthcard> monthcardlist;

	public SRefreshMonthCard() {
		monthcardlist = new java.util.LinkedList<chuhan.gsp.monthcard>();
	}

	public SRefreshMonthCard(java.util.LinkedList<chuhan.gsp.monthcard> _monthcardlist_) {
		this.monthcardlist = _monthcardlist_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.monthcard _v_ : monthcardlist)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.compact_uint32(monthcardlist.size());
		for (chuhan.gsp.monthcard _v_ : monthcardlist) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.monthcard _v_ = new chuhan.gsp.monthcard();
			_v_.unmarshal(_os_);
			monthcardlist.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRefreshMonthCard) {
			SRefreshMonthCard _o_ = (SRefreshMonthCard)_o1_;
			if (!monthcardlist.equals(_o_.monthcardlist)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += monthcardlist.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(monthcardlist).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

