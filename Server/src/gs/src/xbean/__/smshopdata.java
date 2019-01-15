
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class smshopdata extends xdb.XBean implements xbean.smshopdata {
	private int id; // id
	private int isopen; // 是否购买（1购买，0未购买）
	private int price; // 价格

	smshopdata(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
	}

	public smshopdata() {
		this(0, null, null);
	}

	public smshopdata(smshopdata _o_) {
		this(_o_, null, null);
	}

	smshopdata(xbean.smshopdata _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof smshopdata) assign((smshopdata)_o1_);
		else if (_o1_ instanceof smshopdata.Data) assign((smshopdata.Data)_o1_);
		else if (_o1_ instanceof smshopdata.Const) assign(((smshopdata.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(smshopdata _o_) {
		_o_._xdb_verify_unsafe_();
		id = _o_.id;
		isopen = _o_.isopen;
		price = _o_.price;
	}

	private void assign(smshopdata.Data _o_) {
		id = _o_.id;
		isopen = _o_.isopen;
		price = _o_.price;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(id);
		_os_.marshal(isopen);
		_os_.marshal(price);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		id = _os_.unmarshal_int();
		isopen = _os_.unmarshal_int();
		price = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.smshopdata copy() {
		_xdb_verify_unsafe_();
		return new smshopdata(this);
	}

	@Override
	public xbean.smshopdata toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.smshopdata toBean() {
		_xdb_verify_unsafe_();
		return new smshopdata(this); // same as copy()
	}

	@Override
	public xbean.smshopdata toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.smshopdata toBeanIf() {
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
	public int getIsopen() { // 是否购买（1购买，0未购买）
		_xdb_verify_unsafe_();
		return isopen;
	}

	@Override
	public int getPrice() { // 价格
		_xdb_verify_unsafe_();
		return price;
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
	public void setIsopen(int _v_) { // 是否购买（1购买，0未购买）
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "isopen") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, isopen) {
					public void rollback() { isopen = _xdb_saved; }
				};}});
		isopen = _v_;
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
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		smshopdata _o_ = null;
		if ( _o1_ instanceof smshopdata ) _o_ = (smshopdata)_o1_;
		else if ( _o1_ instanceof smshopdata.Const ) _o_ = ((smshopdata.Const)_o1_).nThis();
		else return false;
		if (id != _o_.id) return false;
		if (isopen != _o_.isopen) return false;
		if (price != _o_.price) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += id;
		_h_ += isopen;
		_h_ += price;
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
		_sb_.append(price);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("id"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("isopen"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("price"));
		return lb;
	}

	private class Const implements xbean.smshopdata {
		smshopdata nThis() {
			return smshopdata.this;
		}

		@Override
		public xbean.smshopdata copy() {
			return smshopdata.this.copy();
		}

		@Override
		public xbean.smshopdata toData() {
			return smshopdata.this.toData();
		}

		public xbean.smshopdata toBean() {
			return smshopdata.this.toBean();
		}

		@Override
		public xbean.smshopdata toDataIf() {
			return smshopdata.this.toDataIf();
		}

		public xbean.smshopdata toBeanIf() {
			return smshopdata.this.toBeanIf();
		}

		@Override
		public int getId() { // id
			_xdb_verify_unsafe_();
			return id;
		}

		@Override
		public int getIsopen() { // 是否购买（1购买，0未购买）
			_xdb_verify_unsafe_();
			return isopen;
		}

		@Override
		public int getPrice() { // 价格
			_xdb_verify_unsafe_();
			return price;
		}

		@Override
		public void setId(int _v_) { // id
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setIsopen(int _v_) { // 是否购买（1购买，0未购买）
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setPrice(int _v_) { // 价格
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
			return smshopdata.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return smshopdata.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return smshopdata.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return smshopdata.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return smshopdata.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return smshopdata.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return smshopdata.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return smshopdata.this.hashCode();
		}

		@Override
		public String toString() {
			return smshopdata.this.toString();
		}

	}

	public static final class Data implements xbean.smshopdata {
		private int id; // id
		private int isopen; // 是否购买（1购买，0未购买）
		private int price; // 价格

		public Data() {
		}

		Data(xbean.smshopdata _o1_) {
			if (_o1_ instanceof smshopdata) assign((smshopdata)_o1_);
			else if (_o1_ instanceof smshopdata.Data) assign((smshopdata.Data)_o1_);
			else if (_o1_ instanceof smshopdata.Const) assign(((smshopdata.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(smshopdata _o_) {
			id = _o_.id;
			isopen = _o_.isopen;
			price = _o_.price;
		}

		private void assign(smshopdata.Data _o_) {
			id = _o_.id;
			isopen = _o_.isopen;
			price = _o_.price;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(id);
			_os_.marshal(isopen);
			_os_.marshal(price);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			id = _os_.unmarshal_int();
			isopen = _os_.unmarshal_int();
			price = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.smshopdata copy() {
			return new Data(this);
		}

		@Override
		public xbean.smshopdata toData() {
			return new Data(this);
		}

		public xbean.smshopdata toBean() {
			return new smshopdata(this, null, null);
		}

		@Override
		public xbean.smshopdata toDataIf() {
			return this;
		}

		public xbean.smshopdata toBeanIf() {
			return new smshopdata(this, null, null);
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
		public int getIsopen() { // 是否购买（1购买，0未购买）
			return isopen;
		}

		@Override
		public int getPrice() { // 价格
			return price;
		}

		@Override
		public void setId(int _v_) { // id
			id = _v_;
		}

		@Override
		public void setIsopen(int _v_) { // 是否购买（1购买，0未购买）
			isopen = _v_;
		}

		@Override
		public void setPrice(int _v_) { // 价格
			price = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof smshopdata.Data)) return false;
			smshopdata.Data _o_ = (smshopdata.Data) _o1_;
			if (id != _o_.id) return false;
			if (isopen != _o_.isopen) return false;
			if (price != _o_.price) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += id;
			_h_ += isopen;
			_h_ += price;
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
			_sb_.append(price);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
