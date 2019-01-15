package xtable;

// typed table access point
public class Billroles {
	Billroles() {
	}

	public static xbean.BillRole get(Long key) {
		return _Tables_.getInstance().billroles.get(key);
	}

	public static xbean.BillRole get(Long key, xbean.BillRole value) {
		return _Tables_.getInstance().billroles.get(key, value);
	}

	public static void insert(Long key, xbean.BillRole value) {
		_Tables_.getInstance().billroles.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().billroles.delete(key);
	}

	public static boolean add(Long key, xbean.BillRole value) {
		return _Tables_.getInstance().billroles.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().billroles.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.BillRole> getCache() {
		return _Tables_.getInstance().billroles.getCache();
	}

	public static xdb.TTable<Long, xbean.BillRole> getTable() {
		return _Tables_.getInstance().billroles;
	}

	public static xbean.BillRole select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BillRole, xbean.BillRole>() {
			public xbean.BillRole get(xbean.BillRole v) { return v.toData(); }
		});
	}

	public static java.util.Map<Long, xbean.BillData> selectBills(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BillRole, java.util.Map<Long, xbean.BillData>>() {
				public java.util.Map<Long, xbean.BillData> get(xbean.BillRole v) { return v.getBillsAsData(); }
			});
	}

	public static Integer selectFirstcharge(Long key) {
		return getTable().select(key, new xdb.TField<xbean.BillRole, Integer>() {
				public Integer get(xbean.BillRole v) { return v.getFirstcharge(); }
			});
	}

}
