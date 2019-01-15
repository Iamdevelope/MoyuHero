package chuhan.gsp.hero;

import java.util.LinkedList;
import java.util.List;

public class PSetTroopList extends xdb.Procedure{

	public final long roleId;
	public final List<Integer> trooplist;
	public final List<Integer> herokeys;
	public final List<Integer> viceheroes;
	public PSetTroopList(long roleId, List<Integer> trooplist, List<Integer> herokeys, List<Integer> viceheroes) {
		this.roleId = roleId;
		this.trooplist = trooplist;
		this.herokeys = herokeys;
		this.viceheroes = viceheroes;
	}
	
	@Override
	protected boolean process() throws Exception {
		OldHeroColumn herocol = OldHeroColumn.getHeroColumn(roleId, false);
		return herocol.setTroopList(trooplist, herokeys, viceheroes);
	}
	
}
