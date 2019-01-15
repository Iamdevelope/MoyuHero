package xtable;

// typed table access point
public class Bossranklists {
	Bossranklists() {
	}

	public static xbean.bossRankList get(Integer key) {
		return _Tables_.getInstance().bossranklists.get(key);
	}

	public static xbean.bossRankList get(Integer key, xbean.bossRankList value) {
		return _Tables_.getInstance().bossranklists.get(key, value);
	}

	public static void insert(Integer key, xbean.bossRankList value) {
		_Tables_.getInstance().bossranklists.insert(key, value);
	}

	public static void delete(Integer key) {
		_Tables_.getInstance().bossranklists.delete(key);
	}

	public static boolean add(Integer key, xbean.bossRankList value) {
		return _Tables_.getInstance().bossranklists.add(key, value);
	}

	public static boolean remove(Integer key) {
		return _Tables_.getInstance().bossranklists.remove(key);
	}

	public static xdb.TTableCache<Integer, xbean.bossRankList> getCache() {
		return _Tables_.getInstance().bossranklists.getCache();
	}

	public static xdb.TTable<Integer, xbean.bossRankList> getTable() {
		return _Tables_.getInstance().bossranklists;
	}

	public static xbean.bossRankList select(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.bossRankList, xbean.bossRankList>() {
			public xbean.bossRankList get(xbean.bossRankList v) { return v.toData(); }
		});
	}

	public static java.util.List<xbean.bossRankInfo> selectRanklist(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.bossRankList, java.util.List<xbean.bossRankInfo>>() {
				public java.util.List<xbean.bossRankInfo> get(xbean.bossRankList v) { return v.getRanklistAsData(); }
			});
	}

	public static Long selectRanktime(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.bossRankList, Long>() {
				public Long get(xbean.bossRankList v) { return v.getRanktime(); }
			});
	}

	public static Integer selectBossid(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.bossRankList, Integer>() {
				public Integer get(xbean.bossRankList v) { return v.getBossid(); }
			});
	}

}
