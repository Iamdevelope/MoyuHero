
package chuhan.gsp.stage;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SBuySmShop__ extends xio.Protocol { }

/** 购买神秘商店物品返回 by yanglk
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SBuySmShop extends __SBuySmShop__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787951;

	public int getType() {
		return 787951;
	}

	public final static int END_OK = 1; // 成功
	public final static int END_ERROR = 2; // 失败

	public int endtype;
	public int smshopid; // 成功购买的神秘商店ID
	public java.util.HashMap<Integer,chuhan.gsp.stage.smshopdata> smshop; // 神秘商店随机出的物品（key为随机商店物品id，value为smshopdata）

	public SBuySmShop() {
		smshop = new java.util.HashMap<Integer,chuhan.gsp.stage.smshopdata>();
	}

	public SBuySmShop(int _endtype_, int _smshopid_, java.util.HashMap<Integer,chuhan.gsp.stage.smshopdata> _smshop_) {
		this.endtype = _endtype_;
		this.smshopid = _smshopid_;
		this.smshop = _smshop_;
	}

	public final boolean _validator_() {
		for (java.util.Map.Entry<Integer, chuhan.gsp.stage.smshopdata> _e_ : smshop.entrySet()) {
			if (!_e_.getValue()._validator_()) return false;
		}
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(endtype);
		_os_.marshal(smshopid);
		_os_.compact_uint32(smshop.size());
		for (java.util.Map.Entry<Integer, chuhan.gsp.stage.smshopdata> _e_ : smshop.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		endtype = _os_.unmarshal_int();
		smshopid = _os_.unmarshal_int();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _k_;
			_k_ = _os_.unmarshal_int();
			chuhan.gsp.stage.smshopdata _v_ = new chuhan.gsp.stage.smshopdata();
			_v_.unmarshal(_os_);
			smshop.put(_k_, _v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SBuySmShop) {
			SBuySmShop _o_ = (SBuySmShop)_o1_;
			if (endtype != _o_.endtype) return false;
			if (smshopid != _o_.smshopid) return false;
			if (!smshop.equals(_o_.smshop)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += endtype;
		_h_ += smshopid;
		_h_ += smshop.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(endtype).append(",");
		_sb_.append(smshopid).append(",");
		_sb_.append(smshop).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

