package xtable;

// typed table access point
public class Bloodroles {
	Bloodroles() {
	}

	public static xbean.BloodRole get(Long key) {
		return _Tables_.getInstance().bloodroles.get(key);
	}

	public static xbean.BloodRole get(Long key, xbean.BloodRole value) {
		return _Tables_.getInstance().bloodroles.get(key, value);
	}

	public static void insert(Long key, xbean.BloodRole value) {
		_Tables_.getInstance().bloodroles.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().bloodroles.delete(key);
	}

	public static boolean add(Long key, xbean.BloodRole value) {
		return _Tables_.getInstance().bloodroles.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().bloodroles.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.BloodRole> getCache() {
		return _Tables_.getInstance().bloodroles.getCache();
	}

	public static xdb.TTable<Long, xbean.BloodRole> getTable() {
		return _Tables_.getInstance().bloodroles;
	}

	public static xbean.BloodRole select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BloodRole, xbean.BloodRole>() {
			public xbean.BloodRole get(xbean.BloodRole v) { return v.toData(); }
		});
	}

	public static Integer selectCurlevel(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BloodRole, Integer>() {
				public Integer get(xbean.BloodRole v) { return v.getCurlevel(); }
			});
	}

	public static Integer selectLasthard(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BloodRole, Integer>() {
				public Integer get(xbean.BloodRole v) { return v.getLasthard(); }
			});
	}

	public static Integer selectCurstar(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BloodRole, Integer>() {
				public Integer get(xbean.BloodRole v) { return v.getCurstar(); }
			});
	}

	public static Integer selectBattle1(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BloodRole, Integer>() {
				public Integer get(xbean.BloodRole v) { return v.getBattle1(); }
			});
	}

	public static Integer selectBattle2(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BloodRole, Integer>() {
				public Integer get(xbean.BloodRole v) { return v.getBattle2(); }
			});
	}

	public static Integer selectBattle3(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BloodRole, Integer>() {
				public Integer get(xbean.BloodRole v) { return v.getBattle3(); }
			});
	}

	public static Integer selectItemlevel(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BloodRole, Integer>() {
				public Integer get(xbean.BloodRole v) { return v.getItemlevel(); }
			});
	}

	public static java.util.Map<Integer, Float> selectEffects(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BloodRole, java.util.Map<Integer, Float>>() {
				public java.util.Map<Integer, Float> get(xbean.BloodRole v) { return v.getEffectsAsData(); }
			});
	}

	public static Integer selectFailed(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BloodRole, Integer>() {
				public Integer get(xbean.BloodRole v) { return v.getFailed(); }
			});
	}

	public static Integer selectRelivetimes(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BloodRole, Integer>() {
				public Integer get(xbean.BloodRole v) { return v.getRelivetimes(); }
			});
	}

	public static Long selectLastfighttime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BloodRole, Long>() {
				public Long get(xbean.BloodRole v) { return v.getLastfighttime(); }
			});
	}

	public static Integer selectTotalstar(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BloodRole, Integer>() {
				public Integer get(xbean.BloodRole v) { return v.getTotalstar(); }
			});
	}

	public static Integer selectMaxlevel(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BloodRole, Integer>() {
				public Integer get(xbean.BloodRole v) { return v.getMaxlevel(); }
			});
	}

	public static java.util.Map<Integer, Integer> selectRepeatstaraward(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BloodRole, java.util.Map<Integer, Integer>>() {
				public java.util.Map<Integer, Integer> get(xbean.BloodRole v) { return v.getRepeatstarawardAsData(); }
			});
	}

	public static java.util.Map<Integer, Integer> selectFixstaraward(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BloodRole, java.util.Map<Integer, Integer>>() {
				public java.util.Map<Integer, Integer> get(xbean.BloodRole v) { return v.getFixstarawardAsData(); }
			});
	}

}
