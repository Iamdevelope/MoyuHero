package xtable;

// typed table access point
public class Bloodranklist {
	Bloodranklist() {
	}

	public static xbean.BloodRankList get(Integer key) {
		return _Tables_.getInstance().bloodranklist.get(key);
	}

	public static xbean.BloodRankList get(Integer key, xbean.BloodRankList value) {
		return _Tables_.getInstance().bloodranklist.get(key, value);
	}

	public static void insert(Integer key, xbean.BloodRankList value) {
		_Tables_.getInstance().bloodranklist.insert(key, value);
	}

	public static void delete(Integer key) {
		_Tables_.getInstance().bloodranklist.delete(key);
	}

	public static boolean add(Integer key, xbean.BloodRankList value) {
		return _Tables_.getInstance().bloodranklist.add(key, value);
	}

	public static boolean remove(Integer key) {
		return _Tables_.getInstance().bloodranklist.remove(key);
	}

	public static xdb.TTableCache<Integer, xbean.BloodRankList> getCache() {
		return _Tables_.getInstance().bloodranklist.getCache();
	}

	public static xdb.TTable<Integer, xbean.BloodRankList> getTable() {
		return _Tables_.getInstance().bloodranklist;
	}

	public static xbean.BloodRankList select(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.BloodRankList, xbean.BloodRankList>() {
			public xbean.BloodRankList get(xbean.BloodRankList v) { return v.toData(); }
		});
	}

	public static Integer selectCurweek(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.BloodRankList, Integer>() {
				public Integer get(xbean.BloodRankList v) { return v.getCurweek(); }
			});
	}

	public static java.util.List<xbean.BloodRankRole> selectRankers(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.BloodRankList, java.util.List<xbean.BloodRankRole>>() {
				public java.util.List<xbean.BloodRankRole> get(xbean.BloodRankList v) { return v.getRankersAsData(); }
			});
	}

}
