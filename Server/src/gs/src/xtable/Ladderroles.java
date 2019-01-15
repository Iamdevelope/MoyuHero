package xtable;

// typed table access point
public class Ladderroles {
	Ladderroles() {
	}

	public static xbean.LadderRole get(Long key) {
		return _Tables_.getInstance().ladderroles.get(key);
	}

	public static xbean.LadderRole get(Long key, xbean.LadderRole value) {
		return _Tables_.getInstance().ladderroles.get(key, value);
	}

	public static void insert(Long key, xbean.LadderRole value) {
		_Tables_.getInstance().ladderroles.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().ladderroles.delete(key);
	}

	public static boolean add(Long key, xbean.LadderRole value) {
		return _Tables_.getInstance().ladderroles.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().ladderroles.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.LadderRole> getCache() {
		return _Tables_.getInstance().ladderroles.getCache();
	}

	public static xdb.TTable<Long, xbean.LadderRole> getTable() {
		return _Tables_.getInstance().ladderroles;
	}

	public static xbean.LadderRole select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.LadderRole, xbean.LadderRole>() {
			public xbean.LadderRole get(xbean.LadderRole v) { return v.toData(); }
		});
	}

	public static Integer selectLadderrank(Long key) {
		return getTable().select(key, new xdb.TField<xbean.LadderRole, Integer>() {
				public Integer get(xbean.LadderRole v) { return v.getLadderrank(); }
			});
	}

	public static Integer selectLaddersoul(Long key) {
		return getTable().select(key, new xdb.TField<xbean.LadderRole, Integer>() {
				public Integer get(xbean.LadderRole v) { return v.getLaddersoul(); }
			});
	}

	public static Long selectLastsoulchangetime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.LadderRole, Long>() {
				public Long get(xbean.LadderRole v) { return v.getLastsoulchangetime(); }
			});
	}

	public static java.util.List<Long> selectEnermies(Long key) {
		return getTable().select(key, new xdb.TField<xbean.LadderRole, java.util.List<Long>>() {
				public java.util.List<Long> get(xbean.LadderRole v) { return v.getEnermiesAsData(); }
			});
	}

	public static Integer selectFighttimes(Long key) {
		return getTable().select(key, new xdb.TField<xbean.LadderRole, Integer>() {
				public Integer get(xbean.LadderRole v) { return v.getFighttimes(); }
			});
	}

	public static Long selectLastfighttime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.LadderRole, Long>() {
				public Long get(xbean.LadderRole v) { return v.getLastfighttime(); }
			});
	}

}
