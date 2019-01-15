
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class bossshop extends xdb.XBean implements xbean.bossshop {
	private long time; // 刷新时间
	private java.util.LinkedList<Integer> shoplist; // 今天可买的物品表
	private int hunternum; // 今日猎人集市累计兑换次数

	bossshop(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		shoplist = new java.util.LinkedList<Integer>();
	}

	public bossshop() {
		this(0, null, null);
	}

	public bossshop(bossshop _o_) {
		this(_o_, null, null);
	}

	bossshop(xbean.bossshop _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof bossshop) assign((bossshop)_o1_);
		else if (_o1_ instanceof bossshop.Data) assign((bossshop.Data)_o1_);
		else if (_o1_ instanceof bossshop.Const) assign(((bossshop.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(bossshop _o_) {
		_o_._xdb_verify_unsafe_();
		time = _o_.time;
		shoplist = new java.util.LinkedList<Integer>();
		shoplist.addAll(_o_.shoplist);
		hunternum = _o_.hunternum;
	}

	private void assign(bossshop.Data _o_) {
		time = _o_.time;
		shoplist = new java.util.LinkedList<Integer>();
		shoplist.addAll(_o_.shoplist);
		hunternum = _o_.hunternum;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(time);
		_os_.compact_uint32(shoplist.size());
		for (Integer _v_ : shoplist) {
			_os_.marshal(_v_);
		}
		_os_.marshal(hunternum);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		time = _os_.unmarshal_long();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _v_ = 0;
			_v_ = _os_.unmarshal_int();
			shoplist.add(_v_);
		}
		hunternum = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.bossshop copy() {
		_xdb_verify_unsafe_();
		return new bossshop(this);
	}

	@Override
	public xbean.bossshop toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.bossshop toBean() {
		_xdb_verify_unsafe_();
		return new bossshop(this); // same as copy()
	}

	@Override
	public xbean.bossshop toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.bossshop toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public long getTime() { // 刷新时间
		_xdb_verify_unsafe_();
		return time;
	}

	@Override
	public java.util.List<Integer> getShoplist() { // 今天可买的物品表
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "shoplist"), shoplist);
	}

	public java.util.List<Integer> getShoplistAsData() { // 今天可买的物品表
		_xdb_verify_unsafe_();
		java.util.List<Integer> shoplist;
		bossshop _o_ = this;
		shoplist = new java.util.LinkedList<Integer>();
		shoplist.addAll(_o_.shoplist);
		return shoplist;
	}

	@Override
	public int getHunternum() { // 今日猎人集市累计兑换次数
		_xdb_verify_unsafe_();
		return hunternum;
	}

	@Override
	public void setTime(long _v_) { // 刷新时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "time") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, time) {
					public void rollback() { time = _xdb_saved; }
				};}});
		time = _v_;
	}

	@Override
	public void setHunternum(int _v_) { // 今日猎人集市累计兑换次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "hunternum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, hunternum) {
					public void rollback() { hunternum = _xdb_saved; }
				};}});
		hunternum = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		bossshop _o_ = null;
		if ( _o1_ instanceof bossshop ) _o_ = (bossshop)_o1_;
		else if ( _o1_ instanceof bossshop.Const ) _o_ = ((bossshop.Const)_o1_).nThis();
		else return false;
		if (time != _o_.time) return false;
		if (!shoplist.equals(_o_.shoplist)) return false;
		if (hunternum != _o_.hunternum) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += time;
		_h_ += shoplist.hashCode();
		_h_ += hunternum;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(time);
		_sb_.append(",");
		_sb_.append(shoplist);
		_sb_.append(",");
		_sb_.append(hunternum);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("time"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("shoplist"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("hunternum"));
		return lb;
	}

	private class Const implements xbean.bossshop {
		bossshop nThis() {
			return bossshop.this;
		}

		@Override
		public xbean.bossshop copy() {
			return bossshop.this.copy();
		}

		@Override
		public xbean.bossshop toData() {
			return bossshop.this.toData();
		}

		public xbean.bossshop toBean() {
			return bossshop.this.toBean();
		}

		@Override
		public xbean.bossshop toDataIf() {
			return bossshop.this.toDataIf();
		}

		public xbean.bossshop toBeanIf() {
			return bossshop.this.toBeanIf();
		}

		@Override
		public long getTime() { // 刷新时间
			_xdb_verify_unsafe_();
			return time;
		}

		@Override
		public java.util.List<Integer> getShoplist() { // 今天可买的物品表
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(shoplist);
		}

		public java.util.List<Integer> getShoplistAsData() { // 今天可买的物品表
			_xdb_verify_unsafe_();
			java.util.List<Integer> shoplist;
			bossshop _o_ = bossshop.this;
		shoplist = new java.util.LinkedList<Integer>();
		shoplist.addAll(_o_.shoplist);
			return shoplist;
		}

		@Override
		public int getHunternum() { // 今日猎人集市累计兑换次数
			_xdb_verify_unsafe_();
			return hunternum;
		}

		@Override
		public void setTime(long _v_) { // 刷新时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHunternum(int _v_) { // 今日猎人集市累计兑换次数
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
			return bossshop.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return bossshop.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return bossshop.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return bossshop.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return bossshop.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return bossshop.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return bossshop.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return bossshop.this.hashCode();
		}

		@Override
		public String toString() {
			return bossshop.this.toString();
		}

	}

	public static final class Data implements xbean.bossshop {
		private long time; // 刷新时间
		private java.util.LinkedList<Integer> shoplist; // 今天可买的物品表
		private int hunternum; // 今日猎人集市累计兑换次数

		public Data() {
			shoplist = new java.util.LinkedList<Integer>();
		}

		Data(xbean.bossshop _o1_) {
			if (_o1_ instanceof bossshop) assign((bossshop)_o1_);
			else if (_o1_ instanceof bossshop.Data) assign((bossshop.Data)_o1_);
			else if (_o1_ instanceof bossshop.Const) assign(((bossshop.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(bossshop _o_) {
			time = _o_.time;
			shoplist = new java.util.LinkedList<Integer>();
			shoplist.addAll(_o_.shoplist);
			hunternum = _o_.hunternum;
		}

		private void assign(bossshop.Data _o_) {
			time = _o_.time;
			shoplist = new java.util.LinkedList<Integer>();
			shoplist.addAll(_o_.shoplist);
			hunternum = _o_.hunternum;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(time);
			_os_.compact_uint32(shoplist.size());
			for (Integer _v_ : shoplist) {
				_os_.marshal(_v_);
			}
			_os_.marshal(hunternum);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			time = _os_.unmarshal_long();
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				int _v_ = 0;
				_v_ = _os_.unmarshal_int();
				shoplist.add(_v_);
			}
			hunternum = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.bossshop copy() {
			return new Data(this);
		}

		@Override
		public xbean.bossshop toData() {
			return new Data(this);
		}

		public xbean.bossshop toBean() {
			return new bossshop(this, null, null);
		}

		@Override
		public xbean.bossshop toDataIf() {
			return this;
		}

		public xbean.bossshop toBeanIf() {
			return new bossshop(this, null, null);
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
		public long getTime() { // 刷新时间
			return time;
		}

		@Override
		public java.util.List<Integer> getShoplist() { // 今天可买的物品表
			return shoplist;
		}

		@Override
		public java.util.List<Integer> getShoplistAsData() { // 今天可买的物品表
			return shoplist;
		}

		@Override
		public int getHunternum() { // 今日猎人集市累计兑换次数
			return hunternum;
		}

		@Override
		public void setTime(long _v_) { // 刷新时间
			time = _v_;
		}

		@Override
		public void setHunternum(int _v_) { // 今日猎人集市累计兑换次数
			hunternum = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof bossshop.Data)) return false;
			bossshop.Data _o_ = (bossshop.Data) _o1_;
			if (time != _o_.time) return false;
			if (!shoplist.equals(_o_.shoplist)) return false;
			if (hunternum != _o_.hunternum) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += time;
			_h_ += shoplist.hashCode();
			_h_ += hunternum;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(time);
			_sb_.append(",");
			_sb_.append(shoplist);
			_sb_.append(",");
			_sb_.append(hunternum);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
