
package chuhan.gsp.play.lotteryitem;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CLotteryItem__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CLotteryItem extends __CLotteryItem__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure(){
			protected boolean process() throws Exception {
				LotteryItemColumns lotterycol = LotteryItemColumns.getLotteryItemColumn(roleId, false);
				boolean result = lotterycol.LotteryEntry(lotterytype);
				
				if(!result){
					SLotteryItem snd = new SLotteryItem();
					snd.result = SLotteryItem.END_ERROR;
					snd.lotterytype = lotterytype;
					xdb.Procedure.psend(roleId, snd);
				}
				
				return result;
			};
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788763;

	public int getType() {
		return 788763;
	}

	public final static int ONE = 1;
	public final static int TEN = 2;
	public final static int FREE = 3;
	public final static int REFRESH = 4;

	public int lotterytype;

	public CLotteryItem() {
	}

	public CLotteryItem(int _lotterytype_) {
		this.lotterytype = _lotterytype_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(lotterytype);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		lotterytype = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CLotteryItem) {
			CLotteryItem _o_ = (CLotteryItem)_o1_;
			if (lotterytype != _o_.lotterytype) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += lotterytype;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(lotterytype).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CLotteryItem _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = lotterytype - _o_.lotterytype;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

