
package gnet;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

/** 获取及设置服务器全局信息 设置服务器最大在线人数，获取服务器当前在线人数
*/
public class ErrorCodes implements Marshal , Comparable<ErrorCodes>{
	public final static int error_succeed = 0; // 成功
	public final static int error_invalid = 1; // 失败，token/sessionid无效，定单无效
	public final static int error_plat_args = 2; // 与平台的访问的参数异常
	public final static int error_plat_trans = 3; // 与平台的访问异常
	public final static int error_xdb_store = 4; // xdb存储异常


	public ErrorCodes() {
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
		if (_o1_ instanceof ErrorCodes) {
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

	public int compareTo(ErrorCodes _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		return _c_;
	}

}

