package chuhan.gsp.gm;

import java.util.LinkedList;
import java.util.List;

import chuhan.gsp.exchange.ChargeRole;

import xbean.BillRole;
import xdb.TTable.IWalk;

public class Cmd_clearfirstcharge extends GMCommand {

	@Override
	boolean exec(String[] args) {
		final List<Long> roleids = new LinkedList<Long>();
		xtable.Billroles.getTable().walk(new IWalk<Long, BillRole>() {
			@Override
			public boolean onRecord(Long k, BillRole v) {
				roleids.add(k);
				return true;
			}
		});
		for(final long roleid : roleids) {
			try {
				new xdb.Procedure(){
					protected boolean process() throws Exception {
						ChargeRole chargeRole = ChargeRole.getChargeRole(roleid, false);
						chargeRole.getData().setFirstcharge(0);
						return true;
					}
				}.submit().get();
			} catch(Exception e) {
				e.printStackTrace();
			}
		}
		sendToGM("首次充值记录清除成功");
		return true;
	}

	@Override
	String usage() {
		return "//clearfirstcharge";
	}

}
