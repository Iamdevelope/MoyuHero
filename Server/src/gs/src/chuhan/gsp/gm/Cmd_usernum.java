
package chuhan.gsp.gm;

import gnet.link.Onlines;

public class Cmd_usernum extends GMCommand {

	@Override
	boolean exec(final String[] args) {
		sendToGM("当前在线用户数：\t"+ Onlines.getInstance().getOnlineUsers().size());
		return true;
	}

	@Override
	String usage() {

		return "//usernum 查看在线用户数";
	}

}

