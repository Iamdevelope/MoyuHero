
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class EndlessInfo extends xdb.XBean implements xbean.EndlessInfo {
	private int battleid; // 关卡id
	private int groupnum; // 第几轮
	private java.util.HashMap<Integer, Integer> useherokeylist; // 使用英雄id和位置（key为位置，value为herokey）
	private java.util.LinkedList<Integer> monstergroup; // 怪物组
	private int trooptype; // 战队类型
	private int monstertrooptype; // 怪物战队类型
	private int pact; // 今日战斗强者之约（没有则为-1）
	private int dropnum; // 剩余勇者证明数量
	private int alldropnum; // 勇者证明总数量
	private int add1; // 属性1购买次数
	private int add2; // 属性2购买次数
	private int add3; // 属性3购买次数
	private int add4; // 属性4购买次数（仅计数）
	private java.util.HashMap<Integer, Integer> herobloodlist; // 使用英雄的血量（key为位置，value为血量）
	private int isend; // 0未开始，1进行中，2结束
	private long time; // 此记录时间
	private int ishalfcostpact; // 上次购买的强者之约是否达成（0是达成，1是未达成）
	private long endtime; // 结束时间
	private int expectedrank; // 预期排名
	private java.util.HashMap<Integer, xbean.OtherHero> heroattribute; // 使用英雄的位置和属性信息（key为位置，value为OtherHero属性信息）
	private int onranknum; // 连续在榜次数
	private long onranklasttime; // 最后在榜时间
	private int isnotfirst; // 不是第一次战斗（0是第一次战斗，1不是第一次战斗）

	EndlessInfo(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		useherokeylist = new java.util.HashMap<Integer, Integer>();
		monstergroup = new java.util.LinkedList<Integer>();
		herobloodlist = new java.util.HashMap<Integer, Integer>();
		heroattribute = new java.util.HashMap<Integer, xbean.OtherHero>();
	}

	public EndlessInfo() {
		this(0, null, null);
	}

	public EndlessInfo(EndlessInfo _o_) {
		this(_o_, null, null);
	}

	EndlessInfo(xbean.EndlessInfo _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof EndlessInfo) assign((EndlessInfo)_o1_);
		else if (_o1_ instanceof EndlessInfo.Data) assign((EndlessInfo.Data)_o1_);
		else if (_o1_ instanceof EndlessInfo.Const) assign(((EndlessInfo.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(EndlessInfo _o_) {
		_o_._xdb_verify_unsafe_();
		battleid = _o_.battleid;
		groupnum = _o_.groupnum;
		useherokeylist = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.useherokeylist.entrySet())
			useherokeylist.put(_e_.getKey(), _e_.getValue());
		monstergroup = new java.util.LinkedList<Integer>();
		monstergroup.addAll(_o_.monstergroup);
		trooptype = _o_.trooptype;
		monstertrooptype = _o_.monstertrooptype;
		pact = _o_.pact;
		dropnum = _o_.dropnum;
		alldropnum = _o_.alldropnum;
		add1 = _o_.add1;
		add2 = _o_.add2;
		add3 = _o_.add3;
		add4 = _o_.add4;
		herobloodlist = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.herobloodlist.entrySet())
			herobloodlist.put(_e_.getKey(), _e_.getValue());
		isend = _o_.isend;
		time = _o_.time;
		ishalfcostpact = _o_.ishalfcostpact;
		endtime = _o_.endtime;
		expectedrank = _o_.expectedrank;
		heroattribute = new java.util.HashMap<Integer, xbean.OtherHero>();
		for (java.util.Map.Entry<Integer, xbean.OtherHero> _e_ : _o_.heroattribute.entrySet())
			heroattribute.put(_e_.getKey(), new OtherHero(_e_.getValue(), this, "heroattribute"));
		onranknum = _o_.onranknum;
		onranklasttime = _o_.onranklasttime;
		isnotfirst = _o_.isnotfirst;
	}

	private void assign(EndlessInfo.Data _o_) {
		battleid = _o_.battleid;
		groupnum = _o_.groupnum;
		useherokeylist = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.useherokeylist.entrySet())
			useherokeylist.put(_e_.getKey(), _e_.getValue());
		monstergroup = new java.util.LinkedList<Integer>();
		monstergroup.addAll(_o_.monstergroup);
		trooptype = _o_.trooptype;
		monstertrooptype = _o_.monstertrooptype;
		pact = _o_.pact;
		dropnum = _o_.dropnum;
		alldropnum = _o_.alldropnum;
		add1 = _o_.add1;
		add2 = _o_.add2;
		add3 = _o_.add3;
		add4 = _o_.add4;
		herobloodlist = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.herobloodlist.entrySet())
			herobloodlist.put(_e_.getKey(), _e_.getValue());
		isend = _o_.isend;
		time = _o_.time;
		ishalfcostpact = _o_.ishalfcostpact;
		endtime = _o_.endtime;
		expectedrank = _o_.expectedrank;
		heroattribute = new java.util.HashMap<Integer, xbean.OtherHero>();
		for (java.util.Map.Entry<Integer, xbean.OtherHero> _e_ : _o_.heroattribute.entrySet())
			heroattribute.put(_e_.getKey(), new OtherHero(_e_.getValue(), this, "heroattribute"));
		onranknum = _o_.onranknum;
		onranklasttime = _o_.onranklasttime;
		isnotfirst = _o_.isnotfirst;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(battleid);
		_os_.marshal(groupnum);
		_os_.compact_uint32(useherokeylist.size());
		for (java.util.Map.Entry<Integer, Integer> _e_ : useherokeylist.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.compact_uint32(monstergroup.size());
		for (Integer _v_ : monstergroup) {
			_os_.marshal(_v_);
		}
		_os_.marshal(trooptype);
		_os_.marshal(monstertrooptype);
		_os_.marshal(pact);
		_os_.marshal(dropnum);
		_os_.marshal(alldropnum);
		_os_.marshal(add1);
		_os_.marshal(add2);
		_os_.marshal(add3);
		_os_.marshal(add4);
		_os_.compact_uint32(herobloodlist.size());
		for (java.util.Map.Entry<Integer, Integer> _e_ : herobloodlist.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.marshal(isend);
		_os_.marshal(time);
		_os_.marshal(ishalfcostpact);
		_os_.marshal(endtime);
		_os_.marshal(expectedrank);
		_os_.compact_uint32(heroattribute.size());
		for (java.util.Map.Entry<Integer, xbean.OtherHero> _e_ : heroattribute.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_e_.getValue().marshal(_os_);
		}
		_os_.marshal(onranknum);
		_os_.marshal(onranklasttime);
		_os_.marshal(isnotfirst);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		battleid = _os_.unmarshal_int();
		groupnum = _os_.unmarshal_int();
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				useherokeylist = new java.util.HashMap<Integer, Integer>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				int _v_ = 0;
				_v_ = _os_.unmarshal_int();
				useherokeylist.put(_k_, _v_);
			}
		}
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _v_ = 0;
			_v_ = _os_.unmarshal_int();
			monstergroup.add(_v_);
		}
		trooptype = _os_.unmarshal_int();
		monstertrooptype = _os_.unmarshal_int();
		pact = _os_.unmarshal_int();
		dropnum = _os_.unmarshal_int();
		alldropnum = _os_.unmarshal_int();
		add1 = _os_.unmarshal_int();
		add2 = _os_.unmarshal_int();
		add3 = _os_.unmarshal_int();
		add4 = _os_.unmarshal_int();
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				herobloodlist = new java.util.HashMap<Integer, Integer>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				int _v_ = 0;
				_v_ = _os_.unmarshal_int();
				herobloodlist.put(_k_, _v_);
			}
		}
		isend = _os_.unmarshal_int();
		time = _os_.unmarshal_long();
		ishalfcostpact = _os_.unmarshal_int();
		endtime = _os_.unmarshal_long();
		expectedrank = _os_.unmarshal_int();
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				heroattribute = new java.util.HashMap<Integer, xbean.OtherHero>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				xbean.OtherHero _v_ = new OtherHero(0, this, "heroattribute");
				_v_.unmarshal(_os_);
				heroattribute.put(_k_, _v_);
			}
		}
		onranknum = _os_.unmarshal_int();
		onranklasttime = _os_.unmarshal_long();
		isnotfirst = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.EndlessInfo copy() {
		_xdb_verify_unsafe_();
		return new EndlessInfo(this);
	}

	@Override
	public xbean.EndlessInfo toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.EndlessInfo toBean() {
		_xdb_verify_unsafe_();
		return new EndlessInfo(this); // same as copy()
	}

	@Override
	public xbean.EndlessInfo toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.EndlessInfo toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getBattleid() { // 关卡id
		_xdb_verify_unsafe_();
		return battleid;
	}

	@Override
	public int getGroupnum() { // 第几轮
		_xdb_verify_unsafe_();
		return groupnum;
	}

	@Override
	public java.util.Map<Integer, Integer> getUseherokeylist() { // 使用英雄id和位置（key为位置，value为herokey）
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "useherokeylist"), useherokeylist);
	}

	@Override
	public java.util.Map<Integer, Integer> getUseherokeylistAsData() { // 使用英雄id和位置（key为位置，value为herokey）
		_xdb_verify_unsafe_();
		java.util.Map<Integer, Integer> useherokeylist;
		EndlessInfo _o_ = this;
		useherokeylist = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.useherokeylist.entrySet())
			useherokeylist.put(_e_.getKey(), _e_.getValue());
		return useherokeylist;
	}

	@Override
	public java.util.List<Integer> getMonstergroup() { // 怪物组
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "monstergroup"), monstergroup);
	}

	public java.util.List<Integer> getMonstergroupAsData() { // 怪物组
		_xdb_verify_unsafe_();
		java.util.List<Integer> monstergroup;
		EndlessInfo _o_ = this;
		monstergroup = new java.util.LinkedList<Integer>();
		monstergroup.addAll(_o_.monstergroup);
		return monstergroup;
	}

	@Override
	public int getTrooptype() { // 战队类型
		_xdb_verify_unsafe_();
		return trooptype;
	}

	@Override
	public int getMonstertrooptype() { // 怪物战队类型
		_xdb_verify_unsafe_();
		return monstertrooptype;
	}

	@Override
	public int getPact() { // 今日战斗强者之约（没有则为-1）
		_xdb_verify_unsafe_();
		return pact;
	}

	@Override
	public int getDropnum() { // 剩余勇者证明数量
		_xdb_verify_unsafe_();
		return dropnum;
	}

	@Override
	public int getAlldropnum() { // 勇者证明总数量
		_xdb_verify_unsafe_();
		return alldropnum;
	}

	@Override
	public int getAdd1() { // 属性1购买次数
		_xdb_verify_unsafe_();
		return add1;
	}

	@Override
	public int getAdd2() { // 属性2购买次数
		_xdb_verify_unsafe_();
		return add2;
	}

	@Override
	public int getAdd3() { // 属性3购买次数
		_xdb_verify_unsafe_();
		return add3;
	}

	@Override
	public int getAdd4() { // 属性4购买次数（仅计数）
		_xdb_verify_unsafe_();
		return add4;
	}

	@Override
	public java.util.Map<Integer, Integer> getHerobloodlist() { // 使用英雄的血量（key为位置，value为血量）
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "herobloodlist"), herobloodlist);
	}

	@Override
	public java.util.Map<Integer, Integer> getHerobloodlistAsData() { // 使用英雄的血量（key为位置，value为血量）
		_xdb_verify_unsafe_();
		java.util.Map<Integer, Integer> herobloodlist;
		EndlessInfo _o_ = this;
		herobloodlist = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.herobloodlist.entrySet())
			herobloodlist.put(_e_.getKey(), _e_.getValue());
		return herobloodlist;
	}

	@Override
	public int getIsend() { // 0未开始，1进行中，2结束
		_xdb_verify_unsafe_();
		return isend;
	}

	@Override
	public long getTime() { // 此记录时间
		_xdb_verify_unsafe_();
		return time;
	}

	@Override
	public int getIshalfcostpact() { // 上次购买的强者之约是否达成（0是达成，1是未达成）
		_xdb_verify_unsafe_();
		return ishalfcostpact;
	}

	@Override
	public long getEndtime() { // 结束时间
		_xdb_verify_unsafe_();
		return endtime;
	}

	@Override
	public int getExpectedrank() { // 预期排名
		_xdb_verify_unsafe_();
		return expectedrank;
	}

	@Override
	public java.util.Map<Integer, xbean.OtherHero> getHeroattribute() { // 使用英雄的位置和属性信息（key为位置，value为OtherHero属性信息）
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "heroattribute"), heroattribute);
	}

	@Override
	public java.util.Map<Integer, xbean.OtherHero> getHeroattributeAsData() { // 使用英雄的位置和属性信息（key为位置，value为OtherHero属性信息）
		_xdb_verify_unsafe_();
		java.util.Map<Integer, xbean.OtherHero> heroattribute;
		EndlessInfo _o_ = this;
		heroattribute = new java.util.HashMap<Integer, xbean.OtherHero>();
		for (java.util.Map.Entry<Integer, xbean.OtherHero> _e_ : _o_.heroattribute.entrySet())
			heroattribute.put(_e_.getKey(), new OtherHero.Data(_e_.getValue()));
		return heroattribute;
	}

	@Override
	public int getOnranknum() { // 连续在榜次数
		_xdb_verify_unsafe_();
		return onranknum;
	}

	@Override
	public long getOnranklasttime() { // 最后在榜时间
		_xdb_verify_unsafe_();
		return onranklasttime;
	}

	@Override
	public int getIsnotfirst() { // 不是第一次战斗（0是第一次战斗，1不是第一次战斗）
		_xdb_verify_unsafe_();
		return isnotfirst;
	}

	@Override
	public void setBattleid(int _v_) { // 关卡id
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "battleid") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, battleid) {
					public void rollback() { battleid = _xdb_saved; }
				};}});
		battleid = _v_;
	}

	@Override
	public void setGroupnum(int _v_) { // 第几轮
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "groupnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, groupnum) {
					public void rollback() { groupnum = _xdb_saved; }
				};}});
		groupnum = _v_;
	}

	@Override
	public void setTrooptype(int _v_) { // 战队类型
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "trooptype") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, trooptype) {
					public void rollback() { trooptype = _xdb_saved; }
				};}});
		trooptype = _v_;
	}

	@Override
	public void setMonstertrooptype(int _v_) { // 怪物战队类型
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "monstertrooptype") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, monstertrooptype) {
					public void rollback() { monstertrooptype = _xdb_saved; }
				};}});
		monstertrooptype = _v_;
	}

	@Override
	public void setPact(int _v_) { // 今日战斗强者之约（没有则为-1）
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "pact") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, pact) {
					public void rollback() { pact = _xdb_saved; }
				};}});
		pact = _v_;
	}

	@Override
	public void setDropnum(int _v_) { // 剩余勇者证明数量
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "dropnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, dropnum) {
					public void rollback() { dropnum = _xdb_saved; }
				};}});
		dropnum = _v_;
	}

	@Override
	public void setAlldropnum(int _v_) { // 勇者证明总数量
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "alldropnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, alldropnum) {
					public void rollback() { alldropnum = _xdb_saved; }
				};}});
		alldropnum = _v_;
	}

	@Override
	public void setAdd1(int _v_) { // 属性1购买次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "add1") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, add1) {
					public void rollback() { add1 = _xdb_saved; }
				};}});
		add1 = _v_;
	}

	@Override
	public void setAdd2(int _v_) { // 属性2购买次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "add2") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, add2) {
					public void rollback() { add2 = _xdb_saved; }
				};}});
		add2 = _v_;
	}

	@Override
	public void setAdd3(int _v_) { // 属性3购买次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "add3") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, add3) {
					public void rollback() { add3 = _xdb_saved; }
				};}});
		add3 = _v_;
	}

	@Override
	public void setAdd4(int _v_) { // 属性4购买次数（仅计数）
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "add4") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, add4) {
					public void rollback() { add4 = _xdb_saved; }
				};}});
		add4 = _v_;
	}

	@Override
	public void setIsend(int _v_) { // 0未开始，1进行中，2结束
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "isend") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, isend) {
					public void rollback() { isend = _xdb_saved; }
				};}});
		isend = _v_;
	}

	@Override
	public void setTime(long _v_) { // 此记录时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "time") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, time) {
					public void rollback() { time = _xdb_saved; }
				};}});
		time = _v_;
	}

	@Override
	public void setIshalfcostpact(int _v_) { // 上次购买的强者之约是否达成（0是达成，1是未达成）
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "ishalfcostpact") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, ishalfcostpact) {
					public void rollback() { ishalfcostpact = _xdb_saved; }
				};}});
		ishalfcostpact = _v_;
	}

	@Override
	public void setEndtime(long _v_) { // 结束时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "endtime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, endtime) {
					public void rollback() { endtime = _xdb_saved; }
				};}});
		endtime = _v_;
	}

	@Override
	public void setExpectedrank(int _v_) { // 预期排名
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "expectedrank") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, expectedrank) {
					public void rollback() { expectedrank = _xdb_saved; }
				};}});
		expectedrank = _v_;
	}

	@Override
	public void setOnranknum(int _v_) { // 连续在榜次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "onranknum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, onranknum) {
					public void rollback() { onranknum = _xdb_saved; }
				};}});
		onranknum = _v_;
	}

	@Override
	public void setOnranklasttime(long _v_) { // 最后在榜时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "onranklasttime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, onranklasttime) {
					public void rollback() { onranklasttime = _xdb_saved; }
				};}});
		onranklasttime = _v_;
	}

	@Override
	public void setIsnotfirst(int _v_) { // 不是第一次战斗（0是第一次战斗，1不是第一次战斗）
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "isnotfirst") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, isnotfirst) {
					public void rollback() { isnotfirst = _xdb_saved; }
				};}});
		isnotfirst = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		EndlessInfo _o_ = null;
		if ( _o1_ instanceof EndlessInfo ) _o_ = (EndlessInfo)_o1_;
		else if ( _o1_ instanceof EndlessInfo.Const ) _o_ = ((EndlessInfo.Const)_o1_).nThis();
		else return false;
		if (battleid != _o_.battleid) return false;
		if (groupnum != _o_.groupnum) return false;
		if (!useherokeylist.equals(_o_.useherokeylist)) return false;
		if (!monstergroup.equals(_o_.monstergroup)) return false;
		if (trooptype != _o_.trooptype) return false;
		if (monstertrooptype != _o_.monstertrooptype) return false;
		if (pact != _o_.pact) return false;
		if (dropnum != _o_.dropnum) return false;
		if (alldropnum != _o_.alldropnum) return false;
		if (add1 != _o_.add1) return false;
		if (add2 != _o_.add2) return false;
		if (add3 != _o_.add3) return false;
		if (add4 != _o_.add4) return false;
		if (!herobloodlist.equals(_o_.herobloodlist)) return false;
		if (isend != _o_.isend) return false;
		if (time != _o_.time) return false;
		if (ishalfcostpact != _o_.ishalfcostpact) return false;
		if (endtime != _o_.endtime) return false;
		if (expectedrank != _o_.expectedrank) return false;
		if (!heroattribute.equals(_o_.heroattribute)) return false;
		if (onranknum != _o_.onranknum) return false;
		if (onranklasttime != _o_.onranklasttime) return false;
		if (isnotfirst != _o_.isnotfirst) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += battleid;
		_h_ += groupnum;
		_h_ += useherokeylist.hashCode();
		_h_ += monstergroup.hashCode();
		_h_ += trooptype;
		_h_ += monstertrooptype;
		_h_ += pact;
		_h_ += dropnum;
		_h_ += alldropnum;
		_h_ += add1;
		_h_ += add2;
		_h_ += add3;
		_h_ += add4;
		_h_ += herobloodlist.hashCode();
		_h_ += isend;
		_h_ += time;
		_h_ += ishalfcostpact;
		_h_ += endtime;
		_h_ += expectedrank;
		_h_ += heroattribute.hashCode();
		_h_ += onranknum;
		_h_ += onranklasttime;
		_h_ += isnotfirst;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(battleid);
		_sb_.append(",");
		_sb_.append(groupnum);
		_sb_.append(",");
		_sb_.append(useherokeylist);
		_sb_.append(",");
		_sb_.append(monstergroup);
		_sb_.append(",");
		_sb_.append(trooptype);
		_sb_.append(",");
		_sb_.append(monstertrooptype);
		_sb_.append(",");
		_sb_.append(pact);
		_sb_.append(",");
		_sb_.append(dropnum);
		_sb_.append(",");
		_sb_.append(alldropnum);
		_sb_.append(",");
		_sb_.append(add1);
		_sb_.append(",");
		_sb_.append(add2);
		_sb_.append(",");
		_sb_.append(add3);
		_sb_.append(",");
		_sb_.append(add4);
		_sb_.append(",");
		_sb_.append(herobloodlist);
		_sb_.append(",");
		_sb_.append(isend);
		_sb_.append(",");
		_sb_.append(time);
		_sb_.append(",");
		_sb_.append(ishalfcostpact);
		_sb_.append(",");
		_sb_.append(endtime);
		_sb_.append(",");
		_sb_.append(expectedrank);
		_sb_.append(",");
		_sb_.append(heroattribute);
		_sb_.append(",");
		_sb_.append(onranknum);
		_sb_.append(",");
		_sb_.append(onranklasttime);
		_sb_.append(",");
		_sb_.append(isnotfirst);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("battleid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("groupnum"));
		lb.add(new xdb.logs.ListenableMap().setVarName("useherokeylist"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("monstergroup"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("trooptype"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("monstertrooptype"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("pact"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("dropnum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("alldropnum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("add1"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("add2"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("add3"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("add4"));
		lb.add(new xdb.logs.ListenableMap().setVarName("herobloodlist"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("isend"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("time"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("ishalfcostpact"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("endtime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("expectedrank"));
		lb.add(new xdb.logs.ListenableMap().setVarName("heroattribute"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("onranknum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("onranklasttime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("isnotfirst"));
		return lb;
	}

	private class Const implements xbean.EndlessInfo {
		EndlessInfo nThis() {
			return EndlessInfo.this;
		}

		@Override
		public xbean.EndlessInfo copy() {
			return EndlessInfo.this.copy();
		}

		@Override
		public xbean.EndlessInfo toData() {
			return EndlessInfo.this.toData();
		}

		public xbean.EndlessInfo toBean() {
			return EndlessInfo.this.toBean();
		}

		@Override
		public xbean.EndlessInfo toDataIf() {
			return EndlessInfo.this.toDataIf();
		}

		public xbean.EndlessInfo toBeanIf() {
			return EndlessInfo.this.toBeanIf();
		}

		@Override
		public int getBattleid() { // 关卡id
			_xdb_verify_unsafe_();
			return battleid;
		}

		@Override
		public int getGroupnum() { // 第几轮
			_xdb_verify_unsafe_();
			return groupnum;
		}

		@Override
		public java.util.Map<Integer, Integer> getUseherokeylist() { // 使用英雄id和位置（key为位置，value为herokey）
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(useherokeylist);
		}

		@Override
		public java.util.Map<Integer, Integer> getUseherokeylistAsData() { // 使用英雄id和位置（key为位置，value为herokey）
			_xdb_verify_unsafe_();
			java.util.Map<Integer, Integer> useherokeylist;
			EndlessInfo _o_ = EndlessInfo.this;
			useherokeylist = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.useherokeylist.entrySet())
				useherokeylist.put(_e_.getKey(), _e_.getValue());
			return useherokeylist;
		}

		@Override
		public java.util.List<Integer> getMonstergroup() { // 怪物组
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(monstergroup);
		}

		public java.util.List<Integer> getMonstergroupAsData() { // 怪物组
			_xdb_verify_unsafe_();
			java.util.List<Integer> monstergroup;
			EndlessInfo _o_ = EndlessInfo.this;
		monstergroup = new java.util.LinkedList<Integer>();
		monstergroup.addAll(_o_.monstergroup);
			return monstergroup;
		}

		@Override
		public int getTrooptype() { // 战队类型
			_xdb_verify_unsafe_();
			return trooptype;
		}

		@Override
		public int getMonstertrooptype() { // 怪物战队类型
			_xdb_verify_unsafe_();
			return monstertrooptype;
		}

		@Override
		public int getPact() { // 今日战斗强者之约（没有则为-1）
			_xdb_verify_unsafe_();
			return pact;
		}

		@Override
		public int getDropnum() { // 剩余勇者证明数量
			_xdb_verify_unsafe_();
			return dropnum;
		}

		@Override
		public int getAlldropnum() { // 勇者证明总数量
			_xdb_verify_unsafe_();
			return alldropnum;
		}

		@Override
		public int getAdd1() { // 属性1购买次数
			_xdb_verify_unsafe_();
			return add1;
		}

		@Override
		public int getAdd2() { // 属性2购买次数
			_xdb_verify_unsafe_();
			return add2;
		}

		@Override
		public int getAdd3() { // 属性3购买次数
			_xdb_verify_unsafe_();
			return add3;
		}

		@Override
		public int getAdd4() { // 属性4购买次数（仅计数）
			_xdb_verify_unsafe_();
			return add4;
		}

		@Override
		public java.util.Map<Integer, Integer> getHerobloodlist() { // 使用英雄的血量（key为位置，value为血量）
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(herobloodlist);
		}

		@Override
		public java.util.Map<Integer, Integer> getHerobloodlistAsData() { // 使用英雄的血量（key为位置，value为血量）
			_xdb_verify_unsafe_();
			java.util.Map<Integer, Integer> herobloodlist;
			EndlessInfo _o_ = EndlessInfo.this;
			herobloodlist = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.herobloodlist.entrySet())
				herobloodlist.put(_e_.getKey(), _e_.getValue());
			return herobloodlist;
		}

		@Override
		public int getIsend() { // 0未开始，1进行中，2结束
			_xdb_verify_unsafe_();
			return isend;
		}

		@Override
		public long getTime() { // 此记录时间
			_xdb_verify_unsafe_();
			return time;
		}

		@Override
		public int getIshalfcostpact() { // 上次购买的强者之约是否达成（0是达成，1是未达成）
			_xdb_verify_unsafe_();
			return ishalfcostpact;
		}

		@Override
		public long getEndtime() { // 结束时间
			_xdb_verify_unsafe_();
			return endtime;
		}

		@Override
		public int getExpectedrank() { // 预期排名
			_xdb_verify_unsafe_();
			return expectedrank;
		}

		@Override
		public java.util.Map<Integer, xbean.OtherHero> getHeroattribute() { // 使用英雄的位置和属性信息（key为位置，value为OtherHero属性信息）
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(heroattribute);
		}

		@Override
		public java.util.Map<Integer, xbean.OtherHero> getHeroattributeAsData() { // 使用英雄的位置和属性信息（key为位置，value为OtherHero属性信息）
			_xdb_verify_unsafe_();
			java.util.Map<Integer, xbean.OtherHero> heroattribute;
			EndlessInfo _o_ = EndlessInfo.this;
			heroattribute = new java.util.HashMap<Integer, xbean.OtherHero>();
			for (java.util.Map.Entry<Integer, xbean.OtherHero> _e_ : _o_.heroattribute.entrySet())
				heroattribute.put(_e_.getKey(), new OtherHero.Data(_e_.getValue()));
			return heroattribute;
		}

		@Override
		public int getOnranknum() { // 连续在榜次数
			_xdb_verify_unsafe_();
			return onranknum;
		}

		@Override
		public long getOnranklasttime() { // 最后在榜时间
			_xdb_verify_unsafe_();
			return onranklasttime;
		}

		@Override
		public int getIsnotfirst() { // 不是第一次战斗（0是第一次战斗，1不是第一次战斗）
			_xdb_verify_unsafe_();
			return isnotfirst;
		}

		@Override
		public void setBattleid(int _v_) { // 关卡id
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setGroupnum(int _v_) { // 第几轮
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTrooptype(int _v_) { // 战队类型
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setMonstertrooptype(int _v_) { // 怪物战队类型
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setPact(int _v_) { // 今日战斗强者之约（没有则为-1）
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setDropnum(int _v_) { // 剩余勇者证明数量
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setAlldropnum(int _v_) { // 勇者证明总数量
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setAdd1(int _v_) { // 属性1购买次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setAdd2(int _v_) { // 属性2购买次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setAdd3(int _v_) { // 属性3购买次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setAdd4(int _v_) { // 属性4购买次数（仅计数）
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setIsend(int _v_) { // 0未开始，1进行中，2结束
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTime(long _v_) { // 此记录时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setIshalfcostpact(int _v_) { // 上次购买的强者之约是否达成（0是达成，1是未达成）
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setEndtime(long _v_) { // 结束时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setExpectedrank(int _v_) { // 预期排名
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setOnranknum(int _v_) { // 连续在榜次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setOnranklasttime(long _v_) { // 最后在榜时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setIsnotfirst(int _v_) { // 不是第一次战斗（0是第一次战斗，1不是第一次战斗）
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
			return EndlessInfo.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return EndlessInfo.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return EndlessInfo.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return EndlessInfo.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return EndlessInfo.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return EndlessInfo.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return EndlessInfo.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return EndlessInfo.this.hashCode();
		}

		@Override
		public String toString() {
			return EndlessInfo.this.toString();
		}

	}

	public static final class Data implements xbean.EndlessInfo {
		private int battleid; // 关卡id
		private int groupnum; // 第几轮
		private java.util.HashMap<Integer, Integer> useherokeylist; // 使用英雄id和位置（key为位置，value为herokey）
		private java.util.LinkedList<Integer> monstergroup; // 怪物组
		private int trooptype; // 战队类型
		private int monstertrooptype; // 怪物战队类型
		private int pact; // 今日战斗强者之约（没有则为-1）
		private int dropnum; // 剩余勇者证明数量
		private int alldropnum; // 勇者证明总数量
		private int add1; // 属性1购买次数
		private int add2; // 属性2购买次数
		private int add3; // 属性3购买次数
		private int add4; // 属性4购买次数（仅计数）
		private java.util.HashMap<Integer, Integer> herobloodlist; // 使用英雄的血量（key为位置，value为血量）
		private int isend; // 0未开始，1进行中，2结束
		private long time; // 此记录时间
		private int ishalfcostpact; // 上次购买的强者之约是否达成（0是达成，1是未达成）
		private long endtime; // 结束时间
		private int expectedrank; // 预期排名
		private java.util.HashMap<Integer, xbean.OtherHero> heroattribute; // 使用英雄的位置和属性信息（key为位置，value为OtherHero属性信息）
		private int onranknum; // 连续在榜次数
		private long onranklasttime; // 最后在榜时间
		private int isnotfirst; // 不是第一次战斗（0是第一次战斗，1不是第一次战斗）

		public Data() {
			useherokeylist = new java.util.HashMap<Integer, Integer>();
			monstergroup = new java.util.LinkedList<Integer>();
			herobloodlist = new java.util.HashMap<Integer, Integer>();
			heroattribute = new java.util.HashMap<Integer, xbean.OtherHero>();
		}

		Data(xbean.EndlessInfo _o1_) {
			if (_o1_ instanceof EndlessInfo) assign((EndlessInfo)_o1_);
			else if (_o1_ instanceof EndlessInfo.Data) assign((EndlessInfo.Data)_o1_);
			else if (_o1_ instanceof EndlessInfo.Const) assign(((EndlessInfo.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(EndlessInfo _o_) {
			battleid = _o_.battleid;
			groupnum = _o_.groupnum;
			useherokeylist = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.useherokeylist.entrySet())
				useherokeylist.put(_e_.getKey(), _e_.getValue());
			monstergroup = new java.util.LinkedList<Integer>();
			monstergroup.addAll(_o_.monstergroup);
			trooptype = _o_.trooptype;
			monstertrooptype = _o_.monstertrooptype;
			pact = _o_.pact;
			dropnum = _o_.dropnum;
			alldropnum = _o_.alldropnum;
			add1 = _o_.add1;
			add2 = _o_.add2;
			add3 = _o_.add3;
			add4 = _o_.add4;
			herobloodlist = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.herobloodlist.entrySet())
				herobloodlist.put(_e_.getKey(), _e_.getValue());
			isend = _o_.isend;
			time = _o_.time;
			ishalfcostpact = _o_.ishalfcostpact;
			endtime = _o_.endtime;
			expectedrank = _o_.expectedrank;
			heroattribute = new java.util.HashMap<Integer, xbean.OtherHero>();
			for (java.util.Map.Entry<Integer, xbean.OtherHero> _e_ : _o_.heroattribute.entrySet())
				heroattribute.put(_e_.getKey(), new OtherHero.Data(_e_.getValue()));
			onranknum = _o_.onranknum;
			onranklasttime = _o_.onranklasttime;
			isnotfirst = _o_.isnotfirst;
		}

		private void assign(EndlessInfo.Data _o_) {
			battleid = _o_.battleid;
			groupnum = _o_.groupnum;
			useherokeylist = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.useherokeylist.entrySet())
				useherokeylist.put(_e_.getKey(), _e_.getValue());
			monstergroup = new java.util.LinkedList<Integer>();
			monstergroup.addAll(_o_.monstergroup);
			trooptype = _o_.trooptype;
			monstertrooptype = _o_.monstertrooptype;
			pact = _o_.pact;
			dropnum = _o_.dropnum;
			alldropnum = _o_.alldropnum;
			add1 = _o_.add1;
			add2 = _o_.add2;
			add3 = _o_.add3;
			add4 = _o_.add4;
			herobloodlist = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.herobloodlist.entrySet())
				herobloodlist.put(_e_.getKey(), _e_.getValue());
			isend = _o_.isend;
			time = _o_.time;
			ishalfcostpact = _o_.ishalfcostpact;
			endtime = _o_.endtime;
			expectedrank = _o_.expectedrank;
			heroattribute = new java.util.HashMap<Integer, xbean.OtherHero>();
			for (java.util.Map.Entry<Integer, xbean.OtherHero> _e_ : _o_.heroattribute.entrySet())
				heroattribute.put(_e_.getKey(), new OtherHero.Data(_e_.getValue()));
			onranknum = _o_.onranknum;
			onranklasttime = _o_.onranklasttime;
			isnotfirst = _o_.isnotfirst;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(battleid);
			_os_.marshal(groupnum);
			_os_.compact_uint32(useherokeylist.size());
			for (java.util.Map.Entry<Integer, Integer> _e_ : useherokeylist.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_os_.marshal(_e_.getValue());
			}
			_os_.compact_uint32(monstergroup.size());
			for (Integer _v_ : monstergroup) {
				_os_.marshal(_v_);
			}
			_os_.marshal(trooptype);
			_os_.marshal(monstertrooptype);
			_os_.marshal(pact);
			_os_.marshal(dropnum);
			_os_.marshal(alldropnum);
			_os_.marshal(add1);
			_os_.marshal(add2);
			_os_.marshal(add3);
			_os_.marshal(add4);
			_os_.compact_uint32(herobloodlist.size());
			for (java.util.Map.Entry<Integer, Integer> _e_ : herobloodlist.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_os_.marshal(_e_.getValue());
			}
			_os_.marshal(isend);
			_os_.marshal(time);
			_os_.marshal(ishalfcostpact);
			_os_.marshal(endtime);
			_os_.marshal(expectedrank);
			_os_.compact_uint32(heroattribute.size());
			for (java.util.Map.Entry<Integer, xbean.OtherHero> _e_ : heroattribute.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_e_.getValue().marshal(_os_);
			}
			_os_.marshal(onranknum);
			_os_.marshal(onranklasttime);
			_os_.marshal(isnotfirst);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			battleid = _os_.unmarshal_int();
			groupnum = _os_.unmarshal_int();
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					useherokeylist = new java.util.HashMap<Integer, Integer>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					int _v_ = 0;
					_v_ = _os_.unmarshal_int();
					useherokeylist.put(_k_, _v_);
				}
			}
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				int _v_ = 0;
				_v_ = _os_.unmarshal_int();
				monstergroup.add(_v_);
			}
			trooptype = _os_.unmarshal_int();
			monstertrooptype = _os_.unmarshal_int();
			pact = _os_.unmarshal_int();
			dropnum = _os_.unmarshal_int();
			alldropnum = _os_.unmarshal_int();
			add1 = _os_.unmarshal_int();
			add2 = _os_.unmarshal_int();
			add3 = _os_.unmarshal_int();
			add4 = _os_.unmarshal_int();
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					herobloodlist = new java.util.HashMap<Integer, Integer>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					int _v_ = 0;
					_v_ = _os_.unmarshal_int();
					herobloodlist.put(_k_, _v_);
				}
			}
			isend = _os_.unmarshal_int();
			time = _os_.unmarshal_long();
			ishalfcostpact = _os_.unmarshal_int();
			endtime = _os_.unmarshal_long();
			expectedrank = _os_.unmarshal_int();
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					heroattribute = new java.util.HashMap<Integer, xbean.OtherHero>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					xbean.OtherHero _v_ = xbean.Pod.newOtherHeroData();
					_v_.unmarshal(_os_);
					heroattribute.put(_k_, _v_);
				}
			}
			onranknum = _os_.unmarshal_int();
			onranklasttime = _os_.unmarshal_long();
			isnotfirst = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.EndlessInfo copy() {
			return new Data(this);
		}

		@Override
		public xbean.EndlessInfo toData() {
			return new Data(this);
		}

		public xbean.EndlessInfo toBean() {
			return new EndlessInfo(this, null, null);
		}

		@Override
		public xbean.EndlessInfo toDataIf() {
			return this;
		}

		public xbean.EndlessInfo toBeanIf() {
			return new EndlessInfo(this, null, null);
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
		public int getBattleid() { // 关卡id
			return battleid;
		}

		@Override
		public int getGroupnum() { // 第几轮
			return groupnum;
		}

		@Override
		public java.util.Map<Integer, Integer> getUseherokeylist() { // 使用英雄id和位置（key为位置，value为herokey）
			return useherokeylist;
		}

		@Override
		public java.util.Map<Integer, Integer> getUseherokeylistAsData() { // 使用英雄id和位置（key为位置，value为herokey）
			return useherokeylist;
		}

		@Override
		public java.util.List<Integer> getMonstergroup() { // 怪物组
			return monstergroup;
		}

		@Override
		public java.util.List<Integer> getMonstergroupAsData() { // 怪物组
			return monstergroup;
		}

		@Override
		public int getTrooptype() { // 战队类型
			return trooptype;
		}

		@Override
		public int getMonstertrooptype() { // 怪物战队类型
			return monstertrooptype;
		}

		@Override
		public int getPact() { // 今日战斗强者之约（没有则为-1）
			return pact;
		}

		@Override
		public int getDropnum() { // 剩余勇者证明数量
			return dropnum;
		}

		@Override
		public int getAlldropnum() { // 勇者证明总数量
			return alldropnum;
		}

		@Override
		public int getAdd1() { // 属性1购买次数
			return add1;
		}

		@Override
		public int getAdd2() { // 属性2购买次数
			return add2;
		}

		@Override
		public int getAdd3() { // 属性3购买次数
			return add3;
		}

		@Override
		public int getAdd4() { // 属性4购买次数（仅计数）
			return add4;
		}

		@Override
		public java.util.Map<Integer, Integer> getHerobloodlist() { // 使用英雄的血量（key为位置，value为血量）
			return herobloodlist;
		}

		@Override
		public java.util.Map<Integer, Integer> getHerobloodlistAsData() { // 使用英雄的血量（key为位置，value为血量）
			return herobloodlist;
		}

		@Override
		public int getIsend() { // 0未开始，1进行中，2结束
			return isend;
		}

		@Override
		public long getTime() { // 此记录时间
			return time;
		}

		@Override
		public int getIshalfcostpact() { // 上次购买的强者之约是否达成（0是达成，1是未达成）
			return ishalfcostpact;
		}

		@Override
		public long getEndtime() { // 结束时间
			return endtime;
		}

		@Override
		public int getExpectedrank() { // 预期排名
			return expectedrank;
		}

		@Override
		public java.util.Map<Integer, xbean.OtherHero> getHeroattribute() { // 使用英雄的位置和属性信息（key为位置，value为OtherHero属性信息）
			return heroattribute;
		}

		@Override
		public java.util.Map<Integer, xbean.OtherHero> getHeroattributeAsData() { // 使用英雄的位置和属性信息（key为位置，value为OtherHero属性信息）
			return heroattribute;
		}

		@Override
		public int getOnranknum() { // 连续在榜次数
			return onranknum;
		}

		@Override
		public long getOnranklasttime() { // 最后在榜时间
			return onranklasttime;
		}

		@Override
		public int getIsnotfirst() { // 不是第一次战斗（0是第一次战斗，1不是第一次战斗）
			return isnotfirst;
		}

		@Override
		public void setBattleid(int _v_) { // 关卡id
			battleid = _v_;
		}

		@Override
		public void setGroupnum(int _v_) { // 第几轮
			groupnum = _v_;
		}

		@Override
		public void setTrooptype(int _v_) { // 战队类型
			trooptype = _v_;
		}

		@Override
		public void setMonstertrooptype(int _v_) { // 怪物战队类型
			monstertrooptype = _v_;
		}

		@Override
		public void setPact(int _v_) { // 今日战斗强者之约（没有则为-1）
			pact = _v_;
		}

		@Override
		public void setDropnum(int _v_) { // 剩余勇者证明数量
			dropnum = _v_;
		}

		@Override
		public void setAlldropnum(int _v_) { // 勇者证明总数量
			alldropnum = _v_;
		}

		@Override
		public void setAdd1(int _v_) { // 属性1购买次数
			add1 = _v_;
		}

		@Override
		public void setAdd2(int _v_) { // 属性2购买次数
			add2 = _v_;
		}

		@Override
		public void setAdd3(int _v_) { // 属性3购买次数
			add3 = _v_;
		}

		@Override
		public void setAdd4(int _v_) { // 属性4购买次数（仅计数）
			add4 = _v_;
		}

		@Override
		public void setIsend(int _v_) { // 0未开始，1进行中，2结束
			isend = _v_;
		}

		@Override
		public void setTime(long _v_) { // 此记录时间
			time = _v_;
		}

		@Override
		public void setIshalfcostpact(int _v_) { // 上次购买的强者之约是否达成（0是达成，1是未达成）
			ishalfcostpact = _v_;
		}

		@Override
		public void setEndtime(long _v_) { // 结束时间
			endtime = _v_;
		}

		@Override
		public void setExpectedrank(int _v_) { // 预期排名
			expectedrank = _v_;
		}

		@Override
		public void setOnranknum(int _v_) { // 连续在榜次数
			onranknum = _v_;
		}

		@Override
		public void setOnranklasttime(long _v_) { // 最后在榜时间
			onranklasttime = _v_;
		}

		@Override
		public void setIsnotfirst(int _v_) { // 不是第一次战斗（0是第一次战斗，1不是第一次战斗）
			isnotfirst = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof EndlessInfo.Data)) return false;
			EndlessInfo.Data _o_ = (EndlessInfo.Data) _o1_;
			if (battleid != _o_.battleid) return false;
			if (groupnum != _o_.groupnum) return false;
			if (!useherokeylist.equals(_o_.useherokeylist)) return false;
			if (!monstergroup.equals(_o_.monstergroup)) return false;
			if (trooptype != _o_.trooptype) return false;
			if (monstertrooptype != _o_.monstertrooptype) return false;
			if (pact != _o_.pact) return false;
			if (dropnum != _o_.dropnum) return false;
			if (alldropnum != _o_.alldropnum) return false;
			if (add1 != _o_.add1) return false;
			if (add2 != _o_.add2) return false;
			if (add3 != _o_.add3) return false;
			if (add4 != _o_.add4) return false;
			if (!herobloodlist.equals(_o_.herobloodlist)) return false;
			if (isend != _o_.isend) return false;
			if (time != _o_.time) return false;
			if (ishalfcostpact != _o_.ishalfcostpact) return false;
			if (endtime != _o_.endtime) return false;
			if (expectedrank != _o_.expectedrank) return false;
			if (!heroattribute.equals(_o_.heroattribute)) return false;
			if (onranknum != _o_.onranknum) return false;
			if (onranklasttime != _o_.onranklasttime) return false;
			if (isnotfirst != _o_.isnotfirst) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += battleid;
			_h_ += groupnum;
			_h_ += useherokeylist.hashCode();
			_h_ += monstergroup.hashCode();
			_h_ += trooptype;
			_h_ += monstertrooptype;
			_h_ += pact;
			_h_ += dropnum;
			_h_ += alldropnum;
			_h_ += add1;
			_h_ += add2;
			_h_ += add3;
			_h_ += add4;
			_h_ += herobloodlist.hashCode();
			_h_ += isend;
			_h_ += time;
			_h_ += ishalfcostpact;
			_h_ += endtime;
			_h_ += expectedrank;
			_h_ += heroattribute.hashCode();
			_h_ += onranknum;
			_h_ += onranklasttime;
			_h_ += isnotfirst;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(battleid);
			_sb_.append(",");
			_sb_.append(groupnum);
			_sb_.append(",");
			_sb_.append(useherokeylist);
			_sb_.append(",");
			_sb_.append(monstergroup);
			_sb_.append(",");
			_sb_.append(trooptype);
			_sb_.append(",");
			_sb_.append(monstertrooptype);
			_sb_.append(",");
			_sb_.append(pact);
			_sb_.append(",");
			_sb_.append(dropnum);
			_sb_.append(",");
			_sb_.append(alldropnum);
			_sb_.append(",");
			_sb_.append(add1);
			_sb_.append(",");
			_sb_.append(add2);
			_sb_.append(",");
			_sb_.append(add3);
			_sb_.append(",");
			_sb_.append(add4);
			_sb_.append(",");
			_sb_.append(herobloodlist);
			_sb_.append(",");
			_sb_.append(isend);
			_sb_.append(",");
			_sb_.append(time);
			_sb_.append(",");
			_sb_.append(ishalfcostpact);
			_sb_.append(",");
			_sb_.append(endtime);
			_sb_.append(",");
			_sb_.append(expectedrank);
			_sb_.append(",");
			_sb_.append(heroattribute);
			_sb_.append(",");
			_sb_.append(onranknum);
			_sb_.append(",");
			_sb_.append(onranklasttime);
			_sb_.append(",");
			_sb_.append(isnotfirst);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
