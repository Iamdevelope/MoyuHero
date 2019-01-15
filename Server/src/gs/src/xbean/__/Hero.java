
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class Hero extends xdb.XBean implements xbean.Hero {
	private int key; // 英雄唯一ID（新系统可能不需要）
	private int heroid; // 英雄配表ID
	private int heroexp; // 英雄本级经验
	private int herolevel; // 英雄等级
	private int heroviewid; // 英雄外观
	private int herojinjiestar; // 进阶星级
	private int herojinjiesmall; // 进阶阶级
	private int heropinji; // 品质（升品换英雄配表ID）
	private String heroskill; // 技能（:分割，根据位置记录技能等级）
	private String heromishu; // 秘术（:一级分割，|二级分割，根据位置记录秘术等级和秘术经验）
	private String heropeiyang; // 培养（:分割，根据位置记录培养等级）
	private String heroequip; // 装备（:一级分割，|二级分割，根据位置记录装备ID和强化等级）

	Hero(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		heroskill = "";
		heromishu = "";
		heropeiyang = "";
		heroequip = "";
	}

	public Hero() {
		this(0, null, null);
	}

	public Hero(Hero _o_) {
		this(_o_, null, null);
	}

	Hero(xbean.Hero _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof Hero) assign((Hero)_o1_);
		else if (_o1_ instanceof Hero.Data) assign((Hero.Data)_o1_);
		else if (_o1_ instanceof Hero.Const) assign(((Hero.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(Hero _o_) {
		_o_._xdb_verify_unsafe_();
		key = _o_.key;
		heroid = _o_.heroid;
		heroexp = _o_.heroexp;
		herolevel = _o_.herolevel;
		heroviewid = _o_.heroviewid;
		herojinjiestar = _o_.herojinjiestar;
		herojinjiesmall = _o_.herojinjiesmall;
		heropinji = _o_.heropinji;
		heroskill = _o_.heroskill;
		heromishu = _o_.heromishu;
		heropeiyang = _o_.heropeiyang;
		heroequip = _o_.heroequip;
	}

	private void assign(Hero.Data _o_) {
		key = _o_.key;
		heroid = _o_.heroid;
		heroexp = _o_.heroexp;
		herolevel = _o_.herolevel;
		heroviewid = _o_.heroviewid;
		herojinjiestar = _o_.herojinjiestar;
		herojinjiesmall = _o_.herojinjiesmall;
		heropinji = _o_.heropinji;
		heroskill = _o_.heroskill;
		heromishu = _o_.heromishu;
		heropeiyang = _o_.heropeiyang;
		heroequip = _o_.heroequip;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(key);
		_os_.marshal(heroid);
		_os_.marshal(heroexp);
		_os_.marshal(herolevel);
		_os_.marshal(heroviewid);
		_os_.marshal(herojinjiestar);
		_os_.marshal(herojinjiesmall);
		_os_.marshal(heropinji);
		_os_.marshal(heroskill, xdb.Const.IO_CHARSET);
		_os_.marshal(heromishu, xdb.Const.IO_CHARSET);
		_os_.marshal(heropeiyang, xdb.Const.IO_CHARSET);
		_os_.marshal(heroequip, xdb.Const.IO_CHARSET);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		key = _os_.unmarshal_int();
		heroid = _os_.unmarshal_int();
		heroexp = _os_.unmarshal_int();
		herolevel = _os_.unmarshal_int();
		heroviewid = _os_.unmarshal_int();
		herojinjiestar = _os_.unmarshal_int();
		herojinjiesmall = _os_.unmarshal_int();
		heropinji = _os_.unmarshal_int();
		heroskill = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
		heromishu = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
		heropeiyang = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
		heroequip = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
		return _os_;
	}

	@Override
	public xbean.Hero copy() {
		_xdb_verify_unsafe_();
		return new Hero(this);
	}

	@Override
	public xbean.Hero toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.Hero toBean() {
		_xdb_verify_unsafe_();
		return new Hero(this); // same as copy()
	}

	@Override
	public xbean.Hero toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.Hero toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getKey() { // 英雄唯一ID（新系统可能不需要）
		_xdb_verify_unsafe_();
		return key;
	}

	@Override
	public int getHeroid() { // 英雄配表ID
		_xdb_verify_unsafe_();
		return heroid;
	}

	@Override
	public int getHeroexp() { // 英雄本级经验
		_xdb_verify_unsafe_();
		return heroexp;
	}

	@Override
	public int getHerolevel() { // 英雄等级
		_xdb_verify_unsafe_();
		return herolevel;
	}

	@Override
	public int getHeroviewid() { // 英雄外观
		_xdb_verify_unsafe_();
		return heroviewid;
	}

	@Override
	public int getHerojinjiestar() { // 进阶星级
		_xdb_verify_unsafe_();
		return herojinjiestar;
	}

	@Override
	public int getHerojinjiesmall() { // 进阶阶级
		_xdb_verify_unsafe_();
		return herojinjiesmall;
	}

	@Override
	public int getHeropinji() { // 品质（升品换英雄配表ID）
		_xdb_verify_unsafe_();
		return heropinji;
	}

	@Override
	public String getHeroskill() { // 技能（:分割，根据位置记录技能等级）
		_xdb_verify_unsafe_();
		return heroskill;
	}

	@Override
	public com.goldhuman.Common.Octets getHeroskillOctets() { // 技能（:分割，根据位置记录技能等级）
		_xdb_verify_unsafe_();
		return com.goldhuman.Common.Octets.wrap(getHeroskill(), xdb.Const.IO_CHARSET);
	}

	@Override
	public String getHeromishu() { // 秘术（:一级分割，|二级分割，根据位置记录秘术等级和秘术经验）
		_xdb_verify_unsafe_();
		return heromishu;
	}

	@Override
	public com.goldhuman.Common.Octets getHeromishuOctets() { // 秘术（:一级分割，|二级分割，根据位置记录秘术等级和秘术经验）
		_xdb_verify_unsafe_();
		return com.goldhuman.Common.Octets.wrap(getHeromishu(), xdb.Const.IO_CHARSET);
	}

	@Override
	public String getHeropeiyang() { // 培养（:分割，根据位置记录培养等级）
		_xdb_verify_unsafe_();
		return heropeiyang;
	}

	@Override
	public com.goldhuman.Common.Octets getHeropeiyangOctets() { // 培养（:分割，根据位置记录培养等级）
		_xdb_verify_unsafe_();
		return com.goldhuman.Common.Octets.wrap(getHeropeiyang(), xdb.Const.IO_CHARSET);
	}

	@Override
	public String getHeroequip() { // 装备（:一级分割，|二级分割，根据位置记录装备ID和强化等级）
		_xdb_verify_unsafe_();
		return heroequip;
	}

	@Override
	public com.goldhuman.Common.Octets getHeroequipOctets() { // 装备（:一级分割，|二级分割，根据位置记录装备ID和强化等级）
		_xdb_verify_unsafe_();
		return com.goldhuman.Common.Octets.wrap(getHeroequip(), xdb.Const.IO_CHARSET);
	}

	@Override
	public void setKey(int _v_) { // 英雄唯一ID（新系统可能不需要）
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "key") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, key) {
					public void rollback() { key = _xdb_saved; }
				};}});
		key = _v_;
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
	public void setHeroexp(int _v_) { // 英雄本级经验
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "heroexp") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, heroexp) {
					public void rollback() { heroexp = _xdb_saved; }
				};}});
		heroexp = _v_;
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
	public void setHerojinjiestar(int _v_) { // 进阶星级
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "herojinjiestar") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, herojinjiestar) {
					public void rollback() { herojinjiestar = _xdb_saved; }
				};}});
		herojinjiestar = _v_;
	}

	@Override
	public void setHerojinjiesmall(int _v_) { // 进阶阶级
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "herojinjiesmall") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, herojinjiesmall) {
					public void rollback() { herojinjiesmall = _xdb_saved; }
				};}});
		herojinjiesmall = _v_;
	}

	@Override
	public void setHeropinji(int _v_) { // 品质（升品换英雄配表ID）
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "heropinji") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, heropinji) {
					public void rollback() { heropinji = _xdb_saved; }
				};}});
		heropinji = _v_;
	}

	@Override
	public void setHeroskill(String _v_) { // 技能（:分割，根据位置记录技能等级）
		_xdb_verify_unsafe_();
		if (null == _v_)
			throw new NullPointerException();
		xdb.Logs.logIf(new xdb.LogKey(this, "heroskill") {
			protected xdb.Log create() {
				return new xdb.logs.LogString(this, heroskill) {
					public void rollback() { heroskill = _xdb_saved; }
				};}});
		heroskill = _v_;
	}

	@Override
	public void setHeroskillOctets(com.goldhuman.Common.Octets _v_) { // 技能（:分割，根据位置记录技能等级）
		_xdb_verify_unsafe_();
		this.setHeroskill(_v_.getString(xdb.Const.IO_CHARSET));
	}

	@Override
	public void setHeromishu(String _v_) { // 秘术（:一级分割，|二级分割，根据位置记录秘术等级和秘术经验）
		_xdb_verify_unsafe_();
		if (null == _v_)
			throw new NullPointerException();
		xdb.Logs.logIf(new xdb.LogKey(this, "heromishu") {
			protected xdb.Log create() {
				return new xdb.logs.LogString(this, heromishu) {
					public void rollback() { heromishu = _xdb_saved; }
				};}});
		heromishu = _v_;
	}

	@Override
	public void setHeromishuOctets(com.goldhuman.Common.Octets _v_) { // 秘术（:一级分割，|二级分割，根据位置记录秘术等级和秘术经验）
		_xdb_verify_unsafe_();
		this.setHeromishu(_v_.getString(xdb.Const.IO_CHARSET));
	}

	@Override
	public void setHeropeiyang(String _v_) { // 培养（:分割，根据位置记录培养等级）
		_xdb_verify_unsafe_();
		if (null == _v_)
			throw new NullPointerException();
		xdb.Logs.logIf(new xdb.LogKey(this, "heropeiyang") {
			protected xdb.Log create() {
				return new xdb.logs.LogString(this, heropeiyang) {
					public void rollback() { heropeiyang = _xdb_saved; }
				};}});
		heropeiyang = _v_;
	}

	@Override
	public void setHeropeiyangOctets(com.goldhuman.Common.Octets _v_) { // 培养（:分割，根据位置记录培养等级）
		_xdb_verify_unsafe_();
		this.setHeropeiyang(_v_.getString(xdb.Const.IO_CHARSET));
	}

	@Override
	public void setHeroequip(String _v_) { // 装备（:一级分割，|二级分割，根据位置记录装备ID和强化等级）
		_xdb_verify_unsafe_();
		if (null == _v_)
			throw new NullPointerException();
		xdb.Logs.logIf(new xdb.LogKey(this, "heroequip") {
			protected xdb.Log create() {
				return new xdb.logs.LogString(this, heroequip) {
					public void rollback() { heroequip = _xdb_saved; }
				};}});
		heroequip = _v_;
	}

	@Override
	public void setHeroequipOctets(com.goldhuman.Common.Octets _v_) { // 装备（:一级分割，|二级分割，根据位置记录装备ID和强化等级）
		_xdb_verify_unsafe_();
		this.setHeroequip(_v_.getString(xdb.Const.IO_CHARSET));
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		Hero _o_ = null;
		if ( _o1_ instanceof Hero ) _o_ = (Hero)_o1_;
		else if ( _o1_ instanceof Hero.Const ) _o_ = ((Hero.Const)_o1_).nThis();
		else return false;
		if (key != _o_.key) return false;
		if (heroid != _o_.heroid) return false;
		if (heroexp != _o_.heroexp) return false;
		if (herolevel != _o_.herolevel) return false;
		if (heroviewid != _o_.heroviewid) return false;
		if (herojinjiestar != _o_.herojinjiestar) return false;
		if (herojinjiesmall != _o_.herojinjiesmall) return false;
		if (heropinji != _o_.heropinji) return false;
		if (!heroskill.equals(_o_.heroskill)) return false;
		if (!heromishu.equals(_o_.heromishu)) return false;
		if (!heropeiyang.equals(_o_.heropeiyang)) return false;
		if (!heroequip.equals(_o_.heroequip)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += key;
		_h_ += heroid;
		_h_ += heroexp;
		_h_ += herolevel;
		_h_ += heroviewid;
		_h_ += herojinjiestar;
		_h_ += herojinjiesmall;
		_h_ += heropinji;
		_h_ += heroskill.hashCode();
		_h_ += heromishu.hashCode();
		_h_ += heropeiyang.hashCode();
		_h_ += heroequip.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(key);
		_sb_.append(",");
		_sb_.append(heroid);
		_sb_.append(",");
		_sb_.append(heroexp);
		_sb_.append(",");
		_sb_.append(herolevel);
		_sb_.append(",");
		_sb_.append(heroviewid);
		_sb_.append(",");
		_sb_.append(herojinjiestar);
		_sb_.append(",");
		_sb_.append(herojinjiesmall);
		_sb_.append(",");
		_sb_.append(heropinji);
		_sb_.append(",");
		_sb_.append("'").append(heroskill).append("'");
		_sb_.append(",");
		_sb_.append("'").append(heromishu).append("'");
		_sb_.append(",");
		_sb_.append("'").append(heropeiyang).append("'");
		_sb_.append(",");
		_sb_.append("'").append(heroequip).append("'");
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("key"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("heroid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("heroexp"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("herolevel"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("heroviewid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("herojinjiestar"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("herojinjiesmall"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("heropinji"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("heroskill"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("heromishu"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("heropeiyang"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("heroequip"));
		return lb;
	}

	private class Const implements xbean.Hero {
		Hero nThis() {
			return Hero.this;
		}

		@Override
		public xbean.Hero copy() {
			return Hero.this.copy();
		}

		@Override
		public xbean.Hero toData() {
			return Hero.this.toData();
		}

		public xbean.Hero toBean() {
			return Hero.this.toBean();
		}

		@Override
		public xbean.Hero toDataIf() {
			return Hero.this.toDataIf();
		}

		public xbean.Hero toBeanIf() {
			return Hero.this.toBeanIf();
		}

		@Override
		public int getKey() { // 英雄唯一ID（新系统可能不需要）
			_xdb_verify_unsafe_();
			return key;
		}

		@Override
		public int getHeroid() { // 英雄配表ID
			_xdb_verify_unsafe_();
			return heroid;
		}

		@Override
		public int getHeroexp() { // 英雄本级经验
			_xdb_verify_unsafe_();
			return heroexp;
		}

		@Override
		public int getHerolevel() { // 英雄等级
			_xdb_verify_unsafe_();
			return herolevel;
		}

		@Override
		public int getHeroviewid() { // 英雄外观
			_xdb_verify_unsafe_();
			return heroviewid;
		}

		@Override
		public int getHerojinjiestar() { // 进阶星级
			_xdb_verify_unsafe_();
			return herojinjiestar;
		}

		@Override
		public int getHerojinjiesmall() { // 进阶阶级
			_xdb_verify_unsafe_();
			return herojinjiesmall;
		}

		@Override
		public int getHeropinji() { // 品质（升品换英雄配表ID）
			_xdb_verify_unsafe_();
			return heropinji;
		}

		@Override
		public String getHeroskill() { // 技能（:分割，根据位置记录技能等级）
			_xdb_verify_unsafe_();
			return heroskill;
		}

		@Override
		public com.goldhuman.Common.Octets getHeroskillOctets() { // 技能（:分割，根据位置记录技能等级）
			_xdb_verify_unsafe_();
			return Hero.this.getHeroskillOctets();
		}

		@Override
		public String getHeromishu() { // 秘术（:一级分割，|二级分割，根据位置记录秘术等级和秘术经验）
			_xdb_verify_unsafe_();
			return heromishu;
		}

		@Override
		public com.goldhuman.Common.Octets getHeromishuOctets() { // 秘术（:一级分割，|二级分割，根据位置记录秘术等级和秘术经验）
			_xdb_verify_unsafe_();
			return Hero.this.getHeromishuOctets();
		}

		@Override
		public String getHeropeiyang() { // 培养（:分割，根据位置记录培养等级）
			_xdb_verify_unsafe_();
			return heropeiyang;
		}

		@Override
		public com.goldhuman.Common.Octets getHeropeiyangOctets() { // 培养（:分割，根据位置记录培养等级）
			_xdb_verify_unsafe_();
			return Hero.this.getHeropeiyangOctets();
		}

		@Override
		public String getHeroequip() { // 装备（:一级分割，|二级分割，根据位置记录装备ID和强化等级）
			_xdb_verify_unsafe_();
			return heroequip;
		}

		@Override
		public com.goldhuman.Common.Octets getHeroequipOctets() { // 装备（:一级分割，|二级分割，根据位置记录装备ID和强化等级）
			_xdb_verify_unsafe_();
			return Hero.this.getHeroequipOctets();
		}

		@Override
		public void setKey(int _v_) { // 英雄唯一ID（新系统可能不需要）
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHeroid(int _v_) { // 英雄配表ID
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHeroexp(int _v_) { // 英雄本级经验
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHerolevel(int _v_) { // 英雄等级
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHeroviewid(int _v_) { // 英雄外观
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHerojinjiestar(int _v_) { // 进阶星级
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHerojinjiesmall(int _v_) { // 进阶阶级
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHeropinji(int _v_) { // 品质（升品换英雄配表ID）
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHeroskill(String _v_) { // 技能（:分割，根据位置记录技能等级）
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHeroskillOctets(com.goldhuman.Common.Octets _v_) { // 技能（:分割，根据位置记录技能等级）
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHeromishu(String _v_) { // 秘术（:一级分割，|二级分割，根据位置记录秘术等级和秘术经验）
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHeromishuOctets(com.goldhuman.Common.Octets _v_) { // 秘术（:一级分割，|二级分割，根据位置记录秘术等级和秘术经验）
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHeropeiyang(String _v_) { // 培养（:分割，根据位置记录培养等级）
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHeropeiyangOctets(com.goldhuman.Common.Octets _v_) { // 培养（:分割，根据位置记录培养等级）
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHeroequip(String _v_) { // 装备（:一级分割，|二级分割，根据位置记录装备ID和强化等级）
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHeroequipOctets(com.goldhuman.Common.Octets _v_) { // 装备（:一级分割，|二级分割，根据位置记录装备ID和强化等级）
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
			return Hero.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return Hero.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return Hero.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return Hero.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return Hero.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return Hero.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return Hero.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return Hero.this.hashCode();
		}

		@Override
		public String toString() {
			return Hero.this.toString();
		}

	}

	public static final class Data implements xbean.Hero {
		private int key; // 英雄唯一ID（新系统可能不需要）
		private int heroid; // 英雄配表ID
		private int heroexp; // 英雄本级经验
		private int herolevel; // 英雄等级
		private int heroviewid; // 英雄外观
		private int herojinjiestar; // 进阶星级
		private int herojinjiesmall; // 进阶阶级
		private int heropinji; // 品质（升品换英雄配表ID）
		private String heroskill; // 技能（:分割，根据位置记录技能等级）
		private String heromishu; // 秘术（:一级分割，|二级分割，根据位置记录秘术等级和秘术经验）
		private String heropeiyang; // 培养（:分割，根据位置记录培养等级）
		private String heroequip; // 装备（:一级分割，|二级分割，根据位置记录装备ID和强化等级）

		public Data() {
			heroskill = "";
			heromishu = "";
			heropeiyang = "";
			heroequip = "";
		}

		Data(xbean.Hero _o1_) {
			if (_o1_ instanceof Hero) assign((Hero)_o1_);
			else if (_o1_ instanceof Hero.Data) assign((Hero.Data)_o1_);
			else if (_o1_ instanceof Hero.Const) assign(((Hero.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(Hero _o_) {
			key = _o_.key;
			heroid = _o_.heroid;
			heroexp = _o_.heroexp;
			herolevel = _o_.herolevel;
			heroviewid = _o_.heroviewid;
			herojinjiestar = _o_.herojinjiestar;
			herojinjiesmall = _o_.herojinjiesmall;
			heropinji = _o_.heropinji;
			heroskill = _o_.heroskill;
			heromishu = _o_.heromishu;
			heropeiyang = _o_.heropeiyang;
			heroequip = _o_.heroequip;
		}

		private void assign(Hero.Data _o_) {
			key = _o_.key;
			heroid = _o_.heroid;
			heroexp = _o_.heroexp;
			herolevel = _o_.herolevel;
			heroviewid = _o_.heroviewid;
			herojinjiestar = _o_.herojinjiestar;
			herojinjiesmall = _o_.herojinjiesmall;
			heropinji = _o_.heropinji;
			heroskill = _o_.heroskill;
			heromishu = _o_.heromishu;
			heropeiyang = _o_.heropeiyang;
			heroequip = _o_.heroequip;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(key);
			_os_.marshal(heroid);
			_os_.marshal(heroexp);
			_os_.marshal(herolevel);
			_os_.marshal(heroviewid);
			_os_.marshal(herojinjiestar);
			_os_.marshal(herojinjiesmall);
			_os_.marshal(heropinji);
			_os_.marshal(heroskill, xdb.Const.IO_CHARSET);
			_os_.marshal(heromishu, xdb.Const.IO_CHARSET);
			_os_.marshal(heropeiyang, xdb.Const.IO_CHARSET);
			_os_.marshal(heroequip, xdb.Const.IO_CHARSET);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			key = _os_.unmarshal_int();
			heroid = _os_.unmarshal_int();
			heroexp = _os_.unmarshal_int();
			herolevel = _os_.unmarshal_int();
			heroviewid = _os_.unmarshal_int();
			herojinjiestar = _os_.unmarshal_int();
			herojinjiesmall = _os_.unmarshal_int();
			heropinji = _os_.unmarshal_int();
			heroskill = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			heromishu = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			heropeiyang = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			heroequip = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			return _os_;
		}

		@Override
		public xbean.Hero copy() {
			return new Data(this);
		}

		@Override
		public xbean.Hero toData() {
			return new Data(this);
		}

		public xbean.Hero toBean() {
			return new Hero(this, null, null);
		}

		@Override
		public xbean.Hero toDataIf() {
			return this;
		}

		public xbean.Hero toBeanIf() {
			return new Hero(this, null, null);
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
		public int getKey() { // 英雄唯一ID（新系统可能不需要）
			return key;
		}

		@Override
		public int getHeroid() { // 英雄配表ID
			return heroid;
		}

		@Override
		public int getHeroexp() { // 英雄本级经验
			return heroexp;
		}

		@Override
		public int getHerolevel() { // 英雄等级
			return herolevel;
		}

		@Override
		public int getHeroviewid() { // 英雄外观
			return heroviewid;
		}

		@Override
		public int getHerojinjiestar() { // 进阶星级
			return herojinjiestar;
		}

		@Override
		public int getHerojinjiesmall() { // 进阶阶级
			return herojinjiesmall;
		}

		@Override
		public int getHeropinji() { // 品质（升品换英雄配表ID）
			return heropinji;
		}

		@Override
		public String getHeroskill() { // 技能（:分割，根据位置记录技能等级）
			return heroskill;
		}

		@Override
		public com.goldhuman.Common.Octets getHeroskillOctets() { // 技能（:分割，根据位置记录技能等级）
			return com.goldhuman.Common.Octets.wrap(getHeroskill(), xdb.Const.IO_CHARSET);
		}

		@Override
		public String getHeromishu() { // 秘术（:一级分割，|二级分割，根据位置记录秘术等级和秘术经验）
			return heromishu;
		}

		@Override
		public com.goldhuman.Common.Octets getHeromishuOctets() { // 秘术（:一级分割，|二级分割，根据位置记录秘术等级和秘术经验）
			return com.goldhuman.Common.Octets.wrap(getHeromishu(), xdb.Const.IO_CHARSET);
		}

		@Override
		public String getHeropeiyang() { // 培养（:分割，根据位置记录培养等级）
			return heropeiyang;
		}

		@Override
		public com.goldhuman.Common.Octets getHeropeiyangOctets() { // 培养（:分割，根据位置记录培养等级）
			return com.goldhuman.Common.Octets.wrap(getHeropeiyang(), xdb.Const.IO_CHARSET);
		}

		@Override
		public String getHeroequip() { // 装备（:一级分割，|二级分割，根据位置记录装备ID和强化等级）
			return heroequip;
		}

		@Override
		public com.goldhuman.Common.Octets getHeroequipOctets() { // 装备（:一级分割，|二级分割，根据位置记录装备ID和强化等级）
			return com.goldhuman.Common.Octets.wrap(getHeroequip(), xdb.Const.IO_CHARSET);
		}

		@Override
		public void setKey(int _v_) { // 英雄唯一ID（新系统可能不需要）
			key = _v_;
		}

		@Override
		public void setHeroid(int _v_) { // 英雄配表ID
			heroid = _v_;
		}

		@Override
		public void setHeroexp(int _v_) { // 英雄本级经验
			heroexp = _v_;
		}

		@Override
		public void setHerolevel(int _v_) { // 英雄等级
			herolevel = _v_;
		}

		@Override
		public void setHeroviewid(int _v_) { // 英雄外观
			heroviewid = _v_;
		}

		@Override
		public void setHerojinjiestar(int _v_) { // 进阶星级
			herojinjiestar = _v_;
		}

		@Override
		public void setHerojinjiesmall(int _v_) { // 进阶阶级
			herojinjiesmall = _v_;
		}

		@Override
		public void setHeropinji(int _v_) { // 品质（升品换英雄配表ID）
			heropinji = _v_;
		}

		@Override
		public void setHeroskill(String _v_) { // 技能（:分割，根据位置记录技能等级）
			if (null == _v_)
				throw new NullPointerException();
			heroskill = _v_;
		}

		@Override
		public void setHeroskillOctets(com.goldhuman.Common.Octets _v_) { // 技能（:分割，根据位置记录技能等级）
			this.setHeroskill(_v_.getString(xdb.Const.IO_CHARSET));
		}

		@Override
		public void setHeromishu(String _v_) { // 秘术（:一级分割，|二级分割，根据位置记录秘术等级和秘术经验）
			if (null == _v_)
				throw new NullPointerException();
			heromishu = _v_;
		}

		@Override
		public void setHeromishuOctets(com.goldhuman.Common.Octets _v_) { // 秘术（:一级分割，|二级分割，根据位置记录秘术等级和秘术经验）
			this.setHeromishu(_v_.getString(xdb.Const.IO_CHARSET));
		}

		@Override
		public void setHeropeiyang(String _v_) { // 培养（:分割，根据位置记录培养等级）
			if (null == _v_)
				throw new NullPointerException();
			heropeiyang = _v_;
		}

		@Override
		public void setHeropeiyangOctets(com.goldhuman.Common.Octets _v_) { // 培养（:分割，根据位置记录培养等级）
			this.setHeropeiyang(_v_.getString(xdb.Const.IO_CHARSET));
		}

		@Override
		public void setHeroequip(String _v_) { // 装备（:一级分割，|二级分割，根据位置记录装备ID和强化等级）
			if (null == _v_)
				throw new NullPointerException();
			heroequip = _v_;
		}

		@Override
		public void setHeroequipOctets(com.goldhuman.Common.Octets _v_) { // 装备（:一级分割，|二级分割，根据位置记录装备ID和强化等级）
			this.setHeroequip(_v_.getString(xdb.Const.IO_CHARSET));
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof Hero.Data)) return false;
			Hero.Data _o_ = (Hero.Data) _o1_;
			if (key != _o_.key) return false;
			if (heroid != _o_.heroid) return false;
			if (heroexp != _o_.heroexp) return false;
			if (herolevel != _o_.herolevel) return false;
			if (heroviewid != _o_.heroviewid) return false;
			if (herojinjiestar != _o_.herojinjiestar) return false;
			if (herojinjiesmall != _o_.herojinjiesmall) return false;
			if (heropinji != _o_.heropinji) return false;
			if (!heroskill.equals(_o_.heroskill)) return false;
			if (!heromishu.equals(_o_.heromishu)) return false;
			if (!heropeiyang.equals(_o_.heropeiyang)) return false;
			if (!heroequip.equals(_o_.heroequip)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += key;
			_h_ += heroid;
			_h_ += heroexp;
			_h_ += herolevel;
			_h_ += heroviewid;
			_h_ += herojinjiestar;
			_h_ += herojinjiesmall;
			_h_ += heropinji;
			_h_ += heroskill.hashCode();
			_h_ += heromishu.hashCode();
			_h_ += heropeiyang.hashCode();
			_h_ += heroequip.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(key);
			_sb_.append(",");
			_sb_.append(heroid);
			_sb_.append(",");
			_sb_.append(heroexp);
			_sb_.append(",");
			_sb_.append(herolevel);
			_sb_.append(",");
			_sb_.append(heroviewid);
			_sb_.append(",");
			_sb_.append(herojinjiestar);
			_sb_.append(",");
			_sb_.append(herojinjiesmall);
			_sb_.append(",");
			_sb_.append(heropinji);
			_sb_.append(",");
			_sb_.append("'").append(heroskill).append("'");
			_sb_.append(",");
			_sb_.append("'").append(heromishu).append("'");
			_sb_.append(",");
			_sb_.append("'").append(heropeiyang).append("'");
			_sb_.append(",");
			_sb_.append("'").append(heroequip).append("'");
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
