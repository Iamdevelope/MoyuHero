package chuhan.gsp.gm;

import chuhan.gsp.main.GameTime;
import chuhan.gsp.util.DateUtil;

public class Cmd_clearblood extends GMCommand {
	@Override
	boolean exec(String[] args) {
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
				xbean.BloodRankList xranklist = xtable.Bloodranklist.get(1);
				if(xranklist == null)
					return true;
				long now = GameTime.currentTimeMillis();
				int curweek = DateUtil.getCurrentDay(now);
				xranklist.getRankers().clear();
				xranklist.setCurweek(curweek);
				sendToGM("血战排行清除成功");
				return true;
			};
		}.submit();
		return true;
	}

	@Override
	String usage() {
		return "//clearblood";
	}

}