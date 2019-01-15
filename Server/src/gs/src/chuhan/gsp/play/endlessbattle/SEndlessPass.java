
package chuhan.gsp.play.endlessbattle;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SEndlessPass__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SEndlessPass extends __SEndlessPass__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788936;

	public int getType() {
		return 788936;
	}

	public final static int END_OK = 1; // 成功
	public final static int END_ERROR = 2; // 失败

	public int result;
	public chuhan.gsp.play.endlessbattle.endlessBattleInfo battleinfo;
	public chuhan.gsp.play.endlessbattle.endlessAttr attrinfo;

	public SEndlessPass() {
		battleinfo = new chuhan.gsp.play.endlessbattle.endlessBattleInfo();
		attrinfo = new chuhan.gsp.play.endlessbattle.endlessAttr();
	}

	public SEndlessPass(int _result_, chuhan.gsp.play.endlessbattle.endlessBattleInfo _battleinfo_, chuhan.gsp.play.endlessbattle.endlessAttr _attrinfo_) {
		this.result = _result_;
		this.battleinfo = _battleinfo_;
		this.attrinfo = _attrinfo_;
	}

	public final boolean _validator_() {
		if (!battleinfo._validator_()) return false;
		if (!attrinfo._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(result);
		_os_.marshal(battleinfo);
		_os_.marshal(attrinfo);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		result = _os_.unmarshal_int();
		battleinfo.unmarshal(_os_);
		attrinfo.unmarshal(_os_);
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SEndlessPass) {
			SEndlessPass _o_ = (SEndlessPass)_o1_;
			if (result != _o_.result) return false;
			if (!battleinfo.equals(_o_.battleinfo)) return false;
			if (!attrinfo.equals(_o_.attrinfo)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += result;
		_h_ += battleinfo.hashCode();
		_h_ += attrinfo.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(result).append(",");
		_sb_.append(battleinfo).append(",");
		_sb_.append(attrinfo).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

