
package chuhan.gsp.task;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRefreshHeroTimeTask__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRefreshHeroTimeTask extends __SRefreshHeroTimeTask__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788042;

	public int getType() {
		return 788042;
	}

	public chuhan.gsp.task.HeroTimeTask taskinfo;
	public int herokey;

	public SRefreshHeroTimeTask() {
		taskinfo = new chuhan.gsp.task.HeroTimeTask();
	}

	public SRefreshHeroTimeTask(chuhan.gsp.task.HeroTimeTask _taskinfo_, int _herokey_) {
		this.taskinfo = _taskinfo_;
		this.herokey = _herokey_;
	}

	public final boolean _validator_() {
		if (!taskinfo._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(taskinfo);
		_os_.marshal(herokey);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		taskinfo.unmarshal(_os_);
		herokey = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRefreshHeroTimeTask) {
			SRefreshHeroTimeTask _o_ = (SRefreshHeroTimeTask)_o1_;
			if (!taskinfo.equals(_o_.taskinfo)) return false;
			if (herokey != _o_.herokey) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += taskinfo.hashCode();
		_h_ += herokey;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(taskinfo).append(",");
		_sb_.append(herokey).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SRefreshHeroTimeTask _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = taskinfo.compareTo(_o_.taskinfo);
		if (0 != _c_) return _c_;
		_c_ = herokey - _o_.herokey;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

