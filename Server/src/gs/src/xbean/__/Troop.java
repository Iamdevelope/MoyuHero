
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class Troop extends xdb.XBean implements xbean.Troop {
	private int troopnum; // 战队编号
	private int trooptype; // 战队类型，1为前2后3，2为前3后2
	private int location1; // 0没装
	private int location2; // 0没装
	private int location3; // 0没装
	private int location4; // 0没装
	private int location5; // 0没装
	private int sh1; // 神魂1号，0没装
	private int sh2; // 神魂2号，0没装
	private int sh3; // 神魂3号，0没装
	private int sh4; // 神魂4号，0没装

	Troop(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
	}

	public Troop() {
		this(0, null, null);
	}

	public Troop(Troop _o_) {
		this(_o_, null, null);
	}

	Troop(xbean.Troop _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof Troop) assign((Troop)_o1_);
		else if (_o1_ instanceof Troop.Data) assign((Troop.Data)_o1_);
		else if (_o1_ instanceof Troop.Const) assign(((Troop.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(Troop _o_) {
		_o_._xdb_verify_unsafe_();
		troopnum = _o_.troopnum;
		trooptype = _o_.trooptype;
		location1 = _o_.location1;
		location2 = _o_.location2;
		location3 = _o_.location3;
		location4 = _o_.location4;
		location5 = _o_.location5;
		sh1 = _o_.sh1;
		sh2 = _o_.sh2;
		sh3 = _o_.sh3;
		sh4 = _o_.sh4;
	}

	private void assign(Troop.Data _o_) {
		troopnum = _o_.troopnum;
		trooptype = _o_.trooptype;
		location1 = _o_.location1;
		location2 = _o_.location2;
		location3 = _o_.location3;
		location4 = _o_.location4;
		location5 = _o_.location5;
		sh1 = _o_.sh1;
		sh2 = _o_.sh2;
		sh3 = _o_.sh3;
		sh4 = _o_.sh4;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(troopnum);
		_os_.marshal(trooptype);
		_os_.marshal(location1);
		_os_.marshal(location2);
		_os_.marshal(location3);
		_os_.marshal(location4);
		_os_.marshal(location5);
		_os_.marshal(sh1);
		_os_.marshal(sh2);
		_os_.marshal(sh3);
		_os_.marshal(sh4);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		troopnum = _os_.unmarshal_int();
		trooptype = _os_.unmarshal_int();
		location1 = _os_.unmarshal_int();
		location2 = _os_.unmarshal_int();
		location3 = _os_.unmarshal_int();
		location4 = _os_.unmarshal_int();
		location5 = _os_.unmarshal_int();
		sh1 = _os_.unmarshal_int();
		sh2 = _os_.unmarshal_int();
		sh3 = _os_.unmarshal_int();
		sh4 = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.Troop copy() {
		_xdb_verify_unsafe_();
		return new Troop(this);
	}

	@Override
	public xbean.Troop toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.Troop toBean() {
		_xdb_verify_unsafe_();
		return new Troop(this); // same as copy()
	}

	@Override
	public xbean.Troop toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.Troop toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getTroopnum() { // 战队编号
		_xdb_verify_unsafe_();
		return troopnum;
	}

	@Override
	public int getTrooptype() { // 战队类型，1为前2后3，2为前3后2
		_xdb_verify_unsafe_();
		return trooptype;
	}

	@Override
	public int getLocation1() { // 0没装
		_xdb_verify_unsafe_();
		return location1;
	}

	@Override
	public int getLocation2() { // 0没装
		_xdb_verify_unsafe_();
		return location2;
	}

	@Override
	public int getLocation3() { // 0没装
		_xdb_verify_unsafe_();
		return location3;
	}

	@Override
	public int getLocation4() { // 0没装
		_xdb_verify_unsafe_();
		return location4;
	}

	@Override
	public int getLocation5() { // 0没装
		_xdb_verify_unsafe_();
		return location5;
	}

	@Override
	public int getSh1() { // 神魂1号，0没装
		_xdb_verify_unsafe_();
		return sh1;
	}

	@Override
	public int getSh2() { // 神魂2号，0没装
		_xdb_verify_unsafe_();
		return sh2;
	}

	@Override
	public int getSh3() { // 神魂3号，0没装
		_xdb_verify_unsafe_();
		return sh3;
	}

	@Override
	public int getSh4() { // 神魂4号，0没装
		_xdb_verify_unsafe_();
		return sh4;
	}

	@Override
	public void setTroopnum(int _v_) { // 战队编号
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "troopnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, troopnum) {
					public void rollback() { troopnum = _xdb_saved; }
				};}});
		troopnum = _v_;
	}

	@Override
	public void setTrooptype(int _v_) { // 战队类型，1为前2后3，2为前3后2
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "trooptype") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, trooptype) {
					public void rollback() { trooptype = _xdb_saved; }
				};}});
		trooptype = _v_;
	}

	@Override
	public void setLocation1(int _v_) { // 0没装
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "location1") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, location1) {
					public void rollback() { location1 = _xdb_saved; }
				};}});
		location1 = _v_;
	}

	@Override
	public void setLocation2(int _v_) { // 0没装
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "location2") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, location2) {
					public void rollback() { location2 = _xdb_saved; }
				};}});
		location2 = _v_;
	}

	@Override
	public void setLocation3(int _v_) { // 0没装
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "location3") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, location3) {
					public void rollback() { location3 = _xdb_saved; }
				};}});
		location3 = _v_;
	}

	@Override
	public void setLocation4(int _v_) { // 0没装
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "location4") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, location4) {
					public void rollback() { location4 = _xdb_saved; }
				};}});
		location4 = _v_;
	}

	@Override
	public void setLocation5(int _v_) { // 0没装
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "location5") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, location5) {
					public void rollback() { location5 = _xdb_saved; }
				};}});
		location5 = _v_;
	}

	@Override
	public void setSh1(int _v_) { // 神魂1号，0没装
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "sh1") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, sh1) {
					public void rollback() { sh1 = _xdb_saved; }
				};}});
		sh1 = _v_;
	}

	@Override
	public void setSh2(int _v_) { // 神魂2号，0没装
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "sh2") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, sh2) {
					public void rollback() { sh2 = _xdb_saved; }
				};}});
		sh2 = _v_;
	}

	@Override
	public void setSh3(int _v_) { // 神魂3号，0没装
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "sh3") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, sh3) {
					public void rollback() { sh3 = _xdb_saved; }
				};}});
		sh3 = _v_;
	}

	@Override
	public void setSh4(int _v_) { // 神魂4号，0没装
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "sh4") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, sh4) {
					public void rollback() { sh4 = _xdb_saved; }
				};}});
		sh4 = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		Troop _o_ = null;
		if ( _o1_ instanceof Troop ) _o_ = (Troop)_o1_;
		else if ( _o1_ instanceof Troop.Const ) _o_ = ((Troop.Const)_o1_).nThis();
		else return false;
		if (troopnum != _o_.troopnum) return false;
		if (trooptype != _o_.trooptype) return false;
		if (location1 != _o_.location1) return false;
		if (location2 != _o_.location2) return false;
		if (location3 != _o_.location3) return false;
		if (location4 != _o_.location4) return false;
		if (location5 != _o_.location5) return false;
		if (sh1 != _o_.sh1) return false;
		if (sh2 != _o_.sh2) return false;
		if (sh3 != _o_.sh3) return false;
		if (sh4 != _o_.sh4) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += troopnum;
		_h_ += trooptype;
		_h_ += location1;
		_h_ += location2;
		_h_ += location3;
		_h_ += location4;
		_h_ += location5;
		_h_ += sh1;
		_h_ += sh2;
		_h_ += sh3;
		_h_ += sh4;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(troopnum);
		_sb_.append(",");
		_sb_.append(trooptype);
		_sb_.append(",");
		_sb_.append(location1);
		_sb_.append(",");
		_sb_.append(location2);
		_sb_.append(",");
		_sb_.append(location3);
		_sb_.append(",");
		_sb_.append(location4);
		_sb_.append(",");
		_sb_.append(location5);
		_sb_.append(",");
		_sb_.append(sh1);
		_sb_.append(",");
		_sb_.append(sh2);
		_sb_.append(",");
		_sb_.append(sh3);
		_sb_.append(",");
		_sb_.append(sh4);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("troopnum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("trooptype"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("location1"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("location2"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("location3"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("location4"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("location5"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("sh1"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("sh2"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("sh3"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("sh4"));
		return lb;
	}

	private class Const implements xbean.Troop {
		Troop nThis() {
			return Troop.this;
		}

		@Override
		public xbean.Troop copy() {
			return Troop.this.copy();
		}

		@Override
		public xbean.Troop toData() {
			return Troop.this.toData();
		}

		public xbean.Troop toBean() {
			return Troop.this.toBean();
		}

		@Override
		public xbean.Troop toDataIf() {
			return Troop.this.toDataIf();
		}

		public xbean.Troop toBeanIf() {
			return Troop.this.toBeanIf();
		}

		@Override
		public int getTroopnum() { // 战队编号
			_xdb_verify_unsafe_();
			return troopnum;
		}

		@Override
		public int getTrooptype() { // 战队类型，1为前2后3，2为前3后2
			_xdb_verify_unsafe_();
			return trooptype;
		}

		@Override
		public int getLocation1() { // 0没装
			_xdb_verify_unsafe_();
			return location1;
		}

		@Override
		public int getLocation2() { // 0没装
			_xdb_verify_unsafe_();
			return location2;
		}

		@Override
		public int getLocation3() { // 0没装
			_xdb_verify_unsafe_();
			return location3;
		}

		@Override
		public int getLocation4() { // 0没装
			_xdb_verify_unsafe_();
			return location4;
		}

		@Override
		public int getLocation5() { // 0没装
			_xdb_verify_unsafe_();
			return location5;
		}

		@Override
		public int getSh1() { // 神魂1号，0没装
			_xdb_verify_unsafe_();
			return sh1;
		}

		@Override
		public int getSh2() { // 神魂2号，0没装
			_xdb_verify_unsafe_();
			return sh2;
		}

		@Override
		public int getSh3() { // 神魂3号，0没装
			_xdb_verify_unsafe_();
			return sh3;
		}

		@Override
		public int getSh4() { // 神魂4号，0没装
			_xdb_verify_unsafe_();
			return sh4;
		}

		@Override
		public void setTroopnum(int _v_) { // 战队编号
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTrooptype(int _v_) { // 战队类型，1为前2后3，2为前3后2
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setLocation1(int _v_) { // 0没装
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setLocation2(int _v_) { // 0没装
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setLocation3(int _v_) { // 0没装
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setLocation4(int _v_) { // 0没装
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setLocation5(int _v_) { // 0没装
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setSh1(int _v_) { // 神魂1号，0没装
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setSh2(int _v_) { // 神魂2号，0没装
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setSh3(int _v_) { // 神魂3号，0没装
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setSh4(int _v_) { // 神魂4号，0没装
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
			return Troop.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return Troop.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return Troop.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return Troop.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return Troop.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return Troop.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return Troop.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return Troop.this.hashCode();
		}

		@Override
		public String toString() {
			return Troop.this.toString();
		}

	}

	public static final class Data implements xbean.Troop {
		private int troopnum; // 战队编号
		private int trooptype; // 战队类型，1为前2后3，2为前3后2
		private int location1; // 0没装
		private int location2; // 0没装
		private int location3; // 0没装
		private int location4; // 0没装
		private int location5; // 0没装
		private int sh1; // 神魂1号，0没装
		private int sh2; // 神魂2号，0没装
		private int sh3; // 神魂3号，0没装
		private int sh4; // 神魂4号，0没装

		public Data() {
		}

		Data(xbean.Troop _o1_) {
			if (_o1_ instanceof Troop) assign((Troop)_o1_);
			else if (_o1_ instanceof Troop.Data) assign((Troop.Data)_o1_);
			else if (_o1_ instanceof Troop.Const) assign(((Troop.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(Troop _o_) {
			troopnum = _o_.troopnum;
			trooptype = _o_.trooptype;
			location1 = _o_.location1;
			location2 = _o_.location2;
			location3 = _o_.location3;
			location4 = _o_.location4;
			location5 = _o_.location5;
			sh1 = _o_.sh1;
			sh2 = _o_.sh2;
			sh3 = _o_.sh3;
			sh4 = _o_.sh4;
		}

		private void assign(Troop.Data _o_) {
			troopnum = _o_.troopnum;
			trooptype = _o_.trooptype;
			location1 = _o_.location1;
			location2 = _o_.location2;
			location3 = _o_.location3;
			location4 = _o_.location4;
			location5 = _o_.location5;
			sh1 = _o_.sh1;
			sh2 = _o_.sh2;
			sh3 = _o_.sh3;
			sh4 = _o_.sh4;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(troopnum);
			_os_.marshal(trooptype);
			_os_.marshal(location1);
			_os_.marshal(location2);
			_os_.marshal(location3);
			_os_.marshal(location4);
			_os_.marshal(location5);
			_os_.marshal(sh1);
			_os_.marshal(sh2);
			_os_.marshal(sh3);
			_os_.marshal(sh4);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			troopnum = _os_.unmarshal_int();
			trooptype = _os_.unmarshal_int();
			location1 = _os_.unmarshal_int();
			location2 = _os_.unmarshal_int();
			location3 = _os_.unmarshal_int();
			location4 = _os_.unmarshal_int();
			location5 = _os_.unmarshal_int();
			sh1 = _os_.unmarshal_int();
			sh2 = _os_.unmarshal_int();
			sh3 = _os_.unmarshal_int();
			sh4 = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.Troop copy() {
			return new Data(this);
		}

		@Override
		public xbean.Troop toData() {
			return new Data(this);
		}

		public xbean.Troop toBean() {
			return new Troop(this, null, null);
		}

		@Override
		public xbean.Troop toDataIf() {
			return this;
		}

		public xbean.Troop toBeanIf() {
			return new Troop(this, null, null);
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
		public int getTroopnum() { // 战队编号
			return troopnum;
		}

		@Override
		public int getTrooptype() { // 战队类型，1为前2后3，2为前3后2
			return trooptype;
		}

		@Override
		public int getLocation1() { // 0没装
			return location1;
		}

		@Override
		public int getLocation2() { // 0没装
			return location2;
		}

		@Override
		public int getLocation3() { // 0没装
			return location3;
		}

		@Override
		public int getLocation4() { // 0没装
			return location4;
		}

		@Override
		public int getLocation5() { // 0没装
			return location5;
		}

		@Override
		public int getSh1() { // 神魂1号，0没装
			return sh1;
		}

		@Override
		public int getSh2() { // 神魂2号，0没装
			return sh2;
		}

		@Override
		public int getSh3() { // 神魂3号，0没装
			return sh3;
		}

		@Override
		public int getSh4() { // 神魂4号，0没装
			return sh4;
		}

		@Override
		public void setTroopnum(int _v_) { // 战队编号
			troopnum = _v_;
		}

		@Override
		public void setTrooptype(int _v_) { // 战队类型，1为前2后3，2为前3后2
			trooptype = _v_;
		}

		@Override
		public void setLocation1(int _v_) { // 0没装
			location1 = _v_;
		}

		@Override
		public void setLocation2(int _v_) { // 0没装
			location2 = _v_;
		}

		@Override
		public void setLocation3(int _v_) { // 0没装
			location3 = _v_;
		}

		@Override
		public void setLocation4(int _v_) { // 0没装
			location4 = _v_;
		}

		@Override
		public void setLocation5(int _v_) { // 0没装
			location5 = _v_;
		}

		@Override
		public void setSh1(int _v_) { // 神魂1号，0没装
			sh1 = _v_;
		}

		@Override
		public void setSh2(int _v_) { // 神魂2号，0没装
			sh2 = _v_;
		}

		@Override
		public void setSh3(int _v_) { // 神魂3号，0没装
			sh3 = _v_;
		}

		@Override
		public void setSh4(int _v_) { // 神魂4号，0没装
			sh4 = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof Troop.Data)) return false;
			Troop.Data _o_ = (Troop.Data) _o1_;
			if (troopnum != _o_.troopnum) return false;
			if (trooptype != _o_.trooptype) return false;
			if (location1 != _o_.location1) return false;
			if (location2 != _o_.location2) return false;
			if (location3 != _o_.location3) return false;
			if (location4 != _o_.location4) return false;
			if (location5 != _o_.location5) return false;
			if (sh1 != _o_.sh1) return false;
			if (sh2 != _o_.sh2) return false;
			if (sh3 != _o_.sh3) return false;
			if (sh4 != _o_.sh4) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += troopnum;
			_h_ += trooptype;
			_h_ += location1;
			_h_ += location2;
			_h_ += location3;
			_h_ += location4;
			_h_ += location5;
			_h_ += sh1;
			_h_ += sh2;
			_h_ += sh3;
			_h_ += sh4;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(troopnum);
			_sb_.append(",");
			_sb_.append(trooptype);
			_sb_.append(",");
			_sb_.append(location1);
			_sb_.append(",");
			_sb_.append(location2);
			_sb_.append(",");
			_sb_.append(location3);
			_sb_.append(",");
			_sb_.append(location4);
			_sb_.append(",");
			_sb_.append(location5);
			_sb_.append(",");
			_sb_.append(sh1);
			_sb_.append(",");
			_sb_.append(sh2);
			_sb_.append(",");
			_sb_.append(sh3);
			_sb_.append(",");
			_sb_.append(sh4);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
