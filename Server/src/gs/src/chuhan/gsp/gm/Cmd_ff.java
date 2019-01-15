package chuhan.gsp.gm;

import java.util.ArrayList;
import java.util.List;

import chuhan.gsp.battle.PNewBattle;

public class Cmd_ff extends GMCommand {

	@Override
	boolean exec(String[] args) {
		if(args.length < 1)
			return false;
		PNewBattle pbattle = null;
		if (args.length >= 1 && !args[0].contains("@")) {
			// 按战斗配置ID进战斗
			int battleConfId = Integer.valueOf(args[0]);
			int awardId = (args.length >= 2) ? Integer.valueOf(args[1]) : 0;
			pbattle = new PNewBattle(getGmroleid(), battleConfId, awardId, true, 0);
		} else {
			List<Integer> monsterIDs = new ArrayList<Integer>();
			// 怪物数量不能超过10个
			int num = 0;
			for (int i = 0; i < args.length && num < 10; i++) {
				String[] strs = args[i].split("@");
				if (strs.length != 2)
					return false;
				int monsterID = 0;
				int monsterNum = 0;
				try {
					monsterID = Integer.parseInt(strs[0].trim());
					monsterNum = Integer.parseInt(strs[1].trim());
				} catch (NumberFormatException e) {
					sendToGM("格式错误,@的左边是怪物id,右边是怪物数量");
					return false;
				}
				for (int j = 0; j < monsterNum && num < 10; j++) {
					monsterIDs.add(monsterID);
					num++;
				}
			}
			int awardId = (args.length >= 2) ? Integer.valueOf(args[1]) : 0;
			pbattle = PNewBattle.createBattleWithMonsterIds(getGmroleid(), monsterIDs, awardId, true, 0);
		}
		pbattle.getHostFriends().add(4097l);
		pbattle.getHostFriends().add(8193l);
		pbattle.getHostFriends().add(12289l);
		pbattle.getHostFriends().add(16385l);
		pbattle.getHostFriends().add(20481l);
		pbattle.submit();
		return true;
	}

	@Override
	String usage() {
		return "//ff [battleId] [awardId]";
	}
	
	

}
