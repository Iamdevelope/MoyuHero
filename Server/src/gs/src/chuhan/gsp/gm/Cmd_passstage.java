package chuhan.gsp.gm;

import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.stage.StageRole;
import chuhan.gsp.task.stage11;

public class Cmd_passstage extends GMCommand {
	@Override
	boolean exec(String[] args) {
		long roleid = getGmroleid();
		if (args.length > 1) {
			roleid = GMInterface.getTargetRoleId(args[0]);
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
				for(int i = 1 ; i < 100; i ++)
				{
					int stagebattleid = xstage.getId()*100+i;
					xbean.StageBattleInfo xbattle = xstage.getStagebattles().get(stagebattleid);
					if(xbattle != null && xbattle.getMaxstar() > 0)
						continue;
					stage11 guankacfg = ConfigManager.getInstance().getConf(stage11.class).get(stagebattleid);
					if(guankacfg == null)
						break;
					if(xbattle == null)
					{
						xbattle = xbean.Pod.newStageBattleInfo();
						xstage.getStagebattles().put(stagebattleid, xbattle);
					}
					xbattle.setId(stagebattleid);
					xbattle.setLastfighttime(chuhan.gsp.main.GameTime.currentTimeMillis());
					xbattle.setMaxstar(3);
					xbattle.setFightnum(xbattle.getFightnum() + 1);
					
				}
				stagerole.sendAllStages();
				stagerole.onStageCompleted(xstage);
				return true;
			}
		}.submit();
		
		return true;
	}

	@Override
	String usage() {
		return "//passstage [account不填是自己]";
	}

}