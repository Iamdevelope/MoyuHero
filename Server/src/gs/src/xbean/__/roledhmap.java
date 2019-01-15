
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class roledhmap extends xdb.XBean implements xbean.roledhmap {
	private java.util.HashMap<Integer, xbean.roleduihuanlq> dhmap; // 兑换礼券计数列表

	roledhmap(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		dhmap = new java.util.HashMap<Integer, xbean.roleduihuanlq>();
	}

	public roledhmap() {
		this(0, null, null);
	}

	public roledhmap(roledhmap _o_) {
		this(_o_, null, null);
	}

	roledhmap(xbean.roledhmap _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof roledhmap) assign((roledhmap)_o1_);
		else if (_o1_ instanceof roledhmap.Data) assign((roledhmap.Data)_o1_);
		else if (_o1_ instanceof roledhmap.Const) assign(((roledhmap.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(roledhmap _o_) {
		_o_._xdb_verify_unsafe_();
		dhmap = new java.util.HashMap<Integer, xbean.roleduihuanlq>();
		for (java.util.Map.Entry<Integer, xbean.roleduihuanlq> _e_ : _o_.dhmap.entrySet())
			dhmap.put(_e_.getKey(), new roleduihuanlq(_e_.getValue(), this, "dhmap"));
	}

	private void assign(roledhmap.Data _o_) {
		dhmap = new java.util.HashMap<Integer, xbean.roleduihuanlq>();
		for (java.util.Map.Entry<Integer, xbean.roleduihuanlq> _e_ : _o_.dhmap.entrySet())
			dhmap.put(_e_.getKey(), new roleduihuanlq(_e_.getValue(), this, "dhmap"));
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(dhmap.size());
		for (java.util.Map.Entry<Integer, xbean.roleduihuanlq> _e_ : dhmap.entrySet())
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
				dhmap = new java.util.HashMap<Integer, xbean.roleduihuanlq>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				xbean.roleduihuanlq _v_ = new roleduihuanlq(0, this, "dhmap");
				_v_.unmarshal(_os_);
				dhmap.put(_k_, _v_);
			}
		}
		return _os_;
	}

	@Override
	public xbean.roledhmap copy() {
		_xdb_verify_unsafe_();
		return new roledhmap(this);
	}

	@Override
	public xbean.roledhmap toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.roledhmap toBean() {
		_xdb_verify_unsafe_();
		return new roledhmap(this); // same as copy()
	}

	@Override
	public xbean.roledhmap toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.roledhmap toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.Map<Integer, xbean.roleduihuanlq> getDhmap() { // 兑换礼券计数列表
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "dhmap"), dhmap);
	}

	@Override
	public java.util.Map<Integer, xbean.roleduihuanlq> getDhmapAsData() { // 兑换礼券计数列表
		_xdb_verify_unsafe_();
		java.util.Map<Integer, xbean.roleduihuanlq> dhmap;
		roledhmap _o_ = this;
		dhmap = new java.util.HashMap<Integer, xbean.roleduihuanlq>();
		for (java.util.Map.Entry<Integer, xbean.roleduihuanlq> _e_ : _o_.dhmap.entrySet())
			dhmap.put(_e_.getKey(), new roleduihuanlq.Data(_e_.getValue()));
		return dhmap;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		roledhmap _o_ = null;
		if ( _o1_ instanceof roledhmap ) _o_ = (roledhmap)_o1_;
		else if ( _o1_ instanceof roledhmap.Const ) _o_ = ((roledhmap.Const)_o1_).nThis();
		else return false;
		if (!dhmap.equals(_o_.dhmap)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += dhmap.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(dhmap);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableMap().setVarName("dhmap"));
		return lb;
	}

	private class Const implements xbean.roledhmap {
		roledhmap nThis() {
			return roledhmap.this;
		}

		@Override
		public xbean.roledhmap copy() {
			return roledhmap.this.copy();
		}

		@Override
		public xbean.roledhmap toData() {
			return roledhmap.this.toData();
		}

		public xbean.roledhmap toBean() {
			return roledhmap.this.toBean();
		}

		@Override
		public xbean.roledhmap toDataIf() {
			return roledhmap.this.toDataIf();
		}

		public xbean.roledhmap toBeanIf() {
			return roledhmap.this.toBeanIf();
		}

		@Override
		public java.util.Map<Integer, xbean.roleduihuanlq> getDhmap() { // 兑换礼券计数列表
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(dhmap);
		}

		@Override
		public java.util.Map<Integer, xbean.roleduihuanlq> getDhmapAsData() { // 兑换礼券计数列表
			_xdb_verify_unsafe_();
			java.util.Map<Integer, xbean.roleduihuanlq> dhmap;
			roledhmap _o_ = roledhmap.this;
			dhmap = new java.util.HashMap<Integer, xbean.roleduihuanlq>();
			for (java.util.Map.Entry<Integer, xbean.roleduihuanlq> _e_ : _o_.dhmap.entrySet())
				dhmap.put(_e_.getKey(), new roleduihuanlq.Data(_e_.getValue()));
			return dhmap;
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
			return roledhmap.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return roledhmap.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return roledhmap.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return roledhmap.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return roledhmap.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return roledhmap.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return roledhmap.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return roledhmap.this.hashCode();
		}

		@Override
		public String toString() {
			return roledhmap.this.toString();
		}

	}

	public static final class Data implements xbean.roledhmap {
		private java.util.HashMap<Integer, xbean.roleduihuanlq> dhmap; // 兑换礼券计数列表

		public Data() {
			dhmap = new java.util.HashMap<Integer, xbean.roleduihuanlq>();
		}

		Data(xbean.roledhmap _o1_) {
			if (_o1_ instanceof roledhmap) assign((roledhmap)_o1_);
			else if (_o1_ instanceof roledhmap.Data) assign((roledhmap.Data)_o1_);
			else if (_o1_ instanceof roledhmap.Const) assign(((roledhmap.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(roledhmap _o_) {
			dhmap = new java.util.HashMap<Integer, xbean.roleduihuanlq>();
			for (java.util.Map.Entry<Integer, xbean.roleduihuanlq> _e_ : _o_.dhmap.entrySet())
				dhmap.put(_e_.getKey(), new roleduihuanlq.Data(_e_.getValue()));
		}

		private void assign(roledhmap.Data _o_) {
			dhmap = new java.util.HashMap<Integer, xbean.roleduihuanlq>();
			for (java.util.Map.Entry<Integer, xbean.roleduihuanlq> _e_ : _o_.dhmap.entrySet())
				dhmap.put(_e_.getKey(), new roleduihuanlq.Data(_e_.getValue()));
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(dhmap.size());
			for (java.util.Map.Entry<Integer, xbean.roleduihuanlq> _e_ : dhmap.entrySet())
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
					dhmap = new java.util.HashMap<Integer, xbean.roleduihuanlq>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					xbean.roleduihuanlq _v_ = xbean.Pod.newroleduihuanlqData();
					_v_.unmarshal(_os_);
					dhmap.put(_k_, _v_);
				}
			}
			return _os_;
		}

		@Override
		public xbean.roledhmap copy() {
			return new Data(this);
		}

		@Override
		public xbean.roledhmap toData() {
			return new Data(this);
		}

		public xbean.roledhmap toBean() {
			return new roledhmap(this, null, null);
		}

		@Override
		public xbean.roledhmap toDataIf() {
			return this;
		}

		public xbean.roledhmap toBeanIf() {
			return new roledhmap(this, null, null);
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
		public java.util.Map<Integer, xbean.roleduihuanlq> getDhmap() { // 兑换礼券计数列表
			return dhmap;
		}

		@Override
		public java.util.Map<Integer, xbean.roleduihuanlq> getDhmapAsData() { // 兑换礼券计数列表
			return dhmap;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof roledhmap.Data)) return false;
			roledhmap.Data _o_ = (roledhmap.Data) _o1_;
			if (!dhmap.equals(_o_.dhmap)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += dhmap.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(dhmap);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
