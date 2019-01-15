package chuhan.gsp.gm;

import java.util.Map;

import chuhan.gsp.game.newbieguide60;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.Message;
import chuhan.gsp.play.activity.ActivityManager;
import chuhan.gsp.stage.StageRole;

public class Cmd_passnew extends GMCommand {
	@Override
	boolean exec(String[] args) {
		
		long roleid = getGmroleid();

		final long rid = roleid;

		
//		new PFightStageBattle(rid, guanka, (short)0, false).call();
//		new PEndFightStageBattle(rid, 3).call();
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
				java.util.TreeMap<Integer,newbieguide60> initMap = ConfigManager.getInstance().getConf(newbieguide60.class);
				for(Map.Entry<Integer,newbieguide60> entry : initMap.entrySet()){
					ActivityManager.getInstance().addNewyindao(roleid, entry.getKey());
				}
				Message.psendMsgNotify(rid, 135);
				return true;
			};
		}.submit();
		Message.psendMsgNotify(rid, 135);
		return true;
	}

	@Override
	String usage() {
		return "//passnew";
	}

}