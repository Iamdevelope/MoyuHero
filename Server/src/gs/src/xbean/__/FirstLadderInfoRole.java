
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class FirstLadderInfoRole extends xdb.XBean implements xbean.FirstLadderInfoRole {
	private java.util.HashMap<Long, xbean.FirstLadderInfo> roleinfos; // key=roleId

	FirstLadderInfoRole(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		roleinfos = new java.util.HashMap<Long, xbean.FirstLadderInfo>();
	}

	public FirstLadderInfoRole() {
		this(0, null, null);
	}

	public FirstLadderInfoRole(FirstLadderInfoRole _o_) {
		this(_o_, null, null);
	}

	FirstLadderInfoRole(xbean.FirstLadderInfoRole _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof FirstLadderInfoRole) assign((FirstLadderInfoRole)_o1_);
		else if (_o1_ instanceof FirstLadderInfoRole.Data) assign((FirstLadderInfoRole.Data)_o1_);
		else if (_o1_ instanceof FirstLadderInfoRole.Const) assign(((FirstLadderInfoRole.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(FirstLadderInfoRole _o_) {
		_o_._xdb_verify_unsafe_();
		roleinfos = new java.util.HashMap<Long, xbean.FirstLadderInfo>();
		for (java.util.Map.Entry<Long, xbean.FirstLadderInfo> _e_ : _o_.roleinfos.entrySet())
			roleinfos.put(_e_.getKey(), new FirstLadderInfo(_e_.getValue(), this, "roleinfos"));
	}

	private void assign(FirstLadderInfoRole.Data _o_) {
		roleinfos = new java.util.HashMap<Long, xbean.FirstLadderInfo>();
		for (java.util.Map.Entry<Long, xbean.FirstLadderInfo> _e_ : _o_.roleinfos.entrySet())
			roleinfos.put(_e_.getKey(), new FirstLadderInfo(_e_.getValue(), this, "roleinfos"));
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(roleinfos.size());
		for (java.util.Map.Entry<Long, xbean.FirstLadderInfo> _e_ : roleinfos.entrySet())
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
				roleinfos = new java.util.HashMap<Long, xbean.FirstLadderInfo>(size * 2);
			}
			for (; size > 0; --size)
			{
				long _k_ = 0;
				_k_ = _os_.unmarshal_long();
				xbean.FirstLadderInfo _v_ = new FirstLadderInfo(0, this, "roleinfos");
				_v_.unmarshal(_os_);
				roleinfos.put(_k_, _v_);
			}
		}
		return _os_;
	}

	@Override
	public xbean.FirstLadderInfoRole copy() {
		_xdb_verify_unsafe_();
		return new FirstLadderInfoRole(this);
	}

	@Override
	public xbean.FirstLadderInfoRole toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.FirstLadderInfoRole toBean() {
		_xdb_verify_unsafe_();
		return new FirstLadderInfoRole(this); // same as copy()
	}

	@Override
	public xbean.FirstLadderInfoRole toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.FirstLadderInfoRole toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.Map<Long, xbean.FirstLadderInfo> getRoleinfos() { // key=roleId
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "roleinfos"), roleinfos);
	}

	@Override
	public java.util.Map<Long, xbean.FirstLadderInfo> getRoleinfosAsData() { // key=roleId
		_xdb_verify_unsafe_();
		java.util.Map<Long, xbean.FirstLadderInfo> roleinfos;
		FirstLadderInfoRole _o_ = this;
		roleinfos = new java.util.HashMap<Long, xbean.FirstLadderInfo>();
		for (java.util.Map.Entry<Long, xbean.FirstLadderInfo> _e_ : _o_.roleinfos.entrySet())
			roleinfos.put(_e_.getKey(), new FirstLadderInfo.Data(_e_.getValue()));
		return roleinfos;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		FirstLadderInfoRole _o_ = null;
		if ( _o1_ instanceof FirstLadderInfoRole ) _o_ = (FirstLadderInfoRole)_o1_;
		else if ( _o1_ instanceof FirstLadderInfoRole.Const ) _o_ = ((FirstLadderInfoRole.Const)_o1_).nThis();
		else return false;
		if (!roleinfos.equals(_o_.roleinfos)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += roleinfos.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(roleinfos);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableMap().setVarName("roleinfos"));
		return lb;
	}

	private class Const implements xbean.FirstLadderInfoRole {
		FirstLadderInfoRole nThis() {
			return FirstLadderInfoRole.this;
		}

		@Override
		public xbean.FirstLadderInfoRole copy() {
			return FirstLadderInfoRole.this.copy();
		}

		@Override
		public xbean.FirstLadderInfoRole toData() {
			return FirstLadderInfoRole.this.toData();
		}

		public xbean.FirstLadderInfoRole toBean() {
			return FirstLadderInfoRole.this.toBean();
		}

		@Override
		public xbean.FirstLadderInfoRole toDataIf() {
			return FirstLadderInfoRole.this.toDataIf();
		}

		public xbean.FirstLadderInfoRole toBeanIf() {
			return FirstLadderInfoRole.this.toBeanIf();
		}

		@Override
		public java.util.Map<Long, xbean.FirstLadderInfo> getRoleinfos() { // key=roleId
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(roleinfos);
		}

		@Override
		public java.util.Map<Long, xbean.FirstLadderInfo> getRoleinfosAsData() { // key=roleId
			_xdb_verify_unsafe_();
			java.util.Map<Long, xbean.FirstLadderInfo> roleinfos;
			FirstLadderInfoRole _o_ = FirstLadderInfoRole.this;
			roleinfos = new java.util.HashMap<Long, xbean.FirstLadderInfo>();
			for (java.util.Map.Entry<Long, xbean.FirstLadderInfo> _e_ : _o_.roleinfos.entrySet())
				roleinfos.put(_e_.getKey(), new FirstLadderInfo.Data(_e_.getValue()));
			return roleinfos;
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
			return FirstLadderInfoRole.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return FirstLadderInfoRole.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return FirstLadderInfoRole.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return FirstLadderInfoRole.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return FirstLadderInfoRole.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return FirstLadderInfoRole.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return FirstLadderInfoRole.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return FirstLadderInfoRole.this.hashCode();
		}

		@Override
		public String toString() {
			return FirstLadderInfoRole.this.toString();
		}

	}

	public static final class Data implements xbean.FirstLadderInfoRole {
		private java.util.HashMap<Long, xbean.FirstLadderInfo> roleinfos; // key=roleId

		public Data() {
			roleinfos = new java.util.HashMap<Long, xbean.FirstLadderInfo>();
		}

		Data(xbean.FirstLadderInfoRole _o1_) {
			if (_o1_ instanceof FirstLadderInfoRole) assign((FirstLadderInfoRole)_o1_);
			else if (_o1_ instanceof FirstLadderInfoRole.Data) assign((FirstLadderInfoRole.Data)_o1_);
			else if (_o1_ instanceof FirstLadderInfoRole.Const) assign(((FirstLadderInfoRole.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(FirstLadderInfoRole _o_) {
			roleinfos = new java.util.HashMap<Long, xbean.FirstLadderInfo>();
			for (java.util.Map.Entry<Long, xbean.FirstLadderInfo> _e_ : _o_.roleinfos.entrySet())
				roleinfos.put(_e_.getKey(), new FirstLadderInfo.Data(_e_.getValue()));
		}

		private void assign(FirstLadderInfoRole.Data _o_) {
			roleinfos = new java.util.HashMap<Long, xbean.FirstLadderInfo>();
			for (java.util.Map.Entry<Long, xbean.FirstLadderInfo> _e_ : _o_.roleinfos.entrySet())
				roleinfos.put(_e_.getKey(), new FirstLadderInfo.Data(_e_.getValue()));
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(roleinfos.size());
			for (java.util.Map.Entry<Long, xbean.FirstLadderInfo> _e_ : roleinfos.entrySet())
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
					roleinfos = new java.util.HashMap<Long, xbean.FirstLadderInfo>(size * 2);
				}
				for (; size > 0; --size)
				{
					long _k_ = 0;
					_k_ = _os_.unmarshal_long();
					xbean.FirstLadderInfo _v_ = xbean.Pod.newFirstLadderInfoData();
					_v_.unmarshal(_os_);
					roleinfos.put(_k_, _v_);
				}
			}
			return _os_;
		}

		@Override
		public xbean.FirstLadderInfoRole copy() {
			return new Data(this);
		}

		@Override
		public xbean.FirstLadderInfoRole toData() {
			return new Data(this);
		}

		public xbean.FirstLadderInfoRole toBean() {
			return new FirstLadderInfoRole(this, null, null);
		}

		@Override
		public xbean.FirstLadderInfoRole toDataIf() {
			return this;
		}

		public xbean.FirstLadderInfoRole toBeanIf() {
			return new FirstLadderInfoRole(this, null, null);
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
		public java.util.Map<Long, xbean.FirstLadderInfo> getRoleinfos() { // key=roleId
			return roleinfos;
		}

		@Override
		public java.util.Map<Long, xbean.FirstLadderInfo> getRoleinfosAsData() { // key=roleId
			return roleinfos;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof FirstLadderInfoRole.Data)) return false;
			FirstLadderInfoRole.Data _o_ = (FirstLadderInfoRole.Data) _o1_;
			if (!roleinfos.equals(_o_.roleinfos)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += roleinfos.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(roleinfos);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
