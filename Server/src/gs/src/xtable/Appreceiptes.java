package xtable;

// typed table access point
public class Appreceiptes {
	Appreceiptes() {
	}

	public static xbean.AppReceiptData get(Long key) {
		return _Tables_.getInstance().appreceiptes.get(key);
	}

	public static xbean.AppReceiptData get(Long key, xbean.AppReceiptData value) {
		return _Tables_.getInstance().appreceiptes.get(key, value);
	}

	public static void insert(Long key, xbean.AppReceiptData value) {
		_Tables_.getInstance().appreceiptes.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().appreceiptes.delete(key);
	}

	public static boolean add(Long key, xbean.AppReceiptData value) {
		return _Tables_.getInstance().appreceiptes.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().appreceiptes.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.AppReceiptData> getCache() {
		return _Tables_.getInstance().appreceiptes.getCache();
	}

	public static xdb.TTable<Long, xbean.AppReceiptData> getTable() {
		return _Tables_.getInstance().appreceiptes;
	}

	public static xbean.AppReceiptData select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.AppReceiptData, xbean.AppReceiptData>() {
			public xbean.AppReceiptData get(xbean.AppReceiptData v) { return v.toData(); }
		});
	}

	public static Long selectRoleid(Long key) {
		return getTable().select(key, new xdb.TField<xbean.AppReceiptData, Long>() {
				public Long get(xbean.AppReceiptData v) { return v.getRoleid(); }
			});
	}

	public static String selectReceipt(Long key) {
		return getTable().select(key, new xdb.TField<xbean.AppReceiptData, String>() {
				public String get(xbean.AppReceiptData v) { return v.getReceipt(); }
			});
	}

}
