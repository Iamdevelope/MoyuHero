
package xbean;

public interface Mail extends xdb.Bean {
	public Mail copy(); // deep clone
	public Mail toData(); // a Data instance
	public Mail toBean(); // a Bean instance
	public Mail toDataIf(); // a Data instance If need. else return this
	public Mail toBeanIf(); // a Bean instance If need. else return this

	public int getKey(); // 邮件唯一ID
	public String getSender(); // 发送者
	public com.goldhuman.Common.Octets getSenderOctets(); // 发送者
	public String getTitle(); // 邮件标题
	public com.goldhuman.Common.Octets getTitleOctets(); // 邮件标题
	public String getMsg(); // 消息内容
	public com.goldhuman.Common.Octets getMsgOctets(); // 消息内容
	public java.util.List<Integer> getInnerdropidlist(); // 掉落包ID
	public java.util.List<Integer> getInnerdropidlistAsData(); // 掉落包ID
	public java.util.List<xbean.MailItem> getItems(); // 掉落物品（非掉落包内容）
	public java.util.List<xbean.MailItem> getItemsAsData(); // 掉落物品（非掉落包内容）
	public long getEndtime(); // 结束时间
	public int getIsopen(); // 是否打开过 0未打开，1已打开
	public java.util.List<String> getStrlist(); // 参数列表
	public java.util.List<String> getStrlistAsData(); // 参数列表

	public void setKey(int _v_); // 邮件唯一ID
	public void setSender(String _v_); // 发送者
	public void setSenderOctets(com.goldhuman.Common.Octets _v_); // 发送者
	public void setTitle(String _v_); // 邮件标题
	public void setTitleOctets(com.goldhuman.Common.Octets _v_); // 邮件标题
	public void setMsg(String _v_); // 消息内容
	public void setMsgOctets(com.goldhuman.Common.Octets _v_); // 消息内容
	public void setEndtime(long _v_); // 结束时间
	public void setIsopen(int _v_); // 是否打开过 0未打开，1已打开
}
