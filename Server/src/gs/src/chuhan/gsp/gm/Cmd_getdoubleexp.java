package chuhan.gsp.gm;

import chuhan.gsp.task.Ploy;

public class Cmd_getdoubleexp extends GMCommand {

	@Override
	boolean exec(String[] args) {
		sendToGM(Ploy.getDoubleExp() + "倍");
		return true;
	}

	@Override
	String usage() {
		return "//getdoubleexp";
	}

}
