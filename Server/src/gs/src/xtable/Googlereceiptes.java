package xtable;

// typed table access point
public class Googlereceiptes {
	Googlereceiptes() {
	}

	public static xbean.GoogleReceiptData get(Long key) {
		return _Tables_.getInstance().googlereceiptes.get(key);
	}

	public static xbean.GoogleReceiptData get(Long key, xbean.GoogleReceiptData value) {
		return _Tables_.getInstance().googlereceiptes.get(key, value);
	}

	public static void insert(Long key, xbean.GoogleReceiptData value) {
		_Tables_.getInstance().googlereceiptes.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().googlereceiptes.delete(key);
	}

	public static boolean add(Long key, xbean.GoogleReceiptData value) {
		return _Tables_.getInstance().googlereceiptes.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().googlereceiptes.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.GoogleReceiptData> getCache() {
		return _Tables_.getInstance().googlereceiptes.getCache();
	}

	public static xdb.TTable<Long, xbean.GoogleReceiptData> getTable() {
		return _Tables_.getInstance().googlereceiptes;
	}

	public static xbean.GoogleReceiptData select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.GoogleReceiptData, xbean.GoogleReceiptData>() {
			public xbean.GoogleReceiptData get(xbean.GoogleReceiptData v) { return v.toData(); }
		});
	}

	public static Long selectRoleid(Long key) {
		return getTable().select(key, new xdb.TField<xbean.GoogleReceiptData, Long>() {
				public Long get(xbean.GoogleReceiptData v) { return v.getRoleid(); }
			});
	}

	public static String selectPackagename(Long key) {
		return getTable().select(key, new xdb.TField<xbean.GoogleReceiptData, String>() {
				public String get(xbean.GoogleReceiptData v) { return v.getPackagename(); }
			});
	}

	public static String selectProductid(Long key) {
		return getTable().select(key, new xdb.TField<xbean.GoogleReceiptData, String>() {
				public String get(xbean.GoogleReceiptData v) { return v.getProductid(); }
			});
	}

	public static String selectToken(Long key) {
		return getTable().select(key, new xdb.TField<xbean.GoogleReceiptData, String>() {
				public String get(xbean.GoogleReceiptData v) { return v.getToken(); }
			});
	}

}
