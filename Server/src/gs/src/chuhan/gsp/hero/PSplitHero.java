package chuhan.gsp.hero;



public class PSplitHero extends xdb.Procedure{
	private final long roleid;
	private final java.util.LinkedList<Integer> herokeyList;

	
	public PSplitHero(long roleid, java.util.LinkedList<Integer> herokeyList) {
		this.roleid = roleid;
		this.herokeyList = herokeyList;


	}
	
	@Override
	protected boolean process() throws Exception {
		
		xbean.Properties xprop = xtable.Properties.get(roleid);
		if(xprop == null)
		{
			return false;
		}
		HeroColumn herocol = HeroColumn.getHeroColumn(roleid, false);
		
		boolean result = herocol.splitEntry(herokeyList);

		SSplitHero snd = new SSplitHero();
		if(result)
			snd.result = SSplitHero.END_OK;
		else
		{
			snd.result = SSplitHero.END_ERROR;
		}
		xdb.Procedure.psend(roleid, snd);
		
		return result;
	}
	
}
