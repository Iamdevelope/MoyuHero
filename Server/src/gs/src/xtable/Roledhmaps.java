package xtable;

// typed table access point
public class Roledhmaps {
	Roledhmaps() {
	}

	public static xbean.roledhmap get(Long key) {
		return _Tables_.getInstance().roledhmaps.get(key);
	}

	public static xbean.roledhmap get(Long key, xbean.roledhmap value) {
		return _Tables_.getInstance().roledhmaps.get(key, value);
	}

	public static void insert(Long key, xbean.roledhmap value) {
		_Tables_.getInstance().roledhmaps.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().roledhmaps.delete(key);
	}

	public static boolean add(Long key, xbean.roledhmap value) {
		return _Tables_.getInstance().roledhmaps.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().roledhmaps.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.roledhmap> getCache() {
		return _Tables_.getInstance().roledhmaps.getCache();
	}

	public static xdb.TTable<Long, xbean.roledhmap> getTable() {
		return _Tables_.getInstance().roledhmaps;
	}

	public static xbean.roledhmap select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.roledhmap, xbean.roledhmap>() {
			public xbean.roledhmap get(xbean.roledhmap v) { return v.toData(); }
		});
	}

	public static java.util.Map<Integer, xbean.roleduihuanlq> selectDhmap(Long key) {
		return getTable().select(key, new xdb.TField<xbean.roledhmap, java.util.Map<Integer, xbean.roleduihuanlq>>() {
				public java.util.Map<Integer, xbean.roleduihuanlq> get(xbean.roledhmap v) { return v.getDhmapAsData(); }
			});
	}

}
