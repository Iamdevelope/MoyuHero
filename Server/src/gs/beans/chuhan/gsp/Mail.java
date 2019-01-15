
package chuhan.gsp;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

/** 邮件 by yanglk
*/
public class Mail implements Marshal {
	public int key; // 邮件唯一ID
	public java.lang.String sender; // 发送者
	public java.lang.String title; // 邮件标题
	public java.lang.String msg; // 消息内容
	public java.util.LinkedList<Integer> innerdropidlist; // 掉落包ID
	public java.util.LinkedList<chuhan.gsp.MailItem> items; // 掉落物品（非掉落包内容）
	public long begintime; // 开始时间
	public int isopen; // 个位为是否打开，十位为是否领取 0否，1是
	public java.util.LinkedList<java.lang.String> strlist; // 参数列表

	public Mail() {
		sender = "";
		title = "";
		msg = "";
		innerdropidlist = new java.util.LinkedList<Integer>();
		items = new java.util.LinkedList<chuhan.gsp.MailItem>();
		strlist = new java.util.LinkedList<java.lang.String>();
	}

	public Mail(int _key_, java.lang.String _sender_, java.lang.String _title_, java.lang.String _msg_, java.util.LinkedList<Integer> _innerdropidlist_, java.util.LinkedList<chuhan.gsp.MailItem> _items_, long _begintime_, int _isopen_, java.util.LinkedList<java.lang.String> _strlist_) {
		this.key = _key_;
		this.sender = _sender_;
		this.title = _title_;
		this.msg = _msg_;
		this.innerdropidlist = _innerdropidlist_;
		this.items = _items_;
		this.begintime = _begintime_;
		this.isopen = _isopen_;
		this.strlist = _strlist_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.MailItem _v_ : items)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(key);
		_os_.marshal(sender, "UTF-16LE");
		_os_.marshal(title, "UTF-16LE");
		_os_.marshal(msg, "UTF-16LE");
		_os_.compact_uint32(innerdropidlist.size());
		for (Integer _v_ : innerdropidlist) {
			_os_.marshal(_v_);
		}
		_os_.compact_uint32(items.size());
		for (chuhan.gsp.MailItem _v_ : items) {
			_os_.marshal(_v_);
		}
		_os_.marshal(begintime);
		_os_.marshal(isopen);
		_os_.compact_uint32(strlist.size());
		for (java.lang.String _v_ : strlist) {
			_os_.marshal(_v_, "UTF-16LE");
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		key = _os_.unmarshal_int();
		sender = _os_.unmarshal_String("UTF-16LE");
		title = _os_.unmarshal_String("UTF-16LE");
		msg = _os_.unmarshal_String("UTF-16LE");
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			innerdropidlist.add(_v_);
		}
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.MailItem _v_ = new chuhan.gsp.MailItem();
			_v_.unmarshal(_os_);
			items.add(_v_);
		}
		begintime = _os_.unmarshal_long();
		isopen = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			java.lang.String _v_;
			_v_ = _os_.unmarshal_String("UTF-16LE");
			strlist.add(_v_);
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof Mail) {
			Mail _o_ = (Mail)_o1_;
			if (key != _o_.key) return false;
			if (!sender.equals(_o_.sender)) return false;
			if (!title.equals(_o_.title)) return false;
			if (!msg.equals(_o_.msg)) return false;
			if (!innerdropidlist.equals(_o_.innerdropidlist)) return false;
			if (!items.equals(_o_.items)) return false;
			if (begintime != _o_.begintime) return false;
			if (isopen != _o_.isopen) return false;
			if (!strlist.equals(_o_.strlist)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += key;
		_h_ += sender.hashCode();
		_h_ += title.hashCode();
		_h_ += msg.hashCode();
		_h_ += innerdropidlist.hashCode();
		_h_ += items.hashCode();
		_h_ += (int)begintime;
		_h_ += isopen;
		_h_ += strlist.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(key).append(",");
		_sb_.append("T").append(sender.length()).append(",");
		_sb_.append("T").append(title.length()).append(",");
		_sb_.append("T").append(msg.length()).append(",");
		_sb_.append(innerdropidlist).append(",");
		_sb_.append(items).append(",");
		_sb_.append(begintime).append(",");
		_sb_.append(isopen).append(",");
		_sb_.append(strlist).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

