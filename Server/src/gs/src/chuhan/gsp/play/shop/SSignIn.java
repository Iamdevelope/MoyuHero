
package chuhan.gsp.play.shop;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SSignIn__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SSignIn extends __SSignIn__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788836;

	public int getType() {
		return 788836;
	}

	public final static int END_OK = 1; // 成功
	public final static int END_ERROR = 2; // 失败
	public final static int END_ERROR_DAY = 3; // 失败就是当天签过了
	public final static int END_ERROR_MONTH = 4; // 失败就是当月签满了

	public int result;
	public int daynum;
	public int issign; // 本日是否签到，0为未签，1为已签
	public chuhan.gsp.play.Gift gift;

	public SSignIn() {
		gift = new chuhan.gsp.play.Gift();
	}

	public SSignIn(int _result_, int _daynum_, int _issign_, chuhan.gsp.play.Gift _gift_) {
		this.result = _result_;
		this.daynum = _daynum_;
		this.issign = _issign_;
		this.gift = _gift_;
	}

	public final boolean _validator_() {
		if (!gift._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(result);
		_os_.marshal(daynum);
		_os_.marshal(issign);
		_os_.marshal(gift);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		result = _os_.unmarshal_int();
		daynum = _os_.unmarshal_int();
		issign = _os_.unmarshal_int();
		gift.unmarshal(_os_);
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SSignIn) {
			SSignIn _o_ = (SSignIn)_o1_;
			if (result != _o_.result) return false;
			if (daynum != _o_.daynum) return false;
			if (issign != _o_.issign) return false;
			if (!gift.equals(_o_.gift)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += result;
		_h_ += daynum;
		_h_ += issign;
		_h_ += gift.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(result).append(",");
		_sb_.append(daynum).append(",");
		_sb_.append(issign).append(",");
		_sb_.append(gift).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

