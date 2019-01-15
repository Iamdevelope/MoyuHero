package chuhan.gsp.gm;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.YuanBaoAddType;
import chuhan.gsp.attr.YuanBaoConsumeType;

public class Cmd_addyb extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length < 1){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		final int yuanbao = Integer.valueOf(args[0]);
		if (yuanbao == 0){
			sendToGM("参数格式错误:" + usage());
			return false;
		}
		long roleid = getGmroleid();
		if(args.length > 1)
		{
			roleid =GMInterface.getTargetRoleId(args[1]);
			if(roleid <= 0)
				roleid = getGmroleid();
			//roleid = Long.valueOf(args[1]);
		}
		final long rid = roleid;
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
				PropRole prole = PropRole.getPropRole(rid, false);
				if(yuanbao > 0)
					prole.addYuanBao(yuanbao, YuanBaoAddType.GM_COMMAND);
				else
					prole.delYuanBao(yuanbao, YuanBaoConsumeType.OTHER);
				return true;
			};
		}.submit();
		return true;
	}

	@Override
	String usage() {
		return "//addyb [addnumber] [account不填是自己]";
	}

}