
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class monthcards extends xdb.XBean implements xbean.monthcards {
	private java.util.HashMap<Integer, xbean.monthcard> rolemonthcards; // 

	monthcards(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		rolemonthcards = new java.util.HashMap<Integer, xbean.monthcard>();
	}

	public monthcards() {
		this(0, null, null);
	}

	public monthcards(monthcards _o_) {
		this(_o_, null, null);
	}

	monthcards(xbean.monthcards _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof monthcards) assign((monthcards)_o1_);
		else if (_o1_ instanceof monthcards.Data) assign((monthcards.Data)_o1_);
		else if (_o1_ instanceof monthcards.Const) assign(((monthcards.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(monthcards _o_) {
		_o_._xdb_verify_unsafe_();
		rolemonthcards = new java.util.HashMap<Integer, xbean.monthcard>();
		for (java.util.Map.Entry<Integer, xbean.monthcard> _e_ : _o_.rolemonthcards.entrySet())
			rolemonthcards.put(_e_.getKey(), new monthcard(_e_.getValue(), this, "rolemonthcards"));
	}

	private void assign(monthcards.Data _o_) {
		rolemonthcards = new java.util.HashMap<Integer, xbean.monthcard>();
		for (java.util.Map.Entry<Integer, xbean.monthcard> _e_ : _o_.rolemonthcards.entrySet())
			rolemonthcards.put(_e_.getKey(), new monthcard(_e_.getValue(), this, "rolemonthcards"));
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(rolemonthcards.size());
		for (java.util.Map.Entry<Integer, xbean.monthcard> _e_ : rolemonthcards.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_e_.getValue().marshal(_os_);
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				rolemonthcards = new java.util.HashMap<Integer, xbean.monthcard>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				xbean.monthcard _v_ = new monthcard(0, this, "rolemonthcards");
				_v_.unmarshal(_os_);
				rolemonthcards.put(_k_, _v_);
			}
		}
		return _os_;
	}

	@Override
	public xbean.monthcards copy() {
		_xdb_verify_unsafe_();
		return new monthcards(this);
	}

	@Override
	public xbean.monthcards toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.monthcards toBean() {
		_xdb_verify_unsafe_();
		return new monthcards(this); // same as copy()
	}

	@Override
	public xbean.monthcards toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.monthcards toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.Map<Integer, xbean.monthcard> getRolemonthcards() { // 
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "rolemonthcards"), rolemonthcards);
	}

	@Override
	public java.util.Map<Integer, xbean.monthcard> getRolemonthcardsAsData() { // 
		_xdb_verify_unsafe_();
		java.util.Map<Integer, xbean.monthcard> rolemonthcards;
		monthcards _o_ = this;
		rolemonthcards = new java.util.HashMap<Integer, xbean.monthcard>();
		for (java.util.Map.Entry<Integer, xbean.monthcard> _e_ : _o_.rolemonthcards.entrySet())
			rolemonthcards.put(_e_.getKey(), new monthcard.Data(_e_.getValue()));
		return rolemonthcards;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		monthcards _o_ = null;
		if ( _o1_ instanceof monthcards ) _o_ = (monthcards)_o1_;
		else if ( _o1_ instanceof monthcards.Const ) _o_ = ((monthcards.Const)_o1_).nThis();
		else return false;
		if (!rolemonthcards.equals(_o_.rolemonthcards)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += rolemonthcards.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(rolemonthcards);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableMap().setVarName("rolemonthcards"));
		return lb;
	}

	private class Const implements xbean.monthcards {
		monthcards nThis() {
			return monthcards.this;
		}

		@Override
		public xbean.monthcards copy() {
			return monthcards.this.copy();
		}

		@Override
		public xbean.monthcards toData() {
			return monthcards.this.toData();
		}

		public xbean.monthcards toBean() {
			return monthcards.this.toBean();
		}

		@Override
		public xbean.monthcards toDataIf() {
			return monthcards.this.toDataIf();
		}

		public xbean.monthcards toBeanIf() {
			return monthcards.this.toBeanIf();
		}

		@Override
		public java.util.Map<Integer, xbean.monthcard> getRolemonthcards() { // 
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(rolemonthcards);
		}

		@Override
		public java.util.Map<Integer, xbean.monthcard> getRolemonthcardsAsData() { // 
			_xdb_verify_unsafe_();
			java.util.Map<Integer, xbean.monthcard> rolemonthcards;
			monthcards _o_ = monthcards.this;
			rolemonthcards = new java.util.HashMap<Integer, xbean.monthcard>();
			for (java.util.Map.Entry<Integer, xbean.monthcard> _e_ : _o_.rolemonthcards.entrySet())
				rolemonthcards.put(_e_.getKey(), new monthcard.Data(_e_.getValue()));
			return rolemonthcards;
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
			return monthcards.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return monthcards.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return monthcards.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return monthcards.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return monthcards.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return monthcards.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return monthcards.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return monthcards.this.hashCode();
		}

		@Override
		public String toString() {
			return monthcards.this.toString();
		}

	}

	public static final class Data implements xbean.monthcards {
		private java.util.HashMap<Integer, xbean.monthcard> rolemonthcards; // 

		public Data() {
			rolemonthcards = new java.util.HashMap<Integer, xbean.monthcard>();
		}

		Data(xbean.monthcards _o1_) {
			if (_o1_ instanceof monthcards) assign((monthcards)_o1_);
			else if (_o1_ instanceof monthcards.Data) assign((monthcards.Data)_o1_);
			else if (_o1_ instanceof monthcards.Const) assign(((monthcards.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(monthcards _o_) {
			rolemonthcards = new java.util.HashMap<Integer, xbean.monthcard>();
			for (java.util.Map.Entry<Integer, xbean.monthcard> _e_ : _o_.rolemonthcards.entrySet())
				rolemonthcards.put(_e_.getKey(), new monthcard.Data(_e_.getValue()));
		}

		private void assign(monthcards.Data _o_) {
			rolemonthcards = new java.util.HashMap<Integer, xbean.monthcard>();
			for (java.util.Map.Entry<Integer, xbean.monthcard> _e_ : _o_.rolemonthcards.entrySet())
				rolemonthcards.put(_e_.getKey(), new monthcard.Data(_e_.getValue()));
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(rolemonthcards.size());
			for (java.util.Map.Entry<Integer, xbean.monthcard> _e_ : rolemonthcards.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_e_.getValue().marshal(_os_);
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					rolemonthcards = new java.util.HashMap<Integer, xbean.monthcard>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					xbean.monthcard _v_ = xbean.Pod.newmonthcardData();
					_v_.unmarshal(_os_);
					rolemonthcards.put(_k_, _v_);
				}
			}
			return _os_;
		}

		@Override
		public xbean.monthcards copy() {
			return new Data(this);
		}

		@Override
		public xbean.monthcards toData() {
			return new Data(this);
		}

		public xbean.monthcards toBean() {
			return new monthcards(this, null, null);
		}

		@Override
		public xbean.monthcards toDataIf() {
			return this;
		}

		public xbean.monthcards toBeanIf() {
			return new monthcards(this, null, null);
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
		public java.util.Map<Integer, xbean.monthcard> getRolemonthcards() { // 
			return rolemonthcards;
		}

		@Override
		public java.util.Map<Integer, xbean.monthcard> getRolemonthcardsAsData() { // 
			return rolemonthcards;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof monthcards.Data)) return false;
			monthcards.Data _o_ = (monthcards.Data) _o1_;
			if (!rolemonthcards.equals(_o_.rolemonthcards)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += rolemonthcards.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(rolemonthcards);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
