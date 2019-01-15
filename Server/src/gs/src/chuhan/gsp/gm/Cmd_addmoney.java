package chuhan.gsp.gm;

import chuhan.gsp.attr.GoldAddType;
import chuhan.gsp.attr.PropRole;
import chuhan.gsp.item.Bag;

public class Cmd_addmoney extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length < 1){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		final int money = Integer.valueOf(args[0]);
		if (money == 0){
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
				if(prole == null)
					return false;
				if(money >0)
					prole.addGold(money,GoldAddType.GM_COMMAND);
				else
					prole.delGold(money,GoldAddType.GM_COMMAND);
//				Bag bag = new Bag(rid, false);
//				bag.addMoney(money, "gm_add");
				return true;
			};
		}.submit();
		return true;
	}

	@Override
	String usage() {
		return "//addmoney [addnumber] [account不填是自己]";
	}

}