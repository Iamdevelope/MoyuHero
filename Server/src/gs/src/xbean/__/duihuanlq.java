
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class duihuanlq extends xdb.XBean implements xbean.duihuanlq {
	private int lqkey; // 兑换礼券key
	private int typenum; // 兑换礼券替换计数
	private java.util.LinkedList<String> clonelist; // 

	duihuanlq(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		clonelist = new java.util.LinkedList<String>();
	}

	public duihuanlq() {
		this(0, null, null);
	}

	public duihuanlq(duihuanlq _o_) {
		this(_o_, null, null);
	}

	duihuanlq(xbean.duihuanlq _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof duihuanlq) assign((duihuanlq)_o1_);
		else if (_o1_ instanceof duihuanlq.Data) assign((duihuanlq.Data)_o1_);
		else if (_o1_ instanceof duihuanlq.Const) assign(((duihuanlq.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(duihuanlq _o_) {
		_o_._xdb_verify_unsafe_();
		lqkey = _o_.lqkey;
		typenum = _o_.typenum;
		clonelist = new java.util.LinkedList<String>();
		clonelist.addAll(_o_.clonelist);
	}

	private void assign(duihuanlq.Data _o_) {
		lqkey = _o_.lqkey;
		typenum = _o_.typenum;
		clonelist = new java.util.LinkedList<String>();
		clonelist.addAll(_o_.clonelist);
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(lqkey);
		_os_.marshal(typenum);
		_os_.compact_uint32(clonelist.size());
		for (String _v_ : clonelist) {
			_os_.marshal(_v_, xdb.Const.IO_CHARSET);
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		lqkey = _os_.unmarshal_int();
		typenum = _os_.unmarshal_int();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			String _v_ = "";
			_v_ = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			clonelist.add(_v_);
		}
		return _os_;
	}

	@Override
	public xbean.duihuanlq copy() {
		_xdb_verify_unsafe_();
		return new duihuanlq(this);
	}

	@Override
	public xbean.duihuanlq toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.duihuanlq toBean() {
		_xdb_verify_unsafe_();
		return new duihuanlq(this); // same as copy()
	}

	@Override
	public xbean.duihuanlq toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.duihuanlq toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getLqkey() { // 兑换礼券key
		_xdb_verify_unsafe_();
		return lqkey;
	}

	@Override
	public int getTypenum() { // 兑换礼券替换计数
		_xdb_verify_unsafe_();
		return typenum;
	}

	@Override
	public java.util.List<String> getClonelist() { // 
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "clonelist"), clonelist);
	}

	public java.util.List<String> getClonelistAsData() { // 
		_xdb_verify_unsafe_();
		java.util.List<String> clonelist;
		duihuanlq _o_ = this;
		clonelist = new java.util.LinkedList<String>();
		clonelist.addAll(_o_.clonelist);
		return clonelist;
	}

	@Override
	public void setLqkey(int _v_) { // 兑换礼券key
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "lqkey") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, lqkey) {
					public void rollback() { lqkey = _xdb_saved; }
				};}});
		lqkey = _v_;
	}

	@Override
	public void setTypenum(int _v_) { // 兑换礼券替换计数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "typenum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, typenum) {
					public void rollback() { typenum = _xdb_saved; }
				};}});
		typenum = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		duihuanlq _o_ = null;
		if ( _o1_ instanceof duihuanlq ) _o_ = (duihuanlq)_o1_;
		else if ( _o1_ instanceof duihuanlq.Const ) _o_ = ((duihuanlq.Const)_o1_).nThis();
		else return false;
		if (lqkey != _o_.lqkey) return false;
		if (typenum != _o_.typenum) return false;
		if (!clonelist.equals(_o_.clonelist)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += lqkey;
		_h_ += typenum;
		_h_ += clonelist.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(lqkey);
		_sb_.append(",");
		_sb_.append(typenum);
		_sb_.append(",");
		_sb_.append(clonelist);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("lqkey"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("typenum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("clonelist"));
		return lb;
	}

	private class Const implements xbean.duihuanlq {
		duihuanlq nThis() {
			return duihuanlq.this;
		}

		@Override
		public xbean.duihuanlq copy() {
			return duihuanlq.this.copy();
		}

		@Override
		public xbean.duihuanlq toData() {
			return duihuanlq.this.toData();
		}

		public xbean.duihuanlq toBean() {
			return duihuanlq.this.toBean();
		}

		@Override
		public xbean.duihuanlq toDataIf() {
			return duihuanlq.this.toDataIf();
		}

		public xbean.duihuanlq toBeanIf() {
			return duihuanlq.this.toBeanIf();
		}

		@Override
		public int getLqkey() { // 兑换礼券key
			_xdb_verify_unsafe_();
			return lqkey;
		}

		@Override
		public int getTypenum() { // 兑换礼券替换计数
			_xdb_verify_unsafe_();
			return typenum;
		}

		@Override
		public java.util.List<String> getClonelist() { // 
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(clonelist);
		}

		public java.util.List<String> getClonelistAsData() { // 
			_xdb_verify_unsafe_();
			java.util.List<String> clonelist;
			duihuanlq _o_ = duihuanlq.this;
		clonelist = new java.util.LinkedList<String>();
		clonelist.addAll(_o_.clonelist);
			return clonelist;
		}

		@Override
		public void setLqkey(int _v_) { // 兑换礼券key
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTypenum(int _v_) { // 兑换礼券替换计数
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
			return duihuanlq.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return duihuanlq.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return duihuanlq.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return duihuanlq.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return duihuanlq.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return duihuanlq.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return duihuanlq.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return duihuanlq.this.hashCode();
		}

		@Override
		public String toString() {
			return duihuanlq.this.toString();
		}

	}

	public static final class Data implements xbean.duihuanlq {
		private int lqkey; // 兑换礼券key
		private int typenum; // 兑换礼券替换计数
		private java.util.LinkedList<String> clonelist; // 

		public Data() {
			clonelist = new java.util.LinkedList<String>();
		}

		Data(xbean.duihuanlq _o1_) {
			if (_o1_ instanceof duihuanlq) assign((duihuanlq)_o1_);
			else if (_o1_ instanceof duihuanlq.Data) assign((duihuanlq.Data)_o1_);
			else if (_o1_ instanceof duihuanlq.Const) assign(((duihuanlq.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(duihuanlq _o_) {
			lqkey = _o_.lqkey;
			typenum = _o_.typenum;
			clonelist = new java.util.LinkedList<String>();
			clonelist.addAll(_o_.clonelist);
		}

		private void assign(duihuanlq.Data _o_) {
			lqkey = _o_.lqkey;
			typenum = _o_.typenum;
			clonelist = new java.util.LinkedList<String>();
			clonelist.addAll(_o_.clonelist);
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(lqkey);
			_os_.marshal(typenum);
			_os_.compact_uint32(clonelist.size());
			for (String _v_ : clonelist) {
				_os_.marshal(_v_, xdb.Const.IO_CHARSET);
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			lqkey = _os_.unmarshal_int();
			typenum = _os_.unmarshal_int();
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				String _v_ = "";
				_v_ = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
				clonelist.add(_v_);
			}
			return _os_;
		}

		@Override
		public xbean.duihuanlq copy() {
			return new Data(this);
		}

		@Override
		public xbean.duihuanlq toData() {
			return new Data(this);
		}

		public xbean.duihuanlq toBean() {
			return new duihuanlq(this, null, null);
		}

		@Override
		public xbean.duihuanlq toDataIf() {
			return this;
		}

		public xbean.duihuanlq toBeanIf() {
			return new duihuanlq(this, null, null);
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
		public int getLqkey() { // 兑换礼券key
			return lqkey;
		}

		@Override
		public int getTypenum() { // 兑换礼券替换计数
			return typenum;
		}

		@Override
		public java.util.List<String> getClonelist() { // 
			return clonelist;
		}

		@Override
		public java.util.List<String> getClonelistAsData() { // 
			return clonelist;
		}

		@Override
		public void setLqkey(int _v_) { // 兑换礼券key
			lqkey = _v_;
		}

		@Override
		public void setTypenum(int _v_) { // 兑换礼券替换计数
			typenum = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof duihuanlq.Data)) return false;
			duihuanlq.Data _o_ = (duihuanlq.Data) _o1_;
			if (lqkey != _o_.lqkey) return false;
			if (typenum != _o_.typenum) return false;
			if (!clonelist.equals(_o_.clonelist)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += lqkey;
			_h_ += typenum;
			_h_ += clonelist.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(lqkey);
			_sb_.append(",");
			_sb_.append(typenum);
			_sb_.append(",");
			_sb_.append(clonelist);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
