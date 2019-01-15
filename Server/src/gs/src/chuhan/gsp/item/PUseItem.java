package chuhan.gsp.item;

import chuhan.gsp.item.types.PieceItem;
import chuhan.gsp.msg.Message;

public class PUseItem extends xdb.Procedure{
	private final long roleId;
	private final int bagid;
	private final int itemkey;
	private int num;
	private final int dstkey;//可能为英雄，或其他物品，由物品内部使用的模块去判断
	public PUseItem(long roleId, int bagid, int itemkey, int num, int dstkey) {
		this.roleId = roleId;
		this.bagid = bagid;
		this.itemkey = itemkey;
		this.num = num;
		this.dstkey = dstkey;
	}
	
	
	@Override
	protected boolean process() throws Exception {
		
		if(bagid != BagTypes.BAG 
				&& bagid != BagTypes.SOUL
				&& bagid != BagTypes.COLLECTION
				&& bagid != BagTypes.EQUIP){
			Message.psendMsgNotify(roleId, 135);
			return false;//其他包裹暂时不让用
		}
		
		ItemColumn itemcol = Module.getItemColumn(roleId, bagid, false);
		BasicItem item = itemcol.getItem(itemkey);
		if(item == null){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
			
		
		if(item instanceof PieceItem)
		{
			num = 1;
			if(num <=0){
				Message.psendMsgNotify(roleId, 135);
				return false;
			}
			if(item.getNumber() < num)
			{
				Message.psendMsgNotify(roleId, 218, num);
				return false;
			}
		}
		
		if(item.getNumber() < num && bagid != BagTypes.EQUIP){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		
		if( item.dailyLimit(roleId) ){
			
			Message.psendMsgNotify(roleId, 219, 0);
			return false;//当天使用被限制
		}
		
		UseResult result = item.use(roleId, num, dstkey);
		if(result == UseResult.FAIL){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
			
		if( bagid != BagTypes.EQUIP ){
			if(itemcol.removeItemByKey(itemkey, num, 1, "use")!= num){
				Message.psendMsgNotify(roleId, 135);
				return false;
			}
		}
		
		return true;
	}
	
}
