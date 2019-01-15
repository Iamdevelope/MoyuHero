
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class MacInfo extends xdb.XBean implements xbean.MacInfo {
	private long onlinetime; // 
	private long offlinetime; // 

	MacInfo(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
	}

	public MacInfo() {
		this(0, null, null);
	}

	public MacInfo(MacInfo _o_) {
		this(_o_, null, null);
	}

	MacInfo(xbean.MacInfo _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof MacInfo) assign((MacInfo)_o1_);
		else if (_o1_ instanceof MacInfo.Data) assign((MacInfo.Data)_o1_);
		else if (_o1_ instanceof MacInfo.Const) assign(((MacInfo.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(MacInfo _o_) {
		_o_._xdb_verify_unsafe_();
		onlinetime = _o_.onlinetime;
		offlinetime = _o_.offlinetime;
	}

	private void assign(MacInfo.Data _o_) {
		onlinetime = _o_.onlinetime;
		offlinetime = _o_.offlinetime;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(onlinetime);
		_os_.marshal(offlinetime);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		onlinetime = _os_.unmarshal_long();
		offlinetime = _os_.unmarshal_long();
		return _os_;
	}

	@Override
	public xbean.MacInfo copy() {
		_xdb_verify_unsafe_();
		return new MacInfo(this);
	}

	@Override
	public xbean.MacInfo toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.MacInfo toBean() {
		_xdb_verify_unsafe_();
		return new MacInfo(this); // same as copy()
	}

	@Override
	public xbean.MacInfo toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.MacInfo toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public long getOnlinetime() { // 
		_xdb_verify_unsafe_();
		return onlinetime;
	}

	@Override
	public long getOfflinetime() { // 
		_xdb_verify_unsafe_();
		return offlinetime;
	}

	@Override
	public void setOnlinetime(long _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "onlinetime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, onlinetime) {
					public void rollback() { onlinetime = _xdb_saved; }
				};}});
		onlinetime = _v_;
	}

	@Override
	public void setOfflinetime(long _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "offlinetime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, offlinetime) {
					public void rollback() { offlinetime = _xdb_saved; }
				};}});
		offlinetime = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		MacInfo _o_ = null;
		if ( _o1_ instanceof MacInfo ) _o_ = (MacInfo)_o1_;
		else if ( _o1_ instanceof MacInfo.Const ) _o_ = ((MacInfo.Const)_o1_).nThis();
		else return false;
		if (onlinetime != _o_.onlinetime) return false;
		if (offlinetime != _o_.offlinetime) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += onlinetime;
		_h_ += offlinetime;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(onlinetime);
		_sb_.append(",");
		_sb_.append(offlinetime);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("onlinetime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("offlinetime"));
		return lb;
	}

	private class Const implements xbean.MacInfo {
		MacInfo nThis() {
			return MacInfo.this;
		}

		@Override
		public xbean.MacInfo copy() {
			return MacInfo.this.copy();
		}

		@Override
		public xbean.MacInfo toData() {
			return MacInfo.this.toData();
		}

		public xbean.MacInfo toBean() {
			return MacInfo.this.toBean();
		}

		@Override
		public xbean.MacInfo toDataIf() {
			return MacInfo.this.toDataIf();
		}

		public xbean.MacInfo toBeanIf() {
			return MacInfo.this.toBeanIf();
		}

		@Override
		public long getOnlinetime() { // 
			_xdb_verify_unsafe_();
			return onlinetime;
		}

		@Override
		public long getOfflinetime() { // 
			_xdb_verify_unsafe_();
			return offlinetime;
		}

		@Override
		public void setOnlinetime(long _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setOfflinetime(long _v_) { // 
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
			return MacInfo.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return MacInfo.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return MacInfo.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return MacInfo.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return MacInfo.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return MacInfo.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return MacInfo.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return MacInfo.this.hashCode();
		}

		@Override
		public String toString() {
			return MacInfo.this.toString();
		}

	}

	public static final class Data implements xbean.MacInfo {
		private long onlinetime; // 
		private long offlinetime; // 

		public Data() {
		}

		Data(xbean.MacInfo _o1_) {
			if (_o1_ instanceof MacInfo) assign((MacInfo)_o1_);
			else if (_o1_ instanceof MacInfo.Data) assign((MacInfo.Data)_o1_);
			else if (_o1_ instanceof MacInfo.Const) assign(((MacInfo.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(MacInfo _o_) {
			onlinetime = _o_.onlinetime;
			offlinetime = _o_.offlinetime;
		}

		private void assign(MacInfo.Data _o_) {
			onlinetime = _o_.onlinetime;
			offlinetime = _o_.offlinetime;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(onlinetime);
			_os_.marshal(offlinetime);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			onlinetime = _os_.unmarshal_long();
			offlinetime = _os_.unmarshal_long();
			return _os_;
		}

		@Override
		public xbean.MacInfo copy() {
			return new Data(this);
		}

		@Override
		public xbean.MacInfo toData() {
			return new Data(this);
		}

		public xbean.MacInfo toBean() {
			return new MacInfo(this, null, null);
		}

		@Override
		public xbean.MacInfo toDataIf() {
			return this;
		}

		public xbean.MacInfo toBeanIf() {
			return new MacInfo(this, null, null);
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
		public long getOnlinetime() { // 
			return onlinetime;
		}

		@Override
		public long getOfflinetime() { // 
			return offlinetime;
		}

		@Override
		public void setOnlinetime(long _v_) { // 
			onlinetime = _v_;
		}

		@Override
		public void setOfflinetime(long _v_) { // 
			offlinetime = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof MacInfo.Data)) return false;
			MacInfo.Data _o_ = (MacInfo.Data) _o1_;
			if (onlinetime != _o_.onlinetime) return false;
			if (offlinetime != _o_.offlinetime) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += onlinetime;
			_h_ += offlinetime;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(onlinetime);
			_sb_.append(",");
			_sb_.append(offlinetime);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
