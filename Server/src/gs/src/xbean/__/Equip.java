
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class Equip extends xdb.XBean implements xbean.Equip {
	private int key; // 物品唯一ID
	private int equipid; // 物品ID
	private int qianghualevel; // 强化等级
	private int attr1odds; // 属性1几率
	private int attr2odds; // 属性2几率
	private int qhadd; // 强化增加几率

	Equip(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
	}

	public Equip() {
		this(0, null, null);
	}

	public Equip(Equip _o_) {
		this(_o_, null, null);
	}

	Equip(xbean.Equip _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof Equip) assign((Equip)_o1_);
		else if (_o1_ instanceof Equip.Data) assign((Equip.Data)_o1_);
		else if (_o1_ instanceof Equip.Const) assign(((Equip.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(Equip _o_) {
		_o_._xdb_verify_unsafe_();
		key = _o_.key;
		equipid = _o_.equipid;
		qianghualevel = _o_.qianghualevel;
		attr1odds = _o_.attr1odds;
		attr2odds = _o_.attr2odds;
		qhadd = _o_.qhadd;
	}

	private void assign(Equip.Data _o_) {
		key = _o_.key;
		equipid = _o_.equipid;
		qianghualevel = _o_.qianghualevel;
		attr1odds = _o_.attr1odds;
		attr2odds = _o_.attr2odds;
		qhadd = _o_.qhadd;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(key);
		_os_.marshal(equipid);
		_os_.marshal(qianghualevel);
		_os_.marshal(attr1odds);
		_os_.marshal(attr2odds);
		_os_.marshal(qhadd);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		key = _os_.unmarshal_int();
		equipid = _os_.unmarshal_int();
		qianghualevel = _os_.unmarshal_int();
		attr1odds = _os_.unmarshal_int();
		attr2odds = _os_.unmarshal_int();
		qhadd = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.Equip copy() {
		_xdb_verify_unsafe_();
		return new Equip(this);
	}

	@Override
	public xbean.Equip toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.Equip toBean() {
		_xdb_verify_unsafe_();
		return new Equip(this); // same as copy()
	}

	@Override
	public xbean.Equip toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.Equip toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getKey() { // 物品唯一ID
		_xdb_verify_unsafe_();
		return key;
	}

	@Override
	public int getEquipid() { // 物品ID
		_xdb_verify_unsafe_();
		return equipid;
	}

	@Override
	public int getQianghualevel() { // 强化等级
		_xdb_verify_unsafe_();
		return qianghualevel;
	}

	@Override
	public int getAttr1odds() { // 属性1几率
		_xdb_verify_unsafe_();
		return attr1odds;
	}

	@Override
	public int getAttr2odds() { // 属性2几率
		_xdb_verify_unsafe_();
		return attr2odds;
	}

	@Override
	public int getQhadd() { // 强化增加几率
		_xdb_verify_unsafe_();
		return qhadd;
	}

	@Override
	public void setKey(int _v_) { // 物品唯一ID
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "key") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, key) {
					public void rollback() { key = _xdb_saved; }
				};}});
		key = _v_;
	}

	@Override
	public void setEquipid(int _v_) { // 物品ID
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "equipid") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, equipid) {
					public void rollback() { equipid = _xdb_saved; }
				};}});
		equipid = _v_;
	}

	@Override
	public void setQianghualevel(int _v_) { // 强化等级
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "qianghualevel") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, qianghualevel) {
					public void rollback() { qianghualevel = _xdb_saved; }
				};}});
		qianghualevel = _v_;
	}

	@Override
	public void setAttr1odds(int _v_) { // 属性1几率
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "attr1odds") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, attr1odds) {
					public void rollback() { attr1odds = _xdb_saved; }
				};}});
		attr1odds = _v_;
	}

	@Override
	public void setAttr2odds(int _v_) { // 属性2几率
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "attr2odds") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, attr2odds) {
					public void rollback() { attr2odds = _xdb_saved; }
				};}});
		attr2odds = _v_;
	}

	@Override
	public void setQhadd(int _v_) { // 强化增加几率
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "qhadd") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, qhadd) {
					public void rollback() { qhadd = _xdb_saved; }
				};}});
		qhadd = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		Equip _o_ = null;
		if ( _o1_ instanceof Equip ) _o_ = (Equip)_o1_;
		else if ( _o1_ instanceof Equip.Const ) _o_ = ((Equip.Const)_o1_).nThis();
		else return false;
		if (key != _o_.key) return false;
		if (equipid != _o_.equipid) return false;
		if (qianghualevel != _o_.qianghualevel) return false;
		if (attr1odds != _o_.attr1odds) return false;
		if (attr2odds != _o_.attr2odds) return false;
		if (qhadd != _o_.qhadd) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += key;
		_h_ += equipid;
		_h_ += qianghualevel;
		_h_ += attr1odds;
		_h_ += attr2odds;
		_h_ += qhadd;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(key);
		_sb_.append(",");
		_sb_.append(equipid);
		_sb_.append(",");
		_sb_.append(qianghualevel);
		_sb_.append(",");
		_sb_.append(attr1odds);
		_sb_.append(",");
		_sb_.append(attr2odds);
		_sb_.append(",");
		_sb_.append(qhadd);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("key"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("equipid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("qianghualevel"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("attr1odds"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("attr2odds"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("qhadd"));
		return lb;
	}

	private class Const implements xbean.Equip {
		Equip nThis() {
			return Equip.this;
		}

		@Override
		public xbean.Equip copy() {
			return Equip.this.copy();
		}

		@Override
		public xbean.Equip toData() {
			return Equip.this.toData();
		}

		public xbean.Equip toBean() {
			return Equip.this.toBean();
		}

		@Override
		public xbean.Equip toDataIf() {
			return Equip.this.toDataIf();
		}

		public xbean.Equip toBeanIf() {
			return Equip.this.toBeanIf();
		}

		@Override
		public int getKey() { // 物品唯一ID
			_xdb_verify_unsafe_();
			return key;
		}

		@Override
		public int getEquipid() { // 物品ID
			_xdb_verify_unsafe_();
			return equipid;
		}

		@Override
		public int getQianghualevel() { // 强化等级
			_xdb_verify_unsafe_();
			return qianghualevel;
		}

		@Override
		public int getAttr1odds() { // 属性1几率
			_xdb_verify_unsafe_();
			return attr1odds;
		}

		@Override
		public int getAttr2odds() { // 属性2几率
			_xdb_verify_unsafe_();
			return attr2odds;
		}

		@Override
		public int getQhadd() { // 强化增加几率
			_xdb_verify_unsafe_();
			return qhadd;
		}

		@Override
		public void setKey(int _v_) { // 物品唯一ID
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setEquipid(int _v_) { // 物品ID
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setQianghualevel(int _v_) { // 强化等级
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setAttr1odds(int _v_) { // 属性1几率
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setAttr2odds(int _v_) { // 属性2几率
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setQhadd(int _v_) { // 强化增加几率
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
			return Equip.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return Equip.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return Equip.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return Equip.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return Equip.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return Equip.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return Equip.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return Equip.this.hashCode();
		}

		@Override
		public String toString() {
			return Equip.this.toString();
		}

	}

	public static final class Data implements xbean.Equip {
		private int key; // 物品唯一ID
		private int equipid; // 物品ID
		private int qianghualevel; // 强化等级
		private int attr1odds; // 属性1几率
		private int attr2odds; // 属性2几率
		private int qhadd; // 强化增加几率

		public Data() {
		}

		Data(xbean.Equip _o1_) {
			if (_o1_ instanceof Equip) assign((Equip)_o1_);
			else if (_o1_ instanceof Equip.Data) assign((Equip.Data)_o1_);
			else if (_o1_ instanceof Equip.Const) assign(((Equip.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(Equip _o_) {
			key = _o_.key;
			equipid = _o_.equipid;
			qianghualevel = _o_.qianghualevel;
			attr1odds = _o_.attr1odds;
			attr2odds = _o_.attr2odds;
			qhadd = _o_.qhadd;
		}

		private void assign(Equip.Data _o_) {
			key = _o_.key;
			equipid = _o_.equipid;
			qianghualevel = _o_.qianghualevel;
			attr1odds = _o_.attr1odds;
			attr2odds = _o_.attr2odds;
			qhadd = _o_.qhadd;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(key);
			_os_.marshal(equipid);
			_os_.marshal(qianghualevel);
			_os_.marshal(attr1odds);
			_os_.marshal(attr2odds);
			_os_.marshal(qhadd);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			key = _os_.unmarshal_int();
			equipid = _os_.unmarshal_int();
			qianghualevel = _os_.unmarshal_int();
			attr1odds = _os_.unmarshal_int();
			attr2odds = _os_.unmarshal_int();
			qhadd = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.Equip copy() {
			return new Data(this);
		}

		@Override
		public xbean.Equip toData() {
			return new Data(this);
		}

		public xbean.Equip toBean() {
			return new Equip(this, null, null);
		}

		@Override
		public xbean.Equip toDataIf() {
			return this;
		}

		public xbean.Equip toBeanIf() {
			return new Equip(this, null, null);
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
		public int getKey() { // 物品唯一ID
			return key;
		}

		@Override
		public int getEquipid() { // 物品ID
			return equipid;
		}

		@Override
		public int getQianghualevel() { // 强化等级
			return qianghualevel;
		}

		@Override
		public int getAttr1odds() { // 属性1几率
			return attr1odds;
		}

		@Override
		public int getAttr2odds() { // 属性2几率
			return attr2odds;
		}

		@Override
		public int getQhadd() { // 强化增加几率
			return qhadd;
		}

		@Override
		public void setKey(int _v_) { // 物品唯一ID
			key = _v_;
		}

		@Override
		public void setEquipid(int _v_) { // 物品ID
			equipid = _v_;
		}

		@Override
		public void setQianghualevel(int _v_) { // 强化等级
			qianghualevel = _v_;
		}

		@Override
		public void setAttr1odds(int _v_) { // 属性1几率
			attr1odds = _v_;
		}

		@Override
		public void setAttr2odds(int _v_) { // 属性2几率
			attr2odds = _v_;
		}

		@Override
		public void setQhadd(int _v_) { // 强化增加几率
			qhadd = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof Equip.Data)) return false;
			Equip.Data _o_ = (Equip.Data) _o1_;
			if (key != _o_.key) return false;
			if (equipid != _o_.equipid) return false;
			if (qianghualevel != _o_.qianghualevel) return false;
			if (attr1odds != _o_.attr1odds) return false;
			if (attr2odds != _o_.attr2odds) return false;
			if (qhadd != _o_.qhadd) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += key;
			_h_ += equipid;
			_h_ += qianghualevel;
			_h_ += attr1odds;
			_h_ += attr2odds;
			_h_ += qhadd;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(key);
			_sb_.append(",");
			_sb_.append(equipid);
			_sb_.append(",");
			_sb_.append(qianghualevel);
			_sb_.append(",");
			_sb_.append(attr1odds);
			_sb_.append(",");
			_sb_.append(attr2odds);
			_sb_.append(",");
			_sb_.append(qhadd);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
