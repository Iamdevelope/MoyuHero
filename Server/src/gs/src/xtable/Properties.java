package xtable;

// typed table access point
public class Properties {
	Properties() {
	}

	public static xdb.util.AutoKey<Long> getAutoKey() {
		return _Tables_.getInstance().properties.getAutoKey();
	}

	public static Long nextKey() {
		return getAutoKey().next();
	}

	public static Long insert(xbean.Properties value) {
		Long next = nextKey();
		insert(next, value);
		return next;
	}

	public static xbean.Properties get(Long key) {
		return _Tables_.getInstance().properties.get(key);
	}

	public static xbean.Properties get(Long key, xbean.Properties value) {
		return _Tables_.getInstance().properties.get(key, value);
	}

	public static void insert(Long key, xbean.Properties value) {
		_Tables_.getInstance().properties.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().properties.delete(key);
	}

	public static boolean add(Long key, xbean.Properties value) {
		return _Tables_.getInstance().properties.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().properties.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.Properties> getCache() {
		return _Tables_.getInstance().properties.getCache();
	}

	public static xdb.TTable<Long, xbean.Properties> getTable() {
		return _Tables_.getInstance().properties;
	}

	public static xbean.Properties select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, xbean.Properties>() {
			public xbean.Properties get(xbean.Properties v) { return v.toData(); }
		});
	}

	public static String selectRolename(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, String>() {
				public String get(xbean.Properties v) { return v.getRolename(); }
			});
	}

	public static Integer selectUserid(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getUserid(); }
			});
	}

	public static String selectUsername(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, String>() {
				public String get(xbean.Properties v) { return v.getUsername(); }
			});
	}

	public static String selectPlattypestr(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, String>() {
				public String get(xbean.Properties v) { return v.getPlattypestr(); }
			});
	}

	public static String selectMac(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, String>() {
				public String get(xbean.Properties v) { return v.getMac(); }
			});
	}

	public static Integer selectOstype(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getOstype(); }
			});
	}

	public static Integer selectLevel(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getLevel(); }
			});
	}

	public static Integer selectExp(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getExp(); }
			});
	}

	public static Integer selectViplv(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getViplv(); }
			});
	}

	public static Integer selectVipexp(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getVipexp(); }
			});
	}

	public static Integer selectTi(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getTi(); }
			});
	}

	public static Long selectTichangetime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Long>() {
				public Long get(xbean.Properties v) { return v.getTichangetime(); }
			});
	}

	public static Integer selectGold(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getGold(); }
			});
	}

	public static Integer selectYuanbao(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getYuanbao(); }
			});
	}

	public static Integer selectShenglingzq(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getShenglingzq(); }
			});
	}

	public static Integer selectRonglian(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getRonglian(); }
			});
	}

	public static Integer selectHuangjinxz(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getHuangjinxz(); }
			});
	}

	public static Integer selectBaijinxz(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getBaijinxz(); }
			});
	}

	public static Integer selectQingtongxz(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getQingtongxz(); }
			});
	}

	public static Integer selectChitiexz(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getChitiexz(); }
			});
	}

	public static Integer selectJyjiejing(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getJyjiejing(); }
			});
	}

	public static Integer selectPvpti(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getPvpti(); }
			});
	}

	public static Long selectPvptitime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Long>() {
				public Long get(xbean.Properties v) { return v.getPvptitime(); }
			});
	}

	public static Integer selectTanxianti(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getTanxianti(); }
			});
	}

	public static Long selectTanxiantitime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Long>() {
				public Long get(xbean.Properties v) { return v.getTanxiantitime(); }
			});
	}

	public static Integer selectJinengdian(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getJinengdian(); }
			});
	}

	public static Long selectJinengdiantime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Long>() {
				public Long get(xbean.Properties v) { return v.getJinengdiantime(); }
			});
	}

	public static java.util.Map<Integer, xbean.mohe> selectMoheshop(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, java.util.Map<Integer, xbean.mohe>>() {
				public java.util.Map<Integer, xbean.mohe> get(xbean.Properties v) { return v.getMoheshopAsData(); }
			});
	}

	public static Integer selectSmzhangjie(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getSmzhangjie(); }
			});
	}

	public static Integer selectBattlenum(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getBattlenum(); }
			});
	}

	public static Long selectSmendtime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Long>() {
				public Long get(xbean.Properties v) { return v.getSmendtime(); }
			});
	}

	public static java.util.Map<Integer, xbean.smshopdata> selectSmshop(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, java.util.Map<Integer, xbean.smshopdata>>() {
				public java.util.Map<Integer, xbean.smshopdata> get(xbean.Properties v) { return v.getSmshopAsData(); }
			});
	}

	public static Integer selectSmguanka_nocome(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getSmguanka_nocome(); }
			});
	}

	public static Integer selectSmshop_notcome(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getSmshop_notcome(); }
			});
	}

	public static Long selectCreatetime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Long>() {
				public Long get(xbean.Properties v) { return v.getCreatetime(); }
			});
	}

	public static Long selectOnlinetime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Long>() {
				public Long get(xbean.Properties v) { return v.getOnlinetime(); }
			});
	}

	public static Long selectOfflinetime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Long>() {
				public Long get(xbean.Properties v) { return v.getOfflinetime(); }
			});
	}

	public static Integer selectTibuynum(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getTibuynum(); }
			});
	}

	public static Long selectTibuytime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Long>() {
				public Long get(xbean.Properties v) { return v.getTibuytime(); }
			});
	}

	public static Integer selectGoldbuynum(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getGoldbuynum(); }
			});
	}

	public static Long selectGoldbuytime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Long>() {
				public Long get(xbean.Properties v) { return v.getGoldbuytime(); }
			});
	}

	public static Integer selectSignnum7(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getSignnum7(); }
			});
	}

	public static Integer selectSignnum28(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getSignnum28(); }
			});
	}

	public static Long selectSigntime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Long>() {
				public Long get(xbean.Properties v) { return v.getSigntime(); }
			});
	}

	public static Integer selectQiyuannum(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getQiyuannum(); }
			});
	}

	public static Long selectQiyuantime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Long>() {
				public Long get(xbean.Properties v) { return v.getQiyuantime(); }
			});
	}

	public static Integer selectQiyuanallnum(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getQiyuanallnum(); }
			});
	}

	public static Short selectBuybagnum(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Short>() {
				public Short get(xbean.Properties v) { return v.getBuybagnum(); }
			});
	}

	public static Short selectBuyherobagnum(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Short>() {
				public Short get(xbean.Properties v) { return v.getBuyherobagnum(); }
			});
	}

	public static Short selectTroopnum(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Short>() {
				public Short get(xbean.Properties v) { return v.getTroopnum(); }
			});
	}

	public static Integer selectSweepnum(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getSweepnum(); }
			});
	}

	public static Long selectTodaylasttime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Long>() {
				public Long get(xbean.Properties v) { return v.getTodaylasttime(); }
			});
	}

	public static Integer selectSweepbuynum(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getSweepbuynum(); }
			});
	}

	public static Integer selectMszqgetnum(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, Integer>() {
				public Integer get(xbean.Properties v) { return v.getMszqgetnum(); }
			});
	}

	public static java.util.List<Integer> selectNewyindao(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Properties, java.util.List<Integer>>() {
				public java.util.List<Integer> get(xbean.Properties v) { return v.getNewyindaoAsData(); }
			});
	}

}
