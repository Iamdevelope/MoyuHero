
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class boss extends xdb.XBean implements xbean.boss {
	private long lasthpall; // 上次总血量
	private int lastiskill; // 上次是否杀掉，0未杀，1已杀
	private long lastkillnum; // 杀掉则为用时（毫秒），未杀则为受到的伤害
	private long newhpall; // 最新总血量
	private long nowhp; // 现在血量
	private int bossid1; // bossid(第一个守门人)
	private int bossid2; // bossid(第一个boss)
	private int bossid3; // bossid(第二个守门人)
	private int bossid4; // bossid(第二个boss)
	private int bossiskill; // 个位为第一个boss，十位为第二个boss,0为未击杀，1为击杀
	private String boss1killname; // 击杀1者名称
	private String boss2killname; // 击杀2者名称
	private long time; // 上次刷新时间

	boss(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		boss1killname = "";
		boss2killname = "";
	}

	public boss() {
		this(0, null, null);
	}

	public boss(boss _o_) {
		this(_o_, null, null);
	}

	boss(xbean.boss _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof boss) assign((boss)_o1_);
		else if (_o1_ instanceof boss.Data) assign((boss.Data)_o1_);
		else if (_o1_ instanceof boss.Const) assign(((boss.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(boss _o_) {
		_o_._xdb_verify_unsafe_();
		lasthpall = _o_.lasthpall;
		lastiskill = _o_.lastiskill;
		lastkillnum = _o_.lastkillnum;
		newhpall = _o_.newhpall;
		nowhp = _o_.nowhp;
		bossid1 = _o_.bossid1;
		bossid2 = _o_.bossid2;
		bossid3 = _o_.bossid3;
		bossid4 = _o_.bossid4;
		bossiskill = _o_.bossiskill;
		boss1killname = _o_.boss1killname;
		boss2killname = _o_.boss2killname;
		time = _o_.time;
	}

	private void assign(boss.Data _o_) {
		lasthpall = _o_.lasthpall;
		lastiskill = _o_.lastiskill;
		lastkillnum = _o_.lastkillnum;
		newhpall = _o_.newhpall;
		nowhp = _o_.nowhp;
		bossid1 = _o_.bossid1;
		bossid2 = _o_.bossid2;
		bossid3 = _o_.bossid3;
		bossid4 = _o_.bossid4;
		bossiskill = _o_.bossiskill;
		boss1killname = _o_.boss1killname;
		boss2killname = _o_.boss2killname;
		time = _o_.time;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(lasthpall);
		_os_.marshal(lastiskill);
		_os_.marshal(lastkillnum);
		_os_.marshal(newhpall);
		_os_.marshal(nowhp);
		_os_.marshal(bossid1);
		_os_.marshal(bossid2);
		_os_.marshal(bossid3);
		_os_.marshal(bossid4);
		_os_.marshal(bossiskill);
		_os_.marshal(boss1killname, xdb.Const.IO_CHARSET);
		_os_.marshal(boss2killname, xdb.Const.IO_CHARSET);
		_os_.marshal(time);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		lasthpall = _os_.unmarshal_long();
		lastiskill = _os_.unmarshal_int();
		lastkillnum = _os_.unmarshal_long();
		newhpall = _os_.unmarshal_long();
		nowhp = _os_.unmarshal_long();
		bossid1 = _os_.unmarshal_int();
		bossid2 = _os_.unmarshal_int();
		bossid3 = _os_.unmarshal_int();
		bossid4 = _os_.unmarshal_int();
		bossiskill = _os_.unmarshal_int();
		boss1killname = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
		boss2killname = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
		time = _os_.unmarshal_long();
		return _os_;
	}

	@Override
	public xbean.boss copy() {
		_xdb_verify_unsafe_();
		return new boss(this);
	}

	@Override
	public xbean.boss toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.boss toBean() {
		_xdb_verify_unsafe_();
		return new boss(this); // same as copy()
	}

	@Override
	public xbean.boss toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.boss toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public long getLasthpall() { // 上次总血量
		_xdb_verify_unsafe_();
		return lasthpall;
	}

	@Override
	public int getLastiskill() { // 上次是否杀掉，0未杀，1已杀
		_xdb_verify_unsafe_();
		return lastiskill;
	}

	@Override
	public long getLastkillnum() { // 杀掉则为用时（毫秒），未杀则为受到的伤害
		_xdb_verify_unsafe_();
		return lastkillnum;
	}

	@Override
	public long getNewhpall() { // 最新总血量
		_xdb_verify_unsafe_();
		return newhpall;
	}

	@Override
	public long getNowhp() { // 现在血量
		_xdb_verify_unsafe_();
		return nowhp;
	}

	@Override
	public int getBossid1() { // bossid(第一个守门人)
		_xdb_verify_unsafe_();
		return bossid1;
	}

	@Override
	public int getBossid2() { // bossid(第一个boss)
		_xdb_verify_unsafe_();
		return bossid2;
	}

	@Override
	public int getBossid3() { // bossid(第二个守门人)
		_xdb_verify_unsafe_();
		return bossid3;
	}

	@Override
	public int getBossid4() { // bossid(第二个boss)
		_xdb_verify_unsafe_();
		return bossid4;
	}

	@Override
	public int getBossiskill() { // 个位为第一个boss，十位为第二个boss,0为未击杀，1为击杀
		_xdb_verify_unsafe_();
		return bossiskill;
	}

	@Override
	public String getBoss1killname() { // 击杀1者名称
		_xdb_verify_unsafe_();
		return boss1killname;
	}

	@Override
	public com.goldhuman.Common.Octets getBoss1killnameOctets() { // 击杀1者名称
		_xdb_verify_unsafe_();
		return com.goldhuman.Common.Octets.wrap(getBoss1killname(), xdb.Const.IO_CHARSET);
	}

	@Override
	public String getBoss2killname() { // 击杀2者名称
		_xdb_verify_unsafe_();
		return boss2killname;
	}

	@Override
	public com.goldhuman.Common.Octets getBoss2killnameOctets() { // 击杀2者名称
		_xdb_verify_unsafe_();
		return com.goldhuman.Common.Octets.wrap(getBoss2killname(), xdb.Const.IO_CHARSET);
	}

	@Override
	public long getTime() { // 上次刷新时间
		_xdb_verify_unsafe_();
		return time;
	}

	@Override
	public void setLasthpall(long _v_) { // 上次总血量
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "lasthpall") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, lasthpall) {
					public void rollback() { lasthpall = _xdb_saved; }
				};}});
		lasthpall = _v_;
	}

	@Override
	public void setLastiskill(int _v_) { // 上次是否杀掉，0未杀，1已杀
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "lastiskill") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, lastiskill) {
					public void rollback() { lastiskill = _xdb_saved; }
				};}});
		lastiskill = _v_;
	}

	@Override
	public void setLastkillnum(long _v_) { // 杀掉则为用时（毫秒），未杀则为受到的伤害
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "lastkillnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, lastkillnum) {
					public void rollback() { lastkillnum = _xdb_saved; }
				};}});
		lastkillnum = _v_;
	}

	@Override
	public void setNewhpall(long _v_) { // 最新总血量
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "newhpall") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, newhpall) {
					public void rollback() { newhpall = _xdb_saved; }
				};}});
		newhpall = _v_;
	}

	@Override
	public void setNowhp(long _v_) { // 现在血量
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "nowhp") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, nowhp) {
					public void rollback() { nowhp = _xdb_saved; }
				};}});
		nowhp = _v_;
	}

	@Override
	public void setBossid1(int _v_) { // bossid(第一个守门人)
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "bossid1") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, bossid1) {
					public void rollback() { bossid1 = _xdb_saved; }
				};}});
		bossid1 = _v_;
	}

	@Override
	public void setBossid2(int _v_) { // bossid(第一个boss)
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "bossid2") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, bossid2) {
					public void rollback() { bossid2 = _xdb_saved; }
				};}});
		bossid2 = _v_;
	}

	@Override
	public void setBossid3(int _v_) { // bossid(第二个守门人)
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "bossid3") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, bossid3) {
					public void rollback() { bossid3 = _xdb_saved; }
				};}});
		bossid3 = _v_;
	}

	@Override
	public void setBossid4(int _v_) { // bossid(第二个boss)
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "bossid4") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, bossid4) {
					public void rollback() { bossid4 = _xdb_saved; }
				};}});
		bossid4 = _v_;
	}

	@Override
	public void setBossiskill(int _v_) { // 个位为第一个boss，十位为第二个boss,0为未击杀，1为击杀
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "bossiskill") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, bossiskill) {
					public void rollback() { bossiskill = _xdb_saved; }
				};}});
		bossiskill = _v_;
	}

	@Override
	public void setBoss1killname(String _v_) { // 击杀1者名称
		_xdb_verify_unsafe_();
		if (null == _v_)
			throw new NullPointerException();
		xdb.Logs.logIf(new xdb.LogKey(this, "boss1killname") {
			protected xdb.Log create() {
				return new xdb.logs.LogString(this, boss1killname) {
					public void rollback() { boss1killname = _xdb_saved; }
				};}});
		boss1killname = _v_;
	}

	@Override
	public void setBoss1killnameOctets(com.goldhuman.Common.Octets _v_) { // 击杀1者名称
		_xdb_verify_unsafe_();
		this.setBoss1killname(_v_.getString(xdb.Const.IO_CHARSET));
	}

	@Override
	public void setBoss2killname(String _v_) { // 击杀2者名称
		_xdb_verify_unsafe_();
		if (null == _v_)
			throw new NullPointerException();
		xdb.Logs.logIf(new xdb.LogKey(this, "boss2killname") {
			protected xdb.Log create() {
				return new xdb.logs.LogString(this, boss2killname) {
					public void rollback() { boss2killname = _xdb_saved; }
				};}});
		boss2killname = _v_;
	}

	@Override
	public void setBoss2killnameOctets(com.goldhuman.Common.Octets _v_) { // 击杀2者名称
		_xdb_verify_unsafe_();
		this.setBoss2killname(_v_.getString(xdb.Const.IO_CHARSET));
	}

	@Override
	public void setTime(long _v_) { // 上次刷新时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "time") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, time) {
					public void rollback() { time = _xdb_saved; }
				};}});
		time = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		boss _o_ = null;
		if ( _o1_ instanceof boss ) _o_ = (boss)_o1_;
		else if ( _o1_ instanceof boss.Const ) _o_ = ((boss.Const)_o1_).nThis();
		else return false;
		if (lasthpall != _o_.lasthpall) return false;
		if (lastiskill != _o_.lastiskill) return false;
		if (lastkillnum != _o_.lastkillnum) return false;
		if (newhpall != _o_.newhpall) return false;
		if (nowhp != _o_.nowhp) return false;
		if (bossid1 != _o_.bossid1) return false;
		if (bossid2 != _o_.bossid2) return false;
		if (bossid3 != _o_.bossid3) return false;
		if (bossid4 != _o_.bossid4) return false;
		if (bossiskill != _o_.bossiskill) return false;
		if (!boss1killname.equals(_o_.boss1killname)) return false;
		if (!boss2killname.equals(_o_.boss2killname)) return false;
		if (time != _o_.time) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += lasthpall;
		_h_ += lastiskill;
		_h_ += lastkillnum;
		_h_ += newhpall;
		_h_ += nowhp;
		_h_ += bossid1;
		_h_ += bossid2;
		_h_ += bossid3;
		_h_ += bossid4;
		_h_ += bossiskill;
		_h_ += boss1killname.hashCode();
		_h_ += boss2killname.hashCode();
		_h_ += time;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(lasthpall);
		_sb_.append(",");
		_sb_.append(lastiskill);
		_sb_.append(",");
		_sb_.append(lastkillnum);
		_sb_.append(",");
		_sb_.append(newhpall);
		_sb_.append(",");
		_sb_.append(nowhp);
		_sb_.append(",");
		_sb_.append(bossid1);
		_sb_.append(",");
		_sb_.append(bossid2);
		_sb_.append(",");
		_sb_.append(bossid3);
		_sb_.append(",");
		_sb_.append(bossid4);
		_sb_.append(",");
		_sb_.append(bossiskill);
		_sb_.append(",");
		_sb_.append("'").append(boss1killname).append("'");
		_sb_.append(",");
		_sb_.append("'").append(boss2killname).append("'");
		_sb_.append(",");
		_sb_.append(time);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("lasthpall"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("lastiskill"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("lastkillnum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("newhpall"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("nowhp"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("bossid1"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("bossid2"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("bossid3"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("bossid4"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("bossiskill"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("boss1killname"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("boss2killname"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("time"));
		return lb;
	}

	private class Const implements xbean.boss {
		boss nThis() {
			return boss.this;
		}

		@Override
		public xbean.boss copy() {
			return boss.this.copy();
		}

		@Override
		public xbean.boss toData() {
			return boss.this.toData();
		}

		public xbean.boss toBean() {
			return boss.this.toBean();
		}

		@Override
		public xbean.boss toDataIf() {
			return boss.this.toDataIf();
		}

		public xbean.boss toBeanIf() {
			return boss.this.toBeanIf();
		}

		@Override
		public long getLasthpall() { // 上次总血量
			_xdb_verify_unsafe_();
			return lasthpall;
		}

		@Override
		public int getLastiskill() { // 上次是否杀掉，0未杀，1已杀
			_xdb_verify_unsafe_();
			return lastiskill;
		}

		@Override
		public long getLastkillnum() { // 杀掉则为用时（毫秒），未杀则为受到的伤害
			_xdb_verify_unsafe_();
			return lastkillnum;
		}

		@Override
		public long getNewhpall() { // 最新总血量
			_xdb_verify_unsafe_();
			return newhpall;
		}

		@Override
		public long getNowhp() { // 现在血量
			_xdb_verify_unsafe_();
			return nowhp;
		}

		@Override
		public int getBossid1() { // bossid(第一个守门人)
			_xdb_verify_unsafe_();
			return bossid1;
		}

		@Override
		public int getBossid2() { // bossid(第一个boss)
			_xdb_verify_unsafe_();
			return bossid2;
		}

		@Override
		public int getBossid3() { // bossid(第二个守门人)
			_xdb_verify_unsafe_();
			return bossid3;
		}

		@Override
		public int getBossid4() { // bossid(第二个boss)
			_xdb_verify_unsafe_();
			return bossid4;
		}

		@Override
		public int getBossiskill() { // 个位为第一个boss，十位为第二个boss,0为未击杀，1为击杀
			_xdb_verify_unsafe_();
			return bossiskill;
		}

		@Override
		public String getBoss1killname() { // 击杀1者名称
			_xdb_verify_unsafe_();
			return boss1killname;
		}

		@Override
		public com.goldhuman.Common.Octets getBoss1killnameOctets() { // 击杀1者名称
			_xdb_verify_unsafe_();
			return boss.this.getBoss1killnameOctets();
		}

		@Override
		public String getBoss2killname() { // 击杀2者名称
			_xdb_verify_unsafe_();
			return boss2killname;
		}

		@Override
		public com.goldhuman.Common.Octets getBoss2killnameOctets() { // 击杀2者名称
			_xdb_verify_unsafe_();
			return boss.this.getBoss2killnameOctets();
		}

		@Override
		public long getTime() { // 上次刷新时间
			_xdb_verify_unsafe_();
			return time;
		}

		@Override
		public void setLasthpall(long _v_) { // 上次总血量
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setLastiskill(int _v_) { // 上次是否杀掉，0未杀，1已杀
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setLastkillnum(long _v_) { // 杀掉则为用时（毫秒），未杀则为受到的伤害
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setNewhpall(long _v_) { // 最新总血量
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setNowhp(long _v_) { // 现在血量
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setBossid1(int _v_) { // bossid(第一个守门人)
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setBossid2(int _v_) { // bossid(第一个boss)
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setBossid3(int _v_) { // bossid(第二个守门人)
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setBossid4(int _v_) { // bossid(第二个boss)
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setBossiskill(int _v_) { // 个位为第一个boss，十位为第二个boss,0为未击杀，1为击杀
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setBoss1killname(String _v_) { // 击杀1者名称
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setBoss1killnameOctets(com.goldhuman.Common.Octets _v_) { // 击杀1者名称
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setBoss2killname(String _v_) { // 击杀2者名称
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setBoss2killnameOctets(com.goldhuman.Common.Octets _v_) { // 击杀2者名称
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTime(long _v_) { // 上次刷新时间
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
			return boss.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return boss.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return boss.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return boss.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return boss.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return boss.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return boss.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return boss.this.hashCode();
		}

		@Override
		public String toString() {
			return boss.this.toString();
		}

	}

	public static final class Data implements xbean.boss {
		private long lasthpall; // 上次总血量
		private int lastiskill; // 上次是否杀掉，0未杀，1已杀
		private long lastkillnum; // 杀掉则为用时（毫秒），未杀则为受到的伤害
		private long newhpall; // 最新总血量
		private long nowhp; // 现在血量
		private int bossid1; // bossid(第一个守门人)
		private int bossid2; // bossid(第一个boss)
		private int bossid3; // bossid(第二个守门人)
		private int bossid4; // bossid(第二个boss)
		private int bossiskill; // 个位为第一个boss，十位为第二个boss,0为未击杀，1为击杀
		private String boss1killname; // 击杀1者名称
		private String boss2killname; // 击杀2者名称
		private long time; // 上次刷新时间

		public Data() {
			boss1killname = "";
			boss2killname = "";
		}

		Data(xbean.boss _o1_) {
			if (_o1_ instanceof boss) assign((boss)_o1_);
			else if (_o1_ instanceof boss.Data) assign((boss.Data)_o1_);
			else if (_o1_ instanceof boss.Const) assign(((boss.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(boss _o_) {
			lasthpall = _o_.lasthpall;
			lastiskill = _o_.lastiskill;
			lastkillnum = _o_.lastkillnum;
			newhpall = _o_.newhpall;
			nowhp = _o_.nowhp;
			bossid1 = _o_.bossid1;
			bossid2 = _o_.bossid2;
			bossid3 = _o_.bossid3;
			bossid4 = _o_.bossid4;
			bossiskill = _o_.bossiskill;
			boss1killname = _o_.boss1killname;
			boss2killname = _o_.boss2killname;
			time = _o_.time;
		}

		private void assign(boss.Data _o_) {
			lasthpall = _o_.lasthpall;
			lastiskill = _o_.lastiskill;
			lastkillnum = _o_.lastkillnum;
			newhpall = _o_.newhpall;
			nowhp = _o_.nowhp;
			bossid1 = _o_.bossid1;
			bossid2 = _o_.bossid2;
			bossid3 = _o_.bossid3;
			bossid4 = _o_.bossid4;
			bossiskill = _o_.bossiskill;
			boss1killname = _o_.boss1killname;
			boss2killname = _o_.boss2killname;
			time = _o_.time;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(lasthpall);
			_os_.marshal(lastiskill);
			_os_.marshal(lastkillnum);
			_os_.marshal(newhpall);
			_os_.marshal(nowhp);
			_os_.marshal(bossid1);
			_os_.marshal(bossid2);
			_os_.marshal(bossid3);
			_os_.marshal(bossid4);
			_os_.marshal(bossiskill);
			_os_.marshal(boss1killname, xdb.Const.IO_CHARSET);
			_os_.marshal(boss2killname, xdb.Const.IO_CHARSET);
			_os_.marshal(time);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			lasthpall = _os_.unmarshal_long();
			lastiskill = _os_.unmarshal_int();
			lastkillnum = _os_.unmarshal_long();
			newhpall = _os_.unmarshal_long();
			nowhp = _os_.unmarshal_long();
			bossid1 = _os_.unmarshal_int();
			bossid2 = _os_.unmarshal_int();
			bossid3 = _os_.unmarshal_int();
			bossid4 = _os_.unmarshal_int();
			bossiskill = _os_.unmarshal_int();
			boss1killname = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			boss2killname = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			time = _os_.unmarshal_long();
			return _os_;
		}

		@Override
		public xbean.boss copy() {
			return new Data(this);
		}

		@Override
		public xbean.boss toData() {
			return new Data(this);
		}

		public xbean.boss toBean() {
			return new boss(this, null, null);
		}

		@Override
		public xbean.boss toDataIf() {
			return this;
		}

		public xbean.boss toBeanIf() {
			return new boss(this, null, null);
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
		public long getLasthpall() { // 上次总血量
			return lasthpall;
		}

		@Override
		public int getLastiskill() { // 上次是否杀掉，0未杀，1已杀
			return lastiskill;
		}

		@Override
		public long getLastkillnum() { // 杀掉则为用时（毫秒），未杀则为受到的伤害
			return lastkillnum;
		}

		@Override
		public long getNewhpall() { // 最新总血量
			return newhpall;
		}

		@Override
		public long getNowhp() { // 现在血量
			return nowhp;
		}

		@Override
		public int getBossid1() { // bossid(第一个守门人)
			return bossid1;
		}

		@Override
		public int getBossid2() { // bossid(第一个boss)
			return bossid2;
		}

		@Override
		public int getBossid3() { // bossid(第二个守门人)
			return bossid3;
		}

		@Override
		public int getBossid4() { // bossid(第二个boss)
			return bossid4;
		}

		@Override
		public int getBossiskill() { // 个位为第一个boss，十位为第二个boss,0为未击杀，1为击杀
			return bossiskill;
		}

		@Override
		public String getBoss1killname() { // 击杀1者名称
			return boss1killname;
		}

		@Override
		public com.goldhuman.Common.Octets getBoss1killnameOctets() { // 击杀1者名称
			return com.goldhuman.Common.Octets.wrap(getBoss1killname(), xdb.Const.IO_CHARSET);
		}

		@Override
		public String getBoss2killname() { // 击杀2者名称
			return boss2killname;
		}

		@Override
		public com.goldhuman.Common.Octets getBoss2killnameOctets() { // 击杀2者名称
			return com.goldhuman.Common.Octets.wrap(getBoss2killname(), xdb.Const.IO_CHARSET);
		}

		@Override
		public long getTime() { // 上次刷新时间
			return time;
		}

		@Override
		public void setLasthpall(long _v_) { // 上次总血量
			lasthpall = _v_;
		}

		@Override
		public void setLastiskill(int _v_) { // 上次是否杀掉，0未杀，1已杀
			lastiskill = _v_;
		}

		@Override
		public void setLastkillnum(long _v_) { // 杀掉则为用时（毫秒），未杀则为受到的伤害
			lastkillnum = _v_;
		}

		@Override
		public void setNewhpall(long _v_) { // 最新总血量
			newhpall = _v_;
		}

		@Override
		public void setNowhp(long _v_) { // 现在血量
			nowhp = _v_;
		}

		@Override
		public void setBossid1(int _v_) { // bossid(第一个守门人)
			bossid1 = _v_;
		}

		@Override
		public void setBossid2(int _v_) { // bossid(第一个boss)
			bossid2 = _v_;
		}

		@Override
		public void setBossid3(int _v_) { // bossid(第二个守门人)
			bossid3 = _v_;
		}

		@Override
		public void setBossid4(int _v_) { // bossid(第二个boss)
			bossid4 = _v_;
		}

		@Override
		public void setBossiskill(int _v_) { // 个位为第一个boss，十位为第二个boss,0为未击杀，1为击杀
			bossiskill = _v_;
		}

		@Override
		public void setBoss1killname(String _v_) { // 击杀1者名称
			if (null == _v_)
				throw new NullPointerException();
			boss1killname = _v_;
		}

		@Override
		public void setBoss1killnameOctets(com.goldhuman.Common.Octets _v_) { // 击杀1者名称
			this.setBoss1killname(_v_.getString(xdb.Const.IO_CHARSET));
		}

		@Override
		public void setBoss2killname(String _v_) { // 击杀2者名称
			if (null == _v_)
				throw new NullPointerException();
			boss2killname = _v_;
		}

		@Override
		public void setBoss2killnameOctets(com.goldhuman.Common.Octets _v_) { // 击杀2者名称
			this.setBoss2killname(_v_.getString(xdb.Const.IO_CHARSET));
		}

		@Override
		public void setTime(long _v_) { // 上次刷新时间
			time = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof boss.Data)) return false;
			boss.Data _o_ = (boss.Data) _o1_;
			if (lasthpall != _o_.lasthpall) return false;
			if (lastiskill != _o_.lastiskill) return false;
			if (lastkillnum != _o_.lastkillnum) return false;
			if (newhpall != _o_.newhpall) return false;
			if (nowhp != _o_.nowhp) return false;
			if (bossid1 != _o_.bossid1) return false;
			if (bossid2 != _o_.bossid2) return false;
			if (bossid3 != _o_.bossid3) return false;
			if (bossid4 != _o_.bossid4) return false;
			if (bossiskill != _o_.bossiskill) return false;
			if (!boss1killname.equals(_o_.boss1killname)) return false;
			if (!boss2killname.equals(_o_.boss2killname)) return false;
			if (time != _o_.time) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += lasthpall;
			_h_ += lastiskill;
			_h_ += lastkillnum;
			_h_ += newhpall;
			_h_ += nowhp;
			_h_ += bossid1;
			_h_ += bossid2;
			_h_ += bossid3;
			_h_ += bossid4;
			_h_ += bossiskill;
			_h_ += boss1killname.hashCode();
			_h_ += boss2killname.hashCode();
			_h_ += time;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(lasthpall);
			_sb_.append(",");
			_sb_.append(lastiskill);
			_sb_.append(",");
			_sb_.append(lastkillnum);
			_sb_.append(",");
			_sb_.append(newhpall);
			_sb_.append(",");
			_sb_.append(nowhp);
			_sb_.append(",");
			_sb_.append(bossid1);
			_sb_.append(",");
			_sb_.append(bossid2);
			_sb_.append(",");
			_sb_.append(bossid3);
			_sb_.append(",");
			_sb_.append(bossid4);
			_sb_.append(",");
			_sb_.append(bossiskill);
			_sb_.append(",");
			_sb_.append("'").append(boss1killname).append("'");
			_sb_.append(",");
			_sb_.append("'").append(boss2killname).append("'");
			_sb_.append(",");
			_sb_.append(time);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
