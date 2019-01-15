
package chuhan.gsp.stage;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __COpenMohe__ extends xio.Protocol { }

/** 开魔盒 by yanglk
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class COpenMohe extends __COpenMohe__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure(){
			protected boolean process() throws Exception {
				StageRole srole = StageRole.getStageRole(roleId);
				boolean result = srole.openMohe(place);
				if(!result){
					SOpenMohe snd = new SOpenMohe();
					snd.endtype = SOpenMohe.END_ERROR;
					xdb.Procedure.psend(roleId, snd);
				}
				return result;
			};
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787946;

	public int getType() {
		return 787946;
	}

	public int place; // 魔盒位置1~3

	public COpenMohe() {
	}

	public COpenMohe(int _place_) {
		this.place = _place_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(place);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		place = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof COpenMohe) {
			COpenMohe _o_ = (COpenMohe)_o1_;
			if (place != _o_.place) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += place;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(place).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(COpenMohe _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = place - _o_.place;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

