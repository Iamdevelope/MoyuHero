
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class Artifact extends xdb.XBean implements xbean.Artifact {
	private int artifacttype; // 神器类型（key）
	private int artifactid; // 神器ID
	private int heronum1; // 英雄数量1
	private int heronum2; // 英雄数量2
	private int heronum3; // 英雄数量3
	private int heronum4; // 英雄数量4
	private int heronum5; // 英雄数量5

	Artifact(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
	}

	public Artifact() {
		this(0, null, null);
	}

	public Artifact(Artifact _o_) {
		this(_o_, null, null);
	}

	Artifact(xbean.Artifact _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof Artifact) assign((Artifact)_o1_);
		else if (_o1_ instanceof Artifact.Data) assign((Artifact.Data)_o1_);
		else if (_o1_ instanceof Artifact.Const) assign(((Artifact.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(Artifact _o_) {
		_o_._xdb_verify_unsafe_();
		artifacttype = _o_.artifacttype;
		artifactid = _o_.artifactid;
		heronum1 = _o_.heronum1;
		heronum2 = _o_.heronum2;
		heronum3 = _o_.heronum3;
		heronum4 = _o_.heronum4;
		heronum5 = _o_.heronum5;
	}

	private void assign(Artifact.Data _o_) {
		artifacttype = _o_.artifacttype;
		artifactid = _o_.artifactid;
		heronum1 = _o_.heronum1;
		heronum2 = _o_.heronum2;
		heronum3 = _o_.heronum3;
		heronum4 = _o_.heronum4;
		heronum5 = _o_.heronum5;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(artifacttype);
		_os_.marshal(artifactid);
		_os_.marshal(heronum1);
		_os_.marshal(heronum2);
		_os_.marshal(heronum3);
		_os_.marshal(heronum4);
		_os_.marshal(heronum5);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		artifacttype = _os_.unmarshal_int();
		artifactid = _os_.unmarshal_int();
		heronum1 = _os_.unmarshal_int();
		heronum2 = _os_.unmarshal_int();
		heronum3 = _os_.unmarshal_int();
		heronum4 = _os_.unmarshal_int();
		heronum5 = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.Artifact copy() {
		_xdb_verify_unsafe_();
		return new Artifact(this);
	}

	@Override
	public xbean.Artifact toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.Artifact toBean() {
		_xdb_verify_unsafe_();
		return new Artifact(this); // same as copy()
	}

	@Override
	public xbean.Artifact toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.Artifact toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getArtifacttype() { // 神器类型（key）
		_xdb_verify_unsafe_();
		return artifacttype;
	}

	@Override
	public int getArtifactid() { // 神器ID
		_xdb_verify_unsafe_();
		return artifactid;
	}

	@Override
	public int getHeronum1() { // 英雄数量1
		_xdb_verify_unsafe_();
		return heronum1;
	}

	@Override
	public int getHeronum2() { // 英雄数量2
		_xdb_verify_unsafe_();
		return heronum2;
	}

	@Override
	public int getHeronum3() { // 英雄数量3
		_xdb_verify_unsafe_();
		return heronum3;
	}

	@Override
	public int getHeronum4() { // 英雄数量4
		_xdb_verify_unsafe_();
		return heronum4;
	}

	@Override
	public int getHeronum5() { // 英雄数量5
		_xdb_verify_unsafe_();
		return heronum5;
	}

	@Override
	public void setArtifacttype(int _v_) { // 神器类型（key）
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "artifacttype") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, artifacttype) {
					public void rollback() { artifacttype = _xdb_saved; }
				};}});
		artifacttype = _v_;
	}

	@Override
	public void setArtifactid(int _v_) { // 神器ID
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "artifactid") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, artifactid) {
					public void rollback() { artifactid = _xdb_saved; }
				};}});
		artifactid = _v_;
	}

	@Override
	public void setHeronum1(int _v_) { // 英雄数量1
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "heronum1") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, heronum1) {
					public void rollback() { heronum1 = _xdb_saved; }
				};}});
		heronum1 = _v_;
	}

	@Override
	public void setHeronum2(int _v_) { // 英雄数量2
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "heronum2") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, heronum2) {
					public void rollback() { heronum2 = _xdb_saved; }
				};}});
		heronum2 = _v_;
	}

	@Override
	public void setHeronum3(int _v_) { // 英雄数量3
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "heronum3") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, heronum3) {
					public void rollback() { heronum3 = _xdb_saved; }
				};}});
		heronum3 = _v_;
	}

	@Override
	public void setHeronum4(int _v_) { // 英雄数量4
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "heronum4") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, heronum4) {
					public void rollback() { heronum4 = _xdb_saved; }
				};}});
		heronum4 = _v_;
	}

	@Override
	public void setHeronum5(int _v_) { // 英雄数量5
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "heronum5") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, heronum5) {
					public void rollback() { heronum5 = _xdb_saved; }
				};}});
		heronum5 = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		Artifact _o_ = null;
		if ( _o1_ instanceof Artifact ) _o_ = (Artifact)_o1_;
		else if ( _o1_ instanceof Artifact.Const ) _o_ = ((Artifact.Const)_o1_).nThis();
		else return false;
		if (artifacttype != _o_.artifacttype) return false;
		if (artifactid != _o_.artifactid) return false;
		if (heronum1 != _o_.heronum1) return false;
		if (heronum2 != _o_.heronum2) return false;
		if (heronum3 != _o_.heronum3) return false;
		if (heronum4 != _o_.heronum4) return false;
		if (heronum5 != _o_.heronum5) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += artifacttype;
		_h_ += artifactid;
		_h_ += heronum1;
		_h_ += heronum2;
		_h_ += heronum3;
		_h_ += heronum4;
		_h_ += heronum5;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(artifacttype);
		_sb_.append(",");
		_sb_.append(artifactid);
		_sb_.append(",");
		_sb_.append(heronum1);
		_sb_.append(",");
		_sb_.append(heronum2);
		_sb_.append(",");
		_sb_.append(heronum3);
		_sb_.append(",");
		_sb_.append(heronum4);
		_sb_.append(",");
		_sb_.append(heronum5);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("artifacttype"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("artifactid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("heronum1"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("heronum2"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("heronum3"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("heronum4"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("heronum5"));
		return lb;
	}

	private class Const implements xbean.Artifact {
		Artifact nThis() {
			return Artifact.this;
		}

		@Override
		public xbean.Artifact copy() {
			return Artifact.this.copy();
		}

		@Override
		public xbean.Artifact toData() {
			return Artifact.this.toData();
		}

		public xbean.Artifact toBean() {
			return Artifact.this.toBean();
		}

		@Override
		public xbean.Artifact toDataIf() {
			return Artifact.this.toDataIf();
		}

		public xbean.Artifact toBeanIf() {
			return Artifact.this.toBeanIf();
		}

		@Override
		public int getArtifacttype() { // 神器类型（key）
			_xdb_verify_unsafe_();
			return artifacttype;
		}

		@Override
		public int getArtifactid() { // 神器ID
			_xdb_verify_unsafe_();
			return artifactid;
		}

		@Override
		public int getHeronum1() { // 英雄数量1
			_xdb_verify_unsafe_();
			return heronum1;
		}

		@Override
		public int getHeronum2() { // 英雄数量2
			_xdb_verify_unsafe_();
			return heronum2;
		}

		@Override
		public int getHeronum3() { // 英雄数量3
			_xdb_verify_unsafe_();
			return heronum3;
		}

		@Override
		public int getHeronum4() { // 英雄数量4
			_xdb_verify_unsafe_();
			return heronum4;
		}

		@Override
		public int getHeronum5() { // 英雄数量5
			_xdb_verify_unsafe_();
			return heronum5;
		}

		@Override
		public void setArtifacttype(int _v_) { // 神器类型（key）
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setArtifactid(int _v_) { // 神器ID
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHeronum1(int _v_) { // 英雄数量1
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHeronum2(int _v_) { // 英雄数量2
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHeronum3(int _v_) { // 英雄数量3
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHeronum4(int _v_) { // 英雄数量4
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHeronum5(int _v_) { // 英雄数量5
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
			return Artifact.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return Artifact.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return Artifact.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return Artifact.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return Artifact.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return Artifact.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return Artifact.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return Artifact.this.hashCode();
		}

		@Override
		public String toString() {
			return Artifact.this.toString();
		}

	}

	public static final class Data implements xbean.Artifact {
		private int artifacttype; // 神器类型（key）
		private int artifactid; // 神器ID
		private int heronum1; // 英雄数量1
		private int heronum2; // 英雄数量2
		private int heronum3; // 英雄数量3
		private int heronum4; // 英雄数量4
		private int heronum5; // 英雄数量5

		public Data() {
		}

		Data(xbean.Artifact _o1_) {
			if (_o1_ instanceof Artifact) assign((Artifact)_o1_);
			else if (_o1_ instanceof Artifact.Data) assign((Artifact.Data)_o1_);
			else if (_o1_ instanceof Artifact.Const) assign(((Artifact.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(Artifact _o_) {
			artifacttype = _o_.artifacttype;
			artifactid = _o_.artifactid;
			heronum1 = _o_.heronum1;
			heronum2 = _o_.heronum2;
			heronum3 = _o_.heronum3;
			heronum4 = _o_.heronum4;
			heronum5 = _o_.heronum5;
		}

		private void assign(Artifact.Data _o_) {
			artifacttype = _o_.artifacttype;
			artifactid = _o_.artifactid;
			heronum1 = _o_.heronum1;
			heronum2 = _o_.heronum2;
			heronum3 = _o_.heronum3;
			heronum4 = _o_.heronum4;
			heronum5 = _o_.heronum5;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(artifacttype);
			_os_.marshal(artifactid);
			_os_.marshal(heronum1);
			_os_.marshal(heronum2);
			_os_.marshal(heronum3);
			_os_.marshal(heronum4);
			_os_.marshal(heronum5);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			artifacttype = _os_.unmarshal_int();
			artifactid = _os_.unmarshal_int();
			heronum1 = _os_.unmarshal_int();
			heronum2 = _os_.unmarshal_int();
			heronum3 = _os_.unmarshal_int();
			heronum4 = _os_.unmarshal_int();
			heronum5 = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.Artifact copy() {
			return new Data(this);
		}

		@Override
		public xbean.Artifact toData() {
			return new Data(this);
		}

		public xbean.Artifact toBean() {
			return new Artifact(this, null, null);
		}

		@Override
		public xbean.Artifact toDataIf() {
			return this;
		}

		public xbean.Artifact toBeanIf() {
			return new Artifact(this, null, null);
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
		public int getArtifacttype() { // 神器类型（key）
			return artifacttype;
		}

		@Override
		public int getArtifactid() { // 神器ID
			return artifactid;
		}

		@Override
		public int getHeronum1() { // 英雄数量1
			return heronum1;
		}

		@Override
		public int getHeronum2() { // 英雄数量2
			return heronum2;
		}

		@Override
		public int getHeronum3() { // 英雄数量3
			return heronum3;
		}

		@Override
		public int getHeronum4() { // 英雄数量4
			return heronum4;
		}

		@Override
		public int getHeronum5() { // 英雄数量5
			return heronum5;
		}

		@Override
		public void setArtifacttype(int _v_) { // 神器类型（key）
			artifacttype = _v_;
		}

		@Override
		public void setArtifactid(int _v_) { // 神器ID
			artifactid = _v_;
		}

		@Override
		public void setHeronum1(int _v_) { // 英雄数量1
			heronum1 = _v_;
		}

		@Override
		public void setHeronum2(int _v_) { // 英雄数量2
			heronum2 = _v_;
		}

		@Override
		public void setHeronum3(int _v_) { // 英雄数量3
			heronum3 = _v_;
		}

		@Override
		public void setHeronum4(int _v_) { // 英雄数量4
			heronum4 = _v_;
		}

		@Override
		public void setHeronum5(int _v_) { // 英雄数量5
			heronum5 = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof Artifact.Data)) return false;
			Artifact.Data _o_ = (Artifact.Data) _o1_;
			if (artifacttype != _o_.artifacttype) return false;
			if (artifactid != _o_.artifactid) return false;
			if (heronum1 != _o_.heronum1) return false;
			if (heronum2 != _o_.heronum2) return false;
			if (heronum3 != _o_.heronum3) return false;
			if (heronum4 != _o_.heronum4) return false;
			if (heronum5 != _o_.heronum5) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += artifacttype;
			_h_ += artifactid;
			_h_ += heronum1;
			_h_ += heronum2;
			_h_ += heronum3;
			_h_ += heronum4;
			_h_ += heronum5;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(artifacttype);
			_sb_.append(",");
			_sb_.append(artifactid);
			_sb_.append(",");
			_sb_.append(heronum1);
			_sb_.append(",");
			_sb_.append(heronum2);
			_sb_.append(",");
			_sb_.append(heronum3);
			_sb_.append(",");
			_sb_.append(heronum4);
			_sb_.append(",");
			_sb_.append(heronum5);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
