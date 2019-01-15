package chuhan.gsp.battle;

import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.util.FightJSEngine;

public class Monster {
	private final int monsterId;
	private int fighterId = -1;
	private int level = -1;
	public Monster(int monsterId) {
		this.monsterId = monsterId;
	}
	public int getFighterId() {
		return fighterId;
	}
	public void setFighterId(int fighterId) {
		this.fighterId = fighterId;
	}
	public int getLevel() {
		return level;
	}
	public void setLevel(int level) {
		this.level = level;
	}
	public int getMonsterId() {
		return monsterId;
	}
	
	
	public static Fighter createFighterByMonter(Monster monster)
	{
		SBattleNPCConfig monstercfg = ConfigManager.getInstance().getConf(SBattleNPCConfig.class).get(monster.getMonsterId());
		if(monstercfg == null)
			return null;
		int level = (monster.getLevel() > 0)? monster.getLevel() : monstercfg.getLev();
		Fighter fighter = PNewBattle.createMonsterFighter(monstercfg, level, new FightJSEngine());
		if(monster.getFighterId() > 0)
		{
			fighter.getFighterInfo().setFighterid(monster.getFighterId());
			fighter.getFighterInfo().setPos(monster.getFighterId());
		}
		return fighter;
	}
}
