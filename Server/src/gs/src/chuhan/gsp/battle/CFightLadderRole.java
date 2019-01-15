
package chuhan.gsp.battle;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CFightLadderRole__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CFightLadderRole extends __CFightLadderRole__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		if(roleId == challengeroleid || selfrank == challengerank)
			new PRandomChallenge(roleId).submit(); 
		else
			new PChallengeRanker(roleId, selfrank, challengerank, challengeroleid,true).submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787837;

	public int getType() {
		return 787837;
	}

	public short selfrank; // 自己排名
	public short challengerank; // 挑战排名
	public long challengeroleid; // 挑战的roleId

	public CFightLadderRole() {
	}

	public CFightLadderRole(short _selfrank_, short _challengerank_, long _challengeroleid_) {
		this.selfrank = _selfrank_;
		this.challengerank = _challengerank_;
		this.challengeroleid = _challengeroleid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(selfrank);
		_os_.marshal(challengerank);
		_os_.marshal(challengeroleid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		selfrank = _os_.unmarshal_short();
		challengerank = _os_.unmarshal_short();
		challengeroleid = _os_.unmarshal_long();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CFightLadderRole) {
			CFightLadderRole _o_ = (CFightLadderRole)_o1_;
			if (selfrank != _o_.selfrank) return false;
			if (challengerank != _o_.challengerank) return false;
			if (challengeroleid != _o_.challengeroleid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += selfrank;
		_h_ += challengerank;
		_h_ += (int)challengeroleid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(selfrank).append(",");
		_sb_.append(challengerank).append(",");
		_sb_.append(challengeroleid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CFightLadderRole _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = selfrank - _o_.selfrank;
		if (0 != _c_) return _c_;
		_c_ = challengerank - _o_.challengerank;
		if (0 != _c_) return _c_;
		_c_ = Long.signum(challengeroleid - _o_.challengeroleid);
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

