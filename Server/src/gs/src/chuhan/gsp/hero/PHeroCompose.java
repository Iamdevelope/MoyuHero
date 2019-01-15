package chuhan.gsp.hero;

public class PHeroCompose extends xdb.Procedure
{
	private final long roleId;
	private int heroid;
	public PHeroCompose(long roleId, int heroid) {
		this.roleId = roleId;
		this.heroid = heroid;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		SHeroCompose snd = new SHeroCompose();
		HeroColumn herocol = HeroColumn.getHeroColumn(roleId, false);	
		boolean result = herocol.composeEntry(heroid);
		if(result){
			snd.result = SHeroCompose.END_OK;
		}else{
			snd.result = SHeroCompose.END_NOT_OK;
		}
		
		xdb.Procedure.psend(roleId,snd);
		
		return result;
	}
}
