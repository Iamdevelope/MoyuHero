
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SSkinInfos__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SSkinInfos extends __SSkinInfos__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787766;

	public int getType() {
		return 787766;
	}

	public java.util.HashMap<Integer,chuhan.gsp.hero.BeautySkinInfo> infos;

	public SSkinInfos() {
		infos = new java.util.HashMap<Integer,chuhan.gsp.hero.BeautySkinInfo>();
	}

	public SSkinInfos(java.util.HashMap<Integer,chuhan.gsp.hero.BeautySkinInfo> _infos_) {
		this.infos = _infos_;
	}

	public final boolean _validator_() {
		for (java.util.Map.Entry<Integer, chuhan.gsp.hero.BeautySkinInfo> _e_ : infos.entrySet()) {
			if (!_e_.getValue()._validator_()) return false;
		}
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.compact_uint32(infos.size());
		for (java.util.Map.Entry<Integer, chuhan.gsp.hero.BeautySkinInfo> _e_ : infos.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _k_;
			_k_ = _os_.unmarshal_int();
			chuhan.gsp.hero.BeautySkinInfo _v_ = new chuhan.gsp.hero.BeautySkinInfo();
			_v_.unmarshal(_os_);
			infos.put(_k_, _v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SSkinInfos) {
			SSkinInfos _o_ = (SSkinInfos)_o1_;
			if (!infos.equals(_o_.infos)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += infos.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(infos).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

