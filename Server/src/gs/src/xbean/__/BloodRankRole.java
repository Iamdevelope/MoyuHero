
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class BloodRankRole extends xdb.XBean implements xbean.BloodRankRole {
	private long roleid; // 
	private int maxlevel; // 

	BloodRankRole(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
	}

	public BloodRankRole() {
		this(0, null, null);
	}

	public BloodRankRole(BloodRankRole _o_) {
		this(_o_, null, null);
	}

	BloodRankRole(xbean.BloodRankRole _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof BloodRankRole) assign((BloodRankRole)_o1_);
		else if (_o1_ instanceof BloodRankRole.Data) assign((BloodRankRole.Data)_o1_);
		else if (_o1_ instanceof BloodRankRole.Const) assign(((BloodRankRole.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(BloodRankRole _o_) {
		_o_._xdb_verify_unsafe_();
		roleid = _o_.roleid;
		maxlevel = _o_.maxlevel;
	}

	private void assign(BloodRankRole.Data _o_) {
		roleid = _o_.roleid;
		maxlevel = _o_.maxlevel;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(roleid);
		_os_.marshal(maxlevel);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		roleid = _os_.unmarshal_long();
		maxlevel = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.BloodRankRole copy() {
		_xdb_verify_unsafe_();
		return new BloodRankRole(this);
	}

	@Override
	public xbean.BloodRankRole toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.BloodRankRole toBean() {
		_xdb_verify_unsafe_();
		return new BloodRankRole(this); // same as copy()
	}

	@Override
	public xbean.BloodRankRole toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.BloodRankRole toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public long getRoleid() { // 
		_xdb_verify_unsafe_();
		return roleid;
	}

	@Override
	public int getMaxlevel() { // 
		_xdb_verify_unsafe_();
		return maxlevel;
	}

	@Override
	public void setRoleid(long _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "roleid") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, roleid) {
					public void rollback() { roleid = _xdb_saved; }
				};}});
		roleid = _v_;
	}

	@Override
	public void setMaxlevel(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "maxlevel") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, maxlevel) {
					public void rollback() { maxlevel = _xdb_saved; }
				};}});
		maxlevel = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		BloodRankRole _o_ = null;
		if ( _o1_ instanceof BloodRankRole ) _o_ = (BloodRankRole)_o1_;
		else if ( _o1_ instanceof BloodRankRole.Const ) _o_ = ((BloodRankRole.Const)_o1_).nThis();
		else return false;
		if (roleid != _o_.roleid) return false;
		if (maxlevel != _o_.maxlevel) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += roleid;
		_h_ += maxlevel;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(roleid);
		_sb_.append(",");
		_sb_.append(maxlevel);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("roleid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("maxlevel"));
		return lb;
	}

	private class Const implements xbean.BloodRankRole {
		BloodRankRole nThis() {
			return BloodRankRole.this;
		}

		@Override
		public xbean.BloodRankRole copy() {
			return BloodRankRole.this.copy();
		}

		@Override
		public xbean.BloodRankRole toData() {
			return BloodRankRole.this.toData();
		}

		public xbean.BloodRankRole toBean() {
			return BloodRankRole.this.toBean();
		}

		@Override
		public xbean.BloodRankRole toDataIf() {
			return BloodRankRole.this.toDataIf();
		}

		public xbean.BloodRankRole toBeanIf() {
			return BloodRankRole.this.toBeanIf();
		}

		@Override
		public long getRoleid() { // 
			_xdb_verify_unsafe_();
			return roleid;
		}

		@Override
		public int getMaxlevel() { // 
			_xdb_verify_unsafe_();
			return maxlevel;
		}

		@Override
		public void setRoleid(long _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setMaxlevel(int _v_) { // 
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
			return BloodRankRole.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return BloodRankRole.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return BloodRankRole.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return BloodRankRole.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return BloodRankRole.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return BloodRankRole.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return BloodRankRole.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return BloodRankRole.this.hashCode();
		}

		@Override
		public String toString() {
			return BloodRankRole.this.toString();
		}

	}

	public static final class Data implements xbean.BloodRankRole {
		private long roleid; // 
		private int maxlevel; // 

		public Data() {
		}

		Data(xbean.BloodRankRole _o1_) {
			if (_o1_ instanceof BloodRankRole) assign((BloodRankRole)_o1_);
			else if (_o1_ instanceof BloodRankRole.Data) assign((BloodRankRole.Data)_o1_);
			else if (_o1_ instanceof BloodRankRole.Const) assign(((BloodRankRole.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(BloodRankRole _o_) {
			roleid = _o_.roleid;
			maxlevel = _o_.maxlevel;
		}

		private void assign(BloodRankRole.Data _o_) {
			roleid = _o_.roleid;
			maxlevel = _o_.maxlevel;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(roleid);
			_os_.marshal(maxlevel);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			roleid = _os_.unmarshal_long();
			maxlevel = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.BloodRankRole copy() {
			return new Data(this);
		}

		@Override
		public xbean.BloodRankRole toData() {
			return new Data(this);
		}

		public xbean.BloodRankRole toBean() {
			return new BloodRankRole(this, null, null);
		}

		@Override
		public xbean.BloodRankRole toDataIf() {
			return this;
		}

		public xbean.BloodRankRole toBeanIf() {
			return new BloodRankRole(this, null, null);
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
		public long getRoleid() { // 
			return roleid;
		}

		@Override
		public int getMaxlevel() { // 
			return maxlevel;
		}

		@Override
		public void setRoleid(long _v_) { // 
			roleid = _v_;
		}

		@Override
		public void setMaxlevel(int _v_) { // 
			maxlevel = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof BloodRankRole.Data)) return false;
			BloodRankRole.Data _o_ = (BloodRankRole.Data) _o1_;
			if (roleid != _o_.roleid) return false;
			if (maxlevel != _o_.maxlevel) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += roleid;
			_h_ += maxlevel;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(roleid);
			_sb_.append(",");
			_sb_.append(maxlevel);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
