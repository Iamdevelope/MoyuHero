
package chuhan.gsp.item;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CUseItem__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CUseItem extends __CUseItem__ {
	@Override
	protected void process() {
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new PUseItem(roleId, bagid,itemkey,num,dstkey).submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787545;

	public int getType() {
		return 787545;
	}

	public byte bagid; // 暂时只有bag，soul，collect里的物品能用
	public int itemkey;
	public short num;
	public int dstkey; // 当给别的物品或武将使用时，具体是什么由使用的物品自己判断

	public CUseItem() {
	}

	public CUseItem(byte _bagid_, int _itemkey_, short _num_, int _dstkey_) {
		this.bagid = _bagid_;
		this.itemkey = _itemkey_;
		this.num = _num_;
		this.dstkey = _dstkey_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(bagid);
		_os_.marshal(itemkey);
		_os_.marshal(num);
		_os_.marshal(dstkey);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		bagid = _os_.unmarshal_byte();
		itemkey = _os_.unmarshal_int();
		num = _os_.unmarshal_short();
		dstkey = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CUseItem) {
			CUseItem _o_ = (CUseItem)_o1_;
			if (bagid != _o_.bagid) return false;
			if (itemkey != _o_.itemkey) return false;
			if (num != _o_.num) return false;
			if (dstkey != _o_.dstkey) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)bagid;
		_h_ += itemkey;
		_h_ += num;
		_h_ += dstkey;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(bagid).append(",");
		_sb_.append(itemkey).append(",");
		_sb_.append(num).append(",");
		_sb_.append(dstkey).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CUseItem _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = bagid - _o_.bagid;
		if (0 != _c_) return _c_;
		_c_ = itemkey - _o_.itemkey;
		if (0 != _c_) return _c_;
		_c_ = num - _o_.num;
		if (0 != _c_) return _c_;
		_c_ = dstkey - _o_.dstkey;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

