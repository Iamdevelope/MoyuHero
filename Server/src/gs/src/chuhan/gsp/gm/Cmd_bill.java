package chuhan.gsp.gm;


import chuhan.gsp.exchange.ChargeRole;

public class Cmd_bill extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length < 1){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		final long gamebillid = Long.valueOf(args[0]);
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
				xbean.AppReceiptData xreceipt = xtable.Appreceiptes.get(gamebillid);
				if(xreceipt == null)
				{
					sendToGM("没有该账单");
					return false;
				}
				ChargeRole crole = ChargeRole.getChargeRole(xreceipt.getRoleid(), false);
				long billid = gamebillid;
				xbean.BillData xbill =  crole.getData().getBills().get(billid);
				if(xbill == null)
				{
					GMCommand.logger.info("Cmd_confirmbill roleid = " + xreceipt.getRoleid() + ", billid = "+billid + " not exsit.");
					return false;
				}
				int oldstate = xbill.getState();
				boolean succ = crole.confirmCharge(billid, xbill.getPrice(), "111");
				GMCommand.logger.info("Cmd_confirmbill roleid = " + xreceipt.getRoleid() + ", billid = "+billid + ", price = "+xbill.getPrice() +", oldstate = "+ oldstate+", confirm="+succ);
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