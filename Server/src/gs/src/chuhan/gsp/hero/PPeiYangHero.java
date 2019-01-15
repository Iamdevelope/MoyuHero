package chuhan.gsp.hero;

import chuhan.gsp.msg.Message;





public class PPeiYangHero extends xdb.Procedure{
	private final long roleid;
	private final int herokey; // 英雄key
	private final byte slotnum; // 培养位置
	private final byte isreset; // 是否重置（0为非重置，1为重置）
	
	public PPeiYangHero(long roleid, int herokey, byte slotnum, byte isreset) {
		this.roleid = roleid;
		this.herokey = herokey;
		this.slotnum = slotnum;
		this.isreset = isreset;

	}
	
	@Override
	protected boolean process() throws Exception {
		
		xbean.Properties xprop = xtable.Properties.get(roleid);
		if(xprop == null){
			throw new IllegalArgumentException("构造角色时，角色 "+roleid+" 不存在。");
		}
		HeroColumn herocol = HeroColumn.getHeroColumn(roleid, false);
		
		boolean result = herocol.peiyangEntry(this.herokey,  this.slotnum,this.isreset);

		SPeiyangHero snd = new SPeiyangHero();
		snd.slotnum = this.slotnum;
		snd.isreset = this.isreset;

		if(!result)
			snd.result = SPeiyangHero.END_NOT_OK;
		else{
			snd.result = SPeiyangHero.END_OK;
		}
		xdb.Procedure.psend(roleid, snd);
		
		return result;
	}
	
}
