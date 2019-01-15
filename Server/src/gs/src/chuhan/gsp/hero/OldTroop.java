package chuhan.gsp.hero;

import java.util.HashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Set;

import chuhan.gsp.item.BagTypes;
import chuhan.gsp.item.BasicItem;
import chuhan.gsp.item.ItemColumn;
import chuhan.gsp.item.Module;
import chuhan.gsp.item.types.EquipItem;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.IdType;
import chuhan.gsp.util.Conv;

public class OldTroop {
	
	public static OldTroop getTroop(long roleId, int trooppos, boolean readonly)
	{
		OldHeroColumn troopcol = OldHeroColumn.getHeroColumn(roleId, readonly);
		return troopcol.getTroop(trooppos);
	}
	
	public static OldTroop getTroop(OldHeroColumn herocol, int trooppos, xbean.Troop xtroop, boolean readonly)
	{
		return new OldTroop(herocol, trooppos, xtroop, readonly);
	}
	
	/**
	 * 根据id和等级创建一个侠客
	 * 这个方法的参数可能会变化，暂时只要两个就足够
	 * @param xiakeId
	 * @param level
	 * @return
	 */
	public static xbean.Troop createTroop(int herokey)
	{
		xbean.Troop xtroop = xbean.Pod.newTroop();
		/*by yanglk troop
		xtroop.setHeroid(herokey);
		*/
		return xtroop;
	}
	
	
	
	private final int trooppos;//[1,8]
	private final xbean.Troop xtroop;
	public final boolean readonly;
	private final OldHeroColumn herocol;
	public OldTroop(OldHeroColumn herocol, int trooppos, xbean.Troop troopinfo, boolean readonly) {
		this.herocol = herocol;
		this.trooppos = trooppos;
		this.xtroop = troopinfo;
		this.readonly = readonly;
	}
	
	public chuhan.gsp.Troop getProtocolTroop()
	{
		chuhan.gsp.Troop protocol = new chuhan.gsp.Troop();
		/*by yanglk troop
		protocol.heroid = Conv.toShort(xtroop.getHeroid());
		protocol.weapon = Conv.toShort(xtroop.getWeapon());
		protocol.armor = Conv.toShort(xtroop.getArmor());
		protocol.horse = Conv.toShort(xtroop.getHorse());
		for(int skillid : xtroop.getSkills())
			protocol.skills.add(skillid);
		protocol.viceheros.addAll(xtroop.getViceheros());
		*/
		return protocol;
	}
	
	public boolean hasEquip(int equipId)
	{
		ItemColumn itemcol = Module.getItemColumnByItemId(herocol.roleId, equipId, true);
		/*by yanglk troop
		if(xtroop.getWeapon() > 0)
		{
			BasicItem item = itemcol.getItem(xtroop.getWeapon());
			if(item != null && item.getItemid() == equipId)
				return true;
		}
		if(xtroop.getHorse() > 0)
		{
			BasicItem item = itemcol.getItem(xtroop.getHorse());
			if(item != null && item.getItemid() == equipId)
				return true;
		}
		if(xtroop.getArmor() > 0)
		{
			BasicItem item = itemcol.getItem(xtroop.getArmor());
			if(item != null && item.getItemid() == equipId)
				return true;
		}
		*/
		return false;
	}
	
	public EquipItem getWeapon()
	{
		/*by yanglk troop
		if(xtroop.getWeapon() <= 0)
			return null;
			
		ItemColumn itemcol = Module.getItemColumn(herocol.roleId,BagTypes.EQUIP, readonly);
		BasicItem item = itemcol.getItem(xtroop.getWeapon());
		if(item == null)
			return null;
		return (EquipItem)item;
		*/
		return null;
	}
	
	public EquipItem getArmor()
	{
		/*by yanglk troop
		if(xtroop.getArmor() <= 0)
			return null;
		ItemColumn itemcol = Module.getItemColumn(herocol.roleId,BagTypes.EQUIP, readonly);
		BasicItem item = itemcol.getItem(xtroop.getArmor());
		if(item == null)
			return null;
		return (EquipItem)item;
		*/
		return null;
	}
	
	public EquipItem getHorse()
	{
		/*by yanglk troop
		if(xtroop.getHorse() <= 0)
			return null;
		ItemColumn itemcol = Module.getItemColumn(herocol.roleId,BagTypes.EQUIP, readonly);
		BasicItem item = itemcol.getItem(xtroop.getHorse());
		if(item == null)
			return null;
		return (EquipItem)item;
		*/
		return null;
	}
	/*by yanglk troop
	public Hero getViceHero1()
	{
		if(xtroop.getViceheros().isEmpty())
			return null;
		return herocol.getHero(xtroop.getViceheros().get(0));
	}
	
	public Hero getViceHero2()
	{
		if(xtroop.getViceheros().size() <= 1)
			return null;
		return herocol.getHero(xtroop.getViceheros().get(1));
	}
	
	
	public boolean hasSkill(int skillId)
	{
		ItemColumn itemcol = Module.getItemColumnByItemId(herocol.roleId, skillId, true);
		for(int skillkey : xtroop.getSkills())
		{
			BasicItem item = itemcol.getItem(skillkey);
			if(item != null && item.getItemid() == skillId)
				return true;
		}
		return false;
	}
	*/
	/*by yanglk troop
	public Hero getHero()
	{
		return herocol.getHero(getHerokey());
	}
	
	public xbean.Troop getTroopInfo()
	{
		return xtroop;
	}
	

	public void setHerokey(int herokey)
	{
		xtroop.setHeroid(herokey);
	}
	
	public int getHerokey()
	{
		return xtroop.getHeroid();
	}
	*/
	
	public int getPos()
	{
		return trooppos;
	}
	
	public Set<Integer> getBattleSkills()
	{
		Set<Integer> skills = new HashSet<Integer>();
///*by yanglk troop		skills.addAll(getTroopInfo().getSkills());
		OldHero hero = herocol.getHeroByTroop(getPos());
		//by yanglk  hero		skills.add(hero.getConfig().getBindskill());
		return skills;
	}
	
	public List<TroopRelation> getActiveRelations()
	{
		
		List<TroopRelation> relations = new LinkedList<TroopRelation>();
		OldHero hero = null; //by yanglk troop  herocol.getHero(getHerokey());
		if(hero == null)
			return relations;
		
		/*//by yanglk  hero
		for(String relationstr : hero.getConfig().qiyuanattr)
		{
			TroopRelation relation = new TroopRelation();
			if(!relation.init(relationstr))
				continue;
			boolean triggered = true;//最终是否激活，条件都满足才激活
			for(int targetId : relation.targets)
			{//用条件作为循环
				IdType idtype = ConfigManager.getIdType(targetId);
				if(idtype == IdType.HERO)
				{
//					/* 注释于2013-06-05
//					 * 因为策划需求改回成只判断对应的副将
//					 * 下面的代码是取所有上阵的武将判断
//					boolean invicehero = false;//与当前条件武将能否激活
//					int targetLinkId = Hero.getLinkHeroId(targetId);//与条件武将同质的武将
//					List<Hero> allHeros = herocol.getAllTropHeros();
//					for(Hero tmpHero : allHeros) {
//						if(tmpHero.getId() == targetId || //这个位置的武将是条件武将
//								tmpHero.getId() == targetLinkId) {//或者这个位置的武将是条件武将对应的同质武将
//							invicehero = true;
//						}
//					}
//					if(invicehero) {//当前条件武将没触发则最终就不能触发
//						triggered = false;
//						break;
//					}
					
					TroopOld troop = herocol.getTroopByHeroId(targetId);
					int targetLinkId = Hero.getLinkHeroId(targetId);
					if(null == troop && targetLinkId > 0) {
						troop = herocol.getTroopByHeroId(targetLinkId);
					}
					boolean invicehero = false;
					/*by yanglk troop
					for(int viceherokey : getTroopInfo().getViceheros()) {
						Hero tmphero = herocol.getHero(viceherokey);
						if(tmphero != null && (tmphero.getHeroInfo().getId() == targetId || tmphero.getHeroInfo().getId() == targetLinkId))
							invicehero = true;
					}///end by troop
					
					if(troop == null && !invicehero) {
						triggered = false;
						break;
					}
				}
				else if(idtype == IdType.EQUIP)
				{
					boolean hasequip = hasEquip(targetId);
					if(!hasequip)
					{//没触发
						triggered = false;
						break;
					}
				}
				else if(idtype == IdType.SKILL)
				{
					/*by yanglk troop
					boolean has = hasSkill(targetId);
					if(!has)
					{//没触发
						triggered = false;
						break;
					}//end by troop
					
				}
				else
					triggered = false;
			}
			if(triggered)
				relations.add(relation);
		}
		*///by yanglk  hero
		return relations;
	}
	
}
