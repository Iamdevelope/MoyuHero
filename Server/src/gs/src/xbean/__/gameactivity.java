
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class gameactivity extends xdb.XBean implements xbean.gameactivity {
	private int id; // 活动id
	private long time; // 最近一次时间
	private int todaynum; // 今日次数
	private int allnum; // 累计次数
	private int cangetnum; // 可以领取次数（）
	private int activitynum; // 活动计数
	private int allactivitynum; // 累计计数
	private int issee; // 是否看过（提示用，0未看，1已看）

	gameactivity(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
	}

	public gameactivity() {
		this(0, null, null);
	}

	public gameactivity(gameactivity _o_) {
		this(_o_, null, null);
	}

	gameactivity(xbean.gameactivity _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof gameactivity) assign((gameactivity)_o1_);
		else if (_o1_ instanceof gameactivity.Data) assign((gameactivity.Data)_o1_);
		else if (_o1_ instanceof gameactivity.Const) assign(((gameactivity.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(gameactivity _o_) {
		_o_._xdb_verify_unsafe_();
		id = _o_.id;
		time = _o_.time;
		todaynum = _o_.todaynum;
		allnum = _o_.allnum;
		cangetnum = _o_.cangetnum;
		activitynum = _o_.activitynum;
		allactivitynum = _o_.allactivitynum;
		issee = _o_.issee;
	}

	private void assign(gameactivity.Data _o_) {
		id = _o_.id;
		time = _o_.time;
		todaynum = _o_.todaynum;
		allnum = _o_.allnum;
		cangetnum = _o_.cangetnum;
		activitynum = _o_.activitynum;
		allactivitynum = _o_.allactivitynum;
		issee = _o_.issee;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(id);
		_os_.marshal(time);
		_os_.marshal(todaynum);
		_os_.marshal(allnum);
		_os_.marshal(cangetnum);
		_os_.marshal(activitynum);
		_os_.marshal(allactivitynum);
		_os_.marshal(issee);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		id = _os_.unmarshal_int();
		time = _os_.unmarshal_long();
		todaynum = _os_.unmarshal_int();
		allnum = _os_.unmarshal_int();
		cangetnum = _os_.unmarshal_int();
		activitynum = _os_.unmarshal_int();
		allactivitynum = _os_.unmarshal_int();
		issee = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.gameactivity copy() {
		_xdb_verify_unsafe_();
		return new gameactivity(this);
	}

	@Override
	public xbean.gameactivity toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.gameactivity toBean() {
		_xdb_verify_unsafe_();
		return new gameactivity(this); // same as copy()
	}

	@Override
	public xbean.gameactivity toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.gameactivity toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getId() { // 活动id
		_xdb_verify_unsafe_();
		return id;
	}

	@Override
	public long getTime() { // 最近一次时间
		_xdb_verify_unsafe_();
		return time;
	}

	@Override
	public int getTodaynum() { // 今日次数
		_xdb_verify_unsafe_();
		return todaynum;
	}

	@Override
	public int getAllnum() { // 累计次数
		_xdb_verify_unsafe_();
		return allnum;
	}

	@Override
	public int getCangetnum() { // 可以领取次数（）
		_xdb_verify_unsafe_();
		return cangetnum;
	}

	@Override
	public int getActivitynum() { // 活动计数
		_xdb_verify_unsafe_();
		return activitynum;
	}

	@Override
	public int getAllactivitynum() { // 累计计数
		_xdb_verify_unsafe_();
		return allactivitynum;
	}

	@Override
	public int getIssee() { // 是否看过（提示用，0未看，1已看）
		_xdb_verify_unsafe_();
		return issee;
	}

	@Override
	public void setId(int _v_) { // 活动id
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "id") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, id) {
					public void rollback() { id = _xdb_saved; }
				};}});
		id = _v_;
	}

	@Override
	public void setTime(long _v_) { // 最近一次时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "time") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, time) {
					public void rollback() { time = _xdb_saved; }
				};}});
		time = _v_;
	}

	@Override
	public void setTodaynum(int _v_) { // 今日次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "todaynum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, todaynum) {
					public void rollback() { todaynum = _xdb_saved; }
				};}});
		todaynum = _v_;
	}

	@Override
	public void setAllnum(int _v_) { // 累计次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "allnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, allnum) {
					public void rollback() { allnum = _xdb_saved; }
				};}});
		allnum = _v_;
	}

	@Override
	public void setCangetnum(int _v_) { // 可以领取次数（）
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "cangetnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, cangetnum) {
					public void rollback() { cangetnum = _xdb_saved; }
				};}});
		cangetnum = _v_;
	}

	@Override
	public void setActivitynum(int _v_) { // 活动计数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "activitynum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, activitynum) {
					public void rollback() { activitynum = _xdb_saved; }
				};}});
		activitynum = _v_;
	}

	@Override
	public void setAllactivitynum(int _v_) { // 累计计数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "allactivitynum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, allactivitynum) {
					public void rollback() { allactivitynum = _xdb_saved; }
				};}});
		allactivitynum = _v_;
	}

	@Override
	public void setIssee(int _v_) { // 是否看过（提示用，0未看，1已看）
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "issee") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, issee) {
					public void rollback() { issee = _xdb_saved; }
				};}});
		issee = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		gameactivity _o_ = null;
		if ( _o1_ instanceof gameactivity ) _o_ = (gameactivity)_o1_;
		else if ( _o1_ instanceof gameactivity.Const ) _o_ = ((gameactivity.Const)_o1_).nThis();
		else return false;
		if (id != _o_.id) return false;
		if (time != _o_.time) return false;
		if (todaynum != _o_.todaynum) return false;
		if (allnum != _o_.allnum) return false;
		if (cangetnum != _o_.cangetnum) return false;
		if (activitynum != _o_.activitynum) return false;
		if (allactivitynum != _o_.allactivitynum) return false;
		if (issee != _o_.issee) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += id;
		_h_ += time;
		_h_ += todaynum;
		_h_ += allnum;
		_h_ += cangetnum;
		_h_ += activitynum;
		_h_ += allactivitynum;
		_h_ += issee;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(id);
		_sb_.append(",");
		_sb_.append(time);
		_sb_.append(",");
		_sb_.append(todaynum);
		_sb_.append(",");
		_sb_.append(allnum);
		_sb_.append(",");
		_sb_.append(cangetnum);
		_sb_.append(",");
		_sb_.append(activitynum);
		_sb_.append(",");
		_sb_.append(allactivitynum);
		_sb_.append(",");
		_sb_.append(issee);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("id"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("time"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("todaynum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("allnum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("cangetnum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("activitynum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("allactivitynum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("issee"));
		return lb;
	}

	private class Const implements xbean.gameactivity {
		gameactivity nThis() {
			return gameactivity.this;
		}

		@Override
		public xbean.gameactivity copy() {
			return gameactivity.this.copy();
		}

		@Override
		public xbean.gameactivity toData() {
			return gameactivity.this.toData();
		}

		public xbean.gameactivity toBean() {
			return gameactivity.this.toBean();
		}

		@Override
		public xbean.gameactivity toDataIf() {
			return gameactivity.this.toDataIf();
		}

		public xbean.gameactivity toBeanIf() {
			return gameactivity.this.toBeanIf();
		}

		@Override
		public int getId() { // 活动id
			_xdb_verify_unsafe_();
			return id;
		}

		@Override
		public long getTime() { // 最近一次时间
			_xdb_verify_unsafe_();
			return time;
		}

		@Override
		public int getTodaynum() { // 今日次数
			_xdb_verify_unsafe_();
			return todaynum;
		}

		@Override
		public int getAllnum() { // 累计次数
			_xdb_verify_unsafe_();
			return allnum;
		}

		@Override
		public int getCangetnum() { // 可以领取次数（）
			_xdb_verify_unsafe_();
			return cangetnum;
		}

		@Override
		public int getActivitynum() { // 活动计数
			_xdb_verify_unsafe_();
			return activitynum;
		}

		@Override
		public int getAllactivitynum() { // 累计计数
			_xdb_verify_unsafe_();
			return allactivitynum;
		}

		@Override
		public int getIssee() { // 是否看过（提示用，0未看，1已看）
			_xdb_verify_unsafe_();
			return issee;
		}

		@Override
		public void setId(int _v_) { // 活动id
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTime(long _v_) { // 最近一次时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTodaynum(int _v_) { // 今日次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setAllnum(int _v_) { // 累计次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setCangetnum(int _v_) { // 可以领取次数（）
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setActivitynum(int _v_) { // 活动计数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setAllactivitynum(int _v_) { // 累计计数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setIssee(int _v_) { // 是否看过（提示用，0未看，1已看）
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
			return gameactivity.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return gameactivity.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return gameactivity.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return gameactivity.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return gameactivity.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return gameactivity.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return gameactivity.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return gameactivity.this.hashCode();
		}

		@Override
		public String toString() {
			return gameactivity.this.toString();
		}

	}

	public static final class Data implements xbean.gameactivity {
		private int id; // 活动id
		private long time; // 最近一次时间
		private int todaynum; // 今日次数
		private int allnum; // 累计次数
		private int cangetnum; // 可以领取次数（）
		private int activitynum; // 活动计数
		private int allactivitynum; // 累计计数
		private int issee; // 是否看过（提示用，0未看，1已看）

		public Data() {
		}

		Data(xbean.gameactivity _o1_) {
			if (_o1_ instanceof gameactivity) assign((gameactivity)_o1_);
			else if (_o1_ instanceof gameactivity.Data) assign((gameactivity.Data)_o1_);
			else if (_o1_ instanceof gameactivity.Const) assign(((gameactivity.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(gameactivity _o_) {
			id = _o_.id;
			time = _o_.time;
			todaynum = _o_.todaynum;
			allnum = _o_.allnum;
			cangetnum = _o_.cangetnum;
			activitynum = _o_.activitynum;
			allactivitynum = _o_.allactivitynum;
			issee = _o_.issee;
		}

		private void assign(gameactivity.Data _o_) {
			id = _o_.id;
			time = _o_.time;
			todaynum = _o_.todaynum;
			allnum = _o_.allnum;
			cangetnum = _o_.cangetnum;
			activitynum = _o_.activitynum;
			allactivitynum = _o_.allactivitynum;
			issee = _o_.issee;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(id);
			_os_.marshal(time);
			_os_.marshal(todaynum);
			_os_.marshal(allnum);
			_os_.marshal(cangetnum);
			_os_.marshal(activitynum);
			_os_.marshal(allactivitynum);
			_os_.marshal(issee);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			id = _os_.unmarshal_int();
			time = _os_.unmarshal_long();
			todaynum = _os_.unmarshal_int();
			allnum = _os_.unmarshal_int();
			cangetnum = _os_.unmarshal_int();
			activitynum = _os_.unmarshal_int();
			allactivitynum = _os_.unmarshal_int();
			issee = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.gameactivity copy() {
			return new Data(this);
		}

		@Override
		public xbean.gameactivity toData() {
			return new Data(this);
		}

		public xbean.gameactivity toBean() {
			return new gameactivity(this, null, null);
		}

		@Override
		public xbean.gameactivity toDataIf() {
			return this;
		}

		public xbean.gameactivity toBeanIf() {
			return new gameactivity(this, null, null);
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
		public int getId() { // 活动id
			return id;
		}

		@Override
		public long getTime() { // 最近一次时间
			return time;
		}

		@Override
		public int getTodaynum() { // 今日次数
			return todaynum;
		}

		@Override
		public int getAllnum() { // 累计次数
			return allnum;
		}

		@Override
		public int getCangetnum() { // 可以领取次数（）
			return cangetnum;
		}

		@Override
		public int getActivitynum() { // 活动计数
			return activitynum;
		}

		@Override
		public int getAllactivitynum() { // 累计计数
			return allactivitynum;
		}

		@Override
		public int getIssee() { // 是否看过（提示用，0未看，1已看）
			return issee;
		}

		@Override
		public void setId(int _v_) { // 活动id
			id = _v_;
		}

		@Override
		public void setTime(long _v_) { // 最近一次时间
			time = _v_;
		}

		@Override
		public void setTodaynum(int _v_) { // 今日次数
			todaynum = _v_;
		}

		@Override
		public void setAllnum(int _v_) { // 累计次数
			allnum = _v_;
		}

		@Override
		public void setCangetnum(int _v_) { // 可以领取次数（）
			cangetnum = _v_;
		}

		@Override
		public void setActivitynum(int _v_) { // 活动计数
			activitynum = _v_;
		}

		@Override
		public void setAllactivitynum(int _v_) { // 累计计数
			allactivitynum = _v_;
		}

		@Override
		public void setIssee(int _v_) { // 是否看过（提示用，0未看，1已看）
			issee = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof gameactivity.Data)) return false;
			gameactivity.Data _o_ = (gameactivity.Data) _o1_;
			if (id != _o_.id) return false;
			if (time != _o_.time) return false;
			if (todaynum != _o_.todaynum) return false;
			if (allnum != _o_.allnum) return false;
			if (cangetnum != _o_.cangetnum) return false;
			if (activitynum != _o_.activitynum) return false;
			if (allactivitynum != _o_.allactivitynum) return false;
			if (issee != _o_.issee) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += id;
			_h_ += time;
			_h_ += todaynum;
			_h_ += allnum;
			_h_ += cangetnum;
			_h_ += activitynum;
			_h_ += allactivitynum;
			_h_ += issee;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(id);
			_sb_.append(",");
			_sb_.append(time);
			_sb_.append(",");
			_sb_.append(todaynum);
			_sb_.append(",");
			_sb_.append(allnum);
			_sb_.append(",");
			_sb_.append(cangetnum);
			_sb_.append(",");
			_sb_.append(activitynum);
			_sb_.append(",");
			_sb_.append(allactivitynum);
			_sb_.append(",");
			_sb_.append(issee);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
