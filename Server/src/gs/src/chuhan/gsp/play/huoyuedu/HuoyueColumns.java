package chuhan.gsp.play.huoyuedu;

import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;

import chuhan.gsp.attr.config10;
import chuhan.gsp.award.DropInit;
import chuhan.gsp.award.DropManager;
import chuhan.gsp.award.IDManager;
import chuhan.gsp.game.activitymission55;
import chuhan.gsp.game.herorecruit51;
import chuhan.gsp.log.LogBehavior;
import chuhan.gsp.log.Logger;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.Message;
import chuhan.gsp.play.activity.ActivityManager;
import chuhan.gsp.util.DateUtil;
import chuhan.gsp.util.ParserString;



public class HuoyueColumns {	
	public static Logger logger = Logger.getLogger(HuoyueColumns.class);
	
	public final static int MAP_SUPER = 1;
	public final static int MAP_20 = 20;
	public final static int MAP_15 = 15;
	public final static int MAP_10 = 10;
	
	public final static int BETTLE_BEGIN = 1;
	public final static int BETTLE_END = 2;
	public final static int MOHE_OPEN_ALL = 3;
	public final static int HERO_LEVELUP = 4;
	public final static int HERO_SKILL_UP = 5;
	public final static int HERO_RONGLING = 6;
	public final static int HERO_PEIYANG = 7;
	public final static int FUWEN_QIANGHUA = 8;
	public final static int FUWEN_JIANDING = 9;
	public final static int SHENQI_ZHUHUN = 10;
	public final static int TANXIAN_END = 11;
	public final static int TANXIAN_SPEEDUP = 12;
	public final static int SHOP_SM = 13;
	public final static int SHOP_SM_BUY = 14;
	public final static int LOTTERY = 15;
	public final static int YIJITANBAO = 16;
	public final static int SHOP_BUY_TI = 17;
	public final static int SHOP_BUY_GOLD = 18;
	public final static int ENDLESS_BEGIN = 19;
	public final static int ENDLESS_BUY = 20;
	public final static int MSZQ = 21;
	public final static int TODAY_SIGN = 22;
	public final static int QIYUAN = 23;
	public final static int WORLD_BOSS = 24;
	public final static int SHOP_BUY_XINGDONG = 25;
	public final static int FUWEN_RONGLIAN = 26;

	final public long roleId;
	final public xbean.huoyues xhuoyues;
	final boolean readonly;
	
	public static HuoyueColumns getHuoyueColumn(long roleId, boolean readonly){
		if(xtable.Properties.select(roleId) == null)
			throw new IllegalArgumentException("构造HuoyueColumns时，角色 "+roleId+" 不存在。");
		
		xbean.huoyues huoyuescol = null;
		if(readonly)
			huoyuescol = xtable.Huoyuelist.select(roleId);
		else
			huoyuescol = xtable.Huoyuelist.get(roleId);
		if(huoyuescol == null){
			if(readonly)
				huoyuescol = xbean.Pod.newhuoyuesData();
			else{
				huoyuescol = xbean.Pod.newhuoyues();
				xtable.Huoyuelist.insert(roleId, huoyuescol);
			}
		}
		return new HuoyueColumns(roleId, huoyuescol, readonly);
	}
	
	private HuoyueColumns(long roleId, xbean.huoyues xhuoyues, boolean readonly) {
		this.roleId = roleId;
		this.xhuoyues = xhuoyues;
		this.readonly = readonly;
		init();
	}
	public void init(){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		if( !DateUtil.inTheSameDay(now, this.xhuoyues.getHuoyuetime()) ){
			getHuoYueTask(now);
			this.sSRefreshHuoYue();
			ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.TODAY_SIGN, 1);
		}
	}
	/**
	 * 获得活跃度列表
	 * @param now
	 */
	public void getHuoYueTask(long now){
		Map<Integer,xbean.huoyue> isHave = new HashMap<Integer,xbean.huoyue>();
		int supertask = Integer.parseInt( ConfigManager.getInstance().getConf(config10.class).get(1277).configvalue );
		int task20 = Integer.parseInt( ConfigManager.getInstance().getConf(config10.class).get(1278).configvalue );
		int task15 = Integer.parseInt( ConfigManager.getInstance().getConf(config10.class).get(1279).configvalue );
		int task10 = Integer.parseInt( ConfigManager.getInstance().getConf(config10.class).get(1280).configvalue );
		//特殊收费选择
		selectTask(isHave,MAP_SUPER,supertask);
		//特殊收费类型相应类型扣除
		for(Map.Entry<Integer,xbean.huoyue> entry : isHave.entrySet()){
			activitymission55 init = ConfigManager.getInstance().getConf(activitymission55.class).
					get(entry.getValue().getHuoyueid());
			if(init != null){
				switch(init.getActivitydegree()){
				case MAP_10:
					task10--;
					continue;
				case MAP_15:
					task15--;
					continue;
				case MAP_20:
					task20--;
					continue;
				}
			}
		}
		//20活跃度选择
		selectTask(isHave,MAP_20,task20);
		//15活跃度选择
		selectTask(isHave,MAP_15,task15);
		//10活跃度选择
		selectTask(isHave,MAP_10,task10);
		xhuoyues.setGetnum(0);
		xhuoyues.setHuoyuenum(0);
		xhuoyues.setHuoyuetime(now);
		xhuoyues.getHuoyuemap().clear();
		xhuoyues.getHuoyuemap().putAll(isHave);
	}
	
	/**
	 * 选择并添加活跃任务
	 * @param isHave
	 * @param mapType
	 * @param num
	 */
	public void selectTask(Map<Integer,xbean.huoyue> isHave,int mapType,int num){
		Map<Integer,DropInit> dropInitMap = this.getInitMap(mapType, isHave);
		List<Integer> result = getDropList(dropInitMap,num);
		if(result.size() != num){
			return;
		}
		addHuoYueTask(result,isHave);
	}
	
	/**
	 * 添加活跃任务
	 * @param result
	 * @param isHave
	 */
	public void addHuoYueTask(List<Integer> result,Map<Integer,xbean.huoyue> isHave){
		for(Integer id : result){
			activitymission55 init = ConfigManager.getInstance().getConf(activitymission55.class).get(id);
			if(init != null){
				isHave.put(init.selecttype, this.initToBean(init));
			}
		}
	}
	
	/**
	 * 添加英雄到背包
	 * @param dropList
	 */
/*	public void addHero(List<Integer> dropList){
		for(Integer heroId : dropList){
			herorecruit51 init = ConfigManager.getInstance().getConf(herorecruit51.class).get(heroId);
			if(init != null){
				DropManager.getInstance().dropAddByOther(init.getId(), 1,init.getHerolevel(), 0, roleId, "lotteryaddhero");
			}
		}
	}*/
	
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
	 * 通过配置文件生成活跃度任务数据
	 * @param init
	 * @return
	 */
	public xbean.huoyue initToBean(activitymission55 init){
		xbean.huoyue result = xbean.Pod.newhuoyue();
		result.setHuoyueid(init.id);
		result.setHuoyuetype(init.type);
		result.setNumall(init.getTimes());
		result.setNum(0);
		result.setIsok(0);
		return result;
	}
	
	/**
	 * 根据类型获得基础掉落数据
	 * @param mapType
	 * @return
	 */
	public Map<Integer,DropInit> getInitMap(int mapType, final Map<Integer,xbean.huoyue> isHave){
		Map<Integer,DropInit> resutlMap = new HashMap<Integer,DropInit>();
		TreeMap<Integer, activitymission55> initMap = ConfigManager.getInstance().getConf(activitymission55.class);
		for(Map.Entry<Integer, activitymission55> entry : initMap.entrySet()){
			if(mapType == this.MAP_SUPER){
				if(entry.getValue().getPaytype() == MAP_SUPER ){
					if( isHave == null || isHave.get(entry.getValue().getSelecttype()) == null){
						DropInit drop = new DropInit(100,entry.getValue().id,1);
						drop.sameType = entry.getValue().getSelecttype();
						resutlMap.put(resutlMap.size(), drop);
					}
				}
			}else if(mapType == this.MAP_20 || mapType == this.MAP_15 || mapType == this.MAP_10){
				if(entry.getValue().getActivitydegree() == mapType){
					if( isHave == null || isHave.get(entry.getValue().getSelecttype()) == null){
						DropInit drop = new DropInit(100,entry.getValue().id,1);
						drop.sameType = entry.getValue().getSelecttype();
						resutlMap.put(resutlMap.size(), drop);
					}
				}
			}
		}
		return resutlMap;
	}
	/**
	 * 任务完成判断
	 * @param hyTaskType
	 * @param addNum
	 */
	public void huoyueTaskOver(int hyTaskType,int addNum,int type){
		boolean isRefresh = false;
		for(Map.Entry<Integer, xbean.huoyue> entry : this.xhuoyues.getHuoyuemap().entrySet()){
			if(entry.getValue().getHuoyuetype() == hyTaskType){
				if(entry.getValue().getIsok() == 0){
					activitymission55 init = ConfigManager.getInstance().getConf(activitymission55.class).
							get(entry.getValue().getHuoyueid());
					if(hyTaskType == BETTLE_BEGIN || BETTLE_END == hyTaskType){
						List<Integer> numList = ParserString.parseString2Int(init.getParameter());
						if( !numList.contains(type) ){
							continue;
						}
					}
					isRefresh = true;
					entry.getValue().setNum(entry.getValue().getNum() + addNum);
					if( entry.getValue().getNum() >= entry.getValue().getNumall() ){
						
						this.xhuoyues.setHuoyuenum(this.xhuoyues.getHuoyuenum() + init.getActivitydegree());
						entry.getValue().setNum(entry.getValue().getNumall());
						entry.getValue().setIsok(1);
						
					}
				}
			}
		}
		if(isRefresh){
			this.sSRefreshHuoYue();
		}
	}
	
	/**
	 * 数据库转换成消息
	 * @return
	 */
	public LinkedList<chuhan.gsp.Huoyue> xHuoyueToBean(){
		LinkedList<chuhan.gsp.Huoyue> result = new LinkedList<chuhan.gsp.Huoyue>();
		for(Map.Entry<Integer, xbean.huoyue> entry : this.xhuoyues.getHuoyuemap().entrySet()){
			chuhan.gsp.Huoyue temp = new chuhan.gsp.Huoyue();
			temp.huoyueid = entry.getValue().getHuoyueid();
			temp.huoyuetype = entry.getValue().getHuoyuetype();
			temp.isok = entry.getValue().getIsok();
			temp.num = entry.getValue().getNum();
			temp.numall = entry.getValue().getNumall();
			result.add(temp);
		}
		return result;
	}
	/**
	 * 领取活跃宝箱
	 * @param boxid
	 * @return
	 */
	public boolean getHuoYueBox(int boxid){
		List<Integer> hyNumList = ParserString.parseString2Int(ConfigManager.getInstance().
				getConf(config10.class).get(1281).getConfigvalue());
		List<Integer> dropList = ParserString.parseString2Int(ConfigManager.getInstance().
				getConf(config10.class).get(1282).getConfigvalue());
		if(hyNumList.size() != dropList.size() || dropList.size() < boxid - 1 || boxid < 1){
			return false;
		}
		int needNum = hyNumList.get(boxid - 1);
		if(this.xhuoyues.getHuoyuenum() < needNum){
			return false;
		}
		if( !isGet(boxid) ){
			return false;
		}
		
		changeGet(boxid);
		int dropId = dropList.get(boxid - 1);
		List<Integer> innerdropList = DropManager.getInstance().drop(roleId, String.valueOf(dropId), LogBehavior.HUOYUEBOX);
		this.sSRefreshHuoYue();
		this.sendSGetHuoYueBox(innerdropList);
		return true;	
	}
	/**
	 * get数值修改
	 * @param boxid
	 */
	public void changeGet(int boxid){
		switch(boxid){
		case 1:
			this.xhuoyues.setGetnum(this.xhuoyues.getGetnum() + 1);
			break;
		case 2:
			this.xhuoyues.setGetnum(this.xhuoyues.getGetnum() + 10);
			break;
		case 3:
			this.xhuoyues.setGetnum(this.xhuoyues.getGetnum() + 100);
			break;
		case 4:
			this.xhuoyues.setGetnum(this.xhuoyues.getGetnum() + 1000);
			break;
		}
	}
	/**
	 * 判断是否已领取
	 * @param boxid
	 * @return
	 */
	private boolean isGet(int boxid){
		if(boxid == 1){
			if(this.xhuoyues.getGetnum() % 10 != 0){
				return false;
			}
		}else if(boxid == 2){
			if(this.xhuoyues.getGetnum() / 10 % 10 != 0){
				return false;
			}
		}else if(boxid == 3){
			if(this.xhuoyues.getGetnum() / 100 % 10 != 0){
				return false;
			}
		}else if(boxid == 4){
			if(this.xhuoyues.getGetnum() / 1000 % 10 != 0){
				return false;
			}
		}
		return true;
	}
	
	/**
	 * 刷新活跃信息
	 */
	public void sSRefreshHuoYue(){
		SRefreshHuoYue snd = new SRefreshHuoYue();
		snd.getnum = this.xhuoyues.getGetnum();
		snd.huoyuenum = this.xhuoyues.getHuoyuenum();
		snd.huoyuelist.addAll(this.xHuoyueToBean());
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	/**
	 * 领取成功
	 * @param innerDropList
	 */
	public void sendSGetHuoYueBox(List<Integer> innerDropList){
		SGetHuoYueBox snd = new SGetHuoYueBox();
		snd.result = SGetHuoYueBox.END_OK;
		snd.droplist.addAll(innerDropList);
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	
}
