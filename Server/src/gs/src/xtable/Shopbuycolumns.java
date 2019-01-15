package xtable;

// typed table access point
public class Shopbuycolumns {
	Shopbuycolumns() {
	}

	public static xbean.ShopbuyColumn get(Long key) {
		return _Tables_.getInstance().shopbuycolumns.get(key);
	}

	public static xbean.ShopbuyColumn get(Long key, xbean.ShopbuyColumn value) {
		return _Tables_.getInstance().shopbuycolumns.get(key, value);
	}

	public static void insert(Long key, xbean.ShopbuyColumn value) {
		_Tables_.getInstance().shopbuycolumns.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().shopbuycolumns.delete(key);
	}

	public static boolean add(Long key, xbean.ShopbuyColumn value) {
		return _Tables_.getInstance().shopbuycolumns.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().shopbuycolumns.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.ShopbuyColumn> getCache() {
		return _Tables_.getInstance().shopbuycolumns.getCache();
	}

	public static xdb.TTable<Long, xbean.ShopbuyColumn> getTable() {
		return _Tables_.getInstance().shopbuycolumns;
	}

	public static xbean.ShopbuyColumn select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.ShopbuyColumn, xbean.ShopbuyColumn>() {
			public xbean.ShopbuyColumn get(xbean.ShopbuyColumn v) { return v.toData(); }
		});
	}

	public static java.util.Map<Integer, xbean.Shopbuy> selectShopbuys(Long key) {
		return getTable().select(key, new xdb.TField<xbean.ShopbuyColumn, java.util.Map<Integer, xbean.Shopbuy>>() {
				public java.util.Map<Integer, xbean.Shopbuy> get(xbean.ShopbuyColumn v) { return v.getShopbuysAsData(); }
			});
	}

}
