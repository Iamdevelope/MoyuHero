package chuhan.gsp.gm;

import chuhan.gsp.main.GameTime;

public class Cmd_showtime extends GMCommand {

	@Override
	boolean exec(String[] args) {
		sendToGM("当前服务器时间为:" + GameTime.currentCalendar().getTime()+"，比正常时间偏离："+GameTime.getDeltaStr());
		return true;
	}

	@Override
	String usage() {
		return "//showtime 打印当前时间";
	}

}
