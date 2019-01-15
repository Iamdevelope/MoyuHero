
package xbean;

public interface mohe extends xdb.Bean {
	public mohe copy(); // deep clone
	public mohe toData(); // a Data instance
	public mohe toBean(); // a Bean instance
	public mohe toDataIf(); // a Data instance If need. else return this
	public mohe toBeanIf(); // a Bean instance If need. else return this

	public int getId(); // id
	public int getIsopen(); // 是否开启（1开启，0未开启）
	public int getPlace(); // 排序（0为随机排序，123为正常排序）

	public void setId(int _v_); // id
	public void setIsopen(int _v_); // 是否开启（1开启，0未开启）
	public void setPlace(int _v_); // 排序（0为随机排序，123为正常排序）
}
