
package xbean;

public interface GoogleReceiptData extends xdb.Bean {
	public GoogleReceiptData copy(); // deep clone
	public GoogleReceiptData toData(); // a Data instance
	public GoogleReceiptData toBean(); // a Bean instance
	public GoogleReceiptData toDataIf(); // a Data instance If need. else return this
	public GoogleReceiptData toBeanIf(); // a Bean instance If need. else return this

	public long getRoleid(); // 
	public String getPackagename(); // 
	public com.goldhuman.Common.Octets getPackagenameOctets(); // 
	public String getProductid(); // 
	public com.goldhuman.Common.Octets getProductidOctets(); // 
	public String getToken(); // 
	public com.goldhuman.Common.Octets getTokenOctets(); // 

	public void setRoleid(long _v_); // 
	public void setPackagename(String _v_); // 
	public void setPackagenameOctets(com.goldhuman.Common.Octets _v_); // 
	public void setProductid(String _v_); // 
	public void setProductidOctets(com.goldhuman.Common.Octets _v_); // 
	public void setToken(String _v_); // 
	public void setTokenOctets(com.goldhuman.Common.Octets _v_); // 
}
