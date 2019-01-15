
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class BattleInfo extends xdb.XBean implements xbean.BattleInfo {
	private int battleid; // 
	private int battlelevel; // 
	private int battletype; // 
	private java.util.HashMap<Integer, xbean.FighterInfo> fighterinfos; // key=fighterid
	private java.util.HashMap<Integer, chuhan.gsp.battle.Fighter> fighters; // key=fighterid
	private java.util.HashMap<Integer, xbean.FighterInfo> deadfighters; // key=fighterid
	private int battlereulst; // 
	private int round; // 
	private int turn; // 
	private chuhan.gsp.util.FightJSEngine engine; // 用于本场战斗的JS引擎

	BattleInfo(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		fighterinfos = new java.util.HashMap<Integer, xbean.FighterInfo>();
		fighters = new java.util.HashMap<Integer, chuhan.gsp.battle.Fighter>();
		deadfighters = new java.util.HashMap<Integer, xbean.FighterInfo>();
		engine = null;
	}

	public BattleInfo() {
		this(0, null, null);
	}

	public BattleInfo(BattleInfo _o_) {
		this(_o_, null, null);
	}

	BattleInfo(xbean.BattleInfo _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		throw new UnsupportedOperationException();
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		throw new UnsupportedOperationException();
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		throw new UnsupportedOperationException();
	}

	@Override
	public xbean.BattleInfo copy() {
		_xdb_verify_unsafe_();
		return new BattleInfo(this);
	}

	@Override
	public xbean.BattleInfo toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.BattleInfo toBean() {
		_xdb_verify_unsafe_();
		return new BattleInfo(this); // same as copy()
	}

	@Override
	public xbean.BattleInfo toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.BattleInfo toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getBattleid() { // 
		_xdb_verify_unsafe_();
		return battleid;
	}

	@Override
	public int getBattlelevel() { // 
		_xdb_verify_unsafe_();
		return battlelevel;
	}

	@Override
	public int getBattletype() { // 
		_xdb_verify_unsafe_();
		return battletype;
	}

	@Override
	public java.util.Map<Integer, xbean.FighterInfo> getFighterinfos() { // key=fighterid
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "fighterinfos"), fighterinfos);
	}

	@Override
	public java.util.Map<Integer, xbean.FighterInfo> getFighterinfosAsData() { // key=fighterid
		_xdb_verify_unsafe_();
		java.util.Map<Integer, xbean.FighterInfo> fighterinfos;
		BattleInfo _o_ = this;
		fighterinfos = new java.util.HashMap<Integer, xbean.FighterInfo>();
		for (java.util.Map.Entry<Integer, xbean.FighterInfo> _e_ : _o_.fighterinfos.entrySet())
			fighterinfos.put(_e_.getKey(), new FighterInfo.Data(_e_.getValue()));
		return fighterinfos;
	}

	@Override
	public java.util.Map<Integer, chuhan.gsp.battle.Fighter> getFighters() { // key=fighterid
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "fighters"), fighters);
	}

	@Override
	public java.util.Map<Integer, xbean.FighterInfo> getDeadfighters() { // key=fighterid
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "deadfighters"), deadfighters);
	}

	@Override
	public java.util.Map<Integer, xbean.FighterInfo> getDeadfightersAsData() { // key=fighterid
		_xdb_verify_unsafe_();
		java.util.Map<Integer, xbean.FighterInfo> deadfighters;
		BattleInfo _o_ = this;
		deadfighters = new java.util.HashMap<Integer, xbean.FighterInfo>();
		for (java.util.Map.Entry<Integer, xbean.FighterInfo> _e_ : _o_.deadfighters.entrySet())
			deadfighters.put(_e_.getKey(), new FighterInfo.Data(_e_.getValue()));
		return deadfighters;
	}

	@Override
	public int getBattlereulst() { // 
		_xdb_verify_unsafe_();
		return battlereulst;
	}

	@Override
	public int getRound() { // 
		_xdb_verify_unsafe_();
		return round;
	}

	@Override
	public int getTurn() { // 
		_xdb_verify_unsafe_();
		return turn;
	}

	@Override
	public chuhan.gsp.util.FightJSEngine getEngine() { // 用于本场战斗的JS引擎
		_xdb_verify_unsafe_();
		return engine;
	}

	@Override
	public void setBattleid(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "battleid") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, battleid) {
					public void rollback() { battleid = _xdb_saved; }
				};}});
		battleid = _v_;
	}

	@Override
	public void setBattlelevel(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "battlelevel") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, battlelevel) {
					public void rollback() { battlelevel = _xdb_saved; }
				};}});
		battlelevel = _v_;
	}

	@Override
	public void setBattletype(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "battletype") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, battletype) {
					public void rollback() { battletype = _xdb_saved; }
				};}});
		battletype = _v_;
	}

	@Override
	public void setBattlereulst(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "battlereulst") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, battlereulst) {
					public void rollback() { battlereulst = _xdb_saved; }
				};}});
		battlereulst = _v_;
	}

	@Override
	public void setRound(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "round") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, round) {
					public void rollback() { round = _xdb_saved; }
				};}});
		round = _v_;
	}

	@Override
	public void setTurn(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "turn") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, turn) {
					public void rollback() { turn = _xdb_saved; }
				};}});
		turn = _v_;
	}

	@Override
	public void setEngine(chuhan.gsp.util.FightJSEngine _v_) { // 用于本场战斗的JS引擎
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "engine") {
			protected xdb.Log create() {
				return new xdb.logs.LogObject<chuhan.gsp.util.FightJSEngine>(this, engine) {
					public void rollback() { engine = _xdb_saved; }
			}; }});
		engine = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		BattleInfo _o_ = null;
		if ( _o1_ instanceof BattleInfo ) _o_ = (BattleInfo)_o1_;
		else if ( _o1_ instanceof BattleInfo.Const ) _o_ = ((BattleInfo.Const)_o1_).nThis();
		else return false;
		if (battleid != _o_.battleid) return false;
		if (battlelevel != _o_.battlelevel) return false;
		if (battletype != _o_.battletype) return false;
		if (!fighterinfos.equals(_o_.fighterinfos)) return false;
		if (!fighters.equals(_o_.fighters)) return false;
		if (!deadfighters.equals(_o_.deadfighters)) return false;
		if (battlereulst != _o_.battlereulst) return false;
		if (round != _o_.round) return false;
		if (turn != _o_.turn) return false;
		if (!engine.equals(_o_.engine)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += battleid;
		_h_ += battlelevel;
		_h_ += battletype;
		_h_ += fighterinfos.hashCode();
		_h_ += fighters.hashCode();
		_h_ += deadfighters.hashCode();
		_h_ += battlereulst;
		_h_ += round;
		_h_ += turn;
		_h_ += engine.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(battleid);
		_sb_.append(",");
		_sb_.append(battlelevel);
		_sb_.append(",");
		_sb_.append(battletype);
		_sb_.append(",");
		_sb_.append(fighterinfos);
		_sb_.append(",");
		_sb_.append(fighters);
		_sb_.append(",");
		_sb_.append(deadfighters);
		_sb_.append(",");
		_sb_.append(battlereulst);
		_sb_.append(",");
		_sb_.append(round);
		_sb_.append(",");
		_sb_.append(turn);
		_sb_.append(",");
		_sb_.append(engine);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("battleid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("battlelevel"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("battletype"));
		lb.add(new xdb.logs.ListenableMap().setVarName("fighterinfos"));
		lb.add(new xdb.logs.ListenableMap().setVarName("fighters"));
		lb.add(new xdb.logs.ListenableMap().setVarName("deadfighters"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("battlereulst"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("round"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("turn"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("engine"));
		return lb;
	}

	private class Const implements xbean.BattleInfo {
		BattleInfo nThis() {
			return BattleInfo.this;
		}

		@Override
		public xbean.BattleInfo copy() {
			return BattleInfo.this.copy();
		}

		@Override
		public xbean.BattleInfo toData() {
			return BattleInfo.this.toData();
		}

		public xbean.BattleInfo toBean() {
			return BattleInfo.this.toBean();
		}

		@Override
		public xbean.BattleInfo toDataIf() {
			return BattleInfo.this.toDataIf();
		}

		public xbean.BattleInfo toBeanIf() {
			return BattleInfo.this.toBeanIf();
		}

		@Override
		public int getBattleid() { // 
			_xdb_verify_unsafe_();
			return battleid;
		}

		@Override
		public int getBattlelevel() { // 
			_xdb_verify_unsafe_();
			return battlelevel;
		}

		@Override
		public int getBattletype() { // 
			_xdb_verify_unsafe_();
			return battletype;
		}

		@Override
		public java.util.Map<Integer, xbean.FighterInfo> getFighterinfos() { // key=fighterid
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(fighterinfos);
		}

		@Override
		public java.util.Map<Integer, xbean.FighterInfo> getFighterinfosAsData() { // key=fighterid
			_xdb_verify_unsafe_();
			java.util.Map<Integer, xbean.FighterInfo> fighterinfos;
			BattleInfo _o_ = BattleInfo.this;
			fighterinfos = new java.util.HashMap<Integer, xbean.FighterInfo>();
			for (java.util.Map.Entry<Integer, xbean.FighterInfo> _e_ : _o_.fighterinfos.entrySet())
				fighterinfos.put(_e_.getKey(), new FighterInfo.Data(_e_.getValue()));
			return fighterinfos;
		}

		@Override
		public java.util.Map<Integer, chuhan.gsp.battle.Fighter> getFighters() { // key=fighterid
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(fighters);
		}

		@Override
		public java.util.Map<Integer, xbean.FighterInfo> getDeadfighters() { // key=fighterid
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(deadfighters);
		}

		@Override
		public java.util.Map<Integer, xbean.FighterInfo> getDeadfightersAsData() { // key=fighterid
			_xdb_verify_unsafe_();
			java.util.Map<Integer, xbean.FighterInfo> deadfighters;
			BattleInfo _o_ = BattleInfo.this;
			deadfighters = new java.util.HashMap<Integer, xbean.FighterInfo>();
			for (java.util.Map.Entry<Integer, xbean.FighterInfo> _e_ : _o_.deadfighters.entrySet())
				deadfighters.put(_e_.getKey(), new FighterInfo.Data(_e_.getValue()));
			return deadfighters;
		}

		@Override
		public int getBattlereulst() { // 
			_xdb_verify_unsafe_();
			return battlereulst;
		}

		@Override
		public int getRound() { // 
			_xdb_verify_unsafe_();
			return round;
		}

		@Override
		public int getTurn() { // 
			_xdb_verify_unsafe_();
			return turn;
		}

		@Override
		public chuhan.gsp.util.FightJSEngine getEngine() { // 用于本场战斗的JS引擎
			_xdb_verify_unsafe_();
			return engine;
		}

		@Override
		public void setBattleid(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setBattlelevel(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setBattletype(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setBattlereulst(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setRound(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTurn(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setEngine(chuhan.gsp.util.FightJSEngine _v_) { // 用于本场战斗的JS引擎
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
			return BattleInfo.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return BattleInfo.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return BattleInfo.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return BattleInfo.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return BattleInfo.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return BattleInfo.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return BattleInfo.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return BattleInfo.this.hashCode();
		}

		@Override
		public String toString() {
			return BattleInfo.this.toString();
		}

	}

	public static final class Data implements xbean.BattleInfo {
		private int battleid; // 
		private int battlelevel; // 
		private int battletype; // 
		private java.util.HashMap<Integer, xbean.FighterInfo> fighterinfos; // key=fighterid
		private java.util.HashMap<Integer, chuhan.gsp.battle.Fighter> fighters; // key=fighterid
		private java.util.HashMap<Integer, xbean.FighterInfo> deadfighters; // key=fighterid
		private int battlereulst; // 
		private int round; // 
		private int turn; // 
		private chuhan.gsp.util.FightJSEngine engine; // 用于本场战斗的JS引擎

		public Data() {
			fighterinfos = new java.util.HashMap<Integer, xbean.FighterInfo>();
			fighters = new java.util.HashMap<Integer, chuhan.gsp.battle.Fighter>();
			deadfighters = new java.util.HashMap<Integer, xbean.FighterInfo>();
			engine = null;
		}

		Data(xbean.BattleInfo _o1_) {
			throw new UnsupportedOperationException();
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			throw new UnsupportedOperationException();
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			throw new UnsupportedOperationException();
		}

		@Override
		public xbean.BattleInfo copy() {
			return new Data(this);
		}

		@Override
		public xbean.BattleInfo toData() {
			return new Data(this);
		}

		public xbean.BattleInfo toBean() {
			return new BattleInfo(this, null, null);
		}

		@Override
		public xbean.BattleInfo toDataIf() {
			return this;
		}

		public xbean.BattleInfo toBeanIf() {
			return new BattleInfo(this, null, null);
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
		public int getBattleid() { // 
			return battleid;
		}

		@Override
		public int getBattlelevel() { // 
			return battlelevel;
		}

		@Override
		public int getBattletype() { // 
			return battletype;
		}

		@Override
		public java.util.Map<Integer, xbean.FighterInfo> getFighterinfos() { // key=fighterid
			return fighterinfos;
		}

		@Override
		public java.util.Map<Integer, xbean.FighterInfo> getFighterinfosAsData() { // key=fighterid
			return fighterinfos;
		}

		@Override
		public java.util.Map<Integer, chuhan.gsp.battle.Fighter> getFighters() { // key=fighterid
			return fighters;
		}

		@Override
		public java.util.Map<Integer, xbean.FighterInfo> getDeadfighters() { // key=fighterid
			return deadfighters;
		}

		@Override
		public java.util.Map<Integer, xbean.FighterInfo> getDeadfightersAsData() { // key=fighterid
			return deadfighters;
		}

		@Override
		public int getBattlereulst() { // 
			return battlereulst;
		}

		@Override
		public int getRound() { // 
			return round;
		}

		@Override
		public int getTurn() { // 
			return turn;
		}

		@Override
		public chuhan.gsp.util.FightJSEngine getEngine() { // 用于本场战斗的JS引擎
			return engine;
		}

		@Override
		public void setBattleid(int _v_) { // 
			battleid = _v_;
		}

		@Override
		public void setBattlelevel(int _v_) { // 
			battlelevel = _v_;
		}

		@Override
		public void setBattletype(int _v_) { // 
			battletype = _v_;
		}

		@Override
		public void setBattlereulst(int _v_) { // 
			battlereulst = _v_;
		}

		@Override
		public void setRound(int _v_) { // 
			round = _v_;
		}

		@Override
		public void setTurn(int _v_) { // 
			turn = _v_;
		}

		@Override
		public void setEngine(chuhan.gsp.util.FightJSEngine _v_) { // 用于本场战斗的JS引擎
			engine = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof BattleInfo.Data)) return false;
			BattleInfo.Data _o_ = (BattleInfo.Data) _o1_;
			if (battleid != _o_.battleid) return false;
			if (battlelevel != _o_.battlelevel) return false;
			if (battletype != _o_.battletype) return false;
			if (!fighterinfos.equals(_o_.fighterinfos)) return false;
			if (!fighters.equals(_o_.fighters)) return false;
			if (!deadfighters.equals(_o_.deadfighters)) return false;
			if (battlereulst != _o_.battlereulst) return false;
			if (round != _o_.round) return false;
			if (turn != _o_.turn) return false;
			if (!engine.equals(_o_.engine)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += battleid;
			_h_ += battlelevel;
			_h_ += battletype;
			_h_ += fighterinfos.hashCode();
			_h_ += fighters.hashCode();
			_h_ += deadfighters.hashCode();
			_h_ += battlereulst;
			_h_ += round;
			_h_ += turn;
			_h_ += engine.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(battleid);
			_sb_.append(",");
			_sb_.append(battlelevel);
			_sb_.append(",");
			_sb_.append(battletype);
			_sb_.append(",");
			_sb_.append(fighterinfos);
			_sb_.append(",");
			_sb_.append(fighters);
			_sb_.append(",");
			_sb_.append(deadfighters);
			_sb_.append(",");
			_sb_.append(battlereulst);
			_sb_.append(",");
			_sb_.append(round);
			_sb_.append(",");
			_sb_.append(turn);
			_sb_.append(",");
			_sb_.append(engine);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
