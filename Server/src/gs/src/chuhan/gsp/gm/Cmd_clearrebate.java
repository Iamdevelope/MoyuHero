package chuhan.gsp.gm;

import java.util.LinkedList;
import java.util.List;

import xbean.RebateChargeActivityRole;
import xdb.TTable.IWalk;

public class Cmd_clearrebate extends GMCommand {

	@Override
	boolean exec(String[] args) {
		if(args.length < 1){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		final int activityId = Integer.valueOf(args[0]);
		final List<Long> roleids = new LinkedList<Long>();
		xtable.Rebatechargeactivities.getTable().walk(new IWalk<Long, RebateChargeActivityRole>() {
			@Override
			public boolean onRecord(Long k, RebateChargeActivityRole v) {
				roleids.add(k);
				return true;
			}
		});
		for(final long roleId : roleids) {
			try {
				new xdb.Procedure(){
					protected boolean process() throws Exception {
						xbean.RebateChargeActivityRole reabteChargeActivityRole = xtable.Rebatechargeactivities.get(roleId);
						if(null != reabteChargeActivityRole) {
							reabteChargeActivityRole.getActivities().remove(activityId);
						}
						
						return true;
					}
				}.submit().get();
			} catch(Exception e) {
				e.printStackTrace();
			}
		}
		
		return true;
	}

	@Override
	String usage() {
		return "//clrcharge [activityId]";
	}

}
