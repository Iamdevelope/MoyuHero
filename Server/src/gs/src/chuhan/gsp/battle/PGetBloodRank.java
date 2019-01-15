package chuhan.gsp.battle;

import chuhan.gsp.hero.OldHero;
import chuhan.gsp.hero.OldHeroColumn;
import chuhan.gsp.hero.OldTroop;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.util.Conv;
import chuhan.gsp.util.DateUtil;

public class PGetBloodRank extends xdb.Procedure{
	private final long roleId;
	public PGetBloodRank(long roleId) {
		this.roleId = roleId;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		long now = GameTime.currentTimeMillis();
		int curday = DateUtil.getCurrentDay(now) ;
		xbean.BloodRankList xranklist = xtable.Bloodranklist.select(1);
		if(xranklist == null)
		{
			xdb.Procedure.psendWhileCommit(roleId, new SSendBloodRankList());
			return true;
		}
		
		if(xranklist.getCurweek()!=curday)
		{
			xdb.Procedure.psendWhileCommit(roleId, new SSendBloodRankList());
			return true;
		}
		
		SSendBloodRankList snd = new SSendBloodRankList();
		for(xbean.BloodRankRole xrole : xranklist.getRankers())
		{
			snd.ranklist.add(getProtocolRole(xrole));
		}
		xdb.Procedure.psendWhileCommit(roleId, snd);
		return true;
	}
	
	private BloodRankRole getProtocolRole(xbean.BloodRankRole xrole)
	{
		BloodRankRole r = new BloodRankRole();
		xbean.Properties xprop = xtable.Properties.select(xrole.getRoleid());
		r.roleid = xrole.getRoleid();
		r.rolelevel = Conv.toShort(xprop.getLevel());
		r.rolename = xprop.getRolename();
		r.maxlevel = Conv.toShort(xrole.getMaxlevel());
		OldHeroColumn herocol = OldHeroColumn.getHeroColumn(xrole.getRoleid(), true);
		int i = 0;
		for(OldTroop troop : herocol.getTroops())
		{
			OldHero hero = null; //*by yanglk trooptroop.getHero();
			if(hero == null)
				continue;
			//by yanglk  hero			r.troopheros.add(hero.getHeroInfo().getId());
			if(++i >= 3)
				break;
		}
		return r;
	}
	
}
