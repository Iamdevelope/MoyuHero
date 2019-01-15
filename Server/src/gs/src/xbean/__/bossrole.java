
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class bossrole extends xdb.XBean implements xbean.bossrole {
	private long killhpall; // 击杀总血量
	private int killboss; // 攻击boss类型，值为1234，代表4个boss
	private long bossnowhp; // 本次攻击前boss血量
	private long time; // 上次攻击时间
	private int zhufunum; // 祝福次数
	private int shouwangzl; // 守望之灵
	private int chuanshuozs; // 传说之石
	private xbean.bossshop bshop; // 猎人集市

	bossrole(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		bshop = new bossshop(0, this, "bshop");
	}

	public bossrole() {
		this(0, null, null);
	}

	public bossrole(bossrole _o_) {
		this(_o_, null, null);
	}

	bossrole(xbean.bossrole _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof bossrole) assign((bossrole)_o1_);
		else if (_o1_ instanceof bossrole.Data) assign((bossrole.Data)_o1_);
		else if (_o1_ instanceof bossrole.Const) assign(((bossrole.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(bossrole _o_) {
		_o_._xdb_verify_unsafe_();
		killhpall = _o_.killhpall;
		killboss = _o_.killboss;
		bossnowhp = _o_.bossnowhp;
		time = _o_.time;
		zhufunum = _o_.zhufunum;
		shouwangzl = _o_.shouwangzl;
		chuanshuozs = _o_.chuanshuozs;
		bshop = new bossshop(_o_.bshop, this, "bshop");
	}

	private void assign(bossrole.Data _o_) {
		killhpall = _o_.killhpall;
		killboss = _o_.killboss;
		bossnowhp = _o_.bossnowhp;
		time = _o_.time;
		zhufunum = _o_.zhufunum;
		shouwangzl = _o_.shouwangzl;
		chuanshuozs = _o_.chuanshuozs;
		bshop = new bossshop(_o_.bshop, this, "bshop");
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(killhpall);
		_os_.marshal(killboss);
		_os_.marshal(bossnowhp);
		_os_.marshal(time);
		_os_.marshal(zhufunum);
		_os_.marshal(shouwangzl);
		_os_.marshal(chuanshuozs);
		bshop.marshal(_os_);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		killhpall = _os_.unmarshal_long();
		killboss = _os_.unmarshal_int();
		bossnowhp = _os_.unmarshal_long();
		time = _os_.unmarshal_long();
		zhufunum = _os_.unmarshal_int();
		shouwangzl = _os_.unmarshal_int();
		chuanshuozs = _os_.unmarshal_int();
		bshop.unmarshal(_os_);
		return _os_;
	}

	@Override
	public xbean.bossrole copy() {
		_xdb_verify_unsafe_();
		return new bossrole(this);
	}

	@Override
	public xbean.bossrole toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.bossrole toBean() {
		_xdb_verify_unsafe_();
		return new bossrole(this); // same as copy()
	}

	@Override
	public xbean.bossrole toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.bossrole toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public long getKillhpall() { // 击杀总血量
		_xdb_verify_unsafe_();
		return killhpall;
	}

	@Override
	public int getKillboss() { // 攻击boss类型，值为1234，代表4个boss
		_xdb_verify_unsafe_();
		return killboss;
	}

	@Override
	public long getBossnowhp() { // 本次攻击前boss血量
		_xdb_verify_unsafe_();
		return bossnowhp;
	}

	@Override
	public long getTime() { // 上次攻击时间
		_xdb_verify_unsafe_();
		return time;
	}

	@Override
	public int getZhufunum() { // 祝福次数
		_xdb_verify_unsafe_();
		return zhufunum;
	}

	@Override
	public int getShouwangzl() { // 守望之灵
		_xdb_verify_unsafe_();
		return shouwangzl;
	}

	@Override
	public int getChuanshuozs() { // 传说之石
		_xdb_verify_unsafe_();
		return chuanshuozs;
	}

	@Override
	public xbean.bossshop getBshop() { // 猎人集市
		_xdb_verify_unsafe_();
		return bshop;
	}

	@Override
	public void setKillhpall(long _v_) { // 击杀总血量
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "killhpall") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, killhpall) {
					public void rollback() { killhpall = _xdb_saved; }
				};}});
		killhpall = _v_;
	}

	@Override
	public void setKillboss(int _v_) { // 攻击boss类型，值为1234，代表4个boss
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "killboss") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, killboss) {
					public void rollback() { killboss = _xdb_saved; }
				};}});
		killboss = _v_;
	}

	@Override
	public void setBossnowhp(long _v_) { // 本次攻击前boss血量
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "bossnowhp") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, bossnowhp) {
					public void rollback() { bossnowhp = _xdb_saved; }
				};}});
		bossnowhp = _v_;
	}

	@Override
	public void setTime(long _v_) { // 上次攻击时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "time") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, time) {
					public void rollback() { time = _xdb_saved; }
				};}});
		time = _v_;
	}

	@Override
	public void setZhufunum(int _v_) { // 祝福次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "zhufunum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, zhufunum) {
					public void rollback() { zhufunum = _xdb_saved; }
				};}});
		zhufunum = _v_;
	}

	@Override
	public void setShouwangzl(int _v_) { // 守望之灵
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "shouwangzl") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, shouwangzl) {
					public void rollback() { shouwangzl = _xdb_saved; }
				};}});
		shouwangzl = _v_;
	}

	@Override
	public void setChuanshuozs(int _v_) { // 传说之石
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "chuanshuozs") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, chuanshuozs) {
					public void rollback() { chuanshuozs = _xdb_saved; }
				};}});
		chuanshuozs = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		bossrole _o_ = null;
		if ( _o1_ instanceof bossrole ) _o_ = (bossrole)_o1_;
		else if ( _o1_ instanceof bossrole.Const ) _o_ = ((bossrole.Const)_o1_).nThis();
		else return false;
		if (killhpall != _o_.killhpall) return false;
		if (killboss != _o_.killboss) return false;
		if (bossnowhp != _o_.bossnowhp) return false;
		if (time != _o_.time) return false;
		if (zhufunum != _o_.zhufunum) return false;
		if (shouwangzl != _o_.shouwangzl) return false;
		if (chuanshuozs != _o_.chuanshuozs) return false;
		if (!bshop.equals(_o_.bshop)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += killhpall;
		_h_ += killboss;
		_h_ += bossnowhp;
		_h_ += time;
		_h_ += zhufunum;
		_h_ += shouwangzl;
		_h_ += chuanshuozs;
		_h_ += bshop.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(killhpall);
		_sb_.append(",");
		_sb_.append(killboss);
		_sb_.append(",");
		_sb_.append(bossnowhp);
		_sb_.append(",");
		_sb_.append(time);
		_sb_.append(",");
		_sb_.append(zhufunum);
		_sb_.append(",");
		_sb_.append(shouwangzl);
		_sb_.append(",");
		_sb_.append(chuanshuozs);
		_sb_.append(",");
		_sb_.append(bshop);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("killhpall"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("killboss"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("bossnowhp"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("time"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("zhufunum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("shouwangzl"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("chuanshuozs"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("bshop"));
		return lb;
	}

	private class Const implements xbean.bossrole {
		bossrole nThis() {
			return bossrole.this;
		}

		@Override
		public xbean.bossrole copy() {
			return bossrole.this.copy();
		}

		@Override
		public xbean.bossrole toData() {
			return bossrole.this.toData();
		}

		public xbean.bossrole toBean() {
			return bossrole.this.toBean();
		}

		@Override
		public xbean.bossrole toDataIf() {
			return bossrole.this.toDataIf();
		}

		public xbean.bossrole toBeanIf() {
			return bossrole.this.toBeanIf();
		}

		@Override
		public long getKillhpall() { // 击杀总血量
			_xdb_verify_unsafe_();
			return killhpall;
		}

		@Override
		public int getKillboss() { // 攻击boss类型，值为1234，代表4个boss
			_xdb_verify_unsafe_();
			return killboss;
		}

		@Override
		public long getBossnowhp() { // 本次攻击前boss血量
			_xdb_verify_unsafe_();
			return bossnowhp;
		}

		@Override
		public long getTime() { // 上次攻击时间
			_xdb_verify_unsafe_();
			return time;
		}

		@Override
		public int getZhufunum() { // 祝福次数
			_xdb_verify_unsafe_();
			return zhufunum;
		}

		@Override
		public int getShouwangzl() { // 守望之灵
			_xdb_verify_unsafe_();
			return shouwangzl;
		}

		@Override
		public int getChuanshuozs() { // 传说之石
			_xdb_verify_unsafe_();
			return chuanshuozs;
		}

		@Override
		public xbean.bossshop getBshop() { // 猎人集市
			_xdb_verify_unsafe_();
			return xdb.Consts.toConst(bshop);
		}

		@Override
		public void setKillhpall(long _v_) { // 击杀总血量
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setKillboss(int _v_) { // 攻击boss类型，值为1234，代表4个boss
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setBossnowhp(long _v_) { // 本次攻击前boss血量
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTime(long _v_) { // 上次攻击时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setZhufunum(int _v_) { // 祝福次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setShouwangzl(int _v_) { // 守望之灵
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setChuanshuozs(int _v_) { // 传说之石
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean toConst() {
			_xdb_verify_unsafe_();
			return this;
		}

		@Override
		public boolean isConst() {
			_xdb_verify_unsafe_();
			return true;
		}

		@Override
		public boolean isData() {
			return bossrole.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return bossrole.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return bossrole.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return bossrole.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return bossrole.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return bossrole.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return bossrole.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return bossrole.this.hashCode();
		}

		@Override
		public String toString() {
			return bossrole.this.toString();
		}

	}

	public static final class Data implements xbean.bossrole {
		private long killhpall; // 击杀总血量
		private int killboss; // 攻击boss类型，值为1234，代表4个boss
		private long bossnowhp; // 本次攻击前boss血量
		private long time; // 上次攻击时间
		private int zhufunum; // 祝福次数
		private int shouwangzl; // 守望之灵
		private int chuanshuozs; // 传说之石
		private xbean.bossshop bshop; // 猎人集市

		public Data() {
			bshop = new bossshop.Data();
		}

		Data(xbean.bossrole _o1_) {
			if (_o1_ instanceof bossrole) assign((bossrole)_o1_);
			else if (_o1_ instanceof bossrole.Data) assign((bossrole.Data)_o1_);
			else if (_o1_ instanceof bossrole.Const) assign(((bossrole.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(bossrole _o_) {
			killhpall = _o_.killhpall;
			killboss = _o_.killboss;
			bossnowhp = _o_.bossnowhp;
			time = _o_.time;
			zhufunum = _o_.zhufunum;
			shouwangzl = _o_.shouwangzl;
			chuanshuozs = _o_.chuanshuozs;
			bshop = new bossshop.Data(_o_.bshop);
		}

		private void assign(bossrole.Data _o_) {
			killhpall = _o_.killhpall;
			killboss = _o_.killboss;
			bossnowhp = _o_.bossnowhp;
			time = _o_.time;
			zhufunum = _o_.zhufunum;
			shouwangzl = _o_.shouwangzl;
			chuanshuozs = _o_.chuanshuozs;
			bshop = new bossshop.Data(_o_.bshop);
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(killhpall);
			_os_.marshal(killboss);
			_os_.marshal(bossnowhp);
			_os_.marshal(time);
			_os_.marshal(zhufunum);
			_os_.marshal(shouwangzl);
			_os_.marshal(chuanshuozs);
			bshop.marshal(_os_);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			killhpall = _os_.unmarshal_long();
			killboss = _os_.unmarshal_int();
			bossnowhp = _os_.unmarshal_long();
			time = _os_.unmarshal_long();
			zhufunum = _os_.unmarshal_int();
			shouwangzl = _os_.unmarshal_int();
			chuanshuozs = _os_.unmarshal_int();
			bshop.unmarshal(_os_);
			return _os_;
		}

		@Override
		public xbean.bossrole copy() {
			return new Data(this);
		}

		@Override
		public xbean.bossrole toData() {
			return new Data(this);
		}

		public xbean.bossrole toBean() {
			return new bossrole(this, null, null);
		}

		@Override
		public xbean.bossrole toDataIf() {
			return this;
		}

		public xbean.bossrole toBeanIf() {
			return new bossrole(this, null, null);
		}

		// xdb.Bean interface. Data Unsupported
		public boolean xdbManaged() { throw new UnsupportedOperationException(); }
		public xdb.Bean xdbParent() { throw new UnsupportedOperationException(); }
		public String xdbVarname()  { throw new UnsupportedOperationException(); }
		public Long    xdbObjId()   { throw new UnsupportedOperationException(); }
		public xdb.Bean toConst()   { throw new UnsupportedOperationException(); }
		public boolean isConst()    { return false; }
		public boolean isData()     { return true; }

		@Override
		public long getKillhpall() { // 击杀总血量
			return killhpall;
		}

		@Override
		public int getKillboss() { // 攻击boss类型，值为1234，代表4个boss
			return killboss;
		}

		@Override
		public long getBossnowhp() { // 本次攻击前boss血量
			return bossnowhp;
		}

		@Override
		public long getTime() { // 上次攻击时间
			return time;
		}

		@Override
		public int getZhufunum() { // 祝福次数
			return zhufunum;
		}

		@Override
		public int getShouwangzl() { // 守望之灵
			return shouwangzl;
		}

		@Override
		public int getChuanshuozs() { // 传说之石
			return chuanshuozs;
		}

		@Override
		public xbean.bossshop getBshop() { // 猎人集市
			return bshop;
		}

		@Override
		public void setKillhpall(long _v_) { // 击杀总血量
			killhpall = _v_;
		}

		@Override
		public void setKillboss(int _v_) { // 攻击boss类型，值为1234，代表4个boss
			killboss = _v_;
		}

		@Override
		public void setBossnowhp(long _v_) { // 本次攻击前boss血量
			bossnowhp = _v_;
		}

		@Override
		public void setTime(long _v_) { // 上次攻击时间
			time = _v_;
		}

		@Override
		public void setZhufunum(int _v_) { // 祝福次数
			zhufunum = _v_;
		}

		@Override
		public void setShouwangzl(int _v_) { // 守望之灵
			shouwangzl = _v_;
		}

		@Override
		public void setChuanshuozs(int _v_) { // 传说之石
			chuanshuozs = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof bossrole.Data)) return false;
			bossrole.Data _o_ = (bossrole.Data) _o1_;
			if (killhpall != _o_.killhpall) return false;
			if (killboss != _o_.killboss) return false;
			if (bossnowhp != _o_.bossnowhp) return false;
			if (time != _o_.time) return false;
			if (zhufunum != _o_.zhufunum) return false;
			if (shouwangzl != _o_.shouwangzl) return false;
			if (chuanshuozs != _o_.chuanshuozs) return false;
			if (!bshop.equals(_o_.bshop)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += killhpall;
			_h_ += killboss;
			_h_ += bossnowhp;
			_h_ += time;
			_h_ += zhufunum;
			_h_ += shouwangzl;
			_h_ += chuanshuozs;
			_h_ += bshop.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(killhpall);
			_sb_.append(",");
			_sb_.append(killboss);
			_sb_.append(",");
			_sb_.append(bossnowhp);
			_sb_.append(",");
			_sb_.append(time);
			_sb_.append(",");
			_sb_.append(zhufunum);
			_sb_.append(",");
			_sb_.append(shouwangzl);
			_sb_.append(",");
			_sb_.append(chuanshuozs);
			_sb_.append(",");
			_sb_.append(bshop);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
