
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class teamtanxian extends xdb.XBean implements xbean.teamtanxian {
	private int tanxianid; // 探险id
	private java.util.LinkedList<Integer> team; // 小队英雄key列表

	teamtanxian(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		team = new java.util.LinkedList<Integer>();
	}

	public teamtanxian() {
		this(0, null, null);
	}

	public teamtanxian(teamtanxian _o_) {
		this(_o_, null, null);
	}

	teamtanxian(xbean.teamtanxian _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof teamtanxian) assign((teamtanxian)_o1_);
		else if (_o1_ instanceof teamtanxian.Data) assign((teamtanxian.Data)_o1_);
		else if (_o1_ instanceof teamtanxian.Const) assign(((teamtanxian.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(teamtanxian _o_) {
		_o_._xdb_verify_unsafe_();
		tanxianid = _o_.tanxianid;
		team = new java.util.LinkedList<Integer>();
		team.addAll(_o_.team);
	}

	private void assign(teamtanxian.Data _o_) {
		tanxianid = _o_.tanxianid;
		team = new java.util.LinkedList<Integer>();
		team.addAll(_o_.team);
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(tanxianid);
		_os_.compact_uint32(team.size());
		for (Integer _v_ : team) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		tanxianid = _os_.unmarshal_int();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _v_ = 0;
			_v_ = _os_.unmarshal_int();
			team.add(_v_);
		}
		return _os_;
	}

	@Override
	public xbean.teamtanxian copy() {
		_xdb_verify_unsafe_();
		return new teamtanxian(this);
	}

	@Override
	public xbean.teamtanxian toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.teamtanxian toBean() {
		_xdb_verify_unsafe_();
		return new teamtanxian(this); // same as copy()
	}

	@Override
	public xbean.teamtanxian toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.teamtanxian toBeanIf() {
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
	public java.util.List<Integer> getTeam() { // 小队英雄key列表
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "team"), team);
	}

	public java.util.List<Integer> getTeamAsData() { // 小队英雄key列表
		_xdb_verify_unsafe_();
		java.util.List<Integer> team;
		teamtanxian _o_ = this;
		team = new java.util.LinkedList<Integer>();
		team.addAll(_o_.team);
		return team;
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
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		teamtanxian _o_ = null;
		if ( _o1_ instanceof teamtanxian ) _o_ = (teamtanxian)_o1_;
		else if ( _o1_ instanceof teamtanxian.Const ) _o_ = ((teamtanxian.Const)_o1_).nThis();
		else return false;
		if (tanxianid != _o_.tanxianid) return false;
		if (!team.equals(_o_.team)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += tanxianid;
		_h_ += team.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(tanxianid);
		_sb_.append(",");
		_sb_.append(team);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("tanxianid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("team"));
		return lb;
	}

	private class Const implements xbean.teamtanxian {
		teamtanxian nThis() {
			return teamtanxian.this;
		}

		@Override
		public xbean.teamtanxian copy() {
			return teamtanxian.this.copy();
		}

		@Override
		public xbean.teamtanxian toData() {
			return teamtanxian.this.toData();
		}

		public xbean.teamtanxian toBean() {
			return teamtanxian.this.toBean();
		}

		@Override
		public xbean.teamtanxian toDataIf() {
			return teamtanxian.this.toDataIf();
		}

		public xbean.teamtanxian toBeanIf() {
			return teamtanxian.this.toBeanIf();
		}

		@Override
		public int getTanxianid() { // 探险id
			_xdb_verify_unsafe_();
			return tanxianid;
		}

		@Override
		public java.util.List<Integer> getTeam() { // 小队英雄key列表
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(team);
		}

		public java.util.List<Integer> getTeamAsData() { // 小队英雄key列表
			_xdb_verify_unsafe_();
			java.util.List<Integer> team;
			teamtanxian _o_ = teamtanxian.this;
		team = new java.util.LinkedList<Integer>();
		team.addAll(_o_.team);
			return team;
		}

		@Override
		public void setTanxianid(int _v_) { // 探险id
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
			return teamtanxian.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return teamtanxian.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return teamtanxian.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return teamtanxian.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return teamtanxian.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return teamtanxian.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return teamtanxian.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return teamtanxian.this.hashCode();
		}

		@Override
		public String toString() {
			return teamtanxian.this.toString();
		}

	}

	public static final class Data implements xbean.teamtanxian {
		private int tanxianid; // 探险id
		private java.util.LinkedList<Integer> team; // 小队英雄key列表

		public Data() {
			team = new java.util.LinkedList<Integer>();
		}

		Data(xbean.teamtanxian _o1_) {
			if (_o1_ instanceof teamtanxian) assign((teamtanxian)_o1_);
			else if (_o1_ instanceof teamtanxian.Data) assign((teamtanxian.Data)_o1_);
			else if (_o1_ instanceof teamtanxian.Const) assign(((teamtanxian.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(teamtanxian _o_) {
			tanxianid = _o_.tanxianid;
			team = new java.util.LinkedList<Integer>();
			team.addAll(_o_.team);
		}

		private void assign(teamtanxian.Data _o_) {
			tanxianid = _o_.tanxianid;
			team = new java.util.LinkedList<Integer>();
			team.addAll(_o_.team);
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(tanxianid);
			_os_.compact_uint32(team.size());
			for (Integer _v_ : team) {
				_os_.marshal(_v_);
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			tanxianid = _os_.unmarshal_int();
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				int _v_ = 0;
				_v_ = _os_.unmarshal_int();
				team.add(_v_);
			}
			return _os_;
		}

		@Override
		public xbean.teamtanxian copy() {
			return new Data(this);
		}

		@Override
		public xbean.teamtanxian toData() {
			return new Data(this);
		}

		public xbean.teamtanxian toBean() {
			return new teamtanxian(this, null, null);
		}

		@Override
		public xbean.teamtanxian toDataIf() {
			return this;
		}

		public xbean.teamtanxian toBeanIf() {
			return new teamtanxian(this, null, null);
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
		public java.util.List<Integer> getTeam() { // 小队英雄key列表
			return team;
		}

		@Override
		public java.util.List<Integer> getTeamAsData() { // 小队英雄key列表
			return team;
		}

		@Override
		public void setTanxianid(int _v_) { // 探险id
			tanxianid = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof teamtanxian.Data)) return false;
			teamtanxian.Data _o_ = (teamtanxian.Data) _o1_;
			if (tanxianid != _o_.tanxianid) return false;
			if (!team.equals(_o_.team)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += tanxianid;
			_h_ += team.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(tanxianid);
			_sb_.append(",");
			_sb_.append(team);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
