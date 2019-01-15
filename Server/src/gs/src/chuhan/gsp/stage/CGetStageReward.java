
package chuhan.gsp.stage;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CGetStageReward__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CGetStageReward extends __CGetStageReward__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure(){
			protected boolean process() throws Exception {
				StageRole srole = StageRole.getStageRole(roleId);
				return srole.getStageCompleteReward(stageid,boxnum);
			};
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787940;

	public int getType() {
		return 787940;
	}

	public byte stageid; // 章节id
	public byte boxnum; // 第几个宝箱，从0开始

	public CGetStageReward() {
	}

	public CGetStageReward(byte _stageid_, byte _boxnum_) {
		this.stageid = _stageid_;
		this.boxnum = _boxnum_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(stageid);
		_os_.marshal(boxnum);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		stageid = _os_.unmarshal_byte();
		boxnum = _os_.unmarshal_byte();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CGetStageReward) {
			CGetStageReward _o_ = (CGetStageReward)_o1_;
			if (stageid != _o_.stageid) return false;
			if (boxnum != _o_.boxnum) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)stageid;
		_h_ += (int)boxnum;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(stageid).append(",");
		_sb_.append(boxnum).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CGetStageReward _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = stageid - _o_.stageid;
		if (0 != _c_) return _c_;
		_c_ = boxnum - _o_.boxnum;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

