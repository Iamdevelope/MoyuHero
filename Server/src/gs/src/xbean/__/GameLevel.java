
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class GameLevel extends xdb.XBean implements xbean.GameLevel {
	private int battleid; // 副本ID
	private java.util.HashMap<Integer, Integer> useherokeylist; // 关卡用到的英雄
	private int dropgold; // 掉落金币
	private int dropcrystal; // 掉落宝石
	private java.util.LinkedList<Integer> equipidlist; // 掉落物品列表
	private int trooptype; // 战队类型

	GameLevel(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		useherokeylist = new java.util.HashMap<Integer, Integer>();
		equipidlist = new java.util.LinkedList<Integer>();
	}

	public GameLevel() {
		this(0, null, null);
	}

	public GameLevel(GameLevel _o_) {
		this(_o_, null, null);
	}

	GameLevel(xbean.GameLevel _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof GameLevel) assign((GameLevel)_o1_);
		else if (_o1_ instanceof GameLevel.Data) assign((GameLevel.Data)_o1_);
		else if (_o1_ instanceof GameLevel.Const) assign(((GameLevel.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(GameLevel _o_) {
		_o_._xdb_verify_unsafe_();
		battleid = _o_.battleid;
		useherokeylist = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.useherokeylist.entrySet())
			useherokeylist.put(_e_.getKey(), _e_.getValue());
		dropgold = _o_.dropgold;
		dropcrystal = _o_.dropcrystal;
		equipidlist = new java.util.LinkedList<Integer>();
		equipidlist.addAll(_o_.equipidlist);
		trooptype = _o_.trooptype;
	}

	private void assign(GameLevel.Data _o_) {
		battleid = _o_.battleid;
		useherokeylist = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.useherokeylist.entrySet())
			useherokeylist.put(_e_.getKey(), _e_.getValue());
		dropgold = _o_.dropgold;
		dropcrystal = _o_.dropcrystal;
		equipidlist = new java.util.LinkedList<Integer>();
		equipidlist.addAll(_o_.equipidlist);
		trooptype = _o_.trooptype;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(battleid);
		_os_.compact_uint32(useherokeylist.size());
		for (java.util.Map.Entry<Integer, Integer> _e_ : useherokeylist.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.marshal(dropgold);
		_os_.marshal(dropcrystal);
		_os_.compact_uint32(equipidlist.size());
		for (Integer _v_ : equipidlist) {
			_os_.marshal(_v_);
		}
		_os_.marshal(trooptype);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		battleid = _os_.unmarshal_int();
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
		dropgold = _os_.unmarshal_int();
		dropcrystal = _os_.unmarshal_int();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _v_ = 0;
			_v_ = _os_.unmarshal_int();
			equipidlist.add(_v_);
		}
		trooptype = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.GameLevel copy() {
		_xdb_verify_unsafe_();
		return new GameLevel(this);
	}

	@Override
	public xbean.GameLevel toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.GameLevel toBean() {
		_xdb_verify_unsafe_();
		return new GameLevel(this); // same as copy()
	}

	@Override
	public xbean.GameLevel toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.GameLevel toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getBattleid() { // 副本ID
		_xdb_verify_unsafe_();
		return battleid;
	}

	@Override
	public java.util.Map<Integer, Integer> getUseherokeylist() { // 关卡用到的英雄
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "useherokeylist"), useherokeylist);
	}

	@Override
	public java.util.Map<Integer, Integer> getUseherokeylistAsData() { // 关卡用到的英雄
		_xdb_verify_unsafe_();
		java.util.Map<Integer, Integer> useherokeylist;
		GameLevel _o_ = this;
		useherokeylist = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.useherokeylist.entrySet())
			useherokeylist.put(_e_.getKey(), _e_.getValue());
		return useherokeylist;
	}

	@Override
	public int getDropgold() { // 掉落金币
		_xdb_verify_unsafe_();
		return dropgold;
	}

	@Override
	public int getDropcrystal() { // 掉落宝石
		_xdb_verify_unsafe_();
		return dropcrystal;
	}

	@Override
	public java.util.List<Integer> getEquipidlist() { // 掉落物品列表
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "equipidlist"), equipidlist);
	}

	public java.util.List<Integer> getEquipidlistAsData() { // 掉落物品列表
		_xdb_verify_unsafe_();
		java.util.List<Integer> equipidlist;
		GameLevel _o_ = this;
		equipidlist = new java.util.LinkedList<Integer>();
		equipidlist.addAll(_o_.equipidlist);
		return equipidlist;
	}

	@Override
	public int getTrooptype() { // 战队类型
		_xdb_verify_unsafe_();
		return trooptype;
	}

	@Override
	public void setBattleid(int _v_) { // 副本ID
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "battleid") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, battleid) {
					public void rollback() { battleid = _xdb_saved; }
				};}});
		battleid = _v_;
	}

	@Override
	public void setDropgold(int _v_) { // 掉落金币
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "dropgold") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, dropgold) {
					public void rollback() { dropgold = _xdb_saved; }
				};}});
		dropgold = _v_;
	}

	@Override
	public void setDropcrystal(int _v_) { // 掉落宝石
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "dropcrystal") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, dropcrystal) {
					public void rollback() { dropcrystal = _xdb_saved; }
				};}});
		dropcrystal = _v_;
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
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		GameLevel _o_ = null;
		if ( _o1_ instanceof GameLevel ) _o_ = (GameLevel)_o1_;
		else if ( _o1_ instanceof GameLevel.Const ) _o_ = ((GameLevel.Const)_o1_).nThis();
		else return false;
		if (battleid != _o_.battleid) return false;
		if (!useherokeylist.equals(_o_.useherokeylist)) return false;
		if (dropgold != _o_.dropgold) return false;
		if (dropcrystal != _o_.dropcrystal) return false;
		if (!equipidlist.equals(_o_.equipidlist)) return false;
		if (trooptype != _o_.trooptype) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += battleid;
		_h_ += useherokeylist.hashCode();
		_h_ += dropgold;
		_h_ += dropcrystal;
		_h_ += equipidlist.hashCode();
		_h_ += trooptype;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(battleid);
		_sb_.append(",");
		_sb_.append(useherokeylist);
		_sb_.append(",");
		_sb_.append(dropgold);
		_sb_.append(",");
		_sb_.append(dropcrystal);
		_sb_.append(",");
		_sb_.append(equipidlist);
		_sb_.append(",");
		_sb_.append(trooptype);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("battleid"));
		lb.add(new xdb.logs.ListenableMap().setVarName("useherokeylist"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("dropgold"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("dropcrystal"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("equipidlist"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("trooptype"));
		return lb;
	}

	private class Const implements xbean.GameLevel {
		GameLevel nThis() {
			return GameLevel.this;
		}

		@Override
		public xbean.GameLevel copy() {
			return GameLevel.this.copy();
		}

		@Override
		public xbean.GameLevel toData() {
			return GameLevel.this.toData();
		}

		public xbean.GameLevel toBean() {
			return GameLevel.this.toBean();
		}

		@Override
		public xbean.GameLevel toDataIf() {
			return GameLevel.this.toDataIf();
		}

		public xbean.GameLevel toBeanIf() {
			return GameLevel.this.toBeanIf();
		}

		@Override
		public int getBattleid() { // 副本ID
			_xdb_verify_unsafe_();
			return battleid;
		}

		@Override
		public java.util.Map<Integer, Integer> getUseherokeylist() { // 关卡用到的英雄
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(useherokeylist);
		}

		@Override
		public java.util.Map<Integer, Integer> getUseherokeylistAsData() { // 关卡用到的英雄
			_xdb_verify_unsafe_();
			java.util.Map<Integer, Integer> useherokeylist;
			GameLevel _o_ = GameLevel.this;
			useherokeylist = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.useherokeylist.entrySet())
				useherokeylist.put(_e_.getKey(), _e_.getValue());
			return useherokeylist;
		}

		@Override
		public int getDropgold() { // 掉落金币
			_xdb_verify_unsafe_();
			return dropgold;
		}

		@Override
		public int getDropcrystal() { // 掉落宝石
			_xdb_verify_unsafe_();
			return dropcrystal;
		}

		@Override
		public java.util.List<Integer> getEquipidlist() { // 掉落物品列表
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(equipidlist);
		}

		public java.util.List<Integer> getEquipidlistAsData() { // 掉落物品列表
			_xdb_verify_unsafe_();
			java.util.List<Integer> equipidlist;
			GameLevel _o_ = GameLevel.this;
		equipidlist = new java.util.LinkedList<Integer>();
		equipidlist.addAll(_o_.equipidlist);
			return equipidlist;
		}

		@Override
		public int getTrooptype() { // 战队类型
			_xdb_verify_unsafe_();
			return trooptype;
		}

		@Override
		public void setBattleid(int _v_) { // 副本ID
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setDropgold(int _v_) { // 掉落金币
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setDropcrystal(int _v_) { // 掉落宝石
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTrooptype(int _v_) { // 战队类型
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
			return GameLevel.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return GameLevel.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return GameLevel.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return GameLevel.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return GameLevel.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return GameLevel.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return GameLevel.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return GameLevel.this.hashCode();
		}

		@Override
		public String toString() {
			return GameLevel.this.toString();
		}

	}

	public static final class Data implements xbean.GameLevel {
		private int battleid; // 副本ID
		private java.util.HashMap<Integer, Integer> useherokeylist; // 关卡用到的英雄
		private int dropgold; // 掉落金币
		private int dropcrystal; // 掉落宝石
		private java.util.LinkedList<Integer> equipidlist; // 掉落物品列表
		private int trooptype; // 战队类型

		public Data() {
			useherokeylist = new java.util.HashMap<Integer, Integer>();
			equipidlist = new java.util.LinkedList<Integer>();
		}

		Data(xbean.GameLevel _o1_) {
			if (_o1_ instanceof GameLevel) assign((GameLevel)_o1_);
			else if (_o1_ instanceof GameLevel.Data) assign((GameLevel.Data)_o1_);
			else if (_o1_ instanceof GameLevel.Const) assign(((GameLevel.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(GameLevel _o_) {
			battleid = _o_.battleid;
			useherokeylist = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.useherokeylist.entrySet())
				useherokeylist.put(_e_.getKey(), _e_.getValue());
			dropgold = _o_.dropgold;
			dropcrystal = _o_.dropcrystal;
			equipidlist = new java.util.LinkedList<Integer>();
			equipidlist.addAll(_o_.equipidlist);
			trooptype = _o_.trooptype;
		}

		private void assign(GameLevel.Data _o_) {
			battleid = _o_.battleid;
			useherokeylist = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.useherokeylist.entrySet())
				useherokeylist.put(_e_.getKey(), _e_.getValue());
			dropgold = _o_.dropgold;
			dropcrystal = _o_.dropcrystal;
			equipidlist = new java.util.LinkedList<Integer>();
			equipidlist.addAll(_o_.equipidlist);
			trooptype = _o_.trooptype;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(battleid);
			_os_.compact_uint32(useherokeylist.size());
			for (java.util.Map.Entry<Integer, Integer> _e_ : useherokeylist.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_os_.marshal(_e_.getValue());
			}
			_os_.marshal(dropgold);
			_os_.marshal(dropcrystal);
			_os_.compact_uint32(equipidlist.size());
			for (Integer _v_ : equipidlist) {
				_os_.marshal(_v_);
			}
			_os_.marshal(trooptype);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			battleid = _os_.unmarshal_int();
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
			dropgold = _os_.unmarshal_int();
			dropcrystal = _os_.unmarshal_int();
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				int _v_ = 0;
				_v_ = _os_.unmarshal_int();
				equipidlist.add(_v_);
			}
			trooptype = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.GameLevel copy() {
			return new Data(this);
		}

		@Override
		public xbean.GameLevel toData() {
			return new Data(this);
		}

		public xbean.GameLevel toBean() {
			return new GameLevel(this, null, null);
		}

		@Override
		public xbean.GameLevel toDataIf() {
			return this;
		}

		public xbean.GameLevel toBeanIf() {
			return new GameLevel(this, null, null);
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
		public int getBattleid() { // 副本ID
			return battleid;
		}

		@Override
		public java.util.Map<Integer, Integer> getUseherokeylist() { // 关卡用到的英雄
			return useherokeylist;
		}

		@Override
		public java.util.Map<Integer, Integer> getUseherokeylistAsData() { // 关卡用到的英雄
			return useherokeylist;
		}

		@Override
		public int getDropgold() { // 掉落金币
			return dropgold;
		}

		@Override
		public int getDropcrystal() { // 掉落宝石
			return dropcrystal;
		}

		@Override
		public java.util.List<Integer> getEquipidlist() { // 掉落物品列表
			return equipidlist;
		}

		@Override
		public java.util.List<Integer> getEquipidlistAsData() { // 掉落物品列表
			return equipidlist;
		}

		@Override
		public int getTrooptype() { // 战队类型
			return trooptype;
		}

		@Override
		public void setBattleid(int _v_) { // 副本ID
			battleid = _v_;
		}

		@Override
		public void setDropgold(int _v_) { // 掉落金币
			dropgold = _v_;
		}

		@Override
		public void setDropcrystal(int _v_) { // 掉落宝石
			dropcrystal = _v_;
		}

		@Override
		public void setTrooptype(int _v_) { // 战队类型
			trooptype = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof GameLevel.Data)) return false;
			GameLevel.Data _o_ = (GameLevel.Data) _o1_;
			if (battleid != _o_.battleid) return false;
			if (!useherokeylist.equals(_o_.useherokeylist)) return false;
			if (dropgold != _o_.dropgold) return false;
			if (dropcrystal != _o_.dropcrystal) return false;
			if (!equipidlist.equals(_o_.equipidlist)) return false;
			if (trooptype != _o_.trooptype) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += battleid;
			_h_ += useherokeylist.hashCode();
			_h_ += dropgold;
			_h_ += dropcrystal;
			_h_ += equipidlist.hashCode();
			_h_ += trooptype;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(battleid);
			_sb_.append(",");
			_sb_.append(useherokeylist);
			_sb_.append(",");
			_sb_.append(dropgold);
			_sb_.append(",");
			_sb_.append(dropcrystal);
			_sb_.append(",");
			_sb_.append(equipidlist);
			_sb_.append(",");
			_sb_.append(trooptype);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
