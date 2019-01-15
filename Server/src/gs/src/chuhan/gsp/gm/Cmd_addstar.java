package chuhan.gsp.gm;

public class Cmd_addstar extends GMCommand {

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
		if(args.length > 1) {
			roleid =GMInterface.getTargetRoleId(args[1]);
			if(roleid <= 0)
				roleid = getGmroleid();
		}
		final long rid = roleid;
		new xdb.Procedure() {
			protected boolean process() throws Exception {
				xbean.BloodRole bloodRole = xtable.Bloodroles.get(rid);
				bloodRole.setTotalstar(bloodRole.getTotalstar() + star);
				return true;
			}
		}.submit();
		
		return true;
	}

	@Override
	String usage() {
		return "//addstar [addnumber] [account不填是自己]";
	}

}
