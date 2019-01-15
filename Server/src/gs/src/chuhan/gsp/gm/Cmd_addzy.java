package chuhan.gsp.gm;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.YuanBaoAddType;
import chuhan.gsp.attr.YuanBaoConsumeType;
import chuhan.gsp.award.DropManager;
import chuhan.gsp.award.IDManager;

public class Cmd_addzy extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length < 2){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		final int type = Integer.valueOf(args[0]);
		if (type == 0){
			sendToGM("参数格式错误:" + usage());
			return false;
		}
		long roleid = getGmroleid();

		final int num =Integer.valueOf(args[1]);

		final long rid = roleid;
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
				DropManager.getInstance().dropAddByOther(type, num, 0, 0, rid, "gm");
//				PropRole prole = PropRole.getPropRole(rid, false);
//				prole.addZiYuan(num, 0, type);
				return true;
			};
		}.submit();
		return true;
	}

	@Override
	String usage() {
		return "//addzy [资源ID] [数量]";
	}

}