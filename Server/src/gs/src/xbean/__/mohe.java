
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class mohe extends xdb.XBean implements xbean.mohe {
	private int id; // id
	private int isopen; // 是否开启（1开启，0未开启）
	private int place; // 排序（0为随机排序，123为正常排序）

	mohe(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
	}

	public mohe() {
		this(0, null, null);
	}

	public mohe(mohe _o_) {
		this(_o_, null, null);
	}

	mohe(xbean.mohe _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof mohe) assign((mohe)_o1_);
		else if (_o1_ instanceof mohe.Data) assign((mohe.Data)_o1_);
		else if (_o1_ instanceof mohe.Const) assign(((mohe.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(mohe _o_) {
		_o_._xdb_verify_unsafe_();
		id = _o_.id;
		isopen = _o_.isopen;
		place = _o_.place;
	}

	private void assign(mohe.Data _o_) {
		id = _o_.id;
		isopen = _o_.isopen;
		place = _o_.place;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(id);
		_os_.marshal(isopen);
		_os_.marshal(place);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		id = _os_.unmarshal_int();
		isopen = _os_.unmarshal_int();
		place = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.mohe copy() {
		_xdb_verify_unsafe_();
		return new mohe(this);
	}

	@Override
	public xbean.mohe toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.mohe toBean() {
		_xdb_verify_unsafe_();
		return new mohe(this); // same as copy()
	}

	@Override
	public xbean.mohe toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.mohe toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getId() { // id
		_xdb_verify_unsafe_();
		return id;
	}

	@Override
	public int getIsopen() { // 是否开启（1开启，0未开启）
		_xdb_verify_unsafe_();
		return isopen;
	}

	@Override
	public int getPlace() { // 排序（0为随机排序，123为正常排序）
		_xdb_verify_unsafe_();
		return place;
	}

	@Override
	public void setId(int _v_) { // id
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "id") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, id) {
					public void rollback() { id = _xdb_saved; }
				};}});
		id = _v_;
	}

	@Override
	public void setIsopen(int _v_) { // 是否开启（1开启，0未开启）
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "isopen") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, isopen) {
					public void rollback() { isopen = _xdb_saved; }
				};}});
		isopen = _v_;
	}

	@Override
	public void setPlace(int _v_) { // 排序（0为随机排序，123为正常排序）
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "place") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, place) {
					public void rollback() { place = _xdb_saved; }
				};}});
		place = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		mohe _o_ = null;
		if ( _o1_ instanceof mohe ) _o_ = (mohe)_o1_;
		else if ( _o1_ instanceof mohe.Const ) _o_ = ((mohe.Const)_o1_).nThis();
		else return false;
		if (id != _o_.id) return false;
		if (isopen != _o_.isopen) return false;
		if (place != _o_.place) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += id;
		_h_ += isopen;
		_h_ += place;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(id);
		_sb_.append(",");
		_sb_.append(isopen);
		_sb_.append(",");
		_sb_.append(place);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("id"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("isopen"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("place"));
		return lb;
	}

	private class Const implements xbean.mohe {
		mohe nThis() {
			return mohe.this;
		}

		@Override
		public xbean.mohe copy() {
			return mohe.this.copy();
		}

		@Override
		public xbean.mohe toData() {
			return mohe.this.toData();
		}

		public xbean.mohe toBean() {
			return mohe.this.toBean();
		}

		@Override
		public xbean.mohe toDataIf() {
			return mohe.this.toDataIf();
		}

		public xbean.mohe toBeanIf() {
			return mohe.this.toBeanIf();
		}

		@Override
		public int getId() { // id
			_xdb_verify_unsafe_();
			return id;
		}

		@Override
		public int getIsopen() { // 是否开启（1开启，0未开启）
			_xdb_verify_unsafe_();
			return isopen;
		}

		@Override
		public int getPlace() { // 排序（0为随机排序，123为正常排序）
			_xdb_verify_unsafe_();
			return place;
		}

		@Override
		public void setId(int _v_) { // id
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setIsopen(int _v_) { // 是否开启（1开启，0未开启）
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setPlace(int _v_) { // 排序（0为随机排序，123为正常排序）
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
			return mohe.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return mohe.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return mohe.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return mohe.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return mohe.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return mohe.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return mohe.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return mohe.this.hashCode();
		}

		@Override
		public String toString() {
			return mohe.this.toString();
		}

	}

	public static final class Data implements xbean.mohe {
		private int id; // id
		private int isopen; // 是否开启（1开启，0未开启）
		private int place; // 排序（0为随机排序，123为正常排序）

		public Data() {
		}

		Data(xbean.mohe _o1_) {
			if (_o1_ instanceof mohe) assign((mohe)_o1_);
			else if (_o1_ instanceof mohe.Data) assign((mohe.Data)_o1_);
			else if (_o1_ instanceof mohe.Const) assign(((mohe.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(mohe _o_) {
			id = _o_.id;
			isopen = _o_.isopen;
			place = _o_.place;
		}

		private void assign(mohe.Data _o_) {
			id = _o_.id;
			isopen = _o_.isopen;
			place = _o_.place;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(id);
			_os_.marshal(isopen);
			_os_.marshal(place);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			id = _os_.unmarshal_int();
			isopen = _os_.unmarshal_int();
			place = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.mohe copy() {
			return new Data(this);
		}

		@Override
		public xbean.mohe toData() {
			return new Data(this);
		}

		public xbean.mohe toBean() {
			return new mohe(this, null, null);
		}

		@Override
		public xbean.mohe toDataIf() {
			return this;
		}

		public xbean.mohe toBeanIf() {
			return new mohe(this, null, null);
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
		public int getId() { // id
			return id;
		}

		@Override
		public int getIsopen() { // 是否开启（1开启，0未开启）
			return isopen;
		}

		@Override
		public int getPlace() { // 排序（0为随机排序，123为正常排序）
			return place;
		}

		@Override
		public void setId(int _v_) { // id
			id = _v_;
		}

		@Override
		public void setIsopen(int _v_) { // 是否开启（1开启，0未开启）
			isopen = _v_;
		}

		@Override
		public void setPlace(int _v_) { // 排序（0为随机排序，123为正常排序）
			place = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof mohe.Data)) return false;
			mohe.Data _o_ = (mohe.Data) _o1_;
			if (id != _o_.id) return false;
			if (isopen != _o_.isopen) return false;
			if (place != _o_.place) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += id;
			_h_ += isopen;
			_h_ += place;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(id);
			_sb_.append(",");
			_sb_.append(isopen);
			_sb_.append(",");
			_sb_.append(place);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
