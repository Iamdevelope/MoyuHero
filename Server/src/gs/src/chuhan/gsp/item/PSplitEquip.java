package chuhan.gsp.item;

import java.util.LinkedList;
import java.util.Map;

import chuhan.gsp.DataInit;
import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.player03;
import chuhan.gsp.award.DropManager;
import chuhan.gsp.award.IDManager;
import chuhan.gsp.game.svipconfig;
import chuhan.gsp.hero.Hero;
import chuhan.gsp.hero.HeroColumn;
import chuhan.gsp.item.types.EquipItem;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.Message;
import chuhan.gsp.play.activity.ActivityGameManager;
import chuhan.gsp.play.activity.ActivityManager;
import chuhan.gsp.play.huoyuedu.HuoyueColumns;
import chuhan.gsp.util.Conv;
import chuhan.gsp.util.DateUtil;
import chuhan.gsp.util.Misc;

/**
 * 熔炼
 *
 */
public class PSplitEquip extends xdb.Procedure
{
	private final long roleId;
	private final LinkedList<Integer> equipkeyList;
	public PSplitEquip(long roleId, LinkedList<Integer> equipkeyList) {
		this.roleId = roleId;
		this.equipkeyList = equipkeyList;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		PropRole prole = PropRole.getPropRole(roleId, false);
		if(prole == null){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		ItemColumn itemcol = Module.getItemColumn(roleId, BagTypes.EQUIP, false);
		HeroColumn herocol = HeroColumn.getHeroColumn(roleId, false);
		
		for(Integer equipkey : equipkeyList){
			EquipItem equip = (EquipItem)itemcol.getItem(equipkey);
			if(equip == null){
				Message.psendMsgNotify(roleId, 135);
				return false;
			}
			
			runecost30 rcost = null;
			java.util.TreeMap<Integer,runecost30> costList = ConfigManager.getInstance().getConf(runecost30.class);
			for(Map.Entry<Integer, runecost30> entry : costList.entrySet()){
				if(entry.getValue().getBagId() == equip.getAttr().getRune_strengthenId() &&
						entry.getValue().getLevel() == equip.getLevel() ){
					rcost = entry.getValue();
				}	
			}
			
			Hero hero = herocol.isWearByHero(equipkey);
			if(hero != null){
				herocol.itemOutHero(hero, equipkey);
				hero.refreshHero(roleId);
			}
			//总共加的熔炼点
			int addRLNum = equip.getAttr().getRune_smelt();
			if( equip.getAttr().getRune_smelt() != 
					prole.addZiYuan(equip.getAttr().getRune_smelt(), 0, IDManager.RONGLIAN)){
				Message.psendMsgNotify(roleId, 135);
				return false;
			}
			if(rcost != null){
				if(rcost.getReturnType1() != -1){
					if(rcost.getReturnType1() == IDManager.RONGLIAN){
						addRLNum += rcost.getReturnValue1();
					}
					if( rcost.getReturnValue1() != 
							prole.addZiYuan(rcost.getReturnValue1(), 0, rcost.getReturnType1())){
						Message.psendMsgNotify(roleId, 135);
						return false;
					}
				}
				if(rcost.getReturnType2() != -1){
					if(rcost.getReturnType2() == IDManager.RONGLIAN){
						addRLNum += rcost.getReturnValue2();
					}
					if( rcost.getReturnValue2() != 
							prole.addZiYuan(rcost.getReturnValue2(), 0, rcost.getReturnType2())){
						Message.psendMsgNotify(roleId, 135);
						return false;
					}
				}
			}
			
			//物品相关活动数据统计
			ActivityGameManager.getInstance().addItemActivity(roleId, equip.getAttr(), ActivityGameManager.ITEM_RONGLIAN,
					addRLNum);
			
			if( itemcol.removeItemByKey(equipkey, 1, 1, "equipsplit") != 1 ){
				Message.psendMsgNotify(roleId, 135);
				return false;
			}
		}
		
		psendWhileCommit(roleId, new SSplitEquip(SSplitEquip.END_OK));
		
		ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.FUWEN_RONGLIAN, equipkeyList.size());

		return true;
	}
	
	
}
