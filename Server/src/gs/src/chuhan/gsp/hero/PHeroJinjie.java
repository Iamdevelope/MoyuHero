package chuhan.gsp.hero;

public class PHeroJinjie extends xdb.Procedure
{
	private final long roleId;
	private int herokey;
	public PHeroJinjie(long roleId, int herokey) {
		this.roleId = roleId;
		this.herokey = herokey;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		SHeroJinjie snd = new SHeroJinjie();
		HeroColumn herocol = HeroColumn.getHeroColumn(roleId, false);	
		boolean result = herocol.jinjinEntry(herokey);
		if(result){
			snd.result = SHeroJinjie.END_OK;
		}else{
			snd.result = SHeroJinjie.END_NOT_OK;
		}
		
		xdb.Procedure.psend(roleId,snd);
		
		return result;
	}
}
