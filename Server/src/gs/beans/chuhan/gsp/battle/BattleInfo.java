
package chuhan.gsp.battle;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

/** 关卡相关信息 by yanglk
*/
public class BattleInfo implements Marshal , Comparable<BattleInfo>{
	public int battleid; // 关卡id

	public BattleInfo() {
	}

	public BattleInfo(int _battleid_) {
		this.battleid = _battleid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(battleid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		battleid = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof BattleInfo) {
			BattleInfo _o_ = (BattleInfo)_o1_;
			if (battleid != _o_.battleid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += battleid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(battleid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(BattleInfo _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = battleid - _o_.battleid;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

