package chuhan.gsp.item.types;


import java.util.List;

import xbean.ItemNumLimit;
import chuhan.gsp.award.DropManager;
import chuhan.gsp.item.BagTypes;
import chuhan.gsp.item.Module;
import chuhan.gsp.item.SShowGiftItem;
import chuhan.gsp.item.UseResult;
import chuhan.gsp.log.LogBehavior;
import chuhan.gsp.play.activity.ActivityManager;


/**
 * 魂魄
 * @author liuchen
 *
 */
public class PieceItem extends NormalItem{

	PieceItem(int itemid) {
		super(itemid);
	}
	PieceItem(xbean.Item item) {
		super(item);
	}
	
	@Override
	public UseResult use(long roleId, int num, int dstkey) {
		
		Integer  dropPackId = getAttr().getDropPackId();
		List<Integer> itemlist = DropManager.getInstance().drop(roleId,dropPackId.toString(),LogBehavior.PIECEUSEITEM);
		if(itemlist == null || itemlist.size() == 0){
			return UseResult.FAIL;
		}
		
		if(itemlist.size() > 0  ){	//使用成功
			
			// 显示礼包获得ID
			SShowGiftItem giftinfo = new SShowGiftItem();
			giftinfo.giftitems.addAll(itemlist);
			xdb.Procedure.psendWhileCommit(roleId, giftinfo);
			
			ActivityManager.getInstance().addMsgNotice(roleId,dataItem.getId(),ActivityManager.DAOJU,"");
		}
		return UseResult.SUCC;
	}
	
}
