
package xbean;

public interface smshopdata extends xdb.Bean {
	public smshopdata copy(); // deep clone
	public smshopdata toData(); // a Data instance
	public smshopdata toBean(); // a Bean instance
	public smshopdata toDataIf(); // a Data instance If need. else return this
	public smshopdata toBeanIf(); // a Bean instance If need. else return this

	public int getId(); // id
	public int getIsopen(); // 是否购买（1购买，0未购买）
	public int getPrice(); // 价格

	public void setId(int _v_); // id
	public void setIsopen(int _v_); // 是否购买（1购买，0未购买）
	public void setPrice(int _v_); // 价格
}
