package chuhan.gsp;

import chuhan.gsp.log.Module;
import chuhan.gsp.util.CheckName;

public class Japan extends LangueVersion {
	
	@Override
	public int checkName(String name) {
		int counter = 0;
		int specialCounter = 0;

		for (int i=0; i<name.length(); i++){
			//日文有3段unicode
			String oneWorld = name.substring(i, i+1);
			if (oneWorld.matches(getUnicodeLimit())){
				counter ++;
				continue;
			}
			if (oneWorld.matches("[\u3040-\u309f]")){
				counter ++;
				continue;
			}
			if (oneWorld.matches("[\u30a0-\u30ff]")){
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
				return CheckName.WORD_ILLEGALITY;
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

	@Override
	public int getMaxNameLen() {
		return 7;
	}

	@Override
	public int getMinNameLen() {
		return 2;
	}

	@Override
	protected String langueName() {
		return "日文";
	}

	@Override
	public String getUnicodeLimit() {
		return "[\u4e00-\u9fff]";
	}

}
