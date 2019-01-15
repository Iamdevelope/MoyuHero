package chuhan.gsp.gm;

import chuhan.gsp.item.BagTypes;
import chuhan.gsp.item.BasicItem;
import chuhan.gsp.item.ItemColumn;
import chuhan.gsp.item.Module;
import chuhan.gsp.item.types.EquipItem;


public class Cmd_examequip extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length < 1){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		final long roleid = GMInterface.getTargetRoleId(args[0]);
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
				
				ItemColumn itemcol = Module.getItemColumn(roleid, BagTypes.EQUIP, false);
				int i = 0;
				for(BasicItem item : itemcol)
				{
					if(!(item instanceof EquipItem))
						continue;
					if(((EquipItem)item).getLevel() > Byte.MAX_VALUE)
					{
						((EquipItem)item).setLevel(Byte.MAX_VALUE);
						i++;
					}
				}
				sendToGM("该角色一共有"+i+"件等级大于127的装备。");
				return true;
			};
		}.submit();
		
		return true;
	}

	@Override
	String usage() {
		return "//exam [roleid]";
	}

}