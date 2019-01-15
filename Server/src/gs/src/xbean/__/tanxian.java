
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class tanxian extends xdb.XBean implements xbean.tanxian {
	private int tanxianid; // 探险id
	private int tanxiantype; // 状态，0未开启，1进行中，2已完成
	private long endtime; // 结束时间
	private int teamnum; // 队伍号

	tanxian(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
	}

	public tanxian() {
		this(0, null, null);
	}

	public tanxian(tanxian _o_) {
		this(_o_, null, null);
	}

	tanxian(xbean.tanxian _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof tanxian) assign((tanxian)_o1_);
		else if (_o1_ instanceof tanxian.Data) assign((tanxian.Data)_o1_);
		else if (_o1_ instanceof tanxian.Const) assign(((tanxian.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(tanxian _o_) {
		_o_._xdb_verify_unsafe_();
		tanxianid = _o_.tanxianid;
		tanxiantype = _o_.tanxiantype;
		endtime = _o_.endtime;
		teamnum = _o_.teamnum;
	}

	private void assign(tanxian.Data _o_) {
		tanxianid = _o_.tanxianid;
		tanxiantype = _o_.tanxiantype;
		endtime = _o_.endtime;
		teamnum = _o_.teamnum;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(tanxianid);
		_os_.marshal(tanxiantype);
		_os_.marshal(endtime);
		_os_.marshal(teamnum);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		tanxianid = _os_.unmarshal_int();
		tanxiantype = _os_.unmarshal_int();
		endtime = _os_.unmarshal_long();
		teamnum = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.tanxian copy() {
		_xdb_verify_unsafe_();
		return new tanxian(this);
	}

	@Override
	public xbean.tanxian toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.tanxian toBean() {
		_xdb_verify_unsafe_();
		return new tanxian(this); // same as copy()
	}

	@Override
	public xbean.tanxian toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.tanxian toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getTanxianid() { // 探险id
		_xdb_verify_unsafe_();
		return tanxianid;
	}

	@Override
	public int getTanxiantype() { // 状态，0未开启，1进行中，2已完成
		_xdb_verify_unsafe_();
		return tanxiantype;
	}

	@Override
	public long getEndtime() { // 结束时间
		_xdb_verify_unsafe_();
		return endtime;
	}

	@Override
	public int getTeamnum() { // 队伍号
		_xdb_verify_unsafe_();
		return teamnum;
	}

	@Override
	public void setTanxianid(int _v_) { // 探险id
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "tanxianid") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, tanxianid) {
					public void rollback() { tanxianid = _xdb_saved; }
				};}});
		tanxianid = _v_;
	}

	@Override
	public void setTanxiantype(int _v_) { // 状态，0未开启，1进行中，2已完成
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "tanxiantype") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, tanxiantype) {
					public void rollback() { tanxiantype = _xdb_saved; }
				};}});
		tanxiantype = _v_;
	}

	@Override
	public void setEndtime(long _v_) { // 结束时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "endtime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, endtime) {
					public void rollback() { endtime = _xdb_saved; }
				};}});
		endtime = _v_;
	}

	@Override
	public void setTeamnum(int _v_) { // 队伍号
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "teamnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, teamnum) {
					public void rollback() { teamnum = _xdb_saved; }
				};}});
		teamnum = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		tanxian _o_ = null;
		if ( _o1_ instanceof tanxian ) _o_ = (tanxian)_o1_;
		else if ( _o1_ instanceof tanxian.Const ) _o_ = ((tanxian.Const)_o1_).nThis();
		else return false;
		if (tanxianid != _o_.tanxianid) return false;
		if (tanxiantype != _o_.tanxiantype) return false;
		if (endtime != _o_.endtime) return false;
		if (teamnum != _o_.teamnum) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += tanxianid;
		_h_ += tanxiantype;
		_h_ += endtime;
		_h_ += teamnum;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(tanxianid);
		_sb_.append(",");
		_sb_.append(tanxiantype);
		_sb_.append(",");
		_sb_.append(endtime);
		_sb_.append(",");
		_sb_.append(teamnum);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("tanxianid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("tanxiantype"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("endtime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("teamnum"));
		return lb;
	}

	private class Const implements xbean.tanxian {
		tanxian nThis() {
			return tanxian.this;
		}

		@Override
		public xbean.tanxian copy() {
			return tanxian.this.copy();
		}

		@Override
		public xbean.tanxian toData() {
			return tanxian.this.toData();
		}

		public xbean.tanxian toBean() {
			return tanxian.this.toBean();
		}

		@Override
		public xbean.tanxian toDataIf() {
			return tanxian.this.toDataIf();
		}

		public xbean.tanxian toBeanIf() {
			return tanxian.this.toBeanIf();
		}

		@Override
		public int getTanxianid() { // 探险id
			_xdb_verify_unsafe_();
			return tanxianid;
		}

		@Override
		public int getTanxiantype() { // 状态，0未开启，1进行中，2已完成
			_xdb_verify_unsafe_();
			return tanxiantype;
		}

		@Override
		public long getEndtime() { // 结束时间
			_xdb_verify_unsafe_();
			return endtime;
		}

		@Override
		public int getTeamnum() { // 队伍号
			_xdb_verify_unsafe_();
			return teamnum;
		}

		@Override
		public void setTanxianid(int _v_) { // 探险id
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTanxiantype(int _v_) { // 状态，0未开启，1进行中，2已完成
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setEndtime(long _v_) { // 结束时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTeamnum(int _v_) { // 队伍号
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
			return tanxian.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return tanxian.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return tanxian.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return tanxian.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return tanxian.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return tanxian.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return tanxian.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return tanxian.this.hashCode();
		}

		@Override
		public String toString() {
			return tanxian.this.toString();
		}

	}

	public static final class Data implements xbean.tanxian {
		private int tanxianid; // 探险id
		private int tanxiantype; // 状态，0未开启，1进行中，2已完成
		private long endtime; // 结束时间
		private int teamnum; // 队伍号

		public Data() {
		}

		Data(xbean.tanxian _o1_) {
			if (_o1_ instanceof tanxian) assign((tanxian)_o1_);
			else if (_o1_ instanceof tanxian.Data) assign((tanxian.Data)_o1_);
			else if (_o1_ instanceof tanxian.Const) assign(((tanxian.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(tanxian _o_) {
			tanxianid = _o_.tanxianid;
			tanxiantype = _o_.tanxiantype;
			endtime = _o_.endtime;
			teamnum = _o_.teamnum;
		}

		private void assign(tanxian.Data _o_) {
			tanxianid = _o_.tanxianid;
			tanxiantype = _o_.tanxiantype;
			endtime = _o_.endtime;
			teamnum = _o_.teamnum;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(tanxianid);
			_os_.marshal(tanxiantype);
			_os_.marshal(endtime);
			_os_.marshal(teamnum);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			tanxianid = _os_.unmarshal_int();
			tanxiantype = _os_.unmarshal_int();
			endtime = _os_.unmarshal_long();
			teamnum = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.tanxian copy() {
			return new Data(this);
		}

		@Override
		public xbean.tanxian toData() {
			return new Data(this);
		}

		public xbean.tanxian toBean() {
			return new tanxian(this, null, null);
		}

		@Override
		public xbean.tanxian toDataIf() {
			return this;
		}

		public xbean.tanxian toBeanIf() {
			return new tanxian(this, null, null);
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
		public int getTanxianid() { // 探险id
			return tanxianid;
		}

		@Override
		public int getTanxiantype() { // 状态，0未开启，1进行中，2已完成
			return tanxiantype;
		}

		@Override
		public long getEndtime() { // 结束时间
			return endtime;
		}

		@Override
		public int getTeamnum() { // 队伍号
			return teamnum;
		}

		@Override
		public void setTanxianid(int _v_) { // 探险id
			tanxianid = _v_;
		}

		@Override
		public void setTanxiantype(int _v_) { // 状态，0未开启，1进行中，2已完成
			tanxiantype = _v_;
		}

		@Override
		public void setEndtime(long _v_) { // 结束时间
			endtime = _v_;
		}

		@Override
		public void setTeamnum(int _v_) { // 队伍号
			teamnum = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof tanxian.Data)) return false;
			tanxian.Data _o_ = (tanxian.Data) _o1_;
			if (tanxianid != _o_.tanxianid) return false;
			if (tanxiantype != _o_.tanxiantype) return false;
			if (endtime != _o_.endtime) return false;
			if (teamnum != _o_.teamnum) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += tanxianid;
			_h_ += tanxiantype;
			_h_ += endtime;
			_h_ += teamnum;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(tanxianid);
			_sb_.append(",");
			_sb_.append(tanxiantype);
			_sb_.append(",");
			_sb_.append(endtime);
			_sb_.append(",");
			_sb_.append(teamnum);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
