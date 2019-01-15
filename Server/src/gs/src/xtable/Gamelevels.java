package xtable;

// typed table access point
public class Gamelevels {
	Gamelevels() {
	}

	public static xbean.GameLevel get(Long key) {
		return _Tables_.getInstance().gamelevels.get(key);
	}

	public static xbean.GameLevel get(Long key, xbean.GameLevel value) {
		return _Tables_.getInstance().gamelevels.get(key, value);
	}

	public static void insert(Long key, xbean.GameLevel value) {
		_Tables_.getInstance().gamelevels.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().gamelevels.delete(key);
	}

	public static boolean add(Long key, xbean.GameLevel value) {
		return _Tables_.getInstance().gamelevels.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().gamelevels.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.GameLevel> getCache() {
		return _Tables_.getInstance().gamelevels.getCache();
	}

	public static xdb.TTable<Long, xbean.GameLevel> getTable() {
		return _Tables_.getInstance().gamelevels;
	}

	public static xbean.GameLevel select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.GameLevel, xbean.GameLevel>() {
			public xbean.GameLevel get(xbean.GameLevel v) { return v.toData(); }
		});
	}

	public static Integer selectBattleid(Long key) {
		return getTable().select(key, new xdb.TField<xbean.GameLevel, Integer>() {
				public Integer get(xbean.GameLevel v) { return v.getBattleid(); }
			});
	}

	public static java.util.Map<Integer, Integer> selectUseherokeylist(Long key) {
		return getTable().select(key, new xdb.TField<xbean.GameLevel, java.util.Map<Integer, Integer>>() {
				public java.util.Map<Integer, Integer> get(xbean.GameLevel v) { return v.getUseherokeylistAsData(); }
			});
	}

	public static Integer selectDropgold(Long key) {
		return getTable().select(key, new xdb.TField<xbean.GameLevel, Integer>() {
				public Integer get(xbean.GameLevel v) { return v.getDropgold(); }
			});
	}

	public static Integer selectDropcrystal(Long key) {
		return getTable().select(key, new xdb.TField<xbean.GameLevel, Integer>() {
				public Integer get(xbean.GameLevel v) { return v.getDropcrystal(); }
			});
	}

	public static java.util.List<Integer> selectEquipidlist(Long key) {
		return getTable().select(key, new xdb.TField<xbean.GameLevel, java.util.List<Integer>>() {
				public java.util.List<Integer> get(xbean.GameLevel v) { return v.getEquipidlistAsData(); }
			});
	}

	public static Integer selectTrooptype(Long key) {
		return getTable().select(key, new xdb.TField<xbean.GameLevel, Integer>() {
				public Integer get(xbean.GameLevel v) { return v.getTrooptype(); }
			});
	}

}
