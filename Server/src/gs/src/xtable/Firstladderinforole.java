package xtable;

// typed table access point
public class Firstladderinforole {
	Firstladderinforole() {
	}

	public static xbean.FirstLadderInfoRole get(Integer key) {
		return _Tables_.getInstance().firstladderinforole.get(key);
	}

	public static xbean.FirstLadderInfoRole get(Integer key, xbean.FirstLadderInfoRole value) {
		return _Tables_.getInstance().firstladderinforole.get(key, value);
	}

	public static void insert(Integer key, xbean.FirstLadderInfoRole value) {
		_Tables_.getInstance().firstladderinforole.insert(key, value);
	}

	public static void delete(Integer key) {
		_Tables_.getInstance().firstladderinforole.delete(key);
	}

	public static boolean add(Integer key, xbean.FirstLadderInfoRole value) {
		return _Tables_.getInstance().firstladderinforole.add(key, value);
	}

	public static boolean remove(Integer key) {
		return _Tables_.getInstance().firstladderinforole.remove(key);
	}

	public static xdb.TTableCache<Integer, xbean.FirstLadderInfoRole> getCache() {
		return _Tables_.getInstance().firstladderinforole.getCache();
	}

	public static xdb.TTable<Integer, xbean.FirstLadderInfoRole> getTable() {
		return _Tables_.getInstance().firstladderinforole;
	}

	public static xbean.FirstLadderInfoRole select(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.FirstLadderInfoRole, xbean.FirstLadderInfoRole>() {
			public xbean.FirstLadderInfoRole get(xbean.FirstLadderInfoRole v) { return v.toData(); }
		});
	}

	public static java.util.Map<Long, xbean.FirstLadderInfo> selectRoleinfos(Integer key) {
		return getTable().select(key, new xdb.TField<xbean.FirstLadderInfoRole, java.util.Map<Long, xbean.FirstLadderInfo>>() {
				public java.util.Map<Long, xbean.FirstLadderInfo> get(xbean.FirstLadderInfoRole v) { return v.getRoleinfosAsData(); }
			});
	}

}
