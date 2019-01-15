package xtable;

// typed table access point
public class Endlesscolumns {
	Endlesscolumns() {
	}

	public static xbean.EndlessInfo get(Long key) {
		return _Tables_.getInstance().endlesscolumns.get(key);
	}

	public static xbean.EndlessInfo get(Long key, xbean.EndlessInfo value) {
		return _Tables_.getInstance().endlesscolumns.get(key, value);
	}

	public static void insert(Long key, xbean.EndlessInfo value) {
		_Tables_.getInstance().endlesscolumns.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().endlesscolumns.delete(key);
	}

	public static boolean add(Long key, xbean.EndlessInfo value) {
		return _Tables_.getInstance().endlesscolumns.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().endlesscolumns.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.EndlessInfo> getCache() {
		return _Tables_.getInstance().endlesscolumns.getCache();
	}

	public static xdb.TTable<Long, xbean.EndlessInfo> getTable() {
		return _Tables_.getInstance().endlesscolumns;
	}

	public static xbean.EndlessInfo select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessInfo, xbean.EndlessInfo>() {
			public xbean.EndlessInfo get(xbean.EndlessInfo v) { return v.toData(); }
		});
	}

	public static Integer selectBattleid(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessInfo, Integer>() {
				public Integer get(xbean.EndlessInfo v) { return v.getBattleid(); }
			});
	}

	public static Integer selectGroupnum(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessInfo, Integer>() {
				public Integer get(xbean.EndlessInfo v) { return v.getGroupnum(); }
			});
	}

	public static java.util.Map<Integer, Integer> selectUseherokeylist(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessInfo, java.util.Map<Integer, Integer>>() {
				public java.util.Map<Integer, Integer> get(xbean.EndlessInfo v) { return v.getUseherokeylistAsData(); }
			});
	}

	public static java.util.List<Integer> selectMonstergroup(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessInfo, java.util.List<Integer>>() {
				public java.util.List<Integer> get(xbean.EndlessInfo v) { return v.getMonstergroupAsData(); }
			});
	}

	public static Integer selectTrooptype(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessInfo, Integer>() {
				public Integer get(xbean.EndlessInfo v) { return v.getTrooptype(); }
			});
	}

	public static Integer selectMonstertrooptype(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessInfo, Integer>() {
				public Integer get(xbean.EndlessInfo v) { return v.getMonstertrooptype(); }
			});
	}

	public static Integer selectPact(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessInfo, Integer>() {
				public Integer get(xbean.EndlessInfo v) { return v.getPact(); }
			});
	}

	public static Integer selectDropnum(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessInfo, Integer>() {
				public Integer get(xbean.EndlessInfo v) { return v.getDropnum(); }
			});
	}

	public static Integer selectAlldropnum(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessInfo, Integer>() {
				public Integer get(xbean.EndlessInfo v) { return v.getAlldropnum(); }
			});
	}

	public static Integer selectAdd1(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessInfo, Integer>() {
				public Integer get(xbean.EndlessInfo v) { return v.getAdd1(); }
			});
	}

	public static Integer selectAdd2(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessInfo, Integer>() {
				public Integer get(xbean.EndlessInfo v) { return v.getAdd2(); }
			});
	}

	public static Integer selectAdd3(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessInfo, Integer>() {
				public Integer get(xbean.EndlessInfo v) { return v.getAdd3(); }
			});
	}

	public static Integer selectAdd4(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessInfo, Integer>() {
				public Integer get(xbean.EndlessInfo v) { return v.getAdd4(); }
			});
	}

	public static java.util.Map<Integer, Integer> selectHerobloodlist(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessInfo, java.util.Map<Integer, Integer>>() {
				public java.util.Map<Integer, Integer> get(xbean.EndlessInfo v) { return v.getHerobloodlistAsData(); }
			});
	}

	public static Integer selectIsend(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessInfo, Integer>() {
				public Integer get(xbean.EndlessInfo v) { return v.getIsend(); }
			});
	}

	public static Long selectTime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessInfo, Long>() {
				public Long get(xbean.EndlessInfo v) { return v.getTime(); }
			});
	}

	public static Integer selectIshalfcostpact(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessInfo, Integer>() {
				public Integer get(xbean.EndlessInfo v) { return v.getIshalfcostpact(); }
			});
	}

	public static Long selectEndtime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessInfo, Long>() {
				public Long get(xbean.EndlessInfo v) { return v.getEndtime(); }
			});
	}

	public static Integer selectExpectedrank(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessInfo, Integer>() {
				public Integer get(xbean.EndlessInfo v) { return v.getExpectedrank(); }
			});
	}

	public static java.util.Map<Integer, xbean.OtherHero> selectHeroattribute(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessInfo, java.util.Map<Integer, xbean.OtherHero>>() {
				public java.util.Map<Integer, xbean.OtherHero> get(xbean.EndlessInfo v) { return v.getHeroattributeAsData(); }
			});
	}

	public static Integer selectOnranknum(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessInfo, Integer>() {
				public Integer get(xbean.EndlessInfo v) { return v.getOnranknum(); }
			});
	}

	public static Long selectOnranklasttime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessInfo, Long>() {
				public Long get(xbean.EndlessInfo v) { return v.getOnranklasttime(); }
			});
	}

	public static Integer selectIsnotfirst(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EndlessInfo, Integer>() {
				public Integer get(xbean.EndlessInfo v) { return v.getIsnotfirst(); }
			});
	}

}
