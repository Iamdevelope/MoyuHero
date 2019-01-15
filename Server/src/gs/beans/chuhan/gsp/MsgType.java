
package chuhan.gsp;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class MsgType implements Marshal , Comparable<MsgType>{
	public final static int MSG_HERO_GET = 210001; // 获得英雄
	public final static int MSG_HERO_QIANGHUA = 210002; // 英雄强化
	public final static int MSG_EQUIP_GET = 210003; // 获得物品
	public final static int MSG_EQUIP_QIANGHUA = 210004; // 物品强化
	public final static int MSG_ROLE_LEVEL = 110005; // 人物升级
	public final static int MSG_ACTIVITY_TILI = 110001; // 体力活动


	public MsgType() {
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
		if (_o1_ instanceof MsgType) {
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

	public int compareTo(MsgType _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		return _c_;
	}

}

