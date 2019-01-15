package chuhan.gsp.hero;


public class PSwitchHero extends xdb.Procedure{

	public final long roleId;
	public final int trooppos;
	public final int herokey;
	public PSwitchHero(long roleId, int trooppos, int herokey) {
		this.roleId = roleId;
		this.trooppos = trooppos;
		this.herokey = herokey;
	}
	
	@Override
	protected boolean process() throws Exception {
		OldHeroColumn herocol = OldHeroColumn.getHeroColumn(roleId, false);
		return herocol.switchHero(trooppos, herokey);
	}
	
}
