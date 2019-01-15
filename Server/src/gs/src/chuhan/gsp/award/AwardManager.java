/**
 * Class: AwardManager.java
 * Package: knight.gsp.activity.award
 *
 *
 *   ver     date      		author
 * ──────────────────────────────────
 *   		 2011-1-27 		yesheng
 *
 * Copyright (c) 2011, Perfect World All Rights Reserved.
 */

package chuhan.gsp.award;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;

import javax.script.ScriptException;

import org.apache.log4j.Logger;

import chuhan.gsp.attr.PAddExpProc;
import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.YuanBaoAddType;
import chuhan.gsp.hero.OldHero;
import chuhan.gsp.hero.OldHeroColumn;
import chuhan.gsp.item.AddItemResult;
import chuhan.gsp.item.AddItemResult.AddResultEnum;
import chuhan.gsp.item.Constant;
import chuhan.gsp.item.ItemColumn;
import chuhan.gsp.item.SItemList;
import chuhan.gsp.item.SShowAddItem;
import chuhan.gsp.item.ShowItemData;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.util.Conv;
import chuhan.gsp.util.DateUtil;
import chuhan.gsp.util.Misc;


/**
 * ClassName:AwardManager Function: 活动奖励管理类,包括把打表的原始数据重新封装,分配不同的奖励等等
 * 
 * @version
 * @since
 * @Date 2011-1-27 下午07:58:02
 * 
 * @see
 */
public class AwardManager{

	private class MsgInfos {

		final int sendToTeam;

		final int msgid;

		final String msgitemid;
		
		final String msg2itemid;
		
		final int msg2id;
		
		final int msg2type;
		
		

		private MsgInfos(String msgitemid, int msgid, int sendToTeam,String msg2itemid,int msg2id,int msg2type) {

			this.msgitemid = msgitemid;
			this.msgid = msgid;
			this.sendToTeam = sendToTeam;
			this.msg2itemid = msg2itemid;
			this.msg2id = msg2id;
			this.msg2type = msg2type;
		}

		public int getSendToTeam() {

			return sendToTeam;
		}

		public int getMsgid() {

			return msgid;
		}

		public String getMsgitemid() {

			return msgitemid;
		}

		
		public String getMsg2itemid() {
		
			return msg2itemid;
		}

		
		public int getMsg2id() {
		
			return msg2id;
		}

		
		public int getMsg2type() {
		
			return msg2type;
		}

	}

	public static final String MODULE_NAME = "award";
	
	public static Logger logger = Logger.getLogger(AwardManager.class);

	public static final int FIRSTC_AWARD = 1; // 必给物品奖励

	public static final int SECONDC_AWARD = 2; // 随机物品奖励

	public static final int MONEY_AWARD = 3; // 金钱奖励

	//public static final int SMONEY_AWARD = 4; // 储备金奖励

	public static final int EXP_AWARD = 5; // 经验奖励

	public static final int YUANBAO_AWARD = 6; // 元宝奖励
	
	public static final int HERO_1_EXP_AWARD = 101; // 英雄1经验奖励
	public static final int HERO_2_EXP_AWARD = 102; // 英雄2经验奖励
	public static final int HERO_3_EXP_AWARD = 103; // 英雄3经验奖励
	public static final int HERO_4_EXP_AWARD = 104; // 英雄4经验奖励
	public static final int HERO_5_EXP_AWARD = 105; // 英雄5经验奖励
	

	public static final String HERO_LEVEL = "HeroLv";

	public static final String ROLE_LEVEL = "Lv";

	public static final String VIP_LEVEL = "VIP";
	
	public static final String PURPLE_HERO_LEVEL = "HeroN";
	
	public static final String PURPLE_EQUIP_LEVEL = "EquipN";
	
	public static final String PURPLE_SKILL_LEVEL = "SkillN";
	
	public static final String RANK = "Rank";
	
	public static final String MAX_ROLE_LV = "Blood";
	
	public static final String IWEB_MULTI_EXP = "IsSerMul";
	
	public static final String WEEK_DAY = "Weekday";
	
	public static final String TRADE_GRADE_EXP = "TradeGE";//折算道具养成进度
	
	public static int iwebMultiExp = 0;//默认是0，即没有双倍或多倍

	//private static AwardManager instance = new AwardManager();

	// 奖励一级表的原始数据
	private java.util.NavigableMap<Integer, chuhan.gsp.attr.SActivityAward> award1Map = null;

	// 奖励二级表的原始数据
	private java.util.NavigableMap<Integer, SItemList> award2Map = null;

	// 转换后的奖励:必给奖励
	private java.util.Map<Integer, List<FirstClassAwardItem>> firstCAwardMap = new HashMap<Integer, List<FirstClassAwardItem>>();

	// 转换后的奖励:随机奖励
	private java.util.Map<Integer, SecondClassAwardItems> secondCAwardMap = new HashMap<Integer, SecondClassAwardItems>();

	// 转换后的奖励:金钱奖励
	private java.util.Map<Integer, String> moneyAwardMap = new HashMap<Integer, String>();
	
	// 转换后的奖励:元宝奖励
	private java.util.Map<Integer, String> sYuanbaoAwardMap = new HashMap<Integer, String>();

	// 转换后的奖励:经验奖励
	private java.util.Map<Integer, String> expAwardMap = new HashMap<Integer, String>();

	// 转换后的奖励:英雄经验奖励
	private java.util.Map<Integer, String> heroExpAwardMap = new HashMap<Integer, String>();
	
	// 转换后的奖励:历练声望奖励
	private java.util.Map<Integer, String> swAwardMap = new HashMap<Integer, String>();
	
	//特定时间的额外奖励
	private java.util.Map<Integer, ExAward> externalAwardMap = null;

	// 给予的物品是否绑定
	private java.util.Map<Integer, Boolean> bindMap = null;
	
	//如果包裹栏满了是否放入临时包裹
	private java.util.Map<Integer, Boolean> tempBagAvailableMap = null;

	// 给予的物品是否需要发消息
	private java.util.Map<Integer, MsgInfos> msgInfosMap = null;
	
	// 装备产出途径,有的装备要给一些初始属性
	private java.util.Map<Integer, Integer> equipGenWays = null;
	
	// 是否需要清上限
	private java.util.Map<Integer, Set<Integer>> clearLimitMap = new ConcurrentHashMap<Integer, Set<Integer>>();
	
	// 奖励id的嵌套历史,防止出现环
	private java.util.Map<Long, Set<Integer>> awardidHistoryMap = new ConcurrentHashMap<Long, Set<Integer>>();
	
	//awardid的item上限
	//private java.util.Map<Integer, Integer> awardid2activityidMap = null;	
	
	//不发队伍消息的物品
	private Set<Integer> itemNoMsgs = null;

	public static AwardManager instance = new AwardManager();
	private AwardManager() {

	}

	public static AwardManager getInstance() {

		return instance;
	}
	
	public static void reload() throws Exception
	{
		AwardManager newInstance = new AwardManager();
		newInstance.init();
		instance = newInstance;
	}

	private void initData() throws Exception {
      if(!new xdb.Procedure(){

		@Override
		protected boolean process() throws Exception {
			award1Map = chuhan.gsp.main.ConfigManager.getInstance().getConf(chuhan.gsp.attr.SActivityAward.class);

			award2Map = ConfigManager.getInstance().getConf(chuhan.gsp.item.SItemList.class);

			
			// 给予的物品是否绑定
			bindMap = new HashMap<Integer, Boolean>();
			
			//如果包裹栏满了是否放入临时包裹
			tempBagAvailableMap = new HashMap<Integer, Boolean>();

			// 给予的物品是否需要发消息
			msgInfosMap = new HashMap<Integer, MsgInfos>();
			
			// 装备产出途径,有的装备要给一些初始属性
			equipGenWays = new HashMap<Integer, Integer>();
			
			//awardid到activityid的映射
			//awardid2activityidMap = new HashMap<Integer, Integer>();	
			
			//不发队伍消息的物品
			itemNoMsgs = new HashSet<Integer>();

	        firstCAwardMap.clear();
			
			secondCAwardMap.clear();
			
			moneyAwardMap.clear();
			
			expAwardMap.clear();
			
		    heroExpAwardMap.clear();
			
			swAwardMap.clear();
			
			clearLimitMap.clear();
			
			for (chuhan.gsp.attr.SActivityAward award : award1Map.values()) {
				int id = award.getId();
				
				// 随机物品的分母
				// int denominator=award.getSecondClassDenominator();
				List<Integer> fcAwardRawData = award.getFirstClassAward();
				List<Integer> scAwardRawData = award.getSecondClassAward();
				List<String> scAwardRawDataProbs = award.getSecondClassAwardProb();
				List<FirstClassAwardItem> items = new ArrayList<FirstClassAwardItem>();
				List<SecondClassAwardItem> sItems = new ArrayList<SecondClassAwardItem>();
				int i = 0;
				if (fcAwardRawData != null) {
					while (i < fcAwardRawData.size()) {
						// 每四个int构成一个必给的奖励物品,但是如果第一个int是0，则跳过
						if (fcAwardRawData.get(i) == 0) {
							i = i + 2;
							continue;
						} else {
							try {
								int itemID = fcAwardRawData.get(i);
								int itemNum = fcAwardRawData.get(i + 1);
								int itemProp = 0;//fcAwardRawData.get(i + 2);
								int itemPropValue = 0;//fcAwardRawData.get(i + 3);
								items.add(new FirstClassAwardItem(itemID, itemNum, itemProp, itemPropValue));
								i = i + 2;
							} catch (RuntimeException e) {
								throw new RuntimeException("init first class award table failed,awardid:"+id, e);
							}
						}
					}
					firstCAwardMap.put(id, items);
				}
				if (scAwardRawData != null) {
					SecondClassAwardItems scClassAwardItems = new SecondClassAwardItems();
					scClassAwardItems.setBase(award.getSecondClassDenominator());
					scClassAwardItems.setRandomType(award.getRandomType());
					scClassAwardItems.setTotalProb(award.getTotalProb());
					int j = 0;
					while (j < scAwardRawData.size()) {
						// 每三个int构成一个随机的奖励物品,但是如果第一个int是0，则跳过
						if (scAwardRawData.get(j) == 0) {
							j = j + 2;
							continue;
						} else {
							try {
								int itemID = scAwardRawData.get(j);
								int itemNum = scAwardRawData.get(j + 1);
								String itemRatio = scAwardRawDataProbs.get(j / 2);
								// sItems.add(new SecondClassAwardItem(itemID, itemNum,
								// itemRatio,denominator));
								sItems.add(new SecondClassAwardItem(itemID, itemNum, itemRatio));
								j = j + 2;
							} catch (RuntimeException e) {
								throw new RuntimeException("init second class award table failed,awardid:"+id, e);
							}
						}
					}
					scClassAwardItems.setItems(sItems);
					secondCAwardMap.put(id, scClassAwardItems);
				}
				if (award.getMoney() != null)
					moneyAwardMap.put(id, award.getMoney());
				if (award.getYuanbao() != null)
					sYuanbaoAwardMap.put(id, award.getYuanbao());
				if (award.getExp() != null)
					expAwardMap.put(id, award.getExp());
				if (award.getHeroExp() != null)
					heroExpAwardMap.put(id, award.getHeroExp());
				//是否有物品上限,如果有的话,加入map中
			}
			
			return true;
		}
    	   
       }.call())
    	  throw new RuntimeException("init award table failed");
		
	}

	/*public Map<Integer, AwardItem> distributeAllAward(long roleid, List<Integer> awardIDs, Map<String, Object> paras, int countertype, int xiangguanid,
			int expReasonid, String expHint, boolean showMsg) {
		return distributeAllAward(roleid, awardIDs, paras, false, countertype, xiangguanid, expReasonid, expHint, showMsg);
	}*/

	/*public Map<Integer, AwardItem> distributeAllAward(long roleid, List<Integer> awardIDs, Map<String, Object> paras, boolean petDead, int countertype,
			int xiangguanid, int expReasonid, String expHint, boolean showMsg) {

		Map<Integer, AwardItem> result = new HashMap<Integer, AwardItem>();
		for (Integer awardid : awardIDs) {
			Map<Integer, AwardItem> add = distributeAllAward(roleid, awardid, paras, petDead, countertype, xiangguanid, expReasonid, expHint, showMsg);
			UniteAwardsResult(result, add);
		}
		return result;
	}*/

	/*TODO 战斗后给奖励public Map<Integer, AwardItem> distributeBattleAward(long roleid, int awardID, xbean.BattleInfo battleInfo, int countertype, int xiangguanid,
			int expReasonid, String expHint, boolean showMsg) {

		Map<String, Object> paras = new HashMap<String, Object>();
		putBattleParas(paras, battleInfo);
		return distributeAllAward(roleid, awardID, paras, false, countertype, xiangguanid, expReasonid, expHint, showMsg);
	}*/

	/*public Map<Integer, AwardItem> distributeAllAward(long roleid, int awardID, Map<String, Object> paras, int countertype, int xiangguanid, int expReasonid,
			String expHint, boolean showMsg) {

		return distributeAllAward(roleid, awardID, paras, false, countertype, xiangguanid, expReasonid, expHint, showMsg);
	}*/

	/*public Map<Integer, AwardItem> distributeAllAward(long roleid, int awardID, Map<String, Object> paras, int countertype, int xiangguanid, int expReasonid,
			String expHint) {

		return distributeAllAward(roleid, awardID, paras, false, countertype, xiangguanid, expReasonid, expHint, true);
	}*/

	public Map<Integer, AwardItem> distributeAllAward(long roleid, int awardID, Map<String, Object> paras, boolean notifynewitem){
		return distributeAllAward(roleid, awardID, paras,notifynewitem ,true);
	}
	/**
	 * 注意,该方法只能在AwardManager内部调用
	 * distributeAllAward:根据奖励表中的一行给予奖励.包括金钱,储备金,人物经验,宠物经验,必给物品,随机物品
	 * 注意如果你的奖励表对应的那一行是有公式的,那必须要用paras包含所有公式需要的参数,否则会报错.如果那一行是纯数字,则传入null就行
	 * 目前策划配的奖励公式用到的变量有: 队伍平均等级 TeamLv 队伍人数 TeamNum 自身等级 Lv 环数 Time 宠物等级 PetLv
	 * 当前经验 Exp 练功区等级 MapLv 怪物数量 MonsterNum 头领数量 MasterNum （主）怪物等级 MonsterLv
	 * 是否无多倍时间buff IsNoMul 是否在双倍buff下 IsDouble 是否在系统三倍下 IsSTrible 是否在修为丹三倍下
	 * IsTrible 是否为队长 IsTL AwardManager会默认存入IsNoMul IsDouble IsSTrible IsTrible
	 * IsTL Lv ,如果公式中还需要其他变量,需要调用者自己加入 e.g. 如果你要传入环数为5,就用paras.put("Time",5);
	 * 注意,Is开头的几个变量,都是用0表示false,1表示true的.
	 * 返回值是一个map,用于发各个模块的奖励消息.返回的map的key是以下6个的子集: public static final int
	 * FIRSTC_AWARD = 1; // 必给物品奖励
	 * 
	 * public static final int SECONDC_AWARD = 2; // 随机物品奖励
	 * 
	 * public static final int MONEY_AWARD = 3; // 金钱奖励
	 * 
	 * public static final int SMONEY_AWARD = 4; // 储备金奖励
	 * 
	 * public static final int EXP_AWARD = 5; // 经验奖励
	 * 
	 * public static final int PETEXP_AWARD = 6; // 宠物经验奖励
	 * 
	 * public static final int SHENGWANG_AWARD = 7; // 声望奖励e.g.
	 * 如果你用MONEY_AWARD作key去map中get,如果返回null,说明没有给予金钱奖励.如果返回AwardItem,AwardItem里面的value就表示
	 * 这次奖励给予了多少金钱.储备金,人物经验,宠物经验的奖励类似.如果你用SECONDC_AWARD去获取随机物品的奖励,如果返回null,说明没有给予奖励.
	 * 如果返回AwardItem,里面有一个items list,里面记录着奖励的物品的id和num. 有了这些信息,就可以发送给客户端奖励信息了.
	 * 
	 * 
	 * @param roleid
	 * @param awardID
	 * @param paras
	 *            void
	 * @throws
	 * @since 　
	 */
	private Map<Integer, AwardItem> distributeAllAward(long roleid, int awardID, Map<String, Object> paras, boolean notifynewitem, boolean call) {

		ParseStringExpression pse = new ParseStringExpression();
		Map<Integer, AwardItem> result = new HashMap<Integer, AwardItem>();
		try {
			if (call){ //如果是外部调用,则新建一个调用awardid集合,防止出现环
				Set<Integer> idSet = new HashSet<Integer>();
				awardidHistoryMap.put(roleid, idSet);
			}
			if (awardidHistoryMap.get(roleid).contains(awardID)){
				logger.error("awardid history has a loop!");
				return result;
			}
			awardidHistoryMap.get(roleid).add(awardID);
			
			if (paras == null)
				paras = new HashMap<String, Object>();
			putNeededParas(roleid, paras);
			long money = distributeMoneyAward(roleid, awardID, paras, pse);
			if (money != 0) {
				result.put(MONEY_AWARD, new AwardItem(money));
			}
			int yuanbao = distributeYuanBaoAward(roleid, awardID, paras, pse);
			if (yuanbao != 0) {
				result.put(YUANBAO_AWARD, new AwardItem(yuanbao));
			}
			long exp = distributeExpAward(roleid, awardID, paras, pse,1, "");
			if (exp != 0) {
				result.put(EXP_AWARD, new AwardItem(exp));
			}
			
			AwardItem fAward = distributeFirstClassAward(roleid, awardID, paras);
			if (fAward.itemNotEmpty())
				result.put(FIRSTC_AWARD, fAward);
			Map<Integer, AwardItem> sAwardMap = distributeSecondClassAward(roleid, awardID, true, paras, pse);
			if (!sAwardMap.isEmpty()){
				UniteAwardsResult(result, sAwardMap);
			}
			//看看有没有额外奖励
			int externalAwardid = getExternalAwardid(awardID);
			if (externalAwardid > 0){
				Map<Integer, AwardItem> exAwardMap = distributeAllAward(roleid, externalAwardid, paras,notifynewitem, false);
			    UniteAwardsResult(result, exAwardMap);
			}
            if (call) 
            	awardidHistoryMap.remove(roleid);
            List<AddItem> awarditems = getAwardItems(result);
            
            if(notifynewitem)
            {
            	SShowAddItem snd = new SShowAddItem();
            	for(AddItem additem : awarditems)
            		snd.data.add(new ShowItemData(additem.getKey(), Conv.toShort(additem.getId()), Conv.toShort(additem.getNum())));
            	if(!snd.data.isEmpty())
            		xdb.Procedure.psendWhileCommit(roleid, snd);
            }
		} catch (Exception e) {
			StringBuffer sb = new StringBuffer();
			sb.append("Distribute award failed:").append("roleid ").append(roleid).append("awardid ").append(awardID);
			String str = sb.toString();
			logger.info(str, e);
			awardidHistoryMap.remove(roleid);
		}
		return result;
	}
	
	/**
	 * getExternalAwardid:(这里用一句话描述这个方法的作用)
	 *
	 * @param awardID
	 * @return    
	 * int    
	 * @throws 
	 * @since  　
	*/
	private int getExternalAwardid(int awardID) {
		if(externalAwardMap == null)
			return 0;
		ExAward externalAward = externalAwardMap.get(awardID);
		if (externalAward!=null){
			//SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH：mm：ss");
			try {
				long curTime = chuhan.gsp.main.GameTime.currentTimeMillis();
				int size = externalAward.getStartTimes().size();
			    for (int i = 0; i < size; i++) {
					long startTime = externalAward.getStartTimes().get(i);
					long endTime = externalAward.getEndTimes().get(i);
					String zoneid = externalAward.getZoneids().get(i);
					if (startTime < curTime && curTime < endTime)
						if (zoneid.equals("0")||zoneid.indexOf(String.valueOf(ConfigManager.getGsZoneId()))!=-1) {
							return externalAward.getExawardid();
						}
				}
			} catch (Exception e) {
				StringBuffer sb = new StringBuffer();
				sb.append("find External award failed:").append("awardid ").append(awardID);
				String str = sb.toString();
				logger.info(str, e);
			}
		}
		return 0;
	}


	/**
	 * 将add所表示的奖励结果，合并到result所指的奖励结果。将同类型的奖励，合并。
	 * 
	 * @param result
	 *            最终的奖励结果
	 * @param add
	 *            需要添加的奖励结果
	 * @return result[out]合并后的奖励结果
	 */
	private void UniteAwardsResult(final Map<Integer, AwardItem> result, final Map<Integer, AwardItem> add) {

		if (result != null && add != null && !add.isEmpty()) {
			java.util.Iterator<Integer> iter = add.keySet().iterator();
			while (iter.hasNext()) {
				Integer key = iter.next();
				if (result.containsKey(key)) {
					switch (key) {
					case FIRSTC_AWARD:
					case SECONDC_AWARD: {
						result.get(key).getItems().addAll(add.get(key).getItems());
					}
						break;
					// result 和 add 中，对于一下金钱和奖励奖励的结果只有一个
					case MONEY_AWARD:
					case EXP_AWARD:
					case HERO_1_EXP_AWARD: 
					case HERO_2_EXP_AWARD: 
					case HERO_3_EXP_AWARD: 
					case HERO_4_EXP_AWARD: 
					case HERO_5_EXP_AWARD: 
					{
						long oldValue = result.get(key).getValue();
						long addValue = add.get(key).getValue();
						result.get(key).setValue(oldValue + addValue);
					}
						break;
					}
				} else
					result.put(key, add.get(key));
			}
		}
		return;

	}

	// public Map<Integer, List<AwardItem>> distributeAllAward(long roleid,
	// List<Integer> awardIDs,Map<String, Object> paras){
	// Map<Integer, List<AwardItem>> result = new HashMap<Integer,
	// List<AwardItem>>();
	// for (Integer awardID : awardIDs) {
	// Map<Integer, List<AwardItem>> res=distributeAllAward(roleid, awardID,
	// paras);
	//
	// }
	// return result;
	// }
	/**
	 * 
	 * distributeMoneyAward:发送奖励表中的金钱奖励,金钱奖励是公式,所以要有一个map来存放变量和变量值
	 * 
	 * @param roleid
	 * @param awardID
	 * @param paras
	 * @return long
	 * @throws
	 * @since 　
	 */
	long distributeMoneyAward(long roleid, int awardID, Map<String, Object> paras, ParseStringExpression pse) {

		String str = moneyAwardMap.get(awardID);
		if (str == null) {
			return 0;
		}
		long awardMoney = 0;
		try {
			Double value = pse.ParseStr(str, paras);
			awardMoney = value.longValue();
		} catch (ScriptException e) {
			logger.error("error when parsing money award", e);
			return 0;
		}
		chuhan.gsp.item.Bag bag = new chuhan.gsp.item.Bag(roleid, false);
		long realAdd = bag.addMoney(awardMoney, "awardID:" + awardID, 
				covertItemlog2moneylog(awardID,0, 0), awardID);
		/*if (showMsg&&realAdd>0)
			MessageUtil.psendAddorRemoveMoney(roleid, realAdd);*/
		if (realAdd != awardMoney) {
			logger.error("add money failed!reason:" + "awardID:" + awardID);
			//throw new RuntimeException("add money failed!reason:" + "awardID:" + awardID);
		}
		return realAdd;
	}
	
	/**
	 * 
	 * distributeYuanBaoAward:发送奖励表中的元宝奖励,元宝奖励是公式,所以要有一个map来存放变量和变量值
	 * 
	 * @param roleid
	 * @param awardID
	 * @param paras
	 * @return long
	 * @throws
	 * @since 　
	 */
	int distributeYuanBaoAward(long roleid, int awardID, Map<String, Object> paras, ParseStringExpression pse) {

		String str = sYuanbaoAwardMap.get(awardID);
		if (str == null) {
			return 0;
		}
		int awardYuanbao = 0;
		try {
			Double value = pse.ParseStr(str, paras);
			awardYuanbao = value.intValue();
		} catch (ScriptException e) {
			logger.error("error when parsing Yuanbao award", e);
			return 0;
		}
		PropRole prole = PropRole.getPropRole(roleid, false);
		int realAdd = prole.addYuanBao(awardYuanbao, YuanBaoAddType.DISTRIBUTE_AWARD, awardID);
		/*long realAdd = bag.addMoney(awardYuanbao, "awardID:" + awardID, 
				covertItemlog2moneylog(awardID,countertype, xiangguanid), awardID);*/
		/*if (showMsg&&realAdd>0)
			MessageUtil.psendAddorRemoveMoney(roleid, realAdd);*/
		if (realAdd != awardYuanbao) {
			logger.error("add money failed!reason:" + "awardID:" + awardID);
			//throw new RuntimeException("add money failed!reason:" + "awardID:" + awardID);
		}
		return realAdd;
	}
	
	private int covertItemlog2moneylog(final int awardid,final int itemlogtype, final int xiangguanid) {
		
	/*	if (awardid == 1209){//如果是挖铲子获得游戏币,bug17372
			return 63;
		}
		switch (itemlogtype) {
		case gs.counter.ItemIOState.tujing_Value_Renwu: {
			if (xiangguanid == SpecialQuestID.zhenshouquestid) {
				return gs.counter.MoneyExpIOState.tujing_Value_ZhenshouTask;
			}
			return gs.counter.MoneyExpIOState.tujing_Value_Task;
		}
		case gs.counter.ItemIOState.tujing_Value_WorldPuzzle:
			return gs.counter.MoneyExpIOState.tujing_Value_WorldPuzzleReward;
		case gs.counter.ItemIOState.tujing_Value_EnchouLu:
			return gs.counter.MoneyExpIOState.tujing_Value_Other;
		case gs.counter.ItemIOState.tujing_Value_TransportTask:
			return gs.counter.MoneyExpIOState.tujing_Value_TransSuppliesTask;
		case gs.counter.ItemIOState.tujing_Value_PresentPacket:
			return gs.counter.MoneyExpIOState.tujing_Value_PresentPacket;
		case gs.counter.ItemIOState.tujing_Value_FestivalPresent:
			return gs.counter.MoneyExpIOState.tujing_Value_FestivalPresent;
		case gs.counter.ItemIOState.tujing_Value_PickupItem:
			return gs.counter.MoneyExpIOState.tujing_Value_PickupItem;
		}*/
		return 0;
	}

	/**
	 * 
	 * distributeExpAward:发送奖励表中的pet经验奖励,经验奖励是公式,所以要有一个map来存放变量和变量值
	 * 
	 * @param roleid
	 * @param awardID
	 * @param paras
	 * @return long
	 * @throws
	 * @since 　
	 */
	/*void distributeHeroExpAward(long roleid, int awardID, Map<String, Object> paras, ParseStringExpression pse, boolean showMsg, Map<Integer, AwardItem> result) {

		String str = heroExpAwardMap.get(awardID);
		if (str == null) {
			return;
		}
		HeroColumn herocol = HeroColumn.getHeroColumn(roleid, false);
		int hero_exp_id = HERO_1_EXP_AWARD-1;
		for(Troop troop : herocol.getTroops())
		{
			hero_exp_id++;
			Hero hero = herocol.getHero(troop.getHerokey());
			if(hero == null)
				continue;
			paras.put(HERO_LEVEL, hero.getLevel());
			int exp = 0;
			try {
				Double value = pse.ParseStr(str, paras);
				exp = value.intValue();
			} catch (ScriptException e) {
				logger.error("error when parsing petexp award", e);
				continue;
			}
			new PAddTroopHeroExp(roleid, troop.getPos(), exp).call();
			result.put(hero_exp_id, new AwardItem(exp));
		}
		return;
	}*/

	/**
	 * 
	 * distributeExpAward:发送奖励表中的经验奖励,经验奖励是公式,所以要有一个map来存放变量和变量值
	 * 
	 * @param roleid
	 * @param awardID
	 * @param paras
	 * @return long
	 * @throws
	 * @since 　
	 */
	long distributeExpAward(long roleid, int awardID, Map<String, Object> paras, ParseStringExpression pse, int reason, String hint) {

		String str = expAwardMap.get(awardID);
		if (str == null) {
			return 0;
		}
		long exp = 0;
		try {
			Double value = pse.ParseStr(str, paras);
			exp = value.longValue();
		} catch (ScriptException e) {
			logger.error("error when parsing exp award", e);
			return 0;
		}
		if (hint == null)
			hint = "";
		PAddExpProc proc = new PAddExpProc(roleid, Conv.toInt(exp), reason,hint);//TODO 给人物经验
		boolean res = proc.call();
		if (res) {
			return exp;
		} else {
			return 0;
		}
	}
	
	/**
	 *  返回额外经验加成消息的参数
	 * @param expBase
	 * @param paras
	 * @param extraExpInfo
	 * @return
	 */
	private List<String> getExtraExpParams(long expBase,Map<String, Object> paras, Map<Integer, Integer> extraExpInfo){
		extraExpInfo.put(0, 0);
		List<String> msgParams = new ArrayList<String>();
		msgParams.add(String.valueOf(expBase));  //获得的总经验
		boolean mengpaiExtra = extraExpInfo.containsKey(1);
		/*boolean swearExtra = extraExpInfo.containsKey(2);
		if (mengpaiExtra && swearExtra) {
			msgParams.add(String.valueOf(zhiliaofengyinPercent));  //门派加成百分数
			msgParams.add(SwearRelationManager.getInstance().getSwearExtraExpAwardPercent(paras));  //结拜加成百分数
			extraExpInfo.put(0, 144938);
		} else if (mengpaiExtra) {
			msgParams.add(String.valueOf(zhiliaofengyinPercent));  //门派加成百分数
			extraExpInfo.put(0, 145081);
		} else if (swearExtra) {
			msgParams.add(SwearRelationManager.getInstance().getSwearExtraExpAwardPercent(paras));  //结拜加成百分数
			extraExpInfo.put(0, 145082);
		}*/
		
		return msgParams;
	}
	
	private int getTradeMulti(Map<String, Object> paras)
	{
		try
		{
			if(paras == null)
				return 1;
			Object o = paras.get(TRADE_GRADE_EXP);
			if(o == null)
				return 1;
			return (Integer)o;
		}catch(Exception e)
		{
			e.printStackTrace();
		}
		return 1;
	}

	/**
	 * 
	 * distributeFirstClassAward:发放必给物品奖励.必须在存储过程中调用
	 * 
	 * @param @param roleid 获奖的玩家id
	 * @param @param awardID 奖励表的id,根据这个id找到必给物品
	 * @param @return
	 * @return List<AwardItem> 返回奖励的必给物品,可能是多个.返回这个list可以用来发奖励信息
	 * @throws
	 * @since 　
	 */
	AwardItem distributeFirstClassAward(long roleid, int awardID, Map<String, Object> paras) {

		AwardItem result = new AwardItem();
		int multinum = getTradeMulti(paras);
		List<FirstClassAwardItem> awardItems = firstCAwardMap.get(awardID);
		if (awardItems != null)
			for (FirstClassAwardItem awardItem : awardItems) {
				// 拿到必给物品的id
				int id = awardItem.getItemID();
				// 根据这个id去奖励的二级表找物品
				SItemList items = award2Map.get(id);
				if (items == null)
					continue;
				// 从items中随机一个物品,随机到的物品是必给的
				int size = items.items.size();
				int randomValue = Misc.getRandomBetween(0, size - 1);
				int itemID = items.items.get(randomValue);
				// 要给多少个
				int itemNum = awardItem.getItemNum() * multinum;
				// 给玩家物品
				AddItemResult addresult = giveItem(roleid, awardID, itemID, itemNum);
				if (addresult.isSuccess())
					result.getItems().addAll(addresult.getAddItems());
					//result.getItems().add(new Item(itemID, realAddNum, awardItem.getProp(), awardItem.getPropValue()));
			}
		return result;
	}

	/**
	 * 
	 * distributeSecondClassAward:有时候随机物品的奖励id也只有一个,为了方便,增加这个wrap method
	 * 
	 * @param @param roleid
	 * @param @param awardIDs
	 * @param @return
	 * @return List<AwardItem>
	 * @throws
	 * @since 　
	 */
	private Map<Integer, AwardItem> distributeSecondClassAward(long roleid, int awardID, boolean playCG, Map<String, Object> paras, ParseStringExpression pse) {

		Map<Integer, AwardItem> result = new HashMap<Integer, AwardItem>();
		SecondClassAwardItems secondClassAwardItems = secondCAwardMap.get(awardID);
		if (secondClassAwardItems == null)
			return result;

		List<SecondClassAwardItem> awardItems = secondClassAwardItems.getItems();
		if (awardItems == null || awardItems.size() == 0) {
			return result;
		}

		if (secondClassAwardItems.getRandomType() == SecondClassAwardItems.RELATED_RANDOM) {
			// 判断给不给的总开关
			int totalProb = 0;
			try {
				Double value = pse.ParseStr(secondClassAwardItems.getTotalProb(), paras);
				totalProb = value.intValue();
			} catch (ScriptException e) {
				logger.error("error when parsing second class award prob.awardid:"+awardID, e);
			}

			int base = secondClassAwardItems.getBase();
			if (Misc.checkRate(base - 1, totalProb))
				giveRelatedRandomItems(roleid, awardID, awardItems, result, paras, pse);
		} else if (secondClassAwardItems.getRandomType() == SecondClassAwardItems.UNRELATED_RANDOM) {
			giveUnrelatedRandomItems(roleid, awardID, awardItems, secondClassAwardItems, playCG, result, paras, pse);
		}
		return result;
	}


	/**
	 * giveUnrelatedRandomItems:不关联随机的情况下给予随机奖励
	 * 
	 * @param roleid
	 * @param awardItems
	 * @param secondClassAwardItems
	 * @param result
	 *            void
	 * @throws
	 * @since 　
	 */

	private void giveUnrelatedRandomItems(long roleid, int awardid, List<SecondClassAwardItem> awardItems, SecondClassAwardItems secondClassAwardItems,
			boolean playCG, Map<Integer, AwardItem> result, Map<String, Object> paras, ParseStringExpression pse) {

		int base = secondClassAwardItems.getBase();

		for (SecondClassAwardItem item : awardItems) {
			int prob = 0;
			try {
				Double value = pse.ParseStr(item.getRatio(), paras);
				prob = value.intValue();
			} catch (ScriptException e) {
				logger.error("error when parsing second class award prob.awardid:"+awardid+"ratio:" + item.getRatio(), e);
			}

			if (Misc.checkRate(base - 1, prob)) {
				if (item.getItemID() >= 100000)// 大于100000表示不是递归的
					giveRoleItem(roleid, awardid, item, result);
				else {// 否则是递归的,还要去奖励表中找下一行
					Map<Integer, AwardItem> tmp = distributeAllAward(roleid, item.getItemID(), paras, false,false);
					UniteAwardsResult(result, tmp);
				}
			}
		}
	}

	/**
	 * giveRelatedRandomItems:关联随机的情况下给予随机奖励
	 * 
	 * @param result
	 * @param secondClassAwardItems
	 * @param awardItems
	 *            void
	 * @throws
	 * @since 　
	 */

	private void giveRelatedRandomItems(long roleid, int awardID, List<SecondClassAwardItem> awardItems, Map<Integer, AwardItem> result,
			Map<String, Object> paras, ParseStringExpression pse) {

		// 随机物品中根据概率选出一个
		//int pros[] = new int[awardItems.size()];
		List<Integer> pros = new ArrayList<Integer>();
		boolean allProbZero = true;
		for (int i = 0; i < awardItems.size(); i++) {
			try {
				Double value = pse.ParseStr(awardItems.get(i).getRatio(), paras);
				pros.add(value.intValue());
				if (value > 0)
					allProbZero = false;

			} catch (ScriptException e) {
				logger.error("error when parsing second class award prob.awardid:"+awardID, e);
				pros.add(0);
			}

		} 
		if (allProbZero){
			logger.error("error when parsing second class award: prob all zero.awardid:"+awardID);
			return ;
		}
		int index = Misc.getProbability(pros);
		SecondClassAwardItem awardItem = awardItems.get(index);
		if (awardItem.getItemID() < 10000)// 小于10000表示不是递归的
			giveRoleItem(roleid, awardID, awardItem, result);
		else {// 否则是递归的,还要去奖励表中找下一行
			Map<Integer, AwardItem> tmp = distributeAllAward(roleid, awardItem.getItemID(), paras, false,false);// 这个奖励不可能给经验,所以最后两个参数无所谓
			UniteAwardsResult(result, tmp);
		}
	}

	/**
	 * giveRoleItem:(这里用一句话描述这个方法的作用)
	 * 
	 * @param roleid
	 * @param awardItem
	 * @param result
	 *            void
	 * @throws
	 * @since 　
	 */

	private int giveRoleItem(long roleid, int awardID, SecondClassAwardItem awardItem, Map<Integer, AwardItem> result) {

		// 拿到随机物品的id
		int id = awardItem.getItemID();
		// 根据这个id去奖励的二级表找物品
		SItemList items = award2Map.get(id);
		if (items == null)
			return 0;
		// 从items中随机一个物品,随机到的物品是必给的
		int size = items.items.size();
		int randomValue = Misc.getRandomBetween(0, size - 1);
		int itemID = items.items.get(randomValue);
		// 要给多少个
		int itemNum = awardItem.getItemNum();
		int realAddNum = 0;
		AddItemResult addresult = giveItem(roleid,awardID,itemID,itemNum);
		if (addresult.isSuccess() && !addresult.getAddItems().isEmpty()){
			AwardItem aItem = result.get(AwardManager.SECONDC_AWARD);
			if (aItem == null){
				aItem = new AwardItem();
				result.put(AwardManager.SECONDC_AWARD, aItem);
			}
			aItem.getItems().addAll(addresult.getAddItems());
		}
	
		return realAddNum;
	}
	
	public static List<AddItem> getAwardItems(Map<Integer,AwardItem> awards)
	{
		List<AddItem> adds = new LinkedList<AddItem>();
		AwardItem award = awards.get(FIRSTC_AWARD);
		if(award != null)
			adds.addAll(award.getItems());
		award = awards.get(SECONDC_AWARD);
		if(award != null)
			adds.addAll(award.getItems());
		return adds;
		
	}
	
	/**
	 * giveItem:(这里用一句话描述这个方法的作用)
	 *
	 * @param roleid
	 * @param awardID
	 * @param itemID
	 * @param itemNum
	 * @return    
	 * int    
	 * @throws 
	 * @since  　
	*/
	
	private AddItemResult giveItem(long roleid, int awardID, int itemID, int itemNum) {
		// 给玩家物品
		itemID = checkAwardLimited(roleid, itemID,awardID);
		AddItemResult addresult = addItemToBag(roleid,  awardID, itemID, itemNum);
		addAwardLimit(roleid, itemID, itemNum,awardID);
		return addresult;
		/*if (showMsg&&realAddNum>0)
			MessageUtil.psendAddItemWhileCommit(roleid, itemID, realAddNum);*/
		/*if (realAddNum != itemNum) {
			if (realAddNum < 0 || realAddNum > itemNum){
				logger.error("add more items!itemid:" + itemID);
				return 0;
			}
			if (bag.isFull())
				if (realAddNum == 0)
					chuhan.gsp.msg.Message.sendMsgNotify(roleid, 142856, null);
				else
					chuhan.gsp.msg.Message.sendMsgNotify(roleid, 142857, null);

			if (realAddNum!=itemNum){
				logger.error("add item failed: itemid:" + itemID + " number:" + itemNum+".pls check your bag capacity");
			    //realAddNum为0的时候,说明一个物品
			}
		}*/
		
		//return realAddNum;
	}

	
	private int checkAwardLimited(long roleId, int itemID, int awardID)
	{
		if(itemID != Constant.PIECE_ITEM_WEAPON 
				&& itemID != Constant.PIECE_ITEM_ARMOR 
				&& itemID != Constant.PIECE_ITEM_HORSE)
			return itemID;
		/*if(awardID >= 100001)
			return itemID;
		int nowday = DateUtil.getCurrentDay(GameTime.currentTimeMillis());
		xbean.AwardLimitRole xlimitrole = xtable.Awardlimitroles.get(roleId);
		if(xlimitrole == null)
			return itemID;
		
		xbean.AwardLimitDay limitday = xlimitrole.getLimitdays().get(nowday);
		if(limitday == null)
			return itemID;
		if(itemID == Constant.PIECE_ITEM_WEAPON || itemID == Constant.PIECE_ITEM_ARMOR || itemID == Constant.PIECE_ITEM_HORSE)
		{
			Integer alreadynum = limitday.getLimititems().get(0);
			if(alreadynum == null)
				return itemID;
			if(alreadynum >= 15)
				return Constant.PEIYANG_ITEM;
		}*/
		
		return itemID;
	}
	
	private void addAwardLimit(long roleId, int itemID, int itemNum, int awardID)
	{
//		if(itemID != Constant.PIECE_ITEM_WEAPON 
//				&& itemID != Constant.PIECE_ITEM_ARMOR 
//				&& itemID != Constant.PIECE_ITEM_HORSE)
//			return;
//		if(awardID >= 100001)
//			return;
//		int nowday = DateUtil.getCurrentDay(GameTime.currentTimeMillis());
//		xbean.AwardLimitRole xlimitrole = xtable.Awardlimitroles.get(roleId);
//		if(xlimitrole == null)
//		{
//			xlimitrole = xbean.Pod.newAwardLimitRole();
//			xtable.Awardlimitroles.insert(roleId, xlimitrole);
//		}
//		xbean.AwardLimitDay limitday = xlimitrole.getLimitdays().get(nowday);
//		if(limitday == null)
//		{
//			limitday = xbean.Pod.newAwardLimitDay();
//			xlimitrole.getLimitdays().put(nowday, limitday);
//		}
//		if(itemID == Constant.PIECE_ITEM_WEAPON || itemID == Constant.PIECE_ITEM_ARMOR || itemID == Constant.PIECE_ITEM_HORSE)
//		{
//			Integer alreadynum = limitday.getLimititems().get(0);
//			if(alreadynum == null)
//				alreadynum = 0;
//			alreadynum+=itemNum;
//			limitday.getLimititems().put(0, alreadynum);
//		}
	}
	
	private AddItemResult addItemToBag(long roleid,int awardID, int itemID, int itemNum) {
		// 根据奖励的不同，添加到不同包裹
		ItemColumn itemcol = chuhan.gsp.item.Module.getItemColumnByItemId(roleid, itemID, false);
		if(itemcol == null)//是加英雄？
			return giveHero(roleid, awardID, itemID, itemNum);
		return itemcol.addItem(itemID, itemNum, "award", 0 );
	}
	
	private AddItemResult giveHero(long roleid,int awardID, int itemID, int itemNum)
	{
		AddItemResult addresult = new AddItemResult(AddResultEnum.SUCC);
		xbean.Properties xprop = xtable.Properties.get(roleid);
		OldHeroColumn herocol = OldHeroColumn.getHeroColumn(roleid, false);
		OldHero hero = herocol.createAddHero(itemID, xprop.getLevel());
//by yanglk  hero		addresult.getAddItems().add(new AddItem(hero.getHeroInfo().getKey(), hero.getHeroInfo().getId() , 1));
		return addresult;
	}

	public java.util.NavigableMap<Integer, SItemList> getSActivityAwardItemMap() {

		return award2Map;
	}

	public java.util.Map<Integer, String> getExpAwardMap() {

		return expAwardMap;
	}

	public java.util.Map<Integer, String> getPetExpAwardMap() {

		return heroExpAwardMap;
	}
	
	public java.util.Map<Integer, String> getSwAwardMap(){
		
		return swAwardMap;
	}

	// 一些有没有双倍,三倍时间的参数,可以调这个方法统一存入
	public static void putNeededParas(long roleid, Map<String, Object> paras) {

		/*BuffAgent agent = new BuffRoleImpl(roleid, true);
		boolean hasMultiExpBuff = false;
		if (agent.existBuff(BuffID.SysTriExpTime)) {
			paras.put(IS_SYS_TRIPLE_EXP, 1);
			hasMultiExpBuff = true;
		} else
			paras.put(IS_SYS_TRIPLE_EXP, 0);
		if (agent.existBuff(BuffID.ItemTriExpTime)) {
			paras.put(IS_TRIPLE_EXP, 1);
			hasMultiExpBuff = true;
		} else
			paras.put(IS_TRIPLE_EXP, 0);
		if (agent.existBuff(BuffID.SysDupExpTime)) {
			paras.put(IS_DOUBLE_EXP, 1);
			hasMultiExpBuff = true;
		} else
			paras.put(IS_DOUBLE_EXP, 0);
		if (hasMultiExpBuff)
			paras.put(IS_NO_MULTI_EXP, 0);
		else
			paras.put(IS_NO_MULTI_EXP, 1);
		if (agent.existBuff(PUsePetTripleItem.PET_TRIPLE_BUFF))
			paras.put(IS_PET_TRIBLE, 1);
		else
			paras.put(IS_PET_TRIBLE, 0);
		if(agent.existBuff(BuffConstant.MASTER_HELP_BUFF))
			paras.put(IS_MASTER_HELP, 1);
		else
			paras.put(IS_MASTER_HELP, 0);*/
		
		paras.put(IWEB_MULTI_EXP, iwebMultiExp);
		paras.put(WEEK_DAY, DateUtil.getCurrentWeekDay());
//		Team team = TeamManager.selectTeamByRoleId(roleid);
//		if (team != null && team.getTeamLeaderId() == roleid)
//			paras.put(IS_LEADER, 1);
//		else
//			paras.put(IS_LEADER, 0);
		xbean.Properties role = xtable.Properties.select(roleid);
		paras.put(ROLE_LEVEL, role.getLevel());
		paras.put(VIP_LEVEL, role.getViplv());
		/*int petkey = role.getFightpetkey();
		if (petkey <= 0)
			paras.put(PET_LEVEL, 0);
		else {
			final PetColumn petcol = new PetColumn(roleid, PetColumnTypes.PET, true);
			final xbean.PetInfo xpetinfo = petcol.getPetInfo(petkey);
			if (xpetinfo != null){
				int level = xpetinfo.getLevel();
				paras.put(PET_LEVEL, level);
			}
		}*/
	}

	public java.util.NavigableMap<Integer, SItemList> getAward2Map() {

		return award2Map;
	}

	/*public void putBattleParas(Map<String, Object> paras, xbean.BattleInfo battle) {

		int mapLevel = 0;
		if (battle.getBattletype() % 10 == 0) {
			SMineArea hideAreaInfo = null;
			SShowAreainfo showAreainfo = null;

			if (battle.getAreatype() == xbean.BattleInfo.AREA_BATTLEHIDE) {
				hideAreaInfo = BattleEndHandler.hideMapConfigs.get(battle.getAreaconf());
				mapLevel = hideAreaInfo.getLevel();

			} else if (battle.getAreatype() == xbean.BattleInfo.AREA_BATTLESHOW) {
				showAreainfo = BattleEndHandler.showMapConfigs.get(battle.getAreaconf());
				mapLevel = showAreainfo.getTypelevel();

			}
		}
		paras.put("MapLv", mapLevel);
		paras.put("MasterNum", battle.getBattledatas().get(xbean.BattleInfo.DATA_MONSTER_CHIEF_NUM).intValue());
		paras.put("MonsterNum", battle.getBattledatas().get(xbean.BattleInfo.DATA_MONSTER_NUM).intValue());
		paras.put("TeamNum", battle.getBattledatas().get(xbean.BattleInfo.DATA_HOST_ROLE_NUM).intValue());
		paras.put("TeamLv", battle.getBattledatas().get(xbean.BattleInfo.DATA_HOST_ROLE_AVERAGE_LEVEL));
		paras.put("MonsterLv", battle.getBattledatas().get(xbean.BattleInfo.DATA_MONSTER_AVERAGE_LEVEL));
	}*/



	public void init() throws Exception {
       initData();
	}

	/**
	 * 
	 * calculateExpOrMoney:有的情况下,并不需要立刻给经验或金钱,而是先把值告诉玩家,玩家来决定要不要做这个任务.
	 *
	 * @param awardid
	 * @param type
	 * @return    
	 * long    
	 * @throws 
	 * @since  　
	 */
	public  long calculateExpOrMoney(int awardID,int type,Map<String, Object> paras){
		long result = 0;
		ParseStringExpression pse = new ParseStringExpression();
		if (type == AwardManager.EXP_AWARD){
			String str = expAwardMap.get(awardID);
			if (str == null) {
				return 0;
			}
			try {
				Double value = pse.ParseStr(str, paras);
				result = value.longValue();
			} catch (ScriptException e) {
				return 0;
			}
		}else if (type == AwardManager.MONEY_AWARD) {
			String str = moneyAwardMap.get(awardID);
			if (str == null) {
				return 0;
			}
			try {
				Double value = pse.ParseStr(str, paras);
				result = value.longValue();
			} catch (ScriptException e) {
				return 0;
			}
			
		}else if (type == AwardManager.YUANBAO_AWARD) {
			String str = sYuanbaoAwardMap.get(awardID);
			if (str == null) {
				return 0;
			}
			try {
				Double value = pse.ParseStr(str, paras);
				result = value.longValue();
			} catch (ScriptException e) {
				return 0;
			}
			
		}
		return result;
	}

	/**
	 * 
	 * setIwebMultiExp:0-没有多倍;1-双倍经验;2-三倍经验
	 *
	 * @param iwebMultiExp    
	 * void    
	 * @throws 
	 * @since  　
	 */
	public static boolean setIwebMultiExp(int multiExp) {
	
		if (multiExp<0 || multiExp >3)
			return false;
		if(iwebMultiExp == multiExp)
			return false;
		iwebMultiExp = multiExp;
		//gnet.link.Onlines.getInstance().broadcast(new SSendServerMultiExp(multiExp), 999);
		return true;
	}

	
	public java.util.Map<Integer, Set<Integer>> getClearLimitMap() {
	
		return clearLimitMap;
	}
	
	
	public static void processMacAward(long roleId, String mac)
	{
		xbean.MacInfo macinfo = xtable.Macinfos.get(mac);
		if(macinfo == null)
		{
			macinfo = xbean.Pod.newMacInfo();
			xtable.Macinfos.add(mac, macinfo);
		}
	}
}
