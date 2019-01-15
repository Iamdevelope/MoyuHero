
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class NewShopList extends xdb.XBean implements xbean.NewShopList {
	private java.util.LinkedList<xbean.NewShop> shoplist; // 单个商城列表
	private long lasttime; // 正常刷新时间
	private long refreshtime; // 手动刷新时间
	private int refreshnum; // 手动刷新次数

	NewShopList(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		shoplist = new java.util.LinkedList<xbean.NewShop>();
	}

	public NewShopList() {
		this(0, null, null);
	}

	public NewShopList(NewShopList _o_) {
		this(_o_, null, null);
	}

	NewShopList(xbean.NewShopList _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof NewShopList) assign((NewShopList)_o1_);
		else if (_o1_ instanceof NewShopList.Data) assign((NewShopList.Data)_o1_);
		else if (_o1_ instanceof NewShopList.Const) assign(((NewShopList.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(NewShopList _o_) {
		_o_._xdb_verify_unsafe_();
		shoplist = new java.util.LinkedList<xbean.NewShop>();
		for (xbean.NewShop _v_ : _o_.shoplist)
			shoplist.add(new NewShop(_v_, this, "shoplist"));
		lasttime = _o_.lasttime;
		refreshtime = _o_.refreshtime;
		refreshnum = _o_.refreshnum;
	}

	private void assign(NewShopList.Data _o_) {
		shoplist = new java.util.LinkedList<xbean.NewShop>();
		for (xbean.NewShop _v_ : _o_.shoplist)
			shoplist.add(new NewShop(_v_, this, "shoplist"));
		lasttime = _o_.lasttime;
		refreshtime = _o_.refreshtime;
		refreshnum = _o_.refreshnum;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(shoplist.size());
		for (xbean.NewShop _v_ : shoplist) {
			_v_.marshal(_os_);
		}
		_os_.marshal(lasttime);
		_os_.marshal(refreshtime);
		_os_.marshal(refreshnum);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			xbean.NewShop _v_ = new NewShop(0, this, "shoplist");
			_v_.unmarshal(_os_);
			shoplist.add(_v_);
		}
		lasttime = _os_.unmarshal_long();
		refreshtime = _os_.unmarshal_long();
		refreshnum = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.NewShopList copy() {
		_xdb_verify_unsafe_();
		return new NewShopList(this);
	}

	@Override
	public xbean.NewShopList toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.NewShopList toBean() {
		_xdb_verify_unsafe_();
		return new NewShopList(this); // same as copy()
	}

	@Override
	public xbean.NewShopList toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.NewShopList toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.List<xbean.NewShop> getShoplist() { // 单个商城列表
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "shoplist"), shoplist);
	}

	public java.util.List<xbean.NewShop> getShoplistAsData() { // 单个商城列表
		_xdb_verify_unsafe_();
		java.util.List<xbean.NewShop> shoplist;
		NewShopList _o_ = this;
		shoplist = new java.util.LinkedList<xbean.NewShop>();
		for (xbean.NewShop _v_ : _o_.shoplist)
			shoplist.add(new NewShop.Data(_v_));
		return shoplist;
	}

	@Override
	public long getLasttime() { // 正常刷新时间
		_xdb_verify_unsafe_();
		return lasttime;
	}

	@Override
	public long getRefreshtime() { // 手动刷新时间
		_xdb_verify_unsafe_();
		return refreshtime;
	}

	@Override
	public int getRefreshnum() { // 手动刷新次数
		_xdb_verify_unsafe_();
		return refreshnum;
	}

	@Override
	public void setLasttime(long _v_) { // 正常刷新时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "lasttime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, lasttime) {
					public void rollback() { lasttime = _xdb_saved; }
				};}});
		lasttime = _v_;
	}

	@Override
	public void setRefreshtime(long _v_) { // 手动刷新时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "refreshtime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, refreshtime) {
					public void rollback() { refreshtime = _xdb_saved; }
				};}});
		refreshtime = _v_;
	}

	@Override
	public void setRefreshnum(int _v_) { // 手动刷新次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "refreshnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, refreshnum) {
					public void rollback() { refreshnum = _xdb_saved; }
				};}});
		refreshnum = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		NewShopList _o_ = null;
		if ( _o1_ instanceof NewShopList ) _o_ = (NewShopList)_o1_;
		else if ( _o1_ instanceof NewShopList.Const ) _o_ = ((NewShopList.Const)_o1_).nThis();
		else return false;
		if (!shoplist.equals(_o_.shoplist)) return false;
		if (lasttime != _o_.lasttime) return false;
		if (refreshtime != _o_.refreshtime) return false;
		if (refreshnum != _o_.refreshnum) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += shoplist.hashCode();
		_h_ += lasttime;
		_h_ += refreshtime;
		_h_ += refreshnum;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(shoplist);
		_sb_.append(",");
		_sb_.append(lasttime);
		_sb_.append(",");
		_sb_.append(refreshtime);
		_sb_.append(",");
		_sb_.append(refreshnum);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("shoplist"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("lasttime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("refreshtime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("refreshnum"));
		return lb;
	}

	private class Const implements xbean.NewShopList {
		NewShopList nThis() {
			return NewShopList.this;
		}

		@Override
		public xbean.NewShopList copy() {
			return NewShopList.this.copy();
		}

		@Override
		public xbean.NewShopList toData() {
			return NewShopList.this.toData();
		}

		public xbean.NewShopList toBean() {
			return NewShopList.this.toBean();
		}

		@Override
		public xbean.NewShopList toDataIf() {
			return NewShopList.this.toDataIf();
		}

		public xbean.NewShopList toBeanIf() {
			return NewShopList.this.toBeanIf();
		}

		@Override
		public java.util.List<xbean.NewShop> getShoplist() { // 单个商城列表
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(shoplist);
		}

		public java.util.List<xbean.NewShop> getShoplistAsData() { // 单个商城列表
			_xdb_verify_unsafe_();
			java.util.List<xbean.NewShop> shoplist;
			NewShopList _o_ = NewShopList.this;
		shoplist = new java.util.LinkedList<xbean.NewShop>();
		for (xbean.NewShop _v_ : _o_.shoplist)
			shoplist.add(new NewShop.Data(_v_));
			return shoplist;
		}

		@Override
		public long getLasttime() { // 正常刷新时间
			_xdb_verify_unsafe_();
			return lasttime;
		}

		@Override
		public long getRefreshtime() { // 手动刷新时间
			_xdb_verify_unsafe_();
			return refreshtime;
		}

		@Override
		public int getRefreshnum() { // 手动刷新次数
			_xdb_verify_unsafe_();
			return refreshnum;
		}

		@Override
		public void setLasttime(long _v_) { // 正常刷新时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setRefreshtime(long _v_) { // 手动刷新时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setRefreshnum(int _v_) { // 手动刷新次数
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
			return NewShopList.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return NewShopList.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return NewShopList.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return NewShopList.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return NewShopList.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return NewShopList.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return NewShopList.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return NewShopList.this.hashCode();
		}

		@Override
		public String toString() {
			return NewShopList.this.toString();
		}

	}

	public static final class Data implements xbean.NewShopList {
		private java.util.LinkedList<xbean.NewShop> shoplist; // 单个商城列表
		private long lasttime; // 正常刷新时间
		private long refreshtime; // 手动刷新时间
		private int refreshnum; // 手动刷新次数

		public Data() {
			shoplist = new java.util.LinkedList<xbean.NewShop>();
		}

		Data(xbean.NewShopList _o1_) {
			if (_o1_ instanceof NewShopList) assign((NewShopList)_o1_);
			else if (_o1_ instanceof NewShopList.Data) assign((NewShopList.Data)_o1_);
			else if (_o1_ instanceof NewShopList.Const) assign(((NewShopList.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(NewShopList _o_) {
			shoplist = new java.util.LinkedList<xbean.NewShop>();
			for (xbean.NewShop _v_ : _o_.shoplist)
				shoplist.add(new NewShop.Data(_v_));
			lasttime = _o_.lasttime;
			refreshtime = _o_.refreshtime;
			refreshnum = _o_.refreshnum;
		}

		private void assign(NewShopList.Data _o_) {
			shoplist = new java.util.LinkedList<xbean.NewShop>();
			for (xbean.NewShop _v_ : _o_.shoplist)
				shoplist.add(new NewShop.Data(_v_));
			lasttime = _o_.lasttime;
			refreshtime = _o_.refreshtime;
			refreshnum = _o_.refreshnum;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(shoplist.size());
			for (xbean.NewShop _v_ : shoplist) {
				_v_.marshal(_os_);
			}
			_os_.marshal(lasttime);
			_os_.marshal(refreshtime);
			_os_.marshal(refreshnum);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				xbean.NewShop _v_ = xbean.Pod.newNewShopData();
				_v_.unmarshal(_os_);
				shoplist.add(_v_);
			}
			lasttime = _os_.unmarshal_long();
			refreshtime = _os_.unmarshal_long();
			refreshnum = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.NewShopList copy() {
			return new Data(this);
		}

		@Override
		public xbean.NewShopList toData() {
			return new Data(this);
		}

		public xbean.NewShopList toBean() {
			return new NewShopList(this, null, null);
		}

		@Override
		public xbean.NewShopList toDataIf() {
			return this;
		}

		public xbean.NewShopList toBeanIf() {
			return new NewShopList(this, null, null);
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
		public java.util.List<xbean.NewShop> getShoplist() { // 单个商城列表
			return shoplist;
		}

		@Override
		public java.util.List<xbean.NewShop> getShoplistAsData() { // 单个商城列表
			return shoplist;
		}

		@Override
		public long getLasttime() { // 正常刷新时间
			return lasttime;
		}

		@Override
		public long getRefreshtime() { // 手动刷新时间
			return refreshtime;
		}

		@Override
		public int getRefreshnum() { // 手动刷新次数
			return refreshnum;
		}

		@Override
		public void setLasttime(long _v_) { // 正常刷新时间
			lasttime = _v_;
		}

		@Override
		public void setRefreshtime(long _v_) { // 手动刷新时间
			refreshtime = _v_;
		}

		@Override
		public void setRefreshnum(int _v_) { // 手动刷新次数
			refreshnum = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof NewShopList.Data)) return false;
			NewShopList.Data _o_ = (NewShopList.Data) _o1_;
			if (!shoplist.equals(_o_.shoplist)) return false;
			if (lasttime != _o_.lasttime) return false;
			if (refreshtime != _o_.refreshtime) return false;
			if (refreshnum != _o_.refreshnum) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += shoplist.hashCode();
			_h_ += lasttime;
			_h_ += refreshtime;
			_h_ += refreshnum;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(shoplist);
			_sb_.append(",");
			_sb_.append(lasttime);
			_sb_.append(",");
			_sb_.append(refreshtime);
			_sb_.append(",");
			_sb_.append(refreshnum);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
