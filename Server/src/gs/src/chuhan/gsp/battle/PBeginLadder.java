package chuhan.gsp.battle;

import chuhan.gsp.util.Misc;
/**
 * 开始天梯玩法时调用，随机天梯起点，一个号一辈子只调用一次
 *
 */
public class PBeginLadder extends xdb.Procedure{
	private final long roleId;

	public PBeginLadder(long roleId) {
		this.roleId = roleId;
	}
	
	
	@Override
	protected boolean process() throws Exception {
		int emptyrank = findEmptyEnterRank();
		if(emptyrank <= 0)
			return true;
		xbean.LadderInfo xladderinfo = xtable.Pvpladder.get(emptyrank);
		if(xladderinfo != null)
		{
			//pexecuteWhileCommit(new PBeginLadder(emptyrank));//万一重复了，运气不好，不重新找，直接从榜外开始打
			return true;
		}
		LadderRole ladderrole = LadderRole.getLadderRole(roleId, false);
		if(ladderrole.onLadder())
			return true;//已经在榜上了，可能吗？
		//生成初始排位
		ladderrole.getData().setLadderrank(emptyrank);
		xladderinfo = xbean.Pod.newLadderInfo();
		xladderinfo.setRoleid(roleId);
		xtable.Pvpladder.insert(emptyrank, xladderinfo);
		return true;
	}
	/**
	 * 在后800个排位里，找到一个空的
	 * @return
	 */
	private int findEmptyEnterRank()
	{
		int random = Misc.getRandomBetween(1, 40);//随机一下，只找20次，找不到就放榜外
		for(int i = 1 ; i <= 20; i++)
		{
			int r = LadderRole.MAX_RANK - i*random;
			if(xtable.Pvpladder.select(r) == null)
				return r;
		}
		
		return 0;
	}
	
}
