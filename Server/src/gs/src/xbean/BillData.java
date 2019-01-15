
package xbean;

public interface BillData extends xdb.Bean {
	public BillData copy(); // deep clone
	public BillData toData(); // a Data instance
	public BillData toBean(); // a Bean instance
	public BillData toDataIf(); // a Data instance If need. else return this
	public BillData toBeanIf(); // a Bean instance If need. else return this

	public final static int STATE_SENDED = 1; // 已通知客户端
	public final static int STATE_CONFIRMED = 2; // 已确认并且发放
	public final static int STATE_FAILED = 4; // 确认失败的

	public long getBillid(); // 
	public int getGoodid(); // 
	public int getGoodnum(); // 
	public int getPresent(); // 
	public float getPrice(); // 总价格
	public long getCreatetime(); // 创建时间
	public int getState(); // 
	public int getConfirmtimes(); // 向au确认订单的次数
	public String getPlatbillid(); // 平台生成的订单号
	public com.goldhuman.Common.Octets getPlatbillidOctets(); // 平台生成的订单号

	public void setBillid(long _v_); // 
	public void setGoodid(int _v_); // 
	public void setGoodnum(int _v_); // 
	public void setPresent(int _v_); // 
	public void setPrice(float _v_); // 总价格
	public void setCreatetime(long _v_); // 创建时间
	public void setState(int _v_); // 
	public void setConfirmtimes(int _v_); // 向au确认订单的次数
	public void setPlatbillid(String _v_); // 平台生成的订单号
	public void setPlatbillidOctets(com.goldhuman.Common.Octets _v_); // 平台生成的订单号
}
