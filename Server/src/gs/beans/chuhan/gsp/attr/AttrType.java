
package chuhan.gsp.attr;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

/** 不要擅自在此加名称与ID，如果要加一个新的属性和ID，先与策划商量，以免冲突
*/
public class AttrType implements Marshal , Comparable<AttrType>{
	public final static int ARMY = 10; // 兵力
	public final static int ATTACK = 20; // 攻击
	public final static int DEFEND = 30; // 防御
	public final static int SKILL = 40; // 计略
	public final static int POWER = 50; // 计策强度
	public final static int CRUEL_RATE = 60; // 物理暴击几率
	public final static int POWER_CRUEL_RATE = 70; // 计策暴击几率
	public final static int SPEED = 80; // 速度
	public final static int NO_HURT = 90; // 免伤
	public final static int NO_DEF = 100; // 破防
	public final static int TI = 110; // 体力
	public final static int REDUCE = 130; // 距离衰减
	public final static int HIT = 140; // 命中
	public final static int DODGE = 150; // 躲闪
	public final static int ANTI_CRUEL_RATE = 160; // 暴击抗性几率
	public final static int ANTI_SINGLE_SKILL = 170; // 单法抗
	public final static int ANTI_MULTI_SKILL = 180; // 群法抗


	public AttrType() {
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
		if (_o1_ instanceof AttrType) {
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

	public int compareTo(AttrType _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		return _c_;
	}

}

