package chuhan.gsp.gm;

import chuhan.gsp.attr.PropRole;

public class Cmd_addhuo extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length < 1){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		final int huo = Integer.valueOf(args[0]);
		if (huo == 0){
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
				
				PropRole prole = PropRole.getPropRole(rid, false);
//				prole.addHuoli(huo);
				return true;
			};
		}.submit();
		return true;
	}

	@Override
	String usage() {
		return "//addhuo [addnumber] [account不填是自己]";
	}

}