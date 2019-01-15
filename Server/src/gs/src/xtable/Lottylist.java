package xtable;

// typed table access point
public class Lottylist {
	Lottylist() {
	}

	public static xbean.lotty get(Long key) {
		return _Tables_.getInstance().lottylist.get(key);
	}

	public static xbean.lotty get(Long key, xbean.lotty value) {
		return _Tables_.getInstance().lottylist.get(key, value);
	}

	public static void insert(Long key, xbean.lotty value) {
		_Tables_.getInstance().lottylist.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().lottylist.delete(key);
	}

	public static boolean add(Long key, xbean.lotty value) {
		return _Tables_.getInstance().lottylist.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().lottylist.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.lotty> getCache() {
		return _Tables_.getInstance().lottylist.getCache();
	}

	public static xdb.TTable<Long, xbean.lotty> getTable() {
		return _Tables_.getInstance().lottylist;
	}

	public static xbean.lotty select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.lotty, xbean.lotty>() {
			public xbean.lotty get(xbean.lotty v) { return v.toData(); }
		});
	}

	public static Integer selectNormalrecruitnum(Long key) {
		return getTable().select(key, new xdb.TField<xbean.lotty, Integer>() {
				public Integer get(xbean.lotty v) { return v.getNormalrecruitnum(); }
			});
	}

	public static Long selectNormalrecruittime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.lotty, Long>() {
				public Long get(xbean.lotty v) { return v.getNormalrecruittime(); }
			});
	}

	public static Integer selectToprecruitnum(Long key) {
		return getTable().select(key, new xdb.TField<xbean.lotty, Integer>() {
				public Integer get(xbean.lotty v) { return v.getToprecruitnum(); }
			});
	}

	public static Long selectToprecruittime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.lotty, Long>() {
				public Long get(xbean.lotty v) { return v.getToprecruittime(); }
			});
	}

	public static Integer selectToprecruitheronum(Long key) {
		return getTable().select(key, new xdb.TField<xbean.lotty, Integer>() {
				public Integer get(xbean.lotty v) { return v.getToprecruitheronum(); }
			});
	}

	public static Integer selectToptentime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.lotty, Integer>() {
				public Integer get(xbean.lotty v) { return v.getToptentime(); }
			});
	}

	public static Long selectFreetime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.lotty, Long>() {
				public Long get(xbean.lotty v) { return v.getFreetime(); }
			});
	}

	public static Integer selectFirstget(Long key) {
		return getTable().select(key, new xdb.TField<xbean.lotty, Integer>() {
				public Integer get(xbean.lotty v) { return v.getFirstget(); }
			});
	}

	public static Integer selectDreamexp(Long key) {
		return getTable().select(key, new xdb.TField<xbean.lotty, Integer>() {
				public Integer get(xbean.lotty v) { return v.getDreamexp(); }
			});
	}

	public static Integer selectDreamfree(Long key) {
		return getTable().select(key, new xdb.TField<xbean.lotty, Integer>() {
				public Integer get(xbean.lotty v) { return v.getDreamfree(); }
			});
	}

	public static Integer selectDream(Long key) {
		return getTable().select(key, new xdb.TField<xbean.lotty, Integer>() {
				public Integer get(xbean.lotty v) { return v.getDream(); }
			});
	}

	public static java.util.Map<Integer, Integer> selectSinglelotty(Long key) {
		return getTable().select(key, new xdb.TField<xbean.lotty, java.util.Map<Integer, Integer>>() {
				public java.util.Map<Integer, Integer> get(xbean.lotty v) { return v.getSinglelottyAsData(); }
			});
	}

	public static java.util.Map<Integer, Integer> selectTenlotty(Long key) {
		return getTable().select(key, new xdb.TField<xbean.lotty, java.util.Map<Integer, Integer>>() {
				public java.util.Map<Integer, Integer> get(xbean.lotty v) { return v.getTenlottyAsData(); }
			});
	}

	public static java.util.Map<Integer, Integer> selectTensinglelotty(Long key) {
		return getTable().select(key, new xdb.TField<xbean.lotty, java.util.Map<Integer, Integer>>() {
				public java.util.Map<Integer, Integer> get(xbean.lotty v) { return v.getTensinglelottyAsData(); }
			});
	}

	public static java.util.Map<Integer, Integer> selectGetherolotty(Long key) {
		return getTable().select(key, new xdb.TField<xbean.lotty, java.util.Map<Integer, Integer>>() {
				public java.util.Map<Integer, Integer> get(xbean.lotty v) { return v.getGetherolottyAsData(); }
			});
	}

}
