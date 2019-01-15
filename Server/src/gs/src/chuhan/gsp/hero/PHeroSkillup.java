package chuhan.gsp.hero;





public class PHeroSkillup extends xdb.Procedure{
	private final long roleid;
	public int herokey; // 英雄key
	public byte skillnum; // 培养位置
	
	public PHeroSkillup(long roleid, int herokey, byte skillnum) {
		this.roleid = roleid;
		this.herokey = herokey;
		this.skillnum = skillnum;


	}
	
	@Override
	protected boolean process() throws Exception {
		
		xbean.Properties xprop = xtable.Properties.get(roleid);
		if(xprop == null){
			throw new IllegalArgumentException("构造角色时，角色 "+roleid+" 不存在。");
//			return false;
		}
		HeroColumn herocol = HeroColumn.getHeroColumn(roleid, false);
		
		boolean result = herocol.skillUpEntry(this.herokey,  this.skillnum);

		SHeroSkillup snd = new SHeroSkillup();
		snd.skillnum = this.skillnum;

		if(!result){
			snd.result = SHeroSkillup.END_NOT_OK;
			xdb.Procedure.psendWhileCommit(roleid, snd);
		}
		else{
			snd.result = SHeroSkillup.END_OK;
			xdb.Procedure.psend(roleid, snd);
		}
		
		
		return result;
	}
	
}
