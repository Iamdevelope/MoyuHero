package chuhan.gsp.item.trade;

import chuhan.gsp.hero.OldHero;
import chuhan.gsp.hero.OldHeroColumn;
import chuhan.gsp.msg.Message;

/**
 * 道具-武将需求
 *
 */
public class HeroItem extends AbsItemlist {
	private OldHeroColumn heroColumn;
	private OldHero hero;

	public HeroItem(chuhan.gsp.item.strade strade, int useitemkey,
			byte useitembagtype, long roleId) {
		super(strade, useitemkey, useitembagtype, roleId);
		heroColumn = OldHeroColumn.getHeroColumn(roleId, false);
		hero = heroColumn.getHero(useitemkey);
	}

	@Override
	protected boolean itemRankIsOk() {
		if(null == hero) {
			return false;//没有这个武将
		}
		if(strade.getConvert() == 1) {//要折算养成进度就不判断阶了
			return true;
		}
		//by yanglk  hero		if(hero.getGrade() != strade.getItemrank()) {
		//by yanglk  hero			return false;
		//by yanglk  hero		}
		
		return true;
	}

	@Override
	protected boolean itemNumIsOk() {
		if(null == hero) {
			return false;//没有这个武将
		}
		for(int itemId : sItemList.items) {
			//by yanglk  hero			if(itemId == hero.getId()) {//该武将在道具组里
			//by yanglk  hero				return true;
			//by yanglk  hero			}
		}
		
		return false;
	}

	@Override
	public boolean trade() {
		//by yanglk  hero		if(null == heroColumn.removeHero(useitemkey)) {
		//by yanglk  hero			Message.psendMsgNotify(roleId, 89, hero.getConfig().name);
		//by yanglk  hero			return false;
		//by yanglk  hero		}
		return true;
	}
}
