package chuhan.gsp.util;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.List;

import chuhan.gsp.main.GameTime;

public class DateUtil {
	public static final long minuteMills = 1000 * 60;
	public static final long hourMills = 1000 * 60 * 60;
	public static final long dayMills = hourMills * 24;

	public static final long weekMills = dayMills * 7;
	
	public static final long TIME_ZONE_OFFSET = Calendar.getInstance().getTimeZone().getRawOffset();//时区导致的差值
	
	public static final long addMills = 1000 * 60 * 60 * 24 * 3;//Java时间的第一天（1970.1.1）是周四，这个是把一周的前三天补全的时间（以周一为第一天）
	
	public static int getCurrentDay(long time) {

		return (int) ((time + TIME_ZONE_OFFSET) / dayMills);
	}
	
	//by yanglk 以非0点为计算时间（例如凌晨三点，则changetime为-3*60*60*1000）
	public static int getCurrentDay(long time, long changetime) {
		return (int) ((time + TIME_ZONE_OFFSET + changetime) / dayMills);
	}

	public static int getCurrentWeek(long time) {

		return (int) ((time + TIME_ZONE_OFFSET + addMills) / weekMills) + 1;
	}
	/**
	 * 是否是今天或者是昨天，判断连续签到是否断签
	 * @param now
	 * @param lastTime
	 * @return
	 */
	public static boolean isTodayOrYestoday(long now,long lastTime){
		if(inTheSameDay(now,lastTime)){
			return true;
		}
		if(isYestoday(now,lastTime)){
			return true;
		}
		return false;
	}
	/**
	 * 是否是昨天
	 * @param now
	 * @param lastTime
	 * @return
	 */
	public static boolean isYestoday(long now,long lastTime){
		if(inTheSameDay(now - dayMills,lastTime)){
			return true;
		}
		return false;
	}
	
	public static boolean inTheSameDay(long firstT,long secondT) {
		if (getCurrentDay(firstT) == getCurrentDay(secondT))
			return true;
		return false;
	}
	
	public static boolean inTheSameDay(long firstT,long secondT,long changetime) {
		if (getCurrentDay(firstT,changetime) == getCurrentDay(secondT,changetime))
			return true;
		return false;
	}
	public static boolean inTheSameWeek(long firstT,long secondT) {
		if (getCurrentWeek(firstT) == getCurrentWeek(secondT))
			return true;
		return false;
	}
	//获得今天的最后一秒
	public static Calendar getLastSecondCalendar(){
		Calendar current = Calendar.getInstance();
		current.set(Calendar.HOUR_OF_DAY, 23);
		current.set(Calendar.MINUTE, 59);
		current.set(Calendar.SECOND, 59);
		return current;
	}
	
	//获取某天的第一秒 add by lc
	public static long getDayFirstSecond(long time){
		return time - (time + TIME_ZONE_OFFSET) % dayMills;
	}
	//获取某周的第一秒（以 周一算） add by lc
	public static long getWeekFirstSecond(long time){
		return time - (time + TIME_ZONE_OFFSET + addMills) % weekMills;
	}
	
	//获取时间是周几
	public static int getCurrentWeekDay(){
		long time = GameTime.currentTimeMillis();
		return (int)((time - getWeekFirstSecond(time))/dayMills + 1);
	}
	
	//获取时间是周几
	public static int getCurrentWeekDay(long time){
		return (int)((time - getWeekFirstSecond(time))/dayMills + 1);
	}
	
	//获取该月的第一秒
	public static long getMonthFirstSecond(long time){
		final Calendar cal = Calendar.getInstance();
		cal.setTimeInMillis(time);
		cal.set(Calendar.DAY_OF_MONTH, 1);
		cal.set(Calendar.HOUR_OF_DAY, 0);
		cal.set(Calendar.MINUTE, 0);
		cal.set(Calendar.SECOND, 0);
		cal.set(Calendar.MILLISECOND, 0);
		return cal.getTimeInMillis();
	}
	//获取该月的最后一秒
	public static long getMonthLastSecond(long time){
		final Calendar cal = Calendar.getInstance();
		cal.setTimeInMillis(time);
		cal.set(Calendar.DAY_OF_MONTH, cal.getActualMaximum(Calendar.DAY_OF_MONTH));
		cal.set(Calendar.HOUR_OF_DAY, 23);
		cal.set(Calendar.MINUTE, 59);
		cal.set(Calendar.SECOND, 59);
		cal.set(Calendar.MILLISECOND, 0);
		return cal.getTimeInMillis();
	}
	public static boolean inTheSameMonth(long firstT, long secondT){
		final Calendar cal1 = Calendar.getInstance();
		final Calendar cal2 = Calendar.getInstance();
		cal1.setTimeInMillis(firstT);
		cal2.setTimeInMillis(secondT);
		
		if (cal1.get(Calendar.YEAR) == cal2.get(Calendar.YEAR)
				&& cal1.get(Calendar.MONTH) == cal2.get(Calendar.MONTH))
			return true;
		return false;
	}
	
	//by yanglk 以非0点为计算时间（例如凌晨三点，则changetime为-3*60*60*1000）
	public static boolean inTheSameMonth(long firstT, long secondT, long changetime){
		final Calendar cal1 = Calendar.getInstance();
		final Calendar cal2 = Calendar.getInstance();
		if(firstT + changetime > 0)
			firstT = firstT + changetime;
		if(secondT + changetime > 0)
			secondT = secondT + changetime;
		cal1.setTimeInMillis(firstT);
		cal2.setTimeInMillis(secondT);
		
		if (cal1.get(Calendar.YEAR) == cal2.get(Calendar.YEAR)
				&& cal1.get(Calendar.MONTH) == cal2.get(Calendar.MONTH))
			return true;
		return false;
	}
	
	/**
	 * 获得指定日期的String值，用于客户端显示或者服务器做日期比较
	 * 
	 * @return
	 */
	public static String getCurrentStringFormat(long currentDay){
		SimpleDateFormat fomat = new SimpleDateFormat("yyyy年M月d日");
		String currentTime = fomat.format(currentDay);
		
		return currentTime;
	}
	
	public static String getCurrentStringFormatEn(long currentTime){
		SimpleDateFormat fomat = new SimpleDateFormat("yyyy-MM-dd");
		String sCurrentTime = fomat.format(currentTime);
		
		return sCurrentTime;
	}
	
	/**
	 * 获得一段时间的描述，如: X天X时X分
	 * 
	 * @return
	 */
	public static String getPeriodStringFormat(long period){
		
		long daynum = period /  dayMills;
		long dayret = period %  dayMills;
		
		long hournum = dayret / hourMills;
		long hourret = dayret % hourMills;
		
		long minutenum = hourret / minuteMills;
		
		return daynum + "天" + hournum + "时" + minutenum + "分";
	}
	
	/**
	 * 获得一段时间的短描述，如: X天X小时X分钟，高级的单位没有时则不存在，例如小于1天则变成X小时X分钟
	 * 
	 * @return
	 */
	public static String getPeriodShortFormat(long period){
		
		long daynum = period /  dayMills;
		long dayret = period %  dayMills;
		
		long hournum = dayret / hourMills;
		long hourret = dayret % hourMills;
		
		long minutenum = hourret / minuteMills;
		if(daynum > 0)
			return daynum + "天" + hournum + "小时" + minutenum + "分钟";
		else if(hournum > 0)
			return hournum + "小时" + minutenum + "分钟";
		else if(minutenum > 0)
			return minutenum + "分钟";
		else
			return "1分钟";
	}
	
	/**
	 * 获得指定日期的String值"yyyy-M-d HH:mm:ss"，用于客户端显示或者服务器做日期比较
	 * 
	 * @return
	 */
	public static String getStringFormat2Second(long currentDay){
		SimpleDateFormat fomat = new SimpleDateFormat("yyyy-M-d HH:mm:ss");
		String currentTime = fomat.format(currentDay);
		
		return currentTime;
	}
	
	/**
	 * 将yyyy-MM-dd HH:mm:ss格式的时间转化为long
	 * @param str
	 * @return
	 */
	public static long parseDate(String str) {
		try {
			SimpleDateFormat simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
			return simpleDateFormat.parse(str).getTime();
		} catch (Exception e) {
			throw new RuntimeException("时间格式错误：" + str, e);
		}
	}
	
	/**
	 * 根据类型解析相应格式，时间转化为long
	 * @param str
	 * @param type
	 * @return
	 */
	public static long parseDate(String str,String type) {
		try {
			SimpleDateFormat simpleDateFormat = new SimpleDateFormat(type);
			return simpleDateFormat.parse(str).getTime();
		} catch (Exception e) {
			throw new RuntimeException("时间格式错误：" + str, e);
		}
	}
	
	/**
	 * 传入一个开始时间和结束时间，判断当前时间是否在之间
	 * @param startTime 开始时间 =null("")表示没有开始时间
	 * @param endTime 结束时间 =null("")表示没有结束时间
	 * @return
	 */
	public static boolean isRunning(long now,String startTime, String endTime,String type) {
		try {
			if(CollectionUtil.isEmpty(startTime)) {//没有开始时间
				if(CollectionUtil.isEmpty(endTime)) {//没有结束时间
					return true;
				} else if(now <= parseDate(endTime,type)) {//在结束时间之前
					return true;
				} else {//在结束时间之后
					return false;
				}
			} else {//有开始时间
				if(CollectionUtil.isEmpty(endTime)) {//没有结束时间
					if(now >= parseDate(startTime,type)) {//在开始时间之后
						return true;
					} else {//在开始时间之前
						return false;
					}
				} else {//有结束时间
					if(now < parseDate(startTime,type)
							|| now > parseDate(endTime,type)) {
						return false;
					} else {
						return true;
					}
				}
			}
		} catch(Exception e) {
			e.printStackTrace();
			return false;
		}
	}
	
	/**
	 * 获取下一个整点
	 * @param time
	 * @return
	 */
	public static long getNextIntegral(long time) {
		Calendar calendar = Calendar.getInstance();
		calendar.setTimeInMillis(time);
		calendar.set(Calendar.HOUR_OF_DAY, calendar.get(Calendar.HOUR_OF_DAY) + 1);
		calendar.set(Calendar.MINUTE, 0);
		calendar.set(Calendar.SECOND, 0);
		calendar.set(Calendar.MILLISECOND, 0);
		
		return calendar.getTimeInMillis();
	}
	
	public static void main(String[] args) {
		System.out.println(getStringFormat2Second(getNextIntegral(System.currentTimeMillis())));
	}
	
	/**
	 * 传入一个开始时间和结束时间，判断当前时间是否在之间
	 * @param startTime 开始时间 =null("")表示没有开始时间
	 * @param endTime 结束时间 =null("")表示没有结束时间
	 * @return
	 */
	public static boolean isRunning(String startTime, String endTime) {
		long now = GameTime.currentTimeMillis();
		try {
			if(CollectionUtil.isEmpty(startTime)) {//没有开始时间
				if(CollectionUtil.isEmpty(endTime)) {//没有结束时间
					return true;
				} else if(now <= parseDate(endTime)) {//在结束时间之前
					return true;
				} else {//在结束时间之后
					return false;
				}
			} else {//有开始时间
				if(CollectionUtil.isEmpty(endTime)) {//没有结束时间
					if(now >= parseDate(startTime)) {//在开始时间之后
						return true;
					} else {//在开始时间之前
						return false;
					}
				} else {//有结束时间
					if(now < parseDate(startTime)
							|| now > parseDate(endTime)) {
						return false;
					} else {
						return true;
					}
				}
			}
		} catch(Exception e) {
			e.printStackTrace();
			return false;
		}
	}
	
	/**
	 * 根据每天第一秒时间和小时和分钟，计算出相应的时间
	 * @param todayFirstSecond
	 * @param intTimeList	size为2，第一个为小时，第二个为分钟
	 * @return
	 */
	public static long getTimeByHourMinute(long todayFirstSecond,List<Integer> intTimeList){
		if(intTimeList.size() != 2){
			return -1;
		}
		long time = todayFirstSecond + intTimeList.get(0) * hourMills + 
				intTimeList.get(1) * minuteMills;
		return time;
	}
	/**
	 * 根据每天第一秒时间和小时和分钟，计算出相应的时间
	 * @param todayFirstSecond
	 * @param hour
	 * @param minute
	 * @return
	 */
	public static long getTimeByHourMinute(long todayFirstSecond,int hour,int minute){
		long time = todayFirstSecond + hour * hourMills + 
				minute * minuteMills;
		return time;
	}
	
	/**
	 * 判断是否在一天的定时时间内
	 * @param now
	 * @param strTime	格式：12:00-13:00
	 * @return
	 */
	public static int isRunningOnday(long now, String strTime) {
		long todayFirstSecond = getDayFirstSecond(now);
		try {
			List<String> timeList = ParserString.parseString(strTime, "-");
			if(timeList.size() != 2){
				return -1;
			}
			List<Integer> beginTimeList = ParserString.parseString2Int(timeList.get(0),":");
			long timeBegin = getTimeByHourMinute(todayFirstSecond,beginTimeList);
			if(timeBegin == -1 || now < timeBegin){
				return -1;
			}
			
			List<Integer> endTimeList = ParserString.parseString2Int(timeList.get(1),":");
			long timeend = getTimeByHourMinute(todayFirstSecond,endTimeList);
			if(timeend == -1 || now > timeend){
				return -1;
			}
			return (int) ((timeend - now) / 1000);
		} catch(Exception e) {
			e.printStackTrace();
			return -1;
		}
	}
	
	/**
	 * 判断两次事件是否在一个事件段内
	 * @param now	当前时间
	 * @param lasttime	上一次时间
	 * @param strTime	几个时间点,格式0#6#12#18
	 * @return
	 */
	public static boolean isRunningOnSamedayAndTime(long now,long lasttime, String strTime) {
		if(!inTheSameDay(now,lasttime)){
			return false;
		}
		long todayFirstSecond = getDayFirstSecond(now);
		try {
			List<Integer> timeList = ParserString.parseString2Int(strTime);
			if(timeList == null || timeList.size() < 2){
				return false;
			}
			for(int i = 0;i<timeList.size() - 1;i++){
				long begin = getTimeByHourMinute(todayFirstSecond,timeList.get(i),0);
				long end = getTimeByHourMinute(todayFirstSecond,timeList.get(i+1),0);
				if(now >= begin && now <= end && lasttime >= begin && lasttime <= end){
					return true;
				}
			}
			return false;
		} catch(Exception e) {
			e.printStackTrace();
			return false;
		}
	}
	/**
	 * 判断距离时间点的最小时间
	 * @param now
	 * @param strTime  几个时间点,格式0#6#12#18
	 * @return
	 */
	public static long getTimeFromHour(long now, String strTime) {
		long todayFirstSecond = getDayFirstSecond(now);
		try {
			List<Integer> timeList = ParserString.parseString2Int(strTime);
			if(timeList == null || timeList.size() < 2){
				return -1;
			}
			timeList.add(24 + timeList.get(0));
			long result = -1;
			for(int i = 0;i<timeList.size() ;i++){
				long begin = getTimeByHourMinute(todayFirstSecond,timeList.get(i),0);
				long num = begin - now;
				if(num >=0){
					if(result == -1){
						result = num;
					}else if(num < result){
						result = num;
					}
				}
			}
			return result;
		} catch(Exception e) {
			e.printStackTrace();
			return -1;
		}
	}
	
	/**
	 * 获得开启时间
	 * @param now
	 * @param strTime  格式：12:00-13:00
	 * @return
	 */
	public static long getBeginTimeOnday(long now, String strTime) {
		long todayFirstSecond = getDayFirstSecond(now);
		try {
			List<String> timeList = ParserString.parseString(strTime, "-");
			if(timeList.size() != 2){
				return -1;
			}
			List<Integer> beginTimeList = ParserString.parseString2Int(timeList.get(0),":");
			long timeBegin = getTimeByHourMinute(todayFirstSecond,beginTimeList);
			return timeBegin;
		} catch(Exception e) {
			e.printStackTrace();
			return -1;
		}
	}
	/**
	 * 获得结束时间
	 * @param now
	 * @param strTime  格式：12:00-13:00
	 * @return
	 */
	public static long getEndTimeOnday(long now, String strTime) {
		long todayFirstSecond = getDayFirstSecond(now);
		try {
			List<String> timeList = ParserString.parseString(strTime, "-");
			if(timeList.size() != 2){
				return -1;
			}			
			List<Integer> endTimeList = ParserString.parseString2Int(timeList.get(1),":");
			long timeend = getTimeByHourMinute(todayFirstSecond,endTimeList);
			return timeend;
		} catch(Exception e) {
			e.printStackTrace();
			return -1;
		}
	}
	/**
	 * 获得开启的总秒数
	 * @param now
	 * @param strTime  格式：12:00-13:00
	 * @return
	 */
	public static long getOpenTimeMilli(long now, String strTime) {
		long todayFirstSecond = getDayFirstSecond(now);
		try {
			List<String> timeList = ParserString.parseString(strTime, "-");
			if(timeList.size() != 2){
				return -1;
			}
			List<Integer> beginTimeList = ParserString.parseString2Int(timeList.get(0),":");
			long timeBegin = getTimeByHourMinute(todayFirstSecond,beginTimeList);
			if(timeBegin == -1 || now < timeBegin){
				return -1;
			}
			
			List<Integer> endTimeList = ParserString.parseString2Int(timeList.get(1),":");
			long timeend = getTimeByHourMinute(todayFirstSecond,endTimeList);
			if(timeend == -1 || now > timeend){
				return -1;
			}
			return timeend - timeBegin;
		} catch(Exception e) {
			e.printStackTrace();
			return -1;
		}
	}
}
