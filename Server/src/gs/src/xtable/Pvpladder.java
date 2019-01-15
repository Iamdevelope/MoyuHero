package xtable;

// typed table access point
public class Pvpladder {
	Pvpladder() {
	}

	public static xbean.LadderInfo get(Integer key) {
		return _Tables_.getInstance().pvpladder.get(key);
	}

	public static xbean.LadderInfo get(Integer key, xbean.LadderInfo value) {
		return _Tables_.getInstance().pvpladder.get(key, value);
	}

	public static void insert(Integer key, xbean.LadderInfo value) {
		_Tables_.getInstance().pvpladder.insert(key, value);
	}

	public static void delete(Integer key) {
		_Tables_.getInstance().pvpladder.delete(key);
	}

	public static boolean add(Integer key, xbean.LadderInfo value) {
		return _Tables_.getInstance().pvpladder.add(key, value);
	}

	public static boolean remove(Integer key) {
		return _Tables_.getInstance().pvpladder.remove(key);
	}

	public static xdb.TTableCache<Integer, xbean.LadderInfo> getCache() {
		return _Tables_.getInstance().pvpladder.getCache();
	}

	public static xdb.TTable<Integer, xbean.LadderInfo> getTable() {
		return _Tables_.getInstance().pvpladder;
	}

	public static xbean.LadderInfo select(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.LadderInfo, xbean.LadderInfo>() {
			public xbean.LadderInfo get(xbean.LadderInfo v) { return v.toData(); }
		});
	}

	public static Long selectRoleid(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.LadderInfo, Long>() {
				public Long get(xbean.LadderInfo v) { return v.getRoleid(); }
			});
	}

}
