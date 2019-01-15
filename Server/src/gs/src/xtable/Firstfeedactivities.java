package xtable;

// typed table access point
public class Firstfeedactivities {
	Firstfeedactivities() {
	}

	public static xbean.FirstFeedActivityRole get(Long key) {
		return _Tables_.getInstance().firstfeedactivities.get(key);
	}

	public static xbean.FirstFeedActivityRole get(Long key, xbean.FirstFeedActivityRole value) {
		return _Tables_.getInstance().firstfeedactivities.get(key, value);
	}

	public static void insert(Long key, xbean.FirstFeedActivityRole value) {
		_Tables_.getInstance().firstfeedactivities.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().firstfeedactivities.delete(key);
	}

	public static boolean add(Long key, xbean.FirstFeedActivityRole value) {
		return _Tables_.getInstance().firstfeedactivities.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().firstfeedactivities.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.FirstFeedActivityRole> getCache() {
		return _Tables_.getInstance().firstfeedactivities.getCache();
	}

	public static xdb.TTable<Long, xbean.FirstFeedActivityRole> getTable() {
		return _Tables_.getInstance().firstfeedactivities;
	}

	public static xbean.FirstFeedActivityRole select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.FirstFeedActivityRole, xbean.FirstFeedActivityRole>() {
			public xbean.FirstFeedActivityRole get(xbean.FirstFeedActivityRole v) { return v.toData(); }
		});
	}

	public static java.util.Map<Integer, xbean.FirstFeedActivity> selectActivities(Long key) {
		return getTable().select(key, new xdb.TField<xbean.FirstFeedActivityRole, java.util.Map<Integer, xbean.FirstFeedActivity>>() {
				public java.util.Map<Integer, xbean.FirstFeedActivity> get(xbean.FirstFeedActivityRole v) { return v.getActivitiesAsData(); }
			});
	}

}
