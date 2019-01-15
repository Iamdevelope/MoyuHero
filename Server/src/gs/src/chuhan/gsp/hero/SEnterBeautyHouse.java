
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SEnterBeautyHouse__ extends xio.Protocol { }

/** 进入美人阁 服务器发送
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SEnterBeautyHouse extends __SEnterBeautyHouse__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787758;

	public int getType() {
		return 787758;
	}

	public java.util.ArrayList<chuhan.gsp.hero.BeautyState> beauties;

	public SEnterBeautyHouse() {
		beauties = new java.util.ArrayList<chuhan.gsp.hero.BeautyState>();
	}

	public SEnterBeautyHouse(java.util.ArrayList<chuhan.gsp.hero.BeautyState> _beauties_) {
		this.beauties = _beauties_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.hero.BeautyState _v_ : beauties)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.compact_uint32(beauties.size());
		for (chuhan.gsp.hero.BeautyState _v_ : beauties) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.hero.BeautyState _v_ = new chuhan.gsp.hero.BeautyState();
			_v_.unmarshal(_os_);
			beauties.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SEnterBeautyHouse) {
			SEnterBeautyHouse _o_ = (SEnterBeautyHouse)_o1_;
			if (!beauties.equals(_o_.beauties)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += beauties.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(beauties).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

