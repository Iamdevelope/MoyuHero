package chuhan.gsp.task;

import java.util.TimerTask;

import chuhan.gsp.battle.PCalLadder;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.util.DateUtil;

public class EndOfWeekTask extends TimerTask {

	@Override
	public void run() {
		new PCalLadder().submit();
	}
	
	/**
	 * 获取当前时间到下周一凌晨0点还有多少毫秒
	 * @return 毫秒
	 */
	public static long getDisMilSec2NextMonday() {
		long now = GameTime.currentTimeMillis();
		/* DateUtil.getWeekFirstSecond(now) 当周周一的第一秒
		 * DateUtil.weekMills + DateUtil.getWeekFirstSecond(now) 下周一的第一秒*/
		return DateUtil.weekMills + DateUtil.getWeekFirstSecond(now) - now;
	}

}
