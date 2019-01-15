
package chuhan.gsp.play.endlessbattle;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CEndlessBuyadd__ extends xio.Protocol { }

/** 购买属性增加
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CEndlessBuyadd extends __CEndlessBuyadd__ {
	@Override
	protected void process() {
		// protocol handle
		long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
				
				xbean.Properties xprop = xtable.Properties.get(roleId);
				if(xprop == null){
					throw new IllegalArgumentException("构造角色时，角色 "+roleId+" 不存在。");
				}
				EndlessinfoColumns endcol = EndlessinfoColumns.getEndLessColumn(roleId, false);
				boolean result = endcol.buyAddAttr(addnum);
				
				SEndlessBuyadd snd = new SEndlessBuyadd();
				if(!result){
					snd.result = SEndlessBuyadd.END_ERROR;
				}
				else{
					snd.result = SEndlessBuyadd.END_OK;
					snd.add1 = endcol.xcolumn.getAdd1();
					snd.add2 = endcol.xcolumn.getAdd2();
					snd.add3 = endcol.xcolumn.getAdd3();
					snd.add4 = endcol.xcolumn.getAdd4();
				}
				
				xdb.Procedure.psend(roleId, snd);
				return result;
			};
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788942;

	public int getType() {
		return 788942;
	}

	public int addnum; // 购买的属性1~4

	public CEndlessBuyadd() {
	}

	public CEndlessBuyadd(int _addnum_) {
		this.addnum = _addnum_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(addnum);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		addnum = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CEndlessBuyadd) {
			CEndlessBuyadd _o_ = (CEndlessBuyadd)_o1_;
			if (addnum != _o_.addnum) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += addnum;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(addnum).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CEndlessBuyadd _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = addnum - _o_.addnum;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

