package chuhan.gsp.item;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.game.svipstore;
import chuhan.gsp.main.ConfigManager;

public class PBuyVipItem extends PBuyItem{

	public PBuyVipItem(long roleId, int shopkey, int buynum) {
		super(roleId, shopkey, buynum);
	}
	@Override
	public boolean isValid()
	{
		svipstore cfg = getVipShopConfig(shopkey);
		if(cfg == null)
			return false;
		PropRole prole = PropRole.getPropRole(roleId, false);
		if(prole.getVipLevel() < cfg.vip)
			return false;
		Integer boughtnum = null;
//		if(cfg.type == 1)
//			boughtnum = prole.getProperties().getVipitems().get(shopkey);
//		else if(cfg.type == 2)
//			boughtnum = prole.getProperties().getVipdailyitems().get(shopkey);
//		else
//			return false;
		if(boughtnum == null)
			boughtnum = 0;
		if(boughtnum+buynum >cfg.num)
			return false;
		
//		if(cfg.type == 1)
//			prole.getProperties().getVipitems().put(shopkey, boughtnum+buynum);
//		else if(cfg.type == 2)
//			prole.getProperties().getVipdailyitems().put(shopkey, boughtnum+buynum);
//		prole.sendSRefreshVipBuyInfo();
		
		return true;
	}
	@Override
	public int getPrice()
	{
		svipstore cfg = getVipShopConfig(shopkey);
		if(cfg == null)
			return 0;
		return cfg.getPrice();
	}
	@Override
	public int getItemId()
	{
		svipstore cfg = getVipShopConfig(shopkey);
		if(cfg == null)
			return 0;
		return cfg.item;
	}
	@Override
	public int getNum()
	{
		return 1;//一个一个卖
	}
	@Override
	public int getShopType()
	{
		return ShopTypes.YUAN_BAO;
	}
	
	public svipstore getVipShopConfig(int exchangekey)
	{
		return ConfigManager.getInstance().getConf(svipstore.class).get(exchangekey);
	}
}
