package xtable;

// typed table access point
public class Duihuanlqs {
	Duihuanlqs() {
	}

	public static xbean.duihuanlq get(Integer key) {
		return _Tables_.getInstance().duihuanlqs.get(key);
	}

	public static xbean.duihuanlq get(Integer key, xbean.duihuanlq value) {
		return _Tables_.getInstance().duihuanlqs.get(key, value);
	}

	public static void insert(Integer key, xbean.duihuanlq value) {
		_Tables_.getInstance().duihuanlqs.insert(key, value);
	}

	public static void delete(Integer key) {
		_Tables_.getInstance().duihuanlqs.delete(key);
	}

	public static boolean add(Integer key, xbean.duihuanlq value) {
		return _Tables_.getInstance().duihuanlqs.add(key, value);
	}

	public static boolean remove(Integer key) {
		return _Tables_.getInstance().duihuanlqs.remove(key);
	}

	public static xdb.TTableCache<Integer, xbean.duihuanlq> getCache() {
		return _Tables_.getInstance().duihuanlqs.getCache();
	}

	public static xdb.TTable<Integer, xbean.duihuanlq> getTable() {
		return _Tables_.getInstance().duihuanlqs;
	}

	public static xbean.duihuanlq select(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.duihuanlq, xbean.duihuanlq>() {
			public xbean.duihuanlq get(xbean.duihuanlq v) { return v.toData(); }
		});
	}

	public static Integer selectLqkey(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.duihuanlq, Integer>() {
				public Integer get(xbean.duihuanlq v) { return v.getLqkey(); }
			});
	}

	public static Integer selectTypenum(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.duihuanlq, Integer>() {
				public Integer get(xbean.duihuanlq v) { return v.getTypenum(); }
			});
	}

	public static java.util.List<String> selectClonelist(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.duihuanlq, java.util.List<String>>() {
				public java.util.List<String> get(xbean.duihuanlq v) { return v.getClonelistAsData(); }
			});
	}

}
