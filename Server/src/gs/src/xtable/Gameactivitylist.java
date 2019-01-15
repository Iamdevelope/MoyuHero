package xtable;

// typed table access point
public class Gameactivitylist {
	Gameactivitylist() {
	}

	public static xbean.gameactivitys get(Long key) {
		return _Tables_.getInstance().gameactivitylist.get(key);
	}

	public static xbean.gameactivitys get(Long key, xbean.gameactivitys value) {
		return _Tables_.getInstance().gameactivitylist.get(key, value);
	}

	public static void insert(Long key, xbean.gameactivitys value) {
		_Tables_.getInstance().gameactivitylist.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().gameactivitylist.delete(key);
	}

	public static boolean add(Long key, xbean.gameactivitys value) {
		return _Tables_.getInstance().gameactivitylist.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().gameactivitylist.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.gameactivitys> getCache() {
		return _Tables_.getInstance().gameactivitylist.getCache();
	}

	public static xdb.TTable<Long, xbean.gameactivitys> getTable() {
		return _Tables_.getInstance().gameactivitylist;
	}

	public static xbean.gameactivitys select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.gameactivitys, xbean.gameactivitys>() {
			public xbean.gameactivitys get(xbean.gameactivitys v) { return v.toData(); }
		});
	}

	public static java.util.Map<Integer, xbean.gameactivity> selectGameactivitymap(Long key) {
		return getTable().select(key, new xdb.TField<xbean.gameactivitys, java.util.Map<Integer, xbean.gameactivity>>() {
				public java.util.Map<Integer, xbean.gameactivity> get(xbean.gameactivitys v) { return v.getGameactivitymapAsData(); }
			});
	}

}
