package chuhan.gsp.play.shop;

import chuhan.gsp.msg.Message;


public class PShopBuy extends xdb.Procedure{
	private final long roleid;
	public final int shopid; // 商店物品id
	public final int num; // 购买数量
	public final byte isdiscount;
	
	
	public PShopBuy(long roleid, int shopid, int num,byte isdiscount) {
		this.roleid = roleid;
		this.shopid = shopid;
		this.num = num;
		this.isdiscount = isdiscount;
	}
	
	@Override
	protected boolean process() throws Exception {
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		xbean.Properties xprop = xtable.Properties.get(roleid);
		if(xprop == null){
			throw new IllegalArgumentException("构造角色时，角色 "+roleid+" 不存在。");
//			return false;
		}
		
		ShopBuyColumn shopcol = ShopBuyColumn.getShopBuyColumn(roleid, false);
		boolean result = shopcol.shopBuyEntry(shopid, num,isdiscount);
		SShopBuy snd = new SShopBuy();
		snd.shopid = this.shopid;
		if(result){
			snd.result = SShopBuy.END_OK;
		}else{
			snd.result = SShopBuy.END_ERROR;
		}
		psend(roleid, snd);
		return result;
		
	}
	
}
