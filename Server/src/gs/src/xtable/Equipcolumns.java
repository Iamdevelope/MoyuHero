package xtable;

// typed table access point
public class Equipcolumns {
	Equipcolumns() {
	}

	public static xbean.EquipColumn get(Long key) {
		return _Tables_.getInstance().equipcolumns.get(key);
	}

	public static xbean.EquipColumn get(Long key, xbean.EquipColumn value) {
		return _Tables_.getInstance().equipcolumns.get(key, value);
	}

	public static void insert(Long key, xbean.EquipColumn value) {
		_Tables_.getInstance().equipcolumns.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().equipcolumns.delete(key);
	}

	public static boolean add(Long key, xbean.EquipColumn value) {
		return _Tables_.getInstance().equipcolumns.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().equipcolumns.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.EquipColumn> getCache() {
		return _Tables_.getInstance().equipcolumns.getCache();
	}

	public static xdb.TTable<Long, xbean.EquipColumn> getTable() {
		return _Tables_.getInstance().equipcolumns;
	}

	public static xbean.EquipColumn select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EquipColumn, xbean.EquipColumn>() {
			public xbean.EquipColumn get(xbean.EquipColumn v) { return v.toData(); }
		});
	}

	public static java.util.List<xbean.Equip> selectEquips(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EquipColumn, java.util.List<xbean.Equip>>() {
				public java.util.List<xbean.Equip> get(xbean.EquipColumn v) { return v.getEquipsAsData(); }
			});
	}

	public static Integer selectNextkey(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EquipColumn, Integer>() {
				public Integer get(xbean.EquipColumn v) { return v.getNextkey(); }
			});
	}

}
