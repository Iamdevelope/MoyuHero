package chuhan.gsp.item;

import java.util.Map;

import chuhan.gsp.attr.GoldAddType;
import chuhan.gsp.attr.PropRole;
import chuhan.gsp.msg.Message;

public class PSellItem extends xdb.Procedure
{
	private final long roleId;
	private final int bagid;
	private final Map<Integer,Integer> items;
	public PSellItem(long roleId, int bagid, Map<Integer,Integer> items) {
		this.roleId = roleId;
		this.items = items;
		this.bagid = bagid;
	}
	
	
	@Override
	protected boolean process() throws Exception
	{
		if(bagid != BagTypes.BAG)
			return false;//暂时只能装备栏内的能卖
		ItemColumn itemcol = Module.getItemColumn(roleId, bagid, false);
		int summoney = 0;
		for(Map.Entry<Integer, Integer> entry : items.entrySet())
		{
			int worth = itemcol.sellItem(entry.getKey(), entry.getValue(), false);
			summoney += worth;
		}
		itemcol.sendRemovedItems();
//		Message.psendMsgNotifyWhileCommit(roleId, 3, summoney);
		return true;
	}
	
	
}
