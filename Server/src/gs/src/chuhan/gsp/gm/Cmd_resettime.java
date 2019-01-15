package chuhan.gsp.gm;

import java.util.Calendar;

import chuhan.gsp.main.GameTime;

public class Cmd_resettime extends GMCommand {

	@Override
	boolean exec(String[] args) {
		Calendar newtime = GameTime.currentCalendar();
		String str = "设置时间成功,之前服务器时间为:" + newtime.getTime()+"，比正常时间偏离："+GameTime.getDeltaStr();
		GameTime.resetTime();
		sendToGM(str);
		return true;
	}

	@Override
	String usage() {
		return "//resettime";
	}

}
