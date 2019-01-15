package chuhan.gsp.battle;

import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

import chuhan.gsp.attr.AttrType;

public abstract class BattleUtil {

	public static final int HOST_FIRST_ID = 1;
	public static final int GUEST_FIRST_ID = 6;
	public static final int SIDE_MAX_FIGHTER = 5;
	public static final int HOST_OUTBATTLE_FIRST_ID = 11;
	public static final int GUEST_OUTBATTLE_FIRST_ID = 21;
	public static final int SIDE_MAX_OUTBATTLE = 10;
	
	public static final int BATTLE_RESULT_WIN = 1;//赢
	public static final int BATTLE_RESULT_LOSE = -1;//输
	public static final int BATTLE_RESULT_DRAW = 2;//平局
	public static final int BATTLE_RESULT_CONTINUE = 0;//未结束
	
	public static final int ROUND_MAX_TURN = 30;
	
	static int[] OPERATE_POS_SEQUENCE1 = {1,7,3,9,5,6,2,8,4,10};//第一回合出手顺序,位置序
	static int[] OPERATE_POS_SEQUENCE2 = {6,2,8,4,10,1,7,3,9,5};//第二回合出手顺序,位置序
	
	public static int DEFAULT_DIRECT_END_SECOND = 12;
	
	/**
	 * 根据距离获取顺序的目标
	 * @param battle
	 * @param opfighter
	 * @return
	 */
	public static List<Fighter> getSequenceAims(xbean.BattleInfo battle, Fighter opfighter)
	{
		List<Fighter> fighters = new LinkedList<Fighter>();
		
		int frontpos = getFrontPos(opfighter.getPosition());
		boolean ishost = isHost(frontpos);
		
		int minid = (ishost) ? HOST_FIRST_ID : GUEST_FIRST_ID;
		int maxid = minid+SIDE_MAX_FIGHTER -1;

		for(int i = 0; i < SIDE_MAX_FIGHTER; i++)
		{
			if(frontpos-i >= minid)
			{
				Fighter fighter = getFighterByPos(battle, frontpos-i);
				if(fighter != null && !fighters.contains(fighter))
					fighters.add(fighter);
			}
			if(frontpos+i <= maxid)
			{
				Fighter fighter = getFighterByPos(battle, frontpos+i);
				if(fighter != null && !fighters.contains(fighter))
					fighters.add(fighter);
			}
		}
		return fighters;
	}
	
	
	public static int getFrontPos(int pos)
	{
		boolean ishost = isHost(pos);
		return ishost ? pos + SIDE_MAX_FIGHTER : pos - SIDE_MAX_FIGHTER;
	}
	/**
	 * 
	 * @param fighterId 或者 pos
	 * @return
	 */
	public static boolean isHost(int idorpos)
	{
		return (idorpos >= HOST_FIRST_ID && idorpos < GUEST_FIRST_ID) || (idorpos >= HOST_OUTBATTLE_FIRST_ID && idorpos< HOST_OUTBATTLE_FIRST_ID+SIDE_MAX_OUTBATTLE);
	}
	
	public static Map<Integer,Fighter> getHostFighters(xbean.BattleInfo battle)
	{
		Map<Integer,Fighter> hostfighters = new HashMap<Integer, Fighter>();
		for(Fighter fighter : battle.getFighters().values())
		{
			if(isHost(fighter.getFighterId()))
				hostfighters.put(fighter.getFighterId(), fighter);
		}
		return hostfighters;
	}
	
	public static Map<Integer,Fighter> getGuestFighters(xbean.BattleInfo battle)
	{
		Map<Integer,Fighter> guestfighters = new HashMap<Integer, Fighter>();
		for(Fighter fighter : battle.getFighters().values())
		{
			if(!isHost(fighter.getFighterId()))
				guestfighters.put(fighter.getFighterId(), fighter);
		}
		return guestfighters;
	}
	
	public static Fighter getFighterByPos(xbean.BattleInfo battle, int pos)
	{
		for(Fighter fighter : battle.getFighters().values())
		{
			if(fighter.getPosition() == pos)
				return fighter;
		}
		return null;
	}
	/**
	 * 获取距离，在一边或两边都会返回距离
	 * @param pos1
	 * @param pos2
	 * @return
	 */
	public static int getDistance(int pos1, int pos2)
	{
		boolean ishost1 = isHost(pos1);
		boolean ishost2 = isHost(pos2);
		if(ishost1 == ishost2)
			return Math.abs(pos1 - pos2);
		else
			return Math.abs(getFrontPos(pos1) - pos2);
	}
	
	public static int getBattleResult(xbean.BattleInfo battle)
	{
		int alivehost = 0;
		int aliveguest = 0;
		for(Fighter fighter : battle.getFighters().values())
		{
			if(fighter.isDeath())
				continue;
			if(fighter.isHost())
				alivehost++;
			else
				aliveguest++;
		}
		if(alivehost >0 && aliveguest > 0)
			return BATTLE_RESULT_CONTINUE;
		if(alivehost >0 && aliveguest == 0)
			return BATTLE_RESULT_WIN;
		if(alivehost == 0 && aliveguest > 0)
			return BATTLE_RESULT_LOSE;
		
		return BATTLE_RESULT_DRAW;
	}
	
	public static boolean checkRoundEnd(xbean.BattleInfo battle)
	{
		if(battle.getRound() >= 3)
			return false;//第3回合没有结束
		if(battle.getBattletype() == BattleType.RAID_BOSS_PVE)
			return false;//raid boss没有结束
		for(Fighter fighter : battle.getFighters().values())
		{
			if(fighter.isDeath())
				continue;
			int pos = getFrontPos(fighter.getPosition());
			Fighter frontfighter = getFighterByPos(battle, pos);
			if(frontfighter == null)
				continue;
			if(!frontfighter.isDeath())
				return false;
		}
		
		return true;
	}
	
	public static int getSumSpeed(xbean.BattleInfo battle, boolean ishost)
	{
		int speed = 0;
		for(Fighter fighter : battle.getFighters().values())
		{
			if(isHost(fighter.getFighterId()) == ishost)
				speed+=fighter.getAttrFighter().getAttrById(AttrType.SPEED);
		}
		return speed;
	}
	
	public static int[] getOperateSequence(int round, boolean hostfirst)
	{
		if(round%2 == 1)
			return hostfirst ? OPERATE_POS_SEQUENCE1 : OPERATE_POS_SEQUENCE2;
		else
			return hostfirst ? OPERATE_POS_SEQUENCE2 : OPERATE_POS_SEQUENCE1;
	}
	/**
	 * 是否是被动技能
	 * @param skillId
	 * @return
	 */
	public static boolean isPassiveSkill(int skillId)
	{
		return (skillId >= 5300 && skillId < 5400);
	}
	
	public static int checkValidFighterId(int fighterId,boolean ishost)
	{
		int inBattleStartId = ishost? HOST_FIRST_ID: GUEST_FIRST_ID;
		int inBattleEndId = inBattleStartId + SIDE_MAX_FIGHTER;
		int outBattleStartId = ishost? HOST_OUTBATTLE_FIRST_ID: GUEST_OUTBATTLE_FIRST_ID;
		int outBattleEndId = outBattleStartId + SIDE_MAX_OUTBATTLE;
		if(fighterId >= inBattleStartId && fighterId < inBattleEndId)
			return fighterId;
		else if(fighterId >= inBattleEndId && fighterId < outBattleStartId)
			return outBattleStartId;
		else if(fighterId >= outBattleStartId && fighterId < outBattleEndId)
			return fighterId;
		else
			return -1;
	}
	
	public static boolean inBattle(int pos)
	{
		if(pos >= HOST_FIRST_ID && pos < HOST_FIRST_ID+SIDE_MAX_FIGHTER)
			return true;
		if(pos >= GUEST_FIRST_ID && pos < GUEST_FIRST_ID+SIDE_MAX_FIGHTER)
			return true;
		return false;
	}
}
