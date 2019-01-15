package chuhan.gsp.gm;

import chuhan.gsp.battle.LadderRole;
import chuhan.gsp.util.Misc;

public class Cmd_fightladder extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length < 1){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		final int rank = Integer.valueOf(args[0]);
		if (rank < 0 || rank > LadderRole.MAX_RANK){
			sendToGM("参数格式错误:" + usage());
			return false;
		}
		long roleid = getGmroleid();
		if (args.length > 1) {
			roleid = GMInterface.getTargetRoleId(args[1]);
			if (roleid <= 0)
				roleid = getGmroleid();
		}
		final long rid = roleid;
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
				
				xbean.LadderInfo xladder = xtable.Pvpladder.select(rank);
				long rankerId = (xladder == null) ? Misc.getRandomBetween(-1,-3) : xladder.getRoleid();
				LadderRole lrole = LadderRole.getLadderRole(rid, false);
				return lrole.challenge(rankerId,rank, false);
			};
		}.submit();
		return true;
	}

	@Override
	String usage() {
		return "//fightladder [dstRank] [srcRoleid] srcRoleid与dstRank[1,20000]位置战斗,srcRoleid不填是自己";
	}

}