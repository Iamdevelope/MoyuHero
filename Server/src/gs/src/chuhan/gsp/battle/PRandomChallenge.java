package chuhan.gsp.battle;

import chuhan.gsp.util.Misc;

public class PRandomChallenge extends xdb.Procedure{
	private final long roleId;

	public PRandomChallenge(long roleId) {
		this.roleId = roleId;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		LadderRole lrole = LadderRole.getLadderRole(roleId, true);
		int selfrank = lrole.getMyRank();
		
		int min = Math.max(1, selfrank - 10);
		int max = Math.min(20000, selfrank+29);
		int dstrank = Misc.getRandomBetween(min, max);
		if(dstrank >= selfrank)
			dstrank++;
		xbean.LadderInfo dstladder = xtable.Pvpladder.select(dstrank);
		long rankerId = (dstladder == null)? Misc.getRandomBetween(-1, -3) : dstladder.getRoleid();
		return new PChallengeRanker(roleId, selfrank, dstrank, rankerId, false).call();
	}
}
