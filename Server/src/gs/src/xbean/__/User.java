
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class User extends xdb.XBean implements xbean.User {
	private String username; // 帐号名称
	private java.util.ArrayList<Long> idlist; // 用户的角色列表 value是roleid
	private long createtime; // 帐号第一次进入游戏的时间

	User(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		username = "";
		idlist = new java.util.ArrayList<Long>();
	}

	public User() {
		this(0, null, null);
	}

	public User(User _o_) {
		this(_o_, null, null);
	}

	User(xbean.User _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof User) assign((User)_o1_);
		else if (_o1_ instanceof User.Data) assign((User.Data)_o1_);
		else if (_o1_ instanceof User.Const) assign(((User.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(User _o_) {
		_o_._xdb_verify_unsafe_();
		username = _o_.username;
		idlist = new java.util.ArrayList<Long>();
		idlist.addAll(_o_.idlist);
		createtime = _o_.createtime;
	}

	private void assign(User.Data _o_) {
		username = _o_.username;
		idlist = new java.util.ArrayList<Long>();
		idlist.addAll(_o_.idlist);
		createtime = _o_.createtime;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(username, xdb.Const.IO_CHARSET);
		_os_.compact_uint32(idlist.size());
		for (Long _v_ : idlist) {
			_os_.marshal(_v_);
		}
		_os_.marshal(createtime);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		username = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			long _v_ = 0;
			_v_ = _os_.unmarshal_long();
			idlist.add(_v_);
		}
		createtime = _os_.unmarshal_long();
		return _os_;
	}

	@Override
	public xbean.User copy() {
		_xdb_verify_unsafe_();
		return new User(this);
	}

	@Override
	public xbean.User toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.User toBean() {
		_xdb_verify_unsafe_();
		return new User(this); // same as copy()
	}

	@Override
	public xbean.User toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.User toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public String getUsername() { // 帐号名称
		_xdb_verify_unsafe_();
		return username;
	}

	@Override
	public com.goldhuman.Common.Octets getUsernameOctets() { // 帐号名称
		_xdb_verify_unsafe_();
		return com.goldhuman.Common.Octets.wrap(getUsername(), xdb.Const.IO_CHARSET);
	}

	@Override
	public java.util.List<Long> getIdlist() { // 用户的角色列表 value是roleid
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "idlist"), idlist);
	}

	public java.util.List<Long> getIdlistAsData() { // 用户的角色列表 value是roleid
		_xdb_verify_unsafe_();
		java.util.List<Long> idlist;
		User _o_ = this;
		idlist = new java.util.ArrayList<Long>();
		idlist.addAll(_o_.idlist);
		return idlist;
	}

	@Override
	public long getCreatetime() { // 帐号第一次进入游戏的时间
		_xdb_verify_unsafe_();
		return createtime;
	}

	@Override
	public void setUsername(String _v_) { // 帐号名称
		_xdb_verify_unsafe_();
		if (null == _v_)
			throw new NullPointerException();
		xdb.Logs.logIf(new xdb.LogKey(this, "username") {
			protected xdb.Log create() {
				return new xdb.logs.LogString(this, username) {
					public void rollback() { username = _xdb_saved; }
				};}});
		username = _v_;
	}

	@Override
	public void setUsernameOctets(com.goldhuman.Common.Octets _v_) { // 帐号名称
		_xdb_verify_unsafe_();
		this.setUsername(_v_.getString(xdb.Const.IO_CHARSET));
	}

	@Override
	public void setCreatetime(long _v_) { // 帐号第一次进入游戏的时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "createtime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, createtime) {
					public void rollback() { createtime = _xdb_saved; }
				};}});
		createtime = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		User _o_ = null;
		if ( _o1_ instanceof User ) _o_ = (User)_o1_;
		else if ( _o1_ instanceof User.Const ) _o_ = ((User.Const)_o1_).nThis();
		else return false;
		if (!username.equals(_o_.username)) return false;
		if (!idlist.equals(_o_.idlist)) return false;
		if (createtime != _o_.createtime) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += username.hashCode();
		_h_ += idlist.hashCode();
		_h_ += createtime;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append("'").append(username).append("'");
		_sb_.append(",");
		_sb_.append(idlist);
		_sb_.append(",");
		_sb_.append(createtime);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("username"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("idlist"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("createtime"));
		return lb;
	}

	private class Const implements xbean.User {
		User nThis() {
			return User.this;
		}

		@Override
		public xbean.User copy() {
			return User.this.copy();
		}

		@Override
		public xbean.User toData() {
			return User.this.toData();
		}

		public xbean.User toBean() {
			return User.this.toBean();
		}

		@Override
		public xbean.User toDataIf() {
			return User.this.toDataIf();
		}

		public xbean.User toBeanIf() {
			return User.this.toBeanIf();
		}

		@Override
		public String getUsername() { // 帐号名称
			_xdb_verify_unsafe_();
			return username;
		}

		@Override
		public com.goldhuman.Common.Octets getUsernameOctets() { // 帐号名称
			_xdb_verify_unsafe_();
			return User.this.getUsernameOctets();
		}

		@Override
		public java.util.List<Long> getIdlist() { // 用户的角色列表 value是roleid
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(idlist);
		}

		public java.util.List<Long> getIdlistAsData() { // 用户的角色列表 value是roleid
			_xdb_verify_unsafe_();
			java.util.List<Long> idlist;
			User _o_ = User.this;
		idlist = new java.util.ArrayList<Long>();
		idlist.addAll(_o_.idlist);
			return idlist;
		}

		@Override
		public long getCreatetime() { // 帐号第一次进入游戏的时间
			_xdb_verify_unsafe_();
			return createtime;
		}

		@Override
		public void setUsername(String _v_) { // 帐号名称
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setUsernameOctets(com.goldhuman.Common.Octets _v_) { // 帐号名称
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setCreatetime(long _v_) { // 帐号第一次进入游戏的时间
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
			return User.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return User.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return User.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return User.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return User.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return User.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return User.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return User.this.hashCode();
		}

		@Override
		public String toString() {
			return User.this.toString();
		}

	}

	public static final class Data implements xbean.User {
		private String username; // 帐号名称
		private java.util.ArrayList<Long> idlist; // 用户的角色列表 value是roleid
		private long createtime; // 帐号第一次进入游戏的时间

		public Data() {
			username = "";
			idlist = new java.util.ArrayList<Long>();
		}

		Data(xbean.User _o1_) {
			if (_o1_ instanceof User) assign((User)_o1_);
			else if (_o1_ instanceof User.Data) assign((User.Data)_o1_);
			else if (_o1_ instanceof User.Const) assign(((User.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(User _o_) {
			username = _o_.username;
			idlist = new java.util.ArrayList<Long>();
			idlist.addAll(_o_.idlist);
			createtime = _o_.createtime;
		}

		private void assign(User.Data _o_) {
			username = _o_.username;
			idlist = new java.util.ArrayList<Long>();
			idlist.addAll(_o_.idlist);
			createtime = _o_.createtime;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(username, xdb.Const.IO_CHARSET);
			_os_.compact_uint32(idlist.size());
			for (Long _v_ : idlist) {
				_os_.marshal(_v_);
			}
			_os_.marshal(createtime);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			username = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				long _v_ = 0;
				_v_ = _os_.unmarshal_long();
				idlist.add(_v_);
			}
			createtime = _os_.unmarshal_long();
			return _os_;
		}

		@Override
		public xbean.User copy() {
			return new Data(this);
		}

		@Override
		public xbean.User toData() {
			return new Data(this);
		}

		public xbean.User toBean() {
			return new User(this, null, null);
		}

		@Override
		public xbean.User toDataIf() {
			return this;
		}

		public xbean.User toBeanIf() {
			return new User(this, null, null);
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
		public String getUsername() { // 帐号名称
			return username;
		}

		@Override
		public com.goldhuman.Common.Octets getUsernameOctets() { // 帐号名称
			return com.goldhuman.Common.Octets.wrap(getUsername(), xdb.Const.IO_CHARSET);
		}

		@Override
		public java.util.List<Long> getIdlist() { // 用户的角色列表 value是roleid
			return idlist;
		}

		@Override
		public java.util.List<Long> getIdlistAsData() { // 用户的角色列表 value是roleid
			return idlist;
		}

		@Override
		public long getCreatetime() { // 帐号第一次进入游戏的时间
			return createtime;
		}

		@Override
		public void setUsername(String _v_) { // 帐号名称
			if (null == _v_)
				throw new NullPointerException();
			username = _v_;
		}

		@Override
		public void setUsernameOctets(com.goldhuman.Common.Octets _v_) { // 帐号名称
			this.setUsername(_v_.getString(xdb.Const.IO_CHARSET));
		}

		@Override
		public void setCreatetime(long _v_) { // 帐号第一次进入游戏的时间
			createtime = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof User.Data)) return false;
			User.Data _o_ = (User.Data) _o1_;
			if (!username.equals(_o_.username)) return false;
			if (!idlist.equals(_o_.idlist)) return false;
			if (createtime != _o_.createtime) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += username.hashCode();
			_h_ += idlist.hashCode();
			_h_ += createtime;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append("'").append(username).append("'");
			_sb_.append(",");
			_sb_.append(idlist);
			_sb_.append(",");
			_sb_.append(createtime);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
