package chuhan.gsp.gm;

import chuhan.gsp.stage.StageRole;

public class Cmd_passgk extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length < 1){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		int guanka = Integer.valueOf(args[0]);
		if (guanka == 0){
			guanka = -99919;
		}
		long roleid = getGmroleid();
		if(args.length > 1)
		{
			roleid =GMInterface.getTargetRoleId(args[1]);
			if(roleid <= 0)
				roleid = getGmroleid();
			//roleid = Long.valueOf(args[1]);
		}
		final long rid = roleid;
		final int guankaid = guanka;
		
//		new PFightStageBattle(rid, guanka, (short)0, false).call();
//		new PEndFightStageBattle(rid, 3).call();
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
				StageRole stageRole = StageRole.getStageRole(rid);
				stageRole.onInitStage(guankaid);
				return true;
			};
		}.submit();
		return true;
	}

	@Override
	String usage() {
		return "//addti [addnumber] [account不填是自己]";
	}

}