package xtable;

// typed table access point
public class Maillist {
	Maillist() {
	}

	public static xbean.Mails get(Long key) {
		return _Tables_.getInstance().maillist.get(key);
	}

	public static xbean.Mails get(Long key, xbean.Mails value) {
		return _Tables_.getInstance().maillist.get(key, value);
	}

	public static void insert(Long key, xbean.Mails value) {
		_Tables_.getInstance().maillist.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().maillist.delete(key);
	}

	public static boolean add(Long key, xbean.Mails value) {
		return _Tables_.getInstance().maillist.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().maillist.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.Mails> getCache() {
		return _Tables_.getInstance().maillist.getCache();
	}

	public static xdb.TTable<Long, xbean.Mails> getTable() {
		return _Tables_.getInstance().maillist;
	}

	public static xbean.Mails select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Mails, xbean.Mails>() {
			public xbean.Mails get(xbean.Mails v) { return v.toData(); }
		});
	}

	public static java.util.List<xbean.Mail> selectMails(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Mails, java.util.List<xbean.Mail>>() {
				public java.util.List<xbean.Mail> get(xbean.Mails v) { return v.getMailsAsData(); }
			});
	}

	public static Integer selectNextkey(Long key) {
		return getTable().select(key, new xdb.TField<xbean.Mails, Integer>() {
				public Integer get(xbean.Mails v) { return v.getNextkey(); }
			});
	}

}
