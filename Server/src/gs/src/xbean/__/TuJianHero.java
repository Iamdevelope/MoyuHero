
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class TuJianHero extends xdb.XBean implements xbean.TuJianHero {
	private int heroid; // 获得过的武将
	private int flag; // 是否满级，0未满，1满级

	TuJianHero(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
	}

	public TuJianHero() {
		this(0, null, null);
	}

	public TuJianHero(TuJianHero _o_) {
		this(_o_, null, null);
	}

	TuJianHero(xbean.TuJianHero _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof TuJianHero) assign((TuJianHero)_o1_);
		else if (_o1_ instanceof TuJianHero.Data) assign((TuJianHero.Data)_o1_);
		else if (_o1_ instanceof TuJianHero.Const) assign(((TuJianHero.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(TuJianHero _o_) {
		_o_._xdb_verify_unsafe_();
		heroid = _o_.heroid;
		flag = _o_.flag;
	}

	private void assign(TuJianHero.Data _o_) {
		heroid = _o_.heroid;
		flag = _o_.flag;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(heroid);
		_os_.marshal(flag);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		heroid = _os_.unmarshal_int();
		flag = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.TuJianHero copy() {
		_xdb_verify_unsafe_();
		return new TuJianHero(this);
	}

	@Override
	public xbean.TuJianHero toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.TuJianHero toBean() {
		_xdb_verify_unsafe_();
		return new TuJianHero(this); // same as copy()
	}

	@Override
	public xbean.TuJianHero toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.TuJianHero toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getHeroid() { // 获得过的武将
		_xdb_verify_unsafe_();
		return heroid;
	}

	@Override
	public int getFlag() { // 是否满级，0未满，1满级
		_xdb_verify_unsafe_();
		return flag;
	}

	@Override
	public void setHeroid(int _v_) { // 获得过的武将
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "heroid") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, heroid) {
					public void rollback() { heroid = _xdb_saved; }
				};}});
		heroid = _v_;
	}

	@Override
	public void setFlag(int _v_) { // 是否满级，0未满，1满级
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "flag") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, flag) {
					public void rollback() { flag = _xdb_saved; }
				};}});
		flag = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		TuJianHero _o_ = null;
		if ( _o1_ instanceof TuJianHero ) _o_ = (TuJianHero)_o1_;
		else if ( _o1_ instanceof TuJianHero.Const ) _o_ = ((TuJianHero.Const)_o1_).nThis();
		else return false;
		if (heroid != _o_.heroid) return false;
		if (flag != _o_.flag) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += heroid;
		_h_ += flag;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(heroid);
		_sb_.append(",");
		_sb_.append(flag);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("heroid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("flag"));
		return lb;
	}

	private class Const implements xbean.TuJianHero {
		TuJianHero nThis() {
			return TuJianHero.this;
		}

		@Override
		public xbean.TuJianHero copy() {
			return TuJianHero.this.copy();
		}

		@Override
		public xbean.TuJianHero toData() {
			return TuJianHero.this.toData();
		}

		public xbean.TuJianHero toBean() {
			return TuJianHero.this.toBean();
		}

		@Override
		public xbean.TuJianHero toDataIf() {
			return TuJianHero.this.toDataIf();
		}

		public xbean.TuJianHero toBeanIf() {
			return TuJianHero.this.toBeanIf();
		}

		@Override
		public int getHeroid() { // 获得过的武将
			_xdb_verify_unsafe_();
			return heroid;
		}

		@Override
		public int getFlag() { // 是否满级，0未满，1满级
			_xdb_verify_unsafe_();
			return flag;
		}

		@Override
		public void setHeroid(int _v_) { // 获得过的武将
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setFlag(int _v_) { // 是否满级，0未满，1满级
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
			return TuJianHero.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return TuJianHero.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return TuJianHero.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return TuJianHero.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return TuJianHero.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return TuJianHero.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return TuJianHero.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return TuJianHero.this.hashCode();
		}

		@Override
		public String toString() {
			return TuJianHero.this.toString();
		}

	}

	public static final class Data implements xbean.TuJianHero {
		private int heroid; // 获得过的武将
		private int flag; // 是否满级，0未满，1满级

		public Data() {
		}

		Data(xbean.TuJianHero _o1_) {
			if (_o1_ instanceof TuJianHero) assign((TuJianHero)_o1_);
			else if (_o1_ instanceof TuJianHero.Data) assign((TuJianHero.Data)_o1_);
			else if (_o1_ instanceof TuJianHero.Const) assign(((TuJianHero.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(TuJianHero _o_) {
			heroid = _o_.heroid;
			flag = _o_.flag;
		}

		private void assign(TuJianHero.Data _o_) {
			heroid = _o_.heroid;
			flag = _o_.flag;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(heroid);
			_os_.marshal(flag);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			heroid = _os_.unmarshal_int();
			flag = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.TuJianHero copy() {
			return new Data(this);
		}

		@Override
		public xbean.TuJianHero toData() {
			return new Data(this);
		}

		public xbean.TuJianHero toBean() {
			return new TuJianHero(this, null, null);
		}

		@Override
		public xbean.TuJianHero toDataIf() {
			return this;
		}

		public xbean.TuJianHero toBeanIf() {
			return new TuJianHero(this, null, null);
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
		public int getHeroid() { // 获得过的武将
			return heroid;
		}

		@Override
		public int getFlag() { // 是否满级，0未满，1满级
			return flag;
		}

		@Override
		public void setHeroid(int _v_) { // 获得过的武将
			heroid = _v_;
		}

		@Override
		public void setFlag(int _v_) { // 是否满级，0未满，1满级
			flag = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof TuJianHero.Data)) return false;
			TuJianHero.Data _o_ = (TuJianHero.Data) _o1_;
			if (heroid != _o_.heroid) return false;
			if (flag != _o_.flag) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += heroid;
			_h_ += flag;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(heroid);
			_sb_.append(",");
			_sb_.append(flag);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
