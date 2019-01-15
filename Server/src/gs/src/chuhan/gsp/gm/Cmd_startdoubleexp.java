package chuhan.gsp.gm;

import chuhan.gsp.task.Ploy;

public class Cmd_startdoubleexp extends GMCommand {

	@Override
	boolean exec(String[] args) {
		int newValue = 0;
		if(args.length < 1) {
			newValue = 2;
		} else {
			newValue = Integer.valueOf(args[0]);
			newValue = newValue > 4 ? 4 : newValue;
			newValue = newValue < 1 ? 1 : newValue;
		}
		Ploy.setDoubleExp(newValue);
		sendToGM("多倍经验活动开启成功，当前为" + newValue + "倍");
		return true;
	}

	@Override
	String usage() {
		return "//startdoubleexp [倍数(1-4),不填为2]";
	}

}
