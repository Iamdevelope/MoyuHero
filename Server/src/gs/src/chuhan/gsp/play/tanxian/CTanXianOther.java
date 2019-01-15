
package chuhan.gsp.play.tanxian;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CTanXianOther__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CTanXianOther extends __CTanXianOther__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure(){
			protected boolean process() throws Exception {
				TanXianColumns col = TanXianColumns.getTanXianColumn(roleId, false);
				
				boolean result = col.tanxianOtherEntry(endtype, tanxianid);
				if(!result){
					STanXianOther snd = new STanXianOther();
					snd.result = STanXianOther.END_ERROR;
					xdb.Procedure.psend(roleId, snd);
				}
				return result;
			};
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788986;

	public int getType() {
		return 788986;
	}

	public final static int END_GET = 1; // 领取奖励
	public final static int END_SPEED = 2; // 快速完成
	public final static int END_NULL = 3; // 召回
	public final static int SREFRESH = 4; // 刷新

	public int endtype;
	public int tanxianid; // 探险id

	public CTanXianOther() {
	}

	public CTanXianOther(int _endtype_, int _tanxianid_) {
		this.endtype = _endtype_;
		this.tanxianid = _tanxianid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(endtype);
		_os_.marshal(tanxianid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		endtype = _os_.unmarshal_int();
		tanxianid = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CTanXianOther) {
			CTanXianOther _o_ = (CTanXianOther)_o1_;
			if (endtype != _o_.endtype) return false;
			if (tanxianid != _o_.tanxianid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += endtype;
		_h_ += tanxianid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(endtype).append(",");
		_sb_.append(tanxianid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CTanXianOther _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = endtype - _o_.endtype;
		if (0 != _c_) return _c_;
		_c_ = tanxianid - _o_.tanxianid;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

