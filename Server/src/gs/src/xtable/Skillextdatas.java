package xtable;

// typed table access point
public class Skillextdatas {
	Skillextdatas() {
	}

	public static xbean.SkillExtData get(Long key) {
		return _Tables_.getInstance().skillextdatas.get(key);
	}

	public static xbean.SkillExtData get(Long key, xbean.SkillExtData value) {
		return _Tables_.getInstance().skillextdatas.get(key, value);
	}

	public static void insert(Long key, xbean.SkillExtData value) {
		_Tables_.getInstance().skillextdatas.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().skillextdatas.delete(key);
	}

	public static boolean add(Long key, xbean.SkillExtData value) {
		return _Tables_.getInstance().skillextdatas.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().skillextdatas.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.SkillExtData> getCache() {
		return _Tables_.getInstance().skillextdatas.getCache();
	}

	public static xdb.TTable<Long, xbean.SkillExtData> getTable() {
		return _Tables_.getInstance().skillextdatas;
	}

	public static xbean.SkillExtData select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.SkillExtData, xbean.SkillExtData>() {
			public xbean.SkillExtData get(xbean.SkillExtData v) { return v.toData(); }
		});
	}

	public static Integer selectLevel(Long key) {
		return getTable().select(key, new xdb.TField<xbean.SkillExtData, Integer>() {
				public Integer get(xbean.SkillExtData v) { return v.getLevel(); }
			});
	}

	public static Integer selectGrade(Long key) {
		return getTable().select(key, new xdb.TField<xbean.SkillExtData, Integer>() {
				public Integer get(xbean.SkillExtData v) { return v.getGrade(); }
			});
	}

	public static Integer selectExp(Long key) {
		return getTable().select(key, new xdb.TField<xbean.SkillExtData, Integer>() {
				public Integer get(xbean.SkillExtData v) { return v.getExp(); }
			});
	}

}
