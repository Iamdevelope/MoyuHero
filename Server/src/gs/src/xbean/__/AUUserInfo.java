
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class AUUserInfo extends xdb.XBean implements xbean.AUUserInfo {
	private int retcode; // 
	private int loginip; // 
	private int blisgm; // 
	private String nickname; // 
	private String username; // 

	AUUserInfo(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		nickname = "";
		username = "";
	}

	public AUUserInfo() {
		this(0, null, null);
	}

	public AUUserInfo(AUUserInfo _o_) {
		this(_o_, null, null);
	}

	AUUserInfo(xbean.AUUserInfo _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof AUUserInfo) assign((AUUserInfo)_o1_);
		else if (_o1_ instanceof AUUserInfo.Data) assign((AUUserInfo.Data)_o1_);
		else if (_o1_ instanceof AUUserInfo.Const) assign(((AUUserInfo.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(AUUserInfo _o_) {
		_o_._xdb_verify_unsafe_();
		retcode = _o_.retcode;
		loginip = _o_.loginip;
		blisgm = _o_.blisgm;
		nickname = _o_.nickname;
		username = _o_.username;
	}

	private void assign(AUUserInfo.Data _o_) {
		retcode = _o_.retcode;
		loginip = _o_.loginip;
		blisgm = _o_.blisgm;
		nickname = _o_.nickname;
		username = _o_.username;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(retcode);
		_os_.marshal(loginip);
		_os_.marshal(blisgm);
		_os_.marshal(nickname, xdb.Const.IO_CHARSET);
		_os_.marshal(username, xdb.Const.IO_CHARSET);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		retcode = _os_.unmarshal_int();
		loginip = _os_.unmarshal_int();
		blisgm = _os_.unmarshal_int();
		nickname = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
		username = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
		return _os_;
	}

	@Override
	public xbean.AUUserInfo copy() {
		_xdb_verify_unsafe_();
		return new AUUserInfo(this);
	}

	@Override
	public xbean.AUUserInfo toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.AUUserInfo toBean() {
		_xdb_verify_unsafe_();
		return new AUUserInfo(this); // same as copy()
	}

	@Override
	public xbean.AUUserInfo toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.AUUserInfo toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getRetcode() { // 
		_xdb_verify_unsafe_();
		return retcode;
	}

	@Override
	public int getLoginip() { // 
		_xdb_verify_unsafe_();
		return loginip;
	}

	@Override
	public int getBlisgm() { // 
		_xdb_verify_unsafe_();
		return blisgm;
	}

	@Override
	public String getNickname() { // 
		_xdb_verify_unsafe_();
		return nickname;
	}

	@Override
	public com.goldhuman.Common.Octets getNicknameOctets() { // 
		_xdb_verify_unsafe_();
		return com.goldhuman.Common.Octets.wrap(getNickname(), xdb.Const.IO_CHARSET);
	}

	@Override
	public String getUsername() { // 
		_xdb_verify_unsafe_();
		return username;
	}

	@Override
	public com.goldhuman.Common.Octets getUsernameOctets() { // 
		_xdb_verify_unsafe_();
		return com.goldhuman.Common.Octets.wrap(getUsername(), xdb.Const.IO_CHARSET);
	}

	@Override
	public void setRetcode(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "retcode") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, retcode) {
					public void rollback() { retcode = _xdb_saved; }
				};}});
		retcode = _v_;
	}

	@Override
	public void setLoginip(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "loginip") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, loginip) {
					public void rollback() { loginip = _xdb_saved; }
				};}});
		loginip = _v_;
	}

	@Override
	public void setBlisgm(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "blisgm") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, blisgm) {
					public void rollback() { blisgm = _xdb_saved; }
				};}});
		blisgm = _v_;
	}

	@Override
	public void setNickname(String _v_) { // 
		_xdb_verify_unsafe_();
		if (null == _v_)
			throw new NullPointerException();
		xdb.Logs.logIf(new xdb.LogKey(this, "nickname") {
			protected xdb.Log create() {
				return new xdb.logs.LogString(this, nickname) {
					public void rollback() { nickname = _xdb_saved; }
				};}});
		nickname = _v_;
	}

	@Override
	public void setNicknameOctets(com.goldhuman.Common.Octets _v_) { // 
		_xdb_verify_unsafe_();
		this.setNickname(_v_.getString(xdb.Const.IO_CHARSET));
	}

	@Override
	public void setUsername(String _v_) { // 
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
	public void setUsernameOctets(com.goldhuman.Common.Octets _v_) { // 
		_xdb_verify_unsafe_();
		this.setUsername(_v_.getString(xdb.Const.IO_CHARSET));
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		AUUserInfo _o_ = null;
		if ( _o1_ instanceof AUUserInfo ) _o_ = (AUUserInfo)_o1_;
		else if ( _o1_ instanceof AUUserInfo.Const ) _o_ = ((AUUserInfo.Const)_o1_).nThis();
		else return false;
		if (retcode != _o_.retcode) return false;
		if (loginip != _o_.loginip) return false;
		if (blisgm != _o_.blisgm) return false;
		if (!nickname.equals(_o_.nickname)) return false;
		if (!username.equals(_o_.username)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += retcode;
		_h_ += loginip;
		_h_ += blisgm;
		_h_ += nickname.hashCode();
		_h_ += username.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(retcode);
		_sb_.append(",");
		_sb_.append(loginip);
		_sb_.append(",");
		_sb_.append(blisgm);
		_sb_.append(",");
		_sb_.append("'").append(nickname).append("'");
		_sb_.append(",");
		_sb_.append("'").append(username).append("'");
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("retcode"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("loginip"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("blisgm"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("nickname"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("username"));
		return lb;
	}

	private class Const implements xbean.AUUserInfo {
		AUUserInfo nThis() {
			return AUUserInfo.this;
		}

		@Override
		public xbean.AUUserInfo copy() {
			return AUUserInfo.this.copy();
		}

		@Override
		public xbean.AUUserInfo toData() {
			return AUUserInfo.this.toData();
		}

		public xbean.AUUserInfo toBean() {
			return AUUserInfo.this.toBean();
		}

		@Override
		public xbean.AUUserInfo toDataIf() {
			return AUUserInfo.this.toDataIf();
		}

		public xbean.AUUserInfo toBeanIf() {
			return AUUserInfo.this.toBeanIf();
		}

		@Override
		public int getRetcode() { // 
			_xdb_verify_unsafe_();
			return retcode;
		}

		@Override
		public int getLoginip() { // 
			_xdb_verify_unsafe_();
			return loginip;
		}

		@Override
		public int getBlisgm() { // 
			_xdb_verify_unsafe_();
			return blisgm;
		}

		@Override
		public String getNickname() { // 
			_xdb_verify_unsafe_();
			return nickname;
		}

		@Override
		public com.goldhuman.Common.Octets getNicknameOctets() { // 
			_xdb_verify_unsafe_();
			return AUUserInfo.this.getNicknameOctets();
		}

		@Override
		public String getUsername() { // 
			_xdb_verify_unsafe_();
			return username;
		}

		@Override
		public com.goldhuman.Common.Octets getUsernameOctets() { // 
			_xdb_verify_unsafe_();
			return AUUserInfo.this.getUsernameOctets();
		}

		@Override
		public void setRetcode(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setLoginip(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setBlisgm(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setNickname(String _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setNicknameOctets(com.goldhuman.Common.Octets _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setUsername(String _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setUsernameOctets(com.goldhuman.Common.Octets _v_) { // 
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
			return AUUserInfo.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return AUUserInfo.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return AUUserInfo.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return AUUserInfo.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return AUUserInfo.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return AUUserInfo.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return AUUserInfo.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return AUUserInfo.this.hashCode();
		}

		@Override
		public String toString() {
			return AUUserInfo.this.toString();
		}

	}

	public static final class Data implements xbean.AUUserInfo {
		private int retcode; // 
		private int loginip; // 
		private int blisgm; // 
		private String nickname; // 
		private String username; // 

		public Data() {
			nickname = "";
			username = "";
		}

		Data(xbean.AUUserInfo _o1_) {
			if (_o1_ instanceof AUUserInfo) assign((AUUserInfo)_o1_);
			else if (_o1_ instanceof AUUserInfo.Data) assign((AUUserInfo.Data)_o1_);
			else if (_o1_ instanceof AUUserInfo.Const) assign(((AUUserInfo.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(AUUserInfo _o_) {
			retcode = _o_.retcode;
			loginip = _o_.loginip;
			blisgm = _o_.blisgm;
			nickname = _o_.nickname;
			username = _o_.username;
		}

		private void assign(AUUserInfo.Data _o_) {
			retcode = _o_.retcode;
			loginip = _o_.loginip;
			blisgm = _o_.blisgm;
			nickname = _o_.nickname;
			username = _o_.username;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(retcode);
			_os_.marshal(loginip);
			_os_.marshal(blisgm);
			_os_.marshal(nickname, xdb.Const.IO_CHARSET);
			_os_.marshal(username, xdb.Const.IO_CHARSET);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			retcode = _os_.unmarshal_int();
			loginip = _os_.unmarshal_int();
			blisgm = _os_.unmarshal_int();
			nickname = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			username = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			return _os_;
		}

		@Override
		public xbean.AUUserInfo copy() {
			return new Data(this);
		}

		@Override
		public xbean.AUUserInfo toData() {
			return new Data(this);
		}

		public xbean.AUUserInfo toBean() {
			return new AUUserInfo(this, null, null);
		}

		@Override
		public xbean.AUUserInfo toDataIf() {
			return this;
		}

		public xbean.AUUserInfo toBeanIf() {
			return new AUUserInfo(this, null, null);
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
		public int getRetcode() { // 
			return retcode;
		}

		@Override
		public int getLoginip() { // 
			return loginip;
		}

		@Override
		public int getBlisgm() { // 
			return blisgm;
		}

		@Override
		public String getNickname() { // 
			return nickname;
		}

		@Override
		public com.goldhuman.Common.Octets getNicknameOctets() { // 
			return com.goldhuman.Common.Octets.wrap(getNickname(), xdb.Const.IO_CHARSET);
		}

		@Override
		public String getUsername() { // 
			return username;
		}

		@Override
		public com.goldhuman.Common.Octets getUsernameOctets() { // 
			return com.goldhuman.Common.Octets.wrap(getUsername(), xdb.Const.IO_CHARSET);
		}

		@Override
		public void setRetcode(int _v_) { // 
			retcode = _v_;
		}

		@Override
		public void setLoginip(int _v_) { // 
			loginip = _v_;
		}

		@Override
		public void setBlisgm(int _v_) { // 
			blisgm = _v_;
		}

		@Override
		public void setNickname(String _v_) { // 
			if (null == _v_)
				throw new NullPointerException();
			nickname = _v_;
		}

		@Override
		public void setNicknameOctets(com.goldhuman.Common.Octets _v_) { // 
			this.setNickname(_v_.getString(xdb.Const.IO_CHARSET));
		}

		@Override
		public void setUsername(String _v_) { // 
			if (null == _v_)
				throw new NullPointerException();
			username = _v_;
		}

		@Override
		public void setUsernameOctets(com.goldhuman.Common.Octets _v_) { // 
			this.setUsername(_v_.getString(xdb.Const.IO_CHARSET));
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof AUUserInfo.Data)) return false;
			AUUserInfo.Data _o_ = (AUUserInfo.Data) _o1_;
			if (retcode != _o_.retcode) return false;
			if (loginip != _o_.loginip) return false;
			if (blisgm != _o_.blisgm) return false;
			if (!nickname.equals(_o_.nickname)) return false;
			if (!username.equals(_o_.username)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += retcode;
			_h_ += loginip;
			_h_ += blisgm;
			_h_ += nickname.hashCode();
			_h_ += username.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(retcode);
			_sb_.append(",");
			_sb_.append(loginip);
			_sb_.append(",");
			_sb_.append(blisgm);
			_sb_.append(",");
			_sb_.append("'").append(nickname).append("'");
			_sb_.append(",");
			_sb_.append("'").append(username).append("'");
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
