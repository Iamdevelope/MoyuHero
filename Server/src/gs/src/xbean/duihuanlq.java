
package xbean;

public interface duihuanlq extends xdb.Bean {
	public duihuanlq copy(); // deep clone
	public duihuanlq toData(); // a Data instance
	public duihuanlq toBean(); // a Bean instance
	public duihuanlq toDataIf(); // a Data instance If need. else return this
	public duihuanlq toBeanIf(); // a Bean instance If need. else return this

	public int getLqkey(); // 兑换礼券key
	public int getTypenum(); // 兑换礼券替换计数
	public java.util.List<String> getClonelist(); // 
	public java.util.List<String> getClonelistAsData(); // 

	public void setLqkey(int _v_); // 兑换礼券key
	public void setTypenum(int _v_); // 兑换礼券替换计数
}
