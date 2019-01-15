
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class HeroColumn extends xdb.XBean implements xbean.HeroColumn {
	private java.util.LinkedList<xbean.Hero> heroes; // 
	private int nextkey; // 

	HeroColumn(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		heroes = new java.util.LinkedList<xbean.Hero>();
	}

	public HeroColumn() {
		this(0, null, null);
	}

	public HeroColumn(HeroColumn _o_) {
		this(_o_, null, null);
	}

	HeroColumn(xbean.HeroColumn _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof HeroColumn) assign((HeroColumn)_o1_);
		else if (_o1_ instanceof HeroColumn.Data) assign((HeroColumn.Data)_o1_);
		else if (_o1_ instanceof HeroColumn.Const) assign(((HeroColumn.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(HeroColumn _o_) {
		_o_._xdb_verify_unsafe_();
		heroes = new java.util.LinkedList<xbean.Hero>();
		for (xbean.Hero _v_ : _o_.heroes)
			heroes.add(new Hero(_v_, this, "heroes"));
		nextkey = _o_.nextkey;
	}

	private void assign(HeroColumn.Data _o_) {
		heroes = new java.util.LinkedList<xbean.Hero>();
		for (xbean.Hero _v_ : _o_.heroes)
			heroes.add(new Hero(_v_, this, "heroes"));
		nextkey = _o_.nextkey;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(heroes.size());
		for (xbean.Hero _v_ : heroes) {
			_v_.marshal(_os_);
		}
		_os_.marshal(nextkey);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			xbean.Hero _v_ = new Hero(0, this, "heroes");
			_v_.unmarshal(_os_);
			heroes.add(_v_);
		}
		nextkey = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.HeroColumn copy() {
		_xdb_verify_unsafe_();
		return new HeroColumn(this);
	}

	@Override
	public xbean.HeroColumn toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.HeroColumn toBean() {
		_xdb_verify_unsafe_();
		return new HeroColumn(this); // same as copy()
	}

	@Override
	public xbean.HeroColumn toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.HeroColumn toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.List<xbean.Hero> getHeroes() { // 
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "heroes"), heroes);
	}

	public java.util.List<xbean.Hero> getHeroesAsData() { // 
		_xdb_verify_unsafe_();
		java.util.List<xbean.Hero> heroes;
		HeroColumn _o_ = this;
		heroes = new java.util.LinkedList<xbean.Hero>();
		for (xbean.Hero _v_ : _o_.heroes)
			heroes.add(new Hero.Data(_v_));
		return heroes;
	}

	@Override
	public int getNextkey() { // 
		_xdb_verify_unsafe_();
		return nextkey;
	}

	@Override
	public void setNextkey(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "nextkey") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, nextkey) {
					public void rollback() { nextkey = _xdb_saved; }
				};}});
		nextkey = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		HeroColumn _o_ = null;
		if ( _o1_ instanceof HeroColumn ) _o_ = (HeroColumn)_o1_;
		else if ( _o1_ instanceof HeroColumn.Const ) _o_ = ((HeroColumn.Const)_o1_).nThis();
		else return false;
		if (!heroes.equals(_o_.heroes)) return false;
		if (nextkey != _o_.nextkey) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += heroes.hashCode();
		_h_ += nextkey;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(heroes);
		_sb_.append(",");
		_sb_.append(nextkey);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("heroes"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("nextkey"));
		return lb;
	}

	private class Const implements xbean.HeroColumn {
		HeroColumn nThis() {
			return HeroColumn.this;
		}

		@Override
		public xbean.HeroColumn copy() {
			return HeroColumn.this.copy();
		}

		@Override
		public xbean.HeroColumn toData() {
			return HeroColumn.this.toData();
		}

		public xbean.HeroColumn toBean() {
			return HeroColumn.this.toBean();
		}

		@Override
		public xbean.HeroColumn toDataIf() {
			return HeroColumn.this.toDataIf();
		}

		public xbean.HeroColumn toBeanIf() {
			return HeroColumn.this.toBeanIf();
		}

		@Override
		public java.util.List<xbean.Hero> getHeroes() { // 
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(heroes);
		}

		public java.util.List<xbean.Hero> getHeroesAsData() { // 
			_xdb_verify_unsafe_();
			java.util.List<xbean.Hero> heroes;
			HeroColumn _o_ = HeroColumn.this;
		heroes = new java.util.LinkedList<xbean.Hero>();
		for (xbean.Hero _v_ : _o_.heroes)
			heroes.add(new Hero.Data(_v_));
			return heroes;
		}

		@Override
		public int getNextkey() { // 
			_xdb_verify_unsafe_();
			return nextkey;
		}

		@Override
		public void setNextkey(int _v_) { // 
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
			return HeroColumn.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return HeroColumn.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return HeroColumn.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return HeroColumn.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return HeroColumn.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return HeroColumn.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return HeroColumn.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return HeroColumn.this.hashCode();
		}

		@Override
		public String toString() {
			return HeroColumn.this.toString();
		}

	}

	public static final class Data implements xbean.HeroColumn {
		private java.util.LinkedList<xbean.Hero> heroes; // 
		private int nextkey; // 

		public Data() {
			heroes = new java.util.LinkedList<xbean.Hero>();
		}

		Data(xbean.HeroColumn _o1_) {
			if (_o1_ instanceof HeroColumn) assign((HeroColumn)_o1_);
			else if (_o1_ instanceof HeroColumn.Data) assign((HeroColumn.Data)_o1_);
			else if (_o1_ instanceof HeroColumn.Const) assign(((HeroColumn.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(HeroColumn _o_) {
			heroes = new java.util.LinkedList<xbean.Hero>();
			for (xbean.Hero _v_ : _o_.heroes)
				heroes.add(new Hero.Data(_v_));
			nextkey = _o_.nextkey;
		}

		private void assign(HeroColumn.Data _o_) {
			heroes = new java.util.LinkedList<xbean.Hero>();
			for (xbean.Hero _v_ : _o_.heroes)
				heroes.add(new Hero.Data(_v_));
			nextkey = _o_.nextkey;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(heroes.size());
			for (xbean.Hero _v_ : heroes) {
				_v_.marshal(_os_);
			}
			_os_.marshal(nextkey);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				xbean.Hero _v_ = xbean.Pod.newHeroData();
				_v_.unmarshal(_os_);
				heroes.add(_v_);
			}
			nextkey = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.HeroColumn copy() {
			return new Data(this);
		}

		@Override
		public xbean.HeroColumn toData() {
			return new Data(this);
		}

		public xbean.HeroColumn toBean() {
			return new HeroColumn(this, null, null);
		}

		@Override
		public xbean.HeroColumn toDataIf() {
			return this;
		}

		public xbean.HeroColumn toBeanIf() {
			return new HeroColumn(this, null, null);
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
		public java.util.List<xbean.Hero> getHeroes() { // 
			return heroes;
		}

		@Override
		public java.util.List<xbean.Hero> getHeroesAsData() { // 
			return heroes;
		}

		@Override
		public int getNextkey() { // 
			return nextkey;
		}

		@Override
		public void setNextkey(int _v_) { // 
			nextkey = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof HeroColumn.Data)) return false;
			HeroColumn.Data _o_ = (HeroColumn.Data) _o1_;
			if (!heroes.equals(_o_.heroes)) return false;
			if (nextkey != _o_.nextkey) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += heroes.hashCode();
			_h_ += nextkey;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(heroes);
			_sb_.append(",");
			_sb_.append(nextkey);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
