
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class Item extends xdb.XBean implements xbean.Item {
	private int id; // 物品编号
	private int flags; // 标志，叠加的时候，flags 也 OR 叠加
	private int position; // 包裹属性，位置。从0开始编号
	private int number; // 数量
	private java.util.HashMap<Integer, Integer> numbermap; // 数量
	private long uniqueid; // 物品的唯一id

	Item(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		position = -1;
		numbermap = new java.util.HashMap<Integer, Integer>();
	}

	public Item() {
		this(0, null, null);
	}

	public Item(Item _o_) {
		this(_o_, null, null);
	}

	Item(xbean.Item _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof Item) assign((Item)_o1_);
		else if (_o1_ instanceof Item.Data) assign((Item.Data)_o1_);
		else if (_o1_ instanceof Item.Const) assign(((Item.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(Item _o_) {
		_o_._xdb_verify_unsafe_();
		id = _o_.id;
		flags = _o_.flags;
		position = _o_.position;
		number = _o_.number;
		numbermap = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.numbermap.entrySet())
			numbermap.put(_e_.getKey(), _e_.getValue());
		uniqueid = _o_.uniqueid;
	}

	private void assign(Item.Data _o_) {
		id = _o_.id;
		flags = _o_.flags;
		position = _o_.position;
		number = _o_.number;
		numbermap = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.numbermap.entrySet())
			numbermap.put(_e_.getKey(), _e_.getValue());
		uniqueid = _o_.uniqueid;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(id);
		_os_.marshal(flags);
		_os_.marshal(position);
		_os_.marshal(number);
		_os_.compact_uint32(numbermap.size());
		for (java.util.Map.Entry<Integer, Integer> _e_ : numbermap.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.marshal(uniqueid);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		id = _os_.unmarshal_int();
		flags = _os_.unmarshal_int();
		position = _os_.unmarshal_int();
		number = _os_.unmarshal_int();
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				numbermap = new java.util.HashMap<Integer, Integer>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				int _v_ = 0;
				_v_ = _os_.unmarshal_int();
				numbermap.put(_k_, _v_);
			}
		}
		uniqueid = _os_.unmarshal_long();
		return _os_;
	}

	@Override
	public xbean.Item copy() {
		_xdb_verify_unsafe_();
		return new Item(this);
	}

	@Override
	public xbean.Item toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.Item toBean() {
		_xdb_verify_unsafe_();
		return new Item(this); // same as copy()
	}

	@Override
	public xbean.Item toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.Item toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getId() { // 物品编号
		_xdb_verify_unsafe_();
		return id;
	}

	@Override
	public int getFlags() { // 标志，叠加的时候，flags 也 OR 叠加
		_xdb_verify_unsafe_();
		return flags;
	}

	@Override
	public int getPosition() { // 包裹属性，位置。从0开始编号
		_xdb_verify_unsafe_();
		return position;
	}

	@Override
	public int getNumber() { // 数量
		_xdb_verify_unsafe_();
		return number;
	}

	@Override
	public java.util.Map<Integer, Integer> getNumbermap() { // 数量
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "numbermap"), numbermap);
	}

	@Override
	public java.util.Map<Integer, Integer> getNumbermapAsData() { // 数量
		_xdb_verify_unsafe_();
		java.util.Map<Integer, Integer> numbermap;
		Item _o_ = this;
		numbermap = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.numbermap.entrySet())
			numbermap.put(_e_.getKey(), _e_.getValue());
		return numbermap;
	}

	@Override
	public long getUniqueid() { // 物品的唯一id
		_xdb_verify_unsafe_();
		return uniqueid;
	}

	@Override
	public void setId(int _v_) { // 物品编号
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "id") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, id) {
					public void rollback() { id = _xdb_saved; }
				};}});
		id = _v_;
	}

	@Override
	public void setFlags(int _v_) { // 标志，叠加的时候，flags 也 OR 叠加
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "flags") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, flags) {
					public void rollback() { flags = _xdb_saved; }
				};}});
		flags = _v_;
	}

	@Override
	public void setPosition(int _v_) { // 包裹属性，位置。从0开始编号
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "position") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, position) {
					public void rollback() { position = _xdb_saved; }
				};}});
		position = _v_;
	}

	@Override
	public void setNumber(int _v_) { // 数量
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "number") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, number) {
					public void rollback() { number = _xdb_saved; }
				};}});
		number = _v_;
	}

	@Override
	public void setUniqueid(long _v_) { // 物品的唯一id
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "uniqueid") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, uniqueid) {
					public void rollback() { uniqueid = _xdb_saved; }
				};}});
		uniqueid = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		Item _o_ = null;
		if ( _o1_ instanceof Item ) _o_ = (Item)_o1_;
		else if ( _o1_ instanceof Item.Const ) _o_ = ((Item.Const)_o1_).nThis();
		else return false;
		if (id != _o_.id) return false;
		if (flags != _o_.flags) return false;
		if (position != _o_.position) return false;
		if (number != _o_.number) return false;
		if (!numbermap.equals(_o_.numbermap)) return false;
		if (uniqueid != _o_.uniqueid) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += id;
		_h_ += flags;
		_h_ += position;
		_h_ += number;
		_h_ += numbermap.hashCode();
		_h_ += uniqueid;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(id);
		_sb_.append(",");
		_sb_.append(flags);
		_sb_.append(",");
		_sb_.append(position);
		_sb_.append(",");
		_sb_.append(number);
		_sb_.append(",");
		_sb_.append(numbermap);
		_sb_.append(",");
		_sb_.append(uniqueid);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("id"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("flags"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("position"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("number"));
		lb.add(new xdb.logs.ListenableMap().setVarName("numbermap"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("uniqueid"));
		return lb;
	}

	private class Const implements xbean.Item {
		Item nThis() {
			return Item.this;
		}

		@Override
		public xbean.Item copy() {
			return Item.this.copy();
		}

		@Override
		public xbean.Item toData() {
			return Item.this.toData();
		}

		public xbean.Item toBean() {
			return Item.this.toBean();
		}

		@Override
		public xbean.Item toDataIf() {
			return Item.this.toDataIf();
		}

		public xbean.Item toBeanIf() {
			return Item.this.toBeanIf();
		}

		@Override
		public int getId() { // 物品编号
			_xdb_verify_unsafe_();
			return id;
		}

		@Override
		public int getFlags() { // 标志，叠加的时候，flags 也 OR 叠加
			_xdb_verify_unsafe_();
			return flags;
		}

		@Override
		public int getPosition() { // 包裹属性，位置。从0开始编号
			_xdb_verify_unsafe_();
			return position;
		}

		@Override
		public int getNumber() { // 数量
			_xdb_verify_unsafe_();
			return number;
		}

		@Override
		public java.util.Map<Integer, Integer> getNumbermap() { // 数量
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(numbermap);
		}

		@Override
		public java.util.Map<Integer, Integer> getNumbermapAsData() { // 数量
			_xdb_verify_unsafe_();
			java.util.Map<Integer, Integer> numbermap;
			Item _o_ = Item.this;
			numbermap = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.numbermap.entrySet())
				numbermap.put(_e_.getKey(), _e_.getValue());
			return numbermap;
		}

		@Override
		public long getUniqueid() { // 物品的唯一id
			_xdb_verify_unsafe_();
			return uniqueid;
		}

		@Override
		public void setId(int _v_) { // 物品编号
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setFlags(int _v_) { // 标志，叠加的时候，flags 也 OR 叠加
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setPosition(int _v_) { // 包裹属性，位置。从0开始编号
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setNumber(int _v_) { // 数量
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setUniqueid(long _v_) { // 物品的唯一id
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
			return Item.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return Item.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return Item.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return Item.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return Item.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return Item.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return Item.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return Item.this.hashCode();
		}

		@Override
		public String toString() {
			return Item.this.toString();
		}

	}

	public static final class Data implements xbean.Item {
		private int id; // 物品编号
		private int flags; // 标志，叠加的时候，flags 也 OR 叠加
		private int position; // 包裹属性，位置。从0开始编号
		private int number; // 数量
		private java.util.HashMap<Integer, Integer> numbermap; // 数量
		private long uniqueid; // 物品的唯一id

		public Data() {
			position = -1;
			numbermap = new java.util.HashMap<Integer, Integer>();
		}

		Data(xbean.Item _o1_) {
			if (_o1_ instanceof Item) assign((Item)_o1_);
			else if (_o1_ instanceof Item.Data) assign((Item.Data)_o1_);
			else if (_o1_ instanceof Item.Const) assign(((Item.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(Item _o_) {
			id = _o_.id;
			flags = _o_.flags;
			position = _o_.position;
			number = _o_.number;
			numbermap = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.numbermap.entrySet())
				numbermap.put(_e_.getKey(), _e_.getValue());
			uniqueid = _o_.uniqueid;
		}

		private void assign(Item.Data _o_) {
			id = _o_.id;
			flags = _o_.flags;
			position = _o_.position;
			number = _o_.number;
			numbermap = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.numbermap.entrySet())
				numbermap.put(_e_.getKey(), _e_.getValue());
			uniqueid = _o_.uniqueid;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(id);
			_os_.marshal(flags);
			_os_.marshal(position);
			_os_.marshal(number);
			_os_.compact_uint32(numbermap.size());
			for (java.util.Map.Entry<Integer, Integer> _e_ : numbermap.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_os_.marshal(_e_.getValue());
			}
			_os_.marshal(uniqueid);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			id = _os_.unmarshal_int();
			flags = _os_.unmarshal_int();
			position = _os_.unmarshal_int();
			number = _os_.unmarshal_int();
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					numbermap = new java.util.HashMap<Integer, Integer>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					int _v_ = 0;
					_v_ = _os_.unmarshal_int();
					numbermap.put(_k_, _v_);
				}
			}
			uniqueid = _os_.unmarshal_long();
			return _os_;
		}

		@Override
		public xbean.Item copy() {
			return new Data(this);
		}

		@Override
		public xbean.Item toData() {
			return new Data(this);
		}

		public xbean.Item toBean() {
			return new Item(this, null, null);
		}

		@Override
		public xbean.Item toDataIf() {
			return this;
		}

		public xbean.Item toBeanIf() {
			return new Item(this, null, null);
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
		public int getId() { // 物品编号
			return id;
		}

		@Override
		public int getFlags() { // 标志，叠加的时候，flags 也 OR 叠加
			return flags;
		}

		@Override
		public int getPosition() { // 包裹属性，位置。从0开始编号
			return position;
		}

		@Override
		public int getNumber() { // 数量
			return number;
		}

		@Override
		public java.util.Map<Integer, Integer> getNumbermap() { // 数量
			return numbermap;
		}

		@Override
		public java.util.Map<Integer, Integer> getNumbermapAsData() { // 数量
			return numbermap;
		}

		@Override
		public long getUniqueid() { // 物品的唯一id
			return uniqueid;
		}

		@Override
		public void setId(int _v_) { // 物品编号
			id = _v_;
		}

		@Override
		public void setFlags(int _v_) { // 标志，叠加的时候，flags 也 OR 叠加
			flags = _v_;
		}

		@Override
		public void setPosition(int _v_) { // 包裹属性，位置。从0开始编号
			position = _v_;
		}

		@Override
		public void setNumber(int _v_) { // 数量
			number = _v_;
		}

		@Override
		public void setUniqueid(long _v_) { // 物品的唯一id
			uniqueid = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof Item.Data)) return false;
			Item.Data _o_ = (Item.Data) _o1_;
			if (id != _o_.id) return false;
			if (flags != _o_.flags) return false;
			if (position != _o_.position) return false;
			if (number != _o_.number) return false;
			if (!numbermap.equals(_o_.numbermap)) return false;
			if (uniqueid != _o_.uniqueid) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += id;
			_h_ += flags;
			_h_ += position;
			_h_ += number;
			_h_ += numbermap.hashCode();
			_h_ += uniqueid;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(id);
			_sb_.append(",");
			_sb_.append(flags);
			_sb_.append(",");
			_sb_.append(position);
			_sb_.append(",");
			_sb_.append(number);
			_sb_.append(",");
			_sb_.append(numbermap);
			_sb_.append(",");
			_sb_.append(uniqueid);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
