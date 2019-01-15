package chuhan.gsp.item;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.YuanBaoConsumeType;
import chuhan.gsp.battle.LadderRole;
import chuhan.gsp.item.types.ChestItem;
import chuhan.gsp.item.types.KeyItem;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.Message;

public class PBuyItem extends xdb.Procedure
{
	
	protected final long roleId;
	protected final int shopkey;
	protected final int buynum;
	protected AddItemResult addresult;
	public PBuyItem(long roleId, int shopkey, int buynum) {
		this.roleId = roleId;
		this.shopkey = shopkey;
		this.buynum = buynum;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		if(!isValid())
			return false;
		
		if(!consumeMoney())
			return false;
		
		if(!giveItem())
			return false;
		
		sendNewAdd();
		
		return true;
	}
	
	public sshopconfig getShopConfig(int exchangekey)
	{
		return ConfigManager.getInstance().getConf(sshopconfig.class).get(exchangekey);
	}
	
	public boolean isValid()
	{
		sshopconfig cfg = getShopConfig(shopkey);
		if(cfg == null)
			return false;
	
		return true;
	}
	
	public int getPrice()
	{
		sshopconfig cfg = getShopConfig(shopkey);
		if(cfg == null)
			return 0;
		return cfg.getPrice();
	}
	
	public int getItemId()
	{
		sshopconfig cfg = getShopConfig(shopkey);
		if(cfg == null)
			return 0;
		return cfg.itemid;
	}
	
	public int getNum()
	{
		sshopconfig cfg = getShopConfig(shopkey);
		if(cfg == null)
			return 0;
		return cfg.num;
	}
	
	public int getShopType()
	{
		sshopconfig cfg = getShopConfig(shopkey);
		if(cfg == null)
			return 0;
		return cfg.getShoptype();
	}
	
	public boolean consumeMoney()
	{
		int price = getPrice();
		if(price <= 0)
			return false;
		int shoptype = getShopType();
		if(shoptype <= 0)
			return false;
		PropRole prole = PropRole.getPropRole(roleId, false);
		if(shoptype == ShopTypes.YUAN_BAO)
		{
			int consume = -(price * buynum);
			if(prole.delYuanBao(consume, YuanBaoConsumeType.BUY_ITEM) !=consume)
				return false;
			item26 itemcfg = ConfigManager.getInstance().getConf(item26.class).get(getItemId());
//			if(itemcfg.getClassname().equals(ChestItem.class.getName()) || 
	//				itemcfg.getClassname().equals(KeyItem.class.getName()))
//				prole.getProperties().setBuychestvalue(prole.getProperties().getBuychestvalue() - consume);
		}
		else if(shoptype == ShopTypes.SOUL_VALUE)
		{
			int consume = -(price * buynum);
//			if(prole.addSoul(consume) !=consume)
				return false;
		}
		else if(shoptype == ShopTypes.LADDER_SCORE)
		{
			int consume = -(price * buynum);
			LadderRole lrole = LadderRole.getLadderRole(roleId, false);
			if(lrole.addLadderSocre(consume) != consume)
			{
				Message.psendMsgNotify(roleId, 134);
				return false;
			}
		}
		return true;
	}
	
	public boolean giveItem()
	{
		int itemid = getItemId();
		if(itemid <= 0)
			return false;
		int num = getNum() * buynum;
		if(num <= 0)
			return false;
		ItemColumn itemcol = chuhan.gsp.item.Module.getItemColumnByItemId(roleId, itemid, false);
		addresult = itemcol.addItem(itemid, num, "buy_item", 1);
		if(!addresult.isSuccess())
			return false;
		if(addresult.getSumNum() != num)
			return false;
		return true;
	}
	
	public void sendNewAdd()
	{
		if(addresult == null || !addresult.isSuccess())
			return;
		xdb.Procedure.psendWhileCommit(roleId, addresult.getSShowAddItem());
	}
}
