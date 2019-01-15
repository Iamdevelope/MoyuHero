package chuhan.gsp.item.types;

import java.util.Map;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.award.AwardItem;
import chuhan.gsp.award.AwardManager;
import chuhan.gsp.item.Bag;
import chuhan.gsp.item.item26;
import chuhan.gsp.item.UseResult;
import chuhan.gsp.msg.Message;


public class KeyItem extends NormalItem{
	KeyItem(int itemid) {
		super(itemid);
	}
	KeyItem(xbean.Item item) {
		super(item);
	}
	@Override
	public UseResult use(long roleId, int num, int dstkey) {
		
		int chestid = 0;
		item26 chestattr = chuhan.gsp.item.Module.getInstance().getAttr(chestid);
		if(chestattr == null)
			return UseResult.FAIL;
		if(!chestattr.getClassname().equals(ChestItem.class.getName()))
			return UseResult.FAIL;
		
		if(xtable.Properties.get(roleId).getLevel() <0)
		{
			//Message.psendMsgNotify(roleId, 51, chestattr.getPar3());
			//return UseResult.FAIL;
		}
		PropRole prole = PropRole.getPropRole(roleId, false);
		//消耗对应的钥匙，打开箱子
		Bag bag = new Bag(roleId, false);
		if (bag.removeItemById(1, 1, 1, "open_chest") != 1) {
			Message.psendMsgNotify(roleId, 131);
			return UseResult.FAIL;
		}
		ChestItem.addOpenChestValue(prole, getItemid());
		boolean firstaward = false;
		int awardid = ChestItem.getFirstAwardId(prole, getAttr());
		if(awardid <= 0)
			awardid = ChestItem.getSecondAwardId(prole, getAttr());
		else 
			firstaward = true;
		if(awardid <= 0)
			awardid = 0;
		Map<Integer,AwardItem> awards = AwardManager.getInstance().distributeAllAward(roleId, awardid, null, true);
		ChestItem.addGetGoodValue(prole, awards,firstaward);
		return UseResult.SUCC;
	}
}
