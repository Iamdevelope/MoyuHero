package chuhan.gsp.gm;

import chuhan.gsp.attr.PropRole;


public class Cmd_chestvalue extends GMCommand {

	@Override
	boolean exec(String[] args) {
		long roleid = getGmroleid();
		if (args.length > 0) {
			roleid = GMInterface.getTargetRoleId(args[0]);
			if (roleid <= 0)
				roleid = getGmroleid();
		}
		PropRole prole = PropRole.getPropRole(roleid, true);
		if(prole == null)
		{
			sendToGM("没有这个账号的角色");
			return false;
		}
//		sendToGM("Buy:"+prole.getProperties().getBuychestvalue()+",Get:"+prole.getProperties().getGetgoodvalue()+",Open:"+prole.getProperties().getOpenchestvalue());
		return true;
	}

	@Override
	String usage() {
		return "//chestvalue [账号不填是自己]";
	}

}
