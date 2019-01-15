
package chuhan.gsp.item;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SModItemNum__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SModItemNum extends __SModItemNum__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787534;

	public int getType() {
		return 787534;
	}

	public byte bagid;
	public int itemkey;
	public int curnum;

	public SModItemNum() {
	}

	public SModItemNum(byte _bagid_, int _itemkey_, int _curnum_) {
		this.bagid = _bagid_;
		this.itemkey = _itemkey_;
		this.curnum = _curnum_;
	}

	public final boolean _validator_() {
		if (bagid < 1) return false;
		if (itemkey < 1) return false;
		if (curnum < 1) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(bagid);
		_os_.marshal(itemkey);
		_os_.marshal(curnum);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		bagid = _os_.unmarshal_byte();
		itemkey = _os_.unmarshal_int();
		curnum = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SModItemNum) {
			SModItemNum _o_ = (SModItemNum)_o1_;
			if (bagid != _o_.bagid) return false;
			if (itemkey != _o_.itemkey) return false;
			if (curnum != _o_.curnum) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)bagid;
		_h_ += itemkey;
		_h_ += curnum;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(bagid).append(",");
		_sb_.append(itemkey).append(",");
		_sb_.append(curnum).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SModItemNum _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = bagid - _o_.bagid;
		if (0 != _c_) return _c_;
		_c_ = itemkey - _o_.itemkey;
		if (0 != _c_) return _c_;
		_c_ = curnum - _o_.curnum;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

