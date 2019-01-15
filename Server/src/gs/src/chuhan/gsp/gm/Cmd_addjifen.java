package chuhan.gsp.gm;

import chuhan.gsp.attr.PlayPropRole;

public class Cmd_addjifen extends GMCommand {

	@Override
	boolean exec(String[] args) {
		if(args.length < 1){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		final int jifen = Integer.valueOf(args[0]);
		if (jifen == 0){
			sendToGM("参数格式错误:" + usage());
			return false;
		}
		long roleid = getGmroleid();
		if(args.length > 1) {
			roleid =GMInterface.getTargetRoleId(args[1]);
			if(roleid <= 0)
				roleid = getGmroleid();
		}
		final long rid = roleid;
		new xdb.Procedure() {
			protected boolean process() throws Exception {
				PlayPropRole playPropRole = PlayPropRole.getPlayPropRole(rid, false);
				playPropRole.addPlayJiFen(jifen);
				return true;
			}
		}.submit();
		
		return true;
	}

	@Override
	String usage() {
		return "//addjifen [addnumber] [account不填是自己]";
	}

}
