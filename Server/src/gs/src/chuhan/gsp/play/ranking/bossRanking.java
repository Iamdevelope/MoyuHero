package chuhan.gsp.play.ranking;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

import chuhan.gsp.attr.config10;
import chuhan.gsp.award.DropManager;
import chuhan.gsp.log.LogBehavior;
import chuhan.gsp.log.Logger;
import chuhan.gsp.mail.MailColumn;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.play.endlessbattle.EndlessRankInfo;
import chuhan.gsp.play.endlessbattle.EndlessinfoColumns;
import chuhan.gsp.play.endlessbattle.OtherHero;
import chuhan.gsp.play.wordboss.BossRankInfo;
import chuhan.gsp.util.DateUtil;



public class bossRanking{

	public static Logger logger = Logger.getLogger(bossRanking.class);
	static private bossRanking instance = null;
	
	private bossRanking(){
		init();
	}
	public static bossRanking getInstance() {
		if(instance == null)
		{
			instance = new bossRanking();
		}
		return instance;
	}
	
	public final int rankAllNum = 10;		//排行榜最大容量
	public static int bossId = 0;
	
	private static long sort_time10 = 0;
	private static long sort_timeall = 0;
	
	private final int sort10time = 3 * 1000;
	private final int sortalltime = 15 * 1000;
	
	public ConcurrentHashMap<Long,rankData> map10 = new ConcurrentHashMap<Long,rankData>();
	public ConcurrentHashMap<Long,rankData> mapall = new ConcurrentHashMap<Long,rankData>();
	
	public List<rankData> list10 = new LinkedList<rankData>();
	
	/**
	 * 添加排行榜数据
	 * @param endCol
	 */
	public void addInRank(long roleId,long num,int bossId,long now){
		if(num <= 0){
			return;
		}
		if(this.bossId != bossId){
			map10.clear();
			mapall.clear();
			list10.clear();
			this.bossId = bossId;
		}
		chuhan.gsp.attr.PropRole prole = chuhan.gsp.attr.PropRole.getPropRole(roleId, false);
		rankData rankdata = new rankData();
		rankdata.roleid = roleId;
		rankdata.roleName = prole.getProperties().getRolename();
		rankdata.num = num;
		
		if( list10.size() >= rankAllNum){
			if( list10.get(list10.size() - 1).num < num ){
				rankdata.lastRank = map10.size() + 1;
				map10.put(roleId, rankdata);
			}
			rankData rank10 = map10.get(roleId);
			if(rank10 != null){
				map10.put(roleId, rankdata);
			}
		}else{
			rankdata.lastRank = map10.size() + 1;
			map10.put(roleId, rankdata);
		}
		if(rankdata.lastRank != 0){
			rankdata.lastRank = mapall.size() + 1;
		}
		mapall.put(roleId, rankdata);		
	}

	/**
	 * 初始化内容
	 */
	private void init(){
		xbean.bossRankList ranklistold = xtable.Bossranklists.select(1);
		if(ranklistold == null || ranklistold.getRanklist() == null){
			return;
		}
		this.bossId = ranklistold.getBossid();
		for( xbean.bossRankInfo rankinfo : ranklistold.getRanklist() ){
			rankData data = new rankData();
			data.roleid = rankinfo.getRoleid();
			data.roleName = rankinfo.getRolename();
			data.num = rankinfo.getNum();
			data.lastRank = rankinfo.getRankid();
			if(data.lastRank <= rankAllNum){
				map10.put(data.roleid, data);
			}
			mapall.put(data.roleid, data);
		}
		list10 = rankSort10();
	}
	
	/**
	 * 创建新排行榜数据
	 * @param tempList
	 * @param key
	 * @param now
	 * @param bossId
	 */
	public void createRankList(List<rankData> tempList,int key,long now,int bossId){
		xbean.bossRankList ranklistold = xtable.Bossranklists.get(key);
		if(ranklistold == null){
			ranklistold  = xbean.Pod.newbossRankList();
			xtable.Bossranklists.insert(key, ranklistold);
		}
		ranklistold.getRanklist().clear();
		for(rankData rankdata : tempList){
			xbean.bossRankInfo rankInfo = xbean.Pod.newbossRankInfo();
			rankInfo.setRoleid(rankdata.roleid);
			rankInfo.setRolename(rankdata.roleName);
			rankInfo.setNum(rankdata.num);
			rankInfo.setRankid(rankdata.lastRank);
			ranklistold.getRanklist().add(rankInfo);
		}
		ranklistold.setRanktime(now);
		ranklistold.setBossid(bossId);
	}

	/**
	 * 获取排行列表（消息用列表）
	 * @param key
	 * @return
	 */
	public List<BossRankInfo> getRankList(){
		List<BossRankInfo> result = new LinkedList<BossRankInfo>();
		for(rankData rankdata : list10){
			BossRankInfo info = new BossRankInfo();
			info.roleid = rankdata.roleid;
			info.rolename = rankdata.roleName;
			info.num = rankdata.num;
			result.add(info);
		}
		return result;		
	}
	
	/**
	 * 前10名排名
	 * @param inlist
	 */
	public List<rankData> rankSort10(){
		List<rankData> templist = new ArrayList<rankData>();
		List<Long> dellist = new ArrayList<Long>();
		templist.addAll(map10.values());
		Collections.sort(templist,comparator);
		
		for(int i = 0;i< templist.size();i++){
			rankData temprank = templist.get(i);
			changeRank(temprank,i);
			if(i >= rankAllNum){
				dellist.add(temprank.roleid);
			}
		}
		delOutOfRank(dellist,map10);
		return templist;
	}
	
	/**
	 * 总体排名
	 * @return
	 */
	public List<rankData> rankSortAll(){
		List<rankData> templist = new ArrayList<rankData>();
		templist.addAll(mapall.values());
		Collections.sort(templist,comparator);
		
		ConcurrentHashMap<Long,rankData> maptemp = new ConcurrentHashMap<Long,rankData>();
		List<rankData> listtemp = new LinkedList<rankData>();
		
		for(int i = 0;i< templist.size();i++){
			rankData temprank = templist.get(i);
			changeRank(temprank,i);
			if( i < this.rankAllNum){
				maptemp.put(temprank.roleid, temprank);
				listtemp.add(temprank);
			}
		}
		this.map10.clear();
		this.map10 = maptemp;
		this.list10.clear();
		this.list10 = listtemp;
		
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		createRankList(templist,1,now,this.bossId);
		return templist;
	}
	
	/**
	 * 删除超出排行榜的人（前10）
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
	}
	
	/**
	 * 排序方法
	 */
	private static final Comparator<rankData> comparator = new Comparator<rankData>(){
		@Override
		public int compare(rankData o1, rankData o2) {
			if (o1.num > o2.num){
				return -1;
			}
			return 1;
		}
	};
	
	/**
	 * 根据时间排序入口
	 * @param now
	 */
	public synchronized void ranking(long now){
		if(now > this.sort_timeall){
			this.bossRankTime(true);
			this.sort_timeall = now + sortalltime;
		}else if(now > this.sort_time10){
			this.bossRankTime(false);
			this.sort_time10 = now + sort10time;
		}
	}
	
	/**
	 * 定时排名
	 * @param sleepTime
	 * @param isAll
	 */
	public void bossRankTime(boolean isAll){
		logger.debug("----------bossRank----------");
		if(isAll){
			rankSortAll();
		}else{
			list10 = rankSort10();
		}
	}
	
	/**
	 * 根据排名发放奖励
	 * @param now
	 */
	public void sendMail(long now){
		for(int i = 0;i< list10.size();i++){
			rankData data = list10.get(i);
			String dropStr = "";
			if(i == 0){
				dropStr = ConfigManager.getInstance().getConf(config10.class).get(1318).getConfigvalue();
			}else if(i == 1){
				dropStr = ConfigManager.getInstance().getConf(config10.class).get(1319).getConfigvalue();
			}else if(i == 2){
				dropStr = ConfigManager.getInstance().getConf(config10.class).get(1320).getConfigvalue();
			}else if(i >= 3 && i <= 4){
				dropStr = ConfigManager.getInstance().getConf(config10.class).get(1321).getConfigvalue();
			}else if(i >= 5 && i <= 9){
				dropStr = ConfigManager.getInstance().getConf(config10.class).get(1322).getConfigvalue();
			}else{
				continue;
			}
			List<String> str = new LinkedList<String>();
			str.add(String.valueOf(i+1));
			List<Integer> dropList = DropManager.getInstance().drop(data.roleid, dropStr, LogBehavior.BOSSRANK, false,-1);
			if(dropList != null && dropList.size() != 0){
				MailColumn col = MailColumn.getMailColumn(data.roleid, false);
				col.addMail(col.createMail("mail_tips1","mail_content26","mail_content30", null,
						DropManager.getInstance().getMailItemListByInnerkey(dropList,1.0f), 
						now+MailColumn.DEFAULT_TIME,str),false);
			}
		}
	}
	
	/**
	 * 更改名称更新排行榜接口
	 * @param roleId
	 * @param roleName
	 */
	public void changeName(long roleId,String roleName){
		rankData init = map10.get(roleId);
		if(init != null){
			init.roleName = roleName;
		}
		init = mapall.get(roleId);
		if(init != null){
			init.roleName = roleName;
		}
		for(rankData data : list10){
			if(data.roleid == roleId){
				data.roleName = roleName;
				break;
			}
		}
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		this.createRankList(list10,1,now,this.bossId);
	}
	
	public class rankData{
		public long roleid;
		public String roleName;
		public long num;	//排名所用的伤害值
		public int lastRank = 0;	//今日的上次排名（用于判断是否更新预测排名）
	}


	
}
