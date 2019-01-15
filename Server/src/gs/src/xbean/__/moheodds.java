
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class moheodds extends xdb.XBean implements xbean.moheodds {
	private java.util.HashMap<Integer, Integer> moheoddsmap; // 魔盒几率列表

	moheodds(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		moheoddsmap = new java.util.HashMap<Integer, Integer>();
	}

	public moheodds() {
		this(0, null, null);
	}

	public moheodds(moheodds _o_) {
		this(_o_, null, null);
	}

	moheodds(xbean.moheodds _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof moheodds) assign((moheodds)_o1_);
		else if (_o1_ instanceof moheodds.Data) assign((moheodds.Data)_o1_);
		else if (_o1_ instanceof moheodds.Const) assign(((moheodds.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(moheodds _o_) {
		_o_._xdb_verify_unsafe_();
		moheoddsmap = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.moheoddsmap.entrySet())
			moheoddsmap.put(_e_.getKey(), _e_.getValue());
	}

	private void assign(moheodds.Data _o_) {
		moheoddsmap = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.moheoddsmap.entrySet())
			moheoddsmap.put(_e_.getKey(), _e_.getValue());
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(moheoddsmap.size());
		for (java.util.Map.Entry<Integer, Integer> _e_ : moheoddsmap.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				moheoddsmap = new java.util.HashMap<Integer, Integer>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				int _v_ = 0;
				_v_ = _os_.unmarshal_int();
				moheoddsmap.put(_k_, _v_);
			}
		}
		return _os_;
	}

	@Override
	public xbean.moheodds copy() {
		_xdb_verify_unsafe_();
		return new moheodds(this);
	}

	@Override
	public xbean.moheodds toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.moheodds toBean() {
		_xdb_verify_unsafe_();
		return new moheodds(this); // same as copy()
	}

	@Override
	public xbean.moheodds toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.moheodds toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.Map<Integer, Integer> getMoheoddsmap() { // 魔盒几率列表
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "moheoddsmap"), moheoddsmap);
	}

	@Override
	public java.util.Map<Integer, Integer> getMoheoddsmapAsData() { // 魔盒几率列表
		_xdb_verify_unsafe_();
		java.util.Map<Integer, Integer> moheoddsmap;
		moheodds _o_ = this;
		moheoddsmap = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.moheoddsmap.entrySet())
			moheoddsmap.put(_e_.getKey(), _e_.getValue());
		return moheoddsmap;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		moheodds _o_ = null;
		if ( _o1_ instanceof moheodds ) _o_ = (moheodds)_o1_;
		else if ( _o1_ instanceof moheodds.Const ) _o_ = ((moheodds.Const)_o1_).nThis();
		else return false;
		if (!moheoddsmap.equals(_o_.moheoddsmap)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += moheoddsmap.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(moheoddsmap);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableMap().setVarName("moheoddsmap"));
		return lb;
	}

	private class Const implements xbean.moheodds {
		moheodds nThis() {
			return moheodds.this;
		}

		@Override
		public xbean.moheodds copy() {
			return moheodds.this.copy();
		}

		@Override
		public xbean.moheodds toData() {
			return moheodds.this.toData();
		}

		public xbean.moheodds toBean() {
			return moheodds.this.toBean();
		}

		@Override
		public xbean.moheodds toDataIf() {
			return moheodds.this.toDataIf();
		}

		public xbean.moheodds toBeanIf() {
			return moheodds.this.toBeanIf();
		}

		@Override
		public java.util.Map<Integer, Integer> getMoheoddsmap() { // 魔盒几率列表
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(moheoddsmap);
		}

		@Override
		public java.util.Map<Integer, Integer> getMoheoddsmapAsData() { // 魔盒几率列表
			_xdb_verify_unsafe_();
			java.util.Map<Integer, Integer> moheoddsmap;
			moheodds _o_ = moheodds.this;
			moheoddsmap = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.moheoddsmap.entrySet())
				moheoddsmap.put(_e_.getKey(), _e_.getValue());
			return moheoddsmap;
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
			return moheodds.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return moheodds.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return moheodds.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return moheodds.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return moheodds.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return moheodds.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return moheodds.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return moheodds.this.hashCode();
		}

		@Override
		public String toString() {
			return moheodds.this.toString();
		}

	}

	public static final class Data implements xbean.moheodds {
		private java.util.HashMap<Integer, Integer> moheoddsmap; // 魔盒几率列表

		public Data() {
			moheoddsmap = new java.util.HashMap<Integer, Integer>();
		}

		Data(xbean.moheodds _o1_) {
			if (_o1_ instanceof moheodds) assign((moheodds)_o1_);
			else if (_o1_ instanceof moheodds.Data) assign((moheodds.Data)_o1_);
			else if (_o1_ instanceof moheodds.Const) assign(((moheodds.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(moheodds _o_) {
			moheoddsmap = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.moheoddsmap.entrySet())
				moheoddsmap.put(_e_.getKey(), _e_.getValue());
		}

		private void assign(moheodds.Data _o_) {
			moheoddsmap = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.moheoddsmap.entrySet())
				moheoddsmap.put(_e_.getKey(), _e_.getValue());
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(moheoddsmap.size());
			for (java.util.Map.Entry<Integer, Integer> _e_ : moheoddsmap.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_os_.marshal(_e_.getValue());
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					moheoddsmap = new java.util.HashMap<Integer, Integer>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					int _v_ = 0;
					_v_ = _os_.unmarshal_int();
					moheoddsmap.put(_k_, _v_);
				}
			}
			return _os_;
		}

		@Override
		public xbean.moheodds copy() {
			return new Data(this);
		}

		@Override
		public xbean.moheodds toData() {
			return new Data(this);
		}

		public xbean.moheodds toBean() {
			return new moheodds(this, null, null);
		}

		@Override
		public xbean.moheodds toDataIf() {
			return this;
		}

		public xbean.moheodds toBeanIf() {
			return new moheodds(this, null, null);
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
		public java.util.Map<Integer, Integer> getMoheoddsmap() { // 魔盒几率列表
			return moheoddsmap;
		}

		@Override
		public java.util.Map<Integer, Integer> getMoheoddsmapAsData() { // 魔盒几率列表
			return moheoddsmap;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof moheodds.Data)) return false;
			moheodds.Data _o_ = (moheodds.Data) _o1_;
			if (!moheoddsmap.equals(_o_.moheoddsmap)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += moheoddsmap.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(moheoddsmap);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
