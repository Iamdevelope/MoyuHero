
package xbean;

public interface Buff extends xdb.Bean {
	public Buff copy(); // deep clone
	public Buff toData(); // a Data instance
	public Buff toBean(); // a Bean instance
	public Buff toDataIf(); // a Data instance If need. else return this
	public Buff toBeanIf(); // a Bean instance If need. else return this

	public int getId(); // buff类型Id，一种类型的buff只能有一个
	public long getAttachtime(); // buff attach时的时间，用于计算剩余时间和检测到期
	public long getTime(); // 计时buff总持续时间（period时的period）
	public int getRound(); // 计数buff剩余回合（period时的count）
	public long getAmount(); // buff的剩余量（period时的initDelay）
	public java.util.Map<Integer, Float> getEffects(); // key = effect type id
	public java.util.Map<Integer, Float> getEffectsAsData(); // key = effect type id
	public java.util.Map<Integer, Float> getExtdata(); // 额外数据，由buff实现者自己定义和使用
	public java.util.Map<Integer, Float> getExtdataAsData(); // 额外数据，由buff实现者自己定义和使用

	public void setId(int _v_); // buff类型Id，一种类型的buff只能有一个
	public void setAttachtime(long _v_); // buff attach时的时间，用于计算剩余时间和检测到期
	public void setTime(long _v_); // 计时buff总持续时间（period时的period）
	public void setRound(int _v_); // 计数buff剩余回合（period时的count）
	public void setAmount(long _v_); // buff的剩余量（period时的initDelay）
}
