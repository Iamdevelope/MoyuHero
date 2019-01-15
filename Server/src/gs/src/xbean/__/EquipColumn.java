
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class EquipColumn extends xdb.XBean implements xbean.EquipColumn {
	private java.util.LinkedList<xbean.Equip> equips; // 
	private int nextkey; // 

	EquipColumn(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		equips = new java.util.LinkedList<xbean.Equip>();
	}

	public EquipColumn() {
		this(0, null, null);
	}

	public EquipColumn(EquipColumn _o_) {
		this(_o_, null, null);
	}

	EquipColumn(xbean.EquipColumn _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof EquipColumn) assign((EquipColumn)_o1_);
		else if (_o1_ instanceof EquipColumn.Data) assign((EquipColumn.Data)_o1_);
		else if (_o1_ instanceof EquipColumn.Const) assign(((EquipColumn.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(EquipColumn _o_) {
		_o_._xdb_verify_unsafe_();
		equips = new java.util.LinkedList<xbean.Equip>();
		for (xbean.Equip _v_ : _o_.equips)
			equips.add(new Equip(_v_, this, "equips"));
		nextkey = _o_.nextkey;
	}

	private void assign(EquipColumn.Data _o_) {
		equips = new java.util.LinkedList<xbean.Equip>();
		for (xbean.Equip _v_ : _o_.equips)
			equips.add(new Equip(_v_, this, "equips"));
		nextkey = _o_.nextkey;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(equips.size());
		for (xbean.Equip _v_ : equips) {
			_v_.marshal(_os_);
		}
		_os_.marshal(nextkey);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			xbean.Equip _v_ = new Equip(0, this, "equips");
			_v_.unmarshal(_os_);
			equips.add(_v_);
		}
		nextkey = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.EquipColumn copy() {
		_xdb_verify_unsafe_();
		return new EquipColumn(this);
	}

	@Override
	public xbean.EquipColumn toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.EquipColumn toBean() {
		_xdb_verify_unsafe_();
		return new EquipColumn(this); // same as copy()
	}

	@Override
	public xbean.EquipColumn toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.EquipColumn toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.List<xbean.Equip> getEquips() { // 
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "equips"), equips);
	}

	public java.util.List<xbean.Equip> getEquipsAsData() { // 
		_xdb_verify_unsafe_();
		java.util.List<xbean.Equip> equips;
		EquipColumn _o_ = this;
		equips = new java.util.LinkedList<xbean.Equip>();
		for (xbean.Equip _v_ : _o_.equips)
			equips.add(new Equip.Data(_v_));
		return equips;
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
		EquipColumn _o_ = null;
		if ( _o1_ instanceof EquipColumn ) _o_ = (EquipColumn)_o1_;
		else if ( _o1_ instanceof EquipColumn.Const ) _o_ = ((EquipColumn.Const)_o1_).nThis();
		else return false;
		if (!equips.equals(_o_.equips)) return false;
		if (nextkey != _o_.nextkey) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += equips.hashCode();
		_h_ += nextkey;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(equips);
		_sb_.append(",");
		_sb_.append(nextkey);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("equips"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("nextkey"));
		return lb;
	}

	private class Const implements xbean.EquipColumn {
		EquipColumn nThis() {
			return EquipColumn.this;
		}

		@Override
		public xbean.EquipColumn copy() {
			return EquipColumn.this.copy();
		}

		@Override
		public xbean.EquipColumn toData() {
			return EquipColumn.this.toData();
		}

		public xbean.EquipColumn toBean() {
			return EquipColumn.this.toBean();
		}

		@Override
		public xbean.EquipColumn toDataIf() {
			return EquipColumn.this.toDataIf();
		}

		public xbean.EquipColumn toBeanIf() {
			return EquipColumn.this.toBeanIf();
		}

		@Override
		public java.util.List<xbean.Equip> getEquips() { // 
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(equips);
		}

		public java.util.List<xbean.Equip> getEquipsAsData() { // 
			_xdb_verify_unsafe_();
			java.util.List<xbean.Equip> equips;
			EquipColumn _o_ = EquipColumn.this;
		equips = new java.util.LinkedList<xbean.Equip>();
		for (xbean.Equip _v_ : _o_.equips)
			equips.add(new Equip.Data(_v_));
			return equips;
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
			return EquipColumn.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return EquipColumn.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return EquipColumn.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return EquipColumn.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return EquipColumn.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return EquipColumn.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return EquipColumn.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return EquipColumn.this.hashCode();
		}

		@Override
		public String toString() {
			return EquipColumn.this.toString();
		}

	}

	public static final class Data implements xbean.EquipColumn {
		private java.util.LinkedList<xbean.Equip> equips; // 
		private int nextkey; // 

		public Data() {
			equips = new java.util.LinkedList<xbean.Equip>();
		}

		Data(xbean.EquipColumn _o1_) {
			if (_o1_ instanceof EquipColumn) assign((EquipColumn)_o1_);
			else if (_o1_ instanceof EquipColumn.Data) assign((EquipColumn.Data)_o1_);
			else if (_o1_ instanceof EquipColumn.Const) assign(((EquipColumn.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(EquipColumn _o_) {
			equips = new java.util.LinkedList<xbean.Equip>();
			for (xbean.Equip _v_ : _o_.equips)
				equips.add(new Equip.Data(_v_));
			nextkey = _o_.nextkey;
		}

		private void assign(EquipColumn.Data _o_) {
			equips = new java.util.LinkedList<xbean.Equip>();
			for (xbean.Equip _v_ : _o_.equips)
				equips.add(new Equip.Data(_v_));
			nextkey = _o_.nextkey;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(equips.size());
			for (xbean.Equip _v_ : equips) {
				_v_.marshal(_os_);
			}
			_os_.marshal(nextkey);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				xbean.Equip _v_ = xbean.Pod.newEquipData();
				_v_.unmarshal(_os_);
				equips.add(_v_);
			}
			nextkey = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.EquipColumn copy() {
			return new Data(this);
		}

		@Override
		public xbean.EquipColumn toData() {
			return new Data(this);
		}

		public xbean.EquipColumn toBean() {
			return new EquipColumn(this, null, null);
		}

		@Override
		public xbean.EquipColumn toDataIf() {
			return this;
		}

		public xbean.EquipColumn toBeanIf() {
			return new EquipColumn(this, null, null);
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
		public java.util.List<xbean.Equip> getEquips() { // 
			return equips;
		}

		@Override
		public java.util.List<xbean.Equip> getEquipsAsData() { // 
			return equips;
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
			if (!(_o1_ instanceof EquipColumn.Data)) return false;
			EquipColumn.Data _o_ = (EquipColumn.Data) _o1_;
			if (!equips.equals(_o_.equips)) return false;
			if (nextkey != _o_.nextkey) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += equips.hashCode();
			_h_ += nextkey;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(equips);
			_sb_.append(",");
			_sb_.append(nextkey);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
