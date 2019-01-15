package chuhan.gsp.play.shop;

import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.config10;
import chuhan.gsp.award.DropManager;
import chuhan.gsp.award.IDManager;
import chuhan.gsp.game.shangdian76;
import chuhan.gsp.game.shangdiandiaoluo77;
import chuhan.gsp.game.shop35;
import chuhan.gsp.game.vip39;
import chuhan.gsp.hero.HeroArtifact;
import chuhan.gsp.hero.HeroSkinColumn;
import chuhan.gsp.item.artresource31;
import chuhan.gsp.item.hero01;
import chuhan.gsp.log.Logger;
import chuhan.gsp.log.LogBehavior;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.msg.Message;
import chuhan.gsp.play.activity.ActivityManager;
import chuhan.gsp.play.endlessbattle.SEndlessPass;
import chuhan.gsp.play.huoyuedu.HuoyueColumns;
import chuhan.gsp.util.DateUtil;
import chuhan.gsp.util.ParserString;



public class ShopBuyColumn {	
	public static Logger logger = Logger.getLogger(ShopBuyColumn.class);
	
	final public long roleId;
	final xbean.ShopbuyColumn xcolumn;
	final xbean.NewShopMap xnewshop;
	final boolean readonly;
	
	
	public static ShopBuyColumn getShopBuyColumn(long roleId, boolean readonly){
		if(xtable.Properties.select(roleId) == null)
			throw new IllegalArgumentException("构造TroopColumn时，角色 "+roleId+" 不存在。");
		
		xbean.ShopbuyColumn shopbuycol = null;
		xbean.NewShopMap newShopMap = null;
		if(readonly){
			shopbuycol = xtable.Shopbuycolumns.select(roleId);
			newShopMap = xtable.Newshopcolumns.select(roleId);
		}
		else{
			shopbuycol = xtable.Shopbuycolumns.get(roleId);
			newShopMap = xtable.Newshopcolumns.get(roleId);
		}
		if(shopbuycol == null){
			if(readonly)
				shopbuycol = xbean.Pod.newShopbuyColumnData();
			else{
				shopbuycol = xbean.Pod.newShopbuyColumn();
				xtable.Shopbuycolumns.insert(roleId, shopbuycol);
			}
		}
		if(newShopMap == null){
			if(readonly)
				newShopMap = xbean.Pod.newNewShopMapData();
			else{
				newShopMap = xbean.Pod.newNewShopMap();
				xtable.Newshopcolumns.insert(roleId, newShopMap);
			}
		}
		return new ShopBuyColumn(roleId, shopbuycol,newShopMap, readonly);
	}
	
	private ShopBuyColumn(long roleId, xbean.ShopbuyColumn xcolumn,xbean.NewShopMap xnewshop, boolean readonly) {
		this.roleId = roleId;
		this.xcolumn = xcolumn;
		this.readonly = readonly;
		this.xnewshop = xnewshop;
		init();
	}
	
	/**
	 * 初始化时判断当前时间并重置次数
	 */
	private void init(){
		long now = GameTime.currentTimeMillis();
		for(Map.Entry<Integer,xbean.Shopbuy> entry : xcolumn.getShopbuys().entrySet()){
			this.isSameDay(entry.getValue(), now);
		}
	}
	
	/**
	 * 判断是否为当天，否则重置
	 * @param shopbuy
	 * @param now
	 */
	private void isSameDay(xbean.Shopbuy shopbuy,long now){
		if( !DateUtil.inTheSameDay(shopbuy.getLasttime(), now) ){
			shopbuy.setTodaynum(0);
			shopbuy.setLasttime(now);
		}
	}
	
	public HashMap<Integer,chuhan.gsp.Shopbuy> getProtocolShopBuys(){	
		HashMap<Integer,chuhan.gsp.Shopbuy> datas = new HashMap<Integer,chuhan.gsp.Shopbuy>();
		for(Map.Entry<Integer,xbean.Shopbuy> entry : xcolumn.getShopbuys().entrySet()){
			chuhan.gsp.Shopbuy shopbuy = new chuhan.gsp.Shopbuy();
			shopbuy.shopid = entry.getValue().getShopid();
			shopbuy.todaynum = entry.getValue().getTodaynum();
			shopbuy.buyallnum = entry.getValue().getBuyallnum();
			datas.put(shopbuy.shopid, shopbuy);
		}
		return datas;
	}
	
	/**
	 * 商城购买入口
	 * @param shopid
	 * @param num
	 * @return
	 */
	public boolean shopBuyEntry(int shopid, int num,byte isdiscount){
		long now = GameTime.currentTimeMillis();
		shop35 shopinit = ConfigManager.getInstance().getConf(shop35.class).get(shopid);
		if(shopinit == null){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		chuhan.gsp.attr.PropRole prop = chuhan.gsp.attr.PropRole.getPropRole(roleId, readonly);
		//vip等级限制
		if(shopinit.getVipLimit() > prop.getVipLevel()){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		//判断是否在上架
		if( !DateUtil.isRunning(now,shopinit.getOnShelve(),shopinit.getOffShelve(),"yyyyMMddHHmmss") ){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		xbean.Shopbuy shopbuy = this.xcolumn.getShopbuys().get(shopid);
		if(shopbuy == null){
			shopbuy = xbean.Pod.newShopbuy();//xbean.Pod().newShopbuy();
			shopbuy.setShopid(shopid);
			xcolumn.getShopbuys().put(shopbuy.getShopid(), shopbuy);
		}
		//刷新数据，防止跨天
		this.isSameDay(shopbuy, now);
		//是否超出今日上限
		if(shopinit.getDailyMaxBuy() != -1 ){
			if( shopinit.getDailyMaxBuy() == 0 ){
				if( !this.specialShopId(shopinit, prop, shopbuy, num) ){
					return false;
				}
			}else if(shopbuy.getTodaynum() + num > shopinit.getDailyMaxBuy()){
				Message.psendMsgNotify(roleId, 135);
				return false;
			}
		}
		if(120002 == shopid){
			if(prop.getVipInit().getMaxBuyAp() <= shopbuy.getTodaynum()){
				return false;
			}
		}
		//是否超出最大上限
		if(shopinit.getShelveMaxBuy() != -1 && shopbuy.getBuyallnum() + num > shopinit.getShelveMaxBuy()){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		
		String coststr = shopinit.getCost();
		//是否是促销期
		if(DateUtil.isRunning(now,shopinit.getDiscountOn(),shopinit.getDiscountOff(),"yyyyMMddHHmmss")){
			coststr = shopinit.getDiscountCost();
		}else{
			if(isdiscount == 1){		//客户端为促销期
				Message.psendMsgNotify(roleId, 135);
				return false;
			}
		}
		
		List<Integer> costList = ParserString.parseString2Int(coststr);
		if(costList == null || costList.size() == 0){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		
		int costnum = 0;
		for(int i = 0;i< num;i++){
			costnum += getCost(costList,shopbuy.getTodaynum() + i);
		}
		
		if( !isCostOk(prop,shopinit.getCostType(),costnum) ){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		
		if( addShopItem(prop,shopinit,num) ){
			shopbuy.setTodaynum(shopbuy.getTodaynum() + num);
			shopbuy.setBuyallnum(shopbuy.getBuyallnum() + num);
			this.refreshShopBuy(shopbuy);
			return true;
		}
		
		return false;
	}
	
	/**
	 * 特殊商品购买次数在vip表里控制
	 * @param shopinit
	 * @param prop
	 * @param shopbuy
	 * @param num
	 * @return
	 */
	public boolean specialShopId(shop35 shopinit, PropRole prop, xbean.Shopbuy shopbuy, int num){
		try{
			vip39 vipinit = prop.getVipInit();
			int shopTi = Integer.parseInt(ConfigManager.getInstance().
					getConf(config10.class).get(1251).configvalue);
			
			if(shopinit.id == shopTi){
				if( shopbuy.getTodaynum() + num > vipinit.getMaxBuyAp() ){
					Message.psendMsgNotify(roleId, 135);
					return false;
				}
			}
			
			return true;
			
		}catch(Exception e){
			e.printStackTrace();
			return false;
		}
		
	}
	
	/**
	 * 根据次数计算消耗
	 * @param costList
	 * @param num
	 * @return
	 */
	public int getCost(List<Integer> costList, int num){
		if(costList.size() > num){
			return costList.get(num);
		}else{
			return costList.get(costList.size() - 1);
		}
	}
	
	
	/**
	 * 商城消耗金币或元宝
	 * @param prop
	 * @param type
	 * @param num
	 * @return
	 */
	public boolean isCostOk(chuhan.gsp.attr.PropRole prop, int type, int num){
		return DropManager.getInstance().useDel(type, num, prop.getRoleId(), LogBehavior.SHOPBUYCOST);
/*		if(type == IDManager.GOLD){
			return -num == prop.delGold(-num, 0);
		}else if(type == IDManager.YUANBAO){
			return -num == prop.delYuanBao(-num, 0);
		}
		return false;*/
	}
	
	/**
	 * 商城物品添加到人物身上
	 * @param prop
	 * @param shopinit
	 * @return
	 */
	public boolean addShopItem(chuhan.gsp.attr.PropRole prop, shop35 shopinit,int num){
		if(shopinit.getId() == 120002){
			prop.addTili(100);
			return true;
		}
		switch(shopinit.getType()){
		case 11:
			if(Integer.parseInt(shopinit.getPara()) == 1400000012){
				num = 20;
			}
			DropManager.getInstance().sendMailOrDropAdd(roleId, Integer.parseInt(shopinit.getPara()), num, 0, LogBehavior.SHOPBUY);
			return true;
		case 12:
			prop.fullTili();	
			ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.SHOP_BUY_TI, 1);
			return true;
		case 13:
			prop.fullTXTili();	
			return true;
		case 21:
			DropManager.getInstance().sendMailOrDropAdd(roleId, Integer.parseInt(shopinit.getPara()), num, 0, LogBehavior.SHOPBUY);
			return true;
		case 22:
			DropManager.getInstance().sendMailOrDropAdd(roleId, Integer.parseInt(shopinit.getPara()), num, 0, LogBehavior.SHOPBUY);
			return true;
		case 31:
			HeroSkinColumn skincol = HeroSkinColumn.getHeroSkinColumn(roleId, readonly);
			return skincol.addSkin(Integer.parseInt(shopinit.getPara()),true);
		case 51:
			ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.SHOP_BUY_GOLD, 1);
			DropManager.getInstance().sendMailOrDropAdd(roleId, IDManager.GOLD, 
					Integer.parseInt(shopinit.getPara())*num, 0, LogBehavior.SHOPBUY);
			return true;
//					Integer.parseInt(shopinit.getPara())*num == 	prop.addGold(
//					Integer.parseInt(shopinit.getPara())*num , 0);
			default:
				return false;
		}
	}
	
	/**
	 * 刷新购买信息记录
	 * @param shopbuy
	 */
	public void refreshShopBuy(xbean.Shopbuy shopbuy){
		SRefreshShopBuy snd = new SRefreshShopBuy();
		snd.shopbuy.shopid = shopbuy.getShopid();
		snd.shopbuy.todaynum = shopbuy.getTodaynum();
		snd.shopbuy.buyallnum = shopbuy.getBuyallnum();
		xdb.Procedure.psend(roleId, snd);
	}
	
	public void refreshNewShopBuy(long now){
		SRefreshNewShop snd = new SRefreshNewShop();
		for(Map.Entry<Integer, xbean.NewShopList> entry : xnewshop.getShopmap().entrySet()){
			shangdian76 init76 = ConfigManager.getInstance().getConf(shangdian76.class).get(entry.getKey());
			if(init76 == null){
				continue;
			}
			this.refreshNum(now, entry.getValue(), init76);
			NewShopList list = new NewShopList();
			list.refreshnum = entry.getValue().getRefreshnum();
			for (xbean.NewShop xnshop : entry.getValue().getShoplist()) {
				NewShop nshop = new NewShop();
				nshop.itemid = xnshop.getItemid();
				nshop.costtype = xnshop.getCosttype();
				nshop.price = xnshop.getPrice();
				nshop.num = xnshop.getNum();
				nshop.isbuy = xnshop.getIsbuy();
				list.shoplist.add(nshop);
			}
			if(list.shoplist.size() > 0){
				list.lasttime = DateUtil.getTimeFromHour(now, init76.getRefreshTime()) / 1000;
			}
			snd.shopmap.put(entry.getKey(), list);		
		}

		xdb.Procedure.psend(roleId, snd);
	}
	
	
	
	public void getNewShop(){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		chuhan.gsp.attr.PropRole prop = chuhan.gsp.attr.PropRole.getPropRole(roleId, readonly);
		TreeMap<Integer,shangdian76> map76 = ConfigManager.getInstance().getConf(shangdian76.class);
		for(Map.Entry<Integer,shangdian76> entry : map76.entrySet()){
			shangdian76 init76 = entry.getValue();
			if(init76 == null || !isOpenNewShop(prop,init76)){
				continue;
			}
			xbean.NewShopList shopList = xnewshop.getShopmap().get(entry.getKey());
			if(shopList == null){
				shopList = xbean.Pod.newNewShopList();
				xnewshop.getShopmap().put(entry.getKey(), shopList);
			}
			if(!DateUtil.isRunningOnSamedayAndTime(now, shopList.getLasttime(), init76.getRefreshTime())){
				shopList = this.getNewShopList(init76, prop, now, shopList);
//				xnewshop.getShopmap().put(entry.getKey(), shopList);
			}
		}

/*		for(Map.Entry<Integer, xbean.NewShopList> entry : xnewshop.getShopmap().entrySet()){
			shangdian76 init76 = ConfigManager.getInstance().getConf(shangdian76.class).get(entry.getKey());
			if(init76 == null || !isOpenNewShop(prop,init76)){
				continue;
			}
			if(!DateUtil.isRunningOnSamedayAndTime(now, entry.getValue().getLasttime(), init76.getRefreshTime())){
				entry.setValue(this.getNewShopList(init76, prop, now, entry.getValue()));
			}
		}*/
		this.refreshNewShopBuy(now);
	}
	public boolean isOpenNewShop(chuhan.gsp.attr.PropRole prop,shangdian76 init76){
		switch(init76.getStoreOpen()){
		case 1:
			return prop.getLevel() >= init76.getConditionalData();
		case 2:
			return prop.getVipLevel() >= init76.getConditionalData();
		}
		return false;
	}
	public xbean.NewShopList getNewShopList(shangdian76 init76,chuhan.gsp.attr.PropRole prop,long now,xbean.NewShopList result){
		result.getShoplist().clear();
		result.setLasttime(now);
		TreeMap<Integer,shangdiandiaoluo77> map77 = ConfigManager.getInstance().getConf(shangdiandiaoluo77.class);
		List<Integer> idList = ParserString.parseString2Int(init76.getCommodity());
		for(int i = 0;i<idList.size();i++){
			shangdiandiaoluo77 init77 = null;
			xbean.NewShop newshop = xbean.Pod.newNewShop();
			for(Map.Entry<Integer,shangdiandiaoluo77> entry : map77.entrySet()){
				if(entry.getValue().getDropId() == idList.get(i) && 
						entry.getValue().getSmallLeve() <= prop.getLevel() &&
						entry.getValue().getBigLeve() >= prop.getLevel()){
					init77 = entry.getValue();
					break;
				}
			}
			if(init77 != null){
				List<Integer> allDrop = ParserString.parseString2Int(init77.getItemId());
				List<Integer> allProb = ParserString.parseString2Int(init77.getWeight());
				List<Integer> dropidList = DropManager.getInstance().getDropIdList(
						DropManager.getInstance().getDropMap(allDrop,allProb,0),1);
				if(dropidList.size() >= 1){
					int dropItemId = dropidList.get(0);
					int num = -1;
					for(int j = 0;j < dropidList.size();j++){
						if(dropItemId == dropidList.get(j)){
							num = j;
							break;
						}
					}
					if(num != -1){
						List<Integer> costtype = ParserString.parseString2Int(init77.getConsumerzy());
						List<Integer> price = ParserString.parseString2Int(init77.getPrice());
						List<Integer> number = ParserString.parseString2Int(init77.getNumber());
						if(costtype.size() >= num && price.size() >= num && number.size() >= num){
							newshop.setItemid(dropItemId);
							newshop.setCosttype(costtype.get(num));
							newshop.setPrice(price.get(num));
							newshop.setNum(number.get(num));
							newshop.setIsbuy(0);
						}
					}
				}
			}
			result.getShoplist().add(newshop);
		}
		return result;
	}
	
	public boolean buyNewShop(int shopid,int itemid,int costtype,int price,int num){
		xbean.NewShopList shopList = xnewshop.getShopmap().get(shopid);
		if(shopList == null){
			return false;
		}
		for(xbean.NewShop shop : shopList.getShoplist()){
			if(shop.getItemid() == itemid &&
					shop.getCosttype() == costtype &&
					shop.getPrice() == price &&
					shop.getNum() == num &&
					shop.getIsbuy() == 0){
				if(DropManager.getInstance().useDel(costtype, price, roleId, "NEWSHOPBUY")){
					DropManager.getInstance().dropAddByOther(itemid, num, 0, 0, roleId, "NEWSHOPBUY");
					shangdian76 init76 = ConfigManager.getInstance().getConf(shangdian76.class).get(shopid);
					if(init76 == null || init76.getRepeatPurchase() == 0){
						shop.setIsbuy(1);
					}
					
					long now = chuhan.gsp.main.GameTime.currentTimeMillis();
					this.refreshNewShopBuy(now);
					return true;
				}
			}
		}
		return false;
	}
	
	public boolean getNewShopItem(int shopid){
		chuhan.gsp.attr.PropRole prop = chuhan.gsp.attr.PropRole.getPropRole(roleId, readonly);
		shangdian76 init76 = ConfigManager.getInstance().getConf(shangdian76.class).get(shopid);
		if(init76 == null || !isOpenNewShop(prop,init76)){
			return false;
		}
		xbean.NewShopList shopList = xnewshop.getShopmap().get(shopid);
		if(shopList == null){
			shopList = xbean.Pod.newNewShopList();
			xnewshop.getShopmap().put(shopid, shopList);
		}
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		this.refreshNum(now, shopList, init76);
		List<Integer> costList = ParserString.parseString2Int(init76.getConsumption());
		if(costList == null || costList.size() == 0){
			return false;
		}
		int cost = 0;
		if(costList.size() <= shopList.getRefreshnum()){
			cost = costList.get(costList.size() - 1);
		}else{
			cost = costList.get(shopList.getRefreshnum());
		}
		
		if(DropManager.getInstance().useDel(init76.getCurrencyType(), cost, roleId, "NEWSHOPREFRESH")){
			shopList = this.getNewShopList(init76, prop, now, shopList);
			shopList.setRefreshnum(shopList.getRefreshnum() + 1);
			shopList.setRefreshtime(now);
			this.refreshNewShopBuy(now);
			return true;
		}
		return false;
	}
	
	public void refreshNum(long now,xbean.NewShopList shopList,shangdian76 init76){
		if(shopList.getRefreshnum() == 0){
			return;
		}
		if(!DateUtil.isRunningOnSamedayAndTime(now, shopList.getRefreshtime(), init76.getRefreshTime()) || 
				!DateUtil.isRunningOnSamedayAndTime(shopList.getRefreshtime(), shopList.getLasttime(), init76.getRefreshTime())){
			shopList.setRefreshnum(0);
		}
	}
}
