
package chuhan.gsp.play.camp;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SChangeCamp__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SChangeCamp extends __SChangeCamp__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788652;

	public int getType() {
		return 788652;
	}

	public short mycamp; // 我的阵营，没阵营=0
	public byte isfreetime; // 是否处于免费转阵营时间 0-否 1-是
	public int cjifen; // 楚积分
	public int hjifen; // 汉积分
	public int qjifen; // 群积分

	public SChangeCamp() {
	}

	public SChangeCamp(short _mycamp_, byte _isfreetime_, int _cjifen_, int _hjifen_, int _qjifen_) {
		this.mycamp = _mycamp_;
		this.isfreetime = _isfreetime_;
		this.cjifen = _cjifen_;
		this.hjifen = _hjifen_;
		this.qjifen = _qjifen_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(mycamp);
		_os_.marshal(isfreetime);
		_os_.marshal(cjifen);
		_os_.marshal(hjifen);
		_os_.marshal(qjifen);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		mycamp = _os_.unmarshal_short();
		isfreetime = _os_.unmarshal_byte();
		cjifen = _os_.unmarshal_int();
		hjifen = _os_.unmarshal_int();
		qjifen = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SChangeCamp) {
			SChangeCamp _o_ = (SChangeCamp)_o1_;
			if (mycamp != _o_.mycamp) return false;
			if (isfreetime != _o_.isfreetime) return false;
			if (cjifen != _o_.cjifen) return false;
			if (hjifen != _o_.hjifen) return false;
			if (qjifen != _o_.qjifen) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += mycamp;
		_h_ += (int)isfreetime;
		_h_ += cjifen;
		_h_ += hjifen;
		_h_ += qjifen;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(mycamp).append(",");
		_sb_.append(isfreetime).append(",");
		_sb_.append(cjifen).append(",");
		_sb_.append(hjifen).append(",");
		_sb_.append(qjifen).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SChangeCamp _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = mycamp - _o_.mycamp;
		if (0 != _c_) return _c_;
		_c_ = isfreetime - _o_.isfreetime;
		if (0 != _c_) return _c_;
		_c_ = cjifen - _o_.cjifen;
		if (0 != _c_) return _c_;
		_c_ = hjifen - _o_.hjifen;
		if (0 != _c_) return _c_;
		_c_ = qjifen - _o_.qjifen;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

