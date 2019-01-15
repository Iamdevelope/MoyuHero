package chuhan.gsp.util;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class StringValidateUtil {

	public static List<String> readValidateFile(String path) {
		List<String> list = new LinkedList<String>();
		File file = new File(path);

		if (!file.exists()) {
			chuhan.gsp.log.Module.logger.error("文件路径： " + path + "找不到相关文件");
			return list;
		}

		FileReader fileReader = null;
		BufferedReader bufferedReader = null;
		try {
			fileReader = new FileReader(file);
			bufferedReader = new BufferedReader(fileReader);
			String line = null;

			while ((line = bufferedReader.readLine()) != null) {
				// 如果是一行空串，越过
				if (line.trim().equals(""))
					continue;

				list.add(line.trim());
			}
		} catch (Exception e) {
			e.printStackTrace();
		} finally {
			try {
				if (null != fileReader && null != bufferedReader) {
					fileReader.close();
					bufferedReader.close();
				}
			} catch (IOException e) {
				e.printStackTrace();
			}
		}
		return list;
	}

	public static String convertIllegalWord(String keyWord) {
		if (keyWord == null)
			return "";

		char[] ch = keyWord.toCharArray();
		StringBuffer sbBuffer = new StringBuffer();
		for (int i = 0; i < ch.length; i++) {
			sbBuffer.append('*');
		}

		return sbBuffer.toString();
	}

	public static String checkAndReplaceIllegalWord(String content) {
		if (content == null || content.isEmpty())
			return content;
		for (String keyWord : chuhan.gsp.log.Module.getTextValidList()) {
			if (content.indexOf(keyWord) > -1) {
				content = content.replaceAll(keyWord, convertIllegalWord(keyWord));
			}
		}

		return content;
	}
	
	public static String checkAndReplaceIllegalWord(long roleid,int type,String content) {
		if (content == null)
			return content;

		for (String keyWord : chuhan.gsp.log.Module.getTextValidList()) {
			if (content.indexOf(keyWord) > -1) {
				logIllegalWord(roleid, type, content);
				content = content.replaceAll(keyWord, convertIllegalWord(keyWord));
			}
		}

		return content;
	}
	
	public static void doLogWhileHasIllegalWord(long roleid, int type, String content){
		if (content == null)
			return;
		
		for (String keyWord : chuhan.gsp.log.Module.getTextValidList()) {
			if (content.indexOf(keyWord) > -1) {
				logIllegalWord(roleid, type, content);
			}
		}
		
	}

	public static boolean checkIllegalWord(String content) {
		if (content == null)
			return false;

		for (String keyWord : chuhan.gsp.log.Module.getTextValidList()) {
			if (content.indexOf(keyWord) > -1) {
				return false;
			}
		}

		return true;
	}
	
	public static boolean checkValidName(String name){
		if (name == null)
			return false;
	
		for (String keyWord : chuhan.gsp.log.Module.getTextValidList()) {
			if (name.indexOf(keyWord) > -1) {
				return false;
			}
		}
	
		return true;
	}
	
	public static void writeValiateListToFile(List<String> list, String path) {
		if (list == null || list.size() == 0) {
			return;
		}

		//edit by lc [优化]停服时，以前的屏蔽字文件不再生成bak文件
		//File file = new File(path);
		File bakFile = new File(path + ".bak");
		if (bakFile.exists()) {
			bakFile.delete();
		}

		FileWriter fileWriter = null;
		BufferedWriter fw = null;
		try {
			//file.renameTo(bakFile);

			File newFile = new File(path);
			fileWriter = new FileWriter(newFile);
			fw = new BufferedWriter(fileWriter);
			boolean isFirst = true;
			for (String e : list) {
				e.trim();
				if(isFirst){
					isFirst = false;
				}else{
					fw.newLine();
				}
				fw.write(e.toCharArray());
			}
			fw.flush();

		} catch (Exception e) {
			e.printStackTrace();
		} finally {
			try {
				if(null != fileWriter && null != fw){
					fileWriter.close();
					fw.close();
				}
			} catch (IOException e) {
				e.printStackTrace();
			}
		}

	}
	public static void logIllegalWord(long roleid,int type,String content){
			/*byte[] tmp =EncodeBase64.transform(content.getBytes());
			String newContent = new String(tmp);
			Map<String, Object> param = new HashMap<String, Object>();
			LogUtil.putRoleBasicParams(roleid, param);
			param.put(RemoteLogParam.MSG,newContent );
			param.put(RemoteLogParam.TYPE,type);
			LogManager.getInstance().doLogWhileCommit(RemoteLogID.BANWORD, param);*/
	}
	
	private static Pattern pattern = Pattern.compile("(http:|https:)//[^[A-Za-z0-9\\._\\?%&+\\-=/#]]*");
	/**
	 * 将一个字符串分段，识别其中的url子串
	 * @param str 原字符串
	 * @param strphases 按照url分割原字符串后的子串列表
	 * @param urlindexes url子串在strphases中的index
	 */
	public static void matchURLsFromString(String str, List<String> strphases, java.util.Set<Integer> urlindexes)
	{
		Matcher matcher = pattern.matcher(str);
		if(matcher.groupCount() == 0)
			return;
		int whilenum = 0;
		int lastend = 0;
		int urlindex = 0;
		while(matcher.find())
		{
			int start = matcher.start();
			if(start > lastend)
			{
				strphases.add(str.substring(lastend, start));
				urlindex++;
			}
			strphases.add(matcher.group());
			urlindexes.add(urlindex);
			lastend = matcher.end();
			urlindex++;
			if(++whilenum > 40) return;//防止while过多
		}
		
		if(lastend < str.length())
			strphases.add(str.substring(lastend, str.length()));
		return;
	}
	
	/**
	 * 将带有url格式的字符串变成带有http标签能被客户端识别的字符串
	 * 例如：
	 * 请访问http://www.163.com或者http://www.sina.com浏览新闻
	 * 转化后为：
	 * <T t="请访问"></T><Http address="http://www.163.com"></Http><T t="或者"></T>
	 * <Http address="http://www.sina.com"></Http><T t="浏览新闻"></T>
	 * @param str 带有url格式的字符串
	 * @return 带有http标签能被客户端识别的字符串
	 */
	public static String convertStringToUrlLabelString(String str)
	{
		List<String> strphases = new ArrayList<String>();
		java.util.Set<Integer> urlindexes = new HashSet<Integer>();
		matchURLsFromString(str, strphases, urlindexes);
		if(strphases.isEmpty())
			return str;
		StringBuilder sb = new StringBuilder("<T t=\"\"></T>");
		for(int i = 0 ; i < strphases.size(); i++)
		{
			String phase = strphases.get(i);
			if(urlindexes.contains(i))
				sb.append("<Http t=\"").append(phase).append("\" address=\"").append(phase).append("\"></Http>");
			else
				sb.append("<T t=\"").append(phase).append("\"></T>");
		}
		return sb.toString();
	}
	
	public static void main(String[] args) {
		System.out.println(convertStringToUrlLabelString("请访问http://www.163.com或者http://www.sina.com浏览新闻"));
	}
}
