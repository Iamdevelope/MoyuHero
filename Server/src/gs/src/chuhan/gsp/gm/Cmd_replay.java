package chuhan.gsp.gm;

import chuhan.gsp.battle.BattleScriptReplay;
import chuhan.gsp.play.ranking.endlessRanking;

public class Cmd_replay extends GMCommand {
	@Override
	boolean exec(String[] args) {
/*		long roleId = (args.length < 1)? getGmroleid() : Long.valueOf(args[0]);
		int verseindex = (args.length < 2)? 1 : Integer.valueOf(args[1]);
		BattleScriptReplay.sendScript(getGmroleid(), roleId, verseindex);*/
		endlessRanking.getInstance().endlessRankTime(true);
		return true;
	}

	@Override
	String usage() {
		return "//replay [roleid不填是自己] [倒数第几场]";
	}

}