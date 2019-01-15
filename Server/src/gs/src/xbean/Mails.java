
package xbean;

public interface Mails extends xdb.Bean {
	public Mails copy(); // deep clone
	public Mails toData(); // a Data instance
	public Mails toBean(); // a Bean instance
	public Mails toDataIf(); // a Data instance If need. else return this
	public Mails toBeanIf(); // a Bean instance If need. else return this

	public java.util.List<xbean.Mail> getMails(); // 
	public java.util.List<xbean.Mail> getMailsAsData(); // 
	public int getNextkey(); // 

	public void setNextkey(int _v_); // 
}
