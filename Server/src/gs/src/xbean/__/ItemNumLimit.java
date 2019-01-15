
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class ItemNumLimit extends xdb.XBean implements xbean.ItemNumLimit {
	private java.util.HashMap<Integer, Integer> itemnums; // 每天使用道具次数s
	private long time; // 最后更新时间，清除用

	ItemNumLimit(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		itemnums = new java.util.HashMap<Integer, Integer>();
	}

	public ItemNumLimit() {
		this(0, null, null);
	}

	public ItemNumLimit(ItemNumLimit _o_) {
		this(_o_, null, null);
	}

	ItemNumLimit(xbean.ItemNumLimit _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof ItemNumLimit) assign((ItemNumLimit)_o1_);
		else if (_o1_ instanceof ItemNumLimit.Data) assign((ItemNumLimit.Data)_o1_);
		else if (_o1_ instanceof ItemNumLimit.Const) assign(((ItemNumLimit.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(ItemNumLimit _o_) {
		_o_._xdb_verify_unsafe_();
		itemnums = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.itemnums.entrySet())
			itemnums.put(_e_.getKey(), _e_.getValue());
		time = _o_.time;
	}

	private void assign(ItemNumLimit.Data _o_) {
		itemnums = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.itemnums.entrySet())
			itemnums.put(_e_.getKey(), _e_.getValue());
		time = _o_.time;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(itemnums.size());
		for (java.util.Map.Entry<Integer, Integer> _e_ : itemnums.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.marshal(time);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				itemnums = new java.util.HashMap<Integer, Integer>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				int _v_ = 0;
				_v_ = _os_.unmarshal_int();
				itemnums.put(_k_, _v_);
			}
		}
		time = _os_.unmarshal_long();
		return _os_;
	}

	@Override
	public xbean.ItemNumLimit copy() {
		_xdb_verify_unsafe_();
		return new ItemNumLimit(this);
	}

	@Override
	public xbean.ItemNumLimit toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.ItemNumLimit toBean() {
		_xdb_verify_unsafe_();
		return new ItemNumLimit(this); // same as copy()
	}

	@Override
	public xbean.ItemNumLimit toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.ItemNumLimit toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.Map<Integer, Integer> getItemnums() { // 每天使用道具次数s
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "itemnums"), itemnums);
	}

	@Override
	public java.util.Map<Integer, Integer> getItemnumsAsData() { // 每天使用道具次数s
		_xdb_verify_unsafe_();
		java.util.Map<Integer, Integer> itemnums;
		ItemNumLimit _o_ = this;
		itemnums = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.itemnums.entrySet())
			itemnums.put(_e_.getKey(), _e_.getValue());
		return itemnums;
	}

	@Override
	public long getTime() { // 最后更新时间，清除用
		_xdb_verify_unsafe_();
		return time;
	}

	@Override
	public void setTime(long _v_) { // 最后更新时间，清除用
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "time") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, time) {
					public void rollback() { time = _xdb_saved; }
				};}});
		time = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		ItemNumLimit _o_ = null;
		if ( _o1_ instanceof ItemNumLimit ) _o_ = (ItemNumLimit)_o1_;
		else if ( _o1_ instanceof ItemNumLimit.Const ) _o_ = ((ItemNumLimit.Const)_o1_).nThis();
		else return false;
		if (!itemnums.equals(_o_.itemnums)) return false;
		if (time != _o_.time) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += itemnums.hashCode();
		_h_ += time;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(itemnums);
		_sb_.append(",");
		_sb_.append(time);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableMap().setVarName("itemnums"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("time"));
		return lb;
	}

	private class Const implements xbean.ItemNumLimit {
		ItemNumLimit nThis() {
			return ItemNumLimit.this;
		}

		@Override
		public xbean.ItemNumLimit copy() {
			return ItemNumLimit.this.copy();
		}

		@Override
		public xbean.ItemNumLimit toData() {
			return ItemNumLimit.this.toData();
		}

		public xbean.ItemNumLimit toBean() {
			return ItemNumLimit.this.toBean();
		}

		@Override
		public xbean.ItemNumLimit toDataIf() {
			return ItemNumLimit.this.toDataIf();
		}

		public xbean.ItemNumLimit toBeanIf() {
			return ItemNumLimit.this.toBeanIf();
		}

		@Override
		public java.util.Map<Integer, Integer> getItemnums() { // 每天使用道具次数s
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(itemnums);
		}

		@Override
		public java.util.Map<Integer, Integer> getItemnumsAsData() { // 每天使用道具次数s
			_xdb_verify_unsafe_();
			java.util.Map<Integer, Integer> itemnums;
			ItemNumLimit _o_ = ItemNumLimit.this;
			itemnums = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.itemnums.entrySet())
				itemnums.put(_e_.getKey(), _e_.getValue());
			return itemnums;
		}

		@Override
		public long getTime() { // 最后更新时间，清除用
			_xdb_verify_unsafe_();
			return time;
		}

		@Override
		public void setTime(long _v_) { // 最后更新时间，清除用
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
			return ItemNumLimit.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return ItemNumLimit.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return ItemNumLimit.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return ItemNumLimit.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return ItemNumLimit.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return ItemNumLimit.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return ItemNumLimit.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return ItemNumLimit.this.hashCode();
		}

		@Override
		public String toString() {
			return ItemNumLimit.this.toString();
		}

	}

	public static final class Data implements xbean.ItemNumLimit {
		private java.util.HashMap<Integer, Integer> itemnums; // 每天使用道具次数s
		private long time; // 最后更新时间，清除用

		public Data() {
			itemnums = new java.util.HashMap<Integer, Integer>();
		}

		Data(xbean.ItemNumLimit _o1_) {
			if (_o1_ instanceof ItemNumLimit) assign((ItemNumLimit)_o1_);
			else if (_o1_ instanceof ItemNumLimit.Data) assign((ItemNumLimit.Data)_o1_);
			else if (_o1_ instanceof ItemNumLimit.Const) assign(((ItemNumLimit.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(ItemNumLimit _o_) {
			itemnums = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.itemnums.entrySet())
				itemnums.put(_e_.getKey(), _e_.getValue());
			time = _o_.time;
		}

		private void assign(ItemNumLimit.Data _o_) {
			itemnums = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.itemnums.entrySet())
				itemnums.put(_e_.getKey(), _e_.getValue());
			time = _o_.time;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(itemnums.size());
			for (java.util.Map.Entry<Integer, Integer> _e_ : itemnums.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_os_.marshal(_e_.getValue());
			}
			_os_.marshal(time);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					itemnums = new java.util.HashMap<Integer, Integer>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					int _v_ = 0;
					_v_ = _os_.unmarshal_int();
					itemnums.put(_k_, _v_);
				}
			}
			time = _os_.unmarshal_long();
			return _os_;
		}

		@Override
		public xbean.ItemNumLimit copy() {
			return new Data(this);
		}

		@Override
		public xbean.ItemNumLimit toData() {
			return new Data(this);
		}

		public xbean.ItemNumLimit toBean() {
			return new ItemNumLimit(this, null, null);
		}

		@Override
		public xbean.ItemNumLimit toDataIf() {
			return this;
		}

		public xbean.ItemNumLimit toBeanIf() {
			return new ItemNumLimit(this, null, null);
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
		public java.util.Map<Integer, Integer> getItemnums() { // 每天使用道具次数s
			return itemnums;
		}

		@Override
		public java.util.Map<Integer, Integer> getItemnumsAsData() { // 每天使用道具次数s
			return itemnums;
		}

		@Override
		public long getTime() { // 最后更新时间，清除用
			return time;
		}

		@Override
		public void setTime(long _v_) { // 最后更新时间，清除用
			time = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof ItemNumLimit.Data)) return false;
			ItemNumLimit.Data _o_ = (ItemNumLimit.Data) _o1_;
			if (!itemnums.equals(_o_.itemnums)) return false;
			if (time != _o_.time) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += itemnums.hashCode();
			_h_ += time;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(itemnums);
			_sb_.append(",");
			_sb_.append(time);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
