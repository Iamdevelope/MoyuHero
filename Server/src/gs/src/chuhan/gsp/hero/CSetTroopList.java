
package chuhan.gsp.hero;

import java.util.LinkedList;
import java.util.List;


// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CSetTroopList__ extends xio.Protocol { }

/** 清空战队
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CSetTroopList extends __CSetTroopList__ {
	@Override
	protected void process() {
		/*final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
				xbean.Properties xprop = xtable.Properties.get(roleId);
				if(xprop == null)
				{
//					ErrorManager.getInstance().SendError(roleId, ErrorType.ERR_NOT_ONLINE);
					return false;
				}
				
				TroopColumn troopcol = TroopColumn.getTroopColumn(roleId, false);
				xbean.Troop troop = troopcol.getTroopByNum(troopid)	;
				if(troop == null)
				{
					return false;
				}
				troopcol.cleanTroop(troop);
				troopcol.refreshTroops();
				return true;
			};
		}.submit();*/
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787739;

	public int getType() {
		return 787739;
	}

	public int troopid;

	public CSetTroopList() {
	}

	public CSetTroopList(int _troopid_) {
		this.troopid = _troopid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(troopid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		troopid = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CSetTroopList) {
			CSetTroopList _o_ = (CSetTroopList)_o1_;
			if (troopid != _o_.troopid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += troopid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(troopid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CSetTroopList _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = troopid - _o_.troopid;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

