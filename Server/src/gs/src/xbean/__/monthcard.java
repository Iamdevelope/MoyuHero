
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class monthcard extends xdb.XBean implements xbean.monthcard {
	private int monthcardid; // 月卡id
	private long overtime; // 到期时间
	private long getboxlasttime; // 领取奖励最后一次时间

	monthcard(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
	}

	public monthcard() {
		this(0, null, null);
	}

	public monthcard(monthcard _o_) {
		this(_o_, null, null);
	}

	monthcard(xbean.monthcard _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof monthcard) assign((monthcard)_o1_);
		else if (_o1_ instanceof monthcard.Data) assign((monthcard.Data)_o1_);
		else if (_o1_ instanceof monthcard.Const) assign(((monthcard.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(monthcard _o_) {
		_o_._xdb_verify_unsafe_();
		monthcardid = _o_.monthcardid;
		overtime = _o_.overtime;
		getboxlasttime = _o_.getboxlasttime;
	}

	private void assign(monthcard.Data _o_) {
		monthcardid = _o_.monthcardid;
		overtime = _o_.overtime;
		getboxlasttime = _o_.getboxlasttime;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(monthcardid);
		_os_.marshal(overtime);
		_os_.marshal(getboxlasttime);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		monthcardid = _os_.unmarshal_int();
		overtime = _os_.unmarshal_long();
		getboxlasttime = _os_.unmarshal_long();
		return _os_;
	}

	@Override
	public xbean.monthcard copy() {
		_xdb_verify_unsafe_();
		return new monthcard(this);
	}

	@Override
	public xbean.monthcard toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.monthcard toBean() {
		_xdb_verify_unsafe_();
		return new monthcard(this); // same as copy()
	}

	@Override
	public xbean.monthcard toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.monthcard toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getMonthcardid() { // 月卡id
		_xdb_verify_unsafe_();
		return monthcardid;
	}

	@Override
	public long getOvertime() { // 到期时间
		_xdb_verify_unsafe_();
		return overtime;
	}

	@Override
	public long getGetboxlasttime() { // 领取奖励最后一次时间
		_xdb_verify_unsafe_();
		return getboxlasttime;
	}

	@Override
	public void setMonthcardid(int _v_) { // 月卡id
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "monthcardid") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, monthcardid) {
					public void rollback() { monthcardid = _xdb_saved; }
				};}});
		monthcardid = _v_;
	}

	@Override
	public void setOvertime(long _v_) { // 到期时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "overtime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, overtime) {
					public void rollback() { overtime = _xdb_saved; }
				};}});
		overtime = _v_;
	}

	@Override
	public void setGetboxlasttime(long _v_) { // 领取奖励最后一次时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "getboxlasttime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, getboxlasttime) {
					public void rollback() { getboxlasttime = _xdb_saved; }
				};}});
		getboxlasttime = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		monthcard _o_ = null;
		if ( _o1_ instanceof monthcard ) _o_ = (monthcard)_o1_;
		else if ( _o1_ instanceof monthcard.Const ) _o_ = ((monthcard.Const)_o1_).nThis();
		else return false;
		if (monthcardid != _o_.monthcardid) return false;
		if (overtime != _o_.overtime) return false;
		if (getboxlasttime != _o_.getboxlasttime) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += monthcardid;
		_h_ += overtime;
		_h_ += getboxlasttime;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(monthcardid);
		_sb_.append(",");
		_sb_.append(overtime);
		_sb_.append(",");
		_sb_.append(getboxlasttime);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("monthcardid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("overtime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("getboxlasttime"));
		return lb;
	}

	private class Const implements xbean.monthcard {
		monthcard nThis() {
			return monthcard.this;
		}

		@Override
		public xbean.monthcard copy() {
			return monthcard.this.copy();
		}

		@Override
		public xbean.monthcard toData() {
			return monthcard.this.toData();
		}

		public xbean.monthcard toBean() {
			return monthcard.this.toBean();
		}

		@Override
		public xbean.monthcard toDataIf() {
			return monthcard.this.toDataIf();
		}

		public xbean.monthcard toBeanIf() {
			return monthcard.this.toBeanIf();
		}

		@Override
		public int getMonthcardid() { // 月卡id
			_xdb_verify_unsafe_();
			return monthcardid;
		}

		@Override
		public long getOvertime() { // 到期时间
			_xdb_verify_unsafe_();
			return overtime;
		}

		@Override
		public long getGetboxlasttime() { // 领取奖励最后一次时间
			_xdb_verify_unsafe_();
			return getboxlasttime;
		}

		@Override
		public void setMonthcardid(int _v_) { // 月卡id
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setOvertime(long _v_) { // 到期时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setGetboxlasttime(long _v_) { // 领取奖励最后一次时间
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
			return monthcard.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return monthcard.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return monthcard.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return monthcard.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return monthcard.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return monthcard.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return monthcard.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return monthcard.this.hashCode();
		}

		@Override
		public String toString() {
			return monthcard.this.toString();
		}

	}

	public static final class Data implements xbean.monthcard {
		private int monthcardid; // 月卡id
		private long overtime; // 到期时间
		private long getboxlasttime; // 领取奖励最后一次时间

		public Data() {
		}

		Data(xbean.monthcard _o1_) {
			if (_o1_ instanceof monthcard) assign((monthcard)_o1_);
			else if (_o1_ instanceof monthcard.Data) assign((monthcard.Data)_o1_);
			else if (_o1_ instanceof monthcard.Const) assign(((monthcard.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(monthcard _o_) {
			monthcardid = _o_.monthcardid;
			overtime = _o_.overtime;
			getboxlasttime = _o_.getboxlasttime;
		}

		private void assign(monthcard.Data _o_) {
			monthcardid = _o_.monthcardid;
			overtime = _o_.overtime;
			getboxlasttime = _o_.getboxlasttime;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(monthcardid);
			_os_.marshal(overtime);
			_os_.marshal(getboxlasttime);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			monthcardid = _os_.unmarshal_int();
			overtime = _os_.unmarshal_long();
			getboxlasttime = _os_.unmarshal_long();
			return _os_;
		}

		@Override
		public xbean.monthcard copy() {
			return new Data(this);
		}

		@Override
		public xbean.monthcard toData() {
			return new Data(this);
		}

		public xbean.monthcard toBean() {
			return new monthcard(this, null, null);
		}

		@Override
		public xbean.monthcard toDataIf() {
			return this;
		}

		public xbean.monthcard toBeanIf() {
			return new monthcard(this, null, null);
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
		public int getMonthcardid() { // 月卡id
			return monthcardid;
		}

		@Override
		public long getOvertime() { // 到期时间
			return overtime;
		}

		@Override
		public long getGetboxlasttime() { // 领取奖励最后一次时间
			return getboxlasttime;
		}

		@Override
		public void setMonthcardid(int _v_) { // 月卡id
			monthcardid = _v_;
		}

		@Override
		public void setOvertime(long _v_) { // 到期时间
			overtime = _v_;
		}

		@Override
		public void setGetboxlasttime(long _v_) { // 领取奖励最后一次时间
			getboxlasttime = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof monthcard.Data)) return false;
			monthcard.Data _o_ = (monthcard.Data) _o1_;
			if (monthcardid != _o_.monthcardid) return false;
			if (overtime != _o_.overtime) return false;
			if (getboxlasttime != _o_.getboxlasttime) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += monthcardid;
			_h_ += overtime;
			_h_ += getboxlasttime;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(monthcardid);
			_sb_.append(",");
			_sb_.append(overtime);
			_sb_.append(",");
			_sb_.append(getboxlasttime);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
