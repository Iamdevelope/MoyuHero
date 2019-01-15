package xtable;

// typed table access point
public class Huoyuelist {
	Huoyuelist() {
	}

	public static xbean.huoyues get(Long key) {
		return _Tables_.getInstance().huoyuelist.get(key);
	}

	public static xbean.huoyues get(Long key, xbean.huoyues value) {
		return _Tables_.getInstance().huoyuelist.get(key, value);
	}

	public static void insert(Long key, xbean.huoyues value) {
		_Tables_.getInstance().huoyuelist.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().huoyuelist.delete(key);
	}

	public static boolean add(Long key, xbean.huoyues value) {
		return _Tables_.getInstance().huoyuelist.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().huoyuelist.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.huoyues> getCache() {
		return _Tables_.getInstance().huoyuelist.getCache();
	}

	public static xdb.TTable<Long, xbean.huoyues> getTable() {
		return _Tables_.getInstance().huoyuelist;
	}

	public static xbean.huoyues select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.huoyues, xbean.huoyues>() {
			public xbean.huoyues get(xbean.huoyues v) { return v.toData(); }
		});
	}

	public static Integer selectHuoyuenum(Long key) {
		return getTable().select(key, new xdb.TField<xbean.huoyues, Integer>() {
				public Integer get(xbean.huoyues v) { return v.getHuoyuenum(); }
			});
	}

	public static Integer selectGetnum(Long key) {
		return getTable().select(key, new xdb.TField<xbean.huoyues, Integer>() {
				public Integer get(xbean.huoyues v) { return v.getGetnum(); }
			});
	}

	public static Long selectHuoyuetime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.huoyues, Long>() {
				public Long get(xbean.huoyues v) { return v.getHuoyuetime(); }
			});
	}

	public static java.util.Map<Integer, xbean.huoyue> selectHuoyuemap(Long key) {
		return getTable().select(key, new xdb.TField<xbean.huoyues, java.util.Map<Integer, xbean.huoyue>>() {
				public java.util.Map<Integer, xbean.huoyue> get(xbean.huoyues v) { return v.getHuoyuemapAsData(); }
			});
	}

}
