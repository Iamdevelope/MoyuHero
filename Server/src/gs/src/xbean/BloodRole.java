
package xbean;

public interface BloodRole extends xdb.Bean {
	public BloodRole copy(); // deep clone
	public BloodRole toData(); // a Data instance
	public BloodRole toBean(); // a Bean instance
	public BloodRole toDataIf(); // a Data instance If need. else return this
	public BloodRole toBeanIf(); // a Bean instance If need. else return this

	public int getCurlevel(); // 当前层数
	public int getLasthard(); // 上一次战斗的难度
	public int getCurstar(); // 剩余没用的星
	public int getBattle1(); // 随机出的战斗
	public int getBattle2(); // 
	public int getBattle3(); // 
	public int getItemlevel(); // 已经获得的物品等级
	public java.util.Map<Integer, Float> getEffects(); // 以前已加成的效果
	public java.util.Map<Integer, Float> getEffectsAsData(); // 以前已加成的效果
	public int getFailed(); // 1已失败
	public int getRelivetimes(); // 今天已复活次数
	public long getLastfighttime(); // 上次战斗时间
	public int getTotalstar(); // 累计星
	public int getMaxlevel(); // 最高层
	public java.util.Map<Integer, Integer> getRepeatstaraward(); // 
	public java.util.Map<Integer, Integer> getRepeatstarawardAsData(); // 
	public java.util.Map<Integer, Integer> getFixstaraward(); // 
	public java.util.Map<Integer, Integer> getFixstarawardAsData(); // 

	public void setCurlevel(int _v_); // 当前层数
	public void setLasthard(int _v_); // 上一次战斗的难度
	public void setCurstar(int _v_); // 剩余没用的星
	public void setBattle1(int _v_); // 随机出的战斗
	public void setBattle2(int _v_); // 
	public void setBattle3(int _v_); // 
	public void setItemlevel(int _v_); // 已经获得的物品等级
	public void setFailed(int _v_); // 1已失败
	public void setRelivetimes(int _v_); // 今天已复活次数
	public void setLastfighttime(long _v_); // 上次战斗时间
	public void setTotalstar(int _v_); // 累计星
	public void setMaxlevel(int _v_); // 最高层
}
