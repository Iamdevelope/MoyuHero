
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class FighterInfo extends xdb.XBean implements xbean.FighterInfo {
	private int fighterid; // 
	private int fightertype; // 
	private int pos; // 
	private int heroid; // 
	private int grouptype; // 阵营
	private int level; // 等级
	private int color; // 颜色
	private int grade; // 阶
	private int weaponinfo; // 武器信息
	private int armorinfo; // 铠甲信息
	private int horseinfo; // 战马信息
	private int speed; // 速
	private int hp; // 兵力
	private xbean.BasicFightProperties bfp; // 基础战斗属性
	private java.util.HashMap<Integer, Float> effects; // 效果 key = effect type id
	private java.util.HashMap<Integer, Float> finalattrs; // 最终属性 key = attr type
	private xbean.BuffAgent buffagent; // buff代理
	private java.util.LinkedList<xbean.BattleSkill> skills; // 技能
	private int shape; // 造型ID

	FighterInfo(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		level = 1;
		color = 0;
		grade = 0;
		weaponinfo = 0;
		armorinfo = 0;
		horseinfo = 0;
		speed = 0;
		bfp = new BasicFightProperties(0, this, "bfp");
		effects = new java.util.HashMap<Integer, Float>();
		finalattrs = new java.util.HashMap<Integer, Float>();
		buffagent = new BuffAgent(0, this, "buffagent");
		skills = new java.util.LinkedList<xbean.BattleSkill>();
	}

	public FighterInfo() {
		this(0, null, null);
	}

	public FighterInfo(FighterInfo _o_) {
		this(_o_, null, null);
	}

	FighterInfo(xbean.FighterInfo _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof FighterInfo) assign((FighterInfo)_o1_);
		else if (_o1_ instanceof FighterInfo.Data) assign((FighterInfo.Data)_o1_);
		else if (_o1_ instanceof FighterInfo.Const) assign(((FighterInfo.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(FighterInfo _o_) {
		_o_._xdb_verify_unsafe_();
		fighterid = _o_.fighterid;
		fightertype = _o_.fightertype;
		pos = _o_.pos;
		heroid = _o_.heroid;
		grouptype = _o_.grouptype;
		level = _o_.level;
		color = _o_.color;
		grade = _o_.grade;
		weaponinfo = _o_.weaponinfo;
		armorinfo = _o_.armorinfo;
		horseinfo = _o_.horseinfo;
		speed = _o_.speed;
		hp = _o_.hp;
		bfp = new BasicFightProperties(_o_.bfp, this, "bfp");
		effects = new java.util.HashMap<Integer, Float>();
		for (java.util.Map.Entry<Integer, Float> _e_ : _o_.effects.entrySet())
			effects.put(_e_.getKey(), _e_.getValue());
		finalattrs = new java.util.HashMap<Integer, Float>();
		for (java.util.Map.Entry<Integer, Float> _e_ : _o_.finalattrs.entrySet())
			finalattrs.put(_e_.getKey(), _e_.getValue());
		buffagent = new BuffAgent(_o_.buffagent, this, "buffagent");
		skills = new java.util.LinkedList<xbean.BattleSkill>();
		for (xbean.BattleSkill _v_ : _o_.skills)
			skills.add(new BattleSkill(_v_, this, "skills"));
		shape = _o_.shape;
	}

	private void assign(FighterInfo.Data _o_) {
		fighterid = _o_.fighterid;
		fightertype = _o_.fightertype;
		pos = _o_.pos;
		heroid = _o_.heroid;
		grouptype = _o_.grouptype;
		level = _o_.level;
		color = _o_.color;
		grade = _o_.grade;
		weaponinfo = _o_.weaponinfo;
		armorinfo = _o_.armorinfo;
		horseinfo = _o_.horseinfo;
		speed = _o_.speed;
		hp = _o_.hp;
		bfp = new BasicFightProperties(_o_.bfp, this, "bfp");
		effects = new java.util.HashMap<Integer, Float>();
		for (java.util.Map.Entry<Integer, Float> _e_ : _o_.effects.entrySet())
			effects.put(_e_.getKey(), _e_.getValue());
		finalattrs = new java.util.HashMap<Integer, Float>();
		for (java.util.Map.Entry<Integer, Float> _e_ : _o_.finalattrs.entrySet())
			finalattrs.put(_e_.getKey(), _e_.getValue());
		buffagent = new BuffAgent(_o_.buffagent, this, "buffagent");
		skills = new java.util.LinkedList<xbean.BattleSkill>();
		for (xbean.BattleSkill _v_ : _o_.skills)
			skills.add(new BattleSkill(_v_, this, "skills"));
		shape = _o_.shape;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(fighterid);
		_os_.marshal(fightertype);
		_os_.marshal(pos);
		_os_.marshal(heroid);
		_os_.marshal(grouptype);
		_os_.marshal(level);
		_os_.marshal(color);
		_os_.marshal(grade);
		_os_.marshal(weaponinfo);
		_os_.marshal(armorinfo);
		_os_.marshal(horseinfo);
		_os_.marshal(speed);
		_os_.marshal(hp);
		bfp.marshal(_os_);
		_os_.compact_uint32(effects.size());
		for (java.util.Map.Entry<Integer, Float> _e_ : effects.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.compact_uint32(finalattrs.size());
		for (java.util.Map.Entry<Integer, Float> _e_ : finalattrs.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		buffagent.marshal(_os_);
		_os_.compact_uint32(skills.size());
		for (xbean.BattleSkill _v_ : skills) {
			_v_.marshal(_os_);
		}
		_os_.marshal(shape);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		fighterid = _os_.unmarshal_int();
		fightertype = _os_.unmarshal_int();
		pos = _os_.unmarshal_int();
		heroid = _os_.unmarshal_int();
		grouptype = _os_.unmarshal_int();
		level = _os_.unmarshal_int();
		color = _os_.unmarshal_int();
		grade = _os_.unmarshal_int();
		weaponinfo = _os_.unmarshal_int();
		armorinfo = _os_.unmarshal_int();
		horseinfo = _os_.unmarshal_int();
		speed = _os_.unmarshal_int();
		hp = _os_.unmarshal_int();
		bfp.unmarshal(_os_);
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				effects = new java.util.HashMap<Integer, Float>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				float _v_ = 0.0f;
				_v_ = _os_.unmarshal_float();
				effects.put(_k_, _v_);
			}
		}
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				finalattrs = new java.util.HashMap<Integer, Float>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				float _v_ = 0.0f;
				_v_ = _os_.unmarshal_float();
				finalattrs.put(_k_, _v_);
			}
		}
		buffagent.unmarshal(_os_);
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			xbean.BattleSkill _v_ = new BattleSkill(0, this, "skills");
			_v_.unmarshal(_os_);
			skills.add(_v_);
		}
		shape = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.FighterInfo copy() {
		_xdb_verify_unsafe_();
		return new FighterInfo(this);
	}

	@Override
	public xbean.FighterInfo toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.FighterInfo toBean() {
		_xdb_verify_unsafe_();
		return new FighterInfo(this); // same as copy()
	}

	@Override
	public xbean.FighterInfo toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.FighterInfo toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getFighterid() { // 
		_xdb_verify_unsafe_();
		return fighterid;
	}

	@Override
	public int getFightertype() { // 
		_xdb_verify_unsafe_();
		return fightertype;
	}

	@Override
	public int getPos() { // 
		_xdb_verify_unsafe_();
		return pos;
	}

	@Override
	public int getHeroid() { // 
		_xdb_verify_unsafe_();
		return heroid;
	}

	@Override
	public int getGrouptype() { // 阵营
		_xdb_verify_unsafe_();
		return grouptype;
	}

	@Override
	public int getLevel() { // 等级
		_xdb_verify_unsafe_();
		return level;
	}

	@Override
	public int getColor() { // 颜色
		_xdb_verify_unsafe_();
		return color;
	}

	@Override
	public int getGrade() { // 阶
		_xdb_verify_unsafe_();
		return grade;
	}

	@Override
	public int getWeaponinfo() { // 武器信息
		_xdb_verify_unsafe_();
		return weaponinfo;
	}

	@Override
	public int getArmorinfo() { // 铠甲信息
		_xdb_verify_unsafe_();
		return armorinfo;
	}

	@Override
	public int getHorseinfo() { // 战马信息
		_xdb_verify_unsafe_();
		return horseinfo;
	}

	@Override
	public int getSpeed() { // 速
		_xdb_verify_unsafe_();
		return speed;
	}

	@Override
	public int getHp() { // 兵力
		_xdb_verify_unsafe_();
		return hp;
	}

	@Override
	public xbean.BasicFightProperties getBfp() { // 基础战斗属性
		_xdb_verify_unsafe_();
		return bfp;
	}

	@Override
	public java.util.Map<Integer, Float> getEffects() { // 效果 key = effect type id
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "effects"), effects);
	}

	@Override
	public java.util.Map<Integer, Float> getEffectsAsData() { // 效果 key = effect type id
		_xdb_verify_unsafe_();
		java.util.Map<Integer, Float> effects;
		FighterInfo _o_ = this;
		effects = new java.util.HashMap<Integer, Float>();
		for (java.util.Map.Entry<Integer, Float> _e_ : _o_.effects.entrySet())
			effects.put(_e_.getKey(), _e_.getValue());
		return effects;
	}

	@Override
	public java.util.Map<Integer, Float> getFinalattrs() { // 最终属性 key = attr type
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "finalattrs"), finalattrs);
	}

	@Override
	public java.util.Map<Integer, Float> getFinalattrsAsData() { // 最终属性 key = attr type
		_xdb_verify_unsafe_();
		java.util.Map<Integer, Float> finalattrs;
		FighterInfo _o_ = this;
		finalattrs = new java.util.HashMap<Integer, Float>();
		for (java.util.Map.Entry<Integer, Float> _e_ : _o_.finalattrs.entrySet())
			finalattrs.put(_e_.getKey(), _e_.getValue());
		return finalattrs;
	}

	@Override
	public xbean.BuffAgent getBuffagent() { // buff代理
		_xdb_verify_unsafe_();
		return buffagent;
	}

	@Override
	public java.util.List<xbean.BattleSkill> getSkills() { // 技能
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "skills"), skills);
	}

	public java.util.List<xbean.BattleSkill> getSkillsAsData() { // 技能
		_xdb_verify_unsafe_();
		java.util.List<xbean.BattleSkill> skills;
		FighterInfo _o_ = this;
		skills = new java.util.LinkedList<xbean.BattleSkill>();
		for (xbean.BattleSkill _v_ : _o_.skills)
			skills.add(new BattleSkill.Data(_v_));
		return skills;
	}

	@Override
	public int getShape() { // 造型ID
		_xdb_verify_unsafe_();
		return shape;
	}

	@Override
	public void setFighterid(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "fighterid") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, fighterid) {
					public void rollback() { fighterid = _xdb_saved; }
				};}});
		fighterid = _v_;
	}

	@Override
	public void setFightertype(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "fightertype") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, fightertype) {
					public void rollback() { fightertype = _xdb_saved; }
				};}});
		fightertype = _v_;
	}

	@Override
	public void setPos(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "pos") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, pos) {
					public void rollback() { pos = _xdb_saved; }
				};}});
		pos = _v_;
	}

	@Override
	public void setHeroid(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "heroid") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, heroid) {
					public void rollback() { heroid = _xdb_saved; }
				};}});
		heroid = _v_;
	}

	@Override
	public void setGrouptype(int _v_) { // 阵营
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "grouptype") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, grouptype) {
					public void rollback() { grouptype = _xdb_saved; }
				};}});
		grouptype = _v_;
	}

	@Override
	public void setLevel(int _v_) { // 等级
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "level") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, level) {
					public void rollback() { level = _xdb_saved; }
				};}});
		level = _v_;
	}

	@Override
	public void setColor(int _v_) { // 颜色
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "color") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, color) {
					public void rollback() { color = _xdb_saved; }
				};}});
		color = _v_;
	}

	@Override
	public void setGrade(int _v_) { // 阶
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "grade") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, grade) {
					public void rollback() { grade = _xdb_saved; }
				};}});
		grade = _v_;
	}

	@Override
	public void setWeaponinfo(int _v_) { // 武器信息
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "weaponinfo") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, weaponinfo) {
					public void rollback() { weaponinfo = _xdb_saved; }
				};}});
		weaponinfo = _v_;
	}

	@Override
	public void setArmorinfo(int _v_) { // 铠甲信息
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "armorinfo") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, armorinfo) {
					public void rollback() { armorinfo = _xdb_saved; }
				};}});
		armorinfo = _v_;
	}

	@Override
	public void setHorseinfo(int _v_) { // 战马信息
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "horseinfo") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, horseinfo) {
					public void rollback() { horseinfo = _xdb_saved; }
				};}});
		horseinfo = _v_;
	}

	@Override
	public void setSpeed(int _v_) { // 速
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "speed") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, speed) {
					public void rollback() { speed = _xdb_saved; }
				};}});
		speed = _v_;
	}

	@Override
	public void setHp(int _v_) { // 兵力
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "hp") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, hp) {
					public void rollback() { hp = _xdb_saved; }
				};}});
		hp = _v_;
	}

	@Override
	public void setShape(int _v_) { // 造型ID
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "shape") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, shape) {
					public void rollback() { shape = _xdb_saved; }
				};}});
		shape = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		FighterInfo _o_ = null;
		if ( _o1_ instanceof FighterInfo ) _o_ = (FighterInfo)_o1_;
		else if ( _o1_ instanceof FighterInfo.Const ) _o_ = ((FighterInfo.Const)_o1_).nThis();
		else return false;
		if (fighterid != _o_.fighterid) return false;
		if (fightertype != _o_.fightertype) return false;
		if (pos != _o_.pos) return false;
		if (heroid != _o_.heroid) return false;
		if (grouptype != _o_.grouptype) return false;
		if (level != _o_.level) return false;
		if (color != _o_.color) return false;
		if (grade != _o_.grade) return false;
		if (weaponinfo != _o_.weaponinfo) return false;
		if (armorinfo != _o_.armorinfo) return false;
		if (horseinfo != _o_.horseinfo) return false;
		if (speed != _o_.speed) return false;
		if (hp != _o_.hp) return false;
		if (!bfp.equals(_o_.bfp)) return false;
		if (!effects.equals(_o_.effects)) return false;
		if (!finalattrs.equals(_o_.finalattrs)) return false;
		if (!buffagent.equals(_o_.buffagent)) return false;
		if (!skills.equals(_o_.skills)) return false;
		if (shape != _o_.shape) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += fighterid;
		_h_ += fightertype;
		_h_ += pos;
		_h_ += heroid;
		_h_ += grouptype;
		_h_ += level;
		_h_ += color;
		_h_ += grade;
		_h_ += weaponinfo;
		_h_ += armorinfo;
		_h_ += horseinfo;
		_h_ += speed;
		_h_ += hp;
		_h_ += bfp.hashCode();
		_h_ += effects.hashCode();
		_h_ += finalattrs.hashCode();
		_h_ += buffagent.hashCode();
		_h_ += skills.hashCode();
		_h_ += shape;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(fighterid);
		_sb_.append(",");
		_sb_.append(fightertype);
		_sb_.append(",");
		_sb_.append(pos);
		_sb_.append(",");
		_sb_.append(heroid);
		_sb_.append(",");
		_sb_.append(grouptype);
		_sb_.append(",");
		_sb_.append(level);
		_sb_.append(",");
		_sb_.append(color);
		_sb_.append(",");
		_sb_.append(grade);
		_sb_.append(",");
		_sb_.append(weaponinfo);
		_sb_.append(",");
		_sb_.append(armorinfo);
		_sb_.append(",");
		_sb_.append(horseinfo);
		_sb_.append(",");
		_sb_.append(speed);
		_sb_.append(",");
		_sb_.append(hp);
		_sb_.append(",");
		_sb_.append(bfp);
		_sb_.append(",");
		_sb_.append(effects);
		_sb_.append(",");
		_sb_.append(finalattrs);
		_sb_.append(",");
		_sb_.append(buffagent);
		_sb_.append(",");
		_sb_.append(skills);
		_sb_.append(",");
		_sb_.append(shape);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("fighterid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("fightertype"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("pos"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("heroid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("grouptype"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("level"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("color"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("grade"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("weaponinfo"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("armorinfo"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("horseinfo"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("speed"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("hp"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("bfp"));
		lb.add(new xdb.logs.ListenableMap().setVarName("effects"));
		lb.add(new xdb.logs.ListenableMap().setVarName("finalattrs"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("buffagent"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("skills"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("shape"));
		return lb;
	}

	private class Const implements xbean.FighterInfo {
		FighterInfo nThis() {
			return FighterInfo.this;
		}

		@Override
		public xbean.FighterInfo copy() {
			return FighterInfo.this.copy();
		}

		@Override
		public xbean.FighterInfo toData() {
			return FighterInfo.this.toData();
		}

		public xbean.FighterInfo toBean() {
			return FighterInfo.this.toBean();
		}

		@Override
		public xbean.FighterInfo toDataIf() {
			return FighterInfo.this.toDataIf();
		}

		public xbean.FighterInfo toBeanIf() {
			return FighterInfo.this.toBeanIf();
		}

		@Override
		public int getFighterid() { // 
			_xdb_verify_unsafe_();
			return fighterid;
		}

		@Override
		public int getFightertype() { // 
			_xdb_verify_unsafe_();
			return fightertype;
		}

		@Override
		public int getPos() { // 
			_xdb_verify_unsafe_();
			return pos;
		}

		@Override
		public int getHeroid() { // 
			_xdb_verify_unsafe_();
			return heroid;
		}

		@Override
		public int getGrouptype() { // 阵营
			_xdb_verify_unsafe_();
			return grouptype;
		}

		@Override
		public int getLevel() { // 等级
			_xdb_verify_unsafe_();
			return level;
		}

		@Override
		public int getColor() { // 颜色
			_xdb_verify_unsafe_();
			return color;
		}

		@Override
		public int getGrade() { // 阶
			_xdb_verify_unsafe_();
			return grade;
		}

		@Override
		public int getWeaponinfo() { // 武器信息
			_xdb_verify_unsafe_();
			return weaponinfo;
		}

		@Override
		public int getArmorinfo() { // 铠甲信息
			_xdb_verify_unsafe_();
			return armorinfo;
		}

		@Override
		public int getHorseinfo() { // 战马信息
			_xdb_verify_unsafe_();
			return horseinfo;
		}

		@Override
		public int getSpeed() { // 速
			_xdb_verify_unsafe_();
			return speed;
		}

		@Override
		public int getHp() { // 兵力
			_xdb_verify_unsafe_();
			return hp;
		}

		@Override
		public xbean.BasicFightProperties getBfp() { // 基础战斗属性
			_xdb_verify_unsafe_();
			return xdb.Consts.toConst(bfp);
		}

		@Override
		public java.util.Map<Integer, Float> getEffects() { // 效果 key = effect type id
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(effects);
		}

		@Override
		public java.util.Map<Integer, Float> getEffectsAsData() { // 效果 key = effect type id
			_xdb_verify_unsafe_();
			java.util.Map<Integer, Float> effects;
			FighterInfo _o_ = FighterInfo.this;
			effects = new java.util.HashMap<Integer, Float>();
			for (java.util.Map.Entry<Integer, Float> _e_ : _o_.effects.entrySet())
				effects.put(_e_.getKey(), _e_.getValue());
			return effects;
		}

		@Override
		public java.util.Map<Integer, Float> getFinalattrs() { // 最终属性 key = attr type
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(finalattrs);
		}

		@Override
		public java.util.Map<Integer, Float> getFinalattrsAsData() { // 最终属性 key = attr type
			_xdb_verify_unsafe_();
			java.util.Map<Integer, Float> finalattrs;
			FighterInfo _o_ = FighterInfo.this;
			finalattrs = new java.util.HashMap<Integer, Float>();
			for (java.util.Map.Entry<Integer, Float> _e_ : _o_.finalattrs.entrySet())
				finalattrs.put(_e_.getKey(), _e_.getValue());
			return finalattrs;
		}

		@Override
		public xbean.BuffAgent getBuffagent() { // buff代理
			_xdb_verify_unsafe_();
			return xdb.Consts.toConst(buffagent);
		}

		@Override
		public java.util.List<xbean.BattleSkill> getSkills() { // 技能
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(skills);
		}

		public java.util.List<xbean.BattleSkill> getSkillsAsData() { // 技能
			_xdb_verify_unsafe_();
			java.util.List<xbean.BattleSkill> skills;
			FighterInfo _o_ = FighterInfo.this;
		skills = new java.util.LinkedList<xbean.BattleSkill>();
		for (xbean.BattleSkill _v_ : _o_.skills)
			skills.add(new BattleSkill.Data(_v_));
			return skills;
		}

		@Override
		public int getShape() { // 造型ID
			_xdb_verify_unsafe_();
			return shape;
		}

		@Override
		public void setFighterid(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setFightertype(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setPos(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHeroid(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setGrouptype(int _v_) { // 阵营
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setLevel(int _v_) { // 等级
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setColor(int _v_) { // 颜色
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setGrade(int _v_) { // 阶
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setWeaponinfo(int _v_) { // 武器信息
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setArmorinfo(int _v_) { // 铠甲信息
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHorseinfo(int _v_) { // 战马信息
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setSpeed(int _v_) { // 速
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHp(int _v_) { // 兵力
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setShape(int _v_) { // 造型ID
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
			return FighterInfo.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return FighterInfo.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return FighterInfo.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return FighterInfo.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return FighterInfo.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return FighterInfo.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return FighterInfo.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return FighterInfo.this.hashCode();
		}

		@Override
		public String toString() {
			return FighterInfo.this.toString();
		}

	}

	public static final class Data implements xbean.FighterInfo {
		private int fighterid; // 
		private int fightertype; // 
		private int pos; // 
		private int heroid; // 
		private int grouptype; // 阵营
		private int level; // 等级
		private int color; // 颜色
		private int grade; // 阶
		private int weaponinfo; // 武器信息
		private int armorinfo; // 铠甲信息
		private int horseinfo; // 战马信息
		private int speed; // 速
		private int hp; // 兵力
		private xbean.BasicFightProperties bfp; // 基础战斗属性
		private java.util.HashMap<Integer, Float> effects; // 效果 key = effect type id
		private java.util.HashMap<Integer, Float> finalattrs; // 最终属性 key = attr type
		private xbean.BuffAgent buffagent; // buff代理
		private java.util.LinkedList<xbean.BattleSkill> skills; // 技能
		private int shape; // 造型ID

		public Data() {
			level = 1;
			color = 0;
			grade = 0;
			weaponinfo = 0;
			armorinfo = 0;
			horseinfo = 0;
			speed = 0;
			bfp = new BasicFightProperties.Data();
			effects = new java.util.HashMap<Integer, Float>();
			finalattrs = new java.util.HashMap<Integer, Float>();
			buffagent = new BuffAgent.Data();
			skills = new java.util.LinkedList<xbean.BattleSkill>();
		}

		Data(xbean.FighterInfo _o1_) {
			if (_o1_ instanceof FighterInfo) assign((FighterInfo)_o1_);
			else if (_o1_ instanceof FighterInfo.Data) assign((FighterInfo.Data)_o1_);
			else if (_o1_ instanceof FighterInfo.Const) assign(((FighterInfo.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(FighterInfo _o_) {
			fighterid = _o_.fighterid;
			fightertype = _o_.fightertype;
			pos = _o_.pos;
			heroid = _o_.heroid;
			grouptype = _o_.grouptype;
			level = _o_.level;
			color = _o_.color;
			grade = _o_.grade;
			weaponinfo = _o_.weaponinfo;
			armorinfo = _o_.armorinfo;
			horseinfo = _o_.horseinfo;
			speed = _o_.speed;
			hp = _o_.hp;
			bfp = new BasicFightProperties.Data(_o_.bfp);
			effects = new java.util.HashMap<Integer, Float>();
			for (java.util.Map.Entry<Integer, Float> _e_ : _o_.effects.entrySet())
				effects.put(_e_.getKey(), _e_.getValue());
			finalattrs = new java.util.HashMap<Integer, Float>();
			for (java.util.Map.Entry<Integer, Float> _e_ : _o_.finalattrs.entrySet())
				finalattrs.put(_e_.getKey(), _e_.getValue());
			buffagent = new BuffAgent.Data(_o_.buffagent);
			skills = new java.util.LinkedList<xbean.BattleSkill>();
			for (xbean.BattleSkill _v_ : _o_.skills)
				skills.add(new BattleSkill.Data(_v_));
			shape = _o_.shape;
		}

		private void assign(FighterInfo.Data _o_) {
			fighterid = _o_.fighterid;
			fightertype = _o_.fightertype;
			pos = _o_.pos;
			heroid = _o_.heroid;
			grouptype = _o_.grouptype;
			level = _o_.level;
			color = _o_.color;
			grade = _o_.grade;
			weaponinfo = _o_.weaponinfo;
			armorinfo = _o_.armorinfo;
			horseinfo = _o_.horseinfo;
			speed = _o_.speed;
			hp = _o_.hp;
			bfp = new BasicFightProperties.Data(_o_.bfp);
			effects = new java.util.HashMap<Integer, Float>();
			for (java.util.Map.Entry<Integer, Float> _e_ : _o_.effects.entrySet())
				effects.put(_e_.getKey(), _e_.getValue());
			finalattrs = new java.util.HashMap<Integer, Float>();
			for (java.util.Map.Entry<Integer, Float> _e_ : _o_.finalattrs.entrySet())
				finalattrs.put(_e_.getKey(), _e_.getValue());
			buffagent = new BuffAgent.Data(_o_.buffagent);
			skills = new java.util.LinkedList<xbean.BattleSkill>();
			for (xbean.BattleSkill _v_ : _o_.skills)
				skills.add(new BattleSkill.Data(_v_));
			shape = _o_.shape;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(fighterid);
			_os_.marshal(fightertype);
			_os_.marshal(pos);
			_os_.marshal(heroid);
			_os_.marshal(grouptype);
			_os_.marshal(level);
			_os_.marshal(color);
			_os_.marshal(grade);
			_os_.marshal(weaponinfo);
			_os_.marshal(armorinfo);
			_os_.marshal(horseinfo);
			_os_.marshal(speed);
			_os_.marshal(hp);
			bfp.marshal(_os_);
			_os_.compact_uint32(effects.size());
			for (java.util.Map.Entry<Integer, Float> _e_ : effects.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_os_.marshal(_e_.getValue());
			}
			_os_.compact_uint32(finalattrs.size());
			for (java.util.Map.Entry<Integer, Float> _e_ : finalattrs.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_os_.marshal(_e_.getValue());
			}
			buffagent.marshal(_os_);
			_os_.compact_uint32(skills.size());
			for (xbean.BattleSkill _v_ : skills) {
				_v_.marshal(_os_);
			}
			_os_.marshal(shape);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			fighterid = _os_.unmarshal_int();
			fightertype = _os_.unmarshal_int();
			pos = _os_.unmarshal_int();
			heroid = _os_.unmarshal_int();
			grouptype = _os_.unmarshal_int();
			level = _os_.unmarshal_int();
			color = _os_.unmarshal_int();
			grade = _os_.unmarshal_int();
			weaponinfo = _os_.unmarshal_int();
			armorinfo = _os_.unmarshal_int();
			horseinfo = _os_.unmarshal_int();
			speed = _os_.unmarshal_int();
			hp = _os_.unmarshal_int();
			bfp.unmarshal(_os_);
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					effects = new java.util.HashMap<Integer, Float>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					float _v_ = 0.0f;
					_v_ = _os_.unmarshal_float();
					effects.put(_k_, _v_);
				}
			}
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					finalattrs = new java.util.HashMap<Integer, Float>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					float _v_ = 0.0f;
					_v_ = _os_.unmarshal_float();
					finalattrs.put(_k_, _v_);
				}
			}
			buffagent.unmarshal(_os_);
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				xbean.BattleSkill _v_ = xbean.Pod.newBattleSkillData();
				_v_.unmarshal(_os_);
				skills.add(_v_);
			}
			shape = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.FighterInfo copy() {
			return new Data(this);
		}

		@Override
		public xbean.FighterInfo toData() {
			return new Data(this);
		}

		public xbean.FighterInfo toBean() {
			return new FighterInfo(this, null, null);
		}

		@Override
		public xbean.FighterInfo toDataIf() {
			return this;
		}

		public xbean.FighterInfo toBeanIf() {
			return new FighterInfo(this, null, null);
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
		public int getFighterid() { // 
			return fighterid;
		}

		@Override
		public int getFightertype() { // 
			return fightertype;
		}

		@Override
		public int getPos() { // 
			return pos;
		}

		@Override
		public int getHeroid() { // 
			return heroid;
		}

		@Override
		public int getGrouptype() { // 阵营
			return grouptype;
		}

		@Override
		public int getLevel() { // 等级
			return level;
		}

		@Override
		public int getColor() { // 颜色
			return color;
		}

		@Override
		public int getGrade() { // 阶
			return grade;
		}

		@Override
		public int getWeaponinfo() { // 武器信息
			return weaponinfo;
		}

		@Override
		public int getArmorinfo() { // 铠甲信息
			return armorinfo;
		}

		@Override
		public int getHorseinfo() { // 战马信息
			return horseinfo;
		}

		@Override
		public int getSpeed() { // 速
			return speed;
		}

		@Override
		public int getHp() { // 兵力
			return hp;
		}

		@Override
		public xbean.BasicFightProperties getBfp() { // 基础战斗属性
			return bfp;
		}

		@Override
		public java.util.Map<Integer, Float> getEffects() { // 效果 key = effect type id
			return effects;
		}

		@Override
		public java.util.Map<Integer, Float> getEffectsAsData() { // 效果 key = effect type id
			return effects;
		}

		@Override
		public java.util.Map<Integer, Float> getFinalattrs() { // 最终属性 key = attr type
			return finalattrs;
		}

		@Override
		public java.util.Map<Integer, Float> getFinalattrsAsData() { // 最终属性 key = attr type
			return finalattrs;
		}

		@Override
		public xbean.BuffAgent getBuffagent() { // buff代理
			return buffagent;
		}

		@Override
		public java.util.List<xbean.BattleSkill> getSkills() { // 技能
			return skills;
		}

		@Override
		public java.util.List<xbean.BattleSkill> getSkillsAsData() { // 技能
			return skills;
		}

		@Override
		public int getShape() { // 造型ID
			return shape;
		}

		@Override
		public void setFighterid(int _v_) { // 
			fighterid = _v_;
		}

		@Override
		public void setFightertype(int _v_) { // 
			fightertype = _v_;
		}

		@Override
		public void setPos(int _v_) { // 
			pos = _v_;
		}

		@Override
		public void setHeroid(int _v_) { // 
			heroid = _v_;
		}

		@Override
		public void setGrouptype(int _v_) { // 阵营
			grouptype = _v_;
		}

		@Override
		public void setLevel(int _v_) { // 等级
			level = _v_;
		}

		@Override
		public void setColor(int _v_) { // 颜色
			color = _v_;
		}

		@Override
		public void setGrade(int _v_) { // 阶
			grade = _v_;
		}

		@Override
		public void setWeaponinfo(int _v_) { // 武器信息
			weaponinfo = _v_;
		}

		@Override
		public void setArmorinfo(int _v_) { // 铠甲信息
			armorinfo = _v_;
		}

		@Override
		public void setHorseinfo(int _v_) { // 战马信息
			horseinfo = _v_;
		}

		@Override
		public void setSpeed(int _v_) { // 速
			speed = _v_;
		}

		@Override
		public void setHp(int _v_) { // 兵力
			hp = _v_;
		}

		@Override
		public void setShape(int _v_) { // 造型ID
			shape = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof FighterInfo.Data)) return false;
			FighterInfo.Data _o_ = (FighterInfo.Data) _o1_;
			if (fighterid != _o_.fighterid) return false;
			if (fightertype != _o_.fightertype) return false;
			if (pos != _o_.pos) return false;
			if (heroid != _o_.heroid) return false;
			if (grouptype != _o_.grouptype) return false;
			if (level != _o_.level) return false;
			if (color != _o_.color) return false;
			if (grade != _o_.grade) return false;
			if (weaponinfo != _o_.weaponinfo) return false;
			if (armorinfo != _o_.armorinfo) return false;
			if (horseinfo != _o_.horseinfo) return false;
			if (speed != _o_.speed) return false;
			if (hp != _o_.hp) return false;
			if (!bfp.equals(_o_.bfp)) return false;
			if (!effects.equals(_o_.effects)) return false;
			if (!finalattrs.equals(_o_.finalattrs)) return false;
			if (!buffagent.equals(_o_.buffagent)) return false;
			if (!skills.equals(_o_.skills)) return false;
			if (shape != _o_.shape) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += fighterid;
			_h_ += fightertype;
			_h_ += pos;
			_h_ += heroid;
			_h_ += grouptype;
			_h_ += level;
			_h_ += color;
			_h_ += grade;
			_h_ += weaponinfo;
			_h_ += armorinfo;
			_h_ += horseinfo;
			_h_ += speed;
			_h_ += hp;
			_h_ += bfp.hashCode();
			_h_ += effects.hashCode();
			_h_ += finalattrs.hashCode();
			_h_ += buffagent.hashCode();
			_h_ += skills.hashCode();
			_h_ += shape;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(fighterid);
			_sb_.append(",");
			_sb_.append(fightertype);
			_sb_.append(",");
			_sb_.append(pos);
			_sb_.append(",");
			_sb_.append(heroid);
			_sb_.append(",");
			_sb_.append(grouptype);
			_sb_.append(",");
			_sb_.append(level);
			_sb_.append(",");
			_sb_.append(color);
			_sb_.append(",");
			_sb_.append(grade);
			_sb_.append(",");
			_sb_.append(weaponinfo);
			_sb_.append(",");
			_sb_.append(armorinfo);
			_sb_.append(",");
			_sb_.append(horseinfo);
			_sb_.append(",");
			_sb_.append(speed);
			_sb_.append(",");
			_sb_.append(hp);
			_sb_.append(",");
			_sb_.append(bfp);
			_sb_.append(",");
			_sb_.append(effects);
			_sb_.append(",");
			_sb_.append(finalattrs);
			_sb_.append(",");
			_sb_.append(buffagent);
			_sb_.append(",");
			_sb_.append(skills);
			_sb_.append(",");
			_sb_.append(shape);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
