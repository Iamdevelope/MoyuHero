
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class Mail extends xdb.XBean implements xbean.Mail {
	private int key; // 邮件唯一ID
	private String sender; // 发送者
	private String title; // 邮件标题
	private String msg; // 消息内容
	private java.util.LinkedList<Integer> innerdropidlist; // 掉落包ID
	private java.util.LinkedList<xbean.MailItem> items; // 掉落物品（非掉落包内容）
	private long endtime; // 结束时间
	private int isopen; // 是否打开过 0未打开，1已打开
	private java.util.LinkedList<String> strlist; // 参数列表

	Mail(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		sender = "";
		title = "";
		msg = "";
		innerdropidlist = new java.util.LinkedList<Integer>();
		items = new java.util.LinkedList<xbean.MailItem>();
		strlist = new java.util.LinkedList<String>();
	}

	public Mail() {
		this(0, null, null);
	}

	public Mail(Mail _o_) {
		this(_o_, null, null);
	}

	Mail(xbean.Mail _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof Mail) assign((Mail)_o1_);
		else if (_o1_ instanceof Mail.Data) assign((Mail.Data)_o1_);
		else if (_o1_ instanceof Mail.Const) assign(((Mail.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(Mail _o_) {
		_o_._xdb_verify_unsafe_();
		key = _o_.key;
		sender = _o_.sender;
		title = _o_.title;
		msg = _o_.msg;
		innerdropidlist = new java.util.LinkedList<Integer>();
		innerdropidlist.addAll(_o_.innerdropidlist);
		items = new java.util.LinkedList<xbean.MailItem>();
		for (xbean.MailItem _v_ : _o_.items)
			items.add(new MailItem(_v_, this, "items"));
		endtime = _o_.endtime;
		isopen = _o_.isopen;
		strlist = new java.util.LinkedList<String>();
		strlist.addAll(_o_.strlist);
	}

	private void assign(Mail.Data _o_) {
		key = _o_.key;
		sender = _o_.sender;
		title = _o_.title;
		msg = _o_.msg;
		innerdropidlist = new java.util.LinkedList<Integer>();
		innerdropidlist.addAll(_o_.innerdropidlist);
		items = new java.util.LinkedList<xbean.MailItem>();
		for (xbean.MailItem _v_ : _o_.items)
			items.add(new MailItem(_v_, this, "items"));
		endtime = _o_.endtime;
		isopen = _o_.isopen;
		strlist = new java.util.LinkedList<String>();
		strlist.addAll(_o_.strlist);
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(key);
		_os_.marshal(sender, xdb.Const.IO_CHARSET);
		_os_.marshal(title, xdb.Const.IO_CHARSET);
		_os_.marshal(msg, xdb.Const.IO_CHARSET);
		_os_.compact_uint32(innerdropidlist.size());
		for (Integer _v_ : innerdropidlist) {
			_os_.marshal(_v_);
		}
		_os_.compact_uint32(items.size());
		for (xbean.MailItem _v_ : items) {
			_v_.marshal(_os_);
		}
		_os_.marshal(endtime);
		_os_.marshal(isopen);
		_os_.compact_uint32(strlist.size());
		for (String _v_ : strlist) {
			_os_.marshal(_v_, xdb.Const.IO_CHARSET);
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		key = _os_.unmarshal_int();
		sender = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
		title = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
		msg = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _v_ = 0;
			_v_ = _os_.unmarshal_int();
			innerdropidlist.add(_v_);
		}
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			xbean.MailItem _v_ = new MailItem(0, this, "items");
			_v_.unmarshal(_os_);
			items.add(_v_);
		}
		endtime = _os_.unmarshal_long();
		isopen = _os_.unmarshal_int();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			String _v_ = "";
			_v_ = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			strlist.add(_v_);
		}
		return _os_;
	}

	@Override
	public xbean.Mail copy() {
		_xdb_verify_unsafe_();
		return new Mail(this);
	}

	@Override
	public xbean.Mail toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.Mail toBean() {
		_xdb_verify_unsafe_();
		return new Mail(this); // same as copy()
	}

	@Override
	public xbean.Mail toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.Mail toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getKey() { // 邮件唯一ID
		_xdb_verify_unsafe_();
		return key;
	}

	@Override
	public String getSender() { // 发送者
		_xdb_verify_unsafe_();
		return sender;
	}

	@Override
	public com.goldhuman.Common.Octets getSenderOctets() { // 发送者
		_xdb_verify_unsafe_();
		return com.goldhuman.Common.Octets.wrap(getSender(), xdb.Const.IO_CHARSET);
	}

	@Override
	public String getTitle() { // 邮件标题
		_xdb_verify_unsafe_();
		return title;
	}

	@Override
	public com.goldhuman.Common.Octets getTitleOctets() { // 邮件标题
		_xdb_verify_unsafe_();
		return com.goldhuman.Common.Octets.wrap(getTitle(), xdb.Const.IO_CHARSET);
	}

	@Override
	public String getMsg() { // 消息内容
		_xdb_verify_unsafe_();
		return msg;
	}

	@Override
	public com.goldhuman.Common.Octets getMsgOctets() { // 消息内容
		_xdb_verify_unsafe_();
		return com.goldhuman.Common.Octets.wrap(getMsg(), xdb.Const.IO_CHARSET);
	}

	@Override
	public java.util.List<Integer> getInnerdropidlist() { // 掉落包ID
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "innerdropidlist"), innerdropidlist);
	}

	public java.util.List<Integer> getInnerdropidlistAsData() { // 掉落包ID
		_xdb_verify_unsafe_();
		java.util.List<Integer> innerdropidlist;
		Mail _o_ = this;
		innerdropidlist = new java.util.LinkedList<Integer>();
		innerdropidlist.addAll(_o_.innerdropidlist);
		return innerdropidlist;
	}

	@Override
	public java.util.List<xbean.MailItem> getItems() { // 掉落物品（非掉落包内容）
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "items"), items);
	}

	public java.util.List<xbean.MailItem> getItemsAsData() { // 掉落物品（非掉落包内容）
		_xdb_verify_unsafe_();
		java.util.List<xbean.MailItem> items;
		Mail _o_ = this;
		items = new java.util.LinkedList<xbean.MailItem>();
		for (xbean.MailItem _v_ : _o_.items)
			items.add(new MailItem.Data(_v_));
		return items;
	}

	@Override
	public long getEndtime() { // 结束时间
		_xdb_verify_unsafe_();
		return endtime;
	}

	@Override
	public int getIsopen() { // 是否打开过 0未打开，1已打开
		_xdb_verify_unsafe_();
		return isopen;
	}

	@Override
	public java.util.List<String> getStrlist() { // 参数列表
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "strlist"), strlist);
	}

	public java.util.List<String> getStrlistAsData() { // 参数列表
		_xdb_verify_unsafe_();
		java.util.List<String> strlist;
		Mail _o_ = this;
		strlist = new java.util.LinkedList<String>();
		strlist.addAll(_o_.strlist);
		return strlist;
	}

	@Override
	public void setKey(int _v_) { // 邮件唯一ID
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "key") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, key) {
					public void rollback() { key = _xdb_saved; }
				};}});
		key = _v_;
	}

	@Override
	public void setSender(String _v_) { // 发送者
		_xdb_verify_unsafe_();
		if (null == _v_)
			throw new NullPointerException();
		xdb.Logs.logIf(new xdb.LogKey(this, "sender") {
			protected xdb.Log create() {
				return new xdb.logs.LogString(this, sender) {
					public void rollback() { sender = _xdb_saved; }
				};}});
		sender = _v_;
	}

	@Override
	public void setSenderOctets(com.goldhuman.Common.Octets _v_) { // 发送者
		_xdb_verify_unsafe_();
		this.setSender(_v_.getString(xdb.Const.IO_CHARSET));
	}

	@Override
	public void setTitle(String _v_) { // 邮件标题
		_xdb_verify_unsafe_();
		if (null == _v_)
			throw new NullPointerException();
		xdb.Logs.logIf(new xdb.LogKey(this, "title") {
			protected xdb.Log create() {
				return new xdb.logs.LogString(this, title) {
					public void rollback() { title = _xdb_saved; }
				};}});
		title = _v_;
	}

	@Override
	public void setTitleOctets(com.goldhuman.Common.Octets _v_) { // 邮件标题
		_xdb_verify_unsafe_();
		this.setTitle(_v_.getString(xdb.Const.IO_CHARSET));
	}

	@Override
	public void setMsg(String _v_) { // 消息内容
		_xdb_verify_unsafe_();
		if (null == _v_)
			throw new NullPointerException();
		xdb.Logs.logIf(new xdb.LogKey(this, "msg") {
			protected xdb.Log create() {
				return new xdb.logs.LogString(this, msg) {
					public void rollback() { msg = _xdb_saved; }
				};}});
		msg = _v_;
	}

	@Override
	public void setMsgOctets(com.goldhuman.Common.Octets _v_) { // 消息内容
		_xdb_verify_unsafe_();
		this.setMsg(_v_.getString(xdb.Const.IO_CHARSET));
	}

	@Override
	public void setEndtime(long _v_) { // 结束时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "endtime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, endtime) {
					public void rollback() { endtime = _xdb_saved; }
				};}});
		endtime = _v_;
	}

	@Override
	public void setIsopen(int _v_) { // 是否打开过 0未打开，1已打开
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "isopen") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, isopen) {
					public void rollback() { isopen = _xdb_saved; }
				};}});
		isopen = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		Mail _o_ = null;
		if ( _o1_ instanceof Mail ) _o_ = (Mail)_o1_;
		else if ( _o1_ instanceof Mail.Const ) _o_ = ((Mail.Const)_o1_).nThis();
		else return false;
		if (key != _o_.key) return false;
		if (!sender.equals(_o_.sender)) return false;
		if (!title.equals(_o_.title)) return false;
		if (!msg.equals(_o_.msg)) return false;
		if (!innerdropidlist.equals(_o_.innerdropidlist)) return false;
		if (!items.equals(_o_.items)) return false;
		if (endtime != _o_.endtime) return false;
		if (isopen != _o_.isopen) return false;
		if (!strlist.equals(_o_.strlist)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += key;
		_h_ += sender.hashCode();
		_h_ += title.hashCode();
		_h_ += msg.hashCode();
		_h_ += innerdropidlist.hashCode();
		_h_ += items.hashCode();
		_h_ += endtime;
		_h_ += isopen;
		_h_ += strlist.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(key);
		_sb_.append(",");
		_sb_.append("'").append(sender).append("'");
		_sb_.append(",");
		_sb_.append("'").append(title).append("'");
		_sb_.append(",");
		_sb_.append("'").append(msg).append("'");
		_sb_.append(",");
		_sb_.append(innerdropidlist);
		_sb_.append(",");
		_sb_.append(items);
		_sb_.append(",");
		_sb_.append(endtime);
		_sb_.append(",");
		_sb_.append(isopen);
		_sb_.append(",");
		_sb_.append(strlist);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("key"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("sender"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("title"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("msg"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("innerdropidlist"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("items"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("endtime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("isopen"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("strlist"));
		return lb;
	}

	private class Const implements xbean.Mail {
		Mail nThis() {
			return Mail.this;
		}

		@Override
		public xbean.Mail copy() {
			return Mail.this.copy();
		}

		@Override
		public xbean.Mail toData() {
			return Mail.this.toData();
		}

		public xbean.Mail toBean() {
			return Mail.this.toBean();
		}

		@Override
		public xbean.Mail toDataIf() {
			return Mail.this.toDataIf();
		}

		public xbean.Mail toBeanIf() {
			return Mail.this.toBeanIf();
		}

		@Override
		public int getKey() { // 邮件唯一ID
			_xdb_verify_unsafe_();
			return key;
		}

		@Override
		public String getSender() { // 发送者
			_xdb_verify_unsafe_();
			return sender;
		}

		@Override
		public com.goldhuman.Common.Octets getSenderOctets() { // 发送者
			_xdb_verify_unsafe_();
			return Mail.this.getSenderOctets();
		}

		@Override
		public String getTitle() { // 邮件标题
			_xdb_verify_unsafe_();
			return title;
		}

		@Override
		public com.goldhuman.Common.Octets getTitleOctets() { // 邮件标题
			_xdb_verify_unsafe_();
			return Mail.this.getTitleOctets();
		}

		@Override
		public String getMsg() { // 消息内容
			_xdb_verify_unsafe_();
			return msg;
		}

		@Override
		public com.goldhuman.Common.Octets getMsgOctets() { // 消息内容
			_xdb_verify_unsafe_();
			return Mail.this.getMsgOctets();
		}

		@Override
		public java.util.List<Integer> getInnerdropidlist() { // 掉落包ID
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(innerdropidlist);
		}

		public java.util.List<Integer> getInnerdropidlistAsData() { // 掉落包ID
			_xdb_verify_unsafe_();
			java.util.List<Integer> innerdropidlist;
			Mail _o_ = Mail.this;
		innerdropidlist = new java.util.LinkedList<Integer>();
		innerdropidlist.addAll(_o_.innerdropidlist);
			return innerdropidlist;
		}

		@Override
		public java.util.List<xbean.MailItem> getItems() { // 掉落物品（非掉落包内容）
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(items);
		}

		public java.util.List<xbean.MailItem> getItemsAsData() { // 掉落物品（非掉落包内容）
			_xdb_verify_unsafe_();
			java.util.List<xbean.MailItem> items;
			Mail _o_ = Mail.this;
		items = new java.util.LinkedList<xbean.MailItem>();
		for (xbean.MailItem _v_ : _o_.items)
			items.add(new MailItem.Data(_v_));
			return items;
		}

		@Override
		public long getEndtime() { // 结束时间
			_xdb_verify_unsafe_();
			return endtime;
		}

		@Override
		public int getIsopen() { // 是否打开过 0未打开，1已打开
			_xdb_verify_unsafe_();
			return isopen;
		}

		@Override
		public java.util.List<String> getStrlist() { // 参数列表
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(strlist);
		}

		public java.util.List<String> getStrlistAsData() { // 参数列表
			_xdb_verify_unsafe_();
			java.util.List<String> strlist;
			Mail _o_ = Mail.this;
		strlist = new java.util.LinkedList<String>();
		strlist.addAll(_o_.strlist);
			return strlist;
		}

		@Override
		public void setKey(int _v_) { // 邮件唯一ID
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setSender(String _v_) { // 发送者
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setSenderOctets(com.goldhuman.Common.Octets _v_) { // 发送者
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTitle(String _v_) { // 邮件标题
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTitleOctets(com.goldhuman.Common.Octets _v_) { // 邮件标题
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setMsg(String _v_) { // 消息内容
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setMsgOctets(com.goldhuman.Common.Octets _v_) { // 消息内容
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setEndtime(long _v_) { // 结束时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setIsopen(int _v_) { // 是否打开过 0未打开，1已打开
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean toConst() {
			_xdb_verify_unsafe_();
			return this;
		}

		@Override
		public boolean isConst() {
			_xdb_verify_unsafe_();
			return true;
		}

		@Override
		public boolean isData() {
			return Mail.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return Mail.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return Mail.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return Mail.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return Mail.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return Mail.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return Mail.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return Mail.this.hashCode();
		}

		@Override
		public String toString() {
			return Mail.this.toString();
		}

	}

	public static final class Data implements xbean.Mail {
		private int key; // 邮件唯一ID
		private String sender; // 发送者
		private String title; // 邮件标题
		private String msg; // 消息内容
		private java.util.LinkedList<Integer> innerdropidlist; // 掉落包ID
		private java.util.LinkedList<xbean.MailItem> items; // 掉落物品（非掉落包内容）
		private long endtime; // 结束时间
		private int isopen; // 是否打开过 0未打开，1已打开
		private java.util.LinkedList<String> strlist; // 参数列表

		public Data() {
			sender = "";
			title = "";
			msg = "";
			innerdropidlist = new java.util.LinkedList<Integer>();
			items = new java.util.LinkedList<xbean.MailItem>();
			strlist = new java.util.LinkedList<String>();
		}

		Data(xbean.Mail _o1_) {
			if (_o1_ instanceof Mail) assign((Mail)_o1_);
			else if (_o1_ instanceof Mail.Data) assign((Mail.Data)_o1_);
			else if (_o1_ instanceof Mail.Const) assign(((Mail.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(Mail _o_) {
			key = _o_.key;
			sender = _o_.sender;
			title = _o_.title;
			msg = _o_.msg;
			innerdropidlist = new java.util.LinkedList<Integer>();
			innerdropidlist.addAll(_o_.innerdropidlist);
			items = new java.util.LinkedList<xbean.MailItem>();
			for (xbean.MailItem _v_ : _o_.items)
				items.add(new MailItem.Data(_v_));
			endtime = _o_.endtime;
			isopen = _o_.isopen;
			strlist = new java.util.LinkedList<String>();
			strlist.addAll(_o_.strlist);
		}

		private void assign(Mail.Data _o_) {
			key = _o_.key;
			sender = _o_.sender;
			title = _o_.title;
			msg = _o_.msg;
			innerdropidlist = new java.util.LinkedList<Integer>();
			innerdropidlist.addAll(_o_.innerdropidlist);
			items = new java.util.LinkedList<xbean.MailItem>();
			for (xbean.MailItem _v_ : _o_.items)
				items.add(new MailItem.Data(_v_));
			endtime = _o_.endtime;
			isopen = _o_.isopen;
			strlist = new java.util.LinkedList<String>();
			strlist.addAll(_o_.strlist);
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(key);
			_os_.marshal(sender, xdb.Const.IO_CHARSET);
			_os_.marshal(title, xdb.Const.IO_CHARSET);
			_os_.marshal(msg, xdb.Const.IO_CHARSET);
			_os_.compact_uint32(innerdropidlist.size());
			for (Integer _v_ : innerdropidlist) {
				_os_.marshal(_v_);
			}
			_os_.compact_uint32(items.size());
			for (xbean.MailItem _v_ : items) {
				_v_.marshal(_os_);
			}
			_os_.marshal(endtime);
			_os_.marshal(isopen);
			_os_.compact_uint32(strlist.size());
			for (String _v_ : strlist) {
				_os_.marshal(_v_, xdb.Const.IO_CHARSET);
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			key = _os_.unmarshal_int();
			sender = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			title = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			msg = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				int _v_ = 0;
				_v_ = _os_.unmarshal_int();
				innerdropidlist.add(_v_);
			}
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				xbean.MailItem _v_ = xbean.Pod.newMailItemData();
				_v_.unmarshal(_os_);
				items.add(_v_);
			}
			endtime = _os_.unmarshal_long();
			isopen = _os_.unmarshal_int();
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				String _v_ = "";
				_v_ = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
				strlist.add(_v_);
			}
			return _os_;
		}

		@Override
		public xbean.Mail copy() {
			return new Data(this);
		}

		@Override
		public xbean.Mail toData() {
			return new Data(this);
		}

		public xbean.Mail toBean() {
			return new Mail(this, null, null);
		}

		@Override
		public xbean.Mail toDataIf() {
			return this;
		}

		public xbean.Mail toBeanIf() {
			return new Mail(this, null, null);
		}

		// xdb.Bean interface. Data Unsupported
		public boolean xdbManaged() { throw new UnsupportedOperationException(); }
		public xdb.Bean xdbParent() { throw new UnsupportedOperationException(); }
		public String xdbVarname()  { throw new UnsupportedOperationException(); }
		public Long    xdbObjId()   { throw new UnsupportedOperationException(); }
		public xdb.Bean toConst()   { throw new UnsupportedOperationException(); }
		public boolean isConst()    { return false; }
		public boolean isData()     { return true; }

		@Override
		public int getKey() { // 邮件唯一ID
			return key;
		}

		@Override
		public String getSender() { // 发送者
			return sender;
		}

		@Override
		public com.goldhuman.Common.Octets getSenderOctets() { // 发送者
			return com.goldhuman.Common.Octets.wrap(getSender(), xdb.Const.IO_CHARSET);
		}

		@Override
		public String getTitle() { // 邮件标题
			return title;
		}

		@Override
		public com.goldhuman.Common.Octets getTitleOctets() { // 邮件标题
			return com.goldhuman.Common.Octets.wrap(getTitle(), xdb.Const.IO_CHARSET);
		}

		@Override
		public String getMsg() { // 消息内容
			return msg;
		}

		@Override
		public com.goldhuman.Common.Octets getMsgOctets() { // 消息内容
			return com.goldhuman.Common.Octets.wrap(getMsg(), xdb.Const.IO_CHARSET);
		}

		@Override
		public java.util.List<Integer> getInnerdropidlist() { // 掉落包ID
			return innerdropidlist;
		}

		@Override
		public java.util.List<Integer> getInnerdropidlistAsData() { // 掉落包ID
			return innerdropidlist;
		}

		@Override
		public java.util.List<xbean.MailItem> getItems() { // 掉落物品（非掉落包内容）
			return items;
		}

		@Override
		public java.util.List<xbean.MailItem> getItemsAsData() { // 掉落物品（非掉落包内容）
			return items;
		}

		@Override
		public long getEndtime() { // 结束时间
			return endtime;
		}

		@Override
		public int getIsopen() { // 是否打开过 0未打开，1已打开
			return isopen;
		}

		@Override
		public java.util.List<String> getStrlist() { // 参数列表
			return strlist;
		}

		@Override
		public java.util.List<String> getStrlistAsData() { // 参数列表
			return strlist;
		}

		@Override
		public void setKey(int _v_) { // 邮件唯一ID
			key = _v_;
		}

		@Override
		public void setSender(String _v_) { // 发送者
			if (null == _v_)
				throw new NullPointerException();
			sender = _v_;
		}

		@Override
		public void setSenderOctets(com.goldhuman.Common.Octets _v_) { // 发送者
			this.setSender(_v_.getString(xdb.Const.IO_CHARSET));
		}

		@Override
		public void setTitle(String _v_) { // 邮件标题
			if (null == _v_)
				throw new NullPointerException();
			title = _v_;
		}

		@Override
		public void setTitleOctets(com.goldhuman.Common.Octets _v_) { // 邮件标题
			this.setTitle(_v_.getString(xdb.Const.IO_CHARSET));
		}

		@Override
		public void setMsg(String _v_) { // 消息内容
			if (null == _v_)
				throw new NullPointerException();
			msg = _v_;
		}

		@Override
		public void setMsgOctets(com.goldhuman.Common.Octets _v_) { // 消息内容
			this.setMsg(_v_.getString(xdb.Const.IO_CHARSET));
		}

		@Override
		public void setEndtime(long _v_) { // 结束时间
			endtime = _v_;
		}

		@Override
		public void setIsopen(int _v_) { // 是否打开过 0未打开，1已打开
			isopen = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof Mail.Data)) return false;
			Mail.Data _o_ = (Mail.Data) _o1_;
			if (key != _o_.key) return false;
			if (!sender.equals(_o_.sender)) return false;
			if (!title.equals(_o_.title)) return false;
			if (!msg.equals(_o_.msg)) return false;
			if (!innerdropidlist.equals(_o_.innerdropidlist)) return false;
			if (!items.equals(_o_.items)) return false;
			if (endtime != _o_.endtime) return false;
			if (isopen != _o_.isopen) return false;
			if (!strlist.equals(_o_.strlist)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += key;
			_h_ += sender.hashCode();
			_h_ += title.hashCode();
			_h_ += msg.hashCode();
			_h_ += innerdropidlist.hashCode();
			_h_ += items.hashCode();
			_h_ += endtime;
			_h_ += isopen;
			_h_ += strlist.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(key);
			_sb_.append(",");
			_sb_.append("'").append(sender).append("'");
			_sb_.append(",");
			_sb_.append("'").append(title).append("'");
			_sb_.append(",");
			_sb_.append("'").append(msg).append("'");
			_sb_.append(",");
			_sb_.append(innerdropidlist);
			_sb_.append(",");
			_sb_.append(items);
			_sb_.append(",");
			_sb_.append(endtime);
			_sb_.append(",");
			_sb_.append(isopen);
			_sb_.append(",");
			_sb_.append(strlist);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
