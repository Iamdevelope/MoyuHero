package chuhan.gsp.stage;


import java.util.*;
import java.util.Map.Entry;

import xbean.StageBattleInfo;
import xbean.StageInfo;
import chuhan.gsp.attr.GoldAddType;
import chuhan.gsp.attr.PAddExpProc;
import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.config10;
import chuhan.gsp.award.DropManager;
import chuhan.gsp.award.IDManager;
import chuhan.gsp.battle.Battle;
import chuhan.gsp.battle.BattleManager;
import chuhan.gsp.game.bossbox25;
import chuhan.gsp.game.mysteriousshop43;
import chuhan.gsp.hero.PAddExpHero;
import chuhan.gsp.item.innerdrop16;
import chuhan.gsp.log.LogBehavior;
import chuhan.gsp.log.Logger;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.msg.Message;
import chuhan.gsp.play.activity.ActivityGameManager;
import chuhan.gsp.play.activity.ActivityManager;
import chuhan.gsp.play.huoyuedu.HuoyueColumns;
import chuhan.gsp.play.tanxian.TanXianColumns;
import chuhan.gsp.task.chapterinfo23;
import chuhan.gsp.task.stage11;
import chuhan.gsp.util.Conv;
import chuhan.gsp.util.DateUtil;
import chuhan.gsp.util.Misc;
import chuhan.gsp.util.ParserString;

public class StageRole {
	
	public static Logger logger = Logger.getLogger("StageRole");
	public static final int SM_STAGE = 5;
	public static final int TIME_GOLD_STAGE = 6;
	public static final int TIME_EXP_STAGE = 7;
	public static final int MOHE_NUM = 3;
	
	final private long roleId;
	final private xbean.StageRole xrole;
	final private boolean readonly;
	
	public List<BattleInfo> battleInfoList = new LinkedList<BattleInfo>();
	
	public static StageRole getStageRole(long roleId)
	{
		return getStageRole(roleId,false);
	}
	
	public static StageRole getStageRole(long roleId, boolean readonly)
	{
		xbean.StageRole xrole;
		if(readonly)
		{
			if(xtable.Properties.select(roleId) == null)
				return null;
			xrole = xtable.Stageroles.select(roleId);
		}
		else
		{
			if(xtable.Properties.get(roleId) == null)
				return null;
			xrole = xtable.Stageroles.get(roleId);
		}
		if(xrole == null)
		{
			if(readonly)
				xrole = xbean.Pod.newStageRoleData();
			else
			{
				xrole = xbean.Pod.newStageRole();
				xtable.Stageroles.insert(roleId, xrole);
			}
		}
		
		return new StageRole(roleId, xrole, readonly);
	}
	
	public static xbean.moheodds getMoheOdds(long roleId, boolean readonly)
	{
		xbean.moheodds xmohe;
		if(readonly){
			if(xtable.Properties.select(roleId) == null)
				return null;
			xmohe = xtable.Moheoddses.select(roleId);
		}else{
			if(xtable.Properties.get(roleId) == null)
				return null;
			xmohe = xtable.Moheoddses.get(roleId);
		}
		if(xmohe == null)
		{
			if(readonly)
				xmohe = xbean.Pod.newmoheoddsData();
			else
			{
				xmohe = xbean.Pod.newmoheodds();
				xtable.Moheoddses.insert(roleId, xmohe);
			}
		}
		
		return xmohe;
	}
	
	private StageRole(long roleId, xbean.StageRole xrole, boolean readonly)
	{
		this.roleId = roleId;
		this.xrole = xrole;
		this.readonly = readonly;
		init();
	}
	
	public boolean isReadonly() {
		return readonly;
	}
	
	private void init()
	{
		onInitStage(-1);
//		if(xrole.getStages().isEmpty())
//			activeStage(1);
	}
	
	public xbean.StageRole getStageRoleXbean()
	{
		return xrole;
	}
	/**
	 * 根据关卡id获取关卡数据
	 * @param stagebattleId
	 * @return
	 */
	public xbean.StageBattleInfo getBattleInfo(int stagebattleId){
		int stageId = getStageNumByBattleId(stagebattleId);
		xbean.StageInfo xstage = xrole.getStages().get(stageId);
		if(xstage == null)
			return null;
		xbean.StageBattleInfo xbattle = xstage.getStagebattles().get(stagebattleId);
		return xbattle;
	}
	
	/**
	 * 开始战斗入口
	 * @param stagebattleId
	 * @param troopid
	 * @param useyuanbao
	 * @return
	 */
	public boolean fightStageBattle(int stagebattleId,int troopid, boolean useyuanbao,boolean isSweep){
		PropRole prole = PropRole.getPropRole(roleId, false);
		stage11 guankacfg = ConfigManager.getInstance().getConf(stage11.class).get(stagebattleId);
		if(guankacfg == null)
			return false;
/*		int stageId = getStageNumByBattleId(stagebattleId);
		xbean.StageInfo xstage = xrole.getStages().get(stageId);
		if(xstage == null)
			return false;*/
		xbean.StageBattleInfo xbattle = getBattleInfo(stagebattleId);
		if(xbattle == null)
			return false;
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		
		//判断扫荡
		if(isSweep){
//			if(!prole.useSweep(now)){
//				return false;
//			}
			if(xbattle.getMaxstar() != 3){
				return false;
			}
		}
		
		//神秘关卡判断
		if(guankacfg.getStagetype() == SM_STAGE && 
				( now > prole.getProperties().getSmendtime() || 
				prole.getProperties().getBattlenum() != stagebattleId) ){
			Message.psendMsgNotify(roleId, 235);
			return false;
		}
		//限时关卡判断
		if(guankacfg.getStagetype() == TIME_GOLD_STAGE || guankacfg.getStagetype() == TIME_EXP_STAGE){
			int weekday = DateUtil.getCurrentWeekDay(now);
			if(guankacfg.getStagetype() == TIME_GOLD_STAGE){
				if(weekday == 1 || weekday == 3 || weekday == 5){
					Message.psendMsgNotify(roleId, 135);
					return false;
				}
			}
			if(guankacfg.getStagetype() == TIME_EXP_STAGE){
				if(weekday == 2 || weekday == 4 || weekday == 6){
					Message.psendMsgNotify(roleId, 135);
					return false;
				}
			}
		}
		
		processStageBattleInfo(xbattle, now);
		if(!useyuanbao){
			if (xbattle.getFightnum() >= guankacfg.getLimittime() && guankacfg.getLimittime() != -1) {
				// 达到战斗上限
				Message.psendMsgNotify(roleId, 135);
				return false;
			}
//			if(xbattle.getSweepnum() >= prole.getVipInit().getRapidClearNums()){
//				Message.psendMsgNotify(roleId, 135);
//				return false;
//			}

		}else{
			return false;//TODO
		}
		
		if( !prole.useTili(guankacfg.cost)){
			return false;
		}
		
//		PBeginBattle beginbattle = new PBeginBattle(roleId, stagebattleId,troopid);
//		if(!beginbattle.call()){	
//			return false;
//		}
		String dropstr = guankacfg.getStagedrop();
		if(xbattle.getAllfightnum() % guankacfg.getSpecialcondition() == 0 || guankacfg.getSpecialcondition() != -1){
			dropstr = guankacfg.getSpecialstagedrop();
		}
		
		Battle battleinfo = BattleManager.getInstance().CreateBattleInfo(roleId, stagebattleId, troopid,dropstr);
		if(battleinfo == null)
			return false;
		
		//神秘关卡进入后数据清0
		if(guankacfg.getStagetype() == SM_STAGE){
			prole.getProperties().setBattlenum(0);
		}
		//魔盒数据清空
		prole.getProperties().getMoheshop().clear();
		//特殊新手引导
		int newbattleId = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1331)
				.configvalue);
		if(newbattleId == stagebattleId){
			int newydId = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1332)
					.configvalue);
			if( !prole.getProperties().getNewyindao().contains(newydId) ){
				int innerId = Integer.parseInt(ConfigManager.getInstance().
						getConf(config10.class).get(1330).configvalue);
				List<Integer> addList = DropManager.getInstance().getAllInnerKeyListByInDpId(innerId);
				battleinfo.getInDropList().addAll(addList);
			}
		}
		
		if(!isSweep){
			xdb.Procedure.psendWhileCommit(roleId, new SFightStageBattle(battleinfo.getProtocolBattelInfo()));
		}
		updateStateBattleInfo(xbattle, now, isSweep);
		
		this.sendSRefreshStageBattle(xbattle, getStageNumByBattleId(stagebattleId));
		
		ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.BETTLE_BEGIN, 1,
				battleinfo.getiBattleInfo().getStagetype());
//		boolean firstwin = (xbattle.getLastfighttime() == 0);
		
//		int directend = 0;
//		if(firstwin)//没打过
		{
//			directend = (guankacfg.type == 3)? -1 : 7;
		}
		//处理战斗
//		int awardId = 0;
		//触发了斩魔BOSS不发奖
//		boolean isRaidBoss = RaidBossRole.getRaidBossRole(roleId, false).activateBoss();
//		if(!isRaidBoss) {
//			awardId = firstwin ? guankacfg.rewardIDfirst : guankacfg.rewardID;
//		}
//		PNewBattle pbattle = new PNewBattle(roleId, guankacfg.getId(), awardId, false,directend);
//		pbattle.call();
//		if(!pbattle.isSuccess())
//		{
//			logger.error("关卡进战斗失败：roleId="+roleId+",guankaId="+guankacfg.id);
//			return false;
//		}
/*		
		boolean iswin =( pbattle.getSSendBattleScript().result.winround > 0 );
		int first3star = (pbattle.getSSendBattleScript().result.winround == 1 && xbattle.getMaxstar() < 3)? 2 : 1;
		
		int addexp = guankacfg.shengwangreward * first3star;
		//给人经验
		if(addexp == guankacfg.shengwangreward)
		  addexp = specialActivity(addexp);
		if(addexp == guankacfg.shengwangreward)
			addexp *= Ploy.getDoubleExp();
		
		PAddExpProc proc = new PAddExpProc(roleId, addexp, 1, "");//给人物经验
		if(proc.call())
			pbattle.getSSendBattleScript().result.addexp = addexp;
		
		//给银两
		int addmoney = guankacfg.moneyreward * first3star;
		if(addmoney == guankacfg.moneyreward && DateUtil.getCurrentWeekDay() == 1)
			addmoney = addmoney * 2;
		chuhan.gsp.item.Bag bag = new chuhan.gsp.item.Bag(roleId, false);
		long realAdd = bag.addMoney(addmoney, "fubem money award", 1,1);
		pbattle.getSSendBattleScript().result.addmoney = Conv.toInt(realAdd);
		if(isRaidBoss) {//触发了斩魔BOSS
			pbattle.getSSendBattleScript().battletype = BattleType.TRIGGER_BOSS_PVE;
		}
		pbattle.sendSSendBattleScript();
		//触发
		if(iswin)
		{
			xbattle.setFightnum(xbattle.getFightnum() + 1);
			xbattle.setLastfighttime(now);
			refreshStageAndBattle(xstage, xbattle, 4-pbattle.getSSendBattleScript().result.winround);
		}
		else
		{
			sendSRefreshStageBattle(xbattle);
		}
		*/
//		new PEndFightStageBattle(roleId, 3).call();
		return true;
	}
	
	/**
	 * 结束战斗结果统计
	 * @param roleId
	 * @param pass
	 * @return
	 */
	public boolean EndfightStageBattle(long roleId, int pass, boolean isSweep){
		Battle battleInfo = BattleManager.getInstance().GetBInfoByRId(roleId);
		if(battleInfo == null){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		PropRole prole = PropRole.getPropRole(roleId, false);
		if(pass == 0){
//			sendEndBattle(prole,pass,isSweep,null);
			return false;
		}
		int stageType = battleInfo.getiBattleInfo().getStagetype();
		int stagebattleId = battleInfo.getiBattleInfo().getId();
		xbean.StageInfo xstage = xrole.getStages().get(getStageNumByBattleId(stagebattleId));		//临时
		if(xstage == null)
			return false;
		xbean.StageBattleInfo xbattle = xstage.getStagebattles().get(battleInfo.getiBattleInfo().getId());
		if(xbattle == null)
			return false;
		
		refreshStageAndBattle(xstage,xbattle,pass);
		
		chuhan.gsp.stage.BattleInfo battleinfo = battleInfo.getProtocolBattelInfo();
		
		//全服活动加成
		float gameActGoldAdd = ActivityGameManager.getInstance().getExpAdd();
		if(battleInfo.getiBattleInfo().getGoldreward() != -1)
			prole.addGold((int) ((float)battleInfo.getiBattleInfo().getGoldreward() * gameActGoldAdd)
					, GoldAddType.ADD_BATTLE);
		if(battleInfo.getiBattleInfo().getExpcrystal() != -1)
			prole.addZiYuan(battleInfo.getiBattleInfo().getExpcrystal(), 0, IDManager.EXPJIEJING);
		
/*		float passAdd = 1.0f;
		try{
			if(pass == 1){
				passAdd = Float.parseFloat(ConfigManager.getInstance().getConf(config10.class).get(1242).configvalue);
			}else if(pass == 2){
				passAdd = Float.parseFloat(ConfigManager.getInstance().getConf(config10.class).get(1243).configvalue);
			}else if(pass == 3){
				passAdd = Float.parseFloat(ConfigManager.getInstance().getConf(config10.class).get(1244).configvalue);
			}
		}catch(Exception ex){
			ex.printStackTrace();
		}
		//月卡加成
		float monthAdd = ActivityManager.getInstance().getMonthHeroExpAdd(roleId);
		//全服活动加成
		float gameActExpAdd = ActivityGameManager.getInstance().getExpAdd();
		PAddExpHero hero = new PAddExpHero(roleId,battleInfo.getUseHeroList(),
				(int)((float)battleInfo.getiBattleInfo().getHeroexp()* passAdd * monthAdd * gameActExpAdd),
				PAddExpHero.BATTLE,"");
		hero.call();
*/
		
		PAddExpProc proc = new PAddExpProc(roleId, 
				(battleInfo.getiBattleInfo().getPlayerexp()) , 
				PAddExpProc.BATTLE, "");
		proc.call();

		//处理神秘商店和神秘关卡
		int smStageOrShop = getSMstageOrshop(battleInfo,xstage);
		//处理魔盒相关内容
		if(battleInfo.getiBattleInfo().getBossbox() != -1){
			getMoheItem(prole,battleInfo.getiBattleInfo().getBossbox());
		}
		
		float add[] = ActivityGameManager.getInstance().dropAddArray(stagebattleId);
		DropManager.getInstance().innerIdListToDrop(battleInfo.getInDropList(), roleId, LogBehavior.STAGEREWARD,false,add[2]);
		
		
		
		sendEndBattle(prole,smStageOrShop,isSweep,battleinfo);
		ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.BETTLE_END, 1,
				battleInfo.getiBattleInfo().getStagetype());
		
		//通关和杀怪相关活动数据统计
		ActivityGameManager.getInstance().addBattleActivity(roleId, stagebattleId,1);
		ActivityGameManager.getInstance().addKillActivity(roleId, battleinfo.monstergroup);
		
		//跑马等
		String msgType = ActivityManager.TONGGUAN;
		if(stageType == this.SM_STAGE){
			msgType = ActivityManager.GUANKA;
		}
		List<innerdrop16> innerList = DropManager.getInstance().getInnerByInnerIdList(battleInfo.getInDropList());
		for(innerdrop16 innerDrop : innerList){
			ActivityManager.getInstance().addMsgNotice(roleId,innerDrop.getObjectid(),msgType,"");
		}
		
		

		
		// 特殊新手引导
		int newbattleId = Integer.parseInt(ConfigManager.getInstance()
				.getConf(config10.class).get(1331).configvalue);
		if (newbattleId == stagebattleId) {
			int newydId = Integer.parseInt(ConfigManager.getInstance()
					.getConf(config10.class).get(1332).configvalue);
			if (!prole.getProperties().getNewyindao().contains(newydId)) {
				ActivityManager.getInstance().addNewyindao(roleId, newydId);
			}
		}
		
		return true;
	}
	
	/**
	 * 购买关卡数量
	 * @param prole
	 * @param battleId
	 * @return
	 */
	public boolean buyStateBattleNum(PropRole prole,int battleId,long now){
//		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		stage11 guankacfg = ConfigManager.getInstance().getConf(stage11.class).get(battleId);
		if(guankacfg == null)
			return false;
		xbean.StageBattleInfo xbattle = getBattleInfo(battleId);
		if(xbattle == null)
			return false;
		List<Integer> costList = ParserString.parseString2Int(guankacfg.getResetCost());
		if(costList == null)
			return false;
		processStageBattleInfo(xbattle,now);
		if(xbattle.getBuybattlenum() < prole.getBuyBattleMaxNum()){
			int cost = 0;
			if( costList.size() > xbattle.getBuybattlenum() ){
				cost = costList.get(xbattle.getBuybattlenum());
			}else{
				cost = costList.get(costList.size() - 1);
			}
			if( -cost != prole.delYuanBao(-cost, 0)){
				return false;
			}
			xbattle.setFightnum(0);
			xbattle.setBuybattlenum(xbattle.getBuybattlenum() + 1);
			xbattle.setBuybattlelasttime(now);
			this.sendSRefreshStageBattle(xbattle, getStageNumByBattleId(battleId));
			return true;
		}
		return false;
	}
	
	/**
	 * 发送战斗结束（包括扫荡结束）
	 * @param prole
	 * @param result
	 * @param isSweep
	 */
	public void sendEndBattle(PropRole prole,int result,boolean isSweep,chuhan.gsp.stage.BattleInfo battleinfo){
		BattleManager.getInstance().DeleteBattleInfo(roleId);
		if(isSweep){
			/*SSweepBattle snd = new SSweepBattle();
			snd.time = 0;
			snd.zhangjie = 0;

			snd.endtype = SSweepBattle.END_OK;
			snd.smid = result;
			if(battleinfo != null){
				snd.battleinfo = battleinfo;
			}
			if(result != 0){
				snd.time = (int) ((prole.getProperties().getSmendtime() - chuhan.gsp.main.GameTime.currentTimeMillis())/1000);
				snd.zhangjie = prole.getProperties().getSmzhangjie();
			}
			snd.moheshop.putAll(getMoheMap(prole));
			snd.smshop.putAll(this.getSmShopMap(prole));
			xdb.Procedure.psend(roleId, snd);*/	
			this.battleInfoList.add(battleinfo);
		}else{
			SEndFightStageBattle snd = new SEndFightStageBattle();
			snd.time = 0;
			snd.zhangjie = 0;

			snd.endtype = SEndFightStageBattle.END_OK;
			snd.smid = result;
			if(result != 0){
				snd.time = (int) ((prole.getProperties().getSmendtime() - chuhan.gsp.main.GameTime.currentTimeMillis())/1000);
				snd.zhangjie = prole.getProperties().getSmzhangjie();
			}
			snd.moheshop.putAll(getMoheMap(prole));
			snd.smshop.putAll(this.getSmShopMap(prole));
			xdb.Procedure.psend(roleId, snd);
		}		
	}
	/**
	 * 返回魔盒数据（用于消息）
	 * @param prole
	 * @return
	 */
	private HashMap<Integer,chuhan.gsp.stage.mohe> getMoheMap(PropRole prole){
		HashMap<Integer,chuhan.gsp.stage.mohe> mohemap = new HashMap<Integer,chuhan.gsp.stage.mohe>();
		for(Map.Entry<Integer, xbean.mohe> entry : prole.getProperties().getMoheshop().entrySet()){
			mohe mohedata = new mohe();
			mohedata.id = entry.getValue().getId();
			mohedata.isopen = entry.getValue().getIsopen();
			mohedata.place = entry.getValue().getPlace();
			mohemap.put(mohedata.id, mohedata);
		}
		return mohemap;	
	}
	/**
	 * 返回神秘商店物品信息（用于消息）
	 * @param prole
	 * @return
	 */
	public static HashMap<Integer,chuhan.gsp.stage.smshopdata> getSmShopMap(PropRole prole){
		HashMap<Integer,chuhan.gsp.stage.smshopdata> smShopmap = new HashMap<Integer,chuhan.gsp.stage.smshopdata>();
		for(Map.Entry<Integer, xbean.smshopdata> entry : prole.getProperties().getSmshop().entrySet()){
			smshopdata smShopdata = new smshopdata();
			smShopdata.id = entry.getValue().getId();
			smShopdata.isopen = entry.getValue().getIsopen();
			smShopdata.price = entry.getValue().getPrice();
			smShopmap.put(smShopdata.id, smShopdata);
		}
		return smShopmap;	
	}
	/**
	 * 随机魔盒信息
	 * @param prole
	 * @param bossboxid
	 */
	public void getMoheItem(PropRole prole,int bossboxid){
		xbean.moheodds moheodds = this.getMoheOdds(prole.getRoleId(), false);
		prole.getProperties().getMoheshop().clear();
		List<bossbox25> allList = this.getHaveBossbox(prole.getLevel(),bossboxid );
		List<Integer> allDrop = new ArrayList<Integer>();
		List<Integer> allProb = new ArrayList<Integer>();
		for(bossbox25 bossbox : allList){
			int dropwight = bossbox.getDropwight1();
			Integer odds = moheodds.getMoheoddsmap().get(bossbox.getId());
			if(odds != null){
				dropwight = dropwight + odds.intValue() * bossbox.getDropwight1plus();
			}
			allDrop.add(bossbox.getId());
			allProb.add(dropwight);
		}
		List<Integer> getList = DropManager.getInstance().getDropIdList(
				DropManager.getInstance().getDropMap(allDrop,allProb,0),3);
		//添加未出魔盒累计次数
		for(bossbox25 bossbox : allList){
			if( !getList.contains(Integer.valueOf(bossbox.getId())) &&
					bossbox.getDropwight1plus() > 0){
				Integer odds = moheodds.getMoheoddsmap().get(bossbox.getId());
				if(odds != null){
					moheodds.getMoheoddsmap().put(bossbox.getId(), odds.intValue() + 1);
				}else{
					moheodds.getMoheoddsmap().put(bossbox.getId(),1);
				}
			}
		}
		//清空已出魔盒累计次数
		for(Integer moheid : getList){
			if(moheid == -1){
				break;
			}
			moheodds.getMoheoddsmap().put(moheid,0);
			xbean.mohe mohedata = xbean.Pod.newmohe();
			mohedata.setId(moheid);
			prole.getProperties().getMoheshop().put(moheid, mohedata);
		}
	}
	
	/**
	 * 获得所有能掉落的魔盒
	 * @param level
	 * @return
	 */
	public List<bossbox25> getHaveBossbox(int level,int bossboxid){
		List<bossbox25> result = new LinkedList<bossbox25>();
		TreeMap<Integer,bossbox25> bossboxMap = ConfigManager.getInstance().getConf(bossbox25.class);
		for(Map.Entry<Integer, bossbox25> entry : bossboxMap.entrySet() ){
			if(entry.getValue().getDorplevel() <= level && entry.getValue().getBossboxid() == bossboxid){
				result.add(entry.getValue());
			}
		}
		return result;
	}
	/**
	 * 计算神秘商店和神秘关卡
	 * @param battleInfo
	 * @return
	 */
	public int getSMstageOrshop(Battle battleInfo,xbean.StageInfo xstage){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		int stagebattleId = battleInfo.getiBattleInfo().getId();
		PropRole prole = PropRole.getPropRole(roleId, false);
		if( !prole.isHaveSM(stagebattleId) ){
			//神秘商店
			int percent = prole.getSMShopAdd();
			if(chuhan.gsp.util.Misc.getRateValue(10000) < percent &&
					battleInfo.getiBattleInfo().getMysteriousShop() != -1){
				prole.getProperties().getSmshop().clear();
				int time = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1116).configvalue);
				prole.getProperties().setSmshop_notcome(0);
				prole.getProperties().setSmguanka_nocome(prole.getProperties().getSmguanka_nocome() + 1);
				prole.getProperties().setSmzhangjie(xstage.getId());
				prole.getProperties().setBattlenum(battleInfo.getiBattleInfo().getMysteriousShop());
				prole.getProperties().setSmendtime(now + time * 1000);
				getSmShopList(prole,battleInfo.getiBattleInfo().getMysteriousShop());
				
				ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.SHOP_SM, 1);

				return battleInfo.getiBattleInfo().getMysteriousShop();
			}
			//神秘关卡
			percent = prole.getSMGuankaAdd();
			if(chuhan.gsp.util.Misc.getRateValue(10000) < percent &&
					battleInfo.getiBattleInfo().getMysteriousStage() != -1){
				int time = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1117).configvalue);
				prole.getProperties().setSmguanka_nocome(0);
				prole.getProperties().setSmshop_notcome(prole.getProperties().getSmshop_notcome() + 1);
				prole.getProperties().setSmzhangjie(xstage.getId());
				prole.getProperties().setBattlenum(battleInfo.getiBattleInfo().getMysteriousStage());
				prole.getProperties().setSmendtime(now + time * 1000);
				prole.getProperties().getSmshop().clear();
				return battleInfo.getiBattleInfo().getMysteriousStage();
			}
			
			prole.getProperties().setSmguanka_nocome(prole.getProperties().getSmguanka_nocome() + 1);
			prole.getProperties().setSmshop_notcome(prole.getProperties().getSmshop_notcome() + 1);
		}
		return 0;
	}
	/**
	 * 获取神秘商店物品
	 * @param prole
	 * @param shopId
	 */
	public void getSmShopList(PropRole prole,int shopId){
		List<mysteriousshop43> allshop =  getAllCanShop(shopId);
		List<mysteriousshop43> haveList = new LinkedList<mysteriousshop43>();
		int ybNeedNum = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1266).configvalue);
		int allNeedNum = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1267).configvalue);
		
		getRandomId(0,ybNeedNum,true,allshop,haveList);
		getRandomId(haveList.size(),allNeedNum,false,allshop,haveList);
		for(mysteriousshop43 smShopinit : haveList){
			xbean.smshopdata smShopData = xbean.Pod.newsmshopdata();
			smShopData.setId(smShopinit.getId());
			smShopData.setIsopen(0);
			List<Integer> costList = Misc.getRandomNotRepeatIntList(smShopinit.getMincost(), 
					smShopinit.getMaxcost(), smShopinit.getUnitcost(), 1);
			if(costList != null){
				smShopData.setPrice(costList.get(0));
			}else{
				continue;
			}
			prole.getProperties().getSmshop().put(smShopData.getId(), smShopData);
		}	
	}
	/**
	 * 获取随机商城id并放入到haveList
	 * @param min
	 * @param max
	 * @param isYb
	 * @param allshop
	 * @param haveList
	 */
	private void getRandomId(int min,int max,boolean isYb,List<mysteriousshop43> allshop,
			List<mysteriousshop43> haveList){
		for(int i = min; i < max; i++){
			List<Integer> allDrop = new ArrayList<Integer>();
			List<Integer> allProb = new ArrayList<Integer>();
			getDropList(allshop,allDrop,allProb,haveList,false);
			List<Integer> getList = DropManager.getInstance().getDropIdList(
					DropManager.getInstance().getDropMap(allDrop,allProb,0),1);
			if(getList != null){
				int smShopId = getList.get(0);
				for(mysteriousshop43 shopinit : allshop){
					if(shopinit.getId() == smShopId){
						haveList.add(shopinit);
						break;
					}
				}
			}
		}
	}
	/**
	 * 获取掉落id和几率list
	 * @param allList
	 * @param allDrop
	 * @param allProb
	 * @param haveList
	 */
	private void getDropList(List<mysteriousshop43> allList,List<Integer> allDrop,List<Integer> allProb,
			List<mysteriousshop43> haveList,boolean isYb){
		for(mysteriousshop43 smshop : allList){
			boolean isHave = false;
			for(mysteriousshop43 smhaveshop : haveList){
				if(smshop.getCommoditytype() == smhaveshop.getCommoditytype()){
					isHave = true;
					break;
				}
				if( isYb && smshop.costType != IDManager.YUANBAO){
					isHave = true;
					break;
				}
			}
			if(!isHave){
				allDrop.add(smshop.getId());
				allProb.add(smshop.sellweight);
			}
		}
	}
	/**
	 * 获取能够随机的神秘商店物品
	 * @param shopId
	 * @return
	 */
	private List<mysteriousshop43> getAllCanShop(int shopId){
		long now = GameTime.currentTimeMillis();
		List<mysteriousshop43> result = new LinkedList<mysteriousshop43>();
		TreeMap<Integer, mysteriousshop43> allmap = ConfigManager.getInstance().getConf(mysteriousshop43.class);
		for(Map.Entry<Integer, mysteriousshop43> entry : allmap.entrySet()){
			if(entry.getValue().getShopID() == shopId && 
				DateUtil.isRunning(now,entry.getValue().getOnShelve(),entry.getValue().getOffShelve(),"yyyyMMddHHmmss") ){
				result.add(entry.getValue());
			}
		}
		return result;
	}
	/**
	 * 购买神秘商店物品
	 * @param shopid
	 * @return
	 */
	public boolean buySmShop(int shopid){
		PropRole prole = PropRole.getPropRole(roleId, false);
		Map<Integer, xbean.smshopdata> smShopMap = prole.getProperties().getSmshop();
		if(smShopMap == null || smShopMap.isEmpty()){
			return false;
		}
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		if(now > prole.getProperties().getSmendtime()){
			 return false;
		}
		xbean.smshopdata smShopData = smShopMap.get(shopid);
		if(smShopData == null || smShopData.getIsopen() != 0){
			return false;
		}
		mysteriousshop43 smShopInit = ConfigManager.getInstance().getConf(mysteriousshop43.class).get(shopid);
		if(smShopInit == null){
			return false;
		}
		if(DropManager.getInstance().useDel(smShopInit.getCostType(), smShopData.getPrice(), 
				roleId, LogBehavior.BUYSMSHOPCOST)){
			DropManager.getInstance().dropAddByOther(smShopInit.getCommodityid(), smShopInit.getCommoditynum()
					, 0, 0, roleId, LogBehavior.BUYSMSHOP);
			smShopData.setIsopen(1);
			sendSBuySmShop(prole,shopid);
			/*
			boolean allBuy = true;
			for(Map.Entry<Integer, xbean.smshopdata> entry : smShopMap.entrySet()){
				if(entry.getValue().getIsopen() == 0){
					allBuy = false;
					break;
				}
			}
			if(allBuy){
				prole.getProperties().setBattlenum(0);
				prole.getProperties().setSmendtime(0);
			}
			*/
			ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.SHOP_SM_BUY, 1);

			return true;
		}
		return false;
	}
	/**
	 * 发送神秘商店购买成功
	 * @param prole
	 * @param shopid
	 */
	public void sendSBuySmShop(PropRole prole,int shopid){
		SBuySmShop snd = new SBuySmShop();
		snd.endtype = SBuySmShop.END_OK;
		snd.smshopid = shopid;
		snd.smshop.putAll(this.getSmShopMap(prole));
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	/**
	 * 开魔盒
	 * @param roleId
	 * @param place
	 * @return
	 */
	public boolean openMohe(int place){
		if(place < 1 || place > this.MOHE_NUM){
			return false;
		}
		PropRole prole = PropRole.getPropRole(roleId, false);
		Map<Integer,xbean.mohe> moheMap = prole.getProperties().getMoheshop();
		if(moheMap == null || moheMap.isEmpty()){
			return false;
		}
		
		List<Integer> allDrop = new ArrayList<Integer>();
		List<Integer> allProb = new ArrayList<Integer>();
		
		int noOpenNum = 0;
		for(Map.Entry<Integer, xbean.mohe> entry : moheMap.entrySet()){
			if(entry.getValue().getPlace() == place){
				return false;
			}
			if(entry.getValue().getPlace() == 0){
				noOpenNum++;
				bossbox25 bossboxinit = ConfigManager.getInstance().getConf(bossbox25.class).
										get(entry.getValue().getId());
				if(bossboxinit == null){
					return false;
				}
				allDrop.add(entry.getValue().getId());
				allProb.add(bossboxinit.getDropwight2());
			}
		}
		//验证开启消耗
		if( !openMoheCost(noOpenNum) ){
			return false;
		}

		List<Integer> getList = DropManager.getInstance().getDropIdList(
				DropManager.getInstance().getDropMap(allDrop,allProb,0),1);
		
		if( getList == null || getList.size() == 0 ){
			return false;
		}
		
		xbean.mohe mohedata = moheMap.get(getList.get(0));
		if(mohedata == null){
			return false;
		}
		bossbox25 bossboxinit = ConfigManager.getInstance().getConf(bossbox25.class).
				get(mohedata.getId());
		if(bossboxinit == null){
			return false;
		}
		DropManager.getInstance().dropAddByOther(bossboxinit.getRewardid(), bossboxinit.getRewardnum(), 
				-1, -1, roleId, LogBehavior.OPENMOHE);
		mohedata.setIsopen(1);
		mohedata.setPlace(place);
		
		sendSOpenMohe(prole,mohedata.getId());
		
		//判断是否全开，用于活跃度
		boolean isAllOpen = true;
		for(Map.Entry<Integer, xbean.mohe> entry : moheMap.entrySet()){
			if(entry.getValue().getIsopen() != 1){
				isAllOpen = false;
				break;
			}
		}
		if(isAllOpen){
			ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.MOHE_OPEN_ALL, 1);
		}
		
		return true;
	}
	/**
	 * 发送魔盒开启成功
	 * @param prole
	 * @param moheId
	 */
	public void sendSOpenMohe(PropRole prole,int moheId){
		SOpenMohe snd = new SOpenMohe();
		snd.endtype = SOpenMohe.END_OK;
		snd.moheid = moheId;
		snd.moheshop.putAll(this.getMoheMap(prole));
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	/**
	 * 开启魔盒消耗
	 * @param roleId
	 * @param noOpenNum
	 * @return
	 */
	public boolean openMoheCost(int noOpenNum){
		int costNum = 0;
		List<Integer> costNumlist = ParserString.parseString2Int(ConfigManager.getInstance().
				getConf(config10.class).get(1270).configvalue);
		if(costNumlist == null || costNumlist.size() < this.MOHE_NUM){
			return false;
		}
		switch(noOpenNum){
		case 1:
			costNum = costNumlist.get(2);
			break;
		case 2:
			costNum = costNumlist.get(1);
			break;
		case 3:
			costNum = costNumlist.get(0);
			break;
		default:
			return false;
		}
		if(costNum == 0)
			return true;
		try{
			int costid = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1269).configvalue);
			return DropManager.getInstance().useDel(costid, costNum, roleId, LogBehavior.OPENMOHECOST);
		}catch(Exception ex){
			ex.printStackTrace();
			return false;
		}
		
	}
	
	
	private int specialActivity(int exp)
	{
		long now = GameTime.currentTimeMillis();
		/*int nowday = DateUtil.getCurrentDay(now);
		int firststartday = DateUtil.getCurrentDay(ConfigManager.FIRST_START_TIME);
		if(nowday - firststartday == 1 || nowday - firststartday == 2)
			return 2*exp;*/
		if(DateUtil.getCurrentWeekDay(now) == 4)
			return 2*exp;
		return exp;
	}
	
	/**
	 * 刷星和激活新关卡
	 * @param xstage
	 * @param xbattle
	 * @param newstar
	 */
	public void refreshStageAndBattle(xbean.StageInfo xstage, xbean.StageBattleInfo xbattle,int newstar ){
		int oldstar = xbattle.getMaxstar();
		//刷星
		if(newstar > oldstar){
			xbattle.setMaxstar(newstar);
			sendSRefreshStage(xstage);
		}
		sendSRefreshStageBattle(xbattle,xstage.getId());
		//过关
		if(oldstar == 0 && newstar > 0){
			//激活新关卡
			onStageBattlePassed(xstage, xbattle);
		}
	}
	
	/**
	 * 根据关卡激活下一个关卡和副本
	 * @param xstage
	 * @param xbattle
	 */
	public void onStageBattlePassed(xbean.StageInfo xstage, xbean.StageBattleInfo xbattle)
	{
		List<Integer> openList = getOpenBattleId(xbattle.getId());
		for(Integer nextbattleId : openList){
			stage11 nextbattlecfg = ConfigManager.getInstance().getConf(stage11.class).get(nextbattleId);
			if(nextbattlecfg != null)
			{
				Integer nextbattlestageid = getStageNumByBattleId(nextbattleId);
				if(nextbattlestageid == null)
					continue;
				if(xstage.getId() == nextbattlestageid){
					if(xstage.getStagebattles().get(nextbattleId) != null){
						continue;
					}
					//激活下一个关卡
					xbean.StageBattleInfo xnextbattle = xbean.Pod.newStageBattleInfo();
					xnextbattle.setId(nextbattleId);
					xstage.getStagebattles().put(xnextbattle.getId(), xnextbattle);
					sendSRefreshStageBattle(xnextbattle,xstage.getId());
				}else{
					activeStage(nextbattlestageid);
					StageInfo nextxstage = xrole.getStages().get(nextbattlestageid);
					if(nextxstage != null){
						if(nextxstage.getStagebattles().get(nextbattleId) != null){
							continue;
						}
						xbean.StageBattleInfo xnextbattle = xbean.Pod.newStageBattleInfo();
						xnextbattle.setId(nextbattleId);
						nextxstage.getStagebattles().put(xnextbattle.getId(), xnextbattle);
						sendSRefreshStageBattle(xnextbattle,nextxstage.getId());
					}
				}
				
			}
		}		
		/*
		int nextbattleId = xbattle.getId()+1;
		SGuankaConfig nextbattlecfg = ConfigManager.getInstance().getConf(SGuankaConfig.class).get(nextbattleId);
		if(nextbattlecfg != null)
		{//激活下一个关卡
			xbean.StageBattleInfo xnextbattle = xbean.Pod.newStageBattleInfo();
			xnextbattle.setId(nextbattleId);
			xstage.getStagebattles().put(xnextbattle.getId(), xnextbattle);
			sendSRefreshStageBattle(xnextbattle);
		}
		else
		{//副本完成了，激活下一个副本
			onStageCompleted(xstage);
		}
		*/
	}
	
	/**
	 * 初始化关卡数据
	 * @param xstage
	 * @param xbattle
	 */
	public void onInitStage(int initId)
	{
		List<Integer> openList = getOpenBattleId(initId);
		for(Integer nextbattleId : openList){
			stage11 nextbattlecfg = ConfigManager.getInstance().getConf(stage11.class).get(nextbattleId);
			if(nextbattlecfg != null)
			{
				Integer nextbattlestageid = getStageNumByBattleId(nextbattleId);
				if(nextbattlestageid == null)
					continue;
				activeStage(nextbattlestageid);
				StageInfo nextxstage = xrole.getStages().get(nextbattlestageid);
				if(nextxstage != null){
					xbean.StageBattleInfo xnextbattle = nextxstage.getStagebattles().get(nextbattleId);
					if(xnextbattle == null){
						//激活下一个关卡
						xnextbattle = xbean.Pod.newStageBattleInfo();
						xnextbattle.setId(nextbattleId);
						nextxstage.getStagebattles().put(xnextbattle.getId(), xnextbattle);
						sendSRefreshStageBattle(xnextbattle,nextxstage.getId());
					}
				}				
			}
		}
	}
	
	/**
	 * 根据小关卡获取章节ID
	 * @param stageBattleId
	 * @return
	 */
	public Integer getStageNumByBattleId(int stageBattleId){
		java.util.TreeMap<Integer,chapterinfo23> fubenMap = (java.util.TreeMap<Integer, chapterinfo23>) ConfigManager.getInstance().getConf(chapterinfo23.class);
		for(Map.Entry<Integer, chapterinfo23> entry : fubenMap.entrySet() ){
			if(entry.getValue().stageID.contains(Integer.toString(stageBattleId))){
				return entry.getValue().getId();
			}
		}
		return null;
	}
	
	/**
	 * 根据关卡返回对应开启关卡
	 * @param stageBattleId
	 * @return
	 */
	public List<Integer> getOpenBattleId(int stageBattleId){
		List<Integer> openList = new ArrayList<Integer>();
		java.util.TreeMap<Integer,stage11> guankaMap = (java.util.TreeMap<Integer, stage11>) ConfigManager.getInstance().getConf(stage11.class);
		//GM命令
		if(stageBattleId == -99919){
			openList.addAll(guankaMap.keySet());
			return openList;
		}
		
		for(Map.Entry<Integer, stage11> entry : guankaMap.entrySet()){
			if(entry.getValue().getPremissionid() == stageBattleId){
				openList.add(entry.getValue().getId());
			}
		}
		return openList;
	}
	
	public void onStageCompleted(xbean.StageInfo xstage)
	{
		//TODO 一些处理
		activeStage(xstage.getId()+1);//激活下一个副本
	}
	
	public xbean.StageInfo getCurStage()
	{
		return xrole.getStages().get(getCurStageId());
	}
	
	public int getCurStageId()
	{
		int maxkey = 0 ;
		for(int key : xrole.getStages().keySet())
		{
			if(key > maxkey)
				maxkey = key;
		}
		return maxkey;
	}
	
	public xbean.StageBattleInfo getCurBattle()
	{
		xbean.StageInfo xstageinfo = getCurStage();
		int maxkey = 0 ;
		for(int key : xstageinfo.getStagebattles().keySet())
		{
			if(key > maxkey)
				maxkey = key;
		}

		return xstageinfo.getStagebattles().get(maxkey);
	}
	
	protected void activeStage(int stageId)
	{
		chapterinfo23 stagecfg = ConfigManager.getInstance().getConf(chapterinfo23.class).get(stageId);
		if(stagecfg == null)
			return;//没有下一个副本了
		if(xrole.getStages().containsKey(stageId))
			return;//已经开了，不要再重置
		xbean.StageInfo xnextstage = xbean.Pod.newStageInfo();
		xnextstage.setId(stageId);
		xrole.getStages().put(stageId, xnextstage);
		sendSRefreshStage(xnextstage);
		
		TanXianColumns txcol = TanXianColumns.getTanXianColumn(roleId, false);
		txcol.refreshTask(stageId,false);
		txcol.sSRefreshTanXian();
		/*
		if(stageId == 1){
		xbean.StageBattleInfo xfirstbattle = xbean.Pod.newStageBattleInfo();
		xfirstbattle.setId(100101);		//初始副本ID
		xnextstage.getStagebattles().put(xfirstbattle.getId(), xfirstbattle);
		sendSRefreshStageBattle(xfirstbattle,xnextstage.getId());
		}
		*/
	}
	
	/**
	 * gm命令：清空所有关卡次数
	 */
	public void gmClearBattleTime(){
		for(Map.Entry<Integer, StageInfo> entry : xrole.getStages().entrySet()){
			for(Map.Entry<Integer, StageBattleInfo> inentry : entry.getValue().getStagebattles().entrySet()){
				inentry.getValue().setFightnum(0);
				inentry.getValue().setAllfightnum(0);
			}
		}
		this.sendAllStages();
	}
	
	public void sendStageInfo(int stageId)
	{
		sendStageInfo(xrole.getStages().get(stageId));
	}
	
	public void sendStageInfo(xbean.StageInfo xstage)
	{
		if(xstage == null)
			return;
		chuhan.gsp.stage.StageInfo stageinfo = getStageInfo(xstage, true);
		SEnterStage snd = new SEnterStage();
		snd.stageid = Conv.toByte(stageinfo.id);
		snd.stagebattles.addAll(stageinfo.stagebattles);
		if(xdb.Transaction.current() != null)
			xdb.Procedure.psendWhileCommit(roleId, snd);
		else
			gnet.link.Onlines.getInstance().send(roleId, snd);
	}
	
	private chuhan.gsp.stage.StageInfo getStageInfo(xbean.StageInfo xstage, boolean includebatle)
	{
		int starsum = getStageStarSum(xstage);
		chuhan.gsp.stage.StageInfo stageinfo = new chuhan.gsp.stage.StageInfo();
		stageinfo.id = xstage.getId();
		stageinfo.starsum = Conv.toByte(starsum);
		stageinfo.rewardgot = Conv.toByte(xstage.getRewardgot());
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		List<xbean.StageBattleInfo> battlelist = new LinkedList<xbean.StageBattleInfo>();
		battlelist.addAll(xstage.getStagebattles().values());
		Collections.sort(battlelist,new Comparator<xbean.StageBattleInfo>() {
			@Override
			public int compare(StageBattleInfo arg0, StageBattleInfo arg1) {
				if(arg0.getId() > arg1.getId())
					return -1;
				else
					return 1;
			}
		});
		if(!includebatle)
			return stageinfo;
		for(xbean.StageBattleInfo xbattle : battlelist)
		{
			processStageBattleInfo(xbattle, now);
			stageinfo.stagebattles.add(new StageBattle(xbattle.getId(),Conv.toByte(xbattle.getMaxstar()),
					Conv.toShort(xbattle.getFightnum()),xbattle.getBuybattlenum(),0,0));
		}
		return stageinfo;
	}
	
	/**
	 * 获取关卡宝箱
	 * @param stageId
	 * @param boxnum
	 * @return
	 */
	public boolean getStageCompleteReward(int stageId, int boxnum){
/*		if(difficulttype < 1 || difficulttype > 3){
			Message.psendMsgNotify(roleId, 159);
			return false;
		}	*/		
		xbean.StageInfo xstage = xrole.getStages().get(stageId);
		if(xstage == null){
			Message.psendMsgNotify(roleId, 159);
			return false;
		}
		chapterinfo23 chap = ConfigManager.getInstance().getConf(chapterinfo23.class).get(xstage.getId());
		if(chap == null){
			Message.psendMsgNotify(roleId, 159);
			return false;
		}	
		if(isReward(boxnum,xstage.getRewardgot())){
			Message.psendMsgNotify(roleId, 159);
			return false;
		}
		if( !isPerfectComplete(xstage,chap,boxnum) ){
			Message.psendMsgNotify(roleId, 159);
			return false;
		}
		
		List<Integer> droplist = ParserString.parseString2Int(chap.getChapterDrop());
		if(droplist.size() <= boxnum)
			return false;
		int dropNum = droplist.get(boxnum);
		if(dropNum == -1)
			return false;
		
		List<Integer> result = DropManager.getInstance().drop(roleId, String.valueOf(dropNum), LogBehavior.STAGEBOX);
		/*
		if(boxnum == 1){
			xstage.setRewardgot(xstage.getRewardgot() + 1);
		}else if(boxnum == 2){
			xstage.setRewardgot(xstage.getRewardgot() + 10);
		}else if(boxnum == 3){
			xstage.setRewardgot(xstage.getRewardgot() + 100);
		}
		*/
		int add = 1;
		for(int i = 0;i<boxnum;i++){
			add = add * 10;
		}
		xstage.setRewardgot(xstage.getRewardgot() + add);
		sendSRefreshStage(xstage);
//		Message.psendMsgNotifyWhileCommit(roleId,92,10);
		
		SGetStageReward snd = new SGetStageReward();
		snd.endtype = SGetStageReward.END_OK;
		snd.buytype.addAll(result);
		snd.boxnum = (byte) boxnum;
		xdb.Procedure.psendWhileCommit(roleId, snd);
		return true;	
	}
	
	/**
	 * 判断是都已经领取
	 * @param difficulttype
	 * @param rewardnum
	 * @return
	 */
	public boolean isReward(int boxnum,int rewardnum){
		int reward = 0;
		for(int i = 0;i<boxnum;i++){
			rewardnum = rewardnum /10;
		}
		reward = rewardnum%10;
		return reward != 0;
		/*
		if(difficulttype == 1){
			reward = rewardnum % 10;
		}else if(difficulttype == 2){
			reward = rewardnum % 100 / 10;
		}else if(difficulttype == 3){
			reward = rewardnum / 100;
		}
		return reward != 0;
		*/
	}
	
	/**
	 * 是否完美的完成了
	 * @param xstage
	 * @param difficulttype
	 * @return
	 */
	public boolean isPerfectComplete(xbean.StageInfo xstage,chapterinfo23 chap,int boxnum){
		int startNum = 0;
		for(Map.Entry<Integer, xbean.StageBattleInfo> entry : xstage.getStagebattles().entrySet()){
			stage11 battlecfg = ConfigManager.getInstance().getConf(stage11.class).get(entry.getValue().getId());
			if(battlecfg == null)
				continue;
			startNum += entry.getValue().getMaxstar();
		}
		List<Integer> needstart = ParserString.parseString2Int(chap.getStarnum());
		if(needstart.size() <= boxnum)
			return false;
		int needNum = needstart.get(boxnum);
		if(needNum == -1)
			return false;
		return startNum >= needNum;
	}
	
	public boolean isPerfectComplete(xbean.StageInfo xstage)
	{
		int lastbattle = 0;
		for(Map.Entry<Integer, xbean.StageBattleInfo> entry : xstage.getStagebattles().entrySet())
		{
			if(entry.getKey() > lastbattle)
				lastbattle = entry.getKey();
			if(entry.getValue().getMaxstar() < 3)
				return false;
		}
		stage11 nextbattlecfg = ConfigManager.getInstance().getConf(stage11.class).get(lastbattle+1);
		if(nextbattlecfg == null)
			return true;
		else
			return false;
	}
	
	public void examStageCompleted(List<xbean.StageInfo> xstages)
	{
		if(xstages.isEmpty())
			return;
		xbean.StageInfo xstage = xstages.get(0);
		if(!isComplete(xstage))
			return;
		onStageCompleted(xstage);
	}
	
	public boolean isComplete(xbean.StageInfo xstage)
	{
		int lastbattle = 0;
		for(int battleID : xstage.getStagebattles().keySet())
		{
			if(battleID > lastbattle)
				lastbattle = battleID;
		}
		xbean.StageBattleInfo stagebattle =  xstage.getStagebattles().get(lastbattle);
		if(stagebattle == null || stagebattle.getMaxstar() <= 0)
			return false;
		stage11 nextbattlecfg = ConfigManager.getInstance().getConf(stage11.class).get(lastbattle+1);
		if(nextbattlecfg == null)
			return true;
		else
			return false;
	}
	/**
	 * 战斗次数重置判断
	 * @param xbattle
	 * @param now
	 */
	private void processStageBattleInfo(xbean.StageBattleInfo xbattle, long now)
	{
//		if(xbattle.getFightnum() == 0)
//			return;
		if(DateUtil.inTheSameDay(xbattle.getLastfighttime(),now)){
			return;
		}else{
			xbattle.setFightnum(0);
			xbattle.setSweepnum(0);
		}
		
		if(DateUtil.inTheSameDay(xbattle.getBuybattlelasttime(),now)){
			return;
		}else{
			xbattle.setBuybattlenum(0);
		}
		
	}
	
	private void updateStateBattleInfo(xbean.StageBattleInfo xbattle, long now,boolean isSweep){
		xbattle.setFightnum(xbattle.getFightnum() + 1);
		xbattle.setLastfighttime(now);
		xbattle.setAllfightnum(xbattle.getAllfightnum() + 1);
		if(isSweep){
			xbattle.setSweepnum(xbattle.getSweepnum() + 1);
		}
	}
	
	public void sendAllStages()
	{
		SSendStages snd = new SSendStages();
		List<xbean.StageInfo> xstages = new LinkedList<xbean.StageInfo>();
		xstages.addAll(xrole.getStages().values());
		Collections.sort(xstages, new Comparator<xbean.StageInfo>() {
			@Override
			public int compare(StageInfo arg0, StageInfo arg1) {
				return -(arg0.getId() - arg1.getId());
			}
		});
		int i = 1;
		for(xbean.StageInfo xstage : xstages)
		{
			snd.stages.add(getStageInfo(xstage, true));
			i++;
		}
		if(snd.stages.isEmpty())
			return;
		if(xdb.Transaction.current() != null)
			xdb.Procedure.psendWhileCommit(roleId, snd);
		else
			gnet.link.Onlines.getInstance().send(roleId, snd);
//		examStageCompleted(xstages);
	}
	
	public int getStageStarSum(xbean.StageInfo xstage)
	{
		int sum = 0;
		for(xbean.StageBattleInfo xbattle : xstage.getStagebattles().values())
		{
			sum += xbattle.getMaxstar();
		}
		return sum;
	}
	
	public void sendSRefreshStage(xbean.StageInfo xstage)
	{
		SRefreshStage snd = new SRefreshStage(xstage.getId(), Conv.toByte(getStageStarSum(xstage)), Conv.toByte(xstage.getRewardgot()));
		if(xdb.Transaction.current() != null)
			xdb.Procedure.psendWhileCommit(roleId, snd);
		else
			gnet.link.Onlines.getInstance().send(roleId, snd);
	}
	
	public void sendSRefreshStageBattle(xbean.StageBattleInfo xbattle,int stageId)
	{
		SRefreshStageBattle snd = new SRefreshStageBattle();
		snd.stageid = stageId;
		snd.stagebattle.id = xbattle.getId();
		snd.stagebattle.maxstar = Conv.toByte(xbattle.getMaxstar());
		snd.stagebattle.fightnum = Conv.toShort(xbattle.getFightnum());
		snd.stagebattle.buybattlenum = xbattle.getBuybattlenum();
		snd.stagebattle.sweepnum = xbattle.getSweepnum();
		if(xdb.Transaction.current() != null)
			xdb.Procedure.psendWhileCommit(roleId, snd);
		else
			gnet.link.Onlines.getInstance().send(roleId, snd);
	}
	
}
