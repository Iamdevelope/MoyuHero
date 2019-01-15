package chuhan.gsp.main;

import java.util.Calendar;

import chuhan.gsp.SGameTime;
import chuhan.gsp.msg.Message;
import chuhan.gsp.util.DateUtil;

public class GameTime {
	private static volatile long deltatime;
	/**
	 * 设置时间
	 * @param newtime
	 * @return
	 */
	public static long setTime(Calendar newtime)
	{
		long newtimemilli = newtime.getTimeInMillis();
		deltatime = (newtimemilli - System.currentTimeMillis());
		broadcastGameTime();
		return deltatime;
	}
	
	/**
	 * 清除时间差为正常时间
	 */
	public static void resetTime()
	{
		deltatime = 0;
		broadcastGameTime();
	}
	
	public static long currentTimeMillis()
	{
		return System.currentTimeMillis() + deltatime;
	}
	
	public static Calendar currentCalendar()
	{
		Calendar c = Calendar.getInstance();
		c.setTimeInMillis(System.currentTimeMillis() + deltatime);
		return c;
	}
	
	public static long getDelta()
	{
		return deltatime;
	}
	
	public static String getDeltaStr()
	{
		return DateUtil.getPeriodShortFormat(deltatime);
	}
	
	public static void broadcastGameTime()
	{
		//广播给所有客户端
//		String str[] = {"测试","数据","text12345"};
//		gnet.link.Onlines.getInstance().broadcast(Message.getInstance().getMsgNotify(355, str));
		gnet.link.Onlines.getInstance().broadcast(new SGameTime( currentTimeMillis() ));
	}
	
	public static void sendGameTime(long roleId)
	{
		gnet.link.Onlines.getInstance().send(roleId, new SGameTime( currentTimeMillis() ));
	}
	
}
