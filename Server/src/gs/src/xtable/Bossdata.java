package xtable;

// typed table access point
public class Bossdata {
	Bossdata() {
	}

	public static xbean.boss get(Integer key) {
		return _Tables_.getInstance().bossdata.get(key);
	}

	public static xbean.boss get(Integer key, xbean.boss value) {
		return _Tables_.getInstance().bossdata.get(key, value);
	}

	public static void insert(Integer key, xbean.boss value) {
		_Tables_.getInstance().bossdata.insert(key, value);
	}

	public static void delete(Integer key) {
		_Tables_.getInstance().bossdata.delete(key);
	}

	public static boolean add(Integer key, xbean.boss value) {
		return _Tables_.getInstance().bossdata.add(key, value);
	}

	public static boolean remove(Integer key) {
		return _Tables_.getInstance().bossdata.remove(key);
	}

	public static xdb.TTableCache<Integer, xbean.boss> getCache() {
		return _Tables_.getInstance().bossdata.getCache();
	}

	public static xdb.TTable<Integer, xbean.boss> getTable() {
		return _Tables_.getInstance().bossdata;
	}

	public static xbean.boss select(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.boss, xbean.boss>() {
			public xbean.boss get(xbean.boss v) { return v.toData(); }
		});
	}

	public static Long selectLasthpall(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.boss, Long>() {
				public Long get(xbean.boss v) { return v.getLasthpall(); }
			});
	}

	public static Integer selectLastiskill(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.boss, Integer>() {
				public Integer get(xbean.boss v) { return v.getLastiskill(); }
			});
	}

	public static Long selectLastkillnum(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.boss, Long>() {
				public Long get(xbean.boss v) { return v.getLastkillnum(); }
			});
	}

	public static Long selectNewhpall(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.boss, Long>() {
				public Long get(xbean.boss v) { return v.getNewhpall(); }
			});
	}

	public static Long selectNowhp(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.boss, Long>() {
				public Long get(xbean.boss v) { return v.getNowhp(); }
			});
	}

	public static Integer selectBossid1(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.boss, Integer>() {
				public Integer get(xbean.boss v) { return v.getBossid1(); }
			});
	}

	public static Integer selectBossid2(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.boss, Integer>() {
				public Integer get(xbean.boss v) { return v.getBossid2(); }
			});
	}

	public static Integer selectBossid3(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.boss, Integer>() {
				public Integer get(xbean.boss v) { return v.getBossid3(); }
			});
	}

	public static Integer selectBossid4(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.boss, Integer>() {
				public Integer get(xbean.boss v) { return v.getBossid4(); }
			});
	}

	public static Integer selectBossiskill(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.boss, Integer>() {
				public Integer get(xbean.boss v) { return v.getBossiskill(); }
			});
	}

	public static String selectBoss1killname(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.boss, String>() {
				public String get(xbean.boss v) { return v.getBoss1killname(); }
			});
	}

	public static String selectBoss2killname(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.boss, String>() {
				public String get(xbean.boss v) { return v.getBoss2killname(); }
			});
	}

	public static Long selectTime(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.boss, Long>() {
				public Long get(xbean.boss v) { return v.getTime(); }
			});
	}

}
