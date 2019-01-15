
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class EndlessRankInfo extends xdb.XBean implements xbean.EndlessRankInfo {
	private long roleid; // 玩家guid
	private String rolename; // 玩家名称
	private int level; // 玩家等级
	private int groupnum; // 第几轮
	private int trooptype; // 战队类型
	private int alldropnum; // 勇者证明总数量
	private java.util.HashMap<Integer, xbean.OtherHero> heroattribute; // 使用英雄的位置和属性信息（key为位置，value为OtherHero属性信息）
	private int onranknum; // 连续在榜次数

	EndlessRankInfo(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		rolename = "";
		heroattribute = new java.util.HashMap<Integer, xbean.OtherHero>();
	}

	public EndlessRankInfo() {
		this(0, null, null);
	}

	public EndlessRankInfo(EndlessRankInfo _o_) {
		this(_o_, null, null);
	}

	EndlessRankInfo(xbean.EndlessRankInfo _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof EndlessRankInfo) assign((EndlessRankInfo)_o1_);
		else if (_o1_ instanceof EndlessRankInfo.Data) assign((EndlessRankInfo.Data)_o1_);
		else if (_o1_ instanceof EndlessRankInfo.Const) assign(((EndlessRankInfo.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(EndlessRankInfo _o_) {
		_o_._xdb_verify_unsafe_();
		roleid = _o_.roleid;
		rolename = _o_.rolename;
		level = _o_.level;
		groupnum = _o_.groupnum;
		trooptype = _o_.trooptype;
		alldropnum = _o_.alldropnum;
		heroattribute = new java.util.HashMap<Integer, xbean.OtherHero>();
		for (java.util.Map.Entry<Integer, xbean.OtherHero> _e_ : _o_.heroattribute.entrySet())
			heroattribute.put(_e_.getKey(), new OtherHero(_e_.getValue(), this, "heroattribute"));
		onranknum = _o_.onranknum;
	}

	private void assign(EndlessRankInfo.Data _o_) {
		roleid = _o_.roleid;
		rolename = _o_.rolename;
		level = _o_.level;
		groupnum = _o_.groupnum;
		trooptype = _o_.trooptype;
		alldropnum = _o_.alldropnum;
		heroattribute = new java.util.HashMap<Integer, xbean.OtherHero>();
		for (java.util.Map.Entry<Integer, xbean.OtherHero> _e_ : _o_.heroattribute.entrySet())
			heroattribute.put(_e_.getKey(), new OtherHero(_e_.getValue(), this, "heroattribute"));
		onranknum = _o_.onranknum;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(roleid);
		_os_.marshal(rolename, xdb.Const.IO_CHARSET);
		_os_.marshal(level);
		_os_.marshal(groupnum);
		_os_.marshal(trooptype);
		_os_.marshal(alldropnum);
		_os_.compact_uint32(heroattribute.size());
		for (java.util.Map.Entry<Integer, xbean.OtherHero> _e_ : heroattribute.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_e_.getValue().marshal(_os_);
		}
		_os_.marshal(onranknum);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		roleid = _os_.unmarshal_long();
		rolename = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
		level = _os_.unmarshal_int();
		groupnum = _os_.unmarshal_int();
		trooptype = _os_.unmarshal_int();
		alldropnum = _os_.unmarshal_int();
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
		return _os_;
	}

	@Override
	public xbean.EndlessRankInfo copy() {
		_xdb_verify_unsafe_();
		return new EndlessRankInfo(this);
	}

	@Override
	public xbean.EndlessRankInfo toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.EndlessRankInfo toBean() {
		_xdb_verify_unsafe_();
		return new EndlessRankInfo(this); // same as copy()
	}

	@Override
	public xbean.EndlessRankInfo toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.EndlessRankInfo toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public long getRoleid() { // 玩家guid
		_xdb_verify_unsafe_();
		return roleid;
	}

	@Override
	public String getRolename() { // 玩家名称
		_xdb_verify_unsafe_();
		return rolename;
	}

	@Override
	public com.goldhuman.Common.Octets getRolenameOctets() { // 玩家名称
		_xdb_verify_unsafe_();
		return com.goldhuman.Common.Octets.wrap(getRolename(), xdb.Const.IO_CHARSET);
	}

	@Override
	public int getLevel() { // 玩家等级
		_xdb_verify_unsafe_();
		return level;
	}

	@Override
	public int getGroupnum() { // 第几轮
		_xdb_verify_unsafe_();
		return groupnum;
	}

	@Override
	public int getTrooptype() { // 战队类型
		_xdb_verify_unsafe_();
		return trooptype;
	}

	@Override
	public int getAlldropnum() { // 勇者证明总数量
		_xdb_verify_unsafe_();
		return alldropnum;
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
		EndlessRankInfo _o_ = this;
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
	public void setRoleid(long _v_) { // 玩家guid
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "roleid") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, roleid) {
					public void rollback() { roleid = _xdb_saved; }
				};}});
		roleid = _v_;
	}

	@Override
	public void setRolename(String _v_) { // 玩家名称
		_xdb_verify_unsafe_();
		if (null == _v_)
			throw new NullPointerException();
		xdb.Logs.logIf(new xdb.LogKey(this, "rolename") {
			protected xdb.Log create() {
				return new xdb.logs.LogString(this, rolename) {
					public void rollback() { rolename = _xdb_saved; }
				};}});
		rolename = _v_;
	}

	@Override
	public void setRolenameOctets(com.goldhuman.Common.Octets _v_) { // 玩家名称
		_xdb_verify_unsafe_();
		this.setRolename(_v_.getString(xdb.Const.IO_CHARSET));
	}

	@Override
	public void setLevel(int _v_) { // 玩家等级
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "level") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, level) {
					public void rollback() { level = _xdb_saved; }
				};}});
		level = _v_;
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
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		EndlessRankInfo _o_ = null;
		if ( _o1_ instanceof EndlessRankInfo ) _o_ = (EndlessRankInfo)_o1_;
		else if ( _o1_ instanceof EndlessRankInfo.Const ) _o_ = ((EndlessRankInfo.Const)_o1_).nThis();
		else return false;
		if (roleid != _o_.roleid) return false;
		if (!rolename.equals(_o_.rolename)) return false;
		if (level != _o_.level) return false;
		if (groupnum != _o_.groupnum) return false;
		if (trooptype != _o_.trooptype) return false;
		if (alldropnum != _o_.alldropnum) return false;
		if (!heroattribute.equals(_o_.heroattribute)) return false;
		if (onranknum != _o_.onranknum) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += roleid;
		_h_ += rolename.hashCode();
		_h_ += level;
		_h_ += groupnum;
		_h_ += trooptype;
		_h_ += alldropnum;
		_h_ += heroattribute.hashCode();
		_h_ += onranknum;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(roleid);
		_sb_.append(",");
		_sb_.append("'").append(rolename).append("'");
		_sb_.append(",");
		_sb_.append(level);
		_sb_.append(",");
		_sb_.append(groupnum);
		_sb_.append(",");
		_sb_.append(trooptype);
		_sb_.append(",");
		_sb_.append(alldropnum);
		_sb_.append(",");
		_sb_.append(heroattribute);
		_sb_.append(",");
		_sb_.append(onranknum);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("roleid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("rolename"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("level"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("groupnum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("trooptype"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("alldropnum"));
		lb.add(new xdb.logs.ListenableMap().setVarName("heroattribute"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("onranknum"));
		return lb;
	}

	private class Const implements xbean.EndlessRankInfo {
		EndlessRankInfo nThis() {
			return EndlessRankInfo.this;
		}

		@Override
		public xbean.EndlessRankInfo copy() {
			return EndlessRankInfo.this.copy();
		}

		@Override
		public xbean.EndlessRankInfo toData() {
			return EndlessRankInfo.this.toData();
		}

		public xbean.EndlessRankInfo toBean() {
			return EndlessRankInfo.this.toBean();
		}

		@Override
		public xbean.EndlessRankInfo toDataIf() {
			return EndlessRankInfo.this.toDataIf();
		}

		public xbean.EndlessRankInfo toBeanIf() {
			return EndlessRankInfo.this.toBeanIf();
		}

		@Override
		public long getRoleid() { // 玩家guid
			_xdb_verify_unsafe_();
			return roleid;
		}

		@Override
		public String getRolename() { // 玩家名称
			_xdb_verify_unsafe_();
			return rolename;
		}

		@Override
		public com.goldhuman.Common.Octets getRolenameOctets() { // 玩家名称
			_xdb_verify_unsafe_();
			return EndlessRankInfo.this.getRolenameOctets();
		}

		@Override
		public int getLevel() { // 玩家等级
			_xdb_verify_unsafe_();
			return level;
		}

		@Override
		public int getGroupnum() { // 第几轮
			_xdb_verify_unsafe_();
			return groupnum;
		}

		@Override
		public int getTrooptype() { // 战队类型
			_xdb_verify_unsafe_();
			return trooptype;
		}

		@Override
		public int getAlldropnum() { // 勇者证明总数量
			_xdb_verify_unsafe_();
			return alldropnum;
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
			EndlessRankInfo _o_ = EndlessRankInfo.this;
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
		public void setRoleid(long _v_) { // 玩家guid
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setRolename(String _v_) { // 玩家名称
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setRolenameOctets(com.goldhuman.Common.Octets _v_) { // 玩家名称
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setLevel(int _v_) { // 玩家等级
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
		public void setAlldropnum(int _v_) { // 勇者证明总数量
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setOnranknum(int _v_) { // 连续在榜次数
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
			return EndlessRankInfo.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return EndlessRankInfo.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return EndlessRankInfo.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return EndlessRankInfo.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return EndlessRankInfo.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return EndlessRankInfo.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return EndlessRankInfo.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return EndlessRankInfo.this.hashCode();
		}

		@Override
		public String toString() {
			return EndlessRankInfo.this.toString();
		}

	}

	public static final class Data implements xbean.EndlessRankInfo {
		private long roleid; // 玩家guid
		private String rolename; // 玩家名称
		private int level; // 玩家等级
		private int groupnum; // 第几轮
		private int trooptype; // 战队类型
		private int alldropnum; // 勇者证明总数量
		private java.util.HashMap<Integer, xbean.OtherHero> heroattribute; // 使用英雄的位置和属性信息（key为位置，value为OtherHero属性信息）
		private int onranknum; // 连续在榜次数

		public Data() {
			rolename = "";
			heroattribute = new java.util.HashMap<Integer, xbean.OtherHero>();
		}

		Data(xbean.EndlessRankInfo _o1_) {
			if (_o1_ instanceof EndlessRankInfo) assign((EndlessRankInfo)_o1_);
			else if (_o1_ instanceof EndlessRankInfo.Data) assign((EndlessRankInfo.Data)_o1_);
			else if (_o1_ instanceof EndlessRankInfo.Const) assign(((EndlessRankInfo.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(EndlessRankInfo _o_) {
			roleid = _o_.roleid;
			rolename = _o_.rolename;
			level = _o_.level;
			groupnum = _o_.groupnum;
			trooptype = _o_.trooptype;
			alldropnum = _o_.alldropnum;
			heroattribute = new java.util.HashMap<Integer, xbean.OtherHero>();
			for (java.util.Map.Entry<Integer, xbean.OtherHero> _e_ : _o_.heroattribute.entrySet())
				heroattribute.put(_e_.getKey(), new OtherHero.Data(_e_.getValue()));
			onranknum = _o_.onranknum;
		}

		private void assign(EndlessRankInfo.Data _o_) {
			roleid = _o_.roleid;
			rolename = _o_.rolename;
			level = _o_.level;
			groupnum = _o_.groupnum;
			trooptype = _o_.trooptype;
			alldropnum = _o_.alldropnum;
			heroattribute = new java.util.HashMap<Integer, xbean.OtherHero>();
			for (java.util.Map.Entry<Integer, xbean.OtherHero> _e_ : _o_.heroattribute.entrySet())
				heroattribute.put(_e_.getKey(), new OtherHero.Data(_e_.getValue()));
			onranknum = _o_.onranknum;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(roleid);
			_os_.marshal(rolename, xdb.Const.IO_CHARSET);
			_os_.marshal(level);
			_os_.marshal(groupnum);
			_os_.marshal(trooptype);
			_os_.marshal(alldropnum);
			_os_.compact_uint32(heroattribute.size());
			for (java.util.Map.Entry<Integer, xbean.OtherHero> _e_ : heroattribute.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_e_.getValue().marshal(_os_);
			}
			_os_.marshal(onranknum);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			roleid = _os_.unmarshal_long();
			rolename = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			level = _os_.unmarshal_int();
			groupnum = _os_.unmarshal_int();
			trooptype = _os_.unmarshal_int();
			alldropnum = _os_.unmarshal_int();
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
			return _os_;
		}

		@Override
		public xbean.EndlessRankInfo copy() {
			return new Data(this);
		}

		@Override
		public xbean.EndlessRankInfo toData() {
			return new Data(this);
		}

		public xbean.EndlessRankInfo toBean() {
			return new EndlessRankInfo(this, null, null);
		}

		@Override
		public xbean.EndlessRankInfo toDataIf() {
			return this;
		}

		public xbean.EndlessRankInfo toBeanIf() {
			return new EndlessRankInfo(this, null, null);
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
		public long getRoleid() { // 玩家guid
			return roleid;
		}

		@Override
		public String getRolename() { // 玩家名称
			return rolename;
		}

		@Override
		public com.goldhuman.Common.Octets getRolenameOctets() { // 玩家名称
			return com.goldhuman.Common.Octets.wrap(getRolename(), xdb.Const.IO_CHARSET);
		}

		@Override
		public int getLevel() { // 玩家等级
			return level;
		}

		@Override
		public int getGroupnum() { // 第几轮
			return groupnum;
		}

		@Override
		public int getTrooptype() { // 战队类型
			return trooptype;
		}

		@Override
		public int getAlldropnum() { // 勇者证明总数量
			return alldropnum;
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
		public void setRoleid(long _v_) { // 玩家guid
			roleid = _v_;
		}

		@Override
		public void setRolename(String _v_) { // 玩家名称
			if (null == _v_)
				throw new NullPointerException();
			rolename = _v_;
		}

		@Override
		public void setRolenameOctets(com.goldhuman.Common.Octets _v_) { // 玩家名称
			this.setRolename(_v_.getString(xdb.Const.IO_CHARSET));
		}

		@Override
		public void setLevel(int _v_) { // 玩家等级
			level = _v_;
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
		public void setAlldropnum(int _v_) { // 勇者证明总数量
			alldropnum = _v_;
		}

		@Override
		public void setOnranknum(int _v_) { // 连续在榜次数
			onranknum = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof EndlessRankInfo.Data)) return false;
			EndlessRankInfo.Data _o_ = (EndlessRankInfo.Data) _o1_;
			if (roleid != _o_.roleid) return false;
			if (!rolename.equals(_o_.rolename)) return false;
			if (level != _o_.level) return false;
			if (groupnum != _o_.groupnum) return false;
			if (trooptype != _o_.trooptype) return false;
			if (alldropnum != _o_.alldropnum) return false;
			if (!heroattribute.equals(_o_.heroattribute)) return false;
			if (onranknum != _o_.onranknum) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += roleid;
			_h_ += rolename.hashCode();
			_h_ += level;
			_h_ += groupnum;
			_h_ += trooptype;
			_h_ += alldropnum;
			_h_ += heroattribute.hashCode();
			_h_ += onranknum;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(roleid);
			_sb_.append(",");
			_sb_.append("'").append(rolename).append("'");
			_sb_.append(",");
			_sb_.append(level);
			_sb_.append(",");
			_sb_.append(groupnum);
			_sb_.append(",");
			_sb_.append(trooptype);
			_sb_.append(",");
			_sb_.append(alldropnum);
			_sb_.append(",");
			_sb_.append(heroattribute);
			_sb_.append(",");
			_sb_.append(onranknum);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
