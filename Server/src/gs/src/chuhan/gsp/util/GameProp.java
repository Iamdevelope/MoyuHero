package chuhan.gsp.util;

import java.util.Properties;

import chuhan.gsp.main.ConfigManager;

/**
 * 封装一些常用的properties方法
 * 
 * 
 */
public class GameProp {

	/**
	 * 返回一个int型的value,注意这里没有catch runtime exception
	 * 
	 * @param prop
	 * @param key
	 * @return
	 */
	public static int getIntValue(Properties prop, String key) {
		String value = prop.getProperty(key);
		return Integer.parseInt(value.trim());

	}

	/**
	 * 返回一个long型的value,注意这里没有catch runtime exception
	 * 
	 * @param prop
	 * @param key
	 * @return
	 */
	public static long getLongValue(Properties prop, String key) {
		String value = prop.getProperty(key);
		return Long.parseLong(value.trim());

	}

	public static float getFloatValue(Properties prop, String key) {
		String value = prop.getProperty(key);
		return Float.parseFloat(value.trim());

	}
	public static boolean getBooleanValue(Properties prop, String key) {
		String value = prop.getProperty(key);
		return value.trim().equals("1");
		
	}
   /**
    * 和getIntValue(Properties prop, String key)类似,好处是不用提供Properties的实例,只要提供
    * properties文件的名字(不包括扩展名.properties)就可以获得value
    * @param fName
    * @param key
    * @return
    */
	public static int getIntValue(String fName, String key) {
		Properties prop = ConfigManager.getInstance().getPropConf(fName);
		String value = prop.getProperty(key);
		return Integer.parseInt(value.trim());
	}

	public static long getLongValue(String fName, String key) {
		Properties prop = ConfigManager.getInstance().getPropConf(fName);
		String value = prop.getProperty(key);
		return Long.parseLong(value.trim());

	}

	public static float getFloatValue(String fName, String key) {
		Properties prop = ConfigManager.getInstance().getPropConf(fName);
		String value = prop.getProperty(key);
		return Float.parseFloat(value.trim());
	}
	public static boolean getBooleanValue(String fName, String key) {
		Properties prop = ConfigManager.getInstance().getPropConf(fName);
		String value = prop.getProperty(key);
		return value.trim().equals("1");
	}
	
}
