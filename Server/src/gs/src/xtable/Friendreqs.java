package xtable;

// typed table access point
public class Friendreqs {
	Friendreqs() {
	}

	public static xbean.FriendReqs get(Long key) {
		return _Tables_.getInstance().friendreqs.get(key);
	}

	public static xbean.FriendReqs get(Long key, xbean.FriendReqs value) {
		return _Tables_.getInstance().friendreqs.get(key, value);
	}

	public static void insert(Long key, xbean.FriendReqs value) {
		_Tables_.getInstance().friendreqs.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().friendreqs.delete(key);
	}

	public static boolean add(Long key, xbean.FriendReqs value) {
		return _Tables_.getInstance().friendreqs.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().friendreqs.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.FriendReqs> getCache() {
		return _Tables_.getInstance().friendreqs.getCache();
	}

	public static xdb.TTable<Long, xbean.FriendReqs> getTable() {
		return _Tables_.getInstance().friendreqs;
	}

	public static xbean.FriendReqs select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.FriendReqs, xbean.FriendReqs>() {
			public xbean.FriendReqs get(xbean.FriendReqs v) { return v.toData(); }
		});
	}

	public static java.util.Set<Long> selectByme(Long key) {
		return getTable().select(key, new xdb.TField<xbean.FriendReqs, java.util.Set<Long>>() {
				public java.util.Set<Long> get(xbean.FriendReqs v) { return v.getBymeAsData(); }
			});
	}

	public static java.util.Set<Long> selectImby(Long key) {
		return getTable().select(key, new xdb.TField<xbean.FriendReqs, java.util.Set<Long>>() {
				public java.util.Set<Long> get(xbean.FriendReqs v) { return v.getImbyAsData(); }
			});
	}

}
