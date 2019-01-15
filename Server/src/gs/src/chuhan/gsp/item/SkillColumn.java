package chuhan.gsp.item;

import chuhan.gsp.award.AddItem;
import chuhan.gsp.hero.OldHeroColumn;
import chuhan.gsp.hero.OldTroop;

public class SkillColumn extends ItemColumn{

	SkillColumn(long roleid, boolean readonly) {
		super(roleid, BagTypes.SKILL, readonly);
	}
	
	public AddItemResult addItem(final int itemid, final int num, final int numtype, 
			final int initflag,
			final String reason,
			final int countertype) {
		AddItemResult addresult = super.addItem(itemid, num, numtype, initflag, reason, countertype);
		if(!addresult.isSuccess())
			return addresult;
		//自动装备武器
		for(AddItem additem : addresult.getAddItems())
		{
			OldHeroColumn herocol = OldHeroColumn.getHeroColumn(roleid, false);
			for(OldTroop troop : herocol.getTroops())
			{
				int nextskillpos =0; //*by yanglk trooptroop.getTroopInfo().getSkills().size()+1;
				if(nextskillpos > OldHeroColumn.MAX_SKILL_NUM || nextskillpos < 1)
					continue;
				boolean added = herocol.switchSkill(troop.getPos(), nextskillpos, additem.getKey());
				if(added)
					return addresult;
			}
				
		}
		return addresult;
	}
	
}
