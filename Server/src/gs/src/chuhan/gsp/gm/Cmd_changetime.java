package chuhan.gsp.gm;

import java.io.IOException;
import java.util.concurrent.ExecutionException;

import chuhan.gsp.hero.PAddHero;

public class Cmd_changetime extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length < 2){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		String str1 = args[0];
		String str2 = args[1];
		
		String osName = System.getProperty("os.name");
		String cmd = "";
		try {
			if (osName.matches("^(?i)Windows.*$")) {// Window 系统
				// 格式 HH:mm:ss
				cmd = "  cmd /c "+str1+" "+str2;
				Runtime.getRuntime().exec(cmd);
				// 格式：yyyy-MM-dd
//				cmd = " cmd /c date 2015-08-30";
//				Runtime.getRuntime().exec(cmd);
			} else {// Linux 系统
				// 格式：yyyyMMdd
				cmd = " sodo date -s 20090326";
				Runtime.getRuntime().exec(cmd);
				// 格式 HH:mm:ss
				cmd = " sodo date -s 22:35:00";
				Runtime.getRuntime().exec(cmd);
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		return true;
	}

	@Override
	String usage() {
		return "//changetime [time/date] [HH:mm:ss/yyyy-MM-dd]";
	}

}