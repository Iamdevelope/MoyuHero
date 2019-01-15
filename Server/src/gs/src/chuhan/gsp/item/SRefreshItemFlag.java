
package chuhan.gsp.item;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRefreshItemFlag__ extends xio.Protocol { }

/** 服务器通知客户端刷新道具的flag
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRefreshItemFlag extends __SRefreshItemFlag__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787540;

	public int getType() {
		return 787540;
	}

	public int itemkey; // 道具key
	public int flag; // 道具flag
	public byte bagid; // bag的类型

	public SRefreshItemFlag() {
	}

	public SRefreshItemFlag(int _itemkey_, int _flag_, byte _bagid_) {
		this.itemkey = _itemkey_;
		this.flag = _flag_;
		this.bagid = _bagid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(itemkey);
		_os_.marshal(flag);
		_os_.marshal(bagid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		itemkey = _os_.unmarshal_int();
		flag = _os_.unmarshal_int();
		bagid = _os_.unmarshal_byte();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRefreshItemFlag) {
			SRefreshItemFlag _o_ = (SRefreshItemFlag)_o1_;
			if (itemkey != _o_.itemkey) return false;
			if (flag != _o_.flag) return false;
			if (bagid != _o_.bagid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += itemkey;
		_h_ += flag;
		_h_ += (int)bagid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(itemkey).append(",");
		_sb_.append(flag).append(",");
		_sb_.append(bagid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SRefreshItemFlag _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = itemkey - _o_.itemkey;
		if (0 != _c_) return _c_;
		_c_ = flag - _o_.flag;
		if (0 != _c_) return _c_;
		_c_ = bagid - _o_.bagid;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

