
package xbean;

public interface AppReceiptData extends xdb.Bean {
	public AppReceiptData copy(); // deep clone
	public AppReceiptData toData(); // a Data instance
	public AppReceiptData toBean(); // a Bean instance
	public AppReceiptData toDataIf(); // a Data instance If need. else return this
	public AppReceiptData toBeanIf(); // a Bean instance If need. else return this

	public long getRoleid(); // 
	public String getReceipt(); // 苹果账单
	public com.goldhuman.Common.Octets getReceiptOctets(); // 苹果账单

	public void setRoleid(long _v_); // 
	public void setReceipt(String _v_); // 苹果账单
	public void setReceiptOctets(com.goldhuman.Common.Octets _v_); // 苹果账单
}
