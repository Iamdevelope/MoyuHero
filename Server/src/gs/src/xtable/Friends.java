package xtable;

// typed table access point
public class Friends {
	Friends() {
	}

	public static xbean.Friends get(Long key) {
		return _Tables_.getInstance().friends.get(key);
	}

	public static xbean.Friends get(Long key, xbean.Friends value) {
		return _Tables_.getInstance().friends.get(key, value);
	}

	public static void insert(Long key, xbean.Friends value) {
		_Tables_.getInstance().friends.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().friends.delete(key);
	}

	public static boolean add(Long key, xbean.Friends value) {
		return _Tables_.getInstance().friends.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().friends.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.Friends> getCache() {
		return _Tables_.getInstance().friends.getCache();
	}

	public static xdb.TTable<Long, xbean.Friends> getTable() {
		return _Tables_.getInstance().friends;
	}

	public static xbean.Friends select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Friends, xbean.Friends>() {
			public xbean.Friends get(xbean.Friends v) { return v.toData(); }
		});
	}

	public static java.util.Map<Long, xbean.FriendInfo> selectMine(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Friends, java.util.Map<Long, xbean.FriendInfo>>() {
				public java.util.Map<Long, xbean.FriendInfo> get(xbean.Friends v) { return v.getMineAsData(); }
			});
	}

}
