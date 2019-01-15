package xtable;

// typed table access point
public class Chargeactivities {
	Chargeactivities() {
	}

	public static xbean.ChargeActivityRole get(Long key) {
		return _Tables_.getInstance().chargeactivities.get(key);
	}

	public static xbean.ChargeActivityRole get(Long key, xbean.ChargeActivityRole value) {
		return _Tables_.getInstance().chargeactivities.get(key, value);
	}

	public static void insert(Long key, xbean.ChargeActivityRole value) {
		_Tables_.getInstance().chargeactivities.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().chargeactivities.delete(key);
	}

	public static boolean add(Long key, xbean.ChargeActivityRole value) {
		return _Tables_.getInstance().chargeactivities.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().chargeactivities.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.ChargeActivityRole> getCache() {
		return _Tables_.getInstance().chargeactivities.getCache();
	}

	public static xdb.TTable<Long, xbean.ChargeActivityRole> getTable() {
		return _Tables_.getInstance().chargeactivities;
	}

	public static xbean.ChargeActivityRole select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.ChargeActivityRole, xbean.ChargeActivityRole>() {
			public xbean.ChargeActivityRole get(xbean.ChargeActivityRole v) { return v.toData(); }
		});
	}

	public static java.util.Map<Integer, xbean.ChargeActivity> selectActivities(Long key) {
		return getTable().select(key, new xdb.TField<xbean.ChargeActivityRole, java.util.Map<Integer, xbean.ChargeActivity>>() {
				public java.util.Map<Integer, xbean.ChargeActivity> get(xbean.ChargeActivityRole v) { return v.getActivitiesAsData(); }
			});
	}

}
