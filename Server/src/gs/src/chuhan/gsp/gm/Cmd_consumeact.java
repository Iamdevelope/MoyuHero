package chuhan.gsp.gm;

import xbean.__.ConsumeActivity;
import chuhan.gsp.consume.PConsumeActivity;
import chuhan.gsp.task.ChargeActivity;

public class Cmd_consumeact extends GMCommand {

	@Override
	boolean exec(String[] args) {
		if(args.length < 1){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		final int rmb = Integer.valueOf(args[0]);
		if (rmb == 0){
			sendToGM("参数格式错误:" + usage());
			return false;
		}
		
		long roleid = getGmroleid();
		if(args.length > 1)
		{
			roleid =GMInterface.getTargetRoleId(args[1]);
			if(roleid <= 0)
				roleid = getGmroleid();
		}
		
		final long rid = roleid;
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
			PConsumeActivity.process(rid, rmb);
			sendToGM("consume rmb:" + rmb);
				return true;
			};
		}.submit();
		return true;
	}

	@Override
	String usage() {
		return "//consumeactivity [chargenumber] [account不填是自己]";
	}

}
