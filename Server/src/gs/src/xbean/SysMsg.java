
package xbean;

public interface SysMsg extends xdb.Bean {
	public SysMsg copy(); // deep clone
	public SysMsg toData(); // a Data instance
	public SysMsg toBean(); // a Bean instance
	public SysMsg toDataIf(); // a Data instance If need. else return this
	public SysMsg toBeanIf(); // a Bean instance If need. else return this

	public long getTime(); // 
	public int getMsgid(); // 
	public java.util.List<String> getParams(); // 
	public java.util.List<String> getParamsAsData(); // 
	public String getText(); // 
	public com.goldhuman.Common.Octets getTextOctets(); // 
	public boolean getIsnew(); // 
	public boolean getSended(); // 
	public long getSendroleid(); // 发送者id 系统-0
	public int getMsgtype(); // 消息类型 0-系统 1-好友

	public void setTime(long _v_); // 
	public void setMsgid(int _v_); // 
	public void setText(String _v_); // 
	public void setTextOctets(com.goldhuman.Common.Octets _v_); // 
	public void setIsnew(boolean _v_); // 
	public void setSended(boolean _v_); // 
	public void setSendroleid(long _v_); // 发送者id 系统-0
	public void setMsgtype(int _v_); // 消息类型 0-系统 1-好友
}
