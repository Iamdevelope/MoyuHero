
package chuhan.gsp.item;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class SkillItemData implements Marshal , Comparable<SkillItemData>{
	public byte level; // 等级
	public byte grade; // 阶数
	public int exp; // 经验

	public SkillItemData() {
	}

	public SkillItemData(byte _level_, byte _grade_, int _exp_) {
		this.level = _level_;
		this.grade = _grade_;
		this.exp = _exp_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(level);
		_os_.marshal(grade);
		_os_.marshal(exp);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		level = _os_.unmarshal_byte();
		grade = _os_.unmarshal_byte();
		exp = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SkillItemData) {
			SkillItemData _o_ = (SkillItemData)_o1_;
			if (level != _o_.level) return false;
			if (grade != _o_.grade) return false;
			if (exp != _o_.exp) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)level;
		_h_ += (int)grade;
		_h_ += exp;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(level).append(",");
		_sb_.append(grade).append(",");
		_sb_.append(exp).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(SkillItemData _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = level - _o_.level;
		if (0 != _c_) return _c_;
		_c_ = grade - _o_.grade;
		if (0 != _c_) return _c_;
		_c_ = exp - _o_.exp;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

