
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CSleepWithBeauty__ extends xio.Protocol { }

/** 请求缠绵
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CSleepWithBeauty extends __CSleepWithBeauty__ {
	@Override
	protected void process() {
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
//				BeautyRole brole = BeautyRole.getBeautyRole(roleId, false);
//				return brole.sleepWith(beautyid);
				return true;
			};
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787759;

	public int getType() {
		return 787759;
	}

	public byte beautyid;

	public CSleepWithBeauty() {
	}

	public CSleepWithBeauty(byte _beautyid_) {
		this.beautyid = _beautyid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(beautyid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		beautyid = _os_.unmarshal_byte();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CSleepWithBeauty) {
			CSleepWithBeauty _o_ = (CSleepWithBeauty)_o1_;
			if (beautyid != _o_.beautyid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)beautyid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(beautyid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CSleepWithBeauty _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = beautyid - _o_.beautyid;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

