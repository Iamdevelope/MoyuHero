package chuhan.gsp.item.trade;

import chuhan.gsp.item.SItemList;
import chuhan.gsp.item.strade;
import chuhan.gsp.main.ConfigManager;

/**
 * 道具组需求
 *
 */
public abstract class AbsItemlist implements AbsTrade {
	protected final strade strade;
	protected final int useitemkey;
	protected final int useitembagtype;
	protected final SItemList sItemList;
	protected final long roleId;
	
	public AbsItemlist(chuhan.gsp.item.strade strade, int useitemkey, byte useitembagtype,
			long roleId) {
		this.strade = strade;
		this.roleId = roleId;
		this.useitembagtype = useitembagtype;
		this.useitemkey = useitemkey;
		sItemList = ConfigManager.getInstance().getConf(SItemList.class).get(strade.getItemlist());
	}

	/**
	 * 道具的阶数要求是否满足
	 * @return
	 */
	protected abstract boolean itemRankIsOk();
	
	/**
	 * 道具的数量要求是否满足
	 * @return
	 */
	protected abstract boolean itemNumIsOk();
	
	@Override
	public boolean canTrade() {
		if(itemNumIsOk() && itemRankIsOk()) {
			return true;
		}
		
		return false;
	}
	
	public static AbsItemlist getTradeItemlist(chuhan.gsp.item.strade strade, int useitemkey, byte useitembagtype,
			long roleId) {
		if(useitembagtype == 0) {//表示是武将
			return new HeroItem(strade, useitemkey, useitembagtype, roleId);
		} else {
			return new NotHeroItem(strade, useitemkey, useitembagtype, roleId);
		}
	}
	
}
