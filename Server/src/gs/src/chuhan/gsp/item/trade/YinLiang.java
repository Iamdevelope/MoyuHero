package chuhan.gsp.item.trade;

import chuhan.gsp.item.Bag;

/**
 * 需求银两
 */
public class YinLiang implements AbsTrade {
	private final chuhan.gsp.item.strade strade;
	private final Bag bag;

	public YinLiang(chuhan.gsp.item.strade strade, Bag bag) {
		this.bag = bag;
		this.strade = strade;
	}

	@Override
	public boolean canTrade() {
		if(bag.getMoney() < strade.getYinliang()) {
			return false;
		}
		return true;
	}

	@Override
	public boolean trade() {
		bag.addMoney(-strade.getYinliang(), "trade_del");//扣钱
		return true;
	}

}
