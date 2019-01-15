
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class FriendReqs extends xdb.XBean implements xbean.FriendReqs {
	private xdb.util.SetX<Long> byme; // 我邀请的人
	private xdb.util.SetX<Long> imby; // 邀请我的人

	FriendReqs(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		byme = new xdb.util.SetX<Long>();
		imby = new xdb.util.SetX<Long>();
	}

	public FriendReqs() {
		this(0, null, null);
	}

	public FriendReqs(FriendReqs _o_) {
		this(_o_, null, null);
	}

	FriendReqs(xbean.FriendReqs _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof FriendReqs) assign((FriendReqs)_o1_);
		else if (_o1_ instanceof FriendReqs.Data) assign((FriendReqs.Data)_o1_);
		else if (_o1_ instanceof FriendReqs.Const) assign(((FriendReqs.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(FriendReqs _o_) {
		_o_._xdb_verify_unsafe_();
		byme = new xdb.util.SetX<Long>();
		byme.addAll(_o_.byme);
		imby = new xdb.util.SetX<Long>();
		imby.addAll(_o_.imby);
	}

	private void assign(FriendReqs.Data _o_) {
		byme = new xdb.util.SetX<Long>();
		byme.addAll(_o_.byme);
		imby = new xdb.util.SetX<Long>();
		imby.addAll(_o_.imby);
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(byme.size());
		for (Long _v_ : byme) {
			_os_.marshal(_v_);
		}
		_os_.compact_uint32(imby.size());
		for (Long _v_ : imby) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			long _v_ = 0;
			_v_ = _os_.unmarshal_long();
			byme.add(_v_);
		}
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			long _v_ = 0;
			_v_ = _os_.unmarshal_long();
			imby.add(_v_);
		}
		return _os_;
	}

	@Override
	public xbean.FriendReqs copy() {
		_xdb_verify_unsafe_();
		return new FriendReqs(this);
	}

	@Override
	public xbean.FriendReqs toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.FriendReqs toBean() {
		_xdb_verify_unsafe_();
		return new FriendReqs(this); // same as copy()
	}

	@Override
	public xbean.FriendReqs toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.FriendReqs toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.Set<Long> getByme() { // 我邀请的人
		_xdb_verify_unsafe_();
		return xdb.Logs.logSet(new xdb.LogKey(this, "byme"), byme);
	}

	public java.util.Set<Long> getBymeAsData() { // 我邀请的人
		_xdb_verify_unsafe_();
		java.util.Set<Long> byme;
		FriendReqs _o_ = this;
		byme = new xdb.util.SetX<Long>();
		byme.addAll(_o_.byme);
		return byme;
	}

	@Override
	public java.util.Set<Long> getImby() { // 邀请我的人
		_xdb_verify_unsafe_();
		return xdb.Logs.logSet(new xdb.LogKey(this, "imby"), imby);
	}

	public java.util.Set<Long> getImbyAsData() { // 邀请我的人
		_xdb_verify_unsafe_();
		java.util.Set<Long> imby;
		FriendReqs _o_ = this;
		imby = new xdb.util.SetX<Long>();
		imby.addAll(_o_.imby);
		return imby;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		FriendReqs _o_ = null;
		if ( _o1_ instanceof FriendReqs ) _o_ = (FriendReqs)_o1_;
		else if ( _o1_ instanceof FriendReqs.Const ) _o_ = ((FriendReqs.Const)_o1_).nThis();
		else return false;
		if (!byme.equals(_o_.byme)) return false;
		if (!imby.equals(_o_.imby)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += byme.hashCode();
		_h_ += imby.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(byme);
		_sb_.append(",");
		_sb_.append(imby);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableSet().setVarName("byme"));
		lb.add(new xdb.logs.ListenableSet().setVarName("imby"));
		return lb;
	}

	private class Const implements xbean.FriendReqs {
		FriendReqs nThis() {
			return FriendReqs.this;
		}

		@Override
		public xbean.FriendReqs copy() {
			return FriendReqs.this.copy();
		}

		@Override
		public xbean.FriendReqs toData() {
			return FriendReqs.this.toData();
		}

		public xbean.FriendReqs toBean() {
			return FriendReqs.this.toBean();
		}

		@Override
		public xbean.FriendReqs toDataIf() {
			return FriendReqs.this.toDataIf();
		}

		public xbean.FriendReqs toBeanIf() {
			return FriendReqs.this.toBeanIf();
		}

		@Override
		public java.util.Set<Long> getByme() { // 我邀请的人
			_xdb_verify_unsafe_();
			return xdb.Consts.constSet(byme);
		}

		public java.util.Set<Long> getBymeAsData() { // 我邀请的人
			_xdb_verify_unsafe_();
			java.util.Set<Long> byme;
			FriendReqs _o_ = FriendReqs.this;
		byme = new xdb.util.SetX<Long>();
		byme.addAll(_o_.byme);
			return byme;
		}

		@Override
		public java.util.Set<Long> getImby() { // 邀请我的人
			_xdb_verify_unsafe_();
			return xdb.Consts.constSet(imby);
		}

		public java.util.Set<Long> getImbyAsData() { // 邀请我的人
			_xdb_verify_unsafe_();
			java.util.Set<Long> imby;
			FriendReqs _o_ = FriendReqs.this;
		imby = new xdb.util.SetX<Long>();
		imby.addAll(_o_.imby);
			return imby;
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
			return FriendReqs.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return FriendReqs.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return FriendReqs.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return FriendReqs.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return FriendReqs.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return FriendReqs.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return FriendReqs.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return FriendReqs.this.hashCode();
		}

		@Override
		public String toString() {
			return FriendReqs.this.toString();
		}

	}

	public static final class Data implements xbean.FriendReqs {
		private java.util.HashSet<Long> byme; // 我邀请的人
		private java.util.HashSet<Long> imby; // 邀请我的人

		public Data() {
			byme = new java.util.HashSet<Long>();
			imby = new java.util.HashSet<Long>();
		}

		Data(xbean.FriendReqs _o1_) {
			if (_o1_ instanceof FriendReqs) assign((FriendReqs)_o1_);
			else if (_o1_ instanceof FriendReqs.Data) assign((FriendReqs.Data)_o1_);
			else if (_o1_ instanceof FriendReqs.Const) assign(((FriendReqs.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(FriendReqs _o_) {
			byme = new java.util.HashSet<Long>();
			byme.addAll(_o_.byme);
			imby = new java.util.HashSet<Long>();
			imby.addAll(_o_.imby);
		}

		private void assign(FriendReqs.Data _o_) {
			byme = new java.util.HashSet<Long>();
			byme.addAll(_o_.byme);
			imby = new java.util.HashSet<Long>();
			imby.addAll(_o_.imby);
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(byme.size());
			for (Long _v_ : byme) {
				_os_.marshal(_v_);
			}
			_os_.compact_uint32(imby.size());
			for (Long _v_ : imby) {
				_os_.marshal(_v_);
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				long _v_ = 0;
				_v_ = _os_.unmarshal_long();
				byme.add(_v_);
			}
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				long _v_ = 0;
				_v_ = _os_.unmarshal_long();
				imby.add(_v_);
			}
			return _os_;
		}

		@Override
		public xbean.FriendReqs copy() {
			return new Data(this);
		}

		@Override
		public xbean.FriendReqs toData() {
			return new Data(this);
		}

		public xbean.FriendReqs toBean() {
			return new FriendReqs(this, null, null);
		}

		@Override
		public xbean.FriendReqs toDataIf() {
			return this;
		}

		public xbean.FriendReqs toBeanIf() {
			return new FriendReqs(this, null, null);
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
		public java.util.Set<Long> getByme() { // 我邀请的人
			return byme;
		}

		@Override
		public java.util.Set<Long> getBymeAsData() { // 我邀请的人
			return byme;
		}

		@Override
		public java.util.Set<Long> getImby() { // 邀请我的人
			return imby;
		}

		@Override
		public java.util.Set<Long> getImbyAsData() { // 邀请我的人
			return imby;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof FriendReqs.Data)) return false;
			FriendReqs.Data _o_ = (FriendReqs.Data) _o1_;
			if (!byme.equals(_o_.byme)) return false;
			if (!imby.equals(_o_.imby)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += byme.hashCode();
			_h_ += imby.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(byme);
			_sb_.append(",");
			_sb_.append(imby);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
