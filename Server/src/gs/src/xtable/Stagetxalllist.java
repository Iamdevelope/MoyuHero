package xtable;

// typed table access point
public class Stagetxalllist {
	Stagetxalllist() {
	}

	public static xbean.stagetxall get(Long key) {
		return _Tables_.getInstance().stagetxalllist.get(key);
	}

	public static xbean.stagetxall get(Long key, xbean.stagetxall value) {
		return _Tables_.getInstance().stagetxalllist.get(key, value);
	}

	public static void insert(Long key, xbean.stagetxall value) {
		_Tables_.getInstance().stagetxalllist.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().stagetxalllist.delete(key);
	}

	public static boolean add(Long key, xbean.stagetxall value) {
		return _Tables_.getInstance().stagetxalllist.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().stagetxalllist.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.stagetxall> getCache() {
		return _Tables_.getInstance().stagetxalllist.getCache();
	}

	public static xdb.TTable<Long, xbean.stagetxall> getTable() {
		return _Tables_.getInstance().stagetxalllist;
	}

	public static xbean.stagetxall select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.stagetxall, xbean.stagetxall>() {
			public xbean.stagetxall get(xbean.stagetxall v) { return v.toData(); }
		});
	}

	public static Long selectTxtime(Long key) {
		return getTable().select(key, new xdb.TField<xbean.stagetxall, Long>() {
				public Long get(xbean.stagetxall v) { return v.getTxtime(); }
			});
	}

	public static java.util.Map<Integer, xbean.teamtanxian> selectTeamallmap(Long key) {
		return getTable().select(key, new xdb.TField<xbean.stagetxall, java.util.Map<Integer, xbean.teamtanxian>>() {
				public java.util.Map<Integer, xbean.teamtanxian> get(xbean.stagetxall v) { return v.getTeamallmapAsData(); }
			});
	}

	public static java.util.Map<Integer, xbean.stagetanxian> selectStagetxallmap(Long key) {
		return getTable().select(key, new xdb.TField<xbean.stagetxall, java.util.Map<Integer, xbean.stagetanxian>>() {
				public java.util.Map<Integer, xbean.stagetanxian> get(xbean.stagetxall v) { return v.getStagetxallmapAsData(); }
			});
	}

}
