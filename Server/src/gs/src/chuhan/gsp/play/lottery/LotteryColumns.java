package chuhan.gsp.play.lottery;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;

import chuhan.gsp.attr.config10;
import chuhan.gsp.award.DropInit;
import chuhan.gsp.award.DropManager;
import chuhan.gsp.award.IDManager;
import chuhan.gsp.game.herorecruit51;
import chuhan.gsp.log.LogBehavior;
import chuhan.gsp.log.Logger;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.Message;
import chuhan.gsp.play.activity.ActivityGameManager;
import chuhan.gsp.play.activity.ActivityManager;
import chuhan.gsp.play.huoyuedu.HuoyueColumns;
import chuhan.gsp.util.DateUtil;
import chuhan.gsp.util.ParserString;

public class LotteryColumns {
	public static Logger logger = Logger.getLogger(LotteryColumns.class);

	public final static int SINGLE_MAP = 1;
	public final static int TEN_MAP = 2;
	public final static int TENSINGLT_MAP = 3;
	public final static int GETHERO_MAP = 4;

	final public long roleId;
	final public xbean.lotty xlottery;
	final boolean readonly;

	public static LotteryColumns getLotteryColumn(long roleId, boolean readonly) {
		if (xtable.Properties.select(roleId) == null)
			throw new IllegalArgumentException("构造LotteryColumns时，角色 " + roleId + " 不存在。");

		xbean.lotty lotterycol = null;
		if (readonly)
			lotterycol = xtable.Lottylist.select(roleId);
		else
			lotterycol = xtable.Lottylist.get(roleId);
		if (lotterycol == null) {
			if (readonly)
				lotterycol = xbean.Pod.newlottyData();
			else {
				lotterycol = xbean.Pod.newlotty();
				xtable.Lottylist.insert(roleId, lotterycol);
			}
		}
		if (!readonly) {
			TimeCheck(lotterycol);
		}
		return new LotteryColumns(roleId, lotterycol, readonly);
	}

	private LotteryColumns(long roleId, xbean.lotty xlottery, boolean readonly) {
		this.roleId = roleId;
		this.xlottery = xlottery;
		this.readonly = readonly;
		init();
	}

	public void init() {

	}

	public static void TimeCheck(xbean.lotty lotterycol) {
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		int refreshRecruitTime = Integer
				.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1372).configvalue);
		if (DateUtil.inTheSameDay(lotterycol.getNormalrecruittime(), now,
				-refreshRecruitTime * 60 * 60 * 1000) == false) {
			lotterycol.setNormalrecruitnum(0);
			lotterycol.setToprecruitnum(0);
		}
	}

	public boolean Lottery(int lotterytype) {
		TimeCheck(xlottery);
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		int freeTimesCd = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1370).configvalue)
				* 1000;
		List<chuhan.gsp.play.lottery.Items> items = new ArrayList<chuhan.gsp.play.lottery.Items>();
		String logType = "";
		List<Integer> drops;
		if (lotterytype == CLottery.NORMALONE) {
			int freeTimes = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1369).configvalue);
			if (((now - xlottery.getNormalrecruittime()) > freeTimesCd) && xlottery.getNormalrecruitnum() < freeTimes) {
				xlottery.setNormalrecruittime(now);
				logType = LogBehavior.LOTTERYFREE;
			} else {
				int cost = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1349).configvalue);
				if (!DropManager.getInstance().useDel(IDManager.YUANBAO, cost, roleId, LogBehavior.LOTTERYONECOST)) {
					return false;
				}
				logType = LogBehavior.LOTTERYONECOST;
			}
			xlottery.setNormalrecruitnum(xlottery.getNormalrecruitnum() + 1);
			drops = ParserString.strList2IntList(ParserString
					.parseString(ConfigManager.getInstance().getConf(config10.class).get(1341).configvalue));
			List<Integer> itemlist = DropManager.getInstance().drop(roleId, Integer.toString(drops.get(0)), logType,
					true);
			DropManager.getInstance().drop(roleId, Integer.toString(drops.get(1)), logType, true);
			items.add(DropManager.getInstance().getLottery(itemlist));
		} else if (lotterytype == CLottery.NORMALTEN) {
			int cost = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1350).configvalue);
			if (!DropManager.getInstance().useDel(IDManager.YUANBAO, cost, roleId, LogBehavior.LOTTERYONECOST)) {
				return false;
			}
			for (int i = 0; i < 8; i++) {
				xlottery.setNormalrecruitnum(xlottery.getNormalrecruitnum() + 1);
				drops = ParserString.strList2IntList(ParserString
						.parseString(ConfigManager.getInstance().getConf(config10.class).get(1341).configvalue));
				List<Integer> itemlist = DropManager.getInstance().drop(roleId, Integer.toString(drops.get(0)),
						LogBehavior.LOTTERYONECOST, true);
				DropManager.getInstance().drop(roleId, Integer.toString(drops.get(1)), logType, true);
				items.add(DropManager.getInstance().getLottery(itemlist));
			}
			xlottery.setNormalrecruitnum(xlottery.getNormalrecruitnum() + 1);
			drops = ParserString.strList2IntList(ParserString
					.parseString(ConfigManager.getInstance().getConf(config10.class).get(1342).configvalue));
			List<Integer> itemlist = DropManager.getInstance().drop(roleId, Integer.toString(drops.get(0)),
					LogBehavior.LOTTERYONECOST, true);
			DropManager.getInstance().drop(roleId, Integer.toString(drops.get(1)), logType, true);
			items.add(DropManager.getInstance().getLottery(itemlist));
		} else if (lotterytype == CLottery.TOPONE) {
			int freeTimes = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1371).configvalue);
			if (((now - xlottery.getToprecruittime()) > freeTimesCd) && xlottery.getToprecruitnum() < freeTimes) {
				xlottery.setToprecruittime(now);
				logType = LogBehavior.LOTTERYFREE;
			} else {
				int cost = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1351).configvalue);
				if (!DropManager.getInstance().useDel(IDManager.YUANBAO, cost, roleId, LogBehavior.LOTTERYONECOST)) {
					return false;
				}
				logType = LogBehavior.LOTTERYONECOST;
			}
			
			if (xlottery.getToprecruitheronum() == 8) {
				drops = ParserString.strList2IntList(ParserString
						.parseString(ConfigManager.getInstance().getConf(config10.class).get(1344).configvalue));
				xlottery.setToprecruitheronum(0);
			} else {
				drops = ParserString.strList2IntList(ParserString
						.parseString(ConfigManager.getInstance().getConf(config10.class).get(1343).configvalue));
				xlottery.setToprecruitheronum(xlottery.getToprecruitheronum() + 1);
			}
			xlottery.setToprecruitnum(xlottery.getToprecruitnum() + 1);
			List<Integer> itemlist = DropManager.getInstance().drop(roleId, Integer.toString(drops.get(0)), logType,
					true);
			DropManager.getInstance().drop(roleId, Integer.toString(drops.get(1)), logType, true);
			items.add(DropManager.getInstance().getLottery(itemlist));
		} else if (lotterytype == CLottery.TOPTEN) {
			int cost = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1352).configvalue);
			if (!DropManager.getInstance().useDel(IDManager.YUANBAO, cost, roleId, LogBehavior.LOTTERYONECOST)) {
				return false;
			}
			for (int i = 0; i < 8; i++) {
				xlottery.setToprecruitnum(xlottery.getToprecruitnum() + 1);
				drops = ParserString.strList2IntList(ParserString
						.parseString(ConfigManager.getInstance().getConf(config10.class).get(1343).configvalue));
				List<Integer> itemlist = DropManager.getInstance().drop(roleId, Integer.toString(drops.get(0)),
						LogBehavior.LOTTERYONECOST, true);
				DropManager.getInstance().drop(roleId, Integer.toString(drops.get(1)), logType, true);
				items.add(DropManager.getInstance().getLottery(itemlist));
			}
			xlottery.setToprecruitnum(xlottery.getToprecruitnum() + 1);
			drops = ParserString.strList2IntList(ParserString
					.parseString(ConfigManager.getInstance().getConf(config10.class).get(1344).configvalue));
			List<Integer> itemlist = DropManager.getInstance().drop(roleId, Integer.toString(drops.get(0)),
					LogBehavior.LOTTERYONECOST, true);
			DropManager.getInstance().drop(roleId, Integer.toString(drops.get(1)), logType, true);
			items.add(DropManager.getInstance().getLottery(itemlist));
		}
		sendSLotteryNew(items, lotterytype);
		return true;
	}

	/**
	 * 抽奖入口
	 * 
	 * @param lotterytype
	 * @return
	 */
	public boolean LotteryEntry(int lotterytype) {
		switch (lotterytype) {
		case CLottery.NORMALONE:
			return lotteryOne(lotterytype);
		case CLottery.NORMALTEN:
			return lotteryOne(lotterytype);
		case CLottery.TOPONE:
			return lotteryTen(lotterytype);
		case CLottery.TOPTEN:
			return lotteryDream(true, 0);
		// case CLottery.FREE:
		// return lotteryOne(lotterytype);
		// case CLottery.ONE:
		// return lotteryOne(lotterytype);
		// case CLottery.TEN:
		// return lotteryTen(lotterytype);
		// case CLottery.DREAM:
		// return lotteryDream(true,0);
		}
		return false;
	}

	/**
	 * 单抽
	 * 
	 * @param isFree
	 * @return
	 */
	public boolean lotteryOne(int lotterytype) {
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		if (lotterytype == CLottery.FREE) {
			// 第一次新手引导特殊处理
			if (xlottery.getFreetime() == 0) {
				List<Integer> result = new LinkedList<Integer>();
				int heroId = Integer
						.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1329).configvalue);
				result.add(heroId);

				int free = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1289).configvalue);
				xlottery.setFreetime(now + free * DateUtil.minuteMills);

				// 添加英雄
				addHero(result, LogBehavior.LOTTERYFREE);
				// 添加固定物品
				this.addItem(lotterytype, LogBehavior.LOTTERYFREE);
				// 刷新抽奖数据
				this.sSRefreshLotty(now);
				// 返回成功
				sendSLottery(result, lotterytype);

				ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.LOTTERY, 1);
				return true;
			}
			if (now < xlottery.getFreetime()) {
				Message.psendMsgNotify(roleId, 135);
				return false;
			}
			int free = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1289).configvalue);
			xlottery.setFreetime(now + free * DateUtil.minuteMills);
		} else {
			int cost = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1275).configvalue);
			float discount = ActivityGameManager.getInstance().getZMherodiscount();
			cost = (int) ((float) cost * discount);
			if (!DropManager.getInstance().useDel(IDManager.YUANBAO, cost, roleId, LogBehavior.LOTTERYONECOST)) {
				return false;
			}
		}

		Map<Integer, Integer> oddsMap = null;
		Map<Integer, DropInit> dropInitMap = null;
		if (xlottery.getFirstget() == 0 && lotterytype != CLottery.FREE) {
			oddsMap = this.getOddsMap(TENSINGLT_MAP);
			dropInitMap = this.getInitMap(TENSINGLT_MAP);
			xlottery.setFirstget(1);
		} else {
			oddsMap = this.getOddsMap(SINGLE_MAP);
			dropInitMap = this.getInitMap(SINGLE_MAP);
		}
		List<Integer> result = getDropList(dropInitMap, oddsMap, 1);
		if (result.size() == 1) {
			// 伪随机计数
			addOdds(result, oddsMap, dropInitMap);
			// 梦想值增加
			addDreamNum(lotterytype);
			// 添加英雄
			addHero(result, LogBehavior.LOTTERYONE);
			// 添加固定物品
			this.addItem(lotterytype, LogBehavior.LOTTERYONE);
			// 刷新抽奖数据
			this.sSRefreshLotty(now);
			// 返回成功
			sendSLottery(result, lotterytype);

			ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.LOTTERY, 1);

			return true;
		}
		return false;
	}

	/**
	 * 十连抽
	 * 
	 * @return
	 */
	public boolean lotteryTen(int lotterytype) {
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		int cost = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1276).configvalue);
		float discount = ActivityGameManager.getInstance().getZMherodiscount();
		cost = (int) ((float) cost * discount);
		if (!DropManager.getInstance().useDel(IDManager.YUANBAO, cost, roleId, LogBehavior.LOTTERYTENCOST)) {
			return false;
		}

		Map<Integer, Integer> oddsMap2 = this.getOddsMap(TENSINGLT_MAP);
		Map<Integer, DropInit> dropInitMap2 = this.getInitMap(TENSINGLT_MAP);
		List<Integer> result2 = getDropList(dropInitMap2, oddsMap2, 1);
		if (result2.size() != 1) {
			return false;
		}

		Map<Integer, Integer> oddsMap1 = this.getOddsMap(TEN_MAP);
		Map<Integer, DropInit> dropInitMap1 = this.getInitMap(TEN_MAP);
		List<Integer> result1 = getDropList(dropInitMap1, oddsMap1, 9);
		if (result1.size() != 9) {
			return false;
		}
		// 伪随机计数
		addOdds(result2, oddsMap2, dropInitMap2);
		addOdds(result1, oddsMap1, dropInitMap1);
		// 梦想值增加
		addDreamNum(lotterytype);
		// 添加英雄
		List<Integer> resultAll = new LinkedList<Integer>();
		resultAll.addAll(result1);
		resultAll.addAll(result2);
		addHero(resultAll, LogBehavior.LOTTERYTEN);
		// 添加固定物品
		this.addItem(lotterytype, LogBehavior.LOTTERYTEN);
		// 刷新抽奖数据
		this.sSRefreshLotty(now);
		// 返回成功
		sendSLottery(resultAll, lotterytype);

		ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.LOTTERY, 10);

		return true;
	}

	/**
	 * 梦想兑换和更改
	 * 
	 * @param isFree
	 * @return
	 */
	public boolean lotteryDream(boolean isFree, int isfree) {
		int dreamAll = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1287).configvalue);
		if (xlottery.getDreamexp() < dreamAll) {
			return false;
		}
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		if (isFree) {
			if (xlottery.getDream() != 0) {
				return false;
			}
		} else {
			if (isfree == 1) {
				if (xlottery.getDreamfree() != 0) {
					return false;
				} else {
					xlottery.setDreamfree(1);
				}
			} else {
				if (xlottery.getDreamfree() == 0) {
					xlottery.setDreamfree(1);
				} else {
					int cost = Integer
							.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1288).configvalue);
					if (!DropManager.getInstance().useDel(IDManager.YUANBAO, cost, roleId,
							LogBehavior.LOTTERYDREAMCHANGECOST)) {
						return false;
					}
				}
			}
		}

		Map<Integer, Integer> oddsMap1 = this.getOddsMap(GETHERO_MAP);
		Map<Integer, DropInit> dropInitMap1 = this.getInitMap(GETHERO_MAP);
		List<Integer> result1 = getDropList(dropInitMap1, oddsMap1, 1);
		if (result1.size() != 1 || result1.get(0) == -1) {
			return false;
		}
		xlottery.setDream(result1.get(0));
		// 伪随机计数
		addOdds(result1, oddsMap1, dropInitMap1);
		// 添加固定物品
		this.addItem(CLottery.DREAM, LogBehavior.LOTTERYDREAM);
		// 刷新抽奖数据
		this.sSRefreshLotty(now);
		// 返回成功
		if (isFree) {
			sendSLottery(result1, CLottery.DREAM);
		} else {
			sendSChangeDream(xlottery.getDream());
		}

		return true;
	}

	/**
	 * 领取梦想兑换英雄
	 * 
	 * @return
	 */
	public boolean getDreamHero() {
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		if (xlottery.getDream() == 0) {
			return false;
		}

		List<Integer> result = new LinkedList<Integer>();
		result.add(xlottery.getDream());
		this.addHero(result, LogBehavior.LOTTERYDREAM);
		int dreamhero = xlottery.getDream();

		xlottery.setDream(0);
		xlottery.setDreamexp(0);
		xlottery.setDreamfree(0);
		this.sSRefreshLotty(now);

		sendSGetDream(dreamhero);

		ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.LOTTERY, 1);
		return true;
	}

	/**
	 * 梦想值增加
	 * 
	 * @param lotterytype
	 */
	public void addDreamNum(int lotterytype) {
		int dreamAll = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1287).configvalue);
		if (xlottery.getDreamexp() >= dreamAll) {
			return;
		}
		switch (lotterytype) {
		case CLottery.FREE:
			// int dreamAdd = Integer.parseInt(
			// ConfigManager.getInstance().getConf(config10.class).get(1285).configvalue
			// );
			// xlottery.setDreamexp(xlottery.getDreamexp() + dreamAdd);
			break;
		case CLottery.ONE:
			int dreamAdd = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1285).configvalue);
			xlottery.setDreamexp(xlottery.getDreamexp() + dreamAdd);
			break;
		case CLottery.TEN:
			dreamAdd = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1286).configvalue);
			xlottery.setDreamexp(xlottery.getDreamexp() + dreamAdd);
			break;
		}
		if (xlottery.getDreamexp() >= dreamAll) {
			xlottery.setDreamexp(dreamAll);
		}
	}

	/**
	 * 添加物品
	 * 
	 * @param lotterytype
	 */
	public void addItem(int lotterytype, String logStr) {
		int itemId = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1290).configvalue);
		int addNum = 0;
		switch (lotterytype) {
		case CLottery.FREE:
			addNum = 1;
			break;
		case CLottery.ONE:
			addNum = 1;
			break;
		case CLottery.TEN:
			addNum = 10;
			break;
		case CLottery.DREAM:
			addNum = 1;
			break;
		}
		DropManager.getInstance().dropAddByOther(itemId, addNum, 0, 0, roleId, logStr);
	}

	/**
	 * 进行伪随机计数
	 * 
	 * @param result
	 * @param oddsMap
	 * @param dropInitMap
	 */
	public void addOdds(List<Integer> result, Map<Integer, Integer> oddsMap, Map<Integer, DropInit> dropInitMap) {
		for (Map.Entry<Integer, DropInit> entry : dropInitMap.entrySet()) {
			if (entry.getValue().lotteryteam != -1) {
				boolean addOdds = true;
				for (Integer heroId : result) {
					if (heroId == entry.getValue().id) {
						addOdds = false;
						break;
					}
					if (entry.getValue().lotteryteam > 0) {
						DropInit getdrop = dropInitMap.get(heroId);
						if (getdrop != null) {
							if (getdrop.lotteryteam == entry.getValue().lotteryteam) {
								addOdds = false;
								break;
							}
						}
					}

				}
				if (addOdds) {
					Integer odds = oddsMap.get(entry.getValue().id);
					if (odds == null) {
						oddsMap.put(entry.getValue().id, 1);
					} else {
						oddsMap.put(entry.getValue().id, odds + 1);
					}
				} else {
					oddsMap.put(entry.getValue().id, 0);
				}
			}
		}
	}

	/**
	 * 添加英雄到背包
	 * 
	 * @param dropList
	 */
	public void addHero(List<Integer> dropList, String logStr) {
		List<Integer> objectId = new ArrayList<Integer>();
		List<Integer> objectNum = new ArrayList<Integer>();
		List<Integer> par = new ArrayList<Integer>();
		for (Integer heroId : dropList) {
			herorecruit51 init = ConfigManager.getInstance().getConf(herorecruit51.class).get(heroId);
			if (init != null) {
				// DropManager.getInstance().dropAddByOther(init.getId(),
				// 1,init.getHerolevel(), 0, roleId, "lotteryaddhero");
				objectId.add(init.getId());
				objectNum.add(1);
				par.add(init.getHerolevel());
				ActivityManager.getInstance().addMsgNotice(roleId, init.getId(), ActivityManager.ZHAOMU, "");
			}
		}
		DropManager.getInstance().sendMailOrDropAdd(roleId, objectId, objectNum, par, logStr);
	}

	/**
	 * 获取抽奖后ID列表
	 * 
	 * @param dropMap
	 * @param oddsAddMap
	 * @param num
	 * @return
	 */
	public List<Integer> getDropList(Map<Integer, DropInit> dropMap, Map<Integer, Integer> oddsAddMap, int num) {
		for (Map.Entry<Integer, DropInit> entry : dropMap.entrySet()) {
			Integer addNum = oddsAddMap.get(entry.getValue().id);
			if (addNum != null) {
				entry.getValue().percent += entry.getValue().oddsAdd * addNum;
			}
		}
		List<Integer> result = DropManager.getInstance().getDropIdList(dropMap, num);
		return result;
	}

	/**
	 * 根据类型获得基础掉落数据
	 * 
	 * @param mapType
	 * @return
	 */
	public Map<Integer, DropInit> getInitMap(int mapType) {
		Map<Integer, DropInit> resutlMap = new HashMap<Integer, DropInit>();
		TreeMap<Integer, herorecruit51> initMap = ConfigManager.getInstance().getConf(herorecruit51.class);
		for (Map.Entry<Integer, herorecruit51> entry : initMap.entrySet()) {
			switch (mapType) {
			case SINGLE_MAP:
				if (entry.getValue().getInitialweight1() != 0 || entry.getValue().getWeightpuls1() != 0) {
					DropInit drop = new DropInit(entry.getValue().getInitialweight1(), entry.getValue().getId(),
							Integer.MAX_VALUE, entry.getValue().getHerolevel(), entry.getValue().getWeightpuls1(),
							entry.getValue().getPulsrange1());
					resutlMap.put(resutlMap.size(), drop);
				}
				continue;
			case TEN_MAP:
				if (entry.getValue().getInitialweight2() != 0 || entry.getValue().getWeightpuls2() != 0) {
					DropInit drop = new DropInit(entry.getValue().getInitialweight2(), entry.getValue().getId(),
							Integer.MAX_VALUE, entry.getValue().getHerolevel(), entry.getValue().getWeightpuls2(),
							entry.getValue().getPulsrange2());
					resutlMap.put(resutlMap.size(), drop);
				}
				continue;
			case TENSINGLT_MAP:
				if (entry.getValue().getInitialweight3() != 0 || entry.getValue().getWeightpuls3() != 0) {
					DropInit drop = new DropInit(entry.getValue().getInitialweight3(), entry.getValue().getId(),
							Integer.MAX_VALUE, entry.getValue().getHerolevel(), entry.getValue().getWeightpuls3(),
							entry.getValue().getPulsrange3());
					resutlMap.put(resutlMap.size(), drop);
				}
				continue;
			case GETHERO_MAP:
				if (entry.getValue().getInitialweight4() != 0 || entry.getValue().getWeightpuls4() != 0) {
					DropInit drop = new DropInit(entry.getValue().getInitialweight4(), entry.getValue().getId(),
							Integer.MAX_VALUE, entry.getValue().getHerolevel(), entry.getValue().getWeightpuls4(),
							entry.getValue().getPulsrange4());
					resutlMap.put(resutlMap.size(), drop);
				}
				continue;
			}
		}
		return resutlMap;
	}

	/**
	 * 根据type获取添加值
	 * 
	 * @param lotteryType
	 * @return
	 */
	public Map<Integer, Integer> getOddsMap(int mapType) {
		switch (mapType) {
		case SINGLE_MAP:
			return xlottery.getSinglelotty();
		case TEN_MAP:
			return xlottery.getTenlotty();
		case TENSINGLT_MAP:
			return xlottery.getTensinglelotty();
		case GETHERO_MAP:
			return xlottery.getGetherolotty();
		}
		return null;
	}

	/**
	 * 发送抽奖成功消息
	 * 
	 * @param result
	 * @param lotteryType
	 */
	public void sendSLottery(List<Integer> result, int lotteryType) {
		SLottery snd = new SLottery();
		snd.result = SLottery.END_OK;
		snd.lotterytype = lotteryType;
		snd.herolist.addAll(result);
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}

	public void sendSLotteryNew(List<chuhan.gsp.play.lottery.Items> result, int lotteryType) {
		SLottery snd = new SLottery();
		snd.result = SLottery.END_OK;
		snd.lotterytype = lotteryType;
		snd.items.addAll(result);
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}

	/**
	 * 更改梦想兑换内容
	 * 
	 * @param dream
	 */
	public void sendSChangeDream(int dream) {
		SChangeDream snd = new SChangeDream();
		snd.result = SChangeDream.END_OK;
		snd.dream = dream;
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}

	/**
	 * 获取梦想兑换
	 * 
	 * @param dream
	 */
	public void sendSGetDream(int dream) {
		SGetDream snd = new SGetDream();
		snd.result = SChangeDream.END_OK;
		snd.heroid = dream;
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}

	/**
	 * 刷新抽奖信息
	 * 
	 * @param now
	 */
	public void sSRefreshLotty(long now) {
		SRefreshLotty snd = new SRefreshLotty();
		snd.lotty.firstget = xlottery.getFirstget();
		snd.lotty.dreamexp = xlottery.getDreamexp();
		snd.lotty.dream = xlottery.getDream();
		snd.lotty.dreamfree = xlottery.getDreamfree();
		if (now < xlottery.getFreetime()) {
			snd.lotty.freetime = (int) ((xlottery.getFreetime() - now) / 1000);
		} else {
			snd.lotty.freetime = 0;
		}
		snd.lotty.normalrecruitnum = xlottery.getNormalrecruitnum();
		if (now < xlottery.getNormalrecruittime()) {
			snd.lotty.normalrecruittime = (int) ((xlottery.getNormalrecruittime() - now) / 1000);
		} else {
			snd.lotty.normalrecruittime = 0;
		}
		snd.lotty.toprecruitnum = xlottery.getToprecruitnum();
		if (now < xlottery.getToprecruittime()) {
			snd.lotty.toprecruittime = (int) ((xlottery.getToprecruittime() - now) / 1000);
		} else {
			snd.lotty.toprecruittime = 0;
		}
		snd.lotty.toprecruitheronum = xlottery.getToprecruitheronum();
		if (now < xlottery.getToptentime()) {
			snd.lotty.toptentime = (int) ((xlottery.getToptentime() - now) / 1000);
		} else {
			snd.lotty.toptentime = 0;
		}
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
}
