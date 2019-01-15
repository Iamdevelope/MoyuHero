
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class ServerInfo extends xdb.XBean implements xbean.ServerInfo {
	private long firsttime; // 第一次起服时间
	private long starttime; // 本次起服时间

	ServerInfo(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
	}

	public ServerInfo() {
		this(0, null, null);
	}

	public ServerInfo(ServerInfo _o_) {
		this(_o_, null, null);
	}

	ServerInfo(xbean.ServerInfo _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof ServerInfo) assign((ServerInfo)_o1_);
		else if (_o1_ instanceof ServerInfo.Data) assign((ServerInfo.Data)_o1_);
		else if (_o1_ instanceof ServerInfo.Const) assign(((ServerInfo.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(ServerInfo _o_) {
		_o_._xdb_verify_unsafe_();
		firsttime = _o_.firsttime;
		starttime = _o_.starttime;
	}

	private void assign(ServerInfo.Data _o_) {
		firsttime = _o_.firsttime;
		starttime = _o_.starttime;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(firsttime);
		_os_.marshal(starttime);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		firsttime = _os_.unmarshal_long();
		starttime = _os_.unmarshal_long();
		return _os_;
	}

	@Override
	public xbean.ServerInfo copy() {
		_xdb_verify_unsafe_();
		return new ServerInfo(this);
	}

	@Override
	public xbean.ServerInfo toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.ServerInfo toBean() {
		_xdb_verify_unsafe_();
		return new ServerInfo(this); // same as copy()
	}

	@Override
	public xbean.ServerInfo toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.ServerInfo toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public long getFirsttime() { // 第一次起服时间
		_xdb_verify_unsafe_();
		return firsttime;
	}

	@Override
	public long getStarttime() { // 本次起服时间
		_xdb_verify_unsafe_();
		return starttime;
	}

	@Override
	public void setFirsttime(long _v_) { // 第一次起服时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "firsttime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, firsttime) {
					public void rollback() { firsttime = _xdb_saved; }
				};}});
		firsttime = _v_;
	}

	@Override
	public void setStarttime(long _v_) { // 本次起服时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "starttime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, starttime) {
					public void rollback() { starttime = _xdb_saved; }
				};}});
		starttime = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		ServerInfo _o_ = null;
		if ( _o1_ instanceof ServerInfo ) _o_ = (ServerInfo)_o1_;
		else if ( _o1_ instanceof ServerInfo.Const ) _o_ = ((ServerInfo.Const)_o1_).nThis();
		else return false;
		if (firsttime != _o_.firsttime) return false;
		if (starttime != _o_.starttime) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += firsttime;
		_h_ += starttime;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(firsttime);
		_sb_.append(",");
		_sb_.append(starttime);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("firsttime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("starttime"));
		return lb;
	}

	private class Const implements xbean.ServerInfo {
		ServerInfo nThis() {
			return ServerInfo.this;
		}

		@Override
		public xbean.ServerInfo copy() {
			return ServerInfo.this.copy();
		}

		@Override
		public xbean.ServerInfo toData() {
			return ServerInfo.this.toData();
		}

		public xbean.ServerInfo toBean() {
			return ServerInfo.this.toBean();
		}

		@Override
		public xbean.ServerInfo toDataIf() {
			return ServerInfo.this.toDataIf();
		}

		public xbean.ServerInfo toBeanIf() {
			return ServerInfo.this.toBeanIf();
		}

		@Override
		public long getFirsttime() { // 第一次起服时间
			_xdb_verify_unsafe_();
			return firsttime;
		}

		@Override
		public long getStarttime() { // 本次起服时间
			_xdb_verify_unsafe_();
			return starttime;
		}

		@Override
		public void setFirsttime(long _v_) { // 第一次起服时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setStarttime(long _v_) { // 本次起服时间
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
			return ServerInfo.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return ServerInfo.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return ServerInfo.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return ServerInfo.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return ServerInfo.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return ServerInfo.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return ServerInfo.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return ServerInfo.this.hashCode();
		}

		@Override
		public String toString() {
			return ServerInfo.this.toString();
		}

	}

	public static final class Data implements xbean.ServerInfo {
		private long firsttime; // 第一次起服时间
		private long starttime; // 本次起服时间

		public Data() {
		}

		Data(xbean.ServerInfo _o1_) {
			if (_o1_ instanceof ServerInfo) assign((ServerInfo)_o1_);
			else if (_o1_ instanceof ServerInfo.Data) assign((ServerInfo.Data)_o1_);
			else if (_o1_ instanceof ServerInfo.Const) assign(((ServerInfo.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(ServerInfo _o_) {
			firsttime = _o_.firsttime;
			starttime = _o_.starttime;
		}

		private void assign(ServerInfo.Data _o_) {
			firsttime = _o_.firsttime;
			starttime = _o_.starttime;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(firsttime);
			_os_.marshal(starttime);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			firsttime = _os_.unmarshal_long();
			starttime = _os_.unmarshal_long();
			return _os_;
		}

		@Override
		public xbean.ServerInfo copy() {
			return new Data(this);
		}

		@Override
		public xbean.ServerInfo toData() {
			return new Data(this);
		}

		public xbean.ServerInfo toBean() {
			return new ServerInfo(this, null, null);
		}

		@Override
		public xbean.ServerInfo toDataIf() {
			return this;
		}

		public xbean.ServerInfo toBeanIf() {
			return new ServerInfo(this, null, null);
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
		public long getFirsttime() { // 第一次起服时间
			return firsttime;
		}

		@Override
		public long getStarttime() { // 本次起服时间
			return starttime;
		}

		@Override
		public void setFirsttime(long _v_) { // 第一次起服时间
			firsttime = _v_;
		}

		@Override
		public void setStarttime(long _v_) { // 本次起服时间
			starttime = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof ServerInfo.Data)) return false;
			ServerInfo.Data _o_ = (ServerInfo.Data) _o1_;
			if (firsttime != _o_.firsttime) return false;
			if (starttime != _o_.starttime) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += firsttime;
			_h_ += starttime;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(firsttime);
			_sb_.append(",");
			_sb_.append(starttime);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
