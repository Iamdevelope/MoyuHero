package chuhan.gsp.gm;

import chuhan.gsp.item.ItemColumn;
import chuhan.gsp.item.Module;

public class Cmd_additem extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length < 1){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		
		final int itemid = args[0].matches("\\d+")? Integer.valueOf(args[0]) : Module.getItemIdByName(args[0]);
		if (itemid == 0){
			sendToGM("参数格式错误:" + usage());
			return false;
		}
		final int num = (args.length > 1)? Integer.valueOf(args[1]) : 1;
//		if(num <= 0 || num >500)
//		{
//			sendToGM("数量不能超过50："+num);
//			return false;
//		}
		long roleid = getGmroleid();
		if(args.length > 2)
		{
			roleid =GMInterface.getTargetRoleId(args[2]);
			if(roleid <= 0)
				roleid = getGmroleid();
			//roleid = Long.valueOf(args[1]);
		}
		final long rid = roleid;
		//final long roleid = (args.length > 2) ? Long.valueOf(args[2]) : getGmroleid();
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
				ItemColumn itemcol = Module.getItemColumnByItemId(rid, itemid, false);
				itemcol.addItem(itemid, num, "gm_add", 1);
				return true;
			};
		}.submit();
		return true;
	}

	@Override
	String usage() {
		return "//additem [itemid] [addnumber] [account不填是自己]";
	}
	
}