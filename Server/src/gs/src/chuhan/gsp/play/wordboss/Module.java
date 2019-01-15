package chuhan.gsp.play.wordboss;

import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;

import chuhan.gsp.defenceInfo;
import chuhan.gsp.fightInfo;
import chuhan.gsp.attr.config10;
import chuhan.gsp.award.DropManager;
import chuhan.gsp.award.IDManager;
import chuhan.gsp.battle.BattleManager;
import chuhan.gsp.game.legendexcharge57;
import chuhan.gsp.hero.TroopColumn;
import chuhan.gsp.log.LogBehavior;
import chuhan.gsp.log.Logger;
import chuhan.gsp.mail.MailColumn;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.ModuleInterface;
import chuhan.gsp.main.ModuleManager;
import chuhan.gsp.main.ReloadResult;
import chuhan.gsp.play.activity.ActivityManager;
import chuhan.gsp.play.huoyuedu.HuoyueColumns;
import chuhan.gsp.play.ranking.bossRanking;
import chuhan.gsp.task.stage11;
import chuhan.gsp.util.DateUtil;
import chuhan.gsp.util.Misc;
import chuhan.gsp.util.ParserString;



public class Module implements ModuleInterface{
	@Override
	public void exit() {
		// TODO Auto-generated method stub
		
	}
	@Override
	public void init() throws Exception {
		// TODO Auto-generated method stub
		xbean.boss xcol = getxboss(true);
		xbosscol.copyFrom(xcol);
		initData();
	}
	@Override
	public ReloadResult reload() throws Exception {
		// TODO Auto-generated method stub
		return null;
	}
	
	public static Logger logger = Logger.getLogger(Module.class);
//	static private Module instance = null;
	
	private static final long BOSS_ALL_TIME = DateUtil.minuteMills * 20;	//boss战总时间
	
	public static bossNowData xbosscol = new bossNowData();
	
	public Module(){}
	public static Module getInstance() {
		return (Module)ModuleManager.getInstance().getModuleByName("wordboss");
	}
	
	public static xbean.boss getxboss(boolean isReadOnly){
		xbean.boss xcol = null;
		if(isReadOnly){
			xcol = xtable.Bossdata.select(1);
		}else{
			xcol = xtable.Bossdata.get(1);
		}
		if(xcol == null){
			if(!isReadOnly){
				xcol = xbean.Pod.newboss();
				xtable.Bossdata.insert(1, xcol);
			}else{
				xcol = xbean.Pod.newbossData();
			}
		}
		return xcol;
	}
	

	
	/**
	 * 保存数据
	 */
	private void setxbeanboxx(){
		xbean.boss xcol = getxboss(false);
		xcol.setLasthpall(xbosscol.lasthpall);
		xcol.setLastiskill(xbosscol.lastiskill);
		xcol.setLastkillnum(xbosscol.lastkillnum);
		xcol.setNewhpall(xbosscol.newhpall);
		xcol.setNowhp(xbosscol.nowhp);
		xcol.setBossid1(xbosscol.bossid1);
		xcol.setBossid2(xbosscol.bossid2);
		xcol.setBossid3(xbosscol.bossid3);
		xcol.setBossid4(xbosscol.bossid4);
		xcol.setBossiskill(xbosscol.bossiskill);
		xcol.setBoss1killname(xbosscol.boss1killname);
		xcol.setBoss2killname(xbosscol.boss2killname);
		xcol.setTime(xbosscol.time);	
	}
	/**
	 * 初始化数据
	 */
	public void initData(){
		logger.infoWhileCommit("WordBossManager::initData");
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		if( !DateUtil.inTheSameDay(now, xbosscol.getTime()) ){
			if( xbosscol.getTime() == 0){
				this.setNewHp(now);
			}
			this.setBossId();
			xbosscol.setTime(now);
//			setxbeanboxx();		//暂时不保存，启动时xdb写入有问题
		}
	}
	
	/**
	 * 更换boss相关数据
	 */
	private void setBossId(){
		TreeMap<Integer, stage11> initMap = ConfigManager.getInstance().getConf(stage11.class);
		stage11 stageinit9 = null;
		stage11 stageinit10 = null;
		for( Map.Entry<Integer, stage11> entry : initMap.entrySet() ){
			if(entry.getValue().getStagetype() == 9){
				stageinit9 = entry.getValue();
			}
			if(entry.getValue().getStagetype() == 10){
				stageinit10 = entry.getValue();
			}
		}
		if(stageinit9 != null){
			List<Integer> mgroupList = ParserString.parseString2Int(stageinit9.getMonstergroup());
			LinkedList<Integer> mList = BattleManager.getInstance().getMonsterList(mgroupList);
			if(mList != null){
				xbosscol.setBossid1(mList.get(0));				
			}
			do{
				mList = BattleManager.getInstance().getMonsterList(mgroupList);
			}
			while( xbosscol.getBossid1() == mList.get(0) );
			xbosscol.setBossid3(mList.get(0));
		}
		if(stageinit10 != null){
			List<Integer> mgroupList = ParserString.parseString2Int(stageinit10.getMonstergroup());
			LinkedList<Integer> mList = BattleManager.getInstance().getMonsterList(mgroupList);
			if(mList != null){
				xbosscol.setBossid2(mList.get(0));
				xbosscol.setBossid4(mList.get(0));
			}
		}
		xbosscol.setBoss1killname("");
		xbosscol.setBoss2killname("");
		xbosscol.setBossiskill(0);
	}
	
	/**
	 * 更新新血量
	 */
	public void setNewHp(long now){
		xbosscol.setLasthpall(xbosscol.getNewhpall());
		int newHp = 0;
		if( xbosscol.getLastiskill() == 0 ){
			xbosscol.setLastkillnum(xbosscol.getNewhpall() - xbosscol.getNowhp());
			double b = Double.parseDouble(ConfigManager.getInstance().getConf(config10.class).get(1303).configvalue);
			double c = Double.parseDouble(ConfigManager.getInstance().getConf(config10.class).get(1304).configvalue);
			double d = Double.parseDouble(ConfigManager.getInstance().getConf(config10.class).get(1305).configvalue);
			newHp = (int) (( (double)xbosscol.getLasthpall() * b + (double)xbosscol.getLastkillnum() * c) / d);
			int bossId = this.getOpenBossId(now - 60 * 7 * 1000);
			if(bossId != 0){
				ActivityManager.addMsgNotice(0,bossId,ActivityManager.BOSSTAOZOU,"");
			}
		}else{
			if( xbosscol.getLasthpall() != 0 && xbosscol.getLastkillnum() != 0 ){
				double a = Double.parseDouble(ConfigManager.getInstance().getConf(config10.class).get(1302).configvalue);
				newHp = (int) ((double)BOSS_ALL_TIME / (double)xbosscol.getLastkillnum() * a * (double)xbosscol.getLasthpall());
			}
		}
		if(newHp == 0){
			newHp = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1301).configvalue);
		}
		xbosscol.setNewhpall(newHp);
		xbosscol.setNowhp(newHp);
		xbosscol.setLastiskill(0);
	}
	
	/**
	 * 获取人物boss数据
	 * @param roleId
	 * @param readonly
	 * @return
	 */
	public static xbean.bossrole getxbossrole(long roleId, boolean readonly){
		if(xtable.Properties.select(roleId) == null)
			throw new IllegalArgumentException("构造xbean.bossrole时，角色 "+roleId+" 不存在。");
		xbean.bossrole xbossrole;
		if(readonly){
			xbossrole = xtable.Bossrolelist.select(roleId);
		}else{
			xbossrole = xtable.Bossrolelist.get(roleId);
		}
		if(xbossrole == null){
			if(readonly)
				xbossrole = xbean.Pod.newbossroleData();
			else{
				xbossrole = xbean.Pod.newbossrole();
				xtable.Bossrolelist.insert(roleId, xbossrole);
			}
		}
		
		return xbossrole;
	}
	
	/**
	 * 初始化boss商城数据
	 * @param xbossrole
	 * @param now
	 */
	private void initBossShop(xbean.bossrole xbossrole,long now){
		if( !DateUtil.inTheSameDay(now, xbossrole.getBshop().getTime()) ){
			xbossrole.getBshop().getShoplist().clear();
			xbossrole.getBshop().setTime(now);
			xbossrole.getBshop().setHunternum(0);
			Map<Integer,legendexcharge57> initShopMap = ConfigManager.getInstance().getConf(legendexcharge57.class);
			for(Map.Entry<Integer,legendexcharge57> entry : initShopMap.entrySet()){
				int probability = entry.getValue().getProbability();
				if(probability == -1){
					xbossrole.getBshop().getShoplist().add(entry.getValue().getId());
					continue;
				}else{
					int random = Misc.getRandomBetween(1, 10000);
					if(random < probability){
						xbossrole.getBshop().getShoplist().add(entry.getValue().getId());
					}
				}
			}
		}
	}
	
	/**
	 * 计算伤害
	 * @param fInfo
	 * @param bossHp
	 * @return
	 */
	public long attackNum(fightInfo fInfo,long bossHp){
		long hpKill = 0;
		for(defenceInfo dinfo : fInfo.m_defenceinfo){
			if( BattleManager.isHeroAttacker(dinfo.m_defencer)){
				long num = bossHp - dinfo.m_remainhp;
				if( num > 0 ){
					hpKill += num;
					bossHp = dinfo.m_remainhp;
				}
			}
		}
		return hpKill;
	}
	/**
	 * boss减血，线程安全
	 * @param hpKill
	 * @param roleId
	 * @param bossId
	 * @return
	 */
	private synchronized long bossCostHp(long hpKill,long roleId,int bossId,long now){
		if( Module.xbosscol.nowhp <=0 ){
			return -1;
		}
		long nowhp = Module.xbosscol.nowhp - hpKill;
		if( nowhp > 0 ){
			Module.xbosscol.nowhp = nowhp;
			return nowhp;
		}else{
			chuhan.gsp.attr.PropRole prole = chuhan.gsp.attr.PropRole.getPropRole(roleId, true);
			Module.xbosscol.nowhp = 0;
			if(bossId == 2){
				Module.xbosscol.bossiskill = Module.xbosscol.bossiskill + 1;
				Module.xbosscol.boss1killname = prole.getProperties().getRolename();
			}else if(bossId == 4){
				Module.xbosscol.bossiskill = Module.xbosscol.bossiskill + 10;
				Module.xbosscol.boss2killname = prole.getProperties().getRolename();
			}
			long time = now - this.getBeginTime(bossId, now);
			Module.xbosscol.setLastkillnum(time / 1000);
			return 0;
		}
	}
	/**
	 * bossshop购买入口
	 * @param roleId
	 * @param shopId
	 * @return
	 */
	public boolean buyBossShopEntry(long roleId, int shopId){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		xbean.bossrole xbossrole = Module.getxbossrole(roleId, false);
		this.initBossShop(xbossrole, now);
		if( !xbossrole.getBshop().getShoplist().contains(shopId) ){
			return false;
		}
		legendexcharge57 initShopMap = ConfigManager.getInstance().getConf(legendexcharge57.class).get(shopId);
		//XX之血，如果有了，就不让买了
		if(initShopMap.getType() == 1){
			xbean.heroclone xheroClone = ActivityManager.getxHeroClone(roleId, true);
			if( xheroClone.getClonelist().contains(initShopMap.getShow()) ){
				return false;
			}
		}else if(initShopMap.getType() == 4){		//总共计算购买次数
			int maxNum = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1323).configvalue);
			if(xbossrole.getBshop().getHunternum() >= maxNum){
				return false;
			}
			xbossrole.getBshop().setHunternum(xbossrole.getBshop().getHunternum() + 1);
		}
		
		int cszs = xbossrole.getChuanshuozs() - initShopMap.getCost();
		if(!DropManager.getInstance().useDel(IDManager.CHUANSHUOZS, initShopMap.getCost(), roleId, LogBehavior.BOSSSHOPBUYCOST)){
			return false;
		}
//		if(cszs < 0){
//			return false;
//		}
//		xbossrole.setChuanshuozs(cszs);
		List<Integer> drop = DropManager.getInstance().drop(roleId, String.valueOf(initShopMap.getReward()), LogBehavior.BOSSSHOPBUY);
		this.sendSBuyBossShop(roleId, shopId, xbossrole.getBshop().getHunternum(), cszs, drop);
		return true;
	}
	
	/**
	 * boss战斗结束结算
	 * @param fightinfolist
	 * @param roleId
	 * @return
	 */
	public boolean bossPass(java.util.LinkedList<fightInfo> fightinfolist,long roleId){
		long hpKill = 0;
		xbean.bossrole bossrole = Module.getxbossrole(roleId, false);
		long bossHp = bossrole.getBossnowhp();
		logger.debug("----------boss:"+fightinfolist.size()+"----------");
		for(fightInfo fInfo: fightinfolist){
			if(BattleManager.isHeroAttacker(fInfo.m_attacker)){
				long num = attackNum(fInfo,bossHp);
//				if(num > 10000000){
//					num = 0;
//				}
				hpKill += num;
				bossHp -= num;
			}
		}
		Map<Integer,Integer> addMap = new HashMap<Integer,Integer>();
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		if( this.isInOpenTime(bossrole.getKillboss(), bossrole.getTime()) != -1 &&
				DateUtil.inTheSameDay(now, bossrole.getTime()) && 
				now - this.getEndTime(bossrole.getKillboss(), now) <= 5*DateUtil.minuteMills){
			if( bossrole.getKillboss() == 1 || bossrole.getKillboss() == 3 ){
				double a = Double.parseDouble(ConfigManager.getInstance().getConf(config10.class).get(1306).configvalue);
				double b = Double.parseDouble(ConfigManager.getInstance().getConf(config10.class).get(1307).configvalue);
				double c = Double.parseDouble(ConfigManager.getInstance().getConf(config10.class).get(1308).configvalue);
				double d = Double.parseDouble(ConfigManager.getInstance().getConf(config10.class).get(1309).configvalue);
				int swzl = (int) (a*(double)hpKill/(b+(double)hpKill));
				int gold = (int) (c*(double)hpKill/(d+(double)hpKill));
				addMap.put(IDManager.SHOUWANGZL, swzl);
				addMap.put(IDManager.GOLD, gold);
				bossrole.setShouwangzl(bossrole.getShouwangzl() + swzl);
				DropManager.getInstance().dropAddByOther(IDManager.GOLD, gold, 0, 0, roleId, LogBehavior.BOSSPASS);
				bossrole.setKillhpall(bossrole.getKillhpall() + hpKill);
				bossrole.setTime(now);
				this.sendSBossPass(bossrole, roleId, bossrole.getKillboss(), now, addMap,SBossPass.END_OK,"");
				return true;
			}else{
				double a = Double.parseDouble(ConfigManager.getInstance().getConf(config10.class).get(1310).configvalue);
				double b = Double.parseDouble(ConfigManager.getInstance().getConf(config10.class).get(1311).configvalue);
				double c = Double.parseDouble(ConfigManager.getInstance().getConf(config10.class).get(1312).configvalue);
				double d = Double.parseDouble(ConfigManager.getInstance().getConf(config10.class).get(1313).configvalue);
				double e = Double.parseDouble(ConfigManager.getInstance().getConf(config10.class).get(1314).configvalue);
				double f = Double.parseDouble(ConfigManager.getInstance().getConf(config10.class).get(1315).configvalue);
				int cszs = (int) (a*(double)hpKill/(b+(double)hpKill)) + (int)( bossrole.getTime() % 2);
				int sznum = (int) (c*(double)hpKill/(d+(double)hpKill)) + (int)( now % 2);
				int exp = (int) (e*(double)hpKill/(f+(double)hpKill));
				addMap.put(IDManager.CHUANSHUOZS, cszs);
				addMap.put(1402020036, sznum);
				addMap.put(IDManager.EXPJIEJING, exp);
				bossrole.setChuanshuozs(bossrole.getChuanshuozs() + cszs);
				DropManager.getInstance().dropAddByOther(1402020036, sznum, 0, 0, roleId, LogBehavior.BOSSPASS);
				DropManager.getInstance().dropAddByOther(IDManager.EXPJIEJING, exp, 0, 0, roleId, LogBehavior.BOSSPASS);
				bossrole.setTime(now);
				int endType = SBossPass.END_OK;
				String killName = "";
				long nextHp = bossCostHp(hpKill,roleId,bossrole.getKillboss(),now);
				if(nextHp >= 0){
					bossrole.setKillhpall(bossrole.getKillhpall() + hpKill);
					if(nextHp == 0){
						endType = SBossPass.END_KILLBOSS;
						int yb = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1316).configvalue);
						int cszsadd = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1317).configvalue);
						bossrole.setChuanshuozs(bossrole.getChuanshuozs() + cszsadd);
						//发送奖励邮件
						MailColumn col = MailColumn.getMailColumn(roleId, false);
						col.addMail(col.createMail("mail_tips1","mail_content25","mail_content29", null,
								IDManager.YUANBAO,yb, now+MailColumn.DEFAULT_TIME,null),false);
//						DropManager.getInstance().dropAddByOther(IDManager.YUANBAO, yb, 0, 0, roleId, "bossadd");
						addMap.put(IDManager.CHUANSHUOZS, cszs + cszsadd);
						addMap.put(IDManager.YUANBAO, yb);
						//发送击杀跑马灯
						int bossId = this.getOpenBossId(now);
						if(bossId != 0){
							ActivityManager.addMsgNotice(roleId,bossId,ActivityManager.BOSSKILL,"");
						}
						xbosscol.setLastiskill(1);
					}
					this.setxbeanboxx();
					if(bossrole.getKillboss() == 2 || bossrole.getKillboss() == 4){
						//加入到排行榜数据
						bossRanking.getInstance().addInRank(roleId, bossrole.getKillhpall(), bossrole.getKillboss(), now);
					}
				}else{
					endType = SBossPass.END_ISKILL;
					if(bossrole.getKillboss() == 2){
						killName = Module.xbosscol.boss1killname;
					}else{
						killName = Module.xbosscol.boss2killname;
					}
				}
				this.sendSBossPass(bossrole, roleId, bossrole.getKillboss(), now, addMap,endType,killName);
				return true;
			}	
		}
		return false;
	}
	
	/**
	 * 购买祝福
	 * @param roleId
	 * @param bossId
	 * @return
	 */
	public boolean bossBuyZhufuEntry(long roleId,int bossId){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		xbean.bossrole bossrole = Module.getxbossrole(roleId, false);
		if( isInOpenTime(bossId,now) == -1 ){
			return false;
		}
		this.sRefreshMyData(bossrole, now, bossId);
		List<Integer> initList = ParserString.parseString2Int(ConfigManager.getInstance().getConf(config10.class).
				get(1296).configvalue);
		if( initList == null || initList.size() <= bossrole.getZhufunum() ){
			return false;
		}
		int cost = initList.get(bossrole.getZhufunum());
		int end = bossrole.getShouwangzl() - cost;
		if( end < 0 ){
			return false;
		}
		bossrole.setShouwangzl(end);
		bossrole.setZhufunum(bossrole.getZhufunum() + 1);
		this.sendSBossBuyZhufu(roleId, bossrole.getZhufunum(), end,bossId);
		return true;
	}
	/**
	 * 购买守望之灵入口
	 * @param roleId
	 * @param num
	 * @return
	 */
	public boolean buyShouwangzlEntry(long roleId,int num){
		if(num < 0){
			return false;
		}
		int cost = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1300).configvalue);
		int costall = cost * num;
		if( DropManager.getInstance().useDel(IDManager.YUANBAO, costall, roleId, LogBehavior.BOSSBUYSWZLCOST) ){
			xbean.bossrole bossrole = Module.getxbossrole(roleId, false);
			bossrole.setShouwangzl(bossrole.getShouwangzl() + num);
			this.sendSBuyShouwangzl(roleId, bossrole.getShouwangzl());
			return true;
		}
		return false;
	}
	
	/**
	 * 发送战斗开始数据入口
	 * @param roleId
	 * @param bossId
	 * @param troopId
	 * @return
	 */
	public boolean beginBossEntry(long roleId,int bossId,int troopId,int cost){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		if(isInOpenTime(bossId,now) == -1 || Module.xbosscol.getNowhp() <= 0){
			return false;
		}
		TroopColumn troopcol = TroopColumn.getTroopColumn(roleId, false);
		java.util.HashMap<Integer,Integer> map = troopcol.getHkeyLolistByTrid(troopId) ;
		if(map == null || map.size() == 0){
			return false;
		}
		
		xbean.bossrole bossrole = Module.getxbossrole(roleId, false);
		if(getCdTime(bossId)*1000 > now - bossrole.getTime()){
			if( cost == 1 && (bossId == 2 || bossId == 4) ){
				Integer costyb = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1295).configvalue);
				if( !DropManager.getInstance().useDel(IDManager.YUANBAO, costyb, roleId, 
						LogBehavior.BEGINBOSSCOST ) ){
					return false;
				}
			}else{
				return false;
			}
		}
		ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.WORLD_BOSS, 1);
		this.sRefreshMyData(bossrole, now, bossId);
		bossrole.setTime(now);
		this.sendSBeginBoss(bossrole, roleId, bossId, troopId);
		return true;
	}
	/**
	 * 发送战斗开始数据
	 * @param bossrole
	 * @param roleId
	 * @param bossId
	 * @param troopid
	 */
	public void sendSBeginBoss(xbean.bossrole bossrole,long roleId,int bossId,int troopid){
		SBeginBoss snd = new SBeginBoss();
		snd.result = SBeginBoss.END_OK;
		TroopColumn troopcol = TroopColumn.getTroopColumn(roleId, false);
		snd.battleinfo.useherokeylist.putAll(troopcol.getHkeyLolistByTrid(troopid));
		snd.battleinfo.trooptype = troopcol.getTroopTypeByTrid(troopid);
		snd.battleinfo.monstertrooptype = 1;
		snd.battleinfo.zhufunum = bossrole.getZhufunum();
		snd.battleinfo.bossnowhp = bossrole.getBossnowhp();
		snd.battleinfo.bossid = bossId;
		switch(bossId){
		case 1:
			snd.battleinfo.battleid = 1340410000;
			snd.battleinfo.monstergroup.add(Module.xbosscol.bossid1);
			break;
		case 2:
			snd.battleinfo.battleid = 1340420000;
			snd.battleinfo.monstergroup.add(Module.xbosscol.bossid2);
			break;
		case 3:
			snd.battleinfo.battleid = 1340410000;
			snd.battleinfo.monstergroup.add(Module.xbosscol.bossid3);
			break;
		case 4:
			snd.battleinfo.battleid = 1340420000;
			snd.battleinfo.monstergroup.add(Module.xbosscol.bossid4);
			break;
		}
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	
	/**
	 * 获取本人boss战斗数据入口
	 * @param roleId
	 * @param bossId
	 */
	public void getMyWordBossEntry(long roleId,int bossId){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		xbean.bossrole bossrole = Module.getxbossrole(roleId, false);
		if(isInOpenTime(bossId,now) == -1){
			sendSGetMyWordBoss(bossrole, roleId, bossId,now);
			return;
		}
//		xbean.bossrole bossrole = this.getxbossrole(roleId, false);
		this.sRefreshMyData(bossrole, now, bossId);
		this.sendSGetMyWordBoss(bossrole, roleId, bossId,now);
	}
	
	/**
	 * 刷新个人数据
	 * @param bossrole
	 * @param now
	 * @param bossId
	 */
	private void sRefreshMyData(xbean.bossrole bossrole,long now,int bossId){
		if( !DateUtil.inTheSameDay(now, bossrole.getTime()) || isInOpenTime(bossId,bossrole.getTime()) == -1 || 
				bossrole.getKillboss() != bossId){
			//不是同一天，或者为守门者时，祝福次数归0
			if( !DateUtil.inTheSameDay(now, bossrole.getTime()) || bossId == 1 || bossId == 3 ){
				bossrole.setZhufunum(0);
			}else if( (bossId == 2 && isInOpenTime(1,bossrole.getTime()) == -1) ||
						(bossId == 4 && isInOpenTime(3,bossrole.getTime()) == -1) ){ 	//或者上一次战斗不是本次的守门战斗
				bossrole.setZhufunum(0);
			}
			bossrole.setKillboss(bossId);
			bossrole.setKillhpall(0);
//			bossrole.setTime(now);
		}
		bossrole.setBossnowhp(Module.xbosscol.getNowhp());
	}
	
	/**
	 * 发送战斗结束消息
	 * @param bossrole
	 * @param roleId
	 * @param bossId
	 * @param now
	 * @param drop
	 */
	public void sendSBossPass(xbean.bossrole bossrole,long roleId,int bossId,long now,Map<Integer,Integer> drop,int endtype,
			String killName){
		SBossPass snd = new SBossPass();
		snd.result = endtype;
		snd.bossid = bossId;
		snd.mywordboss = this.getBossRole(bossrole, now, bossId);
		snd.dropmap.putAll(drop);
		snd.bosskillname = killName;
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	
	/**
	 * 获取本人boss信息
	 * @param bossrole
	 * @param roleId
	 * @param bossId
	 */
	public void sendSGetMyWordBoss(xbean.bossrole bossrole,long roleId,int bossId,long now){
		SGetMyWordBoss snd = new SGetMyWordBoss();
		snd.bossid = bossId;
		
		snd.mywordboss = this.getBossRole(bossrole, now, bossId);
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	/**
	 * 转换成bossrole消息数据结构
	 * @param bossrole
	 * @param now
	 * @param bossId
	 * @return
	 */
	public bossrole getBossRole(xbean.bossrole bossrole,long now,int bossId){
		bossrole mywordboss = new bossrole();
		mywordboss.bosshpall = xbosscol.getNewhpall();
		mywordboss.killhpall = bossrole.getKillhpall();
		mywordboss.bossnowhp = bossrole.getBossnowhp();
		mywordboss.zhufunum = bossrole.getZhufunum();
		mywordboss.shouwangzl = bossrole.getShouwangzl();
		mywordboss.chuanshuozs = bossrole.getChuanshuozs();
		for(int i = 1;i<=4;i++){
			int time = this.isInOpenTime(i, now);
			if(time != -1){
				mywordboss.openboss = i;
				mywordboss.openendtime = time;
				break;
			}
		}
		int initcdTime = this.getCdTime(bossId);
		int cdTime = (int) (initcdTime - (now - bossrole.getTime())/1000);
		if( cdTime > 0 && bossrole.getKillboss() == bossId){	//不同boss没有CD
			//当前CD和BOSS结束时间取最短的
			int endtime = (int) (this.getEndTime(bossId, now) - now )/1000;
			if( cdTime > endtime ){
				cdTime = endtime;
			}
			mywordboss.nextintime = cdTime;
		}else{
			mywordboss.nextintime = 0;
		}
		return mywordboss;
	}
	/**
	 * 发送世界boss信息
	 * @param roleId
	 */
	public void sendSGetWordBoss(long roleId){
		this.initData();
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		SGetWordBoss snd = new SGetWordBoss();
		snd.wordboss.bossid1 = xbosscol.getBossid1();
		snd.wordboss.bossid2 = xbosscol.getBossid2();
		snd.wordboss.bossid3 = xbosscol.getBossid3();
		snd.wordboss.bossid4 = xbosscol.getBossid4();
		snd.wordboss.bossiskill = xbosscol.getBossiskill();
		snd.wordboss.boss1killname = xbosscol.getBoss1killname();
		snd.wordboss.boss2killname = xbosscol.getBoss2killname();
		for(int i = 1;i<=4;i++){
			int time = this.isInOpenTime(i, now);
			if(time != -1){
				snd.wordboss.openboss = i;
				snd.wordboss.openendtime = time;
				break;
			}
		}
		xbean.bossrole bossrole = Module.getxbossrole(roleId, true);
		snd.wordboss.shouwangzl = bossrole.getShouwangzl();
		snd.wordboss.chuanshuozs = bossrole.getChuanshuozs();
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	/**
	 * 世界boss开启跑马灯消息
	 * @param now
	 */
	public void sendBossOpenMsg(long now){
		int bossid = getOpenBossId(now);
		if(bossid != 0){
			ActivityManager.addMsgNotice(0,bossid,ActivityManager.BOSSOPEN,"");
		}
	}
	
	/**
	 * 通过时间得知开启的bossID
	 * @param now
	 * @return
	 */
	public int getOpenBossId(long now){
		int bossid = 0;
		for(int i = 1;i<=4;i++){
			int time = this.isInOpenTime(i, now);
			if(time != -1){
				switch(i){
				case 1:
					bossid = xbosscol.getBossid1();
					break;
				case 2:
					bossid = xbosscol.getBossid2();
					break;
				case 3:
					bossid = xbosscol.getBossid3();
					break;
				case 4:
					bossid = xbosscol.getBossid4();
					break;
				}
				break;
			}
		}
		return bossid;
	}
	
	/**
	 * 是否开放时间
	 * @param bossId
	 * @param now
	 * @return
	 */
	public int isInOpenTime(int bossId,long now){
		try{
			List<String> timeListstr = null;
			switch (bossId) {
			case 1:
				timeListstr = ParserString.parseString(ConfigManager.getInstance()
								.getConf(config10.class).get(1291).configvalue);
				return DateUtil.isRunningOnday(now, timeListstr.get(0));
			case 2:
				timeListstr = ParserString.parseString(ConfigManager.getInstance()
								.getConf(config10.class).get(1292).configvalue);
				return DateUtil.isRunningOnday(now, timeListstr.get(0));
			case 3:
				timeListstr = ParserString.parseString(ConfigManager.getInstance()
								.getConf(config10.class).get(1291).configvalue);
				return DateUtil.isRunningOnday(now, timeListstr.get(1));
			case 4:
				timeListstr = ParserString.parseString(ConfigManager.getInstance()
								.getConf(config10.class).get(1292).configvalue);
				return DateUtil.isRunningOnday(now, timeListstr.get(1));
			}
			return -1;
		}catch(Exception e){
			e.printStackTrace();
			return -1;
		}
	}
	/**
	 * 获得结束时间
	 * @param bossId
	 * @param now
	 * @return
	 */
	public long getEndTime(int bossId,long now){
		try{
			List<String> timeListstr = null;
			switch (bossId) {
			case 1:
				timeListstr = ParserString.parseString(ConfigManager.getInstance()
								.getConf(config10.class).get(1291).configvalue);
				return DateUtil.getEndTimeOnday(now, timeListstr.get(0));
			case 2:
				timeListstr = ParserString.parseString(ConfigManager.getInstance()
								.getConf(config10.class).get(1292).configvalue);
				return DateUtil.getEndTimeOnday(now, timeListstr.get(0));
			case 3:
				timeListstr = ParserString.parseString(ConfigManager.getInstance()
								.getConf(config10.class).get(1291).configvalue);
				return DateUtil.getEndTimeOnday(now, timeListstr.get(1));
			case 4:
				timeListstr = ParserString.parseString(ConfigManager.getInstance()
								.getConf(config10.class).get(1292).configvalue);
				return DateUtil.getEndTimeOnday(now, timeListstr.get(1));
			}
			return -1;
		}catch(Exception e){
			e.printStackTrace();
			return -1;
		}
	}
	/**
	 * 获得开始时间
	 * @param bossId
	 * @param now
	 * @return
	 */
	public long getBeginTime(int bossId,long now){
		try{
			List<String> timeListstr = null;
			switch (bossId) {
			case 1:
				timeListstr = ParserString.parseString(ConfigManager.getInstance()
								.getConf(config10.class).get(1291).configvalue);
				return DateUtil.getBeginTimeOnday(now, timeListstr.get(0));
			case 2:
				timeListstr = ParserString.parseString(ConfigManager.getInstance()
								.getConf(config10.class).get(1292).configvalue);
				return DateUtil.getBeginTimeOnday(now, timeListstr.get(0));
			case 3:
				timeListstr = ParserString.parseString(ConfigManager.getInstance()
								.getConf(config10.class).get(1291).configvalue);
				return DateUtil.getBeginTimeOnday(now, timeListstr.get(1));
			case 4:
				timeListstr = ParserString.parseString(ConfigManager.getInstance()
								.getConf(config10.class).get(1292).configvalue);
				return DateUtil.getBeginTimeOnday(now, timeListstr.get(1));
			}
			return -1;
		}catch(Exception e){
			e.printStackTrace();
			return -1;
		}
	}
	/**
	 * 获得活动总时间
	 * @param bossId
	 * @param now
	 * @return
	 */
	public long getOpenTimeMilli(int bossId,long now){
		try{
			List<String> timeListstr = null;
			switch (bossId) {
			case 1:
				timeListstr = ParserString.parseString(ConfigManager.getInstance()
								.getConf(config10.class).get(1291).configvalue);
				return DateUtil.getOpenTimeMilli(now, timeListstr.get(0));
			case 2:
				timeListstr = ParserString.parseString(ConfigManager.getInstance()
								.getConf(config10.class).get(1292).configvalue);
				return DateUtil.getOpenTimeMilli(now, timeListstr.get(0));
			case 3:
				timeListstr = ParserString.parseString(ConfigManager.getInstance()
								.getConf(config10.class).get(1291).configvalue);
				return DateUtil.getOpenTimeMilli(now, timeListstr.get(1));
			case 4:
				timeListstr = ParserString.parseString(ConfigManager.getInstance()
								.getConf(config10.class).get(1292).configvalue);
				return DateUtil.getOpenTimeMilli(now, timeListstr.get(1));
			}
			return -1;
		}catch(Exception e){
			e.printStackTrace();
			return -1;
		}
	}
	
	/**
	 * 获得进入战斗CD时间
	 * @param bossId
	 * @return
	 */
	public int getCdTime(int bossId){
		if(bossId == 1 || bossId == 3){
			return Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1293).configvalue);
		}else{
			return Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1294).configvalue);
		}
	}
	
	/**
	 * 发送boss商城信息
	 * @param roleId
	 */
	public void sendSBossShop(long roleId){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		xbean.bossrole xbossrole = Module.getxbossrole(roleId, false);
		this.initBossShop(xbossrole, now);
		SBossShop snd = new SBossShop();
		snd.shoplist.addAll(xbossrole.getBshop().getShoplist());
		snd.hunternum = xbossrole.getBshop().getHunternum();
		snd.chuanshuozs = xbossrole.getChuanshuozs();
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	/**
	 * 发送购买成功接口
	 * @param roleId
	 * @param shopId
	 * @param hunterNum
	 * @param chuanshuozs
	 * @param dropList
	 */
	private void sendSBuyBossShop(long roleId,int shopId,int hunterNum,int chuanshuozs,List<Integer> dropList){
		SBuyBossShop snd = new SBuyBossShop();
		snd.result = SBuyBossShop.END_OK;
		snd.bossshopid = shopId;
		snd.hunternum = hunterNum;
		snd.chuanshuozs = chuanshuozs;
		snd.droplist.addAll(dropList);
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	/**
	 * 发送购买祝福接口
	 * @param roleId
	 * @param zhufunum
	 * @param shouwangzl
	 */
	private void sendSBossBuyZhufu(long roleId,int zhufunum,int shouwangzl,int bossId){
		SBossBuyZhufu snd = new SBossBuyZhufu();
		snd.result = SBossBuyZhufu.END_OK;
		snd.zhufunum = zhufunum;
		snd.shouwangzl = shouwangzl;
		snd.bossid = bossId;
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	/**
	 * 发送守望之灵购买成功
	 * @param roleId
	 * @param shouwangzl
	 */
	private void sendSBuyShouwangzl(long roleId,int shouwangzl){
		SBuyShouwangzl snd = new SBuyShouwangzl();
		snd.result = SBuyShouwangzl.END_OK;
		snd.shouwangzl = shouwangzl;
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	
}
