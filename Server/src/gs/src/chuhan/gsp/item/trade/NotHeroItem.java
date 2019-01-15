package chuhan.gsp.item.trade;

import chuhan.gsp.item.BasicItem;
import chuhan.gsp.item.EquipColumn;
import chuhan.gsp.item.ItemColumn;
import chuhan.gsp.item.Module;
import chuhan.gsp.item.types.EquipItem;

/**
 * 道具-非武将需求
 *
 */
public class NotHeroItem extends AbsItemlist {
	private ItemColumn itemColumn;

	public NotHeroItem(chuhan.gsp.item.strade strade, int useitemkey,
			byte useitembagtype, long roleId) {
		super(strade, useitemkey, useitembagtype, roleId);
		itemColumn = Module.getItemColumn(roleId, useitembagtype, false);
	}

	@Override
	protected boolean itemRankIsOk() {
		if(itemColumn instanceof EquipColumn) {
			BasicItem basicItem = itemColumn.getItem(useitemkey);
			if(basicItem instanceof EquipItem) {
				EquipItem equipItem = (EquipItem) basicItem;
//				if(equipItem.getGrade() != strade.getItemrank()) {
//					return false;
//				}
			}
		}
		
		return true;
	}

	@Override
	protected boolean itemNumIsOk() {
		boolean isAt = false;
		for(int itemId : sItemList.items) {
			if(itemId == itemColumn.getItem(useitemkey).getItemid()) {//该道具在道具组里
				isAt = true;
				break;
			}
		}
		if(!isAt) {//该道具没在道具组里
			return false;
		}
		if(itemColumn.getItem(useitemkey).getNumber() < strade.getItemnum()) {
			return false;
		}
		
		return true;
	}

	@Override
	public boolean trade() {
		if(strade.getItemnum() != itemColumn.removeItemByKey(useitemkey, strade.getItemnum(), 
				1, "trade_del")) {
			return false;
		}
		return true;
	}

}
