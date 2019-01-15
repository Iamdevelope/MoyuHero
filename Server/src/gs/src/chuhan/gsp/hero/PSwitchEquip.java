package chuhan.gsp.hero;


public class PSwitchEquip extends xdb.Procedure{

	public final long roleId;
	public final int trooppos;
	public final int equipkey;
	public PSwitchEquip(long roleId, int trooppos, int equipkey) {
		this.roleId = roleId;
		this.trooppos = trooppos;
		this.equipkey = equipkey;
	}
	
	@Override
	protected boolean process() throws Exception {
		OldHeroColumn herocol = OldHeroColumn.getHeroColumn(roleId, false);
		return herocol.switchEquip(trooppos,equipkey);
	}
	
}
