
package chuhan.gsp.play.turntable;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SMainView__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SMainView extends __SMainView__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788434;

	public int getType() {
		return 788434;
	}

	public byte ishavefree; // 是否有免费转的次数
	public int tableid; // 转盘ID
	public byte itemcheck; // 道具格信息,按位算(0-空 1-非空)

	public SMainView() {
	}

	public SMainView(byte _ishavefree_, int _tableid_, byte _itemcheck_) {
		this.ishavefree = _ishavefree_;
		this.tableid = _tableid_;
		this.itemcheck = _itemcheck_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(ishavefree);
		_os_.marshal(tableid);
		_os_.marshal(itemcheck);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		ishavefree = _os_.unmarshal_byte();
		tableid = _os_.unmarshal_int();
		itemcheck = _os_.unmarshal_byte();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SMainView) {
			SMainView _o_ = (SMainView)_o1_;
			if (ishavefree != _o_.ishavefree) return false;
			if (tableid != _o_.tableid) return false;
			if (itemcheck != _o_.itemcheck) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)ishavefree;
		_h_ += tableid;
		_h_ += (int)itemcheck;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(ishavefree).append(",");
		_sb_.append(tableid).append(",");
		_sb_.append(itemcheck).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SMainView _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = ishavefree - _o_.ishavefree;
		if (0 != _c_) return _c_;
		_c_ = tableid - _o_.tableid;
		if (0 != _c_) return _c_;
		_c_ = itemcheck - _o_.itemcheck;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

