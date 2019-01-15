
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class bossRankList extends xdb.XBean implements xbean.bossRankList {
	private java.util.LinkedList<xbean.bossRankInfo> ranklist; // 排名列表
	private long ranktime; // 排名时间
	private int bossid; // bossid：1234

	bossRankList(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		ranklist = new java.util.LinkedList<xbean.bossRankInfo>();
	}

	public bossRankList() {
		this(0, null, null);
	}

	public bossRankList(bossRankList _o_) {
		this(_o_, null, null);
	}

	bossRankList(xbean.bossRankList _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof bossRankList) assign((bossRankList)_o1_);
		else if (_o1_ instanceof bossRankList.Data) assign((bossRankList.Data)_o1_);
		else if (_o1_ instanceof bossRankList.Const) assign(((bossRankList.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(bossRankList _o_) {
		_o_._xdb_verify_unsafe_();
		ranklist = new java.util.LinkedList<xbean.bossRankInfo>();
		for (xbean.bossRankInfo _v_ : _o_.ranklist)
			ranklist.add(new bossRankInfo(_v_, this, "ranklist"));
		ranktime = _o_.ranktime;
		bossid = _o_.bossid;
	}

	private void assign(bossRankList.Data _o_) {
		ranklist = new java.util.LinkedList<xbean.bossRankInfo>();
		for (xbean.bossRankInfo _v_ : _o_.ranklist)
			ranklist.add(new bossRankInfo(_v_, this, "ranklist"));
		ranktime = _o_.ranktime;
		bossid = _o_.bossid;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(ranklist.size());
		for (xbean.bossRankInfo _v_ : ranklist) {
			_v_.marshal(_os_);
		}
		_os_.marshal(ranktime);
		_os_.marshal(bossid);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			xbean.bossRankInfo _v_ = new bossRankInfo(0, this, "ranklist");
			_v_.unmarshal(_os_);
			ranklist.add(_v_);
		}
		ranktime = _os_.unmarshal_long();
		bossid = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.bossRankList copy() {
		_xdb_verify_unsafe_();
		return new bossRankList(this);
	}

	@Override
	public xbean.bossRankList toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.bossRankList toBean() {
		_xdb_verify_unsafe_();
		return new bossRankList(this); // same as copy()
	}

	@Override
	public xbean.bossRankList toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.bossRankList toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.List<xbean.bossRankInfo> getRanklist() { // 排名列表
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "ranklist"), ranklist);
	}

	public java.util.List<xbean.bossRankInfo> getRanklistAsData() { // 排名列表
		_xdb_verify_unsafe_();
		java.util.List<xbean.bossRankInfo> ranklist;
		bossRankList _o_ = this;
		ranklist = new java.util.LinkedList<xbean.bossRankInfo>();
		for (xbean.bossRankInfo _v_ : _o_.ranklist)
			ranklist.add(new bossRankInfo.Data(_v_));
		return ranklist;
	}

	@Override
	public long getRanktime() { // 排名时间
		_xdb_verify_unsafe_();
		return ranktime;
	}

	@Override
	public int getBossid() { // bossid：1234
		_xdb_verify_unsafe_();
		return bossid;
	}

	@Override
	public void setRanktime(long _v_) { // 排名时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "ranktime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, ranktime) {
					public void rollback() { ranktime = _xdb_saved; }
				};}});
		ranktime = _v_;
	}

	@Override
	public void setBossid(int _v_) { // bossid：1234
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "bossid") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, bossid) {
					public void rollback() { bossid = _xdb_saved; }
				};}});
		bossid = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		bossRankList _o_ = null;
		if ( _o1_ instanceof bossRankList ) _o_ = (bossRankList)_o1_;
		else if ( _o1_ instanceof bossRankList.Const ) _o_ = ((bossRankList.Const)_o1_).nThis();
		else return false;
		if (!ranklist.equals(_o_.ranklist)) return false;
		if (ranktime != _o_.ranktime) return false;
		if (bossid != _o_.bossid) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += ranklist.hashCode();
		_h_ += ranktime;
		_h_ += bossid;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(ranklist);
		_sb_.append(",");
		_sb_.append(ranktime);
		_sb_.append(",");
		_sb_.append(bossid);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("ranklist"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("ranktime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("bossid"));
		return lb;
	}

	private class Const implements xbean.bossRankList {
		bossRankList nThis() {
			return bossRankList.this;
		}

		@Override
		public xbean.bossRankList copy() {
			return bossRankList.this.copy();
		}

		@Override
		public xbean.bossRankList toData() {
			return bossRankList.this.toData();
		}

		public xbean.bossRankList toBean() {
			return bossRankList.this.toBean();
		}

		@Override
		public xbean.bossRankList toDataIf() {
			return bossRankList.this.toDataIf();
		}

		public xbean.bossRankList toBeanIf() {
			return bossRankList.this.toBeanIf();
		}

		@Override
		public java.util.List<xbean.bossRankInfo> getRanklist() { // 排名列表
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(ranklist);
		}

		public java.util.List<xbean.bossRankInfo> getRanklistAsData() { // 排名列表
			_xdb_verify_unsafe_();
			java.util.List<xbean.bossRankInfo> ranklist;
			bossRankList _o_ = bossRankList.this;
		ranklist = new java.util.LinkedList<xbean.bossRankInfo>();
		for (xbean.bossRankInfo _v_ : _o_.ranklist)
			ranklist.add(new bossRankInfo.Data(_v_));
			return ranklist;
		}

		@Override
		public long getRanktime() { // 排名时间
			_xdb_verify_unsafe_();
			return ranktime;
		}

		@Override
		public int getBossid() { // bossid：1234
			_xdb_verify_unsafe_();
			return bossid;
		}

		@Override
		public void setRanktime(long _v_) { // 排名时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setBossid(int _v_) { // bossid：1234
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
			return bossRankList.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return bossRankList.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return bossRankList.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return bossRankList.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return bossRankList.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return bossRankList.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return bossRankList.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return bossRankList.this.hashCode();
		}

		@Override
		public String toString() {
			return bossRankList.this.toString();
		}

	}

	public static final class Data implements xbean.bossRankList {
		private java.util.LinkedList<xbean.bossRankInfo> ranklist; // 排名列表
		private long ranktime; // 排名时间
		private int bossid; // bossid：1234

		public Data() {
			ranklist = new java.util.LinkedList<xbean.bossRankInfo>();
		}

		Data(xbean.bossRankList _o1_) {
			if (_o1_ instanceof bossRankList) assign((bossRankList)_o1_);
			else if (_o1_ instanceof bossRankList.Data) assign((bossRankList.Data)_o1_);
			else if (_o1_ instanceof bossRankList.Const) assign(((bossRankList.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(bossRankList _o_) {
			ranklist = new java.util.LinkedList<xbean.bossRankInfo>();
			for (xbean.bossRankInfo _v_ : _o_.ranklist)
				ranklist.add(new bossRankInfo.Data(_v_));
			ranktime = _o_.ranktime;
			bossid = _o_.bossid;
		}

		private void assign(bossRankList.Data _o_) {
			ranklist = new java.util.LinkedList<xbean.bossRankInfo>();
			for (xbean.bossRankInfo _v_ : _o_.ranklist)
				ranklist.add(new bossRankInfo.Data(_v_));
			ranktime = _o_.ranktime;
			bossid = _o_.bossid;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(ranklist.size());
			for (xbean.bossRankInfo _v_ : ranklist) {
				_v_.marshal(_os_);
			}
			_os_.marshal(ranktime);
			_os_.marshal(bossid);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				xbean.bossRankInfo _v_ = xbean.Pod.newbossRankInfoData();
				_v_.unmarshal(_os_);
				ranklist.add(_v_);
			}
			ranktime = _os_.unmarshal_long();
			bossid = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.bossRankList copy() {
			return new Data(this);
		}

		@Override
		public xbean.bossRankList toData() {
			return new Data(this);
		}

		public xbean.bossRankList toBean() {
			return new bossRankList(this, null, null);
		}

		@Override
		public xbean.bossRankList toDataIf() {
			return this;
		}

		public xbean.bossRankList toBeanIf() {
			return new bossRankList(this, null, null);
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
		public java.util.List<xbean.bossRankInfo> getRanklist() { // 排名列表
			return ranklist;
		}

		@Override
		public java.util.List<xbean.bossRankInfo> getRanklistAsData() { // 排名列表
			return ranklist;
		}

		@Override
		public long getRanktime() { // 排名时间
			return ranktime;
		}

		@Override
		public int getBossid() { // bossid：1234
			return bossid;
		}

		@Override
		public void setRanktime(long _v_) { // 排名时间
			ranktime = _v_;
		}

		@Override
		public void setBossid(int _v_) { // bossid：1234
			bossid = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof bossRankList.Data)) return false;
			bossRankList.Data _o_ = (bossRankList.Data) _o1_;
			if (!ranklist.equals(_o_.ranklist)) return false;
			if (ranktime != _o_.ranktime) return false;
			if (bossid != _o_.bossid) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += ranklist.hashCode();
			_h_ += ranktime;
			_h_ += bossid;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(ranklist);
			_sb_.append(",");
			_sb_.append(ranktime);
			_sb_.append(",");
			_sb_.append(bossid);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
