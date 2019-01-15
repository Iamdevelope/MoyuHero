
package chuhan.gsp.play.activity;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CMonthCard__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CMonthCard extends __CMonthCard__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure(){
			protected boolean process() throws Exception {
				
				boolean result = ActivityManager.getInstance().getMonthCard(roleId,cardid);
				
				if(!result){
					SMonthCard snd = new SMonthCard();
					snd.result = SMonthCard.END_ERROR;
					snd.cardid = cardid;
					xdb.Procedure.psend(roleId, snd);
				}
				
				return result;
			};
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 789040;

	public int getType() {
		return 789040;
	}

	public int cardid;

	public CMonthCard() {
	}

	public CMonthCard(int _cardid_) {
		this.cardid = _cardid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(cardid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		cardid = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CMonthCard) {
			CMonthCard _o_ = (CMonthCard)_o1_;
			if (cardid != _o_.cardid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += cardid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(cardid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CMonthCard _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = cardid - _o_.cardid;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

