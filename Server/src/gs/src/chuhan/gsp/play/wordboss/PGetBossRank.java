package chuhan.gsp.play.wordboss;

public class PGetBossRank extends xdb.Procedure{
	private final long roleId;

	
	public PGetBossRank(long roleId) {
		this.roleId = roleId;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		xbean.Properties xprop = xtable.Properties.get(roleId);
		if(xprop == null){
			throw new IllegalArgumentException("构造角色时，角色 "+roleId+" 不存在。");
		}
		SGetBossRank snd = new SGetBossRank();
		snd.rank.addAll(chuhan.gsp.play.ranking.bossRanking.getInstance().getRankList());
		chuhan.gsp.play.ranking.bossRanking.rankData rankinit = chuhan.gsp.play.ranking.bossRanking.
				getInstance().map10.get(roleId);
		if(rankinit == null){
			rankinit = chuhan.gsp.play.ranking.bossRanking.getInstance().mapall.get(roleId);
			if(rankinit == null){
				snd.num = 0;
				snd.ranknum = 0;
			}else{
				snd.num = rankinit.num;
				snd.ranknum = rankinit.lastRank;
			}
		}else{
			
			snd.num = rankinit.num;
			snd.ranknum = rankinit.lastRank;
		}
		xdb.Procedure.psend(roleId, snd);
		return true;
	}
	
}
