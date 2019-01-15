package chuhan.gsp.play.activity;

import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;

import org.apache.log4j.Logger;

import chuhan.gsp.GAMEACTIVITY;
import chuhan.gsp.award.DropManager;
import chuhan.gsp.award.IDManager;
import chuhan.gsp.game.gameactivity61;
import chuhan.gsp.item.BagTypes;
import chuhan.gsp.item.hero01;
import chuhan.gsp.item.item26;
import chuhan.gsp.log.LogBehavior;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.task.monster13;
import chuhan.gsp.task.stage11;
import chuhan.gsp.util.DateUtil;


public class ActivityGameManager{
	
	public static Logger logger = Logger.getLogger(ActivityGameManager.class);
	static private ActivityGameManager instance = null;
	
	private ActivityGameManager(){}
	public static ActivityGameManager getInstance() {
		if(instance == null)
		{
			instance = new ActivityGameManager();
		}
		return instance;
	}
	//充值相关
	private int[] cz_array = {GAMEACTIVITY.CZ_FIRST_HISTORY,GAMEACTIVITY.CZ_FIRST_HISTORY2,
			GAMEACTIVITY.CZ_FIRST_INTIME,GAMEACTIVITY.CZ_FIRST_INTIME2,GAMEACTIVITY.CZ_ALL,
			GAMEACTIVITY.CZ_EVERYDAY,GAMEACTIVITY.CZ_GUDING};
	//消费元宝
	private int[] xfyb_array = {GAMEACTIVITY.XFYB_ALL,GAMEACTIVITY.XFYB_EVERYDAY,
			GAMEACTIVITY.XFYB_GUDING};
	//消费体力
	private int[] xfti_array = {GAMEACTIVITY.XFTI_ALL,GAMEACTIVITY.XFTI_EVERYDAY,
			GAMEACTIVITY.XFTI_GUDING};
	//登录
	private int[] dl_array = {GAMEACTIVITY.DL_EVERYDAY,GAMEACTIVITY.DL_INDAY,
			GAMEACTIVITY.DL_INTIME};
	//杀怪
	private int[] kill_array = {GAMEACTIVITY.KILL_MONSTER};
	//通关
	private int[] passbattle_array = {GAMEACTIVITY.PASS_BATTLE};
	//全服经验
	private int[] qf_exp_array = {GAMEACTIVITY.ALL_EXPBUFF};
	//全服金币
	private int[] qf_gold_array = {GAMEACTIVITY.ALL_GOLDBUFF};
	//全服英雄掉率
	private int[] qf_hero_array = {GAMEACTIVITY.ALL_HERODROP};
	//全服物品掉率
	private int[] qf_item_array = {GAMEACTIVITY.ALL_ITEMDROP};
	//全服关卡掉率
	private int[] qf_battle_array = {GAMEACTIVITY.ALL_TESHUBATTLE,GAMEACTIVITY.ALL_BOSSBATTLE,
			GAMEACTIVITY.ALL_INBATTLE};
	//人物升级
	private int[] level_array = {GAMEACTIVITY.LEVEL_BEGIN,GAMEACTIVITY.LEVEL_UP};
	//熔炼
	private int[] ronglian_array = {GAMEACTIVITY.RONGLIAN_STAR,GAMEACTIVITY.RONGLIAN_ALL,
			GAMEACTIVITY.RONGLIAN_ID,GAMEACTIVITY.RONGLIAN_NUM};
	//收集符文
	private int[] getitem_array = {GAMEACTIVITY.GET_ITEM_STAR,GAMEACTIVITY.GET_ITEM_ALL,
			GAMEACTIVITY.GET_ITEM_ID};
	//鉴定符文
	private int[] jditem_array = {GAMEACTIVITY.JD_ITEM_STAR,GAMEACTIVITY.JD_ITEM_ALL,
			GAMEACTIVITY.JD_ITEM_ID};
	//强化符文
	private int[] qhitem_array = {GAMEACTIVITY.QH_ITEM_STAR,GAMEACTIVITY.QH_ITEM_ALL,
			GAMEACTIVITY.QH_ITEM_ID};
	//招募英雄
	private int[] zmhero_array = {GAMEACTIVITY.ZM_HERO_ALL,GAMEACTIVITY.ZM_HERO_STAR,
			GAMEACTIVITY.ZM_HERO_ZHENYING,GAMEACTIVITY.ZM_HERO_ID};
	//收集英雄
	private int[] gethero_array = {GAMEACTIVITY.GET_HERO_ALL,GAMEACTIVITY.GET_HERO_STAR,
			GAMEACTIVITY.GET_HERO_ZHENYING,GAMEACTIVITY.GET_HERO_ID};
	//英雄升级
	private int[] herolevel_array = {GAMEACTIVITY.HERO_LEVEL,GAMEACTIVITY.HERO_LEVEL_STAR,
			GAMEACTIVITY.HERO_LEVEL_ZHENYING,GAMEACTIVITY.HERO_LEVEL_ID};
	//遗迹宝藏
	private int[] zmitem_array = {GAMEACTIVITY.ZM_ITEM_ALL};
	//招募英雄折扣
	private int[] zmherooff_array = {GAMEACTIVITY.ZM_HERO_OFF};
	
	public static final int ITEM_RONGLIAN = 1;	//熔炼符文
	public static final int ITEM_GET = 2;		//收集符文
	public static final int ITEM_JD = 3;		//鉴定符文
	public static final int ITEM_QH = 4;		//强化符文	
	
	public static final int XF_YUANBAO = 5;		//消费元宝
	public static final int XF_TILI = 6;		//消费体力
	
	public static final int KILL_MOSTER = 7;	//杀怪
	public static final int PASS_BATTLE = 8;	//通关
	
	public static final int HERO_ZM = 9;		//招募英雄
	public static final int HERO_GET = 10;		//收集英雄
	public static final int HERO_LEVEL = 11;	//英雄升级
	
	/**
	 * 获取人物活动列表数据库数据
	 * @param roleId
	 * @param readonly
	 * @return
	 */
	public static xbean.gameactivitys getxGameActivitys(long roleId, boolean readonly){
		xbean.gameactivitys xresult;
		if(readonly){
			if(xtable.Properties.select(roleId) == null)
				return null;
			xresult = xtable.Gameactivitylist.select(roleId);
		}else{
			if(xtable.Properties.get(roleId) == null)
				return null;
			xresult = xtable.Gameactivitylist.get(roleId);
		}
		if(xresult == null){
			if(readonly)
				xresult = xbean.Pod.newgameactivitysData();
			else{
				xresult = xbean.Pod.newgameactivitys();
				xtable.Gameactivitylist.insert(roleId, xresult);
			}
		}
		return xresult;
	}
	/**
	 * 获得活动具体数据
	 * @param xactivitys
	 * @param key
	 * @return
	 */
	public xbean.gameactivity getxActivitys(xbean.gameactivitys xactivitys ,int key){
		xbean.gameactivity result = xactivitys.getGameactivitymap().get(key);
		if(result == null){
			result = xbean.Pod.newgameactivity();
			result.setId(key);
			xactivitys.getGameactivitymap().put(key, result);
		}
		return result;
	}
	/**
	 * 根据类型获取活动配表list
	 * @param inarray
	 * @param now
	 * @return
	 */
	private List<gameactivity61> getInitList(int[] inarray,long now){
		List<gameactivity61> result = new LinkedList<gameactivity61>();
		TreeMap<Integer,gameactivity61> initMap = ConfigManager.getInstance().getConf(gameactivity61.class);
		for(int i = 0; i < inarray.length ; i++){
			for(Map.Entry<Integer, gameactivity61> entry : initMap.entrySet()){
				if(entry.getValue().getType() == inarray[i]){
					if(DateUtil.isRunning(now, entry.getValue().getBeginday(), entry.getValue().getDeadline()
							, "yyyyMMddHHmmss")){
						result.add(entry.getValue());
					}
				}
			}
		}
		return result;
	}
	/**
	 * 根据活动分组获取活动配表list
	 * @param teamId
	 * @param now
	 * @return
	 */
	private List<gameactivity61> getInitListByTeam(int teamId,long now){
		List<gameactivity61> result = new LinkedList<gameactivity61>();
		TreeMap<Integer,gameactivity61> initMap = ConfigManager.getInstance().getConf(gameactivity61.class);
		for(Map.Entry<Integer, gameactivity61> entry : initMap.entrySet()){
			if(entry.getValue().getTeam() == teamId){
				if(DateUtil.isRunning(now, entry.getValue().getBeginday(), entry.getValue().getDeadline()
						, "yyyyMMddHHmmss")){
					result.add(entry.getValue());
				}
			}
		}
		return result;
	}
	/**
	 * 记录已看分组信息
	 * @param roleId
	 * @param teamId
	 */
	public void activityIsSee(long roleId,int teamId){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		xbean.gameactivitys xactList = this.getxGameActivitys(roleId, false);
		
		List<gameactivity61> initList = this.getInitListByTeam(teamId, now);
		boolean isChange = false;
		for(gameactivity61 init : initList){
			xbean.gameactivity xact = this.getxActivitys(xactList, init.getId());
			if( xact.getIssee() != 1 ){
				xact.setIssee(1);
				isChange = true;
			}
		}
		if(isChange){
			this.sRefreshGameAct(roleId, xactList);
		}
	}
	/**
	 * 判断任务是否已经完成
	 * @param init
	 * @param xactivity
	 * @param now
	 * @return
	 */
	public boolean isAllFinish(gameactivity61 init,xbean.gameactivity xactivity,long now){
		if( init.getPeriodmax() == -1 && init.getDaymax() == -1){
			return false;
		}
		if( init.getPeriodmax() != -1 && init.getPeriodmax() <= xactivity.getAllnum() ){
			return true;
		}
		if( init.getDaymax() != -1 ){
			if( !DateUtil.inTheSameDay(now, xactivity.getTime()) ){
				xactivity.setTime(now);
				xactivity.setTodaynum(0);
				xactivity.setActivitynum(0);
			}
			if( init.getDaymax() <= xactivity.getTodaynum() ){
				return true;
			}
		}
		return false;
	}
	/**
	 * 增加次数，计算次数
	 * @param xactivity
	 */
	private void addNum(xbean.gameactivity xactivity){
		xactivity.setTodaynum(xactivity.getTodaynum() + 1);
		xactivity.setAllnum(xactivity.getAllnum() + 1);
		xactivity.setCangetnum(xactivity.getCangetnum() + 1);
	}
	/**
	 * 领取活动数据奖励入口
	 * @param roleId
	 * @param actId
	 * @return
	 */
	public boolean getGameActEntry(long roleId,int actId){
		xbean.gameactivitys xactList = this.getxGameActivitys(roleId, false);
		xbean.gameactivity xact = xactList.getGameactivitymap().get(actId);
		boolean isChange = false;
		if(xact != null && xact.getCangetnum() > 0){
			gameactivity61 actInit = ConfigManager.getInstance().getConf(gameactivity61.class).get(actId);
			if(actInit != null){
				if(actInit.getType() == GAMEACTIVITY.CZ_FIRST_HISTORY ||
						actInit.getType() == GAMEACTIVITY.CZ_FIRST_INTIME ){
					DropManager.getInstance().dropAddByOther(IDManager.YUANBAO, 
							xact.getAllactivitynum() * xact.getCangetnum(), 
							0, 0, roleId, LogBehavior.GAMEACT);
				}else{
					DropManager.getInstance().drop(roleId, actInit.getDrop(), LogBehavior.GAMEACT,
							xact.getCangetnum());
				}
				xact.setCangetnum(0);
				isChange = true;	
			}
		}
		if(isChange){
			this.sRefreshSingleGameAct(roleId, xact);
			return true;
		}
		return false;
	}
	
	/**
	 * 登录活动统计
	 * @param roleId
	 */
	public void addDLActivity(long roleId){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		xbean.gameactivitys xactList = this.getxGameActivitys(roleId, false);
		
		List<gameactivity61> initList = this.getInitList(dl_array, now);
		boolean isChange = false;
		for(gameactivity61 init : initList){
			xbean.gameactivity xact = this.getxActivitys(xactList, init.getId());
			if( !this.isAllFinish(init, xact, now) ){
				switch(init.getType()){
				case GAMEACTIVITY.DL_EVERYDAY:
				case GAMEACTIVITY.DL_INDAY:
				case GAMEACTIVITY.DL_INTIME:
					xact.setActivitynum(xact.getActivitynum() + 1);
					xact.setAllactivitynum(xact.getAllactivitynum() + 1);
					this.addNum(xact);
					isChange = true;
					break;
				}
			}
		}
		if(isChange){
			this.sRefreshGameAct(roleId, xactList);
		}
	}
	/**
	 * 遗迹宝藏计数活动
	 * @param roleId
	 * @param num
	 */
	public void addZMItemActivity(long roleId,int num){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		xbean.gameactivitys xactList = this.getxGameActivitys(roleId, false);
		
		List<gameactivity61> initList = this.getInitList(zmitem_array, now);
		boolean isChange = false;
		for(gameactivity61 init : initList){
			xbean.gameactivity xact = this.getxActivitys(xactList, init.getId());
			if( !this.isAllFinish(init, xact, now) ){
				int getNum = xact.getAllactivitynum() / init.getParameter1();
				xact.setActivitynum(xact.getActivitynum() + num);
				xact.setAllactivitynum(xact.getAllactivitynum() + num);
				int haveNum = xact.getAllactivitynum() / init.getParameter1();
				if( haveNum > getNum ){
					//循环增加次数
					for(int i = 0;i < haveNum - getNum;i++){
						isChange = true;
						this.addNum(xact);
						//是否超出界限
						if(this.isAllFinish(init, xact, now)){
							break;
						}
					}
				}
			}
		}
		if(isChange){
			this.sRefreshGameAct(roleId, xactList);
		}
	}
	/**
	 * 人物等级活动统计
	 * @param roleId
	 * @param level
	 */
	public void addRoleLevelActivity(long roleId,int level){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		xbean.gameactivitys xactList = this.getxGameActivitys(roleId, false);
		List<gameactivity61> initList = this.getInitList(level_array, now);
		boolean isChange = false;
		for(gameactivity61 init : initList){
			xbean.gameactivity xact = this.getxActivitys(xactList, init.getId());
			if( !this.isAllFinish(init, xact, now) ){
				switch(init.getType()){
				case GAMEACTIVITY.LEVEL_BEGIN:
				case GAMEACTIVITY.LEVEL_UP:
					if(init.getParameter1() <= level)
					this.addNum(xact);
					isChange = true;
					break;
				}
			}
		}
		if(isChange){
			this.sRefreshGameAct(roleId, xactList);
		}
	}
	
	/**
	 * 充值活动统计
	 * @param roleId
	 * @param rmb
	 * @param yb
	 */
	public void addCZActivity(long roleId,int rmb,int yb){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		xbean.gameactivitys xactList = this.getxGameActivitys(roleId, false);
		
		List<gameactivity61> initList = this.getInitList(cz_array, now);
		boolean isChange = false;
		for(gameactivity61 init : initList){
			xbean.gameactivity xact = this.getxActivitys(xactList, init.getId());
			if( !this.isAllFinish(init, xact, now) ){
				switch(init.getType()){
				case GAMEACTIVITY.CZ_FIRST_HISTORY:
				case GAMEACTIVITY.CZ_FIRST_INTIME:
					xact.setActivitynum(xact.getActivitynum() + yb);
					xact.setAllactivitynum(xact.getAllactivitynum() + yb);
					this.addNum(xact);
					isChange = true;
					break;
				case GAMEACTIVITY.CZ_FIRST_HISTORY2:
				case GAMEACTIVITY.CZ_FIRST_INTIME2:
					if( rmb >= init.getParameter1() ){	
						this.addNum(xact);
						isChange = true;
					}else{
//						xact.setTodaynum(xact.getTodaynum() + 1);
						xact.setAllnum(xact.getAllnum() + 1);
					}
					xact.setActivitynum(xact.getActivitynum() + rmb);
					xact.setAllactivitynum(xact.getAllactivitynum() + rmb);
					break;
				case GAMEACTIVITY.CZ_ALL:
					xact.setActivitynum(xact.getActivitynum() + rmb);
					xact.setAllactivitynum(xact.getAllactivitynum() + rmb);
					if(xact.getAllactivitynum() >= init.getParameter1()){
						this.addNum(xact);
						isChange = true;
					}
					break;
				case GAMEACTIVITY.CZ_EVERYDAY:
					xact.setActivitynum(xact.getActivitynum() + rmb);
					xact.setAllactivitynum(xact.getAllactivitynum() + rmb);
					if(xact.getActivitynum() >= init.getParameter1()){
						if(xact.getActivitynum() > init.getParameter1() * init.getDaymax()){
							xact.setActivitynum(init.getParameter1() * init.getDaymax());
						}
						this.addNum(xact);
						isChange = true;
					}
					break;
				case GAMEACTIVITY.CZ_GUDING:
					int getNum = xact.getAllactivitynum() / init.getParameter1();
					xact.setActivitynum(xact.getActivitynum() + rmb);
					xact.setAllactivitynum(xact.getAllactivitynum() + rmb);
					int haveNum = xact.getAllactivitynum() / init.getParameter1();
					if( haveNum > getNum ){
						//循环增加次数
						for(int i = 0;i < haveNum - getNum;i++){
							isChange = true;
							this.addNum(xact);
							//是否超出界限
							if(this.isAllFinish(init, xact, now)){
								break;
							}
						}
					}
				}
			}
		}
		if(isChange){
			this.sRefreshGameAct(roleId, xactList);
		}
	}
	/**
	 * 物品相关活动统计入口
	 * @param roleId
	 * @param iteminit
	 * @param type
	 * @param rongliannum
	 */
	public void addItemActivity(long roleId,item26 iteminit,int type,int rongliannum){
		if(iteminit.getBag() != BagTypes.EQUIP){
			return;
		}
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		xbean.gameactivitys xactList = this.getxGameActivitys(roleId, false);
		List<gameactivity61> initList = null;
		switch(type){
		case ITEM_RONGLIAN:
			initList = this.getInitList(ronglian_array, now);
			break;
		case ITEM_GET:
			initList = this.getInitList(getitem_array, now);
			break;
		case ITEM_JD:
			initList = this.getInitList(jditem_array, now);
			break;
		case ITEM_QH:
			initList = this.getInitList(qhitem_array, now);
			break;
		}
		boolean isChange = false;
		if(initList != null){
			isChange = this.actItem(xactList, now, initList, iteminit, rongliannum);
		}
		if(isChange){
			this.sRefreshGameAct(roleId, xactList);
		}

	}
	/**
	 * 物品相关活动统计
	 * @param xactList
	 * @param now
	 * @param initList
	 * @param iteminit
	 * @param rongliannum
	 * @return
	 */
	private boolean actItem(xbean.gameactivitys xactList,long now,List<gameactivity61> initList,item26 iteminit,
			int rongliannum){
		boolean isChange = false;
		for(gameactivity61 init : initList){
			xbean.gameactivity xact = this.getxActivitys(xactList, init.getId());
			if( !this.isAllFinish(init, xact, now) ){
				switch(init.getType()){
				case GAMEACTIVITY.RONGLIAN_STAR:
				case GAMEACTIVITY.GET_ITEM_STAR:
				case GAMEACTIVITY.JD_ITEM_STAR:
				case GAMEACTIVITY.QH_ITEM_STAR:
					if( iteminit.getRune_quality() == init.getParameter1() ){
						xact.setActivitynum(xact.getActivitynum() + 1);
						xact.setAllactivitynum(xact.getAllactivitynum() + 1);
						if(xact.getActivitynum() >= init.getParameter2()){
							xact.setActivitynum(xact.getActivitynum() - init.getParameter2());
							this.addNum(xact);
						}
						isChange = true;
					}
					break;
				case GAMEACTIVITY.RONGLIAN_ID:
				case GAMEACTIVITY.GET_ITEM_ID:
				case GAMEACTIVITY.JD_ITEM_ID:
				case GAMEACTIVITY.QH_ITEM_ID:
					if( iteminit.getId() == init.getParameter1() ){
						xact.setActivitynum(xact.getActivitynum() + 1);
						xact.setAllactivitynum(xact.getAllactivitynum() + 1);
						if(xact.getActivitynum() >= init.getParameter2()){
							xact.setActivitynum(xact.getActivitynum() - init.getParameter2());
							this.addNum(xact);
						}
						isChange = true;
					}
					break;
				case GAMEACTIVITY.RONGLIAN_ALL:
					xact.setActivitynum(xact.getActivitynum() + 1);
					xact.setAllactivitynum(xact.getAllactivitynum() + 1);
					if(xact.getActivitynum() >= init.getParameter2()){
						xact.setActivitynum(xact.getActivitynum() - init.getParameter2());
						this.addNum(xact);
					}
					isChange = true;
					break;
				case GAMEACTIVITY.GET_ITEM_ALL:
				case GAMEACTIVITY.JD_ITEM_ALL:
				case GAMEACTIVITY.QH_ITEM_ALL:
					xact.setActivitynum(xact.getActivitynum() + 1);
					xact.setAllactivitynum(xact.getAllactivitynum() + 1);
					if(xact.getActivitynum() >= init.getParameter1()){
						xact.setActivitynum(xact.getActivitynum() - init.getParameter1());
						this.addNum(xact);
					}
					isChange = true;
					break;
				case GAMEACTIVITY.RONGLIAN_NUM:
					xact.setActivitynum(xact.getActivitynum() + rongliannum);
					xact.setAllactivitynum(xact.getAllactivitynum() + rongliannum);
					if(xact.getActivitynum() >= init.getParameter1()){
						xact.setActivitynum(xact.getActivitynum() - init.getParameter1());
						this.addNum(xact);
						isChange = true;
					}
					break;
				}
			}
		}
		return isChange;
	}
	
	/**
	 * 英雄相关活动统计入口
	 * @param roleId
	 * @param heroId
	 * @param level
	 * @param type
	 */
	public void addHeroActivity(long roleId,int heroId,int level,int type){
		hero01 heroinit = ConfigManager.getInstance().getConf(hero01.class).get(heroId);
		if(heroinit == null){
			return;
		}
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		xbean.gameactivitys xactList = this.getxGameActivitys(roleId, false);
		List<gameactivity61> initList = null;
		switch(type){
		case HERO_ZM:
			initList = this.getInitList(zmhero_array, now);
			break;
		case HERO_GET:
			initList = this.getInitList(gethero_array, now);
			break;
		case HERO_LEVEL:
			initList = this.getInitList(herolevel_array, now);
			break;
		}
		boolean isChange = false;
		if(initList != null){
			isChange = this.actHero(xactList, now, initList, heroinit, level);
		}
		if(isChange){
			this.sRefreshGameAct(roleId, xactList);
		}

	}
	/**
	 * 英雄相关活动统计
	 * @param xactList
	 * @param now
	 * @param initList
	 * @param heroinit
	 * @param level
	 * @return
	 */
	private boolean actHero(xbean.gameactivitys xactList,long now,List<gameactivity61> initList,hero01 heroinit,
			int level){
		boolean isChange = false;
		for(gameactivity61 init : initList){
			xbean.gameactivity xact = this.getxActivitys(xactList, init.getId());
			if( !this.isAllFinish(init, xact, now) ){
				switch(init.getType()){
				case GAMEACTIVITY.ZM_HERO_STAR:
				case GAMEACTIVITY.GET_HERO_STAR:
					if( heroinit.getQuality() == init.getParameter1() ){
						xact.setActivitynum(xact.getActivitynum() + 1);
						xact.setAllactivitynum(xact.getAllactivitynum() + 1);
						if(xact.getActivitynum() >= init.getParameter2()){
							xact.setActivitynum(xact.getActivitynum() - init.getParameter2());
							this.addNum(xact);
						}
						isChange = true;
					}
					break;
				case GAMEACTIVITY.ZM_HERO_ID:
				case GAMEACTIVITY.GET_HERO_ID:
					if( heroinit.getId() == init.getParameter1() ){
						xact.setActivitynum(xact.getActivitynum() + 1);
						xact.setAllactivitynum(xact.getAllactivitynum() + 1);
						if(xact.getActivitynum() >= init.getParameter2()){
							xact.setActivitynum(xact.getActivitynum() - init.getParameter2());
							this.addNum(xact);
						}
						isChange = true;
					}
					break;
				case GAMEACTIVITY.ZM_HERO_ZHENYING:
				case GAMEACTIVITY.GET_HERO_ZHENYING:
					if( heroinit.getCamp() == init.getParameter1() ){
						xact.setActivitynum(xact.getActivitynum() + 1);
						xact.setAllactivitynum(xact.getAllactivitynum() + 1);
						if(xact.getActivitynum() >= init.getParameter2()){
							xact.setActivitynum(xact.getActivitynum() - init.getParameter2());
							this.addNum(xact);
						}
						isChange = true;
					}
					break;
				case GAMEACTIVITY.ZM_HERO_ALL:
				case GAMEACTIVITY.GET_HERO_ALL:
					xact.setActivitynum(xact.getActivitynum() + 1);
					xact.setAllactivitynum(xact.getAllactivitynum() + 1);
					if(xact.getActivitynum() >= init.getParameter1()){
						xact.setActivitynum(xact.getActivitynum() - init.getParameter1());
						this.addNum(xact);
					}
					isChange = true;
					break;
				case GAMEACTIVITY.HERO_LEVEL:
					if( level >= init.getParameter1() ){
						xact.setActivitynum(xact.getActivitynum() + 1);
						xact.setAllactivitynum(xact.getAllactivitynum() + 1);
						if(xact.getActivitynum() >= init.getParameter1()){
							xact.setActivitynum(xact.getActivitynum() - init.getParameter1());
							this.addNum(xact);
						}
						isChange = true;
					}
					break;
				case GAMEACTIVITY.HERO_LEVEL_STAR:
					if( level >= init.getParameter2() && heroinit.getQuality() == init.getParameter1() ){
						xact.setActivitynum(xact.getActivitynum() + 1);
						xact.setAllactivitynum(xact.getAllactivitynum() + 1);
						if(xact.getActivitynum() >= init.getParameter1()){
							xact.setActivitynum(xact.getActivitynum() - init.getParameter1());
							this.addNum(xact);
						}
						isChange = true;
					}
					break;
				case GAMEACTIVITY.HERO_LEVEL_ZHENYING:
					if( level >= init.getParameter2() && heroinit.getCamp() == init.getParameter1() ){
						xact.setActivitynum(xact.getActivitynum() + 1);
						xact.setAllactivitynum(xact.getAllactivitynum() + 1);
						if(xact.getActivitynum() >= init.getParameter1()){
							xact.setActivitynum(xact.getActivitynum() - init.getParameter1());
							this.addNum(xact);
						}
						isChange = true;
					}
					break;
				case GAMEACTIVITY.HERO_LEVEL_ID:
					if( level >= init.getParameter2() && heroinit.getId() == init.getParameter1() ){
						xact.setActivitynum(xact.getActivitynum() + 1);
						xact.setAllactivitynum(xact.getAllactivitynum() + 1);
						if(xact.getActivitynum() >= init.getParameter1()){
							xact.setActivitynum(xact.getActivitynum() - init.getParameter1());
							this.addNum(xact);
						}
						isChange = true;
					}
					break;
				}
			}
		}
		return isChange;
	}
	
	/**
	 * 消费相关活动统计入口
	 * @param roleId
	 * @param cost
	 * @param type
	 */
	public void addXFActivity(long roleId,int cost,int type){
		cost = Math.abs(cost);
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		xbean.gameactivitys xactList = this.getxGameActivitys(roleId, false);
		List<gameactivity61> initList = null;
		switch(type){
		case XF_YUANBAO:
			initList = this.getInitList(xfyb_array, now);
			break;
		case XF_TILI:
			initList = this.getInitList(xfti_array, now);
			break;
		}
		boolean isChange = false;
		if(initList != null){
			isChange = this.actXF(xactList, now, initList, cost);
		}
		if(isChange){
			this.sRefreshGameAct(roleId, xactList);
		}

	}
	/**
	 * 消费相关活动统计
	 * @param xactList
	 * @param now
	 * @param initList
	 * @param cost
	 * @return
	 */
	private boolean actXF(xbean.gameactivitys xactList,long now,List<gameactivity61> initList,int cost){
		boolean isChange = false;
		for(gameactivity61 init : initList){
			xbean.gameactivity xact = this.getxActivitys(xactList, init.getId());
			if( !this.isAllFinish(init, xact, now) ){
				switch(init.getType()){
				case GAMEACTIVITY.XFYB_ALL:
				case GAMEACTIVITY.XFTI_ALL:
					xact.setActivitynum(xact.getActivitynum() + cost);
					xact.setAllactivitynum(xact.getAllactivitynum() + cost);
					if( xact.getAllactivitynum() >= init.getParameter1() ){
						this.addNum(xact);
						isChange = true;
					}
					break;
				case GAMEACTIVITY.XFYB_EVERYDAY:
				case GAMEACTIVITY.XFTI_EVERYDAY:
					xact.setActivitynum(xact.getActivitynum() + cost);
					xact.setAllactivitynum(xact.getAllactivitynum() + cost);
					if(xact.getActivitynum() >= init.getParameter1()){
						this.addNum(xact);
						isChange = true;
					}
					break;
				case GAMEACTIVITY.XFYB_GUDING:
				case GAMEACTIVITY.XFTI_GUDING:
					int getNum = xact.getAllactivitynum() / init.getParameter1();
					xact.setActivitynum(xact.getActivitynum() + cost);
					xact.setAllactivitynum(xact.getAllactivitynum() + cost);
					int haveNum = xact.getAllactivitynum() / init.getParameter1();
					if( haveNum > getNum ){
						//循环增加次数
						for(int i = 0;i < haveNum - getNum;i++){
							isChange = true;
							this.addNum(xact);
							//是否超出界限
							if(this.isAllFinish(init, xact, now)){
								break;
							}
						}
					}
					break;
				}
			}
		}
		return isChange;
	}
	/**
	 * 通关相关活动统计入口
	 * @param roleId
	 * @param id
	 * @param num
	 */
	public void addBattleActivity(long roleId,int id,int num){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		xbean.gameactivitys xactList = this.getxGameActivitys(roleId, false);
		List<gameactivity61> initList = this.getInitList(passbattle_array, now);
		boolean isChange = false;
		if(initList != null){
			isChange = this.actKillAndBattle(xactList, now, initList, id,num);
		}
		if(isChange){
			this.sRefreshGameAct(roleId, xactList);
		}
	}
	/**
	 * 杀怪相关活动统计入口
	 * @param roleId
	 * @param monsterList
	 */
	public void addKillActivity(long roleId,List<Integer> monsterList){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		xbean.gameactivitys xactList = this.getxGameActivitys(roleId, false);
		List<gameactivity61> initList = this.getInitList(kill_array, now);

		boolean isChange = false;
		if(initList != null){
			for(Integer monsterId : monsterList){
				boolean result = this.actKillAndBattle(xactList, now, initList, monsterId,1);
				if(result){
					isChange = true;
				}
			}
		}
		if(isChange){
			this.sRefreshGameAct(roleId, xactList);
		}
	}
	/**
	 * 杀怪和通关相关活动统计
	 * @param xactList
	 * @param now
	 * @param initList
	 * @param id
	 * @param num
	 * @return
	 */
	private boolean actKillAndBattle(xbean.gameactivitys xactList,long now,List<gameactivity61> initList,int id,int num){
		boolean isChange = false;
		for(gameactivity61 init : initList){
			xbean.gameactivity xact = this.getxActivitys(xactList, init.getId());
			if( !this.isAllFinish(init, xact, now) ){
				switch(init.getType()){
				case GAMEACTIVITY.KILL_MONSTER:
					monster13 minit = (monster13) ConfigManager.getInstance().getConf(monster13.class).get(id);
					if(minit == null){
						return false;
					}
					//怪物为资源ID
					id = minit.getArtresources();
				case GAMEACTIVITY.PASS_BATTLE:
					if(init.getParameter1() == id){
						xact.setActivitynum(xact.getActivitynum() + num);
						xact.setAllactivitynum(xact.getAllactivitynum() + num);
						if( xact.getActivitynum() >= init.getParameter2() ){
							xact.setActivitynum( xact.getActivitynum() - init.getParameter2() );
							this.addNum(xact);
						}
						isChange = true;
					}
					break;
				}
			}
		}
		return isChange;
	}
	/**
	 * 招募英雄折扣
	 * @return
	 */
	public float getZMherodiscount(){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		List<gameactivity61> initList = this.getInitList(zmherooff_array, now);
		float result = 1.0f;
		for(gameactivity61 init : initList){
			if(init.getParameter1() == -1){
				continue;
			}
			float add = 1.0f - (float)init.getParameter1() / 100.0f;
			result = result * add;
		}
		return result;
	}
	
	/**
	 * 经验加成入口
	 * @return
	 */
	public float getExpAdd(){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		List<gameactivity61> initList = this.getInitList(qf_exp_array, now);
		return getfloatAdd(initList);
	}
	/**
	 * 金币加成入口
	 * @return
	 */
	public float getGoldAdd(){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		List<gameactivity61> initList = this.getInitList(qf_gold_array, now);
		return getfloatAdd(initList);
	}
	/**
	 * 掉落加成入口
	 * @param battleId
	 * @return
	 */
	public float[] dropAddArray(int battleId){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		float[] result = {1.0f,1.0f,1.0f};			//hero,item,num
		List<gameactivity61> initListhero = this.getInitList(qf_hero_array, now);
		result[0] = result[0] * getfloatAdd(initListhero);
		List<gameactivity61> initListitem = this.getInitList(qf_item_array, now);
		result[1] = result[1] * getfloatAdd(initListitem);
		
		stage11 battleinfo = ConfigManager.getInstance().getConf(stage11.class).get(battleId);
		if(battleinfo == null){
			return result;
		}
		List<gameactivity61> initListother = this.getInitList(qf_battle_array, now);
		result = this.getfloatAddByBattle(battleinfo, initListother, result);
		return result;
	}
	/**
	 * 加成计算
	 * @param initList
	 * @return
	 */
	private float getfloatAdd(List<gameactivity61> initList){
		float result = 1.0f;
		for(gameactivity61 init : initList){
			if(init.getParameter1() == -1){
				continue;
			}
			float add = 1.0f + (float)init.getParameter1() / 100.0f;
			result = result * add;
		}
		return result;
	}
	/**
	 * 其他加成计算
	 * @param battleinfo
	 * @param initList
	 * @param result
	 * @return
	 */
	private float[] getfloatAddByBattle(stage11 battleinfo,List<gameactivity61> initList,float[] result){
		for(gameactivity61 init : initList){
			
			float add1 = 1.0f + (float)init.getParameter1() / 100.0f;
			float add2 = 1.0f + (float)init.getParameter2() / 100.0f;
			if(init.getParameter1() == -1){
				add1 = 1.0f;
			}
			if(init.getParameter2() == -1){
				add2 = 1.0f;
			}
			
			switch(init.getType()){
			case GAMEACTIVITY.ALL_TESHUBATTLE:
				if(battleinfo.getStagetype() == 5){
					result[0] = result[0] * add1;
					result[1] = result[1] * add1;
					result[2] = result[2] * add2;
				}
				break;
			case GAMEACTIVITY.ALL_BOSSBATTLE:
				if(battleinfo.getIsBoss() == 1){
					result[0] = result[0] * add1;
					result[1] = result[1] * add1;
					result[2] = result[2] * add2;
				}
				break;
			case GAMEACTIVITY.ALL_INBATTLE:
//				if(battleinfo.getIsBoss() == 1){
					result[0] = result[0] * add1;
					result[1] = result[1] * add1;
					result[2] = result[2] * add2;
//				}
				break;
			}
		}
		return result;
	}
	
	/**
	 * 刷新活动数据信息
	 * @param roleId
	 */
	public void sRefreshGameAct(long roleId){
		xbean.gameactivitys xactList = this.getxGameActivitys(roleId, false);
		sRefreshGameAct(roleId,xactList);
	}
	/**
	 * 刷新活动数据信息
	 * @param roleId
	 * @param xactList
	 */
	private void sRefreshGameAct(long roleId,xbean.gameactivitys xactList){
		SRefreshGameAct snd = new SRefreshGameAct();
		for( Map.Entry<Integer, xbean.gameactivity> entry : xactList.getGameactivitymap().entrySet() ){
			gactivity gact = new gactivity();
			gact.id = entry.getValue().getId();
			gact.time = entry.getValue().getTime();
			gact.todaynum = entry.getValue().getTodaynum();
			gact.allnum = entry.getValue().getAllnum();
			gact.cangetnum = entry.getValue().getCangetnum();
			gact.activitynum = entry.getValue().getActivitynum();
			gact.allactivitynum = entry.getValue().getAllactivitynum();
			gact.issee = entry.getValue().getIssee();
			snd.gameactivitymap.put(gact.id, gact);
		}
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	/**
	 * 刷新单个活动数据信息
	 * @param roleId
	 * @param xact
	 */
	public void sRefreshSingleGameAct(long roleId,xbean.gameactivity xact){
		SRefreshSingleGameAct snd = new SRefreshSingleGameAct();
		snd.gameactivity.id = xact.getId();
		snd.gameactivity.time = xact.getTime();
		snd.gameactivity.todaynum = xact.getTodaynum();
		snd.gameactivity.allnum = xact.getAllnum();
		snd.gameactivity.cangetnum = xact.getCangetnum();
		snd.gameactivity.activitynum = xact.getActivitynum();
		snd.gameactivity.allactivitynum = xact.getAllactivitynum();
		snd.gameactivity.issee = xact.getIssee();
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
}
