package xtable;

// typed table access point
public class Serverinfo {
	Serverinfo() {
	}

	public static xbean.ServerInfo get(Integer key) {
		return _Tables_.getInstance().serverinfo.get(key);
	}

	public static xbean.ServerInfo get(Integer key, xbean.ServerInfo value) {
		return _Tables_.getInstance().serverinfo.get(key, value);
	}

	public static void insert(Integer key, xbean.ServerInfo value) {
		_Tables_.getInstance().serverinfo.insert(key, value);
	}

	public static void delete(Integer key) {
		_Tables_.getInstance().serverinfo.delete(key);
	}

	public static boolean add(Integer key, xbean.ServerInfo value) {
		return _Tables_.getInstance().serverinfo.add(key, value);
	}

	public static boolean remove(Integer key) {
		return _Tables_.getInstance().serverinfo.remove(key);
	}

	public static xdb.TTableCache<Integer, xbean.ServerInfo> getCache() {
		return _Tables_.getInstance().serverinfo.getCache();
	}

	public static xdb.TTable<Integer, xbean.ServerInfo> getTable() {
		return _Tables_.getInstance().serverinfo;
	}

	public static xbean.ServerInfo select(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.ServerInfo, xbean.ServerInfo>() {
			public xbean.ServerInfo get(xbean.ServerInfo v) { return v.toData(); }
		});
	}

	public static Long selectFirsttime(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.ServerInfo, Long>() {
				public Long get(xbean.ServerInfo v) { return v.getFirsttime(); }
			});
	}

	public static Long selectStarttime(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.ServerInfo, Long>() {
				public Long get(xbean.ServerInfo v) { return v.getStarttime(); }
			});
	}

}
