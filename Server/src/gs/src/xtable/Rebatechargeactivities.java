package xtable;

// typed table access point
public class Rebatechargeactivities {
	Rebatechargeactivities() {
	}

	public static xbean.RebateChargeActivityRole get(Long key) {
		return _Tables_.getInstance().rebatechargeactivities.get(key);
	}

	public static xbean.RebateChargeActivityRole get(Long key, xbean.RebateChargeActivityRole value) {
		return _Tables_.getInstance().rebatechargeactivities.get(key, value);
	}

	public static void insert(Long key, xbean.RebateChargeActivityRole value) {
		_Tables_.getInstance().rebatechargeactivities.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().rebatechargeactivities.delete(key);
	}

	public static boolean add(Long key, xbean.RebateChargeActivityRole value) {
		return _Tables_.getInstance().rebatechargeactivities.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().rebatechargeactivities.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.RebateChargeActivityRole> getCache() {
		return _Tables_.getInstance().rebatechargeactivities.getCache();
	}

	public static xdb.TTable<Long, xbean.RebateChargeActivityRole> getTable() {
		return _Tables_.getInstance().rebatechargeactivities;
	}

	public static xbean.RebateChargeActivityRole select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.RebateChargeActivityRole, xbean.RebateChargeActivityRole>() {
			public xbean.RebateChargeActivityRole get(xbean.RebateChargeActivityRole v) { return v.toData(); }
		});
	}

	public static java.util.Map<Integer, xbean.RebateChargeActivity> selectActivities(Long key) {
		return getTable().select(key, new xdb.TField<xbean.RebateChargeActivityRole, java.util.Map<Integer, xbean.RebateChargeActivity>>() {
				public java.util.Map<Integer, xbean.RebateChargeActivity> get(xbean.RebateChargeActivityRole v) { return v.getActivitiesAsData(); }
			});
	}

}
