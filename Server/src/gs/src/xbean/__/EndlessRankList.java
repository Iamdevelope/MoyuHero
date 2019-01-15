
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class EndlessRankList extends xdb.XBean implements xbean.EndlessRankList {
	private java.util.LinkedList<xbean.EndlessRankInfo> ranklist; // 排名列表
	private long ranktime; // 排名时间

	EndlessRankList(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		ranklist = new java.util.LinkedList<xbean.EndlessRankInfo>();
	}

	public EndlessRankList() {
		this(0, null, null);
	}

	public EndlessRankList(EndlessRankList _o_) {
		this(_o_, null, null);
	}

	EndlessRankList(xbean.EndlessRankList _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof EndlessRankList) assign((EndlessRankList)_o1_);
		else if (_o1_ instanceof EndlessRankList.Data) assign((EndlessRankList.Data)_o1_);
		else if (_o1_ instanceof EndlessRankList.Const) assign(((EndlessRankList.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(EndlessRankList _o_) {
		_o_._xdb_verify_unsafe_();
		ranklist = new java.util.LinkedList<xbean.EndlessRankInfo>();
		for (xbean.EndlessRankInfo _v_ : _o_.ranklist)
			ranklist.add(new EndlessRankInfo(_v_, this, "ranklist"));
		ranktime = _o_.ranktime;
	}

	private void assign(EndlessRankList.Data _o_) {
		ranklist = new java.util.LinkedList<xbean.EndlessRankInfo>();
		for (xbean.EndlessRankInfo _v_ : _o_.ranklist)
			ranklist.add(new EndlessRankInfo(_v_, this, "ranklist"));
		ranktime = _o_.ranktime;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(ranklist.size());
		for (xbean.EndlessRankInfo _v_ : ranklist) {
			_v_.marshal(_os_);
		}
		_os_.marshal(ranktime);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			xbean.EndlessRankInfo _v_ = new EndlessRankInfo(0, this, "ranklist");
			_v_.unmarshal(_os_);
			ranklist.add(_v_);
		}
		ranktime = _os_.unmarshal_long();
		return _os_;
	}

	@Override
	public xbean.EndlessRankList copy() {
		_xdb_verify_unsafe_();
		return new EndlessRankList(this);
	}

	@Override
	public xbean.EndlessRankList toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.EndlessRankList toBean() {
		_xdb_verify_unsafe_();
		return new EndlessRankList(this); // same as copy()
	}

	@Override
	public xbean.EndlessRankList toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.EndlessRankList toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.List<xbean.EndlessRankInfo> getRanklist() { // 排名列表
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "ranklist"), ranklist);
	}

	public java.util.List<xbean.EndlessRankInfo> getRanklistAsData() { // 排名列表
		_xdb_verify_unsafe_();
		java.util.List<xbean.EndlessRankInfo> ranklist;
		EndlessRankList _o_ = this;
		ranklist = new java.util.LinkedList<xbean.EndlessRankInfo>();
		for (xbean.EndlessRankInfo _v_ : _o_.ranklist)
			ranklist.add(new EndlessRankInfo.Data(_v_));
		return ranklist;
	}

	@Override
	public long getRanktime() { // 排名时间
		_xdb_verify_unsafe_();
		return ranktime;
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
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		EndlessRankList _o_ = null;
		if ( _o1_ instanceof EndlessRankList ) _o_ = (EndlessRankList)_o1_;
		else if ( _o1_ instanceof EndlessRankList.Const ) _o_ = ((EndlessRankList.Const)_o1_).nThis();
		else return false;
		if (!ranklist.equals(_o_.ranklist)) return false;
		if (ranktime != _o_.ranktime) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += ranklist.hashCode();
		_h_ += ranktime;
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
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("ranklist"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("ranktime"));
		return lb;
	}

	private class Const implements xbean.EndlessRankList {
		EndlessRankList nThis() {
			return EndlessRankList.this;
		}

		@Override
		public xbean.EndlessRankList copy() {
			return EndlessRankList.this.copy();
		}

		@Override
		public xbean.EndlessRankList toData() {
			return EndlessRankList.this.toData();
		}

		public xbean.EndlessRankList toBean() {
			return EndlessRankList.this.toBean();
		}

		@Override
		public xbean.EndlessRankList toDataIf() {
			return EndlessRankList.this.toDataIf();
		}

		public xbean.EndlessRankList toBeanIf() {
			return EndlessRankList.this.toBeanIf();
		}

		@Override
		public java.util.List<xbean.EndlessRankInfo> getRanklist() { // 排名列表
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(ranklist);
		}

		public java.util.List<xbean.EndlessRankInfo> getRanklistAsData() { // 排名列表
			_xdb_verify_unsafe_();
			java.util.List<xbean.EndlessRankInfo> ranklist;
			EndlessRankList _o_ = EndlessRankList.this;
		ranklist = new java.util.LinkedList<xbean.EndlessRankInfo>();
		for (xbean.EndlessRankInfo _v_ : _o_.ranklist)
			ranklist.add(new EndlessRankInfo.Data(_v_));
			return ranklist;
		}

		@Override
		public long getRanktime() { // 排名时间
			_xdb_verify_unsafe_();
			return ranktime;
		}

		@Override
		public void setRanktime(long _v_) { // 排名时间
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
			return EndlessRankList.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return EndlessRankList.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return EndlessRankList.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return EndlessRankList.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return EndlessRankList.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return EndlessRankList.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return EndlessRankList.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return EndlessRankList.this.hashCode();
		}

		@Override
		public String toString() {
			return EndlessRankList.this.toString();
		}

	}

	public static final class Data implements xbean.EndlessRankList {
		private java.util.LinkedList<xbean.EndlessRankInfo> ranklist; // 排名列表
		private long ranktime; // 排名时间

		public Data() {
			ranklist = new java.util.LinkedList<xbean.EndlessRankInfo>();
		}

		Data(xbean.EndlessRankList _o1_) {
			if (_o1_ instanceof EndlessRankList) assign((EndlessRankList)_o1_);
			else if (_o1_ instanceof EndlessRankList.Data) assign((EndlessRankList.Data)_o1_);
			else if (_o1_ instanceof EndlessRankList.Const) assign(((EndlessRankList.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(EndlessRankList _o_) {
			ranklist = new java.util.LinkedList<xbean.EndlessRankInfo>();
			for (xbean.EndlessRankInfo _v_ : _o_.ranklist)
				ranklist.add(new EndlessRankInfo.Data(_v_));
			ranktime = _o_.ranktime;
		}

		private void assign(EndlessRankList.Data _o_) {
			ranklist = new java.util.LinkedList<xbean.EndlessRankInfo>();
			for (xbean.EndlessRankInfo _v_ : _o_.ranklist)
				ranklist.add(new EndlessRankInfo.Data(_v_));
			ranktime = _o_.ranktime;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(ranklist.size());
			for (xbean.EndlessRankInfo _v_ : ranklist) {
				_v_.marshal(_os_);
			}
			_os_.marshal(ranktime);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				xbean.EndlessRankInfo _v_ = xbean.Pod.newEndlessRankInfoData();
				_v_.unmarshal(_os_);
				ranklist.add(_v_);
			}
			ranktime = _os_.unmarshal_long();
			return _os_;
		}

		@Override
		public xbean.EndlessRankList copy() {
			return new Data(this);
		}

		@Override
		public xbean.EndlessRankList toData() {
			return new Data(this);
		}

		public xbean.EndlessRankList toBean() {
			return new EndlessRankList(this, null, null);
		}

		@Override
		public xbean.EndlessRankList toDataIf() {
			return this;
		}

		public xbean.EndlessRankList toBeanIf() {
			return new EndlessRankList(this, null, null);
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
		public java.util.List<xbean.EndlessRankInfo> getRanklist() { // 排名列表
			return ranklist;
		}

		@Override
		public java.util.List<xbean.EndlessRankInfo> getRanklistAsData() { // 排名列表
			return ranklist;
		}

		@Override
		public long getRanktime() { // 排名时间
			return ranktime;
		}

		@Override
		public void setRanktime(long _v_) { // 排名时间
			ranktime = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof EndlessRankList.Data)) return false;
			EndlessRankList.Data _o_ = (EndlessRankList.Data) _o1_;
			if (!ranklist.equals(_o_.ranklist)) return false;
			if (ranktime != _o_.ranktime) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += ranklist.hashCode();
			_h_ += ranktime;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(ranklist);
			_sb_.append(",");
			_sb_.append(ranktime);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
