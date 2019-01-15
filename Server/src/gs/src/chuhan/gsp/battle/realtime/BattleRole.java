
package chuhan.gsp.battle.realtime;


import chuhan.gsp.hero.HeroColumn;
import chuhan.gsp.hero.TroopColumn;
import chuhan.gsp.item.EquipColumn;


public class BattleRole {
	public final long roleid;		//人物id
	public final int ranking;		//排名
	public final String rolename;
	public final long starttime;
	public java.util.HashMap<Integer,Integer> useherokeylist; // 使用英雄id和位置（key为位置，value为herokey）
	public java.util.LinkedList<chuhan.gsp.Hero> heroList;

	public int isWin = SRealTimeEnd.FAIL;
	
	public boolean isOk = false;
	
	BattleRole(long roleid, int ranking, String rolename)
	{
		this.roleid = roleid;
		this.ranking = ranking;		
		this.rolename = rolename;
		starttime = chuhan.gsp.main.GameTime.currentTimeMillis();
	}
	
	public chuhan.gsp.battle.realtime.Battleroleinfo getProBattleRoleInfo()
	{
		chuhan.gsp.battle.realtime.Battleroleinfo battlerole = new chuhan.gsp.battle.realtime.Battleroleinfo();
		battlerole.roleid = this.roleid;
		battlerole.ranking = this.ranking;
		battlerole.name = this.rolename;
		battlerole.useherokeylist.putAll(this.useherokeylist);
		battlerole.heroes.addAll(this.heroList);
//		for(chuhan.gsp.Hero hero : this.heroList)
//		{
//			battlerole.heroes.addFirst(hero);
//		}
		return battlerole;
	}
	
	public boolean begin(int troopid)
	{
		/*TroopColumn troopcol = TroopColumn.getTroopColumn(roleid, false);
		this.useherokeylist = troopcol.getHkeyLolistByTrid(troopid);
		
		HeroColumn herocol = HeroColumn.getHeroColumn(roleid, false);
		EquipColumn equipcol = EquipColumn.getEquipColumn(roleid, false);
		
		for(java.util.Map.Entry<Integer,Integer> herokey : useherokeylist.entrySet())
		{
			if(herokey.getValue() != 0)
			{
				chuhan.gsp.hero.Hero hero = herocol.getHByHKey(herokey.getValue());
				if(hero != null)
				{
					heroList.addFirst(hero.getProtocolHero());
					addEquip(equipcol,hero.getxHeroInfo().getWeapon());
					addEquip(equipcol,hero.getxHeroInfo().getBarde());
					addEquip(equipcol,hero.getxHeroInfo().getOrnament());
				}
					
			}
		}
		if(heroList.size() == 0)
			return false;*/
		return true;
	}
	
}


