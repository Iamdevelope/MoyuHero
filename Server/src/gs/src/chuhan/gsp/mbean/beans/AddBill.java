package chuhan.gsp.mbean.beans;

import java.util.Map;

import chuhan.gsp.exchange.ChargeRole;
import chuhan.gsp.game.SAddCashConfig;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.mbean.AbstractRequestHandler;

public class AddBill extends AbstractRequestHandler {

	public AddBill(String name) {
		super(name);
	}

	@Override
	protected Map<Object, Object> handleRequest(Map<?, ?> paras) {
		try {
			String roleidStr = (String) paras.get("roleid");
			String platformStr = (String) paras.get("platform");
			String priceStr = (String) paras.get("price");
			if(null != roleidStr && null != platformStr && null != priceStr) {
				final Long roleid = Long.valueOf(roleidStr);
				final Integer price = Integer.valueOf(priceStr);
				final Map<Integer, SAddCashConfig> cfgs = ConfigManager.getInstance().getConf(SAddCashConfig.class);
				SAddCashConfig cfg = null;
				for(SAddCashConfig sAddCashConfig : cfgs.values()) {
					if(sAddCashConfig.platform.equals(platformStr)
							&& price.intValue() == sAddCashConfig.price) {
						cfg = sAddCashConfig;
						break;
					}
				}
				if(null == cfg) {
					return failedMsg("不存在的充值项目");
				}
				final SAddCashConfig fcfg = cfg;
				xbean.Properties properties = xtable.Properties.select(roleid);
				if (null == properties){
					return failedMsg("不存在的玩家roleid:" + roleidStr);
				} else {
					boolean succ = new xdb.Procedure() {
						protected boolean process() throws Exception {
							final ChargeRole crole = ChargeRole.getChargeRole(roleid, false);
							long uid = crole.makeBillId();
							xbean.BillData xbilldata = xbean.Pod.newBillData();
							xbilldata.setBillid(uid);
							xbilldata.setConfirmtimes(0);
							xbilldata.setCreatetime(GameTime.currentTimeMillis());
							xbilldata.setGoodid(fcfg.id);
							xbilldata.setGoodnum(1);
							xbilldata.setPresent(0);
							xbilldata.setPrice((float)fcfg.price);
							xbilldata.setState(xbean.BillData.STATE_SENDED);
							crole.getData().getBills().put(uid, xbilldata);
							return crole.confirmCharge(uid, fcfg.price, "-1");
						}
					}.submit().get().isSuccess();
					if(!succ) {
						return failedMsg("创建充值账单失败");
					}
					return successMsg();
				}
			} else {
				return failedMsg("需要参数roleid platform price");
			}
		} catch (Exception e) {
			e.printStackTrace();
			return failedMsg("执行出错");
		}
	}

}
