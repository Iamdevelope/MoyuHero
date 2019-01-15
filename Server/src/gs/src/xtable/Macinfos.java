package xtable;

// typed table access point
public class Macinfos {
	Macinfos() {
	}

	public static xbean.MacInfo get(String key) {
		return _Tables_.getInstance().macinfos.get(key);
	}

	public static xbean.MacInfo get(String key, xbean.MacInfo value) {
		return _Tables_.getInstance().macinfos.get(key, value);
	}

	public static void insert(String key, xbean.MacInfo value) {
		_Tables_.getInstance().macinfos.insert(key, value);
	}

	public static void delete(String key) {
		_Tables_.getInstance().macinfos.delete(key);
	}

	public static boolean add(String key, xbean.MacInfo value) {
		return _Tables_.getInstance().macinfos.add(key, value);
	}

	public static boolean remove(String key) {
		return _Tables_.getInstance().macinfos.remove(key);
	}

	public static xdb.TTableCache<String, xbean.MacInfo> getCache() {
		return _Tables_.getInstance().macinfos.getCache();
	}

	public static xdb.TTable<String, xbean.MacInfo> getTable() {
		return _Tables_.getInstance().macinfos;
	}

	public static xbean.MacInfo select(String key) {
		return getTable().select(key, new xdb.TField<xbean.MacInfo, xbean.MacInfo>() {
			public xbean.MacInfo get(xbean.MacInfo v) { return v.toData(); }
		});
	}

	public static Long selectOnlinetime(String key) {
		return getTable().select(key, new xdb.TField<xbean.MacInfo, Long>() {
				public Long get(xbean.MacInfo v) { return v.getOnlinetime(); }
			});
	}

	public static Long selectOfflinetime(String key) {
		return getTable().select(key, new xdb.TField<xbean.MacInfo, Long>() {
				public Long get(xbean.MacInfo v) { return v.getOfflinetime(); }
			});
	}

}
