package xtable;

// typed table access point
public class Tujianheros {
	Tujianheros() {
	}

	public static xbean.TuJianHeros get(Long key) {
		return _Tables_.getInstance().tujianheros.get(key);
	}

	public static xbean.TuJianHeros get(Long key, xbean.TuJianHeros value) {
		return _Tables_.getInstance().tujianheros.get(key, value);
	}

	public static void insert(Long key, xbean.TuJianHeros value) {
		_Tables_.getInstance().tujianheros.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().tujianheros.delete(key);
	}

	public static boolean add(Long key, xbean.TuJianHeros value) {
		return _Tables_.getInstance().tujianheros.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().tujianheros.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.TuJianHeros> getCache() {
		return _Tables_.getInstance().tujianheros.getCache();
	}

	public static xdb.TTable<Long, xbean.TuJianHeros> getTable() {
		return _Tables_.getInstance().tujianheros;
	}

	public static xbean.TuJianHeros select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.TuJianHeros, xbean.TuJianHeros>() {
			public xbean.TuJianHeros get(xbean.TuJianHeros v) { return v.toData(); }
		});
	}

	public static java.util.Map<Integer, Integer> selectTujianbox(Long key) {
		return getTable().select(key, new xdb.TField<xbean.TuJianHeros, java.util.Map<Integer, Integer>>() {
				public java.util.Map<Integer, Integer> get(xbean.TuJianHeros v) { return v.getTujianboxAsData(); }
			});
	}

	public static java.util.Map<Integer, xbean.TuJianHero> selectTujianhero(Long key) {
		return getTable().select(key, new xdb.TField<xbean.TuJianHeros, java.util.Map<Integer, xbean.TuJianHero>>() {
				public java.util.Map<Integer, xbean.TuJianHero> get(xbean.TuJianHeros v) { return v.getTujianheroAsData(); }
			});
	}

	public static java.util.List<Integer> selectTjheromaxlevel(Long key) {
		return getTable().select(key, new xdb.TField<xbean.TuJianHeros, java.util.List<Integer>>() {
				public java.util.List<Integer> get(xbean.TuJianHeros v) { return v.getTjheromaxlevelAsData(); }
			});
	}

}
