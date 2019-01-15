
package chuhan.gsp.stage;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SGetStageReward__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SGetStageReward extends __SGetStageReward__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787952;

	public int getType() {
		return 787952;
	}

	public final static int END_OK = 1; // 成功
	public final static int END_ERROR = 2; // 失败

	public int endtype;
	public java.util.LinkedList<Integer> buytype; // 掉落小包组
	public byte boxnum; // 第几个宝箱，从0开始

	public SGetStageReward() {
		buytype = new java.util.LinkedList<Integer>();
	}

	public SGetStageReward(int _endtype_, java.util.LinkedList<Integer> _buytype_, byte _boxnum_) {
		this.endtype = _endtype_;
		this.buytype = _buytype_;
		this.boxnum = _boxnum_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(endtype);
		_os_.compact_uint32(buytype.size());
		for (Integer _v_ : buytype) {
			_os_.marshal(_v_);
		}
		_os_.marshal(boxnum);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		endtype = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			buytype.add(_v_);
		}
		boxnum = _os_.unmarshal_byte();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SGetStageReward) {
			SGetStageReward _o_ = (SGetStageReward)_o1_;
			if (endtype != _o_.endtype) return false;
			if (!buytype.equals(_o_.buytype)) return false;
			if (boxnum != _o_.boxnum) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += endtype;
		_h_ += buytype.hashCode();
		_h_ += (int)boxnum;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(endtype).append(",");
		_sb_.append(buytype).append(",");
		_sb_.append(boxnum).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

