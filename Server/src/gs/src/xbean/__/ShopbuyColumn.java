
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class ShopbuyColumn extends xdb.XBean implements xbean.ShopbuyColumn {
	private java.util.HashMap<Integer, xbean.Shopbuy> shopbuys; // 

	ShopbuyColumn(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		shopbuys = new java.util.HashMap<Integer, xbean.Shopbuy>();
	}

	public ShopbuyColumn() {
		this(0, null, null);
	}

	public ShopbuyColumn(ShopbuyColumn _o_) {
		this(_o_, null, null);
	}

	ShopbuyColumn(xbean.ShopbuyColumn _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof ShopbuyColumn) assign((ShopbuyColumn)_o1_);
		else if (_o1_ instanceof ShopbuyColumn.Data) assign((ShopbuyColumn.Data)_o1_);
		else if (_o1_ instanceof ShopbuyColumn.Const) assign(((ShopbuyColumn.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(ShopbuyColumn _o_) {
		_o_._xdb_verify_unsafe_();
		shopbuys = new java.util.HashMap<Integer, xbean.Shopbuy>();
		for (java.util.Map.Entry<Integer, xbean.Shopbuy> _e_ : _o_.shopbuys.entrySet())
			shopbuys.put(_e_.getKey(), new Shopbuy(_e_.getValue(), this, "shopbuys"));
	}

	private void assign(ShopbuyColumn.Data _o_) {
		shopbuys = new java.util.HashMap<Integer, xbean.Shopbuy>();
		for (java.util.Map.Entry<Integer, xbean.Shopbuy> _e_ : _o_.shopbuys.entrySet())
			shopbuys.put(_e_.getKey(), new Shopbuy(_e_.getValue(), this, "shopbuys"));
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(shopbuys.size());
		for (java.util.Map.Entry<Integer, xbean.Shopbuy> _e_ : shopbuys.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_e_.getValue().marshal(_os_);
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				shopbuys = new java.util.HashMap<Integer, xbean.Shopbuy>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				xbean.Shopbuy _v_ = new Shopbuy(0, this, "shopbuys");
				_v_.unmarshal(_os_);
				shopbuys.put(_k_, _v_);
			}
		}
		return _os_;
	}

	@Override
	public xbean.ShopbuyColumn copy() {
		_xdb_verify_unsafe_();
		return new ShopbuyColumn(this);
	}

	@Override
	public xbean.ShopbuyColumn toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.ShopbuyColumn toBean() {
		_xdb_verify_unsafe_();
		return new ShopbuyColumn(this); // same as copy()
	}

	@Override
	public xbean.ShopbuyColumn toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.ShopbuyColumn toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.Map<Integer, xbean.Shopbuy> getShopbuys() { // 
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "shopbuys"), shopbuys);
	}

	@Override
	public java.util.Map<Integer, xbean.Shopbuy> getShopbuysAsData() { // 
		_xdb_verify_unsafe_();
		java.util.Map<Integer, xbean.Shopbuy> shopbuys;
		ShopbuyColumn _o_ = this;
		shopbuys = new java.util.HashMap<Integer, xbean.Shopbuy>();
		for (java.util.Map.Entry<Integer, xbean.Shopbuy> _e_ : _o_.shopbuys.entrySet())
			shopbuys.put(_e_.getKey(), new Shopbuy.Data(_e_.getValue()));
		return shopbuys;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		ShopbuyColumn _o_ = null;
		if ( _o1_ instanceof ShopbuyColumn ) _o_ = (ShopbuyColumn)_o1_;
		else if ( _o1_ instanceof ShopbuyColumn.Const ) _o_ = ((ShopbuyColumn.Const)_o1_).nThis();
		else return false;
		if (!shopbuys.equals(_o_.shopbuys)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += shopbuys.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(shopbuys);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableMap().setVarName("shopbuys"));
		return lb;
	}

	private class Const implements xbean.ShopbuyColumn {
		ShopbuyColumn nThis() {
			return ShopbuyColumn.this;
		}

		@Override
		public xbean.ShopbuyColumn copy() {
			return ShopbuyColumn.this.copy();
		}

		@Override
		public xbean.ShopbuyColumn toData() {
			return ShopbuyColumn.this.toData();
		}

		public xbean.ShopbuyColumn toBean() {
			return ShopbuyColumn.this.toBean();
		}

		@Override
		public xbean.ShopbuyColumn toDataIf() {
			return ShopbuyColumn.this.toDataIf();
		}

		public xbean.ShopbuyColumn toBeanIf() {
			return ShopbuyColumn.this.toBeanIf();
		}

		@Override
		public java.util.Map<Integer, xbean.Shopbuy> getShopbuys() { // 
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(shopbuys);
		}

		@Override
		public java.util.Map<Integer, xbean.Shopbuy> getShopbuysAsData() { // 
			_xdb_verify_unsafe_();
			java.util.Map<Integer, xbean.Shopbuy> shopbuys;
			ShopbuyColumn _o_ = ShopbuyColumn.this;
			shopbuys = new java.util.HashMap<Integer, xbean.Shopbuy>();
			for (java.util.Map.Entry<Integer, xbean.Shopbuy> _e_ : _o_.shopbuys.entrySet())
				shopbuys.put(_e_.getKey(), new Shopbuy.Data(_e_.getValue()));
			return shopbuys;
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
			return ShopbuyColumn.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return ShopbuyColumn.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return ShopbuyColumn.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return ShopbuyColumn.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return ShopbuyColumn.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return ShopbuyColumn.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return ShopbuyColumn.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return ShopbuyColumn.this.hashCode();
		}

		@Override
		public String toString() {
			return ShopbuyColumn.this.toString();
		}

	}

	public static final class Data implements xbean.ShopbuyColumn {
		private java.util.HashMap<Integer, xbean.Shopbuy> shopbuys; // 

		public Data() {
			shopbuys = new java.util.HashMap<Integer, xbean.Shopbuy>();
		}

		Data(xbean.ShopbuyColumn _o1_) {
			if (_o1_ instanceof ShopbuyColumn) assign((ShopbuyColumn)_o1_);
			else if (_o1_ instanceof ShopbuyColumn.Data) assign((ShopbuyColumn.Data)_o1_);
			else if (_o1_ instanceof ShopbuyColumn.Const) assign(((ShopbuyColumn.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(ShopbuyColumn _o_) {
			shopbuys = new java.util.HashMap<Integer, xbean.Shopbuy>();
			for (java.util.Map.Entry<Integer, xbean.Shopbuy> _e_ : _o_.shopbuys.entrySet())
				shopbuys.put(_e_.getKey(), new Shopbuy.Data(_e_.getValue()));
		}

		private void assign(ShopbuyColumn.Data _o_) {
			shopbuys = new java.util.HashMap<Integer, xbean.Shopbuy>();
			for (java.util.Map.Entry<Integer, xbean.Shopbuy> _e_ : _o_.shopbuys.entrySet())
				shopbuys.put(_e_.getKey(), new Shopbuy.Data(_e_.getValue()));
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(shopbuys.size());
			for (java.util.Map.Entry<Integer, xbean.Shopbuy> _e_ : shopbuys.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_e_.getValue().marshal(_os_);
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					shopbuys = new java.util.HashMap<Integer, xbean.Shopbuy>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					xbean.Shopbuy _v_ = xbean.Pod.newShopbuyData();
					_v_.unmarshal(_os_);
					shopbuys.put(_k_, _v_);
				}
			}
			return _os_;
		}

		@Override
		public xbean.ShopbuyColumn copy() {
			return new Data(this);
		}

		@Override
		public xbean.ShopbuyColumn toData() {
			return new Data(this);
		}

		public xbean.ShopbuyColumn toBean() {
			return new ShopbuyColumn(this, null, null);
		}

		@Override
		public xbean.ShopbuyColumn toDataIf() {
			return this;
		}

		public xbean.ShopbuyColumn toBeanIf() {
			return new ShopbuyColumn(this, null, null);
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
		public java.util.Map<Integer, xbean.Shopbuy> getShopbuys() { // 
			return shopbuys;
		}

		@Override
		public java.util.Map<Integer, xbean.Shopbuy> getShopbuysAsData() { // 
			return shopbuys;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof ShopbuyColumn.Data)) return false;
			ShopbuyColumn.Data _o_ = (ShopbuyColumn.Data) _o1_;
			if (!shopbuys.equals(_o_.shopbuys)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += shopbuys.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(shopbuys);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
