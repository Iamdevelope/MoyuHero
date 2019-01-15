
package chuhan.gsp.task;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SSendActivities__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SSendActivities extends __SSendActivities__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788034;

	public int getType() {
		return 788034;
	}

	public java.util.ArrayList<chuhan.gsp.task.ActivityInfo> activities;
	public byte showactivityid;

	public SSendActivities() {
		activities = new java.util.ArrayList<chuhan.gsp.task.ActivityInfo>();
	}

	public SSendActivities(java.util.ArrayList<chuhan.gsp.task.ActivityInfo> _activities_, byte _showactivityid_) {
		this.activities = _activities_;
		this.showactivityid = _showactivityid_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.task.ActivityInfo _v_ : activities)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.compact_uint32(activities.size());
		for (chuhan.gsp.task.ActivityInfo _v_ : activities) {
			_os_.marshal(_v_);
		}
		_os_.marshal(showactivityid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.task.ActivityInfo _v_ = new chuhan.gsp.task.ActivityInfo();
			_v_.unmarshal(_os_);
			activities.add(_v_);
		}
		showactivityid = _os_.unmarshal_byte();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SSendActivities) {
			SSendActivities _o_ = (SSendActivities)_o1_;
			if (!activities.equals(_o_.activities)) return false;
			if (showactivityid != _o_.showactivityid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += activities.hashCode();
		_h_ += (int)showactivityid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(activities).append(",");
		_sb_.append(showactivityid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

