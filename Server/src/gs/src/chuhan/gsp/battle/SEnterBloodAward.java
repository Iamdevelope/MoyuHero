
package chuhan.gsp.battle;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SEnterBloodAward__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SEnterBloodAward extends __SEnterBloodAward__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787846;

	public int getType() {
		return 787846;
	}

	public int totalstar;
	public java.util.HashMap<Byte,Integer> repeatnum;
	public java.util.LinkedList<Byte> fixednum;

	public SEnterBloodAward() {
		repeatnum = new java.util.HashMap<Byte,Integer>();
		fixednum = new java.util.LinkedList<Byte>();
	}

	public SEnterBloodAward(int _totalstar_, java.util.HashMap<Byte,Integer> _repeatnum_, java.util.LinkedList<Byte> _fixednum_) {
		this.totalstar = _totalstar_;
		this.repeatnum = _repeatnum_;
		this.fixednum = _fixednum_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(totalstar);
		_os_.compact_uint32(repeatnum.size());
		for (java.util.Map.Entry<Byte, Integer> _e_ : repeatnum.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.compact_uint32(fixednum.size());
		for (Byte _v_ : fixednum) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		totalstar = _os_.unmarshal_int();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			byte _k_;
			_k_ = _os_.unmarshal_byte();
			int _v_;
			_v_ = _os_.unmarshal_int();
			repeatnum.put(_k_, _v_);
		}
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			byte _v_;
			_v_ = _os_.unmarshal_byte();
			fixednum.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SEnterBloodAward) {
			SEnterBloodAward _o_ = (SEnterBloodAward)_o1_;
			if (totalstar != _o_.totalstar) return false;
			if (!repeatnum.equals(_o_.repeatnum)) return false;
			if (!fixednum.equals(_o_.fixednum)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += totalstar;
		_h_ += repeatnum.hashCode();
		_h_ += fixednum.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(totalstar).append(",");
		_sb_.append(repeatnum).append(",");
		_sb_.append(fixednum).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

