package xtable;

// typed table access point
public class Monthcardlist {
	Monthcardlist() {
	}

	public static xbean.monthcards get(Long key) {
		return _Tables_.getInstance().monthcardlist.get(key);
	}

	public static xbean.monthcards get(Long key, xbean.monthcards value) {
		return _Tables_.getInstance().monthcardlist.get(key, value);
	}

	public static void insert(Long key, xbean.monthcards value) {
		_Tables_.getInstance().monthcardlist.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().monthcardlist.delete(key);
	}

	public static boolean add(Long key, xbean.monthcards value) {
		return _Tables_.getInstance().monthcardlist.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().monthcardlist.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.monthcards> getCache() {
		return _Tables_.getInstance().monthcardlist.getCache();
	}

	public static xdb.TTable<Long, xbean.monthcards> getTable() {
		return _Tables_.getInstance().monthcardlist;
	}

	public static xbean.monthcards select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.monthcards, xbean.monthcards>() {
			public xbean.monthcards get(xbean.monthcards v) { return v.toData(); }
		});
	}

	public static java.util.Map<Integer, xbean.monthcard> selectRolemonthcards(Long key) {
		return getTable().select(key, new xdb.TField<xbean.monthcards, java.util.Map<Integer, xbean.monthcard>>() {
				public java.util.Map<Integer, xbean.monthcard> get(xbean.monthcards v) { return v.getRolemonthcardsAsData(); }
			});
	}

}
