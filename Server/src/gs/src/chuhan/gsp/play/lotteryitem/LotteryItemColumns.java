package chuhan.gsp.play.lotteryitem;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;

import xdb.util.Misc;
import chuhan.gsp.attr.config10;
import chuhan.gsp.award.DropInit;
import chuhan.gsp.award.DropManager;
import chuhan.gsp.award.IDManager;
import chuhan.gsp.game.herorecruit51;
import chuhan.gsp.game.ruintreasure52;
import chuhan.gsp.log.LogBehavior;
import chuhan.gsp.log.Logger;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.Message;
import chuhan.gsp.play.activity.ActivityGameManager;
import chuhan.gsp.play.activity.ActivityManager;
import chuhan.gsp.play.huoyuedu.HuoyueColumns;
import chuhan.gsp.util.DateUtil;



public class LotteryItemColumns {	
	public static Logger logger = Logger.getLogger(LotteryItemColumns.class);
	
	public final static int FIRST_MAP = 1;
	public final static int SECOND_MAP = 2;
	public final static int THIRD_MAP = 3;
	public final static int FOURTH_MAP = 4;
	
	public final static int LAYER_NUM = 4;
	public final static int ITEMNUM_EACH_LAYER = 6;
	
	public final static int SUPER_NUM = 5;
	
	public final static int SUPER_1 = 100001;	//事件1  下次获得宝藏数量X3
	public final static int SUPER_2 = 100002;	//事件2  下次获得宝藏数量X2
	public final static int SUPER_3 = 100003;	//事件3  跳至第2层
	public final static int SUPER_4 = 100004;	//事件4  跳至第3层
	public final static int SUPER_5 = 100005;	//事件5  跳至第4层
	public final static int SUPER_6 = 100006;	//事件6  获得本层剩余宝藏
	public final static int SUPER_7 = 100007;	//事件7  获得1次免费刷新机会
	public final static int SUPER_8 = 100008;	//事件8  下次获得钻石X3
	public final static int SUPER_9 = 100009;	//事件9  下次获得金币X3
	public final static int SUPER_10 = 100010;	//事件10  随机钻石奖励（数量在10_config表中）
	
	
	final public long roleId;
	final public xbean.LotteryItemAll xlotteryItem;
	final boolean readonly;
	
	List<Integer> objectId = new ArrayList<Integer>();
	List<Integer> objectNum = new ArrayList<Integer>();
	
	public static LotteryItemColumns getLotteryItemColumn(long roleId, boolean readonly){
		if(xtable.Properties.select(roleId) == null)
			throw new IllegalArgumentException("构造LotteryItemColumns时，角色 "+roleId+" 不存在。");
		
		xbean.LotteryItemAll lotteryItemcol = null;
		if(readonly)
			lotteryItemcol = xtable.Lotteryitemlist.select(roleId);
		else
			lotteryItemcol = xtable.Lotteryitemlist.get(roleId);
		if(lotteryItemcol == null){
			if(readonly)
				lotteryItemcol = xbean.Pod.newLotteryItemAllData();
			else{
				lotteryItemcol = xbean.Pod.newLotteryItemAll();
				xtable.Lotteryitemlist.insert(roleId, lotteryItemcol);
			}
		}
		return new LotteryItemColumns(roleId, lotteryItemcol, readonly);
	}
	
	private LotteryItemColumns(long roleId, xbean.LotteryItemAll xlotteryItem, boolean readonly) {
		this.roleId = roleId;
		this.xlotteryItem = xlotteryItem;
		this.readonly = readonly;
		init();
	}
	public void init(){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		if( !DateUtil.inTheSameDay(this.xlotteryItem.getLastrefreshtime(), now) ){
			this.refreshItem(true);
			this.xlotteryItem.getSuperlist().clear();
//			sSRefreshLottyItem(now);
		}
	}
	
	/**
	 * 抽奖入口
	 * @param lotterytype
	 * @return
	 */
	public boolean LotteryEntry(int lotterytype){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		int cost = 0;
		int addGold = 0;
		switch(lotterytype){
		case CLotteryItem.FREE:
			if(now > this.xlotteryItem.getFreelotterytime()){
				int initTime = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1241).configvalue);
				long addTime = initTime * DateUtil.minuteMills;
				this.xlotteryItem.setFreelotterytime(now + addTime);
				addGold = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1231).configvalue);
				DropManager.getInstance().dropAddByOther(IDManager.GOLD, addGold, 0, 0, roleId, LogBehavior.LOTTERYITEMFREE);
				return buyItem(1,now,lotterytype,LogBehavior.LOTTERYITEMFREE);
			}
			return false;
		case CLotteryItem.ONE:
			cost = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1230).configvalue);
			if(DropManager.getInstance().useDel(IDManager.YUANBAO, cost, roleId, LogBehavior.LOTTERYITEMONECOST)){
				addGold = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1231).configvalue);
				DropManager.getInstance().dropAddByOther(IDManager.GOLD, addGold, 0, 0, roleId, LogBehavior.LOTTERYITEMONE);
				return buyItem(1,now,lotterytype,LogBehavior.LOTTERYITEMONE);
			}
			return false;
		case CLotteryItem.TEN:
			cost = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1232).configvalue);
			if(DropManager.getInstance().useDel(IDManager.YUANBAO, cost, roleId, LogBehavior.LOTTERYITEMTENCOST)){
				addGold = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1233).configvalue);
				DropManager.getInstance().dropAddByOther(IDManager.GOLD, addGold, 0, 0, roleId, LogBehavior.LOTTERYITEMTEN);
				return buyItem(10,now,lotterytype,LogBehavior.LOTTERYITEMTEN);
			}
			return false;
		case CLotteryItem.REFRESH:
			return refreshItem(false);
		}
		return false;
	}
	
	/**
	 * 进行循环抽奖
	 * @param num
	 * @param now
	 * @param lotteryType
	 * @return
	 */
	public boolean buyItem(int num,long now,int lotteryType,String logStr){
		this.objectId.clear();
		this.objectNum.clear();
		
		List<LotteryItemget> result = new LinkedList<LotteryItemget>();
		for(int i = 0;i < num;i++){
			int[] res = this.getItem(now);
			if(res == null){
				return false;
			}
			LotteryItemget lget = new LotteryItemget();
			lget.id = res[0];
			lget.superid = res[1];
			lget.num = res[2];
			result.add(lget);
			if(this.xlotteryItem.getMapvalue() >= this.ITEMNUM_EACH_LAYER){
				this.xlotteryItem.setMapkey(this.xlotteryItem.getMapkey() + 1);
				this.xlotteryItem.setMapvalue(1);
			}else{
				this.xlotteryItem.setMapvalue(this.xlotteryItem.getMapvalue() + 1);
			}
			if(this.xlotteryItem.getMapkey() > this.LAYER_NUM){
				this.refreshItem(true);
			}
		}
		//判断是否掉落或者发邮件
		DropManager.getInstance().sendMailOrDropAdd(roleId, this.objectId, this.objectNum, null, logStr);
		this.sSRefreshLottyItem(now);
		this.sendSLotteryItem(result,lotteryType);
		
		//遗迹宝藏抽奖活动统计数据
		ActivityGameManager.getInstance().addZMItemActivity(roleId, num);
		//活跃度添加
		ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.YIJITANBAO, num);
		return true;
	}

	/**
	 * 抽奖信息
	 * @param now
	 * @return
	 */
	private int[] getItem(long now){
		int[] result = {0,0,0};
		xbean.LotteryItemlayer xlayer = this.xlotteryItem.getLotteryitemmap().get(this.xlotteryItem.getMapkey());
		if(xlayer == null){
			return null;
		}
		if(xlayer.getLotteryitemlist().size() < this.xlotteryItem.getMapvalue() ){
			return null;
		}
		xbean.LotteryItem lotteryItem = xlayer.getLotteryitemlist().get(this.xlotteryItem.getMapvalue() - 1);
		ruintreasure52 init = ConfigManager.getInstance().getConf(ruintreasure52.class).get(lotteryItem.getId());
		if(init == null){
			return null;
		}
		if(init.getType() == 2){
			result = this.addSuper(init.getId(), now);
			lotteryItem.setId(result[0]);
		}else{
			result[0] = init.getId();
			int addNum = Integer.parseInt(init.getParameter2());
			int superId = getSuper(init);
			if(superId != -1){
				if(superId == SUPER_1 || superId == SUPER_8){
					addNum = addNum * 3;
				}else if(superId == SUPER_2 || superId == SUPER_9){
					addNum = addNum * 2;
				}
				result[1] = superId;
			}
			result[2] =  addNum;
			lotteryItem.setSuperid(superId);
			//将掉落物统计起来
			this.objectId.add(init.getParameter1());
			this.objectNum.add(addNum);
//			DropManager.getInstance().dropAddByOther(init.getParameter1(), addNum, 0, 0, roleId, "lotteryitem");
			ActivityManager.getInstance().addMsgNotice(roleId,init.getParameter1(),ActivityManager.BAOZANG,"");
		}
		lotteryItem.setIsget(1);
		
		return result;
	}
	
	/**
	 * 获取激活的特殊事件
	 * @param init
	 * @return
	 */
	public int getSuper(ruintreasure52 init){
		boolean isGold = false;
		boolean isYuanbao = false;
		if(init.getParameter1() == IDManager.YUANBAO){
			isYuanbao = true;
		}
		if(init.getParameter1() == IDManager.GOLD){
			isGold = true;
		}
		for( Integer lotteryId : this.xlotteryItem.getSuperlist() ){
			if(lotteryId == SUPER_1 || lotteryId == SUPER_2){
				this.xlotteryItem.getSuperlist().remove(lotteryId);
				return lotteryId;
			}
			if(lotteryId == SUPER_8 && isYuanbao){
				this.xlotteryItem.getSuperlist().remove(lotteryId);
				return lotteryId;
			}
			if(lotteryId == SUPER_9 && isGold){
				this.xlotteryItem.getSuperlist().remove(lotteryId);
				return lotteryId;
			}
		}
		return -1;
	}
	
	
	/**
	 * 增加特殊事件
	 * @param lotteryId
	 * @param now
	 * @return
	 */
	private int[] addSuper(int lotteryId,long now){
		int[] result = {0,0,1};
		if(lotteryId == SUPER_1 || SUPER_2 == lotteryId || SUPER_7 == lotteryId || SUPER_8 == lotteryId || 
				SUPER_9 == lotteryId){
			int isHave = 0;
			if(!DateUtil.inTheSameDay(this.xlotteryItem.getMonthfirsttime(), now) &&
					ActivityManager.getInstance().isHaveLotteryMonth(roleId)){
				isHave = 1; 
			}
			if( this.xlotteryItem.getSuperlist().size() + isHave >= SUPER_NUM){
				lotteryId = SUPER_10;
			}else{
				this.xlotteryItem.getSuperlist().add(lotteryId);
			}
		}else if(lotteryId == SUPER_3){
			this.xlotteryItem.setMapkey(2);
			this.xlotteryItem.setMapvalue(0);
		}else if(lotteryId == SUPER_4){
			this.xlotteryItem.setMapkey(3);
			this.xlotteryItem.setMapvalue(0);
		}else if(lotteryId == SUPER_5){
			this.xlotteryItem.setMapkey(4);
			this.xlotteryItem.setMapvalue(0);
		}else if(lotteryId == SUPER_6){
			this.xlotteryItem.setMapvalue(this.xlotteryItem.getMapvalue() + 1);
			while(this.xlotteryItem.getMapvalue() <= this.ITEMNUM_EACH_LAYER){
				this.getItem(now);
				this.xlotteryItem.setMapvalue(this.xlotteryItem.getMapvalue() + 1);
			}
		}
		result[0] = lotteryId;
		if(lotteryId == SUPER_10){
			int min = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1228).configvalue);
			int max = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1229).configvalue);
			int add = chuhan.gsp.util.Misc.getRandomBetween(min,max);
			result[2] = add;
			DropManager.getInstance().dropAddByOther(IDManager.YUANBAO, add, 0, 0, roleId, LogBehavior.LOTTERYITEMSUPER10);
			
		}
		return result;
	}
	
	/**
	 * 刷新列表
	 * @param isFree
	 * @return
	 */
	public boolean refreshItem(boolean isFree){	
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		boolean isMonthFirst = false;
		if(!isFree){
			int cost = Integer.parseInt( ConfigManager.getInstance().getConf(config10.class).get(1234).configvalue );
			for(int i = 0;i<this.xlotteryItem.getSuperlist().size();i++){
				if(xlotteryItem.getSuperlist().get(i) == SUPER_7){
					cost = 0;
					xlotteryItem.getSuperlist().remove(i);
					break;
				}
			}
			int goldadd = Integer.parseInt( ConfigManager.getInstance().getConf(config10.class).get(1235).configvalue );
			if( !DropManager.getInstance().useDel(IDManager.YUANBAO, cost, roleId, LogBehavior.LOTTERYITEMREFRESHCOST) ){
				return false;
			}
			DropManager.getInstance().dropAddByOther(IDManager.GOLD, goldadd, 0, 0, roleId, LogBehavior.LOTTERYITEMREFRESH);
			if( !DateUtil.inTheSameDay(this.xlotteryItem.getMonthfirsttime(), now) &&
					ActivityManager.getInstance().isHaveLotteryMonth(roleId) ){
				if(cost != 0){
					isMonthFirst = true;
				}
				this.xlotteryItem.setMonthfirsttime(now);
			}
		}
		//逐层随机数据
		for(int i = 1;i <= LAYER_NUM ; i++){
			List<Integer> latteryId = new ArrayList<Integer>();
			//是否触发特殊事件
			if(isSuper(i)){
				Map<Integer,DropInit> initSuperMap = this.getInitMap(i, false, 2);
				latteryId.addAll(this.getDropList(initSuperMap, 1));
			}
			//随机基础数据
			Map<Integer,DropInit> initSuperMap = this.getInitMap(i, false, 1);
			latteryId.addAll(this.getDropList(initSuperMap, this.ITEMNUM_EACH_LAYER - latteryId.size() - 1));
			//是否月卡首次刷新判断
			if(isMonthFirst){
				Map<Integer,DropInit> initMonthMap = this.getInitMap(i, true, 1);
				if(initMonthMap != null && initMonthMap.size() > 0){
					boolean needMonthFirst = true;
					for(Map.Entry<Integer, DropInit> entry : initMonthMap.entrySet()){
						if(latteryId.contains(entry.getValue().id)){
							needMonthFirst = false;
							break;
						}
					}
					if(needMonthFirst){
						latteryId.addAll(this.getDropList(initMonthMap, 1));
					}
				}
			}
			if(latteryId.size() < ITEMNUM_EACH_LAYER){
				latteryId.addAll(this.getDropList(initSuperMap, this.ITEMNUM_EACH_LAYER - latteryId.size()));
			}
			//数据显示先后处理
			Map<Integer,DropInit> initViewMap = this.getInitMap(latteryId);
			List<Integer> endList = this.getDropList(initViewMap, initViewMap.size());
			this.addNewData(i, endList);	
		}
		this.xlotteryItem.setLastrefreshtime(now);
		this.xlotteryItem.setMapkey(1);
		this.xlotteryItem.setMapvalue(1);
		this.sSRefreshLottyItem(now);
		if(!isFree){
			this.sendSLotteryItem(new LinkedList<LotteryItemget>(), CLotteryItem.REFRESH);
		}
		return true;
	}
	
	/**
	 * 保存新随机出的数据
	 * @param layerNum
	 * @param endList
	 */
	private void addNewData(int layerNum,List<Integer> endList){
		xbean.LotteryItemlayer layer = xbean.Pod.newLotteryItemlayer();
		for(Integer lotteryId : endList){
			xbean.LotteryItem lotteryItem = xbean.Pod.newLotteryItem();
			lotteryItem.setId(lotteryId);
			lotteryItem.setIsget(0);
			layer.getLotteryitemlist().add(lotteryItem);
		}
		int view = 1;
		//随机10次
		for(int i = 0;i<10;i++){
			int random = chuhan.gsp.util.Misc.getRandomBetween(0, 5);
			if(random < layer.getLotteryitemlist().size()){
				xbean.LotteryItem lotteryItem = layer.getLotteryitemlist().get(random);
				if(lotteryItem.getViewnum() == 0){
					lotteryItem.setViewnum(view++);
				}
			}
		}
		for(xbean.LotteryItem lotteryItem : layer.getLotteryitemlist()){
			if(lotteryItem.getViewnum() == 0){
				lotteryItem.setViewnum(view++);
			}
		}
		this.xlotteryItem.getLotteryitemmap().put(layerNum, layer);
	}
	
	/**
	 * 是否随机事件
	 * @param layerNum
	 * @return
	 */
	private boolean isSuper(int layerNum){
		try{
			int num = chuhan.gsp.util.Misc.getRandomBetween(1,100);
			int percent = 0;
			switch(layerNum){
			case FIRST_MAP:
				percent = Integer.parseInt( ConfigManager.getInstance().getConf(config10.class).get(1124).configvalue );
				return num <= percent;
			case SECOND_MAP:
				percent = Integer.parseInt( ConfigManager.getInstance().getConf(config10.class).get(1125).configvalue );
				return num <= percent;
			case THIRD_MAP:
				percent = Integer.parseInt( ConfigManager.getInstance().getConf(config10.class).get(1126).configvalue );
				return num <= percent;
			case FOURTH_MAP:
				percent = Integer.parseInt( ConfigManager.getInstance().getConf(config10.class).get(1127).configvalue );
				return num <= percent;
			}
		}
		catch(Exception e){
			e.printStackTrace();
		}
		return false;
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
	 * 根据ID获取显示顺序掉落数据
	 * @param lotteryIdList
	 * @return
	 */
	public Map<Integer,DropInit> getInitMap(List<Integer> lotteryIdList){
		Map<Integer,DropInit> resultMap = new HashMap<Integer,DropInit>();
		for(Integer lotteryId : lotteryIdList){
			ruintreasure52 init = ConfigManager.getInstance().getConf(ruintreasure52.class).get(lotteryId);
			if(init != null){
				DropInit drop = new DropInit(init.getGet_weight(),init.getId(),1);
				resultMap.put(resultMap.size(), drop);
			}
		}
		return resultMap;
	}
	
	/**
	 * 根据类型获得基础掉落数据
	 * @param mapType
	 * @param isMonthFirst
	 * @return
	 */
	public Map<Integer,DropInit> getInitMap(int mapType,boolean isMonthFirst,int type){
		Map<Integer,DropInit> resultMap = new HashMap<Integer,DropInit>();
		TreeMap<Integer, ruintreasure52> initMap = ConfigManager.getInstance().getConf(ruintreasure52.class);
		for(Map.Entry<Integer, ruintreasure52> entry : initMap.entrySet()){
			switch(mapType){
			case FIRST_MAP:
				if(isMonthFirst){
					if(entry.getValue().getMonthcard_refresh1() != 0 && entry.getValue().getMonthcard_refresh1() != -1
							&& entry.getValue().getType() == type ){
						DropInit drop = new DropInit(entry.getValue().getMonthcard_refresh1(),entry.getValue().getId(),
								Integer.MAX_VALUE);
						resultMap.put(resultMap.size(), drop);
					}
				}else{
					if(entry.getValue().getWeight1() != 0 && entry.getValue().getWeight1() != -1
							&& entry.getValue().getType() == type ){
						DropInit drop = new DropInit(entry.getValue().getWeight1(),entry.getValue().getId(),
								Integer.MAX_VALUE);
						resultMap.put(resultMap.size(), drop);
					}
				}
				continue;
			case SECOND_MAP:
				if(isMonthFirst){
					if(entry.getValue().getMonthcard_refresh2() != 0 && entry.getValue().getMonthcard_refresh2() != -1
							&& entry.getValue().getType() == type ){
						DropInit drop = new DropInit(entry.getValue().getMonthcard_refresh2(),entry.getValue().getId(),
								Integer.MAX_VALUE);
						resultMap.put(resultMap.size(), drop);
					}
				}else{
					if(entry.getValue().getWeight2() != 0 && entry.getValue().getWeight2() != -1
							&& entry.getValue().getType() == type ){
						DropInit drop = new DropInit(entry.getValue().getWeight2(),entry.getValue().getId(),
								Integer.MAX_VALUE);
						resultMap.put(resultMap.size(), drop);
					}
				}
				continue;
			case THIRD_MAP:
				if(isMonthFirst){
					if(entry.getValue().getMonthcard_refresh3() != 0 && entry.getValue().getMonthcard_refresh3() != -1
							&& entry.getValue().getType() == type ){
						DropInit drop = new DropInit(entry.getValue().getMonthcard_refresh3(),entry.getValue().getId(),
								Integer.MAX_VALUE);
						resultMap.put(resultMap.size(), drop);
					}
				}else{
					if(entry.getValue().getWeight3() != 0 && entry.getValue().getWeight3() != -1
							&& entry.getValue().getType() == type ){
						DropInit drop = new DropInit(entry.getValue().getWeight3(),entry.getValue().getId(),
								Integer.MAX_VALUE);
						resultMap.put(resultMap.size(), drop);
					}
				}
				continue;
			case FOURTH_MAP:
				if(isMonthFirst){
					if(entry.getValue().getMonthcard_refresh4() != 0 && entry.getValue().getMonthcard_refresh4() != -1
							&& entry.getValue().getType() == type ){
						DropInit drop = new DropInit(entry.getValue().getMonthcard_refresh4(),entry.getValue().getId(),
								Integer.MAX_VALUE);
						resultMap.put(resultMap.size(), drop);
					}
				}else{
					if(entry.getValue().getWeight4() != 0 && entry.getValue().getWeight4() != -1
							&& entry.getValue().getType() == type ){
						DropInit drop = new DropInit(entry.getValue().getWeight4(),entry.getValue().getId(),
								Integer.MAX_VALUE);
						resultMap.put(resultMap.size(), drop);
					}
				}
				continue;
			}
		}
		return resultMap;
	}
	
	/**
	 * 发送抽奖成功消息
	 * @param result
	 * @param lotteryType
	 */
	public void sendSLotteryItem(List<LotteryItemget> result,int lotteryType){
		SLotteryItem snd = new SLotteryItem();
		snd.result = SLotteryItem.END_OK;
		snd.lotterytype = lotteryType;
		snd.itemlist.addAll(result);
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	
	/**
	 * 根据数据库内容生成消息信息
	 * @param now
	 * @return
	 */
	private chuhan.gsp.play.lotteryitem.LotteryItemAll getReturn(long now){
		chuhan.gsp.play.lotteryitem.LotteryItemAll lottery = new chuhan.gsp.play.lotteryitem.LotteryItemAll();
		lottery.mapkey = this.xlotteryItem.getMapkey();
		lottery.mapvalue = this.xlotteryItem.getMapvalue();
		lottery.superlist.addAll(this.xlotteryItem.getSuperlist());
		if( !DateUtil.inTheSameDay(this.xlotteryItem.getMonthfirsttime(), now) && 
				ActivityManager.getInstance().isHaveLotteryMonth(roleId) ){
			lottery.ismonthfirsthave = 1;
		}else{
			lottery.ismonthfirsthave = 0;
		}
		if( now > this.xlotteryItem.getFreelotterytime() ){
			lottery.ishavefree = 0;
		}else{
			lottery.ishavefree = (int) ((this.xlotteryItem.getFreelotterytime() - now)/1000);
		}
		for( Map.Entry<Integer, xbean.LotteryItemlayer> entry : this.xlotteryItem.getLotteryitemmap().entrySet() ){
			chuhan.gsp.play.lotteryitem.LotteryItemlayer layer = new chuhan.gsp.play.lotteryitem.LotteryItemlayer();
			for( xbean.LotteryItem xitem : entry.getValue().getLotteryitemlist() ){
				chuhan.gsp.play.lotteryitem.LotteryItem item = new chuhan.gsp.play.lotteryitem.LotteryItem();
				item.id = xitem.getId();
				item.isget = xitem.getIsget();
				item.viewnum = xitem.getViewnum();
				item.superid = xitem.getSuperid();
				layer.lotteryitemlist.add(item);
			}
			lottery.lotteryitemmap.put(entry.getKey(), layer);
		}
		return lottery;
	}
	
	/**
	 * 刷新抽奖信息
	 * @param now
	 */
	public void sSRefreshLottyItem(long now){
		SRefreshLottyItem snd = new SRefreshLottyItem();
		snd.lotteryitem = this.getReturn(now);
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
}
