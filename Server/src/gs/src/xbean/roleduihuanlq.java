
package xbean;

public interface roleduihuanlq extends xdb.Bean {
	public roleduihuanlq copy(); // deep clone
	public roleduihuanlq toData(); // a Data instance
	public roleduihuanlq toBean(); // a Bean instance
	public roleduihuanlq toDataIf(); // a Data instance If need. else return this
	public roleduihuanlq toBeanIf(); // a Bean instance If need. else return this

	public int getLqkey(); // 兑换礼券key
	public int getTypenum(); // 兑换礼券替换计数
	public int getNum(); // 兑换礼券计数

	public void setLqkey(int _v_); // 兑换礼券key
	public void setTypenum(int _v_); // 兑换礼券替换计数
	public void setNum(int _v_); // 兑换礼券计数
}
