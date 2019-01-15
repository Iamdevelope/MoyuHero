
package chuhan.gsp.attr;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class CalcAttrType implements Marshal , Comparable<CalcAttrType>{
	public final static int ARMY = 10; // 兵力
	public final static int ATTACK = 20; // 攻击
	public final static int DEFEND = 30; // 防御
	public final static int SKILL = 40; // 计略
	public final static int CRUEL_RATE = 60; // 物理暴击几率
	public final static int POWER_CRUEL_RATE = 70; // 计策暴击几率
	public final static int SPEED = 80; // 速度
	public final static int NO_HURT = 90; // 免伤
	public final static int NO_DEF = 100; // 破防
	public final static int HIT = 140; // 命中
	public final static int DODGE = 150; // 躲闪
	public final static int ANTI_CRUEL_RATE = 160; // 暴击抗性几率


	public CalcAttrType() {
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
		if (_o1_ instanceof CalcAttrType) {
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

	public int compareTo(CalcAttrType _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		return _c_;
	}

}

