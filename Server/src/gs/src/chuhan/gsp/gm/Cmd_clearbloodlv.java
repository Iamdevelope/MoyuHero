package chuhan.gsp.gm;

import java.util.LinkedList;
import java.util.List;

import xbean.BloodRole;
import xdb.TTable.IWalk;

public class Cmd_clearbloodlv extends GMCommand {

	@Override
	boolean exec(String[] args) {
		final List<Long> roleids = new LinkedList<Long>();
		xtable.Bloodroles.getTable().walk(new IWalk<Long, BloodRole>() {
			@Override
			public boolean onRecord(Long k, BloodRole v) {
				roleids.add(k);
				return true;
			}
			
		});
		for(final long roleid : roleids) {
			try {
				new xdb.Procedure(){
					protected boolean process() throws Exception {
						chuhan.gsp.battle.BloodRole bloodRole = chuhan.gsp.battle.BloodRole.getBloodRole(roleid, false);
						bloodRole.getData().setMaxlevel(0);
						return true;
					}
				}.submit().get();
			} catch(Exception e) {
				e.printStackTrace();
			}
		}
		sendToGM("血战最高纪录清除成功");
		return true;
	}

	@Override
	String usage() {
		return "//clearbloodlv";
	}

}
