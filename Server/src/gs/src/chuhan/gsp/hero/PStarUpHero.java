package chuhan.gsp.hero;

public class PStarUpHero extends xdb.Procedure
{
	private final long roleId;
	private int herokey;
	public PStarUpHero(long roleId, int herokey) {
		this.roleId = roleId;
		this.herokey = herokey;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		SStarUpHero snd = new SStarUpHero();
		HeroColumn herocol = HeroColumn.getHeroColumn(roleId, false);	
		boolean result = herocol.starupEntry(herokey);
		if(result){
			snd.result = SStarUpHero.END_OK;
		}else{
			snd.result = SStarUpHero.END_ERROR;
		}
		
		xdb.Procedure.psend(roleId,snd);
		
		return result;
	}
}
