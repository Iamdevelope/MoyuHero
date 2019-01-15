package chuhan.gsp.item;

import java.util.Map;

import chuhan.gsp.DataInit;
import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.player03;
import chuhan.gsp.award.DropManager;
import chuhan.gsp.game.svipconfig;
import chuhan.gsp.item.types.EquipItem;
import chuhan.gsp.log.LogBehavior;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.Message;
import chuhan.gsp.play.activity.ActivityGameManager;
import chuhan.gsp.play.activity.ActivityManager;
import chuhan.gsp.play.huoyuedu.HuoyueColumns;
import chuhan.gsp.util.Conv;
import chuhan.gsp.util.DateUtil;
import chuhan.gsp.util.Misc;

/**
 * 强化
 *
 */
public class PLevelUpEquip extends xdb.Procedure
{
	private final long roleId;
	private final int equipkey;
	public PLevelUpEquip(long roleId, int equipkey) {
		this.roleId = roleId;
		this.equipkey = equipkey;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		ItemColumn itemcol = Module.getItemColumn(roleId, BagTypes.EQUIP, false);
		EquipItem equip = (EquipItem)itemcol.getItem(equipkey);
		if(equip == null){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		PropRole prole = PropRole.getPropRole(roleId, false);
		if(prole == null){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		if( !equip.isHaveNextLevel() ){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		runecost30 rcost = null;
		java.util.TreeMap<Integer,runecost30> costList = ConfigManager.getInstance().getConf(runecost30.class);
		for(Map.Entry<Integer, runecost30> entry : costList.entrySet()){
			if(entry.getValue().getBagId() == equip.getAttr().getRune_strengthenId() &&
					entry.getValue().getLevel() == equip.getLevel() + 1){
				rcost = entry.getValue();
			}	
		}
		if(rcost == null){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		if(rcost.getAttriType1() != -1){
			if( !DropManager.getInstance().useDel(rcost.getAttriType1(), rcost.getAttriValue1(), 
					roleId, LogBehavior.RUNELEVELUPCOST)){
				Message.psendMsgNotify(roleId, 135);
				return false;
			}
		}
		if(rcost.getAttriType2() != -1){
			if( !DropManager.getInstance().useDel(rcost.getAttriType2(), rcost.getAttriValue2(), 
					roleId, LogBehavior.RUNELEVELUPCOST)){
				Message.psendMsgNotify(roleId, 135);
				return false;
			}
		}
		equip.extdata.setLevel(equip.extdata.getLevel() + 1);
		equip.initExtBaseData();
		
		//物品相关活动数据统计
		ActivityGameManager.getInstance().addItemActivity(roleId, equip.getAttr(), ActivityGameManager.ITEM_QH,0);

		//发送协议
		SRefreshItem snd = new SRefreshItem();
		snd.bagid = Conv.toByte(BagTypes.EQUIP);
		snd.data = equip.getProtocolItem();
		psendWhileCommit(roleId, snd);
		
		psendWhileCommit(roleId, new SRefreshRefineEquip(SRefreshRefineEquip.END_OK));
		
		ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.FUWEN_QIANGHUA, 1);

		return true;
	}
	
	
}
