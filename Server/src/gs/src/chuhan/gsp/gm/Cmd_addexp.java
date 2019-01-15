package chuhan.gsp.gm;

import chuhan.gsp.attr.PAddExpProc;

public class Cmd_addexp extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length < 1){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		final int exp = Integer.valueOf(args[0]);
		if (exp == 0){
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
		final PAddExpProc addexpProc = new PAddExpProc(roleid, exp,PAddExpProc.OTHER,"Cmd_addexp添加");
		addexpProc.submit();
		return true;
	}

	@Override
	String usage() {
		return "//addexp [addnumber] [account不填是自己]";
	}

}