
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class AppReceiptData extends xdb.XBean implements xbean.AppReceiptData {
	private long roleid; // 
	private String receipt; // 苹果账单

	AppReceiptData(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		receipt = "";
	}

	public AppReceiptData() {
		this(0, null, null);
	}

	public AppReceiptData(AppReceiptData _o_) {
		this(_o_, null, null);
	}

	AppReceiptData(xbean.AppReceiptData _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof AppReceiptData) assign((AppReceiptData)_o1_);
		else if (_o1_ instanceof AppReceiptData.Data) assign((AppReceiptData.Data)_o1_);
		else if (_o1_ instanceof AppReceiptData.Const) assign(((AppReceiptData.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(AppReceiptData _o_) {
		_o_._xdb_verify_unsafe_();
		roleid = _o_.roleid;
		receipt = _o_.receipt;
	}

	private void assign(AppReceiptData.Data _o_) {
		roleid = _o_.roleid;
		receipt = _o_.receipt;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(roleid);
		_os_.marshal(receipt, xdb.Const.IO_CHARSET);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		roleid = _os_.unmarshal_long();
		receipt = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
		return _os_;
	}

	@Override
	public xbean.AppReceiptData copy() {
		_xdb_verify_unsafe_();
		return new AppReceiptData(this);
	}

	@Override
	public xbean.AppReceiptData toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.AppReceiptData toBean() {
		_xdb_verify_unsafe_();
		return new AppReceiptData(this); // same as copy()
	}

	@Override
	public xbean.AppReceiptData toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.AppReceiptData toBeanIf() {
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
	public String getReceipt() { // 苹果账单
		_xdb_verify_unsafe_();
		return receipt;
	}

	@Override
	public com.goldhuman.Common.Octets getReceiptOctets() { // 苹果账单
		_xdb_verify_unsafe_();
		return com.goldhuman.Common.Octets.wrap(getReceipt(), xdb.Const.IO_CHARSET);
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
	public void setReceipt(String _v_) { // 苹果账单
		_xdb_verify_unsafe_();
		if (null == _v_)
			throw new NullPointerException();
		xdb.Logs.logIf(new xdb.LogKey(this, "receipt") {
			protected xdb.Log create() {
				return new xdb.logs.LogString(this, receipt) {
					public void rollback() { receipt = _xdb_saved; }
				};}});
		receipt = _v_;
	}

	@Override
	public void setReceiptOctets(com.goldhuman.Common.Octets _v_) { // 苹果账单
		_xdb_verify_unsafe_();
		this.setReceipt(_v_.getString(xdb.Const.IO_CHARSET));
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		AppReceiptData _o_ = null;
		if ( _o1_ instanceof AppReceiptData ) _o_ = (AppReceiptData)_o1_;
		else if ( _o1_ instanceof AppReceiptData.Const ) _o_ = ((AppReceiptData.Const)_o1_).nThis();
		else return false;
		if (roleid != _o_.roleid) return false;
		if (!receipt.equals(_o_.receipt)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += roleid;
		_h_ += receipt.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(roleid);
		_sb_.append(",");
		_sb_.append("'").append(receipt).append("'");
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("roleid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("receipt"));
		return lb;
	}

	private class Const implements xbean.AppReceiptData {
		AppReceiptData nThis() {
			return AppReceiptData.this;
		}

		@Override
		public xbean.AppReceiptData copy() {
			return AppReceiptData.this.copy();
		}

		@Override
		public xbean.AppReceiptData toData() {
			return AppReceiptData.this.toData();
		}

		public xbean.AppReceiptData toBean() {
			return AppReceiptData.this.toBean();
		}

		@Override
		public xbean.AppReceiptData toDataIf() {
			return AppReceiptData.this.toDataIf();
		}

		public xbean.AppReceiptData toBeanIf() {
			return AppReceiptData.this.toBeanIf();
		}

		@Override
		public long getRoleid() { // 
			_xdb_verify_unsafe_();
			return roleid;
		}

		@Override
		public String getReceipt() { // 苹果账单
			_xdb_verify_unsafe_();
			return receipt;
		}

		@Override
		public com.goldhuman.Common.Octets getReceiptOctets() { // 苹果账单
			_xdb_verify_unsafe_();
			return AppReceiptData.this.getReceiptOctets();
		}

		@Override
		public void setRoleid(long _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setReceipt(String _v_) { // 苹果账单
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setReceiptOctets(com.goldhuman.Common.Octets _v_) { // 苹果账单
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
			return AppReceiptData.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return AppReceiptData.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return AppReceiptData.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return AppReceiptData.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return AppReceiptData.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return AppReceiptData.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return AppReceiptData.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return AppReceiptData.this.hashCode();
		}

		@Override
		public String toString() {
			return AppReceiptData.this.toString();
		}

	}

	public static final class Data implements xbean.AppReceiptData {
		private long roleid; // 
		private String receipt; // 苹果账单

		public Data() {
			receipt = "";
		}

		Data(xbean.AppReceiptData _o1_) {
			if (_o1_ instanceof AppReceiptData) assign((AppReceiptData)_o1_);
			else if (_o1_ instanceof AppReceiptData.Data) assign((AppReceiptData.Data)_o1_);
			else if (_o1_ instanceof AppReceiptData.Const) assign(((AppReceiptData.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(AppReceiptData _o_) {
			roleid = _o_.roleid;
			receipt = _o_.receipt;
		}

		private void assign(AppReceiptData.Data _o_) {
			roleid = _o_.roleid;
			receipt = _o_.receipt;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(roleid);
			_os_.marshal(receipt, xdb.Const.IO_CHARSET);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			roleid = _os_.unmarshal_long();
			receipt = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			return _os_;
		}

		@Override
		public xbean.AppReceiptData copy() {
			return new Data(this);
		}

		@Override
		public xbean.AppReceiptData toData() {
			return new Data(this);
		}

		public xbean.AppReceiptData toBean() {
			return new AppReceiptData(this, null, null);
		}

		@Override
		public xbean.AppReceiptData toDataIf() {
			return this;
		}

		public xbean.AppReceiptData toBeanIf() {
			return new AppReceiptData(this, null, null);
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
		public String getReceipt() { // 苹果账单
			return receipt;
		}

		@Override
		public com.goldhuman.Common.Octets getReceiptOctets() { // 苹果账单
			return com.goldhuman.Common.Octets.wrap(getReceipt(), xdb.Const.IO_CHARSET);
		}

		@Override
		public void setRoleid(long _v_) { // 
			roleid = _v_;
		}

		@Override
		public void setReceipt(String _v_) { // 苹果账单
			if (null == _v_)
				throw new NullPointerException();
			receipt = _v_;
		}

		@Override
		public void setReceiptOctets(com.goldhuman.Common.Octets _v_) { // 苹果账单
			this.setReceipt(_v_.getString(xdb.Const.IO_CHARSET));
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof AppReceiptData.Data)) return false;
			AppReceiptData.Data _o_ = (AppReceiptData.Data) _o1_;
			if (roleid != _o_.roleid) return false;
			if (!receipt.equals(_o_.receipt)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += roleid;
			_h_ += receipt.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(roleid);
			_sb_.append(",");
			_sb_.append("'").append(receipt).append("'");
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
