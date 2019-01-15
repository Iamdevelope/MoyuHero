package chuhan.gsp.gm;

import chuhan.gsp.main.GameTime;
import chuhan.gsp.stage.StageRole;
import chuhan.gsp.util.DateUtil;

public class Cmd_clearstage extends GMCommand {
	@Override
	boolean exec(String[] args) {
//		long roleId = getGmroleid();
		final long rid = getGmroleid();
		new xdb.Procedure(){
			protected boolean process() throws Exception {
				StageRole stagerole = StageRole.getStageRole(rid, false);
				stagerole.gmClearBattleTime();
				return true;
			};
		}.submit();
		return true;
	}

	@Override
	String usage() {
		return "//clearstage";
	}

}