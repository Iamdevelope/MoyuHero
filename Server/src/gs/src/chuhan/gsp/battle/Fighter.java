package chuhan.gsp.battle;


import chuhan.gsp.attr.AttrFighter;
import chuhan.gsp.attr.GroupType;
import chuhan.gsp.buff.BuffConstant;
import chuhan.gsp.buff.BuffFighter;
import chuhan.gsp.item.SHero;
import chuhan.gsp.item.SSkill;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.util.Conv;

public class Fighter {
	
	private xbean.FighterInfo fighterInfo;
	private BuffFighter buffFighter;
	private AttrFighter attrFighter;
	
	public Fighter(xbean.FighterInfo fighterInfo)
	{
		this.fighterInfo = fighterInfo;
//		this.buffFighter = new BuffFighter(fighterInfo);
//		this.attrFighter = new AttrFighter(fighterInfo);
	}
	
	public int getFighterId()
	{
		return fighterInfo.getFighterid();
	}
	
	/**
	 * 获取战斗者位置
	 * @return -1 则未找到位置
	 */
	public int getPosition()
	{
		
		return fighterInfo.getPos();
	}
	
	public boolean canOperation()
	{
		if(isDeath())
			return false;
		if(!inBattle())
			return false;
		return true;
	}
	
	public int getGroupType()
	{
		return fighterInfo.getGrouptype();
	}
	
	public double getGroupAmendByAim(int targettype)
	{
		if(getGroupType() == targettype)
			return 1;
		if(getGroupType() == GroupType.NULL || targettype == GroupType.NULL)
			return 1;
		if(getGroupType() == GroupType.CHU)
		{
			if(targettype == GroupType.HAN)
				return 0.8;
			if(targettype == GroupType.QUN)
				return 1.2;
		}
		if(getGroupType() == GroupType.HAN)
		{
			if(targettype == GroupType.QUN)
				return 0.8;
			if(targettype == GroupType.CHU)
				return 1.2;
		}
		if(getGroupType() == GroupType.QUN)
		{
			if(targettype == GroupType.CHU)
				return 0.8;
			if(targettype == GroupType.HAN)
				return 1.2;
		}
		return 1;
	}
	
	public BuffFighter getBuffFighter()
	{
		return buffFighter;
	}
	
	public AttrFighter getAttrFighter()
	{
		return attrFighter;
	}
	
	public xbean.FighterInfo getFighterInfo()
	{
		return fighterInfo;
	}
	
	public int getFighterType()
	{
		return fighterInfo.getFightertype();
	}
	
	public boolean isDeath()
	{
		return buffFighter.existBuff(BuffConstant.BUFF_DEATH);
	}
	
	public boolean isHost()
	{
		return BattleUtil.isHost(getFighterId());
	}
	
	public boolean inBattle()
	{
		return BattleUtil.inBattle(getPosition());
	}
	
	public void attachHpChange(int v)
	{
		attrFighter.addHp(v);
	}
	
	public chuhan.gsp.battle.FighterInfo getProtocolFighter()
	{
		chuhan.gsp.battle.FighterInfo pfighter = new chuhan.gsp.battle.FighterInfo();
		pfighter.fighterid = Conv.toByte(getFighterId());
		pfighter.heroid = fighterInfo.getHeroid();
		pfighter.hp = fighterInfo.getHp();
		pfighter.colorgrade = (byte)((fighterInfo.getColor() << 4) + fighterInfo.getGrade());
		pfighter.weapon = Conv.toByte(fighterInfo.getWeaponinfo());
		pfighter.armor = Conv.toByte(fighterInfo.getArmorinfo());
		pfighter.horse = Conv.toByte(fighterInfo.getHorseinfo());
		pfighter.shape = Conv.toByte(fighterInfo.getShape());
		return pfighter;
	}
	
	public xbean.BattleSkill randomBattleSkill()
	{
		xbean.BattleSkill randomSkill = null;
		int maxpower = 0;
		for(xbean.BattleSkill xskill : fighterInfo.getSkills())
		{
			if(Math.random() > (double)xskill.getCastrate()/1000)
				continue;
			SSkill skillcfg = ConfigManager.getInstance().getConf(SSkill.class).get(xskill.getId());
			if(skillcfg == null)
				continue;
			if(BattleUtil.isPassiveSkill(skillcfg.getId()))
				continue;
			int power = (int)(skillcfg.skillstrength + skillcfg.skillstrength_grow * xskill.getLevel());
			if(power < maxpower)
				continue;
			randomSkill = xskill;
			maxpower = power;
		}
		return randomSkill;
	}
	
	
	public SBattleNPCConfig getSBattleNPCConfig()
	{
		if(fighterInfo.getFightertype() != xbean.FighterInfo.MONSTER)
			return null;
		SBattleNPCConfig monstercfg = ConfigManager.getInstance().getConf(SBattleNPCConfig.class).get(fighterInfo.getHeroid());
		return monstercfg;
	}
	
	public SHero getSHeroConfig()
	{
		if(fighterInfo.getFightertype() != xbean.FighterInfo.HERO)
			return null;
		SHero cfg = ConfigManager.getInstance().getConf(SHero.class).get(fighterInfo.getHeroid());
		return cfg;
	}
	
	
}
