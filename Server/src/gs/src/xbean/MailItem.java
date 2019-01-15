
package xbean;

public interface MailItem extends xdb.Bean {
	public MailItem copy(); // deep clone
	public MailItem toData(); // a Data instance
	public MailItem toBean(); // a Bean instance
	public MailItem toDataIf(); // a Data instance If need. else return this
	public MailItem toBeanIf(); // a Bean instance If need. else return this

	public int getObjectid(); // 物品ID
	public int getDropnum(); // 数量
	public int getDropparameter1(); // 附加条件1
	public int getDropparameter2(); // 附加条件2

	public void setObjectid(int _v_); // 物品ID
	public void setDropnum(int _v_); // 数量
	public void setDropparameter1(int _v_); // 附加条件1
	public void setDropparameter2(int _v_); // 附加条件2
}
