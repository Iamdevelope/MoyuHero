package chuhan.gsp.util;


import java.util.ArrayList;
import java.util.List;

import org.apache.log4j.Logger;

/**
 * 解析一串字符串为list
 * @author aa
 *
 */
public class ParserString
{
	
	public static final Logger logger = Logger.getLogger(ParserString.class);
	
	private static final String FIRST_SPLIT = "#";
	
//	/*
	
	public static List<Integer> parseString2Int(String strin){
		return strList2IntList(parseString(strin,FIRST_SPLIT));
	}
	
	public static List<Integer> parseString2Int(String strin,String strsplit){
		return strList2IntList(parseString(strin,strsplit));
	}
	/**
	 * 将list<String>全转换成list<int>
	 * @param strList
	 * @return
	 */
	public static List<Integer> strList2IntList(List<String> strList)
	{
		try{
			if(strList == null || strList.size() == 0)
				return null;
			List<Integer> result = new ArrayList<Integer>();
			for(String str : strList){
				result.add(Integer.parseInt(str));
			}
			
			return result;
		}
		catch(Exception e)
		{
			logger.error("ParserString strList2IntList error." , e);
			e.printStackTrace();
		}
		return null; 
	}
	
	/**
	 * 解析string根据默认#分割字符串
	 * @param strin
	 * @return
	 */
	public static List<String> parseString(String strin){
		return parseString(strin,FIRST_SPLIT);
	}
	
	/**
	 * 解析string根据strsplit分割字符串
	 * @param strin
	 * @param strsplit
	 * @return
	 */
	public static List<String> parseString(String strin,String strsplit)
	{
		try
		{	
			if(strin == null || strin.equals(""))
				return null;
			List<String> result = new ArrayList<String>();
			if (strin.contains(strsplit) || strsplit.contains("|")) {
				if(strsplit.equals("|")){
					strsplit = "\\|";
				}
				String[] strArray = strin.split(strsplit);
				for(String str : strArray) {
					result.add(str);
				}
			}else{
				result.add(strin);
			}
			return result;
		}
		catch(Exception e)
		{
			logger.error("ParserString parseString error. String = " + strin , e);
			e.printStackTrace();
		}
		return null; 
	}

	/**
	 * 将list根据分隔符写成字符串
	 * @param inList
	 * @param strsplit
	 * @return
	 */
	public static String getStrFromList(List inList,String strsplit){
		StringBuffer result = new StringBuffer();
		for(int i = 0;i< inList.size();i++){
			result.append(inList.get(i));
			if(i != inList.size() - 1){
				result.append(strsplit);
			}
		}
		return result.toString();
	}
	
	
	
}
