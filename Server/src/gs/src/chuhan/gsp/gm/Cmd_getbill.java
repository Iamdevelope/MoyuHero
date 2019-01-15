package chuhan.gsp.gm;

import java.util.concurrent.ExecutionException;

import chuhan.gsp.exchange.ChargeRole;
import chuhan.gsp.exchange.PGetRoleBills;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.util.DateUtil;

public class Cmd_getbill extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length < 1){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		/*try {
			new xdb.Procedure()
			{
				protected boolean process() throws Exception {
					long now = GameTime.currentTimeMillis();
					ChargeRole crole = ChargeRole.getChargeRole(4097, false);	
					xbean.BillData xbill1 = xbean.Pod.newBillData();
					xbill1.setBillid(1);
					xbill1.setGoodid(11);
					xbill1.setPrice(1111);
					xbill1.setPlatbillid("111111");
					xbill1.setState(1);
					xbill1.setCreatetime(now);
					crole.getData().getBills().put(xbill1.getBillid(), xbill1);
					
					xbean.BillData xbill2 = xbean.Pod.newBillData();
					xbill2.setBillid(2);
					xbill2.setGoodid(12);
					xbill2.setPrice(1212);
					xbill2.setPlatbillid("121212");
					xbill2.setState(2);
					xbill2.setCreatetime(now- DateUtil.dayMills);
					crole.getData().getBills().put(xbill2.getBillid(), xbill2);
					
					xbean.BillData xbill3 = xbean.Pod.newBillData();
					xbill3.setBillid(3);
					xbill3.setGoodid(13);
					xbill3.setPrice(1313);
					xbill3.setPlatbillid("131313");
					xbill3.setState(4);
					xbill3.setCreatetime(now+ DateUtil.dayMills);
					crole.getData().getBills().put(xbill3.getBillid(), xbill3);
					return true;
				};
			}.submit().get();
		} catch (InterruptedException e) {
			e.printStackTrace();
		} catch (ExecutionException e) {
			e.printStackTrace();
		}*/
		
		final long roleId = Integer.valueOf(args[0]);
		if (roleId == 0){
			sendToGM("参数格式错误:" + usage());
			return false;
		}
		
		final int state = (args.length > 1) ? Integer.valueOf(args[1]) : 0;
		final String platbillid = (args.length > 2) ? args[2] :null;
		final long gamebillid = (args.length > 3) ? Long.valueOf(args[3]) :0;
		
		new PGetRoleBills(getGmroleid(), roleId, state, platbillid, gamebillid).submit();
		return true;
	}

	@Override
	String usage() {
		return "//getbill [roleid] [state:0不限,1:确认中,2:已确认,4:失败] [platbillid:0不限] [gamebillid:0不限] 按筛选类型查找角色的充值记录发送至GM信箱";
	}

}