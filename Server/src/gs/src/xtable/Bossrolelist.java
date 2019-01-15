package xtable;

// typed table access point
public class Bossrolelist {
	Bossrolelist() {
	}

	public static xbean.bossrole get(Long key) {
		return _Tables_.getInstance().bossrolelist.get(key);
	}

	public static xbean.bossrole get(Long key, xbean.bossrole value) {
		return _Tables_.getInstance().bossrolelist.get(key, value);
	}

	public static void insert(Long key, xbean.bossrole value) {
		_Tables_.getInstance().bossrolelist.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().bossrolelist.delete(key);
	}

	public static boolean add(Long key, xbean.bossrole value) {
		return _Tables_.getInstance().bossrolelist.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().bossrolelist.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.bossrole> getCache() {
		return _Tables_.getInstance().bossrolelist.getCache();
	}

	public static xdb.TTable<Long, xbean.bossrole> getTable() {
		return _Tables_.getInstance().bossrolelist;
	}

	public static xbean.bossrole select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.bossrole, xbean.bossrole>() {
			public xbean.bossrole get(xbean.bossrole v) { return v.toData(); }
		});
	}

	public static Long selectKillhpall(Long key) {
		return getTable().select(key, new xdb.TField<xbean.bossrole, Long>() {
				public Long get(xbean.bossrole v) { return v.getKillhpall(); }
			});
	}

	public static Integer selectKillboss(Long key) {
		return getTable().select(key, new xdb.TField<xbean.bossrole, Integer>() {
				public Integer get(xbean.bossrole v) { return v.getKillboss(); }
			});
	}

	public static Long selectBossnowhp(Long key) {
		return getTable().select(key, new xdb.TField<xbean.bossrole, Long>() {
				public Long get(xbean.bossrole v) { return v.getBossnowhp(); }
			});
	}

	public static Long selectTime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.bossrole, Long>() {
				public Long get(xbean.bossrole v) { return v.getTime(); }
			});
	}

	public static Integer selectZhufunum(Long key) {
		return getTable().select(key, new xdb.TField<xbean.bossrole, Integer>() {
				public Integer get(xbean.bossrole v) { return v.getZhufunum(); }
			});
	}

	public static Integer selectShouwangzl(Long key) {
		return getTable().select(key, new xdb.TField<xbean.bossrole, Integer>() {
				public Integer get(xbean.bossrole v) { return v.getShouwangzl(); }
			});
	}

	public static Integer selectChuanshuozs(Long key) {
		return getTable().select(key, new xdb.TField<xbean.bossrole, Integer>() {
				public Integer get(xbean.bossrole v) { return v.getChuanshuozs(); }
			});
	}

	public static xbean.bossshop selectBshop(Long key) {
		return getTable().select(key, new xdb.TField<xbean.bossrole, xbean.bossshop>() {
				public xbean.bossshop get(xbean.bossrole v) { return v.getBshop(); }
			});
	}

}
