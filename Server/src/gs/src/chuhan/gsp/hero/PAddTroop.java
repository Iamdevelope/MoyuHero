package chuhan.gsp.hero;




public class PAddTroop extends xdb.Procedure{

	public final long roleId;
	public final int herokey;
	public final int locationid;
	public PAddTroop(long roleId,int herokey,int locationid) {
		this.roleId = roleId;
		this.herokey = herokey;
		this.locationid = locationid;
	}
	
	@Override
	protected boolean process() throws Exception {
		xbean.Properties xprop = xtable.Properties.get(roleId);
		if(xprop == null)
		{
			throw new IllegalArgumentException("构造角色时，角色 "+roleId+" 不存在。");
//			return false;
		}
		
		TroopColumn troopcol = TroopColumn.getTroopColumn(roleId, false);
		xbean.Troop xtroop = troopcol.addTroop(herokey,locationid);	
		if(xtroop == null){
			return false;
		}
		troopcol.refreshTroops();
		return true;
	}
	
}
