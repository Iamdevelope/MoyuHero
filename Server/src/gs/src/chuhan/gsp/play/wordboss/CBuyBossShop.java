
package chuhan.gsp.play.wordboss;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CBuyBossShop__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CBuyBossShop extends __CBuyBossShop__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure(){
			protected boolean process() throws Exception {
				
				boolean result = Module.getInstance().buyBossShopEntry(roleId, bossshopid);
				if( !result ){
					SBuyBossShop snd = new SBuyBossShop();
					snd.result = SBuyBossShop.END_ERROR;
					xdb.Procedure.psend(roleId, snd);
				}
				return result;
			};
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788893;

	public int getType() {
		return 788893;
	}

	public int bossshopid; // 商品ID

	public CBuyBossShop() {
	}

	public CBuyBossShop(int _bossshopid_) {
		this.bossshopid = _bossshopid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(bossshopid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		bossshopid = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CBuyBossShop) {
			CBuyBossShop _o_ = (CBuyBossShop)_o1_;
			if (bossshopid != _o_.bossshopid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += bossshopid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(bossshopid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CBuyBossShop _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = bossshopid - _o_.bossshopid;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

