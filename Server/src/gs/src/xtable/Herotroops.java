package xtable;

// typed table access point
public class Herotroops {
	Herotroops() {
	}

	public static xbean.Troops get(Long key) {
		return _Tables_.getInstance().herotroops.get(key);
	}

	public static xbean.Troops get(Long key, xbean.Troops value) {
		return _Tables_.getInstance().herotroops.get(key, value);
	}

	public static void insert(Long key, xbean.Troops value) {
		_Tables_.getInstance().herotroops.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().herotroops.delete(key);
	}

	public static boolean add(Long key, xbean.Troops value) {
		return _Tables_.getInstance().herotroops.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().herotroops.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.Troops> getCache() {
		return _Tables_.getInstance().herotroops.getCache();
	}

	public static xdb.TTable<Long, xbean.Troops> getTable() {
		return _Tables_.getInstance().herotroops;
	}

	public static xbean.Troops select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Troops, xbean.Troops>() {
			public xbean.Troops get(xbean.Troops v) { return v.toData(); }
		});
	}

	public static java.util.List<xbean.Troop> selectTroops(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Troops, java.util.List<xbean.Troop>>() {
				public java.util.List<xbean.Troop> get(xbean.Troops v) { return v.getTroopsAsData(); }
			});
	}

}
