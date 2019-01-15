package chuhan.gsp.item.trade;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.YuanBaoConsumeType;

/**
 * 需求元宝
 */
public class YuanBao implements AbsTrade {
	private final PropRole propRole;
	private final chuhan.gsp.item.strade strade;

	public YuanBao(chuhan.gsp.item.strade strade, PropRole propRole) {
		this.propRole = propRole;
		this.strade = strade;
	}

	@Override
	public boolean canTrade() {
		if(propRole.getYuanBao() < strade.getYuanbao()) {
			return false;
		}
		return true;
	}

	@Override
	public boolean trade() {
		propRole.delYuanBao(-strade.getYuanbao(), YuanBaoConsumeType.TRADE);//扣元宝
		return true;
	}

}
