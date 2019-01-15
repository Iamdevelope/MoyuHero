
package xbean;

public interface NewShop extends xdb.Bean {
	public NewShop copy(); // deep clone
	public NewShop toData(); // a Data instance
	public NewShop toBean(); // a Bean instance
	public NewShop toDataIf(); // a Data instance If need. else return this
	public NewShop toBeanIf(); // a Bean instance If need. else return this

	public int getItemid(); // 77表的道具ID
	public int getCosttype(); // 消耗资源
	public int getPrice(); // 价格
	public int getNum(); // 数量
	public int getIsbuy(); // 0未购买，1为已购买

	public void setItemid(int _v_); // 77表的道具ID
	public void setCosttype(int _v_); // 消耗资源
	public void setPrice(int _v_); // 价格
	public void setNum(int _v_); // 数量
	public void setIsbuy(int _v_); // 0未购买，1为已购买
}
