package chuhan.gsp.gm;


import chuhan.gsp.exchange.ChargeRole;
import chuhan.gsp.game.SAddCashConfig;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.GameTime;

public class Cmd_addbill extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length < 2){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		final long roleId = Long.valueOf(args[0]);
		if (roleId == 0){
			sendToGM("参数格式错误:" + usage());
			return false;
		}
		
		final int addcashcfgId = Integer.valueOf(args[1]);
		if (addcashcfgId == 0){
			sendToGM("参数格式错误:" + usage());
			return false;
		}
		
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
				ChargeRole crole = ChargeRole.getChargeRole(roleId, false);
				SAddCashConfig cfg = ConfigManager.getInstance().getConf(SAddCashConfig.class).get(addcashcfgId);
				if(!crole.isServerCharge(cfg))
				{
					sendToGM("没有该id的充值配置");
					return false;
				}
				long uid = crole.makeBillId();
				xbean.BillData xbilldata = xbean.Pod.newBillData();
				xbilldata.setBillid(uid);
				xbilldata.setConfirmtimes(0);
				xbilldata.setCreatetime(GameTime.currentTimeMillis());
				xbilldata.setGoodid(addcashcfgId);
				xbilldata.setGoodnum(1);
				xbilldata.setPresent(0);
				xbilldata.setPrice((float)cfg.price);
				xbilldata.setState(xbean.BillData.STATE_SENDED);
				crole.getData().getBills().put(uid, xbilldata);
				boolean succ = crole.confirmCharge(uid, cfg.price, "-1");
				sendToGM("创建并确认配置为："+addcashcfgId+"的账单"+(succ?"成功":"失败"));
				return succ;
			};
		}.submit();
		return true;
	}

	@Override
	String usage() {
		return "//addbill [roleid] [cashconfigid]";
	}

}