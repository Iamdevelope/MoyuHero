package chuhan.gsp.hero;


import java.util.LinkedList;
import java.util.List;

import xbean.Troop;
import chuhan.gsp.log.Logger;
import chuhan.gsp.msg.Message;
import chuhan.gsp.util.Conv;


public class TroopColumn {
	
	public final static int MAX_TROOP_NUM = 3;
	public final static int TROOP_LOCATION1 = 1;
	public final static int TROOP_LOCATION2 = 2;
	public final static int TROOP_LOCATION3 = 3;
	public final static int TROOP_LOCATION4 = 4;	
	public final static int TROOP_LOCATION5 = 5;
	public final static int TROOP_SH1 = 6;
	public final static int TROOP_SH2 = 7;
	public final static int TROOP_SH3 = 8;
	public final static int TROOP_SH4 = 9;
	
	public static Logger logger = Logger.getLogger(TroopColumn.class);
	
	final public long roleId;
	final xbean.Troops xcolumn;
	final boolean readonly;
	
	
	public static TroopColumn getTroopColumn(long roleId, boolean readonly)
	{
		if(xtable.Properties.select(roleId) == null)
			throw new IllegalArgumentException("构造TroopColumn时，角色 "+roleId+" 不存在。");
		
		xbean.Troops troopcol = null;
		if(readonly)
			troopcol = xtable.Herotroops.select(roleId);
		else
			troopcol = xtable.Herotroops.get(roleId);
		if(troopcol == null)
		{
			if(readonly)
				troopcol = xbean.Pod.newTroopsData();
			else
			{
				troopcol = xbean.Pod.newTroops();
				xtable.Herotroops.insert(roleId, troopcol);
			}
		}
		return new TroopColumn(roleId, troopcol, readonly);
	}
	
	
	
	private TroopColumn(long roleId, xbean.Troops xcolumn, boolean readonly) {
		this.roleId = roleId;
		this.xcolumn = xcolumn;
		this.readonly = readonly;
	}
	
	//是否是正确的阵形数值
	public boolean isRightTroopNum(int troopNum)
	{
		if(troopNum >= 0 && troopNum < MAX_TROOP_NUM)
			return true;
		return false;
	}
	
	//改变阵形（最终处理）
	private void changeTroop(xbean.Troop troop,int heroLocationNum,int herokey)
	{
		if(troop == null)
			return;
		else
		{
			switch((Integer)heroLocationNum)
			{
			case TROOP_LOCATION1:
				troop.setLocation1(herokey);
				return;
			case TROOP_LOCATION2:
				troop.setLocation2(herokey);
				return;
			case TROOP_LOCATION3:
				troop.setLocation3(herokey);
				return;
			case TROOP_LOCATION4:
				troop.setLocation4(herokey);
				return;
			case TROOP_LOCATION5:
				troop.setLocation5(herokey);
				return;
			case TROOP_SH1:
				troop.setSh1(herokey);
				return;
			case TROOP_SH2:
				troop.setSh2(herokey);
				return;
			case TROOP_SH3:
				troop.setSh3(herokey);
				return;
			case TROOP_SH4:
				troop.setSh4(herokey);
				return;
			default:
				return;
			}
		}
	}
	
	//通过位置取到英雄的key
	public int getHkeyFrTrByloc(xbean.Troop troop,int heroLocationNum)
	{
		if(troop == null)
			return 0;
		else
		{
			switch((Integer)heroLocationNum)
			{
			case TROOP_LOCATION1:
				return troop.getLocation1();
			case TROOP_LOCATION2:
				return troop.getLocation2();
			case TROOP_LOCATION3:
				return troop.getLocation3();
			case TROOP_LOCATION4:
				return troop.getLocation4();
			case TROOP_LOCATION5:
				return troop.getLocation5();
			default:
				return 0;
			}
		}
	}
	
	//通过英雄的KEY取到阵形位置
	public int getLocFrTrByHkey(xbean.Troop troop,int herokey)
	{
		if(troop == null)
			return 0;
		else
		{
			if(troop.getLocation1() == herokey)
				return TROOP_LOCATION1;
			if(troop.getLocation2() == herokey)
				return TROOP_LOCATION2;
			if(troop.getLocation3() == herokey)
				return TROOP_LOCATION3;
			if(troop.getLocation4() == herokey)
				return TROOP_LOCATION4;
			if(troop.getLocation5() == herokey)
				return TROOP_LOCATION5;
			if(troop.getSh1() == herokey)
				return TROOP_SH1;
			if(troop.getSh2() == herokey)
				return TROOP_SH2;
			if(troop.getSh3() == herokey)
				return TROOP_SH3;
			if(troop.getSh4() == herokey)
				return TROOP_SH4;
			return 0;
		}
	}
	
	public xbean.Troop createTroop(int troopNum,int trooptype,int location1,int location2,int location3,
			int location4,int location5,int sh1,int sh2,int sh3,int sh4)
	{
		xbean.Troop troop = xbean.Pod.newTroop();
		troop.setTroopnum(troopNum);
		troop.setTrooptype(trooptype);
		troop.setLocation1(location1);
		troop.setLocation2(location2);
		troop.setLocation3(location3);
		troop.setLocation4(location4);
		troop.setLocation5(location5);
		troop.setSh1(sh1);
		troop.setSh2(sh2);
		troop.setSh3(sh3);
		troop.setSh4(sh4);
		return troop;
	}
	
	//通过编号获得阵形
	public xbean.Troop getTroopByNum(int troopNum)
	{
		if( !isRightTroopNum(troopNum))
		{
			Message.psendMsgNotify(roleId, 135);
			return null;
		}
		List<xbean.Troop> troops = getTroops();
		for(xbean.Troop troop : troops )
		{
			if(troop.getTroopnum() == troopNum)
				return troop;		
		}
		
		xbean.Troop troop = createTroop(troopNum,1,0,0,0,0,0,1401100072,0,-1,-1);	//数值为测试数据
		xcolumn.getTroops().add(troop);
		return troop;
	}
	
	//获取阵形列表
	public List<xbean.Troop> getTroops(){
		return xcolumn.getTroops();
	}
	
	public List<chuhan.gsp.Troop> getProtocolTroops()
	{
		
		////测试用~~~~~~~~~~~~~~~~
		if(getTroops().size() == 0)
		{
//			PAddTroop pAddTroop = new PAddTroop(roleId, 1,0,1);
//			pAddTroop.call();
//			PAddTroop pAddTroop2 = new PAddTroop(roleId, 2,0,2);
//			pAddTroop2.call();
		}
		
		List<chuhan.gsp.Troop> troops = new LinkedList<chuhan.gsp.Troop>();
		for(xbean.Troop trp : getTroops())
		{
			chuhan.gsp.Troop troop = new chuhan.gsp.Troop();
			troop.troopnum = trp.getTroopnum();
			troop.trooptype = trp.getTrooptype();
			troop.location1 = trp.getLocation1();
			troop.location2 = trp.getLocation2();
			troop.location3 = trp.getLocation3();
			troop.location4 = trp.getLocation4();
			troop.location5 = trp.getLocation5();
			troop.sh1 = trp.getSh1();
			troop.sh2 = trp.getSh2();
			troop.sh3 = trp.getSh3();
			troop.sh4 = trp.getSh4();
			troops.add(troop);
		}
			
		return troops;
	}
	
	//刷新阵形，给客户端发消息
	public void refreshTroops()
	{
		SRefreshTroops snd = new SRefreshTroops();
		snd.troops.addAll(getProtocolTroops());
		xdb.Procedure.psend(roleId, snd);
	}
	
	//英雄是否在阵形中
	public boolean heroInTroop(int herokey,xbean.Troop troop)
	{
		if(herokey == troop.getLocation1() || 
				herokey == troop.getLocation2() || 
				herokey == troop.getLocation3() || 
				herokey == troop.getLocation4() ||
				herokey == troop.getLocation5() ||
				herokey == troop.getSh1() ||
				herokey == troop.getSh2() ||
				herokey == troop.getSh3() ||
				herokey == troop.getSh4() )
		{
			return true;
		}
		return false;
	}
	
	//英雄是否在阵形中
	public boolean heroInTroops(int key) {
		for(xbean.Troop trp : getTroops())
		{
			if(heroInTroop(key,trp))
			{
				return true;
			}
		}
		return false;
	}
	
	/**
	 * 是否有相同英雄在非对换位置上
	 * @param hero
	 * @param troop
	 * @param herocol
	 * @param location
	 * @return
	 *//*
	public boolean sameTypeheroInTroop(Hero hero,xbean.Troop troop,HeroColumn herocol,int location)
	{
		Hero heroTemp = null;
		heroTemp = herocol.getHByHKey(troop.getLocation1());
		if(hero.isSameModeHero(heroTemp) && !hero.isSameHero(heroTemp) && location != TROOP_LOCATION1)
			return true;	
		heroTemp = herocol.getHByHKey(troop.getLocation2());
		if(hero.isSameModeHero(heroTemp) && !hero.isSameHero(heroTemp)&& location != TROOP_LOCATION2)
			return true;
		heroTemp = herocol.getHByHKey(troop.getLocation3());
		if(hero.isSameModeHero(heroTemp) && !hero.isSameHero(heroTemp)&& location != TROOP_LOCATION3)
			return true;
		heroTemp = herocol.getHByHKey(troop.getLocation4());
		if(hero.isSameModeHero(heroTemp) && !hero.isSameHero(heroTemp)&& location != TROOP_LOCATION4)
			return true;
		heroTemp = herocol.getHByHKey(troop.getLocation5());
		if(hero.isSameModeHero(heroTemp) && !hero.isSameHero(heroTemp)&& location != TROOP_LOCATION5)
			return true;

		return false;
	}*/
	
	/**
	 * 清空战队
	 * @param troop
	 *//*
	public void cleanTroop(Troop troop){
		troop.setLocation1(0);
		troop.setLocation2(0);
		troop.setLocation3(0);
		troop.setLocation4(0);
		troop.setLocation5(0);
	}*/
	
	/**
	 * 改变队形入口
	 * @param herokey
	 * @param troopId
	 * @param location
	 * @param trooptype
	 * @return
	 */
	public xbean.Troop addTroop(int herokey,int locationid)
	{
		
		HeroColumn herocol = HeroColumn.getHeroColumn(roleId, false);
		
		this.saveDefaultTroop((byte)0);
		xbean.Troop xtroop = this.getTroopByNum(0);
		/*
		//判断战队类型是否能改变
		if(trooptype != troop.getTrooptype() && (trooptype == 1 || trooptype == 2)){
			if(trooptype == 2 && troop.getLocation3() != 0){
				Hero hero = herocol.getHByHKey(troop.getLocation3());
				if(hero == null){
					Message.psendMsgNotify(roleId, 135);
					return null;
				}
				if(hero.isCloseAttack()){
					Message.psendMsgNotify(roleId, 135);
					return null;
				}
			}
			troop.setTrooptype(trooptype);
			cleanTroop(troop);
			return troop;
		}*/
		
		if(herokey == 0){
			changeTroop(xtroop,locationid,herokey);
			return xtroop;
		}
		
		if(locationid == TROOP_LOCATION1 || locationid == TROOP_LOCATION2 || locationid == TROOP_LOCATION3 ||
				locationid == TROOP_LOCATION4 || locationid == TROOP_LOCATION5){
			Hero hero = herocol.getHByHKey(herokey);
			if(hero == null){
				Message.psendMsgNotify(roleId, 135);
				return null;
			}
			/*
			if(sameTypeheroInTroop(hero,troop,herocol,location))
			{
				Message.psendMsgNotify(roleId, 135);
				return null;
			}
			
			if( !isCloseLocation(troop.getTrooptype(),location) && hero.isCloseAttack() ){
				Message.psendMsgNotify(roleId, 135);
				return null;
			}*/
			if(hero.getiHeroInfo().getQosition() == 1 && 
					locationid != this.TROOP_LOCATION1 && locationid != this.TROOP_LOCATION2){
				Message.psendMsgNotify(roleId, 135);
				return null;
			}	
		}
		
			
		if(heroInTroop(herokey,xtroop)){
			int nowLoc = getLocFrTrByHkey(xtroop,herokey);
			int herokeytemp = getHkeyFrTrByloc(xtroop,locationid);
			
			Hero herotemp = herocol.getHByHKey(herokeytemp);
			if( herotemp != null && herotemp.getiHeroInfo().getQosition() == 1 && 
					nowLoc != this.TROOP_LOCATION1 && nowLoc != this.TROOP_LOCATION2 ){
				Message.psendMsgNotify(roleId, 135);
				return null;
			}
			
			changeTroop(xtroop,locationid,herokey);
			changeTroop(xtroop,nowLoc,herokeytemp);
		}else{
			changeTroop(xtroop,locationid,herokey);
		}
		
		return xtroop;
	}
	
	/**
	 * 判断位置是否为近战位置
	 * @param troopType
	 * @param location
	 * @return
	 *//*
	public boolean isCloseLocation(int troopType,int location){
		if(location == TROOP_LOCATION1 || location == TROOP_LOCATION2){
			return true;
		}
		if(location == TROOP_LOCATION3 && troopType == 2){
			return true;
		}
		return false;
	}*/
	
	//获取阵形中的英雄
	public java.util.LinkedList<Integer> getHkeylistByTrid(int troopid)
	{
		xbean.Troop troop = getTroopByNum(troopid);
		if(troop == null)
			return null;
		java.util.LinkedList<Integer> returnlist = new java.util.LinkedList<Integer>();
		if(troop.getLocation1() != 0)
			returnlist.addFirst(troop.getLocation1());
		if(troop.getLocation2() != 0)
			returnlist.addFirst(troop.getLocation2());
		if(troop.getLocation3() != 0)
			returnlist.addFirst(troop.getLocation3());
		if(troop.getLocation4() != 0)
			returnlist.addFirst(troop.getLocation4());
		if(troop.getLocation5() != 0)
			returnlist.addFirst(troop.getLocation5());
		return returnlist;
	}
	
	//获取阵形中的英雄
	public java.util.HashMap<Integer,Integer> getHkeyLolistByTrid(int troopid)
	{
		xbean.Troop troop = getTroopByNum(troopid);
		if(troop == null)
			return null;
		java.util.HashMap<Integer,Integer> returnMap = new java.util.HashMap<Integer,Integer>();
		returnMap.put(TROOP_LOCATION1, troop.getLocation1());
		returnMap.put(TROOP_LOCATION2, troop.getLocation2());
		returnMap.put(TROOP_LOCATION3, troop.getLocation3());
		returnMap.put(TROOP_LOCATION4, troop.getLocation4());
		returnMap.put(TROOP_LOCATION5, troop.getLocation5());
		return returnMap;
	}
	
	/**
	 * 获取战队阵型
	 * @param troopid
	 * @return
	 */
	public int getTroopTypeByTrid(int troopid)
	{
		xbean.Troop troop = getTroopByNum(troopid);
		if(troop == null)
			return 1;
		return troop.getTrooptype();
	}
	
	/**
	 * 英雄退出所有战队
	 * @param team
	 */
	public void HeroOutAllTroop(List<Integer> team){
		for(Integer herokey : team){
			HeroOutAllTroop(herokey);
		}
	}
	
	/**
	 * 英雄退出所有战队
	 * @param herokey
	 */
	public void HeroOutAllTroop(int herokey)
	{
		if( !heroInTroops(herokey) )
			return;
		for(xbean.Troop trp : getTroops())
		{
			if(trp.getLocation1() == herokey)
				trp.setLocation1(0);
			else if(trp.getLocation2() == herokey)
				trp.setLocation2(0);
			else if(trp.getLocation3() == herokey)
				trp.setLocation3(0);
			else if(trp.getLocation4() == herokey)
				trp.setLocation4(0);
			else if(trp.getLocation5() == herokey)
				trp.setLocation5(0);
		}
		this.refreshTroops();
		return;
	}
	
	/**
	 * 保存默认队形
	 * @param troopNum
	 */
	public void saveDefaultTroop(byte troopNum){
		if( !isRightTroopNum(troopNum) )
			return;
		chuhan.gsp.attr.PropRole proprole = chuhan.gsp.attr.PropRole.getPropRole(roleId, false);
		if(proprole != null)
			proprole.getProperties().setTroopnum(troopNum);
	}
	
	
}
