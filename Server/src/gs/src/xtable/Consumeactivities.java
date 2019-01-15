package xtable;

// typed table access point
public class Consumeactivities {
	Consumeactivities() {
	}

	public static xbean.ConsumeActivityRole get(Long key) {
		return _Tables_.getInstance().consumeactivities.get(key);
	}

	public static xbean.ConsumeActivityRole get(Long key, xbean.ConsumeActivityRole value) {
		return _Tables_.getInstance().consumeactivities.get(key, value);
	}

	public static void insert(Long key, xbean.ConsumeActivityRole value) {
		_Tables_.getInstance().consumeactivities.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().consumeactivities.delete(key);
	}

	public static boolean add(Long key, xbean.ConsumeActivityRole value) {
		return _Tables_.getInstance().consumeactivities.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().consumeactivities.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.ConsumeActivityRole> getCache() {
		return _Tables_.getInstance().consumeactivities.getCache();
	}

	public static xdb.TTable<Long, xbean.ConsumeActivityRole> getTable() {
		return _Tables_.getInstance().consumeactivities;
	}

	public static xbean.ConsumeActivityRole select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.ConsumeActivityRole, xbean.ConsumeActivityRole>() {
			public xbean.ConsumeActivityRole get(xbean.ConsumeActivityRole v) { return v.toData(); }
		});
	}

	public static java.util.Map<Integer, xbean.ConsumeActivity> selectActivities(Long key) {
		return getTable().select(key, new xdb.TField<xbean.ConsumeActivityRole, java.util.Map<Integer, xbean.ConsumeActivity>>() {
				public java.util.Map<Integer, xbean.ConsumeActivity> get(xbean.ConsumeActivityRole v) { return v.getActivitiesAsData(); }
			});
	}

}
