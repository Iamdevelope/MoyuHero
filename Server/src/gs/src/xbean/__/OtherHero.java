
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class OtherHero extends xdb.XBean implements xbean.OtherHero {
	private int heroid; // 英雄配表ID
	private int exp; // 当前经验
	private int herolevel; // 英雄等级
	private int hp; // 血量
	private int physicalattack; // 物理攻击
	private int physicaldefence; // 物理防御
	private int magicattack; // 魔法攻击
	private int magicdefence; // 魔法防御
	private int skill1; // 技能1编号（未开通为0）
	private int skill2; // 技能2编号（未开通为0）
	private int skill3; // 技能3编号（未开通为0）
	private int heroviewid; // 英雄外观

	OtherHero(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
	}

	public OtherHero() {
		this(0, null, null);
	}

	public OtherHero(OtherHero _o_) {
		this(_o_, null, null);
	}

	OtherHero(xbean.OtherHero _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof OtherHero) assign((OtherHero)_o1_);
		else if (_o1_ instanceof OtherHero.Data) assign((OtherHero.Data)_o1_);
		else if (_o1_ instanceof OtherHero.Const) assign(((OtherHero.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(OtherHero _o_) {
		_o_._xdb_verify_unsafe_();
		heroid = _o_.heroid;
		exp = _o_.exp;
		herolevel = _o_.herolevel;
		hp = _o_.hp;
		physicalattack = _o_.physicalattack;
		physicaldefence = _o_.physicaldefence;
		magicattack = _o_.magicattack;
		magicdefence = _o_.magicdefence;
		skill1 = _o_.skill1;
		skill2 = _o_.skill2;
		skill3 = _o_.skill3;
		heroviewid = _o_.heroviewid;
	}

	private void assign(OtherHero.Data _o_) {
		heroid = _o_.heroid;
		exp = _o_.exp;
		herolevel = _o_.herolevel;
		hp = _o_.hp;
		physicalattack = _o_.physicalattack;
		physicaldefence = _o_.physicaldefence;
		magicattack = _o_.magicattack;
		magicdefence = _o_.magicdefence;
		skill1 = _o_.skill1;
		skill2 = _o_.skill2;
		skill3 = _o_.skill3;
		heroviewid = _o_.heroviewid;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(heroid);
		_os_.marshal(exp);
		_os_.marshal(herolevel);
		_os_.marshal(hp);
		_os_.marshal(physicalattack);
		_os_.marshal(physicaldefence);
		_os_.marshal(magicattack);
		_os_.marshal(magicdefence);
		_os_.marshal(skill1);
		_os_.marshal(skill2);
		_os_.marshal(skill3);
		_os_.marshal(heroviewid);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		heroid = _os_.unmarshal_int();
		exp = _os_.unmarshal_int();
		herolevel = _os_.unmarshal_int();
		hp = _os_.unmarshal_int();
		physicalattack = _os_.unmarshal_int();
		physicaldefence = _os_.unmarshal_int();
		magicattack = _os_.unmarshal_int();
		magicdefence = _os_.unmarshal_int();
		skill1 = _os_.unmarshal_int();
		skill2 = _os_.unmarshal_int();
		skill3 = _os_.unmarshal_int();
		heroviewid = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.OtherHero copy() {
		_xdb_verify_unsafe_();
		return new OtherHero(this);
	}

	@Override
	public xbean.OtherHero toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.OtherHero toBean() {
		_xdb_verify_unsafe_();
		return new OtherHero(this); // same as copy()
	}

	@Override
	public xbean.OtherHero toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.OtherHero toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getHeroid() { // 英雄配表ID
		_xdb_verify_unsafe_();
		return heroid;
	}

	@Override
	public int getExp() { // 当前经验
		_xdb_verify_unsafe_();
		return exp;
	}

	@Override
	public int getHerolevel() { // 英雄等级
		_xdb_verify_unsafe_();
		return herolevel;
	}

	@Override
	public int getHp() { // 血量
		_xdb_verify_unsafe_();
		return hp;
	}

	@Override
	public int getPhysicalattack() { // 物理攻击
		_xdb_verify_unsafe_();
		return physicalattack;
	}

	@Override
	public int getPhysicaldefence() { // 物理防御
		_xdb_verify_unsafe_();
		return physicaldefence;
	}

	@Override
	public int getMagicattack() { // 魔法攻击
		_xdb_verify_unsafe_();
		return magicattack;
	}

	@Override
	public int getMagicdefence() { // 魔法防御
		_xdb_verify_unsafe_();
		return magicdefence;
	}

	@Override
	public int getSkill1() { // 技能1编号（未开通为0）
		_xdb_verify_unsafe_();
		return skill1;
	}

	@Override
	public int getSkill2() { // 技能2编号（未开通为0）
		_xdb_verify_unsafe_();
		return skill2;
	}

	@Override
	public int getSkill3() { // 技能3编号（未开通为0）
		_xdb_verify_unsafe_();
		return skill3;
	}

	@Override
	public int getHeroviewid() { // 英雄外观
		_xdb_verify_unsafe_();
		return heroviewid;
	}

	@Override
	public void setHeroid(int _v_) { // 英雄配表ID
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "heroid") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, heroid) {
					public void rollback() { heroid = _xdb_saved; }
				};}});
		heroid = _v_;
	}

	@Override
	public void setExp(int _v_) { // 当前经验
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "exp") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, exp) {
					public void rollback() { exp = _xdb_saved; }
				};}});
		exp = _v_;
	}

	@Override
	public void setHerolevel(int _v_) { // 英雄等级
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "herolevel") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, herolevel) {
					public void rollback() { herolevel = _xdb_saved; }
				};}});
		herolevel = _v_;
	}

	@Override
	public void setHp(int _v_) { // 血量
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "hp") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, hp) {
					public void rollback() { hp = _xdb_saved; }
				};}});
		hp = _v_;
	}

	@Override
	public void setPhysicalattack(int _v_) { // 物理攻击
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "physicalattack") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, physicalattack) {
					public void rollback() { physicalattack = _xdb_saved; }
				};}});
		physicalattack = _v_;
	}

	@Override
	public void setPhysicaldefence(int _v_) { // 物理防御
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "physicaldefence") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, physicaldefence) {
					public void rollback() { physicaldefence = _xdb_saved; }
				};}});
		physicaldefence = _v_;
	}

	@Override
	public void setMagicattack(int _v_) { // 魔法攻击
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "magicattack") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, magicattack) {
					public void rollback() { magicattack = _xdb_saved; }
				};}});
		magicattack = _v_;
	}

	@Override
	public void setMagicdefence(int _v_) { // 魔法防御
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "magicdefence") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, magicdefence) {
					public void rollback() { magicdefence = _xdb_saved; }
				};}});
		magicdefence = _v_;
	}

	@Override
	public void setSkill1(int _v_) { // 技能1编号（未开通为0）
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "skill1") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, skill1) {
					public void rollback() { skill1 = _xdb_saved; }
				};}});
		skill1 = _v_;
	}

	@Override
	public void setSkill2(int _v_) { // 技能2编号（未开通为0）
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "skill2") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, skill2) {
					public void rollback() { skill2 = _xdb_saved; }
				};}});
		skill2 = _v_;
	}

	@Override
	public void setSkill3(int _v_) { // 技能3编号（未开通为0）
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "skill3") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, skill3) {
					public void rollback() { skill3 = _xdb_saved; }
				};}});
		skill3 = _v_;
	}

	@Override
	public void setHeroviewid(int _v_) { // 英雄外观
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "heroviewid") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, heroviewid) {
					public void rollback() { heroviewid = _xdb_saved; }
				};}});
		heroviewid = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		OtherHero _o_ = null;
		if ( _o1_ instanceof OtherHero ) _o_ = (OtherHero)_o1_;
		else if ( _o1_ instanceof OtherHero.Const ) _o_ = ((OtherHero.Const)_o1_).nThis();
		else return false;
		if (heroid != _o_.heroid) return false;
		if (exp != _o_.exp) return false;
		if (herolevel != _o_.herolevel) return false;
		if (hp != _o_.hp) return false;
		if (physicalattack != _o_.physicalattack) return false;
		if (physicaldefence != _o_.physicaldefence) return false;
		if (magicattack != _o_.magicattack) return false;
		if (magicdefence != _o_.magicdefence) return false;
		if (skill1 != _o_.skill1) return false;
		if (skill2 != _o_.skill2) return false;
		if (skill3 != _o_.skill3) return false;
		if (heroviewid != _o_.heroviewid) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += heroid;
		_h_ += exp;
		_h_ += herolevel;
		_h_ += hp;
		_h_ += physicalattack;
		_h_ += physicaldefence;
		_h_ += magicattack;
		_h_ += magicdefence;
		_h_ += skill1;
		_h_ += skill2;
		_h_ += skill3;
		_h_ += heroviewid;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(heroid);
		_sb_.append(",");
		_sb_.append(exp);
		_sb_.append(",");
		_sb_.append(herolevel);
		_sb_.append(",");
		_sb_.append(hp);
		_sb_.append(",");
		_sb_.append(physicalattack);
		_sb_.append(",");
		_sb_.append(physicaldefence);
		_sb_.append(",");
		_sb_.append(magicattack);
		_sb_.append(",");
		_sb_.append(magicdefence);
		_sb_.append(",");
		_sb_.append(skill1);
		_sb_.append(",");
		_sb_.append(skill2);
		_sb_.append(",");
		_sb_.append(skill3);
		_sb_.append(",");
		_sb_.append(heroviewid);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("heroid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("exp"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("herolevel"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("hp"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("physicalattack"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("physicaldefence"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("magicattack"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("magicdefence"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("skill1"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("skill2"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("skill3"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("heroviewid"));
		return lb;
	}

	private class Const implements xbean.OtherHero {
		OtherHero nThis() {
			return OtherHero.this;
		}

		@Override
		public xbean.OtherHero copy() {
			return OtherHero.this.copy();
		}

		@Override
		public xbean.OtherHero toData() {
			return OtherHero.this.toData();
		}

		public xbean.OtherHero toBean() {
			return OtherHero.this.toBean();
		}

		@Override
		public xbean.OtherHero toDataIf() {
			return OtherHero.this.toDataIf();
		}

		public xbean.OtherHero toBeanIf() {
			return OtherHero.this.toBeanIf();
		}

		@Override
		public int getHeroid() { // 英雄配表ID
			_xdb_verify_unsafe_();
			return heroid;
		}

		@Override
		public int getExp() { // 当前经验
			_xdb_verify_unsafe_();
			return exp;
		}

		@Override
		public int getHerolevel() { // 英雄等级
			_xdb_verify_unsafe_();
			return herolevel;
		}

		@Override
		public int getHp() { // 血量
			_xdb_verify_unsafe_();
			return hp;
		}

		@Override
		public int getPhysicalattack() { // 物理攻击
			_xdb_verify_unsafe_();
			return physicalattack;
		}

		@Override
		public int getPhysicaldefence() { // 物理防御
			_xdb_verify_unsafe_();
			return physicaldefence;
		}

		@Override
		public int getMagicattack() { // 魔法攻击
			_xdb_verify_unsafe_();
			return magicattack;
		}

		@Override
		public int getMagicdefence() { // 魔法防御
			_xdb_verify_unsafe_();
			return magicdefence;
		}

		@Override
		public int getSkill1() { // 技能1编号（未开通为0）
			_xdb_verify_unsafe_();
			return skill1;
		}

		@Override
		public int getSkill2() { // 技能2编号（未开通为0）
			_xdb_verify_unsafe_();
			return skill2;
		}

		@Override
		public int getSkill3() { // 技能3编号（未开通为0）
			_xdb_verify_unsafe_();
			return skill3;
		}

		@Override
		public int getHeroviewid() { // 英雄外观
			_xdb_verify_unsafe_();
			return heroviewid;
		}

		@Override
		public void setHeroid(int _v_) { // 英雄配表ID
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setExp(int _v_) { // 当前经验
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHerolevel(int _v_) { // 英雄等级
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHp(int _v_) { // 血量
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setPhysicalattack(int _v_) { // 物理攻击
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setPhysicaldefence(int _v_) { // 物理防御
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setMagicattack(int _v_) { // 魔法攻击
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setMagicdefence(int _v_) { // 魔法防御
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setSkill1(int _v_) { // 技能1编号（未开通为0）
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setSkill2(int _v_) { // 技能2编号（未开通为0）
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setSkill3(int _v_) { // 技能3编号（未开通为0）
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHeroviewid(int _v_) { // 英雄外观
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
			return OtherHero.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return OtherHero.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return OtherHero.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return OtherHero.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return OtherHero.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return OtherHero.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return OtherHero.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return OtherHero.this.hashCode();
		}

		@Override
		public String toString() {
			return OtherHero.this.toString();
		}

	}

	public static final class Data implements xbean.OtherHero {
		private int heroid; // 英雄配表ID
		private int exp; // 当前经验
		private int herolevel; // 英雄等级
		private int hp; // 血量
		private int physicalattack; // 物理攻击
		private int physicaldefence; // 物理防御
		private int magicattack; // 魔法攻击
		private int magicdefence; // 魔法防御
		private int skill1; // 技能1编号（未开通为0）
		private int skill2; // 技能2编号（未开通为0）
		private int skill3; // 技能3编号（未开通为0）
		private int heroviewid; // 英雄外观

		public Data() {
		}

		Data(xbean.OtherHero _o1_) {
			if (_o1_ instanceof OtherHero) assign((OtherHero)_o1_);
			else if (_o1_ instanceof OtherHero.Data) assign((OtherHero.Data)_o1_);
			else if (_o1_ instanceof OtherHero.Const) assign(((OtherHero.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(OtherHero _o_) {
			heroid = _o_.heroid;
			exp = _o_.exp;
			herolevel = _o_.herolevel;
			hp = _o_.hp;
			physicalattack = _o_.physicalattack;
			physicaldefence = _o_.physicaldefence;
			magicattack = _o_.magicattack;
			magicdefence = _o_.magicdefence;
			skill1 = _o_.skill1;
			skill2 = _o_.skill2;
			skill3 = _o_.skill3;
			heroviewid = _o_.heroviewid;
		}

		private void assign(OtherHero.Data _o_) {
			heroid = _o_.heroid;
			exp = _o_.exp;
			herolevel = _o_.herolevel;
			hp = _o_.hp;
			physicalattack = _o_.physicalattack;
			physicaldefence = _o_.physicaldefence;
			magicattack = _o_.magicattack;
			magicdefence = _o_.magicdefence;
			skill1 = _o_.skill1;
			skill2 = _o_.skill2;
			skill3 = _o_.skill3;
			heroviewid = _o_.heroviewid;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(heroid);
			_os_.marshal(exp);
			_os_.marshal(herolevel);
			_os_.marshal(hp);
			_os_.marshal(physicalattack);
			_os_.marshal(physicaldefence);
			_os_.marshal(magicattack);
			_os_.marshal(magicdefence);
			_os_.marshal(skill1);
			_os_.marshal(skill2);
			_os_.marshal(skill3);
			_os_.marshal(heroviewid);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			heroid = _os_.unmarshal_int();
			exp = _os_.unmarshal_int();
			herolevel = _os_.unmarshal_int();
			hp = _os_.unmarshal_int();
			physicalattack = _os_.unmarshal_int();
			physicaldefence = _os_.unmarshal_int();
			magicattack = _os_.unmarshal_int();
			magicdefence = _os_.unmarshal_int();
			skill1 = _os_.unmarshal_int();
			skill2 = _os_.unmarshal_int();
			skill3 = _os_.unmarshal_int();
			heroviewid = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.OtherHero copy() {
			return new Data(this);
		}

		@Override
		public xbean.OtherHero toData() {
			return new Data(this);
		}

		public xbean.OtherHero toBean() {
			return new OtherHero(this, null, null);
		}

		@Override
		public xbean.OtherHero toDataIf() {
			return this;
		}

		public xbean.OtherHero toBeanIf() {
			return new OtherHero(this, null, null);
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
		public int getHeroid() { // 英雄配表ID
			return heroid;
		}

		@Override
		public int getExp() { // 当前经验
			return exp;
		}

		@Override
		public int getHerolevel() { // 英雄等级
			return herolevel;
		}

		@Override
		public int getHp() { // 血量
			return hp;
		}

		@Override
		public int getPhysicalattack() { // 物理攻击
			return physicalattack;
		}

		@Override
		public int getPhysicaldefence() { // 物理防御
			return physicaldefence;
		}

		@Override
		public int getMagicattack() { // 魔法攻击
			return magicattack;
		}

		@Override
		public int getMagicdefence() { // 魔法防御
			return magicdefence;
		}

		@Override
		public int getSkill1() { // 技能1编号（未开通为0）
			return skill1;
		}

		@Override
		public int getSkill2() { // 技能2编号（未开通为0）
			return skill2;
		}

		@Override
		public int getSkill3() { // 技能3编号（未开通为0）
			return skill3;
		}

		@Override
		public int getHeroviewid() { // 英雄外观
			return heroviewid;
		}

		@Override
		public void setHeroid(int _v_) { // 英雄配表ID
			heroid = _v_;
		}

		@Override
		public void setExp(int _v_) { // 当前经验
			exp = _v_;
		}

		@Override
		public void setHerolevel(int _v_) { // 英雄等级
			herolevel = _v_;
		}

		@Override
		public void setHp(int _v_) { // 血量
			hp = _v_;
		}

		@Override
		public void setPhysicalattack(int _v_) { // 物理攻击
			physicalattack = _v_;
		}

		@Override
		public void setPhysicaldefence(int _v_) { // 物理防御
			physicaldefence = _v_;
		}

		@Override
		public void setMagicattack(int _v_) { // 魔法攻击
			magicattack = _v_;
		}

		@Override
		public void setMagicdefence(int _v_) { // 魔法防御
			magicdefence = _v_;
		}

		@Override
		public void setSkill1(int _v_) { // 技能1编号（未开通为0）
			skill1 = _v_;
		}

		@Override
		public void setSkill2(int _v_) { // 技能2编号（未开通为0）
			skill2 = _v_;
		}

		@Override
		public void setSkill3(int _v_) { // 技能3编号（未开通为0）
			skill3 = _v_;
		}

		@Override
		public void setHeroviewid(int _v_) { // 英雄外观
			heroviewid = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof OtherHero.Data)) return false;
			OtherHero.Data _o_ = (OtherHero.Data) _o1_;
			if (heroid != _o_.heroid) return false;
			if (exp != _o_.exp) return false;
			if (herolevel != _o_.herolevel) return false;
			if (hp != _o_.hp) return false;
			if (physicalattack != _o_.physicalattack) return false;
			if (physicaldefence != _o_.physicaldefence) return false;
			if (magicattack != _o_.magicattack) return false;
			if (magicdefence != _o_.magicdefence) return false;
			if (skill1 != _o_.skill1) return false;
			if (skill2 != _o_.skill2) return false;
			if (skill3 != _o_.skill3) return false;
			if (heroviewid != _o_.heroviewid) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += heroid;
			_h_ += exp;
			_h_ += herolevel;
			_h_ += hp;
			_h_ += physicalattack;
			_h_ += physicaldefence;
			_h_ += magicattack;
			_h_ += magicdefence;
			_h_ += skill1;
			_h_ += skill2;
			_h_ += skill3;
			_h_ += heroviewid;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(heroid);
			_sb_.append(",");
			_sb_.append(exp);
			_sb_.append(",");
			_sb_.append(herolevel);
			_sb_.append(",");
			_sb_.append(hp);
			_sb_.append(",");
			_sb_.append(physicalattack);
			_sb_.append(",");
			_sb_.append(physicaldefence);
			_sb_.append(",");
			_sb_.append(magicattack);
			_sb_.append(",");
			_sb_.append(magicdefence);
			_sb_.append(",");
			_sb_.append(skill1);
			_sb_.append(",");
			_sb_.append(skill2);
			_sb_.append(",");
			_sb_.append(skill3);
			_sb_.append(",");
			_sb_.append(heroviewid);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
