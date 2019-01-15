
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class EquipExtData extends xdb.XBean implements xbean.EquipExtData {
	private int level; // 强化等级
	private int init1; // 基础属性1，默认-1
	private int init2; // 基础属性2，默认-1
	private int init3; // 基础属性3，默认-1
	private int attr1; // 附属属性1，默认-1
	private int attr2; // 附属属性2，默认-1
	private int attr3; // 附属属性3，默认-1
	private int attr4; // 附属属性4，默认-1

	EquipExtData(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		level = 0;
		init1 = -1;
		init2 = -1;
		init3 = -1;
		attr1 = -1;
		attr2 = -1;
		attr3 = -1;
		attr4 = -1;
	}

	public EquipExtData() {
		this(0, null, null);
	}

	public EquipExtData(EquipExtData _o_) {
		this(_o_, null, null);
	}

	EquipExtData(xbean.EquipExtData _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof EquipExtData) assign((EquipExtData)_o1_);
		else if (_o1_ instanceof EquipExtData.Data) assign((EquipExtData.Data)_o1_);
		else if (_o1_ instanceof EquipExtData.Const) assign(((EquipExtData.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(EquipExtData _o_) {
		_o_._xdb_verify_unsafe_();
		level = _o_.level;
		init1 = _o_.init1;
		init2 = _o_.init2;
		init3 = _o_.init3;
		attr1 = _o_.attr1;
		attr2 = _o_.attr2;
		attr3 = _o_.attr3;
		attr4 = _o_.attr4;
	}

	private void assign(EquipExtData.Data _o_) {
		level = _o_.level;
		init1 = _o_.init1;
		init2 = _o_.init2;
		init3 = _o_.init3;
		attr1 = _o_.attr1;
		attr2 = _o_.attr2;
		attr3 = _o_.attr3;
		attr4 = _o_.attr4;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(level);
		_os_.marshal(init1);
		_os_.marshal(init2);
		_os_.marshal(init3);
		_os_.marshal(attr1);
		_os_.marshal(attr2);
		_os_.marshal(attr3);
		_os_.marshal(attr4);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		level = _os_.unmarshal_int();
		init1 = _os_.unmarshal_int();
		init2 = _os_.unmarshal_int();
		init3 = _os_.unmarshal_int();
		attr1 = _os_.unmarshal_int();
		attr2 = _os_.unmarshal_int();
		attr3 = _os_.unmarshal_int();
		attr4 = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.EquipExtData copy() {
		_xdb_verify_unsafe_();
		return new EquipExtData(this);
	}

	@Override
	public xbean.EquipExtData toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.EquipExtData toBean() {
		_xdb_verify_unsafe_();
		return new EquipExtData(this); // same as copy()
	}

	@Override
	public xbean.EquipExtData toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.EquipExtData toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getLevel() { // 强化等级
		_xdb_verify_unsafe_();
		return level;
	}

	@Override
	public int getInit1() { // 基础属性1，默认-1
		_xdb_verify_unsafe_();
		return init1;
	}

	@Override
	public int getInit2() { // 基础属性2，默认-1
		_xdb_verify_unsafe_();
		return init2;
	}

	@Override
	public int getInit3() { // 基础属性3，默认-1
		_xdb_verify_unsafe_();
		return init3;
	}

	@Override
	public int getAttr1() { // 附属属性1，默认-1
		_xdb_verify_unsafe_();
		return attr1;
	}

	@Override
	public int getAttr2() { // 附属属性2，默认-1
		_xdb_verify_unsafe_();
		return attr2;
	}

	@Override
	public int getAttr3() { // 附属属性3，默认-1
		_xdb_verify_unsafe_();
		return attr3;
	}

	@Override
	public int getAttr4() { // 附属属性4，默认-1
		_xdb_verify_unsafe_();
		return attr4;
	}

	@Override
	public void setLevel(int _v_) { // 强化等级
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "level") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, level) {
					public void rollback() { level = _xdb_saved; }
				};}});
		level = _v_;
	}

	@Override
	public void setInit1(int _v_) { // 基础属性1，默认-1
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "init1") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, init1) {
					public void rollback() { init1 = _xdb_saved; }
				};}});
		init1 = _v_;
	}

	@Override
	public void setInit2(int _v_) { // 基础属性2，默认-1
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "init2") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, init2) {
					public void rollback() { init2 = _xdb_saved; }
				};}});
		init2 = _v_;
	}

	@Override
	public void setInit3(int _v_) { // 基础属性3，默认-1
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "init3") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, init3) {
					public void rollback() { init3 = _xdb_saved; }
				};}});
		init3 = _v_;
	}

	@Override
	public void setAttr1(int _v_) { // 附属属性1，默认-1
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "attr1") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, attr1) {
					public void rollback() { attr1 = _xdb_saved; }
				};}});
		attr1 = _v_;
	}

	@Override
	public void setAttr2(int _v_) { // 附属属性2，默认-1
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "attr2") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, attr2) {
					public void rollback() { attr2 = _xdb_saved; }
				};}});
		attr2 = _v_;
	}

	@Override
	public void setAttr3(int _v_) { // 附属属性3，默认-1
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "attr3") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, attr3) {
					public void rollback() { attr3 = _xdb_saved; }
				};}});
		attr3 = _v_;
	}

	@Override
	public void setAttr4(int _v_) { // 附属属性4，默认-1
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "attr4") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, attr4) {
					public void rollback() { attr4 = _xdb_saved; }
				};}});
		attr4 = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		EquipExtData _o_ = null;
		if ( _o1_ instanceof EquipExtData ) _o_ = (EquipExtData)_o1_;
		else if ( _o1_ instanceof EquipExtData.Const ) _o_ = ((EquipExtData.Const)_o1_).nThis();
		else return false;
		if (level != _o_.level) return false;
		if (init1 != _o_.init1) return false;
		if (init2 != _o_.init2) return false;
		if (init3 != _o_.init3) return false;
		if (attr1 != _o_.attr1) return false;
		if (attr2 != _o_.attr2) return false;
		if (attr3 != _o_.attr3) return false;
		if (attr4 != _o_.attr4) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += level;
		_h_ += init1;
		_h_ += init2;
		_h_ += init3;
		_h_ += attr1;
		_h_ += attr2;
		_h_ += attr3;
		_h_ += attr4;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(level);
		_sb_.append(",");
		_sb_.append(init1);
		_sb_.append(",");
		_sb_.append(init2);
		_sb_.append(",");
		_sb_.append(init3);
		_sb_.append(",");
		_sb_.append(attr1);
		_sb_.append(",");
		_sb_.append(attr2);
		_sb_.append(",");
		_sb_.append(attr3);
		_sb_.append(",");
		_sb_.append(attr4);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("level"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("init1"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("init2"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("init3"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("attr1"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("attr2"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("attr3"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("attr4"));
		return lb;
	}

	private class Const implements xbean.EquipExtData {
		EquipExtData nThis() {
			return EquipExtData.this;
		}

		@Override
		public xbean.EquipExtData copy() {
			return EquipExtData.this.copy();
		}

		@Override
		public xbean.EquipExtData toData() {
			return EquipExtData.this.toData();
		}

		public xbean.EquipExtData toBean() {
			return EquipExtData.this.toBean();
		}

		@Override
		public xbean.EquipExtData toDataIf() {
			return EquipExtData.this.toDataIf();
		}

		public xbean.EquipExtData toBeanIf() {
			return EquipExtData.this.toBeanIf();
		}

		@Override
		public int getLevel() { // 强化等级
			_xdb_verify_unsafe_();
			return level;
		}

		@Override
		public int getInit1() { // 基础属性1，默认-1
			_xdb_verify_unsafe_();
			return init1;
		}

		@Override
		public int getInit2() { // 基础属性2，默认-1
			_xdb_verify_unsafe_();
			return init2;
		}

		@Override
		public int getInit3() { // 基础属性3，默认-1
			_xdb_verify_unsafe_();
			return init3;
		}

		@Override
		public int getAttr1() { // 附属属性1，默认-1
			_xdb_verify_unsafe_();
			return attr1;
		}

		@Override
		public int getAttr2() { // 附属属性2，默认-1
			_xdb_verify_unsafe_();
			return attr2;
		}

		@Override
		public int getAttr3() { // 附属属性3，默认-1
			_xdb_verify_unsafe_();
			return attr3;
		}

		@Override
		public int getAttr4() { // 附属属性4，默认-1
			_xdb_verify_unsafe_();
			return attr4;
		}

		@Override
		public void setLevel(int _v_) { // 强化等级
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setInit1(int _v_) { // 基础属性1，默认-1
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setInit2(int _v_) { // 基础属性2，默认-1
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setInit3(int _v_) { // 基础属性3，默认-1
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setAttr1(int _v_) { // 附属属性1，默认-1
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setAttr2(int _v_) { // 附属属性2，默认-1
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setAttr3(int _v_) { // 附属属性3，默认-1
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setAttr4(int _v_) { // 附属属性4，默认-1
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
			return EquipExtData.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return EquipExtData.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return EquipExtData.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return EquipExtData.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return EquipExtData.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return EquipExtData.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return EquipExtData.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return EquipExtData.this.hashCode();
		}

		@Override
		public String toString() {
			return EquipExtData.this.toString();
		}

	}

	public static final class Data implements xbean.EquipExtData {
		private int level; // 强化等级
		private int init1; // 基础属性1，默认-1
		private int init2; // 基础属性2，默认-1
		private int init3; // 基础属性3，默认-1
		private int attr1; // 附属属性1，默认-1
		private int attr2; // 附属属性2，默认-1
		private int attr3; // 附属属性3，默认-1
		private int attr4; // 附属属性4，默认-1

		public Data() {
			level = 0;
			init1 = -1;
			init2 = -1;
			init3 = -1;
			attr1 = -1;
			attr2 = -1;
			attr3 = -1;
			attr4 = -1;
		}

		Data(xbean.EquipExtData _o1_) {
			if (_o1_ instanceof EquipExtData) assign((EquipExtData)_o1_);
			else if (_o1_ instanceof EquipExtData.Data) assign((EquipExtData.Data)_o1_);
			else if (_o1_ instanceof EquipExtData.Const) assign(((EquipExtData.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(EquipExtData _o_) {
			level = _o_.level;
			init1 = _o_.init1;
			init2 = _o_.init2;
			init3 = _o_.init3;
			attr1 = _o_.attr1;
			attr2 = _o_.attr2;
			attr3 = _o_.attr3;
			attr4 = _o_.attr4;
		}

		private void assign(EquipExtData.Data _o_) {
			level = _o_.level;
			init1 = _o_.init1;
			init2 = _o_.init2;
			init3 = _o_.init3;
			attr1 = _o_.attr1;
			attr2 = _o_.attr2;
			attr3 = _o_.attr3;
			attr4 = _o_.attr4;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(level);
			_os_.marshal(init1);
			_os_.marshal(init2);
			_os_.marshal(init3);
			_os_.marshal(attr1);
			_os_.marshal(attr2);
			_os_.marshal(attr3);
			_os_.marshal(attr4);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			level = _os_.unmarshal_int();
			init1 = _os_.unmarshal_int();
			init2 = _os_.unmarshal_int();
			init3 = _os_.unmarshal_int();
			attr1 = _os_.unmarshal_int();
			attr2 = _os_.unmarshal_int();
			attr3 = _os_.unmarshal_int();
			attr4 = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.EquipExtData copy() {
			return new Data(this);
		}

		@Override
		public xbean.EquipExtData toData() {
			return new Data(this);
		}

		public xbean.EquipExtData toBean() {
			return new EquipExtData(this, null, null);
		}

		@Override
		public xbean.EquipExtData toDataIf() {
			return this;
		}

		public xbean.EquipExtData toBeanIf() {
			return new EquipExtData(this, null, null);
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
		public int getLevel() { // 强化等级
			return level;
		}

		@Override
		public int getInit1() { // 基础属性1，默认-1
			return init1;
		}

		@Override
		public int getInit2() { // 基础属性2，默认-1
			return init2;
		}

		@Override
		public int getInit3() { // 基础属性3，默认-1
			return init3;
		}

		@Override
		public int getAttr1() { // 附属属性1，默认-1
			return attr1;
		}

		@Override
		public int getAttr2() { // 附属属性2，默认-1
			return attr2;
		}

		@Override
		public int getAttr3() { // 附属属性3，默认-1
			return attr3;
		}

		@Override
		public int getAttr4() { // 附属属性4，默认-1
			return attr4;
		}

		@Override
		public void setLevel(int _v_) { // 强化等级
			level = _v_;
		}

		@Override
		public void setInit1(int _v_) { // 基础属性1，默认-1
			init1 = _v_;
		}

		@Override
		public void setInit2(int _v_) { // 基础属性2，默认-1
			init2 = _v_;
		}

		@Override
		public void setInit3(int _v_) { // 基础属性3，默认-1
			init3 = _v_;
		}

		@Override
		public void setAttr1(int _v_) { // 附属属性1，默认-1
			attr1 = _v_;
		}

		@Override
		public void setAttr2(int _v_) { // 附属属性2，默认-1
			attr2 = _v_;
		}

		@Override
		public void setAttr3(int _v_) { // 附属属性3，默认-1
			attr3 = _v_;
		}

		@Override
		public void setAttr4(int _v_) { // 附属属性4，默认-1
			attr4 = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof EquipExtData.Data)) return false;
			EquipExtData.Data _o_ = (EquipExtData.Data) _o1_;
			if (level != _o_.level) return false;
			if (init1 != _o_.init1) return false;
			if (init2 != _o_.init2) return false;
			if (init3 != _o_.init3) return false;
			if (attr1 != _o_.attr1) return false;
			if (attr2 != _o_.attr2) return false;
			if (attr3 != _o_.attr3) return false;
			if (attr4 != _o_.attr4) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += level;
			_h_ += init1;
			_h_ += init2;
			_h_ += init3;
			_h_ += attr1;
			_h_ += attr2;
			_h_ += attr3;
			_h_ += attr4;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(level);
			_sb_.append(",");
			_sb_.append(init1);
			_sb_.append(",");
			_sb_.append(init2);
			_sb_.append(",");
			_sb_.append(init3);
			_sb_.append(",");
			_sb_.append(attr1);
			_sb_.append(",");
			_sb_.append(attr2);
			_sb_.append(",");
			_sb_.append(attr3);
			_sb_.append(",");
			_sb_.append(attr4);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
