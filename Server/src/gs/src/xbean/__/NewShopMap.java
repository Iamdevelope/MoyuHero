
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class NewShopMap extends xdb.XBean implements xbean.NewShopMap {
	private java.util.HashMap<Integer, xbean.NewShopList> shopmap; // 整个商城map，key为76表的序列号

	NewShopMap(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		shopmap = new java.util.HashMap<Integer, xbean.NewShopList>();
	}

	public NewShopMap() {
		this(0, null, null);
	}

	public NewShopMap(NewShopMap _o_) {
		this(_o_, null, null);
	}

	NewShopMap(xbean.NewShopMap _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof NewShopMap) assign((NewShopMap)_o1_);
		else if (_o1_ instanceof NewShopMap.Data) assign((NewShopMap.Data)_o1_);
		else if (_o1_ instanceof NewShopMap.Const) assign(((NewShopMap.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(NewShopMap _o_) {
		_o_._xdb_verify_unsafe_();
		shopmap = new java.util.HashMap<Integer, xbean.NewShopList>();
		for (java.util.Map.Entry<Integer, xbean.NewShopList> _e_ : _o_.shopmap.entrySet())
			shopmap.put(_e_.getKey(), new NewShopList(_e_.getValue(), this, "shopmap"));
	}

	private void assign(NewShopMap.Data _o_) {
		shopmap = new java.util.HashMap<Integer, xbean.NewShopList>();
		for (java.util.Map.Entry<Integer, xbean.NewShopList> _e_ : _o_.shopmap.entrySet())
			shopmap.put(_e_.getKey(), new NewShopList(_e_.getValue(), this, "shopmap"));
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(shopmap.size());
		for (java.util.Map.Entry<Integer, xbean.NewShopList> _e_ : shopmap.entrySet())
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
				shopmap = new java.util.HashMap<Integer, xbean.NewShopList>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				xbean.NewShopList _v_ = new NewShopList(0, this, "shopmap");
				_v_.unmarshal(_os_);
				shopmap.put(_k_, _v_);
			}
		}
		return _os_;
	}

	@Override
	public xbean.NewShopMap copy() {
		_xdb_verify_unsafe_();
		return new NewShopMap(this);
	}

	@Override
	public xbean.NewShopMap toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.NewShopMap toBean() {
		_xdb_verify_unsafe_();
		return new NewShopMap(this); // same as copy()
	}

	@Override
	public xbean.NewShopMap toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.NewShopMap toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.Map<Integer, xbean.NewShopList> getShopmap() { // 整个商城map，key为76表的序列号
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "shopmap"), shopmap);
	}

	@Override
	public java.util.Map<Integer, xbean.NewShopList> getShopmapAsData() { // 整个商城map，key为76表的序列号
		_xdb_verify_unsafe_();
		java.util.Map<Integer, xbean.NewShopList> shopmap;
		NewShopMap _o_ = this;
		shopmap = new java.util.HashMap<Integer, xbean.NewShopList>();
		for (java.util.Map.Entry<Integer, xbean.NewShopList> _e_ : _o_.shopmap.entrySet())
			shopmap.put(_e_.getKey(), new NewShopList.Data(_e_.getValue()));
		return shopmap;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		NewShopMap _o_ = null;
		if ( _o1_ instanceof NewShopMap ) _o_ = (NewShopMap)_o1_;
		else if ( _o1_ instanceof NewShopMap.Const ) _o_ = ((NewShopMap.Const)_o1_).nThis();
		else return false;
		if (!shopmap.equals(_o_.shopmap)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += shopmap.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(shopmap);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableMap().setVarName("shopmap"));
		return lb;
	}

	private class Const implements xbean.NewShopMap {
		NewShopMap nThis() {
			return NewShopMap.this;
		}

		@Override
		public xbean.NewShopMap copy() {
			return NewShopMap.this.copy();
		}

		@Override
		public xbean.NewShopMap toData() {
			return NewShopMap.this.toData();
		}

		public xbean.NewShopMap toBean() {
			return NewShopMap.this.toBean();
		}

		@Override
		public xbean.NewShopMap toDataIf() {
			return NewShopMap.this.toDataIf();
		}

		public xbean.NewShopMap toBeanIf() {
			return NewShopMap.this.toBeanIf();
		}

		@Override
		public java.util.Map<Integer, xbean.NewShopList> getShopmap() { // 整个商城map，key为76表的序列号
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(shopmap);
		}

		@Override
		public java.util.Map<Integer, xbean.NewShopList> getShopmapAsData() { // 整个商城map，key为76表的序列号
			_xdb_verify_unsafe_();
			java.util.Map<Integer, xbean.NewShopList> shopmap;
			NewShopMap _o_ = NewShopMap.this;
			shopmap = new java.util.HashMap<Integer, xbean.NewShopList>();
			for (java.util.Map.Entry<Integer, xbean.NewShopList> _e_ : _o_.shopmap.entrySet())
				shopmap.put(_e_.getKey(), new NewShopList.Data(_e_.getValue()));
			return shopmap;
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
			return NewShopMap.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return NewShopMap.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return NewShopMap.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return NewShopMap.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return NewShopMap.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return NewShopMap.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return NewShopMap.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return NewShopMap.this.hashCode();
		}

		@Override
		public String toString() {
			return NewShopMap.this.toString();
		}

	}

	public static final class Data implements xbean.NewShopMap {
		private java.util.HashMap<Integer, xbean.NewShopList> shopmap; // 整个商城map，key为76表的序列号

		public Data() {
			shopmap = new java.util.HashMap<Integer, xbean.NewShopList>();
		}

		Data(xbean.NewShopMap _o1_) {
			if (_o1_ instanceof NewShopMap) assign((NewShopMap)_o1_);
			else if (_o1_ instanceof NewShopMap.Data) assign((NewShopMap.Data)_o1_);
			else if (_o1_ instanceof NewShopMap.Const) assign(((NewShopMap.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(NewShopMap _o_) {
			shopmap = new java.util.HashMap<Integer, xbean.NewShopList>();
			for (java.util.Map.Entry<Integer, xbean.NewShopList> _e_ : _o_.shopmap.entrySet())
				shopmap.put(_e_.getKey(), new NewShopList.Data(_e_.getValue()));
		}

		private void assign(NewShopMap.Data _o_) {
			shopmap = new java.util.HashMap<Integer, xbean.NewShopList>();
			for (java.util.Map.Entry<Integer, xbean.NewShopList> _e_ : _o_.shopmap.entrySet())
				shopmap.put(_e_.getKey(), new NewShopList.Data(_e_.getValue()));
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(shopmap.size());
			for (java.util.Map.Entry<Integer, xbean.NewShopList> _e_ : shopmap.entrySet())
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
					shopmap = new java.util.HashMap<Integer, xbean.NewShopList>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					xbean.NewShopList _v_ = xbean.Pod.newNewShopListData();
					_v_.unmarshal(_os_);
					shopmap.put(_k_, _v_);
				}
			}
			return _os_;
		}

		@Override
		public xbean.NewShopMap copy() {
			return new Data(this);
		}

		@Override
		public xbean.NewShopMap toData() {
			return new Data(this);
		}

		public xbean.NewShopMap toBean() {
			return new NewShopMap(this, null, null);
		}

		@Override
		public xbean.NewShopMap toDataIf() {
			return this;
		}

		public xbean.NewShopMap toBeanIf() {
			return new NewShopMap(this, null, null);
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
		public java.util.Map<Integer, xbean.NewShopList> getShopmap() { // 整个商城map，key为76表的序列号
			return shopmap;
		}

		@Override
		public java.util.Map<Integer, xbean.NewShopList> getShopmapAsData() { // 整个商城map，key为76表的序列号
			return shopmap;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof NewShopMap.Data)) return false;
			NewShopMap.Data _o_ = (NewShopMap.Data) _o1_;
			if (!shopmap.equals(_o_.shopmap)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += shopmap.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(shopmap);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
