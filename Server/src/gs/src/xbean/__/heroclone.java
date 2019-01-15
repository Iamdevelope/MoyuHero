
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class heroclone extends xdb.XBean implements xbean.heroclone {
	private java.util.LinkedList<Integer> clonelist; // 英雄克隆信息列表

	heroclone(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		clonelist = new java.util.LinkedList<Integer>();
	}

	public heroclone() {
		this(0, null, null);
	}

	public heroclone(heroclone _o_) {
		this(_o_, null, null);
	}

	heroclone(xbean.heroclone _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof heroclone) assign((heroclone)_o1_);
		else if (_o1_ instanceof heroclone.Data) assign((heroclone.Data)_o1_);
		else if (_o1_ instanceof heroclone.Const) assign(((heroclone.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(heroclone _o_) {
		_o_._xdb_verify_unsafe_();
		clonelist = new java.util.LinkedList<Integer>();
		clonelist.addAll(_o_.clonelist);
	}

	private void assign(heroclone.Data _o_) {
		clonelist = new java.util.LinkedList<Integer>();
		clonelist.addAll(_o_.clonelist);
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(clonelist.size());
		for (Integer _v_ : clonelist) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _v_ = 0;
			_v_ = _os_.unmarshal_int();
			clonelist.add(_v_);
		}
		return _os_;
	}

	@Override
	public xbean.heroclone copy() {
		_xdb_verify_unsafe_();
		return new heroclone(this);
	}

	@Override
	public xbean.heroclone toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.heroclone toBean() {
		_xdb_verify_unsafe_();
		return new heroclone(this); // same as copy()
	}

	@Override
	public xbean.heroclone toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.heroclone toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.List<Integer> getClonelist() { // 英雄克隆信息列表
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "clonelist"), clonelist);
	}

	public java.util.List<Integer> getClonelistAsData() { // 英雄克隆信息列表
		_xdb_verify_unsafe_();
		java.util.List<Integer> clonelist;
		heroclone _o_ = this;
		clonelist = new java.util.LinkedList<Integer>();
		clonelist.addAll(_o_.clonelist);
		return clonelist;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		heroclone _o_ = null;
		if ( _o1_ instanceof heroclone ) _o_ = (heroclone)_o1_;
		else if ( _o1_ instanceof heroclone.Const ) _o_ = ((heroclone.Const)_o1_).nThis();
		else return false;
		if (!clonelist.equals(_o_.clonelist)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += clonelist.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(clonelist);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("clonelist"));
		return lb;
	}

	private class Const implements xbean.heroclone {
		heroclone nThis() {
			return heroclone.this;
		}

		@Override
		public xbean.heroclone copy() {
			return heroclone.this.copy();
		}

		@Override
		public xbean.heroclone toData() {
			return heroclone.this.toData();
		}

		public xbean.heroclone toBean() {
			return heroclone.this.toBean();
		}

		@Override
		public xbean.heroclone toDataIf() {
			return heroclone.this.toDataIf();
		}

		public xbean.heroclone toBeanIf() {
			return heroclone.this.toBeanIf();
		}

		@Override
		public java.util.List<Integer> getClonelist() { // 英雄克隆信息列表
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(clonelist);
		}

		public java.util.List<Integer> getClonelistAsData() { // 英雄克隆信息列表
			_xdb_verify_unsafe_();
			java.util.List<Integer> clonelist;
			heroclone _o_ = heroclone.this;
		clonelist = new java.util.LinkedList<Integer>();
		clonelist.addAll(_o_.clonelist);
			return clonelist;
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
			return heroclone.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return heroclone.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return heroclone.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return heroclone.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return heroclone.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return heroclone.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return heroclone.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return heroclone.this.hashCode();
		}

		@Override
		public String toString() {
			return heroclone.this.toString();
		}

	}

	public static final class Data implements xbean.heroclone {
		private java.util.LinkedList<Integer> clonelist; // 英雄克隆信息列表

		public Data() {
			clonelist = new java.util.LinkedList<Integer>();
		}

		Data(xbean.heroclone _o1_) {
			if (_o1_ instanceof heroclone) assign((heroclone)_o1_);
			else if (_o1_ instanceof heroclone.Data) assign((heroclone.Data)_o1_);
			else if (_o1_ instanceof heroclone.Const) assign(((heroclone.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(heroclone _o_) {
			clonelist = new java.util.LinkedList<Integer>();
			clonelist.addAll(_o_.clonelist);
		}

		private void assign(heroclone.Data _o_) {
			clonelist = new java.util.LinkedList<Integer>();
			clonelist.addAll(_o_.clonelist);
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(clonelist.size());
			for (Integer _v_ : clonelist) {
				_os_.marshal(_v_);
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				int _v_ = 0;
				_v_ = _os_.unmarshal_int();
				clonelist.add(_v_);
			}
			return _os_;
		}

		@Override
		public xbean.heroclone copy() {
			return new Data(this);
		}

		@Override
		public xbean.heroclone toData() {
			return new Data(this);
		}

		public xbean.heroclone toBean() {
			return new heroclone(this, null, null);
		}

		@Override
		public xbean.heroclone toDataIf() {
			return this;
		}

		public xbean.heroclone toBeanIf() {
			return new heroclone(this, null, null);
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
		public java.util.List<Integer> getClonelist() { // 英雄克隆信息列表
			return clonelist;
		}

		@Override
		public java.util.List<Integer> getClonelistAsData() { // 英雄克隆信息列表
			return clonelist;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof heroclone.Data)) return false;
			heroclone.Data _o_ = (heroclone.Data) _o1_;
			if (!clonelist.equals(_o_.clonelist)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += clonelist.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(clonelist);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
