package xtable;

// typed table access point
public class Battles {
	Battles() {
	}

	public static xbean.BattleInfo get(Long key) {
		return _Tables_.getInstance().battles.get(key);
	}

	public static xbean.BattleInfo get(Long key, xbean.BattleInfo value) {
		return _Tables_.getInstance().battles.get(key, value);
	}

	public static void insert(Long key, xbean.BattleInfo value) {
		_Tables_.getInstance().battles.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().battles.delete(key);
	}

	public static boolean add(Long key, xbean.BattleInfo value) {
		return _Tables_.getInstance().battles.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().battles.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.BattleInfo> getCache() {
		return _Tables_.getInstance().battles.getCache();
	}

	public static xdb.TTable<Long, xbean.BattleInfo> getTable() {
		return _Tables_.getInstance().battles;
	}

	public static Integer selectBattleid(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BattleInfo, Integer>() {
				public Integer get(xbean.BattleInfo v) { return v.getBattleid(); }
			});
	}

	public static Integer selectBattlelevel(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BattleInfo, Integer>() {
				public Integer get(xbean.BattleInfo v) { return v.getBattlelevel(); }
			});
	}

	public static Integer selectBattletype(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BattleInfo, Integer>() {
				public Integer get(xbean.BattleInfo v) { return v.getBattletype(); }
			});
	}

	public static java.util.Map<Integer, xbean.FighterInfo> selectFighterinfos(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BattleInfo, java.util.Map<Integer, xbean.FighterInfo>>() {
				public java.util.Map<Integer, xbean.FighterInfo> get(xbean.BattleInfo v) { return v.getFighterinfosAsData(); }
			});
	}

	public static java.util.Map<Integer, xbean.FighterInfo> selectDeadfighters(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BattleInfo, java.util.Map<Integer, xbean.FighterInfo>>() {
				public java.util.Map<Integer, xbean.FighterInfo> get(xbean.BattleInfo v) { return v.getDeadfightersAsData(); }
			});
	}

	public static Integer selectBattlereulst(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BattleInfo, Integer>() {
				public Integer get(xbean.BattleInfo v) { return v.getBattlereulst(); }
			});
	}

	public static Integer selectRound(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BattleInfo, Integer>() {
				public Integer get(xbean.BattleInfo v) { return v.getRound(); }
			});
	}

	public static Integer selectTurn(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BattleInfo, Integer>() {
				public Integer get(xbean.BattleInfo v) { return v.getTurn(); }
			});
	}

}
