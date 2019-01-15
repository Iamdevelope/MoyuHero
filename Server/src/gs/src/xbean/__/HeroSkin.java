
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class HeroSkin extends xdb.XBean implements xbean.HeroSkin {
	private int heroskinid; // 皮肤ID
	private long createtime; // 创建时间

	HeroSkin(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
	}

	public HeroSkin() {
		this(0, null, null);
	}

	public HeroSkin(HeroSkin _o_) {
		this(_o_, null, null);
	}

	HeroSkin(xbean.HeroSkin _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof HeroSkin) assign((HeroSkin)_o1_);
		else if (_o1_ instanceof HeroSkin.Data) assign((HeroSkin.Data)_o1_);
		else if (_o1_ instanceof HeroSkin.Const) assign(((HeroSkin.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(HeroSkin _o_) {
		_o_._xdb_verify_unsafe_();
		heroskinid = _o_.heroskinid;
		createtime = _o_.createtime;
	}

	private void assign(HeroSkin.Data _o_) {
		heroskinid = _o_.heroskinid;
		createtime = _o_.createtime;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(heroskinid);
		_os_.marshal(createtime);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		heroskinid = _os_.unmarshal_int();
		createtime = _os_.unmarshal_long();
		return _os_;
	}

	@Override
	public xbean.HeroSkin copy() {
		_xdb_verify_unsafe_();
		return new HeroSkin(this);
	}

	@Override
	public xbean.HeroSkin toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.HeroSkin toBean() {
		_xdb_verify_unsafe_();
		return new HeroSkin(this); // same as copy()
	}

	@Override
	public xbean.HeroSkin toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.HeroSkin toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getHeroskinid() { // 皮肤ID
		_xdb_verify_unsafe_();
		return heroskinid;
	}

	@Override
	public long getCreatetime() { // 创建时间
		_xdb_verify_unsafe_();
		return createtime;
	}

	@Override
	public void setHeroskinid(int _v_) { // 皮肤ID
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "heroskinid") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, heroskinid) {
					public void rollback() { heroskinid = _xdb_saved; }
				};}});
		heroskinid = _v_;
	}

	@Override
	public void setCreatetime(long _v_) { // 创建时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "createtime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, createtime) {
					public void rollback() { createtime = _xdb_saved; }
				};}});
		createtime = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		HeroSkin _o_ = null;
		if ( _o1_ instanceof HeroSkin ) _o_ = (HeroSkin)_o1_;
		else if ( _o1_ instanceof HeroSkin.Const ) _o_ = ((HeroSkin.Const)_o1_).nThis();
		else return false;
		if (heroskinid != _o_.heroskinid) return false;
		if (createtime != _o_.createtime) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += heroskinid;
		_h_ += createtime;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(heroskinid);
		_sb_.append(",");
		_sb_.append(createtime);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("heroskinid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("createtime"));
		return lb;
	}

	private class Const implements xbean.HeroSkin {
		HeroSkin nThis() {
			return HeroSkin.this;
		}

		@Override
		public xbean.HeroSkin copy() {
			return HeroSkin.this.copy();
		}

		@Override
		public xbean.HeroSkin toData() {
			return HeroSkin.this.toData();
		}

		public xbean.HeroSkin toBean() {
			return HeroSkin.this.toBean();
		}

		@Override
		public xbean.HeroSkin toDataIf() {
			return HeroSkin.this.toDataIf();
		}

		public xbean.HeroSkin toBeanIf() {
			return HeroSkin.this.toBeanIf();
		}

		@Override
		public int getHeroskinid() { // 皮肤ID
			_xdb_verify_unsafe_();
			return heroskinid;
		}

		@Override
		public long getCreatetime() { // 创建时间
			_xdb_verify_unsafe_();
			return createtime;
		}

		@Override
		public void setHeroskinid(int _v_) { // 皮肤ID
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setCreatetime(long _v_) { // 创建时间
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
			return HeroSkin.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return HeroSkin.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return HeroSkin.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return HeroSkin.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return HeroSkin.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return HeroSkin.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return HeroSkin.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return HeroSkin.this.hashCode();
		}

		@Override
		public String toString() {
			return HeroSkin.this.toString();
		}

	}

	public static final class Data implements xbean.HeroSkin {
		private int heroskinid; // 皮肤ID
		private long createtime; // 创建时间

		public Data() {
		}

		Data(xbean.HeroSkin _o1_) {
			if (_o1_ instanceof HeroSkin) assign((HeroSkin)_o1_);
			else if (_o1_ instanceof HeroSkin.Data) assign((HeroSkin.Data)_o1_);
			else if (_o1_ instanceof HeroSkin.Const) assign(((HeroSkin.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(HeroSkin _o_) {
			heroskinid = _o_.heroskinid;
			createtime = _o_.createtime;
		}

		private void assign(HeroSkin.Data _o_) {
			heroskinid = _o_.heroskinid;
			createtime = _o_.createtime;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(heroskinid);
			_os_.marshal(createtime);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			heroskinid = _os_.unmarshal_int();
			createtime = _os_.unmarshal_long();
			return _os_;
		}

		@Override
		public xbean.HeroSkin copy() {
			return new Data(this);
		}

		@Override
		public xbean.HeroSkin toData() {
			return new Data(this);
		}

		public xbean.HeroSkin toBean() {
			return new HeroSkin(this, null, null);
		}

		@Override
		public xbean.HeroSkin toDataIf() {
			return this;
		}

		public xbean.HeroSkin toBeanIf() {
			return new HeroSkin(this, null, null);
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
		public int getHeroskinid() { // 皮肤ID
			return heroskinid;
		}

		@Override
		public long getCreatetime() { // 创建时间
			return createtime;
		}

		@Override
		public void setHeroskinid(int _v_) { // 皮肤ID
			heroskinid = _v_;
		}

		@Override
		public void setCreatetime(long _v_) { // 创建时间
			createtime = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof HeroSkin.Data)) return false;
			HeroSkin.Data _o_ = (HeroSkin.Data) _o1_;
			if (heroskinid != _o_.heroskinid) return false;
			if (createtime != _o_.createtime) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += heroskinid;
			_h_ += createtime;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(heroskinid);
			_sb_.append(",");
			_sb_.append(createtime);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
