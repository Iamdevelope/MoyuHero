
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CHeroMSExp__ extends xio.Protocol { }

/** 英雄秘术增加经验
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CHeroMSExp extends __CHeroMSExp__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new PHeroMSExp(roleId, herokey,mslocation,itemidlist,itemnumlist).submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787788;

	public int getType() {
		return 787788;
	}

	public int herokey; // 英雄key
	public int mslocation; // 秘术位置 从1开始
	public java.util.LinkedList<Integer> itemidlist; // 物品配表ID
	public java.util.LinkedList<Integer> itemnumlist; // 物品数量

	public CHeroMSExp() {
		itemidlist = new java.util.LinkedList<Integer>();
		itemnumlist = new java.util.LinkedList<Integer>();
	}

	public CHeroMSExp(int _herokey_, int _mslocation_, java.util.LinkedList<Integer> _itemidlist_, java.util.LinkedList<Integer> _itemnumlist_) {
		this.herokey = _herokey_;
		this.mslocation = _mslocation_;
		this.itemidlist = _itemidlist_;
		this.itemnumlist = _itemnumlist_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(herokey);
		_os_.marshal(mslocation);
		_os_.compact_uint32(itemidlist.size());
		for (Integer _v_ : itemidlist) {
			_os_.marshal(_v_);
		}
		_os_.compact_uint32(itemnumlist.size());
		for (Integer _v_ : itemnumlist) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		herokey = _os_.unmarshal_int();
		mslocation = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			itemidlist.add(_v_);
		}
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			itemnumlist.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CHeroMSExp) {
			CHeroMSExp _o_ = (CHeroMSExp)_o1_;
			if (herokey != _o_.herokey) return false;
			if (mslocation != _o_.mslocation) return false;
			if (!itemidlist.equals(_o_.itemidlist)) return false;
			if (!itemnumlist.equals(_o_.itemnumlist)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += herokey;
		_h_ += mslocation;
		_h_ += itemidlist.hashCode();
		_h_ += itemnumlist.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(herokey).append(",");
		_sb_.append(mslocation).append(",");
		_sb_.append(itemidlist).append(",");
		_sb_.append(itemnumlist).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

