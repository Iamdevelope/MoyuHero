
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class NewShop extends xdb.XBean implements xbean.NewShop {
	private int itemid; // 77表的道具ID
	private int costtype; // 消耗资源
	private int price; // 价格
	private int num; // 数量
	private int isbuy; // 0未购买，1为已购买

	NewShop(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
	}

	public NewShop() {
		this(0, null, null);
	}

	public NewShop(NewShop _o_) {
		this(_o_, null, null);
	}

	NewShop(xbean.NewShop _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof NewShop) assign((NewShop)_o1_);
		else if (_o1_ instanceof NewShop.Data) assign((NewShop.Data)_o1_);
		else if (_o1_ instanceof NewShop.Const) assign(((NewShop.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(NewShop _o_) {
		_o_._xdb_verify_unsafe_();
		itemid = _o_.itemid;
		costtype = _o_.costtype;
		price = _o_.price;
		num = _o_.num;
		isbuy = _o_.isbuy;
	}

	private void assign(NewShop.Data _o_) {
		itemid = _o_.itemid;
		costtype = _o_.costtype;
		price = _o_.price;
		num = _o_.num;
		isbuy = _o_.isbuy;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(itemid);
		_os_.marshal(costtype);
		_os_.marshal(price);
		_os_.marshal(num);
		_os_.marshal(isbuy);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		itemid = _os_.unmarshal_int();
		costtype = _os_.unmarshal_int();
		price = _os_.unmarshal_int();
		num = _os_.unmarshal_int();
		isbuy = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.NewShop copy() {
		_xdb_verify_unsafe_();
		return new NewShop(this);
	}

	@Override
	public xbean.NewShop toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.NewShop toBean() {
		_xdb_verify_unsafe_();
		return new NewShop(this); // same as copy()
	}

	@Override
	public xbean.NewShop toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.NewShop toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getItemid() { // 77表的道具ID
		_xdb_verify_unsafe_();
		return itemid;
	}

	@Override
	public int getCosttype() { // 消耗资源
		_xdb_verify_unsafe_();
		return costtype;
	}

	@Override
	public int getPrice() { // 价格
		_xdb_verify_unsafe_();
		return price;
	}

	@Override
	public int getNum() { // 数量
		_xdb_verify_unsafe_();
		return num;
	}

	@Override
	public int getIsbuy() { // 0未购买，1为已购买
		_xdb_verify_unsafe_();
		return isbuy;
	}

	@Override
	public void setItemid(int _v_) { // 77表的道具ID
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "itemid") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, itemid) {
					public void rollback() { itemid = _xdb_saved; }
				};}});
		itemid = _v_;
	}

	@Override
	public void setCosttype(int _v_) { // 消耗资源
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "costtype") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, costtype) {
					public void rollback() { costtype = _xdb_saved; }
				};}});
		costtype = _v_;
	}

	@Override
	public void setPrice(int _v_) { // 价格
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "price") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, price) {
					public void rollback() { price = _xdb_saved; }
				};}});
		price = _v_;
	}

	@Override
	public void setNum(int _v_) { // 数量
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "num") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, num) {
					public void rollback() { num = _xdb_saved; }
				};}});
		num = _v_;
	}

	@Override
	public void setIsbuy(int _v_) { // 0未购买，1为已购买
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "isbuy") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, isbuy) {
					public void rollback() { isbuy = _xdb_saved; }
				};}});
		isbuy = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		NewShop _o_ = null;
		if ( _o1_ instanceof NewShop ) _o_ = (NewShop)_o1_;
		else if ( _o1_ instanceof NewShop.Const ) _o_ = ((NewShop.Const)_o1_).nThis();
		else return false;
		if (itemid != _o_.itemid) return false;
		if (costtype != _o_.costtype) return false;
		if (price != _o_.price) return false;
		if (num != _o_.num) return false;
		if (isbuy != _o_.isbuy) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += itemid;
		_h_ += costtype;
		_h_ += price;
		_h_ += num;
		_h_ += isbuy;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(itemid);
		_sb_.append(",");
		_sb_.append(costtype);
		_sb_.append(",");
		_sb_.append(price);
		_sb_.append(",");
		_sb_.append(num);
		_sb_.append(",");
		_sb_.append(isbuy);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("itemid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("costtype"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("price"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("num"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("isbuy"));
		return lb;
	}

	private class Const implements xbean.NewShop {
		NewShop nThis() {
			return NewShop.this;
		}

		@Override
		public xbean.NewShop copy() {
			return NewShop.this.copy();
		}

		@Override
		public xbean.NewShop toData() {
			return NewShop.this.toData();
		}

		public xbean.NewShop toBean() {
			return NewShop.this.toBean();
		}

		@Override
		public xbean.NewShop toDataIf() {
			return NewShop.this.toDataIf();
		}

		public xbean.NewShop toBeanIf() {
			return NewShop.this.toBeanIf();
		}

		@Override
		public int getItemid() { // 77表的道具ID
			_xdb_verify_unsafe_();
			return itemid;
		}

		@Override
		public int getCosttype() { // 消耗资源
			_xdb_verify_unsafe_();
			return costtype;
		}

		@Override
		public int getPrice() { // 价格
			_xdb_verify_unsafe_();
			return price;
		}

		@Override
		public int getNum() { // 数量
			_xdb_verify_unsafe_();
			return num;
		}

		@Override
		public int getIsbuy() { // 0未购买，1为已购买
			_xdb_verify_unsafe_();
			return isbuy;
		}

		@Override
		public void setItemid(int _v_) { // 77表的道具ID
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setCosttype(int _v_) { // 消耗资源
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setPrice(int _v_) { // 价格
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setNum(int _v_) { // 数量
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setIsbuy(int _v_) { // 0未购买，1为已购买
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
			return NewShop.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return NewShop.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return NewShop.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return NewShop.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return NewShop.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return NewShop.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return NewShop.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return NewShop.this.hashCode();
		}

		@Override
		public String toString() {
			return NewShop.this.toString();
		}

	}

	public static final class Data implements xbean.NewShop {
		private int itemid; // 77表的道具ID
		private int costtype; // 消耗资源
		private int price; // 价格
		private int num; // 数量
		private int isbuy; // 0未购买，1为已购买

		public Data() {
		}

		Data(xbean.NewShop _o1_) {
			if (_o1_ instanceof NewShop) assign((NewShop)_o1_);
			else if (_o1_ instanceof NewShop.Data) assign((NewShop.Data)_o1_);
			else if (_o1_ instanceof NewShop.Const) assign(((NewShop.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(NewShop _o_) {
			itemid = _o_.itemid;
			costtype = _o_.costtype;
			price = _o_.price;
			num = _o_.num;
			isbuy = _o_.isbuy;
		}

		private void assign(NewShop.Data _o_) {
			itemid = _o_.itemid;
			costtype = _o_.costtype;
			price = _o_.price;
			num = _o_.num;
			isbuy = _o_.isbuy;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(itemid);
			_os_.marshal(costtype);
			_os_.marshal(price);
			_os_.marshal(num);
			_os_.marshal(isbuy);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			itemid = _os_.unmarshal_int();
			costtype = _os_.unmarshal_int();
			price = _os_.unmarshal_int();
			num = _os_.unmarshal_int();
			isbuy = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.NewShop copy() {
			return new Data(this);
		}

		@Override
		public xbean.NewShop toData() {
			return new Data(this);
		}

		public xbean.NewShop toBean() {
			return new NewShop(this, null, null);
		}

		@Override
		public xbean.NewShop toDataIf() {
			return this;
		}

		public xbean.NewShop toBeanIf() {
			return new NewShop(this, null, null);
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
		public int getItemid() { // 77表的道具ID
			return itemid;
		}

		@Override
		public int getCosttype() { // 消耗资源
			return costtype;
		}

		@Override
		public int getPrice() { // 价格
			return price;
		}

		@Override
		public int getNum() { // 数量
			return num;
		}

		@Override
		public int getIsbuy() { // 0未购买，1为已购买
			return isbuy;
		}

		@Override
		public void setItemid(int _v_) { // 77表的道具ID
			itemid = _v_;
		}

		@Override
		public void setCosttype(int _v_) { // 消耗资源
			costtype = _v_;
		}

		@Override
		public void setPrice(int _v_) { // 价格
			price = _v_;
		}

		@Override
		public void setNum(int _v_) { // 数量
			num = _v_;
		}

		@Override
		public void setIsbuy(int _v_) { // 0未购买，1为已购买
			isbuy = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof NewShop.Data)) return false;
			NewShop.Data _o_ = (NewShop.Data) _o1_;
			if (itemid != _o_.itemid) return false;
			if (costtype != _o_.costtype) return false;
			if (price != _o_.price) return false;
			if (num != _o_.num) return false;
			if (isbuy != _o_.isbuy) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += itemid;
			_h_ += costtype;
			_h_ += price;
			_h_ += num;
			_h_ += isbuy;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(itemid);
			_sb_.append(",");
			_sb_.append(costtype);
			_sb_.append(",");
			_sb_.append(price);
			_sb_.append(",");
			_sb_.append(num);
			_sb_.append(",");
			_sb_.append(isbuy);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
