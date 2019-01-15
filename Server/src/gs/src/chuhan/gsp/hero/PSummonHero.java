package chuhan.gsp.hero;

import chuhan.gsp.item.BagTypes;
import chuhan.gsp.item.BasicItem;
import chuhan.gsp.item.ItemColumn;
import chuhan.gsp.item.types.SoulItem;

public class PSummonHero extends xdb.Procedure
{
	
	private final long roleId;
	private final int itemkey;
	public PSummonHero(long roleId, int itemkey) {
		this.roleId = roleId;
		this.itemkey = itemkey;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		ItemColumn itemcol = chuhan.gsp.item.Module.getItemColumn(roleId, BagTypes.SOUL, false);
		
		BasicItem item = itemcol.getItem(itemkey);
		if(item == null || !(item instanceof SoulItem))
			return false;
		
	
		return true;
	}
		
}
