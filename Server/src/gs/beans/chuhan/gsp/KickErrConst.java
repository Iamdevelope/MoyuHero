
package chuhan.gsp;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class KickErrConst implements Marshal , Comparable<KickErrConst>{
	public final static int ERR_OLD_ONLINE_KICKOUT = 2048; // 老用户在线
	public final static int ERR_GM_KICKOUT = 2049; // 被GM踢下线
	public final static int ERR_SERVER_SHUTDOWN = 2050; // 服务器关闭
	public final static int ERR_GACD_PUNISH = 2051; // 反外挂答题没有通过
	public final static int ERR_RUN_TOO_FAST = 2052; // 走路太快被踢
	public final static int ERR_GACD_WAIGUA = 2053; // 使用外挂
	public final static int ERR_XUNBAO_SELLROLE = 2054; // 寻宝网寄售角色
	public final static int ERR_FORBID_USER = 2055; // 账号被单服封禁
	public final static int ERR_GACD_KICKOUT = 2056; // gacd发-1踢玩家下线


	public KickErrConst() {
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
		if (_o1_ instanceof KickErrConst) {
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

	public int compareTo(KickErrConst _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		return _c_;
	}

}

