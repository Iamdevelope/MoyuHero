package chuhan.gsp.battle;

import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

import chuhan.gsp.attr.AttrType;
import chuhan.gsp.award.AwardItem;
import chuhan.gsp.award.AwardManager;
import chuhan.gsp.battle.operation.Attack;
import chuhan.gsp.battle.operation.BasicOperation;
import chuhan.gsp.battle.operation.UseSkill;
import chuhan.gsp.buff.BuffConstant;
import chuhan.gsp.buff.ContinualBuff;
import chuhan.gsp.gm.GMInterface;
import chuhan.gsp.hero.OldHero;
import chuhan.gsp.hero.OldHeroColumn;
import chuhan.gsp.hero.OldTroop;
import chuhan.gsp.hero.TroopRelation;
import chuhan.gsp.item.BagTypes;
import chuhan.gsp.item.ItemColumn;
import chuhan.gsp.item.SSkill;
import chuhan.gsp.item.types.EquipItem;
import chuhan.gsp.log.Logger;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.MsgRole;
import chuhan.gsp.util.Conv;
import chuhan.gsp.util.DateUtil;
import chuhan.gsp.util.FightJSEngine;

public class PNewBattle extends xdb.Procedure {

	/**
	 * 
	 * @param roleId
	 * @param battleId 战斗id
	 * @param awardId 如果为0，则不在战斗内发送奖励
	 * @param sendscript 如果设置则直接发送协议
	 * @param directend -1不能结束，0可以结束，大于0时为中途结束，离结束的秒数（可以暂不处理，默认7s）
	 */
	public static PNewBattle createBattleByBattleId(long roleId, int battleId, int awardId, boolean sendscript, int directend) {
		return new PNewBattle(roleId, battleId, awardId, sendscript, directend);
	}
	
	/**
	 * 
	 * @param roleId
	 * @param guestRoleId pvp战斗，对方id
	 * @param awardId 如果为0，则不在战斗内发送奖励
	 * @param sendscript 如果设置则直接发送协议
	 * @param directend -1不能结束，0可以结束，大于0时为中途结束，离结束的秒数（可以暂不处理，默认7s）
	 */
	public static PNewBattle createBattleWithGuest(long roleId, long guestRoleId, int awardId, boolean sendscript, int directend) {
		return new PNewBattle(roleId, guestRoleId, awardId, sendscript, directend);
	}
	
	/**
	 * 
	 * @param roleId
	 * @param monsterIds 怪IDs
	 * @param awardId 如果为0，则不在战斗内发送奖励
	 * @param sendscript 如果设置则直接发送协议
	 * @param directend -1不能结束，0可以结束，大于0时为中途结束，离结束的秒数（可以暂不处理，默认7s）
	 */
	public static PNewBattle createBattleWithMonsterIds(long roleId, List<Integer> monsterIds, int awardId, boolean sendscript, int directend) {
		List<Monster> monsters = new LinkedList<Monster>();
		for(int monsterId : monsterIds)
			monsters.add(new Monster(monsterId));
		return createBattleWithMonsters(roleId, monsters, awardId, sendscript, directend);
	}
	
	/**
	 * 
	 * @param roleId
	 * @param monsters 怪
	 * @param awardId 如果为0，则不在战斗内发送奖励
	 * @param sendscript 如果设置则直接发送协议
	 * @param directend -1不能结束，0可以结束，大于0时为中途结束，离结束的秒数（可以暂不处理，默认7s）
	 */
	public static PNewBattle createBattleWithMonsters(long roleId, List<Monster> monsters, int awardId, boolean sendscript, int directend) {
		return new PNewBattle(roleId, monsters, awardId, sendscript, directend);
	}
	
	
	public static Logger logger = Logger.getLogger("BATTLE"); 
	public final long roleId;
	public final long guestRoleId;
	public final int battleId;
	public final int awardId;
	public final boolean sendscript;
	public final List<Monster> monsters;
	public final int directend;
	public int battlelevel;
	public int battletype = -1;
	public boolean needRelief = true;//是否需要援军
	public Map<Integer,ContinualBuff> hostbuffs = new HashMap<Integer,ContinualBuff>();//主方buff
	public Map<Integer,ContinualBuff> guestbuffs = new HashMap<Integer,ContinualBuff>();//客方buff
	public List<Long> hostfriends = new LinkedList<Long>();//主方好友
	
	Map<Integer, AwardItem> awardresult = new HashMap<Integer, AwardItem>();
	protected SSendBattleScript snd = new SSendBattleScript();
	protected xbean.BattleInfo battle;

	/**
	 * 
	 * @param roleId
	 * @param battleId 战斗id
	 * @param awardId 如果为0，则不在战斗内发送奖励
	 * @param sendscript 如果设置则直接发送协议
	 * @param directend -1不能结束，0可以结束，大于0时为中途结束，离结束的秒数（可以暂不处理，默认7s）
	 */
	public PNewBattle(long roleId, int battleId, int awardId, boolean sendscript, int directend) {
		this.roleId = roleId;
		this.guestRoleId = 0;
		this.battleId = battleId;
		this.monsters = new LinkedList<Monster>();
		this.awardId = awardId;
		this.sendscript= sendscript;
		this.directend = directend;
	}
	
	/**
	 * 
	 * @param roleId
	 * @param guestRoleId pvp战斗，对方id
	 * @param awardId 如果为0，则不在战斗内发送奖励
	 * @param sendscript 如果设置则直接发送协议
	 * @param directend -1不能结束，0可以结束，大于0时为中途结束，离结束的秒数（可以暂不处理，默认7s）
	 */
	public PNewBattle(long roleId, long guestRoleId, int awardId, boolean sendscript, int directend) {
		this.roleId = roleId;
		this.guestRoleId = guestRoleId;
		this.battleId = 0;
		this.monsters = new LinkedList<Monster>();
		this.awardId = awardId;
		this.sendscript= sendscript; 
		this.directend = directend;
	}
	
	/**
	 * 
	 * @param roleId
	 * @param monsterIds 怪IDs
	 * @param awardId 如果为0，则不在战斗内发送奖励
	 * @param sendscript 如果设置则直接发送协议
	 * @param directend -1不能结束，0可以结束，大于0时为中途结束，离结束的秒数（可以暂不处理，默认7s）
	 *//*
	public PNewBattle(long roleId, List<Integer> monsterIds, int awardId, boolean sendscript, int directend) {
		this.roleId = roleId;
		this.monsterIds = monsterIds;
		this.monsters = new LinkedList<Monster>();
		this.guestRoleId = 0;
		this.battleId = 0;
		this.awardId = awardId;
		this.sendscript= sendscript; 
		this.directend = directend;
	}*/
	/**
	 * 
	 * @param roleId
	 * @param monsters 怪
	 * @param awardId 如果为0，则不在战斗内发送奖励
	 * @param sendscript 如果设置则直接发送协议
	 * @param directend -1不能结束，0可以结束，大于0时为中途结束，离结束的秒数（可以暂不处理，默认7s）
	 */
	public PNewBattle(long roleId, List<Monster> monsters, int awardId, boolean sendscript, int directend) {
		this.roleId = roleId;
		this.monsters = monsters;
		this.guestRoleId = 0;
		this.battleId = 0;
		this.awardId = awardId;
		this.sendscript= sendscript; 
		this.directend = directend;
	}
	
	public void setBattleLevel(int lv)
	{
		this.battlelevel = lv;
	}
	
	public Map<Integer,ContinualBuff> getHostBuffs()
	{
		return hostbuffs;
	}
	public Map<Integer,ContinualBuff> getGuestBuffs()
	{
		return guestbuffs;
	}
	public List<Long> getHostFriends()
	{
		return hostfriends;
	}
	public void setBattleType(int battletype)
	{
		this.battletype = battletype;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		battle = xbean.Pod.newBattleInfo();
		battle.setBattleid(battleId);
		battle.setEngine(new FightJSEngine());
		battle.setBattlelevel(battlelevel);
		battle.setBattletype(battletype);
		if(!enterBattle())
		{
			logger.error("enterBattle fail, cfgId = " + battleId);
			return false;
		}
		
		for(Fighter fighter : battle.getFighters().values())
		{
			snd.fighters.put(fighter.getFighterId(), fighter.getProtocolFighter());
		}
		int hostadd = 10;
		if(DateUtil.getCurrentWeekDay() == 2)
			hostadd = 50;
		snd.hostspeed = Conv.toShort(BattleUtil.getSumSpeed(battle, true)+hostadd);
		snd.guestspeed = Conv.toShort(BattleUtil.getSumSpeed(battle, false));
		if(this.battletype >= 0)
			snd.battletype = Conv.toByte(this.battletype);
		else
			snd.battletype = (byte)((guestRoleId == 0)? 0 : 1);
		snd.directend = Conv.toByte(directend);	
		processRounds();
		
		processBattleEnd();
		
		if(sendscript)
			sendSSendBattleScript();
		
		return true;
	}
	
	public boolean enterBattle()
	{
		
		if(!enterTroops(roleId, BattleUtil.HOST_FIRST_ID))
			return false;
		if(guestRoleId != 0)
		{
			if(!enterTroops(guestRoleId, BattleUtil.GUEST_FIRST_ID))
				return false;
		}
		else
		{
			SBattleConfig battlecfg = ConfigManager.getInstance()
					.getConf(SBattleConfig.class).get(battle.getBattleid());
			if (battlecfg != null)
			{
				for(int mid : battlecfg.getSpot())
					monsters.add(new Monster(mid));
			}
			if (!enterMonsters(monsters))
				return false;
		}
		sendDataForTest();
		return true;
	}
	
	public boolean enterTroops(long roleId, int startfighterId)
	{
		int fighterId = startfighterId;
		new chuhan.gsp.hero.PProcessXiulianAttr(roleId).call();
		OldHeroColumn herocol = OldHeroColumn.getHeroColumn(roleId, false);
		List<OldTroop> troops = herocol.getTroops();
		if(troops.isEmpty())
			return false;
		boolean ishost = BattleUtil.isHost(fighterId);
		Map<Integer,ContinualBuff> buffs = ishost?hostbuffs:guestbuffs;
		for(OldTroop troop : troops)
		{
			if(enterTroop(roleId, fighterId, troop, buffs, battle));
			{
				//fighterId++;
				fighterId = BattleUtil.checkValidFighterId(fighterId+1, ishost);
				if(!isNeedRelief() && fighterId >= startfighterId + BattleUtil.SIDE_MAX_FIGHTER)
					break;
			}
		}
		if(fighterId == startfighterId)
		{//一个都没加入
			return false;
		}
		//加入好友助战英雄
		enterFriends(roleId, ishost, fighterId);
		return true;
	}
	
	public boolean enterFriends(long roleId, boolean ishost,int troopfighterId)
	{
		if(!ishost)//暂时只有主方有助战
			return false;
		int startfighterId = BattleUtil.checkValidFighterId(troopfighterId, ishost);
		int fighterId = startfighterId;
		Map<Integer,ContinualBuff> buffs = BattleUtil.isHost(fighterId)?hostbuffs:guestbuffs;
		for(long friendId : hostfriends)
		{
			if(enterFriend(friendId, fighterId,  buffs, battle));
			{
				fighterId = BattleUtil.checkValidFighterId(fighterId+1, ishost);
				if(fighterId <= 0)
					break;
			}
		}
		if(fighterId == startfighterId)
		{//一个都没加入
			return false;
		}
		return true;
	}
	
	public static boolean enterTroop(long roleId, int fighterId, OldTroop troop, Map<Integer,ContinualBuff> buffs, xbean.BattleInfo battle)
	{
		Fighter fighter = creatFighterFromTroop(roleId, troop, buffs);
		fighter.getFighterInfo().setFighterid(fighterId);
		fighter.getFighterInfo().setPos(fighterId);
		battle.getFighterinfos().put(fighterId, fighter.getFighterInfo());
		battle.getFighters().put(fighterId, fighter);
		return true;
	}
	
	public static boolean enterFriend(long roleId, int fighterId, Map<Integer,ContinualBuff> buffs, xbean.BattleInfo battle)
	{
		OldHeroColumn herocol = OldHeroColumn.getHeroColumn(roleId, true);
		OldTroop troop = herocol.getTroop(1);
		if(troop == null)
			return false;
		Fighter fighter = creatFighterFromTroop(roleId, troop, buffs);
		fighter.getFighterInfo().setFighterid(fighterId);
		fighter.getFighterInfo().setPos(fighterId);
		battle.getFighterinfos().put(fighterId, fighter.getFighterInfo());
		battle.getFighters().put(fighterId, fighter);
		return true;
	}
	
	public static Fighter creatFighterFromTroop(long roleId, OldTroop troop, Map<Integer,ContinualBuff> buffs)
	{
		OldHeroColumn herocol = OldHeroColumn.getHeroColumn(roleId, false);
		OldHero hero = null;//by yanglk troop herocol.getHero(troop.getHerokey());
		if(hero == null)
			return null;
		
		xbean.FighterInfo fighterinfo = xbean.Pod.newFighterInfo();
		//by yanglk  hero		fighterinfo.setHeroid(hero.getHeroInfo().getId());
		/*//by yanglk  hero
		fighterinfo.setGrouptype(hero.getGroupType());
		fighterinfo.setColor(hero.getColor());
		fighterinfo.setLevel(hero.getLevel());
		fighterinfo.setGrade(hero.getGrade());
		fighterinfo.getBfp().setHp(hero.getArmy());
		fighterinfo.getBfp().setAttack(hero.getAttack());
		fighterinfo.getBfp().setDefend(hero.getDefend());
		fighterinfo.getBfp().setWisdom(hero.getWisdom());
		fighterinfo.setSpeed(hero.getSpeed());
		*/
		fighterinfo.setFightertype(xbean.FighterInfo.HERO);
		int skinId = 0;//BeautyRole.getEquipSkinId(roleId, fighterinfo.getHeroid()); 
		if(skinId > 0) {
//			fighterinfo.setShape(BeautyRole.getShapeWithSkinId(skinId));
		}
		
		Fighter fighter = new Fighter(fighterinfo);
		
		fighter.getAttrFighter().updateAllFinalAttrs();
		
		addEquipBuff(fighter, troop.getWeapon(), BuffConstant.BUFF_WEAPON);
		addEquipBuff(fighter, troop.getArmor(), BuffConstant.BUFF_ARMOR);
		addEquipBuff(fighter, troop.getHorse(), BuffConstant.BUFF_HORSE);
		addBindSkill(hero, fighter);//bindskill
		ItemColumn skillcol = chuhan.gsp.item.Module.getItemColumn(roleId, BagTypes.SKILL, false);
		
		/*by yanglk troop
		for(int skillitemkey : troop.getTroopInfo().getSkills())
		{
			BasicItem skillitem = skillcol.getItem(skillitemkey);
			if(skillitem == null)
				continue;
			int skillid = ((SkillItem)skillitem).getItemid();
			int level = ((SkillItem)skillitem).getSkillExtData().getLevel();
			SSkill skillcfg = ConfigManager.getInstance().getConf(SSkill.class).get(skillid);
			if(skillcfg == null)
				continue;
			addSkill(skillid, level, (int)skillcfg.skillshifang, fighter);
		}
		*/
		addViceHeroBuff(fighter,troop, herocol);
		addRelationBuff(fighter, troop);
		if(buffs != null)
		{
			for(ContinualBuff cbuff : buffs.values())
				fighter.getBuffFighter().addCBuff(cbuff.copy());
		}
		fighterinfo.setHp(Math.min(Short.MAX_VALUE, (int)fighter.getAttrFighter().getAttrById(AttrType.ARMY)));
		return fighter;
	}
	
	public static void addEquipBuff(Fighter fighter,EquipItem equip, int buffId)
	{
		if(equip != null)
		{
			ContinualBuff buff = chuhan.gsp.buff.Module.getInstance().createContinualBuff(buffId);
//			buff.getEffects().putAll(equip.getEffects());
			fighter.getBuffFighter().addCBuff(buff);
			int info = 0;//(equip.getFinalColor() << 4) + equip.getGrade(); 
			switch(buffId)
			{
			case BuffConstant.BUFF_WEAPON:
				fighter.getFighterInfo().setWeaponinfo(info);
				break;
			case BuffConstant.BUFF_ARMOR:
				fighter.getFighterInfo().setArmorinfo(info);
				break;
			case BuffConstant.BUFF_HORSE:
				fighter.getFighterInfo().setHorseinfo(info);
				break;
			}
		}
	}
	
	private static void addBindSkill(OldHero hero, Fighter fighter)
	{
		SSkill skillcfg = null;//by yanglk  heroConfigManager.getInstance().getConf(SSkill.class).get(hero.getConfig().getBindskill());
		if(skillcfg == null)
			return;
		//by yanglk  hero		addSkill(hero.getConfig().getBindskill(), hero.getHeroInfo().getSkilllv(), (int)skillcfg.skillshifang, fighter);
	}
	
	
	public static void addRelationBuff(Fighter fighter, OldTroop troop)
	{
		ContinualBuff buff = chuhan.gsp.buff.Module.getInstance().createContinualBuff(BuffConstant.BUFF_RELATION);
		for(TroopRelation relation : troop.getActiveRelations())
		{
			Float oldv =  buff.getEffects().get(relation.effectId);
			if(oldv == null)
				oldv = 0f;
			buff.getEffects().put(relation.effectId,relation.effectvalue+oldv);
		}
		fighter.getBuffFighter().addCBuff(buff);
	}
	
	public static void addViceHeroBuff(Fighter fighter, OldTroop troop, OldHeroColumn herocol)
	{
		/*by yanglk troop
		if(troop.getTroopInfo().getViceheros().isEmpty())
			return;
			*/
		int army = 0;
		int attack = 0;
		int defend = 0;
		int wisdom = 0;
		int speed = 0;
		
		/*by yanglk troop
		for(int herokey : troop.getTroopInfo().getViceheros())
		{
			Hero hero = herocol.getHero(herokey);
			if(hero == null)
				continue;
			army += hero.getArmy()/5;
			attack += hero.getAttack()/5;
			defend += hero.getDefend()/5;
			wisdom += hero.getWisdom()/5;
			speed += hero.getSpeed()/5;
		}
		*/
		ContinualBuff buff = chuhan.gsp.buff.Module.getInstance().createContinualBuff(BuffConstant.BUFF_VICE_HERO);
		buff.getEffects().put(AttrType.ARMY+1, (float)army);
		buff.getEffects().put(AttrType.ATTACK+1, (float)attack);
		buff.getEffects().put(AttrType.DEFEND+1, (float)defend);
		buff.getEffects().put(AttrType.SKILL+1, (float)wisdom);
		buff.getEffects().put(AttrType.SPEED+1, (float)speed);
		fighter.getBuffFighter().addCBuff(buff);
	}
	
	public boolean enterMonsters(List<Monster> monsters)
	{
		int fighterId = BattleUtil.GUEST_FIRST_ID;
		for(Monster monster : monsters)
		{
			if(enterMonster(fighterId, monster, battle))
			{
				fighterId++;
				if(fighterId >= (BattleUtil.GUEST_FIRST_ID+ BattleUtil.SIDE_MAX_FIGHTER))
					break;
			}
		}
		if(fighterId == BattleUtil.GUEST_FIRST_ID)
		{//一个都没加入
			return false;
		}
		return true;
	}
	
	public static boolean enterMonster(int fighterId, Monster monster, xbean.BattleInfo battle)
	{
		SBattleNPCConfig monstercfg = ConfigManager.getInstance().getConf(SBattleNPCConfig.class).get(monster.getMonsterId());
		if(monstercfg == null)
			return false;
		if(monstercfg.battletype == 1)
			fighterId = BattleUtil.GUEST_FIRST_ID + 2;//TODO 中间的位置，这部分代码是临时的，要删掉！
		if(monster.getFighterId() > 0)
			fighterId = monster.getFighterId();
		Fighter fighter = createMonsterFighter(monstercfg, battle.getBattlelevel(), battle.getEngine());
		if(fighter.getFighterId() <= 0)
			fighter.getFighterInfo().setFighterid(fighterId);
		if(fighter.getPosition() <= 0)
			fighter.getFighterInfo().setPos(fighter.getFighterId());
		battle.getFighterinfos().put(fighter.getFighterId(), fighter.getFighterInfo());
		battle.getFighters().put(fighter.getFighterId(), fighter);
		
		return true;
	}
	
	public static Fighter createMonsterFighter(SBattleNPCConfig monstercfg, int battlelevel, FightJSEngine jsengine)
	{
		xbean.FighterInfo fighterinfo = xbean.Pod.newFighterInfo();
		
		fighterinfo.setFightertype(xbean.FighterInfo.MONSTER);
		fighterinfo.setHeroid(monstercfg.getId());
		fighterinfo.setGrouptype(monstercfg.chuhan);
		fighterinfo.setColor(monstercfg.getColor());
		fighterinfo.setLevel(monstercfg.getLev());
		if(battlelevel >= 1)
		{
			fighterinfo.setLevel(battlelevel);
		}
		jsengine.setLevel(fighterinfo.getLevel());
		fighterinfo.getBfp().setHp(jsengine.evalToDouble("with(Math){"+monstercfg.getArmy()+"}").intValue());
		fighterinfo.getBfp().setAttack(jsengine.evalToDouble("with(Math){"+monstercfg.getAttack()+"}").intValue());
		fighterinfo.getBfp().setDefend(jsengine.evalToDouble("with(Math){"+monstercfg.getDefend()+"}").intValue());
		fighterinfo.getBfp().setWisdom(jsengine.evalToDouble("with(Math){"+monstercfg.getWise()+"}").intValue());
		fighterinfo.setSpeed(Integer.valueOf(monstercfg.getSpeed()));
		
		Fighter fighter = new Fighter(fighterinfo);
		fighter.getAttrFighter().updateAllFinalAttrs();
		
		fighterinfo.setHp((int)fighter.getAttrFighter().getAttrById(AttrType.ARMY));
		
		addSkill(monstercfg.skill1,1,monstercfg.prob1,fighter);
		addSkill(monstercfg.skill2,1,monstercfg.prob2,fighter);
		addSkill(monstercfg.skill3,1,monstercfg.prob3,fighter);
		
		return fighter;
	}
	
	public static void addSkill(int skillId, int level, int castrate, Fighter fighter)
	{
		
		SSkill skillcfg = ConfigManager.getInstance().getConf(SSkill.class).get(skillId);
		if(skillcfg == null)
			return;
		if(BattleUtil.isPassiveSkill(skillcfg.getId()))
		{//被动技能
			int effectId = skillcfg.attrup;
			float effectvalue = (float)(skillcfg.skillstrength + skillcfg.skillstrength_grow * level)/((effectId %10 ==2)? 100:1);
			ContinualBuff newbuff = chuhan.gsp.buff.Module.getInstance().createContinualBuff(BuffConstant.BUFF_SKILL);
			newbuff.getEffects().put(effectId, effectvalue);
			ContinualBuff buff = fighter.getBuffFighter().getBuff(BuffConstant.BUFF_SKILL);
			if(buff != null)
			{
				for(Map.Entry<Integer, Float> entry : buff.getEffects().entrySet())
				{
					if(entry.getKey() == effectId)
						newbuff.getEffects().put(effectId, effectvalue+entry.getValue());
					else
						newbuff.getEffects().put(entry.getKey(), entry.getValue());
				}
			}
			fighter.getBuffFighter().addCBuff(newbuff);
		}
		else
		{
			xbean.BattleSkill battleskill = xbean.Pod.newBattleSkill();
			battleskill.setId(skillId);
			battleskill.setLevel(level);
			battleskill.setCastrate(castrate);
			fighter.getFighterInfo().getSkills().add(battleskill);
		}
	}
	
	private void processRounds()
	{
		List<BattleDemo> rounddemos = processRound(battle,1, this.snd.hostspeed >= this.snd.guestspeed);
		if(rounddemos == null || rounddemos.isEmpty())
			return;
		snd.round1.addAll(rounddemos);
		if(battle.getBattlereulst() != BattleUtil.BATTLE_RESULT_CONTINUE)
			return;
		
		rounddemos = processRound(battle,2, this.snd.hostspeed >= this.snd.guestspeed);
		if(rounddemos == null || rounddemos.isEmpty())
			return;
		snd.round2.addAll(rounddemos);
		if(battle.getBattlereulst() != BattleUtil.BATTLE_RESULT_CONTINUE)
			return;
		
		rounddemos = processRound(battle,3, this.snd.hostspeed >= this.snd.guestspeed);
		if(rounddemos == null || rounddemos.isEmpty())
			return;
		snd.round3.addAll(rounddemos);
		
	}

	
	public static List<chuhan.gsp.battle.BattleDemo> processRound(xbean.BattleInfo battle, int round, boolean hostfirst)
	{
		battle.setRound(round);
		battle.setTurn(0);
		List<BattleDemo> rounddemo = new LinkedList<BattleDemo>();
		int battleresult = BattleUtil.BATTLE_RESULT_CONTINUE;
		int turn = 0;
		while(battleresult == BattleUtil.BATTLE_RESULT_CONTINUE)
		{
			if(turn >= BattleUtil.ROUND_MAX_TURN && round != 3)
				break;
			if(BattleUtil.checkRoundEnd(battle))
				break;
			rounddemo.addAll(processOneTurn(battle,hostfirst));
			battleresult = BattleUtil.getBattleResult(battle);
			turn++;
		}
		battle.setBattlereulst(battleresult);
		processRoundEnd(battle);
		return rounddemo;
	}
	
	/**
	 * 处理回合结束
	 * @param battle
	 */
	private static void processRoundEnd(xbean.BattleInfo battle)
	{
		List<Fighter> deadfighters = new LinkedList<Fighter>();
		for(Fighter fighter : battle.getFighters().values())
		{
			if(fighter.isDeath())
				deadfighters.add(fighter);
		}
		for(Fighter deadfighter : deadfighters)
		{
			battle.getFighterinfos().remove(deadfighter.getFighterId());
			battle.getFighters().remove(deadfighter.getFighterId());
			battle.getDeadfighters().put(deadfighter.getFighterId(), deadfighter.getFighterInfo().copy());
		}
		//重排位置
		int hostpos = BattleUtil.HOST_FIRST_ID;
		int guestpos = BattleUtil.GUEST_FIRST_ID;
		for(int i = BattleUtil.HOST_FIRST_ID;i < BattleUtil.GUEST_FIRST_ID+BattleUtil.SIDE_MAX_FIGHTER;i++)
		{
			Fighter fighter = BattleUtil.getFighterByPos(battle, i);
			if(fighter == null)
				continue;
			if(fighter.isHost())
			{
				fighter.getFighterInfo().setPos(hostpos);
				hostpos++;
			}
			else
			{
				fighter.getFighterInfo().setPos(guestpos);
				guestpos++;
			}
		}
	}
	/**
	 * 处理一轮
	 * @param battle
	 * @return
	 */
	public static List<chuhan.gsp.battle.BattleDemo> processOneTurn(xbean.BattleInfo battle, boolean hostfirst)
	{
		battle.setTurn(battle.getTurn()+1);
		List<BattleDemo> rounddemo = new LinkedList<BattleDemo>();
		
		for(int curpos : BattleUtil.getOperateSequence(battle.getRound(),hostfirst))
		{
			if(BattleUtil.checkRoundEnd(battle))
				return rounddemo;
			Fighter fighter = BattleUtil.getFighterByPos(battle, curpos);
			if(fighter == null)
				continue;
			if(fighter.isDeath())
				continue;
			if(!fighter.inBattle())
				continue;
			BattleDemo demo = processOperation(battle, fighter);
			if(demo == null)
				continue;
			rounddemo.add(demo);
			BattleDemo afterdemo =processAfterOperation(battle, demo);
			if(!afterdemo.targets.isEmpty())
				rounddemo.add(afterdemo);
		}
		
		return rounddemo;
	}
	
	public static chuhan.gsp.battle.BattleDemo processAfterOperation(xbean.BattleInfo battle, BattleDemo demo)
	{
		BattleDemo afterDieDemo = new BattleDemo();
		for(chuhan.gsp.battle.TargetDemo targetdemo : demo.targets)
		{
			if(((int)targetdemo.targetresult & ResultType.RESULT_DEATH) != 0)
			{
				Fighter deadfighter = battle.getFighters().get((int)(targetdemo.targetid));
				int pos = deadfighter.getPosition();
				Fighter intofighter = getNextFighterIntoBattle(pos, battle);
				if(intofighter == null)
					break;
				deadfighter.getFighterInfo().setPos(0);
				TargetDemo tdemo = new TargetDemo(targetdemo.targetid,Conv.toShort(intofighter.getFighterId()),(byte)0);
				afterDieDemo.targets.add(tdemo);
			}
		}
		return afterDieDemo;
	}
	
	private static Fighter getNextFighterIntoBattle(int pos, xbean.BattleInfo battle)
	{
		boolean ishost = BattleUtil.isHost(pos);
		int firstoutpos = ishost ? BattleUtil.HOST_OUTBATTLE_FIRST_ID : BattleUtil.GUEST_OUTBATTLE_FIRST_ID;
		Fighter fighter = BattleUtil.getFighterByPos(battle, firstoutpos);
		if(fighter == null)
			return null;
		fighter.getFighterInfo().setPos(pos);
		for(Fighter otherfighter : battle.getFighters().values())
		{
			if(otherfighter.getPosition() > firstoutpos && otherfighter.getPosition() < firstoutpos + BattleUtil.SIDE_MAX_OUTBATTLE)
			{
				otherfighter.getFighterInfo().setPos(otherfighter.getFighterInfo().getPos()-1);
			}
		}
		return fighter;
	}
	
	/**
	 * 预处理一轮
	 * @param battle
	 * @return
	 */
	private static List<chuhan.gsp.battle.BattleDemo> processPreOneTurn(xbean.BattleInfo battle, boolean hostfirst)
	{
		battle.setTurn(battle.getTurn()+1);
		List<BattleDemo> rounddemo = new LinkedList<BattleDemo>();
		
		for(int curpos : BattleUtil.getOperateSequence(1,hostfirst))
		{
			Fighter fighter = BattleUtil.getFighterByPos(battle, curpos);
			if(fighter == null)
				continue;
			if(fighter.isDeath())
				continue;
			if(!fighter.inBattle())
				continue;
			BattleDemo demo = processOperation(battle, fighter);
			if(demo == null)
				continue;
			rounddemo.add(demo);
		}
		
		return rounddemo;
	}
	
	private static BattleDemo processOperation(xbean.BattleInfo battle, Fighter opfighter)
	{
		battle.getEngine().setOpFighter(opfighter);
		BasicOperation operation = getOpration(battle, opfighter);
		return operation.process();
	}
	
	private static BasicOperation getOpration(xbean.BattleInfo battle, Fighter opfighter)
	{
		opfighter.getFighterInfo().getSkills();
		//随机技能
		xbean.BattleSkill xskill = opfighter.randomBattleSkill();
		
		if(xskill != null)
		{//skill
			SSkill skillcfg = ConfigManager.getInstance().getConf(SSkill.class).get(xskill.getId());
			if(skillcfg == null)
				return null;
			return new UseSkill(battle, opfighter, new BattleSkill(xskill.getId(), xskill.getLevel(), skillcfg));
		}
		else
		{//attack
			return new Attack(battle, opfighter);
		}
	}
	
	protected void processBattleEnd()
	{
		int i = 0;
		if(battle.getBattlereulst() == BattleUtil.BATTLE_RESULT_WIN)
			i = 1;
		else if(battle.getBattlereulst() == BattleUtil.BATTLE_RESULT_LOSE)
			i = -1;
		int round = 0;
		if(!snd.round1.isEmpty())
			round = 1;
		if(!snd.round2.isEmpty())
			round = 2;
		if(!snd.round3.isEmpty())
			round = 3;
		snd.result.winround = Conv.toByte(i*round);
		if(awardId > 0 && battle.getBattlereulst() == BattleUtil.BATTLE_RESULT_WIN)
			processAward();
	}
	
	protected void processAward()
	{
		awardresult = AwardManager.getInstance().distributeAllAward(roleId, awardId, null, false);
		AwardItem awarditem = awardresult.get(AwardManager.FIRSTC_AWARD);
		if(awarditem != null)
		{
			if(!awarditem.getItems().isEmpty())
			{
				chuhan.gsp.award.AddItem item = awarditem.getItems().get(0);
				snd.result.itemkey = item.getKey();
				snd.result.itemid = Conv.toShort(item.getId());
				snd.result.num = item.getNum();
			}
		}
		awarditem = awardresult.get(AwardManager.SECONDC_AWARD);
		if(awarditem != null)
		{
			if(!awarditem.getItems().isEmpty())
			{
				chuhan.gsp.award.AddItem item = awarditem.getItems().get(0);
				snd.result.itemkey = item.getKey();
				snd.result.itemid = Conv.toShort(item.getId());
				snd.result.num = item.getNum();
			}
		}
		awarditem = awardresult.get(AwardManager.EXP_AWARD);
		if(awarditem != null)
		{
			snd.result.addexp = Conv.toInt(awarditem.getValue());
		}
		awarditem = awardresult.get(AwardManager.MONEY_AWARD);
		if(awarditem != null)
		{//战斗内通过奖励表获得的钱需要特殊显示
			snd.result.itemkey = -1;
			snd.result.itemid = (short)3814;//money
			snd.result.num = Conv.toInt(awarditem.getValue());
			//snd.result.addmoney = Conv.toInt(awarditem.getValue());
		}
		
		/*for(int i = 0 ; i < 5; i ++)
		{
			awarditem = awardresult.get(AwardManager.HERO_1_EXP_AWARD + i);
			if(awarditem != null)
			{
				FighterResult fighterResult = new FighterResult((byte)(i+1), Conv.toInt(awarditem.getValue()));
				snd.result.fighters.add(fighterResult);
			}
		}*/
	}
	
	public Map<Integer, AwardItem> getAwardresult()
	{
		return awardresult;
	}
	
	public SSendBattleScript getSSendBattleScript()
	{
		return snd;
	}
	
	public xbean.BattleInfo getBattleInfo()
	{
		return battle;
	}
	
	public void sendSSendBattleScript()
	{
		xdb.Procedure.psendWhileCommit(roleId, snd);
		if(GMInterface.getGMOn() || GMInterface.gmPriv(xtable.Properties.selectUserid(roleId)))
		{
			BattleScriptReplay.addScript(roleId, snd);
		}
	}
	
	public void sendDataForTest()
	{
		if(GMInterface.getGMOn() || GMInterface.gmPriv(xtable.Properties.selectUserid(roleId)))
		{
			StringBuilder sb1 = new StringBuilder("战斗数据主@");
			StringBuilder sb2 = new StringBuilder("战斗数据客@");
			for(Fighter fighter : battle.getFighters().values())
			{
				int army = (int)fighter.getAttrFighter().getAttrById(AttrType.ARMY);
				int attack = (int)fighter.getAttrFighter().getAttrById(AttrType.ATTACK);
				int defend = (int)fighter.getAttrFighter().getAttrById(AttrType.DEFEND);
				int wisdom = (int)fighter.getAttrFighter().getAttrById(AttrType.SKILL);
				int speed = (int)fighter.getAttrFighter().getAttrById(AttrType.SPEED);
				if (fighter.isHost())
					sb1.append(fighter.getFighterId()).append(":").append(army)
							.append(",").append(attack).append(",")
							.append(defend).append(",").append(wisdom)
							.append(",").append(speed).append("; ");
				else
					sb2.append(fighter.getFighterId()).append(":").append(army)
							.append(",").append(attack).append(",")
							.append(defend).append(",").append(wisdom)
							.append(",").append(speed).append("; ");
			}
			MsgRole msgrole = MsgRole.getMsgRole(roleId, false);
			logger.debug("BattleRole:"+roleId+sb1.toString());
			logger.debug("BattleRole:"+roleId+sb2.toString());
			msgrole.addSysMsgWithSP(0, null, sb1.toString(), 0, MsgRole.MST_TYPE_SYS);
			msgrole.addSysMsgWithSP(0, null, sb2.toString(), 0, MsgRole.MST_TYPE_SYS);
		}
	}

	public boolean isNeedRelief() {
		return needRelief;
	}

	public void setNeedRelief(boolean needRelief) {
		this.needRelief = needRelief;
	}
}
