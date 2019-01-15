package chuhan.gsp.exchange;

import gnet.QueryOrderRequest;
import gnet.link.Onlines;

import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Properties;
import java.util.concurrent.atomic.AtomicBoolean;

import xdb.Transaction;
import xdb.util.AutoKey;
import chuhan.gsp.PlatformTypeStr;
import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.SRefreshChargeSum;
import chuhan.gsp.attr.YuanBaoAddType;
import chuhan.gsp.attr.config10;
import chuhan.gsp.game.SAddCashConfig;
import chuhan.gsp.game.svipconfig;
import chuhan.gsp.log.LogManager;
import chuhan.gsp.log.Logger;
import chuhan.gsp.log.OpLogManager;
import chuhan.gsp.log.RemoteLogID;
import chuhan.gsp.log.RemoteLogParam;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.main.XdbModule;
import chuhan.gsp.msg.Message;
import chuhan.gsp.msg.MsgRole;
import chuhan.gsp.play.activity.ActivityGameManager;
import chuhan.gsp.play.activity.ActivityManager;
import chuhan.gsp.util.Conv;
import chuhan.gsp.util.DateUtil;
import chuhan.gsp.util.GameProp;
import chuhan.gsp.util.LogUtil;

import com.pwrd.op.LogOpChannel;

public class ChargeRole {
	
	//private static String YUEKA_NAME = "com.wanmei.mini.chuhan_yueka";
	//private static String ZHOUKA_NAME = "com.wanmei.mini.chuhan_zhouka";
	
	private static Properties prop = chuhan.gsp.main.ConfigManager.getInstance().getPropConf("sys");
	private static AtomicBoolean isChargeOn = new AtomicBoolean(GameProp.getIntValue(prop, "sys.charge.initalOn") == 1);
	public final static Logger logger = Logger.getLogger("充值");
	
	public static ChargeRole getChargeRole(long roleId, boolean readonly )
	{
		
		if(readonly)
		{
			if(xtable.Properties.select(roleId) == null)
				return null;
		}
		else
		{
			if(xtable.Properties.get(roleId) == null)
				return null;
		}
		xbean.BillRole xrole = null;
		
		if(readonly)
			xrole = xtable.Billroles.select(roleId);
		else
			xrole = xtable.Billroles.get(roleId);
		
		if(xrole == null)
		{
			if(readonly)
				xrole = xbean.Pod.newBillRoleData();
			else
			{
				xrole = xbean.Pod.newBillRole();
				xtable.Billroles.insert(roleId, xrole);
			}
		}
		
		return new ChargeRole(roleId, xrole, readonly);
	}
	
	
	public final long roleId;
	private final xbean.BillRole xrole;
	public final boolean readonly;
	public final String plattypestr;
	
	public ChargeRole(long roleId, xbean.BillRole xrole, boolean readonly) {
		plattypestr = xtable.Properties.selectPlattypestr(roleId);
		if(plattypestr == null)
			throw new IllegalArgumentException("roleid="+roleId);
		this.roleId = roleId;
		this.xrole = xrole;
		this.readonly = readonly;
	}
	
	public xbean.BillRole getData()
	{
		return xrole;
	}
	
	
	public void sendGoods()
	{
		if(ConfigManager.link_zoneid < 0)
		{
			if(Transaction.current() != null)
				xdb.Procedure.psendWhileCommit(roleId, new SReplyGoods());
			else
				gnet.link.Onlines.getInstance().send(roleId, new SReplyGoods());
			return;
		}
		/*if(ConfigManager.PLATFORM_TYPE < 0)
		{
			if(Transaction.current() != null)
				xdb.Procedure.psendWhileCommit(roleId, new SReplyGoods());
			else
				gnet.link.Onlines.getInstance().send(roleId, new SReplyGoods());
			return;
		}*/
		boolean first = isFirstCharge();
		
		Map<Integer,SAddCashConfig> goodcfgs = ConfigManager.getInstance().getConf(SAddCashConfig.class);
		SReplyGoods sndgoods = new SReplyGoods();
		if(first)
			sndgoods.goodtype = Conv.toByte(GoodType.FIRST);
		for(SAddCashConfig cfg : goodcfgs.values())
		{
			if(!isServerCharge(cfg))
				continue;
			chuhan.gsp.exchange.GoodInfo goodinfo = new chuhan.gsp.exchange.GoodInfo();
			goodinfo.goodid = Conv.toShort(cfg.id);
			goodinfo.price = (float)cfg.price;
			goodinfo.yuanbao = Conv.toShort(cfg.yuanbao);
			if(first) {
				goodinfo.present = Conv.toShort(cfg.yuanbao);
			} else {
				goodinfo.present = 0;
			}
			
			/*PropRole prole = PropRole.getPropRole(roleId, true);
			
			int leftTime;
			//月卡判断
			if(cfg.name.equals(YUEKA_NAME)) {
				if(prole.getProperties().getCardname() == YUEKA_NAME) {
					leftTime = (int) (DateUtil.getCurrentDay(prole.getProperties().getCardbuytime()) + 30 - DateUtil.getCurrentDay(GameTime.currentTimeMillis()));
					goodinfo.present = Short.parseShort("-111" + (leftTime < 10 ? "0" + leftTime : leftTime));
				} else if(prole.getProperties().getCardname() == ZHOUKA_NAME) {//已经购买周卡
					goodinfo.present = Short.parseShort("-10100");
				} else {
					goodinfo.present = Short.parseShort("-10000");
				}
			} else if(cfg.name.equals(ZHOUKA_NAME)) {//周卡判断
				if(prole.getProperties().getCardname() == ZHOUKA_NAME) {
					leftTime = (int) (DateUtil.getCurrentDay(prole.getProperties().getCardbuytime()) + 7 - DateUtil.getCurrentDay(GameTime.currentTimeMillis()));
					goodinfo.present = Short.parseShort("-211" + (leftTime < 10 ? "0" + leftTime : leftTime));
				} else if(prole.getProperties().getCardname() == YUEKA_NAME) {//已经购买月卡
					goodinfo.present = Short.parseShort("-20100");
				} else {
					goodinfo.present = Short.parseShort("-20000");
				}
			}*/
			sndgoods.goods.add(goodinfo);
		}
		if(Transaction.current() != null)
			xdb.Procedure.psendWhileCommit(roleId, sndgoods);
		else
			gnet.link.Onlines.getInstance().send(roleId, sndgoods);
		
		if(PlatformTypeStr.isQihoo360(plattypestr))
		{
//			DataBetweenAuAnyAndGS ausnd = new DataBetweenAuAnyAndGS();
//			ausnd.flag = DataBetweenAuAnyAndGS.GS_TO_AuAny;
//			ausnd.qtype = DataBetweenAuAnyAndGS.REFRESH_QIHOO_ACCESS_TOKEN;
//			xbean.Properties xprop = xtable.Properties.select(roleId);
//			ausnd.userid = xprop.getUserid();
//			ausnd.info = com.goldhuman.Common.Octets.wrap(xprop.getUsername(), "UTF-8");
//			DeliveryManager.getInstance().send(ausnd);
		}
		
	}
	
	public boolean isServerCharge(SAddCashConfig cfg)
	{
		if(cfg == null)
			return false;
		if(!cfg.platform.equals(plattypestr))
			return false;
//		if(cfg.serverids != null && !cfg.serverids.equals("")
//				&& !cfg.serverids.contains(String.valueOf(ConfigManager.LINK_ZONEID)))
//			return false;
		return true;
	}
	
	public boolean requestCharge(int goodid, int goodnum)
	{
		if(!isChargeOn.get()) {
			Message.psendMsgNotify(roleId, 126, Message.getMessage(1));
			return false;
		}
		if(readonly)
			return false;
		SAddCashConfig cfg = ConfigManager.getInstance().getConf(SAddCashConfig.class).get(goodid);
		if(!isServerCharge(cfg))
			return false;
		//if(ConfigManager.PLATFORM_TYPE == ConfigManager.PLATFORM_TYPE_APPSTORE)
		//PropRole prole = PropRole.getPropRole(roleId, false);
		
		/*if(prole.getProperties().getCardname().equals(cfg.name)) {
			return false;
		}*/
		
		if(plattypestr.equals(PlatformTypeStr.AppStore) 
				|| plattypestr.equals(PlatformTypeStr.LaHu)
				|| plattypestr.equals(PlatformTypeStr.AppT))
		{
			SReplyExchangeBill snd = new SReplyExchangeBill();
			snd.billid =cfg.getName();
			snd.goodid = goodid;
			snd.goodname = cfg.getName();
			snd.goodnum = 1;
			snd.price = "0.0";
			snd.serverid = ConfigManager.gs_zoneid;
			xdb.Procedure.psendWhileCommit(roleId, snd);
			return true;
		}
		float price = (float)cfg.price * goodnum;
		long uid = makeBillId();
		xbean.AppReceiptData xreceipt = xbean.Pod.newAppReceiptData();
		xreceipt.setRoleid(roleId);
		xtable.Appreceiptes.insert(uid, xreceipt);
		xbean.BillData xbilldata = xbean.Pod.newBillData();
		xbilldata.setBillid(uid);
		xbilldata.setConfirmtimes(0);
		xbilldata.setCreatetime(GameTime.currentTimeMillis());
		xbilldata.setGoodid(goodid);
		xbilldata.setGoodnum(goodnum);
		xbilldata.setPresent(0);
		xbilldata.setPrice(price);
		xbilldata.setState(xbean.BillData.STATE_SENDED);
		xrole.getBills().put(uid, xbilldata);
		SReplyExchangeBill snd = new SReplyExchangeBill();
		snd.billid = String.valueOf(uid);
		snd.goodid = goodid;
		snd.goodname = cfg.getName();
		snd.goodnum = goodnum;
		if(PlatformTypeStr.isQihoo360(this.plattypestr))
			price = price * 100;
		if(PlatformTypeStr.isOppo(plattypestr)) {
			price = price * 100;
		}
		if(PlatformTypeStr.isWanmei173(this.plattypestr))
			price *= 100;//老虎以分为单位
		if(PlatformTypeStr.isTongbutui(this.plattypestr))
			snd.goodname = cfg.getYuanbao()+"yuanbao";//同步推对名称要求不能有中文
		snd.price = String.valueOf(price);
		snd.serverid = ConfigManager.gs_zoneid;
		xdb.Procedure.psendWhileCommit(roleId, snd);
		return true;
	}
	
	public long makeBillId()
	{
		AutoKey<Long> autokey = xdb.Xdb.getInstance().getTables().getTableSys().getAutoKeys().getAutoKeyLong(XdbModule.EXCHANGE_BILL_UID);
		Long uid = autokey.next();
		if(uid == null)
			throw new IllegalArgumentException("Get bill uid equal null");
		
		return uid * 1000000 + ConfigManager.link_zoneid * 1000 + ConfigManager.gs_zoneid ;
	}
	
	public boolean responseCharge(long billid, int status)
	{
		/*xbean.BillData xbill = xrole.getBills().get(billid);
		if(xbill == null)
			return false;
		if(xbill.getState() == xbean.BillData.STATE_CONFIRMED)
			return true;//如果已经成功了，不要置状态
		if(status != 0)
			xbill.setState(xbean.BillData.STATE_FAILED);*/
		//TODO 成功
		return true;
	}
	/**
	 * 为gm指令所用，直接创造一个订单并确认
	 * @param billid
	 * @param goodid
	 * @return
	 */
	public boolean createAndConfirmCharge(int goodid)
	{
		if(readonly)
			return false;
		SAddCashConfig cfg = ConfigManager.getInstance().getConf(SAddCashConfig.class).get(goodid);
		if(!isServerCharge(cfg))
			return false;
		long uid = makeBillId();
		xbean.BillData xbilldata = xbean.Pod.newBillData();
		xbilldata.setBillid(uid);
		xbilldata.setConfirmtimes(0);
		xbilldata.setCreatetime(GameTime.currentTimeMillis());
		xbilldata.setGoodid(goodid);
		xbilldata.setGoodnum(1);
		xbilldata.setPresent(0);
		xbilldata.setPrice((float)cfg.price);
		xbilldata.setState(xbean.BillData.STATE_SENDED);
		xrole.getBills().put(uid, xbilldata);
		return confirmCharge(uid, cfg.price, "-1");
	}
	
	public boolean confirmChargeByAppStore(long billid, String goodbid, double price, String platbillid)
	{
		xbean.BillData xbill = xrole.getBills().get(billid);
		if(xbill == null)
		{
			//TODO log
			return false;
		}
		if(xbill.getState() == xbean.BillData.STATE_CONFIRMED)
		{//不能重复发放，失败和未确认的状态都可以发放
			//TODO log
			return false;
		}
		SAddCashConfig cfg = null;
		for(SAddCashConfig c : ConfigManager.getInstance().getConf(SAddCashConfig.class).values())
		{
			if(c.getName().equals(goodbid))
			{
				cfg = c;
				break;
			}
		}
		if(cfg == null)
			return false;
		if(!isServerCharge(cfg))
			return false;
		//一直到APPstore返回后，才知道买了啥
		xbill.setGoodid(cfg.id);
		xbill.setPrice((float)cfg.getPrice());
		xbill.setGoodnum(1);
		xbill.setPresent(0);
		return confirmCharge(billid, cfg.getPrice(), platbillid);
	}
	
	public boolean confirmCharge(long billid, double price, String platbillid)
	{
		xbean.BillData xbill = xrole.getBills().get(billid);
		if(xbill == null)
		{
			//TODO log
			return false;
		}
		if(xbill.getState() == xbean.BillData.STATE_CONFIRMED)
		{//不能重复发放，失败和未确认的状态都可以发放
			//TODO log
			return false;
		}
		
		if(Math.abs(xbill.getPrice() - price) > 0.01)//误差在1分之内才合法
		{
			//TODO log			
			return false;
		}
		
		SAddCashConfig cfg = ConfigManager.getInstance().getConf(SAddCashConfig.class).get(xbill.getGoodid());
		if(!isServerCharge(cfg))
			return false;
		
		xbill.setPlatbillid(platbillid);
		xbill.setPrice((float)price);
		
		boolean firstcharge = isFirstCharge();
		if(firstcharge)
		{//首充改送的元宝
			//xbill.setGoodnum(1);
			//xbill.setPresent(cfg.yuanbao);
		}
		
		if(cfg.monthcardID > 0)//月卡
		{
			ActivityManager.getInstance().addMonthCard(roleId, cfg.monthcardID);
		}
		//TODO 将来可能还会买别的，不止元宝 
		int addyuanbao = cfg.yuanbao * xbill.getGoodnum() + xbill.getPresent() * xbill.getGoodnum();
		
		if(addyuanbao < 0)
			throw new IllegalArgumentException("addyuanbao < 0");
		
		PropRole prole = PropRole.getPropRole(roleId, false);
		
		/*if(cfg.name.equals(YUEKA_NAME)) {
			if(!prole.getProperties().equals(YUEKA_NAME)) {
				prole.getProperties().setCardname(YUEKA_NAME);
				prole.getProperties().setCardbuytime(GameTime.currentTimeMillis());
				addyuanbao = cfg.yuanbao * xbill.getGoodnum();
			} else {
				return false;
			}
		}
		
		if(cfg.name.equals(ZHOUKA_NAME)) {
			if(!prole.getProperties().equals(ZHOUKA_NAME)) {
				prole.getProperties().setCardname(ZHOUKA_NAME);
				prole.getProperties().setCardbuytime(GameTime.currentTimeMillis());
				addyuanbao = cfg.yuanbao * xbill.getGoodnum();
			} else {
				return false;
			}
		}*/
		
		if(prole.addYuanBao(addyuanbao, YuanBaoAddType.ADD_CASH) != addyuanbao)
			return false;
		xbill.setState(xbean.BillData.STATE_CONFIRMED);
		float chargesum = getChargedSum();
		xdb.Procedure.psendWhileCommit(roleId, new SRefreshChargeSum((int)addyuanbao));
		
		//VIP经验增加
		prole.addVipExp(price, 1);
		//充值活动统计数据
		ActivityGameManager.getInstance().addCZActivity(roleId, (int)price, addyuanbao);
		
		/*int curmaxlv = getChargeMaxViplv(chargesum); 
		if(curmaxlv > prole.getVipLevel())
			prole.setVipLevel(curmaxlv);*/
		
		/*if(xrole.getFirstcharge() == 0 && !cfg.name.equals(YUEKA_NAME) && !cfg.name.equals(ZHOUKA_NAME)) {
			xrole.setFirstcharge(1);
		}*/
		
		if(xrole.getFirstcharge() == 0) {
			xrole.setFirstcharge(1);
		}
		
		MsgRole msgRole = MsgRole.getMsgRole(roleId, false);
		List<String> strs = new LinkedList<String>();
		strs.add(DateUtil.getStringFormat2Second(GameTime.currentTimeMillis()));
		strs.add(String.valueOf(addyuanbao));
		//msgRole.addSysMsgWithSP(189, strs, null, 0, MsgRole.MST_TYPE_SYS);
		
		//充值活动部分，记录当前充值活动RMB
//		ChargeActivity.charge(roleId, (int)xbill.getPrice());
		
		//充值返利活动，符合条件直接返利
		//if(!cfg.name.equals(YUEKA_NAME) && !cfg.name.equals(ZHOUKA_NAME)) {
//			RebateChargeActivity.rebateCharge(roleId, (int)xbill.getPrice());
		//}
		
		//首充活动，符合首充条件后可每日领取（期限X天）
		//if(!cfg.name.equals(YUEKA_NAME) && !cfg.name.equals(ZHOUKA_NAME)) {
//			FirstFeedActivity.sendFirstFeed(roleId, (int)xbill.getPrice());
		//}
		
		try{//do log
			PropRole propRole = PropRole.getPropRole(roleId, true);
			if(null != propRole) {
				OpLogManager.getInstance().doLogWhileCommit(LogOpChannel.RECHARGE, roleId, GameTime.currentTimeMillis(),
						DateUtil.getCurrentStringFormatEn(GameTime.currentTimeMillis()), (int)xbill.getPrice(), addyuanbao,
						propRole.getProperties().getUsername(), propRole.getProperties().getRolename(), propRole.getProperties().getLevel());
			}
			
			java.util.Map<String, Object> params = LogUtil.putRoleBasicParams(roleId, false, new HashMap<String, Object>());
			params.put(RemoteLogParam.CASH, xbill.getPrice());
			params.put(RemoteLogParam.YUANBAO, addyuanbao);
			params.put(RemoteLogParam.ID1, billid);
			params.put(RemoteLogParam.ID2, platbillid);
			params.put(RemoteLogParam.ROLENAME, propRole.getProperties().getRolename());
			LogManager.getInstance().doLogWhileCommit(RemoteLogID.ADDCASH, params);
		}catch(Exception e)
		{
			e.printStackTrace();
		} 
		
		logger.infoWhileCommit("Charge confirmed, roleId="+roleId+", billid="+billid+", price="+price+", yuanbao="+addyuanbao);
		//if(firstcharge)
			//sendGoods();
		return true;
	}
	
	public int getChargeMaxViplv(float chargesum)
	{
		if(chargesum <= 0)
			return 0;
		int max = 0;
		for(svipconfig cfg : ConfigManager.getInstance().getConf(svipconfig.class).values())
		{
			if(chargesum >= cfg.getRmb() && cfg.id > max)
				max = cfg.id;
		}
		return max;
	}
	
	public boolean isFirstCharge()
	{
		return xrole.getFirstcharge() == 0;
		//return getBillsByState(xbean.BillData.STATE_CONFIRMED).isEmpty();
	}
	
	public Map<Long,xbean.BillData> getBillsByState(int state)
	{
		Map<Long,xbean.BillData> datas = new HashMap<Long, xbean.BillData>();
		for(Map.Entry<Long, xbean.BillData> billdata : xrole.getBills().entrySet())
		{
			if(state == 0 || (billdata.getValue().getState() & state )!= 0)
				datas.put(billdata.getKey(), billdata.getValue());
		}
		return datas;
	}
	
	public float getChargedSum()
	{
		float sum = 0;
		for(xbean.BillData xbill : getBillsByState(xbean.BillData.STATE_CONFIRMED).values())
		{
			sum += xbill.getPrice();
		}
		return sum;
	}
	
	public void processWhileOnline()
	{
		Map<Long, xbean.BillData> unconfirms = getBillsByState(xbean.BillData.STATE_SENDED);
		if(PlatformTypeStr.isAppStore(plattypestr) 
				|| PlatformTypeStr.isLaHu(plattypestr)
				|| PlatformTypeStr.isAppT(plattypestr))
		{
			Map<Long, xbean.BillData> failed = getBillsByState(xbean.BillData.STATE_FAILED);
			for(Map.Entry<Long, xbean.BillData> entry : failed.entrySet())
			{
				if(entry.getValue().getConfirmtimes() < 3)
				{
					entry.getValue().setState(xbean.BillData.STATE_SENDED);
					unconfirms.put(entry.getKey(), entry.getValue());
				}
			}
		}
		long now = GameTime.currentTimeMillis();
		for (xbean.BillData xbill : unconfirms.values()) 
		{
			long period = getConfirmPeriod(xbill.getConfirmtimes());
			if (period <= 0) {
				xbill.setState(xbean.BillData.STATE_FAILED);
				continue;
			}
			if ((xbill.getCreatetime() + period) > now)
				continue;
			QueryOrderRequest query = new QueryOrderRequest();
			query.orderserialgame = String.valueOf(xbill.getBillid());
			if(PlatformTypeStr.isAppStore(plattypestr) 
					|| PlatformTypeStr.isLaHu(plattypestr)
					|| PlatformTypeStr.isAppT(plattypestr))
			{
				xbean.AppReceiptData xreceipt  = xtable.Appreceiptes.get(xbill.getBillid());
				if(null == xreceipt) {
					continue;
				}
				query.orderserialplat = xreceipt.getReceipt();
			}
			query.platid = plattypestr;
			//query.send(query);
		}
		
		/*PropRole prole = PropRole.getPropRole(roleId, false);
		if(prole.getProperties().getCardname() != null && !prole.getProperties().getCardname().equals("") && !DateUtil.inTheSameDay(prole.getProperties().getCardbuytime(), GameTime.currentTimeMillis())){
			if(!DateUtil.inTheSameDay(prole.getProperties().getCardrebatetime(), GameTime.currentTimeMillis())) { 
				int addyuanbao; 
				int leftTime;
				if(prole.getProperties().getCardname().equals(YUEKA_NAME)) {
					leftTime = (int) (DateUtil.getCurrentDay(prole.getProperties().getCardbuytime()) + 30 - DateUtil.getCurrentDay(GameTime.currentTimeMillis()));
					if(leftTime >= 0) {
						addyuanbao = 100;
						if(prole.addYuanBao(addyuanbao, YuanBaoAddType.ADD_CASH) == addyuanbao){
							MsgRole msgRole = MsgRole.getMsgRole(roleId, false);
							List<String> strs = new LinkedList<String>();
							strs.add(String.valueOf(addyuanbao));
							strs.add(String.valueOf(leftTime));
							msgRole.addSysMsgWithSP(427, strs, null, 0, MsgRole.MST_TYPE_SYS);
							prole.getProperties().setCardrebatetime(GameTime.currentTimeMillis());
						}
					} else {
						prole.getProperties().setCardname("");
						prole.getProperties().setCardbuytime(0);
					}
				} else if (prole.getProperties().getCardname().equals(ZHOUKA_NAME)) {
					leftTime = (int) (DateUtil.getCurrentDay(prole.getProperties().getCardbuytime()) + 7 - DateUtil.getCurrentDay(GameTime.currentTimeMillis()));
					if(leftTime >= 0) {
						addyuanbao = 50;
						if(prole.addYuanBao(addyuanbao, YuanBaoAddType.ADD_CASH) == addyuanbao){
							MsgRole msgRole = MsgRole.getMsgRole(roleId, false);
							List<String> strs = new LinkedList<String>();
							strs.add(String.valueOf(addyuanbao));
							strs.add(String.valueOf(leftTime));
							msgRole.addSysMsgWithSP(426, strs, null, 0, MsgRole.MST_TYPE_SYS);
							prole.getProperties().setCardrebatetime(GameTime.currentTimeMillis());
						}
					} else {
						prole.getProperties().setCardname("");
						prole.getProperties().setCardbuytime(0);
					}
				}
			}
		}*/
//		FirstFeedActivity.sendFeedPreDay(roleId);
	}
	
	private long getConfirmPeriod(int num)
	{
		/*//if(ConfigManager.PLATFORM_TYPE == ConfigManager.PLATFORM_TYPE_91)
		if(plattypestr.equalsIgnoreCase(PlatformTypeStr.Wanglong91))
		{//91重验，表示肯定丢单了，几率出现小，可以多验几次
			switch (num) {
			case 0:
				return 10 * 60 * 1000;// 10m
			case 1:
				return 30 * 60 * 1000;// 30m
			case 2:
				return 60 * 60 * 1000;// 1h
			case 3:
				return 4 * 60 * 60 * 1000;// 4h
			case 4:
				return 12 * 60 * 60 * 1000;// 12h
			case 5:
				return 24 * 60 * 60 * 1000;// 24h
			case 6:
				return 48 * 60 * 60 * 1000;// 48h
			}
		}
		else if(plattypestr.equalsIgnoreCase(PlatformTypeStr.Tieren25pp))
		{//PP只验两次，大部分是根本未充值的单子，要减少AU压力
			switch (num) {
			case 0:
				return 10 * 60 * 1000;// 10m
			case 1:
				return 60 * 60 * 1000;// 1h
			}
		}
		else if(PlatformTypeStr.isAppStore(plattypestr))
		{//appstore
			switch (num) {
			case 0:
				return 10 * 60 * 1000;// 10m
			case 1:
				return 30 * 60 * 1000;// 30m
			case 2:
				return 60 * 60 * 1000;// 1h
			case 3:
				return 4 * 60 * 60 * 1000;// 4h
			}
		}
		else*/
		{//所有平台验4次
			switch (num) {
			case 0:
				return 10 * 60 * 1000;// 10m
			case 1:
				return 30 * 60 * 1000;// 30m
			case 2:
				return 4 * 60 * 60 * 1000;// 4h
			case 3:
				return 24 * 60 * 60 * 1000;// 24h
			}
		}
		return -1;
	}
	
	public xbean.BillData getBill(long billid)
	{
		return xrole.getBills().get(billid);
	}
	
	public boolean verifyReceipt(long transid, String receipt)
	{
		xbean.BillData xbill = getBill(transid);
		if(xbill == null)
		{
			xbill = xbean.Pod.newBillData();
			xbill.setBillid(transid);
			xbill.setConfirmtimes(0);
			xbill.setCreatetime(GameTime.currentTimeMillis());
			xbill.setGoodid(0);
			xbill.setGoodnum(1);
			xbill.setPresent(0);
			xbill.setPrice(0);
			xbill.setState(xbean.BillData.STATE_SENDED);
			xrole.getBills().put(transid, xbill);
		}
		QueryOrderRequest query = new QueryOrderRequest();
		query.platid = plattypestr;
		query.orderserialgame = String.valueOf(transid);
		query.orderserialplat = receipt;
	
//		DeliveryManager.getInstance().send(query);
		return true;
	}
}
