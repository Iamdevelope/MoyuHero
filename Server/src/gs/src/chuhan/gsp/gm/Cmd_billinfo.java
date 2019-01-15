package chuhan.gsp.gm;


import chuhan.gsp.exchange.PGetRoleBills;

public class Cmd_billinfo extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length < 1){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		long gamebillid = Long.valueOf(args[0]);
		
		xbean.AppReceiptData xrecepit = xtable.Appreceiptes.select(gamebillid);
		if(xrecepit == null)
		{
			sendToGM("billid未找到");
			return false;
		}
		
		long roleId = xrecepit.getRoleid();
		final int state = 0;
		final String platbillid = null;
		
		new PGetRoleBills(getGmroleid(), roleId, state, platbillid, gamebillid).submit();
		return true;
	}

	@Override
	String usage() {
		return "//getbill [gamebillid]";
	}

}