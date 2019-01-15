
package chuhan.gsp.stage;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SSendStages__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SSendStages extends __SSendStages__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787934;

	public int getType() {
		return 787934;
	}

	public java.util.LinkedList<chuhan.gsp.stage.StageInfo> stages;

	public SSendStages() {
		stages = new java.util.LinkedList<chuhan.gsp.stage.StageInfo>();
	}

	public SSendStages(java.util.LinkedList<chuhan.gsp.stage.StageInfo> _stages_) {
		this.stages = _stages_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.stage.StageInfo _v_ : stages)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.compact_uint32(stages.size());
		for (chuhan.gsp.stage.StageInfo _v_ : stages) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.stage.StageInfo _v_ = new chuhan.gsp.stage.StageInfo();
			_v_.unmarshal(_os_);
			stages.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SSendStages) {
			SSendStages _o_ = (SSendStages)_o1_;
			if (!stages.equals(_o_.stages)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += stages.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(stages).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

