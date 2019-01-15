package chuhan.gsp.gm;

import chuhan.gsp.log.LogManager;


public class Cmd_showlog extends GMCommand {

	@Override
	boolean exec(String[] args) {
		if(args.length < 1){
			sendToGM("GM命令格式   : " + usage());
			return true;
		}
		
		long roleId = 0;
		String swich = "";
		if(args.length == 1)
		{
			roleId = getGmroleid();
			swich = args[0];
		}
		else
		{
			roleId = Long.parseLong(args[0]);
			swich = args[1];
		}
		xbean.Properties role = xtable.Properties.select(roleId);
		if(null == role){
			sendToGM("roleid错误");
			return true;
		}
		boolean isOn = false;
		if("on".equals(swich)){
			isOn = true;
		}else if("off".equals(swich)){
			isOn = false;
		}else{
			sendToGM("GM命令格式错误  : " + usage());
			return true;
		}
		LogManager.isShowLog = isOn;
		LogManager.showLogGMRoleid = roleId;
		sendToGM(" show log " + swich);
		return true;
	}

	@Override
	String usage() {
		return "//showlog [gmroleid] on||off";
	}

}
