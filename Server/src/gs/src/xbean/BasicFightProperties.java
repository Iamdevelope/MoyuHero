
package xbean;

public interface BasicFightProperties extends xdb.Bean {
	public BasicFightProperties copy(); // deep clone
	public BasicFightProperties toData(); // a Data instance
	public BasicFightProperties toBean(); // a Bean instance
	public BasicFightProperties toDataIf(); // a Data instance If need. else return this
	public BasicFightProperties toBeanIf(); // a Bean instance If need. else return this

	public float getHp(); // 
	public float getAttack(); // 
	public float getDefend(); // 
	public float getWisdom(); // 

	public void setHp(float _v_); // 
	public void setAttack(float _v_); // 
	public void setDefend(float _v_); // 
	public void setWisdom(float _v_); // 
}
