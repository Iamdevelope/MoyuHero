package chuhan.gsp.item.trade;

public interface AbsTrade {

	/**
	 * 是否满足条件
	 * @return
	 */
	boolean canTrade();
	
	/**
	 * 执行交易扣除条件
	 * @return
	 */
	boolean trade();
	
}
