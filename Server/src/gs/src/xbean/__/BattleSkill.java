
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class BattleSkill extends xdb.XBean implements xbean.BattleSkill {
	private int id; // 
	private int level; // 
	private int castrate; // 以千为底

	BattleSkill(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
	}

	public BattleSkill() {
		this(0, null, null);
	}

	public BattleSkill(BattleSkill _o_) {
		this(_o_, null, null);
	}

	BattleSkill(xbean.BattleSkill _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof BattleSkill) assign((BattleSkill)_o1_);
		else if (_o1_ instanceof BattleSkill.Data) assign((BattleSkill.Data)_o1_);
		else if (_o1_ instanceof BattleSkill.Const) assign(((BattleSkill.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(BattleSkill _o_) {
		_o_._xdb_verify_unsafe_();
		id = _o_.id;
		level = _o_.level;
		castrate = _o_.castrate;
	}

	private void assign(BattleSkill.Data _o_) {
		id = _o_.id;
		level = _o_.level;
		castrate = _o_.castrate;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(id);
		_os_.marshal(level);
		_os_.marshal(castrate);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		id = _os_.unmarshal_int();
		level = _os_.unmarshal_int();
		castrate = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.BattleSkill copy() {
		_xdb_verify_unsafe_();
		return new BattleSkill(this);
	}

	@Override
	public xbean.BattleSkill toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.BattleSkill toBean() {
		_xdb_verify_unsafe_();
		return new BattleSkill(this); // same as copy()
	}

	@Override
	public xbean.BattleSkill toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.BattleSkill toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getId() { // 
		_xdb_verify_unsafe_();
		return id;
	}

	@Override
	public int getLevel() { // 
		_xdb_verify_unsafe_();
		return level;
	}

	@Override
	public int getCastrate() { // 以千为底
		_xdb_verify_unsafe_();
		return castrate;
	}

	@Override
	public void setId(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "id") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, id) {
					public void rollback() { id = _xdb_saved; }
				};}});
		id = _v_;
	}

	@Override
	public void setLevel(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "level") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, level) {
					public void rollback() { level = _xdb_saved; }
				};}});
		level = _v_;
	}

	@Override
	public void setCastrate(int _v_) { // 以千为底
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "castrate") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, castrate) {
					public void rollback() { castrate = _xdb_saved; }
				};}});
		castrate = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		BattleSkill _o_ = null;
		if ( _o1_ instanceof BattleSkill ) _o_ = (BattleSkill)_o1_;
		else if ( _o1_ instanceof BattleSkill.Const ) _o_ = ((BattleSkill.Const)_o1_).nThis();
		else return false;
		if (id != _o_.id) return false;
		if (level != _o_.level) return false;
		if (castrate != _o_.castrate) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += id;
		_h_ += level;
		_h_ += castrate;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(id);
		_sb_.append(",");
		_sb_.append(level);
		_sb_.append(",");
		_sb_.append(castrate);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("id"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("level"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("castrate"));
		return lb;
	}

	private class Const implements xbean.BattleSkill {
		BattleSkill nThis() {
			return BattleSkill.this;
		}

		@Override
		public xbean.BattleSkill copy() {
			return BattleSkill.this.copy();
		}

		@Override
		public xbean.BattleSkill toData() {
			return BattleSkill.this.toData();
		}

		public xbean.BattleSkill toBean() {
			return BattleSkill.this.toBean();
		}

		@Override
		public xbean.BattleSkill toDataIf() {
			return BattleSkill.this.toDataIf();
		}

		public xbean.BattleSkill toBeanIf() {
			return BattleSkill.this.toBeanIf();
		}

		@Override
		public int getId() { // 
			_xdb_verify_unsafe_();
			return id;
		}

		@Override
		public int getLevel() { // 
			_xdb_verify_unsafe_();
			return level;
		}

		@Override
		public int getCastrate() { // 以千为底
			_xdb_verify_unsafe_();
			return castrate;
		}

		@Override
		public void setId(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setLevel(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setCastrate(int _v_) { // 以千为底
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
			return BattleSkill.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return BattleSkill.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return BattleSkill.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return BattleSkill.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return BattleSkill.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return BattleSkill.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return BattleSkill.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return BattleSkill.this.hashCode();
		}

		@Override
		public String toString() {
			return BattleSkill.this.toString();
		}

	}

	public static final class Data implements xbean.BattleSkill {
		private int id; // 
		private int level; // 
		private int castrate; // 以千为底

		public Data() {
		}

		Data(xbean.BattleSkill _o1_) {
			if (_o1_ instanceof BattleSkill) assign((BattleSkill)_o1_);
			else if (_o1_ instanceof BattleSkill.Data) assign((BattleSkill.Data)_o1_);
			else if (_o1_ instanceof BattleSkill.Const) assign(((BattleSkill.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(BattleSkill _o_) {
			id = _o_.id;
			level = _o_.level;
			castrate = _o_.castrate;
		}

		private void assign(BattleSkill.Data _o_) {
			id = _o_.id;
			level = _o_.level;
			castrate = _o_.castrate;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(id);
			_os_.marshal(level);
			_os_.marshal(castrate);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			id = _os_.unmarshal_int();
			level = _os_.unmarshal_int();
			castrate = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.BattleSkill copy() {
			return new Data(this);
		}

		@Override
		public xbean.BattleSkill toData() {
			return new Data(this);
		}

		public xbean.BattleSkill toBean() {
			return new BattleSkill(this, null, null);
		}

		@Override
		public xbean.BattleSkill toDataIf() {
			return this;
		}

		public xbean.BattleSkill toBeanIf() {
			return new BattleSkill(this, null, null);
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
		public int getId() { // 
			return id;
		}

		@Override
		public int getLevel() { // 
			return level;
		}

		@Override
		public int getCastrate() { // 以千为底
			return castrate;
		}

		@Override
		public void setId(int _v_) { // 
			id = _v_;
		}

		@Override
		public void setLevel(int _v_) { // 
			level = _v_;
		}

		@Override
		public void setCastrate(int _v_) { // 以千为底
			castrate = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof BattleSkill.Data)) return false;
			BattleSkill.Data _o_ = (BattleSkill.Data) _o1_;
			if (id != _o_.id) return false;
			if (level != _o_.level) return false;
			if (castrate != _o_.castrate) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += id;
			_h_ += level;
			_h_ += castrate;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(id);
			_sb_.append(",");
			_sb_.append(level);
			_sb_.append(",");
			_sb_.append(castrate);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
