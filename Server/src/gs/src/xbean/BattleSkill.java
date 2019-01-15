
package xbean;

public interface BattleSkill extends xdb.Bean {
	public BattleSkill copy(); // deep clone
	public BattleSkill toData(); // a Data instance
	public BattleSkill toBean(); // a Bean instance
	public BattleSkill toDataIf(); // a Data instance If need. else return this
	public BattleSkill toBeanIf(); // a Bean instance If need. else return this

	public int getId(); // 
	public int getLevel(); // 
	public int getCastrate(); // 以千为底

	public void setId(int _v_); // 
	public void setLevel(int _v_); // 
	public void setCastrate(int _v_); // 以千为底
}
