package xtable;

// typed table access point
public class Msgroles {
	Msgroles() {
	}

	public static xbean.MsgRole get(Long key) {
		return _Tables_.getInstance().msgroles.get(key);
	}

	public static xbean.MsgRole get(Long key, xbean.MsgRole value) {
		return _Tables_.getInstance().msgroles.get(key, value);
	}

	public static void insert(Long key, xbean.MsgRole value) {
		_Tables_.getInstance().msgroles.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().msgroles.delete(key);
	}

	public static boolean add(Long key, xbean.MsgRole value) {
		return _Tables_.getInstance().msgroles.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().msgroles.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.MsgRole> getCache() {
		return _Tables_.getInstance().msgroles.getCache();
	}

	public static xdb.TTable<Long, xbean.MsgRole> getTable() {
		return _Tables_.getInstance().msgroles;
	}

	public static xbean.MsgRole select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.MsgRole, xbean.MsgRole>() {
			public xbean.MsgRole get(xbean.MsgRole v) { return v.toData(); }
		});
	}

	public static java.util.List<xbean.SysMsg> selectSysmsgs(Long key) {
		return getTable().select(key, new xdb.TField<xbean.MsgRole, java.util.List<xbean.SysMsg>>() {
				public java.util.List<xbean.SysMsg> get(xbean.MsgRole v) { return v.getSysmsgsAsData(); }
			});
	}

}
