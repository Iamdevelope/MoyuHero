package chuhan.gsp.hero;




public class PHeroOutAllTroop extends xdb.Procedure{

	public final long roleId;
	public final int herokey;

	public PHeroOutAllTroop(long roleId, int herokey) {
		this.roleId = roleId;
		this.herokey = herokey;

	}
	
	@Override
	protected boolean process() throws Exception {
		xbean.Properties xprop = xtable.Properties.get(roleId);
		if(xprop == null)
		{
			throw new IllegalArgumentException("构造角色时，角色 "+roleId+" 不存在。");
			//return false;
		}
			
		TroopColumn troopcol = TroopColumn.getTroopColumn(roleId, false);
		troopcol.HeroOutAllTroop(herokey);

		return true;
	}
	
}
