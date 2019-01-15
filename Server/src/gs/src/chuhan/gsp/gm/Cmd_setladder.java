package chuhan.gsp.gm;

import chuhan.gsp.battle.LadderRole;

public class Cmd_setladder extends GMCommand {
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
				
				LadderRole lrole = LadderRole.getLadderRole(rid, false);
				if(!lrole.setRank(rank))
					return false;
				lrole.sendSEnterLadder();
				return true;
			};
		}.submit();
		return true;
	}

	@Override
	String usage() {
		return "//setladder [dstRank] [srcRoleid] srcRoleid与dstRank[1,20000]位置互换,srcRoleid不填是自己";
	}

}