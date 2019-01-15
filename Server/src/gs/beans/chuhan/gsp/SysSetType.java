
package chuhan.gsp;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class SysSetType implements Marshal , Comparable<SysSetType>{
	public final static int AcceptTrade = 1; // 接受交易
	public final static int AcceptTeam = 2; // 接受组队
	public final static int CheckEquipOn = 3; // 允许查看装备
	public final static int AcceptGiven = 4; // 接受给予
	public final static int AcceptSmallGame = 5; // 接受小游戏邀请
	public final static int EscapeConfirm = 6; // 逃跑确认
	public final static int PurchaseConfirm = 7; // 购买确认
	public final static int FriendMessageNotify = 8; // 购买确认
	public final static int DaHongNotify = 9; // 大红大蓝获得提示
	public final static int Temp = 10; // 临时，无用
	public final static int MaxShowRoleNum = 11; // 最大显示数量


	public SysSetType() {
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SysSetType) {
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SysSetType _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		return _c_;
	}

}

