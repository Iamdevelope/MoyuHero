package chuhan.gsp.play.activity;

import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.config10;
import chuhan.gsp.award.DropManager;
import chuhan.gsp.award.IDManager;
import chuhan.gsp.game.exchangecode38;
import chuhan.gsp.game.exchangecode381;
import chuhan.gsp.game.exchangecode382;
import chuhan.gsp.game.exchangecode383;
import chuhan.gsp.game.exchangecode384;
import chuhan.gsp.game.exchangecode385;
import chuhan.gsp.game.exchangecode386;
import chuhan.gsp.game.exchangecode387;
import chuhan.gsp.game.exchangecode388;
import chuhan.gsp.game.exchangecode389;
import chuhan.gsp.game.heroclone47;
import chuhan.gsp.game.loginbonus42;
import chuhan.gsp.game.monthcard45;
import chuhan.gsp.game.newbieguide60;
import chuhan.gsp.hero.HeroColumn;
import chuhan.gsp.item.BagTypes;
import chuhan.gsp.item.ItemColumn;
import chuhan.gsp.item.Module;
import chuhan.gsp.item.PUseItem;
import chuhan.gsp.item.hero01;
import chuhan.gsp.item.item26;
import chuhan.gsp.log.LogBehavior;
import chuhan.gsp.log.Logger;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.msg.Message;
import chuhan.gsp.play.huoyuedu.HuoyueColumns;
import chuhan.gsp.play.lotteryitem.LotteryItemColumns;
import chuhan.gsp.task.monster13;
import chuhan.gsp.util.DateUtil;
import chuhan.gsp.util.ParserString;



public class ActivityManager{

	public final static int QIYUAN_MAX = 5;
	
	public static Logger logger = Logger.getLogger(ActivityManager.class);
	static private ActivityManager instance = null;

	private static hero01 ihero = null;
	private static  item26 item  = null;
	
	public static final String ZHAOMU = "zhaomu";
	public static final String BAOZANG = "baozang";
	public static final String GUANKA = "guanka";
	public static final String TONGGUAN = "tongguan";
	public static final String JINJIE = "jinjie";
	public static final String DAOJU = "daoju";
	public static final String VIPLEVELUP = "viplevelup";
	public static final String SHENQIFULL = "shenqifull";
	public static final String BOSSOPEN = "bossopen";
	public static final String BOSSKILL = "bosskill";
	public static final String BOSSTAOZOU = "bosstaozou";
	public static final String GMSEND = "gmsend";

	
	private ActivityManager(){}
	public static ActivityManager getInstance() {
		if(instance == null)
		{
			instance = new ActivityManager();
		}
		return instance;
	}
	
	/**
	 * 缪斯奏曲
	 * @param roleId
	 * @return
	 */
	public boolean GetMSZQ(long roleId){
		List<String> timeListstr = ParserString.parseString(ConfigManager.getInstance().
				getConf(config10.class).get(1272).configvalue);
		int addNum = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1273).configvalue);
		if(timeListstr.size() != 2){
			return false;
		}
		long now = GameTime.currentTimeMillis();
		chuhan.gsp.attr.PropRole prop = chuhan.gsp.attr.PropRole.getPropRole(roleId, false);
		if( prop.getTili(now) >= prop.getMaxTili() ){
			return false;
		}
		if( DateUtil.isRunningOnday(now, timeListstr.get(0)) != -1 ){
			if( prop.getProperties().getMszqgetnum() % 10 == 0 ){
				prop.addTili(addNum);
				prop.getProperties().setMszqgetnum(prop.getProperties().getMszqgetnum() + 1);
				prop.refreshSweep(now);
				return true;
			}
		}
		if( DateUtil.isRunningOnday(now, timeListstr.get(1)) != -1 ){
			if( prop.getProperties().getMszqgetnum() / 10 == 0 ){
				prop.addTili(addNum);
				prop.getProperties().setMszqgetnum(prop.getProperties().getMszqgetnum() + 10);
				prop.refreshSweep(now);
				
				ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.MSZQ, 1);

				return true;
			}
		}
		return false;
	}
	
	/**
	 * 获取英雄兑换礼券数据库数据
	 * @param roleId
	 * @param readonly
	 * @return
	 */
	public static xbean.roledhmap getxRoledhMap(long roleId, boolean readonly){
		xbean.roledhmap xresult;
		if(readonly){
			if(xtable.Properties.select(roleId) == null)
				return null;
			xresult = xtable.Roledhmaps.select(roleId);
		}else{
			if(xtable.Properties.get(roleId) == null)
				return null;
			xresult = xtable.Roledhmaps.get(roleId);
		}
		if(xresult == null){
			if(readonly)
				xresult = xbean.Pod.newroledhmapData();
			else{
				xresult = xbean.Pod.newroledhmap();
				xtable.Roledhmaps.insert(roleId, xresult);
			}
		}
		return xresult;
	}
	/**
	 * 获取服务器兑换礼券数据库数据
	 * @param key
	 * @param readonly
	 * @return
	 */
	public static xbean.duihuanlq getxDuihuanlq(int key, boolean readonly){
		xbean.duihuanlq xresult;
		if(readonly){
			xresult = xtable.Duihuanlqs.select(key);
		}else{
			xresult = xtable.Duihuanlqs.get(key);
		}
		if(xresult == null){
			if(readonly)
				xresult = xbean.Pod.newduihuanlqData();
			else{
				xresult = xbean.Pod.newduihuanlq();
				xtable.Duihuanlqs.insert(key, xresult);
			}		}
		return xresult;
	}
	
	/**
	 * 兑换礼券入口
	 * @param roleId
	 * @param strlb
	 * @return
	 */
	public boolean duihuanLBEntry(long roleId,String strlb){
		try{
			long now = chuhan.gsp.main.GameTime.currentTimeMillis();
			String firststr = strlb.substring(0, 1);
			int key = Integer.parseInt(firststr);
			Map<Integer,Object> mapinit = getDuiHuanInit(key);
			if(mapinit == null){
				return false;
			}
			boolean isHave = false;
			for(Map.Entry<Integer, Object> entry : mapinit.entrySet()){
				if( this.isHave(key, entry.getValue(), strlb) ){
					isHave = true;
					break;
				}
			}
			if(isHave){
				exchangecode38 exchangeinit = ConfigManager.getInstance().getConf(exchangecode38.class).get(key);
				if(exchangeinit == null){
					return false;
				}
				//是否在开放时间
				if( !DateUtil.isRunning(now,exchangeinit.getStartdate()+"000000", 
						exchangeinit.getDeadline()+"235959","yyyyMMddHHmmss") ){
					return false;
				}
				//是否超出个人次数
				xbean.roledhmap roledhmap = ActivityManager.getxRoledhMap(roleId, false);
				xbean.roleduihuanlq roledh = roledhmap.getDhmap().get(key);
				if(roledh != null){
					if(roledh.getTypenum() == exchangeinit.getTypenum()){
						if(roledh.getNum() >= exchangeinit.getExchangetimes()){
							return false;
						}
						roledh.setNum(roledh.getNum() + 1);
					}else{
						roledh.setTypenum(exchangeinit.getTypenum());
						roledh.setNum(1);
					}
				}else{
					roledh = xbean.Pod.newroleduihuanlq();
					roledh.setLqkey(key);
					roledh.setTypenum(exchangeinit.getTypenum());
					roledh.setNum(1);
					roledhmap.getDhmap().put(key, roledh);
				}
				//判断服务器是否已经用过了
				xbean.duihuanlq xduihuan = ActivityManager.getxDuihuanlq(key, false);
				if(xduihuan.getTypenum() == exchangeinit.getTypenum()){
					if(xduihuan.getClonelist().contains(strlb)){
						return false;
					}
					xduihuan.getClonelist().add(strlb);
				}else{
					xduihuan.getClonelist().clear();
					xduihuan.getClonelist().add(strlb);
					xduihuan.setTypenum(exchangeinit.getTypenum());
				}
				List<Integer> drop = DropManager.getInstance().drop(roleId, 
						String.valueOf(exchangeinit.getNormaldropid()), LogBehavior.DUIHUANLP);
				SDuihuanlb snd = new SDuihuanlb();
				snd.result = SDuihuanlb.END_OK;
				if(drop != null)
					snd.innerdropidlist.addAll(drop);
				xdb.Procedure.psendWhileCommit(roleId, snd);
				return true;
			}
			return false;
		}catch(Exception e){
			e.printStackTrace();
			return false;
		}
	}
	/**
	 * 转换类型并判断是否想相同
	 * @param key
	 * @param tempin
	 * @param strin
	 * @return
	 */
	public boolean isHave(int key,Object tempin,String strin){
		try{
			String strtemp = "";
			switch (key) {
			case 1:
				exchangecode381 temp1 = (exchangecode381) tempin;
				strtemp = temp1.code;
				break;
			case 2:
				exchangecode382 temp2 = (exchangecode382) tempin;
				strtemp = temp2.code;
				break;
			case 3:
				exchangecode383 temp3 = (exchangecode383) tempin;
				strtemp = temp3.code;
				break;
			case 4:
				exchangecode384 temp4 = (exchangecode384) tempin;
				strtemp = temp4.code;
				break;
			case 5:
				exchangecode385 temp5 = (exchangecode385) tempin;
				strtemp = temp5.code;
				break;
			case 6:
				exchangecode386 temp6 = (exchangecode386) tempin;
				strtemp = temp6.code;
				break;
			case 7:
				exchangecode387 temp7 = (exchangecode387) tempin;
				strtemp = temp7.code;
				break;
			case 8:
				exchangecode388 temp8 = (exchangecode388) tempin;
				strtemp = temp8.code;
				break;
			case 9:
				exchangecode389 temp9 = (exchangecode389) tempin;
				strtemp = temp9.code;
				break;
			}
			return strtemp.equals(strin);
		}catch(Exception e){
			e.printStackTrace();
			return false;
		}
	}
	/**
	 * 返回兑换码基础列表
	 * @param key
	 * @return
	 */
	@SuppressWarnings("unchecked")
	public Map<Integer, Object> getDuiHuanInit(int key){
		try{
			return (Map<Integer, Object>) ConfigManager.getInstance()
					.getConf(Class.forName("chuhan.gsp.game.exchangecode38"+key));
		}catch(Exception e){
			e.printStackTrace();
			return null;
		}
	}
	
	/**
	 * 获取英雄克隆数据库数据
	 * @param roleId
	 * @param readonly
	 * @return
	 */
	public static xbean.heroclone getxHeroClone(long roleId, boolean readonly){
		xbean.heroclone xheroclone;
		if(readonly){
			if(xtable.Properties.select(roleId) == null)
				return null;
			xheroclone = xtable.Heroclones.select(roleId);
		}else{
			if(xtable.Properties.get(roleId) == null)
				return null;
			xheroclone = xtable.Heroclones.get(roleId);
		}
		if(xheroclone == null){
			if(readonly)
				xheroclone = xbean.Pod.newherocloneData();
			else{
				xheroclone = xbean.Pod.newheroclone();
				xtable.Heroclones.insert(roleId, xheroclone);
			}
		}
		return xheroclone;
	}
	
	/**
	 * 添加英雄克隆信息
	 * @param roleId
	 * @param openCondition
	 * @return
	 */
	public boolean addHeroClone(long roleId, int openCondition){
		TreeMap<Integer, heroclone47> treeMap = ConfigManager.getInstance().getConf(heroclone47.class);
		int heroId = -1;
		for(Map.Entry<Integer, heroclone47> entry : treeMap.entrySet() ){
			if( entry.getValue().getOpenCondition() == openCondition){
				heroId = entry.getValue().getId();
				break;
			}
		}
		if(heroId != -1){
			xbean.heroclone xheroClone = ActivityManager.getxHeroClone(roleId, false);
			if( !xheroClone.getClonelist().contains(Integer.valueOf(heroId)) ){
				xheroClone.getClonelist().add(heroId);
				sendSAddHeroClone(roleId,openCondition);
				sRefreshHeroClone(roleId);
				return true;
			}
		}
		return false;
	}
	
	/**
	 * 保存新手引导数据
	 * @param roleId
	 * @param yindaoId
	 * @return
	 */
	public boolean addNewyindao(long roleId,int yindaoId){
		chuhan.gsp.attr.PropRole prole = chuhan.gsp.attr.PropRole.getPropRole(roleId, false);
		
/*		//战队恢复到初始
		if(yindaoId == 100301){
			PAddTroop pAddTroop7 = new PAddTroop(roleId,2, 0,0,0);
			pAddTroop7.call();
			PAddTroop pAddTroop8 = new PAddTroop(roleId,1, 0,0,0);
			pAddTroop8.call();
			PAddTroop pAddTroop3 = new PAddTroop(roleId,1, 1,0,3);
			pAddTroop3.call();
		}*/
		
		if( !prole.getProperties().getNewyindao().contains(yindaoId) ){
			prole.getProperties().getNewyindao().add(yindaoId);
			newbieguide60 init = ConfigManager.getInstance().getConf(newbieguide60.class).get(yindaoId);
			if(init == null){
				return false;
			}
			List<Integer> dropadd = null;
			if( !init.getReward().equals("-1") ){
				dropadd = DropManager.getInstance().drop(roleId, init.getReward(), LogBehavior.NEWYINDAO,true);
			}
			
			//关卡通关装配符文引导特殊处理
			int newydId = Integer.parseInt(ConfigManager.getInstance()
					.getConf(config10.class).get(1332).configvalue);
			if(newydId == yindaoId){
				
				ItemColumn itemcol = Module.getItemColumn(roleId, BagTypes.EQUIP, false);
				HeroColumn herocol = HeroColumn.getHeroColumn(roleId, false);
				int heroId = Integer.parseInt(ConfigManager.getInstance().
						getConf(config10.class).get(1329).configvalue);
				int herokey = herocol.getKeyByHeroId(heroId);
				if(herokey != -1){
					int i = 1;	//符文位置从1开始
					Map<Integer,Integer> objMap = DropManager.getInstance().getDropObjectMapByInnerList(dropadd);
					for( Map.Entry<Integer, Integer> entry : objMap.entrySet() ){
						int itemkey = itemcol.getKeyByItemId(entry.getKey());
						if(itemkey != -1){
							PUseItem puse = new PUseItem(roleId, BagTypes.EQUIP,itemkey,i++,herokey);
							puse.call();
						}
					}
				}
			}
			return true;
		}
		return true;
	}
	
	/**
	 * 英雄克隆信息初始化
	 * @param roleId
	 */
	public void initHeroClont(long roleId){
		xbean.heroclone xheroClone = ActivityManager.getxHeroClone(roleId, false);
		TreeMap<Integer, heroclone47> treeMap = ConfigManager.getInstance().getConf(heroclone47.class);
		for(Map.Entry<Integer, heroclone47> entry : treeMap.entrySet() ){
			if( entry.getValue().getIs_had() == 1){
				if( !xheroClone.getClonelist().contains(entry.getValue().getId()) ){
					xheroClone.getClonelist().add(entry.getValue().getId());
				}
			}
		}
	}
	
	/**
	 * 刷新英雄克隆列表
	 * @param roleId
	 */
	public void sRefreshHeroClone(long roleId){
		initHeroClont(roleId);
		xbean.heroclone xheroClone = ActivityManager.getxHeroClone(roleId, false);
		SRefreshHeroClone snd = new SRefreshHeroClone();
		if(xheroClone.getClonelist() != null){
			snd.heroclonelist.addAll(xheroClone.getClonelist());
		}
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	/**
	 * 发送新增英雄之血的英雄ID
	 * @param roleId
	 * @param heroId
	 */
	public void sendSAddHeroClone(long roleId,int heroId){
		SAddHeroClone snd = new SAddHeroClone();
		snd.heroid = heroId;
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	
	/**
	 * 英雄克隆入口
	 * @param roleId
	 * @param heroid
	 * @return
	 */
	public boolean heroCloneEntry(long roleId, int heroid){
		xbean.heroclone xheroClone = ActivityManager.getxHeroClone(roleId, true);
		if( !xheroClone.getClonelist().contains(Integer.valueOf(heroid)) ){
			return false;
		}
		heroclone47 herocloneInit = ConfigManager.getInstance().getConf(heroclone47.class).get(heroid);
		if(herocloneInit == null){
			return false;
		}
		if( DropManager.getInstance().useDel(herocloneInit.getCloneCostId(), 
				herocloneInit.getCloneCostValue(),roleId, LogBehavior.HEROCLONECOST) ){
//			DropManager.getInstance().addHero(heroid,1,1,roleId,"heroclone");
			DropManager.getInstance().sendMailOrDropAdd(roleId, heroid, 1, 1, LogBehavior.HEROCLONE);
			return true;
		}	
		return false;
	}
	
	/**
	 * 祈愿入口
	 * @param roleId
	 * @return
	 */
	public boolean qiyuanEntry(long roleId){
		long now = GameTime.currentTimeMillis();
		chuhan.gsp.attr.PropRole prop = chuhan.gsp.attr.PropRole.getPropRole(roleId, false);
		if(DateUtil.inTheSameDay(now, prop.getProperties().getQiyuantime())){
			return false;
		}
		List<Integer> innerDropList = null;
		if(DateUtil.isYestoday(now, prop.getProperties().getQiyuantime())){
			if(prop.getProperties().getQiyuannum() < prop.getProperties().getQiyuanallnum()){
				prop.getProperties().setQiyuannum(prop.getProperties().getQiyuannum() + 1);
				prop.getProperties().setQiyuantime(now);

				if(prop.getProperties().getQiyuannum() == prop.getProperties().getQiyuanallnum()){
					try{
						String nmDropId = ConfigManager.getInstance().
								getConf(config10.class).get(1274).configvalue;
						innerDropList = DropManager.getInstance().drop(roleId, nmDropId, LogBehavior.QIYUANTAI);
					}catch(Exception e){
						e.printStackTrace();
					}
				}
			}else{
				prop.getProperties().setQiyuanallnum(QIYUAN_MAX);
				prop.getProperties().setQiyuannum(1);
//				prop.getProperties().setQiyuantime(now);
			}
		}else{
			prop.getProperties().setQiyuannum(1);
		}
		prop.getProperties().setQiyuantime(now);
		prop.refreshSweep(now);
		sendSGetQiYuan(roleId,innerDropList);
		
		ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.QIYUAN, 1);
		
		return true;
	}
	/**
	 * 祈愿成功返回
	 * @param roleId
	 * @param innerDropList
	 */
	public void sendSGetQiYuan(long roleId,List<Integer> innerDropList){
		SGetQiYuan snd = new SGetQiYuan();
		snd.result = SGetQiYuan.END_OK;
		if(innerDropList != null){
			snd.innerdropidlist.addAll(innerDropList);
		}
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	
	/**
	 * 获取月卡信息数据库数据
	 * @param roleId
	 * @param readonly
	 * @return
	 */
	public static xbean.monthcards getxMouthCards(long roleId, boolean readonly){
		xbean.monthcards xmonthcards;
		if(readonly){
			if(xtable.Properties.select(roleId) == null)
				return null;
			xmonthcards = xtable.Monthcardlist.select(roleId);
		}else{
			if(xtable.Properties.get(roleId) == null)
				return null;
			xmonthcards = xtable.Monthcardlist.get(roleId);
		}
		if(xmonthcards == null){
			if(readonly)
				xmonthcards = xbean.Pod.newmonthcardsData();
			else{
				xmonthcards = xbean.Pod.newmonthcards();
				xtable.Monthcardlist.insert(roleId, xmonthcards);
			}
		}
		return xmonthcards;
	}
	/**
	 * 添加月卡信息
	 * @param roleId
	 * @param cardId
	 * @return
	 */
	public boolean addMonthCard(long roleId,int cardId){
		monthcard45 monthcardInit = ConfigManager.getInstance().getConf(monthcard45.class).get(cardId);
		if(monthcardInit == null){
			return false;
		}
		long now = GameTime.currentTimeMillis();
		xbean.monthcards xmonthcards = ActivityManager.getxMouthCards(roleId, false);
		xbean.monthcard xcard = xmonthcards.getRolemonthcards().get(cardId);
		if(xcard == null){
			xcard = xbean.Pod.newmonthcard();
			xcard.setMonthcardid(cardId);
			xcard.setOvertime(now + 30 * DateUtil.dayMills);
			xcard.setGetboxlasttime(0);
			xmonthcards.getRolemonthcards().put(cardId, xcard);
			chuhan.gsp.attr.PropRole prole = chuhan.gsp.attr.PropRole.getPropRole(roleId, false);
			prole.addVipExp(monthcardInit.getVipexperience(), 0);
		}else{
			if( xcard.getOvertime() > now){
				xcard.setOvertime(xcard.getOvertime() + 30 * DateUtil.dayMills);
			}else{
				xcard.setOvertime(now + 30 * DateUtil.dayMills);
			}
		}		
		this.sSRefreshMonthCard(roleId, now);
		//购买月卡后刷新遗迹宝藏信息
		LotteryItemColumns lotterycol = LotteryItemColumns.getLotteryItemColumn(roleId, false);
		lotterycol.sSRefreshLottyItem(now);
		
		return true;
	}
	
	/**
	 * 是否有遗迹宝藏刷新月卡
	 * @param roleId
	 * @return
	 */
	public boolean isHaveLotteryMonth(long roleId){
		long now = GameTime.currentTimeMillis();
		xbean.monthcards cards = ActivityManager.getxMouthCards(roleId, false);
		for( Map.Entry<Integer, xbean.monthcard> entry : cards.getRolemonthcards().entrySet() ){
			if(now < entry.getValue().getOvertime()){
				monthcard45 monthcardInit = ConfigManager.getInstance().getConf(monthcard45.class).
						get(entry.getValue().getMonthcardid());
				if(monthcardInit != null){
					if( monthcardInit.getRefresh5Star() == 1 ){
						return true;
					}
				}
			}
		}
		return false;
	}
	
	/**
	 * 获得月卡英雄经验加成
	 * @param roleId
	 * @return
	 */
	public float getMonthHeroExpAdd(long roleId){
		try {
			long now = GameTime.currentTimeMillis();
			float result = 1.0f;
			xbean.monthcards cards = ActivityManager.getxMouthCards(roleId, false);
			for (Map.Entry<Integer, xbean.monthcard> entry : cards.getRolemonthcards().entrySet()) {
				if (now < entry.getValue().getOvertime()) {
					monthcard45 monthcardInit = ConfigManager.getInstance().getConf(monthcard45.class)
							.get(entry.getValue().getMonthcardid());
					if (monthcardInit != null) {
						float add = Float.parseFloat(monthcardInit.getExpBonus());
						result *= (1.0 + add);
					}
				}
			}
			return result;
		} catch (Exception e) {
			e.printStackTrace();
			return 1.0f;
		}
	}
	
	/**
	 * 获取月卡礼盒信息
	 * @param roleId
	 * @param cardId
	 * @return
	 */
	public boolean getMonthCard(long roleId, int cardId){
		monthcard45 monthcardInit = ConfigManager.getInstance().getConf(monthcard45.class).get(cardId);
		if(monthcardInit == null){
			return false;
		}
		long now = GameTime.currentTimeMillis();
		xbean.monthcards xmonthcards = ActivityManager.getxMouthCards(roleId, false);
		xbean.monthcard xcard = xmonthcards.getRolemonthcards().get(cardId);
		if(xcard == null){
			xcard = xbean.Pod.newmonthcard();
			xcard.setMonthcardid(cardId);
			xcard.setOvertime(0);
			xcard.setGetboxlasttime(0);
			xmonthcards.getRolemonthcards().put(cardId, xcard);
		}
		if(monthcardInit.getDuration() == -1 || xcard.getOvertime() > now){
			if( !DateUtil.inTheSameDay(now, xcard.getGetboxlasttime()) ){
				xcard.setGetboxlasttime(now);
				DropManager.getInstance().dropAddByOther(IDManager.YUANBAO, monthcardInit.getDailydiamond(), 
						0, 0, roleId,LogBehavior.MONTHCARDTODAY);
				DropManager.getInstance().dropAddByOther(IDManager.GOLD, monthcardInit.getDailygold(), 
						0, 0, roleId, LogBehavior.MONTHCARDTODAY);
				List<Integer> innerDropList = new LinkedList<Integer>();
				this.sendSMonthCard(roleId, innerDropList,cardId);
				this.sSRefreshMonthCard(roleId,now);
				return true;
			}
		}
		return false;
	}
	
	/**
	 * 月卡领取返回
	 * @param roleId
	 * @param innerDropList
	 */
	public void sendSMonthCard(long roleId,List<Integer> innerDropList,int cardId){
		SMonthCard snd = new SMonthCard();
		snd.result = SGetQiYuan.END_OK;
		snd.cardid = cardId;
		if(innerDropList != null){
			snd.innerdropidlist.addAll(innerDropList);
		}
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	/**
	 * 刷新月卡列表
	 * @param roleId
	 * @param now
	 */
	public void sSRefreshMonthCard(long roleId, long now){
		xbean.monthcards xmonthcards = ActivityManager.getxMouthCards(roleId, false);
		SRefreshMonthCard snd = new SRefreshMonthCard();
		for( Map.Entry<Integer, xbean.monthcard> entry : xmonthcards.getRolemonthcards().entrySet() ){
			chuhan.gsp.monthcard monthcard = new chuhan.gsp.monthcard();
			monthcard.monthcardid = entry.getValue().getMonthcardid();
			monthcard.overtime = entry.getValue().getOvertime();
			if( DateUtil.inTheSameDay(now, entry.getValue().getGetboxlasttime()) ){
				monthcard.istodayget = 1;
			}else{
				monthcard.istodayget = 0;
			}
			snd.monthcardlist.add(monthcard);
		}
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	
	/**
	 * 获取连续登陆基础数据
	 * @param day
	 * @param type
	 * @param room
	 * @return
	 */
	public loginbonus42 getLoginInit(int day, int type, int room){
		TreeMap<Integer, loginbonus42> treepMap = ConfigManager.getInstance().getConf(loginbonus42.class);
		for( Map.Entry<Integer, loginbonus42> entry : treepMap.entrySet() ){
			if(type == 1){
				if(entry.getValue().getDay() == day && entry.getValue().getType() == type){
					return entry.getValue();
				}
			}else{
				if(entry.getValue().getDay() == day && entry.getValue().getType() == type &&
						entry.getValue().getRoom() == room){
					return entry.getValue();
				}
			}
		}
		return null;
	}
	/**
	 * 连续登陆物品掉落
	 * @param roleId
	 * @param init
	 */
	public void loginDrop(long roleId,loginbonus42 init){
		List<Integer> list = ParserString.parseString2Int(init.getRewardAndNum());
		if(list.size() != 2){
			return;
		}
		DropManager.getInstance().sendMailOrDropAdd(roleId, list.get(0), list.get(1), 0, LogBehavior.LOGINDROP);
	}
	/**
	 * 判断是否继续连续登陆
	 * @param prop
	 * @param now
	 * @return
	 */
	public boolean loginNumAdd(PropRole prop, long now){
		if( !DateUtil.inTheSameDay(now, prop.getProperties().getSigntime()) ){
/*			if(prop.getProperties().getSigntime() == 0){
				prop.getProperties().setSigntime(now);
				sRefreshLogin(prop);
				return true;
			}*/
			prop.getProperties().setSigntime(now);
			//7天
			int room7 = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1283).configvalue);
			if(prop.getProperties().getSignnum7() == 0 || 
					!DateUtil.isYestoday(now, prop.getProperties().getSigntime()) ){
				
				loginbonus42 init = this.getLoginInit(1, 1, room7);
				if(init == null){
					return false;
				}
				prop.getProperties().setSignnum7(init.getId());
				loginDrop(prop.getRoleId(),init);
			}else{
				loginbonus42 oldinit = ConfigManager.getInstance().getConf(loginbonus42.class).
						get(prop.getProperties().getSignnum7());
				if(oldinit == null){
					return false;
				}
				loginbonus42 next = getLoginInit(oldinit.getDay()+1,1,room7);
				if(next == null){
					next = getLoginInit(1,1,room7);
				}
				if(next == null){
					return false;
				}
				prop.getProperties().setSignnum7(next.getId());
				loginDrop(prop.getRoleId(),next);
			}

			//28天
			if(prop.getProperties().getSignnum28() == 0){
				int room = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1284).configvalue);
				loginbonus42 init = this.getLoginInit(1, 2, room);
				if(init == null){
					return false;
				}
				prop.getProperties().setSignnum28(init.getId());
				loginDrop(prop.getRoleId(),init);
			}else{
				loginbonus42 oldinit = ConfigManager.getInstance().getConf(loginbonus42.class).
						get(prop.getProperties().getSignnum28());
				if(oldinit == null){
					return false;
				}
				loginbonus42 next = getLoginInit(oldinit.getDay()+1,2,oldinit.getRoom());
				if(next == null){
					next = getLoginInit(1,2,oldinit.getNextRoom());
				}
				if(next == null){
					return false;
				}
				prop.getProperties().setSignnum28(next.getId());
				loginDrop(prop.getRoleId(),next);
			}
			//新手引导完成后再弹窗
			if( prop.getProperties().getNewyindao().contains(100501) ){
				sRefreshLogin(prop);
			}
			return true;
		}
		return false;
	}
	/**
	 * 刷新连续登陆数据
	 * @param prop
	 */
	public void sRefreshLogin(PropRole prop){
		SRefreshLogin snd = new SRefreshLogin();
		snd.signnum7 = prop.getProperties().getSignnum7();
		snd.signnum28 = prop.getProperties().getSignnum28();
		xdb.Procedure.psendWhileCommit(prop.getRoleId(), snd);
	}
	/**
	 * 活跃度任务完成
	 * @param roleId
	 * @param taskType
	 * @param num
	 */
	public void addHYTaskOver(long roleId,int taskType,int num){
		HuoyueColumns hycol = HuoyueColumns.getHuoyueColumn(roleId, false);
		hycol.huoyueTaskOver(taskType, num,0);
	}
	/**
	 * 活跃度任务完成（关卡相关使用）
	 * @param roleId
	 * @param taskType
	 * @param num
	 */
	public void addHYTaskOver(long roleId,int taskType,int num,int type){
		HuoyueColumns hycol = HuoyueColumns.getHuoyueColumn(roleId, false);
		hycol.huoyueTaskOver(taskType, num,type);
	}
	
	/**
	 * 系统公告
	 * @param roleId
	 * @param ID
	 */
	public static void addMsgNotice(long roleId,int logicid,String source,String word1){
		if(source.equals(ActivityManager.BOSSOPEN)){	//boss 出现
			monster13 mInit = ConfigManager.getInstance().getConf(monster13.class).get(logicid);
			if(mInit == null){
				return;
			}
			Message.broadcastMsgNotifyWithDelay(2000,9, mInit.getMonstername() );
		}else if(source.equals(ActivityManager.BOSSTAOZOU)){	//boss 逃走
			monster13 mInit = ConfigManager.getInstance().getConf(monster13.class).get(logicid);
			if(mInit == null){
				return;
			}
			Message.broadcastMsgNotifyWithDelay(2000,11, mInit.getMonstername() );
		}else if(source.equals(ActivityManager.GMSEND)){	//GM发送跑马灯
			Message.broadcastMsgNotifyWithDelay(2000,13, word1);
		} else {
			chuhan.gsp.attr.PropRole prop = chuhan.gsp.attr.PropRole.getPropRole(roleId, false);
			if (prop == null) {
				return;
			} else if (source.equals(ActivityManager.ZHAOMU)) { // 英雄招募
				ihero = ConfigManager.getInstance().getConf(hero01.class).get(logicid);
				if (ihero != null && ihero.getSystemBroadcasts() == 1) {
					Message.broadcastMsgNotifyWithDelay(2000, 1, prop.getProperties().getRolename(), 
							String.valueOf(ihero.getId()));
				}
			} else if (source.equals(ActivityManager.BAOZANG)) { // 遗迹宝藏
				item = ConfigManager.getInstance().getConf(item26.class).get(logicid);
				if (item != null && item.getSystemBroadcasts() == 1) {
					Message.broadcastMsgNotifyWithDelay(2000, 2, prop.getProperties().getRolename(), 
							String.valueOf(item.getId()));
				}
			} else if (source.equals(ActivityManager.GUANKA)) { // 特殊关卡
				item = ConfigManager.getInstance().getConf(item26.class).get(logicid);
				if (item != null && item.getSystemBroadcasts() == 1) {
					Message.broadcastMsgNotifyWithDelay(2000, 3, prop.getProperties().getRolename(), 
							String.valueOf(item.getId()));
				}
				ihero = ConfigManager.getInstance().getConf(hero01.class).get(logicid);
				if (ihero != null && ihero.getSystemBroadcasts() == 1) {
					Message.broadcastMsgNotifyWithDelay(2000, 3, prop.getProperties().getRolename(), 
							String.valueOf(ihero.getId()));
				}
			} else if (source.equals(ActivityManager.TONGGUAN)) { // 通关简历
				item = ConfigManager.getInstance().getConf(item26.class).get(logicid);
				if (item != null && item.getSystemBroadcasts() == 1) {
					Message.broadcastMsgNotifyWithDelay(2000, 4, prop.getProperties().getRolename(), 
							String.valueOf(item.getId()));
				}
				ihero = ConfigManager.getInstance().getConf(hero01.class).get(logicid);
				if (ihero != null && ihero.getSystemBroadcasts() == 1) {
					Message.broadcastMsgNotifyWithDelay(2000, 4, prop.getProperties().getRolename(), 
							String.valueOf(ihero.getId()));
				}
			} else if (source.equals(ActivityManager.JINJIE)) { // 英雄进阶
				ihero = ConfigManager.getInstance().getConf(hero01.class).get(logicid);
				if (ihero != null && ihero.getSystemBroadcasts() == 1) {
					Message.broadcastMsgNotifyWithDelay(2000, 5, prop.getProperties().getRolename(), 
							String.valueOf(ihero.getId()));
				}
			} else if (source.equals(ActivityManager.DAOJU)) { // 道具使用
				item = ConfigManager.getInstance().getConf(item26.class).get(logicid);
				if (item != null && item.getSystemBroadcasts() == 1) {
					Message.broadcastMsgNotifyWithDelay(2000, 6, prop.getProperties().getRolename(), 
							String.valueOf(item.getId()));
				}
			} else if (source.equals(ActivityManager.VIPLEVELUP)) { // vip升级
				if (prop.getVipLevel() < 5) {
					String[] parameters = { prop.getProperties().getRolename(),String.valueOf(prop.getVipLevel()) };
					xdb.Procedure.psendWhileCommit(prop.getRoleId(),Message.getMsgNotify(7, parameters));
				} else {
					Message.broadcastMsgNotifyWithDelay(2000, 7, prop.getProperties().getRolename(), 
							String.valueOf(prop.getVipLevel()));
				}
			} else if (source.equals(ActivityManager.SHENQIFULL)) { // 神器满级
				Message.broadcastMsgNotifyWithDelay(2000, 8, prop.getProperties().getRolename(), word1);
			}else if (source.equals(ActivityManager.BOSSKILL)) { // BOSS击杀
				monster13 mInit = ConfigManager.getInstance().getConf(monster13.class).get(logicid);
				if(mInit == null){
					return;
				}
				Message.broadcastMsgNotifyWithDelay(2000, 10, prop.getProperties().getRolename(), mInit.getMonstername());
			}
		}	
	}
	
}
