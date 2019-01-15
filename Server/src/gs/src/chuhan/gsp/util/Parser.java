package chuhan.gsp.util;


import org.apache.log4j.Logger;


public class Parser
{
	
	public static final Logger logger = Logger.getLogger(Parser.class);
	
	/**
	 * 将ID@odds的格式解析为ID2Odds，形如 1001@40;1002@60 (@前为ID，后为出现的几率)
	 * @param str
	 * @return 返回null时表示该str不合法,解析失败
	 */
	public static ID2Odds parseIdAndOdds(String str)
	{
		try
		{
			if(str == null || str.equals(""))
				return null;
			String[] strs = str.split(";");
			ID2Odds id2odds = new ID2Odds(strs.length);
			for (int i = 0 ; i < strs.length;i++)
			{
				String[] rands = strs[i].split("@");
				id2odds.ids[i] = Integer.valueOf(rands[0]);
				id2odds.odds[i] = Integer.valueOf(rands[1]);
			}
			return id2odds;
		}
		catch(Exception e)
		{
			logger.error("Parse IdAndOdds error. String = " + str , e);
			e.printStackTrace();
		}
		return null;
		 
	}
	
	/**
	 * 将ID@odds的格式解析为ID2Odds，形如 1001@40,1002@60 (@前为ID，后为出现的几率)
	 * 与parseIdAndOdds的区别在于用逗号隔开而不是分号
	 * @param str
	 * @return 返回null时表示该str不合法,解析失败
	 */
	public static ID2Odds parseIdAndOddsWithComma(String str)
	{
		try
		{
			if(str == null || str.equals(""))
				return null;
			String[] strs = str.split(",");
			ID2Odds id2odds = new ID2Odds(strs.length);
			for (int i = 0 ; i < strs.length;i++)
			{
				String[] rands = strs[i].split("@");
				id2odds.ids[i] = Integer.valueOf(rands[0]);
				id2odds.odds[i] = Integer.valueOf(rands[1]);
			}
			return id2odds;
		}
		catch(Exception e)
		{
			logger.error("Parse IdAndOdds error. String = " + str , e);
			e.printStackTrace();
		}
		return null;
		 
	}
	
	
	public static class ID2Odds
	{
		public ID2Odds(int num )
		{
			ids = new int[num];
			odds = new int[num];
		}
		
		public final int[] ids;
		public final int[] odds;
		
		public Integer getRandomId()
		{
			int index = Misc.getProbability(odds);
			if(index == -1)
				return null;

			return ids[index];
		}
	}
	
/*	*//**
	 * 解析诸如
	 * @param effectstrs
	 * @return
	 * @throws Exception
	 *//*
	public static Map<Integer, Float> parseEffects(String effectstrs) throws Exception
	{
		Map<Integer, Float> effectMap = new HashMap<Integer, Float>();
		if (effectstrs != null && !effectstrs.equals(""))
		{
			String[] effectStr = effectstrs.split(";");
			for (int i = 0; i < effectStr.length; i++)
			{
				String[] tmpstrs = effectStr[i].split("=");
				if (tmpstrs.length < 2)
					throw new Exception("Wrong effects str.");

				int effectId = knight.gsp.effect.Module.getInstance().getIdByName(tmpstrs[0].trim());// 第一个“=”之前的字符串是Effectname
				Float effectValue = Float.valueOf((effectStr[i].substring(tmpstrs[0].length() + 1)).trim());// 取第一个“=”之后的字符串，是效果值
				effectMap.put(effectId, effectValue);
			}
		}
		return effectMap;
	}
	
	*//**
	 * 解析战斗中buff的效果公式，形如 hp_value=skilllevela*4+10;wound_value=-(skilllevela*4+10)
	 * @param effectstrs
	 * @return
	 * @throws Exception 解析失败时会抛出异常
	 *//*
	public static Map<Integer,JavaScript> parseFightJsEffects(String effectstrs) throws Exception
	{
		Map<Integer,JavaScript> effectMap = new HashMap<Integer, JavaScript>();
		if(effectstrs !=null && !effectstrs.equals(""))
		{
			String[] effectStr = effectstrs.split(";");
			for (int i = 0; i < effectStr.length; i++)
			{
				String[] tmpstrs = effectStr[i].split("=");
				if (tmpstrs.length < 2)
					throw new Exception("Wrong effects str：	  "+ effectstrs);

				int effectId = knight.gsp.effect.Module.getInstance().getIdByName(tmpstrs[0].trim());// 第一个“=”之前的字符串是Effectname
				String effectValueJS = effectStr[i].substring(tmpstrs[0].length() + 1);// 取第一个“=”之后的字符串，是JS公式
				JavaScript compiledJS = new JavaScript(effectValueJS);
				effectMap.put(effectId, compiledJS);
			}
		}
		return effectMap;
	}*/
	
	
	/**
	 * 将Exception的堆栈信息转化为字符串
	 * 因为log.error(String,Throwable)有时不记录堆栈信息
	 * @param e
	 * @return 堆栈信息
	 */
    public static String convertStackTrace2String(Exception e) {
    	String tracestr = e.toString();
		StackTraceElement[] trace = e.getStackTrace();
		for (int i = 0; i < trace.length; i++)
			tracestr += ("\tat " + trace[i] + "\n");
		return tracestr;
    }
	/**
	 * 将Exception的堆栈信息转化为字符串
	 * 因为log.error(String,Throwable)有时不记录堆栈信息
	 * @param e
	 * @return 堆栈信息
	 */
    public static String convertStackTrace2String(StackTraceElement[] trace) {
    	String tracestr = "";
		for (int i = 0; i < trace.length; i++)
			tracestr += ("\tat " + trace[i] + "\n");
		return tracestr;
    }
    
    public static String convertKnightStackTrace2String(StackTraceElement[] trace) {
    	StringBuilder tracestr = new StringBuilder();
		for (int i = 0; i < trace.length; i++){
			if (trace[i].toString().indexOf("knight")>-1)
				tracestr.append(trace[i]);
		}
		return tracestr.toString();
    }
}
