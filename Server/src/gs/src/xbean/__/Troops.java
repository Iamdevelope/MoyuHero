
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class Troops extends xdb.XBean implements xbean.Troops {
	private java.util.LinkedList<xbean.Troop> troops; // 

	Troops(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		troops = new java.util.LinkedList<xbean.Troop>();
	}

	public Troops() {
		this(0, null, null);
	}

	public Troops(Troops _o_) {
		this(_o_, null, null);
	}

	Troops(xbean.Troops _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof Troops) assign((Troops)_o1_);
		else if (_o1_ instanceof Troops.Data) assign((Troops.Data)_o1_);
		else if (_o1_ instanceof Troops.Const) assign(((Troops.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(Troops _o_) {
		_o_._xdb_verify_unsafe_();
		troops = new java.util.LinkedList<xbean.Troop>();
		for (xbean.Troop _v_ : _o_.troops)
			troops.add(new Troop(_v_, this, "troops"));
	}

	private void assign(Troops.Data _o_) {
		troops = new java.util.LinkedList<xbean.Troop>();
		for (xbean.Troop _v_ : _o_.troops)
			troops.add(new Troop(_v_, this, "troops"));
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(troops.size());
		for (xbean.Troop _v_ : troops) {
			_v_.marshal(_os_);
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			xbean.Troop _v_ = new Troop(0, this, "troops");
			_v_.unmarshal(_os_);
			troops.add(_v_);
		}
		return _os_;
	}

	@Override
	public xbean.Troops copy() {
		_xdb_verify_unsafe_();
		return new Troops(this);
	}

	@Override
	public xbean.Troops toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.Troops toBean() {
		_xdb_verify_unsafe_();
		return new Troops(this); // same as copy()
	}

	@Override
	public xbean.Troops toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.Troops toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.List<xbean.Troop> getTroops() { // 
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "troops"), troops);
	}

	public java.util.List<xbean.Troop> getTroopsAsData() { // 
		_xdb_verify_unsafe_();
		java.util.List<xbean.Troop> troops;
		Troops _o_ = this;
		troops = new java.util.LinkedList<xbean.Troop>();
		for (xbean.Troop _v_ : _o_.troops)
			troops.add(new Troop.Data(_v_));
		return troops;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		Troops _o_ = null;
		if ( _o1_ instanceof Troops ) _o_ = (Troops)_o1_;
		else if ( _o1_ instanceof Troops.Const ) _o_ = ((Troops.Const)_o1_).nThis();
		else return false;
		if (!troops.equals(_o_.troops)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += troops.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(troops);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("troops"));
		return lb;
	}

	private class Const implements xbean.Troops {
		Troops nThis() {
			return Troops.this;
		}

		@Override
		public xbean.Troops copy() {
			return Troops.this.copy();
		}

		@Override
		public xbean.Troops toData() {
			return Troops.this.toData();
		}

		public xbean.Troops toBean() {
			return Troops.this.toBean();
		}

		@Override
		public xbean.Troops toDataIf() {
			return Troops.this.toDataIf();
		}

		public xbean.Troops toBeanIf() {
			return Troops.this.toBeanIf();
		}

		@Override
		public java.util.List<xbean.Troop> getTroops() { // 
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(troops);
		}

		public java.util.List<xbean.Troop> getTroopsAsData() { // 
			_xdb_verify_unsafe_();
			java.util.List<xbean.Troop> troops;
			Troops _o_ = Troops.this;
		troops = new java.util.LinkedList<xbean.Troop>();
		for (xbean.Troop _v_ : _o_.troops)
			troops.add(new Troop.Data(_v_));
			return troops;
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
			return Troops.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return Troops.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return Troops.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return Troops.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return Troops.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return Troops.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return Troops.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return Troops.this.hashCode();
		}

		@Override
		public String toString() {
			return Troops.this.toString();
		}

	}

	public static final class Data implements xbean.Troops {
		private java.util.LinkedList<xbean.Troop> troops; // 

		public Data() {
			troops = new java.util.LinkedList<xbean.Troop>();
		}

		Data(xbean.Troops _o1_) {
			if (_o1_ instanceof Troops) assign((Troops)_o1_);
			else if (_o1_ instanceof Troops.Data) assign((Troops.Data)_o1_);
			else if (_o1_ instanceof Troops.Const) assign(((Troops.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(Troops _o_) {
			troops = new java.util.LinkedList<xbean.Troop>();
			for (xbean.Troop _v_ : _o_.troops)
				troops.add(new Troop.Data(_v_));
		}

		private void assign(Troops.Data _o_) {
			troops = new java.util.LinkedList<xbean.Troop>();
			for (xbean.Troop _v_ : _o_.troops)
				troops.add(new Troop.Data(_v_));
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(troops.size());
			for (xbean.Troop _v_ : troops) {
				_v_.marshal(_os_);
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				xbean.Troop _v_ = xbean.Pod.newTroopData();
				_v_.unmarshal(_os_);
				troops.add(_v_);
			}
			return _os_;
		}

		@Override
		public xbean.Troops copy() {
			return new Data(this);
		}

		@Override
		public xbean.Troops toData() {
			return new Data(this);
		}

		public xbean.Troops toBean() {
			return new Troops(this, null, null);
		}

		@Override
		public xbean.Troops toDataIf() {
			return this;
		}

		public xbean.Troops toBeanIf() {
			return new Troops(this, null, null);
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
		public java.util.List<xbean.Troop> getTroops() { // 
			return troops;
		}

		@Override
		public java.util.List<xbean.Troop> getTroopsAsData() { // 
			return troops;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof Troops.Data)) return false;
			Troops.Data _o_ = (Troops.Data) _o1_;
			if (!troops.equals(_o_.troops)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += troops.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(troops);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
