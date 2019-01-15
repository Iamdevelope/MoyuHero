
package chuhan.gsp.battle;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SEnterLadder__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SEnterLadder extends __SEnterLadder__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787836;

	public int getType() {
		return 787836;
	}

	public byte todayfightnum;
	public short myrank;
	public int laddersoul; // 天梯元魂值
	public java.util.LinkedList<chuhan.gsp.battle.LadderRoleInfo> ladderroles;

	public SEnterLadder() {
		ladderroles = new java.util.LinkedList<chuhan.gsp.battle.LadderRoleInfo>();
	}

	public SEnterLadder(byte _todayfightnum_, short _myrank_, int _laddersoul_, java.util.LinkedList<chuhan.gsp.battle.LadderRoleInfo> _ladderroles_) {
		this.todayfightnum = _todayfightnum_;
		this.myrank = _myrank_;
		this.laddersoul = _laddersoul_;
		this.ladderroles = _ladderroles_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.battle.LadderRoleInfo _v_ : ladderroles)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(todayfightnum);
		_os_.marshal(myrank);
		_os_.marshal(laddersoul);
		_os_.compact_uint32(ladderroles.size());
		for (chuhan.gsp.battle.LadderRoleInfo _v_ : ladderroles) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		todayfightnum = _os_.unmarshal_byte();
		myrank = _os_.unmarshal_short();
		laddersoul = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.battle.LadderRoleInfo _v_ = new chuhan.gsp.battle.LadderRoleInfo();
			_v_.unmarshal(_os_);
			ladderroles.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SEnterLadder) {
			SEnterLadder _o_ = (SEnterLadder)_o1_;
			if (todayfightnum != _o_.todayfightnum) return false;
			if (myrank != _o_.myrank) return false;
			if (laddersoul != _o_.laddersoul) return false;
			if (!ladderroles.equals(_o_.ladderroles)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)todayfightnum;
		_h_ += myrank;
		_h_ += laddersoul;
		_h_ += ladderroles.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(todayfightnum).append(",");
		_sb_.append(myrank).append(",");
		_sb_.append(laddersoul).append(",");
		_sb_.append(ladderroles).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

