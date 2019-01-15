package chuhan.gsp.battle;

import xdb.Lockey;
import xdb.Lockeys;
import chuhan.gsp.msg.Message;

public class PChallengeRanker extends xdb.Procedure
{
	private final long roleId;
	private final int selfrank;
	private final long rankerId;
	private final int rank;
	private final boolean isinvite;
	public PChallengeRanker(long roleId, int selfrank, int rank, long rankerId, boolean isinvite) {
		this.roleId = roleId;
		this.selfrank = selfrank;
		this.rankerId = rankerId;
		this.rank = rank;
		this.isinvite = isinvite;
	}
	
	/**
	 * 注意：锁顺序：FIRSTLADDERINFO->PVPLADDER->roleId
	 */
	@Override
	protected boolean process() throws Exception {
		
		if(rank == 1) {//只有操作到第一名的时候才锁FIRSTLADDERINFO
			Lockey[] keys = new Lockey[1];
			keys[0] = Lockeys.get(xtable.Locks.FIRSTLADDERINFOROLE, LadderRole.FIRSTLADDERINFO_ID);
			lock(keys);//为什么只有传数组参数的方法?
		}
		
		if(LadderRole.onLadder(selfrank))
			lock(Lockeys.get(xtable.Locks.PVPLADDER, selfrank,rank));
		xbean.LadderInfo xinfo = xtable.Pvpladder.get(rank);
		
		if(rankerId > 0)
			lock(Lockeys.get(xtable.Locks.ROLELOCK, roleId,rankerId));
		LadderRole ladderrole = LadderRole.getLadderRole(roleId, false);
		
		if(ladderrole.getMyRank() != selfrank && ladderrole.getRank() != selfrank)
		{
			ladderrole.sendSEnterLadder();
			Message.psendMsgNotify(roleId, 128);
			return false;//TODO 发送已经变化，重新战斗
		}
		if(xinfo == null)
		{
			if(rankerId > 0)
			{
				ladderrole.sendSEnterLadder();
				Message.psendMsgNotify(roleId, 128);
				return false;//TODO 发送已经变化，重新战斗
			}
		}
		else
		{
			if(xinfo.getRoleid() != rankerId)
			{
				ladderrole.sendSEnterLadder();
				Message.psendMsgNotify(roleId, 128);
				return false;//TODO 发送已经变化，重新战斗
			}
		}
		
		return ladderrole.challenge(rankerId,rank,isinvite);
	}
	
	
}
