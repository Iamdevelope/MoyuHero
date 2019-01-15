package xtable;

// typed table access point
public class Moheoddses {
	Moheoddses() {
	}

	public static xbean.moheodds get(Long key) {
		return _Tables_.getInstance().moheoddses.get(key);
	}

	public static xbean.moheodds get(Long key, xbean.moheodds value) {
		return _Tables_.getInstance().moheoddses.get(key, value);
	}

	public static void insert(Long key, xbean.moheodds value) {
		_Tables_.getInstance().moheoddses.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().moheoddses.delete(key);
	}

	public static boolean add(Long key, xbean.moheodds value) {
		return _Tables_.getInstance().moheoddses.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().moheoddses.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.moheodds> getCache() {
		return _Tables_.getInstance().moheoddses.getCache();
	}

	public static xdb.TTable<Long, xbean.moheodds> getTable() {
		return _Tables_.getInstance().moheoddses;
	}

	public static xbean.moheodds select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.moheodds, xbean.moheodds>() {
			public xbean.moheodds get(xbean.moheodds v) { return v.toData(); }
		});
	}

	public static java.util.Map<Integer, Integer> selectMoheoddsmap(Long key) {
		return getTable().select(key, new xdb.TField<xbean.moheodds, java.util.Map<Integer, Integer>>() {
				public java.util.Map<Integer, Integer> get(xbean.moheodds v) { return v.getMoheoddsmapAsData(); }
			});
	}

}
