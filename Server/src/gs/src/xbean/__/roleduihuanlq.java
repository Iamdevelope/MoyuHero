
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class roleduihuanlq extends xdb.XBean implements xbean.roleduihuanlq {
	private int lqkey; // 兑换礼券key
	private int typenum; // 兑换礼券替换计数
	private int num; // 兑换礼券计数

	roleduihuanlq(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
	}

	public roleduihuanlq() {
		this(0, null, null);
	}

	public roleduihuanlq(roleduihuanlq _o_) {
		this(_o_, null, null);
	}

	roleduihuanlq(xbean.roleduihuanlq _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof roleduihuanlq) assign((roleduihuanlq)_o1_);
		else if (_o1_ instanceof roleduihuanlq.Data) assign((roleduihuanlq.Data)_o1_);
		else if (_o1_ instanceof roleduihuanlq.Const) assign(((roleduihuanlq.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(roleduihuanlq _o_) {
		_o_._xdb_verify_unsafe_();
		lqkey = _o_.lqkey;
		typenum = _o_.typenum;
		num = _o_.num;
	}

	private void assign(roleduihuanlq.Data _o_) {
		lqkey = _o_.lqkey;
		typenum = _o_.typenum;
		num = _o_.num;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(lqkey);
		_os_.marshal(typenum);
		_os_.marshal(num);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		lqkey = _os_.unmarshal_int();
		typenum = _os_.unmarshal_int();
		num = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.roleduihuanlq copy() {
		_xdb_verify_unsafe_();
		return new roleduihuanlq(this);
	}

	@Override
	public xbean.roleduihuanlq toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.roleduihuanlq toBean() {
		_xdb_verify_unsafe_();
		return new roleduihuanlq(this); // same as copy()
	}

	@Override
	public xbean.roleduihuanlq toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.roleduihuanlq toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getLqkey() { // 兑换礼券key
		_xdb_verify_unsafe_();
		return lqkey;
	}

	@Override
	public int getTypenum() { // 兑换礼券替换计数
		_xdb_verify_unsafe_();
		return typenum;
	}

	@Override
	public int getNum() { // 兑换礼券计数
		_xdb_verify_unsafe_();
		return num;
	}

	@Override
	public void setLqkey(int _v_) { // 兑换礼券key
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "lqkey") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, lqkey) {
					public void rollback() { lqkey = _xdb_saved; }
				};}});
		lqkey = _v_;
	}

	@Override
	public void setTypenum(int _v_) { // 兑换礼券替换计数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "typenum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, typenum) {
					public void rollback() { typenum = _xdb_saved; }
				};}});
		typenum = _v_;
	}

	@Override
	public void setNum(int _v_) { // 兑换礼券计数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "num") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, num) {
					public void rollback() { num = _xdb_saved; }
				};}});
		num = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		roleduihuanlq _o_ = null;
		if ( _o1_ instanceof roleduihuanlq ) _o_ = (roleduihuanlq)_o1_;
		else if ( _o1_ instanceof roleduihuanlq.Const ) _o_ = ((roleduihuanlq.Const)_o1_).nThis();
		else return false;
		if (lqkey != _o_.lqkey) return false;
		if (typenum != _o_.typenum) return false;
		if (num != _o_.num) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += lqkey;
		_h_ += typenum;
		_h_ += num;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(lqkey);
		_sb_.append(",");
		_sb_.append(typenum);
		_sb_.append(",");
		_sb_.append(num);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("lqkey"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("typenum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("num"));
		return lb;
	}

	private class Const implements xbean.roleduihuanlq {
		roleduihuanlq nThis() {
			return roleduihuanlq.this;
		}

		@Override
		public xbean.roleduihuanlq copy() {
			return roleduihuanlq.this.copy();
		}

		@Override
		public xbean.roleduihuanlq toData() {
			return roleduihuanlq.this.toData();
		}

		public xbean.roleduihuanlq toBean() {
			return roleduihuanlq.this.toBean();
		}

		@Override
		public xbean.roleduihuanlq toDataIf() {
			return roleduihuanlq.this.toDataIf();
		}

		public xbean.roleduihuanlq toBeanIf() {
			return roleduihuanlq.this.toBeanIf();
		}

		@Override
		public int getLqkey() { // 兑换礼券key
			_xdb_verify_unsafe_();
			return lqkey;
		}

		@Override
		public int getTypenum() { // 兑换礼券替换计数
			_xdb_verify_unsafe_();
			return typenum;
		}

		@Override
		public int getNum() { // 兑换礼券计数
			_xdb_verify_unsafe_();
			return num;
		}

		@Override
		public void setLqkey(int _v_) { // 兑换礼券key
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTypenum(int _v_) { // 兑换礼券替换计数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setNum(int _v_) { // 兑换礼券计数
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
			return roleduihuanlq.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return roleduihuanlq.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return roleduihuanlq.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return roleduihuanlq.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return roleduihuanlq.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return roleduihuanlq.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return roleduihuanlq.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return roleduihuanlq.this.hashCode();
		}

		@Override
		public String toString() {
			return roleduihuanlq.this.toString();
		}

	}

	public static final class Data implements xbean.roleduihuanlq {
		private int lqkey; // 兑换礼券key
		private int typenum; // 兑换礼券替换计数
		private int num; // 兑换礼券计数

		public Data() {
		}

		Data(xbean.roleduihuanlq _o1_) {
			if (_o1_ instanceof roleduihuanlq) assign((roleduihuanlq)_o1_);
			else if (_o1_ instanceof roleduihuanlq.Data) assign((roleduihuanlq.Data)_o1_);
			else if (_o1_ instanceof roleduihuanlq.Const) assign(((roleduihuanlq.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(roleduihuanlq _o_) {
			lqkey = _o_.lqkey;
			typenum = _o_.typenum;
			num = _o_.num;
		}

		private void assign(roleduihuanlq.Data _o_) {
			lqkey = _o_.lqkey;
			typenum = _o_.typenum;
			num = _o_.num;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(lqkey);
			_os_.marshal(typenum);
			_os_.marshal(num);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			lqkey = _os_.unmarshal_int();
			typenum = _os_.unmarshal_int();
			num = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.roleduihuanlq copy() {
			return new Data(this);
		}

		@Override
		public xbean.roleduihuanlq toData() {
			return new Data(this);
		}

		public xbean.roleduihuanlq toBean() {
			return new roleduihuanlq(this, null, null);
		}

		@Override
		public xbean.roleduihuanlq toDataIf() {
			return this;
		}

		public xbean.roleduihuanlq toBeanIf() {
			return new roleduihuanlq(this, null, null);
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
		public int getLqkey() { // 兑换礼券key
			return lqkey;
		}

		@Override
		public int getTypenum() { // 兑换礼券替换计数
			return typenum;
		}

		@Override
		public int getNum() { // 兑换礼券计数
			return num;
		}

		@Override
		public void setLqkey(int _v_) { // 兑换礼券key
			lqkey = _v_;
		}

		@Override
		public void setTypenum(int _v_) { // 兑换礼券替换计数
			typenum = _v_;
		}

		@Override
		public void setNum(int _v_) { // 兑换礼券计数
			num = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof roleduihuanlq.Data)) return false;
			roleduihuanlq.Data _o_ = (roleduihuanlq.Data) _o1_;
			if (lqkey != _o_.lqkey) return false;
			if (typenum != _o_.typenum) return false;
			if (num != _o_.num) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += lqkey;
			_h_ += typenum;
			_h_ += num;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(lqkey);
			_sb_.append(",");
			_sb_.append(typenum);
			_sb_.append(",");
			_sb_.append(num);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
