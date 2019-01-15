
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class Shopbuy extends xdb.XBean implements xbean.Shopbuy {
	private int shopid; // 商城ID（key）
	private int todaynum; // 今日已购买次数
	private long lasttime; // 最后一次购买时间
	private int buyallnum; // 总共购买次数

	Shopbuy(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
	}

	public Shopbuy() {
		this(0, null, null);
	}

	public Shopbuy(Shopbuy _o_) {
		this(_o_, null, null);
	}

	Shopbuy(xbean.Shopbuy _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof Shopbuy) assign((Shopbuy)_o1_);
		else if (_o1_ instanceof Shopbuy.Data) assign((Shopbuy.Data)_o1_);
		else if (_o1_ instanceof Shopbuy.Const) assign(((Shopbuy.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(Shopbuy _o_) {
		_o_._xdb_verify_unsafe_();
		shopid = _o_.shopid;
		todaynum = _o_.todaynum;
		lasttime = _o_.lasttime;
		buyallnum = _o_.buyallnum;
	}

	private void assign(Shopbuy.Data _o_) {
		shopid = _o_.shopid;
		todaynum = _o_.todaynum;
		lasttime = _o_.lasttime;
		buyallnum = _o_.buyallnum;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(shopid);
		_os_.marshal(todaynum);
		_os_.marshal(lasttime);
		_os_.marshal(buyallnum);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		shopid = _os_.unmarshal_int();
		todaynum = _os_.unmarshal_int();
		lasttime = _os_.unmarshal_long();
		buyallnum = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.Shopbuy copy() {
		_xdb_verify_unsafe_();
		return new Shopbuy(this);
	}

	@Override
	public xbean.Shopbuy toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.Shopbuy toBean() {
		_xdb_verify_unsafe_();
		return new Shopbuy(this); // same as copy()
	}

	@Override
	public xbean.Shopbuy toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.Shopbuy toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getShopid() { // 商城ID（key）
		_xdb_verify_unsafe_();
		return shopid;
	}

	@Override
	public int getTodaynum() { // 今日已购买次数
		_xdb_verify_unsafe_();
		return todaynum;
	}

	@Override
	public long getLasttime() { // 最后一次购买时间
		_xdb_verify_unsafe_();
		return lasttime;
	}

	@Override
	public int getBuyallnum() { // 总共购买次数
		_xdb_verify_unsafe_();
		return buyallnum;
	}

	@Override
	public void setShopid(int _v_) { // 商城ID（key）
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "shopid") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, shopid) {
					public void rollback() { shopid = _xdb_saved; }
				};}});
		shopid = _v_;
	}

	@Override
	public void setTodaynum(int _v_) { // 今日已购买次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "todaynum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, todaynum) {
					public void rollback() { todaynum = _xdb_saved; }
				};}});
		todaynum = _v_;
	}

	@Override
	public void setLasttime(long _v_) { // 最后一次购买时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "lasttime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, lasttime) {
					public void rollback() { lasttime = _xdb_saved; }
				};}});
		lasttime = _v_;
	}

	@Override
	public void setBuyallnum(int _v_) { // 总共购买次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "buyallnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, buyallnum) {
					public void rollback() { buyallnum = _xdb_saved; }
				};}});
		buyallnum = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		Shopbuy _o_ = null;
		if ( _o1_ instanceof Shopbuy ) _o_ = (Shopbuy)_o1_;
		else if ( _o1_ instanceof Shopbuy.Const ) _o_ = ((Shopbuy.Const)_o1_).nThis();
		else return false;
		if (shopid != _o_.shopid) return false;
		if (todaynum != _o_.todaynum) return false;
		if (lasttime != _o_.lasttime) return false;
		if (buyallnum != _o_.buyallnum) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += shopid;
		_h_ += todaynum;
		_h_ += lasttime;
		_h_ += buyallnum;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(shopid);
		_sb_.append(",");
		_sb_.append(todaynum);
		_sb_.append(",");
		_sb_.append(lasttime);
		_sb_.append(",");
		_sb_.append(buyallnum);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("shopid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("todaynum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("lasttime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("buyallnum"));
		return lb;
	}

	private class Const implements xbean.Shopbuy {
		Shopbuy nThis() {
			return Shopbuy.this;
		}

		@Override
		public xbean.Shopbuy copy() {
			return Shopbuy.this.copy();
		}

		@Override
		public xbean.Shopbuy toData() {
			return Shopbuy.this.toData();
		}

		public xbean.Shopbuy toBean() {
			return Shopbuy.this.toBean();
		}

		@Override
		public xbean.Shopbuy toDataIf() {
			return Shopbuy.this.toDataIf();
		}

		public xbean.Shopbuy toBeanIf() {
			return Shopbuy.this.toBeanIf();
		}

		@Override
		public int getShopid() { // 商城ID（key）
			_xdb_verify_unsafe_();
			return shopid;
		}

		@Override
		public int getTodaynum() { // 今日已购买次数
			_xdb_verify_unsafe_();
			return todaynum;
		}

		@Override
		public long getLasttime() { // 最后一次购买时间
			_xdb_verify_unsafe_();
			return lasttime;
		}

		@Override
		public int getBuyallnum() { // 总共购买次数
			_xdb_verify_unsafe_();
			return buyallnum;
		}

		@Override
		public void setShopid(int _v_) { // 商城ID（key）
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTodaynum(int _v_) { // 今日已购买次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setLasttime(long _v_) { // 最后一次购买时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setBuyallnum(int _v_) { // 总共购买次数
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
			return Shopbuy.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return Shopbuy.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return Shopbuy.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return Shopbuy.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return Shopbuy.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return Shopbuy.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return Shopbuy.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return Shopbuy.this.hashCode();
		}

		@Override
		public String toString() {
			return Shopbuy.this.toString();
		}

	}

	public static final class Data implements xbean.Shopbuy {
		private int shopid; // 商城ID（key）
		private int todaynum; // 今日已购买次数
		private long lasttime; // 最后一次购买时间
		private int buyallnum; // 总共购买次数

		public Data() {
		}

		Data(xbean.Shopbuy _o1_) {
			if (_o1_ instanceof Shopbuy) assign((Shopbuy)_o1_);
			else if (_o1_ instanceof Shopbuy.Data) assign((Shopbuy.Data)_o1_);
			else if (_o1_ instanceof Shopbuy.Const) assign(((Shopbuy.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(Shopbuy _o_) {
			shopid = _o_.shopid;
			todaynum = _o_.todaynum;
			lasttime = _o_.lasttime;
			buyallnum = _o_.buyallnum;
		}

		private void assign(Shopbuy.Data _o_) {
			shopid = _o_.shopid;
			todaynum = _o_.todaynum;
			lasttime = _o_.lasttime;
			buyallnum = _o_.buyallnum;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(shopid);
			_os_.marshal(todaynum);
			_os_.marshal(lasttime);
			_os_.marshal(buyallnum);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			shopid = _os_.unmarshal_int();
			todaynum = _os_.unmarshal_int();
			lasttime = _os_.unmarshal_long();
			buyallnum = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.Shopbuy copy() {
			return new Data(this);
		}

		@Override
		public xbean.Shopbuy toData() {
			return new Data(this);
		}

		public xbean.Shopbuy toBean() {
			return new Shopbuy(this, null, null);
		}

		@Override
		public xbean.Shopbuy toDataIf() {
			return this;
		}

		public xbean.Shopbuy toBeanIf() {
			return new Shopbuy(this, null, null);
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
		public int getShopid() { // 商城ID（key）
			return shopid;
		}

		@Override
		public int getTodaynum() { // 今日已购买次数
			return todaynum;
		}

		@Override
		public long getLasttime() { // 最后一次购买时间
			return lasttime;
		}

		@Override
		public int getBuyallnum() { // 总共购买次数
			return buyallnum;
		}

		@Override
		public void setShopid(int _v_) { // 商城ID（key）
			shopid = _v_;
		}

		@Override
		public void setTodaynum(int _v_) { // 今日已购买次数
			todaynum = _v_;
		}

		@Override
		public void setLasttime(long _v_) { // 最后一次购买时间
			lasttime = _v_;
		}

		@Override
		public void setBuyallnum(int _v_) { // 总共购买次数
			buyallnum = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof Shopbuy.Data)) return false;
			Shopbuy.Data _o_ = (Shopbuy.Data) _o1_;
			if (shopid != _o_.shopid) return false;
			if (todaynum != _o_.todaynum) return false;
			if (lasttime != _o_.lasttime) return false;
			if (buyallnum != _o_.buyallnum) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += shopid;
			_h_ += todaynum;
			_h_ += lasttime;
			_h_ += buyallnum;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(shopid);
			_sb_.append(",");
			_sb_.append(todaynum);
			_sb_.append(",");
			_sb_.append(lasttime);
			_sb_.append(",");
			_sb_.append(buyallnum);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
