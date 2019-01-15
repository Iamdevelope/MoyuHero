package chuhan.gsp;

import java.util.Map;

import org.apache.log4j.Logger;

import chuhan.gsp.game.shieldcharacter58;
import chuhan.gsp.log.Module;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.util.CheckName;

public abstract class LangueVersion {
	private static LangueVersion currentVersion;
	protected static final Logger logger = Logger.getLogger(LangueVersion.class);
	
	public abstract String getUnicodeLimit();
	public abstract int getMaxNameLen();
	public abstract int getMinNameLen();
	protected abstract String langueName();
	
	public static final void init() {
		String langueVersion = ConfigManager.getInstance().getPropConf("sys").getProperty("langueVersion");
		if(null == langueVersion) {
			langueVersion = "chuhan.gsp.SimpleChinese";
		}
		try {
			currentVersion = (LangueVersion) Class.forName(langueVersion).newInstance();
			logger.info("当前语言版本：" + currentVersion.langueName());
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	
	public static final LangueVersion getLangueVersion() {
		return currentVersion;
	}
	
	public static final String getSuffix() {
		return currentVersion.getClass().getSimpleName();
	}
	
	public static boolean isJapan() {
		return currentVersion instanceof Japan;
	}
	
	public int checkName(String name) {
		int counter = 0;
		int specialCounter = 0;

		for (int i=0; i<name.length(); i++){
			//匹配汉字
			if (name.substring(i, i+1).matches(getUnicodeLimit())){
				counter ++;
				continue;
			}
			
			//取得index位置的char Value
			final int asc = name.charAt(i);
			if (asc >='a' &&  asc<='z'
					|| asc >= 'A' && asc<='Z'){
				counter ++;
				continue;
			}
			
			if(asc >= '0' && asc <= '9'){
				continue;
			}
			
			//最后匹配火星文，如都不符合，则字符非法
			boolean isIllegal = false;
			int length = Module.SPECIAL_CHAR_ARRAY.length;
			for(int j=0; j<length; j++){
				if(asc == Module.SPECIAL_CHAR_ARRAY[j]){
					specialCounter ++;
					isIllegal = true;
					if(specialCounter > 3){
						return CheckName.SPECIAL_WORD_TOO_MANY;
					}
					break;
				}
			}
			if(!isIllegal){
				return CheckName.WORD_ERROR_CHAR;
			}
				
		}
		
		if(counter < 1){
			return CheckName.NONE_CHARACTER;
		}
		
		if (name.matches("(.*)(GM|Gm|gM|gm)(.*)")){
			return CheckName.WORD_ILLEGALITY;
		}
		if (!chuhan.gsp.util.StringValidateUtil.checkValidName(name)) {
			return CheckName.WORD_ILLEGALITY;
		}
		
		
		
		return CheckName.WORD_LEGAL;
	
	}
}
