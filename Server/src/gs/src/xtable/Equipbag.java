package xtable;

// typed table access point
public class Equipbag {
	Equipbag() {
	}

	public static xbean.Bag get(Long key) {
		return _Tables_.getInstance().equipbag.get(key);
	}

	public static xbean.Bag get(Long key, xbean.Bag value) {
		return _Tables_.getInstance().equipbag.get(key, value);
	}

	public static void insert(Long key, xbean.Bag value) {
		_Tables_.getInstance().equipbag.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().equipbag.delete(key);
	}

	public static boolean add(Long key, xbean.Bag value) {
		return _Tables_.getInstance().equipbag.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().equipbag.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.Bag> getCache() {
		return _Tables_.getInstance().equipbag.getCache();
	}

	public static xdb.TTable<Long, xbean.Bag> getTable() {
		return _Tables_.getInstance().equipbag;
	}

	public static xbean.Bag select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Bag, xbean.Bag>() {
			public xbean.Bag get(xbean.Bag v) { return v.toData(); }
		});
	}

	public static Long selectMoney(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Bag, Long>() {
				public Long get(xbean.Bag v) { return v.getMoney(); }
			});
	}

	public static Integer selectCapacity(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Bag, Integer>() {
				public Integer get(xbean.Bag v) { return v.getCapacity(); }
			});
	}

	public static Integer selectNextid(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Bag, Integer>() {
				public Integer get(xbean.Bag v) { return v.getNextid(); }
			});
	}

	public static java.util.Map<Integer, xbean.Item> selectItems(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Bag, java.util.Map<Integer, xbean.Item>>() {
				public java.util.Map<Integer, xbean.Item> get(xbean.Bag v) { return v.getItemsAsData(); }
			});
	}

	public static java.util.List<Integer> selectRemovedkeys(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Bag, java.util.List<Integer>>() {
				public java.util.List<Integer> get(xbean.Bag v) { return v.getRemovedkeysAsData(); }
			});
	}

}
