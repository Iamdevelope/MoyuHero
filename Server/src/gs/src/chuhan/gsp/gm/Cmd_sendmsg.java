package chuhan.gsp.gm;

import gnet.link.Role;
import chuhan.gsp.msg.PAddSysMsg;

public class Cmd_sendmsg extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length == 0)
		{
			sendToGM(usage());
			return false;
		}
		final String msg = args[0];
		final long rid = (args.length <= 1)? 0 : Long.valueOf(args[1]);
		int msgid = 0;
		String text = "";
		if(msg.matches("\\d+"))
			msgid = Integer.valueOf(msg);
		else
			text = msg;
		if(rid > 0)
			new PAddSysMsg(rid, msgid, null, text).submit();
		else
		{
			for(Role lrole : gnet.link.Onlines.getInstance().getRoles())
				new PAddSysMsg(lrole.getRoleid(),msgid,null,text).submit();
		}
		return true;
	}

	@Override
	String usage() {
		return "//addsysmsg [msgId] [account不填是在线广播]";
	}

}