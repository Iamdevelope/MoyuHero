package chuhan.gsp.hero;


public class PSwitchSkill extends xdb.Procedure{

	public final long roleId;
	public final int trooppos;
	public final int skillpos;
	public final int skillkey;
	public PSwitchSkill(long roleId, int trooppos, int skillpos, int skillkey) {
		this.roleId = roleId;
		this.trooppos = trooppos;
		this.skillpos = skillpos;
		this.skillkey = skillkey;
	}
	
	@Override
	protected boolean process() throws Exception {
		OldHeroColumn herocol = OldHeroColumn.getHeroColumn(roleId, false);
		return herocol.switchSkill(trooppos,skillpos,skillkey);
	}
	
}
