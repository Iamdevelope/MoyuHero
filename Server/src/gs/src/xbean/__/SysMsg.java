
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class SysMsg extends xdb.XBean implements xbean.SysMsg {
	private long time; // 
	private int msgid; // 
	private java.util.LinkedList<String> params; // 
	private String text; // 
	private boolean isnew; // 
	private boolean sended; // 
	private long sendroleid; // 发送者id 系统-0
	private int msgtype; // 消息类型 0-系统 1-好友

	SysMsg(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		params = new java.util.LinkedList<String>();
		text = "";
		sended = false;
	}

	public SysMsg() {
		this(0, null, null);
	}

	public SysMsg(SysMsg _o_) {
		this(_o_, null, null);
	}

	SysMsg(xbean.SysMsg _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof SysMsg) assign((SysMsg)_o1_);
		else if (_o1_ instanceof SysMsg.Data) assign((SysMsg.Data)_o1_);
		else if (_o1_ instanceof SysMsg.Const) assign(((SysMsg.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(SysMsg _o_) {
		_o_._xdb_verify_unsafe_();
		time = _o_.time;
		msgid = _o_.msgid;
		params = new java.util.LinkedList<String>();
		params.addAll(_o_.params);
		text = _o_.text;
		isnew = _o_.isnew;
		sended = _o_.sended;
		sendroleid = _o_.sendroleid;
		msgtype = _o_.msgtype;
	}

	private void assign(SysMsg.Data _o_) {
		time = _o_.time;
		msgid = _o_.msgid;
		params = new java.util.LinkedList<String>();
		params.addAll(_o_.params);
		text = _o_.text;
		isnew = _o_.isnew;
		sended = _o_.sended;
		sendroleid = _o_.sendroleid;
		msgtype = _o_.msgtype;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(time);
		_os_.marshal(msgid);
		_os_.compact_uint32(params.size());
		for (String _v_ : params) {
			_os_.marshal(_v_, xdb.Const.IO_CHARSET);
		}
		_os_.marshal(text, xdb.Const.IO_CHARSET);
		_os_.marshal(isnew);
		_os_.marshal(sended);
		_os_.marshal(sendroleid);
		_os_.marshal(msgtype);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		time = _os_.unmarshal_long();
		msgid = _os_.unmarshal_int();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			String _v_ = "";
			_v_ = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			params.add(_v_);
		}
		text = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
		isnew = _os_.unmarshal_boolean();
		sended = _os_.unmarshal_boolean();
		sendroleid = _os_.unmarshal_long();
		msgtype = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.SysMsg copy() {
		_xdb_verify_unsafe_();
		return new SysMsg(this);
	}

	@Override
	public xbean.SysMsg toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.SysMsg toBean() {
		_xdb_verify_unsafe_();
		return new SysMsg(this); // same as copy()
	}

	@Override
	public xbean.SysMsg toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.SysMsg toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public long getTime() { // 
		_xdb_verify_unsafe_();
		return time;
	}

	@Override
	public int getMsgid() { // 
		_xdb_verify_unsafe_();
		return msgid;
	}

	@Override
	public java.util.List<String> getParams() { // 
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "params"), params);
	}

	public java.util.List<String> getParamsAsData() { // 
		_xdb_verify_unsafe_();
		java.util.List<String> params;
		SysMsg _o_ = this;
		params = new java.util.LinkedList<String>();
		params.addAll(_o_.params);
		return params;
	}

	@Override
	public String getText() { // 
		_xdb_verify_unsafe_();
		return text;
	}

	@Override
	public com.goldhuman.Common.Octets getTextOctets() { // 
		_xdb_verify_unsafe_();
		return com.goldhuman.Common.Octets.wrap(getText(), xdb.Const.IO_CHARSET);
	}

	@Override
	public boolean getIsnew() { // 
		_xdb_verify_unsafe_();
		return isnew;
	}

	@Override
	public boolean getSended() { // 
		_xdb_verify_unsafe_();
		return sended;
	}

	@Override
	public long getSendroleid() { // 发送者id 系统-0
		_xdb_verify_unsafe_();
		return sendroleid;
	}

	@Override
	public int getMsgtype() { // 消息类型 0-系统 1-好友
		_xdb_verify_unsafe_();
		return msgtype;
	}

	@Override
	public void setTime(long _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "time") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, time) {
					public void rollback() { time = _xdb_saved; }
				};}});
		time = _v_;
	}

	@Override
	public void setMsgid(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "msgid") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, msgid) {
					public void rollback() { msgid = _xdb_saved; }
				};}});
		msgid = _v_;
	}

	@Override
	public void setText(String _v_) { // 
		_xdb_verify_unsafe_();
		if (null == _v_)
			throw new NullPointerException();
		xdb.Logs.logIf(new xdb.LogKey(this, "text") {
			protected xdb.Log create() {
				return new xdb.logs.LogString(this, text) {
					public void rollback() { text = _xdb_saved; }
				};}});
		text = _v_;
	}

	@Override
	public void setTextOctets(com.goldhuman.Common.Octets _v_) { // 
		_xdb_verify_unsafe_();
		this.setText(_v_.getString(xdb.Const.IO_CHARSET));
	}

	@Override
	public void setIsnew(boolean _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "isnew") {
			protected xdb.Log create() {
				return new xdb.logs.LogObject<Boolean>(this, isnew) {
					public void rollback() { isnew = _xdb_saved; }
				};}});
		isnew = _v_;
	}

	@Override
	public void setSended(boolean _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "sended") {
			protected xdb.Log create() {
				return new xdb.logs.LogObject<Boolean>(this, sended) {
					public void rollback() { sended = _xdb_saved; }
				};}});
		sended = _v_;
	}

	@Override
	public void setSendroleid(long _v_) { // 发送者id 系统-0
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "sendroleid") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, sendroleid) {
					public void rollback() { sendroleid = _xdb_saved; }
				};}});
		sendroleid = _v_;
	}

	@Override
	public void setMsgtype(int _v_) { // 消息类型 0-系统 1-好友
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "msgtype") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, msgtype) {
					public void rollback() { msgtype = _xdb_saved; }
				};}});
		msgtype = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		SysMsg _o_ = null;
		if ( _o1_ instanceof SysMsg ) _o_ = (SysMsg)_o1_;
		else if ( _o1_ instanceof SysMsg.Const ) _o_ = ((SysMsg.Const)_o1_).nThis();
		else return false;
		if (time != _o_.time) return false;
		if (msgid != _o_.msgid) return false;
		if (!params.equals(_o_.params)) return false;
		if (!text.equals(_o_.text)) return false;
		if (isnew != _o_.isnew) return false;
		if (sended != _o_.sended) return false;
		if (sendroleid != _o_.sendroleid) return false;
		if (msgtype != _o_.msgtype) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += time;
		_h_ += msgid;
		_h_ += params.hashCode();
		_h_ += text.hashCode();
		_h_ += isnew ? 1231 : 1237;
		_h_ += sended ? 1231 : 1237;
		_h_ += sendroleid;
		_h_ += msgtype;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(time);
		_sb_.append(",");
		_sb_.append(msgid);
		_sb_.append(",");
		_sb_.append(params);
		_sb_.append(",");
		_sb_.append("'").append(text).append("'");
		_sb_.append(",");
		_sb_.append(isnew);
		_sb_.append(",");
		_sb_.append(sended);
		_sb_.append(",");
		_sb_.append(sendroleid);
		_sb_.append(",");
		_sb_.append(msgtype);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("time"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("msgid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("params"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("text"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("isnew"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("sended"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("sendroleid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("msgtype"));
		return lb;
	}

	private class Const implements xbean.SysMsg {
		SysMsg nThis() {
			return SysMsg.this;
		}

		@Override
		public xbean.SysMsg copy() {
			return SysMsg.this.copy();
		}

		@Override
		public xbean.SysMsg toData() {
			return SysMsg.this.toData();
		}

		public xbean.SysMsg toBean() {
			return SysMsg.this.toBean();
		}

		@Override
		public xbean.SysMsg toDataIf() {
			return SysMsg.this.toDataIf();
		}

		public xbean.SysMsg toBeanIf() {
			return SysMsg.this.toBeanIf();
		}

		@Override
		public long getTime() { // 
			_xdb_verify_unsafe_();
			return time;
		}

		@Override
		public int getMsgid() { // 
			_xdb_verify_unsafe_();
			return msgid;
		}

		@Override
		public java.util.List<String> getParams() { // 
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(params);
		}

		public java.util.List<String> getParamsAsData() { // 
			_xdb_verify_unsafe_();
			java.util.List<String> params;
			SysMsg _o_ = SysMsg.this;
		params = new java.util.LinkedList<String>();
		params.addAll(_o_.params);
			return params;
		}

		@Override
		public String getText() { // 
			_xdb_verify_unsafe_();
			return text;
		}

		@Override
		public com.goldhuman.Common.Octets getTextOctets() { // 
			_xdb_verify_unsafe_();
			return SysMsg.this.getTextOctets();
		}

		@Override
		public boolean getIsnew() { // 
			_xdb_verify_unsafe_();
			return isnew;
		}

		@Override
		public boolean getSended() { // 
			_xdb_verify_unsafe_();
			return sended;
		}

		@Override
		public long getSendroleid() { // 发送者id 系统-0
			_xdb_verify_unsafe_();
			return sendroleid;
		}

		@Override
		public int getMsgtype() { // 消息类型 0-系统 1-好友
			_xdb_verify_unsafe_();
			return msgtype;
		}

		@Override
		public void setTime(long _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setMsgid(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setText(String _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTextOctets(com.goldhuman.Common.Octets _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setIsnew(boolean _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setSended(boolean _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setSendroleid(long _v_) { // 发送者id 系统-0
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setMsgtype(int _v_) { // 消息类型 0-系统 1-好友
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
			return SysMsg.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return SysMsg.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return SysMsg.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return SysMsg.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return SysMsg.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return SysMsg.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return SysMsg.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return SysMsg.this.hashCode();
		}

		@Override
		public String toString() {
			return SysMsg.this.toString();
		}

	}

	public static final class Data implements xbean.SysMsg {
		private long time; // 
		private int msgid; // 
		private java.util.LinkedList<String> params; // 
		private String text; // 
		private boolean isnew; // 
		private boolean sended; // 
		private long sendroleid; // 发送者id 系统-0
		private int msgtype; // 消息类型 0-系统 1-好友

		public Data() {
			params = new java.util.LinkedList<String>();
			text = "";
			sended = false;
		}

		Data(xbean.SysMsg _o1_) {
			if (_o1_ instanceof SysMsg) assign((SysMsg)_o1_);
			else if (_o1_ instanceof SysMsg.Data) assign((SysMsg.Data)_o1_);
			else if (_o1_ instanceof SysMsg.Const) assign(((SysMsg.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(SysMsg _o_) {
			time = _o_.time;
			msgid = _o_.msgid;
			params = new java.util.LinkedList<String>();
			params.addAll(_o_.params);
			text = _o_.text;
			isnew = _o_.isnew;
			sended = _o_.sended;
			sendroleid = _o_.sendroleid;
			msgtype = _o_.msgtype;
		}

		private void assign(SysMsg.Data _o_) {
			time = _o_.time;
			msgid = _o_.msgid;
			params = new java.util.LinkedList<String>();
			params.addAll(_o_.params);
			text = _o_.text;
			isnew = _o_.isnew;
			sended = _o_.sended;
			sendroleid = _o_.sendroleid;
			msgtype = _o_.msgtype;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(time);
			_os_.marshal(msgid);
			_os_.compact_uint32(params.size());
			for (String _v_ : params) {
				_os_.marshal(_v_, xdb.Const.IO_CHARSET);
			}
			_os_.marshal(text, xdb.Const.IO_CHARSET);
			_os_.marshal(isnew);
			_os_.marshal(sended);
			_os_.marshal(sendroleid);
			_os_.marshal(msgtype);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			time = _os_.unmarshal_long();
			msgid = _os_.unmarshal_int();
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				String _v_ = "";
				_v_ = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
				params.add(_v_);
			}
			text = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			isnew = _os_.unmarshal_boolean();
			sended = _os_.unmarshal_boolean();
			sendroleid = _os_.unmarshal_long();
			msgtype = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.SysMsg copy() {
			return new Data(this);
		}

		@Override
		public xbean.SysMsg toData() {
			return new Data(this);
		}

		public xbean.SysMsg toBean() {
			return new SysMsg(this, null, null);
		}

		@Override
		public xbean.SysMsg toDataIf() {
			return this;
		}

		public xbean.SysMsg toBeanIf() {
			return new SysMsg(this, null, null);
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
		public long getTime() { // 
			return time;
		}

		@Override
		public int getMsgid() { // 
			return msgid;
		}

		@Override
		public java.util.List<String> getParams() { // 
			return params;
		}

		@Override
		public java.util.List<String> getParamsAsData() { // 
			return params;
		}

		@Override
		public String getText() { // 
			return text;
		}

		@Override
		public com.goldhuman.Common.Octets getTextOctets() { // 
			return com.goldhuman.Common.Octets.wrap(getText(), xdb.Const.IO_CHARSET);
		}

		@Override
		public boolean getIsnew() { // 
			return isnew;
		}

		@Override
		public boolean getSended() { // 
			return sended;
		}

		@Override
		public long getSendroleid() { // 发送者id 系统-0
			return sendroleid;
		}

		@Override
		public int getMsgtype() { // 消息类型 0-系统 1-好友
			return msgtype;
		}

		@Override
		public void setTime(long _v_) { // 
			time = _v_;
		}

		@Override
		public void setMsgid(int _v_) { // 
			msgid = _v_;
		}

		@Override
		public void setText(String _v_) { // 
			if (null == _v_)
				throw new NullPointerException();
			text = _v_;
		}

		@Override
		public void setTextOctets(com.goldhuman.Common.Octets _v_) { // 
			this.setText(_v_.getString(xdb.Const.IO_CHARSET));
		}

		@Override
		public void setIsnew(boolean _v_) { // 
			isnew = _v_;
		}

		@Override
		public void setSended(boolean _v_) { // 
			sended = _v_;
		}

		@Override
		public void setSendroleid(long _v_) { // 发送者id 系统-0
			sendroleid = _v_;
		}

		@Override
		public void setMsgtype(int _v_) { // 消息类型 0-系统 1-好友
			msgtype = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof SysMsg.Data)) return false;
			SysMsg.Data _o_ = (SysMsg.Data) _o1_;
			if (time != _o_.time) return false;
			if (msgid != _o_.msgid) return false;
			if (!params.equals(_o_.params)) return false;
			if (!text.equals(_o_.text)) return false;
			if (isnew != _o_.isnew) return false;
			if (sended != _o_.sended) return false;
			if (sendroleid != _o_.sendroleid) return false;
			if (msgtype != _o_.msgtype) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += time;
			_h_ += msgid;
			_h_ += params.hashCode();
			_h_ += text.hashCode();
			_h_ += isnew ? 1231 : 1237;
			_h_ += sended ? 1231 : 1237;
			_h_ += sendroleid;
			_h_ += msgtype;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(time);
			_sb_.append(",");
			_sb_.append(msgid);
			_sb_.append(",");
			_sb_.append(params);
			_sb_.append(",");
			_sb_.append("'").append(text).append("'");
			_sb_.append(",");
			_sb_.append(isnew);
			_sb_.append(",");
			_sb_.append(sended);
			_sb_.append(",");
			_sb_.append(sendroleid);
			_sb_.append(",");
			_sb_.append(msgtype);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
