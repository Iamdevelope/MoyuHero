package chuhan.gsp.util;

public abstract class StringUtil {
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
}
