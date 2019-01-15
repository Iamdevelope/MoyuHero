
package chuhan.gsp.attr;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __BeanImport__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class BeanImport extends __BeanImport__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787433;

	public int getType() {
		return 787433;
	}

	public chuhan.gsp.attr.AttrType attr;
	public chuhan.gsp.attr.CalcAttrType calcattr;
	public chuhan.gsp.attr.EffectType effect;

	public BeanImport() {
		attr = new chuhan.gsp.attr.AttrType();
		calcattr = new chuhan.gsp.attr.CalcAttrType();
		effect = new chuhan.gsp.attr.EffectType();
	}

	public BeanImport(chuhan.gsp.attr.AttrType _attr_, chuhan.gsp.attr.CalcAttrType _calcattr_, chuhan.gsp.attr.EffectType _effect_) {
		this.attr = _attr_;
		this.calcattr = _calcattr_;
		this.effect = _effect_;
	}

	public final boolean _validator_() {
		if (!attr._validator_()) return false;
		if (!calcattr._validator_()) return false;
		if (!effect._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(attr);
		_os_.marshal(calcattr);
		_os_.marshal(effect);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		attr.unmarshal(_os_);
		calcattr.unmarshal(_os_);
		effect.unmarshal(_os_);
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof BeanImport) {
			BeanImport _o_ = (BeanImport)_o1_;
			if (!attr.equals(_o_.attr)) return false;
			if (!calcattr.equals(_o_.calcattr)) return false;
			if (!effect.equals(_o_.effect)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += attr.hashCode();
		_h_ += calcattr.hashCode();
		_h_ += effect.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(attr).append(",");
		_sb_.append(calcattr).append(",");
		_sb_.append(effect).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(BeanImport _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = attr.compareTo(_o_.attr);
		if (0 != _c_) return _c_;
		_c_ = calcattr.compareTo(_o_.calcattr);
		if (0 != _c_) return _c_;
		_c_ = effect.compareTo(_o_.effect);
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

