package chuhan.gsp.gm;


import chuhan.gsp.PlatformTypeStr;
import chuhan.gsp.exchange.ChargeRole;
import chuhan.gsp.game.SAddCashConfig;
import chuhan.gsp.main.ConfigManager;

public class Cmd_verifybill extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length < 1){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		final long transactionid = Long.valueOf(args[0]);
		if (transactionid == 0){
			sendToGM("参数格式错误:" + usage());
			return false;
		}
		
		final long addcashcfgId = (args.length <= 1)? 0 : Long.valueOf(args[1]);
		
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
				xbean.AppReceiptData xreceipt = xtable.Appreceiptes.get(transactionid);
				if(xreceipt == null)
				{
					sendToGM("没有该tid的苹果订单");
					return false;
				}
				ChargeRole crole = ChargeRole.getChargeRole(xreceipt.getRoleid(), false);
				xbean.BillData xbill = crole.getBill(transactionid);
				String str = "订单存在，属于"+xreceipt.getRoleid()+"，状态："+xbill.getState()+"，重试次数："+xbill.getConfirmtimes();
				GMCommand.logger.info(str);
				sendToGM(str);
				if(xbill.getState() == xbean.BillData.STATE_CONFIRMED)
					return false;
				boolean succ = false;
				if(addcashcfgId <= 0)
					succ = crole.verifyReceipt(transactionid, xreceipt.getReceipt());
				else
				{
					SAddCashConfig addcashcfg = ConfigManager.getInstance().getConf(SAddCashConfig.class).get(addcashcfgId);
					if(addcashcfg == null 
							|| (!addcashcfg.platform.equalsIgnoreCase(PlatformTypeStr.AppStore)
									&& !addcashcfg.platform.equalsIgnoreCase(PlatformTypeStr.LaHu)
									&& !addcashcfg.platform.equalsIgnoreCase(PlatformTypeStr.AppT)))
					{
						sendToGM("该配置ID的APP充值不存在");
						return false;
					}
					succ = crole.confirmChargeByAppStore(transactionid, addcashcfg.name, addcashcfg.price, "000");
				}
				sendToGM("验单成功?"+succ);
				return succ;
			};
		}.submit();
		return true;
	}

	@Override
	String usage() {
		return "//verifybill [transactionid]";
	}

}