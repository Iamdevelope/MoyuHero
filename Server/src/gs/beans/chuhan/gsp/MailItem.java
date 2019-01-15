
package chuhan.gsp;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

/** 邮件物品掉落信息 by yanglk
*/
public class MailItem implements Marshal , Comparable<MailItem>{
	public int objectid; // 物品ID
	public int dropnum; // 数量
	public int dropparameter1; // 附加条件1
	public int dropparameter2; // 附加条件2

	public MailItem() {
	}

	public MailItem(int _objectid_, int _dropnum_, int _dropparameter1_, int _dropparameter2_) {
		this.objectid = _objectid_;
		this.dropnum = _dropnum_;
		this.dropparameter1 = _dropparameter1_;
		this.dropparameter2 = _dropparameter2_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(objectid);
		_os_.marshal(dropnum);
		_os_.marshal(dropparameter1);
		_os_.marshal(dropparameter2);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		objectid = _os_.unmarshal_int();
		dropnum = _os_.unmarshal_int();
		dropparameter1 = _os_.unmarshal_int();
		dropparameter2 = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof MailItem) {
			MailItem _o_ = (MailItem)_o1_;
			if (objectid != _o_.objectid) return false;
			if (dropnum != _o_.dropnum) return false;
			if (dropparameter1 != _o_.dropparameter1) return false;
			if (dropparameter2 != _o_.dropparameter2) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += objectid;
		_h_ += dropnum;
		_h_ += dropparameter1;
		_h_ += dropparameter2;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(objectid).append(",");
		_sb_.append(dropnum).append(",");
		_sb_.append(dropparameter1).append(",");
		_sb_.append(dropparameter2).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(MailItem _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = objectid - _o_.objectid;
		if (0 != _c_) return _c_;
		_c_ = dropnum - _o_.dropnum;
		if (0 != _c_) return _c_;
		_c_ = dropparameter1 - _o_.dropparameter1;
		if (0 != _c_) return _c_;
		_c_ = dropparameter2 - _o_.dropparameter2;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

