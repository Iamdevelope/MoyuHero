package xtable;

// typed table access point
public class Heroclones {
	Heroclones() {
	}

	public static xbean.heroclone get(Long key) {
		return _Tables_.getInstance().heroclones.get(key);
	}

	public static xbean.heroclone get(Long key, xbean.heroclone value) {
		return _Tables_.getInstance().heroclones.get(key, value);
	}

	public static void insert(Long key, xbean.heroclone value) {
		_Tables_.getInstance().heroclones.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().heroclones.delete(key);
	}

	public static boolean add(Long key, xbean.heroclone value) {
		return _Tables_.getInstance().heroclones.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().heroclones.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.heroclone> getCache() {
		return _Tables_.getInstance().heroclones.getCache();
	}

	public static xdb.TTable<Long, xbean.heroclone> getTable() {
		return _Tables_.getInstance().heroclones;
	}

	public static xbean.heroclone select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.heroclone, xbean.heroclone>() {
			public xbean.heroclone get(xbean.heroclone v) { return v.toData(); }
		});
	}

	public static java.util.List<Integer> selectClonelist(Long key) {
		return getTable().select(key, new xdb.TField<xbean.heroclone, java.util.List<Integer>>() {
				public java.util.List<Integer> get(xbean.heroclone v) { return v.getClonelistAsData(); }
			});
	}

}
