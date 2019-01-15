
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class HeroSkinColumn extends xdb.XBean implements xbean.HeroSkinColumn {
	private java.util.LinkedList<xbean.HeroSkin> heroskins; // 

	HeroSkinColumn(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		heroskins = new java.util.LinkedList<xbean.HeroSkin>();
	}

	public HeroSkinColumn() {
		this(0, null, null);
	}

	public HeroSkinColumn(HeroSkinColumn _o_) {
		this(_o_, null, null);
	}

	HeroSkinColumn(xbean.HeroSkinColumn _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof HeroSkinColumn) assign((HeroSkinColumn)_o1_);
		else if (_o1_ instanceof HeroSkinColumn.Data) assign((HeroSkinColumn.Data)_o1_);
		else if (_o1_ instanceof HeroSkinColumn.Const) assign(((HeroSkinColumn.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(HeroSkinColumn _o_) {
		_o_._xdb_verify_unsafe_();
		heroskins = new java.util.LinkedList<xbean.HeroSkin>();
		for (xbean.HeroSkin _v_ : _o_.heroskins)
			heroskins.add(new HeroSkin(_v_, this, "heroskins"));
	}

	private void assign(HeroSkinColumn.Data _o_) {
		heroskins = new java.util.LinkedList<xbean.HeroSkin>();
		for (xbean.HeroSkin _v_ : _o_.heroskins)
			heroskins.add(new HeroSkin(_v_, this, "heroskins"));
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(heroskins.size());
		for (xbean.HeroSkin _v_ : heroskins) {
			_v_.marshal(_os_);
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			xbean.HeroSkin _v_ = new HeroSkin(0, this, "heroskins");
			_v_.unmarshal(_os_);
			heroskins.add(_v_);
		}
		return _os_;
	}

	@Override
	public xbean.HeroSkinColumn copy() {
		_xdb_verify_unsafe_();
		return new HeroSkinColumn(this);
	}

	@Override
	public xbean.HeroSkinColumn toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.HeroSkinColumn toBean() {
		_xdb_verify_unsafe_();
		return new HeroSkinColumn(this); // same as copy()
	}

	@Override
	public xbean.HeroSkinColumn toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.HeroSkinColumn toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.List<xbean.HeroSkin> getHeroskins() { // 
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "heroskins"), heroskins);
	}

	public java.util.List<xbean.HeroSkin> getHeroskinsAsData() { // 
		_xdb_verify_unsafe_();
		java.util.List<xbean.HeroSkin> heroskins;
		HeroSkinColumn _o_ = this;
		heroskins = new java.util.LinkedList<xbean.HeroSkin>();
		for (xbean.HeroSkin _v_ : _o_.heroskins)
			heroskins.add(new HeroSkin.Data(_v_));
		return heroskins;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		HeroSkinColumn _o_ = null;
		if ( _o1_ instanceof HeroSkinColumn ) _o_ = (HeroSkinColumn)_o1_;
		else if ( _o1_ instanceof HeroSkinColumn.Const ) _o_ = ((HeroSkinColumn.Const)_o1_).nThis();
		else return false;
		if (!heroskins.equals(_o_.heroskins)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += heroskins.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(heroskins);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("heroskins"));
		return lb;
	}

	private class Const implements xbean.HeroSkinColumn {
		HeroSkinColumn nThis() {
			return HeroSkinColumn.this;
		}

		@Override
		public xbean.HeroSkinColumn copy() {
			return HeroSkinColumn.this.copy();
		}

		@Override
		public xbean.HeroSkinColumn toData() {
			return HeroSkinColumn.this.toData();
		}

		public xbean.HeroSkinColumn toBean() {
			return HeroSkinColumn.this.toBean();
		}

		@Override
		public xbean.HeroSkinColumn toDataIf() {
			return HeroSkinColumn.this.toDataIf();
		}

		public xbean.HeroSkinColumn toBeanIf() {
			return HeroSkinColumn.this.toBeanIf();
		}

		@Override
		public java.util.List<xbean.HeroSkin> getHeroskins() { // 
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(heroskins);
		}

		public java.util.List<xbean.HeroSkin> getHeroskinsAsData() { // 
			_xdb_verify_unsafe_();
			java.util.List<xbean.HeroSkin> heroskins;
			HeroSkinColumn _o_ = HeroSkinColumn.this;
		heroskins = new java.util.LinkedList<xbean.HeroSkin>();
		for (xbean.HeroSkin _v_ : _o_.heroskins)
			heroskins.add(new HeroSkin.Data(_v_));
			return heroskins;
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
			return HeroSkinColumn.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return HeroSkinColumn.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return HeroSkinColumn.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return HeroSkinColumn.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return HeroSkinColumn.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return HeroSkinColumn.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return HeroSkinColumn.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return HeroSkinColumn.this.hashCode();
		}

		@Override
		public String toString() {
			return HeroSkinColumn.this.toString();
		}

	}

	public static final class Data implements xbean.HeroSkinColumn {
		private java.util.LinkedList<xbean.HeroSkin> heroskins; // 

		public Data() {
			heroskins = new java.util.LinkedList<xbean.HeroSkin>();
		}

		Data(xbean.HeroSkinColumn _o1_) {
			if (_o1_ instanceof HeroSkinColumn) assign((HeroSkinColumn)_o1_);
			else if (_o1_ instanceof HeroSkinColumn.Data) assign((HeroSkinColumn.Data)_o1_);
			else if (_o1_ instanceof HeroSkinColumn.Const) assign(((HeroSkinColumn.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(HeroSkinColumn _o_) {
			heroskins = new java.util.LinkedList<xbean.HeroSkin>();
			for (xbean.HeroSkin _v_ : _o_.heroskins)
				heroskins.add(new HeroSkin.Data(_v_));
		}

		private void assign(HeroSkinColumn.Data _o_) {
			heroskins = new java.util.LinkedList<xbean.HeroSkin>();
			for (xbean.HeroSkin _v_ : _o_.heroskins)
				heroskins.add(new HeroSkin.Data(_v_));
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(heroskins.size());
			for (xbean.HeroSkin _v_ : heroskins) {
				_v_.marshal(_os_);
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				xbean.HeroSkin _v_ = xbean.Pod.newHeroSkinData();
				_v_.unmarshal(_os_);
				heroskins.add(_v_);
			}
			return _os_;
		}

		@Override
		public xbean.HeroSkinColumn copy() {
			return new Data(this);
		}

		@Override
		public xbean.HeroSkinColumn toData() {
			return new Data(this);
		}

		public xbean.HeroSkinColumn toBean() {
			return new HeroSkinColumn(this, null, null);
		}

		@Override
		public xbean.HeroSkinColumn toDataIf() {
			return this;
		}

		public xbean.HeroSkinColumn toBeanIf() {
			return new HeroSkinColumn(this, null, null);
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
		public java.util.List<xbean.HeroSkin> getHeroskins() { // 
			return heroskins;
		}

		@Override
		public java.util.List<xbean.HeroSkin> getHeroskinsAsData() { // 
			return heroskins;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof HeroSkinColumn.Data)) return false;
			HeroSkinColumn.Data _o_ = (HeroSkinColumn.Data) _o1_;
			if (!heroskins.equals(_o_.heroskins)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += heroskins.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(heroskins);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
