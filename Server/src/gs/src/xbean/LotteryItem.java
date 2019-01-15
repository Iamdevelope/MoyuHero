
package xbean;

public interface LotteryItem extends xdb.Bean {
	public LotteryItem copy(); // deep clone
	public LotteryItem toData(); // a Data instance
	public LotteryItem toBean(); // a Bean instance
	public LotteryItem toDataIf(); // a Data instance If need. else return this
	public LotteryItem toBeanIf(); // a Bean instance If need. else return this

	public int getId(); // 遗迹宝藏ID
	public int getIsget(); // 是否领取
	public int getViewnum(); // 显示位置
	public int getSuperid(); // 激活的特殊事件

	public void setId(int _v_); // 遗迹宝藏ID
	public void setIsget(int _v_); // 是否领取
	public void setViewnum(int _v_); // 显示位置
	public void setSuperid(int _v_); // 激活的特殊事件
}
