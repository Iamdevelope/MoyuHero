
package xbean;

public interface BattleInfo extends xdb.Bean {
	public BattleInfo copy(); // deep clone
	public BattleInfo toData(); // a Data instance
	public BattleInfo toBean(); // a Bean instance
	public BattleInfo toDataIf(); // a Data instance If need. else return this
	public BattleInfo toBeanIf(); // a Bean instance If need. else return this

	public int getBattleid(); // 
	public int getBattlelevel(); // 
	public int getBattletype(); // 
	public java.util.Map<Integer, xbean.FighterInfo> getFighterinfos(); // key=fighterid
	public java.util.Map<Integer, xbean.FighterInfo> getFighterinfosAsData(); // key=fighterid
	public java.util.Map<Integer, chuhan.gsp.battle.Fighter> getFighters(); // key=fighterid
	public java.util.Map<Integer, xbean.FighterInfo> getDeadfighters(); // key=fighterid
	public java.util.Map<Integer, xbean.FighterInfo> getDeadfightersAsData(); // key=fighterid
	public int getBattlereulst(); // 
	public int getRound(); // 
	public int getTurn(); // 
	public chuhan.gsp.util.FightJSEngine getEngine(); // 用于本场战斗的JS引擎

	public void setBattleid(int _v_); // 
	public void setBattlelevel(int _v_); // 
	public void setBattletype(int _v_); // 
	public void setBattlereulst(int _v_); // 
	public void setRound(int _v_); // 
	public void setTurn(int _v_); // 
	public void setEngine(chuhan.gsp.util.FightJSEngine _v_); // 用于本场战斗的JS引擎
}
