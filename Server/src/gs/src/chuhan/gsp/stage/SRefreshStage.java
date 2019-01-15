
package chuhan.gsp.stage;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SRefreshStage__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SRefreshStage extends __SRefreshStage__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787935;

	public int getType() {
		return 787935;
	}

	public int id;
	public byte starsum;
	public byte rewardgot; // 111，个位表示难度1，十位为难度2，百位为难度3，1已领取，0未领取

	public SRefreshStage() {
	}

	public SRefreshStage(int _id_, byte _starsum_, byte _rewardgot_) {
		this.id = _id_;
		this.starsum = _starsum_;
		this.rewardgot = _rewardgot_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(id);
		_os_.marshal(starsum);
		_os_.marshal(rewardgot);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		id = _os_.unmarshal_int();
		starsum = _os_.unmarshal_byte();
		rewardgot = _os_.unmarshal_byte();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SRefreshStage) {
			SRefreshStage _o_ = (SRefreshStage)_o1_;
			if (id != _o_.id) return false;
			if (starsum != _o_.starsum) return false;
			if (rewardgot != _o_.rewardgot) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += id;
		_h_ += (int)starsum;
		_h_ += (int)rewardgot;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(id).append(",");
		_sb_.append(starsum).append(",");
		_sb_.append(rewardgot).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SRefreshStage _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = id - _o_.id;
		if (0 != _c_) return _c_;
		_c_ = starsum - _o_.starsum;
		if (0 != _c_) return _c_;
		_c_ = rewardgot - _o_.rewardgot;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

