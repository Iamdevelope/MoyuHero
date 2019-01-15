
package chuhan.gsp;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class LogPriority implements Marshal , Comparable<LogPriority>{
	public final static int LOG_FORMAT = 15; // 玩家行为标准的日志，如：账号登陆，充值，任务相关。
	public final static int LOG_STAT = 16; // 每5分钟记录邮件次数，金钱修改量，物品修改量等。
	public final static int LOG_GM = 17; // 记录GM操作的log
	public final static int LOG_MONEY = 18; // 记录金钱的产生及消耗数量原因。
	public final static int LOG_CHAR2 = 19; // 聊天记录，记录采用BASE64编码记录。
	public final static int LOG_COUNTER = 20; // 每5分钟记录邮件次数，金钱修改量，物品修改量等。
	public final static int LOG_XINGCHENG = 21; // 玩家详细行为日志，如：移动，打怪，组队，拾取等。
	public final static int LOG_TRADE = 22; // 每日商城消耗记录。
	public final static int LOG_YUANBAO = 23; // 累计记录玩家充值，买，卖，消耗元宝数量。


	public LogPriority() {
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
		if (_o1_ instanceof LogPriority) {
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

	public int compareTo(LogPriority _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		return _c_;
	}

}

