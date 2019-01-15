package xtable;

// typed table access point
public class Equipextdatas {
	Equipextdatas() {
	}

	public static xbean.EquipExtData get(Long key) {
		return _Tables_.getInstance().equipextdatas.get(key);
	}

	public static xbean.EquipExtData get(Long key, xbean.EquipExtData value) {
		return _Tables_.getInstance().equipextdatas.get(key, value);
	}

	public static void insert(Long key, xbean.EquipExtData value) {
		_Tables_.getInstance().equipextdatas.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().equipextdatas.delete(key);
	}

	public static boolean add(Long key, xbean.EquipExtData value) {
		return _Tables_.getInstance().equipextdatas.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().equipextdatas.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.EquipExtData> getCache() {
		return _Tables_.getInstance().equipextdatas.getCache();
	}

	public static xdb.TTable<Long, xbean.EquipExtData> getTable() {
		return _Tables_.getInstance().equipextdatas;
	}

	public static xbean.EquipExtData select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EquipExtData, xbean.EquipExtData>() {
			public xbean.EquipExtData get(xbean.EquipExtData v) { return v.toData(); }
		});
	}

	public static Integer selectLevel(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EquipExtData, Integer>() {
				public Integer get(xbean.EquipExtData v) { return v.getLevel(); }
			});
	}

	public static Integer selectInit1(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EquipExtData, Integer>() {
				public Integer get(xbean.EquipExtData v) { return v.getInit1(); }
			});
	}

	public static Integer selectInit2(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EquipExtData, Integer>() {
				public Integer get(xbean.EquipExtData v) { return v.getInit2(); }
			});
	}

	public static Integer selectInit3(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EquipExtData, Integer>() {
				public Integer get(xbean.EquipExtData v) { return v.getInit3(); }
			});
	}

	public static Integer selectAttr1(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EquipExtData, Integer>() {
				public Integer get(xbean.EquipExtData v) { return v.getAttr1(); }
			});
	}

	public static Integer selectAttr2(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EquipExtData, Integer>() {
				public Integer get(xbean.EquipExtData v) { return v.getAttr2(); }
			});
	}

	public static Integer selectAttr3(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EquipExtData, Integer>() {
				public Integer get(xbean.EquipExtData v) { return v.getAttr3(); }
			});
	}

	public static Integer selectAttr4(Long key) {
		return getTable().select(key, new xdb.TField<xbean.EquipExtData, Integer>() {
				public Integer get(xbean.EquipExtData v) { return v.getAttr4(); }
			});
	}

}
