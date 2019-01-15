package chuhan.gsp.battle;

import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;

import chuhan.gsp.Dictionary;
import chuhan.gsp.attr.GoldAddType;
import chuhan.gsp.attr.PAddExpProc;
import chuhan.gsp.attr.PropRole;
import chuhan.gsp.hero.PAddExpHero;
import chuhan.gsp.hero.PAddHero;
import chuhan.gsp.log.Logger;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.Message;
import chuhan.gsp.task.monstergroup12;
import chuhan.gsp.util.ParserString;




public class BattleManager{

	public static Logger logger = Logger.getLogger(BattleManager.class);
	static private BattleManager instance = null;
	
	public static final int PASS_LEVEL0 = 0;
	public static final int PASS_LEVEL1 = 1;
	public static final int PASS_LEVEL2 = 2;
	public static final int PASS_LEVELALL = 3;
	
	public static final int PASS_EXP_PERCENT = 100;
	public static final int PASS_LEVEL0_EXP_PERCENT = 0;
	public static final int PASS_LEVEL1_EXP_PERCENT = 20;
	public static final int PASS_LEVEL2_EXP_PERCENT = 40;
	public static final int PASS_LEVELALL_EXP_PERCENT = 100;
	
	public static final int PASS_GOLD_PERCENT = 100;
	public static final int PASS_LEVEL0_GOLD_PERCENT = 0;
	public static final int PASS_LEVEL1_GOLD_PERCENT = 30;
	public static final int PASS_LEVEL2_GOLD_PERCENT = 60;
	public static final int PASS_LEVELALL_GOLD_PERCENT = 100;
	
	private BattleManager(){}
	public static BattleManager getInstance() {
		if(instance == null)
		{
			instance = new BattleManager();
		}
		return instance;
	}
	
	private java.util.Hashtable<Long,Battle> mBattleMap = new java.util.Hashtable<Long,Battle>();
	
	//创建一个关卡
	public Battle CreateBattleInfo(long roleId, int battleId, int troopid, String dropstr)
	{
		Battle battleInfo = new Battle(roleId,battleId);
		if(!battleInfo.CreateBattleInfo(troopid,dropstr))
			return null;
		
		this.mBattleMap.put(roleId, battleInfo);
		SaveGameLevel(battleInfo);
		return battleInfo;
	}
	
	public void SaveGameLevel(Battle battleInfo)
	{
		xbean.GameLevel gamelevel = xtable.Gamelevels.select(battleInfo.getRoleId());
		if(gamelevel == null)
		{
			gamelevel = xbean.Pod.newGameLevel();
			xtable.Gamelevels.insert(battleInfo.getRoleId(),gamelevel);
		}
		gamelevel.getUseherokeylist().clear();
		gamelevel.getEquipidlist().clear();
		
		gamelevel.setBattleid(battleInfo.getiBattleInfo().id);
		gamelevel.getUseherokeylist().putAll(battleInfo.getUseherokeyList());
		gamelevel.setDropgold(battleInfo.getDropGold());
		gamelevel.setDropcrystal(battleInfo.getDropCrystal());
		gamelevel.getEquipidlist().addAll(battleInfo.getInDropList());
		gamelevel.setTrooptype(battleInfo.getTrooptype());
	}
	
	public void DelGameLevel(long roleId)
	{
		if(xtable.Gamelevels.select(roleId) != null)
		{
			xtable.Gamelevels.delete(roleId);
		}
	}
	
	public void DeleteBattleInfo(long roleId)
	{
		mBattleMap.remove(roleId);
		DelGameLevel(roleId);
	}
	
	public Battle GetBInfoByRId(long roleId)
	{
		Battle returnBIn = this.mBattleMap.get(roleId);
		if(returnBIn == null)
		{
			xbean.GameLevel gamelevel = xtable.Gamelevels.select(roleId);
			if(gamelevel != null)
			{
				returnBIn = new Battle(roleId,gamelevel);
				if(returnBIn.getiBattleInfo() == null)
				{
					returnBIn = null;
				}else{
					mBattleMap.put(roleId, returnBIn);
				}
			}
		}
		return returnBIn;
	}
	
	public int GetPassExpPercent(int pass)
	{
		switch((Integer)pass)
		{
		case PASS_LEVEL0:
			return PASS_LEVEL0_EXP_PERCENT;
		case PASS_LEVEL1:
			return PASS_LEVEL1_EXP_PERCENT;
		case PASS_LEVEL2:
			return PASS_LEVEL2_EXP_PERCENT;
		case PASS_LEVELALL:
			return PASS_LEVELALL_EXP_PERCENT;
		default:
			return 0;
		}
	}
	
	public int GetPassGoldPercent(int pass)
	{
		switch((Integer)pass)
		{
		case PASS_LEVEL0:
			return PASS_LEVEL0_GOLD_PERCENT;
		case PASS_LEVEL1:
			return PASS_LEVEL1_GOLD_PERCENT;
		case PASS_LEVEL2:
			return PASS_LEVEL2_GOLD_PERCENT;
		case PASS_LEVELALL:
			return PASS_LEVELALL_GOLD_PERCENT;
		default:
			return 0;
		}
	}
	
	//完成一个关卡，处理奖励相关内容
	public boolean EndBattle(long roleId, int pass)
	{
		Battle battleInfo = GetBInfoByRId(roleId);
		if(battleInfo == null)
		{
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		if(pass == PASS_LEVEL0)
			return true;
		
		PropRole prole = PropRole.getPropRole(roleId, false);
		
		int gold_percent = GetPassGoldPercent(pass);
		int exp_percent = GetPassExpPercent(pass);
		
		//prole.addGold(battleInfo.getiBattleInfo().getGold()*gold_percent/PASS_EXP_PERCENT, GoldAddType.ADD_BATTLE);
		
		PAddExpHero hero = new PAddExpHero(roleId,battleInfo.getUseHeroList(),
				battleInfo.getiBattleInfo().getHeroexp()*exp_percent/PASS_EXP_PERCENT,
				PAddExpHero.BATTLE,"");
		hero.call();
		
		if (pass == PASS_LEVELALL) 
		{
			PAddExpProc proc = new PAddExpProc(roleId, 
					battleInfo.getiBattleInfo().getPlayerexp()* exp_percent/ PASS_EXP_PERCENT, 
					PAddExpProc.BATTLE, "");
			proc.call();
			
//			if(battleInfo.getHeroIdList().size() > 0)
			{
//				RewardHero(roleId, battleInfo.getHeroIdList());
			}
//			if(battleInfo.getEquipIdList().size() > 0)
			{
//				RewardEquip(roleId, battleInfo.getEquipIdList());
			}
			
//			prole.setBattleNum(battleInfo.getiBattleInfo().getId());
		}
		
		DeleteBattleInfo(roleId);
		return true;
	}

	//获取英雄卡片奖励
	public boolean RewardHero(long roleId, java.util.LinkedList<Integer> heroIdList)
	{
		for(Integer heroId : heroIdList)
		{
			PAddHero pAddHero = new PAddHero(roleId, heroId, 1,Dictionary.SYS_GET_HERO);
			pAddHero.call();
		}
		
		return true;
	}
	
	//获取装备奖励
	public boolean RewardEquip(long roleId, java.util.LinkedList<Integer> equipIdList)
	{
		for(Integer equipId : equipIdList)
		{
			
		}	
		
		return true;
	}
	
	/**
	 * 通过怪物组列表返回怪物列表ID
	 * @param mgroupList
	 * @return
	 */
	public LinkedList<Integer> getMonsterList(List<Integer> mgroupList){
		java.util.LinkedList<Integer> mList = new java.util.LinkedList<Integer>();
		for(Integer groupid : mgroupList){
			monstergroup12 group = ConfigManager.getInstance().getConf(monstergroup12.class).get(groupid);
			if(group == null){
				mList.add(groupid);
				continue;
			}
			mList.add(getDropId(getDropMap(group)));
		}
		
		return mList;
	}
	
	/**
	 * 通过怪物组ID返回怪物
	 * @param mgroupid
	 * @return
	 */
	public int getMonsterList(int mgroupid){
		monstergroup12 group = ConfigManager.getInstance().getConf(monstergroup12.class).get(mgroupid);
		if(group == null){
			return -1;
		}
		return getDropId(getDropMap(group));
	}
	
	/**
	 * 通过怪物组返回几率map
	 * @param group
	 * @return
	 */
	public HashMap<Integer,DropInit> getDropMap(monstergroup12 group){
		List<Integer> allDrop = ParserString.parseString2Int(group.getMonsterid());
		List<Integer> allProb = ParserString.parseString2Int(group.getMonsterprobability());
		HashMap<Integer,DropInit> dropMap = new HashMap<Integer,DropInit>();
		if(allDrop == null || allProb == null || allDrop.size() != allProb.size())
			return dropMap;

		for(int i = 0;i< allDrop.size();i++){
			int id = allDrop.get(i);
			int percent = allProb.get(i);
			DropInit di = new DropInit(percent, id);
			dropMap.put(dropMap.size(), di);
		}
		return dropMap;
	}
	
	/**
	 * 根据比例计算掉落
	 * @param dropMap
	 * @return
	 */
	public int getDropId(java.util.HashMap<Integer,DropInit> dropMap)
	{
		int[] percentlist = new int[dropMap.size()];
		int i = 0;
		for (java.util.Map.Entry<Integer, DropInit> drop : dropMap.entrySet()) {
			percentlist[i++] = drop.getValue().percent;
		}
		int dropnum = chuhan.gsp.util.Misc.getProbability(percentlist);
		if(dropnum == -1)
			return 0;
		DropInit drop = dropMap.get(dropnum);
		if(drop == null)
			return 0;
		
		return drop.id;
	}
	
	public class DropInit
	{
		final int percent;
		final int id;
		DropInit(int percent, int id)
		{
			this.percent = percent;
			this.id = id;
		}
	}
	
	/**
	 * 是否是英雄攻击
	 * @param m_attacker
	 * @return
	 */
	public static boolean isHeroAttacker(byte m_attacker){
		if((m_attacker & 0x80) == 0x80){
			return true;
		}
		return false;
	}
	
	
	
	
}
