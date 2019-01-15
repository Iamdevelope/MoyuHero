package chuhan.gsp.gm;


import chuhan.gsp.exchange.ChargeRole;

public class Cmd_confirmbill extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length < 1){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		final long roleId = Long.valueOf(args[0]);
		if (roleId == 0){
			sendToGM("参数格式错误:" + usage());
			return false;
		}
		
		final long gamebillid = Long.valueOf(args[1]);
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
				ChargeRole crole = ChargeRole.getChargeRole(roleId, false);
				long billid = gamebillid;
				xbean.BillData xbill =  crole.getData().getBills().get(billid);
				if(xbill == null)
				{
					GMCommand.logger.info("Cmd_confirmbill roleid = " + roleId + ", billid = "+billid + " not exsit.");
					return false;
				}
				int oldstate = xbill.getState();
				boolean succ = crole.confirmCharge(billid, xbill.getPrice(), "111");
				GMCommand.logger.info("Cmd_confirmbill roleid = " + roleId + ", billid = "+billid + ", price = "+xbill.getPrice() +", oldstate = "+ oldstate+", confirm="+succ);
				return succ;
			};
		}.submit();
		return true;
	}

	@Override
	String usage() {
		return "//getbill [roleid] [gamebillid]";
	}

}