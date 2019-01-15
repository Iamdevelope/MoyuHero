
package chuhan.gsp.stage;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SOpenMohe__ extends xio.Protocol { }

/** 开魔盒返回 by yanglk
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SOpenMohe extends __SOpenMohe__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787947;

	public int getType() {
		return 787947;
	}

	public final static int END_OK = 1; // 成功
	public final static int END_ERROR = 2; // 失败

	public int endtype;
	public int moheid; // 成功开启的魔盒ID
	public java.util.HashMap<Integer,chuhan.gsp.stage.mohe> moheshop; // 魔盒列表

	public SOpenMohe() {
		moheshop = new java.util.HashMap<Integer,chuhan.gsp.stage.mohe>();
	}

	public SOpenMohe(int _endtype_, int _moheid_, java.util.HashMap<Integer,chuhan.gsp.stage.mohe> _moheshop_) {
		this.endtype = _endtype_;
		this.moheid = _moheid_;
		this.moheshop = _moheshop_;
	}

	public final boolean _validator_() {
		for (java.util.Map.Entry<Integer, chuhan.gsp.stage.mohe> _e_ : moheshop.entrySet()) {
			if (!_e_.getValue()._validator_()) return false;
		}
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(endtype);
		_os_.marshal(moheid);
		_os_.compact_uint32(moheshop.size());
		for (java.util.Map.Entry<Integer, chuhan.gsp.stage.mohe> _e_ : moheshop.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		endtype = _os_.unmarshal_int();
		moheid = _os_.unmarshal_int();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _k_;
			_k_ = _os_.unmarshal_int();
			chuhan.gsp.stage.mohe _v_ = new chuhan.gsp.stage.mohe();
			_v_.unmarshal(_os_);
			moheshop.put(_k_, _v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SOpenMohe) {
			SOpenMohe _o_ = (SOpenMohe)_o1_;
			if (endtype != _o_.endtype) return false;
			if (moheid != _o_.moheid) return false;
			if (!moheshop.equals(_o_.moheshop)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += endtype;
		_h_ += moheid;
		_h_ += moheshop.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(endtype).append(",");
		_sb_.append(moheid).append(",");
		_sb_.append(moheshop).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

