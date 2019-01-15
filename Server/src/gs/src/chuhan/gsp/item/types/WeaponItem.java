package chuhan.gsp.item.types;


import chuhan.gsp.hero.Hero;
import chuhan.gsp.hero.HeroColumn;
import chuhan.gsp.item.UseResult;


public class WeaponItem extends EquipItem{

	WeaponItem(int itemid) {
		super(itemid);
	}
	WeaponItem(xbean.Item item) {
		super(item);
	}
	
	
	@Override
	public UseResult use(long roleId, int num, int dstkey) {
		
		HeroColumn herocol = HeroColumn.getHeroColumn(roleId, false);
		Hero hero = herocol.isWearByHero(this.getKey());
		if(hero != null){
			herocol.itemOutHero(hero,this.getKey());
//			hero.refreshHero(roleId);
		}
		
		boolean result = herocol.itemToHero(dstkey, num, this);
		if(result){
			if(hero != null && hero.getxHeroInfo().getKey() != dstkey){
				hero.refreshHero(roleId);	
			}
			return UseResult.SUCC;
		}
		return UseResult.FAIL;
	}
}
