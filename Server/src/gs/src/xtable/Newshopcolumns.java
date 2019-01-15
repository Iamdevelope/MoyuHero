package xtable;

// typed table access point
public class Newshopcolumns {
	Newshopcolumns() {
	}

	public static xbean.NewShopMap get(Long key) {
		return _Tables_.getInstance().newshopcolumns.get(key);
	}

	public static xbean.NewShopMap get(Long key, xbean.NewShopMap value) {
		return _Tables_.getInstance().newshopcolumns.get(key, value);
	}

	public static void insert(Long key, xbean.NewShopMap value) {
		_Tables_.getInstance().newshopcolumns.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().newshopcolumns.delete(key);
	}

	public static boolean add(Long key, xbean.NewShopMap value) {
		return _Tables_.getInstance().newshopcolumns.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().newshopcolumns.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.NewShopMap> getCache() {
		return _Tables_.getInstance().newshopcolumns.getCache();
	}

	public static xdb.TTable<Long, xbean.NewShopMap> getTable() {
		return _Tables_.getInstance().newshopcolumns;
	}

	public static xbean.NewShopMap select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.NewShopMap, xbean.NewShopMap>() {
			public xbean.NewShopMap get(xbean.NewShopMap v) { return v.toData(); }
		});
	}

	public static java.util.Map<Integer, xbean.NewShopList> selectShopmap(Long key) {
		return getTable().select(key, new xdb.TField<xbean.NewShopMap, java.util.Map<Integer, xbean.NewShopList>>() {
				public java.util.Map<Integer, xbean.NewShopList> get(xbean.NewShopMap v) { return v.getShopmapAsData(); }
			});
	}

}
