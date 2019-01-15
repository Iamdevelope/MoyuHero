package xtable;

// typed table access point
public class Itemlimits {
	Itemlimits() {
	}

	public static xbean.ItemNumLimit get(Long key) {
		return _Tables_.getInstance().itemlimits.get(key);
	}

	public static xbean.ItemNumLimit get(Long key, xbean.ItemNumLimit value) {
		return _Tables_.getInstance().itemlimits.get(key, value);
	}

	public static void insert(Long key, xbean.ItemNumLimit value) {
		_Tables_.getInstance().itemlimits.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().itemlimits.delete(key);
	}

	public static boolean add(Long key, xbean.ItemNumLimit value) {
		return _Tables_.getInstance().itemlimits.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().itemlimits.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.ItemNumLimit> getCache() {
		return _Tables_.getInstance().itemlimits.getCache();
	}

	public static xdb.TTable<Long, xbean.ItemNumLimit> getTable() {
		return _Tables_.getInstance().itemlimits;
	}

	public static xbean.ItemNumLimit select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.ItemNumLimit, xbean.ItemNumLimit>() {
			public xbean.ItemNumLimit get(xbean.ItemNumLimit v) { return v.toData(); }
		});
	}

	public static java.util.Map<Integer, Integer> selectItemnums(Long key) {
		return getTable().select(key, new xdb.TField<xbean.ItemNumLimit, java.util.Map<Integer, Integer>>() {
				public java.util.Map<Integer, Integer> get(xbean.ItemNumLimit v) { return v.getItemnumsAsData(); }
			});
	}

	public static Long selectTime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.ItemNumLimit, Long>() {
				public Long get(xbean.ItemNumLimit v) { return v.getTime(); }
			});
	}

}
