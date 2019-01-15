package chuhan.gsp.gm;

import chuhan.gsp.task.Ploy;

public class Cmd_enddoubleexp extends GMCommand {

	@Override
	boolean exec(String[] args) {
		Ploy.setDoubleExp(1);
		sendToGM("多倍经验活动结束成功");
		return true;
	}

	@Override
	String usage() {
		return "//enddoubleexp";
	}

}
