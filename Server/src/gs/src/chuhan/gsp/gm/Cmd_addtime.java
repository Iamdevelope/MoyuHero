package chuhan.gsp.gm;

import java.util.Calendar;

import chuhan.gsp.main.GameTime;

public class Cmd_addtime extends GMCommand {

	@Override
	boolean exec(String[] args) {
		final int field;
		final int amount;
		if (args.length < 1) {
			sendToGM(usage());
			return false;
		}
		if (!args[0].matches("-?\\d+")) {
			sendToGM("请输入时间量");
			return false;
		}
		amount = Integer.parseInt(args[0]);
		if (args.length < 2) {
			field = Calendar.MINUTE;
		} else {
			if (args[1].equals("小时")) {
				field = Calendar.HOUR_OF_DAY;
			} else if (args[1].equals("分钟")) {
				field = Calendar.MINUTE;
			} else if (args[1].equals("天")) {
				field = Calendar.DAY_OF_YEAR;
			} else if (args[1].equals("星期")) {
				field = Calendar.WEEK_OF_YEAR;
			} else {
				sendToGM("请输入正确的单位.小时/分钟/天/星期");
				return false;
			}
		}
		Calendar newtime = GameTime.currentCalendar();
		newtime.add(field, amount);
		GameTime.setTime(newtime);
		sendToGM("设置时间成功,当前服务器时间为:" + newtime.getTime()+"，比正常时间偏离："+GameTime.getDeltaStr());
		return true;
	}

	@Override
	String usage() {
		return "//addtime [添加的量] [单位:分钟/小时/天/星期，没有单位则为分钟]";
	}

}
