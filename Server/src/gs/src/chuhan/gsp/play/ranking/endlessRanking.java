package chuhan.gsp.play.ranking;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;
import java.util.concurrent.ConcurrentHashMap;





import chuhan.gsp.attr.config10;
import chuhan.gsp.award.DropManager;
import chuhan.gsp.award.IDManager;
import chuhan.gsp.game.ultimatetrialreward50;
import chuhan.gsp.item.innerdrop16;
import chuhan.gsp.log.Logger;
import chuhan.gsp.mail.MailColumn;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.play.endlessbattle.EndlessRankInfo;
import chuhan.gsp.play.endlessbattle.EndlessinfoColumns;
import chuhan.gsp.play.endlessbattle.OtherHero;
import chuhan.gsp.play.ranking.bossRanking.rankData;
import chuhan.gsp.util.DateUtil;
import chuhan.gsp.util.ParserString;



public class endlessRanking{

	public static Logger logger = Logger.getLogger(endlessRanking.class);
	static private endlessRanking instance = null;
	public final static int TEMP_KEY = 101;
	
	private endlessRanking(){
		initByResult();
	}
	public static endlessRanking getInstance() {
		if(instance == null)
		{
			instance = new endlessRanking();
		}
		return instance;
	}
	
	public final int rankAllNum = 20;		//排行榜最大容量
	private static Boolean isBeginTodayEnd = Boolean.FALSE;
	
	public ConcurrentHashMap<Long,rankData> map1_50 = new ConcurrentHashMap<Long,rankData>();
	public ConcurrentHashMap<Long,rankData> map51_100 = new ConcurrentHashMap<Long,rankData>();
	public ConcurrentHashMap<Long,rankData> map101_ = new ConcurrentHashMap<Long,rankData>();
	
	public ConcurrentHashMap<Long,rankData> map1_50temp = new ConcurrentHashMap<Long,rankData>();
	public ConcurrentHashMap<Long,rankData> map51_100temp = new ConcurrentHashMap<Long,rankData>();
	public ConcurrentHashMap<Long,rankData> map101_temp = new ConcurrentHashMap<Long,rankData>();
	
	public List<EndlessRankInfo> list1_50 = new LinkedList<EndlessRankInfo>();
	public List<EndlessRankInfo> list51_100 = new LinkedList<EndlessRankInfo>();
	public List<EndlessRankInfo> list101_ = new LinkedList<EndlessRankInfo>();
	
	/**
	 * 添加排行榜数据
	 * @param endCol
	 */
	public void addInRank(EndlessinfoColumns endCol){
		chuhan.gsp.attr.PropRole prole = chuhan.gsp.attr.PropRole.getPropRole(endCol.roleId, false);
		rankData rankdata = new rankData();
		rankdata.roleid = endCol.roleId;
		rankdata.roleName = prole.getProperties().getRolename();
		rankdata.groupnum = endCol.xcolumn.getGroupnum();
		rankdata.alldropnum = endCol.xcolumn.getAlldropnum();
		rankdata.endtime = endCol.xcolumn.getEndtime();
		rankdata.roleInit.level = prole.getLevel();
		rankdata.roleInit.trooptype = endCol.xcolumn.getTrooptype();
//		rankdata.roleInit.heroattribute.putAll(endCol.xcolumn.getHeroattribute());
		for(Map.Entry<Integer, xbean.OtherHero> entry : endCol.xcolumn.getHeroattribute().entrySet()){
			OtherHero otherhero = new OtherHero();
			otherhero.setExp(entry.getValue().getExp());
			otherhero.setHeroid(entry.getValue().getHeroid());
			otherhero.setHerolevel(entry.getValue().getHerolevel());
			otherhero.setHeroviewid(entry.getValue().getHeroviewid());
			otherhero.setHp(entry.getValue().getHp());
			otherhero.setMagicattack(entry.getValue().getMagicattack());
			otherhero.setMagicdefence(entry.getValue().getMagicdefence());
			otherhero.setPhysicalattack(entry.getValue().getPhysicalattack());
			otherhero.setPhysicaldefence(entry.getValue().getPhysicaldefence());
			otherhero.setSkill1(entry.getValue().getSkill1());
			otherhero.setSkill2(entry.getValue().getSkill2());
			otherhero.setSkill3(entry.getValue().getSkill3());
			rankdata.roleInit.heroattribute.put(entry.getKey(), otherhero);
		}
		rankdata.roleInit.onranknum = endCol.xcolumn.getOnranknum();
		rankdata.roleInit.onranklasttime = endCol.xcolumn.getOnranklasttime();
		
		synchronized(isBeginTodayEnd){
			if(isBeginTodayEnd){
				if(prole.getLevel() <= 50){
					map1_50temp.put(rankdata.roleid, rankdata);
				}else if(prole.getLevel() <= 100){
					map51_100temp.put(rankdata.roleid, rankdata);
				}else{
					map101_temp.put(rankdata.roleid, rankdata);
				}
			}else{
				if(prole.getLevel() <= 50){
					map1_50.put(rankdata.roleid, rankdata);
				}else if(prole.getLevel() <= 100){
					map51_100.put(rankdata.roleid, rankdata);
				}else{
					map101_.put(rankdata.roleid, rankdata);
				}
			}	
		}
		
		List<rankData> tempList = new LinkedList<rankData>();
		tempList.add(rankdata);
//		createRankList(tempList,TEMP_KEY,rankdata.endtime);
//		//测试数据
//		sortEntry(true);
		
	}

	
	/**
	 * 排序启动入口
	 * @param isTodayEnd
	 */
	public synchronized void sortEntry(boolean isTodayEnd){
		if(isTodayEnd){
			long now = GameTime.currentTimeMillis();
			//将临时保存数据删除
			xbean.EndlessRankList ranklistold = xtable.Endlessranklists.get(TEMP_KEY);
			ranklistold.getRanklist().clear();
			
			isBeginTodayEnd = Boolean.TRUE;
			
			List<rankData> tempList = rankSort(map1_50);
			createRankList(tempList,1,now);
			
			tempList = rankSort(map51_100);
			createRankList(tempList,2,now);
			
			tempList = rankSort(map101_);
			createRankList(tempList,3,now);	
			//清空列表
			map1_50.clear();
			map51_100.clear();
			map101_.clear();
			//修改状态
			isBeginTodayEnd = Boolean.FALSE;
			//将临时表数据导入
			map1_50.putAll(map1_50temp);
			map51_100.putAll(map51_100temp);
			map101_.putAll(map101_temp);
			//清空临时表数据
			map1_50temp.clear();
			map51_100temp.clear();
			map101_temp.clear();
			initByResult();
		}else{
			 rankSort(map1_50);
			 rankSort(map51_100);
			 rankSort(map101_);
		}
	}
	
	/**
	 * 创建新排行榜数据
	 * @param tempList
	 * @param key
	 * @param now
	 */
	public void createRankList(List<rankData> tempList,int key,long now){
		xbean.EndlessRankList ranklistold = xtable.Endlessranklists.get(key);
		if(ranklistold == null){
			ranklistold  = xbean.Pod.newEndlessRankList();
			xtable.Endlessranklists.insert(key, ranklistold);
		}else{
			//缓存数据不清楚
			if(key != this.TEMP_KEY){
				ranklistold.getRanklist().clear();
			}else if( !DateUtil.inTheSameDay(ranklistold.getRanktime(), now)){
				ranklistold.getRanklist().clear();
			}
		}
		
//		xbean.EndlessRankList ranklist  = xbean.Pod.newEndlessRankList();
		int i = 0;
		for(rankData rankdata : tempList){
			if(i >= rankAllNum){
				break;
			}
			i++;
			xbean.EndlessRankInfo rankInfo = xbean.Pod.newEndlessRankInfo();
			rankInfo.setRoleid(rankdata.roleid);
			rankInfo.setRolename(rankdata.roleName);
			rankInfo.setLevel(rankdata.roleInit.level);
			rankInfo.setGroupnum(rankdata.groupnum);
			rankInfo.setTrooptype(rankdata.roleInit.trooptype);
			rankInfo.setAlldropnum(rankdata.alldropnum);
			for(Map.Entry<Integer, OtherHero> entry : rankdata.roleInit.heroattribute.entrySet()){
				xbean.OtherHero otherhero = xbean.Pod.newOtherHero();
				otherhero.setExp(entry.getValue().getExp());
				otherhero.setHeroid(entry.getValue().getHeroid());
				otherhero.setHerolevel(entry.getValue().getHerolevel());
				otherhero.setHeroviewid(entry.getValue().getHeroviewid());
				otherhero.setHp(entry.getValue().getHp());
				otherhero.setMagicattack(entry.getValue().getMagicattack());
				otherhero.setMagicdefence(entry.getValue().getMagicdefence());
				otherhero.setPhysicalattack(entry.getValue().getPhysicalattack());
				otherhero.setPhysicaldefence(entry.getValue().getPhysicaldefence());
				otherhero.setSkill1(entry.getValue().getSkill1());
				otherhero.setSkill2(entry.getValue().getSkill2());
				otherhero.setSkill3(entry.getValue().getSkill3());
				rankInfo.getHeroattribute().put(entry.getKey(), otherhero);
			}
//			rankInfo.getHeroattribute().putAll(rankdata.roleInit.heroattribute);
			if (key != this.TEMP_KEY) {
				if (DateUtil.inTheSameDay(rankdata.roleInit.onranklasttime
						+ DateUtil.dayMills, now)) {
					rankInfo.setOnranknum(rankdata.roleInit.onranknum + 1);
				} else {
					rankInfo.setOnranknum(1);
				}
			}
			
			if(key == this.TEMP_KEY){
				for(int num = 0;num<ranklistold.getRanklist().size();num++){
					xbean.EndlessRankInfo old = ranklistold.getRanklist().get(num);
					if(old.getRoleid() == rankInfo.getRoleid()){
						ranklistold.getRanklist().remove(num);
						break;
					}
				}
			}
			
			ranklistold.getRanklist().add(rankInfo);

			EndlessinfoColumns endCol = EndlessinfoColumns.getEndLessColumn(rankdata.roleid, false);
			endCol.xcolumn.setOnranklasttime(now);
			endCol.xcolumn.setOnranknum(rankInfo.getOnranknum());
			this.sendMail(now, rankInfo.getRoleid(), rankInfo.getLevel(), i, rankInfo.getOnranknum());
		}
		ranklistold.setRanktime(now);
	}
	
	/**
	 * 根据排名发放奖励
	 * @param now
	 */
	public void sendMail(long now,long roleId,int rolelv,int ranklv,int onRankNum){
		TreeMap<Integer,ultimatetrialreward50> initMap = ConfigManager.getInstance().getConf(ultimatetrialreward50.class);
		//等级段
		int lvType = 1;
		if(rolelv % 50 == 0){
			lvType = rolelv / 50;
		}else{
			lvType = rolelv / 50 + 1;
		}
		if(lvType > 3){
			lvType = 3;
		}
		//排行榜奖励
		for(Map.Entry<Integer,ultimatetrialreward50> entry : initMap.entrySet()){
			if(entry.getValue().getLevelrange() == lvType && 
				entry.getValue().getRank1() >= ranklv && entry.getValue().getRank2() <= ranklv){
				
				List<xbean.MailItem> items = new LinkedList<xbean.MailItem>();
				List<Integer> objectIdList = ParserString.parseString2Int(entry.getValue().getReward_id());
				List<Integer> numList = ParserString.parseString2Int(entry.getValue().getReward_num());
				if(objectIdList.size() != numList.size()){
					logger.error("endlessRank ultimatetrialreward50 error! id = " + entry.getKey());
					break;
				}
				for(int i = 0;i< objectIdList.size();i++){
					xbean.MailItem test = xbean.Pod.newMailItem();
					test.setObjectid(objectIdList.get(i));
					test.setDropnum(numList.get(i));
					test.setDropparameter1(0);
					test.setDropparameter2(0);
					items.add(test);
				}
				if(items != null && items.size() != 0){
					List<String> str = new LinkedList<String>();
					str.add(String.valueOf(ranklv));
					MailColumn col = MailColumn.getMailColumn(roleId, false);
					col.addMail(col.createMail("mail_tips1","mail_content24","mail_content28", null,
							items, now+MailColumn.DEFAULT_TIME,str),false);
				}
				break;
			}
		}
		//持续在榜奖励
		List<Integer> dayList = ParserString.parseString2Int(
				ConfigManager.getInstance().getConf(config10.class).get(1264).getConfigvalue());
		List<Integer> ybList = ParserString.parseString2Int(
				ConfigManager.getInstance().getConf(config10.class).get(1265).getConfigvalue());
		if(dayList.size() != ybList.size()){
			logger.error("endlessRank config10 error! id = 1264 and id = 1265");
			return;
		}
		for(int i = 0;i< dayList.size();i++){
			if(onRankNum == dayList.get(i)){
				List<xbean.MailItem> items = new LinkedList<xbean.MailItem>();
				xbean.MailItem test = xbean.Pod.newMailItem();
				test.setObjectid(IDManager.YUANBAO);
				test.setDropnum(ybList.get(i));
				test.setDropparameter1(0);
				test.setDropparameter2(0);
				items.add(test);
				List<String> str = new LinkedList<String>();
				str.add(String.valueOf(ranklv));
				MailColumn col = MailColumn.getMailColumn(roleId, false);
				col.addMail(col.createMail("mail_tips1","mail_content24","mail_content28", null,
						items, now+MailColumn.DEFAULT_TIME,str),false);
				break;
			}
		}
	}
	
	/**
	 * 初始化排行榜数据（消息用）
	 */
	private void initByResult(){
		this.list1_50.clear();
		this.list51_100.clear();
		this.list101_.clear();
		List<EndlessRankInfo> tempList = getRankList(1);
		if(tempList != null){
			list1_50.addAll(tempList);
		}
		tempList = getRankList(2);
		if(tempList != null){
			list51_100.addAll(tempList);
		}
		tempList = getRankList(3);
		if(tempList != null){
			list101_.addAll(tempList);
		}
		
		//将保存的临时数据初始化
		long now = GameTime.currentTimeMillis();
		xbean.EndlessRankList ranklistold = xtable.Endlessranklists.select(TEMP_KEY);
		if(ranklistold != null && DateUtil.inTheSameDay(now, ranklistold.getRanktime())){
			for(int num = ranklistold.getRanklist().size() - 1; num>= 0 ; num--){
				xbean.EndlessRankInfo old = ranklistold.getRanklist().get(num);
				if(old == null){
					continue;
				}
				rankData rankdata = this.getRankDataByxbean(old);
				if(old.getLevel() <= 50){
					map1_50.put(rankdata.roleid, rankdata);
				}else if(old.getLevel() <= 100){
					map51_100.put(rankdata.roleid, rankdata);
				}else{
					map101_.put(rankdata.roleid, rankdata);
				}
			}
		}		
	}
	private rankData getRankDataByxbean(xbean.EndlessRankInfo rankInfo){
		rankData rankdata = new rankData();
		rankdata.roleid = rankInfo.getRoleid();
		rankdata.roleName = rankInfo.getRolename();
		rankdata.roleInit.level = rankInfo.getLevel();
		rankdata.groupnum = rankInfo.getGroupnum();
		rankdata.roleInit.trooptype = rankInfo.getTrooptype();
		rankdata.alldropnum = rankInfo.getAlldropnum();
		for(Map.Entry<Integer, xbean.OtherHero> entry : rankInfo.getHeroattribute().entrySet()){
			OtherHero otherhero = new OtherHero();
			otherhero.setExp(entry.getValue().getExp());
			otherhero.setHeroid(entry.getValue().getHeroid());
			otherhero.setHerolevel(entry.getValue().getHerolevel());
			otherhero.setHeroviewid(entry.getValue().getHeroviewid());
			otherhero.setHp(entry.getValue().getHp());
			otherhero.setMagicattack(entry.getValue().getMagicattack());
			otherhero.setMagicdefence(entry.getValue().getMagicdefence());
			otherhero.setPhysicalattack(entry.getValue().getPhysicalattack());
			otherhero.setPhysicaldefence(entry.getValue().getPhysicaldefence());
			otherhero.setSkill1(entry.getValue().getSkill1());
			otherhero.setSkill2(entry.getValue().getSkill2());
			otherhero.setSkill3(entry.getValue().getSkill3());
			rankdata.roleInit.heroattribute.put(entry.getKey(), otherhero);
		}
		return rankdata;
	}
	/**
	 * 获取排行列表（消息用列表）
	 * @param key
	 * @return
	 */
	public List<EndlessRankInfo> getRankList(int key){
		List<EndlessRankInfo> result = new LinkedList<EndlessRankInfo>();
		xbean.EndlessRankList ranklist = xtable.Endlessranklists.select(key);
		if(ranklist == null)
			return null;
		for(xbean.EndlessRankInfo xRankInfo : ranklist.getRanklist()){
			EndlessRankInfo bRankInfo = new EndlessRankInfo();
			bRankInfo.roleid = xRankInfo.getRoleid();
			bRankInfo.rolename = xRankInfo.getRolename();
			bRankInfo.level = xRankInfo.getLevel();
			bRankInfo.groupnum = xRankInfo.getGroupnum();
			bRankInfo.trooptype = xRankInfo.getTrooptype();
			bRankInfo.alldropnum = xRankInfo.getAlldropnum();
//			bRankInfo.heroattribute.putAll(xRankInfo.getHeroattribute());
			for(Map.Entry<Integer, xbean.OtherHero> entry : xRankInfo.getHeroattribute().entrySet()){
				chuhan.gsp.play.endlessbattle.OtherHero otherHero = new chuhan.gsp.play.endlessbattle.OtherHero();
				otherHero.heroid = entry.getValue().getHeroid();
				otherHero.exp = entry.getValue().getExp();
				otherHero.herolevel = entry.getValue().getHerolevel();
				otherHero.hp = entry.getValue().getHp();
				otherHero.physicalattack = entry.getValue().getPhysicalattack();
				otherHero.physicaldefence = entry.getValue().getPhysicaldefence();
				otherHero.magicattack = entry.getValue().getMagicattack();
				otherHero.magicdefence = entry.getValue().getMagicdefence();
				otherHero.skill1 = entry.getValue().getSkill1();
				otherHero.skill2 = entry.getValue().getSkill2();
				otherHero.skill3 = entry.getValue().getSkill3();
				otherHero.heroviewid = entry.getValue().getHeroviewid();
				bRankInfo.heroattribute.put(entry.getKey(), otherHero);
			}
			bRankInfo.onranknum = xRankInfo.getOnranknum();
			result.add(bRankInfo);
		}
		return result;		
	}
	
	/**
	 * 定时排序并更新数据
	 * @param inlist
	 */
	public List<rankData> rankSort(ConcurrentHashMap<Long,rankData> inMap){
		List<rankData> templist = new ArrayList<rankData>();
		List<Long> dellist = new ArrayList<Long>();
		templist.addAll(inMap.values());
		Collections.sort(templist,comparator);
		
		for(int i = 0;i< templist.size();i++){
			rankData temprank = templist.get(i);
			changeRank(temprank,i);
			if(i >= rankAllNum){
				dellist.add(temprank.roleid);
			}
		}
		delOutOfRank(dellist,inMap);
		return templist;
	}
	
	/**
	 * 删除超出排行榜的人
	 * @param dellist
	 * @param inMap
	 */
	public void delOutOfRank(List<Long> dellist,ConcurrentHashMap<Long,rankData> inMap){
		for(Long roleid : dellist){
			inMap.remove(roleid);
		}
	}
	
	/**
	 * 更新今日预测排名数据
	 * @param rankdata
	 * @param i
	 */
	public void changeRank(rankData rankdata, int i){
		if(rankdata.lastRank == i + 1){
			return;
		}
		rankdata.lastRank = i + 1;
		EndlessinfoColumns endCol = EndlessinfoColumns.getEndLessColumn(rankdata.roleid, false);
		endCol.xcolumn.setExpectedrank(i + 1);
	}
	
	/**
	 * 排序方法
	 */
	private static final Comparator<rankData> comparator = new Comparator<rankData>(){
		@Override
		public int compare(rankData o1, rankData o2) {
			if (o1.groupnum > o2.groupnum){
				return -1;
			}else if(o1.groupnum == o2.groupnum){
				if(o1.alldropnum > o2.alldropnum){
					return -1;
				}else if(o1.alldropnum == o2.alldropnum){
					if(o1.endtime < o2.endtime){
						return -1;
					}
				}
			}
			return 1;
		}
	};
	
	/**
	 * 定时任务
	 * @param isEnd
	 */
	public void endlessRankTime(boolean isEnd){
		logger.infoWhileCommit("endlessRanking::endlessRank::"+isEnd);
		try {
			new xdb.Procedure(){
				protected boolean process() throws Exception {			
					sortEntry(isEnd);
					return true;
				};
			}.submit();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	
	/**
	 * 更改名称更新排行榜接口
	 * @param roleId
	 * @param roleName
	 */
	public void changeName(long roleId,String roleName){
		this.changeName(map1_50, roleId, roleName);
		this.changeName(map51_100, roleId, roleName);
		this.changeName(map101_, roleId, roleName);
		this.changeName(map1_50temp, roleId, roleName);
		this.changeName(map51_100temp, roleId, roleName);
		this.changeName(map101_temp, roleId, roleName);
		this.changeNmae(list1_50, roleId, roleName);
		this.changeNmae(list51_100, roleId, roleName);
		this.changeNmae(list101_, roleId, roleName);
		this.changeName(1, roleId, roleName);
		this.changeName(2, roleId, roleName);
		this.changeName(3, roleId, roleName);
		this.changeName(TEMP_KEY, roleId, roleName);
	}
	
	private void changeName(ConcurrentHashMap<Long,rankData> map, long roleId,String roleName){
		rankData data = map.get(roleId);
		if(data != null){
			data.roleName = roleName;
			return;
		}
	}
	private void changeNmae(List<EndlessRankInfo> list,long roleId,String roleName){
		for(EndlessRankInfo data : list){
			if(data.roleid == roleId){
				data.rolename = roleName;
				return;
			}
		}
	}
	private void changeName(int key,long roleId,String roleName){
		xbean.EndlessRankList ranklist = xtable.Endlessranklists.get(key);
		if(ranklist == null)
			return;
		for(xbean.EndlessRankInfo xRankInfo : ranklist.getRanklist()){
			if(xRankInfo.getRoleid() == roleId){
				xRankInfo.setRolename(roleName);
				return;
			}
		}	
	}
	
	
	public class rankData{
		long roleid;
		String roleName;
		int groupnum;
		int alldropnum;
		long endtime;
		int lastRank = 0;	//今日的上次排名（用于判断是否更新预测排名）
		
		rankDataInit roleInit = new rankDataInit();
	}
	public class rankDataInit{
		int level;		//人物等级
		int trooptype;	//战队类型
//		int expectedrank;	//预期排名（最后排名）
		java.util.HashMap<Integer, OtherHero> heroattribute = new java.util.HashMap<Integer, OtherHero>(); 
											// 使用英雄的位置和属性信息（key为位置，value为OtherHero属性信息）
		int onranknum;	//连续在榜次数
		long onranklasttime;	//最后在榜时间	
	}
	public class OtherHero{
		private int heroid; // 英雄配表ID
		private int exp; // 当前经验
		private int herolevel; // 英雄等级
		private int hp; // 血量
		private int physicalattack; // 物理攻击
		private int physicaldefence; // 物理防御
		private int magicattack; // 魔法攻击
		private int magicdefence; // 魔法防御
		private int skill1; // 技能1编号（未开通为0）
		private int skill2; // 技能2编号（未开通为0）
		private int skill3; // 技能3编号（未开通为0）
		private int heroviewid; // 英雄外观
		
		public int getHeroid() {
			return heroid;
		}
		public void setHeroid(int heroid) {
			this.heroid = heroid;
		}
		public int getExp() {
			return exp;
		}
		public void setExp(int exp) {
			this.exp = exp;
		}
		public int getHerolevel() {
			return herolevel;
		}
		public void setHerolevel(int herolevel) {
			this.herolevel = herolevel;
		}
		public int getHp() {
			return hp;
		}
		public void setHp(int hp) {
			this.hp = hp;
		}
		public int getPhysicalattack() {
			return physicalattack;
		}
		public void setPhysicalattack(int physicalattack) {
			this.physicalattack = physicalattack;
		}
		public int getPhysicaldefence() {
			return physicaldefence;
		}
		public void setPhysicaldefence(int physicaldefence) {
			this.physicaldefence = physicaldefence;
		}
		public int getMagicattack() {
			return magicattack;
		}
		public void setMagicattack(int magicattack) {
			this.magicattack = magicattack;
		}
		public int getMagicdefence() {
			return magicdefence;
		}
		public void setMagicdefence(int magicdefence) {
			this.magicdefence = magicdefence;
		}
		public int getSkill1() {
			return skill1;
		}
		public void setSkill1(int skill1) {
			this.skill1 = skill1;
		}
		public int getSkill2() {
			return skill2;
		}
		public void setSkill2(int skill2) {
			this.skill2 = skill2;
		}
		public int getSkill3() {
			return skill3;
		}
		public void setSkill3(int skill3) {
			this.skill3 = skill3;
		}
		public int getHeroviewid() {
			return heroviewid;
		}
		public void setHeroviewid(int heroviewid) {
			this.heroviewid = heroviewid;
		}
	}
	
}
