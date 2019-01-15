package xtable;

// typed table access point
public class Herocolumns {
	Herocolumns() {
	}

	public static xbean.HeroColumn get(Long key) {
		return _Tables_.getInstance().herocolumns.get(key);
	}

	public static xbean.HeroColumn get(Long key, xbean.HeroColumn value) {
		return _Tables_.getInstance().herocolumns.get(key, value);
	}

	public static void insert(Long key, xbean.HeroColumn value) {
		_Tables_.getInstance().herocolumns.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().herocolumns.delete(key);
	}

	public static boolean add(Long key, xbean.HeroColumn value) {
		return _Tables_.getInstance().herocolumns.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().herocolumns.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.HeroColumn> getCache() {
		return _Tables_.getInstance().herocolumns.getCache();
	}

	public static xdb.TTable<Long, xbean.HeroColumn> getTable() {
		return _Tables_.getInstance().herocolumns;
	}

	public static xbean.HeroColumn select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.HeroColumn, xbean.HeroColumn>() {
			public xbean.HeroColumn get(xbean.HeroColumn v) { return v.toData(); }
		});
	}

	public static java.util.List<xbean.Hero> selectHeroes(Long key) {
		return getTable().select(key, new xdb.TField<xbean.HeroColumn, java.util.List<xbean.Hero>>() {
				public java.util.List<xbean.Hero> get(xbean.HeroColumn v) { return v.getHeroesAsData(); }
			});
	}

	public static Integer selectNextkey(Long key) {
		return getTable().select(key, new xdb.TField<xbean.HeroColumn, Integer>() {
				public Integer get(xbean.HeroColumn v) { return v.getNextkey(); }
			});
	}

}
