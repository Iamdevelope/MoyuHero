package chuhan.gsp.gm;

import chuhan.gsp.msg.Message;

public class Cmd_broad extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length == 0)
		{
			sendToGM(usage());
			return false;
		}
		final String msg = args[0];
		int msgid = 0;
		String text = "";
		if(msg.matches("\\d+"))
			msgid = Integer.valueOf(msg);
		else
			text = msg;
		if(msgid > 0)
			Message.broadcastMsgNotify(msgid);
		else
			Message.broadcastMsgNotify(250,text);
		return true;
	}

	@Override
	String usage() {
		return "//broad [消息ID或者内容]";
	}

}