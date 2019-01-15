package chuhan.gsp.gm;

import chuhan.gsp.battle.BloodRole;

public class Cmd_setstar extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length < 1){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		final int star = Integer.valueOf(args[0]);
		if (star == 0){
			sendToGM("参数格式错误:" + usage());
			return false;
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
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
				BloodRole brole = BloodRole.getBloodRole(rid, false);
				brole.getData().setTotalstar(star);
				return true;
			};
		}.submit();
		return true;
	}

	@Override
	String usage() {
		return "//addexp [addnumber] [account不填是自己]";
	}

}