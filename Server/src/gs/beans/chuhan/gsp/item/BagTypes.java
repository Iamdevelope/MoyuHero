
package chuhan.gsp.item;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

/** 与bagconfig.xlsx对应
*/
public class BagTypes implements Marshal , Comparable<BagTypes>{
	public final static int EMPTY = 0;
	public final static int BAG = 1; // 物品包裹
	public final static int SKILL = 2; // 技能包裹
	public final static int EQUIP = 3; // 装备包裹
	public final static int SOUL = 4; // 魂魄包裹
	public final static int COLLECTION = 5; // 收集包裹
	public final static int COUNT = 6; // 包裹类型数量		人物身上的包裹


	public BagTypes() {
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
		if (_o1_ instanceof BagTypes) {
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

	public int compareTo(BagTypes _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		return _c_;
	}

}

