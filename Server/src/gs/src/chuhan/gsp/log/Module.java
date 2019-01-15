package chuhan.gsp.log;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStreamWriter;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import java.util.TreeMap;

import chuhan.gsp.LangueVersion;
import chuhan.gsp.main.ModuleInterface;
import chuhan.gsp.main.ReloadResult;

public class Module implements ModuleInterface {
	
	public static Logger logger = Logger.getLogger("Log");
	
	//火星文字符串
	public static final String SPECIAL_STRING = "丶〆╰つ╮ヽ∝°〝り≈ァ＇№↘↙←→のっ￡⊿△▽╭↓◇ジ§﹌ザ︶ㄣ☆⊕○◎⊙Θ〞∮づレ※しιふぶ︵┿┊√〃∷いらんごζ♂ベこ≮≯ㄟ灬ぇぃ︷ミ︿╯乆ゐ＊乷ξぷあ∠∫¢τヤぅべぜˇぺ゛゜「」↑↗↖─│╱╲╳¤♀〤〩ぁえぉおかがきぎくぐけげさざじすずせそぞただちぢてでとどなにぬねはばぱひびぴへほぼぽまみむめもゃやゅゆょよるれろゎわゑを";
	public static int[] SPECIAL_CHAR_ARRAY;
	//屏蔽字列表
	private static List<String> normalValidList;
	private static List<String> npcValidList;
	private static List<String> textValidList;

	private static Map<String, Integer> logid2Type = new HashMap<String, Integer>();
	private static Map<String, Integer> paramid2name = new HashMap<String, Integer>();
	private static int counter = 1;
	
	public static List<String> getNormalValidList(){
		return normalValidList;
	}
	
	public static List<String> getNpcValidList(){
		return npcValidList;
	}
	
	public static List<String> getTextValidList(){
		return textValidList;
	}
	private String getValidFilePath(){
		StringBuffer sb = new StringBuffer();
		sb.append("gamedata/files");
		
		return sb.toString();
	}
	@Override
	public void exit() {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void init() throws Exception {
		//createLogInterfaceAndParam();
		//读取屏蔽字文件
		//normalValidList = chuhan.gsp.util.StringValidateUtil.readValidateFile(getValidFilePath() + "/chatshield.txt");
		//npcValidList = chuhan.gsp.util.StringValidateUtil.readValidateFile(getValidFilePath() + "/nameshield.txt");
		String shieldFileName = "/shield_" + LangueVersion.getSuffix() + ".txt";
		textValidList = chuhan.gsp.util.StringValidateUtil.readValidateFile(getValidFilePath() + shieldFileName);
		//火星文字符转换
		int length = SPECIAL_STRING.toCharArray().length;
		SPECIAL_CHAR_ARRAY = new int[length];
		for(int i=0; i<length; i++){
			SPECIAL_CHAR_ARRAY[i] = SPECIAL_STRING.charAt(i);
		}
	}
	
	private void createLogInterfaceAndParam() {
		
		//解析log配置文件的参数
		parseLogFormatParam();
		
		TreeMap<Integer, String> newLogid2Type = new TreeMap<Integer, String>();
		TreeMap<Integer, String> newParamid2name = new TreeMap<Integer, String>();
		
		reverseMap(newLogid2Type, newParamid2name);
		
		try {
			genLogIDInterface(newLogid2Type, newParamid2name);
		} catch (Exception e) {
			e.printStackTrace();
			logger.error("生成log接口文件出错！  ", e);
		}
		
	}
	private void parseLogFormatParam() {
		Map<Integer, chuhan.gsp.log.SLogFormatConfig> configs = chuhan.gsp.main.ConfigManager.getInstance().getConf(chuhan.gsp.log.SLogFormatConfig.class);
		Iterator<Entry<Integer, SLogFormatConfig>> iterator = configs.entrySet().iterator();
		while(iterator.hasNext()){
			Entry<Integer, SLogFormatConfig> current = iterator.next();
			int logid = current.getKey();
			SLogFormatConfig config = current.getValue();
			String logFormat = config.getFormat();
			int index = logFormat.indexOf(":");
			if(-1 == index)
				continue;
			String logType = logFormat.substring(0,index).toUpperCase();
			if(logid2Type.get(logType) == null){
				logid2Type.put(logType, logid);
			}else{
				logger.error("Log配置表中第  " + logid + " 条Log的类型与其他有重复！");
			}
			
			putParam2Map(logFormat);
			
		}

		counter = 1;
	}
	
	private void putParam2Map(String logFormat) {
		boolean isEnd = false;
		int startIndex = 0;
		while(!isEnd){
			int index = logFormat.indexOf("$", startIndex);
			if(index == -1)
				break;
			
			int nexIndex = logFormat.indexOf("$", index + 1);
			if(nexIndex == -1)
				break;
			
			String param = logFormat.substring(index + 1, nexIndex).toUpperCase();
			
			if(paramid2name.get(param) == null){
				paramid2name.put(param, counter);
				counter ++;
			}
			startIndex = nexIndex + 1;
		}
		
	}
	private void reverseMap(TreeMap<Integer, String> newLogid2Type,	TreeMap<Integer, String> newParamid2name) {
		Iterator<Entry<String, Integer>> logidIterator = logid2Type.entrySet().iterator();
		while(logidIterator.hasNext()){
			Entry<String, Integer> current = logidIterator.next();
			newLogid2Type.put(current.getValue(), current.getKey());
		}
		
		Iterator<Entry<String, Integer>> param = paramid2name.entrySet().iterator();
		while(param.hasNext()){
			Entry<String, Integer> current = param.next();
			newParamid2name.put(current.getValue(), current.getKey());
		}
		
	}
	
	private void genLogIDInterface(TreeMap<Integer, String> logid, TreeMap<Integer, String> param) throws Exception {
		String filePath = getFilePath();
		File sourceFile = new File(filePath + "\\" + "RemoteLogID.java");
		
		if(sourceFile.exists()){
			sourceFile.delete();
		}
		
		sourceFile.createNewFile();
		
		writeInterFaceContent(sourceFile, logid, 1);
		
		File paramFile = new File(filePath + "\\" + "RemoteLogParam.java");
		if(paramFile.exists()){
			paramFile.delete();
		}
		paramFile.createNewFile();
		
		writeInterFaceContent(paramFile, param, 2);
		
	}

	private void writeComment(OutputStreamWriter writer, int commentType) throws IOException {
		if(commentType == 1){
			writer.write("	//日志的类型，为每条log第一个冒号前的单词\n");
			writer.write("	//例：login：account=hbe4589:userid=12542045:peer=58.22.232.12\n");
			writer.write("	//上例中日志的类型为：login\n");
		}
		
		if(commentType == 2){
			writer.write("	//日志的参数类型\n");
		}
		
	}
	
	private void writeInterFaceContent(File sourceFile, TreeMap<Integer, String> logid, int type) throws Exception {
		OutputStreamWriter writer = new OutputStreamWriter(new FileOutputStream(sourceFile));
		writer.write("package chuhan.gsp.log;\n");
		if(type == 1){
			writer.write("public interface RemoteLogID{\n");
		}else{
			writer.write("public interface RemoteLogParam{\n");
		}
		
		writeComment(writer, type);
		
		if(type == 1){
			Iterator<Entry<Integer, String>> logidIterator = logid.entrySet().iterator();
			while(logidIterator.hasNext()){
				Entry<Integer, String> current = logidIterator.next();
				writer.write("	public final static int " + current.getValue() + " = " + current.getKey() + ";\n");
			}
		}else{
			for(String value : logid.values()){
				writer.write("	public final static String " + value.toUpperCase() + " = \"" +  value.toLowerCase() + "\";\n");
			}
		}
		
		writer.write("}\n");
		
		writer.flush();
		
		writer.close();
	}

	
	private String getFilePath() {
		StringBuffer sb = new StringBuffer();
		
		String packageName = getClass().getPackage().getName().replace(".", "/");
		sb.append("src/" + packageName);
		
		return sb.toString();
	}
	
	@Override
	public ReloadResult reload() throws Exception {
		//normalValidList = chuhan.gsp.util.StringValidateUtil.readValidateFile(getValidFilePath() + "/chatshield.txt");
		//npcValidList = chuhan.gsp.util.StringValidateUtil.readValidateFile(getValidFilePath() + "/nameshield.txt");
		String shieldFileName = "/shield_" + LangueVersion.getSuffix() + ".txt";
		textValidList = chuhan.gsp.util.StringValidateUtil.readValidateFile(getValidFilePath() + shieldFileName);
		return new ReloadResult(true);
	}

}
