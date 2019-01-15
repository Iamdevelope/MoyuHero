
package xbean;

public interface LotteryItemlayer extends xdb.Bean {
	public LotteryItemlayer copy(); // deep clone
	public LotteryItemlayer toData(); // a Data instance
	public LotteryItemlayer toBean(); // a Bean instance
	public LotteryItemlayer toDataIf(); // a Data instance If need. else return this
	public LotteryItemlayer toBeanIf(); // a Bean instance If need. else return this

	public java.util.List<xbean.LotteryItem> getLotteryitemlist(); // 遗迹宝藏每层list
	public java.util.List<xbean.LotteryItem> getLotteryitemlistAsData(); // 遗迹宝藏每层list

}
