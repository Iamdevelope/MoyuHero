package chuhan.gsp.hero;

import chuhan.gsp.DataInit;
import chuhan.gsp.msg.Message;



public class PSellHero extends xdb.Procedure{
	private final long roleid;
	private final int herokey;

	
	public PSellHero(long roleid, int herokey) {
		this.roleid = roleid;
		this.herokey = herokey;


	}
	
	@Override
	protected boolean process() throws Exception {
		
		xbean.Properties xprop = xtable.Properties.get(roleid);
		if(xprop == null){
			throw new IllegalArgumentException("构造角色时，角色 "+roleid+" 不存在。");
		}
		HeroColumn herocol = HeroColumn.getHeroColumn(roleid, false);
		
		int result = 1;//herocol.sellGold(herokey);

		boolean bresult = false;
		SSellHero snd = new SSellHero();
		if(result != DataInit.ERROR_RESULT)
		{
			snd.result = SSellHero.END_OK;
			bresult = true;
		}
		else
		{
			snd.result = SSellHero.END_ERROR;
		}
		xdb.Procedure.psend(roleid, snd);
		
		return bresult;
	}
	
}
