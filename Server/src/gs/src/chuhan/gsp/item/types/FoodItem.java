package chuhan.gsp.item.types;

import java.util.List;

import xbean.ItemNumLimit;

import com.goldhuman.Common.Octets;

import chuhan.gsp.award.DropManager;
import chuhan.gsp.item.BagTypes;
import chuhan.gsp.item.BasicItem;
import chuhan.gsp.item.Module;
import chuhan.gsp.item.SRefreshItem;
import chuhan.gsp.item.UseResult;
import chuhan.gsp.log.LogBehavior;
import chuhan.gsp.play.activity.ActivityManager;
import chuhan.gsp.util.Conv;
/**
 * 食品，回体力活力
 * @author liuchen
 *
 */
public class FoodItem extends BasicItem{

	FoodItem(int itemid) {
		super(itemid);
	}
	FoodItem(xbean.Item item) {
		super(item);
	}

	@Override
	protected void afterInsert() {
		// TODO Auto-generated method stub
		
	}

	@Override
	protected void afterDelete() {
		// TODO Auto-generated method stub
		
	}
	@Override
	public Octets getExtdataOctets() {
		return new Octets();
	}
	@Override
	public UseResult use(long roleId, int num, int dstkey) {
		
		Integer  dropPackId = getAttr().getDropPackId();
		List<Integer> itemlist = DropManager.getInstance().drop(roleId,dropPackId.toString(),LogBehavior.FOODUSEITEM);
		if( (itemlist == null || itemlist.size() == 0) && getAttr().getId() != 1402030009){
			return UseResult.FAIL;
		}
		
		if(itemlist.size() > 0 || getAttr().getId() == 1402030009 ){	//使用成功
			
			//添加数量
			Module.addUseNum(roleId,dataItem.getId());
			
			SRefreshItem snd = new SRefreshItem();
			snd.bagid = Conv.toByte(BagTypes.BAG);
			snd.data = getProtocolItem();
			xdb.Procedure.psendWhileCommit(roleId, snd);
			
			ActivityManager.getInstance().addMsgNotice(roleId,dataItem.getId(),ActivityManager.DAOJU,"");
	
		}

		return UseResult.SUCC;
	}
	
}
