
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class GoogleReceiptData extends xdb.XBean implements xbean.GoogleReceiptData {
	private long roleid; // 
	private String packagename; // 
	private String productid; // 
	private String token; // 

	GoogleReceiptData(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		packagename = "";
		productid = "";
		token = "";
	}

	public GoogleReceiptData() {
		this(0, null, null);
	}

	public GoogleReceiptData(GoogleReceiptData _o_) {
		this(_o_, null, null);
	}

	GoogleReceiptData(xbean.GoogleReceiptData _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof GoogleReceiptData) assign((GoogleReceiptData)_o1_);
		else if (_o1_ instanceof GoogleReceiptData.Data) assign((GoogleReceiptData.Data)_o1_);
		else if (_o1_ instanceof GoogleReceiptData.Const) assign(((GoogleReceiptData.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(GoogleReceiptData _o_) {
		_o_._xdb_verify_unsafe_();
		roleid = _o_.roleid;
		packagename = _o_.packagename;
		productid = _o_.productid;
		token = _o_.token;
	}

	private void assign(GoogleReceiptData.Data _o_) {
		roleid = _o_.roleid;
		packagename = _o_.packagename;
		productid = _o_.productid;
		token = _o_.token;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(roleid);
		_os_.marshal(packagename, xdb.Const.IO_CHARSET);
		_os_.marshal(productid, xdb.Const.IO_CHARSET);
		_os_.marshal(token, xdb.Const.IO_CHARSET);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		roleid = _os_.unmarshal_long();
		packagename = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
		productid = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
		token = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
		return _os_;
	}

	@Override
	public xbean.GoogleReceiptData copy() {
		_xdb_verify_unsafe_();
		return new GoogleReceiptData(this);
	}

	@Override
	public xbean.GoogleReceiptData toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.GoogleReceiptData toBean() {
		_xdb_verify_unsafe_();
		return new GoogleReceiptData(this); // same as copy()
	}

	@Override
	public xbean.GoogleReceiptData toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.GoogleReceiptData toBeanIf() {
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
	public String getPackagename() { // 
		_xdb_verify_unsafe_();
		return packagename;
	}

	@Override
	public com.goldhuman.Common.Octets getPackagenameOctets() { // 
		_xdb_verify_unsafe_();
		return com.goldhuman.Common.Octets.wrap(getPackagename(), xdb.Const.IO_CHARSET);
	}

	@Override
	public String getProductid() { // 
		_xdb_verify_unsafe_();
		return productid;
	}

	@Override
	public com.goldhuman.Common.Octets getProductidOctets() { // 
		_xdb_verify_unsafe_();
		return com.goldhuman.Common.Octets.wrap(getProductid(), xdb.Const.IO_CHARSET);
	}

	@Override
	public String getToken() { // 
		_xdb_verify_unsafe_();
		return token;
	}

	@Override
	public com.goldhuman.Common.Octets getTokenOctets() { // 
		_xdb_verify_unsafe_();
		return com.goldhuman.Common.Octets.wrap(getToken(), xdb.Const.IO_CHARSET);
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
	public void setPackagename(String _v_) { // 
		_xdb_verify_unsafe_();
		if (null == _v_)
			throw new NullPointerException();
		xdb.Logs.logIf(new xdb.LogKey(this, "packagename") {
			protected xdb.Log create() {
				return new xdb.logs.LogString(this, packagename) {
					public void rollback() { packagename = _xdb_saved; }
				};}});
		packagename = _v_;
	}

	@Override
	public void setPackagenameOctets(com.goldhuman.Common.Octets _v_) { // 
		_xdb_verify_unsafe_();
		this.setPackagename(_v_.getString(xdb.Const.IO_CHARSET));
	}

	@Override
	public void setProductid(String _v_) { // 
		_xdb_verify_unsafe_();
		if (null == _v_)
			throw new NullPointerException();
		xdb.Logs.logIf(new xdb.LogKey(this, "productid") {
			protected xdb.Log create() {
				return new xdb.logs.LogString(this, productid) {
					public void rollback() { productid = _xdb_saved; }
				};}});
		productid = _v_;
	}

	@Override
	public void setProductidOctets(com.goldhuman.Common.Octets _v_) { // 
		_xdb_verify_unsafe_();
		this.setProductid(_v_.getString(xdb.Const.IO_CHARSET));
	}

	@Override
	public void setToken(String _v_) { // 
		_xdb_verify_unsafe_();
		if (null == _v_)
			throw new NullPointerException();
		xdb.Logs.logIf(new xdb.LogKey(this, "token") {
			protected xdb.Log create() {
				return new xdb.logs.LogString(this, token) {
					public void rollback() { token = _xdb_saved; }
				};}});
		token = _v_;
	}

	@Override
	public void setTokenOctets(com.goldhuman.Common.Octets _v_) { // 
		_xdb_verify_unsafe_();
		this.setToken(_v_.getString(xdb.Const.IO_CHARSET));
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		GoogleReceiptData _o_ = null;
		if ( _o1_ instanceof GoogleReceiptData ) _o_ = (GoogleReceiptData)_o1_;
		else if ( _o1_ instanceof GoogleReceiptData.Const ) _o_ = ((GoogleReceiptData.Const)_o1_).nThis();
		else return false;
		if (roleid != _o_.roleid) return false;
		if (!packagename.equals(_o_.packagename)) return false;
		if (!productid.equals(_o_.productid)) return false;
		if (!token.equals(_o_.token)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += roleid;
		_h_ += packagename.hashCode();
		_h_ += productid.hashCode();
		_h_ += token.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(roleid);
		_sb_.append(",");
		_sb_.append("'").append(packagename).append("'");
		_sb_.append(",");
		_sb_.append("'").append(productid).append("'");
		_sb_.append(",");
		_sb_.append("'").append(token).append("'");
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("roleid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("packagename"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("productid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("token"));
		return lb;
	}

	private class Const implements xbean.GoogleReceiptData {
		GoogleReceiptData nThis() {
			return GoogleReceiptData.this;
		}

		@Override
		public xbean.GoogleReceiptData copy() {
			return GoogleReceiptData.this.copy();
		}

		@Override
		public xbean.GoogleReceiptData toData() {
			return GoogleReceiptData.this.toData();
		}

		public xbean.GoogleReceiptData toBean() {
			return GoogleReceiptData.this.toBean();
		}

		@Override
		public xbean.GoogleReceiptData toDataIf() {
			return GoogleReceiptData.this.toDataIf();
		}

		public xbean.GoogleReceiptData toBeanIf() {
			return GoogleReceiptData.this.toBeanIf();
		}

		@Override
		public long getRoleid() { // 
			_xdb_verify_unsafe_();
			return roleid;
		}

		@Override
		public String getPackagename() { // 
			_xdb_verify_unsafe_();
			return packagename;
		}

		@Override
		public com.goldhuman.Common.Octets getPackagenameOctets() { // 
			_xdb_verify_unsafe_();
			return GoogleReceiptData.this.getPackagenameOctets();
		}

		@Override
		public String getProductid() { // 
			_xdb_verify_unsafe_();
			return productid;
		}

		@Override
		public com.goldhuman.Common.Octets getProductidOctets() { // 
			_xdb_verify_unsafe_();
			return GoogleReceiptData.this.getProductidOctets();
		}

		@Override
		public String getToken() { // 
			_xdb_verify_unsafe_();
			return token;
		}

		@Override
		public com.goldhuman.Common.Octets getTokenOctets() { // 
			_xdb_verify_unsafe_();
			return GoogleReceiptData.this.getTokenOctets();
		}

		@Override
		public void setRoleid(long _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setPackagename(String _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setPackagenameOctets(com.goldhuman.Common.Octets _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setProductid(String _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setProductidOctets(com.goldhuman.Common.Octets _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setToken(String _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTokenOctets(com.goldhuman.Common.Octets _v_) { // 
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
			return GoogleReceiptData.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return GoogleReceiptData.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return GoogleReceiptData.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return GoogleReceiptData.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return GoogleReceiptData.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return GoogleReceiptData.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return GoogleReceiptData.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return GoogleReceiptData.this.hashCode();
		}

		@Override
		public String toString() {
			return GoogleReceiptData.this.toString();
		}

	}

	public static final class Data implements xbean.GoogleReceiptData {
		private long roleid; // 
		private String packagename; // 
		private String productid; // 
		private String token; // 

		public Data() {
			packagename = "";
			productid = "";
			token = "";
		}

		Data(xbean.GoogleReceiptData _o1_) {
			if (_o1_ instanceof GoogleReceiptData) assign((GoogleReceiptData)_o1_);
			else if (_o1_ instanceof GoogleReceiptData.Data) assign((GoogleReceiptData.Data)_o1_);
			else if (_o1_ instanceof GoogleReceiptData.Const) assign(((GoogleReceiptData.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(GoogleReceiptData _o_) {
			roleid = _o_.roleid;
			packagename = _o_.packagename;
			productid = _o_.productid;
			token = _o_.token;
		}

		private void assign(GoogleReceiptData.Data _o_) {
			roleid = _o_.roleid;
			packagename = _o_.packagename;
			productid = _o_.productid;
			token = _o_.token;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(roleid);
			_os_.marshal(packagename, xdb.Const.IO_CHARSET);
			_os_.marshal(productid, xdb.Const.IO_CHARSET);
			_os_.marshal(token, xdb.Const.IO_CHARSET);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			roleid = _os_.unmarshal_long();
			packagename = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			productid = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			token = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			return _os_;
		}

		@Override
		public xbean.GoogleReceiptData copy() {
			return new Data(this);
		}

		@Override
		public xbean.GoogleReceiptData toData() {
			return new Data(this);
		}

		public xbean.GoogleReceiptData toBean() {
			return new GoogleReceiptData(this, null, null);
		}

		@Override
		public xbean.GoogleReceiptData toDataIf() {
			return this;
		}

		public xbean.GoogleReceiptData toBeanIf() {
			return new GoogleReceiptData(this, null, null);
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
		public String getPackagename() { // 
			return packagename;
		}

		@Override
		public com.goldhuman.Common.Octets getPackagenameOctets() { // 
			return com.goldhuman.Common.Octets.wrap(getPackagename(), xdb.Const.IO_CHARSET);
		}

		@Override
		public String getProductid() { // 
			return productid;
		}

		@Override
		public com.goldhuman.Common.Octets getProductidOctets() { // 
			return com.goldhuman.Common.Octets.wrap(getProductid(), xdb.Const.IO_CHARSET);
		}

		@Override
		public String getToken() { // 
			return token;
		}

		@Override
		public com.goldhuman.Common.Octets getTokenOctets() { // 
			return com.goldhuman.Common.Octets.wrap(getToken(), xdb.Const.IO_CHARSET);
		}

		@Override
		public void setRoleid(long _v_) { // 
			roleid = _v_;
		}

		@Override
		public void setPackagename(String _v_) { // 
			if (null == _v_)
				throw new NullPointerException();
			packagename = _v_;
		}

		@Override
		public void setPackagenameOctets(com.goldhuman.Common.Octets _v_) { // 
			this.setPackagename(_v_.getString(xdb.Const.IO_CHARSET));
		}

		@Override
		public void setProductid(String _v_) { // 
			if (null == _v_)
				throw new NullPointerException();
			productid = _v_;
		}

		@Override
		public void setProductidOctets(com.goldhuman.Common.Octets _v_) { // 
			this.setProductid(_v_.getString(xdb.Const.IO_CHARSET));
		}

		@Override
		public void setToken(String _v_) { // 
			if (null == _v_)
				throw new NullPointerException();
			token = _v_;
		}

		@Override
		public void setTokenOctets(com.goldhuman.Common.Octets _v_) { // 
			this.setToken(_v_.getString(xdb.Const.IO_CHARSET));
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof GoogleReceiptData.Data)) return false;
			GoogleReceiptData.Data _o_ = (GoogleReceiptData.Data) _o1_;
			if (roleid != _o_.roleid) return false;
			if (!packagename.equals(_o_.packagename)) return false;
			if (!productid.equals(_o_.productid)) return false;
			if (!token.equals(_o_.token)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += roleid;
			_h_ += packagename.hashCode();
			_h_ += productid.hashCode();
			_h_ += token.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(roleid);
			_sb_.append(",");
			_sb_.append("'").append(packagename).append("'");
			_sb_.append(",");
			_sb_.append("'").append(productid).append("'");
			_sb_.append(",");
			_sb_.append("'").append(token).append("'");
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
