
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class BloodRankList extends xdb.XBean implements xbean.BloodRankList {
	private int curweek; // 
	private java.util.LinkedList<xbean.BloodRankRole> rankers; // 以前已加成的效果

	BloodRankList(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		rankers = new java.util.LinkedList<xbean.BloodRankRole>();
	}

	public BloodRankList() {
		this(0, null, null);
	}

	public BloodRankList(BloodRankList _o_) {
		this(_o_, null, null);
	}

	BloodRankList(xbean.BloodRankList _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof BloodRankList) assign((BloodRankList)_o1_);
		else if (_o1_ instanceof BloodRankList.Data) assign((BloodRankList.Data)_o1_);
		else if (_o1_ instanceof BloodRankList.Const) assign(((BloodRankList.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(BloodRankList _o_) {
		_o_._xdb_verify_unsafe_();
		curweek = _o_.curweek;
		rankers = new java.util.LinkedList<xbean.BloodRankRole>();
		for (xbean.BloodRankRole _v_ : _o_.rankers)
			rankers.add(new BloodRankRole(_v_, this, "rankers"));
	}

	private void assign(BloodRankList.Data _o_) {
		curweek = _o_.curweek;
		rankers = new java.util.LinkedList<xbean.BloodRankRole>();
		for (xbean.BloodRankRole _v_ : _o_.rankers)
			rankers.add(new BloodRankRole(_v_, this, "rankers"));
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(curweek);
		_os_.compact_uint32(rankers.size());
		for (xbean.BloodRankRole _v_ : rankers) {
			_v_.marshal(_os_);
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		curweek = _os_.unmarshal_int();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			xbean.BloodRankRole _v_ = new BloodRankRole(0, this, "rankers");
			_v_.unmarshal(_os_);
			rankers.add(_v_);
		}
		return _os_;
	}

	@Override
	public xbean.BloodRankList copy() {
		_xdb_verify_unsafe_();
		return new BloodRankList(this);
	}

	@Override
	public xbean.BloodRankList toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.BloodRankList toBean() {
		_xdb_verify_unsafe_();
		return new BloodRankList(this); // same as copy()
	}

	@Override
	public xbean.BloodRankList toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.BloodRankList toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getCurweek() { // 
		_xdb_verify_unsafe_();
		return curweek;
	}

	@Override
	public java.util.List<xbean.BloodRankRole> getRankers() { // 以前已加成的效果
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "rankers"), rankers);
	}

	public java.util.List<xbean.BloodRankRole> getRankersAsData() { // 以前已加成的效果
		_xdb_verify_unsafe_();
		java.util.List<xbean.BloodRankRole> rankers;
		BloodRankList _o_ = this;
		rankers = new java.util.LinkedList<xbean.BloodRankRole>();
		for (xbean.BloodRankRole _v_ : _o_.rankers)
			rankers.add(new BloodRankRole.Data(_v_));
		return rankers;
	}

	@Override
	public void setCurweek(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "curweek") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, curweek) {
					public void rollback() { curweek = _xdb_saved; }
				};}});
		curweek = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		BloodRankList _o_ = null;
		if ( _o1_ instanceof BloodRankList ) _o_ = (BloodRankList)_o1_;
		else if ( _o1_ instanceof BloodRankList.Const ) _o_ = ((BloodRankList.Const)_o1_).nThis();
		else return false;
		if (curweek != _o_.curweek) return false;
		if (!rankers.equals(_o_.rankers)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += curweek;
		_h_ += rankers.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(curweek);
		_sb_.append(",");
		_sb_.append(rankers);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("curweek"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("rankers"));
		return lb;
	}

	private class Const implements xbean.BloodRankList {
		BloodRankList nThis() {
			return BloodRankList.this;
		}

		@Override
		public xbean.BloodRankList copy() {
			return BloodRankList.this.copy();
		}

		@Override
		public xbean.BloodRankList toData() {
			return BloodRankList.this.toData();
		}

		public xbean.BloodRankList toBean() {
			return BloodRankList.this.toBean();
		}

		@Override
		public xbean.BloodRankList toDataIf() {
			return BloodRankList.this.toDataIf();
		}

		public xbean.BloodRankList toBeanIf() {
			return BloodRankList.this.toBeanIf();
		}

		@Override
		public int getCurweek() { // 
			_xdb_verify_unsafe_();
			return curweek;
		}

		@Override
		public java.util.List<xbean.BloodRankRole> getRankers() { // 以前已加成的效果
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(rankers);
		}

		public java.util.List<xbean.BloodRankRole> getRankersAsData() { // 以前已加成的效果
			_xdb_verify_unsafe_();
			java.util.List<xbean.BloodRankRole> rankers;
			BloodRankList _o_ = BloodRankList.this;
		rankers = new java.util.LinkedList<xbean.BloodRankRole>();
		for (xbean.BloodRankRole _v_ : _o_.rankers)
			rankers.add(new BloodRankRole.Data(_v_));
			return rankers;
		}

		@Override
		public void setCurweek(int _v_) { // 
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
			return BloodRankList.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return BloodRankList.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return BloodRankList.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return BloodRankList.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return BloodRankList.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return BloodRankList.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return BloodRankList.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return BloodRankList.this.hashCode();
		}

		@Override
		public String toString() {
			return BloodRankList.this.toString();
		}

	}

	public static final class Data implements xbean.BloodRankList {
		private int curweek; // 
		private java.util.LinkedList<xbean.BloodRankRole> rankers; // 以前已加成的效果

		public Data() {
			rankers = new java.util.LinkedList<xbean.BloodRankRole>();
		}

		Data(xbean.BloodRankList _o1_) {
			if (_o1_ instanceof BloodRankList) assign((BloodRankList)_o1_);
			else if (_o1_ instanceof BloodRankList.Data) assign((BloodRankList.Data)_o1_);
			else if (_o1_ instanceof BloodRankList.Const) assign(((BloodRankList.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(BloodRankList _o_) {
			curweek = _o_.curweek;
			rankers = new java.util.LinkedList<xbean.BloodRankRole>();
			for (xbean.BloodRankRole _v_ : _o_.rankers)
				rankers.add(new BloodRankRole.Data(_v_));
		}

		private void assign(BloodRankList.Data _o_) {
			curweek = _o_.curweek;
			rankers = new java.util.LinkedList<xbean.BloodRankRole>();
			for (xbean.BloodRankRole _v_ : _o_.rankers)
				rankers.add(new BloodRankRole.Data(_v_));
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(curweek);
			_os_.compact_uint32(rankers.size());
			for (xbean.BloodRankRole _v_ : rankers) {
				_v_.marshal(_os_);
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			curweek = _os_.unmarshal_int();
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				xbean.BloodRankRole _v_ = xbean.Pod.newBloodRankRoleData();
				_v_.unmarshal(_os_);
				rankers.add(_v_);
			}
			return _os_;
		}

		@Override
		public xbean.BloodRankList copy() {
			return new Data(this);
		}

		@Override
		public xbean.BloodRankList toData() {
			return new Data(this);
		}

		public xbean.BloodRankList toBean() {
			return new BloodRankList(this, null, null);
		}

		@Override
		public xbean.BloodRankList toDataIf() {
			return this;
		}

		public xbean.BloodRankList toBeanIf() {
			return new BloodRankList(this, null, null);
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
		public int getCurweek() { // 
			return curweek;
		}

		@Override
		public java.util.List<xbean.BloodRankRole> getRankers() { // 以前已加成的效果
			return rankers;
		}

		@Override
		public java.util.List<xbean.BloodRankRole> getRankersAsData() { // 以前已加成的效果
			return rankers;
		}

		@Override
		public void setCurweek(int _v_) { // 
			curweek = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof BloodRankList.Data)) return false;
			BloodRankList.Data _o_ = (BloodRankList.Data) _o1_;
			if (curweek != _o_.curweek) return false;
			if (!rankers.equals(_o_.rankers)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += curweek;
			_h_ += rankers.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(curweek);
			_sb_.append(",");
			_sb_.append(rankers);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
