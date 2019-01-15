package xtable;

// typed table access point
public class Endlessranklists {
	Endlessranklists() {
	}

	public static xbean.EndlessRankList get(Integer key) {
		return _Tables_.getInstance().endlessranklists.get(key);
	}

	public static xbean.EndlessRankList get(Integer key, xbean.EndlessRankList value) {
		return _Tables_.getInstance().endlessranklists.get(key, value);
	}

	public static void insert(Integer key, xbean.EndlessRankList value) {
		_Tables_.getInstance().endlessranklists.insert(key, value);
	}

	public static void delete(Integer key) {
		_Tables_.getInstance().endlessranklists.delete(key);
	}

	public static boolean add(Integer key, xbean.EndlessRankList value) {
		return _Tables_.getInstance().endlessranklists.add(key, value);
	}

	public static boolean remove(Integer key) {
		return _Tables_.getInstance().endlessranklists.remove(key);
	}

	public static xdb.TTableCache<Integer, xbean.EndlessRankList> getCache() {
		return _Tables_.getInstance().endlessranklists.getCache();
	}

	public static xdb.TTable<Integer, xbean.EndlessRankList> getTable() {
		return _Tables_.getInstance().endlessranklists;
	}

	public static xbean.EndlessRankList select(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessRankList, xbean.EndlessRankList>() {
			public xbean.EndlessRankList get(xbean.EndlessRankList v) { return v.toData(); }
		});
	}

	public static java.util.List<xbean.EndlessRankInfo> selectRanklist(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessRankList, java.util.List<xbean.EndlessRankInfo>>() {
				public java.util.List<xbean.EndlessRankInfo> get(xbean.EndlessRankList v) { return v.getRanklistAsData(); }
			});
	}

	public static Long selectRanktime(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessRankList, Long>() {
				public Long get(xbean.EndlessRankList v) { return v.getRanktime(); }
			});
	}

}
