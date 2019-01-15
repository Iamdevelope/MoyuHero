
package xbean;

public interface huoyue extends xdb.Bean {
	public huoyue copy(); // deep clone
	public huoyue toData(); // a Data instance
	public huoyue toBean(); // a Bean instance
	public huoyue toDataIf(); // a Data instance If need. else return this
	public huoyue toBeanIf(); // a Bean instance If need. else return this

	public int getHuoyueid(); // 活跃id
	public int getNum(); // 发生次数
	public int getNumall(); // 总次数
	public int getHuoyuetype(); // 任务类型
	public int getIsok(); // 是否完成

	public void setHuoyueid(int _v_); // 活跃id
	public void setNum(int _v_); // 发生次数
	public void setNumall(int _v_); // 总次数
	public void setHuoyuetype(int _v_); // 任务类型
	public void setIsok(int _v_); // 是否完成
}
