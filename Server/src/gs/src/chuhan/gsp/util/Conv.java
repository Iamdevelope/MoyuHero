package chuhan.gsp.util;

import chuhan.gsp.log.Logger;

public abstract class Conv {
	
	public static Logger logger = Logger.getLogger(Conv.class);
	
	public static long toLong(int v)
	{
		return (long)v;
	}
	public static long toLong(short v)
	{
		return (long)v;
	}
	
	public static long toLong(byte v)
	{
		return (long)v;
	}
	
	public static long toLong(float v)
	{
		return (long)v;
	}
	
	
	public static int toInt(long v)
	{
		if(!(v >= Integer.MIN_VALUE && v<= Integer.MAX_VALUE))
		{
			logger.error("Conv Error : v = " + v + "\n"+chuhan.gsp.util.StringUtil.convertStackTrace2String(Thread.currentThread().getStackTrace()));
		}
		return (int)v;
	}
	public static int toInt(short v)
	{
		return (int)v;
	}
	
	public static int toInt(byte v)
	{
		return (int)v;
	}
	
	public static int toInt(float v)
	{
		return (int)v;
	}
	
	public static short toShort(int v)
	{
		if(!(v >= Short.MIN_VALUE && v<= Short.MAX_VALUE))
		{
			logger.error("Conv Error : v = " + v + "\n"+chuhan.gsp.util.StringUtil.convertStackTrace2String(Thread.currentThread().getStackTrace()));
		}
		return (short)v;
	}
	public static short toShort(long v)
	{
		if(!(v >= Short.MIN_VALUE && v<= Short.MAX_VALUE))
		{
			logger.error("Conv Error : v = " + v + "\n"+chuhan.gsp.util.StringUtil.convertStackTrace2String(Thread.currentThread().getStackTrace()));
		}
		return (short)v;
	}
	
	public static short toShort(float v)
	{
		if(!(v >= Short.MIN_VALUE && v<= Short.MAX_VALUE))
		{
			logger.error("Conv Error : v = " + v + "\n"+chuhan.gsp.util.StringUtil.convertStackTrace2String(Thread.currentThread().getStackTrace()));
		}
		return (short)v;
	}
	
	public static short toShort(byte v)
	{
		return (short)v;
	}
	
	public static byte toByte(int v)
	{
		if(!(v >= Byte.MIN_VALUE && v<= Byte.MAX_VALUE))
		{
			logger.error("Conv Error : v = " + v + "\n"+chuhan.gsp.util.StringUtil.convertStackTrace2String(Thread.currentThread().getStackTrace()));
		}
		return (byte)v;
	}
	public static byte toByte(long v)
	{
		if(!(v >= Byte.MIN_VALUE && v<= Byte.MAX_VALUE))
		{
			logger.error("Conv Error : v = " + v + "\n"+chuhan.gsp.util.StringUtil.convertStackTrace2String(Thread.currentThread().getStackTrace()));
		}
		return (byte)v;
	}
	
	public static byte toByte(short v)
	{
		if(!(v >= Byte.MIN_VALUE && v<= Byte.MAX_VALUE))
		{
			logger.error("Conv Error : v = " + v + "\n"+chuhan.gsp.util.StringUtil.convertStackTrace2String(Thread.currentThread().getStackTrace()));
		}
		return (byte)v;
	}
	
}
