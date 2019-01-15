package chuhan.gsp.gm;

import chuhan.gsp.battle.LadderRole;
import chuhan.gsp.battle.PAddBloodRank;

public class Cmd_bloodrank extends GMCommand {
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
		new PAddBloodRank(roleid, rank).submit();
		return true;
	}

	@Override
	String usage() {
		return "//bloodrank [关数] [srcRoleid不填是自己";
	}

}