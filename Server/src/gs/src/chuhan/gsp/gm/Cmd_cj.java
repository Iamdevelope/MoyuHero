package chuhan.gsp.gm;

import java.util.concurrent.ExecutionException;

import chuhan.gsp.hero.PAddHero;

public class Cmd_cj extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length < 1){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		final int intype = Integer.valueOf(args[0]);
		if (intype == 0){
			sendToGM("参数格式错误:" + usage());
			return false;
		}
		
		long roleid = getGmroleid();




		return true;
	}

	@Override
	String usage() {
		return "//cj [type]";
	}

}