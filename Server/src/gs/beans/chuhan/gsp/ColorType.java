
package chuhan.gsp;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class ColorType implements Marshal , Comparable<ColorType>{
	public final static int WHITE = 1; // 白
	public final static int GREEN = 2; // 绿
	public final static int BLUE = 3; // 蓝
	public final static int PURPLE = 4; // 紫
	public final static int ORANGE = 5; // 橙
	public final static int GOLDEN = 6; // 金
	public final static int JEW = 7; // 钻石


	public ColorType() {
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
		if (_o1_ instanceof ColorType) {
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

	public int compareTo(ColorType _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		return _c_;
	}

}

