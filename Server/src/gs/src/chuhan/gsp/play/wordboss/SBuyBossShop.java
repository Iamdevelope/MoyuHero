
package chuhan.gsp.play.wordboss;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SBuyBossShop__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SBuyBossShop extends __SBuyBossShop__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788894;

	public int getType() {
		return 788894;
	}

	public final static int END_OK = 1; // 成功
	public final static int END_ERROR = 2; // 失败

	public int result;
	public int bossshopid; // 商品ID
	public int hunternum; // 今日猎人集市累计兑换次数
	public int chuanshuozs; // 传说之石
	public java.util.LinkedList<Integer> droplist; // 掉落小包ID

	public SBuyBossShop() {
		droplist = new java.util.LinkedList<Integer>();
	}

	public SBuyBossShop(int _result_, int _bossshopid_, int _hunternum_, int _chuanshuozs_, java.util.LinkedList<Integer> _droplist_) {
		this.result = _result_;
		this.bossshopid = _bossshopid_;
		this.hunternum = _hunternum_;
		this.chuanshuozs = _chuanshuozs_;
		this.droplist = _droplist_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(result);
		_os_.marshal(bossshopid);
		_os_.marshal(hunternum);
		_os_.marshal(chuanshuozs);
		_os_.compact_uint32(droplist.size());
		for (Integer _v_ : droplist) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		result = _os_.unmarshal_int();
		bossshopid = _os_.unmarshal_int();
		hunternum = _os_.unmarshal_int();
		chuanshuozs = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			droplist.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SBuyBossShop) {
			SBuyBossShop _o_ = (SBuyBossShop)_o1_;
			if (result != _o_.result) return false;
			if (bossshopid != _o_.bossshopid) return false;
			if (hunternum != _o_.hunternum) return false;
			if (chuanshuozs != _o_.chuanshuozs) return false;
			if (!droplist.equals(_o_.droplist)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += result;
		_h_ += bossshopid;
		_h_ += hunternum;
		_h_ += chuanshuozs;
		_h_ += droplist.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(result).append(",");
		_sb_.append(bossshopid).append(",");
		_sb_.append(hunternum).append(",");
		_sb_.append(chuanshuozs).append(",");
		_sb_.append(droplist).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

