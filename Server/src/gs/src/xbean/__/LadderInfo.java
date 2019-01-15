
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class LadderInfo extends xdb.XBean implements xbean.LadderInfo {
	private long roleid; // 

	LadderInfo(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
	}

	public LadderInfo() {
		this(0, null, null);
	}

	public LadderInfo(LadderInfo _o_) {
		this(_o_, null, null);
	}

	LadderInfo(xbean.LadderInfo _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof LadderInfo) assign((LadderInfo)_o1_);
		else if (_o1_ instanceof LadderInfo.Data) assign((LadderInfo.Data)_o1_);
		else if (_o1_ instanceof LadderInfo.Const) assign(((LadderInfo.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(LadderInfo _o_) {
		_o_._xdb_verify_unsafe_();
		roleid = _o_.roleid;
	}

	private void assign(LadderInfo.Data _o_) {
		roleid = _o_.roleid;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(roleid);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		roleid = _os_.unmarshal_long();
		return _os_;
	}

	@Override
	public xbean.LadderInfo copy() {
		_xdb_verify_unsafe_();
		return new LadderInfo(this);
	}

	@Override
	public xbean.LadderInfo toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.LadderInfo toBean() {
		_xdb_verify_unsafe_();
		return new LadderInfo(this); // same as copy()
	}

	@Override
	public xbean.LadderInfo toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.LadderInfo toBeanIf() {
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
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		LadderInfo _o_ = null;
		if ( _o1_ instanceof LadderInfo ) _o_ = (LadderInfo)_o1_;
		else if ( _o1_ instanceof LadderInfo.Const ) _o_ = ((LadderInfo.Const)_o1_).nThis();
		else return false;
		if (roleid != _o_.roleid) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += roleid;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(roleid);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("roleid"));
		return lb;
	}

	private class Const implements xbean.LadderInfo {
		LadderInfo nThis() {
			return LadderInfo.this;
		}

		@Override
		public xbean.LadderInfo copy() {
			return LadderInfo.this.copy();
		}

		@Override
		public xbean.LadderInfo toData() {
			return LadderInfo.this.toData();
		}

		public xbean.LadderInfo toBean() {
			return LadderInfo.this.toBean();
		}

		@Override
		public xbean.LadderInfo toDataIf() {
			return LadderInfo.this.toDataIf();
		}

		public xbean.LadderInfo toBeanIf() {
			return LadderInfo.this.toBeanIf();
		}

		@Override
		public long getRoleid() { // 
			_xdb_verify_unsafe_();
			return roleid;
		}

		@Override
		public void setRoleid(long _v_) { // 
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
			return LadderInfo.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return LadderInfo.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return LadderInfo.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return LadderInfo.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return LadderInfo.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return LadderInfo.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return LadderInfo.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return LadderInfo.this.hashCode();
		}

		@Override
		public String toString() {
			return LadderInfo.this.toString();
		}

	}

	public static final class Data implements xbean.LadderInfo {
		private long roleid; // 

		public Data() {
		}

		Data(xbean.LadderInfo _o1_) {
			if (_o1_ instanceof LadderInfo) assign((LadderInfo)_o1_);
			else if (_o1_ instanceof LadderInfo.Data) assign((LadderInfo.Data)_o1_);
			else if (_o1_ instanceof LadderInfo.Const) assign(((LadderInfo.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(LadderInfo _o_) {
			roleid = _o_.roleid;
		}

		private void assign(LadderInfo.Data _o_) {
			roleid = _o_.roleid;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(roleid);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			roleid = _os_.unmarshal_long();
			return _os_;
		}

		@Override
		public xbean.LadderInfo copy() {
			return new Data(this);
		}

		@Override
		public xbean.LadderInfo toData() {
			return new Data(this);
		}

		public xbean.LadderInfo toBean() {
			return new LadderInfo(this, null, null);
		}

		@Override
		public xbean.LadderInfo toDataIf() {
			return this;
		}

		public xbean.LadderInfo toBeanIf() {
			return new LadderInfo(this, null, null);
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
		public void setRoleid(long _v_) { // 
			roleid = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof LadderInfo.Data)) return false;
			LadderInfo.Data _o_ = (LadderInfo.Data) _o1_;
			if (roleid != _o_.roleid) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += roleid;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(roleid);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
