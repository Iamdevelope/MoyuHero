package chuhan.gsp.gm;

import chuhan.gsp.attr.PropRole;

public class Cmd_setvip extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length < 1){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		final int viplv = Integer.valueOf(args[0]);
		if (viplv <= 0){
			sendToGM("参数格式错误:" + usage());
			return false;
		}
		long roleid = getGmroleid();
		if (args.length > 1) {
			roleid = GMInterface.getTargetRoleId(args[1]);
			if (roleid <= 0)
				roleid = getGmroleid();
		}
		final long rid = roleid;
		final xdb.Procedure p = new xdb.Procedure()
		{
			@Override
			protected boolean process() throws Exception {
				PropRole prole = PropRole.getPropRole(rid, false);
				prole.setVipLevel(viplv);
				return true;
			}
		};
		p.submit();
		return true;
	}

	@Override
	String usage() {
		return "//setvip [viplv] [roleid不填是自己]";
	}

}