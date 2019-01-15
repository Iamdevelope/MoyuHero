
package chuhan.gsp.battle;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SSendBloodRankList__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SSendBloodRankList extends __SSendBloodRankList__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787849;

	public int getType() {
		return 787849;
	}

	public java.util.ArrayList<chuhan.gsp.battle.BloodRankRole> ranklist;

	public SSendBloodRankList() {
		ranklist = new java.util.ArrayList<chuhan.gsp.battle.BloodRankRole>();
	}

	public SSendBloodRankList(java.util.ArrayList<chuhan.gsp.battle.BloodRankRole> _ranklist_) {
		this.ranklist = _ranklist_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.battle.BloodRankRole _v_ : ranklist)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.compact_uint32(ranklist.size());
		for (chuhan.gsp.battle.BloodRankRole _v_ : ranklist) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.battle.BloodRankRole _v_ = new chuhan.gsp.battle.BloodRankRole();
			_v_.unmarshal(_os_);
			ranklist.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SSendBloodRankList) {
			SSendBloodRankList _o_ = (SSendBloodRankList)_o1_;
			if (!ranklist.equals(_o_.ranklist)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += ranklist.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(ranklist).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

