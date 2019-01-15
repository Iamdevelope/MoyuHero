
package chuhan.gsp.gm;

import gnet.link.Onlines;

public class Cmd_rolenum extends GMCommand {

	@Override
	boolean exec(final String[] args) {
		sendToGM("当前在线角色数：\t"+ Onlines.getInstance().getRoles().size());
		return true;
	}

	@Override
	String usage() {

		return "//rolenum 查看在线角色数";
	}

}

