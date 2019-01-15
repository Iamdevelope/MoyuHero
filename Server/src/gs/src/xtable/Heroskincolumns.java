package xtable;

// typed table access point
public class Heroskincolumns {
	Heroskincolumns() {
	}

	public static xbean.HeroSkinColumn get(Long key) {
		return _Tables_.getInstance().heroskincolumns.get(key);
	}

	public static xbean.HeroSkinColumn get(Long key, xbean.HeroSkinColumn value) {
		return _Tables_.getInstance().heroskincolumns.get(key, value);
	}

	public static void insert(Long key, xbean.HeroSkinColumn value) {
		_Tables_.getInstance().heroskincolumns.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().heroskincolumns.delete(key);
	}

	public static boolean add(Long key, xbean.HeroSkinColumn value) {
		return _Tables_.getInstance().heroskincolumns.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().heroskincolumns.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.HeroSkinColumn> getCache() {
		return _Tables_.getInstance().heroskincolumns.getCache();
	}

	public static xdb.TTable<Long, xbean.HeroSkinColumn> getTable() {
		return _Tables_.getInstance().heroskincolumns;
	}

	public static xbean.HeroSkinColumn select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.HeroSkinColumn, xbean.HeroSkinColumn>() {
			public xbean.HeroSkinColumn get(xbean.HeroSkinColumn v) { return v.toData(); }
		});
	}

	public static java.util.List<xbean.HeroSkin> selectHeroskins(Long key) {
		return getTable().select(key, new xdb.TField<xbean.HeroSkinColumn, java.util.List<xbean.HeroSkin>>() {
				public java.util.List<xbean.HeroSkin> get(xbean.HeroSkinColumn v) { return v.getHeroskinsAsData(); }
			});
	}

}
