package chuhan.gsp.item.trade;

import chuhan.gsp.attr.PropRole;

/**
 * 有VIP等级限制
 */
public class Vip implements AbsTrade {
	private final chuhan.gsp.item.strade strade;
	private final PropRole propRole;

	public Vip(chuhan.gsp.item.strade strade, PropRole propRole) {
		this.strade = strade;
		this.propRole = propRole;
	}

	@Override
	public boolean canTrade() {
		if(propRole.getVipLevel() < strade.getViplevel()) {
			return false;
		}
		return true;
	}

	@Override
	public boolean trade() {
		return true;
	}

}
