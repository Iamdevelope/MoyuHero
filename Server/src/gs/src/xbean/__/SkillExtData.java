
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class SkillExtData extends xdb.XBean implements xbean.SkillExtData {
	private int level; // 
	private int grade; // 
	private int exp; // 

	SkillExtData(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		level = 1;
		grade = 0;
		exp = 0;
	}

	public SkillExtData() {
		this(0, null, null);
	}

	public SkillExtData(SkillExtData _o_) {
		this(_o_, null, null);
	}

	SkillExtData(xbean.SkillExtData _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof SkillExtData) assign((SkillExtData)_o1_);
		else if (_o1_ instanceof SkillExtData.Data) assign((SkillExtData.Data)_o1_);
		else if (_o1_ instanceof SkillExtData.Const) assign(((SkillExtData.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(SkillExtData _o_) {
		_o_._xdb_verify_unsafe_();
		level = _o_.level;
		grade = _o_.grade;
		exp = _o_.exp;
	}

	private void assign(SkillExtData.Data _o_) {
		level = _o_.level;
		grade = _o_.grade;
		exp = _o_.exp;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(level);
		_os_.marshal(grade);
		_os_.marshal(exp);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		level = _os_.unmarshal_int();
		grade = _os_.unmarshal_int();
		exp = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.SkillExtData copy() {
		_xdb_verify_unsafe_();
		return new SkillExtData(this);
	}

	@Override
	public xbean.SkillExtData toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.SkillExtData toBean() {
		_xdb_verify_unsafe_();
		return new SkillExtData(this); // same as copy()
	}

	@Override
	public xbean.SkillExtData toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.SkillExtData toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getLevel() { // 
		_xdb_verify_unsafe_();
		return level;
	}

	@Override
	public int getGrade() { // 
		_xdb_verify_unsafe_();
		return grade;
	}

	@Override
	public int getExp() { // 
		_xdb_verify_unsafe_();
		return exp;
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
	public void setGrade(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "grade") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, grade) {
					public void rollback() { grade = _xdb_saved; }
				};}});
		grade = _v_;
	}

	@Override
	public void setExp(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "exp") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, exp) {
					public void rollback() { exp = _xdb_saved; }
				};}});
		exp = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		SkillExtData _o_ = null;
		if ( _o1_ instanceof SkillExtData ) _o_ = (SkillExtData)_o1_;
		else if ( _o1_ instanceof SkillExtData.Const ) _o_ = ((SkillExtData.Const)_o1_).nThis();
		else return false;
		if (level != _o_.level) return false;
		if (grade != _o_.grade) return false;
		if (exp != _o_.exp) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += level;
		_h_ += grade;
		_h_ += exp;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(level);
		_sb_.append(",");
		_sb_.append(grade);
		_sb_.append(",");
		_sb_.append(exp);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("level"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("grade"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("exp"));
		return lb;
	}

	private class Const implements xbean.SkillExtData {
		SkillExtData nThis() {
			return SkillExtData.this;
		}

		@Override
		public xbean.SkillExtData copy() {
			return SkillExtData.this.copy();
		}

		@Override
		public xbean.SkillExtData toData() {
			return SkillExtData.this.toData();
		}

		public xbean.SkillExtData toBean() {
			return SkillExtData.this.toBean();
		}

		@Override
		public xbean.SkillExtData toDataIf() {
			return SkillExtData.this.toDataIf();
		}

		public xbean.SkillExtData toBeanIf() {
			return SkillExtData.this.toBeanIf();
		}

		@Override
		public int getLevel() { // 
			_xdb_verify_unsafe_();
			return level;
		}

		@Override
		public int getGrade() { // 
			_xdb_verify_unsafe_();
			return grade;
		}

		@Override
		public int getExp() { // 
			_xdb_verify_unsafe_();
			return exp;
		}

		@Override
		public void setLevel(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setGrade(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setExp(int _v_) { // 
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
			return SkillExtData.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return SkillExtData.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return SkillExtData.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return SkillExtData.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return SkillExtData.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return SkillExtData.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return SkillExtData.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return SkillExtData.this.hashCode();
		}

		@Override
		public String toString() {
			return SkillExtData.this.toString();
		}

	}

	public static final class Data implements xbean.SkillExtData {
		private int level; // 
		private int grade; // 
		private int exp; // 

		public Data() {
			level = 1;
			grade = 0;
			exp = 0;
		}

		Data(xbean.SkillExtData _o1_) {
			if (_o1_ instanceof SkillExtData) assign((SkillExtData)_o1_);
			else if (_o1_ instanceof SkillExtData.Data) assign((SkillExtData.Data)_o1_);
			else if (_o1_ instanceof SkillExtData.Const) assign(((SkillExtData.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(SkillExtData _o_) {
			level = _o_.level;
			grade = _o_.grade;
			exp = _o_.exp;
		}

		private void assign(SkillExtData.Data _o_) {
			level = _o_.level;
			grade = _o_.grade;
			exp = _o_.exp;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(level);
			_os_.marshal(grade);
			_os_.marshal(exp);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			level = _os_.unmarshal_int();
			grade = _os_.unmarshal_int();
			exp = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.SkillExtData copy() {
			return new Data(this);
		}

		@Override
		public xbean.SkillExtData toData() {
			return new Data(this);
		}

		public xbean.SkillExtData toBean() {
			return new SkillExtData(this, null, null);
		}

		@Override
		public xbean.SkillExtData toDataIf() {
			return this;
		}

		public xbean.SkillExtData toBeanIf() {
			return new SkillExtData(this, null, null);
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
		public int getLevel() { // 
			return level;
		}

		@Override
		public int getGrade() { // 
			return grade;
		}

		@Override
		public int getExp() { // 
			return exp;
		}

		@Override
		public void setLevel(int _v_) { // 
			level = _v_;
		}

		@Override
		public void setGrade(int _v_) { // 
			grade = _v_;
		}

		@Override
		public void setExp(int _v_) { // 
			exp = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof SkillExtData.Data)) return false;
			SkillExtData.Data _o_ = (SkillExtData.Data) _o1_;
			if (level != _o_.level) return false;
			if (grade != _o_.grade) return false;
			if (exp != _o_.exp) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += level;
			_h_ += grade;
			_h_ += exp;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(level);
			_sb_.append(",");
			_sb_.append(grade);
			_sb_.append(",");
			_sb_.append(exp);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
