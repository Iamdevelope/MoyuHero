
package chuhan.gsp.play.huoyuedu;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CGetHuoYueBox__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CGetHuoYueBox extends __CGetHuoYueBox__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure(){
			protected boolean process() throws Exception {
				HuoyueColumns huoyuecol = HuoyueColumns.getHuoyueColumn(roleId, false);
				boolean result = huoyuecol.getHuoYueBox(boxnum);
				
				if(!result){
					SGetHuoYueBox snd = new SGetHuoYueBox();
					snd.result = SGetHuoYueBox.END_ERROR;
					xdb.Procedure.psend(roleId, snd);
				}
				
				return result;
			};
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788785;

	public int getType() {
		return 788785;
	}

	public int boxnum; // 第几个宝箱，从1开始

	public CGetHuoYueBox() {
	}

	public CGetHuoYueBox(int _boxnum_) {
		this.boxnum = _boxnum_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(boxnum);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		boxnum = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CGetHuoYueBox) {
			CGetHuoYueBox _o_ = (CGetHuoYueBox)_o1_;
			if (boxnum != _o_.boxnum) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += boxnum;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(boxnum).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CGetHuoYueBox _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = boxnum - _o_.boxnum;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

