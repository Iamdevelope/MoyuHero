package chuhan.gsp.gm;

import chuhan.gsp.battle.PCalLadder;

/**
 * 根据天梯第一名在位时长方法奖励
 *
 */
public class Cmd_calladder extends GMCommand {

	@Override
	boolean exec(String[] args) {
		new PCalLadder().submit();
		return true;
	}

	@Override
	String usage() {
		return "//calLadder [无]";
	}

}
