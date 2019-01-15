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
 * 鉴定
 *
 */
public class PIdentifyEquip extends xdb.Procedure
{
	private final long roleId;
	private final int equipkey;
	public PIdentifyEquip(long roleId, int equipkey) {
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
		
		int costtype = EquipItem.ATTR_NULL;
		int costnum = 0;
		int attrbagid = EquipItem.ATTR_NULL;
		
		int attrPlace = equip.getWitchAttr();
		switch(attrPlace){
		case EquipItem.ATTR1:
			costtype = equip.attr.getRune_exposeCostType1();
			costnum = equip.attr.getRune_exposeCostValue1();
			attrbagid = equip.attr.getRune_addAttri1();
			break;
		case EquipItem.ATTR2:
			costtype = equip.attr.getRune_exposeCostType2();
			costnum = equip.attr.getRune_exposeCostValue2();
			attrbagid = equip.attr.getRune_addAttri2();
			break;
		case EquipItem.ATTR3:
			costtype = equip.attr.getRune_exposeCostType3();
			costnum = equip.attr.getRune_exposeCostValue3();
			attrbagid = equip.attr.getRune_addAttri3();
			break;
		case EquipItem.ATTR4:
			costtype = equip.attr.getRune_exposeCostType4();
			costnum = equip.attr.getRune_exposeCostValue4();
			attrbagid = equip.attr.getRune_addAttri4();
			break;
		case EquipItem.ATTR_NULL:
			Message.psendMsgNotify(roleId, 135);
			return false;
		default:
			Message.psendMsgNotify(roleId, 135);
			return false;	
		}
		if(costtype == -1){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		
		if( !DropManager.getInstance().useDel(costtype, costnum, 
				roleId, LogBehavior.RUNEIDENTIFYCOST)){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		
		int attrId = equip.getAttrRandom(attrbagid);
		if(EquipItem.ATTR_NULL == attrId){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		
		if(!equip.setAttrId(attrPlace, attrId)){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}

		//物品相关活动数据统计
		ActivityGameManager.getInstance().addItemActivity(roleId, equip.getAttr(), ActivityGameManager.ITEM_JD,0);
		
		//发送协议
		SRefreshItem snd = new SRefreshItem();
		snd.bagid = Conv.toByte(BagTypes.EQUIP);
		snd.data = equip.getProtocolItem();
		psendWhileCommit(roleId, snd);
		
		psendWhileCommit(roleId, new SIdentifyEquip(SIdentifyEquip.END_OK));
		
		ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.FUWEN_JIANDING, 1);
		
		return true;
	}
	
	
}
