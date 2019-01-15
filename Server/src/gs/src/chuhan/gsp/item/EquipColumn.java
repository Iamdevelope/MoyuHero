package chuhan.gsp.item;

import chuhan.gsp.award.AddItem;
import chuhan.gsp.hero.HeroColumn;
//import chuhan.gsp.hero.Troop;
import chuhan.gsp.item.types.ArmorItem;
import chuhan.gsp.item.types.EquipItem;
import chuhan.gsp.item.types.HorseItem;
import chuhan.gsp.item.types.WeaponItem;

public class EquipColumn extends ItemColumn{

	EquipColumn(long roleid, boolean readonly) {
		super(roleid, BagTypes.EQUIP, readonly);
	}
	
	public AddItemResult addItem(final int itemid, final int num, final int numtype, 
			final int initflag,
			final String reason,
			final int countertype) {
		AddItemResult addresult = super.addItem(itemid, num, numtype, initflag, reason, countertype);
		if(!addresult.isSuccess())
			return addresult;
		//自动装备武器
		/*for(AddItem additem : addresult.getAddItems())
		{
			EquipItem equipitem = (EquipItem)getItem(additem.getKey());
			HeroColumn herocol = HeroColumn.getHeroColumn(roleid, false);
			for(Troop troop : herocol.getTroops())
			{
				if(equipitem instanceof WeaponItem)
				{
					if(troop.getTroopInfo().getWeapon() == 0)
					{
						herocol.switchEquip(troop.getPos(), additem.getKey());
						return addresult;
					}
				}
				else if(equipitem instanceof ArmorItem)
				{
					if(troop.getTroopInfo().getArmor() == 0)
					{
						herocol.switchEquip(troop.getPos(), additem.getKey());
						return addresult;
					}
				}
				else if(equipitem instanceof HorseItem)
				{
					if(troop.getTroopInfo().getHorse() == 0)
					{
						herocol.switchEquip(troop.getPos(), additem.getKey());
						return addresult;
					}
				}
			}
				
		}*/
		return addresult;
	}
	
}
