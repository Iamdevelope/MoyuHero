
package chuhan.gsp.play.wordboss;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class bossrole implements Marshal , Comparable<bossrole>{
	public long bosshpall; // boss总血量
	public long killhpall; // 击杀总血量
	public long bossnowhp; // 本次攻击前boss血量
	public int zhufunum; // 祝福次数
	public int shouwangzl; // 守望之灵
	public int chuanshuozs; // 传说之石
	public int openboss; // 值为1234，代表第几个boss,没有则为-1
	public int openendtime; // 倒计时，秒
	public int nextintime; // 下次进入倒计时，秒

	public bossrole() {
	}

	public bossrole(long _bosshpall_, long _killhpall_, long _bossnowhp_, int _zhufunum_, int _shouwangzl_, int _chuanshuozs_, int _openboss_, int _openendtime_, int _nextintime_) {
		this.bosshpall = _bosshpall_;
		this.killhpall = _killhpall_;
		this.bossnowhp = _bossnowhp_;
		this.zhufunum = _zhufunum_;
		this.shouwangzl = _shouwangzl_;
		this.chuanshuozs = _chuanshuozs_;
		this.openboss = _openboss_;
		this.openendtime = _openendtime_;
		this.nextintime = _nextintime_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(bosshpall);
		_os_.marshal(killhpall);
		_os_.marshal(bossnowhp);
		_os_.marshal(zhufunum);
		_os_.marshal(shouwangzl);
		_os_.marshal(chuanshuozs);
		_os_.marshal(openboss);
		_os_.marshal(openendtime);
		_os_.marshal(nextintime);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		bosshpall = _os_.unmarshal_long();
		killhpall = _os_.unmarshal_long();
		bossnowhp = _os_.unmarshal_long();
		zhufunum = _os_.unmarshal_int();
		shouwangzl = _os_.unmarshal_int();
		chuanshuozs = _os_.unmarshal_int();
		openboss = _os_.unmarshal_int();
		openendtime = _os_.unmarshal_int();
		nextintime = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof bossrole) {
			bossrole _o_ = (bossrole)_o1_;
			if (bosshpall != _o_.bosshpall) return false;
			if (killhpall != _o_.killhpall) return false;
			if (bossnowhp != _o_.bossnowhp) return false;
			if (zhufunum != _o_.zhufunum) return false;
			if (shouwangzl != _o_.shouwangzl) return false;
			if (chuanshuozs != _o_.chuanshuozs) return false;
			if (openboss != _o_.openboss) return false;
			if (openendtime != _o_.openendtime) return false;
			if (nextintime != _o_.nextintime) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)bosshpall;
		_h_ += (int)killhpall;
		_h_ += (int)bossnowhp;
		_h_ += zhufunum;
		_h_ += shouwangzl;
		_h_ += chuanshuozs;
		_h_ += openboss;
		_h_ += openendtime;
		_h_ += nextintime;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(bosshpall).append(",");
		_sb_.append(killhpall).append(",");
		_sb_.append(bossnowhp).append(",");
		_sb_.append(zhufunum).append(",");
		_sb_.append(shouwangzl).append(",");
		_sb_.append(chuanshuozs).append(",");
		_sb_.append(openboss).append(",");
		_sb_.append(openendtime).append(",");
		_sb_.append(nextintime).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(bossrole _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = Long.signum(bosshpall - _o_.bosshpall);
		if (0 != _c_) return _c_;
		_c_ = Long.signum(killhpall - _o_.killhpall);
		if (0 != _c_) return _c_;
		_c_ = Long.signum(bossnowhp - _o_.bossnowhp);
		if (0 != _c_) return _c_;
		_c_ = zhufunum - _o_.zhufunum;
		if (0 != _c_) return _c_;
		_c_ = shouwangzl - _o_.shouwangzl;
		if (0 != _c_) return _c_;
		_c_ = chuanshuozs - _o_.chuanshuozs;
		if (0 != _c_) return _c_;
		_c_ = openboss - _o_.openboss;
		if (0 != _c_) return _c_;
		_c_ = openendtime - _o_.openendtime;
		if (0 != _c_) return _c_;
		_c_ = nextintime - _o_.nextintime;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

