
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class Mails extends xdb.XBean implements xbean.Mails {
	private java.util.LinkedList<xbean.Mail> mails; // 
	private int nextkey; // 

	Mails(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		mails = new java.util.LinkedList<xbean.Mail>();
	}

	public Mails() {
		this(0, null, null);
	}

	public Mails(Mails _o_) {
		this(_o_, null, null);
	}

	Mails(xbean.Mails _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof Mails) assign((Mails)_o1_);
		else if (_o1_ instanceof Mails.Data) assign((Mails.Data)_o1_);
		else if (_o1_ instanceof Mails.Const) assign(((Mails.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(Mails _o_) {
		_o_._xdb_verify_unsafe_();
		mails = new java.util.LinkedList<xbean.Mail>();
		for (xbean.Mail _v_ : _o_.mails)
			mails.add(new Mail(_v_, this, "mails"));
		nextkey = _o_.nextkey;
	}

	private void assign(Mails.Data _o_) {
		mails = new java.util.LinkedList<xbean.Mail>();
		for (xbean.Mail _v_ : _o_.mails)
			mails.add(new Mail(_v_, this, "mails"));
		nextkey = _o_.nextkey;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(mails.size());
		for (xbean.Mail _v_ : mails) {
			_v_.marshal(_os_);
		}
		_os_.marshal(nextkey);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			xbean.Mail _v_ = new Mail(0, this, "mails");
			_v_.unmarshal(_os_);
			mails.add(_v_);
		}
		nextkey = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.Mails copy() {
		_xdb_verify_unsafe_();
		return new Mails(this);
	}

	@Override
	public xbean.Mails toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.Mails toBean() {
		_xdb_verify_unsafe_();
		return new Mails(this); // same as copy()
	}

	@Override
	public xbean.Mails toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.Mails toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.List<xbean.Mail> getMails() { // 
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "mails"), mails);
	}

	public java.util.List<xbean.Mail> getMailsAsData() { // 
		_xdb_verify_unsafe_();
		java.util.List<xbean.Mail> mails;
		Mails _o_ = this;
		mails = new java.util.LinkedList<xbean.Mail>();
		for (xbean.Mail _v_ : _o_.mails)
			mails.add(new Mail.Data(_v_));
		return mails;
	}

	@Override
	public int getNextkey() { // 
		_xdb_verify_unsafe_();
		return nextkey;
	}

	@Override
	public void setNextkey(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "nextkey") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, nextkey) {
					public void rollback() { nextkey = _xdb_saved; }
				};}});
		nextkey = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		Mails _o_ = null;
		if ( _o1_ instanceof Mails ) _o_ = (Mails)_o1_;
		else if ( _o1_ instanceof Mails.Const ) _o_ = ((Mails.Const)_o1_).nThis();
		else return false;
		if (!mails.equals(_o_.mails)) return false;
		if (nextkey != _o_.nextkey) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += mails.hashCode();
		_h_ += nextkey;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(mails);
		_sb_.append(",");
		_sb_.append(nextkey);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("mails"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("nextkey"));
		return lb;
	}

	private class Const implements xbean.Mails {
		Mails nThis() {
			return Mails.this;
		}

		@Override
		public xbean.Mails copy() {
			return Mails.this.copy();
		}

		@Override
		public xbean.Mails toData() {
			return Mails.this.toData();
		}

		public xbean.Mails toBean() {
			return Mails.this.toBean();
		}

		@Override
		public xbean.Mails toDataIf() {
			return Mails.this.toDataIf();
		}

		public xbean.Mails toBeanIf() {
			return Mails.this.toBeanIf();
		}

		@Override
		public java.util.List<xbean.Mail> getMails() { // 
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(mails);
		}

		public java.util.List<xbean.Mail> getMailsAsData() { // 
			_xdb_verify_unsafe_();
			java.util.List<xbean.Mail> mails;
			Mails _o_ = Mails.this;
		mails = new java.util.LinkedList<xbean.Mail>();
		for (xbean.Mail _v_ : _o_.mails)
			mails.add(new Mail.Data(_v_));
			return mails;
		}

		@Override
		public int getNextkey() { // 
			_xdb_verify_unsafe_();
			return nextkey;
		}

		@Override
		public void setNextkey(int _v_) { // 
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
			return Mails.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return Mails.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return Mails.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return Mails.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return Mails.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return Mails.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return Mails.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return Mails.this.hashCode();
		}

		@Override
		public String toString() {
			return Mails.this.toString();
		}

	}

	public static final class Data implements xbean.Mails {
		private java.util.LinkedList<xbean.Mail> mails; // 
		private int nextkey; // 

		public Data() {
			mails = new java.util.LinkedList<xbean.Mail>();
		}

		Data(xbean.Mails _o1_) {
			if (_o1_ instanceof Mails) assign((Mails)_o1_);
			else if (_o1_ instanceof Mails.Data) assign((Mails.Data)_o1_);
			else if (_o1_ instanceof Mails.Const) assign(((Mails.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(Mails _o_) {
			mails = new java.util.LinkedList<xbean.Mail>();
			for (xbean.Mail _v_ : _o_.mails)
				mails.add(new Mail.Data(_v_));
			nextkey = _o_.nextkey;
		}

		private void assign(Mails.Data _o_) {
			mails = new java.util.LinkedList<xbean.Mail>();
			for (xbean.Mail _v_ : _o_.mails)
				mails.add(new Mail.Data(_v_));
			nextkey = _o_.nextkey;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(mails.size());
			for (xbean.Mail _v_ : mails) {
				_v_.marshal(_os_);
			}
			_os_.marshal(nextkey);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				xbean.Mail _v_ = xbean.Pod.newMailData();
				_v_.unmarshal(_os_);
				mails.add(_v_);
			}
			nextkey = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.Mails copy() {
			return new Data(this);
		}

		@Override
		public xbean.Mails toData() {
			return new Data(this);
		}

		public xbean.Mails toBean() {
			return new Mails(this, null, null);
		}

		@Override
		public xbean.Mails toDataIf() {
			return this;
		}

		public xbean.Mails toBeanIf() {
			return new Mails(this, null, null);
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
		public java.util.List<xbean.Mail> getMails() { // 
			return mails;
		}

		@Override
		public java.util.List<xbean.Mail> getMailsAsData() { // 
			return mails;
		}

		@Override
		public int getNextkey() { // 
			return nextkey;
		}

		@Override
		public void setNextkey(int _v_) { // 
			nextkey = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof Mails.Data)) return false;
			Mails.Data _o_ = (Mails.Data) _o1_;
			if (!mails.equals(_o_.mails)) return false;
			if (nextkey != _o_.nextkey) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += mails.hashCode();
			_h_ += nextkey;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(mails);
			_sb_.append(",");
			_sb_.append(nextkey);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
