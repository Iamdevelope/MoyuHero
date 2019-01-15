package chuhan.gsp.battle;

import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;

import chuhan.gsp.award.DropManager;
import chuhan.gsp.hero.TroopColumn;
import chuhan.gsp.log.LogBehavior;
import chuhan.gsp.log.Logger;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.Message;
import chuhan.gsp.task.monstergroup12;
import chuhan.gsp.task.stage11;
import chuhan.gsp.util.Conv;
import chuhan.gsp.util.ParserString;



public class Battle {

	public static final Logger logger = Logger.getLogger(Battle.class);
	
//	private static final double GOLD_FLOAT = 0.05;
	private static final String LINE_SPLIT = "#";
//	private static final String PERCENT_SPLIT = ":";
	
	private final long roleId;
	private final stage11 iGuankaInfo;
	
	

	private java.util.HashMap<Integer,Integer> useherokeyList; 
	private int dropGold = 0;
	private int dropCrystal = 0;
	private java.util.LinkedList<Integer> inDropList;
	private java.util.LinkedList<Integer> monsterList;
	private int trooptype = 1;
	
	private int shopOrBattle = 0;

	

	public java.util.HashMap<Integer, Integer> getUseherokeyList() {
		return useherokeyList;
	}
	
	public java.util.LinkedList<Integer> getUseHeroList()
	{
		java.util.LinkedList<Integer> returnlist = new java.util.LinkedList<Integer>(); 
		for(java.util.Map.Entry<Integer, Integer> e : useherokeyList.entrySet())
		{
			if(e.getValue() != 0)
			{
				returnlist.addFirst(e.getValue());
			}
		}
		return returnlist;
	}
	
	public Battle(long roleId, int scenarioKey)
	{
		this.iGuankaInfo = ConfigManager.getInstance().getConf(stage11.class).get(scenarioKey);
		this.roleId = roleId;
		useherokeyList = new java.util.HashMap<Integer,Integer>();
		inDropList = new java.util.LinkedList<Integer>();
		monsterList = new java.util.LinkedList<Integer>();
	
	}
	
	public Battle(long roleId, xbean.GameLevel gamelevel)
	{
		this.iGuankaInfo = ConfigManager.getInstance().getConf(stage11.class).get(gamelevel.getBattleid());
		this.roleId = roleId;
		useherokeyList.putAll(gamelevel.getUseherokeylist());
		inDropList.addAll(gamelevel.getEquipidlist());
		this.dropGold = gamelevel.getDropgold();
		this.dropCrystal = gamelevel.getDropcrystal();
		this.trooptype = gamelevel.getTrooptype();
	}
	
	public chuhan.gsp.stage.BattleInfo getProtocolBattelInfo()
	{
		chuhan.gsp.stage.BattleInfo pReturn = new chuhan.gsp.stage.BattleInfo();
		pReturn.battleid = this.iGuankaInfo.getId();
		pReturn.useherokeylist.putAll(this.useherokeyList);
		pReturn.monstergroup.addAll(this.monsterList);
		pReturn.heroexp = this.iGuankaInfo.getHeroexp();
		pReturn.teamexp = this.iGuankaInfo.getPlayerexp();
		pReturn.tili = this.iGuankaInfo.getCost();
		pReturn.trooptype = this.getTrooptype();
		pReturn.indroplist = this.getInDropList();
		
		return pReturn;
	}
	
	public boolean CreateBattleInfo(int troopid,String dropstr)
	{
		if(this.iGuankaInfo == null)
		{
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		chuhan.gsp.attr.PropRole role = chuhan.gsp.attr.PropRole.getPropRole(roleId,false);
//		if(!proprole.useTili(iGuankaInfo.getTili()))
//		{
//			xdb.Procedure.psend(roleId, new SErrorType(ErrorType.ERR_NOT_ENOUGH_TI));
//			return false;
//		}
		
		TroopColumn troopcol = TroopColumn.getTroopColumn(roleId, false);
		this.useherokeyList = troopcol.getHkeyLolistByTrid(troopid);
		this.trooptype = troopcol.getTroopTypeByTrid(troopid);
		
		this.dropGold = iGuankaInfo.getGoldreward();
		this.dropCrystal = 0;
//		HeOrEqDrop();
		
//		String str = iGuankaInfo.getMonstergroup();
		List<Integer> mgroupList = ParserString.parseString2Int(iGuankaInfo.getMonstergroup());
		LinkedList<Integer> mList = BattleManager.getInstance().getMonsterList(mgroupList);
		if(mList != null && mList.size() != 0)
			this.monsterList.addAll(mList);
		this.inDropList.clear();
		this.inDropList.addAll(DropManager.getInstance().drop(roleId, dropstr, LogBehavior.STAGEREWARD,false,iGuankaInfo.getId()));
		troopcol.saveDefaultTroop(Conv.toByte(troopid));
		return true;
	}
	
	
	
	
	/*
	//计算英雄卡片或装备卡片掉落
	public void HeOrEqDrop()
	{
		int[] percentlist = new int[3];
		//percentlist[0] = iBattleInfo.getHeropercent();
		//percentlist[1] = iBattleInfo.getEquippercent();
		int noDrop = 100-percentlist[0]-percentlist[1];
		if(noDrop < 0)
			percentlist[2] = 0;
		else
			percentlist[2] = noDrop;
		
		int dropnum = chuhan.gsp.util.Misc.getProbability(percentlist);
		if(dropnum == 0)
		{
			//this.heroIdList.add(getDropId(strTodrop(iBattleInfo.getHerodrop())));
		}
		else if(dropnum == 1)
		{
			//this.equipIdList.add(getDropId(strTodrop(iBattleInfo.getEquipdrop())));
		}
		
		String str = iGuankaInfo.getMonstergroup();
		if (-1 != str.indexOf(LINE_SPLIT)) {
			String[] drop = str.split(LINE_SPLIT);
			for(String id : drop) {
				this.monsterList.add(Integer.valueOf(id));
			}
		}else{
			this.monsterList.add(Integer.valueOf(str));
		}		
	}
	*/
	/*
	//解析英雄掉落和物品掉落配表
	public java.util.HashMap<Integer,DropInit> strTodrop(String str)
	{
		java.util.HashMap<Integer,DropInit> dropMap = new java.util.HashMap<Integer,DropInit>();
		
		if ( -1 != str.indexOf(LINE_SPLIT) ) {
			String[] dropline = str.split(LINE_SPLIT);
			for (String strline : dropline) {
				if (-1 != strline.indexOf(PERCENT_SPLIT)) {
					String[] drop = strline.split(PERCENT_SPLIT);
					if (drop.length == 2) {
						int id = Integer.parseInt(drop[0]);
						int percent = Integer.parseInt(drop[1]);
						DropInit di = new DropInit(percent, id);
						dropMap.put(dropMap.size(), di);
					}
				}
			}
		}
		else
		{
			if (-1 != str.indexOf(PERCENT_SPLIT)) {
				String[] drop = str.split(PERCENT_SPLIT);
				if (drop.length == 2) {
					int id = Integer.parseInt(drop[0]);
					int percent = Integer.parseInt(drop[1]);
					DropInit di = new DropInit(percent, id);
					dropMap.put(dropMap.size(), di);
				}
			}
			
		}
		return dropMap;
	}
	*/
	
	
	//金币的随机数
	public int floatnum(int initnum, double floatnum, boolean isunsigned)
	{
		double minPct = 1 - floatnum;
		double maxPct = 1 + floatnum;
		if(isunsigned)
		{
			minPct = 1;
		}
		return chuhan.gsp.util.Misc.randomValue(initnum, minPct, maxPct);
	}
	
	public int getDropGold() {
		return dropGold;
	}

	public void setDropGold(int dropGold) {
		this.dropGold = dropGold;
	}

	public int getDropCrystal() {
		return dropCrystal;
	}

	public void setDropCrystal(int dropCrystal) {
		this.dropCrystal = dropCrystal;
	}
	
	public java.util.LinkedList<Integer> getInDropList() {
		return inDropList;
	}

	public void setInDropList(java.util.LinkedList<Integer> inDropList) {
		this.inDropList = inDropList;
	}
	
	public stage11 getiBattleInfo() {
		return iGuankaInfo;
	}
	
	
	public long getRoleId() {
		return roleId;
	}
	
	public int getTrooptype() {
		return trooptype;
	}

	public void setTrooptype(int trooptype) {
		this.trooptype = trooptype;
	}


	
	
}
