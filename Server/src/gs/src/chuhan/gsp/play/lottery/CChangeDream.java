
package chuhan.gsp.play.lottery;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CChangeDream__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CChangeDream extends __CChangeDream__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure(){
			protected boolean process() throws Exception {
				LotteryColumns lotterycol = LotteryColumns.getLotteryColumn(roleId, false);
				boolean result = lotterycol.lotteryDream(false,isfree);
				
				if(!result){
					SChangeDream snd = new SChangeDream();
					snd.result = SChangeDream.END_ERROR;
					xdb.Procedure.psend(roleId, snd);
				}
				
				return result;
			};
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788736;

	public int getType() {
		return 788736;
	}

	public int isfree; // 0收费，1免费

	public CChangeDream() {
	}

	public CChangeDream(int _isfree_) {
		this.isfree = _isfree_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(isfree);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		isfree = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CChangeDream) {
			CChangeDream _o_ = (CChangeDream)_o1_;
			if (isfree != _o_.isfree) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += isfree;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(isfree).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CChangeDream _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = isfree - _o_.isfree;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

