package xtable;

// typed table access point
public class Stageroles {
	Stageroles() {
	}

	public static xbean.StageRole get(Long key) {
		return _Tables_.getInstance().stageroles.get(key);
	}

	public static xbean.StageRole get(Long key, xbean.StageRole value) {
		return _Tables_.getInstance().stageroles.get(key, value);
	}

	public static void insert(Long key, xbean.StageRole value) {
		_Tables_.getInstance().stageroles.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().stageroles.delete(key);
	}

	public static boolean add(Long key, xbean.StageRole value) {
		return _Tables_.getInstance().stageroles.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().stageroles.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.StageRole> getCache() {
		return _Tables_.getInstance().stageroles.getCache();
	}

	public static xdb.TTable<Long, xbean.StageRole> getTable() {
		return _Tables_.getInstance().stageroles;
	}

	public static xbean.StageRole select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.StageRole, xbean.StageRole>() {
			public xbean.StageRole get(xbean.StageRole v) { return v.toData(); }
		});
	}

	public static java.util.Map<Integer, xbean.StageInfo> selectStages(Long key) {
		return getTable().select(key, new xdb.TField<xbean.StageRole, java.util.Map<Integer, xbean.StageInfo>>() {
				public java.util.Map<Integer, xbean.StageInfo> get(xbean.StageRole v) { return v.getStagesAsData(); }
			});
	}

}
