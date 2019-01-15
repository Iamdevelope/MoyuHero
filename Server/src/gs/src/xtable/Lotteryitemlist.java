package xtable;

// typed table access point
public class Lotteryitemlist {
	Lotteryitemlist() {
	}

	public static xbean.LotteryItemAll get(Long key) {
		return _Tables_.getInstance().lotteryitemlist.get(key);
	}

	public static xbean.LotteryItemAll get(Long key, xbean.LotteryItemAll value) {
		return _Tables_.getInstance().lotteryitemlist.get(key, value);
	}

	public static void insert(Long key, xbean.LotteryItemAll value) {
		_Tables_.getInstance().lotteryitemlist.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().lotteryitemlist.delete(key);
	}

	public static boolean add(Long key, xbean.LotteryItemAll value) {
		return _Tables_.getInstance().lotteryitemlist.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().lotteryitemlist.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.LotteryItemAll> getCache() {
		return _Tables_.getInstance().lotteryitemlist.getCache();
	}

	public static xdb.TTable<Long, xbean.LotteryItemAll> getTable() {
		return _Tables_.getInstance().lotteryitemlist;
	}

	public static xbean.LotteryItemAll select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.LotteryItemAll, xbean.LotteryItemAll>() {
			public xbean.LotteryItemAll get(xbean.LotteryItemAll v) { return v.toData(); }
		});
	}

	public static Integer selectMapkey(Long key) {
		return getTable().select(key, new xdb.TField<xbean.LotteryItemAll, Integer>() {
				public Integer get(xbean.LotteryItemAll v) { return v.getMapkey(); }
			});
	}

	public static Integer selectMapvalue(Long key) {
		return getTable().select(key, new xdb.TField<xbean.LotteryItemAll, Integer>() {
				public Integer get(xbean.LotteryItemAll v) { return v.getMapvalue(); }
			});
	}

	public static java.util.List<Integer> selectSuperlist(Long key) {
		return getTable().select(key, new xdb.TField<xbean.LotteryItemAll, java.util.List<Integer>>() {
				public java.util.List<Integer> get(xbean.LotteryItemAll v) { return v.getSuperlistAsData(); }
			});
	}

	public static Long selectMonthfirsttime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.LotteryItemAll, Long>() {
				public Long get(xbean.LotteryItemAll v) { return v.getMonthfirsttime(); }
			});
	}

	public static Long selectFreelotterytime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.LotteryItemAll, Long>() {
				public Long get(xbean.LotteryItemAll v) { return v.getFreelotterytime(); }
			});
	}

	public static Long selectLastrefreshtime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.LotteryItemAll, Long>() {
				public Long get(xbean.LotteryItemAll v) { return v.getLastrefreshtime(); }
			});
	}

	public static java.util.Map<Integer, xbean.LotteryItemlayer> selectLotteryitemmap(Long key) {
		return getTable().select(key, new xdb.TField<xbean.LotteryItemAll, java.util.Map<Integer, xbean.LotteryItemlayer>>() {
				public java.util.Map<Integer, xbean.LotteryItemlayer> get(xbean.LotteryItemAll v) { return v.getLotteryitemmapAsData(); }
			});
	}

}
