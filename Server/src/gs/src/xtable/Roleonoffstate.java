package xtable;

// typed table access point
public class Roleonoffstate {
	Roleonoffstate() {
	}

	public static Integer get(Long key) {
		return _Tables_.getInstance().roleonoffstate.get(key);
	}

	public static Integer get(Long key, Integer value) {
		return _Tables_.getInstance().roleonoffstate.get(key, value);
	}

	public static void insert(Long key, Integer value) {
		_Tables_.getInstance().roleonoffstate.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().roleonoffstate.delete(key);
	}

	public static boolean add(Long key, Integer value) {
		return _Tables_.getInstance().roleonoffstate.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().roleonoffstate.remove(key);
	}

	public static xdb.TTableCache<Long, Integer> getCache() {
		return _Tables_.getInstance().roleonoffstate.getCache();
	}

	public static xdb.TTable<Long, Integer> getTable() {
		return _Tables_.getInstance().roleonoffstate;
	}

	public static Integer select(Long key) {
		return getTable().select(key, new xdb.TField<Integer, Integer>() {
			public Integer get(Integer v) { return v; }
		});
	}

}
