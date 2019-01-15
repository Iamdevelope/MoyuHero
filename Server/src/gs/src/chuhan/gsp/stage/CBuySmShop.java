
package chuhan.gsp.stage;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CBuySmShop__ extends xio.Protocol { }

/** 购买神秘商店物品 by yanglk
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CBuySmShop extends __CBuySmShop__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure(){
			protected boolean process() throws Exception {
				StageRole srole = StageRole.getStageRole(roleId);
				boolean result = srole.buySmShop(smshopid);
				if(!result){
					SBuySmShop snd = new SBuySmShop();
					snd.endtype = SBuySmShop.END_ERROR;
					xdb.Procedure.psend(roleId, snd);
				}
				return result;
			};
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787950;

	public int getType() {
		return 787950;
	}

	public int smshopid; // 神秘商店id

	public CBuySmShop() {
	}

	public CBuySmShop(int _smshopid_) {
		this.smshopid = _smshopid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(smshopid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		smshopid = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CBuySmShop) {
			CBuySmShop _o_ = (CBuySmShop)_o1_;
			if (smshopid != _o_.smshopid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += smshopid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(smshopid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CBuySmShop _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = smshopid - _o_.smshopid;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

