package chuhan.gsp.util;

import java.util.Map;

import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.Message;
import chuhan.gsp.game.shieldcharacter58;
import chuhan.gsp.log.Module;


public class CheckName {
	
	public static final int SPECIAL_WORD_TOO_MANY = -1;//特殊字符过多
	public static final int NONE_CHARACTER = -2;//无汉字或字母
	public static final int WORD_ILLEGALITY = -3;//违禁字符
	public static final int WORD_ERROR_CHAR = -4;//非法字符
	public static final int WORD_SPACE = -5;//空格
	public static final int WORD_LEGAL = 0;//字符合法
	
	/**
	 * 字符校验，规则如下：
	 * 1.游戏中命名可以包括 
	 *	①a-z  A-Z  
	 *	②0-9   
	 *	③汉字( u4e00~u9fa5)
	 *	④上述特殊字符
	 *
	 * 2.特殊字符最多只能出现3个
	 * 
	 * 3.名字必须包含1个字母或1个汉字
	 * 
	 * @param name
	 * @return
	 */
	public static int checkValid(final String name){
		int counter = 0;
		int specialCounter = 0;
		
		// 匹配敏感字表
		java.util.TreeMap<Integer, shieldcharacter58> initMap = ConfigManager
				.getInstance().getConf(shieldcharacter58.class);
		for (Map.Entry<Integer, shieldcharacter58> data : initMap.entrySet()) {
			if (data.getValue().name == 1) {
				if (name.contains(data.getValue().word)) {
					return CheckName.WORD_ILLEGALITY;
				}
			}
		}
		if (name.contains(" ")) {
			return CheckName.WORD_SPACE;
		}

		for (int i=0; i<name.length(); i++){
			//匹配汉字
			if (name.substring(i, i+1).matches("[\u4e00-\u9fa5]")){
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
				counter ++;
				continue;
			}
		
			
			//最后匹配火星文，如都不符合，则字符非法
			boolean isIllegal = false;
			int length = Module.SPECIAL_CHAR_ARRAY.length;
			for(int j=0; j<length; j++){
				if(asc == Module.SPECIAL_CHAR_ARRAY[j]){
					specialCounter ++;
					isIllegal = true;
//					if(specialCounter > 3){
//						return SPECIAL_WORD_TOO_MANY;
//					}
					counter ++;
					break;
				}
			}
			if(!isIllegal){
				return WORD_ERROR_CHAR;
			}
				
		}
		
		if(counter < 1){
			return NONE_CHARACTER;
		}
		
		if (name.matches("(.*)(GM|Gm|gM|gm)(.*)")){
			return WORD_ILLEGALITY;
		}
		if (!chuhan.gsp.util.StringValidateUtil.checkValidName(name)) {
			return WORD_ILLEGALITY;
		}
		
		return WORD_LEGAL;
	}

	public static boolean checkAndSendMessage(long roleid,String name, boolean isProcedure){
		int resultCode = chuhan.gsp.util.CheckName.checkValid(name);
		int msgId = 0;
		if(resultCode == CheckName.WORD_ILLEGALITY){
			msgId = 142260;
		}else if(resultCode == CheckName.SPECIAL_WORD_TOO_MANY){
			msgId = 142294;
		}else if(resultCode == CheckName.NONE_CHARACTER){
			msgId = 142293;
		}
		
		if(msgId != 0){
			if(isProcedure){
				Message.psendMsgNotify(roleid, msgId);
			}else{
				Message.sendMsgNotify(roleid, msgId);
			}
			return false;
		}else{
			return true;
		}
		
	}
	
}
