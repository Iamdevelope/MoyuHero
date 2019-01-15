package chuhan.gsp.item;

import chuhan.gsp.hero.OldHeroColumn;
import chuhan.gsp.hero.OldTroop;
import chuhan.gsp.item.types.ArmorItem;
import chuhan.gsp.item.types.EquipItem;
import chuhan.gsp.item.types.HorseItem;
import chuhan.gsp.item.types.WeaponItem;
import chuhan.gsp.msg.Message;
import chuhan.gsp.util.Conv;

/**
 * 重铸
 *
 */
public class PUpgradeEquip extends xdb.Procedure
{
	//isXuanTie字段
	public static final byte ISXUANTIE_NO = 0;//不是
	public static final byte ISXUANTIE_YES = 1;//是
	
	public static final int XUANTIE_Q_ID = 3402;//千年玄铁的ID
	
	private final long roleId;
	private int equipkey;
	private int consumeequipkey;
	private byte isXuanTie;//是否用玄铁重铸的 0-否 1-是
	
	public PUpgradeEquip(long roleId, int equipkey, int consumeequipkey, byte isXuanTie) {
		this.roleId = roleId;
		this.equipkey = equipkey;
		this.consumeequipkey = consumeequipkey;
		this.isXuanTie = isXuanTie;
	}
	
	@Override
	protected boolean process() throws Exception {
		if(equipkey == consumeequipkey)
			return false;
		ItemColumn equipitemcol = Module.getItemColumn(roleId, BagTypes.EQUIP, false);
		EquipItem equip = (EquipItem)equipitemcol.getItem(equipkey);
//		if(equip.getGrade() >= EquipItem.MAX_GRADE)
		if(false)
		{
			Message.psendMsgNotify(roleId, 171);
			return false;
		}
		
		int addExp = 0;
		if(!isUseXuanTie()) {//用的装备重铸
			EquipItem consumeequip = (EquipItem)equipitemcol.getItem(consumeequipkey);
			if(equip.getItemid() != consumeequip.getItemid())
				return false;
			//是穿戴好的装备要卸掉
			OldHeroColumn herocol = OldHeroColumn.getHeroColumn(roleId, false);
			OldTroop troop = herocol.getTroopByEquip(consumeequip);
			if(troop != null)
			{
				/*by yanglk troop
				if (consumeequip instanceof WeaponItem)
					troop.getTroopInfo().setWeapon(0);
				else if (consumeequip instanceof ArmorItem)
					troop.getTroopInfo().setArmor(0);
				else if (consumeequip instanceof HorseItem)
					troop.getTroopInfo().setHorse(0);
					*/
			}
//			addExp = consumeequip.getAllGradeExp()+1;
			equipitemcol.removeItemByKey(consumeequipkey, 1, 1, "renew");
			
			if(equip.getLevel() < consumeequip.getLevel())
			{
				equip.setLevel(consumeequip.getLevel());
			}
		} else {//用玄铁重铸
			ItemColumn bagitemcol = Module.getItemColumn(roleId, BagTypes.BAG, false);
			if(bagitemcol.removeItemById(XUANTIE_Q_ID, 1, 1, "renew") != 1) {
				return false;
			}
			addExp = 1;
		}
		
		/*int viplevel = xtable.Properties.selectViplv(roleId);
		if( viplevel >= 4)
		{
			if(Math.random() < (0.2 + viplevel * 0.01))
			{
				addexp = 2;
				//TODO 发送协议通知暴击
			}
		}*/
		/*
		equip.setGradeExp(equip.getGradeExp()+addExp);
		int needexp = equip.getNeedExp();
		while(equip.getGradeExp() >= needexp)
		{
			if(equip.getGrade() >= EquipItem.MAX_GRADE)
				break;
			equip.setGrade(equip.getGrade()+1);
			equip.setGradeExp(equip.getGradeExp()-needexp);
			needexp = equip.getNeedExp();
		}
		*/
		SRefreshItem snd = new SRefreshItem();
		snd.bagid = Conv.toByte(BagTypes.EQUIP);
		snd.data = equip.getProtocolItem();
		psendWhileCommit(roleId, snd);
		psendWhileCommit(roleId, new SRefreshRenewEquip((byte)0));
		return true;
	}
	
	/**
	 * 是否用的玄铁重铸
	 * @return
	 */
	private boolean isUseXuanTie() {
		return isXuanTie == ISXUANTIE_YES;
	}
}
