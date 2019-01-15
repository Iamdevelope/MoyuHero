
package chuhan.gsp;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __BeanImport__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class BeanImport extends __BeanImport__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 786432;

	public int getType() {
		return 786432;
	}

	public chuhan.gsp.KickErrConst b1;
	public chuhan.gsp.DataInit b2;
	public chuhan.gsp.LogPriority b3;
	public chuhan.gsp.SysSetType b4;
	public chuhan.gsp.ColorType b5;
	public chuhan.gsp.PlatformType b6;
	public chuhan.gsp.MsgType b7;
	public chuhan.gsp.Huoyue b8;
	public chuhan.gsp.GAMEACTIVITY b9;

	public BeanImport() {
		b1 = new chuhan.gsp.KickErrConst();
		b2 = new chuhan.gsp.DataInit();
		b3 = new chuhan.gsp.LogPriority();
		b4 = new chuhan.gsp.SysSetType();
		b5 = new chuhan.gsp.ColorType();
		b6 = new chuhan.gsp.PlatformType();
		b7 = new chuhan.gsp.MsgType();
		b8 = new chuhan.gsp.Huoyue();
		b9 = new chuhan.gsp.GAMEACTIVITY();
	}

	public BeanImport(chuhan.gsp.KickErrConst _b1_, chuhan.gsp.DataInit _b2_, chuhan.gsp.LogPriority _b3_, chuhan.gsp.SysSetType _b4_, chuhan.gsp.ColorType _b5_, chuhan.gsp.PlatformType _b6_, chuhan.gsp.MsgType _b7_, chuhan.gsp.Huoyue _b8_, chuhan.gsp.GAMEACTIVITY _b9_) {
		this.b1 = _b1_;
		this.b2 = _b2_;
		this.b3 = _b3_;
		this.b4 = _b4_;
		this.b5 = _b5_;
		this.b6 = _b6_;
		this.b7 = _b7_;
		this.b8 = _b8_;
		this.b9 = _b9_;
	}

	public final boolean _validator_() {
		if (!b1._validator_()) return false;
		if (!b2._validator_()) return false;
		if (!b3._validator_()) return false;
		if (!b4._validator_()) return false;
		if (!b5._validator_()) return false;
		if (!b6._validator_()) return false;
		if (!b7._validator_()) return false;
		if (!b8._validator_()) return false;
		if (!b9._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(b1);
		_os_.marshal(b2);
		_os_.marshal(b3);
		_os_.marshal(b4);
		_os_.marshal(b5);
		_os_.marshal(b6);
		_os_.marshal(b7);
		_os_.marshal(b8);
		_os_.marshal(b9);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		b1.unmarshal(_os_);
		b2.unmarshal(_os_);
		b3.unmarshal(_os_);
		b4.unmarshal(_os_);
		b5.unmarshal(_os_);
		b6.unmarshal(_os_);
		b7.unmarshal(_os_);
		b8.unmarshal(_os_);
		b9.unmarshal(_os_);
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof BeanImport) {
			BeanImport _o_ = (BeanImport)_o1_;
			if (!b1.equals(_o_.b1)) return false;
			if (!b2.equals(_o_.b2)) return false;
			if (!b3.equals(_o_.b3)) return false;
			if (!b4.equals(_o_.b4)) return false;
			if (!b5.equals(_o_.b5)) return false;
			if (!b6.equals(_o_.b6)) return false;
			if (!b7.equals(_o_.b7)) return false;
			if (!b8.equals(_o_.b8)) return false;
			if (!b9.equals(_o_.b9)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += b1.hashCode();
		_h_ += b2.hashCode();
		_h_ += b3.hashCode();
		_h_ += b4.hashCode();
		_h_ += b5.hashCode();
		_h_ += b6.hashCode();
		_h_ += b7.hashCode();
		_h_ += b8.hashCode();
		_h_ += b9.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(b1).append(",");
		_sb_.append(b2).append(",");
		_sb_.append(b3).append(",");
		_sb_.append(b4).append(",");
		_sb_.append(b5).append(",");
		_sb_.append(b6).append(",");
		_sb_.append(b7).append(",");
		_sb_.append(b8).append(",");
		_sb_.append(b9).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(BeanImport _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = b1.compareTo(_o_.b1);
		if (0 != _c_) return _c_;
		_c_ = b2.compareTo(_o_.b2);
		if (0 != _c_) return _c_;
		_c_ = b3.compareTo(_o_.b3);
		if (0 != _c_) return _c_;
		_c_ = b4.compareTo(_o_.b4);
		if (0 != _c_) return _c_;
		_c_ = b5.compareTo(_o_.b5);
		if (0 != _c_) return _c_;
		_c_ = b6.compareTo(_o_.b6);
		if (0 != _c_) return _c_;
		_c_ = b7.compareTo(_o_.b7);
		if (0 != _c_) return _c_;
		_c_ = b8.compareTo(_o_.b8);
		if (0 != _c_) return _c_;
		_c_ = b9.compareTo(_o_.b9);
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

