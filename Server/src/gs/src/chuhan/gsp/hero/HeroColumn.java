package chuhan.gsp.hero;

import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;

import chuhan.gsp.DataInit;
import chuhan.gsp.attr.attributetrain32;
import chuhan.gsp.attr.config10;
import chuhan.gsp.award.DropManager;
import chuhan.gsp.award.IDManager;
import chuhan.gsp.item.BagTypes;
import chuhan.gsp.item.BasicItem;
import chuhan.gsp.item.ItemColumn;
import chuhan.gsp.item.Module;
import chuhan.gsp.item.equipmentquality71;
import chuhan.gsp.item.equipmentstrength72;
import chuhan.gsp.item.hero01;
import chuhan.gsp.item.heroaddstage67;
import chuhan.gsp.item.heroculture70;
import chuhan.gsp.item.item26;
import chuhan.gsp.item.ms73;
import chuhan.gsp.item.skillupcost17;
import chuhan.gsp.log.LogBehavior;
import chuhan.gsp.log.Logger;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.Message;
import chuhan.gsp.play.activity.ActivityManager;
import chuhan.gsp.play.huoyuedu.HuoyueColumns;
import chuhan.gsp.play.tanxian.TanXianColumns;
import chuhan.gsp.util.ParserString;


public class HeroColumn {
	
	public final static int MAX_TROOP_NUM = 5;
	
	public static Logger logger = Logger.getLogger(HeroColumn.class);
	public static final int PERCENT_ALL = 1000;
	public static final int QIANGHUA_MAXLEVEL = 5;
	public static final int QIANGHUA_MAXLEVEL_HIGH = 8;
	public static final int STARUP_RESULT_NUM = 3;	//装备升星返回装备数量
	
	public static final int PLACE1 = 1;	//位置
	public static final int PLACE2 = 2;
	public static final int PLACE3 = 3;
	public static final int PLACE4 = 4;
	
	public static final int PEIYANGNUM = 4;	//培养总数（初始化时用）
	public static final int MSNUM = 6;	//秘术总数（初始化时用）
	public static final String SPLITSTR = ":";	//数据保存用分隔符
	public static final String SPLITSTR2 = "|";	//数据保存用分隔符
	
	public static final int ITEM_LOCATION_NUM = 6;
	
	
	final public long roleId;
	final xbean.HeroColumn xcolumn;
	final boolean readonly;
	
	public static HeroColumn getHeroColumn(long roleId, boolean readonly)
	{
		if(xtable.Properties.select(roleId) == null)
			throw new IllegalArgumentException("构造HeroColumn时，角色 "+roleId+" 不存在。");
		
		xbean.HeroColumn herocol = null;
		if(readonly)
			herocol = xtable.Herocolumns.select(roleId);
		else
			herocol = xtable.Herocolumns.get(roleId);
		if(herocol == null)
		{
			if(readonly)
				herocol = xbean.Pod.newHeroColumnData();
			else
			{
				herocol = xbean.Pod.newHeroColumn();
				xtable.Herocolumns.insert(roleId, herocol);
			}
		}
		return new HeroColumn(roleId, herocol, readonly);
	}
	
	
	private HeroColumn(long roleId, xbean.HeroColumn xcolumn, boolean readonly) {
		this.roleId = roleId;
		this.xcolumn = xcolumn;
		this.readonly = readonly;
	}
	
	public List<chuhan.gsp.Hero> getProtocolHeros()
	{
		List<chuhan.gsp.Hero> datas = new LinkedList<chuhan.gsp.Hero>();
		for(xbean.Hero xhero : xcolumn.getHeroes())
		{
			Hero hero = new Hero(xhero,false);
//			hero.getProtocolHero();
			datas.add(hero.getProtocolHero());
		}
		return datas;
	}
	
	public xbean.HeroColumn getxcolumn()
	{
		return xcolumn;
	}
		
	/**
	 * 增加一个hero
	 * @param xhero
	 * @return
	 */
	public Hero addHero(xbean.Hero xhero){
		xhero.setKey(getNextKey());
		xcolumn.getHeroes().add(xhero);
		Hero.logger.infoWhileCommit("Role："+roleId+"添加武将："+xhero.getHeroid());
//		BeautyRole.activeBeauty(roleId, xhero.getHeroid());			//美人阁逻辑by yanglk
		//设置图鉴数据
		List<xbean.Hero> heros = new ArrayList<xbean.Hero>();
		heros.add(xhero);
		new PAddTuJianHero(roleId, heros, STuJianHeros.IS_NEW).call();
		
		return getHByHKey(xhero.getKey());
	}
	
	/**
	 * 通过英雄KEY获得hero
	 * @param herokey
	 * @return
	 */
	public Hero getHByHKey(int herokey){
		for(xbean.Hero xhero : xcolumn.getHeroes()){
			if(xhero.getKey() == herokey)
				return Hero.getHero(xhero, readonly);
		}
		return null;
	}
	/**
	 * 通过英雄ID取到key（新手引导特殊处理用）
	 * @param heroId
	 * @return
	 */
	public int getKeyByHeroId(int heroId){
		for(xbean.Hero xhero : xcolumn.getHeroes()){
			if(xhero.getHeroid() == heroId)
				return xhero.getKey();
		}
		return -1;
	}
	
	//获得下一个key
	public int getNextKey()
	{
		xcolumn.setNextkey(xcolumn.getNextkey()+1);
		return xcolumn.getNextkey();
	}
	
	/**
	 * 符文是否被英雄装备
	 * @param equipkey
	 * @return
	 */
	public Hero isWearByHero(int equipkey){
//		for(xbean.Hero xhero : xcolumn.getHeroes()){
//			for(Map.Entry<Integer, Integer> entry : xhero.getItems().entrySet()){
//				if(entry.getValue().intValue() == equipkey){
//					return Hero.getHero(xhero, readonly);
//				}
//			}
//		}
		return null;
	}
	
	/**
	 * 英雄装备符文
	 * @param itemKey
	 * @param location
	 * @return
	 */
	public boolean itemToHero(int heroKey,int location,BasicItem bitem){
		Hero hero = getHByHKey(heroKey);
		if(hero == null)
			return false;
		if(location <= 0 || location > ITEM_LOCATION_NUM){
			return false;
		}
		if(location == ITEM_LOCATION_NUM ){
			if(bitem.getAttr().getRune_type() != 5 && bitem.getAttr().getRune_type() != 6)
				return false;	
		}else if(bitem.getAttr().getRune_type() == 5 || bitem.getAttr().getRune_type() == 6){
			return false;
		}
		
//		hero.getxHeroInfo().getItems().put(location, bitem.getKey());
		hero.refreshHero(roleId);
		return true;
	}
	
	/**
	 * 英雄摘除符文
	 * @param hero
	 * @param itemKey
	 * @return
	 */
	public boolean itemOutHero(Hero hero,int itemKey){
		if(hero == null)
			return false;
/*		for(Map.Entry<Integer, Integer> entry : hero.getxHeroInfo().getItems().entrySet()){
			if(entry.getValue().intValue() == itemKey){
				entry.setValue(0);
				break;
			}
		}*/
		return true;
	}
	
	/**
	 * 判断符文是否激活组合属性
	 * @param hero
	 * @return
	 */
	public boolean isFullItemAttr(Hero hero){
		/*if(hero == null)
			return false;
		ItemColumn itemcol = Module.getItemColumn(roleId, BagTypes.EQUIP, false);
		if(itemcol == null)
			return false;
		List<Integer> needList = new ArrayList<Integer>();
		List<Integer> isUse = new ArrayList<Integer>();
		needList.add(hero.getiHeroInfo().getRunePair1());
		needList.add(hero.getiHeroInfo().getRunePair2());
		needList.add(hero.getiHeroInfo().getRunePair3());
		needList.add(hero.getiHeroInfo().getRunePair4());
		for(Integer needId : needList){
			boolean isHave = false;
			for(Map.Entry<Integer, Integer> entry : hero.getxHeroInfo().getItems().entrySet()){
				BasicItem item = itemcol.getItem(entry.getValue());
				if(item == null)
					continue;
				if(item.getAttr().getRune_type() == needId){
					if( !isUse.contains(Integer.valueOf(item.getKey())) ){
						isUse.add(item.getKey());
						isHave = true;
						break;
					}
				}
			}
			if( !isHave )
				return false;
		}*/
		
		return true;
	}
	
	/**
	 * 删除一个英雄
	 * @param heroKey
	 * @return
	 */
	public boolean removeByKey(int heroKey){
		java.util.LinkedList<Integer> removeList = new java.util.LinkedList<Integer>();
		removeList.add(heroKey);
		return removeByKeyList(removeList);
	}
	
	/**
	 * 删除多个英雄
	 * @param itemkeys
	 * @return
	 */
	public boolean removeByKeyList(java.util.LinkedList<Integer> itemkeys){
		TroopColumn troopcol = TroopColumn.getTroopColumn(roleId, false);
		troopcol.HeroOutAllTroop(itemkeys);
		SRemoveHero send = new SRemoveHero();
		for(int key : itemkeys){
			for(xbean.Hero xhero : xcolumn.getHeroes()){
				if(key == xhero.getKey()){
					send.herokey.add(xhero.getKey());
					xcolumn.getHeroes().remove(xhero);	
					break;
				}
			}
		}
		if(send.herokey.size() > 0){
			xdb.Procedure.psendWhileCommit(roleId, send);
			return true;
		}else{
			return false;
		}
	}
	
	/**
	 * 刷新单个英雄
	 * @param herokey
	 */
	public void refreshHero(int herokey){		
		for(xbean.Hero xhero : xcolumn.getHeroes()){
			if(xhero.getKey() == herokey){
				Hero hero = new Hero(xhero,false);
				hero.refreshHero(roleId);
				return;
			}
		}
	}
	
	/**
	 * 是否已有同种英雄
	 * @param addHeroInit
	 * @return
	 */
	public boolean isHaveSameHero(hero01 addHeroInit){
		for(xbean.Hero xhero : xcolumn.getHeroes()){
			hero01 hInit = ConfigManager.getInstance().getConf(hero01.class).get(xhero.getHeroid());
			if(hInit == null){
				continue;
			}
			if(hInit.getNameID().equals(addHeroInit.getNameID())){
				return true;
			}
		}
		return false;
	}
	
	/**
	 * 根据培养位置获取培养的基础数据
	 * @param hero
	 * @param slotNum
	 * @return
	 */
	public heroculture70 getPeiyangInit(Hero hero,int slotNum,List<Integer> pyList){
		TreeMap<Integer, heroculture70> map = ConfigManager.getInstance().getConf(heroculture70.class);
		for(Map.Entry<Integer, heroculture70> entry : map.entrySet()){
			if(entry.getValue().getBorn() == hero.getiHeroInfo().getBorn() &&
					entry.getValue().getQosition() == hero.getiHeroInfo().getQosition() &&
					entry.getValue().getElement() == slotNum + 1 &&
					entry.getValue().getElementLeve() == pyList.get(slotNum) + 1){
				return entry.getValue();
			}
		}
		return null;
		/*
		switch(slotNum){
		case PLACE1:
			result[0] = hero.getiHeroInfo().getTrainSlot1();
			result[1] = hero.getiHeroInfo().getTrainMaximum1();
			return result;
		case PLACE2:
			result[0] = hero.getiHeroInfo().getTrainSlot2();
			result[1] = hero.getiHeroInfo().getTrainMaximum2();
			return result;
		case PLACE3:
			result[0] = hero.getiHeroInfo().getTrainSlot3();
			result[1] = hero.getiHeroInfo().getTrainMaximum3();
			return result;
		case PLACE4:
			result[0] = hero.getiHeroInfo().getTrainSlot4();
			result[1] = hero.getiHeroInfo().getTrainMaximum4();
			return result;
		}
		return null;
		*/
		
	}
	
/*	*//**
	 * 获取现在的培养ID
	 * @param hero
	 * @param slotNum
	 * @return
	 *//*
	public int getNowPeiyangId(Hero hero,int slotNum){
		switch(slotNum){
		case PLACE1:
			return hero.getxHeroInfo().getPeiyang1();
		case PLACE2:
			return hero.getxHeroInfo().getPeiyang2();
		case PLACE3:
			return hero.getxHeroInfo().getPeiyang3();
		case PLACE4:
			return hero.getxHeroInfo().getPeiyang4();
		}
		return -1;
	}*/
	
/*	*//**
	 * 根据培养ID获取配表培养内容
	 * @param peiyangId
	 * @return
	 *//*
	public attributetrain32 getPeiyangInit(int peiyangId){
		attributetrain32 attr32 = ConfigManager.getInstance().getConf(attributetrain32.class).get(peiyangId);
		return attr32;
	}*/
	
	/**
	 * 获取下一等级培养数据
	 * @param attrNow
	 * @param bagId
	 * @return
	 *//*
	public attributetrain32 getNextPeiyang(attributetrain32 attrNow,int bagId){
		int nextTime = 1;
		if(attrNow != null){
			nextTime = attrNow.getTimes() + 1;
		}
		TreeMap<Integer, attributetrain32> map = ConfigManager.getInstance().getConf(attributetrain32.class);
		for(Map.Entry<Integer, attributetrain32> entry : map.entrySet()){
			if(entry.getValue().getBagId() == bagId){
				if(entry.getValue().getTimes() == nextTime){
					return entry.getValue();
				}
			}
		}
		return null;
	}*/
	
//	/**
//	 * 保存培养信息
//	 * @param hero
//	 * @param slotNum
//	 * @param peiyangId
//	 */
//	public void setPeiyang(Hero hero,int slotNum,int peiyangId){
//		switch(slotNum){
//		case PLACE1:
//			hero.getxHeroInfo().setPeiyang1(peiyangId);
//			return;
//		case PLACE2:
//			hero.getxHeroInfo().setPeiyang2(peiyangId);
//			return;
//		case PLACE3:
//			hero.getxHeroInfo().setPeiyang3(peiyangId);
//			return;
//		case PLACE4:
//			hero.getxHeroInfo().setPeiyang4(peiyangId);
//			return;
//		}
//	}

	/**
	 * 培养英雄入口
	 * @param herokey
	 * @param slotnum
	 * @return
	 */
	public boolean peiyangEntry(int herokey, int slotnum,byte isreset) {
		slotnum = slotnum - 1;	//配表从1开始，但是list是从0开始
		
		Hero hup = this.getHByHKey(herokey);
		if (hup == null ){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		if(isreset == 1){
			hup.getxHeroInfo().setHeropeiyang("0:0:0:0");
			this.refreshHero(hup.getxHeroInfo().getKey());
			return true;
		}
//		chuhan.gsp.attr.PropRole proprole = chuhan.gsp.attr.PropRole.getPropRole(roleId, false);
		
		List<Integer> pyList = ParserString.parseString2Int(hup.getxHeroInfo().getHeropeiyang(),SPLITSTR);
		if(pyList == null || pyList.size() == 0){
			pyList = new LinkedList<Integer>();
			for(int i = 0;i<PEIYANGNUM;i++){
				pyList.add(0);
			}
		}
		
		if(pyList.size() <= slotnum || slotnum < 0){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		
		heroculture70 pyInit = getPeiyangInit(hup,slotnum,pyList);
		if(pyInit == null){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		/*int nowPeiyangId = getNowPeiyangId(hup,slotnum);
		attributetrain32 nowPeiyang = null;
		if( nowPeiyangId != 0){
			nowPeiyang = getPeiyangInit(nowPeiyangId);
			if(nowPeiyang == null){
				Message.psendMsgNotify(roleId, 135);
				return false;
			}
		}
		
		attributetrain32 nextPeiyang = getNextPeiyang(nowPeiyang,peiyang[0]);
		if(nextPeiyang == null || nextPeiyang.getTimes() > peiyang[1] ){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		
		int cost = nextPeiyang.getCost();
		if((cost*-1) == proprole.delZiYuan(cost*-1, 0,IDManager.SHENGLINGZQ)){
			setPeiyang(hup,slotnum,nextPeiyang.getId());
			this.refreshHero(hup.getxHeroInfo().getKey());
			
			ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.HERO_PEIYANG, 1);

			return true;
		}*/
		if(DropManager.getInstance().useDel(pyInit.getConsumption(), pyInit.getNumber(), roleId, 
				LogBehavior.HEROPEIYANGCOST)){
			pyList.set(slotnum, pyList.get(slotnum) + 1);
			hup.getxHeroInfo().setHeropeiyang(ParserString.getStrFromList(pyList, SPLITSTR));
			this.refreshHero(hup.getxHeroInfo().getKey());
			return true;
		}
		
		return false;
	}
	
	/**
	 * 获取英雄基础技能数据
	 * @param hero
	 * @param skillNum
	 * @return
	 */
	public int getSkillInitId(Hero hero,int skillNum){
		switch(skillNum){
		case PLACE1:
			return hero.getiHeroInfo().getSkill1ID();
		case PLACE2:
			return hero.getiHeroInfo().getSkill2ID();
		case PLACE3:
			return hero.getiHeroInfo().getSkill3ID();
		}
		return -1;
	}
	
	/**
	 * 获取英雄现在技能数据
	 * @param hero
	 * @param skillNum
	 * @return
	 */
	public int getSkillNowId(Hero hero,int skillNum){
		List<Integer> skillList = ParserString.parseString2Int(hero.getxHeroInfo().getHeroskill(),SPLITSTR);
		if(skillList == null || skillList.size() < skillNum){
			return -1;
		}
		return skillList.get(skillNum - 1);
/*		switch(skillNum){
		case PLACE1:
			return hero.getxHeroInfo().getSkill1();
		case PLACE2:
			return hero.getxHeroInfo().getSkill2();
		case PLACE3:
			return hero.getxHeroInfo().getSkill3();
		}*/
//		return -1;
	}
	
	/**
	 * 获取技能升级数据
	 * @param skillId
	 * @return
	 */
	public skillupcost17 getSkillUpCostInit(int skillId){
		skillupcost17 result = ConfigManager.getInstance().getConf(skillupcost17.class).get(skillId);
		return result;
	}
		
	/**
	 * 保存技能信息
	 * @param hero
	 * @param skillNum
	 * @param skillId
	 */
	public void setSkillId(Hero hero,int skillNum,int skillId){
		List<Integer> skillList = ParserString.parseString2Int(hero.getxHeroInfo().getHeroskill(),SPLITSTR);
		if(skillList == null || skillList.size() < skillNum){
			return;
		}
		skillList.set(skillNum - 1, skillId);
		hero.getxHeroInfo().setHeroskill(ParserString.getStrFromList(skillList, SPLITSTR));
/*		switch(skillNum){
		case PLACE1:
			hero.getxHeroInfo().setSkill1(skillId);
			return;
		case PLACE2:
			hero.getxHeroInfo().setSkill2(skillId);
			return;
		case PLACE3:
			hero.getxHeroInfo().setSkill3(skillId);
			return;
		}*/
	}
	
	/**
	 * 技能升级入口
	 * @param herokey
	 * @param skillnum
	 * @return
	 */
	public boolean skillUpEntry(int herokey,int skillnum){
		Hero hup = this.getHByHKey(herokey);
		if (hup == null ){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		chuhan.gsp.attr.PropRole proprole = chuhan.gsp.attr.PropRole.getPropRole(roleId, false);
		
		int skillId = getSkillNowId(hup,skillnum);
		if(skillId == 0){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		skillupcost17 skillNow = getSkillUpCostInit(skillId);
		if(skillNow == null){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		if(skillNow.getUpgradeSkillID() == -1){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		if(skillNow.getUpgradeStarCondition() > hup.getiHeroInfo().getQuality()){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		
		if( !DropManager.getInstance().useDel(skillNow.getUpgradeCostId2(), skillNow.getUpgradeCostNum2(), 
				roleId, LogBehavior.SKILLUPCOST) ||
				!DropManager.getInstance().useDel(skillNow.getUpgradeCostId3(), skillNow.getUpgradeCostNum3(), 
						roleId, LogBehavior.SKILLUPCOST)){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		setSkillId(hup,skillnum,skillNow.getUpgradeSkillID());
		
		
		this.refreshHero(hup.getxHeroInfo().getKey());
		
		ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.HERO_SKILL_UP, 1);
		return true;
	}
	
	/**
	 * 找到下一个品级的数据
	 * @param hup
	 * @return
	 */
	public hero01 getInitNextStar(Hero hup){
		TreeMap<Integer, hero01> map = ConfigManager.getInstance().getConf(hero01.class);
		for(Map.Entry<Integer, hero01> entry : map.entrySet()){
			if(hup.getiHeroInfo().getQuality() + 1 == entry.getValue().getQuality() &&
					hup.getiHeroInfo().getNameID().equals(entry.getValue().getNameID()))
				return entry.getValue();
		}
		return null;
	}
	
	/**
	 * 英雄升品级入口
	 * @param upkey
	 * @return
	 */
	public boolean starupEntry(int upkey){
		Hero hup = this.getHByHKey(upkey);
		if (hup == null){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		hero01 nextHeroInit = getInitNextStar(hup);
		if(nextHeroInit == null){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		if(!DropManager.getInstance().useDel(IDManager.GOLD, 
				hup.getiHeroInfo().getGold(), roleId, LogBehavior.HEROSTARUPCOST) ||
				!DropManager.getInstance().useDel(hup.getiHeroInfo().getStuff(), 
						hup.getiHeroInfo().getNumbers(), roleId, LogBehavior.HEROSTARUPCOST)){
			Message.psendMsgNotify(roleId, 135);
			return false;
			
		}
		
		/*
		if( hup.getLevel() != hup.getiHeroInfo().getMaxLevel() ){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		if(hup.getiHeroInfo().getStageUpCostType1() == -1 && hup.getiHeroInfo().getStageUpCostType2() == -1){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		if( hup.getiHeroInfo().getStageUpCostType1() != -1 &&
				!DropManager.getInstance().useDel(hup.getiHeroInfo().getStageUpCostType1(), 
				hup.getiHeroInfo().getStageUpCost1(), roleId, LogBehavior.HEROSTARUPCOST)){
			return false;
		}
		if( hup.getiHeroInfo().getStageUpCostType2() != -1 &&
				!DropManager.getInstance().useDel(hup.getiHeroInfo().getStageUpCostType2(), 
				hup.getiHeroInfo().getStageUpCost2(), roleId, LogBehavior.HEROSTARUPCOST)){
			return false;
		}
		*/
		if( !hup.changeihero(nextHeroInit.getId()) ){
			return false;
		}
		/*
		String levelstr = "";
		if(hup.getiHeroInfo().getQuality() == 3){
			levelstr = ConfigManager.getInstance().getConf(config10.class).get(1247).getConfigvalue();
		}else if(hup.getiHeroInfo().getQuality() == 4){
			levelstr = ConfigManager.getInstance().getConf(config10.class).get(1248).getConfigvalue();
		}else if(hup.getiHeroInfo().getQuality() == 5){
			levelstr = ConfigManager.getInstance().getConf(config10.class).get(1249).getConfigvalue();
		}
		int level = 1;
		try{
			level = Integer.parseInt(levelstr);
		}catch(Exception e){
			e.printStackTrace();
		}
		hup.getxHeroInfo().setHerolevel(level);
		*/
		hup.refreshHero(roleId);
		
		//跑马灯
		ActivityManager.getInstance().addMsgNotice(roleId,hup.getxHeroInfo().getHeroid(),ActivityManager.JINJIE,"");
		
		return true;
	}
	
	/**
	 * 英雄合成
	 * @param heroid
	 * @return
	 */
	public boolean composeEntry(int heroid){
		hero01 ihero = ConfigManager.getInstance().getConf(hero01.class).get(heroid);
		if(ihero == null){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		if(this.isHaveSameHero(ihero)){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}

		if(!DropManager.getInstance().useDel(ihero.getSyntheticItemid(), 
				ihero.getSyntheticCount(), roleId, LogBehavior.HEROCOMPOSECOST)){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		DropManager.getInstance().dropAddByOther(heroid, 1, 0, 0, roleId, LogBehavior.HEROCOMPOSE);

		return true;
	}
	
	/**
	 * 英雄进阶
	 * @param upkey
	 * @return
	 */
	public boolean jinjinEntry(int upkey){
		Hero hup = this.getHByHKey(upkey);
		if (hup == null){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		
		heroaddstage67 iheroJinjie = getHeroNextJinjie(hup);
		if(iheroJinjie == null){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		if( hup.getLevel() < iheroJinjie.getLevel() ){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		List<Integer> costId = ParserString.parseString2Int(iheroJinjie.getStuff());
		List<Integer> costNum = ParserString.parseString2Int(iheroJinjie.getNumbers());
		if(costId == null || costNum == null || costId.size() != costNum.size()){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		for(int i = 0;i<costId.size(); i++){
			if( !DropManager.getInstance().useDel(costId.get(i), costNum.get(i), roleId, 
					LogBehavior.HEROJINJIECOST) ){
				Message.psendMsgNotify(roleId, 135);
				return false;
				}
		}
		hup.getxHeroInfo().setHerojinjiestar(iheroJinjie.getQuality());
		hup.getxHeroInfo().setHerojinjiesmall(iheroJinjie.getHalosPn());
		hup.refreshHero(roleId);
		
		//跑马灯
//		ActivityManager.getInstance().addMsgNotice(roleId,hup.getxHeroInfo().getHeroid(),ActivityManager.JINJIE,"");
		
		return true;
	}
	
	/**
	 * 获取英雄的下一个进阶数据
	 * @param hup
	 * @return
	 */
	private heroaddstage67 getHeroNextJinjie(Hero hup){
		heroaddstage67 result = null;
		TreeMap<Integer, heroaddstage67> map = ConfigManager.getInstance().getConf(heroaddstage67.class);
		//先根据阶级找
		for(Map.Entry<Integer, heroaddstage67> entry : map.entrySet()){
			if(entry.getValue().getBorn() == hup.getiHeroInfo().getBorn() &&
					entry.getValue().getQosition() == hup.getiHeroInfo().getQosition() &&
					entry.getValue().getQuality() == hup.getxHeroInfo().getHerojinjiestar() &&
					entry.getValue().getHalosPn() == hup.getxHeroInfo().getHerojinjiesmall() + 1){
				result = entry.getValue();
				break;
			}
		}
		//如果不是进阶，而是升星级
		if(result == null){
			for(Map.Entry<Integer, heroaddstage67> entry : map.entrySet()){
				if(entry.getValue().getBorn() == hup.getiHeroInfo().getBorn() &&
						entry.getValue().getQosition() == hup.getiHeroInfo().getQosition() &&
						entry.getValue().getQuality() == hup.getxHeroInfo().getHerojinjiestar() + 1 &&
						entry.getValue().getHalosPn() == 0){
					result = entry.getValue();
					break;
				}
			}
		}
		return result;
	}
	
	
	/**
	 * 秘术升级和添加经验入口
	 * @param herokey
	 * @param mslocation
	 * @param itemid
	 * @param itemnum
	 * @param islevelup
	 * @return
	 */
	public boolean msEntry(int herokey,int mslocation, List<Integer> itemidlist,List<Integer> itemnumlist){
		if(itemidlist == null || itemnumlist == null || itemidlist.size() != itemnumlist.size()){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		Hero hero = this.getHByHKey(herokey);
		if (hero == null){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		//秘术list
		List<String> xMsList = ParserString.parseString(hero.getxHeroInfo().getHeromishu(),SPLITSTR);
		if(xMsList == null || xMsList.size() == 0){
			xMsList = new LinkedList<String>();
			for(int i = 0;i<MSNUM;i++){
				xMsList.add("0|0");
			}
		}
		List<Integer> iMsList = ParserString.parseString2Int(hero.getiHeroInfo().getMsid());
		if(iMsList == null || iMsList.size() < mslocation || xMsList.size() < mslocation || mslocation < 1){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		//单个秘术数据
		List<Integer> xMsNum = ParserString.parseString2Int(xMsList.get(mslocation - 1),SPLITSTR2);
		if(xMsNum == null || xMsNum.size() != 2){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		//配表数据
		ms73 iMsInit = ConfigManager.getInstance().getConf(ms73.class).get(iMsList.get(mslocation - 1));
		if(iMsInit == null){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		//配表经验
		List<Integer> iMsExp = ParserString.parseString2Int(iMsInit.getConsumexpevalue());
		//消耗资源ID
		List<Integer> iMsCostId = ParserString.parseString2Int(iMsInit.getConsumzyid());
		//消耗资源总数量
		List<Integer> iMsCostNum = ParserString.parseString2Int(iMsInit.getConsumnb());
		if(iMsExp == null || iMsCostId == null || iMsCostNum == null ||
				iMsExp.size() != iMsCostId.size() || iMsCostId.size() != iMsCostNum.size()){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		if(iMsExp.size() < xMsNum.get(0) + 1){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		int addExp = 0;
		for(int i = 0;i<itemidlist.size();i++){
			int itemId = itemidlist.get(i);
			int itemNum = itemnumlist.get(i);
			item26 itemInit = ConfigManager.getInstance().getConf(item26.class).get(itemId);
			if(itemInit == null){
				Message.psendMsgNotify(roleId, 135);
				return false;
			}
			
			for(int j = 0;j<itemNum;j++){
				if(iMsExp.size() < xMsNum.get(0) + 1){
					break;
				}
				addExp = addExp + itemInit.getImprovexperience();
				//本等级的总经验,消耗ID,消耗数量
				int needAllExp = iMsExp.get(xMsNum.get(0));
				int needCostId = iMsCostId.get(xMsNum.get(0));
				int needAllCost = iMsCostNum.get(xMsNum.get(0));
				int cost = needAllCost/needAllExp;
				
				if(addExp + xMsNum.get(1) >= needAllExp){
					int delNum = needAllExp - xMsNum.get(1);
					cost = delNum * cost;
					addExp = addExp - delNum;
					xMsNum.set(0, xMsNum.get(0) + 1);
					xMsNum.set(1, 0);
				}else{
					cost = addExp * cost;
					xMsNum.set(1, addExp + xMsNum.get(1));
					addExp = 0;
				}
				if(!DropManager.getInstance().useDel(itemId, 1, roleId, LogBehavior.HEROMSCOST) ||
						!DropManager.getInstance().useDel(needCostId, cost, roleId, LogBehavior.HEROMSCOST) ){
					Message.psendMsgNotify(roleId, 135);
					return false;
				}
			}
		}
		
		xMsList.set(mslocation - 1, xMsNum.get(0)+SPLITSTR2+xMsNum.get(1));	
		hero.getxHeroInfo().setHeromishu(ParserString.getStrFromList(xMsList, SPLITSTR));
		hero.refreshHero(roleId);
		return true;
	}
	
	
	/**
	 * 装备升级强化入口
	 * @param herokey
	 * @param equiplocation
	 * @param islevelup
	 * @return
	 */
	public boolean equipUpEntry(int herokey,int equiplocation,int islevelup,int isstrength){
		Hero hero = this.getHByHKey(herokey);
		if (hero == null){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		//装备list
		List<String> xEquipList = ParserString.parseString(hero.getxHeroInfo().getHeroequip(),SPLITSTR);
		if(xEquipList == null || xEquipList.size() == 0){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		if(xEquipList.size() < equiplocation || equiplocation < 1){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		//单个装备数据
		List<Integer> xEquipData = ParserString.parseString2Int(xEquipList.get(equiplocation - 1),SPLITSTR2);
		if(xEquipData == null || xEquipData.size() != 2){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		//是否是升级
		if(islevelup == 1){
			equipmentquality71 init = ConfigManager.getInstance().getConf(equipmentquality71.class).
					get(xEquipData.get(0));
			if(init == null || init.NextId == -1){
				Message.psendMsgNotify(roleId, 135);
				return false;
			}
			if( !DropManager.getInstance().useDel(init.getPropId(), init.getNumbers(), roleId, 
					LogBehavior.HEROEQUIPUPCOST)){
				Message.psendMsgNotify(roleId, 135);
				return false;
			}
			xEquipData.set(0, init.getNextId());
		}else{
			boolean next = true;
			boolean isUp = false;
			while(next){
				if(isstrength != 1){
					next = false;
				}
				TreeMap<Integer,equipmentstrength72> map = ConfigManager.getInstance().getConf(equipmentstrength72.class);
				equipmentstrength72 init = null;
				for(Map.Entry<Integer, equipmentstrength72> entry : map.entrySet()){
					if(hero.getiHeroInfo().getQosition() == entry.getValue().getQosition() &&
							equiplocation == entry.getValue().getParts() &&
							xEquipData.get(1) + 1 == entry.getValue().getSthequipmentlevel() ){
						init = entry.getValue();
						break;
					}
				}
				if(init == null){
					next = false;
					break;
				}
				if(!DropManager.getInstance().useDel(init.getPropid(), init.getNumbers(), roleId, 
						LogBehavior.HEROEQUIPUPCOST) || 
						!DropManager.getInstance().useDel(init.getPropid2(), init.getNumbers2(), roleId, 
								LogBehavior.HEROEQUIPUPCOST)){
					next = false;
					break;
				}
				xEquipData.set(1, init.getSthequipmentlevel());
				isUp = true;
			}
			if(!isUp){
				Message.psendMsgNotify(roleId, 135);
				return false;
			}
			
		}
		xEquipList.set(equiplocation - 1, xEquipData.get(0)+SPLITSTR2+xEquipData.get(1));	
		
		hero.getxHeroInfo().setHeroequip(ParserString.getStrFromList(xEquipList, SPLITSTR));
		hero.refreshHero(roleId);
		return true;
	}
	
	/**
	 * 更换皮肤入口
	 * @param heroid
	 * @param skinid
	 * @return
	 */
	public boolean changeskinEntry(int heroid,int skinid){
		Hero hero = this.getHByHKey(heroid);
		if (hero == null){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		HeroSkinColumn heroskincol = HeroSkinColumn.getHeroSkinColumn(roleId, false);
		xbean.HeroSkin xheroskin = heroskincol.getSkin(skinid);
		if(xheroskin == null){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		List<Integer> heroNeedSkinList = ParserString.parseString2Int(hero.getiHeroInfo().getUseableArtresource());
		if(heroNeedSkinList == null || !heroNeedSkinList.contains(Integer.valueOf(skinid))){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		
		hero.getxHeroInfo().setHeroviewid(skinid);
		hero.refreshHero(roleId);
		return true;
	}
		
	/**
	 * 英雄熔炼
	 * @param heroIdList
	 * @return
	 */
	public boolean splitEntry(java.util.LinkedList<Integer> herokeyList){
		chuhan.gsp.attr.PropRole proprole = chuhan.gsp.attr.PropRole.getPropRole(roleId, false);
		//判断是否探险
		TanXianColumns col = TanXianColumns.getTanXianColumn(roleId, false);
		if(col.isHeroTanXian(herokeyList)){
			return false;
		}
		
		TroopColumn troopcol = TroopColumn.getTroopColumn(roleId, false);
		int addNum = 0;
		for(int herokey : herokeyList){
			Hero hero = this.getHByHKey(herokey);
			if (hero == null){
				Message.psendMsgNotify(roleId, 135);
				return false;
			}
			troopcol.HeroOutAllTroop(herokey);
			addNum += hero.getSplitNum();
		}
		if(proprole.addZiYuan(addNum, 0, IDManager.EXPJIEJING) != addNum){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		if( this.removeByKeyList(herokeyList) ){
			
			ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.HERO_RONGLING, herokeyList.size());

			return true;
		}
		
		return false;
	}
	
	
	
}
