package chuhan.gsp.play.tanxian;

import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;

import chuhan.gsp.attr.config10;
import chuhan.gsp.award.DropInit;
import chuhan.gsp.award.DropManager;
import chuhan.gsp.award.IDManager;
import chuhan.gsp.game.explorequest46;
import chuhan.gsp.game.vip39;
import chuhan.gsp.hero.Hero;
import chuhan.gsp.hero.HeroColumn;
import chuhan.gsp.hero.TroopColumn;
import chuhan.gsp.log.LogBehavior;
import chuhan.gsp.log.Logger;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.play.activity.ActivityManager;
import chuhan.gsp.play.huoyuedu.HuoyueColumns;
import chuhan.gsp.stage.StageRole;
import chuhan.gsp.util.DateUtil;
import chuhan.gsp.util.ParserString;



public class TanXianColumns {	
	public static Logger logger = Logger.getLogger(TanXianColumns.class);	
	
	private static final int ERROR_STAGEID = 1001;
	
	final public long roleId;
	final public xbean.stagetxall xcol;
	final boolean readonly;
	
	public static TanXianColumns getTanXianColumn(long roleId, boolean readonly){
		if(xtable.Properties.select(roleId) == null)
			throw new IllegalArgumentException("构造TanXianColumns时，角色 "+roleId+" 不存在。");
		
		xbean.stagetxall xcol = null;
		if(readonly)
			xcol = xtable.Stagetxalllist.select(roleId);
		else
			xcol = xtable.Stagetxalllist.get(roleId);
		if(xcol == null){
			if(readonly)
				xcol = xbean.Pod.newstagetxallData();
			else{
				xcol = xbean.Pod.newstagetxall();
				xtable.Stagetxalllist.insert(roleId, xcol);
			}
		}
		return new TanXianColumns(roleId, xcol, readonly);
	}
	
	private TanXianColumns(long roleId, xbean.stagetxall xcol, boolean readonly) {
		this.roleId = roleId;
		this.xcol = xcol;
		this.readonly = readonly;
		init();
	}
	public void init(){
		StageRole stage = StageRole.getStageRole(roleId);
		/*开启条件，暂时没有
		int battleInfoId = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1241).configvalue);
		xbean.StageBattleInfo battleInfo = stage.getBattleInfo(battleInfoId);
		if(battleInfo == null || battleInfo.getMaxstar() == 0){
			return;
		}
		*/
		
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		if( !DateUtil.inTheSameDay(now, this.xcol.getTxtime()) ){
			for(Map.Entry<Integer, xbean.StageInfo> entry : stage.getStageRoleXbean().getStages().entrySet() ){
				if(entry.getValue().getId() != ERROR_STAGEID){
					this.refreshTask(entry.getValue().getId(), false);
				}
			}
			this.xcol.setTxtime(now);
//			this.xcol.getTeamallmap().clear();
			this.sSRefreshTanXian();
		}
	}
	
	/**
	 * 探险操作入口
	 * @param endType
	 * @param tanxianId
	 * @return
	 */
	public boolean tanxianOtherEntry(int endType,int tanxianId){
		switch(endType){
		case CTanXianOther.END_GET:
			return this.endGet(tanxianId, endType, false);
		case CTanXianOther.END_SPEED:
			return this.endGet(tanxianId, endType, true);
		case CTanXianOther.END_NULL:
			return this.endBackNull(tanxianId, endType);
		case CTanXianOther.SREFRESH:
			return this.refreshEntry(tanxianId, endType);
		}
		return false;
	}
	
	/**
	 * 刷新入口
	 * @param tanxianId
	 * @param endtype
	 * @return
	 */
	public boolean refreshEntry(int tanxianId,int endtype){
		boolean result = false;
		int cost = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1120).configvalue);
		if( DropManager.getInstance().useDel(IDManager.YUANBAO, cost, roleId, LogBehavior.TANXIANREFRESHCOST) ){
			result = this.refreshTask(tanxianId, false);
		}
		if(result){
			this.sSRefreshTanXian();
			this.sendSTanXianOther(null, tanxianId, endtype);
		}
		return result;
	}
	
	/**
	 * 召回队伍
	 * @param tanxianId
	 * @param endtype
	 * @return
	 */
	public boolean endBackNull(int tanxianId,int endtype){
		explorequest46 init = ConfigManager.getInstance().getConf(explorequest46.class).get(tanxianId);
		if(init == null){
			return false;
		}
		for(xbean.tanxian tx : this.xcol.getStagetxallmap().get(init.getChapterID()).getStagetanxian()){
			if(tx.getTanxianid() == tanxianId){
				if(tx.getTanxiantype() == 1){
					tx.setTanxiantype(0);
					this.xcol.getTeamallmap().get(tx.getTeamnum()).getTeam().clear();
					this.xcol.getTeamallmap().get(tx.getTeamnum()).setTanxianid(0);	
					tx.setTeamnum(-1);
					this.sSRefreshTanXian();
					this.sendSTanXianOther(null, tanxianId, endtype);
					return true;
				}
			}
		}
		return false;
	}
	
	/**
	 * 探险结算领取
	 * @param tanxianId
	 * @param endtype
	 * @return
	 */
	public boolean endGet(int tanxianId,int endtype,boolean isCost){
		explorequest46 init = ConfigManager.getInstance().getConf(explorequest46.class).get(tanxianId);
		if(init == null){
			return false;
		}
		boolean result = false;
		for(xbean.tanxian tx : this.xcol.getStagetxallmap().get(init.getChapterID()).getStagetanxian()){
			if(tx.getTanxianid() == tanxianId){
				if(tx.getTanxiantype() == 1){
					long now = chuhan.gsp.main.GameTime.currentTimeMillis();
					if(now >= tx.getEndtime() || isCost){
						if(isCost){
							chuhan.gsp.attr.PropRole prole = chuhan.gsp.attr.PropRole.getPropRole(roleId, false);
							vip39 vipinit = prole.getVipInit();
							if(vipinit.getIfCanAccelerate() != 1){
								return false;
							}
							double a = Double.parseDouble(ConfigManager.getInstance().getConf(config10.class).get(1121).
									configvalue);
							double b = Double.parseDouble(ConfigManager.getInstance().getConf(config10.class).get(1122).
									configvalue);
							double c = Double.parseDouble(ConfigManager.getInstance().getConf(config10.class).get(1123).
									configvalue);
							double mtime = (tx.getEndtime() - now) / DateUtil.minuteMills;
							double dcost = 0;
							if(mtime > 0){
								dcost = a * mtime * mtime + b * mtime + c;
							}
							int cost = (int)dcost;
							if(cost > 0){
								if( !DropManager.getInstance().useDel(IDManager.YUANBAO, cost, roleId, LogBehavior.TANXIANSPEEDCOST) ){
									return false;
								}
							}
							tx.setEndtime(0);
							result = true;
							ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.TANXIAN_SPEEDUP, 1);
						}else{
							tx.setTanxiantype(2);
							this.xcol.getTeamallmap().get(tx.getTeamnum()).getTeam().clear();
							this.xcol.getTeamallmap().get(tx.getTeamnum()).setTanxianid(0);
							tx.setTeamnum(-1);
							result = true;
							ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.TANXIAN_END, 1);
						}
						break;
					}
				}
			}
		}
		if(result){
			//是否全完成并刷新
			boolean isAllFinish = true;
			for(xbean.tanxian txnew : this.xcol.getStagetxallmap().get(init.getChapterID()).getStagetanxian()){
				if(txnew.getTanxiantype() != 2){
					isAllFinish = false;
					break;
				}
			}
			if(isAllFinish){
				this.refreshTask(init.getChapterID(), false);
			}
			List<Integer> dropList = new java.util.LinkedList<Integer>();
			if( !isCost ){
				dropList = DropManager.getInstance().drop(roleId, String.valueOf(init.getBonus()),LogBehavior.TANXIANENDGET);
			}
			this.sSRefreshTanXian();
			this.sendSTanXianOther(dropList, tanxianId, endtype);
		}
		return result;
	}
	
	/**
	 * 英雄是否在探险队伍中
	 * @param herokey
	 * @return
	 */
	public boolean isHeroTanXian(int herokey){
		for(Map.Entry<Integer, xbean.teamtanxian> entry : this.xcol.getTeamallmap().entrySet()){
			if(entry.getValue().getTeam().contains(herokey)){
				return true;
			}
		}
		return false;
	}
	/**
	 * 英雄是否在探险队伍中
	 * @param team
	 * @return
	 */
	public boolean isHeroTanXian(List<Integer> team){
		for(Integer herokey : team){
			if( isHeroTanXian(herokey) ){
				return true;
			}
		}
		return false;
	}
	/**
	 * 获取空队伍
	 * @return
	 */
	public int getEmptyTeam(){
		int allTeam = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1118).getConfigvalue());
		for(int i = 1;i<= allTeam;i++){
			xbean.teamtanxian team = this.xcol.getTeamallmap().get(i);
			if(team == null){
				team = xbean.Pod.newteamtanxian();
				this.xcol.getTeamallmap().put(i, team);
				return i;
			}else if(team.getTeam().size() == 0){
				return i;
			}
		}
		return -1;
	}
	
	/**
	 * 探险开始入口
	 * @param team
	 * @param tanxianid
	 * @return
	 */
	public boolean tanxianBeginEntry(java.util.LinkedList<Integer> team,int tanxianid){
		explorequest46 init = ConfigManager.getInstance().getConf(explorequest46.class).get(tanxianid);
		if(init == null){
			return false;
		}
		if( isHeroTanXian(team) ){
			return false;
		}

		for(xbean.tanxian tx : this.xcol.getStagetxallmap().get(init.getChapterID()).getStagetanxian()){
			if(tx.getTanxianid() == tanxianid){
				if(tx.getTanxiantype() == 0){
					if( heroIsOk(team,init) ){
						int teamid = this.getEmptyTeam();
						if(teamid != -1){
							chuhan.gsp.attr.PropRole prole = chuhan.gsp.attr.PropRole.getPropRole(roleId, false);
							if( !prole.useTXTili(init.getCost()) ){
								return false;
							}
							xbean.teamtanxian xteam = this.xcol.getTeamallmap().get(teamid);
							xteam.setTanxianid(tanxianid);
							xteam.getTeam().addAll(team);
							long now = chuhan.gsp.main.GameTime.currentTimeMillis();
							tx.setTanxiantype(1);
							tx.setEndtime(now + init.getTime()*DateUtil.minuteMills);
							tx.setTeamnum(teamid);
							this.sSRefreshTanXian();
							this.sendSTanxianBegin(team, tanxianid, tx.getEndtime());
//							TroopColumn troopcol = TroopColumn.getTroopColumn(roleId, false);
//							troopcol.HeroOutAllTroop(team);
							return true;
						}
					}
				}
			}
		}
		return false;
	}
	
	/**
	 * 英雄列表是否符合需求
	 * @param team
	 * @param init
	 * @return
	 */
	public boolean heroIsOk(java.util.LinkedList<Integer> team,explorequest46 init){
		HeroColumn herocol = HeroColumn.getHeroColumn(roleId, false);
		if(init.getNeedHeroType() == 1){
			if( team.size() != init.getNeedNum() ){
				return false;
			}
			List<Integer> camp = ParserString.parseString2Int(init.getNeedHeroCamp());
//			List<Integer> type1 = ParserString.parseString2Int(init.getNeedHeroType1());
//			List<Integer> type2 = ParserString.parseString2Int(init.getNeedHeroType2());
//			List<Integer> type3 = ParserString.parseString2Int(init.getNeedHeroType3());
			for(Integer herokey : team){
				Hero hero = herocol.getHByHKey(herokey);
				if(hero == null){
					return false;
				}
				if(hero.getLevel() < init.getNeedHeroLevel() || hero.getiHeroInfo().getMaxQuality() < init.getNeedHeroStar()){
					return false;
				}
				if( !camp.contains(hero.getiHeroInfo().getCamp()) ){
					return false;
				}
/*				List<Integer> herotype = ParserString.parseString2Int(hero.getiHeroInfo().getClientSignType());
				if(herotype.size() < 3){
					return false;
				}
				if( !type1.contains(herotype.get(0)) || !type2.contains(herotype.get(1)) || !type3.contains(herotype.get(2)) ){
					return false;
				}*/
			}
			
		}else{
			int neednum = 0;
			List<Integer> needHero1 = null,needHero2 = null,needHero3 = null,needHero4 = null,needHero5= null;
			if( !init.getNeedHeroID1().equals("-1") ){
				neednum = 1;
				needHero1 = ParserString.parseString2Int(init.getNeedHeroID1());
			}
			if( !init.getNeedHeroID2().equals("-1") ){
				neednum = 2;
				needHero2 = ParserString.parseString2Int(init.getNeedHeroID2());
			}
			if( !init.getNeedHeroID3().equals("-1") ){
				neednum = 3;
				needHero3 = ParserString.parseString2Int(init.getNeedHeroID3());
			}
			if( !init.getNeedHeroID4().equals("-1") ){
				neednum = 4;
				needHero4 = ParserString.parseString2Int(init.getNeedHeroID4());
			}
			if( !init.getNeedHeroID5().equals("-1") ){
				neednum = 5;
				needHero5 = ParserString.parseString2Int(init.getNeedHeroID5());
			}
			if( team.size() != neednum || neednum == 0 ){
				return false;
			}
			
			for(int i = 0;i< neednum;i++){
				Hero hero = herocol.getHByHKey(team.get(i));
				if(hero == null){
					return false;
				}
				if(hero.getLevel() < init.getNeedHeroLevel() || hero.getiHeroInfo().getMaxQuality() < init.getNeedHeroStar()){
					return false;
				}
				switch(i){
				case 0:
					if( needHero1 == null || !needHero1.contains(hero.getiHeroInfo().getId()) ){
						return false;
					}
					continue;
				case 1:
					if( needHero2 == null || !needHero2.contains(hero.getiHeroInfo().getId()) ){
						return false;
					}
					continue;
				case 2:
					if( needHero3 == null || !needHero3.contains(hero.getiHeroInfo().getId()) ){
						return false;
					}
					continue;
				case 3:
					if( needHero4 == null || !needHero4.contains(hero.getiHeroInfo().getId()) ){
						return false;
					}
					continue;
				case 4:
					if( needHero5 == null || !needHero5.contains(hero.getiHeroInfo().getId()) ){
						return false;
					}
					continue;
				}
			}
		}
		return true;
	}
	
	/**
	 * 刷新列表
	 * @param chapterID
	 * @param isTodayFirst
	 * @return
	 */
	public boolean refreshTask(int chapterID,boolean isTodayFirst){
		if(chapterID == ERROR_STAGEID){
			return false;
		}
		if(this.xcol.getStagetxallmap().get(chapterID) == null){
			xbean.stagetanxian stagetx = xbean.Pod.newstagetanxian();
			this.xcol.getStagetxallmap().put(chapterID, stagetx);
		}
		if(isTodayFirst){
			this.xcol.getStagetxallmap().get(chapterID).getStagetanxian().clear();
		}else{
			for(int i = this.xcol.getStagetxallmap().get(chapterID).getStagetanxian().size() - 1;i>=0;i--){
				xbean.tanxian tx = this.xcol.getStagetxallmap().get(chapterID).getStagetanxian().get(i);
				if(tx.getTanxiantype() != 1){
					this.xcol.getStagetxallmap().get(chapterID).getStagetanxian().remove(i);
				}
			}
		}
		int allNum = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1119).getConfigvalue());
		if(this.xcol.getStagetxallmap().get(chapterID).getStagetanxian().size() >= allNum){
			return false;
		}
		Map<Integer,DropInit> initMap = this.getInitMap(chapterID,
				this.xcol.getStagetxallmap().get(chapterID).getStagetanxian());
		List<Integer> result = this.getDropList(initMap, allNum - this.xcol.getStagetxallmap().get(chapterID).
				getStagetanxian().size());
		for(Integer txId : result){
			if(txId == -1){
				continue;
			}
			xbean.tanxian tx = xbean.Pod.newtanxian();
			tx.setTanxianid(txId);
			tx.setTanxiantype(0);
			this.xcol.getStagetxallmap().get(chapterID).getStagetanxian().add(tx);
		}
		return true;
	}
	
	/**
	 * 获取抽奖后ID列表
	 * @param dropMap
	 * @param oddsAddMap
	 * @param num
	 * @return
	 */
	public List<Integer> getDropList(Map<Integer,DropInit> dropMap,int num){
		List<Integer> result = DropManager.getInstance().getDropIdList(dropMap, num);	
		return result;
	}
	
	/**
	 * 根据类型获得基础掉落数据
	 * @param chapterID
	 * @param haveList
	 * @return
	 */
	public Map<Integer,DropInit> getInitMap(int chapterID,List<xbean.tanxian> haveList){
		Map<Integer,DropInit> resultMap = new HashMap<Integer,DropInit>();
		TreeMap<Integer, explorequest46> initMap = ConfigManager.getInstance().getConf(explorequest46.class);
		for(Map.Entry<Integer, explorequest46> entry : initMap.entrySet()){
			if( entry.getValue().getChapterID() == chapterID ){
				boolean isHave = false;
				for(xbean.tanxian tx : haveList){
					explorequest46 init = ConfigManager.getInstance().getConf(explorequest46.class).get(tx.getTanxianid());
					if(init.getType() == entry.getValue().getType()){
						isHave = true;
						break;
					}
				}
				if(isHave){
					continue;
				}
				DropInit drop = new DropInit(entry.getValue().getWeight(),entry.getValue().getId(),1);
				drop.sameType = entry.getValue().getType();
				resultMap.put(resultMap.size(), drop);
			}
		}
		return resultMap;
	}
	
	/**
	 * 发送探险开始成功消息
	 * @param team
	 * @param tanxianId
	 * @param endTime
	 */
	public void sendSTanxianBegin(List<Integer> team,int tanxianId,long endTime){
		STanxianBegin snd = new STanxianBegin();
		snd.result = STanxianBegin.END_OK;
		snd.team.addAll(team);
		snd.tanxianid = tanxianId;
		snd.endtime = endTime;
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	
	/**
	 * 发送探险其他信息
	 * @param dropList
	 * @param tanxianId
	 * @param endtype
	 */
	public void sendSTanXianOther(List<Integer> dropList,int tanxianId,int endtype){
		STanXianOther snd = new STanXianOther();
		snd.result = STanxianBegin.END_OK;
		if(dropList != null){
			snd.dropidlist.addAll(dropList);
		}
		snd.tanxianid = tanxianId;
		snd.endtype = endtype;
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}

	/**
	 * 根据数据库内容生成消息信息
	 * @return
	 */
	private chuhan.gsp.play.tanxian.stagetxall getReturn(){
		chuhan.gsp.play.tanxian.stagetxall result = new chuhan.gsp.play.tanxian.stagetxall();
		for( Map.Entry<Integer, xbean.stagetanxian> entry : this.xcol.getStagetxallmap().entrySet() ){
			stagetanxian newstagetx = new stagetanxian();
			for( xbean.tanxian tx : entry.getValue().getStagetanxian() ){
				tanxian newtx = new tanxian();
				newtx.tanxianid = tx.getTanxianid();
				newtx.tanxiantype = tx.getTanxiantype();
				newtx.teamnum = tx.getTeamnum();
				newtx.endtime = tx.getEndtime();
				newstagetx.stagetanxian.add(newtx);
			}
			result.stagetxallmap.put(entry.getKey(), newstagetx);
		}
		for( Map.Entry<Integer, xbean.teamtanxian> entry : this.xcol.getTeamallmap().entrySet() ){
			teamtanxian teamtx = new teamtanxian();
			teamtx.tanxianid = entry.getValue().getTanxianid();
			teamtx.team.addAll(entry.getValue().getTeam());
			result.teamallmap.put(entry.getKey(), teamtx);
		}
		
		return result;
	}
	
	/**
	 * 刷新抽奖信息
	 */
	public void sSRefreshTanXian(){
		SRefreshTanXian snd = new SRefreshTanXian();
		snd.tanxianinfo = this.getReturn();
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
}
