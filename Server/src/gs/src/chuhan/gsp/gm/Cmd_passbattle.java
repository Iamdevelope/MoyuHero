package chuhan.gsp.gm;

import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.stage.StageRole;
import chuhan.gsp.task.chapterinfo23;
import chuhan.gsp.task.stage11;

public class Cmd_passbattle extends GMCommand {
	@Override
	boolean exec(String[] args) {
		final long tobattleId = (args.length > 0)? Integer.valueOf(args[0]) : 0;
		long roleid = getGmroleid();
		if (args.length > 1) {
			roleid = GMInterface.getTargetRoleId(args[1]);
			if (roleid <= 0)
				roleid = getGmroleid();
		}
		final long rid = roleid;
		new xdb.Procedure()
		{
			@Override
			protected boolean process() throws Exception {

				StageRole stagerole = StageRole.getStageRole(rid);
				xbean.StageInfo xstage = stagerole.getCurStage();
				chapterinfo23 fubencfg = ConfigManager.getInstance().getConf(chapterinfo23.class).get(xstage.getId());
				if(fubencfg == null)
					return false;
				xbean.StageBattleInfo xbattle = stagerole.getCurBattle();
				int stagebattleid = xbattle.getId();
				for(; stagebattleid%100 < 100; stagebattleid++)
				{
					stage11 guankacfg = ConfigManager.getInstance().getConf(stage11.class).get(stagebattleid);
					if(guankacfg == null)
						break;
					xbattle = stagerole.getCurBattle();
					if(xbattle == null)
						break;
					stagerole.refreshStageAndBattle(xstage, xbattle,3);
					if((xbattle.getId() % 100) > tobattleId)
						break;
				}
				return true;
			}
		}.submit();
		
		return true;
	}

	@Override
	String usage() {
		return "//passbattle [tobattleid,注意是短id] [account不填是自己]";
	}

}