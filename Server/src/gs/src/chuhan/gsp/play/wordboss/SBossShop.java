
package chuhan.gsp.play.wordboss;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SBossShop__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SBossShop extends __SBossShop__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788892;

	public int getType() {
		return 788892;
	}

	public java.util.LinkedList<Integer> shoplist; // 今天可买的物品表
	public int hunternum; // 今日猎人集市累计兑换次数
	public int chuanshuozs; // 传说之石

	public SBossShop() {
		shoplist = new java.util.LinkedList<Integer>();
	}

	public SBossShop(java.util.LinkedList<Integer> _shoplist_, int _hunternum_, int _chuanshuozs_) {
		this.shoplist = _shoplist_;
		this.hunternum = _hunternum_;
		this.chuanshuozs = _chuanshuozs_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.compact_uint32(shoplist.size());
		for (Integer _v_ : shoplist) {
			_os_.marshal(_v_);
		}
		_os_.marshal(hunternum);
		_os_.marshal(chuanshuozs);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			shoplist.add(_v_);
		}
		hunternum = _os_.unmarshal_int();
		chuanshuozs = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SBossShop) {
			SBossShop _o_ = (SBossShop)_o1_;
			if (!shoplist.equals(_o_.shoplist)) return false;
			if (hunternum != _o_.hunternum) return false;
			if (chuanshuozs != _o_.chuanshuozs) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += shoplist.hashCode();
		_h_ += hunternum;
		_h_ += chuanshuozs;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(shoplist).append(",");
		_sb_.append(hunternum).append(",");
		_sb_.append(chuanshuozs).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

